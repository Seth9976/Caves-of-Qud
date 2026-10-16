using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000583 RID: 1411
	internal sealed class SynthesizedDelegateSymbol : SynthesizedContainer
	{
		// Token: 0x060075F7 RID: 30199 RVA: 0x001B6E57 File Offset: 0x001B5057
		public SynthesizedDelegateSymbol(NamespaceOrTypeSymbol containingSymbol, string name, TypeSymbol objectType, TypeSymbol intPtrType, TypeSymbol voidReturnTypeOpt, int parameterCount, BitVector byRefParameters)
			: base(name, parameterCount, voidReturnTypeOpt != null)
		{
			this._containingSymbol = containingSymbol;
			this._constructor = new SynthesizedDelegateSymbol.DelegateConstructor(this, objectType, intPtrType);
			this._invoke = new SynthesizedDelegateSymbol.InvokeMethod(this, byRefParameters, voidReturnTypeOpt);
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x060075F8 RID: 30200 RVA: 0x001B6E8D File Offset: 0x001B508D
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x060075F9 RID: 30201 RVA: 0x001B6E95 File Offset: 0x001B5095
		public override TypeKind TypeKind
		{
			get
			{
				return TypeKind.Delegate;
			}
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x060075FA RID: 30202 RVA: 0x001B6E98 File Offset: 0x001B5098
		internal override MethodSymbol Constructor
		{
			get
			{
				return this._constructor;
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x060075FB RID: 30203 RVA: 0x001B6EA0 File Offset: 0x001B50A0
		public override IEnumerable<string> MemberNames
		{
			get
			{
				return new string[]
				{
					this._constructor.Name,
					this._invoke.Name
				};
			}
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x001B6EC4 File Offset: 0x001B50C4
		public override ImmutableArray<Symbol> GetMembers()
		{
			return ImmutableArray.Create<Symbol>(this._constructor, this._invoke);
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x001B6ED8 File Offset: 0x001B50D8
		public override ImmutableArray<Symbol> GetMembers(string name)
		{
			if (name == this._constructor.Name)
			{
				return ImmutableArray.Create<Symbol>(this._constructor);
			}
			if (!(name == this._invoke.Name))
			{
				return ImmutableArray<Symbol>.Empty;
			}
			return ImmutableArray.Create<Symbol>(this._invoke);
		}

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x060075FE RID: 30206 RVA: 0x001B6F28 File Offset: 0x001B5128
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Internal;
			}
		}

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x060075FF RID: 30207 RVA: 0x001B6F2B File Offset: 0x001B512B
		public override bool IsSealed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x06007600 RID: 30208 RVA: 0x001B6F2E File Offset: 0x001B512E
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this.ContainingAssembly.GetSpecialType(SpecialType.System_MulticastDelegate);
			}
		}

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x06007601 RID: 30209 RVA: 0x001B6F3C File Offset: 0x001B513C
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x06007602 RID: 30210 RVA: 0x001B6F43 File Offset: 0x001B5143
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x06007603 RID: 30211 RVA: 0x001B6F46 File Offset: 0x001B5146
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x001B6F49 File Offset: 0x001B5149
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x04001CAE RID: 7342
		private readonly NamespaceOrTypeSymbol _containingSymbol;

		// Token: 0x04001CAF RID: 7343
		private readonly MethodSymbol _constructor;

		// Token: 0x04001CB0 RID: 7344
		private readonly MethodSymbol _invoke;

		// Token: 0x0200094A RID: 2378
		private sealed class DelegateConstructor : SynthesizedInstanceConstructor
		{
			// Token: 0x06009097 RID: 37015 RVA: 0x001FC208 File Offset: 0x001FA408
			public DelegateConstructor(NamedTypeSymbol containingType, TypeSymbol objectType, TypeSymbol intPtrType)
				: base(containingType)
			{
				this._parameters = ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(objectType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "object", default(ImmutableArray<CustomModifier>), null), SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(intPtrType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 1, RefKind.None, "method", default(ImmutableArray<CustomModifier>), null));
			}

			// Token: 0x170028B2 RID: 10418
			// (get) Token: 0x06009098 RID: 37016 RVA: 0x001FC275 File Offset: 0x001FA475
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x0400283E RID: 10302
			private readonly ImmutableArray<ParameterSymbol> _parameters;
		}

		// Token: 0x0200094B RID: 2379
		private sealed class InvokeMethod : SynthesizedInstanceMethodSymbol
		{
			// Token: 0x06009099 RID: 37017 RVA: 0x001FC280 File Offset: 0x001FA480
			internal InvokeMethod(SynthesizedDelegateSymbol containingType, BitVector byRefParameters, TypeSymbol voidReturnTypeOpt)
			{
				ImmutableArray<TypeParameterSymbol> typeParameters = containingType.TypeParameters;
				this._containingType = containingType;
				this._returnType = voidReturnTypeOpt ?? typeParameters.Last<TypeParameterSymbol>();
				ParameterSymbol[] array = new ParameterSymbol[typeParameters.Length - ((voidReturnTypeOpt != null) ? 0 : 1)];
				for (int i = 0; i < array.Length; i++)
				{
					RefKind refKind = ((!byRefParameters.IsNull && byRefParameters[i]) ? RefKind.Ref : RefKind.None);
					array[i] = SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(typeParameters[i], NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), i, refKind, "", default(ImmutableArray<CustomModifier>), null);
				}
				this._parameters = array.AsImmutableOrNull<ParameterSymbol>();
			}

			// Token: 0x170028B3 RID: 10419
			// (get) Token: 0x0600909A RID: 37018 RVA: 0x001FC32C File Offset: 0x001FA52C
			public override string Name
			{
				get
				{
					return "Invoke";
				}
			}

			// Token: 0x0600909B RID: 37019 RVA: 0x001FC333 File Offset: 0x001FA533
			internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
			{
				return true;
			}

			// Token: 0x0600909C RID: 37020 RVA: 0x001FC336 File Offset: 0x001FA536
			internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
			{
				return true;
			}

			// Token: 0x170028B4 RID: 10420
			// (get) Token: 0x0600909D RID: 37021 RVA: 0x001FC339 File Offset: 0x001FA539
			internal override bool IsMetadataFinal
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028B5 RID: 10421
			// (get) Token: 0x0600909E RID: 37022 RVA: 0x001FC33C File Offset: 0x001FA53C
			public override MethodKind MethodKind
			{
				get
				{
					return MethodKind.DelegateInvoke;
				}
			}

			// Token: 0x170028B6 RID: 10422
			// (get) Token: 0x0600909F RID: 37023 RVA: 0x001FC33F File Offset: 0x001FA53F
			public override int Arity
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170028B7 RID: 10423
			// (get) Token: 0x060090A0 RID: 37024 RVA: 0x001FC342 File Offset: 0x001FA542
			public override bool IsExtensionMethod
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028B8 RID: 10424
			// (get) Token: 0x060090A1 RID: 37025 RVA: 0x001FC345 File Offset: 0x001FA545
			internal override bool HasSpecialName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028B9 RID: 10425
			// (get) Token: 0x060090A2 RID: 37026 RVA: 0x001FC348 File Offset: 0x001FA548
			internal override MethodImplAttributes ImplementationAttributes
			{
				get
				{
					return 3;
				}
			}

			// Token: 0x170028BA RID: 10426
			// (get) Token: 0x060090A3 RID: 37027 RVA: 0x001FC34B File Offset: 0x001FA54B
			internal override bool HasDeclarativeSecurity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060090A4 RID: 37028 RVA: 0x001FC34E File Offset: 0x001FA54E
			public override DllImportData GetDllImportData()
			{
				return null;
			}

			// Token: 0x060090A5 RID: 37029 RVA: 0x001FC351 File Offset: 0x001FA551
			internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x170028BB RID: 10427
			// (get) Token: 0x060090A6 RID: 37030 RVA: 0x001FC358 File Offset: 0x001FA558
			internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170028BC RID: 10428
			// (get) Token: 0x060090A7 RID: 37031 RVA: 0x001FC35B File Offset: 0x001FA55B
			internal override bool RequiresSecurityObject
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028BD RID: 10429
			// (get) Token: 0x060090A8 RID: 37032 RVA: 0x001FC35E File Offset: 0x001FA55E
			public override bool HidesBaseMethodsByName
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028BE RID: 10430
			// (get) Token: 0x060090A9 RID: 37033 RVA: 0x001FC361 File Offset: 0x001FA561
			public override bool IsVararg
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028BF RID: 10431
			// (get) Token: 0x060090AA RID: 37034 RVA: 0x001FC364 File Offset: 0x001FA564
			public override bool ReturnsVoid
			{
				get
				{
					return this._returnType.IsVoidType();
				}
			}

			// Token: 0x170028C0 RID: 10432
			// (get) Token: 0x060090AB RID: 37035 RVA: 0x001FC371 File Offset: 0x001FA571
			public override bool IsAsync
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028C1 RID: 10433
			// (get) Token: 0x060090AC RID: 37036 RVA: 0x001FC374 File Offset: 0x001FA574
			public override RefKind RefKind
			{
				get
				{
					return RefKind.None;
				}
			}

			// Token: 0x170028C2 RID: 10434
			// (get) Token: 0x060090AD RID: 37037 RVA: 0x001FC378 File Offset: 0x001FA578
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return TypeWithAnnotations.Create(this._returnType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}

			// Token: 0x170028C3 RID: 10435
			// (get) Token: 0x060090AE RID: 37038 RVA: 0x001FC39A File Offset: 0x001FA59A
			public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
			{
				get
				{
					return FlowAnalysisAnnotations.None;
				}
			}

			// Token: 0x170028C4 RID: 10436
			// (get) Token: 0x060090AF RID: 37039 RVA: 0x001FC39D File Offset: 0x001FA59D
			public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
			{
				get
				{
					return ImmutableHashSet<string>.Empty;
				}
			}

			// Token: 0x170028C5 RID: 10437
			// (get) Token: 0x060090B0 RID: 37040 RVA: 0x001FC3A4 File Offset: 0x001FA5A4
			public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
			{
				get
				{
					return ImmutableArray<TypeWithAnnotations>.Empty;
				}
			}

			// Token: 0x170028C6 RID: 10438
			// (get) Token: 0x060090B1 RID: 37041 RVA: 0x001FC3AB File Offset: 0x001FA5AB
			public override ImmutableArray<TypeParameterSymbol> TypeParameters
			{
				get
				{
					return ImmutableArray<TypeParameterSymbol>.Empty;
				}
			}

			// Token: 0x170028C7 RID: 10439
			// (get) Token: 0x060090B2 RID: 37042 RVA: 0x001FC3B2 File Offset: 0x001FA5B2
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x170028C8 RID: 10440
			// (get) Token: 0x060090B3 RID: 37043 RVA: 0x001FC3BA File Offset: 0x001FA5BA
			public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
			{
				get
				{
					return ImmutableArray<MethodSymbol>.Empty;
				}
			}

			// Token: 0x170028C9 RID: 10441
			// (get) Token: 0x060090B4 RID: 37044 RVA: 0x001FC3C1 File Offset: 0x001FA5C1
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return ImmutableArray<CustomModifier>.Empty;
				}
			}

			// Token: 0x170028CA RID: 10442
			// (get) Token: 0x060090B5 RID: 37045 RVA: 0x001FC3C8 File Offset: 0x001FA5C8
			public override Symbol AssociatedSymbol
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060090B6 RID: 37046 RVA: 0x001FC3CB File Offset: 0x001FA5CB
			internal override ImmutableArray<string> GetAppliedConditionalSymbols()
			{
				return ImmutableArray<string>.Empty;
			}

			// Token: 0x170028CB RID: 10443
			// (get) Token: 0x060090B7 RID: 37047 RVA: 0x001FC3D2 File Offset: 0x001FA5D2
			internal override CallingConvention CallingConvention
			{
				get
				{
					return CallingConvention.HasThis;
				}
			}

			// Token: 0x170028CC RID: 10444
			// (get) Token: 0x060090B8 RID: 37048 RVA: 0x001FC3D6 File Offset: 0x001FA5D6
			internal override bool GenerateDebugInfo
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028CD RID: 10445
			// (get) Token: 0x060090B9 RID: 37049 RVA: 0x001FC3D9 File Offset: 0x001FA5D9
			public override Symbol ContainingSymbol
			{
				get
				{
					return this._containingType;
				}
			}

			// Token: 0x170028CE RID: 10446
			// (get) Token: 0x060090BA RID: 37050 RVA: 0x001FC3E1 File Offset: 0x001FA5E1
			public override ImmutableArray<Location> Locations
			{
				get
				{
					return ImmutableArray<Location>.Empty;
				}
			}

			// Token: 0x170028CF RID: 10447
			// (get) Token: 0x060090BB RID: 37051 RVA: 0x001FC3E8 File Offset: 0x001FA5E8
			public override Accessibility DeclaredAccessibility
			{
				get
				{
					return Accessibility.Public;
				}
			}

			// Token: 0x170028D0 RID: 10448
			// (get) Token: 0x060090BC RID: 37052 RVA: 0x001FC3EB File Offset: 0x001FA5EB
			public override bool IsStatic
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028D1 RID: 10449
			// (get) Token: 0x060090BD RID: 37053 RVA: 0x001FC3EE File Offset: 0x001FA5EE
			public override bool IsVirtual
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170028D2 RID: 10450
			// (get) Token: 0x060090BE RID: 37054 RVA: 0x001FC3F1 File Offset: 0x001FA5F1
			public override bool IsOverride
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028D3 RID: 10451
			// (get) Token: 0x060090BF RID: 37055 RVA: 0x001FC3F4 File Offset: 0x001FA5F4
			public override bool IsAbstract
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028D4 RID: 10452
			// (get) Token: 0x060090C0 RID: 37056 RVA: 0x001FC3F7 File Offset: 0x001FA5F7
			public override bool IsSealed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170028D5 RID: 10453
			// (get) Token: 0x060090C1 RID: 37057 RVA: 0x001FC3FA File Offset: 0x001FA5FA
			public override bool IsExtern
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400283F RID: 10303
			private readonly ImmutableArray<ParameterSymbol> _parameters;

			// Token: 0x04002840 RID: 10304
			private readonly TypeSymbol _containingType;

			// Token: 0x04002841 RID: 10305
			private readonly TypeSymbol _returnType;
		}
	}
}
