using ListaTarefas.Domain;
using ListaTarefas.Exception;
using ListaTarefas.Utils.validations;

namespace Domain.Entities
{
    public class User
    {        

        // Constructor
        public User( string email, string password)
        {
          
            SetEmail(email);
            SetPassword(password);          

        }
        //Constructor entity
        private User(){}

        // Public properties
        // 
        public string Email { get; private set; }
        public string Password { get; private set; }
        public int Id { get; private set; }


        // Domain methods   

        public void SetEmail(string email)
        {
             email.ValidateEmail(email);
            Email = email.Trim().ToLower();
        }

        public void SetPassword(string password)
        {
            password.ValidateNotEmpty(ErrorMessage.Password_EMPTY);
            password.ValidateMinLength(6,ErrorMessage.PASSWORD_MIN);
            Password = password;
        }      

     

       
    
    }
}
