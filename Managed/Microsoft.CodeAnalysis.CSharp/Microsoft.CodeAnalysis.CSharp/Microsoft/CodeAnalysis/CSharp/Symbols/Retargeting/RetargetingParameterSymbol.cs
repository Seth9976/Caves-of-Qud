using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005DA RID: 1498
	internal abstract class RetargetingParameterSymbol : WrappedParameterSymbol
	{
		// Token: 0x06007D62 RID: 32098 RVA: 0x001C2FF8 File Offset: 0x001C11F8
		protected RetargetingParameterSymbol(ParameterSymbol underlyingParameter)
			: base(underlyingParameter)
		{
		}

		// Token: 0x170024F7 RID: 9463
		// (get) Token: 0x06007D63 RID: 32099
		protected abstract RetargetingModuleSymbol RetargetingModule { get; }

		// Token: 0x170024F8 RID: 9464
		// (get) Token: 0x06007D64 RID: 32100 RVA: 0x001C3001 File Offset: 0x001C1201
		public sealed override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this.RetargetingModule.RetargetingTranslator.Retarget(this._underlyingParameter.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
			}
		}

		// Token: 0x170024F9 RID: 9465
		// (get) Token: 0x06007D65 RID: 32101 RVA: 0x001C3020 File Offset: 0x001C1220
		public sealed override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this.RetargetingModule.RetargetingTranslator.RetargetModifiers(this._underlyingParameter.RefCustomModifiers, ref this._lazyRefCustomModifiers);
			}
		}

		// Token: 0x170024FA RID: 9466
		// (get) Token: 0x06007D66 RID: 32102 RVA: 0x001C3043 File Offset: 0x001C1243
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingModule.RetargetingTranslator.Retarget(this._underlyingParameter.ContainingSymbol);
			}
		}

		// Token: 0x06007D67 RID: 32103 RVA: 0x001C3060 File Offset: 0x001C1260
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingModule.RetargetingTranslator.GetRetargetedAttributes(this._underlyingParameter.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x06007D68 RID: 32104 RVA: 0x001C3083 File Offset: 0x001C1283
		internal sealed override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.RetargetingModule.RetargetingTranslator.RetargetAttributes(this._underlyingParameter.GetCustomAttributesToEmit(moduleBuilder));
		}

		// Token: 0x170024FB RID: 9467
		// (get) Token: 0x06007D69 RID: 32105 RVA: 0x001C30A1 File Offset: 0x001C12A1
		public sealed override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this.RetargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x170024FC RID: 9468
		// (get) Token: 0x06007D6A RID: 32106 RVA: 0x001C30AE File Offset: 0x001C12AE
		internal sealed override ModuleSymbol ContainingModule
		{
			get
			{
				return this.RetargetingModule;
			}
		}

		// Token: 0x170024FD RID: 9469
		// (get) Token: 0x06007D6B RID: 32107 RVA: 0x001C30B6 File Offset: 0x001C12B6
		internal sealed override bool HasMetadataConstantValue
		{
			get
			{
				return this._underlyingParameter.HasMetadataConstantValue;
			}
		}

		// Token: 0x170024FE RID: 9470
		// (get) Token: 0x06007D6C RID: 32108 RVA: 0x001C30C3 File Offset: 0x001C12C3
		internal sealed override bool IsMarshalledExplicitly
		{
			get
			{
				return this._underlyingParameter.IsMarshalledExplicitly;
			}
		}

		// Token: 0x170024FF RID: 9471
		// (get) Token: 0x06007D6D RID: 32109 RVA: 0x001C30D0 File Offset: 0x001C12D0
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this.RetargetingModule.RetargetingTranslator.Retarget(this._underlyingParameter.MarshallingInformation);
			}
		}

		// Token: 0x17002500 RID: 9472
		// (get) Token: 0x06007D6E RID: 32110 RVA: 0x001C30ED File Offset: 0x001C12ED
		internal override ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				return this._underlyingParameter.MarshallingDescriptor;
			}
		}

		// Token: 0x17002501 RID: 9473
		// (get) Token: 0x06007D6F RID: 32111 RVA: 0x001C30FA File Offset: 0x001C12FA
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001D85 RID: 7557
		private ImmutableArray<CustomModifier> _lazyRefCustomModifiers;

		// Token: 0x04001D86 RID: 7558
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;
	}
}
