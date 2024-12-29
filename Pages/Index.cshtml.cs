using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace SecureNotes.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string? NoteID { get; set; }  // This binds the input field value to the NoteID property
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    // public IActionResult OnPost(string noteID)
    // {
    //     Console.WriteLine("Button was clicked!");
    //     Console.WriteLine(noteID);
    //     if (string.IsNullOrEmpty(noteID))
    //         return RedirectToPage("/Index"); // Redirect back if noteID is missing

    //     // Generate a hash from the note ID
    //     using (var sha256 = System.Security.Cryptography.SHA256.Create())
    //     {
    //         var bytes = System.Text.Encoding.UTF8.GetBytes(noteID);
    //         var hash = sha256.ComputeHash(bytes);
    //         var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();

    //         // Set a session flag indicating the user accessed through the search bar
    //         HttpContext.Session.SetString("ValidSearch", "true");

    //         // Redirect to the hashed page
    //         return Redirect($"/notes/{hashString}");
    //     }
    // }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(NoteID))
        {
            ModelState.AddModelError(string.Empty, "Note ID cannot be empty.");
            return Page(); // Stay on the same page and display error
        }

        // Generate a hash from the Note ID
        string hash = GenerateHash(NoteID);

        // Log the hash generation
        _logger.LogInformation("Generated hash: {Hash}", hash);

        // Set a session flag indicating valid access
        HttpContext.Session.SetString("ValidSearch", "true");

        // Redirect to the Notes page with the hash
        return RedirectToPage("/notes", new { Hash = hash });
    }

    private string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    // public IActionResult? OnPost()
    // {
    //     string? noteID = NoteID;

    //     // Handle the button click here
    //     Console.WriteLine("Button was clicked!");

    //     Console.WriteLine(noteID);

    //     if (string.IsNullOrEmpty(noteID))
    //     {
    //         Console.WriteLine("NoteID is empty!");
    //         return null;
    //     }

    //     return RedirectToPage("/notes", new { NoteID = noteID });
    // }
}
