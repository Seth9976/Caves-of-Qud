using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005CE RID: 1486
	internal sealed class SourceAssemblySymbol : AssemblySymbol, ISourceAssemblySymbol, IAssemblySymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007C40 RID: 31808 RVA: 0x001C1237 File Offset: 0x001BF437
		public SourceAssemblySymbol(SourceAssemblySymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x17002453 RID: 9299
		// (get) Token: 0x06007C41 RID: 31809 RVA: 0x001C1246 File Offset: 0x001BF446
		internal override AssemblySymbol UnderlyingAssemblySymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002454 RID: 9300
		// (get) Token: 0x06007C42 RID: 31810 RVA: 0x001C124E File Offset: 0x001BF44E
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x17002455 RID: 9301
		// (get) Token: 0x06007C43 RID: 31811 RVA: 0x001C1256 File Offset: 0x001BF456
		Compilation ISourceAssemblySymbol.Compilation
		{
			get
			{
				return this._underlying.DeclaringCompilation;
			}
		}

		// Token: 0x04001D53 RID: 7507
		private readonly SourceAssemblySymbol _underlying;
	}
}
