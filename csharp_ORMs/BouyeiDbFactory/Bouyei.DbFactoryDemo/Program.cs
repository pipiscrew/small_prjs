﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using Bouyei.DbFactory;

namespace Bouyei.DbFactoryDemo
{
    using Bouyei.DbFactory.DbAdoProvider;
    using Bouyei.DbFactory.DbMapper;
    using Bouyei.DbEntities;

    using Bouyei.DbFactory.DbSqlProvider.SqlFunctions;
    using System.Linq.Expressions;

    class Program
    {
        static void Main(string[] args)
        {
            //SelectDo<User> todo = new SelectDo<User>();
            //var rt = todo.ToDo(x => new
            //{
            //    x.id,
            //    x.uname
            //});

             SqlDemo();
            //string str = "Server=127.0.0.1;Port=5432;Userid=postgres;password=12356;database=postgres;";
            //Bulkcopy(str);
            //AdoDemo(str);
            //OrmDemo(str);
        }

        private static void SqlDemo()
        {
            //生成简单查询脚本
            ISqlProvider sqlProvider = SqlProvider.CreateProvider(FactoryType.Oracle);
            var istr = sqlProvider.Insert<User, dynamic>(x => new
            {
                x.status,
                x.logintime
            }).Values(x => new
            {
                status = UStatus.Disabled,
                logintime=DateTime.Now

            }).SqlString;

            var bv = sqlProvider.Select<User>().From().Where(x => x.id == 1).Top(FactoryType.Oracle, 0, 10).SqlString;

            //动态对象列映射查询脚本
            var nsql =sqlProvider.Select<User, dynamic>(x => new { x.uname }).From<User>().SqlString;

            Dictionary<string, string> onWhere = new Dictionary<string, string>();
            onWhere.Add("id", "id");
            onWhere.Add("score", "score");

            DateTime d = DateTime.Now;
            var s = sqlProvider.Insert<User>().Values(new User()
            {
                gentime = d,
                uname = "sdfsd",
                score = 11
            }).SqlString;

            //join 
            string join = sqlProvider.Select<User>().Join<User, User, User>(x => x.id == 1, y => y.id == 2, onWhere).SqlString;

            join = sqlProvider.Select<User>().Join<User, User, User>(x => x.id == 1, y => y.id == 2, (a, b) => a.id == b.id && a.uname == b.uname).SqlString;

            //group by 
            string sqlgroupby = sqlProvider.Select<User>().Count().From<User>()
                .Where(x => x.uage == 1).GroupBy<User>().SqlString;

            //in 语法
            string[] values = new string[] { "a", "b" };
            var inSql = sqlProvider.Select<User>().From().Where(x => values.Contains(x.uname)).SqlString;

            //like 语法 '%bouyei%'
            var likeSql = sqlProvider.Select<User>().From().Where(x => x.uname.Contains("bouyei")).SqlString;

            //like 语法'bouyei%'
            var beginSql = sqlProvider.Select<User>().From().Where(x => x.uname.StartsWith("bouyei") || x.uname.StartsWith("bb")).SqlString;

            //like 语法'%bouyei'
            var endSql = sqlProvider.Select<User>().From().Where(x => x.uname.EndsWith("bouyei")).SqlString;

            //select count(*) from user where id=1
            string commandText = sqlProvider.Select<User>(new Count("*")).From<User>().Where(x => x.id == 1).SqlString;

            //function 
            string sqlfun = sqlProvider.Select<User>(new Max("age")).From<User>().Where(x => x.uage > 20).SqlString;

            //order by
            var osql = sqlProvider.Select<User>().From<User>().OrderBy(SortType.Asc, "name").SqlString;

            var dic = new Dictionary<string, object>();
            dic.Add("name", "hellow");
            dic.Add("age", 0);
            dic.Add("score", 1.0);

            //MappedName 测试
            var sqls = sqlProvider.Insert<UserDto>().Values(new UserDto[] { new UserDto() {
                 Pwd="ds",
                  UserName="d"
            } }).SqlString;

            //查询
            var sql = sqlProvider.Select<User>()
                 .From().Where(x => x.id == 1).Top(FactoryType.PostgreSQL, 10).SqlString;

            //修改
            sql = sqlProvider.Update<User>()
                .Set(new User() { uname = "bouyei" })
                .Where<User>(x => x.id == 1 || (x.uname == "b" && x.score == 2)).SqlString;

            //删除
            sql = sqlProvider.Delete()
                .From<User>().Where(x => x.uname == "bouyei").SqlString;

            //插入
            sql = sqlProvider.Insert<User>()
                .Values(new User[] {
                new User() { uname ="hello", uage=12 }
                ,new User() { uname="bouyei",uage=23} }).SqlString;
        }

