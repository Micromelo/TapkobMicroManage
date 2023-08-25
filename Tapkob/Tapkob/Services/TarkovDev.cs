using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Tapkob.Model;

namespace Tapkob.Services
{
    internal class TarkovDev
    {
        private static readonly GraphQLHttpClient client = new GraphQLHttpClient("https://api.tarkov.dev/graphql", new SystemTextJsonSerializer());
        private static readonly HttpClient httpClient = new HttpClient();

        public static List<TaskModel> Tasks { get; private set; } = new List<TaskModel>();

        public async static Task<List<TaskModel>> GetTasks()
        {
            //var request = new GraphQL.GraphQLRequest()
            //{
            //    Query = @"
            //        query {
            //            tasks {
            //                id
            //                name
            //            }
            //        }
            //    "
            //};

            var request = new GraphQL.GraphQLRequest()
            {
                Query = @"
                    query {
                        tasks {
                            id
                            name
                            trader {
                                id
                                name
                            }
                            map {
                                id
                                name
                            }
                            experience
                            wikiLink
                            minPlayerLevel
                            taskRequirements {
                                task {
                                    name
                                }
                            }
                            objectives {
                                id
                                type
                                description
                            }
                            kappaRequired
                            lightkeeperRequired
                        }
                    }
                "
            };

            //finishRewards {
            //    items {
            //        item {
            //            name
            //                        }
            //    }
            //    neededKeys {
            //        keys {
            //            name
            //                    }
            //    }
            
            var response = await client.SendQueryAsync<TasksResponse>(request);
            Tasks = response.Data.tasks;
            return Tasks;
        }

        public class TasksResponse
        {
            public List<TaskModel> tasks { get; set; }
        }

        public class TaskData
        {
            public string id { get; set; }
            public string name { get; set; }
            public List<TaskRequirementsData> taskRequirements { get; set; }
        }
        public class TaskRequirementsData
        {
            public TaskData task { get; set; }
        }

        public class TraderData
        {
            string name { get; set; }
        }
    }
}
