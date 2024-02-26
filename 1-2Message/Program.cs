namespace _1_2Message
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myMessage = new Message("Hello, World! Greetings from Message Object.");
            myMessage.Print();

            string name = Console.ReadLine()!.ToLower();
            string[] knownNames = new string[] { "faisal", "walima", "mathew", "james", "peter"};
            
            
            var faisalMsg = new Message("Hi Faisal, how are you?");
            var walimalMsg = new Message("Hi Walima, how are you?");
            var mathewMsg = new Message("Hi Mathew, how are you?");
            var jamesMsg = new Message("Hi James, how are you?");
            var peterMsg = new Message("Hi Peter, how are you?");
            var defaultMsg = new Message("Welcome, nice to me you.");

            if (name == "faisal")
            {
                faisalMsg.Print();
            }else if(name == "Walima")
            {
                walimalMsg.Print();
            }else if (name == "mathew")
            {
                mathewMsg.Print(); 
            }else if (name == "james")
            {
                jamesMsg.Print();
            }else if (name == "peter")
            {
                peterMsg.Print();
            }else
            {
                defaultMsg.Print();
            }


        }
    }
}