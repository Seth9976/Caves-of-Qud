using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000522 RID: 1314
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LambdaSymbol : SourceMethodSymbolWithAttributes
	{
		// Token: 0x06006E31 RID: 28209 RVA: 0x00194D04 File Offset: 0x00192F04
		public LambdaSymbol(Binder binder, CSharpCompilation compilation, Symbol containingSymbol, UnboundLambda unboundLambda, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> parameterTypes, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> parameterRefKinds, RefKind refKind, TypeWithAnnotations returnType)
			: base(unboundLambda.Syntax.GetReference())
		{
			this._binder = binder;
			this._containingSymbol = containingSymbol;
			this._messageID = unboundLambda.Data.MessageID;
			this._refKind = refKind;
			this._returnType = ((!returnType.HasType) ? TypeWithAnnotations.Create(LambdaSymbol.ReturnTypeIsBeingInferred, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)) : returnType);
			this._isSynthesized = unboundLambda.WasCompilerGenerated;
			this._isAsync = unboundLambda.IsAsync;
			this._isStatic = unboundLambda.IsStatic;
			this._parameters = this.MakeParameters(compilation, unboundLambda, parameterTypes, parameterRefKinds);
			this._declarationDiagnostics = new BindingDiagnosticBag();
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x06006E32 RID: 28210 RVA: 0x00194DB7 File Offset: 0x00192FB7
		public MessageID MessageID
		{
			get
			{
				return this._messageID;
			}
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x06006E33 RID: 28211 RVA: 0x00194DBF File Offset: 0x00192FBF
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.AnonymousFunction;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x06006E34 RID: 28212 RVA: 0x00194DC2 File Offset: 0x00192FC2
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x06006E35 RID: 28213 RVA: 0x00194DC5 File Offset: 0x00192FC5
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x06006E36 RID: 28214 RVA: 0x00194DC8 File Offset: 0x00192FC8
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06006E37 RID: 28215 RVA: 0x00194DCB File Offset: 0x00192FCB
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06006E38 RID: 28216 RVA: 0x00194DCE File Offset: 0x00192FCE
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06006E39 RID: 28217 RVA: 0x00194DD1 File Offset: 0x00192FD1
		public override bool IsStatic
		{
			get
			{
				return this._isStatic;
			}
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06006E3A RID: 28218 RVA: 0x00194DD9 File Offset: 0x00192FD9
		public override bool IsAsync
		{
			get
			{
				return this._isAsync;
			}
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x00194DE1 File Offset: 0x00192FE1
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06006E3C RID: 28220 RVA: 0x00194DE4 File Offset: 0x00192FE4
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x06006E3D RID: 28221 RVA: 0x00194DE7 File Offset: 0x00192FE7
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x06006E3E RID: 28222 RVA: 0x00194DEA File Offset: 0x00192FEA
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x06006E3F RID: 28223 RVA: 0x00194DED File Offset: 0x00192FED
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06006E40 RID: 28224 RVA: 0x00194DF0 File Offset: 0x00192FF0
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06006E41 RID: 28225 RVA: 0x00194DF4 File Offset: 0x00192FF4
		public override bool ReturnsVoid
		{
			get
			{
				return this.ReturnTypeWithAnnotations.HasType && base.ReturnType.IsVoidType();
			}
		}

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x06006E42 RID: 28226 RVA: 0x00194E1E File Offset: 0x0019301E
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x06006E43 RID: 28227 RVA: 0x00194E26 File Offset: 0x00193026
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x06006E44 RID: 28228 RVA: 0x00194E2E File Offset: 0x0019302E
		internal void SetInferredReturnType(RefKind refKind, TypeWithAnnotations inferredReturnType)
		{
			this._refKind = refKind;
			this._returnType = inferredReturnType;
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x06006E45 RID: 28229 RVA: 0x00194E3E File Offset: 0x0019303E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x06006E46 RID: 28230 RVA: 0x00194E45 File Offset: 0x00193045
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x06006E47 RID: 28231 RVA: 0x00194E48 File Offset: 0x00193048
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x06006E48 RID: 28232 RVA: 0x00194E4F File Offset: 0x0019304F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol AssociatedSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x06006E49 RID: 28233 RVA: 0x00194E52 File Offset: 0x00193052
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x06006E4A RID: 28234 RVA: 0x00194E59 File Offset: 0x00193059
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x06006E4B RID: 28235 RVA: 0x00194E60 File Offset: 0x00193060
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06006E4C RID: 28236 RVA: 0x00194E63 File Offset: 0x00193063
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06006E4D RID: 28237 RVA: 0x00194E6B File Offset: 0x0019306B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			thisParameter = null;
			return true;
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06006E4E RID: 28238 RVA: 0x00194E71 File Offset: 0x00193071
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Private;
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06006E4F RID: 28239 RVA: 0x00194E74 File Offset: 0x00193074
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray.Create<Location>(this.Syntax.Location);
			}
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x06006E50 RID: 28240 RVA: 0x00194E88 File Offset: 0x00193088
		internal Location DiagnosticLocation
		{
			get
			{
				SyntaxNode syntax = this.Syntax;
				AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax = syntax as AnonymousMethodExpressionSyntax;
				Location location;
				if (anonymousMethodExpressionSyntax == null)
				{
					LambdaExpressionSyntax lambdaExpressionSyntax = syntax as LambdaExpressionSyntax;
					if (lambdaExpressionSyntax == null)
					{
						location = this.Locations[0];
					}
					else
					{
						location = lambdaExpressionSyntax.ArrowToken.GetLocation();
					}
				}
				else
				{
					location = anonymousMethodExpressionSyntax.DelegateKeyword.GetLocation();
				}
				return location;
			}
		}

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x06006E51 RID: 28241 RVA: 0x00194EE8 File Offset: 0x001930E8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray.Create<SyntaxReference>(this.syntaxReferenceOpt);
			}
		}

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x06006E52 RID: 28242 RVA: 0x00194EF5 File Offset: 0x001930F5
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x06006E53 RID: 28243 RVA: 0x00194EFD File Offset: 0x001930FD
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x06006E54 RID: 28244 RVA: 0x00194F00 File Offset: 0x00193100
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06006E55 RID: 28245 RVA: 0x00194F04 File Offset: 0x00193104
		internal SyntaxNode Syntax
		{
			get
			{
				return this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
			}
		}

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06006E56 RID: 28246 RVA: 0x00194F25 File Offset: 0x00193125
		internal override Binder SignatureBinder
		{
			get
			{
				return this._binder;
			}
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x06006E57 RID: 28247 RVA: 0x00194F2D File Offset: 0x0019312D
		internal override Binder ParameterBinder
		{
			get
			{
				return new WithLambdaParametersBinder(this, this._binder);
			}
		}

		// Token: 0x06006E58 RID: 28248 RVA: 0x00194F3C File Offset: 0x0019313C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			LambdaExpressionSyntax lambdaExpressionSyntax = this.Syntax as LambdaExpressionSyntax;
			if (lambdaExpressionSyntax == null)
			{
				return default(OneOrMany<SyntaxList<AttributeListSyntax>>);
			}
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(lambdaExpressionSyntax.AttributeLists);
		}

		// Token: 0x06006E59 RID: 28249 RVA: 0x00194F70 File Offset: 0x00193170
		internal void GetDeclarationDiagnostics(BindingDiagnosticBag addTo)
		{
			foreach (ParameterSymbol parameterSymbol in this._parameters)
			{
				parameterSymbol.ForceComplete(null, default(CancellationToken));
			}
			this.GetAttributes();
			this.GetReturnTypeAttributes();
			addTo.AddRange(this._declarationDiagnostics, true);
		}

		// Token: 0x06006E5A RID: 28250 RVA: 0x00194FC8 File Offset: 0x001931C8
		internal override void AddDeclarationDiagnostics(BindingDiagnosticBag diagnostics)
		{
			this._declarationDiagnostics.AddRange(diagnostics, false);
		}

		// Token: 0x06006E5B RID: 28251 RVA: 0x00194FD8 File Offset: 0x001931D8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ParameterSymbol> MakeParameters([global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(1)] UnboundLambda unboundLambda, ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds)
		{
			if (!unboundLambda.HasSignature || unboundLambda.ParameterCount == 0)
			{
				return parameterTypes.SelectAsArray((TypeWithAnnotations type, int ordinal, [TupleElementNames(new string[] { "owner", "refKinds" })] ValueTuple<LambdaSymbol, ImmutableArray<RefKind>> arg) => SynthesizedParameterSymbol.Create(arg.Item1, type, ordinal, arg.Item2[ordinal], GeneratedNames.LambdaCopyParameterName(ordinal), default(ImmutableArray<CustomModifier>), null), new ValueTuple<LambdaSymbol, ImmutableArray<RefKind>>(this, parameterRefKinds));
			}
			ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance(unboundLambda.ParameterCount);
			bool hasExplicitlyTypedParameterList = unboundLambda.HasExplicitlyTypedParameterList;
			int length = parameterTypes.Length;
			for (int i = 0; i < unboundLambda.ParameterCount; i++)
			{
				TypeWithAnnotations typeWithAnnotations;
				RefKind refKind;
				if (hasExplicitlyTypedParameterList)
				{
					typeWithAnnotations = unboundLambda.ParameterTypeWithAnnotations(i);
					refKind = unboundLambda.RefKind(i);
				}
				else if (i < length)
				{
					typeWithAnnotations = parameterTypes[i];
					refKind = parameterRefKinds[i];
				}
				else
				{
					typeWithAnnotations = TypeWithAnnotations.Create(new ExtendedErrorTypeSymbol(compilation, string.Empty, 0, null, false, false), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					refKind = RefKind.None;
				}
				SyntaxList<AttributeListSyntax> syntaxList = unboundLambda.ParameterAttributes(i);
				string text = unboundLambda.ParameterName(i);
				Location location = unboundLambda.ParameterLocation(i);
				ImmutableArray<Location> immutableArray = ((location == null) ? ImmutableArray<Location>.Empty : ImmutableArray.Create<Location>(location));
				LambdaParameterSymbol lambdaParameterSymbol = new LambdaParameterSymbol(this, syntaxList, typeWithAnnotations, i, refKind, text, unboundLambda.ParameterIsDiscard(i), immutableArray);
				instance.Add(lambdaParameterSymbol);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006E5C RID: 28252 RVA: 0x00195108 File Offset: 0x00193308
		public sealed override bool Equals(Symbol symbol, TypeCompareKind compareKind)
		{
			if (this == symbol)
			{
				return true;
			}
			LambdaSymbol lambdaSymbol = symbol as LambdaSymbol;
			if (lambdaSymbol != null && LambdaSymbol.<Equals>g__areEqual|89_1(lambdaSymbol.syntaxReferenceOpt, this.syntaxReferenceOpt) && lambdaSymbol._refKind == this._refKind && TypeSymbol.Equals(lambdaSymbol.ReturnType, base.ReturnType, compareKind))
			{
				if (base.ParameterTypesWithAnnotations.SequenceEqual(lambdaSymbol.ParameterTypesWithAnnotations, compareKind, (TypeWithAnnotations p1, TypeWithAnnotations p2, TypeCompareKind compareKind) => p1.Equals(p2, compareKind)))
				{
					return lambdaSymbol.ContainingSymbol.Equals(this.ContainingSymbol, compareKind);
				}
			}
			return false;
		}

		// Token: 0x06006E5D RID: 28253 RVA: 0x001951A1 File Offset: 0x001933A1
		public override int GetHashCode()
		{
			return this.syntaxReferenceOpt.GetHashCode();
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x06006E5E RID: 28254 RVA: 0x001951AE File Offset: 0x001933AE
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._isSynthesized;
			}
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x06006E5F RID: 28255 RVA: 0x001951B6 File Offset: 0x001933B6
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x06006E60 RID: 28256 RVA: 0x001951B9 File Offset: 0x001933B9
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06006E61 RID: 28257 RVA: 0x001951BC File Offset: 0x001933BC
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006E62 RID: 28258 RVA: 0x001951BF File Offset: 0x001933BF
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06006E63 RID: 28259 RVA: 0x001951C6 File Offset: 0x001933C6
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x06006E64 RID: 28260 RVA: 0x001951CD File Offset: 0x001933CD
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006E65 RID: 28261 RVA: 0x001951D4 File Offset: 0x001933D4
		internal override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006E66 RID: 28262 RVA: 0x001951DB File Offset: 0x001933DB
		protected override void NoteAttributesComplete(bool forReturnType)
		{
		}

		// Token: 0x06006E68 RID: 28264 RVA: 0x001951F5 File Offset: 0x001933F5
		[CompilerGenerated]
		internal static bool <Equals>g__areEqual|89_1(SyntaxReference a, SyntaxReference b)
		{
			return a.SyntaxTree == b.SyntaxTree && a.Span == b.Span;
		}

		// Token: 0x04001B23 RID: 6947
		private readonly Binder _binder;

		// Token: 0x04001B24 RID: 6948
		private readonly Symbol _containingSymbol;

		// Token: 0x04001B25 RID: 6949
		private readonly MessageID _messageID;

		// Token: 0x04001B26 RID: 6950
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001B27 RID: 6951
		private RefKind _refKind;

		// Token: 0x04001B28 RID: 6952
		private TypeWithAnnotations _returnType;

		// Token: 0x04001B29 RID: 6953
		private readonly bool _isSynthesized;

		// Token: 0x04001B2A RID: 6954
		private readonly bool _isAsync;

		// Token: 0x04001B2B RID: 6955
		private readonly bool _isStatic;

		// Token: 0x04001B2C RID: 6956
		private readonly BindingDiagnosticBag _declarationDiagnostics;

		// Token: 0x04001B2D RID: 6957
		internal static readonly TypeSymbol ReturnTypeIsBeingInferred = new UnsupportedMetadataTypeSymbol(null);

		// Token: 0x04001B2E RID: 6958
		internal static readonly TypeSymbol InferenceFailureReturnType = new UnsupportedMetadataTypeSymbol(null);
	}
}
