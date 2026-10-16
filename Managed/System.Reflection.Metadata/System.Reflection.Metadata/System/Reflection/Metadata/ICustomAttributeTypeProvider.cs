using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <typeparam name="TType" />
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	public interface ICustomAttributeTypeProvider<[Nullable(2)] TType> : ISimpleTypeProvider<TType>, ISZArrayTypeProvider<TType>
	{
		/// <summary>Gets the <typeparamref name="TType" /> representation for <see cref="T:System.Type" />.</summary>
		// Token: 0x060002D7 RID: 727
		TType GetSystemType();

		/// <summary>Verifies if the given type represents <see cref="T:System.Type" />.</summary>
		/// <param name="type">The type to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the given type is a <see cref="T:System.Type" />, <see langword="false" /> otherwise.</returns>
		// Token: 0x060002D8 RID: 728
		bool IsSystemType(TType type);

		/// <summary>Gets the type symbol for the given serialized type name.</summary>
		/// <param name="name">The serialized type name in so-called "reflection notation" format (as understood by the <see cref="M:System.Type.GetType(System.String)" /> method.)</param>
		/// <exception cref="T:System.BadImageFormatException">The name is malformed.</exception>
		/// <returns>A <typeparamref name="TType" /> instance.</returns>
		// Token: 0x060002D9 RID: 729
		TType GetTypeFromSerializedName(string name);

		/// <summary>Gets the underlying type of the given enum type symbol.</summary>
		/// <param name="type">An enum type.</param>
		/// <exception cref="T:System.BadImageFormatException">The given type symbol does not represent an enum.</exception>
		/// <returns>A type code that indicates the underlying type of the enumeration.</returns>
		// Token: 0x060002DA RID: 730
		PrimitiveTypeCode GetUnderlyingEnumType(TType type);
	}
}