        private static void Bulkcopy(string str)
        {
            //DataTable dt = new DataTable();
            //dt.TableName = "gz_gwqhzs";

            //dt.Columns.Add("sprovince",typeof(string));
            //dt.Columns.Add("sbiaotuzhidi",typeof(double));

            //dt.Rows.Add("贵州", 111.2);
            //dt.Rows.Add("贵州", 111.4);
            //dt.Rows.Add("贵州", 114.3);
            //dt.Rows.Add("贵州", 111.2);
            //dt.Rows.Add("贵州", 131.2);
            //dt.Rows.Add("贵州", 131.2);
            //dt.Rows.Add("贵州", 115.2);
            //dt.Rows.Add("贵州", 1141.2);
            //rt.Result.TableName = "gz_gwqhzs";
            //var brt = pgProvider.BulkCopy(new BulkParameter(rt.Result));
            //if (string.IsNullOrEmpty(brt.Info) == false)
            //    throw new Exception(brt.Info);

            // rt.Result.TableName = "gz_weight";        

            // IAdoProvider dbprovider = AdoProvider.CreateProvider("PORT=5432;DATABASE=GDZL;HOST=localhost;PASSWORD=123456;USER ID=postgres",DbFactory.DbType.PostgreSQL);
            //var brt= dbprovider.BulkCopy(new BulkParameter(rt.Result));


            //DataTable dt = new DataTable();
            //dt.Columns.Add("uname",typeof(string));
            //dt.Columns.Add("upwd", typeof(string));
            //dt.Columns.Add("age",typeof(int));
            //dt.Columns.Add("score", typeof(float));

            //dt.Rows.Add("bouyei", "232a", 12, 239.4);
            //dt.Rows.Add("hell哦", "232a", 12, 239.4);
            //dt.TableName = "luser";

            IAdoProvider dbProvider = AdoProvider.CreateProvider(str, DbFactory.FactoryType.PostgreSQL);

            //var brt= dbProvider.BulkCopy(new BulkParameter(dt));

            fc3d[] fc = new fc3d[] {
                new fc3d(){ fname="dd", fcode=121 },
                new fc3d(){fname="sd",fcode=23 },
                new fc3d(){ fname="个",fcode=2323}
            };

            var param = new CopyParameter<Array>(fc);
            param.TableName = "fc3d";

            var rt = dbProvider.BulkCopy(param);

        }

