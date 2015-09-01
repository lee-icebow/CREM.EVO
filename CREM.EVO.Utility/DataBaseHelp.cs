using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
namespace ZengoApp
{
    class DataBaseHelp
    {
        private string _connectString = @"Data Source=.\db\ZengoDb.sqlite3";
        private static  SQLiteConnection _SQLiteConnection;
        SQLiteDataReader _reader; //= command.ExecuteReader();
        public DataBaseHelp()
        {
            _SQLiteConnection = new SQLiteConnection(_connectString);           
        }
        public string GetRecipeName(UInt16 RcpId)
        {
            string ret = "Not Defined Juice";
            try
            {
                _SQLiteConnection.Open();
                string qury = string.Format("select * from tb_recipe where ID={0}", RcpId);
                SQLiteCommand command = new SQLiteCommand(qury, _SQLiteConnection);
                _reader = command.ExecuteReader();
                while (_reader.Read())
                ret = _reader["DisplayName"].ToString();
                    Console.ReadLine();
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                _reader.Close();
                _SQLiteConnection.Close();
                
            }
            return ret;   
        }

        public string GetPreText(UInt16 RcpId)
        {
            string ret = "";
            try
            {
                _SQLiteConnection.Open();
                string qury = string.Format("select * from tb_recipe where ID={0}", RcpId);
                SQLiteCommand command = new SQLiteCommand(qury, _SQLiteConnection);
                _reader = command.ExecuteReader();
                while (_reader.Read())
                    ret = _reader["ShowInfo"].ToString();
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                _reader.Close();
                _SQLiteConnection.Close();

            }
            return ret;
        }

        public string GetIconName(UInt16 RcpId)
        {
            string ret = "";
            try
            {
                _SQLiteConnection.Open();
                string qury = string.Format("select * from tb_recipe where ID={0}", RcpId);
                SQLiteCommand command = new SQLiteCommand(qury, _SQLiteConnection);
                _reader = command.ExecuteReader();
                while (_reader.Read())
                    ret = _reader["IconName"].ToString();
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                _reader.Close();
                _SQLiteConnection.Close();

            }
            return ret;
        }

        public void DeleteRcpInfo(UInt16 RcpId)
        {
            try
            {
                _SQLiteConnection.Open();
                string qury = string.Format("delete from tb_recipe where ID={0}", RcpId);
               
                SQLiteCommand command = new SQLiteCommand(qury, _SQLiteConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                _SQLiteConnection.Close();

            }
        }

        public void SetRecipeName(UInt16 RcpId, string Dispalyname,string PreText,string IconName,bool isadd =false)
        {
            try
            {
                _SQLiteConnection.Open();
                string qury = string.Format("update tb_recipe set DisplayName='{1}',ShowInfo='{2}',IconName='{3}' where ID={0}", RcpId, Dispalyname, PreText, IconName);
                if (isadd)
                {
                    qury = string.Format("insert into tb_recipe(ID,DisplayName,ShowInfo,IconName) values({0},'{1}','{2}','{3}')", RcpId, Dispalyname, PreText, IconName);
                }
                SQLiteCommand command = new SQLiteCommand(qury, _SQLiteConnection);
                command.ExecuteNonQuery();               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                _SQLiteConnection.Close();

            }
        }

        public void UpdatePictoDb(UInt16 RcpId,string Picname)
        {
            try
            {
                FileStream fs = new FileStream(Picname, FileMode.Open, FileAccess.Read);

                Byte[] blob = new Byte[fs.Length];

                fs.Read(blob, 0, blob.Length);

                fs.Close();
                _SQLiteConnection.Open();
                string qury = string.Format("update tb_recipe set IconPic=:IconPic  where ID={0}", RcpId);
                
                SQLiteCommand command = new SQLiteCommand(qury, _SQLiteConnection);
                command.Parameters.Add("IconPic", System.Data.DbType.Binary, blob.Length);
                command.Parameters["IconPic"].Value = blob;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                _SQLiteConnection.Close();

            }
        }

        public  static byte[] GetIconstream(UInt16 RcpId)
        {
            byte[] ret;
            try
            {
                _SQLiteConnection.Open();
                string qury = string.Format("select IconPic from tb_recipe where ID={0}", RcpId);
                SQLiteCommand command = new SQLiteCommand(qury, _SQLiteConnection);
                ret = (byte[])command.ExecuteScalar();               

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ret = null;
            }
            finally
            {
                _SQLiteConnection.Close();

            }
            return ret;
        }
    }

}
