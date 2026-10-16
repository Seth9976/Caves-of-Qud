using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000024 RID: 36
	internal struct ExtensionMethodScopeEnumerator
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0003629C File Offset: 0x0003449C
		public ExtensionMethodScopeEnumerator(Binder binder)
		{
			this._binder = binder;
			this._current = default(ExtensionMethodScope);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000362B1 File Offset: 0x000344B1
		public ExtensionMethodScope Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000362BC File Offset: 0x000344BC
		public bool MoveNext()
		{
			if (this._current.Binder == null)
			{
				this._current = ExtensionMethodScopeEnumerator.GetNextScope(this._binder);
			}
			else
			{
				Binder binder = this._current.Binder;
				this._current = ExtensionMethodScopeEnumerator.GetNextScope(binder.Next);
			}
			return this._current.Binder != null;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00036314 File Offset: 0x00034514
		private static ExtensionMethodScope GetNextScope(Binder binder)
		{
			for (Binder binder2 = binder; binder2 != null; binder2 = binder2.Next)
			{
				if (binder2.SupportsExtensionMethods)
				{
					return new ExtensionMethodScope(binder2);
				}
			}
			return default(ExtensionMethodScope);
		}

		// Token: 0x04000062 RID: 98
		private readonly Binder _binder;

		// Token: 0x04000063 RID: 99
		private ExtensionMethodScope _current;
	}
}
