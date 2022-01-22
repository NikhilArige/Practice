using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
	public static void Main()
	{
		var user1 = new User("Nikhil");
		var user2 = new User("User2");
		var user3 = new User("User3");

		user2.Subscribe(user1);
		user3.Subscribe(user1);

		user1.SendTweet("Bring me Thanos ^_^");

		user3.UnSubscribe(user1);

		user1.SendTweet("Thanos was right!");

	}

	public class User {

		public event EventHandler<Tweet> Process;

		public string Name;

		public User(string name) { 
			this.Name = name; 
		}

		public void SendTweet(string message)
		{
			var tweet = new Tweet(message);

			if (Process != null)
			{
				Process(this, tweet);
			}
		}

		public void Subscribe(User user) {

			user.Process += ShowTweet;	
		}

		public void UnSubscribe(User user)
		{
			user.Process -= ShowTweet;
		}

		public void ShowTweet(object sender,Tweet tweet) {

			var user = (User)sender;
			Console.WriteLine($"New tweet from {user.Name} @{tweet.Time.ToLongTimeString()} to user {this.Name}: {tweet.Message} "); 
		}

	}

	public class Tweet : EventArgs{

		public Tweet(string message){

			this.Message = message;
			this.Time = DateTime.Now;
		}

        public string Message { get; set; }

        public DateTime Time { get; set; }

    }

}

