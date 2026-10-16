using System;
using System.Collections.Generic;
using Stores.Util;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000057 RID: 87
	internal class AppleJsonProductDescriptionsDeserializer : JsonProductDescriptionsDeserializer
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000063CC File Offset: 0x000045CC
		internal override ProductMetadata DeserializeMetadata(Dictionary<string, object> data)
		{
			return new AppleProductMetadata(base.DeserializeMetadata(data), data.TryGetString("isFamilyShareable"));
		}
	}
}
