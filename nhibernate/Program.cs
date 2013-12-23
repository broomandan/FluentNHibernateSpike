using System;
using System.CodeDom;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using nhibernateSpike.Domain;

namespace nhibernateSpike
{
    internal class Program
    {
        /// <summary>
        /// Fluent NHibernate example application.
        /// </summary>
        /// <remarks>Please look into data model diagram included in the solution.</remarks>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            CreateAndPopulateDb();
            Console.ReadLine();
        }

        private static void CreateAndPopulateDb()
        {
            ISessionFactory sessionFactory = CreateSessionFactory();
            using (ISession session = sessionFactory.OpenSession())
            {

                PopulateDb(session);
                ReadDb(session);
                Console.ReadLine();
            }
        }

        private static void ReadDb(ISession session)
        {
            using (session.BeginTransaction())
            {
                var applicationInstance = session.CreateCriteria(typeof(ApplicationInstance))
                    .List<ApplicationInstance>();

                foreach (var app in applicationInstance)
                {
                    Console.WriteLine("ID:{0} - Application Name:{1} - Version:{2}, machinename{3}", app.Id, app.Application.Name, app.Version, app.Host.MachineName);
                }
            }
        }

        private static void PopulateDb(ISession session)
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                var pa = new Product() { Name = "Awesome product" };

                var webApplication = new Application { Name = "Web application", Type = ApplicationType.Web };
                var windowsService = new Application { Name = "MyApplication Windows Service ", Type = ApplicationType.Service };
                var winformApplication = new Application { Name = "MyApplication Controller", Type = ApplicationType.WinForm };

                var host1 = new Host
                {
                    ClrVersion = "4.5",
                    Is64BitProcess = true,
                    Is64BitOperatingSystem = true,
                    MachineName = System.Environment.MachineName,
                    OsVersion = System.Environment.OSVersion.VersionString,
                    Platform = System.Environment.OSVersion.Platform.ToString(),
                    ProcessorCount = 8,
                    UserName = "RobertB"
                };
                var host2 = new Host
                {
                    ClrVersion = "3.5",
                    Is64BitProcess = true,
                    Is64BitOperatingSystem = false,
                    MachineName = "Win8",
                    OsVersion = "18.65",
                    Platform = "Win81",
                    ProcessorCount = 4,
                    UserName = "RobertK"
                };


                var office1Service = new ApplicationInstance
                {
                    Version = "13.2.3.4",
                    UpdatDate = new DateTime(2013, 12, 5),
                    UpdateStatus = true,
                    UpdateNotes = "Service Initial installation"
                };
                var office2Service = new ApplicationInstance
                {
                    Version = "13.2.3.4",
                    UpdatDate = new DateTime(2013, 12, 5),
                    UpdateStatus = true,
                    UpdateNotes = "Service Initial installation"
                };

                var office2Winform = new ApplicationInstance
                {
                    Version = "10.21.3.4",
                    UpdatDate = new DateTime(2013, 12, 6),
                    UpdateStatus = true,
                    UpdateNotes = "MyApplication Controller Initial installation!"
                };

                host1.AddApplicationInstances(office1Service);
                host1.AddApplicationInstances(office2Service);
                host2.AddApplicationInstances(office2Winform);

                windowsService.AddApplicationInstances(office1Service);
                windowsService.AddApplicationInstances(office2Service);
                winformApplication.AddApplicationInstances(office2Winform);

                pa.AddApplication(windowsService);
                pa.AddApplication(winformApplication);
                pa.AddApplication(webApplication);

                session.SaveOrUpdate(pa);

                transaction.Commit();
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(c => c
                        .FromConnectionStringWithKey("MyConnectionString"))
                    .ShowSql())
                .Mappings(x => x
                    .FluentMappings.AddFromAssemblyOf<Product>())
                 .ExposeConfiguration(DropCreateSchema)
                .BuildSessionFactory();
        }

        private static void DropCreateSchema(Configuration cfg)
        {
            new SchemaExport(cfg)
                .Create(false, true);
        }

        // Updates the database schema if there are any changes to the model
        private static void UpdateSchema(Configuration cfg)
        {
            new SchemaUpdate(cfg);
        }
    }
}