using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004BA RID: 1210
	internal abstract class SynthesizedMethodBaseSymbol : SourceMemberMethodSymbol
	{
		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x06006788 RID: 26504 RVA: 0x001860EC File Offset: 0x001842EC
		// (set) Token: 0x06006789 RID: 26505 RVA: 0x001860F4 File Offset: 0x001842F4
		internal TypeMap TypeMap { get; private set; }

		// Token: 0x0600678A RID: 26506 RVA: 0x001860FD File Offset: 0x001842FD
		protected SynthesizedMethodBaseSymbol(NamedTypeSymbol containingType, MethodSymbol baseMethod, SyntaxReference syntaxReference, Location location, string name, DeclarationModifiers declarationModifiers)
			: base(containingType, syntaxReference, location, false)
		{
			this.BaseMethod = baseMethod;
			this._name = name;
			base.MakeFlags(MethodKind.Ordinary, declarationModifiers, baseMethod.ReturnsVoid, false, false, false);
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x0018612C File Offset: 0x0018432C
		protected void AssignTypeMapAndTypeParameters(TypeMap typeMap, ImmutableArray<TypeParameterSymbol> typeParameters)
		{
			this.TypeMap = typeMap;
			this._typeParameters = typeParameters;
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x0018613C File Offset: 0x0018433C
		protected override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x00186140 File Offset: 0x00184340
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			if (this.ContainingType.IsImplicitlyDeclared || this.ContainingType is SimpleProgramNamedTypeSymbol)
			{
				return;
			}
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x0600678E RID: 26510 RVA: 0x00186198 File Offset: 0x00184398
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return this._typeParameters;
			}
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x001861A0 File Offset: 0x001843A0
		public sealed override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x001861A7 File Offset: 0x001843A7
		public sealed override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x06006791 RID: 26513 RVA: 0x001861B0 File Offset: 0x001843B0
		internal override int ParameterCount
		{
			get
			{
				return this.Parameters.Length;
			}
		}

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06006792 RID: 26514 RVA: 0x001861CC File Offset: 0x001843CC
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				if (this._parameters.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<ParameterSymbol>(ref this._parameters, this.MakeParameters(), default(ImmutableArray<ParameterSymbol>));
				}
				return this._parameters;
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06006793 RID: 26515 RVA: 0x00186208 File Offset: 0x00184408
		protected virtual ImmutableArray<TypeSymbol> ExtraSynthesizedRefParameters
		{
			get
			{
				return default(ImmutableArray<TypeSymbol>);
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06006794 RID: 26516 RVA: 0x0018621E File Offset: 0x0018441E
		protected virtual ImmutableArray<ParameterSymbol> BaseMethodParameters
		{
			get
			{
				return this.BaseMethod.Parameters;
			}
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x0018622C File Offset: 0x0018442C
		private ImmutableArray<ParameterSymbol> MakeParameters()
		{
			int num = 0;
			ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance();
			ImmutableArray<ParameterSymbol> baseMethodParameters = this.BaseMethodParameters;
			bool inheritsBaseMethodAttributes = this.InheritsBaseMethodAttributes;
			foreach (ParameterSymbol parameterSymbol in baseMethodParameters)
			{
				instance.Add(SynthesizedParameterSymbol.Create(this, this.TypeMap.SubstituteType(parameterSymbol.OriginalDefinition.TypeWithAnnotations), num++, parameterSymbol.RefKind, parameterSymbol.Name, default(ImmutableArray<CustomModifier>), inheritsBaseMethodAttributes ? (parameterSymbol as SourceComplexParameterSymbol) : null));
			}
			ImmutableArray<TypeSymbol> extraSynthesizedRefParameters = this.ExtraSynthesizedRefParameters;
			if (!extraSynthesizedRefParameters.IsDefaultOrEmpty)
			{
				foreach (TypeSymbol typeSymbol in extraSynthesizedRefParameters)
				{
					instance.Add(SynthesizedParameterSymbol.Create(this, this.TypeMap.SubstituteType(typeSymbol), num++, RefKind.Ref, "", default(ImmutableArray<CustomModifier>), null));
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06006796 RID: 26518 RVA: 0x0018631C File Offset: 0x0018451C
		internal virtual bool InheritsBaseMethodAttributes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x0018631F File Offset: 0x0018451F
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (!this.InheritsBaseMethodAttributes)
			{
				return ImmutableArray<CSharpAttributeData>.Empty;
			}
			return this.BaseMethod.GetAttributes();
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x0018633A File Offset: 0x0018453A
		public sealed override ImmutableArray<CSharpAttributeData> GetReturnTypeAttributes()
		{
			if (!this.InheritsBaseMethodAttributes)
			{
				return ImmutableArray<CSharpAttributeData>.Empty;
			}
			return this.BaseMethod.GetReturnTypeAttributes();
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x00186355 File Offset: 0x00184555
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public sealed override DllImportData GetDllImportData()
		{
			if (!this.InheritsBaseMethodAttributes)
			{
				return null;
			}
			return this.BaseMethod.GetDllImportData();
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x0600679A RID: 26522 RVA: 0x0018636C File Offset: 0x0018456C
		internal sealed override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				if (!this.InheritsBaseMethodAttributes)
				{
					return 0;
				}
				return this.BaseMethod.ImplementationAttributes;
			}
		}

		// Token: 0x170019F6 RID: 6646
		// (get) Token: 0x0600679B RID: 26523 RVA: 0x00186383 File Offset: 0x00184583
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (!this.InheritsBaseMethodAttributes)
				{
					return null;
				}
				return this.BaseMethod.ReturnValueMarshallingInformation;
			}
		}

		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x0600679C RID: 26524 RVA: 0x0018639A File Offset: 0x0018459A
		internal sealed override bool HasSpecialName
		{
			get
			{
				return this.InheritsBaseMethodAttributes && this.BaseMethod.HasSpecialName;
			}
		}

		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x0600679D RID: 26525 RVA: 0x001863B4 File Offset: 0x001845B4
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				SourceMethodSymbol sourceMethodSymbol = this.BaseMethod as SourceMethodSymbol;
				return sourceMethodSymbol == null || sourceMethodSymbol.AreLocalsZeroed;
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x0600679E RID: 26526 RVA: 0x001863D8 File Offset: 0x001845D8
		internal sealed override bool RequiresSecurityObject
		{
			get
			{
				return this.InheritsBaseMethodAttributes && this.BaseMethod.RequiresSecurityObject;
			}
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x0600679F RID: 26527 RVA: 0x001863EF File Offset: 0x001845EF
		internal sealed override bool HasDeclarativeSecurity
		{
			get
			{
				return this.InheritsBaseMethodAttributes && this.BaseMethod.HasDeclarativeSecurity;
			}
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x00186406 File Offset: 0x00184606
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			if (!this.InheritsBaseMethodAttributes)
			{
				return SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
			}
			return this.BaseMethod.GetSecurityInformation();
		}

		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x060067A1 RID: 26529 RVA: 0x00186421 File Offset: 0x00184621
		public sealed override RefKind RefKind
		{
			get
			{
				return this.BaseMethod.RefKind;
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x060067A2 RID: 26530 RVA: 0x0018642E File Offset: 0x0018462E
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this.TypeMap.SubstituteType(this.BaseMethod.OriginalDefinition.ReturnTypeWithAnnotations);
			}
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x060067A3 RID: 26531 RVA: 0x0018644B File Offset: 0x0018464B
		public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return this.BaseMethod.ReturnTypeFlowAnalysisAnnotations;
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x00186458 File Offset: 0x00184658
		public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return this.BaseMethod.ReturnNotNullIfParameterNotNull;
			}
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x060067A5 RID: 26533 RVA: 0x00186465 File Offset: 0x00184665
		public sealed override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x060067A6 RID: 26534 RVA: 0x00186468 File Offset: 0x00184668
		public sealed override bool IsVararg
		{
			get
			{
				return this.BaseMethod.IsVararg;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x060067A7 RID: 26535 RVA: 0x00186475 File Offset: 0x00184675
		public sealed override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x060067A8 RID: 26536 RVA: 0x0018647D File Offset: 0x0018467D
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x060067A9 RID: 26537 RVA: 0x00186480 File Offset: 0x00184680
		internal override bool IsExpressionBodied
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x060067AA RID: 26538 RVA: 0x00186484 File Offset: 0x00184684
		// (set) Token: 0x060067AB RID: 26539 RVA: 0x001864D4 File Offset: 0x001846D4
		internal override TypeWithAnnotations IteratorElementTypeWithAnnotations
		{
			get
			{
				if (this._iteratorElementType == null)
				{
					Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._iteratorElementType, new TypeWithAnnotations.Boxed(this.TypeMap.SubstituteType(this.BaseMethod.IteratorElementTypeWithAnnotations.Type)), null);
				}
				return this._iteratorElementType.Value;
			}
			set
			{
				Interlocked.Exchange<TypeWithAnnotations.Boxed>(ref this._iteratorElementType, new TypeWithAnnotations.Boxed(value));
			}
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x060067AC RID: 26540 RVA: 0x001864E8 File Offset: 0x001846E8
		internal override bool IsIterator
		{
			get
			{
				return this.BaseMethod.IsIterator;
			}
		}

		// Token: 0x040019B7 RID: 6583
		protected readonly MethodSymbol BaseMethod;

		// Token: 0x040019B9 RID: 6585
		private readonly string _name;

		// Token: 0x040019BA RID: 6586
		private ImmutableArray<TypeParameterSymbol> _typeParameters;

		// Token: 0x040019BB RID: 6587
		private ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x040019BC RID: 6588
		private TypeWithAnnotations.Boxed _iteratorElementType;
	}
}
