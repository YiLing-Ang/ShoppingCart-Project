using System;
using System.Collections.Generic;
using System.Linq;
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
				Image = "../../image/Analytics.jpeg",
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
		}

		public void SeedUsers()
		{
			dbContext.Add(new User
			{
				Username = "John",
				Password = "john123"
			});
			dbContext.Add(new User
			{
				Username = "Mary",
				Password = "mary123"
			});
			dbContext.Add(new User
			{
				Username = "Hazel",
				Password = "hazel123"
			});
		}
	}
}
