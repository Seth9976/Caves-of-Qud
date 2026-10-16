using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000529 RID: 1321
	internal sealed class SourceClonedParameterSymbol : SourceParameterSymbolBase
	{
		// Token: 0x06006EEC RID: 28396 RVA: 0x00197879 File Offset: 0x00195A79
		internal SourceClonedParameterSymbol(SourceParameterSymbol originalParam, Symbol newOwner, int newOrdinal, bool suppressOptional)
			: base(newOwner, newOrdinal)
		{
			this._suppressOptional = suppressOptional;
			this._originalParam = originalParam;
		}

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x06006EED RID: 28397 RVA: 0x00197892 File Offset: 0x00195A92
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06006EEE RID: 28398 RVA: 0x00197895 File Offset: 0x00195A95
		public override bool IsDiscard
		{
			get
			{
				return this._originalParam.IsDiscard;
			}
		}

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x06006EEF RID: 28399 RVA: 0x001978A2 File Offset: 0x00195AA2
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x06006EF0 RID: 28400 RVA: 0x001978A9 File Offset: 0x00195AA9
		public override bool IsParams
		{
			get
			{
				return !this._suppressOptional && this._originalParam.IsParams;
			}
		}

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x06006EF1 RID: 28401 RVA: 0x001978C0 File Offset: 0x00195AC0
		internal override bool IsMetadataOptional
		{
			get
			{
				if (!this._suppressOptional)
				{
					return this._originalParam.IsMetadataOptional;
				}
				return this._originalParam.HasOptionalAttribute;
			}
		}

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x06006EF2 RID: 28402 RVA: 0x001978E1 File Offset: 0x00195AE1
		internal override ConstantValue ExplicitDefaultConstantValue
		{
			get
			{
				if (!this._suppressOptional)
				{
					return this._originalParam.ExplicitDefaultConstantValue;
				}
				return this._originalParam.DefaultValueFromAttributes;
			}
		}

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x06006EF3 RID: 28403 RVA: 0x00197902 File Offset: 0x00195B02
		internal override ConstantValue DefaultValueFromAttributes
		{
			get
			{
				return this._originalParam.DefaultValueFromAttributes;
			}
		}

		// Token: 0x06006EF4 RID: 28404 RVA: 0x0019790F File Offset: 0x00195B0F
		internal override ParameterSymbol WithCustomModifiersAndParams(TypeSymbol newType, ImmutableArray<CustomModifier> newCustomModifiers, ImmutableArray<CustomModifier> newRefCustomModifiers, bool newIsParams)
		{
			return new SourceClonedParameterSymbol(this._originalParam.WithCustomModifiersAndParamsCore(newType, newCustomModifiers, newRefCustomModifiers, newIsParams), this.ContainingSymbol, this.Ordinal, this._suppressOptional);
		}

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x06006EF5 RID: 28405 RVA: 0x00197938 File Offset: 0x00195B38
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._originalParam.TypeWithAnnotations;
			}
		}

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06006EF6 RID: 28406 RVA: 0x00197945 File Offset: 0x00195B45
		public override RefKind RefKind
		{
			get
			{
				return this._originalParam.RefKind;
			}
		}

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06006EF7 RID: 28407 RVA: 0x00197952 File Offset: 0x00195B52
		internal override bool IsMetadataIn
		{
			get
			{
				return this._originalParam.IsMetadataIn;
			}
		}

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x0019795F File Offset: 0x00195B5F
		internal override bool IsMetadataOut
		{
			get
			{
				return this._originalParam.IsMetadataOut;
			}
		}

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x06006EF9 RID: 28409 RVA: 0x0019796C File Offset: 0x00195B6C
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._originalParam.Locations;
			}
		}

		// Token: 0x06006EFA RID: 28410 RVA: 0x00197979 File Offset: 0x00195B79
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this._originalParam.GetAttributes();
		}

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x06006EFB RID: 28411 RVA: 0x00197986 File Offset: 0x00195B86
		public sealed override string Name
		{
			get
			{
				return this._originalParam.Name;
			}
		}

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x06006EFC RID: 28412 RVA: 0x00197993 File Offset: 0x00195B93
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._originalParam.RefCustomModifiers;
			}
		}

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x06006EFD RID: 28413 RVA: 0x001979A0 File Offset: 0x00195BA0
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return this._originalParam.MarshallingInformation;
			}
		}

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x06006EFE RID: 28414 RVA: 0x001979AD File Offset: 0x00195BAD
		internal override bool IsIDispatchConstant
		{
			get
			{
				return this._originalParam.IsIDispatchConstant;
			}
		}

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x06006EFF RID: 28415 RVA: 0x001979BA File Offset: 0x00195BBA
		internal override bool IsIUnknownConstant
		{
			get
			{
				return this._originalParam.IsIUnknownConstant;
			}
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x06006F00 RID: 28416 RVA: 0x001979C7 File Offset: 0x00195BC7
		internal override bool IsCallerFilePath
		{
			get
			{
				return this._originalParam.IsCallerFilePath;
			}
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x06006F01 RID: 28417 RVA: 0x001979D4 File Offset: 0x00195BD4
		internal override bool IsCallerLineNumber
		{
			get
			{
				return this._originalParam.IsCallerLineNumber;
			}
		}

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x06006F02 RID: 28418 RVA: 0x001979E1 File Offset: 0x00195BE1
		internal override bool IsCallerMemberName
		{
			get
			{
				return this._originalParam.IsCallerMemberName;
			}
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06006F03 RID: 28419 RVA: 0x001979EE File Offset: 0x00195BEE
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x06006F04 RID: 28420 RVA: 0x001979F1 File Offset: 0x00195BF1
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x04001B45 RID: 6981
		private readonly bool _suppressOptional;

		// Token: 0x04001B46 RID: 6982
		private readonly SourceParameterSymbol _originalParam;
	}
}
