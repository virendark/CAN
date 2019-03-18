using System;
using System.IO;
using Xamarin.Forms;
using CAN.Droid;
using SQLite;


[assembly: Dependency(typeof(SQLite_Android))]
namespace CAN.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var filename = "Student.db3";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);


            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}