
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using foodtruckaliService.Models;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server.Config;
using foodtruckaliService.DataObjects;
using System.Security.Claims;
using Microsoft.Azure.Mobile.Server.Authentication;
using System.Security.Principal;

namespace foodtruckaliService.Controllers
{
    [MobileAppController]
    public class FoodTrucksController : ApiController
    {
        private foodtruckaliContext db = new foodtruckaliContext();

        // GET: api/FoodTrucks
        

        // GET: api/FoodTrucks/5
        [ResponseType(typeof(FoodTruck))]
        public IHttpActionResult GetFoodTruck(string id)
        {
            FoodTruck foodTruck = db.FoodTrucks.Find(id);
            if (foodTruck == null)
            {
                return NotFound();
            }

            return Ok(foodTruck);
        }

        [ResponseType(typeof(IEnumerable<FoodTruck>))]
        public async Task<IHttpActionResult> GetFoodTruck()
        {
            ClaimsPrincipal userClaims = User as ClaimsPrincipal;
            string provider = userClaims.FindFirst("http://schemas.microsoft.com/identity/claims/identityprovider").Value;
            string sid = userClaims.FindFirst("stable_sid").Value;
            ProviderCredentials creds = await this.User.GetAppServiceIdentityAsync<TwitterCredentials>(this.Request);
            var userAuthType = User.Identity.AuthenticationType;
            var user = creds.UserClaims;
            return Ok(provider);
        }

        // PUT: api/FoodTrucks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFoodTruck(string id, FoodTruck foodTruck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodTruck.Id)
            {
                return BadRequest();
            }

            db.Entry(foodTruck).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodTruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FoodTrucks
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> PostFoodTruckAsync(FoodTruck foodTruck)
        {
            var searchResult = db.FoodTrucks.SingleOrDefault(i => string.Equals(foodTruck.Name.ToLower(), i.Name.ToLower()));

            if (!ModelState.IsValid || searchResult != null)
            {
                return BadRequest(ModelState);
            }


            foodTruck.Id = Guid.NewGuid().ToString();
            db.FoodTrucks.Add(foodTruck);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FoodTruckExists(foodTruck.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok("");
            // return CreatedAtRoute("DefaultApi", new { id = foodTruck.Id }, foodTruck);
        }


        // DELETE: api/FoodTrucks/5
        [ResponseType(typeof(FoodTruck))]
        public IHttpActionResult DeleteFoodTruck(string id)
        {
            FoodTruck foodTruck = db.FoodTrucks.Find(id);
            if (foodTruck == null)
            {
                return NotFound();
            }

            db.FoodTrucks.Remove(foodTruck);
            db.SaveChanges();

            return Ok(foodTruck);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodTruckExists(string id)
        {
            return db.FoodTrucks.Count(e => e.Id == id) > 0;
        }
    }
}