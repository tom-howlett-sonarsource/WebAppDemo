using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace WebAppDemo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public string Email { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        if (!ValidateEmail(Email))
        {
            ModelState.AddModelError("Email", "Invalid email format");
            return Page();
        }

        // The email is valid. You can add your code here.

        return RedirectToPage("./Index");
    }

    public bool ValidateEmail(string email)
    {
        var emailRegex = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
        return Regex.IsMatch(email, emailRegex);
    }
}
