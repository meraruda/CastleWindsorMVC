using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;

namespace CastleWindsor
{
    class Program
    {
        static void Main(string[] args)
        {
#if DynamicProxy

            var rex = Freezable.MakeFreezable<Pet>();
            rex.Name = "Rex";

            Console.WriteLine(Freezable.IsFreezable(rex) ? "Rex is freezable!" : "Rex is not freezable. Something");
            Console.WriteLine(rex.ToString());
            Console.WriteLine("Add 50 yeadrs");
            rex.Age += 50;
            Console.WriteLine("Age: {0}", rex.Age);
            rex.Deceased = true;

            Console.WriteLine("Deceased: {0}", rex.Deceased);
            Freezable.Freeze(rex);

            try
            {
                rex.Age++;
            }
            catch(ObjectDisposedException ex)
            {
                Console.WriteLine("Oups. it's frozen. Can't change that anymore");
            }

            Console.ReadLine();
#endif

#if Ioc
            var container = new WindsorContainer();

            container.Install(FromAssembly.This());

            var myObject = container.Resolve<Rowan>();
            //var myObject = new Rowan(new Dependency1(), new Dependency2());

            myObject.DoSomething();

            Console.ReadKey();
#endif

        }
    }

    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().Pick().WithServiceAllInterfaces());
        }
    }

    public class Rowan
    {
        private IDenpendency1 object1;
        private IDenpendency2 object2;

        public Rowan(IDenpendency1 dependency1, IDenpendency2 dependency2)
        {
            object1 = dependency1;
            object2 = dependency2;
        }

        public void DoSomething()
        {
            object1.SomeObject = "Hello 1";
            object2.SomeOtherObject = "Hello 2";

            Console.WriteLine($"{object1.SomeObject}\n{object2.SomeOtherObject}");
        }
    }

    public interface IDenpendency2
    {
        object SomeOtherObject { get; set; }
    }

    public interface IDenpendency1
    {
        object SomeObject { get; set; }
    }

    public class Dependency1 : IDenpendency1
    {
        public object SomeObject { get; set; }
    }

    public class Dependency2 : IDenpendency2
    {
        public object SomeOtherObject { get; set; }
    }
}
