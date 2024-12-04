using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebAppDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        [Required(ErrorMessage = "Driving Licence Number is required")]
        public string DrivingLicenceNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Expiry Date is required")]
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
                if (!ValidateDrivingLicence(DrivingLicenceNumber))
                {
                    ModelState.AddModelError("DrivingLicenceNumber", "Invalid UK Driving Licence Number");
                }

                if (!ValidateExpiryDate(ExpiryDate))
                {
                    ModelState.AddModelError("ExpiryDate", "Expiry Date should be at least 6 months from now");
                }
            }
        }

        private bool ValidateDrivingLicence(string licenceNumber)
        {
            // UK Driving Licence Number regex
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)[A-Z\d]{16}$");
            return regex.IsMatch(licenceNumber);
        }

        private bool ValidateExpiryDate(DateTime expiryDate)
        {
            // Check if the expiry date is at least 6 months from now
            return expiryDate > DateTime.Now.AddMonths(6);
        }
    }
}