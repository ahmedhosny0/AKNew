using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace CK.Models.CKPro;

public partial class Storeuser
{
    public string? Inventlocation { get; set; }

    public string? Storenumber { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Server { get; set; }

    public string? RmsstoNumber { get; set; }

    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Dbase { get; set; }

    public string? PriceCategory { get; set; }

    public string? Franchise { get; set; }

    public string? Company { get; set; }

    public string? Zkip { get; set; }

    public string? StartDate { get; set; }

    public string? ArabicN { get; set; }

    public string? District { get; set; }

    public string? Dmanager { get; set; }

    public string? Fmanager { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public int? BranchStatus { get; set; }

    public string? BranchOwner { get; set; }

    public int? Delivery { get; set; }
    [NotMapped]
    public string? DecryptedPassword { get; set; }
    public static User Encrypt(User User)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(User.Password);


        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                User.Password = Convert.ToBase64String(ms.ToArray());
            }
        }
        return User;
    }
    public static User Decrypt(User User)
    {
        string DecryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Convert.FromBase64String(User.Password);


        using (Aes decryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(DecryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            decryptor.Key = pdb.GetBytes(32);
            decryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                User.Password = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return User;
    }
}
