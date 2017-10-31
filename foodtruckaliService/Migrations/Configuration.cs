namespace foodtruckaliService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.Azure.Mobile.Server;
    using foodtruckaliService.DataObjects;

    internal sealed class Configuration : DbMigrationsConfiguration<foodtruckaliService.Models.foodtruckaliContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new Microsoft.Azure.Mobile.Server.Tables.EntityTableSqlGenerator());
        }

        protected override void Seed(foodtruckaliService.Models.foodtruckaliContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.TodoItems.AddOrUpdate(
            //  p => p.FullName,
            //  new Person { FullName = "Andrew Peters" },
            //  new Person { FullName = "Brice Lambson" },
            //  new Person { FullName = "Rowan Miller" }
            //);
            context.FoodTrucks.AddOrUpdate<FoodTruck>(
                new FoodTruck {Id = Guid.NewGuid().ToString() ,Name = "Test Name 1", Description = "Test Description 1", IsAvailable = true, Latitude = 37.1, Longitude = -122.40183 },
                new FoodTruck { Id = Guid.NewGuid().ToString(), Name = "Test Name 2", Description = "Test Description 2", IsAvailable = true, Latitude = 37.3, Longitude = -122.5 },
            new FoodTruck { Id = Guid.NewGuid().ToString(), Name = "Test Name 3", Description = "Test Description 3", IsAvailable = true, Latitude = 38, Longitude = -123 });

        }
    }
}
