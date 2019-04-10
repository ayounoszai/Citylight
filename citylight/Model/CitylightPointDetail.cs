using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using citylight.Model;
using Newtonsoft.Json;

namespace citylight.Model
{

    public class CitylightPointDetail
    {
        public string type { get; set; }
        public CitylightPointDetail.Geometry geometry { get; set; }
        public CitylightPointDetail.Properties properties { get; set; }
        //public IList<CitylightGroup> citylight_groups { get; set; }
        public IList<CitylightPointDetail.CitylightImage> citylight_images { get; set; }

        public class Geometry
        {
            public string type { get; set; }
            public IList<double> coordinates { get; set; }
        }

        public class Properties
        {
            public string image_filename { get; set; }
            public string table_id { get; set; }
            public string table_name { get; set; }
            public string point_id { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string point_name { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city_state { get; set; }
            public string zip { get; set; }
            public string telephone { get; set; }
            public string pURL { get; set; }
            public string pfeatured { get; set; }
            public string pdesc { get; set; }
            public string pcomment { get; set; }
            public string pfacebook { get; set; }
            public string ptwitter { get; set; }
            public string pinstagram { get; set; }
            public string last_edit { get; set; }
        }

        public class CitylightImage
        {
            public string type { get; set; }
            public CitylightPointDetail.Properties properties { get; set; }
        }

        public static async Task<CitylightPointDetail> GetPointDetailAsync(int pointId)
        {
            CitylightPointDetail pt = new CitylightPointDetail();
            var url = $"https://citypost.citylightlabs.com/_api/v2/point_detail.json?m=10&target={pointId}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<List<CitylightPointDetail>>(json);
                    if(items.Count == 1)
                    {
                        return items.First();
                    }
                    return pt;
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return pt;
            }
        }
    }
}
