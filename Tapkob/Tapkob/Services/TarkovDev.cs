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
      ... on TaskObjectiveShoot {
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
        id
        type
        description
        optional
        questItem {
          id
          name
          shortName
          description
        }
        count
      }
    }
    factionName
    neededKeys {
      keys {
        id
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
