
namespace ListTarefas.Communication.response
{
    public class ResponseException
    {
        public IList<string> ErrorMessage { get; set; }
        public ResponseException(IList<string> errors)
        {
            ErrorMessage = errors;
        }
        public ResponseException(string erro)
        {
            ErrorMessage = [erro];
        }
    }
}
