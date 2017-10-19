using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class hopArrival
    {
        public hopArrival(string code, DateTime dateTime)
        {
            this.code = code;
            this.dateTime = dateTime;
        }

        private string code;
        private DateTime dateTime;
    }
}
