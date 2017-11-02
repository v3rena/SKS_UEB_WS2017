using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IRecipientRepository
    {
        int Create(Recipient p); void
        Update(Recipient p); void
        Delete(int id);
        Recipient GetById(int id);
    }
}
