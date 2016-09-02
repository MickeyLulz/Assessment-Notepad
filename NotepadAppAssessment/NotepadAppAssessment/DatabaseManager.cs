using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;

namespace NotepadAppAssessment
{
   public class DatabaseManager
    {
        static string dbName = "notepadapp.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

        public DatabaseManager()
        {

        }

        public List<Notes> ViewAll()
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "select * from notepadapp";
                    var ListNotes = cmd.ExecuteQuery<Notes>();
                    return ListNotes;                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return null;
            }
        }

        public List<Notes> SearchNotes(string title)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "Select * from notepadapp where Title like '" + title + "%'";
                    var SearchItems = cmd.ExecuteQuery<Notes>();
                    return SearchItems;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return null;
            }
        }

        public void AddItem(string title, string details)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "insert into notepadapp(Title, Details) values('" + title + "', '" + details + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
            }
        }

        public void EditItem(string title, string details, int noteid)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "update notepadapp set Title = '" + title + "', Details = '" + details + "' where NoteID = " + noteid;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
        }

        public void DeleteItem(int noteid)
        {
            try
            {
                using (var conn = new SQLiteConnection(dbPath))
                {
                    var cmd = new SQLiteCommand(conn);
                    cmd.CommandText = "delete from notepadapp where NoteID = " + noteid;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error : " + e.Message);
            }
        }     
    }
}