using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Interfaces;

namespace Tapkob.Model.TaskObjective
{
    public class TaskObjectiveShoot : ITaskObjective
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("maps")]
		public List<MapModel> Maps { get; set; }

		[JsonProperty("optional")]
		public bool Optional { get; set; }

		[JsonProperty("target")]
		public string Target { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("shotType")]
		public string ShotType { get; set; }

		[JsonProperty("zoneNames")]
		public List<string> ZoneNames { get; set; }

		[JsonProperty("bodyParts")]
		public List<string> BodyParts { get; set; }

		[JsonProperty("usingWeapon")]
		public List<ItemModel> UsingWeapon { get; set; }

		[JsonProperty("usingWeaponMods")]
		public List<List<ItemModel>> UsingWeaponMods { get; set; }

		[JsonProperty("wearing")]
		public List<List<ItemModel>> Wearing { get; set; }

		[JsonProperty("notWearing")]
		public List<ItemModel> NotWearing { get; set; }

		[JsonProperty("distance")]
		public NumberCompareModel Distance { get; set; }

		[JsonProperty("playerHealthEffect")]
		public HealthEffectModel PlayerHealthEffect { get; set; }

		[JsonProperty("enemyHealthEffect")]
		public HealthEffectModel EnemyHealthEffect { get; set; }
	}
}
