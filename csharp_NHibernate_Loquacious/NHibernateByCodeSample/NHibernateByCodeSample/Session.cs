using DBManager.DBASES;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace NHibernateByCodeSample
{
    public static class DBASE
    {
        private static ISessionFactory sessionFactory;
        private static ISession session;

        public static SQLiteClass db; 

        public static ISession Session
        {
            get
            {
                if (session == null || !session.IsOpen)
                {
                    if (db == null)
                        session = sessionFactory.OpenSession();
                    else
                        session = sessionFactory.WithOptions().Connection(db.GetConnection()).OpenSession();
                }

                return session;
            }
        }

        public static void Initialize()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes().Where(x => x.Namespace.EndsWith(".Domain") || x.Namespace.EndsWith(".Maps")));
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var cfg = new Configuration();
            cfg.DataBaseIntegration(d =>
            {
                d.ConnectionString = string.Format("Data Source={0};Version=3;New=False;", Application.StartupPath + "\\..\\..\\..\\dbase.db");
                d.Dialect<SQLiteDialect>();
                d.Driver<SQLite20Driver>();
#if DEBUG
                d.LogSqlInConsole = true;
                d.LogFormattedSql = true;
#endif
            });

            cfg.AddMapping(domainMapping);

            sessionFactory = cfg.BuildSessionFactory();
        }


        public static void InitializeWithExistingConnection()
        {
            string connection_string = string.Format("Data Source={0};Version=3;New=False;", Application.StartupPath + "\\..\\..\\..\\dbase.db");
            db = new SQLiteClass(connection_string);

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes().Where(x=>x.Namespace.EndsWith(".Domain") || x.Namespace.EndsWith(".Maps")));
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var cfg = new Configuration();
            cfg.DataBaseIntegration(d =>
            {
                d.ConnectionProvider<DriverConnectionProvider>();
                d.Driver<SQLite20Driver>();
                d.Dialect<NHibernate.Dialect.SQLiteDialect>();
                d.ConnectionString = connection_string;          //you still need to provide the connection string within the configuration to ensure NHibernate functions correctly.
                d.IsolationLevel = IsolationLevel.ReadCommitted; //ensures that a transaction only sees committed changes made by other transactions, providing a higher level of data consistency.
#if DEBUG
                d.LogSqlInConsole = true;
                d.LogFormattedSql = true;
#endif
            });

            cfg.AddMapping(domainMapping);

            sessionFactory = cfg.BuildSessionFactory();
        }

        public static void CloseSession()
        {
            if (session != null && session.IsOpen)
                session.Close();
        }
    }
}
