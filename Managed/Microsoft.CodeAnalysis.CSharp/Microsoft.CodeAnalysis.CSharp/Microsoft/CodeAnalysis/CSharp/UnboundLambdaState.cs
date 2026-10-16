using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F9 RID: 249
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class UnboundLambdaState
	{
		// Token: 0x060012D6 RID: 4822 RVA: 0x00063D1F File Offset: 0x00061F1F
		public UnboundLambdaState(Binder binder, bool includeCache)
		{
			if (includeCache)
			{
				this._bindingCache = ImmutableDictionary<NamedTypeSymbol, BoundLambda>.Empty.WithComparers(SymbolEqualityComparer.ConsiderEverything);
				this._returnInferenceCache = ImmutableDictionary<UnboundLambdaState.ReturnInferenceCacheKey, BoundLambda>.Empty;
			}
			this.Binder = binder;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00063D51 File Offset: 0x00061F51
		public void SetUnboundLambda(UnboundLambda unbound)
		{
			this._unboundLambda = unbound;
		}

		// Token: 0x060012D8 RID: 4824
		protected abstract UnboundLambdaState WithCachingCore(bool includeCache);

		// Token: 0x060012D9 RID: 4825 RVA: 0x00063D5A File Offset: 0x00061F5A
		internal UnboundLambdaState WithCaching(bool includeCache)
		{
			if (this._bindingCache == null != includeCache)
			{
				return this;
			}
			return this.WithCachingCore(includeCache);
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00063D71 File Offset: 0x00061F71
		public UnboundLambda UnboundLambda
		{
			get
			{
				return this._unboundLambda;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060012DB RID: 4827
		public abstract MessageID MessageID { get; }

		// Token: 0x060012DC RID: 4828
		public abstract string ParameterName(int index);

		// Token: 0x060012DD RID: 4829
		public abstract bool ParameterIsDiscard(int index);

		// Token: 0x060012DE RID: 4830
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> ParameterAttributes(int index);

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060012DF RID: 4831
		public abstract bool HasSignature { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060012E0 RID: 4832
		public abstract bool HasExplicitlyTypedParameterList { get; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060012E1 RID: 4833
		public abstract int ParameterCount { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060012E2 RID: 4834
		public abstract bool IsAsync { get; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060012E3 RID: 4835
		public abstract bool HasNames { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060012E4 RID: 4836
		public abstract bool IsStatic { get; }

		// Token: 0x060012E5 RID: 4837
		public abstract Location ParameterLocation(int index);

		// Token: 0x060012E6 RID: 4838
		public abstract TypeWithAnnotations ParameterTypeWithAnnotations(int index);

		// Token: 0x060012E7 RID: 4839
		public abstract RefKind RefKind(int index);

		// Token: 0x060012E8 RID: 4840
		protected abstract BoundBlock BindLambdaBody(LambdaSymbol lambdaSymbol, Binder lambdaBodyBinder, BindingDiagnosticBag diagnostics);

		// Token: 0x060012E9 RID: 4841
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected abstract BoundExpression GetLambdaExpressionBody(BoundBlock body);

		// Token: 0x060012EA RID: 4842
		protected abstract BoundBlock CreateBlockFromLambdaExpressionBody(Binder lambdaBodyBinder, BoundExpression expression, BindingDiagnosticBag diagnostics);

		// Token: 0x060012EB RID: 4843 RVA: 0x00063D79 File Offset: 0x00061F79
		public virtual void GenerateAnonymousFunctionConversionError(BindingDiagnosticBag diagnostics, TypeSymbol targetType)
		{
			this.Binder.GenerateAnonymousFunctionConversionError(diagnostics, this._unboundLambda.Syntax, this._unboundLambda, targetType);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00063D9C File Offset: 0x00061F9C
		public BoundLambda Bind(NamedTypeSymbol delegateType)
		{
			BoundLambda boundLambda;
			if (!this._bindingCache.TryGetValue(delegateType, out boundLambda))
			{
				boundLambda = this.ReallyBind(delegateType);
				boundLambda = ImmutableInterlocked.GetOrAdd<NamedTypeSymbol, BoundLambda>(ref this._bindingCache, delegateType, boundLambda);
			}
			return boundLambda;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00063DD0 File Offset: 0x00061FD0
		internal IEnumerable<TypeSymbol> InferredReturnTypes()
		{
			bool any = false;
			foreach (BoundLambda boundLambda in this._returnInferenceCache.Values)
			{
				TypeWithAnnotations typeWithAnnotations = boundLambda.InferredReturnType.TypeWithAnnotations;
				if (typeWithAnnotations.HasType)
				{
					any = true;
					yield return typeWithAnnotations.Type;
				}
			}
			IEnumerator<BoundLambda> enumerator = null;
			if (!any)
			{
				TypeWithAnnotations typeWithAnnotations2 = this.BindForErrorRecovery().InferredReturnType.TypeWithAnnotations;
				if (typeWithAnnotations2.HasType)
				{
					yield return typeWithAnnotations2.Type;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00063DE0 File Offset: 0x00061FE0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static MethodSymbol DelegateInvokeMethod(NamedTypeSymbol delegateType)
		{
			NamedTypeSymbol delegateType2 = delegateType.GetDelegateType();
			if (delegateType2 == null)
			{
				return null;
			}
			return delegateType2.DelegateInvokeMethod;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00063DF4 File Offset: 0x00061FF4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static TypeWithAnnotations DelegateReturnTypeWithAnnotations(MethodSymbol invokeMethod, out RefKind refKind)
		{
			if (invokeMethod == null)
			{
				refKind = Microsoft.CodeAnalysis.RefKind.None;
				return default(TypeWithAnnotations);
			}
			refKind = invokeMethod.RefKind;
			return invokeMethod.ReturnTypeWithAnnotations;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00063E1F File Offset: 0x0006201F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool DelegateNeedsReturn(MethodSymbol invokeMethod)
		{
			return invokeMethod != null && !invokeMethod.ReturnsVoid && (!this.IsAsync || !invokeMethod.ReturnType.IsNonGenericTaskType(this.Binder.Compilation));
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00063E54 File Offset: 0x00062054
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal NamedTypeSymbol InferDelegateType([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			CSharpCompilation compilation = this.Binder.Compilation;
			ArrayBuilder<RefKind> instance = ArrayBuilder<Microsoft.CodeAnalysis.RefKind>.GetInstance(this.ParameterCount);
			ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance(this.ParameterCount);
			for (int i = 0; i < this.ParameterCount; i++)
			{
				instance.Add(this.HasExplicitlyTypedParameterList ? this.RefKind(i) : Microsoft.CodeAnalysis.RefKind.None);
				instance2.Add(this.HasExplicitlyTypedParameterList ? this.ParameterTypeWithAnnotations(i) : TypeWithAnnotations.Create(compilation.GetSpecialType(SpecialType.System_Object), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			ImmutableArray<RefKind> immutableArray = instance.ToImmutableAndFree();
			ImmutableArray<TypeWithAnnotations> immutableArray2 = instance2.ToImmutableAndFree();
			LambdaSymbol lambdaSymbol = new LambdaSymbol(this.Binder, compilation, this.Binder.ContainingMemberOrLambda, this._unboundLambda, immutableArray2, immutableArray, Microsoft.CodeAnalysis.RefKind.None, default(TypeWithAnnotations));
			ExecutableCodeBinder executableCodeBinder = new ExecutableCodeBinder(this._unboundLambda.Syntax, lambdaSymbol, this.ParameterBinder(lambdaSymbol, this.Binder), null);
			BoundBlock boundBlock = this.BindLambdaBody(lambdaSymbol, executableCodeBinder, BindingDiagnosticBag.Discarded);
			ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> instance3 = ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>>.GetInstance();
			BoundLambda.BlockReturns.GetReturnTypes(instance3, boundBlock);
			InferredLambdaReturnType inferredLambdaReturnType = BoundLambda.InferReturnType(instance3, this._unboundLambda, executableCodeBinder, null, this.IsAsync, this.Binder.Conversions);
			if (this.HasExplicitlyTypedParameterList && (inferredLambdaReturnType.TypeWithAnnotations.HasType || instance3.Count == 0))
			{
				return this.Binder.GetMethodGroupOrLambdaDelegateType(inferredLambdaReturnType.RefKind, inferredLambdaReturnType.TypeWithAnnotations, immutableArray, immutableArray2, ref useSiteInfo);
			}
			return null;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00063FC4 File Offset: 0x000621C4
		private BoundLambda ReallyBind(NamedTypeSymbol delegateType)
		{
			MethodSymbol methodSymbol = UnboundLambdaState.DelegateInvokeMethod(delegateType);
			RefKind refKind;
			TypeWithAnnotations typeWithAnnotations = UnboundLambdaState.DelegateReturnTypeWithAnnotations(methodSymbol, out refKind);
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, this._unboundLambda.WithDependencies);
			CSharpCompilation compilation = this.Binder.Compilation;
			UnboundLambdaState.ReturnInferenceCacheKey returnInferenceCacheKey = UnboundLambdaState.ReturnInferenceCacheKey.Create(delegateType, this.IsAsync);
			BoundLambda boundLambda;
			LambdaSymbol lambdaSymbol;
			Binder binder;
			BoundBlock boundBlock;
			if (refKind == Microsoft.CodeAnalysis.RefKind.None && this._returnInferenceCache.TryGetValue(returnInferenceCacheKey, out boundLambda))
			{
				BoundExpression lambdaExpressionBody = this.GetLambdaExpressionBody(boundLambda.Body);
				if (lambdaExpressionBody != null && (lambdaSymbol = boundLambda.Symbol).RefKind == refKind && LambdaSymbol.InferenceFailureReturnType != lambdaSymbol.ReturnType && lambdaSymbol.ReturnTypeWithAnnotations.Equals(typeWithAnnotations, TypeCompareKind.ConsiderEverything))
				{
					binder = boundLambda.Binder;
					boundBlock = this.CreateBlockFromLambdaExpressionBody(binder, lambdaExpressionBody, instance);
					instance.AddRange(boundLambda.Diagnostics, false);
					goto IL_010E;
				}
			}
			lambdaSymbol = this.CreateLambdaSymbol(this.Binder.ContainingMemberOrLambda, typeWithAnnotations, returnInferenceCacheKey.ParameterTypes, returnInferenceCacheKey.ParameterRefKinds, refKind);
			binder = new ExecutableCodeBinder(this._unboundLambda.Syntax, lambdaSymbol, this.ParameterBinder(lambdaSymbol, this.Binder), null);
			boundBlock = this.BindLambdaBody(lambdaSymbol, binder, instance);
			IL_010E:
			lambdaSymbol.GetDeclarationDiagnostics(instance);
			if (lambdaSymbol.RefKind == Microsoft.CodeAnalysis.RefKind.In)
			{
				compilation.EnsureIsReadOnlyAttributeExists(instance, lambdaSymbol.DiagnosticLocation, false);
			}
			ImmutableArray<ParameterSymbol> parameters = lambdaSymbol.Parameters;
			ParameterHelpers.EnsureIsReadOnlyAttributeExists(compilation, parameters, instance, false);
			if (typeWithAnnotations.HasType)
			{
				if (typeWithAnnotations.Type.ContainsNativeInteger())
				{
					compilation.EnsureNativeIntegerAttributeExists(instance, lambdaSymbol.DiagnosticLocation, false);
				}
				if (compilation.ShouldEmitNullableAttributes(lambdaSymbol) && typeWithAnnotations.NeedsNullableAttribute())
				{
					compilation.EnsureNullableAttributeExists(instance, lambdaSymbol.DiagnosticLocation, false);
				}
			}
			ParameterHelpers.EnsureNativeIntegerAttributeExists(compilation, parameters, instance, false);
			ParameterHelpers.EnsureNullableAttributeExists(compilation, lambdaSymbol, parameters, instance, false);
			this.ValidateUnsafeParameters(instance, returnInferenceCacheKey.ParameterTypes);
			if (ControlFlowPass.Analyze(compilation, lambdaSymbol, boundBlock, instance.DiagnosticBag))
			{
				if (this.DelegateNeedsReturn(methodSymbol))
				{
					instance.Add(ErrorCode.ERR_AnonymousReturnExpected, lambdaSymbol.DiagnosticLocation, new object[]
					{
						this.MessageID.Localize(),
						delegateType
					});
				}
				else
				{
					boundBlock = FlowAnalysisPass.AppendImplicitReturn(boundBlock, lambdaSymbol);
				}
			}
			if (this.IsAsync && !ErrorFacts.PreventsSuccessfulDelegateConversion(instance.DiagnosticBag) && typeWithAnnotations.HasType && !typeWithAnnotations.IsVoidType() && !typeWithAnnotations.Type.IsNonGenericTaskType(compilation) && !typeWithAnnotations.Type.IsGenericTaskType(compilation))
			{
				instance.Add(ErrorCode.ERR_CantConvAsyncAnonFuncReturns, lambdaSymbol.DiagnosticLocation, new object[]
				{
					lambdaSymbol.MessageID.Localize(),
					delegateType
				});
			}
			if (this.IsAsync)
			{
				lambdaSymbol.ReportAsyncParameterErrors(instance, lambdaSymbol.DiagnosticLocation);
			}
			return new BoundLambda(this._unboundLambda.Syntax, this._unboundLambda, boundBlock, instance.ToReadOnlyAndFree(), binder, delegateType, default(InferredLambdaReturnType))
			{
				WasCompilerGenerated = this._unboundLambda.WasCompilerGenerated
			};
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000642A9 File Offset: 0x000624A9
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal LambdaSymbol CreateLambdaSymbol([global::System.Runtime.CompilerServices.Nullable(1)] Symbol containingSymbol, TypeWithAnnotations returnType, ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds, RefKind refKind)
		{
			return new LambdaSymbol(this.Binder, this.Binder.Compilation, containingSymbol, this._unboundLambda, parameterTypes, parameterRefKinds, refKind, returnType);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000642D0 File Offset: 0x000624D0
		internal LambdaSymbol CreateLambdaSymbol(NamedTypeSymbol delegateType, Symbol containingSymbol)
		{
			RefKind refKind;
			TypeWithAnnotations typeWithAnnotations = UnboundLambdaState.DelegateReturnTypeWithAnnotations(UnboundLambdaState.DelegateInvokeMethod(delegateType), out refKind);
			UnboundLambdaState.ReturnInferenceCacheKey returnInferenceCacheKey = UnboundLambdaState.ReturnInferenceCacheKey.Create(delegateType, this.IsAsync);
			return this.CreateLambdaSymbol(containingSymbol, typeWithAnnotations, returnInferenceCacheKey.ParameterTypes, returnInferenceCacheKey.ParameterRefKinds, refKind);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00064310 File Offset: 0x00062510
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private void ValidateUnsafeParameters([global::System.Runtime.CompilerServices.Nullable(1)] BindingDiagnosticBag diagnostics, ImmutableArray<TypeWithAnnotations> targetParameterTypes)
		{
			if (this.HasSignature)
			{
				int num = Math.Min(targetParameterTypes.Length, this.ParameterCount);
				for (int i = 0; i < num; i++)
				{
					if (targetParameterTypes[i].Type.IsUnsafe())
					{
						this.Binder.ReportUnsafeIfNotAllowed(this.ParameterLocation(i), diagnostics);
					}
				}
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00064370 File Offset: 0x00062570
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private BoundLambda ReallyInferReturnType([global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol delegateType, ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds)
		{
			ValueTuple<LambdaSymbol, BoundBlock, ExecutableCodeBinder, BindingDiagnosticBag> valueTuple = this.BindWithParameterAndReturnType(parameterTypes, parameterRefKinds, default(TypeWithAnnotations), Microsoft.CodeAnalysis.RefKind.None);
			LambdaSymbol item = valueTuple.Item1;
			BoundBlock item2 = valueTuple.Item2;
			ExecutableCodeBinder item3 = valueTuple.Item3;
			BindingDiagnosticBag item4 = valueTuple.Item4;
			ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>> instance = ArrayBuilder<ValueTuple<BoundReturnStatement, TypeWithAnnotations>>.GetInstance();
			BoundLambda.BlockReturns.GetReturnTypes(instance, item2);
			InferredLambdaReturnType inferredLambdaReturnType = BoundLambda.InferReturnType(instance, this._unboundLambda, item3, delegateType, item.IsAsync, item3.Conversions);
			BoundLambda boundLambda = new BoundLambda(this._unboundLambda.Syntax, this._unboundLambda, item2, item4.ToReadOnlyAndFree(), item3, delegateType, inferredLambdaReturnType);
			boundLambda.WasCompilerGenerated = this._unboundLambda.WasCompilerGenerated;
			TypeWithAnnotations typeWithAnnotations = inferredLambdaReturnType.TypeWithAnnotations;
			if (!typeWithAnnotations.HasType)
			{
				typeWithAnnotations = ((delegateType == null && instance.Count == 0) ? TypeWithAnnotations.Create(this.Binder.Compilation.GetSpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)) : TypeWithAnnotations.Create(LambdaSymbol.InferenceFailureReturnType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			item.SetInferredReturnType(inferredLambdaReturnType.RefKind, typeWithAnnotations);
			instance.Free();
			return boundLambda;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0006447C File Offset: 0x0006267C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: TupleElementNames(new string[] { "lambdaSymbol", "block", "lambdaBodyBinder", "diagnostics" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1, 1 })]
		private ValueTuple<LambdaSymbol, BoundBlock, ExecutableCodeBinder, BindingDiagnosticBag> BindWithParameterAndReturnType(ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds, TypeWithAnnotations returnType, RefKind refKind)
		{
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, this._unboundLambda.WithDependencies);
			LambdaSymbol lambdaSymbol = this.CreateLambdaSymbol(this.Binder.ContainingMemberOrLambda, returnType, parameterTypes, parameterRefKinds, refKind);
			ExecutableCodeBinder executableCodeBinder = new ExecutableCodeBinder(this._unboundLambda.Syntax, lambdaSymbol, this.ParameterBinder(lambdaSymbol, this.Binder), null);
			BoundBlock boundBlock = this.BindLambdaBody(lambdaSymbol, executableCodeBinder, instance);
			return new ValueTuple<LambdaSymbol, BoundBlock, ExecutableCodeBinder, BindingDiagnosticBag>(lambdaSymbol, boundBlock, executableCodeBinder, instance);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000644E8 File Offset: 0x000626E8
		public BoundLambda BindForReturnTypeInference(NamedTypeSymbol delegateType)
		{
			UnboundLambdaState.ReturnInferenceCacheKey returnInferenceCacheKey = UnboundLambdaState.ReturnInferenceCacheKey.Create(delegateType, this.IsAsync);
			BoundLambda boundLambda;
			if (!this._returnInferenceCache.TryGetValue(returnInferenceCacheKey, out boundLambda))
			{
				boundLambda = this.ReallyInferReturnType(delegateType, returnInferenceCacheKey.ParameterTypes, returnInferenceCacheKey.ParameterRefKinds);
				boundLambda = ImmutableInterlocked.GetOrAdd<UnboundLambdaState.ReturnInferenceCacheKey, BoundLambda>(ref this._returnInferenceCache, returnInferenceCacheKey, boundLambda);
			}
			return boundLambda;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00064535 File Offset: 0x00062735
		public virtual Binder ParameterBinder(LambdaSymbol lambdaSymbol, Binder binder)
		{
			return new WithLambdaParametersBinder(lambdaSymbol, binder);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0006453E File Offset: 0x0006273E
		public BoundLambda BindForErrorRecovery()
		{
			if (this._errorBinding == null)
			{
				Interlocked.CompareExchange<BoundLambda>(ref this._errorBinding, this.ReallyBindForErrorRecovery(), null);
			}
			return this._errorBinding;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00064561 File Offset: 0x00062761
		private BoundLambda ReallyBindForErrorRecovery()
		{
			BoundLambda boundLambda;
			if ((boundLambda = UnboundLambdaState.GuessBestBoundLambda<NamedTypeSymbol>(this._bindingCache)) == null)
			{
				boundLambda = this.<ReallyBindForErrorRecovery>g__rebind|51_0(UnboundLambdaState.GuessBestBoundLambda<UnboundLambdaState.ReturnInferenceCacheKey>(this._returnInferenceCache)) ?? this.<ReallyBindForErrorRecovery>g__rebind|51_0(this.ReallyInferReturnType(null, ImmutableArray<TypeWithAnnotations>.Empty, ImmutableArray<Microsoft.CodeAnalysis.RefKind>.Empty));
			}
			return boundLambda;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000645A0 File Offset: 0x000627A0
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private BoundLambda ReallyBindForErrorRecovery([global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol delegateType, InferredLambdaReturnType inferredReturnType, ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds)
		{
			TypeWithAnnotations typeWithAnnotations = inferredReturnType.TypeWithAnnotations;
			RefKind refKind = inferredReturnType.RefKind;
			if (!typeWithAnnotations.HasType)
			{
				typeWithAnnotations = UnboundLambdaState.DelegateReturnTypeWithAnnotations(UnboundLambdaState.DelegateInvokeMethod(delegateType), out refKind);
				if (!typeWithAnnotations.HasType || typeWithAnnotations.Type.ContainsTypeParameter(null))
				{
					typeWithAnnotations = TypeWithAnnotations.Create((inferredReturnType.HadExpressionlessReturn || inferredReturnType.NumExpressions == 0) ? this.Binder.Compilation.GetSpecialType(SpecialType.System_Void) : this.Binder.CreateErrorType(""), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					refKind = Microsoft.CodeAnalysis.RefKind.None;
				}
			}
			ValueTuple<LambdaSymbol, BoundBlock, ExecutableCodeBinder, BindingDiagnosticBag> valueTuple = this.BindWithParameterAndReturnType(parameterTypes, parameterRefKinds, typeWithAnnotations, refKind);
			BoundBlock item = valueTuple.Item2;
			ExecutableCodeBinder item2 = valueTuple.Item3;
			BindingDiagnosticBag item3 = valueTuple.Item4;
			return new BoundLambda(this._unboundLambda.Syntax, this._unboundLambda, item, item3.ToReadOnlyAndFree(), item2, delegateType, new InferredLambdaReturnType(inferredReturnType.NumExpressions, inferredReturnType.HadExpressionlessReturn, refKind, typeWithAnnotations, ImmutableArray<DiagnosticInfo>.Empty, ImmutableArray<AssemblySymbol>.Empty))
			{
				WasCompilerGenerated = this._unboundLambda.WasCompilerGenerated
			};
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000646A0 File Offset: 0x000628A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static BoundLambda GuessBestBoundLambda<T>(ImmutableDictionary<T, BoundLambda> candidates)
		{
			int count = candidates.Count;
			if (count == 0)
			{
				return null;
			}
			if (count != 1)
			{
				return Enumerable.FirstOrDefault<KeyValuePair<T, BoundLambda>>(Enumerable.OrderBy<KeyValuePair<T, BoundLambda>, string>(Enumerable.First<IGrouping<int, KeyValuePair<T, BoundLambda>>>(Enumerable.OrderBy<IGrouping<int, KeyValuePair<T, BoundLambda>>, int>(Enumerable.GroupBy<KeyValuePair<T, BoundLambda>, int>(candidates, (KeyValuePair<T, BoundLambda> lambda) => lambda.Value.Diagnostics.Diagnostics.Length), (IGrouping<int, KeyValuePair<T, BoundLambda>> group) => group.Key)), (KeyValuePair<T, BoundLambda> lambda) => UnboundLambdaState.GetLambdaSortString(lambda.Value.Symbol))).Value;
			}
			return Enumerable.First<KeyValuePair<T, BoundLambda>>(candidates).Value;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00064750 File Offset: 0x00062950
		private static string GetLambdaSortString(LambdaSymbol lambda)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			foreach (ParameterSymbol parameterSymbol in lambda.Parameters)
			{
				instance.Builder.Append(parameterSymbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat));
			}
			if (lambda.ReturnTypeWithAnnotations.HasType)
			{
				instance.Builder.Append(lambda.ReturnTypeWithAnnotations.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000647D4 File Offset: 0x000629D4
		public bool GenerateSummaryErrors(BindingDiagnosticBag diagnostics)
		{
			IEnumerable<ImmutableBindingDiagnostic<AssemblySymbol>> enumerable = Enumerable.Select<KeyValuePair<NamedTypeSymbol, BoundLambda>, ImmutableBindingDiagnostic<AssemblySymbol>>(this._bindingCache, delegate(KeyValuePair<NamedTypeSymbol, BoundLambda> boundLambda)
			{
				KeyValuePair<NamedTypeSymbol, BoundLambda> keyValuePair = boundLambda;
				return keyValuePair.Value.Diagnostics;
			});
			IEnumerable<ImmutableBindingDiagnostic<AssemblySymbol>> enumerable2 = Enumerable.Select<BoundLambda, ImmutableBindingDiagnostic<AssemblySymbol>>(this._returnInferenceCache.Values, (BoundLambda boundLambda) => boundLambda.Diagnostics);
			IEnumerable<ImmutableBindingDiagnostic<AssemblySymbol>> enumerable3 = Enumerable.Concat<ImmutableBindingDiagnostic<AssemblySymbol>>(enumerable, enumerable2);
			FirstAmongEqualsSet<Diagnostic> firstAmongEqualsSet = null;
			foreach (ImmutableBindingDiagnostic<AssemblySymbol> immutableBindingDiagnostic in enumerable3)
			{
				if (firstAmongEqualsSet == null)
				{
					firstAmongEqualsSet = UnboundLambdaState.CreateFirstAmongEqualsSet(immutableBindingDiagnostic.Diagnostics);
				}
				else
				{
					firstAmongEqualsSet.IntersectWith(immutableBindingDiagnostic.Diagnostics);
				}
			}
			if (firstAmongEqualsSet != null && UnboundLambdaState.PreventsSuccessfulDelegateConversion(firstAmongEqualsSet))
			{
				diagnostics.AddRange(firstAmongEqualsSet);
				return true;
			}
			FirstAmongEqualsSet<Diagnostic> firstAmongEqualsSet2 = null;
			foreach (ImmutableBindingDiagnostic<AssemblySymbol> immutableBindingDiagnostic2 in enumerable3)
			{
				if (firstAmongEqualsSet2 == null)
				{
					firstAmongEqualsSet2 = UnboundLambdaState.CreateFirstAmongEqualsSet(immutableBindingDiagnostic2.Diagnostics);
				}
				else
				{
					firstAmongEqualsSet2.UnionWith(immutableBindingDiagnostic2.Diagnostics);
				}
			}
			if (firstAmongEqualsSet2 != null && UnboundLambdaState.PreventsSuccessfulDelegateConversion(firstAmongEqualsSet2))
			{
				diagnostics.AddRange(firstAmongEqualsSet2);
				return true;
			}
			return false;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00064928 File Offset: 0x00062B28
		private static bool PreventsSuccessfulDelegateConversion(FirstAmongEqualsSet<Diagnostic> set)
		{
			using (IEnumerator<Diagnostic> enumerator = set.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ErrorFacts.PreventsSuccessfulDelegateConversion((ErrorCode)enumerator.Current.Code))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0006497C File Offset: 0x00062B7C
		private static FirstAmongEqualsSet<Diagnostic> CreateFirstAmongEqualsSet([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> bag)
		{
			return new FirstAmongEqualsSet<Diagnostic>(bag, CommonDiagnosticComparer.Instance, new Func<Diagnostic, Diagnostic, int>(UnboundLambdaState.CanonicallyCompareDiagnostics));
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0006499C File Offset: 0x00062B9C
		private static int CanonicallyCompareDiagnostics(Diagnostic x, Diagnostic y)
		{
			if (x.Code != y.Code)
			{
				return x.Code - y.Code;
			}
			IReadOnlyList<object> arguments = x.Arguments;
			int num = ((arguments != null) ? arguments.Count : 0);
			IReadOnlyList<object> arguments2 = y.Arguments;
			int num2 = ((arguments2 != null) ? arguments2.Count : 0);
			int i = 0;
			int num3 = Math.Min(num, num2);
			while (i < num3)
			{
				object obj = x.Arguments[i];
				object obj2 = y.Arguments[i];
				int num4 = string.CompareOrdinal((obj != null) ? obj.ToString() : null, (obj2 != null) ? obj2.ToString() : null);
				if (num4 != 0)
				{
					return num4;
				}
				i++;
			}
			return num - num2;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00064A44 File Offset: 0x00062C44
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		[return: NotNullIfNotNull("lambda")]
		private BoundLambda <ReallyBindForErrorRecovery>g__rebind|51_0(BoundLambda lambda)
		{
			if (lambda == null)
			{
				return null;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)lambda.Type;
			UnboundLambdaState.ReturnInferenceCacheKey returnInferenceCacheKey = UnboundLambdaState.ReturnInferenceCacheKey.Create(namedTypeSymbol, this.IsAsync);
			return this.ReallyBindForErrorRecovery(namedTypeSymbol, lambda.InferredReturnType, returnInferenceCacheKey.ParameterTypes, returnInferenceCacheKey.ParameterRefKinds);
		}

		// Token: 0x040005D9 RID: 1497
		private UnboundLambda _unboundLambda;

		// Token: 0x040005DA RID: 1498
		internal readonly Binder Binder;

		// Token: 0x040005DB RID: 1499
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private ImmutableDictionary<NamedTypeSymbol, BoundLambda> _bindingCache;

		// Token: 0x040005DC RID: 1500
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private ImmutableDictionary<UnboundLambdaState.ReturnInferenceCacheKey, BoundLambda> _returnInferenceCache;

		// Token: 0x040005DD RID: 1501
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundLambda _errorBinding;

		// Token: 0x0200068D RID: 1677
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class ReturnInferenceCacheKey
		{
			// Token: 0x06008334 RID: 33588 RVA: 0x001DBAD1 File Offset: 0x001D9CD1
			private ReturnInferenceCacheKey(ImmutableArray<TypeWithAnnotations> parameterTypes, ImmutableArray<RefKind> parameterRefKinds, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol taskLikeReturnTypeOpt)
			{
				this.ParameterTypes = parameterTypes;
				this.ParameterRefKinds = parameterRefKinds;
				this.TaskLikeReturnTypeOpt = taskLikeReturnTypeOpt;
			}

			// Token: 0x06008335 RID: 33589 RVA: 0x001DBAF0 File Offset: 0x001D9CF0
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				UnboundLambdaState.ReturnInferenceCacheKey returnInferenceCacheKey = obj as UnboundLambdaState.ReturnInferenceCacheKey;
				if (returnInferenceCacheKey == null || returnInferenceCacheKey.ParameterTypes.Length != this.ParameterTypes.Length || !TypeSymbol.Equals(returnInferenceCacheKey.TaskLikeReturnTypeOpt, this.TaskLikeReturnTypeOpt, TypeCompareKind.ConsiderEverything))
				{
					return false;
				}
				for (int i = 0; i < this.ParameterTypes.Length; i++)
				{
					if (!returnInferenceCacheKey.ParameterTypes[i].Equals(this.ParameterTypes[i], TypeCompareKind.ConsiderEverything) || returnInferenceCacheKey.ParameterRefKinds[i] != this.ParameterRefKinds[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06008336 RID: 33590 RVA: 0x001DBBA8 File Offset: 0x001D9DA8
			public override int GetHashCode()
			{
				NamedTypeSymbol taskLikeReturnTypeOpt = this.TaskLikeReturnTypeOpt;
				int num = ((taskLikeReturnTypeOpt != null) ? taskLikeReturnTypeOpt.GetHashCode() : 0);
				foreach (TypeWithAnnotations typeWithAnnotations in this.ParameterTypes)
				{
					num = Hash.Combine<TypeSymbol>(typeWithAnnotations.Type, num);
				}
				return num;
			}

			// Token: 0x06008337 RID: 33591 RVA: 0x001DBBFC File Offset: 0x001D9DFC
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public static UnboundLambdaState.ReturnInferenceCacheKey Create([global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol delegateType, bool isAsync)
			{
				ImmutableArray<TypeWithAnnotations> immutableArray = ImmutableArray<TypeWithAnnotations>.Empty;
				ImmutableArray<RefKind> immutableArray2 = ImmutableArray<Microsoft.CodeAnalysis.RefKind>.Empty;
				NamedTypeSymbol namedTypeSymbol = null;
				MethodSymbol methodSymbol = UnboundLambdaState.DelegateInvokeMethod(delegateType);
				if (methodSymbol != null)
				{
					int parameterCount = methodSymbol.ParameterCount;
					if (parameterCount > 0)
					{
						ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(parameterCount);
						ArrayBuilder<RefKind> instance2 = ArrayBuilder<Microsoft.CodeAnalysis.RefKind>.GetInstance(parameterCount);
						foreach (ParameterSymbol parameterSymbol in methodSymbol.Parameters)
						{
							instance2.Add(parameterSymbol.RefKind);
							instance.Add(parameterSymbol.TypeWithAnnotations);
						}
						immutableArray = instance.ToImmutableAndFree();
						immutableArray2 = instance2.ToImmutableAndFree();
					}
					if (isAsync)
					{
						NamedTypeSymbol namedTypeSymbol2 = methodSymbol.ReturnType as NamedTypeSymbol;
						object obj;
						if (namedTypeSymbol2 != null && !namedTypeSymbol2.IsVoidType() && namedTypeSymbol2.IsCustomTaskType(out obj))
						{
							namedTypeSymbol = namedTypeSymbol2.ConstructedFrom;
						}
					}
				}
				if (immutableArray.IsEmpty && immutableArray2.IsEmpty && namedTypeSymbol == null)
				{
					return UnboundLambdaState.ReturnInferenceCacheKey.Empty;
				}
				return new UnboundLambdaState.ReturnInferenceCacheKey(immutableArray, immutableArray2, namedTypeSymbol);
			}

			// Token: 0x040020AA RID: 8362
			public readonly ImmutableArray<TypeWithAnnotations> ParameterTypes;

			// Token: 0x040020AB RID: 8363
			public readonly ImmutableArray<RefKind> ParameterRefKinds;

			// Token: 0x040020AC RID: 8364
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public readonly NamedTypeSymbol TaskLikeReturnTypeOpt;

			// Token: 0x040020AD RID: 8365
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public static readonly UnboundLambdaState.ReturnInferenceCacheKey Empty = new UnboundLambdaState.ReturnInferenceCacheKey(ImmutableArray<TypeWithAnnotations>.Empty, ImmutableArray<Microsoft.CodeAnalysis.RefKind>.Empty, null);
		}
	}
}
