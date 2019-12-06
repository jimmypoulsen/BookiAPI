using BookiAPI.DataAccessLayer;
using BookiAPI.DataAccessLayer.Models;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookiAPI.RESTfulService.Controllers
{
    public class TablePackagesController : ApiController
    {

        private readonly TablePackageRepository _tablePackageRepository;

        public TablePackagesController() {
            _tablePackageRepository = new TablePackageRepository();
        }

        // GET /api/employees/
        public IEnumerable<TablePackageResponse> Get() {
            return _tablePackageRepository.Get().Select(tablePackage => new TablePackageResponse {
                Id = tablePackage.Id,
                Name = tablePackage.Name,
                Price = tablePackage.Price,
                VenueId = tablePackage.VenueId
            });
        }

        // GET /api/employees/1/
        public IEnumerable<TablePackageResponse> Get(int id) {
            return _tablePackageRepository.Get(id).Select(tablePackage => new TablePackageResponse {
                Id = tablePackage.Id,
                Name = tablePackage.Name,
                Price = tablePackage.Price,
                VenueId = tablePackage.VenueId
            });
        }

        public IEnumerable<TablePackageResponse> GetByVenueId(int venueId)
        {
            return _tablePackageRepository.GetByVenueId(venueId).Select(tablePackage => new TablePackageResponse
            {
                Id = tablePackage.Id,
                Name = tablePackage.Name,
                Price = tablePackage.Price,
                VenueId = tablePackage.VenueId
            });
        }

        // POST /api/employees/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data) {
            BookiAPI.DataAccessLayer.Models.TablePackage tablePackage = new DataAccessLayer.Models.TablePackage {
                Name = data.TablePackage.Name.Value,
                Price = (decimal) data.TablePackage.Price.Value,
                VenueId = (int) data.TablePackage.VenueId.Value
            };

            if (_tablePackageRepository.Add(tablePackage) > 0)
                return Ok("TablePackage was created");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Delete(int id) {
            if (_tablePackageRepository.Delete(id))
                return Ok("TablePackage was deleted");
            else
                return BadRequest("Something went wrong ..");
        }

        public bool DeleteByVenueId(int venueId)
        {
            return _tablePackageRepository.DeleteByVenueId(venueId);
        }
    }
}
