
using System;
using System.Data;
using System.Data.SqlClient;
namespace DemoProject.Models.UserRepository;
public class UserRepository
{
    // FOR GET CONNECTION STRING FROM APPSETTING
    public static IConfigurationBuilder Getbuilder()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        return builder;
    }
    private static IConfigurationBuilder builder = Getbuilder();
    public static string GetConnectionString()
    {
        return builder.Build().GetValue<string>("ConnectionStrings:conndemoproject");
    }

    // REPOSITORY PATTERN FOR CRUD
    public interface IUser
    {
        List<Users.User> GetAllUsers();
        Users.User GetUserDetailsById(int id);
        void AddEditUsers(Users.User user);
        void DeleteUser(int id);
        List<Users.States> GetAllStates();
        List<Users.Cities> GetAllCitiesByState(int id);
    }

    public class UserFunctions : IUser
    {
        public void AddEditUsers(Users.User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AddEditUsers", con);
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.EmailId);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@StateId", user.StateId);
                    cmd.Parameters.AddWithValue("@CityId", user.CityId);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteUser(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("DeleteUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id",Id);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public List<Users.Cities> GetAllCitiesByState(int id)
        {
            List<Users.Cities> objCityList = new List<Users.Cities>();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("GetAllCitiesByState", con);
                    cmd.Parameters.AddWithValue("@StateId", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var obj = new Users.Cities()
                        {
                            CityId = Convert.ToInt32(rdr["Id"]),
                            StateId = Convert.ToInt32(rdr["StateId"]),
                            CityName = rdr["CityName"].ToString(),
                        };
                        objCityList.Add(obj);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objCityList;
        }

        public List<Users.States> GetAllStates()
        {
            List<Users.States> objStateList = new List<Users.States>();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("GetAllStates", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var obj = new Users.States()
                        {
                            StateId = Convert.ToInt32(rdr["Id"]),
                            StateName = rdr["StateName"].ToString(),
                        };
                        objStateList.Add(obj);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objStateList;
        }

        public List<Users.User> GetAllUsers()
        {
            List<Users.User> objUserList = new List<Users.User>();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("GetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var obj = new Users.User()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = rdr["Name"].ToString(),
                            EmailId = rdr["Email"].ToString(),
                            Address = rdr["Address"].ToString(),
                            Phone = rdr["Phone"].ToString(),
                            StateId = Convert.ToInt32(rdr["StateId"]),
                            CityId = Convert.ToInt32(rdr["CityId"]),
                            StateName = rdr["StateName"].ToString(),
                            CityName = rdr["CityName"].ToString()
                        };
                        objUserList.Add(obj);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return objUserList;
        }

        public Users.User GetUserDetailsById(int id)
        {
            var obj = new Users.User();
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("GetUserDetailsById", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        obj.Id = Convert.ToInt32(rdr["Id"]);
                        obj.Name = rdr["Name"].ToString();
                        obj.EmailId = rdr["Email"].ToString();
                        obj.Phone = rdr["Phone"].ToString();
                        obj.Address = rdr["Address"].ToString();
                        obj.StateId = Convert.ToInt32(rdr["StateId"]);
                        obj.CityId = Convert.ToInt32(rdr["CityId"]);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return obj;
        }
    }

}
