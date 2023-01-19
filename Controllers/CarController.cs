using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VehicleTracking.WebAPI.Models;

namespace VehicleTracking.WebAPI.Controllers
{
    public class CarController : ApiController
    {
        //GET api/Car
        [HttpGet]
        public HttpResponseMessage Get()
        {
            IEnumerable<Car> cars = CarRepository.GetAll();
            if (!cars.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, cars);
        }

        //POST api/Car
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Car newCar)
        {
            CarRepository.Add(newCar);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //PUT api/Car?id=7E2DCB8F-8D83-4F64-9AE5-3F7BE66BB4A8
        [HttpPut]
        public HttpResponseMessage Put(Guid id, [FromBody] Car updatedCar)
        {
            CarRepository.Update(id, updatedCar);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //DELETE api/Car?id=7E2DCB8F-8D83-4F64-9AE5-3F7BE66BB4A8
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            CarRepository.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}