using System;

namespace PlayFab
{
	// Token: 0x02000020 RID: 32
	public interface ISerializerPlugin : IPlayFabPlugin
	{
		// Token: 0x060002AF RID: 687
		T DeserializeObject<T>(string serialized);

		// Token: 0x060002B0 RID: 688
		T DeserializeObject<T>(string serialized, object serializerStrategy);

		// Token: 0x060002B1 RID: 689
		object DeserializeObject(string serialized);

		// Token: 0x060002B2 RID: 690
		string SerializeObject(object obj);

		// Token: 0x060002B3 RID: 691
		string SerializeObject(object obj, object serializerStrategy);
	}
}
