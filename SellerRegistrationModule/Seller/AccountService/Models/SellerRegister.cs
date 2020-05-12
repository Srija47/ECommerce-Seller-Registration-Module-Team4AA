using System;
using System.Collections.Generic;

namespace AccountService.Models
{
    //SellerRegister is Dto of Seller class in entity folder
    public class SellerRegister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gst { get; set; }
        public string Companyname { get; set; }
        public string Briefaboutcompany { get; set; }
        public string Postaladdress { get; set; }
        public string Website { get; set; }
        public string Emailid { get; set; }
        public string Contactnumber { get; set; }

    }
}
