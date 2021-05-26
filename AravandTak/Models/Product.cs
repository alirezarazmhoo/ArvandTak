﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AravandTak.Models
{
	public class Product
	{
		private static readonly char delimiter = ',';
		private string _tags;

		[Key]
		public Guid Id { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(100)")]
		public string Title { get; set; }
		[Required]
		[Column(TypeName = "VARCHAR(50)")]
		public string Code { get; set; }
		[Column(TypeName = "NVARCHAR(255)")]
		public string FeatureImage { get; set; }

		[NotMapped]
		public string[] Tags
		{
			get => _tags.Split(delimiter);
			set
			{
				string.Join($"{delimiter}", value);
			}
		}
		[Column(TypeName = "NTEXT")]
		public string Description { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; } = DateTime.Now;

		public virtual ICollection<ProductAttribute> Attributes { get; set; }
		public virtual ICollection<ProductGallery> Galleries { get; set; }

	}
}
