using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200059F RID: 1439
	internal sealed class SynthesizedSubstitutedTypeParameterSymbol : SubstitutedTypeParameterSymbol
	{
		// Token: 0x0600786D RID: 30829 RVA: 0x001B9217 File Offset: 0x001B7417
		public SynthesizedSubstitutedTypeParameterSymbol(Symbol owner, TypeMap map, TypeParameterSymbol substitutedFrom, int ordinal)
			: base(owner, map, substitutedFrom, ordinal)
		{
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x0600786E RID: 30830 RVA: 0x001B9224 File Offset: 0x001B7424
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600786F RID: 30831 RVA: 0x001B9227 File Offset: 0x001B7427
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			if (this.HasUnmanagedTypeConstraint)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsUnmanagedAttribute(this));
			}
		}

		// Token: 0x06007870 RID: 30832 RVA: 0x001B9248 File Offset: 0x001B7448
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			SynthesizedMethodBaseSymbol synthesizedMethodBaseSymbol = this.ContainingSymbol as SynthesizedMethodBaseSymbol;
			if (synthesizedMethodBaseSymbol != null && synthesizedMethodBaseSymbol.InheritsBaseMethodAttributes)
			{
				return this._underlyingTypeParameter.GetAttributes();
			}
			return ImmutableArray<CSharpAttributeData>.Empty;
		}
	}
}
