using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebAppDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty, Required]
        public string DrivingLicenceNumber { get; set; }

        [BindProperty, Required, DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ValidateDrivingLicence(DrivingLicenceNumber))
            {
                ModelState.AddModelError("DrivingLicenceNumber", "Invalid UK driving licence number.");
                return Page();
            }

            if (!ValidateExpiryDate(ExpiryDate))
            {
                ModelState.AddModelError("ExpiryDate", "Expiry date should be at least 6 months from now.");
                return Page();
            }

            // Continue processing...
            return RedirectToPage("./Success");
        }

        private bool ValidateDrivingLicence(string drivingLicenceNumber)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[1-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{5,}$");
            return regex.IsMatch(drivingLicenceNumber);
        }

        private bool ValidateExpiryDate(DateTime expiryDate)
        {
            return expiryDate > DateTime.Now.AddMonths(6);
        }
    }
}