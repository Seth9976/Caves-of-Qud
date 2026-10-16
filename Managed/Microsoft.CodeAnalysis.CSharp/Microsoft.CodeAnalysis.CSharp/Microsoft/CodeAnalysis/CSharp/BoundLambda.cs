using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A1 RID: 161
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLambda : BoundExpression, IBoundLambdaOrFunction
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00054B76 File Offset: 0x00052D76
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.Symbol;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00054B7E File Offset: 0x00052D7E
		public override object Display
		{
			get
			{
				return this.MessageID.Localize();
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00054B90 File Offset: 0x00052D90
		public MessageID MessageID
		{
			get
			{
				if (this.Syntax.Kind() != SyntaxKind.AnonymousMethodExpression)
				{
					return MessageID.IDS_Lambda;
				}
				return MessageID.IDS_AnonMethod;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00054BAF File Offset: 0x00052DAF
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x00054BB7 File Offset: 0x00052DB7
		internal InferredLambdaReturnType InferredReturnType { get; private set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00054BC0 File Offset: 0x00052DC0
		MethodSymbol IBoundLambdaOrFunction.Symbol
		{
			get
			{
				return this.Symbol;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00054BC8 File Offset: 0x00052DC8
		SyntaxNode IBoundLambdaOrFunction.Syntax
		{
			get
			{
				return this.Syntax;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00054BD0 File Offset: 0x00052DD0
		public BoundLambda(SyntaxNode syntax, UnboundLambda unboundLambda, BoundBlock body, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<AssemblySymbol> diagnostics, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol delegateType, InferredLambdaReturnType inferredReturnType)
			: this(syntax, unboundLambda.WithNoCache(), (LambdaSymbol)binder.ContainingMemberOrLambda, body, diagnostics, binder, delegateType, false)
		{
			this.InferredReturnType = inferredReturnType;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00054C06 File Offset: 0x00052E06
		public TypeWithAnnotations GetInferredReturnType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.GetInferredReturnType(null, null, ref useSiteInfo);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00054C14 File Offset: 0x00052E14
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public TypeWithAnnotations GetInferredReturnType(ConversionsBase conversions, NullableWalker.VariableState nullableState, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!this.InferredReturnType.UseSiteDiagnostics.IsEmpty)
			{
				useSiteInfo.AddDiagnostics(this.InferredReturnType.UseSiteDiagnostics);
			}
			if (!this.InferredReturnType.Dependencies.IsEmpty)
			{
				useSiteInfo.AddDependencies(this.InferredReturnType.Dependencies);
			}
			if (nullableState == null)
			{
				return this.InferredReturnType.TypeWithAnnotations;
			}
			ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> instance = ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>>.GetInstance();
			DiagnosticBag instance2 = DiagnosticBag.GetInstance();
			NamedTypeSymbol delegateType = this.Type.GetDelegateType();
			NullableWalker.Analyze(this.Binder.Compilation, this, (Conversions)conversions, instance2, (delegateType != null) ? delegateType.DelegateInvokeMethod : null, nullableState, instance);
			instance2.Free();
			ref InferredLambdaReturnType ptr = BoundLambda.InferReturnType(instance, this, this.Binder, delegateType, this.Symbol.IsAsync, conversions);
			instance.Free();
			return ptr.TypeWithAnnotations;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00054CE9 File Offset: 0x00052EE9
		internal LambdaSymbol CreateLambdaSymbol(NamedTypeSymbol delegateType, Symbol containingSymbol)
		{
			return this.UnboundLambda.Data.CreateLambdaSymbol(delegateType, containingSymbol);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00054CFD File Offset: 0x00052EFD
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal LambdaSymbol CreateLambdaSymbol([global::System.Runtime.CompilerServices.Nullable(1)] Symbol containingSymbol, TypeWithAnnotations returnType, ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds, RefKind refKind)
		{
			return this.UnboundLambda.Data.CreateLambdaSymbol(containingSymbol, returnType, parameterTypes, parameterRefKinds.IsDefault ? Enumerable.Repeat<RefKind>(RefKind.None, parameterTypes.Length).ToImmutableArray<RefKind>() : parameterRefKinds, refKind);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00054D33 File Offset: 0x00052F33
		internal static InferredLambdaReturnType InferReturnType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> returnTypes, BoundLambda node, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol delegateType, bool isAsync, ConversionsBase conversions)
		{
			return BoundLambda.InferReturnTypeImpl(returnTypes, node, binder, delegateType, isAsync, conversions, node.UnboundLambda.WithDependencies);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00054D4D File Offset: 0x00052F4D
		internal static InferredLambdaReturnType InferReturnType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> returnTypes, UnboundLambda node, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol delegateType, bool isAsync, ConversionsBase conversions)
		{
			return BoundLambda.InferReturnTypeImpl(returnTypes, node, binder, delegateType, isAsync, conversions, node.WithDependencies);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00054D64 File Offset: 0x00052F64
		private static InferredLambdaReturnType InferReturnTypeImpl([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> returnTypes, BoundNode node, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol delegateType, bool isAsync, ConversionsBase conversions, bool withDependencies)
		{
			ArrayBuilder<ValueTuple<BoundExpression, TypeWithAnnotations>> instance = ArrayBuilder<ValueTuple<BoundExpression, TypeWithAnnotations>>.GetInstance();
			bool flag = false;
			RefKind refKind = RefKind.None;
			foreach (ValueTuple<BoundReturnStatement, TypeWithAnnotations> valueTuple in returnTypes)
			{
				BoundReturnStatement item = valueTuple.Item1;
				TypeWithAnnotations item2 = valueTuple.Item2;
				RefKind refKind2 = item.RefKind;
				if (refKind2 != RefKind.None)
				{
					refKind = refKind2;
				}
				if (item2.Type == BoundLambda.NoReturnExpression)
				{
					flag = true;
				}
				else
				{
					instance.Add(new ValueTuple<BoundExpression, TypeWithAnnotations>(item.ExpressionOpt, item2));
				}
			}
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = (withDependencies ? new CompoundUseSiteInfo<AssemblySymbol>(binder.Compilation.Assembly) : CompoundUseSiteInfo<AssemblySymbol>.DiscardedDependencies);
			TypeWithAnnotations typeWithAnnotations = BoundLambda.CalculateReturnType(binder, conversions, delegateType, instance, isAsync, node, ref compoundUseSiteInfo);
			int count = instance.Count;
			instance.Free();
			return new InferredLambdaReturnType(count, flag, refKind, typeWithAnnotations, compoundUseSiteInfo.Diagnostics.AsImmutableOrEmpty<DiagnosticInfo>(), compoundUseSiteInfo.AccumulatesDependencies ? compoundUseSiteInfo.Dependencies.AsImmutableOrEmpty<AssemblySymbol>() : ImmutableArray<AssemblySymbol>.Empty);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00054E44 File Offset: 0x00053044
		private static TypeWithAnnotations CalculateReturnType(Binder binder, ConversionsBase conversions, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol delegateType, [TupleElementNames(new string[] { null, "resultType" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<BoundExpression, TypeWithAnnotations>> returns, bool isAsync, BoundNode node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			int count = returns.Count;
			TypeWithAnnotations typeWithAnnotations;
			if (count != 0)
			{
				if (count != 1)
				{
					if (conversions.IncludeNullability)
					{
						typeWithAnnotations = NullableWalker.BestTypeForLambdaReturns(returns, binder, node, (Conversions)conversions);
					}
					else
					{
						ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance(count);
						foreach (ValueTuple<BoundExpression, TypeWithAnnotations> valueTuple in returns)
						{
							TypeWithAnnotations item = valueTuple.Item2;
							instance.Add(item.Type);
						}
						TypeSymbol bestType = BestTypeInferrer.GetBestType(instance, conversions, ref useSiteInfo);
						typeWithAnnotations = ((bestType == null) ? default(TypeWithAnnotations) : TypeWithAnnotations.Create(bestType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
						instance.Free();
					}
				}
				else
				{
					typeWithAnnotations = returns[0].Item2;
				}
			}
			else
			{
				typeWithAnnotations = default(TypeWithAnnotations);
			}
			if (!isAsync)
			{
				return typeWithAnnotations;
			}
			NamedTypeSymbol namedTypeSymbol = null;
			object obj;
			if (delegateType == null)
			{
				obj = null;
			}
			else
			{
				NamedTypeSymbol delegateType2 = delegateType.GetDelegateType();
				if (delegateType2 == null)
				{
					obj = null;
				}
				else
				{
					MethodSymbol delegateInvokeMethod = delegateType2.DelegateInvokeMethod;
					obj = ((delegateInvokeMethod != null) ? delegateInvokeMethod.ReturnType : null);
				}
			}
			NamedTypeSymbol namedTypeSymbol2 = obj as NamedTypeSymbol;
			object obj2;
			if (namedTypeSymbol2 != null && !namedTypeSymbol2.IsVoidType() && namedTypeSymbol2.IsCustomTaskType(out obj2))
			{
				namedTypeSymbol = namedTypeSymbol2.ConstructedFrom;
			}
			if (count == 0)
			{
				return TypeWithAnnotations.Create((namedTypeSymbol != null && namedTypeSymbol.Arity == 0) ? namedTypeSymbol : binder.Compilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_Task), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}
			if (!typeWithAnnotations.HasType || typeWithAnnotations.IsVoidType())
			{
				return default(TypeWithAnnotations);
			}
			return TypeWithAnnotations.Create(((namedTypeSymbol != null && namedTypeSymbol.Arity == 1) ? namedTypeSymbol : binder.Compilation.GetWellKnownType(WellKnownType.System_Threading_Tasks_Task_T)).Construct(ImmutableArray.Create<TypeWithAnnotations>(typeWithAnnotations)), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00054FD8 File Offset: 0x000531D8
		public BoundLambda(SyntaxNode syntax, UnboundLambda unboundLambda, LambdaSymbol symbol, BoundBlock body, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<AssemblySymbol> diagnostics, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.Lambda, syntax, type, hasErrors || unboundLambda.HasErrors() || body.HasErrors())
		{
			this.UnboundLambda = unboundLambda;
			this.Symbol = symbol;
			this.Body = body;
			this.Diagnostics = diagnostics;
			this.Binder = binder;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0005502F File Offset: 0x0005322F
		public UnboundLambda UnboundLambda { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00055037 File Offset: 0x00053237
		public LambdaSymbol Symbol { get; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0005503F File Offset: 0x0005323F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00055047 File Offset: 0x00053247
		public BoundBlock Body { get; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0005504F File Offset: 0x0005324F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableBindingDiagnostic<AssemblySymbol> Diagnostics
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00055057 File Offset: 0x00053257
		public Binder Binder { get; }

		// Token: 0x06000C9F RID: 3231 RVA: 0x0005505F File Offset: 0x0005325F
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLambda(this);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00055068 File Offset: 0x00053268
		public BoundLambda Update(UnboundLambda unboundLambda, LambdaSymbol symbol, BoundBlock body, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<AssemblySymbol> diagnostics, Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (unboundLambda != this.UnboundLambda || !SymbolEqualityComparer.ConsiderEverything.Equals(symbol, this.Symbol) || body != this.Body || diagnostics != this.Diagnostics || binder != this.Binder || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundLambda boundLambda = new BoundLambda(this.Syntax, unboundLambda, symbol, body, diagnostics, binder, type, base.HasErrors);
				boundLambda.CopyAttributes(this);
				return boundLambda;
			}
			return this;
		}

		// Token: 0x040004FF RID: 1279
		internal static readonly TypeSymbol NoReturnExpression = new UnsupportedMetadataTypeSymbol(null);

		// Token: 0x02000685 RID: 1669
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class BlockReturns : BoundTreeWalker
		{
			// Token: 0x0600831C RID: 33564 RVA: 0x001DB8C2 File Offset: 0x001D9AC2
			private BlockReturns([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> builder)
			{
				this._builder = builder;
			}

			// Token: 0x0600831D RID: 33565 RVA: 0x001DB8D1 File Offset: 0x001D9AD1
			public static void GetReturnTypes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> builder, BoundBlock block)
			{
				new BoundLambda.BlockReturns(builder).Visit(block);
			}

			// Token: 0x0600831E RID: 33566 RVA: 0x001DB8E0 File Offset: 0x001D9AE0
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override BoundNode Visit(BoundNode node)
			{
				if (!(node is BoundExpression))
				{
					return base.Visit(node);
				}
				return null;
			}

			// Token: 0x0600831F RID: 33567 RVA: 0x001DB8F3 File Offset: 0x001D9AF3
			protected override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008320 RID: 33568 RVA: 0x001DB8FA File Offset: 0x001D9AFA
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
			{
				return null;
			}

			// Token: 0x06008321 RID: 33569 RVA: 0x001DB900 File Offset: 0x001D9B00
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			public override BoundNode VisitReturnStatement(BoundReturnStatement node)
			{
				BoundExpression expressionOpt = node.ExpressionOpt;
				TypeSymbol typeSymbol;
				if (expressionOpt != null)
				{
					TypeSymbol type = expressionOpt.Type;
					typeSymbol = ((type != null) ? type.SetUnknownNullabilityForReferenceTypes() : null);
				}
				else
				{
					typeSymbol = BoundLambda.NoReturnExpression;
				}
				TypeSymbol typeSymbol2 = typeSymbol;
				this._builder.Add(new ValueTuple<BoundReturnStatement, TypeWithAnnotations>(node, TypeWithAnnotations.Create(typeSymbol2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))));
				return null;
			}

			// Token: 0x04002090 RID: 8336
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })]
			private readonly ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> _builder;
		}
	}
}
