using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Model;
using static Tapkob.Services.Sqlite;

namespace Tapkob.Services
{
    public class Sqlite
    {
        public static void InitializeDatabase()
        {
        }

        public static void InitializeTasksTable(ObservableCollection<TaskModel> Tasks) 
        {
            SqlTask sqlTask = new SqlTask();

            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = conn.QueryFirst<int>("SELECT COUNT(*) FROM Tasks");
                if (output != Tasks.Count)
                {
                    foreach (TaskModel task in Tasks)
                    {
                        sqlTask.TaskID = task.Id;
                        sqlTask.TaskStatus = "Active";

                        conn.Execute("INSERT INTO Tasks(TaskID, TaskStatus) VALUES(@TaskID, @TaskStatus)", sqlTask);
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
