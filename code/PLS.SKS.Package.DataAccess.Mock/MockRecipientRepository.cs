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
		int r_id = 0;

        public MockRecipientRepository()
        {
            //Create(new Recipient("Peter", "Pilz", "Waldweg 15", "A-1120", "Wien"));
            //Create(new Recipient("Hans Peter", "Doskozil", "Industriestrasse 39", "A-1210", "Wien"));
            //Create(new Recipient("August", "Wöginger", "Sonnenhang 7", "A-4779", "Andorf"));
        }

        public int Create(Recipient r)
        {
			r.Id = r_id;
			r_id++;
			recipients.Add(r);
			return r.Id;
        }

        public void Delete(int id)
        {
            Recipient r = recipients.SingleOrDefault(item => item.Id == id);
            recipients.Remove(r);
        }

        public Recipient GetById(int id)
        {
            return recipients.SingleOrDefault(item => item.Id == id);
        }

        public void Update(Recipient r)
        {
            Recipient r2 = recipients.Find(item => item.Id == r.Id);
            r = r2;
        }
    }
}
