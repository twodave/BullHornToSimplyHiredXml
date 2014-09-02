using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace DSheldon {
    public static class BullHornXmlConverter {
        static BullHornXmlConverter() { }

        

        private class SimplyHiredJob {
            public string JobTitle { get; set; }
            public string DetailUrl { get; set; }
            public string Description { get; set; }
            
            public string UniqueJobCode {
                get {
                    if (this.JobTitle != null && this.JobTitle.ToUpper().Contains("JOB #") && this.JobTitle.Contains(":")) {
                        int jobNumberStart = this.JobTitle.ToUpper().IndexOf("JOB #") + 5;
                        int jobNumberEnd = this.JobTitle.IndexOf(":");
                        return this.JobTitle.Substring(jobNumberStart, jobNumberEnd - jobNumberStart).Trim();
                    }
                    return String.Empty;
                }
            }
            public string Company { get { return "Optimum Healthcare IT"; } }
            
            public string City {
                get {
                    if (this.Description != null && this.Description.ToUpper().Contains("LOCATION:")) {
                        int locationStart = this.Description.ToUpper().IndexOf("LOCATION:") + 9;
                        int cityEnd = locationStart + this.Description.ToUpper().Substring(locationStart).IndexOf(",");
                        return this.Description.Substring(locationStart, cityEnd - locationStart).Trim();
                    }
                    return String.Empty;
                }
            }
            public string State {
                get {
                    if (this.Description != null && this.Description.ToUpper().Contains("LOCATION:")) {
                        int locationStart = this.Description.ToUpper().IndexOf("LOCATION:") + 9;
                        int stateStart = locationStart + this.Description.ToUpper().Substring(locationStart).IndexOf(",") + 1;
                        int stateEnd = stateStart + this.Description.ToUpper().Substring(stateStart).IndexOf("&LT;BR&GT;");
                        return this.Description.Substring(stateStart, stateEnd - stateStart).Trim();
                    }
                    return String.Empty;
                }
            }

            public string Country { get { return "United States"; } }
            

            public string ToXml() {
                return SmartFormat.Smart.Format(@"
    <job>
        <title>{JobTitle}</title>
        <job-code>{UniqueJobCode}</job-code>
        <detail-url>{DetailUrl}</detail-url>
        <description><summary>{Description}</summary></description>
        <location>
            <city>{City}</city>
            <state>{State}</state>
            <country>{Country}</country>
        </location>
        <company>
            <name>{Company}</name>
        </company>
    </job>", this);
            }
        }

        

        public static string ToSimplyHiredXml(string bullHornXml) {
            List<SimplyHiredJob> jobs = new List<SimplyHiredJob>();
            StringBuilder simplyHiredXml = new StringBuilder();

            using (XmlReader reader = XmlReader.Create(new StringReader(bullHornXml))) {
                while (reader.ReadToFollowing("item")) {
                    SimplyHiredJob job = new SimplyHiredJob();

                    reader.ReadToFollowing("title");
                    job.JobTitle = reader.ReadElementContentAsString().Trim();

                    reader.ReadToFollowing("link");
                    job.DetailUrl = HttpUtility.HtmlEncode(reader.ReadElementContentAsString().Trim());

                    reader.ReadToFollowing("description");
                    job.Description = HttpUtility.HtmlEncode(reader.ReadElementContentAsString().Trim());
                    jobs.Add(job);
                }
            }
            simplyHiredXml.Append("<jobs>");
            foreach (SimplyHiredJob job in jobs) {
                simplyHiredXml.Append(job.ToXml());
            }
            simplyHiredXml.AppendLine(String.Empty);
            simplyHiredXml.Append("</jobs>");

            return simplyHiredXml.ToString();
        }
    }
}