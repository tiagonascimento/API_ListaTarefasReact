namespace ListTarefas.Communication.response
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public ResponseUser user { get; set; }
        public string Message { get; set; }
    }
}
