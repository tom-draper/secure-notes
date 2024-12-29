using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using SecureNotes.Helpers.Cryptography;

namespace SecureNotes.Pages;

public class Note
{
    public required DateTime Timestamp { get; set; }
    public required string Content { get; set; }
}

public class NotesModel : PageModel
{
    [BindProperty]
    public string? NoteContent { get; set; }  // This binds the input field value to the NoteID property

    public string? NoteID { get; set; }

    public List<Note> GetNotes(string NoteID)
    {
        return new List<Note>
            {
                new Note { Timestamp = DateTime.Now, Content = "This is a note" },
                new Note { Timestamp = DateTime.Now, Content = "This is another note" }
            };
    }

    public IActionResult? OnGet(string hash)
    {
        if (string.IsNullOrEmpty(hash))
            return RedirectToPage("/Index");

        // Check if the session variable exists
        var validSearch = HttpContext.Session.GetString("ValidSearch");
        if (validSearch != "true")
            return RedirectToPage("/Index");

        // Clear the session flag after successful access
        HttpContext.Session.Remove("ValidSearch");

        string decodedURL = System.Uri.UnescapeDataString(hash);
        NoteID = Cryptography.DecryptString(decodedURL);

        return Page();
    }

    public IActionResult? OnPost()
    {
        string? noteContent = NoteContent;

        // Handle the button click here
        Console.WriteLine("Send button was clicked!");

        Console.WriteLine(noteContent);

        if (string.IsNullOrEmpty(noteContent))
        {
            Console.WriteLine("Note content is empty!");
            return null;
        }

        return RedirectToPage("Index");
    }
}
