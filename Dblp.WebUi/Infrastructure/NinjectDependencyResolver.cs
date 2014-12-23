using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dblp.Domain.Abstract;
using Dblp.Domain.Concrete;
using Ninject;

namespace Dblp.WebUi.Infrastructure
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
            _kernel.Bind<IDblpRepository>().To<XmlRepository>();
            _kernel.Bind<IBibTeXContentProvider>().To<ConstantBibTexContentProvider>();
        }
    }
}