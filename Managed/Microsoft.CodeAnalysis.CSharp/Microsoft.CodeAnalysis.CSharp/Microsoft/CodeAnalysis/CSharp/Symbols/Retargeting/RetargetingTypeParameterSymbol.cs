using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005DF RID: 1503
	internal sealed class RetargetingTypeParameterSymbol : WrappedTypeParameterSymbol
	{
		// Token: 0x06007D88 RID: 32136 RVA: 0x001C344A File Offset: 0x001C164A
		public RetargetingTypeParameterSymbol(RetargetingModuleSymbol retargetingModule, TypeParameterSymbol underlyingTypeParameter)
			: base(underlyingTypeParameter)
		{
			this._retargetingModule = retargetingModule;
		}

		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x06007D89 RID: 32137 RVA: 0x001C345A File Offset: 0x001C165A
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			get
			{
				return this._retargetingModule.RetargetingTranslator;
			}
		}

		// Token: 0x17002513 RID: 9491
		// (get) Token: 0x06007D8A RID: 32138 RVA: 0x001C3467 File Offset: 0x001C1667
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingTypeParameter.ContainingSymbol);
			}
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x001C347F File Offset: 0x001C167F
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingTypeParameter.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x17002514 RID: 9492
		// (get) Token: 0x06007D8C RID: 32140 RVA: 0x001C349D File Offset: 0x001C169D
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._retargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x17002515 RID: 9493
		// (get) Token: 0x06007D8D RID: 32141 RVA: 0x001C34AA File Offset: 0x001C16AA
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x001C34B2 File Offset: 0x001C16B2
		internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingTypeParameter.GetConstraintTypes(inProgress));
		}

		// Token: 0x17002516 RID: 9494
		// (get) Token: 0x06007D8F RID: 32143 RVA: 0x001C34CB File Offset: 0x001C16CB
		internal override bool? IsNotNullable
		{
			get
			{
				return this._underlyingTypeParameter.IsNotNullable;
			}
		}

		// Token: 0x06007D90 RID: 32144 RVA: 0x001C34D8 File Offset: 0x001C16D8
		internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingTypeParameter.GetInterfaces(inProgress));
		}

		// Token: 0x06007D91 RID: 32145 RVA: 0x001C34F1 File Offset: 0x001C16F1
		internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingTypeParameter.GetEffectiveBaseClass(inProgress), RetargetOptions.RetargetPrimitiveTypesByTypeCode);
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x001C350B File Offset: 0x001C170B
		internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingTypeParameter.GetDeducedBaseType(inProgress), RetargetOptions.RetargetPrimitiveTypesByTypeCode);
		}

		// Token: 0x17002517 RID: 9495
		// (get) Token: 0x06007D93 RID: 32147 RVA: 0x001C3525 File Offset: 0x001C1725
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001D93 RID: 7571
		private readonly RetargetingModuleSymbol _retargetingModule;

		// Token: 0x04001D94 RID: 7572
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;
	}
}
