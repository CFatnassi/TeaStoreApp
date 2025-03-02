﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaStoreApp.Models
{
	public class ProductDetail
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public int Price { get; set; }

		[JsonProperty("detail")]
		public string Detail { get; set; }

		[JsonProperty("imageUrl")]
		public string ImageUrl { get; set; }

		public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;
	}
}
