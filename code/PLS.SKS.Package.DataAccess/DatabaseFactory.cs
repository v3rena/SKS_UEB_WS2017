using System;

namespace PLS.SKS.Package.DataAccess
{
    public class DatabaseFactory
    {
        Interfaces.IDatabase create(int type)
        {
            switch(type)
            {
                case 0:
                    return new MockDatabase();
                case 1:
                    return new SQLDatabase();
                default:
                    throw new Exception("Ungueltiger Database Type");
            }
        }
    }
}
