using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class HealthEffectModel
    {
		[JsonProperty("bodyParts")]
		public List<string> BodyParts { get; set; }

		[JsonProperty("effects")]
		public List<string> Effects { get; set; }

		[JsonProperty("time")]
		public NumberCompareModel Time { get; set; }
	}
}
