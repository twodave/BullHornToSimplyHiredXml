using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DSheldon.Controllers.Api
{
    public class BullHornToSimplyHiredXmlController : ApiController
    {
        // POST api/bullhorntosimplyhiredxml
        public HttpResponseMessage Post() {

            var t = this.Request.Content.ReadAsStringAsync();
            t.Wait();
            t.Result.ToString();

            return new HttpResponseMessage() {
                Content = new StringContent(BullHornXmlConverter.ToSimplyHiredXml(t.Result), Encoding.UTF8, "application/xml")
            };
        }
    }
}
