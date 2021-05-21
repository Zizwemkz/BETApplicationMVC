using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Data
{
    public partial class Notification
    {
        [Key]
        public int not_id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public bool isViewed { get; set; }
        public string url { get; set; }
        public string reply_email { get; set; }
    }
}
