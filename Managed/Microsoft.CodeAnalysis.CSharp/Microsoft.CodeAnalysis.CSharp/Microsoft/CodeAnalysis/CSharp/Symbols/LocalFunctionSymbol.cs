using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000523 RID: 1315
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LocalFunctionSymbol : SourceMethodSymbolWithAttributes
	{
		// Token: 0x06006E69 RID: 28265 RVA: 0x00195218 File Offset: 0x00193418
		public LocalFunctionSymbol(Binder binder, Symbol containingSymbol, LocalFunctionStatementSyntax syntax)
			: base(syntax.GetReference())
		{
			this._containingSymbol = containingSymbol;
			this._declarationDiagnostics = new BindingDiagnosticBag();
			this._declarationModifiers = DeclarationModifiers.Private | syntax.Modifiers.ToDeclarationModifiers(this._declarationDiagnostics.DiagnosticBag);
			if (SyntaxFacts.HasYieldOperations(syntax.Body))
			{
				this._lazyIteratorElementType = TypeWithAnnotations.Boxed.Sentinel;
			}
			this.CheckUnsafeModifier(this._declarationModifiers, this._declarationDiagnostics);
			this.ScopeBinder = binder;
			binder = binder.WithUnsafeRegionIfNecessary(syntax.Modifiers);
			if (syntax.TypeParameterList != null)
			{
				binder = new WithMethodTypeParametersBinder(this, binder);
				this._typeParameters = this.MakeTypeParameters(this._declarationDiagnostics);
			}
			else
			{
				this._typeParameters = ImmutableArray<SourceMethodTypeParameterSymbol>.Empty;
				Symbol.ReportErrorIfHasConstraints(syntax.ConstraintClauses, this._declarationDiagnostics.DiagnosticBag);
			}
			if (this.IsExtensionMethod)
			{
				this._declarationDiagnostics.Add(ErrorCode.ERR_BadExtensionAgg, this.Locations[0]);
			}
			foreach (ParameterSyntax parameterSyntax in syntax.ParameterList.Parameters)
			{
				this.ReportAttributesDisallowed(parameterSyntax.AttributeLists, this._declarationDiagnostics);
			}
			if (syntax.ReturnType.Kind() == SyntaxKind.RefType)
			{
				if (((RefTypeSyntax)syntax.ReturnType).ReadOnlyKeyword.Kind() == SyntaxKind.ReadOnlyKeyword)
				{
					this._refKind = RefKind.In;
				}
				else
				{
					this._refKind = RefKind.Ref;
				}
			}
			else
			{
				this._refKind = RefKind.None;
			}
			this._binder = binder;
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06006E6A RID: 28266 RVA: 0x00195399 File Offset: 0x00193599
		internal Binder ScopeBinder { get; }

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06006E6B RID: 28267 RVA: 0x001953A1 File Offset: 0x001935A1
		internal override Binder ParameterBinder
		{
			get
			{
				return this._binder;
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x06006E6C RID: 28268 RVA: 0x001953AC File Offset: 0x001935AC
		internal LocalFunctionStatementSyntax Syntax
		{
			get
			{
				return (LocalFunctionStatementSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
			}
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x001953D4 File Offset: 0x001935D4
		internal void GetDeclarationDiagnostics(BindingDiagnosticBag addTo)
		{
			foreach (SourceMethodTypeParameterSymbol sourceMethodTypeParameterSymbol in this._typeParameters)
			{
				sourceMethodTypeParameterSymbol.ForceComplete(null, default(CancellationToken));
			}
			this.ComputeParameters();
			foreach (ParameterSymbol parameterSymbol in this._lazyParameters)
			{
				parameterSymbol.ForceComplete(null, default(CancellationToken));
			}
			this.ComputeReturnType();
			this.GetAttributes();
			this.GetReturnTypeAttributes();
			base.AsyncMethodChecks(this._declarationDiagnostics);
			addTo.AddRange(this._declarationDiagnostics, true);
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(false, addTo.AccumulatesDependencies);
			if (base.IsEntryPointCandidate && !this.IsGenericMethod && this.ContainingSymbol is SynthesizedSimpleProgramEntryPointSymbol && this.DeclaringCompilation.HasEntryPointSignature(this, instance).Item1)
			{
				addTo.Add(ErrorCode.WRN_MainIgnored, this.Syntax.Identifier.GetLocation(), new object[] { this });
			}
			addTo.AddRangeAndFree(instance);
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x001954E1 File Offset: 0x001936E1
		internal override void AddDeclarationDiagnostics(BindingDiagnosticBag diagnostics)
		{
			this._declarationDiagnostics.AddRange(diagnostics, false);
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x06006E6F RID: 28271 RVA: 0x001954F0 File Offset: 0x001936F0
		public override bool RequiresInstanceReceiver
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x06006E70 RID: 28272 RVA: 0x001954F3 File Offset: 0x001936F3
		public override bool IsVararg
		{
			get
			{
				this.ComputeParameters();
				return this._lazyIsVarArg;
			}
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x06006E71 RID: 28273 RVA: 0x00195501 File Offset: 0x00193701
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				this.ComputeParameters();
				return this._lazyParameters;
			}
		}

		// Token: 0x06006E72 RID: 28274 RVA: 0x00195510 File Offset: 0x00193710
		private void ComputeParameters()
		{
			if (new ImmutableArray<ParameterSymbol>?(this._lazyParameters) != default(ImmutableArray<ParameterSymbol>?))
			{
				return;
			}
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._declarationDiagnostics);
			SyntaxToken syntaxToken;
			ImmutableArray<ParameterSymbol> immutableArray = ParameterHelpers.MakeParameters(this._binder, this, this.Syntax.ParameterList, out syntaxToken, instance, true, true, false);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			ParameterHelpers.EnsureIsReadOnlyAttributeExists(declaringCompilation, immutableArray, instance, false);
			ParameterHelpers.EnsureNativeIntegerAttributeExists(declaringCompilation, immutableArray, instance, false);
			ParameterHelpers.EnsureNullableAttributeExists(declaringCompilation, this, immutableArray, instance, false);
			bool flag = syntaxToken.Kind() == SyntaxKind.ArgListKeyword;
			if (flag)
			{
				instance.Add(ErrorCode.ERR_IllegalVarArgs, syntaxToken.GetLocation());
			}
			BindingDiagnosticBag declarationDiagnostics = this._declarationDiagnostics;
			lock (declarationDiagnostics)
			{
				if (new ImmutableArray<ParameterSymbol>?(this._lazyParameters) != default(ImmutableArray<ParameterSymbol>?))
				{
					instance.Free();
				}
				else
				{
					this._declarationDiagnostics.AddRangeAndFree(instance);
					this._lazyIsVarArg = flag;
					this._lazyParameters = immutableArray;
				}
			}
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06006E73 RID: 28275 RVA: 0x0019561C File Offset: 0x0019381C
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				this.ComputeReturnType();
				return this._lazyReturnType.Value;
			}
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06006E74 RID: 28276 RVA: 0x0019562F File Offset: 0x0019382F
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x06006E75 RID: 28277 RVA: 0x00195638 File Offset: 0x00193838
		internal void ComputeReturnType()
		{
			if (this._lazyReturnType != null)
			{
				return;
			}
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._declarationDiagnostics);
			TypeSyntax returnType = this.Syntax.ReturnType;
			TypeWithAnnotations typeWithAnnotations = this._binder.BindType(returnType.SkipRef(), instance, null, false);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (declaringCompilation != null)
			{
				Location location = returnType.Location;
				if (this._refKind == RefKind.In)
				{
					declaringCompilation.EnsureIsReadOnlyAttributeExists(instance, location, false);
				}
				if (typeWithAnnotations.Type.ContainsNativeInteger())
				{
					declaringCompilation.EnsureNativeIntegerAttributeExists(instance, location, false);
				}
				if (declaringCompilation.ShouldEmitNullableAttributes(this) && typeWithAnnotations.NeedsNullableAttribute())
				{
					declaringCompilation.EnsureNullableAttributeExists(instance, location, false);
				}
			}
			if (typeWithAnnotations.IsRestrictedType(true))
			{
				instance.Add(ErrorCode.ERR_MethodReturnCantBeRefAny, returnType.Location, new object[] { typeWithAnnotations.Type });
			}
			BindingDiagnosticBag declarationDiagnostics = this._declarationDiagnostics;
			lock (declarationDiagnostics)
			{
				if (this._lazyReturnType != null)
				{
					instance.Free();
				}
				else
				{
					this._declarationDiagnostics.AddRangeAndFree(instance);
					Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyReturnType, new TypeWithAnnotations.Boxed(typeWithAnnotations), null);
				}
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06006E76 RID: 28278 RVA: 0x00195764 File Offset: 0x00193964
		public override bool ReturnsVoid
		{
			get
			{
				return base.ReturnType.IsVoidType();
			}
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x06006E77 RID: 28279 RVA: 0x00195774 File Offset: 0x00193974
		public override int Arity
		{
			get
			{
				return this.TypeParameters.Length;
			}
		}

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x06006E78 RID: 28280 RVA: 0x0019578F File Offset: 0x0019398F
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x06006E79 RID: 28281 RVA: 0x00195797 File Offset: 0x00193997
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._typeParameters.Cast<SourceMethodTypeParameterSymbol, TypeParameterSymbol>();
			}
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x06006E7A RID: 28282 RVA: 0x001957A4 File Offset: 0x001939A4
		public override bool IsExtensionMethod
		{
			get
			{
				ParameterSyntax parameterSyntax = this.Syntax.ParameterList.Parameters.FirstOrDefault();
				return parameterSyntax != null && !parameterSyntax.IsArgList && parameterSyntax.Modifiers.Any(SyntaxKind.ThisKeyword);
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x06006E7B RID: 28283 RVA: 0x001957E8 File Offset: 0x001939E8
		// (set) Token: 0x06006E7C RID: 28284 RVA: 0x0019580E File Offset: 0x00193A0E
		internal override TypeWithAnnotations IteratorElementTypeWithAnnotations
		{
			get
			{
				TypeWithAnnotations.Boxed lazyIteratorElementType = this._lazyIteratorElementType;
				if (lazyIteratorElementType == null)
				{
					return default(TypeWithAnnotations);
				}
				return lazyIteratorElementType.Value;
			}
			set
			{
				Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyIteratorElementType, new TypeWithAnnotations.Boxed(value), TypeWithAnnotations.Boxed.Sentinel);
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x06006E7D RID: 28285 RVA: 0x00195827 File Offset: 0x00193A27
		internal override bool IsIterator
		{
			get
			{
				return this._lazyIteratorElementType != null;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x06006E7E RID: 28286 RVA: 0x00195832 File Offset: 0x00193A32
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.LocalFunction;
			}
		}

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x06006E7F RID: 28287 RVA: 0x00195836 File Offset: 0x00193A36
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x06006E80 RID: 28288 RVA: 0x00195840 File Offset: 0x00193A40
		public override string Name
		{
			get
			{
				return this.Syntax.Identifier.ValueText ?? "";
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x06006E81 RID: 28289 RVA: 0x00195869 File Offset: 0x00193A69
		public SyntaxToken NameToken
		{
			get
			{
				return this.Syntax.Identifier;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x06006E82 RID: 28290 RVA: 0x00195876 File Offset: 0x00193A76
		internal override Binder SignatureBinder
		{
			get
			{
				return this._binder;
			}
		}

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x06006E83 RID: 28291 RVA: 0x0019587E File Offset: 0x00193A7E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x06006E84 RID: 28292 RVA: 0x00195888 File Offset: 0x00193A88
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray.Create<Location>(this.Syntax.Identifier.GetLocation());
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x06006E85 RID: 28293 RVA: 0x001958AD File Offset: 0x00193AAD
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x06006E86 RID: 28294 RVA: 0x001958B0 File Offset: 0x00193AB0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x06006E87 RID: 28295 RVA: 0x001958B7 File Offset: 0x00193AB7
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x001958BA File Offset: 0x00193ABA
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.Syntax.AttributeLists);
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x001958CC File Offset: 0x00193ACC
		protected override void NoteAttributesComplete(bool forReturnType)
		{
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x06006E8A RID: 28298 RVA: 0x001958CE File Offset: 0x00193ACE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol AssociatedSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06006E8B RID: 28299 RVA: 0x001958D1 File Offset: 0x00193AD1
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this._declarationModifiers);
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06006E8C RID: 28300 RVA: 0x001958DE File Offset: 0x00193ADE
		public override bool IsAsync
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Async) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x06006E8D RID: 28301 RVA: 0x001958EF File Offset: 0x00193AEF
		public override bool IsStatic
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Static) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x06006E8E RID: 28302 RVA: 0x001958FC File Offset: 0x00193AFC
		public override bool IsVirtual
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Virtual) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x06006E8F RID: 28303 RVA: 0x0019590D File Offset: 0x00193B0D
		public override bool IsOverride
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Override) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x06006E90 RID: 28304 RVA: 0x0019591E File Offset: 0x00193B1E
		public override bool IsAbstract
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Abstract) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x06006E91 RID: 28305 RVA: 0x0019592B File Offset: 0x00193B2B
		public override bool IsSealed
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Sealed) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x06006E92 RID: 28306 RVA: 0x00195938 File Offset: 0x00193B38
		public override bool IsExtern
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Extern) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D55 RID: 7509
		// (get) Token: 0x06006E93 RID: 28307 RVA: 0x00195949 File Offset: 0x00193B49
		public bool IsUnsafe
		{
			get
			{
				return (this._declarationModifiers & DeclarationModifiers.Unsafe) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x06006E94 RID: 28308 RVA: 0x0019595C File Offset: 0x00193B5C
		internal bool IsExpressionBodied
		{
			get
			{
				LocalFunctionStatementSyntax syntax = this.Syntax;
				return syntax != null && syntax.Body == null && syntax.ExpressionBody != null;
			}
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x06006E95 RID: 28309 RVA: 0x00195986 File Offset: 0x00193B86
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x06006E96 RID: 28310 RVA: 0x00195989 File Offset: 0x00193B89
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006E97 RID: 28311 RVA: 0x0019598C File Offset: 0x00193B8C
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06006E98 RID: 28312 RVA: 0x0019598F File Offset: 0x00193B8F
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06006E99 RID: 28313 RVA: 0x00195992 File Offset: 0x00193B92
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006E9A RID: 28314 RVA: 0x00195999 File Offset: 0x00193B99
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			thisParameter = null;
			return true;
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x001959A0 File Offset: 0x00193BA0
		private void ReportAttributesDisallowed([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributes, BindingDiagnosticBag diagnostics)
		{
			CSDiagnosticInfo featureAvailabilityDiagnosticInfo = MessageID.IDS_FeatureLocalFunctionAttributes.GetFeatureAvailabilityDiagnosticInfo((CSharpParseOptions)this.syntaxReferenceOpt.SyntaxTree.Options);
			if (featureAvailabilityDiagnosticInfo != null)
			{
				foreach (AttributeListSyntax attributeListSyntax in attributes)
				{
					diagnostics.Add(featureAvailabilityDiagnosticInfo, attributeListSyntax.Location);
				}
			}
		}

		// Token: 0x06006E9C RID: 28316 RVA: 0x001959F8 File Offset: 0x00193BF8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SourceMethodTypeParameterSymbol> MakeTypeParameters(BindingDiagnosticBag diagnostics)
		{
			ArrayBuilder<SourceMethodTypeParameterSymbol> instance = ArrayBuilder<SourceMethodTypeParameterSymbol>.GetInstance();
			TypeParameterListSyntax typeParameterList = this.Syntax.TypeParameterList;
			SeparatedSyntaxList<TypeParameterSyntax> separatedSyntaxList = ((typeParameterList != null) ? typeParameterList.Parameters : default(SeparatedSyntaxList<TypeParameterSyntax>));
			for (int i = 0; i < separatedSyntaxList.Count; i++)
			{
				TypeParameterSyntax typeParameterSyntax = separatedSyntaxList[i];
				if (typeParameterSyntax.VarianceKeyword.Kind() != SyntaxKind.None)
				{
					diagnostics.Add(ErrorCode.ERR_IllegalVarianceSyntax, typeParameterSyntax.VarianceKeyword.GetLocation());
				}
				this.ReportAttributesDisallowed(typeParameterSyntax.AttributeLists, diagnostics);
				SyntaxToken identifier = typeParameterSyntax.Identifier;
				Location location = identifier.GetLocation();
				string text = identifier.ValueText ?? "";
				foreach (SourceMethodTypeParameterSymbol sourceMethodTypeParameterSymbol in instance)
				{
					if (text == sourceMethodTypeParameterSymbol.Name)
					{
						diagnostics.Add(ErrorCode.ERR_DuplicateTypeParameter, location, new object[] { text });
						break;
					}
				}
				SourceMemberContainerTypeSymbol.ReportTypeNamedRecord(identifier.Text, this.DeclaringCompilation, diagnostics.DiagnosticBag, location);
				TypeParameterSymbol typeParameterSymbol = this.ContainingSymbol.FindEnclosingTypeParameter(text);
				if (typeParameterSymbol != null)
				{
					ErrorCode errorCode;
					if (typeParameterSymbol.ContainingSymbol.Kind == SymbolKind.Method)
					{
						errorCode = ErrorCode.WRN_TypeParameterSameAsOuterMethodTypeParameter;
					}
					else
					{
						errorCode = ErrorCode.WRN_TypeParameterSameAsOuterTypeParameter;
					}
					diagnostics.Add(errorCode, location, new object[] { text, typeParameterSymbol.ContainingSymbol });
				}
				SourceMethodTypeParameterSymbol sourceMethodTypeParameterSymbol2 = new SourceMethodTypeParameterSymbol(this, text, i, ImmutableArray.Create<Location>(location), ImmutableArray.Create<SyntaxReference>(typeParameterSyntax.GetReference()));
				instance.Add(sourceMethodTypeParameterSymbol2);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006E9D RID: 28317 RVA: 0x00195B8C File Offset: 0x00193D8C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			if (this._lazyTypeParameterConstraintTypes.IsDefault)
			{
				this.GetTypeParameterConstraintKinds();
				LocalFunctionStatementSyntax syntax = this.Syntax;
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(this._declarationDiagnostics);
				ImmutableArray<ImmutableArray<TypeWithAnnotations>> immutableArray = this.MakeTypeParameterConstraintTypes(this._binder, this.TypeParameters, syntax.TypeParameterList, syntax.ConstraintClauses, instance);
				BindingDiagnosticBag declarationDiagnostics = this._declarationDiagnostics;
				lock (declarationDiagnostics)
				{
					if (this._lazyTypeParameterConstraintTypes.IsDefault)
					{
						this._declarationDiagnostics.AddRange(instance, false);
						this._lazyTypeParameterConstraintTypes = immutableArray;
					}
				}
				instance.Free();
			}
			return this._lazyTypeParameterConstraintTypes;
		}

		// Token: 0x06006E9E RID: 28318 RVA: 0x00195C40 File Offset: 0x00193E40
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			if (this._lazyTypeParameterConstraintKinds.IsDefault)
			{
				LocalFunctionStatementSyntax syntax = this.Syntax;
				ImmutableArray<TypeParameterConstraintKind> immutableArray = this.MakeTypeParameterConstraintKinds(this._binder, this.TypeParameters, syntax.TypeParameterList, syntax.ConstraintClauses);
				ImmutableInterlocked.InterlockedInitialize<TypeParameterConstraintKind>(ref this._lazyTypeParameterConstraintKinds, immutableArray);
			}
			return this._lazyTypeParameterConstraintKinds;
		}

		// Token: 0x06006E9F RID: 28319 RVA: 0x00195C93 File Offset: 0x00193E93
		internal override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x00195C9A File Offset: 0x00193E9A
		public override int GetHashCode()
		{
			return this.Syntax.GetHashCode();
		}

		// Token: 0x06006EA1 RID: 28321 RVA: 0x00195CA7 File Offset: 0x00193EA7
		public sealed override bool Equals(Symbol symbol, TypeCompareKind compareKind)
		{
			if (this == symbol)
			{
				return true;
			}
			LocalFunctionSymbol localFunctionSymbol = symbol as LocalFunctionSymbol;
			return ((localFunctionSymbol != null) ? localFunctionSymbol.Syntax : null) == this.Syntax;
		}

		// Token: 0x04001B2F RID: 6959
		private readonly Binder _binder;

		// Token: 0x04001B30 RID: 6960
		private readonly Symbol _containingSymbol;

		// Token: 0x04001B31 RID: 6961
		private readonly DeclarationModifiers _declarationModifiers;

		// Token: 0x04001B32 RID: 6962
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<SourceMethodTypeParameterSymbol> _typeParameters;

		// Token: 0x04001B33 RID: 6963
		private readonly RefKind _refKind;

		// Token: 0x04001B34 RID: 6964
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001B35 RID: 6965
		private bool _lazyIsVarArg;

		// Token: 0x04001B36 RID: 6966
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private ImmutableArray<ImmutableArray<TypeWithAnnotations>> _lazyTypeParameterConstraintTypes;

		// Token: 0x04001B37 RID: 6967
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private ImmutableArray<TypeParameterConstraintKind> _lazyTypeParameterConstraintKinds;

		// Token: 0x04001B38 RID: 6968
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeWithAnnotations.Boxed _lazyReturnType;

		// Token: 0x04001B39 RID: 6969
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeWithAnnotations.Boxed _lazyIteratorElementType;

		// Token: 0x04001B3A RID: 6970
		private readonly BindingDiagnosticBag _declarationDiagnostics;
	}
}
