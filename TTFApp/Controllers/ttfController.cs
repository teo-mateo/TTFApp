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
        private IEnumerable<Type> GetTTFImplementations()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                    .Where(x => typeof(ITTFCompute).IsAssignableFrom(x) && !x.IsAbstract);
        }

        [Route("ttf")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var urls = GetTTFImplementations()
                    .Select(x => this.Request.RequestUri.AbsoluteUri +  "/" + x.Name);

            return Ok(new
            {
                Message = "Please use one of the following URLs, action: POST",
                URLs = urls, 
                SamplePayload = new TTFInput(true, true, true, 101, 102, 103)
            });
        }

        [Route("ttf/{type}")]
        [HttpGet]
        public IHttpActionResult Get(string type)
        {
            return BadRequest("Use the POST, Luke");
        }

        [Route("ttf/{type}")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]TTFInput input, string type)
        {
            try
            {
                if (input == null)
                    return BadRequest("Missing TTF input");

                var impl = GetTTFImplementations();

                var t = impl
                    .FirstOrDefault(x => x.Name.Equals(type, StringComparison.CurrentCultureIgnoreCase));

                if (t == null)
                {
                    string msg = String.Format("Could not find compute engine for '{0}'. You can use: {1}",
                        type,
                        impl.Select(x => x.Name).Aggregate((p, q) => p + "/" + q));

                    return BadRequest(msg);
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
