using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000BF RID: 191
	[Flags]
	internal enum TypeDefinitionTreatment
	{
		// Token: 0x040002DA RID: 730
		None = 0,
		// Token: 0x040002DB RID: 731
		KindMask = 15,
		// Token: 0x040002DC RID: 732
		NormalType = 1,
		// Token: 0x040002DD RID: 733
		NormalAttribute = 2,
		// Token: 0x040002DE RID: 734
		UnmangleWindowsRuntimeName = 3,
		// Token: 0x040002DF RID: 735
		PrefixWindowsRuntimeName = 4,
		// Token: 0x040002E0 RID: 736
		RedirectToClrType = 5,
		// Token: 0x040002E1 RID: 737
		RedirectToClrAttribute = 6,
		// Token: 0x040002E2 RID: 738
		RedirectImplementedMethods = 7,
		// Token: 0x040002E3 RID: 739
		Abstract = 16,
		// Token: 0x040002E4 RID: 740
		Internal = 32
	}
}
