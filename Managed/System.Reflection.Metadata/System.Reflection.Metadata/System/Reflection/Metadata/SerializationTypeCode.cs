using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies type codes used to encode the types of values in a <see cref="T:System.Reflection.Metadata.CustomAttributeValue`1" /> blob.</summary>
	// Token: 0x020000D2 RID: 210
	public enum SerializationTypeCode : byte
	{
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Invalid" />.</summary>
		// Token: 0x04000496 RID: 1174
		Invalid,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Boolean" />.</summary>
		// Token: 0x04000497 RID: 1175
		Boolean = 2,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Char" />.</summary>
		// Token: 0x04000498 RID: 1176
		Char,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.SByte" />.</summary>
		// Token: 0x04000499 RID: 1177
		SByte,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Byte" />.</summary>
		// Token: 0x0400049A RID: 1178
		Byte,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Int16" />.</summary>
		// Token: 0x0400049B RID: 1179
		Int16,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.UInt16" />.</summary>
		// Token: 0x0400049C RID: 1180
		UInt16,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Int32" />.</summary>
		// Token: 0x0400049D RID: 1181
		Int32,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.UInt32" />.</summary>
		// Token: 0x0400049E RID: 1182
		UInt32,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Int64" />.</summary>
		// Token: 0x0400049F RID: 1183
		Int64,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.UInt64" />.</summary>
		// Token: 0x040004A0 RID: 1184
		UInt64,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Single" />.</summary>
		// Token: 0x040004A1 RID: 1185
		Single,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Double" />.</summary>
		// Token: 0x040004A2 RID: 1186
		Double,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.String" />.</summary>
		// Token: 0x040004A3 RID: 1187
		String,
		/// <summary>A value equivalent to <see cref="F:System.Reflection.Metadata.SignatureTypeCode.SZArray" />.</summary>
		// Token: 0x040004A4 RID: 1188
		SZArray = 29,
		/// <summary>The attribute argument is a <see cref="T:System.Type" /> instance.</summary>
		// Token: 0x040004A5 RID: 1189
		Type = 80,
		/// <summary>The attribute argument is "boxed" (passed to a parameter, field, or property of type object) and carries type information in the attribute blob.</summary>
		// Token: 0x040004A6 RID: 1190
		TaggedObject,
		/// <summary>The attribute argument is an Enum instance.</summary>
		// Token: 0x040004A7 RID: 1191
		Enum = 85
	}
}
