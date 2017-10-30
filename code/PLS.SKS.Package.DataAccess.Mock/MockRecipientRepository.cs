using PLS.SKS.Package.DataAccess.Entities;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace PLS.SKS.Package.DataAccess.Mock
{
    public class MockRecipientRepository : IRecipientRepository
    {
        public List<Recipient> recipients = new List<Recipient>();

        public MockRecipientRepository()
        {

        }

        public int Create(Recipient p)
        {
            recipients.Add(p);
            return 0;
        }

        public void Delete(int id)
        {
            recipients.Remove(Recipient r => r.)
        }

        public Recipient GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Recipient p)
        {
            throw new NotImplementedException();
        }
    }
}
