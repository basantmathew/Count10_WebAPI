using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.items.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.items.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/items/{id}")]
        public item Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.items.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]item items)
        {
            try
            {
                if (string.IsNullOrEmpty(items.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        items.active = items.active.HasValue ? items.active : true;
                        items.archived = items.archived.HasValue ? items.archived : false;
                        items.updated_by = items.updated_by.HasValue ? items.updated_by : 1;
                        items.created_by = items.created_by.HasValue ? items.created_by : 1;
                        items.created_at = DateTime.Now;
                        items.updated_at = DateTime.Now;
                        entities.items.Add(items);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, items);
                    }
                    return Ok(items);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/items/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.items.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.items.Remove(entity);
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
        //[HttpPut]
        //[Route("api/items/{id}")]
        //public HttpResponseMessage put(int id, [FromBody]item items)
        //{ 

        //    try
        //    {
        //        using (Count10_DevEntities entities = new Count10_DevEntities())
        //        {
        //            var entity = entities.items.FirstOrDefault(e => e.id == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "item with Id = " + id.ToString() + " not found to edit");
        //            }
        //            else
        //            {
        //                entity.name = items.name;
        //                entity.alt_name = items.alt_name;
        //                entity.print_name = items.print_name;
        //                entity.parent_id = items.parent_id;
        //                entity.organization_id = items.organization_id;
        //                entity.category_id = items.category_id;
        //                entity.currency_id = items.currency_id;
        //                entity.kind = locations.kind;
        //                entity.@virtual = locations.@virtual;
        //                entity.inventoriable = locations.inventoriable;
        //                entity.grade = locations.grade;
        //                entity.format = locations.format;
        //                entity.area = locations.area;
        //                entity.activity_area = locations.activity_area;
        //                entity.latitude = locations.latitude;
        //                entity.longitude = locations.longitude;
        //                entity.notes = locations.notes;
        //                entities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }

        //}
    }
}
