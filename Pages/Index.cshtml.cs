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

        [BindProperty]
        [Required]
        [ValidateDrivingLicence]
        public string DrivingLicenceNumber { get; set; }

        [BindProperty]
        [Required]
        [FutureDate(6)]
        public DateTime ExpiryDate { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

    public class ValidateDrivingLicenceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var licenceNumber = value as string;
            var regex = new System.Text.RegularExpressions.Regex("Your UK Driving Licence Regex Here");

            if (regex.IsMatch(licenceNumber))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid UK Driving Licence Number.");
        }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        private readonly int _monthsInFuture;

        public FutureDateAttribute(int monthsInFuture)
        {
            _monthsInFuture = monthsInFuture;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Now.AddMonths(_monthsInFuture))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Expiry date should be at least 6 months in the future.");
        }
    }
}