using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000596 RID: 1430
	internal sealed class SynthesizedIntrinsicOperatorSymbol : MethodSymbol
	{
		// Token: 0x0600779C RID: 30620 RVA: 0x001B8424 File Offset: 0x001B6624
		public SynthesizedIntrinsicOperatorSymbol(TypeSymbol leftType, string name, TypeSymbol rightType, TypeSymbol returnType, bool isCheckedBuiltin)
		{
			if (leftType.Equals(rightType, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
			{
				this._containingType = leftType;
			}
			else if (rightType.Equals(returnType, TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
			{
				this._containingType = rightType;
			}
			else
			{
				this._containingType = leftType;
			}
			this._name = name;
			this._returnType = returnType;
			this._parameters = new ParameterSymbol[]
			{
				new SynthesizedIntrinsicOperatorSymbol.SynthesizedOperatorParameterSymbol(this, leftType, 0, "left"),
				new SynthesizedIntrinsicOperatorSymbol.SynthesizedOperatorParameterSymbol(this, rightType, 1, "right")
			}.AsImmutableOrNull<ParameterSymbol>();
			this._isCheckedBuiltin = isCheckedBuiltin;
		}

		// Token: 0x0600779D RID: 30621 RVA: 0x001B84B0 File Offset: 0x001B66B0
		public SynthesizedIntrinsicOperatorSymbol(TypeSymbol container, string name, TypeSymbol returnType, bool isCheckedBuiltin)
		{
			this._containingType = container;
			this._name = name;
			this._returnType = returnType;
			this._parameters = new ParameterSymbol[]
			{
				new SynthesizedIntrinsicOperatorSymbol.SynthesizedOperatorParameterSymbol(this, container, 0, "value")
			}.AsImmutableOrNull<ParameterSymbol>();
			this._isCheckedBuiltin = isCheckedBuiltin;
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x0600779E RID: 30622 RVA: 0x001B8501 File Offset: 0x001B6701
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x0600779F RID: 30623 RVA: 0x001B8509 File Offset: 0x001B6709
		public override bool IsCheckedBuiltin
		{
			get
			{
				return this._isCheckedBuiltin;
			}
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x060077A0 RID: 30624 RVA: 0x001B8511 File Offset: 0x001B6711
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.BuiltinOperator;
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x060077A1 RID: 30625 RVA: 0x001B8515 File Offset: 0x001B6715
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x060077A2 RID: 30626 RVA: 0x001B8518 File Offset: 0x001B6718
		internal override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060077A3 RID: 30627 RVA: 0x001B851B File Offset: 0x001B671B
		public override string GetDocumentationCommentId()
		{
			return null;
		}

		// Token: 0x060077A4 RID: 30628 RVA: 0x001B851E File Offset: 0x001B671E
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x001B8521 File Offset: 0x001B6721
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x060077A6 RID: 30630 RVA: 0x001B8524 File Offset: 0x001B6724
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x060077A7 RID: 30631 RVA: 0x001B8527 File Offset: 0x001B6727
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x060077A8 RID: 30632 RVA: 0x001B852A File Offset: 0x001B672A
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x060077A9 RID: 30633 RVA: 0x001B852D File Offset: 0x001B672D
		internal override bool HasSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x060077AA RID: 30634 RVA: 0x001B8530 File Offset: 0x001B6730
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x060077AB RID: 30635 RVA: 0x001B8533 File Offset: 0x001B6733
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x001B8536 File Offset: 0x001B6736
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x060077AD RID: 30637 RVA: 0x001B8539 File Offset: 0x001B6739
		public override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x001B8540 File Offset: 0x001B6740
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			return SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x060077AF RID: 30639 RVA: 0x001B8547 File Offset: 0x001B6747
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x060077B0 RID: 30640 RVA: 0x001B854A File Offset: 0x001B674A
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x060077B1 RID: 30641 RVA: 0x001B854D File Offset: 0x001B674D
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x060077B2 RID: 30642 RVA: 0x001B8550 File Offset: 0x001B6750
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x060077B3 RID: 30643 RVA: 0x001B8553 File Offset: 0x001B6753
		public override bool ReturnsVoid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x060077B4 RID: 30644 RVA: 0x001B8556 File Offset: 0x001B6756
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x060077B5 RID: 30645 RVA: 0x001B8559 File Offset: 0x001B6759
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x060077B6 RID: 30646 RVA: 0x001B855C File Offset: 0x001B675C
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this._returnType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x060077B7 RID: 30647 RVA: 0x001B857E File Offset: 0x001B677E
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x060077B8 RID: 30648 RVA: 0x001B8581 File Offset: 0x001B6781
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x060077B9 RID: 30649 RVA: 0x001B8588 File Offset: 0x001B6788
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x060077BA RID: 30650 RVA: 0x001B858B File Offset: 0x001B678B
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x060077BB RID: 30651 RVA: 0x001B8592 File Offset: 0x001B6792
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x060077BC RID: 30652 RVA: 0x001B8599 File Offset: 0x001B6799
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x060077BD RID: 30653 RVA: 0x001B85A1 File Offset: 0x001B67A1
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x060077BE RID: 30654 RVA: 0x001B85A8 File Offset: 0x001B67A8
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x060077BF RID: 30655 RVA: 0x001B85AB File Offset: 0x001B67AB
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x060077C0 RID: 30656 RVA: 0x001B85AE File Offset: 0x001B67AE
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x170021E0 RID: 8672
		// (get) Token: 0x060077C1 RID: 30657 RVA: 0x001B85B5 File Offset: 0x001B67B5
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060077C2 RID: 30658 RVA: 0x001B85B8 File Offset: 0x001B67B8
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x060077C3 RID: 30659 RVA: 0x001B85BF File Offset: 0x001B67BF
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x060077C4 RID: 30660 RVA: 0x001B85C2 File Offset: 0x001B67C2
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x060077C5 RID: 30661 RVA: 0x001B85C5 File Offset: 0x001B67C5
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x060077C6 RID: 30662 RVA: 0x001B85CD File Offset: 0x001B67CD
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType as NamedTypeSymbol;
			}
		}

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x060077C7 RID: 30663 RVA: 0x001B85DA File Offset: 0x001B67DA
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x060077C8 RID: 30664 RVA: 0x001B85E1 File Offset: 0x001B67E1
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x060077C9 RID: 30665 RVA: 0x001B85E8 File Offset: 0x001B67E8
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Public;
			}
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x060077CA RID: 30666 RVA: 0x001B85EB File Offset: 0x001B67EB
		public override bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x060077CB RID: 30667 RVA: 0x001B85EE File Offset: 0x001B67EE
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x060077CC RID: 30668 RVA: 0x001B85F1 File Offset: 0x001B67F1
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x060077CD RID: 30669 RVA: 0x001B85F4 File Offset: 0x001B67F4
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x060077CE RID: 30670 RVA: 0x001B85F7 File Offset: 0x001B67F7
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x060077CF RID: 30671 RVA: 0x001B85FA File Offset: 0x001B67FA
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x060077D0 RID: 30672 RVA: 0x001B85FD File Offset: 0x001B67FD
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x001B8600 File Offset: 0x001B6800
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return null;
		}

		// Token: 0x060077D2 RID: 30674 RVA: 0x001B8603 File Offset: 0x001B6803
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x001B860A File Offset: 0x001B680A
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			return false;
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x001B8610 File Offset: 0x001B6810
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (obj == this)
			{
				return true;
			}
			SynthesizedIntrinsicOperatorSymbol synthesizedIntrinsicOperatorSymbol = obj as SynthesizedIntrinsicOperatorSymbol;
			if (synthesizedIntrinsicOperatorSymbol == null)
			{
				return false;
			}
			if (this._isCheckedBuiltin == synthesizedIntrinsicOperatorSymbol._isCheckedBuiltin && this._parameters.Length == synthesizedIntrinsicOperatorSymbol._parameters.Length && string.Equals(this._name, synthesizedIntrinsicOperatorSymbol._name, 4) && TypeSymbol.Equals(this._containingType, synthesizedIntrinsicOperatorSymbol._containingType, compareKind) && TypeSymbol.Equals(this._returnType, synthesizedIntrinsicOperatorSymbol._returnType, compareKind))
			{
				for (int i = 0; i < this._parameters.Length; i++)
				{
					if (!TypeSymbol.Equals(this._parameters[i].Type, synthesizedIntrinsicOperatorSymbol._parameters[i].Type, compareKind))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060077D5 RID: 30677 RVA: 0x001B86EC File Offset: 0x001B68EC
		public override int GetHashCode()
		{
			return Hash.Combine<string>(this._name, Hash.Combine<TypeSymbol>(this._containingType, this._parameters.Length));
		}

		// Token: 0x04001CE1 RID: 7393
		private readonly TypeSymbol _containingType;

		// Token: 0x04001CE2 RID: 7394
		private readonly string _name;

		// Token: 0x04001CE3 RID: 7395
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001CE4 RID: 7396
		private readonly TypeSymbol _returnType;

		// Token: 0x04001CE5 RID: 7397
		private readonly bool _isCheckedBuiltin;

		// Token: 0x02000957 RID: 2391
		private sealed class SynthesizedOperatorParameterSymbol : SynthesizedParameterSymbolBase
		{
			// Token: 0x060090E7 RID: 37095 RVA: 0x001FCA64 File Offset: 0x001FAC64
			public SynthesizedOperatorParameterSymbol(SynthesizedIntrinsicOperatorSymbol container, TypeSymbol type, int ordinal, string name)
				: base(container, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ordinal, RefKind.None, name)
			{
			}

			// Token: 0x060090E8 RID: 37096 RVA: 0x001FCA8C File Offset: 0x001FAC8C
			public override bool Equals(Symbol obj, TypeCompareKind compareKind)
			{
				if (obj == this)
				{
					return true;
				}
				SynthesizedIntrinsicOperatorSymbol.SynthesizedOperatorParameterSymbol synthesizedOperatorParameterSymbol = obj as SynthesizedIntrinsicOperatorSymbol.SynthesizedOperatorParameterSymbol;
				return synthesizedOperatorParameterSymbol != null && this.Ordinal == synthesizedOperatorParameterSymbol.Ordinal && this.ContainingSymbol.Equals(synthesizedOperatorParameterSymbol.ContainingSymbol, compareKind);
			}

			// Token: 0x060090E9 RID: 37097 RVA: 0x001FCAD0 File Offset: 0x001FACD0
			public override int GetHashCode()
			{
				return Hash.Combine<Symbol>(this.ContainingSymbol, this.Ordinal.GetHashCode());
			}

			// Token: 0x170028DF RID: 10463
			// (get) Token: 0x060090EA RID: 37098 RVA: 0x001FCAF6 File Offset: 0x001FACF6
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return ImmutableArray<CustomModifier>.Empty;
				}
			}

			// Token: 0x170028E0 RID: 10464
			// (get) Token: 0x060090EB RID: 37099 RVA: 0x001FCAFD File Offset: 0x001FACFD
			internal override MarshalPseudoCustomAttributeData MarshallingInformation
			{
				get
				{
					return null;
				}
			}
		}
	}
}
