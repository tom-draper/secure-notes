using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecureNotes.Helpers.Cryptography;
using SecureNotes.Helpers.Database;

namespace SecureNotes.Pages
{
    public class Note
    {
        public required DateTime? Timestamp { get; set; }
        public required string Content { get; set; }
    }

    public class NotesModel(ApplicationDbContext context) : PageModel
    {
        [BindProperty]
        public string? NoteContent { get; set; }  // This binds the input field value to the NoteContent property

        public string? NoteID { get; set; }

        private readonly ApplicationDbContext _context = context;

        public string GetNoteID()
        {
            return NoteID ?? string.Empty;
        }

        public List<Note> GetNotes(string noteID)
        {
            try
            {
                // Get the note records from the database
                var entities = _context.Notes
                                        .Where(note => note.NoteID == noteID)
                                        .OrderByDescending(note => note.Timestamp)
                                        .ToList();

                // Map NoteRecord to Note
                var notes = entities.Select(entity => new Note
                {
                    Timestamp = entity.Timestamp,
                    Content = entity.Content
                }).ToList();

                return notes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: failed to read notes\n" + ex.Message);
                return [];
            }
        }

        // OnGet is called when a GET request is made
        public IActionResult? OnGet(string encryptedNoteID)
        {
            Console.WriteLine("Getting page...");
            if (string.IsNullOrEmpty(encryptedNoteID))
                return RedirectToPage("/Index");

            NoteID = DecodeEncryptedNoteID(encryptedNoteID);

            string hash = Cryptography.GenerateHash(NoteID);

            // Check if the session hash exists to avoid direct access
            string storedHash = HttpContext.Session.GetString("SecureNoteHash") ?? string.Empty;
            HttpContext.Session.Remove("SecureNoteHash");
            if (hash != storedHash)
                return RedirectToPage("/Index");

            return null;
        }

        // Decode the encrypted NoteID
        private static string DecodeEncryptedNoteID(string encryptedNoteID)
        {
            // Convert the URL-encoded string to a normal string
            string decodedURL = Uri.UnescapeDataString(encryptedNoteID);
            string noteID = Cryptography.DecryptString(decodedURL);
            return noteID;
        }

        // OnPost is called when a POST request is made (when the form is submitted)
        public IActionResult? OnPost(string encryptedNoteID)
        {
            Console.WriteLine("Posting note...");
            string noteID;
            try
            {
                noteID = DecodeEncryptedNoteID(encryptedNoteID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }

            // Ensure NoteID is not empty
            if (string.IsNullOrEmpty(noteID))
            {
                // Log or handle the error if needed
                Console.WriteLine("Note ID is empty!");
                return null;  // Return the same page if NoteID is empty
            }

            // Ensure the note content is not empty
            if (string.IsNullOrEmpty(NoteContent))
            {
                // Log or handle the error if needed
                Console.WriteLine("Note content is empty!");
                return null;  // Return the same page if content is empty
            }

            // Create a new NoteRecord entity to store the note
            var newNote = new NoteRecord
            {
                NoteID = noteID,  // Use the existing NoteID
                Content = NoteContent,
                Timestamp = DateTime.UtcNow  // Set the current UTC timestamp
            };

            try
            {
                // Add the new note record to the context
                _context.Notes.Add(newNote);

                // Save the changes to the database
                _context.SaveChanges();

                // Optionally, redirect to a page showing the new note or a confirmation page
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                // Log the error and return an error message if something goes wrong
                Console.WriteLine("Error: " + ex.Message);
                return null;  // Return to the same page with an error message if something fails
            }
        }
    }
}
