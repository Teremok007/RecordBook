using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordBook.DAL;

namespace RecordBook
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRecordRepository>().To<EFRepository>();
        }
    }
}