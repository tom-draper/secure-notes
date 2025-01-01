using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecureNotes.Helpers.Cryptography;
using SecureNotes.Helpers.Database;
using System;
using System.Linq;

namespace SecureNotes.Pages
{
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
            return NoteID ?? string.Empty;
        }

        public List<Note> GetNotes(string noteID)
        {
            try
            {
                // Get the note records from the database
                var entities = _context.Notes
                                        .Where(note => note.NoteID == noteID)
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
                return new List<Note>();
            }
        }

        // OnGet is called when a GET request is made
        public IActionResult? OnGet(string hash)
        {
            Console.WriteLine("Getting page...");

            if (string.IsNullOrEmpty(hash))
                return RedirectToPage("/Index");

            // Check if the session variable exists
            var validSearch = HttpContext.Session.GetString("ValidSearch");
            if (validSearch != "true")
                return RedirectToPage("/Index");

            // Clear the session flag after successful access
            HttpContext.Session.Remove("ValidSearch");

            NoteID = DecodeEncryptedNoteID(hash);
            Console.WriteLine("Note ID: " + NoteID);

            return Page();
        }

        // Decode the encrypted NoteID
        private string DecodeEncryptedNoteID(string encryptedNoteID)
        {
            // Convert the URL-encoded string to a normal string
            string decodedURL = System.Uri.UnescapeDataString(encryptedNoteID);
            string noteID = Cryptography.DecryptString(decodedURL);
            return noteID;
        }

        // OnPost is called when a POST request is made (when the form is submitted)
        public IActionResult? OnPost(string hash)
        {
            Console.WriteLine("Posting note...");
            string noteID = DecodeEncryptedNoteID(hash);

            // Ensure NoteID is not empty
            if (string.IsNullOrEmpty(noteID))
            {
                // Log or handle the error if needed
                Console.WriteLine("Note ID is empty!");
                return Page();  // Return the same page if NoteID is empty
            }

            // Ensure the note content is not empty
            if (string.IsNullOrEmpty(NoteContent))
            {
                // Log or handle the error if needed
                Console.WriteLine("Note content is empty!");
                return Page();  // Return the same page if content is empty
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
                return Page();  // Return to the same page with an error message if something fails
            }
        }
    }
}
