using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dblp.Data;
using Dblp.Data.Interfaces;
using Dblp.Domain.Concrete;
using Dblp.Domain.Interfaces;
using Ninject;
using Ninject.Parameters;

namespace Dblp.Domain.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver 
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IDblpRepository>().To<EfRepository>();
            _kernel.Bind<IDblpDataStore>().To<EfDblpDataStore>();
            _kernel.Bind<IStatusRepository>().To<StatusRepository>();
            
                //_kernel.Bind<IDblpDataStore>().To<InMemoryDataStore>().InSingletonScope().WithConstructorArgument("pathTodblpXml", @"D:\dblp\dblp.xml").WithConstructorArgument("pathToBhtFolder", @"D:\dblp\bht\db\conf");
            _kernel.Bind<IBibTeXContentProvider>().To<ConstantBibTexContentProvider>();
        }
    }
}