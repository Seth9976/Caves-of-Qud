using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200055B RID: 1371
	internal sealed class ThisParameterSymbol : ParameterSymbol
	{
		// Token: 0x0600741B RID: 29723 RVA: 0x001B20EF File Offset: 0x001B02EF
		internal ThisParameterSymbol(MethodSymbol forMethod)
			: this(forMethod, forMethod.ContainingType)
		{
		}

		// Token: 0x0600741C RID: 29724 RVA: 0x001B20FE File Offset: 0x001B02FE
		internal ThisParameterSymbol(MethodSymbol forMethod, TypeSymbol containingType)
		{
			this._containingMethod = forMethod;
			this._containingType = containingType;
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x0600741D RID: 29725 RVA: 0x001B2114 File Offset: 0x001B0314
		public override string Name
		{
			get
			{
				return "this";
			}
		}

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x0600741E RID: 29726 RVA: 0x001B211B File Offset: 0x001B031B
		public override bool IsDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x0600741F RID: 29727 RVA: 0x001B2120 File Offset: 0x001B0320
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this._containingType, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x06007420 RID: 29728 RVA: 0x001B2144 File Offset: 0x001B0344
		public override RefKind RefKind
		{
			get
			{
				NamedTypeSymbol containingType = this.ContainingType;
				if (containingType == null || containingType.TypeKind != TypeKind.Struct)
				{
					return RefKind.None;
				}
				MethodSymbol containingMethod = this._containingMethod;
				if (containingMethod != null && containingMethod.MethodKind == MethodKind.Constructor)
				{
					return RefKind.Out;
				}
				MethodSymbol containingMethod2 = this._containingMethod;
				if (containingMethod2 != null && containingMethod2.IsEffectivelyReadOnly)
				{
					return RefKind.In;
				}
				return RefKind.Ref;
			}
		}

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x06007421 RID: 29729 RVA: 0x001B219E File Offset: 0x001B039E
		public override ImmutableArray<Location> Locations
		{
			get
			{
				if (this._containingMethod == null)
				{
					return ImmutableArray<Location>.Empty;
				}
				return this._containingMethod.Locations;
			}
		}

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x06007422 RID: 29730 RVA: 0x001B21B9 File Offset: 0x001B03B9
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x06007423 RID: 29731 RVA: 0x001B21C0 File Offset: 0x001B03C0
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingMethod ?? this._containingType;
			}
		}

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x06007424 RID: 29732 RVA: 0x001B21D2 File Offset: 0x001B03D2
		internal override ConstantValue ExplicitDefaultConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x06007425 RID: 29733 RVA: 0x001B21D5 File Offset: 0x001B03D5
		internal override bool IsMetadataOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x06007426 RID: 29734 RVA: 0x001B21D8 File Offset: 0x001B03D8
		public override bool IsParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x06007427 RID: 29735 RVA: 0x001B21DB File Offset: 0x001B03DB
		internal override bool IsIDispatchConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x06007428 RID: 29736 RVA: 0x001B21DE File Offset: 0x001B03DE
		internal override bool IsIUnknownConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x06007429 RID: 29737 RVA: 0x001B21E1 File Offset: 0x001B03E1
		internal override bool IsCallerFilePath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x0600742A RID: 29738 RVA: 0x001B21E4 File Offset: 0x001B03E4
		internal override bool IsCallerLineNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x0600742B RID: 29739 RVA: 0x001B21E7 File Offset: 0x001B03E7
		internal override bool IsCallerMemberName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x0600742C RID: 29740 RVA: 0x001B21EA File Offset: 0x001B03EA
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x0600742D RID: 29741 RVA: 0x001B21ED File Offset: 0x001B03ED
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x0600742E RID: 29742 RVA: 0x001B21F4 File Offset: 0x001B03F4
		public override int Ordinal
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x0600742F RID: 29743 RVA: 0x001B21F7 File Offset: 0x001B03F7
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x06007430 RID: 29744 RVA: 0x001B21FE File Offset: 0x001B03FE
		public override bool IsThis
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x06007431 RID: 29745 RVA: 0x001B2201 File Offset: 0x001B0401
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x06007432 RID: 29746 RVA: 0x001B2204 File Offset: 0x001B0404
		internal override bool IsMetadataIn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06007433 RID: 29747 RVA: 0x001B2207 File Offset: 0x001B0407
		internal override bool IsMetadataOut
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x06007434 RID: 29748 RVA: 0x001B220A File Offset: 0x001B040A
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001C24 RID: 7204
		internal const string SymbolName = "this";

		// Token: 0x04001C25 RID: 7205
		private readonly MethodSymbol _containingMethod;

		// Token: 0x04001C26 RID: 7206
		private readonly TypeSymbol _containingType;
	}
}
