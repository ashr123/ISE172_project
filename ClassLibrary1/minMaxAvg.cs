using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using DataTier.DataEntries;
using System.Timers;
using DataTier.Loggers;
using System.Threading;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;


namespace LogicTier
{
    public class minMaxAvg
    {


        public static int avgPrice(int cmd)
        {
            int avgPrice = -1;

            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["historyConnectionString"].ConnectionString))
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand("select Avg(price) AS AveragePrice from items where commodity='" + cmd + "'and timestamp>= DATEADD(mi, -120, GETUTCDATE())", myConnection);
                SqlDataReader myDataReader = myCommand.ExecuteReader();
                if (myDataReader.HasRows)
                {
                    myDataReader.Read();
                    try
                    {
                        Trace.WriteLine(myDataReader[0].ToString());
                        Trace.WriteLine(Double.Parse(myDataReader[0].ToString()));
                        Double avgPriceDouble = Double.Parse(myDataReader[0].ToString());
                        avgPrice = Convert.ToInt32(avgPriceDouble);
                        return avgPrice;
                    }
                    catch
                    {
                        return avgPrice;
                    }
                }
            }
            return avgPrice;

        }

        public static int minPrice(int cmd)
        {
            int minPrice = -1;

            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["historyConnectionString"].ConnectionString))
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand("select MIN(Price) AS SmallestPrice items where commodity='" + cmd + "'and timestamp>= DATEADD(mi, -120, GETUTCDATE())", myConnection);
                SqlDataReader myDataReader = myCommand.ExecuteReader();
                if (myDataReader.HasRows)
                {
                    myDataReader.Read();
                    try
                    {
                        Trace.WriteLine(myDataReader[0].ToString());
                        Trace.WriteLine(Double.Parse(myDataReader[0].ToString()));
                        Double minPriceDouble = Double.Parse(myDataReader[0].ToString());
                        minPrice = Convert.ToInt32(minPriceDouble);
                        return minPrice;
                    }
                    catch
                    {
                        return minPrice;
                    }
                }
            }
            return minPrice;
        }

        public static int maxPrice(int cmd)
        {
            int maxPrice = -1;

            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["historyConnectionString"].ConnectionString))
            {
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand("select MAX(Price) AS LargestPrice from items where commodity='" + cmd + "'and timestamp>= DATEADD(mi, -120, GETUTCDATE())", myConnection);
                SqlDataReader myDataReader = myCommand.ExecuteReader();
                if (myDataReader.HasRows)
                {
                    myDataReader.Read();
                    try
                    {
                        Trace.WriteLine(myDataReader[0].ToString());
                        Trace.WriteLine(Double.Parse(myDataReader[0].ToString()));
                        Double maxPriceDouble = Double.Parse(myDataReader[0].ToString());
                        maxPrice = Convert.ToInt32(maxPriceDouble);
                        return maxPrice;
                    }
                    catch
                    {
                        return maxPrice;
                    }
                }
            }
            return maxPrice;
        }
    }
}