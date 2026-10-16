using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000022 RID: 34
	internal struct ExtensionMethodScope
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x0003627D File Offset: 0x0003447D
		public ExtensionMethodScope(Binder binder)
		{
			this.Binder = binder;
		}

		// Token: 0x04000060 RID: 96
		public readonly Binder Binder;
	}
}
