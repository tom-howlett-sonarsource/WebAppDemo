using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace WebAppDemo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public string DrivingLicence { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        if (!ValidateDrivingLicence(DrivingLicence))
        {
            ModelState.AddModelError("DrivingLicence", "Invalid driving licence number");
            return Page();
        }

        // The driving licence number is valid. You can add your code here.

        return RedirectToPage("./Index");
    }

    public bool ValidateDrivingLicence(string drivingLicence)
    {
        var licenceRegex = @"^[A-Z9]{5}\d{6}[A-Z9]\d{2}$";
        return Regex.IsMatch(drivingLicence, licenceRegex);
    }
}
