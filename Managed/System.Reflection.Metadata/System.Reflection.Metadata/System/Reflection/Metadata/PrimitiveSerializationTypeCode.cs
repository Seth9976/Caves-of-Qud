using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies constants that define the type codes used to encode types of primitive values in a <see cref="T:System.Reflection.Metadata.CustomAttribute" /> value blob.</summary>
	// Token: 0x02000050 RID: 80
	public enum PrimitiveSerializationTypeCode : byte
	{
		/// <summary>A <see cref="T:System.Boolean" /> type.</summary>
		// Token: 0x040002AB RID: 683
		Boolean = 2,
		/// <summary>An unsigned 1-byte integer type.</summary>
		// Token: 0x040002AC RID: 684
		Byte = 5,
		/// <summary>A signed 1-byte integer type.</summary>
		// Token: 0x040002AD RID: 685
		SByte = 4,
		/// <summary>A <see cref="T:System.Char" /> type.</summary>
		// Token: 0x040002AE RID: 686
		Char = 3,
		/// <summary>A signed 2-byte integer type.</summary>
		// Token: 0x040002AF RID: 687
		Int16 = 6,
		/// <summary>An unsigned 2-byte integer type.</summary>
		// Token: 0x040002B0 RID: 688
		UInt16,
		/// <summary>A signed 4-byte integer type.</summary>
		// Token: 0x040002B1 RID: 689
		Int32,
		/// <summary>An unsigned 4-byte integer type.</summary>
		// Token: 0x040002B2 RID: 690
		UInt32,
		/// <summary>A signed 8-byte integer type.</summary>
		// Token: 0x040002B3 RID: 691
		Int64,
		/// <summary>An unsigned 8-byte integer type.</summary>
		// Token: 0x040002B4 RID: 692
		UInt64,
		/// <summary>A 4-byte floating point type.</summary>
		// Token: 0x040002B5 RID: 693
		Single,
		/// <summary>An 8-byte floating point type.</summary>
		// Token: 0x040002B6 RID: 694
		Double,
		/// <summary>A <see cref="T:System.String" /> type.</summary>
		// Token: 0x040002B7 RID: 695
		String
	}
}
