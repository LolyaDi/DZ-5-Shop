using System;
using System.Data;

namespace Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataSet = new DataSet("ShopDB");

            var customerIdColumn = new DataColumn("CustomerId", typeof(Guid));
            var productIfColumn = new DataColumn("ProductId", typeof(Guid));
            var orderIdColumn = new DataColumn("OrderId", typeof(Guid));
            var employeeIdColumn = new DataColumn("EmployeeId", typeof(Guid));

            customerIdColumn.AllowDBNull =
                productIfColumn.AllowDBNull =
                    orderIdColumn.AllowDBNull =
                        employeeIdColumn.AllowDBNull = false;
            
            var ordersTable = new DataTable("Orders");
            ordersTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(Guid))
                {
                    AllowDBNull = false
                },
                customerIdColumn,
                productIfColumn
            });

            var customersTable = new DataTable("Customers");
            customersTable.Columns.AddRange(new DataColumn[]
            {
                 new DataColumn("Id", typeof(Guid))
                 {
                    AllowDBNull = false
                 },
                 new DataColumn("FullName", typeof(string))
                 {
                    Unique = true,
                    AllowDBNull = false
                 }
            });

            var employeesTable = new DataTable("Employees");
            employeesTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(Guid))
                {
                    AllowDBNull = false
                },
                new DataColumn("FullName", typeof(string))
                {
                    Unique = true,
                    AllowDBNull = false
                }
            });

            var orderDetailsTable = new DataTable("OrderDetails");
            orderDetailsTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(Guid))
                {
                    AllowDBNull = false
                },
                orderIdColumn,
                employeeIdColumn,
                new DataColumn("Date", typeof(DateTime))
                {
                    AllowDBNull = false
                }
            });

            var productsTable = new DataTable("Products");
            productsTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(Guid))
                {
                    AllowDBNull = false
                },
                new DataColumn("Name", typeof(string))
                {
                    Unique = true,
                    AllowDBNull = false
                }
            });

            ordersTable.PrimaryKey = new DataColumn[] { ordersTable.Columns["Id"] };
            customersTable.PrimaryKey = new DataColumn[] { customersTable.Columns["Id"] };
            employeesTable.PrimaryKey = new DataColumn[] { employeesTable.Columns["Id"] };
            orderDetailsTable.PrimaryKey = new DataColumn[] { orderDetailsTable.Columns["Id"] };
            productsTable.PrimaryKey = new DataColumn[] { productsTable.Columns["Id"] };

            dataSet.Tables.AddRange(new DataTable[]
            {
                ordersTable,
                customersTable,
                employeesTable,
                orderDetailsTable,
                productsTable
            });

            dataSet.Relations.Add("CustomersOrders", customersTable.Columns["Id"], ordersTable.Columns["CustomerId"]);
            dataSet.Relations.Add("ProductsOrders", productsTable.Columns["Id"], ordersTable.Columns["ProductId"]);

            dataSet.Relations.Add("OrdersOrderDetails", ordersTable.Columns["Id"], orderDetailsTable.Columns["OrderId"]);
            dataSet.Relations.Add("EmployeesOrderDetails", employeesTable.Columns["Id"], orderDetailsTable.Columns["EmployeeId"]);
        }
    }
}
