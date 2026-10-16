using System;

namespace System.CodeDom
{
	/// <summary>Specifies how the code type reference is to be resolved.</summary>
	// Token: 0x020002EF RID: 751
	[Flags]
	public enum CodeTypeReferenceOptions
	{
		/// <summary>Resolve the type from the root namespace.</summary>
		// Token: 0x04000D5F RID: 3423
		GlobalReference = 1,
		/// <summary>Resolve the type from the type parameter.</summary>
		// Token: 0x04000D60 RID: 3424
		GenericTypeParameter = 2
	}
}
