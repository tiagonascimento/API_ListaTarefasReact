
using ListaTarefas.Utils.cryptography.cryptosettings;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ListaTarefas.Utils.cryptography
{
    public class Crypto: ICrypto
    {
        private readonly string _encryptionKey;
        public Crypto(IOptions<CryptoSettings> options)
        {
            _encryptionKey = options.Value.EncryptionKey;
        }


        public string EncryptionString(string texto)
        {
            var chave = _encryptionKey;
            var textoNovo = $"{texto}{chave}";
            var bytes = Encoding.UTF8.GetBytes(textoNovo);
            var hashBites = SHA512.Create().ComputeHash(bytes);
            return StringBytes(hashBites);
        }
        private static string StringBytes(byte[] bites)
        {
            var sb = new StringBuilder();
            foreach (byte bite in bites)
            {
                var hex = bite.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
