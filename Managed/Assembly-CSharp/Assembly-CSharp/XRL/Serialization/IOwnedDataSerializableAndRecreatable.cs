using System;

namespace XRL.Serialization
{
	/// <summary>
	/// Allows a class to specify that it can be recreated during deserialization using a default constructor
	/// and then calling DeserializeOwnedData()
	/// </summary>
	// Token: 0x02000570 RID: 1392
	public interface IOwnedDataSerializableAndRecreatable : IOwnedDataSerializable
	{
	}
}
