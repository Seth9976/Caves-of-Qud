using System;

namespace System.Reflection.Metadata
{
	/// <summary>Specifies how arguments in a given signature are passed from the caller to the callee. The underlying values of the fields in this type correspond to the representation in the leading signature byte represented by a <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure.</summary>
	// Token: 0x020000D4 RID: 212
	public enum SignatureCallingConvention : byte
	{
		/// <summary>A managed calling convention with a fixed-length argument list.</summary>
		// Token: 0x040004AE RID: 1198
		Default,
		/// <summary>An unmanaged C/C++ style calling convention where the call stack is cleaned by the caller.</summary>
		// Token: 0x040004AF RID: 1199
		CDecl,
		/// <summary>An unmanaged calling convention where the call stack is cleaned up by the callee.</summary>
		// Token: 0x040004B0 RID: 1200
		StdCall,
		/// <summary>An unmanaged C++ style calling convention for calling instance member functions with a fixed argument list.</summary>
		// Token: 0x040004B1 RID: 1201
		ThisCall,
		/// <summary>An unmanaged calling convention where arguments are passed in registers when possible.</summary>
		// Token: 0x040004B2 RID: 1202
		FastCall,
		/// <summary>A managed calling convention for passing extra arguments.</summary>
		// Token: 0x040004B3 RID: 1203
		VarArgs,
		/// <summary>Indicates that the specifics of the unmanaged calling convention are encoded as modopts.</summary>
		// Token: 0x040004B4 RID: 1204
		Unmanaged = 9
	}
}
