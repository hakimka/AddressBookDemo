using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Xml;

namespace DemoAddressBook.Pages.Customers
{
    public class IndexModel : PageModel
    {

        public List <Customer> customers = new List<Customer> ();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; 
                
                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open ();
                    string sql = "select * from ToyotaDemo.dbo.tblCustomer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read ())
                            {
                                Customer aCustomer   = new Customer ();

                                // Id
								if (!reader.IsDBNull(0))
                                {
									aCustomer.id = "" + reader.GetInt32(0);
								} else
									aCustomer.id = "0" ;

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




								

                                customers.Add(aCustomer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.ToString());

            }
        }
    }

        public class Customer 
        {

        public string id;       
        public string firstName;
        public string lastName;
        public string emailAddress;       
        public string postAddressStreet;
        public string phoneNumber; 

        }
}
