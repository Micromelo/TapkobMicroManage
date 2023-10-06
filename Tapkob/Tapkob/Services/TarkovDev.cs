using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Newtonsoft.Json;
using Tapkob.Interfaces;
using Tapkob.Model;

namespace Tapkob.Services
{
    public static class TarkovDev
    {
        const string ApiEndpoint = "https://api.tarkov.dev/graphql";

        private static readonly HttpClient HttpClient = new(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        });

        public static ObservableCollection<TaskModel> Tasks { get; private set; } = new ObservableCollection<TaskModel>();
        public static ObservableCollection<ItemModel> Items { get; private set; } = new ObservableCollection<ItemModel>();
        
        public async static Task<ObservableCollection<TaskModel>> GetTasks()
        {
            var body = new Dictionary<string, string>() { { "query", TasksQuery } };
            var responseTask = HttpClient.PostAsJsonAsync(ApiEndpoint, body);
            responseTask.Wait();

            if (responseTask.Result.StatusCode != HttpStatusCode.OK) throw new Exception("Tarkov.dev API request failed.");
            var contentTask = responseTask.Result.Content.ReadAsStringAsync();
            contentTask.Wait();

            var result = contentTask.Result;

            // Hack to allow json deserialization
            var replacement = @"""$type"":""Tapkob.Model.TaskObjective.$1, Tapkob""$2";
            result = Regex.Replace(result, @"""__typename"":\s?""(.*?)""(,?)", replacement);

            // Hack to remove image backgrounds
            result = Regex.Replace(result, ".webp", ".png");

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            };

            var neededResponse = JsonConvert.DeserializeObject<TasksResponse>(result, jsonSerializerSettings);
            Tasks = neededResponse.Data.tasks;
            LoadLocalTasksData();
            return Tasks;
        }

        public class TasksResponse
        {
            [JsonProperty("data")]
            public TasksResponseData Data { get; set; }
        }

        public class TasksResponseData
        {
            [JsonProperty("tasks")]
            public ObservableCollection<TaskModel> tasks { get; set; }
        }

        public static void LoadLocalTasksData()
        {
            for(int i = 0; i < Tasks.Count; i++)
            {
                switch (Tasks[i].Objectives[0].Type)
                {
                    case "buildWeapon":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_gun.png";
                        break;
                    case "shoot":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_eliminate.png";
                        break;
                    case "findItem":
                    case "findQuestItem":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_search.png";
                        break;
                    case "giveItem":
                    case "giveQuestItem":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_give.png";
                        break;
                    case "skill":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_skill.png";
                        break;
                    case "visit":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_location.png";
                        break;
                    case "experience":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_time.png";
                        break;
                    case "extract":
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_escape.png";
                        break;
                    default:
                        Tasks[i].TaskIconPath = "/Resources/Icons/icon_objective_search.png";
                        break;
                }
            }
        }

        public async static Task<ObservableCollection<ItemModel>> GetItems()
        {
            var body = new Dictionary<string, string>() { { "query", ItemsQuery } };
            var responseTask = HttpClient.PostAsJsonAsync(ApiEndpoint, body);
            responseTask.Wait();

            if (responseTask.Result.StatusCode != HttpStatusCode.OK) throw new Exception("Tarkov.dev API request failed.");
            var contentTask = responseTask.Result.Content.ReadAsStringAsync();
            contentTask.Wait();

            var result = contentTask.Result;

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            };

            var neededResponse = JsonConvert.DeserializeObject<ItemsResponse>(result, jsonSerializerSettings);
            Items = neededResponse.Data.Items;
            return Items;
        }

        public class ItemsResponse
        {
            [JsonProperty("data")]
            public ItemsResponseData Data { get; set; }
        }

        public class ItemsResponseData
        {
            [JsonProperty("items")]
            public ObservableCollection<ItemModel> Items { get; set; }
        }

        const string TasksQuery = @"
                    query {
                      tasks {
                        id
                        tarkovDataId
                        name
                        trader {
                          id
                          name
                        }
                        map {
                          id
                          name
                        }
                        wikiLink
                        minPlayerLevel
                        taskRequirements {
                          task {
                            id
                            name
                          }
                          status
                        }
                        traderLevelRequirements {
                          trader {
                            id
                            name
                          }
                          level
                        }
                        objectives {
                          __typename
                          id
                          type
                          description
                          optional
                          maps {
                            id
                            name
                          }
                          ... on TaskObjectiveBuildItem {
                            item {
                              id
                            }
                            containsAll {
                              id
                            }
                            containsOne {
                              id
                            }
                          }
                          ... on TaskObjectiveExperience {
                            healthEffect {
                              bodyParts
                              effects
                              time {
                                compareMethod
                                value
                              }
                            }
                          }
                          ... on TaskObjectiveExtract {
                            exitStatus
                            exitName
                            zoneNames
                          }
                          ... on TaskObjectiveItem {
                            item {
                              id
                            }
                            count
                            foundInRaid
                            dogTagLevel
                            maxDurability
                            minDurability
                          }
                          ... on TaskObjectiveMark {
                            markerItem {
                              id
                            }
                          }
                          ... on TaskObjectivePlayerLevel {
                            playerLevel
                          }
                          ... on TaskObjectiveShoot {
                            count
                            usingWeapon {
                              id
                            }
                            usingWeaponMods {
                              id
                            }
                            wearing {
                              id
                            }
                            notWearing {
                              id
                            }
                          }
                          ... on TaskObjectiveQuestItem {
                            questItem {
                              id
                              name
                              shortName
                              description
                            }
                            count
                          }
                          ... on TaskObjectiveSkill {
                            skillLevel {
                              name
                              level
                            }
                          }
                        }
                        finishRewards {
                          traderStanding {
                            trader {
                              id
                              name
                              imageLink
                            }
                            standing
                          }
                          items {
                            item {
                              id
                              name
                              description
                              wikiLink
                              baseImageLink
                            }
                            count
                            quantity
                            attributes {
                              type
                              name
                              value
                            }
                          }
                          offerUnlock {
                            id
                            trader {
                              id
                              name
                              imageLink
                            }
                            level
                            item {
                              id
                              name
                              description
                              wikiLink
                              baseImageLink
                            }
                          }
                          skillLevelReward {
                            name
                            level
                          }
                          traderUnlock {
                            id
                            name
                            imageLink
                          }
                          craftUnlock {
                            id
                            station {
                              id
                              name
                            }
                            level
                          }
                        }
                        factionName
                        neededKeys {
                          keys {
                            id
                            name
                            description
                            wikiLink
                            baseImageLink
                          }
                          map {
                            id
                            name
                          }
                        }
                      }
                      hideoutStations {
                        id
                        name
                        normalizedName
                        levels {
                          id
                          level
                          itemRequirements {
                            id
                            item {
                              id
                            }
                            count
                          }
                          stationLevelRequirements {
                            id
                            station {
                              id
                              name
                            }
                            level
                          }
                          crafts {
                            id
                            duration
                            requiredItems {
                              item {
                                id
                              }
                              count
                            }
                            rewardItems {
                              item {
                                id
                              }
                              count
                            }
                          }
                        }
                      }
                    }
                ";

        const string ItemsQuery = @"
                    query {
                      items {
                        id
                        name
                        description
                        wikiLink
                        baseImageLink
                      }
                    }
                ";
    }
}
