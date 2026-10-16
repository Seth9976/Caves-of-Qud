using System;

namespace System.Reflection.Metadata
{
	/// <summary>Indicates the type definition of the signature.</summary>
	// Token: 0x0200007D RID: 125
	public enum SignatureTypeKind : byte
	{
		/// <summary>It isn't known in the current context if the type reference or definition is a class or value type.</summary>
		// Token: 0x04000332 RID: 818
		Unknown,
		/// <summary>The type definition or reference refers to a class.</summary>
		// Token: 0x04000333 RID: 819
		Class = 18,
		/// <summary>The type definition or reference refers to a value type.</summary>
		// Token: 0x04000334 RID: 820
		ValueType = 17
	}
}
