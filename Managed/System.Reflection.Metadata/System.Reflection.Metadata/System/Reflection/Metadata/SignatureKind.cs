using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies the signature kind. The underlying values of the fields in this type correspond to the representation in the leading signature byte represented by a <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure.</summary>
	// Token: 0x020000D6 RID: 214
	public enum SignatureKind : byte
	{
		/// <summary>A method reference, method definition, or standalone method signature.</summary>
		// Token: 0x040004B9 RID: 1209
		Method,
		/// <summary>A field signature.</summary>
		// Token: 0x040004BA RID: 1210
		Field = 6,
		/// <summary>A local variables signature.</summary>
		// Token: 0x040004BB RID: 1211
		LocalVariables,
		/// <summary>A property signature.</summary>
		// Token: 0x040004BC RID: 1212
		Property,
		/// <summary>A method specification signature.</summary>
		// Token: 0x040004BD RID: 1213
		MethodSpecification = 10
	}
}
