using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public IActionResult? OnPost()
    {
        string? noteID = NoteID;

        // Handle the button click here
        Console.WriteLine("Button was clicked!");

        Console.WriteLine(noteID);

        if (string.IsNullOrEmpty(noteID))
        {
            Console.WriteLine("NoteID is empty!");
            return null;
        }

        return RedirectToPage("/notes", new { NoteID = noteID });
    }
}
