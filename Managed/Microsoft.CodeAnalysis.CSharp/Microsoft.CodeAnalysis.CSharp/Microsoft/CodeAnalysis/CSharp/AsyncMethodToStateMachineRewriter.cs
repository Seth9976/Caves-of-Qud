using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001ED RID: 493
	internal class AsyncMethodToStateMachineRewriter : MethodToStateMachineRewriter
	{
		// Token: 0x06002193 RID: 8595 RVA: 0x000AEDA0 File Offset: 0x000ACFA0
		internal AsyncMethodToStateMachineRewriter(MethodSymbol method, int methodOrdinal, AsyncMethodBuilderMemberCollection asyncMethodBuilderMemberCollection, SyntheticBoundNodeFactory F, FieldSymbol state, FieldSymbol builder, IReadOnlySet<Symbol> hoistedVariables, IReadOnlyDictionary<Symbol, CapturedSymbolReplacement> nonReusableLocalProxies, SynthesizedLocalOrdinalsDispenser synthesizedLocalOrdinals, VariableSlotAllocator slotAllocatorOpt, int nextFreeHoistedLocalSlot, BindingDiagnosticBag diagnostics)
			: base(F, method, state, hoistedVariables, nonReusableLocalProxies, synthesizedLocalOrdinals, slotAllocatorOpt, nextFreeHoistedLocalSlot, diagnostics, false)
		{
			this._method = method;
			this._asyncMethodBuilderMemberCollection = asyncMethodBuilderMemberCollection;
			this._asyncMethodBuilderField = builder;
			this._exprReturnLabel = F.GenerateLabel("exprReturn");
			this._exitLabel = F.GenerateLabel("exitLabel");
			this._exprRetValue = (method.IsAsyncReturningGenericTask(F.Compilation) ? F.SynthesizedLocal(asyncMethodBuilderMemberCollection.ResultType, F.Syntax, false, RefKind.None, SynthesizedLocalKind.AsyncMethodReturnValue) : null);
			this._dynamicFactory = new LoweredDynamicOperationFactory(F, methodOrdinal, -1);
			this._awaiterFields = new Dictionary<TypeSymbol, FieldSymbol>(SymbolEqualityComparer.IgnoringDynamicTupleNamesAndNullability);
			this._nextAwaiterId = ((slotAllocatorOpt != null) ? slotAllocatorOpt.PreviousAwaiterSlotCount : 0);
			this._placeholderMap = new Dictionary<BoundValuePlaceholderBase, BoundExpression>();
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000AEE70 File Offset: 0x000AD070
		private FieldSymbol GetAwaiterField(TypeSymbol awaiterType)
		{
			FieldSymbol fieldSymbol;
			if (!this._awaiterFields.TryGetValue(awaiterType, ref fieldSymbol))
			{
				int num;
				if (this.slotAllocatorOpt == null || !this.slotAllocatorOpt.TryGetPreviousAwaiterSlotIndex(this.F.ModuleBuilderOpt.Translate(awaiterType, this.F.Syntax, this.F.Diagnostics.DiagnosticBag), this.F.Diagnostics.DiagnosticBag, out num))
				{
					int nextAwaiterId = this._nextAwaiterId;
					this._nextAwaiterId = nextAwaiterId + 1;
					num = nextAwaiterId;
				}
				string text = GeneratedNames.AsyncAwaiterFieldName(num);
				fieldSymbol = this.F.StateMachineField(awaiterType, text, SynthesizedLocalKind.AwaiterField, num);
				this._awaiterFields.Add(awaiterType, fieldSymbol);
			}
			return fieldSymbol;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000AEF20 File Offset: 0x000AD120
		internal void GenerateMoveNext(BoundStatement body, MethodSymbol moveNextMethod)
		{
			this.F.CurrentFunction = moveNextMethod;
			BoundStatement boundStatement = this.VisitBody(body);
			ImmutableArray<StateMachineFieldSymbol> immutableArray;
			MethodToStateMachineRewriter.TryUnwrapBoundStateMachineScope(ref boundStatement, out immutableArray);
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(this.F.HiddenSequencePoint(null));
			instance.Add(this.F.Assignment(this.F.Local(this.cachedState), this.F.Field(this.F.This(), this.stateField), false));
			instance.Add(base.CacheThisIfNeeded());
			LocalSymbol localSymbol = this.F.SynthesizedLocal(this.F.WellKnownType(WellKnownType.System_Exception), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			instance.Add(this.GenerateTopLevelTry(this.F.Block(ImmutableArray<LocalSymbol>.Empty, new BoundStatement[]
			{
				this.F.HiddenSequencePoint(null),
				base.Dispatch(),
				boundStatement
			}), this.F.CatchBlocks(new BoundCatchBlock[] { this.GenerateExceptionHandling(localSymbol, immutableArray) })));
			instance.Add(this.F.Label(this._exprReturnLabel));
			BoundExpressionStatement boundExpressionStatement = this.F.Assignment(this.F.Field(this.F.This(), this.stateField), this.F.Literal(-2), false);
			BlockSyntax blockSyntax = body.Syntax as BlockSyntax;
			if (blockSyntax == null)
			{
				instance.Add(boundExpressionStatement);
			}
			else
			{
				instance.Add(this.F.SequencePointWithSpan(blockSyntax, blockSyntax.CloseBraceToken.Span, boundExpressionStatement));
				instance.Add(this.F.HiddenSequencePoint(null));
			}
			instance.Add(this.GenerateHoistedLocalsCleanup(immutableArray));
			instance.Add(this.GenerateSetResultCall());
			instance.Add(this.F.Label(this._exitLabel));
			instance.Add(this.F.Return(null));
			ImmutableArray<BoundStatement> immutableArray2 = instance.ToImmutableAndFree();
			ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
			instance2.Add(this.cachedState);
			if (this.cachedThis != null)
			{
				instance2.Add(this.cachedThis);
			}
			if (this._exprRetValue != null)
			{
				instance2.Add(this._exprRetValue);
			}
			BoundStatement boundStatement2 = this.F.SequencePoint(body.Syntax, this.F.Block(instance2.ToImmutableAndFree(), immutableArray2));
			if (immutableArray.Length > 0)
			{
				boundStatement2 = base.MakeStateMachineScope(immutableArray, boundStatement2);
			}
			this.F.CloseMethod(boundStatement2);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000AF197 File Offset: 0x000AD397
		protected virtual BoundStatement GenerateTopLevelTry(BoundBlock tryBlock, ImmutableArray<BoundCatchBlock> catchBlocks)
		{
			return this.F.Try(tryBlock, catchBlocks, null, null);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000AF1A8 File Offset: 0x000AD3A8
		protected virtual BoundStatement GenerateSetResultCall()
		{
			return this.F.ExpressionStatement(this.F.Call(this.F.Field(this.F.This(), this._asyncMethodBuilderField), this._asyncMethodBuilderMemberCollection.SetResult, this._method.IsAsyncReturningGenericTask(this.F.Compilation) ? ImmutableArray.Create<BoundExpression>(this.F.Local(this._exprRetValue)) : ImmutableArray<BoundExpression>.Empty));
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000AF228 File Offset: 0x000AD428
		protected BoundCatchBlock GenerateExceptionHandling(LocalSymbol exceptionLocal, ImmutableArray<StateMachineFieldSymbol> hoistedLocals)
		{
			BoundStatement boundStatement = this.F.ExpressionStatement(this.F.AssignmentExpression(this.F.Field(this.F.This(), this.stateField), this.F.Literal(-2), false));
			BoundStatement boundStatement2 = this.GenerateSetExceptionCall(exceptionLocal);
			return new BoundCatchBlock(this.F.Syntax, ImmutableArray.Create<LocalSymbol>(exceptionLocal), this.F.Local(exceptionLocal), exceptionLocal.Type, null, null, this.F.Block(new BoundStatement[]
			{
				boundStatement,
				this.GenerateHoistedLocalsCleanup(hoistedLocals),
				boundStatement2,
				this.GenerateReturn(false)
			}), true, false);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000AF2D8 File Offset: 0x000AD4D8
		protected BoundStatement GenerateHoistedLocalsCleanup(ImmutableArray<StateMachineFieldSymbol> hoistedLocals)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			foreach (StateMachineFieldSymbol stateMachineFieldSymbol in hoistedLocals)
			{
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(this.F.Diagnostics, this.F.Compilation.Assembly);
				bool flag = stateMachineFieldSymbol.Type.IsManagedType(ref compoundUseSiteInfo);
				this.F.Diagnostics.Add(stateMachineFieldSymbol.Locations.FirstOrNone(), compoundUseSiteInfo);
				if (flag)
				{
					instance.Add(this.F.Assignment(this.F.Field(this.F.This(), stateMachineFieldSymbol), this.F.NullOrDefault(stateMachineFieldSymbol.Type), false));
				}
			}
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000AF3A8 File Offset: 0x000AD5A8
		protected virtual BoundStatement GenerateSetExceptionCall(LocalSymbol exceptionLocal)
		{
			return this.F.ExpressionStatement(this.F.Call(this.F.Field(this.F.This(), this._asyncMethodBuilderField), this._asyncMethodBuilderMemberCollection.SetException, this.F.Local(exceptionLocal)));
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000AF3FE File Offset: 0x000AD5FE
		protected sealed override BoundStatement GenerateReturn(bool finished)
		{
			return this.F.Goto(this._exitLabel);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000AF411 File Offset: 0x000AD611
		protected virtual BoundStatement VisitBody(BoundStatement body)
		{
			return (BoundStatement)this.Visit(body);
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000AF420 File Offset: 0x000AD620
		public sealed override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			if (node.Expression.Kind == BoundKind.AwaitExpression)
			{
				return this.VisitAwaitExpression((BoundAwaitExpression)node.Expression, null);
			}
			if (node.Expression.Kind == BoundKind.AssignmentOperator)
			{
				BoundAssignmentOperator boundAssignmentOperator = (BoundAssignmentOperator)node.Expression;
				if (boundAssignmentOperator.Right.Kind == BoundKind.AwaitExpression)
				{
					return this.VisitAwaitExpression((BoundAwaitExpression)boundAssignmentOperator.Right, boundAssignmentOperator.Left);
				}
			}
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			if (boundExpression == null)
			{
				return this.F.StatementList();
			}
			return node.Update(boundExpression);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000AF4B9 File Offset: 0x000AD6B9
		public sealed override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000AF4C0 File Offset: 0x000AD6C0
		public sealed override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			return node;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000AF4C4 File Offset: 0x000AD6C4
		private BoundBlock VisitAwaitExpression(BoundAwaitExpression node, BoundExpression resultPlace)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			BoundAwaitableValuePlaceholder awaitableInstancePlaceholder = node.AwaitableInfo.AwaitableInstancePlaceholder;
			if (awaitableInstancePlaceholder != null)
			{
				this._placeholderMap.Add(awaitableInstancePlaceholder, boundExpression);
			}
			BoundExpression boundExpression2 = (node.AwaitableInfo.IsDynamic ? this.MakeCallMaybeDynamic(boundExpression, null, "GetAwaiter", false) : ((BoundExpression)this.Visit(node.AwaitableInfo.GetAwaiter)));
			resultPlace = (BoundExpression)this.Visit(resultPlace);
			MethodSymbol methodSymbol = base.VisitMethodSymbol(node.AwaitableInfo.GetResult);
			MethodSymbol methodSymbol2 = ((node.AwaitableInfo.IsCompleted != null) ? base.VisitMethodSymbol(node.AwaitableInfo.IsCompleted.GetMethod) : null);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			if (awaitableInstancePlaceholder != null)
			{
				this._placeholderMap.Remove(awaitableInstancePlaceholder);
			}
			LocalSymbol localSymbol = this.F.SynthesizedLocal(boundExpression2.Type, node.Syntax, false, RefKind.None, SynthesizedLocalKind.Awaiter);
			BoundBlock boundBlock = this.F.Block(new BoundStatement[]
			{
				this.F.Assignment(this.F.Local(localSymbol), boundExpression2, false),
				this.F.HiddenSequencePoint(null),
				this.F.If(this.F.Not(this.GenerateGetIsCompleted(localSymbol, methodSymbol2)), this.GenerateAwaitForIncompleteTask(localSymbol), null)
			});
			BoundExpression boundExpression3 = this.MakeCallMaybeDynamic(this.F.Local(localSymbol), methodSymbol, "GetResult", resultPlace == null);
			BoundStatement boundStatement = ((resultPlace != null && !typeSymbol.IsVoidType()) ? this.F.Assignment(resultPlace, boundExpression3, false) : this.F.ExpressionStatement(boundExpression3));
			return this.F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol), new BoundStatement[] { boundBlock, boundStatement });
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000AF68E File Offset: 0x000AD88E
		public override BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			return this._placeholderMap[node];
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000AF69C File Offset: 0x000AD89C
		private BoundExpression MakeCallMaybeDynamic(BoundExpression receiver, MethodSymbol methodSymbol = null, string methodName = null, bool resultsDiscarded = false)
		{
			if (methodSymbol == null)
			{
				return this._dynamicFactory.MakeDynamicMemberInvocation(methodName, receiver, ImmutableArray<TypeWithAnnotations>.Empty, ImmutableArray<BoundExpression>.Empty, ImmutableArray<string>.Empty, ImmutableArray<RefKind>.Empty, false, resultsDiscarded).ToExpression();
			}
			if (!methodSymbol.IsStatic)
			{
				return this.F.Call(receiver, methodSymbol);
			}
			return this.F.StaticCall(methodSymbol.ContainingType, methodSymbol, new BoundExpression[] { receiver });
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000AF70C File Offset: 0x000AD90C
		private BoundExpression GenerateGetIsCompleted(LocalSymbol awaiterTemp, MethodSymbol getIsCompletedMethod)
		{
			if (awaiterTemp.Type.IsDynamic())
			{
				return this._dynamicFactory.MakeDynamicConversion(this._dynamicFactory.MakeDynamicGetMember(this.F.Local(awaiterTemp), "IsCompleted", false).ToExpression(), true, false, false, this.F.SpecialType(SpecialType.System_Boolean)).ToExpression();
			}
			return this.F.Call(this.F.Local(awaiterTemp), getIsCompletedMethod);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000AF788 File Offset: 0x000AD988
		private BoundBlock GenerateAwaitForIncompleteTask(LocalSymbol awaiterTemp)
		{
			int num;
			GeneratedLabelSymbol generatedLabelSymbol;
			base.AddState(out num, out generatedLabelSymbol);
			TypeSymbol typeSymbol = (awaiterTemp.Type.IsVerifierReference() ? this.F.SpecialType(SpecialType.System_Object) : awaiterTemp.Type);
			FieldSymbol awaiterField = this.GetAwaiterField(typeSymbol);
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(base.GenerateSetBothStates(num));
			instance.Add(this.F.NoOp(NoOpStatementFlavor.AwaitYieldPoint));
			instance.Add(this.F.Assignment(this.F.Field(this.F.This(), awaiterField), TypeSymbol.Equals(awaiterField.Type, awaiterTemp.Type, TypeCompareKind.ConsiderEverything) ? this.F.Local(awaiterTemp) : this.F.Convert(typeSymbol, this.F.Local(awaiterTemp)), false));
			instance.Add(awaiterTemp.Type.IsDynamic() ? this.GenerateAwaitOnCompletedDynamic(awaiterTemp) : this.GenerateAwaitOnCompleted(awaiterTemp.Type, awaiterTemp));
			instance.Add(this.GenerateReturn(false));
			instance.Add(this.F.Label(generatedLabelSymbol));
			instance.Add(this.F.NoOp(NoOpStatementFlavor.AwaitResumePoint));
			instance.Add(this.F.Assignment(this.F.Local(awaiterTemp), TypeSymbol.Equals(awaiterTemp.Type, awaiterField.Type, TypeCompareKind.ConsiderEverything) ? this.F.Field(this.F.This(), awaiterField) : this.F.Convert(awaiterTemp.Type, this.F.Field(this.F.This(), awaiterField)), false));
			instance.Add(this.F.Assignment(this.F.Field(this.F.This(), awaiterField), this.F.NullOrDefault(awaiterField.Type), false));
			instance.Add(base.GenerateSetBothStates(-1));
			return this.F.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000AF984 File Offset: 0x000ADB84
		private BoundStatement GenerateAwaitOnCompletedDynamic(LocalSymbol awaiterTemp)
		{
			LocalSymbol localSymbol = this.F.SynthesizedLocal(this.F.WellKnownType(WellKnownType.System_Runtime_CompilerServices_ICriticalNotifyCompletion), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			LocalSymbol localSymbol2 = this.F.SynthesizedLocal(this.F.WellKnownType(WellKnownType.System_Runtime_CompilerServices_INotifyCompletion), null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			LocalSymbol localSymbol3 = ((this.F.CurrentType.TypeKind == TypeKind.Class) ? this.F.SynthesizedLocal(this.F.CurrentType, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp) : null);
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(this.F.Assignment(this.F.Local(localSymbol), this.F.As(this.F.Local(awaiterTemp), localSymbol.Type), false));
			if (localSymbol3 != null)
			{
				instance.Add(this.F.Assignment(this.F.Local(localSymbol3), this.F.This(), false));
			}
			instance.Add(this.F.If(this.F.ObjectEqual(this.F.Local(localSymbol), this.F.Null(localSymbol.Type)), this.F.Block(ImmutableArray.Create<LocalSymbol>(localSymbol2), new BoundStatement[]
			{
				this.F.Assignment(this.F.Local(localSymbol2), this.F.Convert(localSymbol2.Type, this.F.Local(awaiterTemp), Conversion.ExplicitReference, false), false),
				this.F.ExpressionStatement(this.F.Call(this.F.Field(this.F.This(), this._asyncMethodBuilderField), this._asyncMethodBuilderMemberCollection.AwaitOnCompleted.Construct(new TypeSymbol[]
				{
					localSymbol2.Type,
					this.F.This().Type
				}), this.F.Local(localSymbol2), this.F.This(localSymbol3))),
				this.F.Assignment(this.F.Local(localSymbol2), this.F.NullOrDefault(localSymbol2.Type), false)
			}), this.F.Block(new BoundStatement[] { this.F.ExpressionStatement(this.F.Call(this.F.Field(this.F.This(), this._asyncMethodBuilderField), this._asyncMethodBuilderMemberCollection.AwaitUnsafeOnCompleted.Construct(new TypeSymbol[]
			{
				localSymbol.Type,
				this.F.This().Type
			}), this.F.Local(localSymbol), this.F.This(localSymbol3))) })));
			instance.Add(this.F.Assignment(this.F.Local(localSymbol), this.F.NullOrDefault(localSymbol.Type), false));
			return this.F.Block(AsyncMethodToStateMachineRewriter.SingletonOrPair(localSymbol, localSymbol3), instance.ToImmutableAndFree());
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000AFC88 File Offset: 0x000ADE88
		private BoundStatement GenerateAwaitOnCompleted(TypeSymbol loweredAwaiterType, LocalSymbol awaiterTemp)
		{
			LocalSymbol localSymbol = ((this.F.CurrentType.TypeKind == TypeKind.Class) ? this.F.SynthesizedLocal(this.F.CurrentType, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp) : null);
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			MethodSymbol methodSymbol = (this.F.Compilation.Conversions.ClassifyImplicitConversionFromType(loweredAwaiterType, this.F.Compilation.GetWellKnownType(WellKnownType.System_Runtime_CompilerServices_ICriticalNotifyCompletion), ref discarded).IsImplicit ? this._asyncMethodBuilderMemberCollection.AwaitUnsafeOnCompleted : this._asyncMethodBuilderMemberCollection.AwaitOnCompleted).Construct(new TypeSymbol[]
			{
				loweredAwaiterType,
				this.F.This().Type
			});
			if (this._asyncMethodBuilderMemberCollection.CheckGenericMethodConstraints)
			{
				MethodSymbol methodSymbol2 = methodSymbol;
				ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(this.F.Compilation, this.F.Compilation.Conversions, false, this.F.Syntax.Location, this.Diagnostics);
				methodSymbol2.CheckConstraints(in checkConstraintsArgs);
			}
			BoundExpression boundExpression = this.F.Call(this.F.Field(this.F.This(), this._asyncMethodBuilderField), methodSymbol, this.F.Local(awaiterTemp), this.F.This(localSymbol));
			if (localSymbol != null)
			{
				boundExpression = this.F.Sequence(ImmutableArray.Create<LocalSymbol>(localSymbol), ImmutableArray.Create<BoundExpression>(this.F.AssignmentExpression(this.F.Local(localSymbol), this.F.This(), false)), boundExpression);
			}
			return this.F.ExpressionStatement(boundExpression);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000AFE20 File Offset: 0x000AE020
		private static ImmutableArray<LocalSymbol> SingletonOrPair(LocalSymbol first, LocalSymbol secondOpt)
		{
			if (!(secondOpt == null))
			{
				return ImmutableArray.Create<LocalSymbol>(first, secondOpt);
			}
			return ImmutableArray.Create<LocalSymbol>(first);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000AFE3C File Offset: 0x000AE03C
		public sealed override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			if (node.ExpressionOpt != null)
			{
				return this.F.Block(new BoundStatement[]
				{
					this.F.Assignment(this.F.Local(this._exprRetValue), (BoundExpression)this.Visit(node.ExpressionOpt), false),
					this.F.Goto(this._exprReturnLabel)
				});
			}
			return this.F.Goto(this._exprReturnLabel);
		}

		// Token: 0x04001054 RID: 4180
		protected readonly MethodSymbol _method;

		// Token: 0x04001055 RID: 4181
		protected readonly FieldSymbol _asyncMethodBuilderField;

		// Token: 0x04001056 RID: 4182
		protected readonly AsyncMethodBuilderMemberCollection _asyncMethodBuilderMemberCollection;

		// Token: 0x04001057 RID: 4183
		protected readonly LabelSymbol _exprReturnLabel;

		// Token: 0x04001058 RID: 4184
		private readonly LabelSymbol _exitLabel;

		// Token: 0x04001059 RID: 4185
		private readonly LocalSymbol _exprRetValue;

		// Token: 0x0400105A RID: 4186
		private readonly LoweredDynamicOperationFactory _dynamicFactory;

		// Token: 0x0400105B RID: 4187
		private readonly Dictionary<TypeSymbol, FieldSymbol> _awaiterFields;

		// Token: 0x0400105C RID: 4188
		private int _nextAwaiterId;

		// Token: 0x0400105D RID: 4189
		private readonly Dictionary<BoundValuePlaceholderBase, BoundExpression> _placeholderMap;
	}
}
