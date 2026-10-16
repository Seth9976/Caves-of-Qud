using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <typeparam name="TType" />
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	public interface ISZArrayTypeProvider<[Nullable(2)] TType>
	{
		/// <summary>Gets the type symbol for a single-dimensional array of the given element type with a lower bounds of zero.</summary>
		/// <param name="elementType" />
		/// <returns>A <typeparamref name="TType" /> instance.</returns>
		// Token: 0x06000504 RID: 1284
		TType GetSZArrayType(TType elementType);
	}
}
