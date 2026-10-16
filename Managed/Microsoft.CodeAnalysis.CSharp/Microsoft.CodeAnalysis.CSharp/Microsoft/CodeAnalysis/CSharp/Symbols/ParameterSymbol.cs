using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B3 RID: 1203
	internal abstract class ParameterSymbol : Symbol, IParameterTypeInformation, IParameterListEntry, IParameterDefinition, IDefinition, IReference, INamedEntity, IParameterSymbolInternal, ISymbolInternal
	{
		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x060065CA RID: 26058 RVA: 0x0018154C File Offset: 0x0017F74C
		ImmutableArray<ICustomModifier> IParameterTypeInformation.CustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this.AdaptedParameterSymbol.TypeWithAnnotations.CustomModifiers);
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x060065CB RID: 26059 RVA: 0x00181571 File Offset: 0x0017F771
		bool IParameterTypeInformation.IsByReference
		{
			get
			{
				return this.AdaptedParameterSymbol.RefKind > RefKind.None;
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x00181581 File Offset: 0x0017F781
		ImmutableArray<ICustomModifier> IParameterTypeInformation.RefCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this.AdaptedParameterSymbol.RefCustomModifiers);
			}
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x00181593 File Offset: 0x0017F793
		ITypeReference IParameterTypeInformation.GetType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedParameterSymbol.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x060065CE RID: 26062 RVA: 0x001815C1 File Offset: 0x0017F7C1
		ushort IParameterListEntry.Index
		{
			get
			{
				return (ushort)this.AdaptedParameterSymbol.Ordinal;
			}
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x001815CF File Offset: 0x0017F7CF
		MetadataConstant IParameterDefinition.GetDefaultValue(EmitContext context)
		{
			return this.GetMetadataConstantValue(context);
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x001815D8 File Offset: 0x0017F7D8
		internal MetadataConstant GetMetadataConstantValue(EmitContext context)
		{
			if (!this.AdaptedParameterSymbol.HasMetadataConstantValue)
			{
				return null;
			}
			ConstantValue explicitDefaultConstantValue = this.AdaptedParameterSymbol.ExplicitDefaultConstantValue;
			TypeSymbol typeSymbol;
			if (explicitDefaultConstantValue.SpecialType != SpecialType.None)
			{
				typeSymbol = this.AdaptedParameterSymbol.ContainingAssembly.GetSpecialType(explicitDefaultConstantValue.SpecialType);
			}
			else
			{
				typeSymbol = this.AdaptedParameterSymbol.Type;
			}
			return ((PEModuleBuilder)context.Module).CreateConstant(typeSymbol, explicitDefaultConstantValue.Value, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x060065D1 RID: 26065 RVA: 0x00181655 File Offset: 0x0017F855
		bool IParameterDefinition.HasDefaultValue
		{
			get
			{
				return this.AdaptedParameterSymbol.HasMetadataConstantValue;
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x060065D2 RID: 26066 RVA: 0x00181662 File Offset: 0x0017F862
		bool IParameterDefinition.IsOptional
		{
			get
			{
				return this.AdaptedParameterSymbol.IsMetadataOptional;
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x060065D3 RID: 26067 RVA: 0x0018166F File Offset: 0x0017F86F
		bool IParameterDefinition.IsIn
		{
			get
			{
				return this.AdaptedParameterSymbol.IsMetadataIn;
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x060065D4 RID: 26068 RVA: 0x0018167C File Offset: 0x0017F87C
		bool IParameterDefinition.IsMarshalledExplicitly
		{
			get
			{
				return this.AdaptedParameterSymbol.IsMarshalledExplicitly;
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x060065D5 RID: 26069 RVA: 0x00181689 File Offset: 0x0017F889
		bool IParameterDefinition.IsOut
		{
			get
			{
				return this.AdaptedParameterSymbol.IsMetadataOut;
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x060065D6 RID: 26070 RVA: 0x00181696 File Offset: 0x0017F896
		IMarshallingInformation IParameterDefinition.MarshallingInformation
		{
			get
			{
				return this.AdaptedParameterSymbol.MarshallingInformation;
			}
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x060065D7 RID: 26071 RVA: 0x001816A3 File Offset: 0x0017F8A3
		ImmutableArray<byte> IParameterDefinition.MarshallingDescriptor
		{
			get
			{
				return this.AdaptedParameterSymbol.MarshallingDescriptor;
			}
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x001816B0 File Offset: 0x0017F8B0
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x001816B8 File Offset: 0x0017F8B8
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			if (this.AdaptedParameterSymbol.IsDefinition && this.AdaptedParameterSymbol.ContainingModule == pemoduleBuilder.SourceModule)
			{
				return this;
			}
			return null;
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x060065DA RID: 26074 RVA: 0x001816F9 File Offset: 0x0017F8F9
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedParameterSymbol.MetadataName;
			}
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x060065DB RID: 26075 RVA: 0x00181706 File Offset: 0x0017F906
		internal ParameterSymbol AdaptedParameterSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x00181709 File Offset: 0x0017F909
		internal new ParameterSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x060065DD RID: 26077 RVA: 0x0018170C File Offset: 0x0017F90C
		internal virtual bool HasMetadataConstantValue
		{
			get
			{
				return this.ExplicitDefaultConstantValue != null && this.ExplicitDefaultConstantValue.SpecialType != SpecialType.System_Decimal && this.ExplicitDefaultConstantValue.SpecialType != SpecialType.System_DateTime;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x060065DE RID: 26078 RVA: 0x0018173F File Offset: 0x0017F93F
		internal virtual bool IsMarshalledExplicitly
		{
			get
			{
				return this.MarshallingInformation != null;
			}
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x060065DF RID: 26079 RVA: 0x0018174C File Offset: 0x0017F94C
		internal virtual ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x00181762 File Offset: 0x0017F962
		internal ParameterSymbol()
		{
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x060065E1 RID: 26081 RVA: 0x0018176A File Offset: 0x0017F96A
		public new virtual ParameterSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x060065E2 RID: 26082 RVA: 0x0018176D File Offset: 0x0017F96D
		protected sealed override Symbol OriginalSymbolDefinition
		{
			get
			{
				return this.OriginalDefinition;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x060065E3 RID: 26083
		public abstract TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x060065E4 RID: 26084 RVA: 0x00181778 File Offset: 0x0017F978
		public TypeSymbol Type
		{
			get
			{
				return this.TypeWithAnnotations.Type;
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x060065E5 RID: 26085
		public abstract RefKind RefKind { get; }

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x060065E6 RID: 26086
		public abstract bool IsDiscard { get; }

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x060065E7 RID: 26087
		public abstract ImmutableArray<CustomModifier> RefCustomModifiers { get; }

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x060065E8 RID: 26088
		internal abstract MarshalPseudoCustomAttributeData MarshallingInformation { get; }

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x060065E9 RID: 26089 RVA: 0x00181794 File Offset: 0x0017F994
		internal virtual UnmanagedType MarshallingType
		{
			get
			{
				MarshalPseudoCustomAttributeData marshallingInformation = this.MarshallingInformation;
				if (marshallingInformation == null)
				{
					return 0;
				}
				return marshallingInformation.UnmanagedType;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x060065EA RID: 26090 RVA: 0x001817B4 File Offset: 0x0017F9B4
		internal bool IsMarshalAsObject
		{
			get
			{
				UnmanagedType marshallingType = this.MarshallingType;
				return marshallingType - 25 <= 1 || marshallingType == 28;
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x060065EB RID: 26091
		public abstract int Ordinal { get; }

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x060065EC RID: 26092
		public abstract bool IsParams { get; }

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x060065ED RID: 26093 RVA: 0x001817D8 File Offset: 0x0017F9D8
		public bool IsOptional
		{
			get
			{
				RefKind refKind;
				return !this.IsParams && this.IsMetadataOptional && ((refKind = this.RefKind) == RefKind.None || refKind == RefKind.In || (refKind == RefKind.Ref && this.ContainingSymbol.ContainingType.IsComImport));
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x060065EE RID: 26094
		internal abstract bool IsMetadataOptional { get; }

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x060065EF RID: 26095
		internal abstract bool IsMetadataIn { get; }

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x060065F0 RID: 26096
		internal abstract bool IsMetadataOut { get; }

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x060065F1 RID: 26097 RVA: 0x0018181D File Offset: 0x0017FA1D
		public bool HasExplicitDefaultValue
		{
			get
			{
				return this.IsOptional && this.ExplicitDefaultConstantValue != null;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x060065F2 RID: 26098 RVA: 0x00181835 File Offset: 0x0017FA35
		[DebuggerBrowsable(0)]
		public object ExplicitDefaultValue
		{
			get
			{
				if (this.HasExplicitDefaultValue)
				{
					return this.ExplicitDefaultConstantValue.Value;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x060065F3 RID: 26099
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal abstract ConstantValue ExplicitDefaultConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x060065F4 RID: 26100 RVA: 0x00181850 File Offset: 0x0017FA50
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Parameter;
			}
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x00181854 File Offset: 0x0017FA54
		internal override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitParameter(this, argument);
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x0018185E File Offset: 0x0017FA5E
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitParameter(this);
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x00181867 File Offset: 0x0017FA67
		public override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitParameter(this);
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x060065F8 RID: 26104 RVA: 0x00181870 File Offset: 0x0017FA70
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x060065F9 RID: 26105 RVA: 0x00181873 File Offset: 0x0017FA73
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x060065FA RID: 26106 RVA: 0x00181876 File Offset: 0x0017FA76
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x060065FB RID: 26107 RVA: 0x00181879 File Offset: 0x0017FA79
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x060065FC RID: 26108 RVA: 0x0018187C File Offset: 0x0017FA7C
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x060065FD RID: 26109 RVA: 0x0018187F File Offset: 0x0017FA7F
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x060065FE RID: 26110 RVA: 0x00181882 File Offset: 0x0017FA82
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x00181885 File Offset: 0x0017FA85
		public virtual bool IsThis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06006600 RID: 26112 RVA: 0x00181888 File Offset: 0x0017FA88
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06006601 RID: 26113
		internal abstract bool IsIDispatchConstant { get; }

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x06006602 RID: 26114
		internal abstract bool IsIUnknownConstant { get; }

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x06006603 RID: 26115
		internal abstract bool IsCallerFilePath { get; }

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x06006604 RID: 26116
		internal abstract bool IsCallerLineNumber { get; }

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06006605 RID: 26117
		internal abstract bool IsCallerMemberName { get; }

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06006606 RID: 26118
		internal abstract FlowAnalysisAnnotations FlowAnalysisAnnotations { get; }

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06006607 RID: 26119
		internal abstract ImmutableHashSet<string> NotNullIfParameterNotNull { get; }

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06006608 RID: 26120 RVA: 0x0018188B File Offset: 0x0017FA8B
		protected sealed override int HighestPriorityUseSiteError
		{
			get
			{
				return 648;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06006609 RID: 26121 RVA: 0x00181894 File Offset: 0x0017FA94
		public sealed override bool HasUnsupportedMetadata
		{
			get
			{
				UseSiteInfo<AssemblySymbol> useSiteInfo = default(UseSiteInfo<AssemblySymbol>);
				base.DeriveUseSiteInfoFromParameter(ref useSiteInfo, this);
				DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
				return diagnosticInfo != null && diagnosticInfo.Code == 648;
			}
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x001818CB File Offset: 0x0017FACB
		protected override ISymbol CreateISymbol()
		{
			return new ParameterSymbol(this);
		}

		// Token: 0x040019A0 RID: 6560
		internal const string ValueParameterName = "value";
	}
}
