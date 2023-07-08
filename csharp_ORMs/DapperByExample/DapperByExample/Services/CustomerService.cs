using Dapper;
using System.Collections.Generic;
using System.Linq;
using DapperByExample.Models;


namespace DapperByExample.Services
{
    //ref - https://github.com/Hytm/CockroachDB-Dapper-Example/blob/main/Controllers/EmployeesController.cs
    public class CustomerService : ICRUDService<Customer>
    {
        public bool Insert(Customer obj)
        {
            var command = @"INSERT INTO [customers] ([cust_name],[cust_addess]) VALUES (@cust_name,@cust_addess)";
            var parms = new { obj.Cust_name, obj.Cust_addess };
            var result =  General.db.GetConnection().Execute(command, parms);
            return result > 0;
        }

        public long InsertReturnId(Customer obj)
        {
            var command = @"INSERT INTO [customers] ([cust_name],[cust_addess]) VALUES (@cust_name,@cust_addess)";
            var parms = new { obj.Cust_name, obj.Cust_addess };
            var result = General.db.GetConnection().Execute(command, parms);
            if (result > 0)
                return long.Parse(General.db.GetConnection().ExecuteScalar("SELECT last_insert_rowid()").ToString()); //on mysql & sqlserver use - "SELECT @@IDENTITY"
            else
                return 0;
        }

        public Customer Get(long id)
        {
            var command = @"SELECT * FROM customers WHERE cust_id = @cust_id";
            var parms = new { cust_id = id };
            return General.db.GetConnection().QueryFirst<Customer>(command, parms);
        }

        public List<Customer> GetList()
        {
            var command = @"SELECT * FROM customers";
            return General.db.GetConnection().Query<Customer>(command).ToList();
        }

        public bool Update(Customer obj)
        {
            var command = @"UPDATE [customers] SET [cust_name] = @cust_name,[cust_addess] = @cust_addess where cust_id = @cust_id";
            var result = General.db.GetConnection().Execute(command, obj);
            return result > 0;
        }

        public bool Delete(long id)
        {
            var command = @"DELETE FROM customers WHERE cust_id = @cust_id";
            var parms = new { cust_id = id };
            var result = General.db.GetConnection().Execute(command, parms);
            return result > 0;
        }

        //async
        //public async Task<bool> Create(Customer obj)
        //{
        //    var command = @"INSERT INTO [customers] ([cust_name],[cust_addess]) VALUES (@cust_name,@cust_addess)";
        //    var parms = new { obj.Cust_name, obj.Cust_addess };
        //    var result = await General.db.GetConnection().ExecuteAsync(command, parms);
        //    return result > 0;
        //}
        //public async Task<Customer> Get(Guid id)
        //{
        //    var command = @"SELECT * FROM customers WHERE cust_Id = @Id";
        //    var parms = new { Id = id };
        //    var customers = await General.db.GetConnection().QueryAsync<Customer>(command, parms);
        //    return customers.FirstOrDefault();
        //}
        //public async Task<List<Customer>> GetList()
        //{
        //    var command = @"SELECT * FROM customers";
        //    var customers = await General.db.GetConnection().QueryAsync<Customer>(command);
        //    return customers.ToList();
        //}
        //public async Task<Customer> Update(Customer obj)
        //{
        //    var command = @"UPDATE [customers] SET [cust_name] = @cust_name,[cust_addess] = @cust_addess where cust_id = @cust_id";
        //    var result = await General.db.GetConnection().ExecuteAsync(command, obj);
        //    return result > 0 ? obj : null;
        //}
        //public async Task<bool> Delete(Guid id)
        //{
        //    var command = @"DELETE FROM customers WHERE cust_id = @cust_id";
        //    var parms = new { cust_id = id };
        //    var result = await General.db.GetConnection().ExecuteAsync(command, parms);
        //    return result > 0;
        //}
    }
}
