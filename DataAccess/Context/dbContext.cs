using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class dbContext
    {
        static readonly object Padlock = new object();


        public ReferLocalsEntities db
        {
            get
            {
                lock (Padlock)
                {
                    if (_db == null)
                    {
                        _db = new ReferLocalsEntities();
                        _db.Database.CommandTimeout = 200;
                       // _db.Configuration.ProxyCreationEnabled = false;
                    }
                }
                return _db;
            }

        }

        private ReferLocalsEntities _db;

    }
}
