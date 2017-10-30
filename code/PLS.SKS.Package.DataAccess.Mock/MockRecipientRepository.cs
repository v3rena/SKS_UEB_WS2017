using PLS.SKS.Package.DataAccess.Entities;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Mock
{
    public class MockRecipientRepository : IRecipientRepository
    {
        public List<Recipient> recipients = new List<Recipient>();

        public MockRecipientRepository()
        {
            Create(new Recipient("Peter", "Pilz", "Waldweg 15", "1120", "Wien"));
            Create(new Recipient("Hans Peter", "Doskozil", "Industriestrasse 39", "1210", "Wien"));
            Create(new Recipient("August", "Wöginger", "Sonnenhang 7", "4779", "Andorf"));
        }

        public int Create(Recipient r)
        {
			recipients.Add(r);
            return 0;
        }

        public void Delete(int id)
        {
            Recipient r = recipients.SingleOrDefault(item => item.id == id);
            recipients.Remove(r);
        }

        public Recipient GetById(int id)
        {
            return recipients.SingleOrDefault(item => item.id == id);
        }

        public void Update(Recipient r)
        {
            Recipient r2 = recipients.Find(item => item.id == r.id);
            r = r2;
        }
    }
}
