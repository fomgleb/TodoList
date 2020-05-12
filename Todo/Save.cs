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

        #region TodoList
        static public void CreateTodoList(string todoListName)
        {
            FileStream fileStream = File.Create($@"{_mainDirectoryName}\{todoListName}.txt"); // Если просто написать File.Create(path), то файл будет не дуступен потом, т.к. занят другим процессом.
            fileStream.Close(); //а так я могу закрыть этот fileStream и всё впорядке
        }

        static public void DeleteTodoList(string todoListName)
        {
            if (File.Exists($@"{_mainDirectoryName}\{todoListName}.txt"))
                File.Delete($@"{_mainDirectoryName}\{todoListName}.txt");
        }

        static public void RenameTodoList(string oldTodoListName, string newTodoListName)
        {
            if (!File.Exists($@"{_mainDirectoryName}\{newTodoListName}.txt"))
                File.Move($@"{_mainDirectoryName}\{oldTodoListName}.txt", $@"{_mainDirectoryName}\{newTodoListName}.txt");
        }
        #endregion

        #region Doing
        static public void CreateDoing(string doingText, string todoListName)
        {
            string doingString = File.ReadAllText($@"{_mainDirectoryName}\{todoListName}.txt");

            File.WriteAllText($@"{_mainDirectoryName}\{todoListName}.txt", $"{doingString}\n{doingText}\n");
        }

        static public void DeleteDoingByIndex(int index, string todoListName)
        {
            string doingString = File.ReadAllText($@"{_mainDirectoryName}\{todoListName}.txt");//Записывем в переменную всё что в текстовом файле
            char[] chars = { '\n', '\r'}; //Символы для разделения строки, для Split
            List<string> doingList = new List<string>(doingString.Split(chars)); //Разрезаем строку в местах где есть chars символы
            doingList.RemoveAll(x => x == ""); // Удаляем пустые строки

            doingList.RemoveAt(index); // Удаляем тот дуинг который мы хотели

            doingString = ""; 
            foreach (var doing in doingList) // Записываем в doingString новые значения
            {
                doingString += doing + "\n";
            }

            File.WriteAllText($@"{_mainDirectoryName}\{todoListName}.txt", doingString); // из doingString в файл на компьютере
        }

        static public void DeleteDoingByText(string text)
        {
            List<string> allTodoLists = new List<string>(GetAllTodoLists());
            foreach (var todoListName in allTodoLists)
            {
                string doingString = File.ReadAllText($@"{_mainDirectoryName}\{todoListName}.txt");//Записывем в переменную всё что в текстовом файле
                char[] chars = { '\n', '\r' }; //Символы для разделения строки, для Split
                List<string> doingList = new List<string>(doingString.Split(chars)); //Разрезаем строку в местах где есть chars символы
                doingList.RemoveAll(x => x == ""); // Удаляем пустые строки
                doingList.RemoveAll(x => x == text); // Удаляем все элемнты из списка которые называются как text

                doingString = "";
                foreach (var doing in doingList) // Записываем в doingString новые значения
                {
                    doingString += doing + "\n";
                }

                File.WriteAllText($@"{_mainDirectoryName}\{todoListName}.txt", doingString); // из doingString в файл на компьютере
            }
        }

        static public void RenameDoing(int index, string newDoingText, string todoListName)
        {
            string doingString = File.ReadAllText($@"{_mainDirectoryName}\{todoListName}.txt");//Записывем в переменную всё что в текстовом файле
            char[] chars = { '\n', '\r' }; //Символы для разделения строки, для Split
            List<string> doingList = new List<string>(doingString.Split(chars)); //Разрезаем строку в местах где есть chars символы
            doingList.RemoveAll(x => x == ""); // Удаляем пустые строки

            doingList.RemoveAt(index); // Удаляем тот дуинг который мы хотели

            doingList.Insert(index, newDoingText); // Добавляем на место старого новый string

            doingString = "";
            foreach (var doing in doingList) // Записываем в doingString новые значения
            {
                doingString += doing + "\n";
            }

            File.WriteAllText($@"{_mainDirectoryName}\{todoListName}.txt", doingString); // из doingString в файл на компьютере
        }
        #endregion

        static public List<string> GetAllTodoLists()
        {
            string[] allTodoListPaths = Directory.GetFiles($@"{_mainDirectoryName}");
            List<string> allTodoLists = new List<string>();

            foreach (var todoPath in allTodoListPaths)
            {
                string todoName = new DirectoryInfo(todoPath).Name;
                todoName = todoName.Remove(todoName.Length - 4);
                allTodoLists.Add(todoName);
            }
            return allTodoLists;
        }

        static public List<string> GetAllDoing(string todoListName)
        {
            string doingString = File.ReadAllText($@"{_mainDirectoryName}\{todoListName}.txt");//Записывем в переменную всё что в текстовом файле
            char[] chars = { '\n', '\r' }; //Символы для разделения строки, для Split
            List<string> doingList = new List<string>(doingString.Split(chars)); //Разрезаем строку в местах где есть chars символы
            doingList.RemoveAll(x => x == ""); // Удаляем пустые строки

            return doingList;
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
