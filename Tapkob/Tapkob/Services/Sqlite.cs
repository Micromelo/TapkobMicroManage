using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Tapkob.Model;

namespace Tapkob.Services
{
    public class Sqlite
    {
        public static void InitializeDatabase()
        {
        }

        public static void InitializeTasksTable(ObservableCollection<TaskModel> Tasks) 
        {
            List<String> sqlTasksIDs = new List<String>();
            SqlTask sqlTask = new SqlTask();

            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = conn.Query<String>("SELECT TaskID FROM Tasks", new DynamicParameters());
                sqlTasksIDs = output.ToList();
                if (sqlTasksIDs.Count != Tasks.Count)
                {
                    foreach (TaskModel task in Tasks)
                    {
                        string result = sqlTasksIDs.FirstOrDefault(x => x == task.Id);
                        if (result == null)
                        {
                            sqlTask.TaskID = task.Id;
                            sqlTask.TaskStatus = "Active";

                            conn.Execute("INSERT INTO Tasks(TaskID, TaskStatus) VALUES(@TaskID, @TaskStatus)", sqlTask);
                        }
                    }
                }
            }
        }

        public static ObservableCollection<TaskModel> LoadTasksStatus(ObservableCollection<TaskModel> Tasks)
        {
            List<SqlTask> sqlTasks = new List<SqlTask>();

            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString())) 
            {
                var output = conn.Query<SqlTask>("SELECT * FROM Tasks", new DynamicParameters());
                sqlTasks = output.ToList();

                foreach (SqlTask sqlTask in sqlTasks)
                {
                    for(int i = 0; i < Tasks.Count; i++) 
                    { 
                        if(sqlTask.TaskID == Tasks[i].Id)
                        {
                            Tasks[i].TaskStatus = sqlTask.TaskStatus;
                        }
                    }
                }
                return Tasks;
            }
        }

        public static void SaveTasksStatus(TaskModel task)
        {
            string query = string.Empty;

            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                query = "UPDATE Tasks SET";
                query += " TaskStatus = '" + task.TaskStatus + "'";
                query += " WHERE TaskID = '" + task.Id + "'";

                conn.Execute(query);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public class SqlTask
        {
            public int ID { get; set; }
            public string TaskID { get; set; }
            public string TaskStatus { get; set; }
        }
    }
}
