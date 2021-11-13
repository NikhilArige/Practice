using System;

namespace EventsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database();
            var idGenerator = new IDGenerator();
            var emailSender = new EmailGenerator();
            Processor.ProcessEvent += db.SavetoDB;
            Processor.ProcessEvent += idGenerator.GenerateId;
            Processor.ProcessEvent += emailSender.SendEmail;

            Processor.Process("Nikhil Arige",23); 
        }
    }

    public static class Processor 
    {
        public static event EventHandler<UserArgs> ProcessEvent;

        public static void Process(string name,int age) 
        {
            UserArgs args = new UserArgs(name,age);
            ProcessEvent?.Invoke(null,args);
        }
    
    }

    public class UserArgs : EventArgs
    {
        public UserArgs(string name,int age)
        {
            this.Name = name;
            this.Age = age;
        }
        public string Name { get; set; }

        public int Age { get; set; }
    }

    public class Database 
    {
        public void SavetoDB(object sender, UserArgs user)
        {
            Console.WriteLine($"Saved user details of {user.Name} to DB.");
        }
    }

    public class IDGenerator
    {
        public void GenerateId(object sender, UserArgs user)
        {
            Console.WriteLine($"Generated ID for {user.Name}.");
        }
    }

    public class EmailGenerator
    {
        public void SendEmail(object sender, UserArgs user)
        {
            Console.WriteLine($"Email sent to {user.Name}.");
        }
    }

}
