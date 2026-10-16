using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies constants that define type codes used in signature encoding.</summary>
	// Token: 0x020000D7 RID: 215
	public enum SignatureTypeCode : byte
	{
		/// <summary>Represents an invalid or uninitialized type code. It will not appear in valid signatures.</summary>
		// Token: 0x040004BF RID: 1215
		Invalid,
		/// <summary>Represents <see cref="T:System.Void" /> in signatures.</summary>
		// Token: 0x040004C0 RID: 1216
		Void,
		/// <summary>Represents a <see cref="T:System.Boolean" /> in signatures.</summary>
		// Token: 0x040004C1 RID: 1217
		Boolean,
		/// <summary>Represents a <see cref="T:System.Char" /> in signatures.</summary>
		// Token: 0x040004C2 RID: 1218
		Char,
		/// <summary>Represents an <see cref="T:System.SByte" /> in signatures.</summary>
		// Token: 0x040004C3 RID: 1219
		SByte,
		/// <summary>Represents a <see cref="T:System.Byte" /> in signatures.</summary>
		// Token: 0x040004C4 RID: 1220
		Byte,
		/// <summary>Represents an <see cref="T:System.Int16" /> in signatures.</summary>
		// Token: 0x040004C5 RID: 1221
		Int16,
		/// <summary>Represents a <see cref="T:System.UInt16" /> in signatures.</summary>
		// Token: 0x040004C6 RID: 1222
		UInt16,
		/// <summary>Represents an <see cref="T:System.Int32" /> in signatures.</summary>
		// Token: 0x040004C7 RID: 1223
		Int32,
		/// <summary>Represents a <see cref="T:System.UInt32" /> in signatures.</summary>
		// Token: 0x040004C8 RID: 1224
		UInt32,
		/// <summary>Represents an <see cref="T:System.Int64" /> in signatures.</summary>
		// Token: 0x040004C9 RID: 1225
		Int64,
		/// <summary>Represents a <see cref="T:System.UInt64" /> in signatures.</summary>
		// Token: 0x040004CA RID: 1226
		UInt64,
		/// <summary>Represents a <see cref="T:System.Single" /> in signatures.</summary>
		// Token: 0x040004CB RID: 1227
		Single,
		/// <summary>Represents a <see cref="T:System.Double" /> in signatures.</summary>
		// Token: 0x040004CC RID: 1228
		Double,
		/// <summary>Represents a <see cref="T:System.String" /> in signatures.</summary>
		// Token: 0x040004CD RID: 1229
		String,
		/// <summary>Represents an unmanaged pointer in signatures. It is followed in the blob by the signature encoding of the underlying type.</summary>
		// Token: 0x040004CE RID: 1230
		Pointer,
		/// <summary>Represents managed pointers (byref return values and parameters) in signatures. It is followed in the blob by the signature encoding of the underlying type.</summary>
		// Token: 0x040004CF RID: 1231
		ByReference,
		/// <summary>Represents a generic type parameter used within a signature.</summary>
		// Token: 0x040004D0 RID: 1232
		GenericTypeParameter = 19,
		/// <summary>Represents a generalized <see cref="T:System.Array" /> in signatures.</summary>
		// Token: 0x040004D1 RID: 1233
		Array,
		/// <summary>Represents the instantiation of a generic type in signatures.</summary>
		// Token: 0x040004D2 RID: 1234
		GenericTypeInstance,
		/// <summary>Represents a typed reference in signatures.</summary>
		// Token: 0x040004D3 RID: 1235
		TypedReference,
		/// <summary>Represents an <see cref="T:System.IntPtr" /> in signatures.</summary>
		// Token: 0x040004D4 RID: 1236
		IntPtr = 24,
		/// <summary>Represents a <see cref="T:System.UIntPtr" /> in signatures.</summary>
		// Token: 0x040004D5 RID: 1237
		UIntPtr,
		/// <summary>Represents function pointer types in signatures.</summary>
		// Token: 0x040004D6 RID: 1238
		FunctionPointer = 27,
		/// <summary>Represents an <see cref="T:System.Object" /> in signatures.</summary>
		// Token: 0x040004D7 RID: 1239
		Object,
		/// <summary>Represents a single dimensional <see cref="T:System.Array" /> with a lower bound of 0.</summary>
		// Token: 0x040004D8 RID: 1240
		SZArray,
		/// <summary>Represents a generic method parameter used within a signature.</summary>
		// Token: 0x040004D9 RID: 1241
		GenericMethodParameter,
		/// <summary>Represents a custom modifier applied to a type within a signature that the caller must understand.</summary>
		// Token: 0x040004DA RID: 1242
		RequiredModifier,
		/// <summary>Represents a custom modifier applied to a type within a signature that the caller can ignore.</summary>
		// Token: 0x040004DB RID: 1243
		OptionalModifier,
		/// <summary>Precedes a type <see cref="T:System.Reflection.Metadata.EntityHandle" /> in signatures. In raw metadata, this is encoded as either ELEMENT_TYPE_CLASS (0x12) for reference types or ELEMENT_TYPE_VALUETYPE (0x11) for value types. This is collapsed to a single code because Windows Runtime projections can project from class to value type or vice-versa, and the raw code is misleading in those cases.</summary>
		// Token: 0x040004DC RID: 1244
		TypeHandle = 64,
		/// <summary>Represents a marker to indicate the end of fixed arguments and the beginning of variable arguments.</summary>
		// Token: 0x040004DD RID: 1245
		Sentinel,
		/// <summary>Represents a local variable that is pinned by garbage collector.</summary>
		// Token: 0x040004DE RID: 1246
		Pinned = 69
	}
}
