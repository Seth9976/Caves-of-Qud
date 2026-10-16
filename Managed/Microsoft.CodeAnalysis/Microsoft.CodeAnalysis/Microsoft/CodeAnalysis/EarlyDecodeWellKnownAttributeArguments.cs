using System;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000229 RID: 553
	internal struct EarlyDecodeWellKnownAttributeArguments<TEarlyBinder, TNamedTypeSymbol, TAttributeSyntax, TAttributeLocation> where TNamedTypeSymbol : INamedTypeSymbolInternal where TAttributeSyntax : SyntaxNode
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x000445FD File Offset: 0x000427FD
		public T GetOrCreateData<T>() where T : EarlyWellKnownAttributeData, new()
		{
			if (this._lazyDecodeData == null)
			{
				this._lazyDecodeData = new T();
			}
			return (T)((object)this._lazyDecodeData);
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00044622 File Offset: 0x00042822
		public bool HasDecodedData
		{
			get
			{
				return this._lazyDecodeData != null;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0004462F File Offset: 0x0004282F
		public EarlyWellKnownAttributeData DecodedData
		{
			get
			{
				return this._lazyDecodeData;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00044637 File Offset: 0x00042837
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x0004463F File Offset: 0x0004283F
		public TEarlyBinder Binder { readonly get; set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00044648 File Offset: 0x00042848
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x00044650 File Offset: 0x00042850
		public TNamedTypeSymbol AttributeType { readonly get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00044659 File Offset: 0x00042859
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x00044661 File Offset: 0x00042861
		public TAttributeSyntax AttributeSyntax { readonly get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0004466A File Offset: 0x0004286A
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x00044672 File Offset: 0x00042872
		public TAttributeLocation SymbolPart { readonly get; set; }

		// Token: 0x040008F5 RID: 2293
		private EarlyWellKnownAttributeData _lazyDecodeData;
	}
}
