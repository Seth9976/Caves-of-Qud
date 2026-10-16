using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies values that represent types of metadata constants.</summary>
	// Token: 0x02000075 RID: 117
	public enum ConstantTypeCode : byte
	{
		/// <summary>An invalid type.</summary>
		// Token: 0x04000318 RID: 792
		Invalid,
		/// <summary>A Boolean type.</summary>
		// Token: 0x04000319 RID: 793
		Boolean = 2,
		/// <summary>A character type.</summary>
		// Token: 0x0400031A RID: 794
		Char,
		/// <summary>A signed 1-byte integer type.</summary>
		// Token: 0x0400031B RID: 795
		SByte,
		/// <summary>An unsigned 1-byte integer.</summary>
		// Token: 0x0400031C RID: 796
		Byte,
		/// <summary>A signed 2-byte integer type.</summary>
		// Token: 0x0400031D RID: 797
		Int16,
		/// <summary>An unsigned 2-byte integer type.</summary>
		// Token: 0x0400031E RID: 798
		UInt16,
		/// <summary>A signed 4-byte integer type.</summary>
		// Token: 0x0400031F RID: 799
		Int32,
		/// <summary>An unsigned 4-byte integer type.</summary>
		// Token: 0x04000320 RID: 800
		UInt32,
		/// <summary>A signed 8-byte integer type.</summary>
		// Token: 0x04000321 RID: 801
		Int64,
		/// <summary>An unsigned 8-byte integer type.</summary>
		// Token: 0x04000322 RID: 802
		UInt64,
		/// <summary>A 4-byte floating point type.</summary>
		// Token: 0x04000323 RID: 803
		Single,
		/// <summary>An 8-byte floating point type.</summary>
		// Token: 0x04000324 RID: 804
		Double,
		/// <summary>A <see cref="T:System.String" /> type.</summary>
		// Token: 0x04000325 RID: 805
		String,
		/// <summary>A null reference.</summary>
		// Token: 0x04000326 RID: 806
		NullReference = 18
	}
}
