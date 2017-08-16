using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class OrganizationsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.organizations.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.organizations.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/organizations/{id}")]
        public organization Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.organizations.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]organization organizations)
        {
            try
            {
                if (string.IsNullOrEmpty(organizations.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        organizations.active = organizations.active.HasValue ? organizations.active : true;
                        organizations.archived = organizations.archived.HasValue ? organizations.archived : false;
                        organizations.updated_by = organizations.updated_by.HasValue ? organizations.updated_by : 1;
                        organizations.created_by = organizations.created_by.HasValue ? organizations.created_by : 1;
                        organizations.created_at = DateTime.Now;
                        organizations.updated_at = DateTime.Now;
                        entities.organizations.Add(organizations);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, organizations);
                    }
                    return Ok(organizations);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/organizations/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.organizations.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Organization with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.organizations.Remove(entity);
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
        [Route("api/organizations/{id}")]
        public HttpResponseMessage put(int id, [FromBody]organization organizations)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.organizations.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Organization with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = organizations.name;
                        entity.alt_name = organizations.alt_name;
                        entity.parent_id = organizations.parent_id;
                        entity.notes = organizations.notes;
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
