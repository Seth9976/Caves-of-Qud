using System;

namespace System.Reflection
{
	// Token: 0x0200001A RID: 26
	[Flags]
	public enum ManifestResourceAttributes
	{
		/// <summary>The resource is exported from the assembly.</summary>
		// Token: 0x04000036 RID: 54
		Public = 1,
		/// <summary>The resource is not exported from the assembly.</summary>
		// Token: 0x04000037 RID: 55
		Private = 2,
		/// <summary>Masks just the visibility-related attributes.</summary>
		// Token: 0x04000038 RID: 56
		VisibilityMask = 7
	}
}
