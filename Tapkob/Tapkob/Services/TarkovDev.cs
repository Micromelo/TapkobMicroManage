using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static ObservableCollection<TaskModel> Tasks { get; private set; } = new ObservableCollection<TaskModel>();

        public async static Task<ObservableCollection<TaskModel>> GetTasks()
        {
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
            public ObservableCollection<TaskModel> tasks { get; set; }
        }

    }
}
