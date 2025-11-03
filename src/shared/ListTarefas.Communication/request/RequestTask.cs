
namespace ListTarefas.Communication.request
{
    public class CreateRequestTaskJson
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
    public class UpdateRequestTask
    {
        public int TaskStatus { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
