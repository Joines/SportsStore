using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBinding();
        }

        private void AddBinding()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductID = 1, Name = "Football", Price = 25 },
                new Product { ProductID = 2, Name = "Surf board", Price = 179 },
                new Product { ProductID = 3, Name = "Running shoes", Price = 95 }
            });

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}