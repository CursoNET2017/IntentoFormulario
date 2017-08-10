using IntentoFormulario.Repository;
using IntentoFormulario.Service;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Web.Http;
using Unity.WebApi;
using System;
using System.Collections.Generic;
using IntentoFormulario.Models;

namespace IntentoFormulario
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            //Añadimos un Interceptor
            container.AddNewExtension<Interception>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //container.RegisterType<IPersonaService, PersonaService>();//Hay que añadir el interceeptor:

            container.RegisterType<IPersonaRepository, PersonaRepository>();
            //Añadimos Metiendole el Interception
            container.RegisterType<IPersonaService, PersonaService>(
              new Interceptor<InterfaceInterceptor>(),
              new InterceptionBehavior<LoggingInterceptionBehavior>());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }

    class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input,
               GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn result;
            using (var context = new ApplicationDbContext())
            {
                ApplicationDbContext.applicationDbContext = context;
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        result = getNext()(input, getNext);
                        if (result.Exception != null)
                        {
                            throw result.Exception;
                        }

                        context.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("He hecho rollback de la transacción", e);
                    }
                }
            }
            ApplicationDbContext.applicationDbContext = null;
            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private void WriteLog(string message)
        {

        }
    }
}
