using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class LocationsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.locations.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.locations.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/locations/{id}")]
        public location Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.locations.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]location locations)
        {
            try
            {
                if (string.IsNullOrEmpty(locations.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        locations.active = locations.active.HasValue ? locations.active : true;
                        locations.archived = locations.archived.HasValue ? locations.archived : false;
                        locations.updated_by = locations.updated_by.HasValue ? locations.updated_by : 1;
                        locations.created_by = locations.created_by.HasValue ? locations.created_by : 1;
                        locations.created_at = DateTime.Now;
                        locations.updated_at = DateTime.Now;
                        entities.locations.Add(locations);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, locations);
                    }
                    return Ok(locations);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/locations/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.locations.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Location with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.locations.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [HttpPut]
        [Route("api/locations/{id}")]
        public HttpResponseMessage put(int id, [FromBody]location locations)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.locations.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "location with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = locations.name;
                        entity.alt_name = locations.alt_name;
                        entity.region_id = locations.region_id;
                        entity.organization_id = locations.organization_id;
                        entity.currency_id = locations.currency_id;
                        entity.stock_ledger_id = locations.stock_ledger_id;
                        entity.manager_id = locations.manager_id;
                        entity.kind = locations.kind;
                        entity.@virtual = locations.@virtual;
                        entity.inventoriable = locations.inventoriable;
                        entity.grade = locations.grade;
                        entity.format = locations.format;
                        entity.area = locations.area;
                        entity.activity_area = locations.activity_area;
                        entity.latitude = locations.latitude;
                        entity.longitude = locations.longitude;
                        entity.notes = locations.notes;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
