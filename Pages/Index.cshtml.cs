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

    private string _textInput;
    [BindProperty]
    public string TextInput
    {
        get { return _textInput; }
        set
        {
            _textInput = value;
            _logger.LogInformation("TextInput has been updated to: {TextInput}", _textInput);
        }
    }

    public string FindFolder(string rootPath)
    {
        try
        {
            var directories = Directory.GetDirectories(rootPath, TextInput, SearchOption.AllDirectories);
            return directories.Length > 0 ? directories[0] : "No match found";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while searching for the directory.");
            return "Error occurred";
        }
    }


    [BindProperty]    
    public string EncryptedText { get; set; }

    public void OnGet()
    {
    }

    public int AddNumber(int number)
    {
        int result = number + 2147;
        return result;
    }

    public static string GenerateHashWithFixedSalt(string inputText)
{
    // Define a fixed salt
    string salt = "your-fixed-salt"; // Replace with your fixed salt

    using (SHA256 sha256Hash = SHA256.Create())
    {
        // Combine the input string and the salt
        var combinedInputs = String.Concat(inputText, salt);

        // Convert the combined inputs to a byte array and compute the hash.
        byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(combinedInputs));

        // Create a new StringBuilder to collect the bytes and create a string.
        var sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
}

    public IActionResult OnPost()
    {
        /*if(TextInput != null)
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
        }*/

        if (!string.IsNullOrEmpty(TextInput))
        {
            Response.Cookies.Append("TextInput", TextInput);
            return Redirect(TextInput);
        }
        return Page();
     
       
    }
}