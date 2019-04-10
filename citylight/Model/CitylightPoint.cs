using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace citylight.Model
{


    public class CitylightPoint
    {
        public string type { get; set; }
        public CitylightPoint.Geometry geometry { get; set; }
        public CitylightPoint.Properties properties { get; set; }

        public class Geometry
        {
            public string type { get; set; }
            public IList<double> coordinates { get; set; }
        }

        public class Properties
        {
            public string table_id { get; set; }
            public string table_name { get; set; }
            public string group_id { get; set; }
            public string group_label { get; set; }
            public string group_depth { get; set; }
            public string point_id { get; set; }
            public string point_name { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city_state { get; set; }
            public string zip { get; set; }
            public string telephone { get; set; }
        }

        public static async Task<List<CitylightPoint>> GetPointsAsync(int groupId)
        {
            List<CitylightPoint> points = new List<CitylightPoint>();
            var url = $"https://citypost.citylightlabs.com/_api/v2/points.json?m=10&group={groupId}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CitylightPoint>>(json);
                };
            }
            catch
            {
                return points;
            }
        }
    }
}
