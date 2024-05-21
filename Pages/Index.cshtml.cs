using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebAppDemo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    [Display(Name = "Driving Licence Number")]
    public string DrivingLicenceNumber { get; set; }

    [BindProperty]
    [Display(Name = "Expiry Date")]
    public DateTime ExpiryDate { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public void OnPost()
    {
        if (ModelState.IsValid)
        {
            bool isLicenceValid = ValidateDrivingLicence(DrivingLicenceNumber);
            bool isExpiryDateValid = ValidateExpiryDate(ExpiryDate);
            if (!isLicenceValid)
            {
                ModelState.AddModelError("", "Invalid Driving Licence Number.");
            }
            if (!isExpiryDateValid)
            {
                ModelState.AddModelError("", "Expiry Date should be at least 6 months from now.");
            }
        }
    }

    public bool ValidateDrivingLicence(string drivingLicence)
    {
        var licenceRegex = @"^[A-Z9]{5}\d{6}[A-Z9]\d{2}$";
        return Regex.IsMatch(drivingLicence, licenceRegex);
    }

    private bool ValidateExpiryDate(DateTime expiryDate)
    {
        // Check if the expiry date is at least 6 months from now
        return expiryDate > DateTime.Now.AddMonths(6);
    }
}
