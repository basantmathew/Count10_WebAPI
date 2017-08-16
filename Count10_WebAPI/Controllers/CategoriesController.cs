using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.categories.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.categories.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/categories/{id}")]
        public category Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.categories.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]category categories)
        {
            try
            {
                if(string.IsNullOrEmpty(categories.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        categories.active = categories.active.HasValue ? categories.active : true;
                        categories.archived = categories.archived.HasValue ? categories.archived : false;
                        categories.created_by = categories.created_by.HasValue ? categories.created_by : 1;
                        categories.updated_by = categories.updated_by.HasValue ? categories.updated_by : 1;
                        categories.created_at = DateTime.Now;
                        categories.updated_at = DateTime.Now;
                        entities.categories.Add(categories);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, categories);
                    }
                    return Ok(categories);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/categories/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.categories.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.categories.Remove(entity);
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
        [Route("api/categories/{id}")]
        public HttpResponseMessage put(int id, [FromBody]category categories)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.categories.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = categories.name;
                        entity.alt_name = categories.alt_name;
                        entity.parent_id = categories.parent_id;
                        entity.organization_id = categories.organization_id;
                        entity.notes = categories.notes;
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
