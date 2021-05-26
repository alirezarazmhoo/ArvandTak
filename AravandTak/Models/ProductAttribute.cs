using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AravandTak.Models
{
	public class ProductAttribute
	{
		private static readonly char delimiter = ',';
		private string _colors;

		[Key]
		public Guid Id { get; set; }
		[Required]
		public Guid ProductId { get; set; }
		[Required]
		public int Packing { get; set; } = 25; // can be: 25, 50, 75, 100
		public bool IsAvailable { get; set; } = false;
		[Required]
		public decimal Price { get; set; }
		[NotMapped]
		public string[] Colors
		{
			get => _colors.Split(delimiter);
			set { string.Join($"{delimiter}", value); }
		}

		public virtual Product Product { get; set; }

	}
}