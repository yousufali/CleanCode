using System;

namespace MBE.Domain.Elections.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
