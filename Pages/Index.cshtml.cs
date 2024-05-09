using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppDemo.Pages;
using System.Security.Cryptography;
using System.Text;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("your-encryption-key-here"); // Replace with your key
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("your-iv-here3456"); // Replace with your IV

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public string TextInput { get; set; }


    [BindProperty]    
    public string EncryptedText { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if(TextInput != null)
        {
             using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(TextInput);
                    }

                    var encrypted = msEncrypt.ToArray();
                    EncryptedText = Encoding.UTF8.GetString(encrypted);
                }
            }
        }
       
    }
}