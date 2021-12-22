using A24_Stroka.Core.Global;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace A24_Stroka.Core.Operations
{
    public class SqlOperations
    {

        #region Sql connection Init
        private MySqlConnection _mySqlConnection;

        public void MySqlConnect()
        {
            try
            {
                //var prop = Singleton.Instance.IPDB;
                var prop = "127.0.0.1";
                //var connectString = "Database=A24Stroka;Data Source=" + prop + ";User Id=sql_admin;Password=auris2013;SslMode=none";
                var connectString = "Database=a24stroka;Data Source=" + prop + ";User Id=root;Password=1234;SslMode=none";
                _mySqlConnection = new MySqlConnection(connectString);
                _mySqlConnection.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        public void MySqlDisconnect()
        {
            try
            {
                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }

        }
        #endregion
        public List<string> DBListGeneralTable = new List<string>();

        #region Private
        private void InsertSqlData(string mediaTableName, string text, bool inwork, int order, bool view)
        {

            try
            {
                var command = new MySqlCommand
                {
                    Connection = _mySqlConnection,
                    CommandText =
                        "INSERT INTO " + mediaTableName +
                        " (Text,  In_Work, Order_By, View) VALUES(@Text,  @In_Work,  @Order_By,  @View)"
                };
                command.Parameters.AddWithValue("@Text", text);
                command.Parameters.AddWithValue("@In_Work", inwork);
                command.Parameters.AddWithValue("@Order_By", order);
                command.Parameters.AddWithValue("@View", view);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }



        private DataTable MySqlTableSelect(string mediaTable, string nameField)
        {

            using (var result = new DataTable())
            {

                string strSql = string.Format("Select " + nameField + " From " + mediaTable + " Order By Order_By");

                try
                {
                    MySqlDataAdapter MyDA = new MySqlDataAdapter();
                    MyDA.SelectCommand = new MySqlCommand(strSql, _mySqlConnection);
                    MyDA.Fill(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
                return result;
            }
        }
        #endregion

        #region Public
        public DataTable GetTable(string mediaTable, string nameField)
        {
            try
            {
                MySqlConnect();
                var result = MySqlTableSelect(mediaTable, nameField);

                MySqlDisconnect();
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
                return null;
            }
        }

        public void AddTable(string Text, bool inwork, int order, bool view)
        {
            try
            {
                MySqlConnect();
                InsertSqlData("A24Stroka.Stroka", Text, inwork, order, view);
                MySqlDisconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        public void UpdateRow(string FieldWithValue, string varibleField)
        {
            try
            {
                MySqlConnect();
                string commandText = "UPDATE A24Stroka.Stroka SET " + FieldWithValue + " WHERE " + varibleField;
                MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection);
                command.ExecuteNonQuery();
                MySqlDisconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }


        public void DeleteSqlData(string mediaTableName, string value)
        {

            try
            {
                MySqlConnect();
                var command = new MySqlCommand
                {
                    Connection = _mySqlConnection,
                    CommandText =
                        "DELETE FROM " + mediaTableName + " WHERE " + value + ""
                };
                command.ExecuteNonQuery();
                MySqlDisconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
        #endregion
    }
}
