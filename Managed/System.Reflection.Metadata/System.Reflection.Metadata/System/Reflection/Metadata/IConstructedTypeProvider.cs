using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <typeparam name="TType" />
	// Token: 0x0200007E RID: 126
	[NullableContext(1)]
	public interface IConstructedTypeProvider<[Nullable(2)] TType> : ISZArrayTypeProvider<TType>
	{
		/// <summary>Gets the type symbol for a generic instantiation of the given generic type with the given type arguments.</summary>
		/// <param name="genericType" />
		/// <param name="typeArguments" />
		// Token: 0x0600050E RID: 1294
		TType GetGenericInstantiation(TType genericType, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TType> typeArguments);

		/// <summary>Gets the type symbol for a generalized array of the given element type and shape.</summary>
		/// <param name="elementType">The type of the elements in the array.</param>
		/// <param name="shape">The shape (rank, sizes, and lower bounds) of the array.</param>
		// Token: 0x0600050F RID: 1295
		TType GetArrayType(TType elementType, ArrayShape shape);

		/// <summary>Gets the type symbol for a managed pointer to the given element type.</summary>
		/// <param name="elementType" />
		// Token: 0x06000510 RID: 1296
		TType GetByReferenceType(TType elementType);

		/// <summary>Gets the type symbol for an unmanaged pointer to the given element type.</summary>
		/// <param name="elementType" />
		// Token: 0x06000511 RID: 1297
		TType GetPointerType(TType elementType);
	}
}
