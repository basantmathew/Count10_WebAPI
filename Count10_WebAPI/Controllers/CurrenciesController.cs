using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class CurrenciesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.currencies.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.currencies.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/currencies/{id}")]
        public currency Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.currencies.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]currency currencies)
        {
            try
            {
                if (string.IsNullOrEmpty(currencies.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        currencies.active = currencies.active.HasValue ? currencies.active : true;
                        currencies.archived = currencies.archived.HasValue ? currencies.archived : false;
                        currencies.created_by = currencies.created_by.HasValue ? currencies.created_by : 1;
                        currencies.updated_by = currencies.updated_by.HasValue ? currencies.updated_by : 1;
                        currencies.created_at = DateTime.Now;
                        currencies.updated_at = DateTime.Now;
                        entities.currencies.Add(currencies);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, currencies);
                    }
                    return Ok(currencies);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/currencies/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.currencies.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Currency with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.currencies.Remove(entity);
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
        [Route("api/currencies/{id}")]
        public HttpResponseMessage put(int id, [FromBody]currency currencies)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.currencies.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Currency with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = currencies.name;
                        entity.alt_name = currencies.alt_name;
                        entity.iso_code = currencies.iso_code;
                        entity.iso_number = currencies.iso_number;
                        entity.fractionals = currencies.fractionals;
                        entity.fractionals_name = currencies.fractionals_name;
                        entity.display_name = currencies.display_name;
                        entity.placement = currencies.placement;
                        entity.notes = currencies.notes;
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
