﻿using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            // find point of interest

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterest == null)
                return NotFound();

            return Ok(pointOfInterest);
        }

        /// <summary>
        /// Add [FromBody] on complex type is optional
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="pointOfInterest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,
            PointOfInterestForCreationDto pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            var maxPointOfInterestId = CitiesDataStore.Current.Cities
                .SelectMany(c => c.PointsOfInterest)
                .Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointOfInterestId = finalPointOfInterest.Id
            }, finalPointOfInterest);
        }
    }
}
