using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using TDJ.WebPanel.Models;

namespace TDJ.WebPanel.Controllers
{
    public class ServiceController : ApiController
    {
        //
        // GET: /Service/

        public PanelModel Get()
        {
            return new PanelModel
            {
                IsArmed = true
            };
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

    }
}
