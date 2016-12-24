using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using TTFApp.Models;
using TTFApp.Models.Compute;

namespace TTFApp.Controllers
{

    [RoutePrefix("api")]
    public class TTFController : ApiController
    {
        [Route("ttf/{type}")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]TTFInput input, string type)
        {
            try
            {
                if (input == null)
                    return BadRequest("Missing TTF input");

                var t = Assembly.GetExecutingAssembly().GetTypes()
                    .FirstOrDefault(x =>
                        typeof(ITTFCompute).IsAssignableFrom(x) &&
                        x.Name.Equals(type, StringComparison.CurrentCultureIgnoreCase)
                        );
                if (t == null)
                {
                    return BadRequest("Could not find compute engine for " + type);
                }
                else
                {
                    var result = ((ITTFCompute)Activator.CreateInstance(t)).Compute(input);
                    return Ok(new
                    {
                        Input = input,
                        Result = result
                    });

                }
            }
            catch(TTFException ex)
            {
                //log, etc
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //log, etc
                return InternalServerError();
            }
        }
    }
}
