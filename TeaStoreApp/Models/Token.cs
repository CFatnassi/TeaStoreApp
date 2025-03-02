﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeaStoreApp.Models
{
	public class Token
	{
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("user_id")]
		public string UserId { get; set; }

		[JsonProperty("user_name")]
		public string UserName { get; set; }
    }
}
