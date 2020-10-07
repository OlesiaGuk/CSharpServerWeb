using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdoNet
{
    class AdoNet
    {
        static void Main()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection state = " + connection.State);
                Console.WriteLine();

                var productsCount = GetProductsCount(connection);
                Console.WriteLine($"Общее количество товаров = {productsCount}");
                Console.WriteLine();

                Console.WriteLine("Добавление категории и товара");
                AddCategoryAndProduct(connection);
                Console.WriteLine();

                Console.WriteLine("Редактирование товара");
                EditProductPrice(connection);
                Console.WriteLine();

                Console.WriteLine("Удаление товара");
                DeleteProduct(connection);
                Console.WriteLine();

                Console.WriteLine("Список товаров (через Reader): ");
                PrintProducts(connection);
                Console.WriteLine();

                Console.WriteLine("Список товаров (через SqlDataAdapter): ");
                PrintProductsDataSet(connection);
            }
        }

        public static int GetProductsCount(SqlConnection connection)
        {
            var productsCountSql = "SELECT COUNT(*) FROM Products";

            using (var command = new SqlCommand(productsCountSql, connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        public static void AddCategoryAndProduct(SqlConnection connection)
        {
            Console.WriteLine("Введите название категории: ");
            var userCategory = Console.ReadLine();

            Console.WriteLine("Введите название товара:");
            var userProductName = Console.ReadLine();

            Console.WriteLine("Введите цену товара:");
            var userProductPrice = Console.ReadLine();

            Console.WriteLine("Введите ID категории товара:");
            var userCategoryId = Console.ReadLine();

            var addCategorySql = "INSERT INTO Categories(Name) VALUES (@userCategory);";
            var addProductSql = "INSERT INTO Products(Name, Price, CategoryID) VALUES (@userProductName, @userProductPrice, @userCategoryId);";

            using (var command = new SqlCommand(addCategorySql + addProductSql, connection))
            {
                command.Parameters.Add(new SqlParameter("@userCategory", userCategory) { SqlDbType = SqlDbType.Text });
                command.Parameters.Add(new SqlParameter("@userProductName", userProductName) { SqlDbType = SqlDbType.Text });
                command.Parameters.Add(new SqlParameter("@userProductPrice", userProductPrice) { SqlDbType = SqlDbType.Int });
                command.Parameters.Add(new SqlParameter("@userCategoryId", userCategoryId) { SqlDbType = SqlDbType.Int });

                var addCommandResult = command.ExecuteNonQuery();
                Console.WriteLine($"Добавлено объектов: {addCommandResult}");
            }
        }

        public static void EditProductPrice(SqlConnection connection)
        {
            var productPrice = 100;
            var productId = 1;
            var productEditingSql = "UPDATE Products SET Price = @productPrice WHERE Id = @productId";

            using (var command = new SqlCommand(productEditingSql, connection))
            {
                command.Parameters.Add(new SqlParameter("@productPrice", productPrice) { SqlDbType = SqlDbType.Int });
                command.Parameters.Add(new SqlParameter("@productId", productId) { SqlDbType = SqlDbType.Int });

                var updateCommandResult = command.ExecuteNonQuery();
                Console.WriteLine($"Обновлено объектов: {updateCommandResult}");
            }
        }

        public static void DeleteProduct(SqlConnection connection)
        {
            var productId = 2;
            var productDeletionSql = "DELETE FROM Products WHERE Id = @productId;";

            using (var command = new SqlCommand(productDeletionSql, connection))
            {
                command.Parameters.Add(new SqlParameter("@productId", productId) { SqlDbType = SqlDbType.Int });

                var deleteCommandResult = command.ExecuteNonQuery();
                Console.WriteLine($"Удалено объектов: {deleteCommandResult}");
            }
        }

        public static void PrintProducts(SqlConnection connection)
        {
            var selectAllProductsSql = "SELECT p.Id, p.Name, p.Price, c.Name AS CategoryName " +
                                       "FROM Products AS p " +
                                       "LEFT JOIN Categories AS c " +
                                       "ON p.CategoryId = c.Id";

            using (var command = new SqlCommand(selectAllProductsSql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine($"{reader.GetName(0),-10}{reader.GetName(1),-10}{ reader.GetName(2),-10}{ reader.GetName(3),-10}");

                    while (reader.Read())
                    {
                        var productId = reader.GetValue(0);
                        var productName = reader.GetValue(1);
                        var productPrice = reader.GetValue(2);
                        var categoryName = reader.GetValue(3);

                        Console.WriteLine($"{productId,-10}{productName,-10}{productPrice,-10}{categoryName,-10}");
                    }
                }
            }
        }

        public static void PrintProductsDataSet(SqlConnection connection)
        {
            var selectAllProductsSql = "SELECT p.Id, p.Name, p.Price, c.Name AS CategoryName " +
                                       "FROM Products AS p " +
                                       "LEFT JOIN Categories AS c " +
                                       "ON p.CategoryId = c.Id";

            var adapter = new SqlDataAdapter(selectAllProductsSql, connection);
            var productsDataSet = new DataSet();

            adapter.Fill(productsDataSet);

            foreach (DataTable dataTable in productsDataSet.Tables)
            {
                for (var i = 0; i < dataTable.Columns.Count; i++)
                {
                    Console.Write($"{dataTable.Columns[i].ColumnName,-10}");
                }

                Console.WriteLine();

                using (var dataTableReader = dataTable.CreateDataReader())
                {
                    while (dataTableReader.Read())
                    {
                        for (var i = 0; i < dataTableReader.FieldCount; i++)
                        {
                            Console.Write($"{dataTableReader.GetValue(i),-10}");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
    }
}