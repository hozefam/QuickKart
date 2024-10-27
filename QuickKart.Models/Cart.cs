using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuickKart.Models
{
	[DataContract]
	public class Cart
	{
        [DataMember]
        public int CartId { get; set; }

		[DataMember]
		public List<Product> CartProducts { get; set; }

		[DataMember]
		public string UserEmail { get; set; }
	}
}
