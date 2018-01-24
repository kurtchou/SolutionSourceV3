using System.Configuration;
using System.Data.SqlClient;

namespace SolutionSource.Services
{
    public static class DB
    {
        public static SqlConnection Get()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["SolutionSourceCString"].ToString());
        }
    }
}