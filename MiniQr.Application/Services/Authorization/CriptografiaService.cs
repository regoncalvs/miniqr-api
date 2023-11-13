using Microsoft.Extensions.Configuration;
using MiniQr.Utils.Constants;
using MiniQr.Utils.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace MiniQr.Application.Services.Authorization
{
    public class CriptografiaService
    {
        private readonly IConfiguration _configuration;

        public CriptografiaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Descriptografar(string senhaCriptografada)
        {
            string senhaDescriptografada;
            byte[] fixedIV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var criptografiaKey = _configuration[ConfiguracaoConstants.CriptografiaKey] ?? throw new ConfiguracaoAusenteException(ConfiguracaoConstants.CriptografiaKey);
            ;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(criptografiaKey);
                aesAlg.IV = fixedIV;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(senhaCriptografada)))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    senhaDescriptografada = srDecrypt.ReadToEnd();
                }
            }

            return senhaDescriptografada;
        }
    }
}
