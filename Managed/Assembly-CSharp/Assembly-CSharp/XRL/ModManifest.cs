using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using XRL.Serialization;

namespace XRL
{
	// Token: 0x02000525 RID: 1317
	public class ModManifest
	{
		// Token: 0x17000607 RID: 1543
		// (set) Token: 0x06003882 RID: 14466 RVA: 0x0011596C File Offset: 0x00113B6C
		[JsonProperty]
		private string Dependency
		{
			set
			{
				this.Dependencies = new Dictionary<string, string> { { value, "*" } };
			}
		}

		// Token: 0x04001C6A RID: 7274
		public string ID;

		// Token: 0x04001C6B RID: 7275
		public int? LoadOrder;

		// Token: 0x04001C6C RID: 7276
		public string Title;

		// Token: 0x04001C6D RID: 7277
		public string Description;

		// Token: 0x04001C6E RID: 7278
		[JsonConverter(typeof(CommaDelimitedArrayConverter))]
		public string[] Tags;

		// Token: 0x04001C6F RID: 7279
		public Version Version;

		// Token: 0x04001C70 RID: 7280
		public string Author;

		// Token: 0x04001C71 RID: 7281
		public string PreviewImage;

		// Token: 0x04001C72 RID: 7282
		public ModDirectory[] Directories;

		// Token: 0x04001C73 RID: 7283
		public Dictionary<string, string> Dependencies;

		// Token: 0x04001C74 RID: 7284
		[JsonConverter(typeof(StringArrayConverter))]
		public string[] LoadBefore;

		// Token: 0x04001C75 RID: 7285
		[JsonConverter(typeof(StringArrayConverter))]
		public string[] LoadAfter;
	}
}
