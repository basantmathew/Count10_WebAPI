using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class LedgersController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.ledgers.ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.ledgers.Where(e => e.id > 0).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/ledgers/{id}")]
        public ledger Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.ledgers.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]ledger ledgers)
        {
            try
            {
                if (string.IsNullOrEmpty(ledgers.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        ledgers.system = ledgers.system.HasValue ? ledgers.system : false;
                        ledgers.managed = ledgers.managed.HasValue ? ledgers.managed : false;
                        ledgers.active = ledgers.active.HasValue ? ledgers.active : true;
                        ledgers.archived = ledgers.archived.HasValue ? ledgers.archived : false;
                        ledgers.updated_by = ledgers.updated_by.HasValue ? ledgers.updated_by : 1;
                        ledgers.created_by = ledgers.created_by.HasValue ? ledgers.created_by : 1;
                        ledgers.created_at = DateTime.Now;
                        ledgers.updated_at = DateTime.Now;
                        entities.ledgers.Add(ledgers);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, ledgers);
                    }
                    return Ok(ledgers);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/ledgers/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.ledgers.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ledgers with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.ledgers.Remove(entity);
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
        [Route("api/ledgers/{id}")]
        public HttpResponseMessage put(int id, [FromBody]ledger ledgers)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.ledgers.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ledger with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = ledgers.name;
                        entity.alt_name = ledgers.alt_name;
                        entity.parent_id = ledgers.parent_id;
                        entity.chart_of_account_id = ledgers.chart_of_account_id;
                        entity.organization_id = ledgers.organization_id;
                        entity.cost_centre_id = ledgers.cost_centre_id;
                        entity.currency_id = ledgers.currency_id;
                        entity.position = ledgers.position;
                        entity.opening_balance = ledgers.opening_balance;
                        entity.opening_cr_dr = ledgers.opening_cr_dr;
                        entity.current_balance = ledgers.current_balance;
                        entity.current_cr_dr = ledgers.current_cr_dr;
                        entity.notes = ledgers.notes;
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
