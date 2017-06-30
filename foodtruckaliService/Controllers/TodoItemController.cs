using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using foodtruckaliService.DataObjects;
using foodtruckaliService.Models;
using System.Collections.Generic;

namespace foodtruckaliService.Controllers
{
    public class FoodTruckController : TableController<FoodTruck>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            foodtruckaliContext context = new foodtruckaliContext();
            DomainManager = new EntityDomainManager<FoodTruck>(context, Request);
        }


        [AllowAnonymous]
        public IQueryable<FoodTruck> GetFoodTruck(string searchTerm, Point point1,Point point2)
        {
            IQueryable<FoodTruck> toReturn = DomainManager.Query().Where(i=> i.IsAvailable);
            return toReturn;
        }
        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FoodTruck> PutTodoItem(string id, Delta<FoodTruck> patch)
        {
            return UpdateAsync(id, patch);
        }

 //       [Authorize]
        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(FoodTruckRegisterModel FoodTruck)
        {
            var searchResult = DomainManager.Query().Single(i => string.Equals(FoodTruck.Name.ToLower(), i.Name.ToLower()));
            if (searchResult != null)
            {
                //name already used, return error 409
                return Conflict();
            }
            FoodTruck current = await InsertAsync(new Controllers.FoodTruck {Name = FoodTruck.Name,Description=FoodTruck.Description,User=User.Identity.Name});
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }


        [Authorize]
        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}