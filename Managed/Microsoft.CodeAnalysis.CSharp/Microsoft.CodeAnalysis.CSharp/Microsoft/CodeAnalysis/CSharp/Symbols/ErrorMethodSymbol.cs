using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004DE RID: 1246
	internal sealed class ErrorMethodSymbol : MethodSymbol
	{
		// Token: 0x06006936 RID: 26934 RVA: 0x0018A7EC File Offset: 0x001889EC
		public ErrorMethodSymbol(TypeSymbol containingType, TypeSymbol returnType, string name)
		{
			this._containingType = containingType;
			this._returnType = returnType;
			this._name = name;
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x06006937 RID: 26935 RVA: 0x0018A809 File Offset: 0x00188A09
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x06006938 RID: 26936 RVA: 0x0018A811 File Offset: 0x00188A11
		internal sealed override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06006939 RID: 26937 RVA: 0x0018A814 File Offset: 0x00188A14
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x0600693A RID: 26938 RVA: 0x0018A817 File Offset: 0x00188A17
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x0600693B RID: 26939 RVA: 0x0018A81A File Offset: 0x00188A1A
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x0600693C RID: 26940 RVA: 0x0018A81D File Offset: 0x00188A1D
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x0600693D RID: 26941 RVA: 0x0018A820 File Offset: 0x00188A20
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x0600693E RID: 26942 RVA: 0x0018A823 File Offset: 0x00188A23
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x0600693F RID: 26943 RVA: 0x0018A826 File Offset: 0x00188A26
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x06006940 RID: 26944 RVA: 0x0018A829 File Offset: 0x00188A29
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06006941 RID: 26945 RVA: 0x0018A82C File Offset: 0x00188A2C
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x0018A82F File Offset: 0x00188A2F
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return null;
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06006943 RID: 26947 RVA: 0x0018A832 File Offset: 0x00188A32
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Public;
			}
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06006944 RID: 26948 RVA: 0x0018A835 File Offset: 0x00188A35
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06006945 RID: 26949 RVA: 0x0018A83C File Offset: 0x00188A3C
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06006946 RID: 26950 RVA: 0x0018A843 File Offset: 0x00188A43
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x06006947 RID: 26951 RVA: 0x0018A84B File Offset: 0x00188A4B
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x06006948 RID: 26952 RVA: 0x0018A84E File Offset: 0x00188A4E
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x06006949 RID: 26953 RVA: 0x0018A851 File Offset: 0x00188A51
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x0600694A RID: 26954 RVA: 0x0018A858 File Offset: 0x00188A58
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x0600694B RID: 26955 RVA: 0x0018A85B File Offset: 0x00188A5B
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x0600694C RID: 26956 RVA: 0x0018A862 File Offset: 0x00188A62
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x0600694D RID: 26957 RVA: 0x0018A865 File Offset: 0x00188A65
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x0600694E RID: 26958 RVA: 0x0018A868 File Offset: 0x00188A68
		internal override int ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x0600694F RID: 26959 RVA: 0x0018A86B File Offset: 0x00188A6B
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x06006950 RID: 26960 RVA: 0x0018A872 File Offset: 0x00188A72
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x06006951 RID: 26961 RVA: 0x0018A879 File Offset: 0x00188A79
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06006952 RID: 26962 RVA: 0x0018A880 File Offset: 0x00188A80
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06006953 RID: 26963 RVA: 0x0018A884 File Offset: 0x00188A84
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return TypeWithAnnotations.Create(this._returnType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06006954 RID: 26964 RVA: 0x0018A8A6 File Offset: 0x00188AA6
		public override bool ReturnsVoid
		{
			get
			{
				return base.ReturnType.IsVoidType();
			}
		}

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x06006955 RID: 26965 RVA: 0x0018A8B3 File Offset: 0x00188AB3
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x06006956 RID: 26966 RVA: 0x0018A8B6 File Offset: 0x00188AB6
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x06006957 RID: 26967 RVA: 0x0018A8BD File Offset: 0x00188ABD
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x06006958 RID: 26968 RVA: 0x0018A8C0 File Offset: 0x00188AC0
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x06006959 RID: 26969 RVA: 0x0018A8C3 File Offset: 0x00188AC3
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x0600695A RID: 26970 RVA: 0x0018A8C6 File Offset: 0x00188AC6
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x0600695B RID: 26971 RVA: 0x0018A8C9 File Offset: 0x00188AC9
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x0600695C RID: 26972 RVA: 0x0018A8CC File Offset: 0x00188ACC
		public override MethodKind MethodKind
		{
			get
			{
				if (this._name == ".ctor")
				{
					return MethodKind.Constructor;
				}
				return MethodKind.Ordinary;
			}
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x0018A8E4 File Offset: 0x00188AE4
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x0018A8E7 File Offset: 0x00188AE7
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x0600695F RID: 26975 RVA: 0x0018A8EA File Offset: 0x00188AEA
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x06006960 RID: 26976 RVA: 0x0018A8ED File Offset: 0x00188AED
		internal sealed override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x0018A8F0 File Offset: 0x00188AF0
		public sealed override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06006962 RID: 26978 RVA: 0x0018A8F3 File Offset: 0x00188AF3
		public override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06006963 RID: 26979 RVA: 0x0018A8FA File Offset: 0x00188AFA
		internal sealed override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x0018A8FD File Offset: 0x00188AFD
		internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06006965 RID: 26981 RVA: 0x0018A904 File Offset: 0x00188B04
		internal sealed override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006966 RID: 26982 RVA: 0x0018A907 File Offset: 0x00188B07
		internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006967 RID: 26983 RVA: 0x0018A90E File Offset: 0x00188B0E
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06006968 RID: 26984 RVA: 0x0018A915 File Offset: 0x00188B15
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x0018A918 File Offset: 0x00188B18
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			return false;
		}

		// Token: 0x04001A40 RID: 6720
		public static readonly ErrorMethodSymbol UnknownMethod = new ErrorMethodSymbol(ErrorTypeSymbol.UnknownResultType, ErrorTypeSymbol.UnknownResultType, string.Empty);

		// Token: 0x04001A41 RID: 6721
		private readonly TypeSymbol _containingType;

		// Token: 0x04001A42 RID: 6722
		private readonly TypeSymbol _returnType;

		// Token: 0x04001A43 RID: 6723
		private readonly string _name;
	}
}
