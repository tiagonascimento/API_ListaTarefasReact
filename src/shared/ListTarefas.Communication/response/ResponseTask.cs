
namespace ListTarefas.Communication.response
{
    public class ResponseTask
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
          
    }
}
