using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBE.Domain.Elections.Models
{
   public class UserAlternateID
    {
        public int UserID { get; set; }
        public string AlternateID { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            UserAlternateID p = obj as UserAlternateID;
            if ((Object)p == null)
            {
                return false;
            }
            return (UserID == p.UserID) && (AlternateID == p.AlternateID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


}
