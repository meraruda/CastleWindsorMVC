using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using CastleWindsorMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CastleWindsorMVC.Facility
{
    public class EntityFrameworkFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.Register(
                Component.For<IDbContext>()
                .ImplementedBy<MyEntities>()
                .LifestylePerWebRequest(),
                Component.For(typeof(IRepository<>))
                .ImplementedBy(typeof(Repository<>))
                .LifestylePerWebRequest()
                );
        }
    }
}