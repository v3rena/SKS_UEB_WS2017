using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class HopArrival
    {
        public HopArrival(string code, DateTime dateTime)
        {
            this.code = code;
            this.dateTime = dateTime;
        }

        public string code;
        public DateTime dateTime;
    }
}
