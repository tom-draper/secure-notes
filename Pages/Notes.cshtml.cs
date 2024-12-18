using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecureNotes.Pages;

    public class Note 
    {
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
    }

    public class NotesModel : PageModel
    {
        public string NoteID { get; set; }

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
    }
