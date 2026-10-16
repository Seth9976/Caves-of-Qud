using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies constants that define primitive types found in metadata signatures.</summary>
	// Token: 0x02000080 RID: 128
	public enum PrimitiveTypeCode : byte
	{
		/// <summary>A <see cref="T:System.Boolean" /> type.</summary>
		// Token: 0x0400033B RID: 827
		Boolean = 2,
		/// <summary>A <see cref="T:System.Byte" /> type.</summary>
		// Token: 0x0400033C RID: 828
		Byte = 5,
		/// <summary>An <see cref="T:System.SByte" /> type.</summary>
		// Token: 0x0400033D RID: 829
		SByte = 4,
		/// <summary>A <see cref="T:System.Char" /> type.</summary>
		// Token: 0x0400033E RID: 830
		Char = 3,
		/// <summary>An <see cref="T:System.Int16" /> type.</summary>
		// Token: 0x0400033F RID: 831
		Int16 = 6,
		/// <summary>A <see cref="T:System.UInt16" /> type.</summary>
		// Token: 0x04000340 RID: 832
		UInt16,
		/// <summary>An <see cref="T:System.Int32" /> type.</summary>
		// Token: 0x04000341 RID: 833
		Int32,
		/// <summary>A <see cref="T:System.UInt32" /> type.</summary>
		// Token: 0x04000342 RID: 834
		UInt32,
		/// <summary>An <see cref="T:System.Int64" /> type.</summary>
		// Token: 0x04000343 RID: 835
		Int64,
		/// <summary>A <see cref="T:System.UInt64" /> type.</summary>
		// Token: 0x04000344 RID: 836
		UInt64,
		/// <summary>A <see cref="T:System.Single" /> type.</summary>
		// Token: 0x04000345 RID: 837
		Single,
		/// <summary>A <see cref="T:System.Double" /> type.</summary>
		// Token: 0x04000346 RID: 838
		Double,
		/// <summary>An <see cref="T:System.IntPtr" /> type.</summary>
		// Token: 0x04000347 RID: 839
		IntPtr = 24,
		/// <summary>A <see cref="T:System.UIntPtr" /> type.</summary>
		// Token: 0x04000348 RID: 840
		UIntPtr,
		/// <summary>An <see cref="T:System.Object" /> type.</summary>
		// Token: 0x04000349 RID: 841
		Object = 28,
		/// <summary>A <see cref="T:System.String" /> type.</summary>
		// Token: 0x0400034A RID: 842
		String = 14,
		/// <summary>A typed reference.</summary>
		// Token: 0x0400034B RID: 843
		TypedReference = 22,
		/// <summary>A <see cref="T:System.Void" /> type.</summary>
		// Token: 0x0400034C RID: 844
		Void = 1
	}
}
