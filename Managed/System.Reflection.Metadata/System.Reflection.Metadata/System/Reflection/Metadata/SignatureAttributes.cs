using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies additional flags that can be applied to method signatures. The underlying values of the fields in this type correspond to the representation in the leading signature byte represented by a <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure.</summary>
	// Token: 0x020000D3 RID: 211
	[Flags]
	public enum SignatureAttributes : byte
	{
		/// <summary>No flags.</summary>
		// Token: 0x040004A9 RID: 1193
		None = 0,
		/// <summary>A generic method.</summary>
		// Token: 0x040004AA RID: 1194
		Generic = 16,
		/// <summary>
		///   <para>An instance method.</para>
		///   <para>The Ecma 335 CLI Specification refers to this flag as <see langword="HAS_THIS" />.</para>
		/// </summary>
		// Token: 0x040004AB RID: 1195
		Instance = 32,
		/// <summary>Indicates the first explicitly declared parameter that represents the instance pointer.</summary>
		// Token: 0x040004AC RID: 1196
		ExplicitThis = 64
	}
}
