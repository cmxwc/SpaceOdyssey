using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class PasswordManager
{
    string key = "adehan@@hdhdSYAJSKjs!k";
    string iv = "127djjdh28938";

    // Encryption of password
    public static string ConvertToEncrypt(string password)
    {
        AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();
        acsp.Key = Encoding.ASCII.GetBytes(key);
        acsp.IV = Encoding.ASCII.GetBytes(iv);
        acsp.BlockSize = 128;
        acsp.KeySize = 256;
        acsp.Mode = CipherMode.CBC;
        acsp.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = Encoding.ASCII.GetBytes(password);
        ICryptoTransform trnsfrm = acsp.CreateEncryptor(acsp.Key, acsp.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }

    // Decryption of password
    public static string ConvertToDecrypt(string inputData)
    {
        AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();
        acsp.Key = Encoding.ASCII.GetBytes(key);
        acsp.IV = Encoding.ASCII.GetBytes(iv);
        acsp.BlockSize = 128;
        acsp.KeySize = 256;
        acsp.Mode = CipherMode.CBC;
        acsp.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = Convert.FromBase64String(inputData);
        ICryptoTransform trnsfrm = acsp.CreateDecryptor();

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Encoding.ASCII.GetString(result);
    }

}