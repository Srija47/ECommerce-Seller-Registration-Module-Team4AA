using System;
using System.Collections.Generic;

namespace AccountService.Entities
{
    public partial class Seller
    {
        public Seller()
        {
            Items = new HashSet<Items>();
        }

        public int Sellerid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gst { get; set; }
        public string Companyname { get; set; }
        public string Briefaboutcompany { get; set; }
        public string Postaladdress { get; set; }
        public string Website { get; set; }
        public string Emailid { get; set; }
        public string Contactnumber { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
