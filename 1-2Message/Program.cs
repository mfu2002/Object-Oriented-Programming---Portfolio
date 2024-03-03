namespace _1_2Message
{
    public class Program
    {
        static void Main(string[] args)
        {
            Message myMessage = new Message("Hello, World! Greetings from Message Object.");
            myMessage.Print();

            string name = Console.ReadLine()!.ToLower();
            
            
            Message faisalMsg = new Message("Hi Faisal, how are you?");
            Message mathewMsg = new Message("Hi Mathew, how are you?");
            Message walimalMsg = new Message("Hi Walima, how are you?");
            Message jamesMsg = new Message("Hi James, how are you?");
            Message peterMsg = new Message("Hi Peter, how are you?");
            Message defaultMsg = new Message("Welcome, nice to me you.");

            if (name == "faisal")
            {
                faisalMsg.Print();
            }else if(name == "walima")
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