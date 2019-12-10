﻿using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookiAPI.RESTfulService.Controllers
{
    public class VenuesController : ApiController
    {
        private readonly VenueRepository _venueRepository;
        private readonly TablePackagesController _tablePackagesController;
        private readonly BeveragesController _beveragesController;
        private readonly VenueHoursController _venueHoursController;
        private readonly TablesController _tablesController;
        private readonly ReservationsController _reservationsController;
        private readonly VenueEmployeesController _venueEmployeesController;

        public VenuesController()
        {
            _venueRepository = new VenueRepository();
            _tablePackagesController = new TablePackagesController();
            _beveragesController = new BeveragesController();
            _venueHoursController = new VenueHoursController();
            _tablesController = new TablesController();
            _reservationsController = new ReservationsController();
            _venueEmployeesController = new VenueEmployeesController();
        }

        // GET /api/venue/
        public IEnumerable<VenueResponse> Get()
        {
            return _venueRepository.Get().Select(venue => new VenueResponse
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address,
                City = venue.City,
                Zip = venue.Zip,
                Employees = GetEmployeesForVenue(venue.Id),
                TablePackages = _tablePackagesController.GetByVenueId(venue.Id),
                Beverages = _beveragesController.GetByVenueId(venue.Id),
                VenueHours = _venueHoursController.GetByVenueId(venue.Id),
                Tables = _tablesController.GetByVenueId(venue.Id)
            });
        }

        // GET /api/venue/1/
        public IEnumerable<VenueResponse> Get(int id)
        {
            return _venueRepository.Get(id).Select(venue => new VenueResponse
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address,
                City = venue.City,
                Zip = venue.Zip,
                Employees = GetEmployeesForVenue(venue.Id),
                TablePackages = _tablePackagesController.GetByVenueId(venue.Id),
                Beverages = _beveragesController.GetByVenueId(venue.Id),
                VenueHours = _venueHoursController.GetByVenueId(venue.Id),
                Tables = _tablesController.GetByVenueId(venue.Id)
            });
        }

        private IEnumerable<EmployeeResponse> GetEmployeesForVenue(int id)
        {
            return _venueRepository.GetEmployeesForVenue(id).Select(employee => new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Phone = employee.Phone,
                Email = employee.Email,
                Password = employee.Password,
                EmployeeNo = employee.EmployeeNo,
                Title = employee.Title
            });
        }

        // POST /api/venue/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data)
        {
            BookiAPI.DataAccessLayer.Models.Venue venue = new DataAccessLayer.Models.Venue
            {
                Name = data.Venue.Name.Value,
                Address = data.Venue.Address.Value,
                City = data.Venue.City.Value,
                Zip = (int)data.Venue.Zip.Value
            };

            if (_venueRepository.Add(venue) > 0)
                return Ok("Venue was created");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Put([FromBody]dynamic data) {
            int id = (int)data.Id.Value;
            
            BookiAPI.DataAccessLayer.Models.Venue newVenue = new BookiAPI.DataAccessLayer.Models.Venue {
                Name = data.Venue.Name.Value == null ? "" : data.Venue.Name.Value,
                Address = data.Venue.Address.Value == null ? "" : data.Venue.Address.Value,
                Zip = data.Venue.Address.Value == null ? 0 : (int)data.Venue.Zip.Value,
                City = data.Venue.City.Value == null ? "" : data.Venue.City.Value
            };

            if (_venueRepository.Update(id, newVenue))
                return Ok($"Venue with ID: {id} was updated");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Delete(int id)
        {
            // delete reservationstablepackages
            // delete reservations
            _reservationsController.DeleteByVenueId(id);

            // delete tablepackages
            _tablePackagesController.DeleteByVenueId(id);

            // delete tables
            _tablesController.DeleteByVenueId(id);

            // delete venuehours
            _venueHoursController.DeleteByVenueId(id);

            // delete beverages
            _beveragesController.DeleteByVenueId(id);

            _venueEmployeesController.DeleteByVenueId(id);


            if (_venueRepository.Delete(id))
                return Ok("Venue was deleted");
            else
                return BadRequest("Something went wrong ..");
        }
    }
    
}