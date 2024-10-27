using System.Runtime.Serialization;

namespace QuickKart.Models
{
	[DataContract]
	public class Product
	{
		[DataMember]
		public short Quantity { get; set; }

		[DataMember]
		public double PricePerPiece { get; set; }

		[DataMember]
		public string ProductName { get; set; }

		[DataMember]
		public string ProductId { get; set; }

	}
}
