using System.Collections.Generic;
using System.IO;

namespace Todo
{
    public static class Save
    {
        static string _mainDirectoryName;

        static public void CreateMainDirectory(string directoryName)
        {
            _mainDirectoryName = directoryName;
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        static public void CreateTodoList(string todoListName)
        {
            if (!Directory.Exists($@"{_mainDirectoryName}\{todoListName}"))
                Directory.CreateDirectory($@"{_mainDirectoryName}\{todoListName}");
        }

        static public void CreateDoing(string doingName, string todoListName)
        {
            File.Create($@"{_mainDirectoryName}\{todoListName}\{doingName}");
        }

        static public void DeleteTodoList(string todoListName)
        {
            if(Directory.Exists($@"{_mainDirectoryName}\{todoListName}"))
                Directory.Delete($@"{_mainDirectoryName}\{todoListName}", true);
        }

        static public void DeleteDoing(string doingName, string todoListName)
        {
            if (File.Exists($@"{_mainDirectoryName}\{todoListName}\{doingName}"))
                File.Delete($@"{_mainDirectoryName}\{todoListName}\{doingName}");
        }

        static public List<string> GetAllTodoLists()
        {
            string[] allTodoListPaths = Directory.GetDirectories($@"{_mainDirectoryName}");
            List<string> allTodoLists = new List<string>();

            foreach (var todoPath in allTodoListPaths)
            {
                string todoName = new DirectoryInfo(todoPath).Name;
                allTodoLists.Add(todoName);
            }
            return allTodoLists;
        }

        static public List<string> GetAllDoing(string todoList)
        {
            string[] allDoingPaths = Directory.GetFiles($@"{_mainDirectoryName}\{todoList}");
            List<string> allDoing = new List<string>();

            foreach (var doingPath in allDoingPaths)
            {
                string doingName = new FileInfo(doingPath).Name;
                allDoing.Add(doingName);
            }
            return allDoing;
        }

        //static string _dataBaseName;


        //static public void CreateDataBase(string nameDataBase)
        //{
        //    _dataBaseName = nameDataBase;
        //    if (!File.Exists($"{nameDataBase}.db"))//Если базы данных нету
        //        SQLiteConnection.CreateFile($"{nameDataBase}.db");//, то создаем её
        //}

        //static private SQLiteConnection CreateConnection()
        //{
        //    var connection = new SQLiteConnection($"Data Source={_dataBaseName}.db");
        //    connection.Open();
        //    return connection;
        //}

        //static public void CreateTable(string tableName)
        //{
        //    var command = CreateConnection().CreateCommand();
        //    command.CommandText = $@"CREATE TABLE IF NOT EXISTS [{tableName}]("+
        //        "[id] integer not null primary key autoincrement," +
        //        "[name] nvarchar(10) null," +
        //        "[text] nvarchar(10) null)";
        //    command.ExecuteNonQuery();
        //}

        //static public void DeleteTable(string tableName)
        //{
        //    var command = CreateConnection().CreateCommand();
        //    command.CommandText = $"DROP TABLE IF EXISTS {tableName}";
        //    command.ExecuteNonQuery();
        //}

        //static public void RenameTable(string oldTableName, string newTableName)
        //{
        //    var command = CreateConnection().CreateCommand();
        //    command.CommandText = $"ALTER TABLE {oldTableName} RENAME TO {newTableName}";
        //    command.ExecuteNonQuery();
        //}

        //static public IEnumerable<string> GetTodoNames(string tableName)
        //{
        //    var command = CreateConnection().CreateCommand();
        //    command.CommandText = $"SELECT * FROM {tableName}";
        //    var reader = command.ExecuteReader();

        //    List<string> names = new List<string>();
        //    while (reader.Read())
        //        names.Add(reader["name"].ToString());
        //    reader.Close();

        //    var sortedNames = names.Distinct();//Удаляем одинаковые

        //    return sortedNames;
        //}

        //public List<string> Get(string name)
        //{
        //    var command = CreateConnection().CreateCommand();
        //    command.CommandText = $"SELECT (text) FROM {tableName}";
        //    var reader = command.ExecuteReader();

        //    List<string> texts
        //}
    }
}
