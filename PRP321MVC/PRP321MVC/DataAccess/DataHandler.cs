using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataHandler
    {
        /// <summary>
        /// The Layer responsible for communicating with the Database
        /// </summary>
        private SqlConnection connection;
        private string connectionString;

        //Hacktity hack
        private string connectionStringPrime;
        private DataTable dataTable;
        private SqlDataAdapter dataAdapter;
        private SqlCommand command;


        //hackatity hack
        public DataTable Select(string ProcedureName)
        {
            dataTable = new DataTable();
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    command = new SqlCommand(ProcedureName, connection);
                    command.CommandType = CommandType.Text;
                    dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }

        // Arnold`s Hack
        public void UpdateProfile(SqlCommand cmd)
        {
            using (connection = new SqlConnection(connectionStringPrime))
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = "[dbo].[sp_UpdateProfile]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "";
                connection.Close();
            }
        }

        public DataHandler(string ConnetionString = "Data Source=DESKTOP-EGBJ4HH\\SQLEXPRESS;Initial Catalog=ExamGen;Integrated Security=True") //CONString must be changed to whichever computer it is currently being ran on
        {
            connectionString = ConnetionString;
            connection = new SqlConnection(connectionString);
        }


        public List<string> Select(string tableName, List<string> columns = null, List<string> where = null)
        {
            List<string> tableRecords = new List<string>();

            try
            {
                connection.Open();
                string qry = @"SET DATEFORMAT ymd; SELECT ";
                if ((columns != null))
                {
                    foreach (string item in columns)
                    {
                        if (columns.IndexOf(item) < (columns.Count - 1))
                        {
                            qry += item + @", ";
                        }
                        else
                        {
                            qry += item;
                        }
                    }
                }
                else
                {
                    qry += "* ";
                }

                qry += @" FROM " + tableName + " ";

                if ((where != null))
                {
                    bool isLong = false;
                    if (where.Count == 1)
                    {
                        isLong = false;
                        qry += " WHERE ";
                    }
                    if (where.Count > 1)
                    {
                        isLong = true;
                        qry += " WHERE (";
                    }
                    foreach (string item in where)
                    {
                        try
                        {
                            int thisItem = where.IndexOf(item);
                            if (thisItem < (where.Count - 1))
                            {
                                qry += item + ") AND (";
                            }
                            else if ((thisItem == (where.Count - 1)) && (!isLong))
                            {
                                qry += item;
                            }
                            else
                            {
                                qry += item + ")";
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                SqlCommand cmd = new SqlCommand(qry, connection);
                DataTable dtbFilteredData = new DataTable();
                SqlDataAdapter daFilteredData = new SqlDataAdapter(cmd);

                daFilteredData.Fill(dtbFilteredData);
                daFilteredData.Dispose();

                foreach (DataRow row in dtbFilteredData.Rows)
                {
                    string line = "";
                    foreach (DataColumn column in dtbFilteredData.Columns)
                    {
                        if (column != dtbFilteredData.Columns[(dtbFilteredData.Columns.Count - 1)])
                            line = line + (row[column] + ",");
                        else
                            line = line + row[column];
                    }
                    tableRecords.Add(line);
                }
            }
            catch (Exception e)
            {
                //Something
            }
            finally
            {
                connection.Close();
            }

            return tableRecords;
        }

        public bool Insert(string tableName, List<string> columns = null, List<string> values = null)
        {
            bool isInserted = false;
            connection.Open();

            string qry = @"INSERT INTO " + tableName + " ";
            if ((columns != null))
            {
                qry += @"(";
                foreach (string item in columns)
                {
                    if (columns.IndexOf(item) < (columns.Count - 1))
                    {
                        qry += item + @", ";
                    }
                    else
                    {
                        qry += item;
                    }
                }
                qry += @") ";
            }

            qry += @"VALUES (";
            try
            {
                if ((values != null))
                {
                    int counter = 0;
                    foreach (string item in values)
                    {
                        if (counter < (values.Count - 1))
                        {
                            qry += item + @", ";
                        }
                        else
                        {
                            qry += item;
                        }
                        counter++;
                    }
                    qry += @")";

                    SqlCommand cmd = new SqlCommand(qry, connection);
                    cmd.ExecuteNonQuery();
                    isInserted = true;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception)
            {
                throw;
            }



            connection.Close();
            return isInserted;
        }

        public bool Delete(string tableName, List<string> where)
        {
            bool isDeleted = false;
            connection.Open();

            string qry = @"DELETE FROM " + tableName + " ";

            try
            {
                if ((where != null))
                {
                    bool isLong = false;
                    if (where.Count == 1)
                    {
                        isLong = false;
                        qry += "WHERE ";
                    }
                    if (where.Count > 1)
                    {
                        isLong = true;
                        qry += "WHERE (";
                    }
                    foreach (string item in where)
                    {
                        int thisItem = where.IndexOf(item);
                        if (thisItem < (where.Count - 1))
                        {
                            qry += item + ") AND (";
                        }
                        else if ((thisItem == (where.Count - 1)) && (!isLong))
                        {
                            qry += item;
                        }
                        else
                        {
                            qry += item + ")";
                        }
                    }

                    SqlCommand cmd = new SqlCommand(qry, connection);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
            return isDeleted;
        }

        public bool Update(string tableName, Dictionary<string, string> ColumnValues, List<string> where)
        {
            bool isUpdated = false;

            connection.Open();

            try
            {
                string qry = @"UPDATE " + tableName + " ";
                if ((ColumnValues != null))
                {
                    qry += @"SET ";
                    int counter = 0;
                    foreach (KeyValuePair<string, string> item in ColumnValues)
                    {
                        counter++;
                        if (counter < ColumnValues.Count)
                        {
                            qry += item.Key + @" = " + item.Value + ", ";
                        }
                        else
                        {
                            qry += item.Key + @" = " + item.Value;
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                }

                if (where != null)
                {
                    bool isLong = false;
                    if (where.Count == 1)
                    {
                        isLong = false;
                        qry += "WHERE ";
                    }
                    if (where.Count > 1)
                    {
                        isLong = true;
                        qry += "WHERE (";
                    }
                    foreach (string item in where)
                    {
                        int thisItem = where.IndexOf(item);
                        if (thisItem < (where.Count - 1))
                        {
                            qry += item + ") AND (";
                        }
                        else if ((thisItem == (where.Count - 1)) && (!isLong))
                        {
                            qry += item;
                        }
                        else
                        {
                            qry += item + ")";
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                }

                SqlCommand cmd = new SqlCommand(qry, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
            return isUpdated;
        }
    }


}


