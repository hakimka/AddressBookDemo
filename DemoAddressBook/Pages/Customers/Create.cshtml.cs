//
//  Create  - backend code to create records in tblCustomer
//  
//  to do: need to validate data
//




using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.Versioning;

namespace DemoAddressBook.Pages.Customers
{
    public class Index1Model : PageModel
    {
        public Customer aCustomer = new Customer();
        public string errorMessage = "";
		public string successMessage = "";
		public void OnGet()
        {
        }
        public void OnPost()
        {
            // populate customer field from request page
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

			// write to DB	
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO  ToyotaDemo.dbo.tblCustomer " +
                                "(firstName, lastName, emailAddress, postAddressStreet, phoneNumber)  VALUES " +
                                "(@fname, @lname, @email, @address, @phone)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fname", aCustomer.firstName);
						command.Parameters.AddWithValue("@lname", aCustomer.lastName);
						command.Parameters.AddWithValue("@email", aCustomer.emailAddress);
						command.Parameters.AddWithValue("@address", aCustomer.postAddressStreet);
						command.Parameters.AddWithValue("@phone", aCustomer.phoneNumber);
                        command.ExecuteNonQuery();

					}

				}
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            // reset customer data and populate success message 
			aCustomer.firstName = "";
			aCustomer.lastName = "";
			aCustomer.emailAddress = "";
			aCustomer.postAddressStreet = "";
			aCustomer.phoneNumber = "";

			successMessage = "Record Added";

            Response.Redirect("Index");

		}
	}
}
