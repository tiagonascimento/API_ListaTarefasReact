using ListaTarefas.Utils.cryptography.cryptosettings;
using Microsoft.Extensions.Options;


namespace ListaTarefas.Utils.cryptography
{
    public interface ICrypto
    {
        public string EncryptionString(string texto);       
    
    }
}
