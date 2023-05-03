using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_WPF.Model
{
    public class APIResponseModel
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<EmployeeDetails> data { get; set; }
    }
    public class Meta
    {
        public Pagination pagination { get; set; }

    }

    public class Pagination
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
    public class EmployeeDetails
    {
        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }

    public class EmployeeById
    {
        [JsonProperty(PropertyName = "code")]
        public long Code { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        [JsonProperty(PropertyName = "data")]
        public EmployeeDetails Data { get; set; }
    }
}
