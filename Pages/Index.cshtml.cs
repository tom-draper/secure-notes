using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using SecureNotes.Helpers.Cryptography;
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
        NoteID = string.Empty;
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(NoteID))
        {
            ModelState.AddModelError(string.Empty, "Note ID cannot be empty.");
            return Page(); // Stay on the same page and display error
        }

        // Generate a hash from the Note ID
        string encryptedText = Cryptography.EncryptString(NoteID);

        // Log the hash generation
        _logger.LogInformation("Generated hash: {hash}", encryptedText);

        // Set a session flag indicating valid access
        HttpContext.Session.SetString("ValidSearch", "true");

        // Redirect to the Notes page with the hash
        return RedirectToPage("/notes", new { hash = encryptedText });
    }

    private string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

}
