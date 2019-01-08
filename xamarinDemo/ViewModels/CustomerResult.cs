using System.Collections.Generic;

namespace xamarinDemo.ViewModels
{
    public class CustomerResult
    {
        public List<Customer> customers { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public int count { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public string next_url { get; set; }
    }

    public class Customer
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string customer_url { get; set; }
    }
}
