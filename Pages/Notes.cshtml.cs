using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using SecureNotes.Helpers.Cryptography;
using SecureNotes.Helpers.Database;

namespace SecureNotes.Pages;

public class Note
{
    public required DateTime? Timestamp { get; set; }
    public required string Content { get; set; }
}

public class NotesModel : PageModel
{
    [BindProperty]
    public string? NoteContent { get; set; }  // This binds the input field value to the NoteContent property

    public string? NoteID { get; set; }

    private readonly ApplicationDbContext _context;

    public NotesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public string GetNoteID()
    {
        return NoteID;
    }

    public List<Note> GetNotes(string NoteID)
    {
        try
        {
            // Get the note records from the database
            IList<NoteRecord> entities = _context.Notes
                                    .Where(note => note.NoteID == NoteID)
                                    .ToList();

            // Map NoteRecord to Note
            List<Note> notes = entities.Select(entity => new Note
            {
                Timestamp = entity.Timestamp,
                Content = entity.Content
            }).ToList();

            return notes;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: failed to read notes\n" + ex.Message);
            return new List<Note>();
        }
    }

    public IActionResult OnGet(string hash)
    {
        if (string.IsNullOrEmpty(hash))
            return RedirectToPage("/Index");

        // Check if the session variable exists
        var validSearch = HttpContext.Session.GetString("ValidSearch");
        if (validSearch != "true")
            return RedirectToPage("/Index");

        // Clear the session flag after successful access
        HttpContext.Session.Remove("ValidSearch");

        NoteID = DecodeEncryptedNoteID(hash);

        return Page();
    }

    private string DecodeEncryptedNoteID(string encryptedNoteID)
    {
        // Convert the URL-encoded string to a normal string
        string decodedURL = System.Uri.UnescapeDataString(encryptedNoteID);
        string noteID = Cryptography.DecryptString(decodedURL);
        return noteID;
    }

    public IActionResult OnPost()
    {
        Console.WriteLine("Send button was clicked!");

        Console.WriteLine(NoteContent);

        if (string.IsNullOrEmpty(NoteContent))
        {
            Console.WriteLine("Note content is empty!");
            return Page();
        }

        return RedirectToPage("Index");
    }
}
