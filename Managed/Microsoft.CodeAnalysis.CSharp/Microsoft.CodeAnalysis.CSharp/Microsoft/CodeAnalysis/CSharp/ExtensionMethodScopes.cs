using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000023 RID: 35
	internal struct ExtensionMethodScopes
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x00036286 File Offset: 0x00034486
		public ExtensionMethodScopes(Binder binder)
		{
			this._binder = binder;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0003628F File Offset: 0x0003448F
		public ExtensionMethodScopeEnumerator GetEnumerator()
		{
			return new ExtensionMethodScopeEnumerator(this._binder);
		}

		// Token: 0x04000061 RID: 97
		private readonly Binder _binder;
	}
}
