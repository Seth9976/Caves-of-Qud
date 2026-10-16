using System;
using XRL.World;

namespace XRL.Serialization
{
	/// <summary>
	/// Interface to allow helper classes to be used to serialize objects
	/// that are not directly supported by SerializationWriter/SerializationReader
	/// </summary>
	// Token: 0x0200056E RID: 1390
	public interface IFastSerializationTypeSurrogate
	{
		/// <summary>
		/// Allows a surrogate to be queried as to whether a particular type is supported
		/// </summary>
		/// <param name="type">The type being queried</param>
		/// <returns>true if the type is supported; otherwise false</returns>
		// Token: 0x06003C4E RID: 15438
		bool SupportsType(Type type);

		/// <summary>
		/// FastSerializes the object into the SerializationWriter.
		/// </summary>
		/// <param name="writer">The SerializationWriter into which the object is to be serialized.</param>
		/// <param name="value">The object to serialize.</param>
		// Token: 0x06003C4F RID: 15439
		void Serialize(SerializationWriter writer, object value);

		/// <summary>
		/// Deserializes an object of the supplied type from the SerializationReader.
		/// </summary>
		/// <param name="reader">The SerializationReader containing the serialized object.</param>
		/// <param name="type">The type of object required to be deserialized.</param>
		/// <returns />
		// Token: 0x06003C50 RID: 15440
		object Deserialize(SerializationReader reader, Type type);
	}
}
