using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class UomsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.uoms.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.uoms.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/uoms/{id}")]
        public uom Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.uoms.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]uom uoms)
        {
            try
            {
                if (string.IsNullOrEmpty(uoms.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        uoms.active = uoms.active.HasValue ? uoms.active : true;
                        uoms.archived = uoms.archived.HasValue ? uoms.archived : false;
                        uoms.updated_by = uoms.updated_by.HasValue ? uoms.updated_by : 1;
                        uoms.created_by = uoms.created_by.HasValue ? uoms.created_by : 1;
                        uoms.created_at = DateTime.Now;
                        uoms.updated_at = DateTime.Now;
                        entities.uoms.Add(uoms);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, uoms);
                    }
                    return Ok(uoms);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/uoms/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.uoms.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Uoms with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.uoms.Remove(entity);
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
        [Route("api/uoms/{id}")]
        public HttpResponseMessage put(int id, [FromBody]uom uoms)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.uoms.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "uoms with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = uoms.name;
                        entity.alt_name = uoms.alt_name;
                        entity.category = uoms.category;
                        entity.desc = uoms.desc;
                        entity.notes = uoms.notes;
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
