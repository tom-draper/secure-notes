using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecureNotes.Helpers.Cryptography;

namespace SecureNotes.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    [BindProperty]
    public string? NoteID { get; set; }

    private readonly ILogger<IndexModel> _logger = logger;

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

        // Encrypt the note ID for URL
        string encryptedNoteID = Cryptography.EncryptString(NoteID);
        // Generate a hash from the note ID to prove search validity
        string hash = Cryptography.GenerateHash(NoteID);

        // Set a session flag indicating valid access
        HttpContext.Session.SetString("SecureNoteHash", hash);

        // Redirect to the Notes page with the hash
        return RedirectToPage("/notes", new { encryptedNoteID = encryptedNoteID });
    }
}