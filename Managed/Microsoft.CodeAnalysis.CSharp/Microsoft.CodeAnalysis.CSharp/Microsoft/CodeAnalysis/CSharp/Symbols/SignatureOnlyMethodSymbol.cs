using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000513 RID: 1299
	internal sealed class SignatureOnlyMethodSymbol : MethodSymbol
	{
		// Token: 0x06006D21 RID: 27937 RVA: 0x00192760 File Offset: 0x00190960
		public SignatureOnlyMethodSymbol(string name, TypeSymbol containingType, MethodKind methodKind, CallingConvention callingConvention, ImmutableArray<TypeParameterSymbol> typeParameters, ImmutableArray<ParameterSymbol> parameters, RefKind refKind, bool isInitOnly, TypeWithAnnotations returnType, ImmutableArray<CustomModifier> refCustomModifiers, ImmutableArray<MethodSymbol> explicitInterfaceImplementations)
		{
			this._callingConvention = callingConvention;
			this._typeParameters = typeParameters;
			this._refKind = refKind;
			this._isInitOnly = isInitOnly;
			this._returnType = returnType;
			this._refCustomModifiers = refCustomModifiers;
			this._parameters = parameters;
			this._explicitInterfaceImplementations = explicitInterfaceImplementations.NullToEmpty<MethodSymbol>();
			this._containingType = containingType;
			this._methodKind = methodKind;
			this._name = name;
		}

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x06006D22 RID: 27938 RVA: 0x001927CD File Offset: 0x001909CD
		internal override CallingConvention CallingConvention
		{
			get
			{
				return this._callingConvention;
			}
		}

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x06006D23 RID: 27939 RVA: 0x001927D8 File Offset: 0x001909D8
		public override bool IsVararg
		{
			get
			{
				return new SignatureHeader((byte)this._callingConvention).CallingConvention == SignatureCallingConvention.VarArgs;
			}
		}

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06006D24 RID: 27940 RVA: 0x001927FC File Offset: 0x001909FC
		public override bool IsGenericMethod
		{
			get
			{
				return this.Arity > 0;
			}
		}

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x06006D25 RID: 27941 RVA: 0x00192808 File Offset: 0x00190A08
		public override int Arity
		{
			get
			{
				return this._typeParameters.Length;
			}
		}

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x06006D26 RID: 27942 RVA: 0x00192823 File Offset: 0x00190A23
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return this._typeParameters;
			}
		}

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x06006D27 RID: 27943 RVA: 0x0019282B File Offset: 0x00190A2B
		public override bool ReturnsVoid
		{
			get
			{
				return this._returnType.IsVoidType();
			}
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06006D28 RID: 27944 RVA: 0x00192838 File Offset: 0x00190A38
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x06006D29 RID: 27945 RVA: 0x00192840 File Offset: 0x00190A40
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x06006D2A RID: 27946 RVA: 0x00192848 File Offset: 0x00190A48
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x06006D2B RID: 27947 RVA: 0x0019284B File Offset: 0x00190A4B
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x06006D2C RID: 27948 RVA: 0x00192852 File Offset: 0x00190A52
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x06006D2D RID: 27949 RVA: 0x00192855 File Offset: 0x00190A55
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._refCustomModifiers;
			}
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x06006D2E RID: 27950 RVA: 0x0019285D File Offset: 0x00190A5D
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x06006D2F RID: 27951 RVA: 0x00192865 File Offset: 0x00190A65
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return this._explicitInterfaceImplementations;
			}
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x06006D30 RID: 27952 RVA: 0x0019286D File Offset: 0x00190A6D
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x06006D31 RID: 27953 RVA: 0x00192875 File Offset: 0x00190A75
		public override MethodKind MethodKind
		{
			get
			{
				return this._methodKind;
			}
		}

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x06006D32 RID: 27954 RVA: 0x0019287D File Offset: 0x00190A7D
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x00192885 File Offset: 0x00190A85
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x06006D34 RID: 27956 RVA: 0x0019288C File Offset: 0x00190A8C
		internal override bool GenerateDebugInfo
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x06006D35 RID: 27957 RVA: 0x00192893 File Offset: 0x00190A93
		internal override bool HasSpecialName
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x06006D36 RID: 27958 RVA: 0x0019289A File Offset: 0x00190A9A
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x06006D37 RID: 27959 RVA: 0x001928A1 File Offset: 0x00190AA1
		internal override bool RequiresSecurityObject
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x001928A8 File Offset: 0x00190AA8
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06006D39 RID: 27961 RVA: 0x001928AB File Offset: 0x00190AAB
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x06006D3A RID: 27962 RVA: 0x001928B2 File Offset: 0x00190AB2
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x001928B9 File Offset: 0x00190AB9
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06006D3C RID: 27964 RVA: 0x001928C0 File Offset: 0x00190AC0
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x001928C7 File Offset: 0x00190AC7
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x001928CE File Offset: 0x00190ACE
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x06006D3F RID: 27967 RVA: 0x001928D5 File Offset: 0x00190AD5
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x06006D40 RID: 27968 RVA: 0x001928DC File Offset: 0x00190ADC
		public override Symbol AssociatedSymbol
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x06006D41 RID: 27969 RVA: 0x001928E3 File Offset: 0x00190AE3
		public override bool IsExtensionMethod
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x06006D42 RID: 27970 RVA: 0x001928EA File Offset: 0x00190AEA
		public override bool HidesBaseMethodsByName
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06006D43 RID: 27971 RVA: 0x001928F1 File Offset: 0x00190AF1
		public override ImmutableArray<Location> Locations
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06006D44 RID: 27972 RVA: 0x001928F8 File Offset: 0x00190AF8
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x06006D45 RID: 27973 RVA: 0x001928FF File Offset: 0x00190AFF
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x06006D46 RID: 27974 RVA: 0x00192906 File Offset: 0x00190B06
		public override bool IsStatic
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06006D47 RID: 27975 RVA: 0x0019290D File Offset: 0x00190B0D
		public override bool IsAsync
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x06006D48 RID: 27976 RVA: 0x00192914 File Offset: 0x00190B14
		public override bool IsVirtual
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06006D49 RID: 27977 RVA: 0x0019291B File Offset: 0x00190B1B
		public override bool IsOverride
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x06006D4A RID: 27978 RVA: 0x00192922 File Offset: 0x00190B22
		public override bool IsAbstract
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06006D4B RID: 27979 RVA: 0x00192929 File Offset: 0x00190B29
		public override bool IsSealed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06006D4C RID: 27980 RVA: 0x00192930 File Offset: 0x00190B30
		public override bool IsExtern
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x06006D4D RID: 27981 RVA: 0x00192937 File Offset: 0x00190B37
		public override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06006D4E RID: 27982 RVA: 0x0019293E File Offset: 0x00190B3E
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x06006D4F RID: 27983 RVA: 0x00192945 File Offset: 0x00190B45
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06006D50 RID: 27984 RVA: 0x0019294C File Offset: 0x00190B4C
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x00192953 File Offset: 0x00190B53
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06006D52 RID: 27986 RVA: 0x0019295A File Offset: 0x00190B5A
		internal override bool IsMetadataFinal
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06006D53 RID: 27987 RVA: 0x00192961 File Offset: 0x00190B61
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x06006D54 RID: 27988 RVA: 0x00192964 File Offset: 0x00190B64
		internal override bool IsInitOnly
		{
			get
			{
				return this._isInitOnly;
			}
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x0019296C File Offset: 0x00190B6C
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001AF4 RID: 6900
		private readonly string _name;

		// Token: 0x04001AF5 RID: 6901
		private readonly TypeSymbol _containingType;

		// Token: 0x04001AF6 RID: 6902
		private readonly MethodKind _methodKind;

		// Token: 0x04001AF7 RID: 6903
		private readonly CallingConvention _callingConvention;

		// Token: 0x04001AF8 RID: 6904
		private readonly ImmutableArray<TypeParameterSymbol> _typeParameters;

		// Token: 0x04001AF9 RID: 6905
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001AFA RID: 6906
		private readonly RefKind _refKind;

		// Token: 0x04001AFB RID: 6907
		private readonly bool _isInitOnly;

		// Token: 0x04001AFC RID: 6908
		private readonly TypeWithAnnotations _returnType;

		// Token: 0x04001AFD RID: 6909
		private readonly ImmutableArray<CustomModifier> _refCustomModifiers;

		// Token: 0x04001AFE RID: 6910
		private readonly ImmutableArray<MethodSymbol> _explicitInterfaceImplementations;
	}
}
