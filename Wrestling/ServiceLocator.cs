using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrestling
{
    public sealed class ServiceLocator
    {
        private static WrestlingService _wrestling;
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


        public WrestlingService Wrestling
        {
            get
            {
                if(_wrestling == null)
                {
                    _wrestling = new WrestlingService();
                }
                return _wrestling;
            }
        }
    }
}
