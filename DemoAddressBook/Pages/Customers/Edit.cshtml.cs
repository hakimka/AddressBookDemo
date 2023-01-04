using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DemoAddressBook.Pages.Customers
{
    public class EditModel : PageModel
    {
        public Customer aCustomer = new Customer();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
        {
			aCustomer.id = Request.Form["id"];
			aCustomer.firstName = Request.Form["fname"];
			aCustomer.lastName = Request.Form["lname"];
			aCustomer.emailAddress = Request.Form["email"];
			aCustomer.postAddressStreet = Request.Form["address"];
			aCustomer.phoneNumber = Request.Form["phone"];


			// put some filters to parse and validate names,phone, address and email
			if (aCustomer.firstName.Length == 0 || aCustomer.lastName.Length == 0)
			{
				errorMessage = "Empty First or Last name";
				return;
			}

			try
			{
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "UPDATE ToyotaDemo.dbo.tblCustomer " +
								" SET    firstName= @fname   , lastName =@lname," +
								"        emailAddress =@email, postAddressStreet=@address, phoneNumber =@phone  " +
								" WHERE id = @id ";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@fname", aCustomer.firstName);
						command.Parameters.AddWithValue("@lname", aCustomer.lastName);
						command.Parameters.AddWithValue("@email", aCustomer.emailAddress);
						command.Parameters.AddWithValue("@address", aCustomer.postAddressStreet);
						command.Parameters.AddWithValue("@phone", aCustomer.phoneNumber);
						command.Parameters.AddWithValue("@id"   , aCustomer.id);
						command.ExecuteNonQuery();

					}

				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			Response.Redirect("Index");
		}

		public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
				string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "select * from  ToyotaDemo.dbo.tblCustomer " +
								"WHERE id =  @id";

                    
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						
						using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
								// Id
								if (!reader.IsDBNull(0))
								{
									aCustomer.id = "" + reader.GetInt32(0);
								}
								else
									aCustomer.id = "0";

								// First Name
								if (!reader.IsDBNull(1))
								{
									aCustomer.firstName = reader.GetString(1);
								}
								else
									aCustomer.firstName = "";


								// Last Name
								if (!reader.IsDBNull(2))
								{
									aCustomer.lastName = reader.GetString(2);
								}
								else
									aCustomer.lastName = "";


								// Email 
								if (!reader.IsDBNull(3))
								{
									aCustomer.emailAddress = reader.GetString(3);
								}
								else
									aCustomer.emailAddress = "";

								// Address
								if (!reader.IsDBNull(4))
								{
									aCustomer.postAddressStreet = reader.GetString(4);
								}
								else
									aCustomer.postAddressStreet = "";


								// Phone Number
								if (!reader.IsDBNull(5))
								{
									aCustomer.phoneNumber = reader.GetString(5);
								}
								else
									aCustomer.phoneNumber = "";

							}
						}

					}

				}

			} catch (Exception ex)
            {
                errorMessage = ex.Message;

            }

        }

    }
}