        private static void AdoDemo(string connectionString)
        {
            IAdoProvider dbProvider = AdoProvider.CreateProvider(connectionString, FactoryType.PostgreSQL);

            //CmdParameter[] cmdParam = new CmdParameter[] {
            //new CmdParameter(){  ParameterName="@uname",Value="hellow", DbType= DbType.String},
            //new CmdParameter(){ParameterName="@uage",Value=32, DbType= DbType.Int32 },
            //new CmdParameter(){ ParameterName="@score",Value=66.66,DbType= DbType.Single}
            //};

            //var cmdrt = dbProvider.ExecuteCmd(new Parameter()
            //{
            //    CommandText = "insert into luser(uname,uage,score) values(@uname,@uage,@score)",
            //    Columns = cmdParam
            //});

            //var p = dbProvider.InsertParameter<User>(new User()
            //{
            //    uname = "bouyei",
            //    uage = 33,
            //    score = 23.44f
            //});

            //mdb测试
            //var db = AdoProvider.CreateProvider(new ConnectionConfig()
            //{
            //    DbName = "F:\\r\\ly\\2017\\52.mdb",
            //    DbType = FactoryType.OleDb
            //});

            //var mrt=db.Query(new Parameter("select * from 52"));

            //var ext = dbProvider.Connect(connectionString);

            //var adort = dbProvider.Query(new Parameter()
            //{
            //    CommandText = "select * from public.db_user"
            //});

            ////
            //var rt= dbProvider.Query<User>(x => x.uage >= 20);

            ////top 语法
            //var takert = dbProvider.PageQuery<User>(x => x.uage == 30, 10);

            ////定义更新
            //var dic = new Dictionary<string, object>();
            //dic.Add("name", "hellow");
            //dic.Add("age", 0);
            //dic.Add("score", 1.0);
            //dbProvider.Update<User>(dic, x => x.id == 1);

            //dbProvider.Delete<User>(x => x.uname == "bouyei");

            //增加
            //var rt = dbProvider.Insert<User>(new User()
            //{
            //    score = 10,
            //    uname = "bouyei",
            //    uage = 30
            //});

            //删除
            // var drt = dbProvider.Delete<User>(x => x.uname == "bouyei" || x.uage > 30);

            //查询
            string[] orderbyColumn = new string[] { "uname" };
            var qrt = dbProvider.QueryOrderBy<User>(x => true,
                c => new { c.uname, c.id },/*动态列名返回*/
                orderbyColumn, SortType.Desc, 0, 10);

            //插入
            //var irt= dbProvider.Insert<User>(x => new
            // {
            //    uname="hello",
            //    id=11
            // });

            //修改
            //var urt = dbProvider.Update<User>(x => new
            //{
            //    uname = "bouyei_hello"
            //}, w => w.id == 11);

        }

        private static void OrmDemo(string connectionString)
        {
            //entity framework 使用例子
            IOrmProvider ormProvider = OrmProvider.CreateProvider(connectionString);
            try
            {
                User item =
                //    new User() {
                //     id=4,
                //      name="bouyei"
                //};//
                ormProvider.GetById<User>(1);
                UserDto ud = new UserDto()
                {
                    UserName = "http://aileenyin.com/"
                };

                //int c= ormProvider.Delete<User>(x => x.id == 3,true);

                //var query = ormProvider.QueryNoTracking<User>(x => true).FirstOrDefault();

                ////使用mapper修改对象
                User u = new User() {
                    uname = "b",
                    id = 1
                };

                User b = new User() {
                    uname = "a",
                    id = 2
                };
                EntityMapper.MapTo(u, b, FilterType.Include, "name");

                ormProvider.Update(item, true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }

    class SelectDo<T>
    {
        public IEnumerable<string> ToDo<R>(Func<T, R> selector)
        {
            var r=selector.Invoke(Activator.CreateInstance<T>());
            return r.GetType().GetProperties().Select(x => x.Name);
        }

        public List<string> FToDo<C,R>(Func<C, R> selector)
        {
            var r = selector.Invoke(Activator.CreateInstance<C>());
            return r.GetType().GetProperties().Select(x => x.Name).ToList();
        }
    }

    [MappedName("db_user")]
    public class UserDto : BaseMapper<UserDto>
    {
        public string UserName { get; set; }

        [Ignore(AttributeType.Ignore)]
        public string Pwd { get; set; }

       public void AddUser(UserDto user)
        {
            base.Insert(user);
        }

        public void DeleteById(string name)
        {
            base.Delete(x => x.UserName == name);
        }

        public void UpdateUser(UserDto dto)
        {
            base.Update(dto, x => x.UserName == "bouyei");
        }

        public List<UserDto> QueryUsers(int page=0,int size=10)
        {
           return base.Select(page, size, x => true);
        }
    }

    public class fc3d:BaseMapper<fc3d>
    {
        public string fname { get; set; }

        public long fcode { get; set; }
    }

    public class BaseMapper<T> : TableMapper<T> where T : class
    {
        public BaseMapper()
        {
            string connstr = "Host=127.0.0.1;Port=5432;User id=postgres;Password=bouyei;Database=postgres;";
            var provider = AdoProvider.CreateProvider(connstr, FactoryType.PostgreSQL);
            Initialized(provider);
        }
    }
}
