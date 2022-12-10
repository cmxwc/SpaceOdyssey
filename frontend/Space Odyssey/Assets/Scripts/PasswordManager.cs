using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class PasswordManager
{
    // string key = "A60A5770FE5E7AB200BA9CFC94E4E8B0";
    // string iv = "1234567887654321";

    // // Encryption of password
    // public string ConvertToEncrypt(string password)
    // {
    //     AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();
    //     acsp.Key = Encoding.ASCII.GetBytes(key);
    //     acsp.IV = Encoding.ASCII.GetBytes(iv);
    //     acsp.BlockSize = 128;
    //     acsp.KeySize = 256;
    //     acsp.Mode = CipherMode.CBC;
    //     acsp.Padding = PaddingMode.PKCS7;

    //     byte[] txtByteData = Encoding.ASCII.GetBytes(password);
    //     ICryptoTransform trnsfrm = acsp.CreateEncryptor(acsp.Key, acsp.IV);

    //     byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
    //     return Convert.ToBase64String(result);
    // }

    // // Decryption of password
    // public string ConvertToDecrypt(string inputData)
    // {
    //     AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();
    //     acsp.Key = Encoding.ASCII.GetBytes(key);
    //     acsp.IV = Encoding.ASCII.GetBytes(iv);
    //     acsp.BlockSize = 128;
    //     acsp.KeySize = 256;
    //     acsp.Mode = CipherMode.CBC;
    //     acsp.Padding = PaddingMode.PKCS7;

    //     byte[] txtByteData = Convert.FromBase64String(inputData);
    //     ICryptoTransform trnsfrm = acsp.CreateDecryptor();

    //     byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
    //     return Encoding.ASCII.GetString(result);
    // }

    public string ConvertToEncrypt(string password)
    {
        using (Aes aes = Aes.Create())
        {
            aes.GenerateKey();
            aes.GenerateIV();

            // Create a encryptor to perform the stream transform
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            // Create the streams used for encryption
            using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (System.IO.StreamWriter swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream
                        swEncrypt.Write(password);
                    }

                    // Return the encrypted data as a byte array
                    byte[] result = msEncrypt.ToArray();
                    return Convert.ToBase64String(result);
                }
            }
        }
    }

    public string ConvertToDecrypt(string inputData)
    {
        byte[] encrypted = Convert.FromBase64String(inputData);
        // Create a new instance of the Aes class
        // and generate a random key and IV
        using (Aes aes = Aes.Create())
        {
            aes.GenerateKey();
            aes.GenerateIV();

            // Create a decryptor to perform the stream transform
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // Create the streams used for decryption

            using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(encrypted))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and return them as a string
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }



}