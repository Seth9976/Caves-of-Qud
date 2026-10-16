using System;

namespace Unity.Services.Core.Internal.Serialization
{
	// Token: 0x02000059 RID: 89
	internal interface IJsonSerializer
	{
		// Token: 0x0600019A RID: 410
		string SerializeObject<T>(T value);

		// Token: 0x0600019B RID: 411
		T DeserializeObject<T>(string value);
	}
}
