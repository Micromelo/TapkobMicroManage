using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class RequirementItemModel
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("item")]
		public ItemModel Item { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }

		[JsonProperty("attributes")]
		public List<ItemAttributeModel> Attributes { get; set; }
	}
}
