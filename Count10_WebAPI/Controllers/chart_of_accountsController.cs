using Count10DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Count10_WebAPI.Controllers
{
    public class chart_of_accountsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string listData = "")
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                switch (listData.ToLower().ToString())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.chart_of_accounts.Where(e => e.reserved == false).ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.OK, entities.chart_of_accounts.Where(e => e.id > 0 && e.reserved == false).ToList());
                }
            }
        }
        [HttpGet]
        [Route("api/chart_of_accounts/{id}")]
        public chart_of_accounts Get(int id)
        {
            using (Count10_DevEntities entities = new Count10_DevEntities())
            {
                return entities.chart_of_accounts.FirstOrDefault(e => e.id == id);
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult post([FromBody]chart_of_accounts chartOfAccounts)
        {
            try
            {
                if (string.IsNullOrEmpty(chartOfAccounts.name))
                {
                    ModelState.AddModelError("name", "Name is Required");
                }
                if (ModelState.IsValid)
                {
                    using (Count10_DevEntities entities = new Count10_DevEntities())
                    {
                        chartOfAccounts.system = chartOfAccounts.system.HasValue ? chartOfAccounts.system : false;
                        chartOfAccounts.reserved = chartOfAccounts.reserved.HasValue ? chartOfAccounts.reserved : false;
                        chartOfAccounts.active = chartOfAccounts.active.HasValue ? chartOfAccounts.active : true;
                        chartOfAccounts.archived = chartOfAccounts.archived.HasValue ? chartOfAccounts.archived : false;
                        chartOfAccounts.created_by = chartOfAccounts.created_by.HasValue ? chartOfAccounts.created_by : 1;
                        chartOfAccounts.updated_by = chartOfAccounts.updated_by.HasValue ? chartOfAccounts.updated_by : 1;
                        chartOfAccounts.created_at = DateTime.Now;
                        chartOfAccounts.updated_at = DateTime.Now;
                        entities.chart_of_accounts.Add(chartOfAccounts);
                        entities.SaveChanges();
                        var message = Request.CreateResponse(HttpStatusCode.Created, chartOfAccounts);
                    }
                    return Ok(chartOfAccounts);
                }
                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }

        }
        [HttpDelete]
        [Route("api/chart_of_accounts/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.chart_of_accounts.FirstOrDefault(e => e.id == id);
                    if (entities == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chart of Account with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.chart_of_accounts.Remove(entity);
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
        [Route("api/chart_of_accounts/{id}")]
        public HttpResponseMessage put(int id, [FromBody]chart_of_accounts chartOfAccounts)
        {

            try
            {
                using (Count10_DevEntities entities = new Count10_DevEntities())
                {
                    var entity = entities.chart_of_accounts.FirstOrDefault(e => e.id == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Chart of Account with Id = " + id.ToString() + " not found to edit");
                    }
                    else
                    {
                        entity.name = chartOfAccounts.name;
                        entity.alt_name = chartOfAccounts.alt_name;
                        entity.predefined_name = chartOfAccounts.predefined_name;
                        entity.parent_id = chartOfAccounts.parent_id;
                        entity.nature_id = chartOfAccounts.nature_id;
                        entity.organization_id = chartOfAccounts.organization_id;
                        entity.blsl = chartOfAccounts.blsl;
                        entity.position = chartOfAccounts.position;
                        entity.tag = chartOfAccounts.tag;
                        entity.notes = chartOfAccounts.notes;
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
