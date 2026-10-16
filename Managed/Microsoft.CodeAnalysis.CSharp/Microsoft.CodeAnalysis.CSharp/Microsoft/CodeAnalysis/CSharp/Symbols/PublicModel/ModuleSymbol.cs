using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C3 RID: 1475
	internal sealed class ModuleSymbol : Symbol, IModuleSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007BA0 RID: 31648 RVA: 0x001C098C File Offset: 0x001BEB8C
		public ModuleSymbol(ModuleSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x06007BA1 RID: 31649 RVA: 0x001C099B File Offset: 0x001BEB9B
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x06007BA2 RID: 31650 RVA: 0x001C09A3 File Offset: 0x001BEBA3
		INamespaceSymbol IModuleSymbol.GlobalNamespace
		{
			get
			{
				return this._underlying.GlobalNamespace.GetPublicSymbol();
			}
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x001C09B5 File Offset: 0x001BEBB5
		INamespaceSymbol IModuleSymbol.GetModuleNamespace(INamespaceSymbol namespaceSymbol)
		{
			return this._underlying.GetModuleNamespace(namespaceSymbol).GetPublicSymbol();
		}

		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x06007BA4 RID: 31652 RVA: 0x001C09C8 File Offset: 0x001BEBC8
		ImmutableArray<IAssemblySymbol> IModuleSymbol.ReferencedAssemblySymbols
		{
			get
			{
				return this._underlying.ReferencedAssemblySymbols.GetPublicSymbols();
			}
		}

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x06007BA5 RID: 31653 RVA: 0x001C09DA File Offset: 0x001BEBDA
		ImmutableArray<AssemblyIdentity> IModuleSymbol.ReferencedAssemblies
		{
			get
			{
				return this._underlying.ReferencedAssemblies;
			}
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x001C09E7 File Offset: 0x001BEBE7
		ModuleMetadata IModuleSymbol.GetMetadata()
		{
			return this._underlying.GetMetadata();
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x001C09F4 File Offset: 0x001BEBF4
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitModule(this);
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x001C09FD File Offset: 0x001BEBFD
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitModule(this);
		}

		// Token: 0x04001D46 RID: 7494
		private readonly ModuleSymbol _underlying;
	}
}
