using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C8 RID: 1480
	internal sealed class NonSourceAssemblySymbol : AssemblySymbol
	{
		// Token: 0x06007BE4 RID: 31716 RVA: 0x001C0DB4 File Offset: 0x001BEFB4
		public NonSourceAssemblySymbol(AssemblySymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x06007BE5 RID: 31717 RVA: 0x001C0DC3 File Offset: 0x001BEFC3
		internal override AssemblySymbol UnderlyingAssemblySymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x06007BE6 RID: 31718 RVA: 0x001C0DCB File Offset: 0x001BEFCB
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x04001D4A RID: 7498
		private readonly AssemblySymbol _underlying;
	}
}
