using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001EE RID: 494
	internal class AsyncRewriter : StateMachineRewriter
	{
		// Token: 0x060021A9 RID: 8617 RVA: 0x000AFEB9 File Offset: 0x000AE0B9
		private AsyncRewriter(BoundStatement body, MethodSymbol method, int methodOrdinal, AsyncStateMachine stateMachineType, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			: base(body, method, stateMachineType, slotAllocatorOpt, compilationState, diagnostics)
		{
			this._constructedSuccessfully = AsyncMethodBuilderMemberCollection.TryCreate(this.F, method, this.stateMachineType.TypeMap, out this._asyncMethodBuilderMemberCollection);
			this._methodOrdinal = methodOrdinal;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000AFEF8 File Offset: 0x000AE0F8
		internal static BoundStatement Rewrite(BoundStatement bodyWithAwaitLifted, MethodSymbol method, int methodOrdinal, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics, out AsyncStateMachine stateMachineType)
		{
			if (!method.IsAsync)
			{
				stateMachineType = null;
				return bodyWithAwaitLifted;
			}
			CSharpCompilation declaringCompilation = method.DeclaringCompilation;
			bool flag = method.IsAsyncReturningIAsyncEnumerable(declaringCompilation) || method.IsAsyncReturningIAsyncEnumerator(declaringCompilation);
			if (flag && !method.IsIterator)
			{
				diagnostics.Add(AsyncRewriter.AwaitDetector.ContainsAwait(bodyWithAwaitLifted) ? ErrorCode.ERR_PossibleAsyncIteratorWithoutYield : ErrorCode.ERR_PossibleAsyncIteratorWithoutYieldOrAwait, method.Locations[0], new object[] { method.ReturnTypeWithAnnotations });
				stateMachineType = null;
				return bodyWithAwaitLifted;
			}
			TypeKind typeKind = ((compilationState.Compilation.Options.EnableEditAndContinue || method.IsIterator) ? TypeKind.Class : TypeKind.Struct);
			stateMachineType = new AsyncStateMachine(slotAllocatorOpt, compilationState, method, methodOrdinal, typeKind);
			compilationState.ModuleBuilderOpt.CompilationState.SetStateMachineType(method, stateMachineType);
			AsyncRewriter asyncRewriter = (flag ? new AsyncRewriter.AsyncIteratorRewriter(bodyWithAwaitLifted, method, methodOrdinal, stateMachineType, slotAllocatorOpt, compilationState, diagnostics) : new AsyncRewriter(bodyWithAwaitLifted, method, methodOrdinal, stateMachineType, slotAllocatorOpt, compilationState, diagnostics));
			if (!asyncRewriter.VerifyPresenceOfRequiredAPIs())
			{
				return bodyWithAwaitLifted;
			}
			BoundStatement boundStatement;
			try
			{
				boundStatement = asyncRewriter.Rewrite();
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				boundStatement = new BoundBadStatement(bodyWithAwaitLifted.Syntax, ImmutableArray.Create<BoundNode>(bodyWithAwaitLifted), true);
			}
			return boundStatement;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000B003C File Offset: 0x000AE23C
		protected bool VerifyPresenceOfRequiredAPIs()
		{
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, this.diagnostics.AccumulatesDependencies);
			this.VerifyPresenceOfRequiredAPIs(instance);
			bool flag = instance.HasAnyErrors();
			if (!flag)
			{
				this.diagnostics.AddDependencies(instance, false);
			}
			else
			{
				this.diagnostics.AddRange(instance, false);
			}
			instance.Free();
			return !flag && this._constructedSuccessfully;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000B0097 File Offset: 0x000AE297
		protected virtual void VerifyPresenceOfRequiredAPIs(BindingDiagnosticBag bag)
		{
			this.EnsureWellKnownMember(WellKnownMember.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext, bag);
			this.EnsureWellKnownMember(WellKnownMember.System_Runtime_CompilerServices_IAsyncStateMachine_SetStateMachine, bag);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000B00B3 File Offset: 0x000AE2B3
		private Symbol EnsureWellKnownMember(WellKnownMember member, BindingDiagnosticBag bag)
		{
			return Binder.GetWellKnownTypeMember(this.F.Compilation, member, bag, this.body.Syntax.Location, null, false);
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000B00D9 File Offset: 0x000AE2D9
		protected override bool PreserveInitialParameterValuesAndThreadId
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000B00DC File Offset: 0x000AE2DC
		protected override void GenerateControlFields()
		{
			this.stateField = this.F.StateMachineField(this.F.SpecialType(SpecialType.System_Int32), GeneratedNames.MakeStateMachineStateFieldName(), true, false);
			this._builderField = this.F.StateMachineField(this._asyncMethodBuilderMemberCollection.BuilderType, GeneratedNames.AsyncBuilderFieldName(), true, false);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000B0134 File Offset: 0x000AE334
		protected override void GenerateMethodImplementations()
		{
			MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Runtime_CompilerServices_IAsyncStateMachine_MoveNext);
			MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Runtime_CompilerServices_IAsyncStateMachine_SetStateMachine);
			SynthesizedImplementationMethod synthesizedImplementationMethod = base.OpenMoveNextMethodImplementation(methodSymbol);
			this.GenerateMoveNext(synthesizedImplementationMethod);
			base.OpenMethodImplementation(methodSymbol2, "SetStateMachine", false);
			if (this.F.CurrentType.TypeKind == TypeKind.Class)
			{
				this.F.CloseMethod(this.F.Return(null));
			}
			else
			{
				this.F.CloseMethod(this.F.Block(new BoundStatement[]
				{
					this.F.ExpressionStatement(this.F.Call(this.F.Field(this.F.This(), this._builderField), this._asyncMethodBuilderMemberCollection.SetStateMachine, new BoundExpression[] { this.F.Parameter(this.F.CurrentFunction.Parameters[0]) })),
					this.F.Return(null)
				}));
			}
			this.GenerateConstructor();
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000B0250 File Offset: 0x000AE450
		protected virtual void GenerateConstructor()
		{
			if (this.stateMachineType.TypeKind == TypeKind.Class)
			{
				this.F.CurrentFunction = this.stateMachineType.Constructor;
				this.F.CloseMethod(this.F.Block(ImmutableArray.Create<BoundStatement>(this.F.BaseInitialization(), this.F.Return(null))));
			}
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000B02B4 File Offset: 0x000AE4B4
		protected override void InitializeStateMachine(ArrayBuilder<BoundStatement> bodyBuilder, NamedTypeSymbol frameType, LocalSymbol stateMachineLocal)
		{
			if (frameType.TypeKind == TypeKind.Class)
			{
				bodyBuilder.Add(this.F.Assignment(this.F.Local(stateMachineLocal), this.F.New(frameType.InstanceConstructors[0], Array.Empty<BoundExpression>()), false));
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000B0308 File Offset: 0x000AE508
		protected override BoundStatement GenerateStateMachineCreation(LocalSymbol stateMachineVariable, NamedTypeSymbol frameType, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> proxies)
		{
			AsyncMethodBuilderMemberCollection asyncMethodBuilderMemberCollection;
			if (!AsyncMethodBuilderMemberCollection.TryCreate(this.F, this.method, null, out asyncMethodBuilderMemberCollection))
			{
				return new BoundBadStatement(this.F.Syntax, ImmutableArray<BoundNode>.Empty, true);
			}
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(this.F.Assignment(this.F.Field(this.F.Local(stateMachineVariable), this._builderField.AsMember(frameType)), this.F.StaticCall(null, asyncMethodBuilderMemberCollection.CreateBuilder, Array.Empty<BoundExpression>()), false));
			instance.Add(base.GenerateParameterStorage(stateMachineVariable, proxies));
			instance.Add(this.F.Assignment(this.F.Field(this.F.Local(stateMachineVariable), this.stateField.AsMember(frameType)), this.F.Literal(-1), false));
			MethodSymbol methodSymbol = asyncMethodBuilderMemberCollection.Start.Construct(new TypeSymbol[] { frameType });
			if (asyncMethodBuilderMemberCollection.CheckGenericMethodConstraints)
			{
				MethodSymbol methodSymbol2 = methodSymbol;
				ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(this.F.Compilation, this.F.Compilation.Conversions, false, this.F.Syntax.Location, this.diagnostics);
				methodSymbol2.CheckConstraints(in checkConstraintsArgs);
			}
			instance.Add(this.F.ExpressionStatement(this.F.Call(this.F.Field(this.F.Local(stateMachineVariable), this._builderField.AsMember(frameType)), methodSymbol, ImmutableArray.Create<BoundExpression>(this.F.Local(stateMachineVariable)))));
			instance.Add(this.method.IsAsyncReturningVoid() ? this.F.Return(null) : this.F.Return(this.F.Property(this.F.Field(this.F.Local(stateMachineVariable), this._builderField.AsMember(frameType)), asyncMethodBuilderMemberCollection.Task)));
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000B0504 File Offset: 0x000AE704
		protected virtual void GenerateMoveNext(SynthesizedImplementationMethod moveNextMethod)
		{
			new AsyncMethodToStateMachineRewriter(this.method, this._methodOrdinal, this._asyncMethodBuilderMemberCollection, this.F, this.stateField, this._builderField, this.hoistedVariables, this.nonReusableLocalProxies, this.synthesizedLocalOrdinals, this.slotAllocatorOpt, this.nextFreeHoistedLocalSlot, this.diagnostics).GenerateMoveNext(this.body, moveNextMethod);
		}

		// Token: 0x0400105E RID: 4190
		private readonly AsyncMethodBuilderMemberCollection _asyncMethodBuilderMemberCollection;

		// Token: 0x0400105F RID: 4191
		private readonly bool _constructedSuccessfully;

		// Token: 0x04001060 RID: 4192
		private readonly int _methodOrdinal;

		// Token: 0x04001061 RID: 4193
		private FieldSymbol _builderField;

		// Token: 0x02000717 RID: 1815
		private sealed class AsyncIteratorRewriter : AsyncRewriter
		{
			// Token: 0x06008590 RID: 34192 RVA: 0x001E2D37 File Offset: 0x001E0F37
			internal AsyncIteratorRewriter(BoundStatement body, MethodSymbol method, int methodOrdinal, AsyncStateMachine stateMachineType, VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
				: base(body, method, methodOrdinal, stateMachineType, slotAllocatorOpt, compilationState, diagnostics)
			{
				this._isEnumerable = method.IsAsyncReturningIAsyncEnumerable(method.DeclaringCompilation);
			}

			// Token: 0x06008591 RID: 34193 RVA: 0x001E2D5C File Offset: 0x001E0F5C
			protected override void VerifyPresenceOfRequiredAPIs(BindingDiagnosticBag bag)
			{
				base.VerifyPresenceOfRequiredAPIs(bag);
				if (this._isEnumerable)
				{
					base.EnsureWellKnownMember(WellKnownMember.System_Collections_Generic_IAsyncEnumerable_T__GetAsyncEnumerator, bag);
					base.EnsureWellKnownMember(WellKnownMember.System_Threading_CancellationToken__Equals, bag);
					base.EnsureWellKnownMember(WellKnownMember.System_Threading_CancellationTokenSource__CreateLinkedTokenSource, bag);
					base.EnsureWellKnownMember(WellKnownMember.System_Threading_CancellationTokenSource__Token, bag);
					base.EnsureWellKnownMember(WellKnownMember.System_Threading_CancellationTokenSource__Dispose, bag);
				}
				base.EnsureWellKnownMember(WellKnownMember.System_Collections_Generic_IAsyncEnumerator_T__MoveNextAsync, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Collections_Generic_IAsyncEnumerator_T__get_Current, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_IAsyncDisposable__DisposeAsync, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_ValueTask_T__ctorSourceAndToken, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_ValueTask_T__ctorValue, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_ValueTask__ctor, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetResult, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetStatus, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__get_Version, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__OnCompleted, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__Reset, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__SetException, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__SetResult, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource_T__GetResult, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource_T__GetStatus, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource_T__OnCompleted, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource__GetResult, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource__GetStatus, bag);
				base.EnsureWellKnownMember(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource__OnCompleted, bag);
			}

			// Token: 0x06008592 RID: 34194 RVA: 0x001E2EB0 File Offset: 0x001E10B0
			protected override void GenerateMethodImplementations()
			{
				base.GenerateMethodImplementations();
				if (this._isEnumerable)
				{
					this.GenerateIAsyncEnumerableImplementation_GetAsyncEnumerator();
				}
				this.GenerateIAsyncEnumeratorImplementation_MoveNextAsync();
				this.GenerateIAsyncEnumeratorImplementation_Current();
				this.GenerateIValueTaskSourceBoolImplementation_GetResult();
				this.GenerateIValueTaskSourceBoolImplementation_GetStatus();
				this.GenerateIValueTaskSourceBoolImplementation_OnCompleted();
				this.GenerateIValueTaskSourceImplementation_GetResult();
				this.GenerateIValueTaskSourceImplementation_GetStatus();
				this.GenerateIValueTaskSourceImplementation_OnCompleted();
				this.GenerateIAsyncDisposable_DisposeAsync();
			}

			// Token: 0x170026C5 RID: 9925
			// (get) Token: 0x06008593 RID: 34195 RVA: 0x001E2F07 File Offset: 0x001E1107
			protected override bool PreserveInitialParameterValuesAndThreadId
			{
				get
				{
					return this._isEnumerable;
				}
			}

			// Token: 0x06008594 RID: 34196 RVA: 0x001E2F10 File Offset: 0x001E1110
			protected override void GenerateControlFields()
			{
				base.GenerateControlFields();
				NamedTypeSymbol namedTypeSymbol = this.F.SpecialType(SpecialType.System_Boolean);
				this._promiseOfValueOrEndField = this.F.StateMachineField(this.F.WellKnownType(WellKnownType.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T).Construct(new TypeSymbol[] { namedTypeSymbol }), GeneratedNames.MakeAsyncIteratorPromiseOfValueOrEndFieldName(), true, false);
				TypeSymbol iteratorElementType = ((AsyncStateMachine)this.stateMachineType).IteratorElementType;
				this._currentField = this.F.StateMachineField(iteratorElementType, GeneratedNames.MakeIteratorCurrentFieldName(), false, false);
				this._disposeModeField = this.F.StateMachineField(namedTypeSymbol, GeneratedNames.MakeDisposeModeFieldName(), false, false);
				if (this._isEnumerable)
				{
					if (this.method.Parameters.Any<ParameterSymbol>((ParameterSymbol p) => p.IsSourceParameterWithEnumeratorCancellationAttribute()))
					{
						this._combinedTokensField = this.F.StateMachineField(this.F.WellKnownType(WellKnownType.System_Threading_CancellationTokenSource), GeneratedNames.MakeAsyncIteratorCombinedTokensFieldName(), false, false);
					}
				}
			}

			// Token: 0x06008595 RID: 34197 RVA: 0x001E300C File Offset: 0x001E120C
			protected override void GenerateConstructor()
			{
				this.F.CurrentFunction = this.stateMachineType.Constructor;
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
				instance.Add(this.F.BaseInitialization());
				instance.Add(this.GenerateCreateAndAssignBuilder());
				instance.Add(this.F.Assignment(this.F.InstanceField(this.stateField), this.F.Parameter(this.F.CurrentFunction.Parameters[0]), false));
				BoundExpression boundExpression = base.MakeCurrentThreadId();
				if (boundExpression != null && this.initialThreadIdField != null)
				{
					instance.Add(this.F.Assignment(this.F.InstanceField(this.initialThreadIdField), boundExpression, false));
				}
				instance.Add(this.F.Return(null));
				this.F.CloseMethod(this.F.Block(instance.ToImmutableAndFree()));
			}

			// Token: 0x06008596 RID: 34198 RVA: 0x001E30FF File Offset: 0x001E12FF
			private BoundExpressionStatement GenerateCreateAndAssignBuilder()
			{
				return this.F.Assignment(this.F.InstanceField(this._builderField), this.F.StaticCall(null, this._asyncMethodBuilderMemberCollection.CreateBuilder, Array.Empty<BoundExpression>()), false);
			}

			// Token: 0x06008597 RID: 34199 RVA: 0x001E313C File Offset: 0x001E133C
			protected override void InitializeStateMachine(ArrayBuilder<BoundStatement> bodyBuilder, NamedTypeSymbol frameType, LocalSymbol stateMachineLocal)
			{
				int num = (this._isEnumerable ? (-2) : (-3));
				bodyBuilder.Add(this.F.Assignment(this.F.Local(stateMachineLocal), this.F.New(this.stateMachineType.Constructor.AsMember(frameType), new BoundExpression[] { this.F.Literal(num) }), false));
			}

			// Token: 0x06008598 RID: 34200 RVA: 0x001E31A8 File Offset: 0x001E13A8
			protected override BoundStatement InitializeParameterField(MethodSymbol getEnumeratorMethod, ParameterSymbol parameter, BoundExpression resultParameter, BoundExpression parameterProxy)
			{
				BoundStatement boundStatement;
				if (this._combinedTokensField != null && parameter.IsSourceParameterWithEnumeratorCancellationAttribute() && parameter.Type.Equals(this.F.Compilation.GetWellKnownType(WellKnownType.System_Threading_CancellationToken), TypeCompareKind.ConsiderEverything))
				{
					BoundParameter boundParameter = this.F.Parameter(getEnumeratorMethod.Parameters[0]);
					BoundFieldAccess boundFieldAccess = this.F.Field(this.F.This(), this._combinedTokensField);
					boundStatement = this.F.If(this.F.Call(parameterProxy, WellKnownMember.System_Threading_CancellationToken__Equals, this.F.Default(parameterProxy.Type)), this.F.Assignment(resultParameter, boundParameter, false), this.F.If(this.F.LogicalOr(this.F.Call(boundParameter, WellKnownMember.System_Threading_CancellationToken__Equals, parameterProxy), this.F.Call(boundParameter, WellKnownMember.System_Threading_CancellationToken__Equals, this.F.Default(boundParameter.Type))), this.F.Assignment(resultParameter, parameterProxy, false), this.F.Block(new BoundStatement[]
					{
						this.F.Assignment(boundFieldAccess, this.F.StaticCall(WellKnownMember.System_Threading_CancellationTokenSource__CreateLinkedTokenSource, new BoundExpression[] { parameterProxy, boundParameter }), false),
						this.F.Assignment(resultParameter, this.F.Property(boundFieldAccess, WellKnownMember.System_Threading_CancellationTokenSource__Token), false)
					})));
				}
				else
				{
					boundStatement = this.F.Assignment(resultParameter, parameterProxy, false);
				}
				return boundStatement;
			}

			// Token: 0x06008599 RID: 34201 RVA: 0x001E3338 File Offset: 0x001E1538
			protected override BoundStatement GenerateStateMachineCreation(LocalSymbol stateMachineVariable, NamedTypeSymbol frameType, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> proxies)
			{
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
				instance.Add(base.GenerateParameterStorage(stateMachineVariable, proxies));
				instance.Add(this.F.Return(this.F.Local(stateMachineVariable)));
				return this.F.Block(instance.ToImmutableAndFree());
			}

			// Token: 0x0600859A RID: 34202 RVA: 0x001E3388 File Offset: 0x001E1588
			private void GenerateIAsyncEnumeratorImplementation_MoveNextAsync()
			{
				NamedTypeSymbol namedTypeSymbol = this.F.WellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerator_T).Construct(new TypeSymbol[] { this._currentField.Type });
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Collections_Generic_IAsyncEnumerator_T__MoveNextAsync).AsMember(namedTypeSymbol);
				NamedTypeSymbol namedTypeSymbol2 = (NamedTypeSymbol)this._promiseOfValueOrEndField.Type;
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetStatus).AsMember(namedTypeSymbol2);
				MethodSymbol methodSymbol3 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetResult).AsMember(namedTypeSymbol2);
				NamedTypeSymbol namedTypeSymbol3 = (NamedTypeSymbol)methodSymbol.ReturnType;
				MethodSymbol methodSymbol4 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_ValueTask_T__ctorValue).AsMember(namedTypeSymbol3);
				MethodSymbol methodSymbol5 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_ValueTask_T__ctorSourceAndToken).AsMember(namedTypeSymbol3);
				base.OpenMethodImplementation(methodSymbol, null, false);
				BoundExpressionStatement boundExpressionStatement;
				LocalSymbol localSymbol;
				BoundStatement boundStatement;
				BoundExpressionStatement boundExpressionStatement2;
				MethodSymbol methodSymbol6;
				this.GetPartsForStartingMachine(out boundExpressionStatement, out localSymbol, out boundStatement, out boundExpressionStatement2, out methodSymbol6);
				BoundStatement boundStatement2 = this.F.If(this.F.IntEqual(this.F.InstanceField(this.stateField), this.F.Literal(-2)), this.F.Return(this.F.Default(namedTypeSymbol3)), null);
				LocalSymbol localSymbol2 = this.F.SynthesizedLocal(this.F.SpecialType(SpecialType.System_Int16), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				BoundLocal boundLocal = this.F.Local(localSymbol2);
				BoundExpressionStatement boundExpressionStatement3 = this.F.Assignment(boundLocal, this.F.Call(this.F.Field(this.F.This(), this._promiseOfValueOrEndField), methodSymbol6), false);
				BoundStatement boundStatement3 = this.F.If(this.F.IntEqual(this.F.Call(this.F.Field(this.F.This(), this._promiseOfValueOrEndField), methodSymbol2, boundLocal), this.F.Literal(1)), this.F.Return(this.F.New(methodSymbol4, new BoundExpression[] { this.F.Call(this.F.Field(this.F.This(), this._promiseOfValueOrEndField), methodSymbol3, boundLocal) })), null);
				BoundReturnStatement boundReturnStatement = this.F.Return(this.F.New(methodSymbol5, new BoundExpression[]
				{
					this.F.This(),
					boundLocal
				}));
				this.F.CloseMethod(this.F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol, localSymbol2), new BoundStatement[] { boundStatement2, boundExpressionStatement, boundStatement, boundExpressionStatement2, boundExpressionStatement3, boundStatement3, boundReturnStatement }));
			}

			// Token: 0x0600859B RID: 34203 RVA: 0x001E363C File Offset: 0x001E183C
			private void GetPartsForStartingMachine(out BoundExpressionStatement callReset, out LocalSymbol instSymbol, out BoundStatement instAssignment, out BoundExpressionStatement startCall, out MethodSymbol promise_get_Version)
			{
				BoundFieldAccess boundFieldAccess = this.F.InstanceField(this._promiseOfValueOrEndField);
				MethodSymbol methodSymbol = (MethodSymbol)this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__Reset, true).SymbolAsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				callReset = this.F.ExpressionStatement(this.F.Call(boundFieldAccess, methodSymbol));
				MethodSymbol methodSymbol2 = this._asyncMethodBuilderMemberCollection.Start.Construct(new TypeSymbol[] { this.stateMachineType });
				instSymbol = this.F.SynthesizedLocal(this.stateMachineType, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				BoundLocal boundLocal = this.F.Local(instSymbol);
				instAssignment = this.F.Assignment(boundLocal, this.F.This(), false);
				startCall = this.F.ExpressionStatement(this.F.Call(this.F.InstanceField(this._builderField), methodSymbol2, ImmutableArray.Create<BoundExpression>(boundLocal)));
				promise_get_Version = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__get_Version).AsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
			}

			// Token: 0x0600859C RID: 34204 RVA: 0x001E375C File Offset: 0x001E195C
			private void GenerateIAsyncDisposable_DisposeAsync()
			{
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_IAsyncDisposable__DisposeAsync);
				base.OpenMethodImplementation(methodSymbol, null, false);
				TypeSymbol returnType = methodSymbol.ReturnType;
				BoundExpressionStatement boundExpressionStatement;
				LocalSymbol localSymbol;
				BoundStatement boundStatement;
				BoundExpressionStatement boundExpressionStatement2;
				MethodSymbol methodSymbol2;
				this.GetPartsForStartingMachine(out boundExpressionStatement, out localSymbol, out boundStatement, out boundExpressionStatement2, out methodSymbol2);
				BoundStatement boundStatement2 = this.F.If(this.F.IntGreaterThanOrEqual(this.F.InstanceField(this.stateField), this.F.Literal(-1)), this.F.Throw(this.F.New(this.F.WellKnownType(WellKnownType.System_NotSupportedException), Array.Empty<BoundExpression>())), null);
				BoundStatement boundStatement3 = this.F.If(this.F.IntEqual(this.F.InstanceField(this.stateField), this.F.Literal(-2)), this.F.Return(this.F.Default(returnType)), null);
				MethodSymbol methodSymbol3 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_ValueTask__ctor).AsMember((NamedTypeSymbol)methodSymbol.ReturnType);
				BoundReturnStatement boundReturnStatement = this.F.Return(this.F.New(methodSymbol3, new BoundExpression[]
				{
					this.F.This(),
					this.F.Call(this.F.InstanceField(this._promiseOfValueOrEndField), methodSymbol2)
				}));
				this.F.CloseMethod(this.F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol), new BoundStatement[]
				{
					boundStatement2,
					boundStatement3,
					this.F.Assignment(this.F.InstanceField(this._disposeModeField), this.F.Literal(true), false),
					boundExpressionStatement,
					boundStatement,
					boundExpressionStatement2,
					boundReturnStatement
				}));
			}

			// Token: 0x0600859D RID: 34205 RVA: 0x001E3928 File Offset: 0x001E1B28
			private void GenerateIAsyncEnumeratorImplementation_Current()
			{
				NamedTypeSymbol namedTypeSymbol = this.F.WellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerator_T).Construct(new TypeSymbol[] { this._currentField.Type });
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Collections_Generic_IAsyncEnumerator_T__get_Current).AsMember(namedTypeSymbol);
				base.OpenPropertyImplementation(methodSymbol);
				this.F.CloseMethod(this.F.Block(new BoundStatement[] { this.F.Return(this.F.InstanceField(this._currentField)) }));
			}

			// Token: 0x0600859E RID: 34206 RVA: 0x001E39BC File Offset: 0x001E1BBC
			private void GenerateIValueTaskSourceBoolImplementation_GetResult()
			{
				NamedTypeSymbol namedTypeSymbol = this.F.WellKnownType(WellKnownType.System_Threading_Tasks_Sources_IValueTaskSource_T).Construct(new TypeSymbol[] { this.F.SpecialType(SpecialType.System_Boolean) });
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource_T__GetResult).AsMember(namedTypeSymbol);
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetResult).AsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				base.OpenMethodImplementation(methodSymbol, null, false);
				this.F.CloseMethod(this.F.Return(this.F.Call(this.F.InstanceField(this._promiseOfValueOrEndField), methodSymbol2, this.F.Parameter(methodSymbol.Parameters[0]))));
			}

			// Token: 0x0600859F RID: 34207 RVA: 0x001E3A88 File Offset: 0x001E1C88
			private void GenerateIValueTaskSourceBoolImplementation_GetStatus()
			{
				NamedTypeSymbol namedTypeSymbol = this.F.WellKnownType(WellKnownType.System_Threading_Tasks_Sources_IValueTaskSource_T).Construct(new TypeSymbol[] { this.F.SpecialType(SpecialType.System_Boolean) });
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource_T__GetStatus).AsMember(namedTypeSymbol);
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetStatus).AsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				base.OpenMethodImplementation(methodSymbol, null, false);
				this.F.CloseMethod(this.F.Return(this.F.Call(this.F.InstanceField(this._promiseOfValueOrEndField), methodSymbol2, this.F.Parameter(methodSymbol.Parameters[0]))));
			}

			// Token: 0x060085A0 RID: 34208 RVA: 0x001E3B54 File Offset: 0x001E1D54
			private void GenerateIValueTaskSourceBoolImplementation_OnCompleted()
			{
				NamedTypeSymbol namedTypeSymbol = this.F.WellKnownType(WellKnownType.System_Threading_Tasks_Sources_IValueTaskSource_T).Construct(new TypeSymbol[] { this.F.SpecialType(SpecialType.System_Boolean) });
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource_T__OnCompleted).AsMember(namedTypeSymbol);
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__OnCompleted).AsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				base.OpenMethodImplementation(methodSymbol, null, false);
				this.F.CloseMethod(this.F.Block(new BoundStatement[]
				{
					this.F.ExpressionStatement(this.F.Call(this.F.InstanceField(this._promiseOfValueOrEndField), methodSymbol2, new BoundExpression[]
					{
						this.F.Parameter(methodSymbol.Parameters[0]),
						this.F.Parameter(methodSymbol.Parameters[1]),
						this.F.Parameter(methodSymbol.Parameters[2]),
						this.F.Parameter(methodSymbol.Parameters[3])
					})),
					this.F.Return(null)
				}));
			}

			// Token: 0x060085A1 RID: 34209 RVA: 0x001E3CA4 File Offset: 0x001E1EA4
			private void GenerateIValueTaskSourceImplementation_GetResult()
			{
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource__GetResult);
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetResult).AsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				base.OpenMethodImplementation(methodSymbol, null, false);
				this.F.CloseMethod(this.F.Block(new BoundStatement[]
				{
					this.F.ExpressionStatement(this.F.Call(this.F.InstanceField(this._promiseOfValueOrEndField), methodSymbol2, this.F.Parameter(methodSymbol.Parameters[0]))),
					this.F.Return(null)
				}));
			}

			// Token: 0x060085A2 RID: 34210 RVA: 0x001E3D64 File Offset: 0x001E1F64
			private void GenerateIValueTaskSourceImplementation_GetStatus()
			{
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource__GetStatus);
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__GetStatus).AsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				base.OpenMethodImplementation(methodSymbol, null, false);
				this.F.CloseMethod(this.F.Return(this.F.Call(this.F.InstanceField(this._promiseOfValueOrEndField), methodSymbol2, this.F.Parameter(methodSymbol.Parameters[0]))));
			}

			// Token: 0x060085A3 RID: 34211 RVA: 0x001E3E00 File Offset: 0x001E2000
			private void GenerateIValueTaskSourceImplementation_OnCompleted()
			{
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_IValueTaskSource__OnCompleted);
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__OnCompleted).AsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				base.OpenMethodImplementation(methodSymbol, null, false);
				this.F.CloseMethod(this.F.Block(new BoundStatement[]
				{
					this.F.ExpressionStatement(this.F.Call(this.F.InstanceField(this._promiseOfValueOrEndField), methodSymbol2, new BoundExpression[]
					{
						this.F.Parameter(methodSymbol.Parameters[0]),
						this.F.Parameter(methodSymbol.Parameters[1]),
						this.F.Parameter(methodSymbol.Parameters[2]),
						this.F.Parameter(methodSymbol.Parameters[3])
					})),
					this.F.Return(null)
				}));
			}

			// Token: 0x060085A4 RID: 34212 RVA: 0x001E3F20 File Offset: 0x001E2120
			private void GenerateIAsyncEnumerableImplementation_GetAsyncEnumerator()
			{
				NamedTypeSymbol namedTypeSymbol = this.F.WellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T).Construct(new TypeSymbol[] { this._currentField.Type });
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Collections_Generic_IAsyncEnumerable_T__GetAsyncEnumerator).AsMember(namedTypeSymbol);
				BoundExpression boundExpression = null;
				base.GenerateIteratorGetEnumerator(methodSymbol, ref boundExpression, -3);
			}

			// Token: 0x060085A5 RID: 34213 RVA: 0x001E3F7C File Offset: 0x001E217C
			protected override void GenerateResetInstance(ArrayBuilder<BoundStatement> builder, int initialState)
			{
				builder.Add(this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(initialState), false));
				builder.Add(this.GenerateCreateAndAssignBuilder());
				builder.Add(this.F.Assignment(this.F.InstanceField(this._disposeModeField), this.F.Literal(false), false));
			}

			// Token: 0x060085A6 RID: 34214 RVA: 0x001E4000 File Offset: 0x001E2200
			protected override void GenerateMoveNext(SynthesizedImplementationMethod moveNextMethod)
			{
				MethodSymbol methodSymbol = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__SetResult, true);
				if (methodSymbol != null)
				{
					methodSymbol = (MethodSymbol)methodSymbol.SymbolAsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				}
				MethodSymbol methodSymbol2 = this.F.WellKnownMethod(WellKnownMember.System_Threading_Tasks_Sources_ManualResetValueTaskSourceCore_T__SetException, true);
				if (methodSymbol2 != null)
				{
					methodSymbol2 = (MethodSymbol)methodSymbol2.SymbolAsMember((NamedTypeSymbol)this._promiseOfValueOrEndField.Type);
				}
				new AsyncIteratorMethodToStateMachineRewriter(this.method, this._methodOrdinal, this._asyncMethodBuilderMemberCollection, new AsyncIteratorInfo(this._promiseOfValueOrEndField, this._combinedTokensField, this._currentField, this._disposeModeField, methodSymbol, methodSymbol2), this.F, this.stateField, this._builderField, this.hoistedVariables, this.nonReusableLocalProxies, this.synthesizedLocalOrdinals, this.slotAllocatorOpt, this.nextFreeHoistedLocalSlot, this.diagnostics).GenerateMoveNext(this.body, moveNextMethod);
			}

			// Token: 0x04002283 RID: 8835
			private FieldSymbol _promiseOfValueOrEndField;

			// Token: 0x04002284 RID: 8836
			private FieldSymbol _currentField;

			// Token: 0x04002285 RID: 8837
			private FieldSymbol _disposeModeField;

			// Token: 0x04002286 RID: 8838
			private FieldSymbol _combinedTokensField;

			// Token: 0x04002287 RID: 8839
			private readonly bool _isEnumerable;
		}

		// Token: 0x02000718 RID: 1816
		private class AwaitDetector : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x060085A7 RID: 34215 RVA: 0x001E40E7 File Offset: 0x001E22E7
			public static bool ContainsAwait(BoundNode node)
			{
				AsyncRewriter.AwaitDetector awaitDetector = new AsyncRewriter.AwaitDetector();
				awaitDetector.Visit(node);
				return awaitDetector._sawAwait;
			}

			// Token: 0x060085A8 RID: 34216 RVA: 0x001E40FB File Offset: 0x001E22FB
			public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
			{
				this._sawAwait = true;
				return null;
			}

			// Token: 0x04002288 RID: 8840
			private bool _sawAwait;
		}
	}
}
