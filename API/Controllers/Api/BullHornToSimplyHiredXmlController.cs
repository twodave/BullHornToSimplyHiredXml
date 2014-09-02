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
        public HttpResponseMessage Get() {
            WebClient wc = new WebClient();
            string xml = wc.DownloadString(@"http://cls4.bullhornstaffing.com/JobBoard/Standard/JobOpportunitiesRSS.cfm?privateLabelID=7270&category");
            return new HttpResponseMessage() {
                Content = new StringContent(BullHornXmlConverter.ToSimplyHiredXml(xml), Encoding.UTF8, "application/xml")
            };
        }
    }
}
