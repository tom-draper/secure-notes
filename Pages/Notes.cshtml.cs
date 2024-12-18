using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecureNotes.Pages;

public class NotesModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public string Slug = string.Empty;

    public NotesModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
