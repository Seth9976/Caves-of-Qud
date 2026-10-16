using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000585 RID: 1413
	internal sealed class SynthesizedEmbeddedAttributeSymbol : SynthesizedEmbeddedAttributeSymbolBase
	{
		// Token: 0x0600763E RID: 30270 RVA: 0x001B719B File Offset: 0x001B539B
		public SynthesizedEmbeddedAttributeSymbol(string name, NamespaceSymbol containingNamespace, ModuleSymbol containingModule, NamedTypeSymbol baseType)
			: base(name, containingNamespace, containingModule, baseType)
		{
			this._constructors = ImmutableArray.Create<MethodSymbol>(new SynthesizedEmbeddedAttributeConstructorSymbol(this, (MethodSymbol m) => ImmutableArray<ParameterSymbol>.Empty));
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x0600763F RID: 30271 RVA: 0x001B71D8 File Offset: 0x001B53D8
		public override ImmutableArray<MethodSymbol> Constructors
		{
			get
			{
				return this._constructors;
			}
		}

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x06007640 RID: 30272 RVA: 0x001B71E0 File Offset: 0x001B53E0
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x06007641 RID: 30273 RVA: 0x001B71E3 File Offset: 0x001B53E3
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x001B71E6 File Offset: 0x001B53E6
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x04001CB5 RID: 7349
		private readonly ImmutableArray<MethodSymbol> _constructors;
	}
}
