using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class ContainedItemModel
    {
		[JsonProperty("item")]
		public ItemModel Item { get; set; }

		[JsonProperty("count")]
		public double Count { get; set; }

		[JsonProperty("quantity")]
		public double Quantity { get; set; }

		[JsonProperty("attributes")]
		public List<ItemAttributeModel> Attributes { get; set; }
	}
}
