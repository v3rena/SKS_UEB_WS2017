﻿using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface IHopArrivalLogic
    {
        void ScanParcel(string trackingNumber, string code);
    }
}