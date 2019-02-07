using DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public sealed class ServiceLocator
    {
        private static WrestlingDAL _wrestlingDal;
        private static volatile ServiceLocator instance;
        private static object syncRoot = new Object();

        private ServiceLocator() { }

        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ServiceLocator();
                    }
                }

                return instance;
            }
        }


        public WrestlingDAL WrestlingDAL
        {
            get
            {
                if(_wrestlingDal == null)
                {
                    _wrestlingDal = new WrestlingDAL();
                }
                return _wrestlingDal;
            }
        }
    }
}
