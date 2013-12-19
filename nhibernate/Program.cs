using System;
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
        }

        private static void CreateAndPopulateDb()
        {
            ISessionFactory sessionFactory = CreateSessionFactory();
            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var pa = new Product() {Name = "Fantastic product"};

                    var webApplication = new Application {Name = "Web application", Type = ApplicationType.Web};
                    var windowsService = new Application {Name = "service application", Type = ApplicationType.Service};
                    var winformApplication = new Application {Name = "Windows form application", Type = ApplicationType.WinForm};

                    var host1 = new Host
                    {
                        ClrVersion = "4.5",
                        Is64BitProcess = true,
                        Is64BitOperatingSystem = true,
                        MachineName = "Win7",
                        OsVersion = new OperatingSystem(new PlatformID(), new Version(10, 5)),
                        ProcessorCount = 8,
                        UserName = "RobertB"
                    };
                    var host2 = new Host
                    {
                        ClrVersion = "3.5",
                        Is64BitProcess = true,
                        Is64BitOperatingSystem = false,
                        MachineName = "Win8",
                        OsVersion = new OperatingSystem(new PlatformID(), new Version(11, 5)),
                        ProcessorCount = 4,
                        UserName = "Tom G"
                    };


                    var office1Service = new ApplicationInstance
                    {
                        Version = "13.2.3.4",
                        UpdatDate = new DateTime(2013, 12, 5),
                        UpdateStatus = true,
                        UpdateNotes = "Service Initial installation",
                        Host = host1
                    };
                    var office2Service = new ApplicationInstance
                    {
                        Version = "13.2.3.4",
                        UpdatDate = new DateTime(2013, 12, 5),
                        UpdateStatus = true,
                        UpdateNotes = "Service Initial installation",
                        Host = host1
                    };
                    var office2Winform = new ApplicationInstance
                    {
                        Version = "10.21.3.4",
                        UpdatDate = new DateTime(2013, 12, 6),
                        UpdateStatus = true,
                        UpdateNotes = "Winform application Initial installation !",
                        Host = host2
                    };

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
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(c => c
                        .FromConnectionStringWithKey("FD-Portal-ConnectionString"))
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