using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        //[HttpGet]
        //public JsonResult GetCities()
        //{
        //    return new JsonResult(CitiesDataStore.Current.Cities);
        //}

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        //[HttpGet("{id}")]
        //public JsonResult GetCity(int id)
        //{
        //    return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        //}

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity([FromRoute] int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}