using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecureNotes.Helpers.Cryptography;

namespace SecureNotes.Pages;

public class Note
{
    public DateTime Timestamp { get; set; }
    public string Content { get; set; }
}

public class NotesModel : PageModel
{
    public string NoteID { get; set; }

    [BindProperty]
    public string? NoteContent { get; set; }  // This binds the input field value to the NoteID property

    // This method is automatically called when the page is requested with a route parameter
    public void OnGet(string NoteID)
    {
        // The NoteID parameter will be automatically passed from the URL
        this.NoteID = NoteID;
    }

    public List<Note> GetNotes(string NoteID)
    {
        return new List<Note>
            {
                new Note { Timestamp = DateTime.Now, Content = "This is a note" },
                new Note { Timestamp = DateTime.Now, Content = "This is another note" }
            };
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
