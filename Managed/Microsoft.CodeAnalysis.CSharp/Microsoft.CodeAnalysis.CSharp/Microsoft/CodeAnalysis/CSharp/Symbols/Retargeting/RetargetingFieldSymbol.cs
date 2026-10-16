using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D5 RID: 1493
	internal sealed class RetargetingFieldSymbol : WrappedFieldSymbol
	{
		// Token: 0x06007CD5 RID: 31957 RVA: 0x001C1E27 File Offset: 0x001C0027
		public RetargetingFieldSymbol(RetargetingModuleSymbol retargetingModule, FieldSymbol underlyingField)
			: base(underlyingField)
		{
			this._retargetingModule = retargetingModule;
		}

		// Token: 0x170024AF RID: 9391
		// (get) Token: 0x06007CD6 RID: 31958 RVA: 0x001C1E42 File Offset: 0x001C0042
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			get
			{
				return this._retargetingModule.RetargetingTranslator;
			}
		}

		// Token: 0x170024B0 RID: 9392
		// (get) Token: 0x06007CD7 RID: 31959 RVA: 0x001C1E4F File Offset: 0x001C004F
		public RetargetingModuleSymbol RetargetingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x001C1E57 File Offset: 0x001C0057
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this.RetargetingTranslator.Retarget(this._underlyingField.GetFieldType(fieldsBeingBound), RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
		}

		// Token: 0x170024B1 RID: 9393
		// (get) Token: 0x06007CD9 RID: 31961 RVA: 0x001C1E72 File Offset: 0x001C0072
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingField.ContainingSymbol);
			}
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x001C1E8A File Offset: 0x001C008A
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingField.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x001C1EA8 File Offset: 0x001C00A8
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.RetargetingTranslator.RetargetAttributes(this._underlyingField.GetCustomAttributesToEmit(moduleBuilder));
		}

		// Token: 0x170024B2 RID: 9394
		// (get) Token: 0x06007CDC RID: 31964 RVA: 0x001C1EC1 File Offset: 0x001C00C1
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._retargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x170024B3 RID: 9395
		// (get) Token: 0x06007CDD RID: 31965 RVA: 0x001C1ECE File Offset: 0x001C00CE
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x170024B4 RID: 9396
		// (get) Token: 0x06007CDE RID: 31966 RVA: 0x001C1ED6 File Offset: 0x001C00D6
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingField.MarshallingInformation);
			}
		}

		// Token: 0x170024B5 RID: 9397
		// (get) Token: 0x06007CDF RID: 31967 RVA: 0x001C1EF0 File Offset: 0x001C00F0
		public override Symbol AssociatedSymbol
		{
			get
			{
				Symbol associatedSymbol = this._underlyingField.AssociatedSymbol;
				if (associatedSymbol != null)
				{
					return this.RetargetingTranslator.Retarget(associatedSymbol);
				}
				return null;
			}
		}

		// Token: 0x170024B6 RID: 9398
		// (get) Token: 0x06007CE0 RID: 31968 RVA: 0x001C1F1A File Offset: 0x001C011A
		public override int TupleElementIndex
		{
			get
			{
				return this._underlyingField.TupleElementIndex;
			}
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x001C1F28 File Offset: 0x001C0128
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (!this._lazyCachedUseSiteInfo.IsInitialized)
			{
				AssemblySymbol primaryDependency = base.PrimaryDependency;
				UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(primaryDependency);
				base.CalculateUseSiteDiagnostic(ref useSiteInfo);
				this._lazyCachedUseSiteInfo.Initialize(primaryDependency, useSiteInfo);
			}
			return this._lazyCachedUseSiteInfo.ToUseSiteInfo(base.PrimaryDependency);
		}

		// Token: 0x170024B7 RID: 9399
		// (get) Token: 0x06007CE2 RID: 31970 RVA: 0x001C1F78 File Offset: 0x001C0178
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001D60 RID: 7520
		private readonly RetargetingModuleSymbol _retargetingModule;

		// Token: 0x04001D61 RID: 7521
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001D62 RID: 7522
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;
	}
}
