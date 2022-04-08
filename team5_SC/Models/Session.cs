using System;

namespace team5_SC.Models
{
	public class Session
	{
		public Session()
		{
			Id = new Guid();

			Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
		}

		public Guid Id { get; set; }

		public long Timestamp { get; set; }

		public virtual User User { get; set; }
	}
}