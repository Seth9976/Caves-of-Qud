using System;

namespace PlayFab.Json
{
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class JsonProperty : Attribute
	{
		// Token: 0x040002D8 RID: 728
		public string PropertyName;

		// Token: 0x040002D9 RID: 729
		public NullValueHandling NullValueHandling;
	}
}
