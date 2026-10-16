using System;

namespace System.Reflection.Metadata
{
	/// <summary>Indicates whether a <see cref="T:System.Reflection.Metadata.StandaloneSignature" /> represents a standalone method or local variable signature.</summary>
	// Token: 0x020000A3 RID: 163
	public enum StandaloneSignatureKind
	{
		/// <summary>The <see cref="T:System.Reflection.Metadata.StandaloneSignature" /> represents a standalone method signature.</summary>
		// Token: 0x040003C0 RID: 960
		Method,
		/// <summary>The <see cref="T:System.Reflection.Metadata.MemberReference" /> references a local variable signature.</summary>
		// Token: 0x040003C1 RID: 961
		LocalVariables
	}
}
