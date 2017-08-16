using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleWindsor
{
    internal interface IFreezable
    {
        bool IsFrozen { get; }
        void Freeze();
    }

    internal class FreezableInterceptor : IInterceptor, IFreezable
    {
        private bool _IsFrozen;

        public void Freeze()
        {
            _IsFrozen = true;
        }

        public bool IsFrozen
        {
            get { return _IsFrozen; }
        }        

        public void Intercept(IInvocation invocation)
        {
            if(_IsFrozen && invocation.Method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase))
                throw new Exception("ObjectFrozenException");

            invocation.Proceed();
        }
    }

    public static class Freezable
    {
        private static readonly IDictionary<object, IFreezable> _freezables = new Dictionary<object, IFreezable>();
        private static readonly ProxyGenerator _generator = new ProxyGenerator();

        public static bool IsFreezable(object obj)
        {
            return obj != null && _freezables.ContainsKey(obj);
        }

        public static void Freeze(object freezable)
        {
            if(!IsFreezable(freezable))
                throw new Exception("NotFreezableObjectException");

            _freezables[freezable].Freeze();
        }

        public static bool IsFrozen(object freezable)
        {
            return IsFreezable(freezable) && _freezables[freezable].IsFrozen;
        }

        public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class, new()
        {
            var freezableInterceptor = new FreezableInterceptor();
            var proxy = _generator.CreateClassProxy<TFreezable>(new CallLoggingInterceptor(), freezableInterceptor);

            _freezables.Add(proxy, freezableInterceptor);
            return proxy;
        }
    }

    internal class CallLoggingInterceptor : IInterceptor
    {
        public CallLoggingInterceptor()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }

    public class Pet
    {
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual bool Deceased { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Deceased: {Deceased}";
        }
    }

}
