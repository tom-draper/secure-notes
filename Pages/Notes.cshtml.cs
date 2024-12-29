using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace SecureNotes.Pages;

public class Note
{
    public DateTime Timestamp { get; set; }
    public string Content { get; set; }
}

public class NotesModel : PageModel
{
    [BindProperty]
    public string? NoteContent { get; set; }  // This binds the input field value to the NoteID property

    public string NoteID { get; set; }

    // This method is automatically called when the page is requested with a route parameter
    // public void OnGet(string NoteID)
    // {
    //     // The NoteID parameter will be automatically passed from the URL
    //     this.NoteID = NoteID;
    // }


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

        NoteID = DecodeHash(hash);

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

    private string DecodeHash(string hash)
    {
        byte[] data = Convert.FromBase64String(hash);
        return Encoding.UTF8.GetString(data);
    }
}