using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using team5_SC.Models;

namespace team5_SC
{
    public class DB
    {
        private DBContext dbContext;

        public DB(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            SeedUsers();
            SeedProducts();
        }

		public void SeedProducts()
		{
			Debug.WriteLine("Testing Testing Testing Testing Testing Testing Testing Testing");
			dbContext.Add(new Product
			{
				Image = "../../image/Chart.jpg",
				Name = ".NET Charts",
				Price = 99,
				Description = "Brings powerful charting capabilities to your .NET applications."
			});
			dbContext.Add(new Product
			{
				Image = "../../image/PayPal.png",
				Name = ".NET PayPal",
				Price = 69,
				Description = "Integrate your .NET apps with PayPal the easy way!"
			});
			dbContext.Add(new Product
			{
				Image = "../../image/ML.jpg",
				Name = ".NET ML",
				Price = 299,
				Description = "Supercharged .NET machine learning libraries."
			});
			dbContext.Add(new Product
			{
				Image = "../../image/Analytics.png",
				Name = ".NET Analytics",
				Price = 299,
				Description = "Performs data mining and analytics easily in .NET."
			});
			dbContext.Add(new Product
			{
				Image = "../../image/Loggers.jpg",
				Name = ".NET Logger",
				Price = 49,
				Description = "Logs and aggregates events easily in your .NET apps."
			});
			dbContext.Add(new Product
			{
				Image = "../../image/Numerics.png",
				Name = ".NET Numerics",
				Price = 199,
				Description = "Powerful numerical methods for your .NET simulations."
			});
			dbContext.SaveChanges();
		}

		public void SeedUsers()
		{
			HashAlgorithm sha = SHA256.Create();

			string[] usernames = { "John", "Mary", "Hazel" };
			string[] passwords = { "john123", "mary123", "hazel123" };

			for(int i=0;i<usernames.Length;i++)
            {
				string combo = usernames[i] + passwords[i];
				byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(combo));

				dbContext.Add(new User
				{
					Username = usernames[i],
					Password = hash
				});
			}

			dbContext.SaveChanges();
		}
	}
}
