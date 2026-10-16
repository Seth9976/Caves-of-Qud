using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000580 RID: 1408
	internal sealed class SynthesizedAccessorValueParameterSymbol : SourceComplexParameterSymbol
	{
		// Token: 0x060075A3 RID: 30115 RVA: 0x001B67B4 File Offset: 0x001B49B4
		public SynthesizedAccessorValueParameterSymbol(SourceMemberMethodSymbol accessor, TypeWithAnnotations paramType, int ordinal)
			: base(accessor, ordinal, paramType, RefKind.None, "value", accessor.Locations, null, false, false)
		{
		}

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x060075A4 RID: 30116 RVA: 0x001B67DC File Offset: 0x001B49DC
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
				SourcePropertyAccessorSymbol sourcePropertyAccessorSymbol = this.ContainingSymbol as SourcePropertyAccessorSymbol;
				if (sourcePropertyAccessorSymbol != null)
				{
					SourcePropertySymbolBase sourcePropertySymbolBase = sourcePropertyAccessorSymbol.AssociatedSymbol as SourcePropertySymbolBase;
					if (sourcePropertySymbolBase != null)
					{
						if (sourcePropertySymbolBase.HasDisallowNull)
						{
							flowAnalysisAnnotations |= FlowAnalysisAnnotations.DisallowNull;
						}
						if (sourcePropertySymbolBase.HasAllowNull)
						{
							flowAnalysisAnnotations |= FlowAnalysisAnnotations.AllowNull;
						}
					}
				}
				return flowAnalysisAnnotations;
			}
		}

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x060075A5 RID: 30117 RVA: 0x001B6822 File Offset: 0x001B4A22
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x060075A6 RID: 30118 RVA: 0x001B6829 File Offset: 0x001B4A29
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x060075A7 RID: 30119 RVA: 0x001B6830 File Offset: 0x001B4A30
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x060075A8 RID: 30120 RVA: 0x001B6833 File Offset: 0x001B4A33
		protected override IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return (SourceMemberMethodSymbol)this.ContainingSymbol;
			}
		}

		// Token: 0x060075A9 RID: 30121 RVA: 0x001B6840 File Offset: 0x001B4A40
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return ((SourceMemberMethodSymbol)this.ContainingSymbol).GetAttributeDeclarations();
		}

		// Token: 0x060075AA RID: 30122 RVA: 0x001B6854 File Offset: 0x001B4A54
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			SourcePropertyAccessorSymbol sourcePropertyAccessorSymbol = this.ContainingSymbol as SourcePropertyAccessorSymbol;
			if (sourcePropertyAccessorSymbol != null)
			{
				SourcePropertySymbolBase sourcePropertySymbolBase = sourcePropertyAccessorSymbol.AssociatedSymbol as SourcePropertySymbolBase;
				if (sourcePropertySymbolBase != null)
				{
					FlowAnalysisAnnotations flowAnalysisAnnotations = this.FlowAnalysisAnnotations;
					if ((flowAnalysisAnnotations & FlowAnalysisAnnotations.DisallowNull) != FlowAnalysisAnnotations.None)
					{
						Symbol.AddSynthesizedAttribute(ref attributes, new SynthesizedAttributeData(sourcePropertySymbolBase.DisallowNullAttributeIfExists));
					}
					if ((flowAnalysisAnnotations & FlowAnalysisAnnotations.AllowNull) != FlowAnalysisAnnotations.None)
					{
						Symbol.AddSynthesizedAttribute(ref attributes, new SynthesizedAttributeData(sourcePropertySymbolBase.AllowNullAttributeIfExists));
					}
				}
			}
		}
	}
}
