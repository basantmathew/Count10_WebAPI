using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class RegionsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.regions.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.regions.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/regions/{id}")]
        public region Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.regions.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]region regions)
        {
            try
            {
                if (string.IsNullOrEmpty(regions.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        regions.active = regions.active.HasValue ? regions.active : true;
                        regions.archived = regions.archived.HasValue ? regions.archived : false;
                        regions.updated_by = regions.updated_by.HasValue ? regions.updated_by : 1;
                        regions.created_by = regions.created_by.HasValue ? regions.created_by : 1;
                        regions.created_at = DateTime.Now;
                        regions.updated_at = DateTime.Now;
                        entities.regions.Add(regions);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, regions);
                    }
                    return Ok(regions);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/regions/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.regions.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Region with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.regions.Remove(entity);
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
        [Route("api/regions/{id}")]
        public HttpResponseMessage put(int id, [FromBody]region regions)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.regions.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Region with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = regions.name;
                        entity.alt_name = regions.alt_name;
                        entity.parent_id = regions.parent_id;
                        entity.organization_id = regions.organization_id;
                        entity.notes = regions.notes;
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
