using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D9 RID: 217
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x040002A1 RID: 673
		Default = 7,
		// Token: 0x040002A2 RID: 674
		Access = 1,
		// Token: 0x040002A3 RID: 675
		Assign = 2,
		// Token: 0x040002A4 RID: 676
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x040002A5 RID: 677
		InstantiatedNoFixedConstructorSignature = 8
	}
}
