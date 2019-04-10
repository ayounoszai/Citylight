using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace citylight.Model
{


    public class CitylightGroup
    {
        public string type { get; set; }
        public Properties properties { get; set; }


        public class Properties
        {
            public string table_id { get; set; }
            public string table_name { get; set; }
            public string group_id { get; set; }
            public string group_label { get; set; }
            public string group_depth { get; set; }
            public string group_parent { get; set; }
            public string group_nesting_path { get; set; }
            public string group_nested_label { get; set; }
            public string group_status { get; set; }
        }

        public static async Task<List<CitylightGroup>> GetGroupsAsync(int? groupId)
        {
            List<CitylightGroup> categories = new List<CitylightGroup>();
            categories.Add(new CitylightGroup
            {
                properties = new CitylightGroup.Properties
                {
                    group_label = "Show All",
                    group_id = groupId.ToString()
                }
            });
            var url = "https://citypost.citylightlabs.com/_api/v2/groups.json?m=10";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var all = JsonConvert.DeserializeObject<List<CitylightGroup>>(json);
                    //categories = all.Where(q => q.properties.group_depth == "2").ToList();
                    if (groupId.HasValue)
                    {
                        categories.AddRange(all.Where(q => q.properties.group_depth == "2").Where(q => q.properties.group_parent == groupId.Value.ToString()).ToList());
                    }
                    else
                    {
                        categories.AddRange(all.Where(q => q.properties.group_depth == "2").ToList());
                    }
                    return categories;
                };
            }
            catch
            {
                return categories;
            }
        }
    }

    //public static class GroupRoot
    //{
    //    public static async Task<List<CitylightGroup>> GetGroupsAsync(int? groupId)
    //    {
    //        List<CitylightGroup> categories = new List<CitylightGroup>();
    //        categories.Add(new CitylightGroup
    //        {
    //            properties = new GroupProperties
    //            {
    //                group_label = "Show All",
    //                group_id = groupId.ToString()
    //            }
    //        });
    //        var url = "https://citypost.citylightlabs.com/_api/v2/groups.json?m=10";
    //        try
    //        {
    //            using (HttpClient client = new HttpClient())
    //            {
    //                var response = await client.GetAsync(url);
    //                var json = await response.Content.ReadAsStringAsync();
    //                var all = JsonConvert.DeserializeObject<List<CitylightGroup>>(json);
    //                //categories = all.Where(q => q.properties.group_depth == "2").ToList();
    //                if(groupId.HasValue)
    //                {
    //                    categories.AddRange(all.Where(q => q.properties.group_depth == "2").Where(q => q.properties.group_parent == groupId.Value.ToString()).ToList());
    //                }
    //                else
    //                {
    //                    categories.AddRange(all.Where(q => q.properties.group_depth == "2").ToList());
    //                }
    //                return categories;
    //            };
    //        }
    //        catch
    //        {
    //            return categories;
    //        }
    //    }
    //}
}
