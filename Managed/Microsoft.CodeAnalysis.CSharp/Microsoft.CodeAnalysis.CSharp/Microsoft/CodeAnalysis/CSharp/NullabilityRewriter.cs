using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F0 RID: 240
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NullabilityRewriter : BoundTreeRewriter
	{
		// Token: 0x06001212 RID: 4626 RVA: 0x0005F58C File Offset: 0x0005D78C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
		{
			return (BoundExpression)this.Visit(node);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0005F59A File Offset: 0x0005D79A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			return this.VisitBinaryOperatorBase(node);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0005F5A3 File Offset: 0x0005D7A3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			return this.VisitBinaryOperatorBase(node);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0005F5AC File Offset: 0x0005D7AC
		private BoundNode VisitBinaryOperatorBase(BoundBinaryOperatorBase binaryOperator)
		{
			ArrayBuilder<BoundBinaryOperatorBase> instance = ArrayBuilder<BoundBinaryOperatorBase>.GetInstance();
			BoundBinaryOperatorBase boundBinaryOperatorBase = binaryOperator;
			do
			{
				instance.Push(boundBinaryOperatorBase);
				boundBinaryOperatorBase = boundBinaryOperatorBase.Left as BoundBinaryOperatorBase;
			}
			while (boundBinaryOperatorBase != null);
			BoundExpression boundExpression = (BoundExpression)this.Visit(instance.Peek<BoundBinaryOperatorBase>().Left);
			for (;;)
			{
				boundBinaryOperatorBase = instance.Pop<BoundBinaryOperatorBase>();
				ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
				bool flag = this._updatedNullabilities.TryGetValue(boundBinaryOperatorBase, out valueTuple);
				BoundExpression boundExpression2 = (BoundExpression)this.Visit(boundBinaryOperatorBase.Right);
				TypeSymbol typeSymbol = (flag ? valueTuple.Item2 : boundBinaryOperatorBase.Type);
				BoundBinaryOperator boundBinaryOperator = boundBinaryOperatorBase as BoundBinaryOperator;
				BoundBinaryOperatorBase boundBinaryOperatorBase2;
				if (boundBinaryOperator == null)
				{
					BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = boundBinaryOperatorBase as BoundUserDefinedConditionalLogicalOperator;
					if (boundUserDefinedConditionalLogicalOperator == null)
					{
						break;
					}
					boundBinaryOperatorBase2 = boundUserDefinedConditionalLogicalOperator.Update(boundUserDefinedConditionalLogicalOperator.OperatorKind, boundUserDefinedConditionalLogicalOperator.LogicalOperator, boundUserDefinedConditionalLogicalOperator.TrueOperator, boundUserDefinedConditionalLogicalOperator.FalseOperator, boundUserDefinedConditionalLogicalOperator.ResultKind, boundUserDefinedConditionalLogicalOperator.OriginalUserDefinedOperatorsOpt, boundExpression, boundExpression2, typeSymbol);
				}
				else
				{
					boundBinaryOperatorBase2 = boundBinaryOperator.Update(boundBinaryOperator.OperatorKind, boundBinaryOperator.ConstantValueOpt, this.GetUpdatedSymbol<MethodSymbol>(boundBinaryOperator, boundBinaryOperator.MethodOpt), boundBinaryOperator.ResultKind, boundBinaryOperator.OriginalUserDefinedOperatorsOpt, boundExpression, boundExpression2, typeSymbol);
				}
				boundBinaryOperatorBase = boundBinaryOperatorBase2;
				if (flag)
				{
					boundBinaryOperatorBase.TopLevelNullability = valueTuple.Item1;
				}
				boundExpression = boundBinaryOperatorBase;
				if (instance.Count <= 0)
				{
					return boundBinaryOperatorBase;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(boundBinaryOperatorBase.Kind);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0005F6EC File Offset: 0x0005D8EC
		private T GetUpdatedSymbol<[global::System.Runtime.CompilerServices.Nullable(0)] T>(BoundNode expr, T sym) where T : Symbol
		{
			if (sym == null)
			{
				return sym;
			}
			Symbol symbol = null;
			NullableWalker.SnapshotManager snapshotManager = this._snapshotManager;
			if (snapshotManager == null || !snapshotManager.TryGetUpdatedSymbol(expr, sym, out symbol))
			{
				symbol = sym;
			}
			LambdaSymbol lambdaSymbol = symbol as LambdaSymbol;
			if (lambdaSymbol != null)
			{
				return (T)((object)this.<GetUpdatedSymbol>g__remapLambda|4_0<T>((BoundLambda)expr, lambdaSymbol));
			}
			SourceLocalSymbol sourceLocalSymbol = symbol as SourceLocalSymbol;
			if (sourceLocalSymbol == null)
			{
				ParameterSymbol parameterSymbol = symbol as ParameterSymbol;
				if (parameterSymbol != null)
				{
					Symbol symbol2;
					if (this._remappedSymbols.TryGetValue(parameterSymbol, out symbol2))
					{
						return (T)((object)symbol2);
					}
				}
				return (T)((object)symbol);
			}
			return (T)((object)this.<GetUpdatedSymbol>g__remapLocal|4_1<T>(sourceLocalSymbol));
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0005F78C File Offset: 0x0005D98C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<T> GetUpdatedArray<T>([global::System.Runtime.CompilerServices.Nullable(1)] BoundNode expr, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<T> symbols) where T : Symbol
		{
			if (symbols.IsDefaultOrEmpty)
			{
				return symbols;
			}
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance(symbols.Length);
			bool flag = false;
			foreach (T t in symbols)
			{
				T t2 = default(T);
				if (t != null)
				{
					t2 = this.GetUpdatedSymbol<T>(expr, t);
					if (t != t2)
					{
						flag = true;
					}
				}
				instance.Add(t2);
			}
			if (flag)
			{
				return instance.ToImmutableAndFree();
			}
			instance.Free();
			return symbols;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0005F812 File Offset: 0x0005DA12
		public NullabilityRewriter([TupleElementNames(new string[] { "Info", "Type" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 2 })] ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>> updatedNullabilities, [global::System.Runtime.CompilerServices.Nullable(2)] NullableWalker.SnapshotManager snapshotManager, ImmutableDictionary<Symbol, Symbol>.Builder remappedSymbols)
		{
			this._updatedNullabilities = updatedNullabilities;
			this._snapshotManager = snapshotManager;
			this._remappedSymbols = remappedSymbols;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0005F830 File Offset: 0x0005DA30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldEqualsValue(BoundFieldEqualsValue node)
		{
			FieldSymbol updatedSymbol = this.GetUpdatedSymbol<FieldSymbol>(node, node.Field);
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(updatedSymbol, updatedArray, boundExpression);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0005F874 File Offset: 0x0005DA74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyEqualsValue(BoundPropertyEqualsValue node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.Property);
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(updatedSymbol, updatedArray, boundExpression);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0005F8B8 File Offset: 0x0005DAB8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameterEqualsValue(BoundParameterEqualsValue node)
		{
			ParameterSymbol updatedSymbol = this.GetUpdatedSymbol<ParameterSymbol>(node, node.Parameter);
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(updatedSymbol, updatedArray, boundExpression);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0005F8FC File Offset: 0x0005DAFC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder = node.Update(node.ValEscape, valueTuple.Item2);
			boundDeconstructValuePlaceholder.TopLevelNullability = valueTuple.Item1;
			return boundDeconstructValuePlaceholder;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0005F93C File Offset: 0x0005DB3C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleOperandPlaceholder(BoundTupleOperandPlaceholder node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundTupleOperandPlaceholder boundTupleOperandPlaceholder = node.Update(valueTuple.Item2);
			boundTupleOperandPlaceholder.TopLevelNullability = valueTuple.Item1;
			return boundTupleOperandPlaceholder;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0005F974 File Offset: 0x0005DB74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = node.Update(node.ValEscape, valueTuple.Item2);
			boundAwaitableValuePlaceholder.TopLevelNullability = valueTuple.Item1;
			return boundAwaitableValuePlaceholder;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0005F9B4 File Offset: 0x0005DBB4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDisposableValuePlaceholder(BoundDisposableValuePlaceholder node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundDisposableValuePlaceholder boundDisposableValuePlaceholder = node.Update(valueTuple.Item2);
			boundDisposableValuePlaceholder.TopLevelNullability = valueTuple.Item1;
			return boundDisposableValuePlaceholder;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0005F9EC File Offset: 0x0005DBEC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundObjectOrCollectionValuePlaceholder boundObjectOrCollectionValuePlaceholder = node.Update(node.IsNewInstance, valueTuple.Item2);
			boundObjectOrCollectionValuePlaceholder.TopLevelNullability = valueTuple.Item1;
			return boundObjectOrCollectionValuePlaceholder;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0005FA2C File Offset: 0x0005DC2C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDup(BoundDup node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundDup boundDup = node.Update(node.RefKind, valueTuple.Item2);
			boundDup.TopLevelNullability = valueTuple.Item1;
			return boundDup;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0005FA6C File Offset: 0x0005DC6C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPassByCopy(BoundPassByCopy node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundPassByCopy boundPassByCopy;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundPassByCopy = node.Update(boundExpression, valueTuple.Item2);
				boundPassByCopy.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundPassByCopy = node.Update(boundExpression, node.Type);
			}
			return boundPassByCopy;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0005FAC8 File Offset: 0x0005DCC8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			this.GetUpdatedArray<Symbol>(node, node.Symbols);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.ChildBoundNodes);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundBadExpression boundBadExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundBadExpression = node.Update(node.ResultKind, node.Symbols, immutableArray, valueTuple.Item2);
				boundBadExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundBadExpression = node.Update(node.ResultKind, node.Symbols, immutableArray, node.Type);
			}
			return boundBadExpression;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0005FB44 File Offset: 0x0005DD44
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeExpression(BoundTypeExpression node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.BoundContainingTypeOpt);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.BoundDimensionsOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundTypeExpression boundTypeExpression2;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundTypeExpression2 = node.Update(node.AliasOpt, boundTypeExpression, immutableArray, node.TypeWithAnnotations, valueTuple.Item2);
				boundTypeExpression2.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundTypeExpression2 = node.Update(node.AliasOpt, boundTypeExpression, immutableArray, node.TypeWithAnnotations, node.Type);
			}
			return boundTypeExpression2;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0005FBC8 File Offset: 0x0005DDC8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOrValueExpression(BoundTypeOrValueExpression node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundTypeOrValueExpression boundTypeOrValueExpression = node.Update(node.Data, valueTuple.Item2);
			boundTypeOrValueExpression.TopLevelNullability = valueTuple.Item1;
			return boundTypeOrValueExpression;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0005FC08 File Offset: 0x0005DE08
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNamespaceExpression(BoundNamespaceExpression node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundNamespaceExpression boundNamespaceExpression = node.Update(node.NamespaceSymbol, node.AliasOpt);
			boundNamespaceExpression.TopLevelNullability = valueTuple.Item1;
			return boundNamespaceExpression;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0005FC48 File Offset: 0x0005DE48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.MethodOpt);
			this.GetUpdatedArray<MethodSymbol>(node, node.OriginalUserDefinedOperatorsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundUnaryOperator boundUnaryOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundUnaryOperator = node.Update(node.OperatorKind, boundExpression, node.ConstantValueOpt, updatedSymbol, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, valueTuple.Item2);
				boundUnaryOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundUnaryOperator = node.Update(node.OperatorKind, boundExpression, node.ConstantValueOpt, updatedSymbol, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, node.Type);
			}
			return boundUnaryOperator;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0005FCF0 File Offset: 0x0005DEF0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.MethodOpt);
			this.GetUpdatedArray<MethodSymbol>(node, node.OriginalUserDefinedOperatorsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundIncrementOperator boundIncrementOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundIncrementOperator = node.Update(node.OperatorKind, boundExpression, updatedSymbol, node.OperandConversion, node.ResultConversion, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, valueTuple.Item2);
				boundIncrementOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundIncrementOperator = node.Update(node.OperatorKind, boundExpression, updatedSymbol, node.OperandConversion, node.ResultConversion, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, node.Type);
			}
			return boundIncrementOperator;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0005FDA4 File Offset: 0x0005DFA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundAddressOfOperator boundAddressOfOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundAddressOfOperator = node.Update(boundExpression, node.IsManaged, valueTuple.Item2);
				boundAddressOfOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundAddressOfOperator = node.Update(boundExpression, node.IsManaged, node.Type);
			}
			return boundAddressOfOperator;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0005FE0C File Offset: 0x0005E00C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedAddressOfOperator(BoundUnconvertedAddressOfOperator node)
		{
			BoundMethodGroup boundMethodGroup = (BoundMethodGroup)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundUnconvertedAddressOfOperator boundUnconvertedAddressOfOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundUnconvertedAddressOfOperator = node.Update(boundMethodGroup);
				boundUnconvertedAddressOfOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundUnconvertedAddressOfOperator = node.Update(boundMethodGroup);
			}
			return boundUnconvertedAddressOfOperator;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0005FE5C File Offset: 0x0005E05C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerLoad(BoundFunctionPointerLoad node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.TargetMethod);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundFunctionPointerLoad boundFunctionPointerLoad;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundFunctionPointerLoad = node.Update(updatedSymbol, valueTuple.Item2);
				boundFunctionPointerLoad.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundFunctionPointerLoad = node.Update(updatedSymbol, node.Type);
			}
			return boundFunctionPointerLoad;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0005FEB4 File Offset: 0x0005E0B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundPointerIndirectionOperator boundPointerIndirectionOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundPointerIndirectionOperator = node.Update(boundExpression, valueTuple.Item2);
				boundPointerIndirectionOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundPointerIndirectionOperator = node.Update(boundExpression, node.Type);
			}
			return boundPointerIndirectionOperator;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0005FF10 File Offset: 0x0005E110
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Index);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundPointerElementAccess boundPointerElementAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundPointerElementAccess = node.Update(boundExpression, boundExpression2, node.Checked, valueTuple.Item2);
				boundPointerElementAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundPointerElementAccess = node.Update(boundExpression, boundExpression2, node.Checked, node.Type);
			}
			return boundPointerElementAccess;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0005FF8C File Offset: 0x0005E18C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.InvokedExpression);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundFunctionPointerInvocation boundFunctionPointerInvocation;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundFunctionPointerInvocation = node.Update(boundExpression, immutableArray, node.ArgumentRefKindsOpt, node.ResultKind, valueTuple.Item2);
				boundFunctionPointerInvocation.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundFunctionPointerInvocation = node.Update(boundExpression, immutableArray, node.ArgumentRefKindsOpt, node.ResultKind, node.Type);
			}
			return boundFunctionPointerInvocation;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00060010 File Offset: 0x0005E210
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetTypeFromHandle);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundRefTypeOperator boundRefTypeOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundRefTypeOperator = node.Update(boundExpression, updatedSymbol, valueTuple.Item2);
				boundRefTypeOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundRefTypeOperator = node.Update(boundExpression, updatedSymbol, node.Type);
			}
			return boundRefTypeOperator;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0006007C File Offset: 0x0005E27C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundMakeRefOperator boundMakeRefOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundMakeRefOperator = node.Update(boundExpression, valueTuple.Item2);
				boundMakeRefOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundMakeRefOperator = node.Update(boundExpression, node.Type);
			}
			return boundMakeRefOperator;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000600D8 File Offset: 0x0005E2D8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefValueOperator(BoundRefValueOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundRefValueOperator boundRefValueOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundRefValueOperator = node.Update(node.NullableAnnotation, boundExpression, valueTuple.Item2);
				boundRefValueOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundRefValueOperator = node.Update(node.NullableAnnotation, boundExpression, node.Type);
			}
			return boundRefValueOperator;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00060140 File Offset: 0x0005E340
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.MethodOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundFromEndIndexExpression boundFromEndIndexExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundFromEndIndexExpression = node.Update(boundExpression, updatedSymbol, valueTuple.Item2);
				boundFromEndIndexExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundFromEndIndexExpression = node.Update(boundExpression, updatedSymbol, node.Type);
			}
			return boundFromEndIndexExpression;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000601AC File Offset: 0x0005E3AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.MethodOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.LeftOperandOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.RightOperandOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundRangeExpression boundRangeExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundRangeExpression = node.Update(boundExpression, boundExpression2, updatedSymbol, valueTuple.Item2);
				boundRangeExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundRangeExpression = node.Update(boundExpression, boundExpression2, updatedSymbol, node.Type);
			}
			return boundRangeExpression;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0006022C File Offset: 0x0005E42C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundTupleBinaryOperator boundTupleBinaryOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundTupleBinaryOperator = node.Update(boundExpression, boundExpression2, node.OperatorKind, node.Operators, valueTuple.Item2);
				boundTupleBinaryOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundTupleBinaryOperator = node.Update(boundExpression, boundExpression2, node.OperatorKind, node.Operators, node.Type);
			}
			return boundTupleBinaryOperator;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000602B4 File Offset: 0x0005E4B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			this.GetUpdatedArray<MethodSymbol>(node, node.OriginalUserDefinedOperatorsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundCompoundAssignmentOperator boundCompoundAssignmentOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundCompoundAssignmentOperator = node.Update(node.Operator, boundExpression, boundExpression2, node.LeftConversion, node.FinalConversion, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, valueTuple.Item2);
				boundCompoundAssignmentOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundCompoundAssignmentOperator = node.Update(node.Operator, boundExpression, boundExpression2, node.LeftConversion, node.FinalConversion, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, node.Type);
			}
			return boundCompoundAssignmentOperator;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0006036C File Offset: 0x0005E56C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundAssignmentOperator boundAssignmentOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundAssignmentOperator = node.Update(boundExpression, boundExpression2, node.IsRef, valueTuple.Item2);
				boundAssignmentOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundAssignmentOperator = node.Update(boundExpression, boundExpression2, node.IsRef, node.Type);
			}
			return boundAssignmentOperator;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000603E8 File Offset: 0x0005E5E8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			BoundTupleExpression boundTupleExpression = (BoundTupleExpression)this.Visit(node.Left);
			BoundConversion boundConversion = (BoundConversion)this.Visit(node.Right);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDeconstructionAssignmentOperator = node.Update(boundTupleExpression, boundConversion, node.IsUsed, valueTuple.Item2);
				boundDeconstructionAssignmentOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDeconstructionAssignmentOperator = node.Update(boundTupleExpression, boundConversion, node.IsUsed, node.Type);
			}
			return boundDeconstructionAssignmentOperator;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00060464 File Offset: 0x0005E664
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.LeftOperand);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.RightOperand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundNullCoalescingOperator boundNullCoalescingOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundNullCoalescingOperator = node.Update(boundExpression, boundExpression2, node.LeftConversion, node.OperatorResultKind, valueTuple.Item2);
				boundNullCoalescingOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundNullCoalescingOperator = node.Update(boundExpression, boundExpression2, node.LeftConversion, node.OperatorResultKind, node.Type);
			}
			return boundNullCoalescingOperator;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000604EC File Offset: 0x0005E6EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.LeftOperand);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.RightOperand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundNullCoalescingAssignmentOperator boundNullCoalescingAssignmentOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundNullCoalescingAssignmentOperator = node.Update(boundExpression, boundExpression2, valueTuple.Item2);
				boundNullCoalescingAssignmentOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundNullCoalescingAssignmentOperator = node.Update(boundExpression, boundExpression2, node.Type);
			}
			return boundNullCoalescingAssignmentOperator;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0006055C File Offset: 0x0005E75C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedConditionalOperator(BoundUnconvertedConditionalOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Consequence);
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.Alternative);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundUnconvertedConditionalOperator boundUnconvertedConditionalOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundUnconvertedConditionalOperator = node.Update(boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, node.NoCommonTypeError, valueTuple.Item2);
				boundUnconvertedConditionalOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundUnconvertedConditionalOperator = node.Update(boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, node.NoCommonTypeError, node.Type);
			}
			return boundUnconvertedConditionalOperator;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000605F8 File Offset: 0x0005E7F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.NaturalTypeOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Consequence);
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.Alternative);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundConditionalOperator boundConditionalOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundConditionalOperator = node.Update(node.IsRef, boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, updatedSymbol, node.WasTargetTyped, valueTuple.Item2);
				boundConditionalOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundConditionalOperator = node.Update(node.IsRef, boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, updatedSymbol, node.WasTargetTyped, node.Type);
			}
			return boundConditionalOperator;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000606B4 File Offset: 0x0005E8B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Indices);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundArrayAccess boundArrayAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundArrayAccess = node.Update(boundExpression, immutableArray, valueTuple.Item2);
				boundArrayAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundArrayAccess = node.Update(boundExpression, immutableArray, node.Type);
			}
			return boundArrayAccess;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00060720 File Offset: 0x0005E920
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayLength(BoundArrayLength node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundArrayLength boundArrayLength;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundArrayLength = node.Update(boundExpression, valueTuple.Item2);
				boundArrayLength.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundArrayLength = node.Update(boundExpression, node.Type);
			}
			return boundArrayLength;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0006077C File Offset: 0x0005E97C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableInfo(BoundAwaitableInfo node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.IsCompleted);
			MethodSymbol updatedSymbol2 = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetResult);
			BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = (BoundAwaitableValuePlaceholder)this.Visit(node.AwaitableInstancePlaceholder);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.GetAwaiter);
			return node.Update(boundAwaitableValuePlaceholder, node.IsDynamic, boundExpression, updatedSymbol, updatedSymbol2);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000607DC File Offset: 0x0005E9DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			BoundAwaitableInfo boundAwaitableInfo = (BoundAwaitableInfo)this.Visit(node.AwaitableInfo);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundAwaitExpression boundAwaitExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundAwaitExpression = node.Update(boundExpression, boundAwaitableInfo, valueTuple.Item2);
				boundAwaitExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundAwaitExpression = node.Update(boundExpression, boundAwaitableInfo, node.Type);
			}
			return boundAwaitExpression;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0006084C File Offset: 0x0005EA4C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOfOperator(BoundTypeOfOperator node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetTypeFromHandle);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.SourceType);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundTypeOfOperator boundTypeOfOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundTypeOfOperator = node.Update(boundTypeExpression, updatedSymbol, valueTuple.Item2);
				boundTypeOfOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundTypeOfOperator = node.Update(boundTypeExpression, updatedSymbol, node.Type);
			}
			return boundTypeOfOperator;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000608B8 File Offset: 0x0005EAB8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodDefIndex(BoundMethodDefIndex node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.Method);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundMethodDefIndex boundMethodDefIndex;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundMethodDefIndex = node.Update(updatedSymbol, valueTuple.Item2);
				boundMethodDefIndex.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundMethodDefIndex = node.Update(updatedSymbol, node.Type);
			}
			return boundMethodDefIndex;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00060910 File Offset: 0x0005EB10
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMaximumMethodDefIndex(BoundMaximumMethodDefIndex node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundMaximumMethodDefIndex boundMaximumMethodDefIndex = node.Update(valueTuple.Item2);
			boundMaximumMethodDefIndex.TopLevelNullability = valueTuple.Item1;
			return boundMaximumMethodDefIndex;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00060948 File Offset: 0x0005EB48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitInstrumentationPayloadRoot(BoundInstrumentationPayloadRoot node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundInstrumentationPayloadRoot boundInstrumentationPayloadRoot = node.Update(node.AnalysisKind, valueTuple.Item2);
			boundInstrumentationPayloadRoot.TopLevelNullability = valueTuple.Item1;
			return boundInstrumentationPayloadRoot;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00060988 File Offset: 0x0005EB88
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitModuleVersionId(BoundModuleVersionId node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundModuleVersionId boundModuleVersionId = node.Update(valueTuple.Item2);
			boundModuleVersionId.TopLevelNullability = valueTuple.Item1;
			return boundModuleVersionId;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000609C0 File Offset: 0x0005EBC0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitModuleVersionIdString(BoundModuleVersionIdString node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundModuleVersionIdString boundModuleVersionIdString = node.Update(valueTuple.Item2);
			boundModuleVersionIdString.TopLevelNullability = valueTuple.Item1;
			return boundModuleVersionIdString;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000609F8 File Offset: 0x0005EBF8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSourceDocumentIndex(BoundSourceDocumentIndex node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundSourceDocumentIndex boundSourceDocumentIndex = node.Update(node.Document, valueTuple.Item2);
			boundSourceDocumentIndex.TopLevelNullability = valueTuple.Item1;
			return boundSourceDocumentIndex;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00060A38 File Offset: 0x0005EC38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodInfo(BoundMethodInfo node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.Method);
			MethodSymbol updatedSymbol2 = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetMethodFromHandle);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundMethodInfo boundMethodInfo;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundMethodInfo = node.Update(updatedSymbol, updatedSymbol2, valueTuple.Item2);
				boundMethodInfo.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundMethodInfo = node.Update(updatedSymbol, updatedSymbol2, node.Type);
			}
			return boundMethodInfo;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00060AA0 File Offset: 0x0005ECA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldInfo(BoundFieldInfo node)
		{
			FieldSymbol updatedSymbol = this.GetUpdatedSymbol<FieldSymbol>(node, node.Field);
			MethodSymbol updatedSymbol2 = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetFieldFromHandle);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundFieldInfo boundFieldInfo;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundFieldInfo = node.Update(updatedSymbol, updatedSymbol2, valueTuple.Item2);
				boundFieldInfo.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundFieldInfo = node.Update(updatedSymbol, updatedSymbol2, node.Type);
			}
			return boundFieldInfo;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00060B08 File Offset: 0x0005ED08
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultLiteral(BoundDefaultLiteral node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundDefaultLiteral boundDefaultLiteral = node.Update();
			boundDefaultLiteral.TopLevelNullability = valueTuple.Item1;
			return boundDefaultLiteral;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00060B3C File Offset: 0x0005ED3C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultExpression(BoundDefaultExpression node)
		{
			BoundTypeExpression targetType = node.TargetType;
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDefaultExpression boundDefaultExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDefaultExpression = node.Update(targetType, node.ConstantValueOpt, valueTuple.Item2);
				boundDefaultExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDefaultExpression = node.Update(targetType, node.ConstantValueOpt, node.Type);
			}
			return boundDefaultExpression;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00060B98 File Offset: 0x0005ED98
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIsOperator(BoundIsOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.TargetType);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundIsOperator boundIsOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundIsOperator = node.Update(boundExpression, boundTypeExpression, node.Conversion, valueTuple.Item2);
				boundIsOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundIsOperator = node.Update(boundExpression, boundTypeExpression, node.Conversion, node.Type);
			}
			return boundIsOperator;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00060C14 File Offset: 0x0005EE14
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAsOperator(BoundAsOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.TargetType);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundAsOperator boundAsOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundAsOperator = node.Update(boundExpression, boundTypeExpression, node.Conversion, valueTuple.Item2);
				boundAsOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundAsOperator = node.Update(boundExpression, boundTypeExpression, node.Conversion, node.Type);
			}
			return boundAsOperator;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00060C90 File Offset: 0x0005EE90
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.SourceType);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundSizeOfOperator boundSizeOfOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundSizeOfOperator = node.Update(boundTypeExpression, node.ConstantValueOpt, valueTuple.Item2);
				boundSizeOfOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundSizeOfOperator = node.Update(boundTypeExpression, node.ConstantValueOpt, node.Type);
			}
			return boundSizeOfOperator;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00060CF8 File Offset: 0x0005EEF8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConversion(BoundConversion node)
		{
			this.GetUpdatedArray<MethodSymbol>(node, node.OriginalUserDefinedConversionsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundConversion boundConversion;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundConversion = node.Update(boundExpression, node.Conversion, node.IsBaseConversion, node.Checked, node.ExplicitCastInCode, node.ConstantValueOpt, node.ConversionGroupOpt, node.OriginalUserDefinedConversionsOpt, valueTuple.Item2);
				boundConversion.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundConversion = node.Update(boundExpression, node.Conversion, node.IsBaseConversion, node.Checked, node.ExplicitCastInCode, node.ConstantValueOpt, node.ConversionGroupOpt, node.OriginalUserDefinedConversionsOpt, node.Type);
			}
			return boundConversion;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00060DB4 File Offset: 0x0005EFB4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.ConversionMethod);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundReadOnlySpanFromArray boundReadOnlySpanFromArray;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundReadOnlySpanFromArray = node.Update(boundExpression, updatedSymbol, valueTuple.Item2);
				boundReadOnlySpanFromArray.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundReadOnlySpanFromArray = node.Update(boundExpression, updatedSymbol, node.Type);
			}
			return boundReadOnlySpanFromArray;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00060E20 File Offset: 0x0005F020
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgList(BoundArgList node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundArgList boundArgList = node.Update(valueTuple.Item2);
			boundArgList.TopLevelNullability = valueTuple.Item1;
			return boundArgList;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00060E58 File Offset: 0x0005F058
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundArgListOperator boundArgListOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundArgListOperator = node.Update(immutableArray, node.ArgumentRefKindsOpt, valueTuple.Item2);
				boundArgListOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundArgListOperator = node.Update(immutableArray, node.ArgumentRefKindsOpt, node.Type);
			}
			return boundArgListOperator;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00060EBC File Offset: 0x0005F0BC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.ElementPointerType);
			MethodSymbol updatedSymbol2 = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetPinnableOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundFixedLocalCollectionInitializer boundFixedLocalCollectionInitializer;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundFixedLocalCollectionInitializer = node.Update(updatedSymbol, node.ElementPointerTypeConversion, boundExpression, updatedSymbol2, valueTuple.Item2);
				boundFixedLocalCollectionInitializer.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundFixedLocalCollectionInitializer = node.Update(updatedSymbol, node.ElementPointerTypeConversion, boundExpression, updatedSymbol2, node.Type);
			}
			return boundFixedLocalCollectionInitializer;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00060F44 File Offset: 0x0005F144
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBlock(BoundBlock node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			ImmutableArray<LocalFunctionSymbol> updatedArray2 = this.GetUpdatedArray<LocalFunctionSymbol>(node, node.LocalFunctions);
			ImmutableArray<BoundStatement> immutableArray = base.VisitList<BoundStatement>(node.Statements);
			return node.Update(updatedArray, updatedArray2, immutableArray);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00060F84 File Offset: 0x0005F184
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitScope(BoundScope node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			ImmutableArray<BoundStatement> immutableArray = base.VisitList<BoundStatement>(node.Statements);
			return node.Update(updatedArray, immutableArray);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00060FB4 File Offset: 0x0005F1B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStateMachineScope(BoundStateMachineScope node)
		{
			ImmutableArray<StateMachineFieldSymbol> updatedArray = this.GetUpdatedArray<StateMachineFieldSymbol>(node, node.Fields);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Statement);
			return node.Update(updatedArray, boundStatement);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00060FEC File Offset: 0x0005F1EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			LocalSymbol updatedSymbol = this.GetUpdatedSymbol<LocalSymbol>(node, node.LocalSymbol);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredTypeOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.InitializerOpt);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.ArgumentsOpt);
			return node.Update(updatedSymbol, boundTypeExpression, boundExpression, immutableArray, node.InferredType);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00061048 File Offset: 0x0005F248
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			LocalFunctionSymbol updatedSymbol = this.GetUpdatedSymbol<LocalFunctionSymbol>(node, node.Symbol);
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.BlockBody);
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.ExpressionBody);
			return node.Update(updatedSymbol, boundBlock, boundBlock2);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00061090 File Offset: 0x0005F290
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchStatement(BoundSwitchStatement node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.InnerLocals);
			ImmutableArray<LocalFunctionSymbol> updatedArray2 = this.GetUpdatedArray<LocalFunctionSymbol>(node, node.InnerLocalFunctions);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundSwitchSection> immutableArray = base.VisitList<BoundSwitchSection>(node.SwitchSections);
			BoundDecisionDag decisionDag = node.DecisionDag;
			BoundSwitchLabel boundSwitchLabel = (BoundSwitchLabel)this.Visit(node.DefaultLabel);
			return node.Update(boundExpression, updatedArray, updatedArray2, immutableArray, decisionDag, boundSwitchLabel, node.BreakLabel);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00061108 File Offset: 0x0005F308
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.EqualityMethod);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			return node.Update(boundExpression, node.Cases, node.DefaultLabel, updatedSymbol);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0006114C File Offset: 0x0005F34C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(updatedArray, boundExpression, boundStatement, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000611A0 File Offset: 0x0005F3A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(updatedArray, boundExpression, boundStatement, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000611F4 File Offset: 0x0005F3F4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.OuterLocals);
			ImmutableArray<LocalSymbol> updatedArray2 = this.GetUpdatedArray<LocalSymbol>(node, node.InnerLocals);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Initializer);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement2 = (BoundStatement)this.Visit(node.Increment);
			BoundStatement boundStatement3 = (BoundStatement)this.Visit(node.Body);
			return node.Update(updatedArray, boundStatement, updatedArray2, boundExpression, boundStatement2, boundStatement3, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00061284 File Offset: 0x0005F484
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.IterationVariables);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.IterationVariableType);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.IterationErrorExpressionOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Expression);
			BoundForEachDeconstructStep boundForEachDeconstructStep = (BoundForEachDeconstructStep)this.Visit(node.DeconstructionOpt);
			BoundAwaitableInfo boundAwaitableInfo = (BoundAwaitableInfo)this.Visit(node.AwaitOpt);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(node.EnumeratorInfoOpt, node.ElementConversion, boundTypeExpression, updatedArray, boundExpression, boundExpression2, boundForEachDeconstructStep, boundAwaitableInfo, boundStatement, node.Checked, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0006133C File Offset: 0x0005F53C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundMultipleLocalDeclarations boundMultipleLocalDeclarations = (BoundMultipleLocalDeclarations)this.Visit(node.DeclarationsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExpressionOpt);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			BoundAwaitableInfo boundAwaitableInfo = (BoundAwaitableInfo)this.Visit(node.AwaitOpt);
			return node.Update(updatedArray, boundMultipleLocalDeclarations, boundExpression, node.IDisposableConversion, boundStatement, boundAwaitableInfo, node.PatternDisposeInfoOpt);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000613B8 File Offset: 0x0005F5B8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundMultipleLocalDeclarations boundMultipleLocalDeclarations = (BoundMultipleLocalDeclarations)this.Visit(node.Declarations);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(updatedArray, boundMultipleLocalDeclarations, boundStatement);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00061400 File Offset: 0x0005F600
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.ExceptionTypeOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExceptionSourceOpt);
			BoundStatementList boundStatementList = (BoundStatementList)this.Visit(node.ExceptionFilterPrologueOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.ExceptionFilterOpt);
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			return node.Update(updatedArray, boundExpression, updatedSymbol, boundStatementList, boundExpression2, boundBlock, node.IsSynthesizedAsyncCatchAll);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00061488 File Offset: 0x0005F688
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLiteral(BoundLiteral node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundLiteral boundLiteral = node.Update(node.ConstantValueOpt, valueTuple.Item2);
			boundLiteral.TopLevelNullability = valueTuple.Item1;
			return boundLiteral;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000614C8 File Offset: 0x0005F6C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundThisReference boundThisReference = node.Update(valueTuple.Item2);
			boundThisReference.TopLevelNullability = valueTuple.Item1;
			return boundThisReference;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00061500 File Offset: 0x0005F700
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundPreviousSubmissionReference boundPreviousSubmissionReference = node.Update(valueTuple.Item2);
			boundPreviousSubmissionReference.TopLevelNullability = valueTuple.Item1;
			return boundPreviousSubmissionReference;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00061538 File Offset: 0x0005F738
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundHostObjectMemberReference boundHostObjectMemberReference = node.Update(valueTuple.Item2);
			boundHostObjectMemberReference.TopLevelNullability = valueTuple.Item1;
			return boundHostObjectMemberReference;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00061570 File Offset: 0x0005F770
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundBaseReference boundBaseReference = node.Update(valueTuple.Item2);
			boundBaseReference.TopLevelNullability = valueTuple.Item1;
			return boundBaseReference;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000615A8 File Offset: 0x0005F7A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocal(BoundLocal node)
		{
			LocalSymbol updatedSymbol = this.GetUpdatedSymbol<LocalSymbol>(node, node.LocalSymbol);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundLocal boundLocal;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundLocal = node.Update(updatedSymbol, node.DeclarationKind, node.ConstantValueOpt, node.IsNullableUnknown, valueTuple.Item2);
				boundLocal.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundLocal = node.Update(updatedSymbol, node.DeclarationKind, node.ConstantValueOpt, node.IsNullableUnknown, node.Type);
			}
			return boundLocal;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00061624 File Offset: 0x0005F824
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPseudoVariable(BoundPseudoVariable node)
		{
			LocalSymbol updatedSymbol = this.GetUpdatedSymbol<LocalSymbol>(node, node.LocalSymbol);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundPseudoVariable boundPseudoVariable;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundPseudoVariable = node.Update(updatedSymbol, node.EmitExpressions, valueTuple.Item2);
				boundPseudoVariable.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundPseudoVariable = node.Update(updatedSymbol, node.EmitExpressions, node.Type);
			}
			return boundPseudoVariable;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00061688 File Offset: 0x0005F888
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			RangeVariableSymbol updatedSymbol = this.GetUpdatedSymbol<RangeVariableSymbol>(node, node.RangeVariableSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundRangeVariable boundRangeVariable;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundRangeVariable = node.Update(updatedSymbol, boundExpression, valueTuple.Item2);
				boundRangeVariable.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundRangeVariable = node.Update(updatedSymbol, boundExpression, node.Type);
			}
			return boundRangeVariable;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000616F4 File Offset: 0x0005F8F4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameter(BoundParameter node)
		{
			ParameterSymbol updatedSymbol = this.GetUpdatedSymbol<ParameterSymbol>(node, node.ParameterSymbol);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundParameter boundParameter;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundParameter = node.Update(updatedSymbol, valueTuple.Item2);
				boundParameter.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundParameter = node.Update(updatedSymbol, node.Type);
			}
			return boundParameter;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0006174C File Offset: 0x0005F94C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabel(BoundLabel node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundLabel boundLabel = node.Update(node.Label, valueTuple.Item2);
			boundLabel.TopLevelNullability = valueTuple.Item1;
			return boundLabel;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0006178C File Offset: 0x0005F98C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchExpressionArm(BoundSwitchExpressionArm node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Pattern);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.WhenClause);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Value);
			return node.Update(updatedArray, boundPattern, boundExpression, boundExpression2, node.Label);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000617F0 File Offset: 0x0005F9F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundSwitchExpressionArm> immutableArray = base.VisitList<BoundSwitchExpressionArm>(node.SwitchArms);
			BoundDecisionDag decisionDag = node.DecisionDag;
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundUnconvertedSwitchExpression boundUnconvertedSwitchExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundUnconvertedSwitchExpression = node.Update(boundExpression, immutableArray, decisionDag, node.DefaultLabel, node.ReportedNotExhaustive, valueTuple.Item2);
				boundUnconvertedSwitchExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundUnconvertedSwitchExpression = node.Update(boundExpression, immutableArray, decisionDag, node.DefaultLabel, node.ReportedNotExhaustive, node.Type);
			}
			return boundUnconvertedSwitchExpression;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0006187C File Offset: 0x0005FA7C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.NaturalTypeOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundSwitchExpressionArm> immutableArray = base.VisitList<BoundSwitchExpressionArm>(node.SwitchArms);
			BoundDecisionDag decisionDag = node.DecisionDag;
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundConvertedSwitchExpression boundConvertedSwitchExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundConvertedSwitchExpression = node.Update(updatedSymbol, node.WasTargetTyped, node.Conversion, boundExpression, immutableArray, decisionDag, node.DefaultLabel, node.ReportedNotExhaustive, valueTuple.Item2);
				boundConvertedSwitchExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundConvertedSwitchExpression = node.Update(updatedSymbol, node.WasTargetTyped, node.Conversion, boundExpression, immutableArray, decisionDag, node.DefaultLabel, node.ReportedNotExhaustive, node.Type);
			}
			return boundConvertedSwitchExpression;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00061934 File Offset: 0x0005FB34
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagDeconstructEvaluation(BoundDagDeconstructEvaluation node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.DeconstructMethod);
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(updatedSymbol, boundDagTemp);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0006196C File Offset: 0x0005FB6C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagFieldEvaluation(BoundDagFieldEvaluation node)
		{
			FieldSymbol updatedSymbol = this.GetUpdatedSymbol<FieldSymbol>(node, node.Field);
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(updatedSymbol, boundDagTemp);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000619A4 File Offset: 0x0005FBA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagPropertyEvaluation(BoundDagPropertyEvaluation node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.Property);
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(updatedSymbol, boundDagTemp);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000619DC File Offset: 0x0005FBDC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagIndexEvaluation(BoundDagIndexEvaluation node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.Property);
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(updatedSymbol, node.Index, boundDagTemp);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00061A18 File Offset: 0x0005FC18
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchSection(BoundSwitchSection node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			ImmutableArray<BoundSwitchLabel> immutableArray = base.VisitList<BoundSwitchLabel>(node.SwitchLabels);
			ImmutableArray<BoundStatement> immutableArray2 = base.VisitList<BoundStatement>(node.Statements);
			return node.Update(updatedArray, immutableArray, immutableArray2);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00061A58 File Offset: 0x0005FC58
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequencePointExpression(BoundSequencePointExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundSequencePointExpression boundSequencePointExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundSequencePointExpression = node.Update(boundExpression, valueTuple.Item2);
				boundSequencePointExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundSequencePointExpression = node.Update(boundExpression, node.Type);
			}
			return boundSequencePointExpression;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00061AB4 File Offset: 0x0005FCB4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequence(BoundSequence node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.SideEffects);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundSequence boundSequence;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundSequence = node.Update(updatedArray, immutableArray, boundExpression, valueTuple.Item2);
				boundSequence.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundSequence = node.Update(updatedArray, immutableArray, boundExpression, node.Type);
			}
			return boundSequence;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00061B30 File Offset: 0x0005FD30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSpillSequence(BoundSpillSequence node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			ImmutableArray<BoundStatement> immutableArray = base.VisitList<BoundStatement>(node.SideEffects);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundSpillSequence boundSpillSequence;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundSpillSequence = node.Update(updatedArray, immutableArray, boundExpression, valueTuple.Item2);
				boundSpillSequence.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundSpillSequence = node.Update(updatedArray, immutableArray, boundExpression, node.Type);
			}
			return boundSpillSequence;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00061BAC File Offset: 0x0005FDAC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDynamicMemberAccess boundDynamicMemberAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDynamicMemberAccess = node.Update(boundExpression, node.TypeArgumentsOpt, node.Name, node.Invoked, node.Indexed, valueTuple.Item2);
				boundDynamicMemberAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDynamicMemberAccess = node.Update(boundExpression, node.TypeArgumentsOpt, node.Name, node.Invoked, node.Indexed, node.Type);
			}
			return boundDynamicMemberAccess;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00061C38 File Offset: 0x0005FE38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			ImmutableArray<MethodSymbol> updatedArray = this.GetUpdatedArray<MethodSymbol>(node, node.ApplicableMethods);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDynamicInvocation boundDynamicInvocation;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDynamicInvocation = node.Update(node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, updatedArray, boundExpression, immutableArray, valueTuple.Item2);
				boundDynamicInvocation.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDynamicInvocation = node.Update(node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, updatedArray, boundExpression, immutableArray, node.Type);
			}
			return boundDynamicInvocation;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00061CCC File Offset: 0x0005FECC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.AccessExpression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundConditionalAccess boundConditionalAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundConditionalAccess = node.Update(boundExpression, boundExpression2, valueTuple.Item2);
				boundConditionalAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundConditionalAccess = node.Update(boundExpression, boundExpression2, node.Type);
			}
			return boundConditionalAccess;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00061D3C File Offset: 0x0005FF3C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.HasValueMethodOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.WhenNotNull);
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.WhenNullOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundLoweredConditionalAccess boundLoweredConditionalAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundLoweredConditionalAccess = node.Update(boundExpression, updatedSymbol, boundExpression2, boundExpression3, node.Id, valueTuple.Item2);
				boundLoweredConditionalAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundLoweredConditionalAccess = node.Update(boundExpression, updatedSymbol, boundExpression2, boundExpression3, node.Id, node.Type);
			}
			return boundLoweredConditionalAccess;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00061DE0 File Offset: 0x0005FFE0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundConditionalReceiver boundConditionalReceiver = node.Update(node.Id, valueTuple.Item2);
			boundConditionalReceiver.TopLevelNullability = valueTuple.Item1;
			return boundConditionalReceiver;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00061E20 File Offset: 0x00060020
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ValueTypeReceiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.ReferenceTypeReceiver);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundComplexConditionalReceiver boundComplexConditionalReceiver;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundComplexConditionalReceiver = node.Update(boundExpression, boundExpression2, valueTuple.Item2);
				boundComplexConditionalReceiver.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundComplexConditionalReceiver = node.Update(boundExpression, boundExpression2, node.Type);
			}
			return boundComplexConditionalReceiver;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00061E90 File Offset: 0x00060090
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			ImmutableArray<MethodSymbol> updatedArray = this.GetUpdatedArray<MethodSymbol>(node, node.Methods);
			Symbol updatedSymbol = this.GetUpdatedSymbol<Symbol>(node, node.LookupSymbolOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundMethodGroup boundMethodGroup;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundMethodGroup = node.Update(node.TypeArgumentsOpt, node.Name, updatedArray, updatedSymbol, node.LookupError, node.Flags, boundExpression, node.ResultKind);
				boundMethodGroup.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundMethodGroup = node.Update(node.TypeArgumentsOpt, node.Name, updatedArray, updatedSymbol, node.LookupError, node.Flags, boundExpression, node.ResultKind);
			}
			return boundMethodGroup;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00061F3C File Offset: 0x0006013C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyGroup(BoundPropertyGroup node)
		{
			ImmutableArray<PropertySymbol> updatedArray = this.GetUpdatedArray<PropertySymbol>(node, node.Properties);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundPropertyGroup boundPropertyGroup;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundPropertyGroup = node.Update(updatedArray, boundExpression, node.ResultKind);
				boundPropertyGroup.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundPropertyGroup = node.Update(updatedArray, boundExpression, node.ResultKind);
			}
			return boundPropertyGroup;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00061FA8 File Offset: 0x000601A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCall(BoundCall node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.Method);
			this.GetUpdatedArray<MethodSymbol>(node, node.OriginalMethodsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundCall boundCall;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundCall = node.Update(boundExpression, updatedSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.IsDelegateCall, node.Expanded, node.InvokedAsExtensionMethod, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, node.OriginalMethodsOpt, valueTuple.Item2);
				boundCall.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundCall = node.Update(boundExpression, updatedSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.IsDelegateCall, node.Expanded, node.InvokedAsExtensionMethod, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, node.OriginalMethodsOpt, node.Type);
			}
			return boundCall;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000620A0 File Offset: 0x000602A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			EventSymbol updatedSymbol = this.GetUpdatedSymbol<EventSymbol>(node, node.Event);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Argument);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundEventAssignmentOperator boundEventAssignmentOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundEventAssignmentOperator = node.Update(updatedSymbol, node.IsAddition, node.IsDynamic, boundExpression, boundExpression2, valueTuple.Item2);
				boundEventAssignmentOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundEventAssignmentOperator = node.Update(updatedSymbol, node.IsAddition, node.IsDynamic, boundExpression, boundExpression2, node.Type);
			}
			return boundEventAssignmentOperator;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00062138 File Offset: 0x00060338
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAttribute(BoundAttribute node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.Constructor);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.ConstructorArguments);
			ImmutableArray<BoundAssignmentOperator> immutableArray2 = base.VisitList<BoundAssignmentOperator>(node.NamedArguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundAttribute boundAttribute;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundAttribute = node.Update(updatedSymbol, immutableArray, node.ConstructorArgumentNamesOpt, node.ConstructorArgumentsToParamsOpt, node.ConstructorExpanded, immutableArray2, node.ResultKind, valueTuple.Item2);
				boundAttribute.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundAttribute = node.Update(updatedSymbol, immutableArray, node.ConstructorArgumentNamesOpt, node.ConstructorArgumentsToParamsOpt, node.ConstructorExpanded, immutableArray2, node.ResultKind, node.Type);
			}
			return boundAttribute;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000621E0 File Offset: 0x000603E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundUnconvertedObjectCreationExpression boundUnconvertedObjectCreationExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundUnconvertedObjectCreationExpression = node.Update(immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.InitializerOpt);
				boundUnconvertedObjectCreationExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundUnconvertedObjectCreationExpression = node.Update(immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.InitializerOpt);
			}
			return boundUnconvertedObjectCreationExpression;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00062250 File Offset: 0x00060450
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.Constructor);
			ImmutableArray<MethodSymbol> updatedArray = this.GetUpdatedArray<MethodSymbol>(node, node.ConstructorsGroup);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundObjectCreationExpression boundObjectCreationExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundObjectCreationExpression = node.Update(updatedSymbol, updatedArray, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ConstantValueOpt, boundObjectInitializerExpressionBase, node.WasTargetTyped, valueTuple.Item2);
				boundObjectCreationExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundObjectCreationExpression = node.Update(updatedSymbol, updatedArray, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ConstantValueOpt, boundObjectInitializerExpressionBase, node.WasTargetTyped, node.Type);
			}
			return boundObjectCreationExpression;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00062334 File Offset: 0x00060534
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundTupleLiteral boundTupleLiteral;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundTupleLiteral = node.Update(immutableArray, node.ArgumentNamesOpt, node.InferredNamesOpt, valueTuple.Item2);
				boundTupleLiteral.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundTupleLiteral = node.Update(immutableArray, node.ArgumentNamesOpt, node.InferredNamesOpt, node.Type);
			}
			return boundTupleLiteral;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000623A4 File Offset: 0x000605A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			BoundTupleLiteral boundTupleLiteral = (BoundTupleLiteral)this.Visit(node.SourceTuple);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundConvertedTupleLiteral boundConvertedTupleLiteral;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundConvertedTupleLiteral = node.Update(boundTupleLiteral, node.WasTargetTyped, immutableArray, node.ArgumentNamesOpt, node.InferredNamesOpt, valueTuple.Item2);
				boundConvertedTupleLiteral.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundConvertedTupleLiteral = node.Update(boundTupleLiteral, node.WasTargetTyped, immutableArray, node.ArgumentNamesOpt, node.InferredNamesOpt, node.Type);
			}
			return boundConvertedTupleLiteral;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00062434 File Offset: 0x00060634
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			ImmutableArray<MethodSymbol> updatedArray = this.GetUpdatedArray<MethodSymbol>(node, node.ApplicableMethods);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDynamicObjectCreationExpression boundDynamicObjectCreationExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDynamicObjectCreationExpression = node.Update(node.Name, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, boundObjectInitializerExpressionBase, updatedArray, valueTuple.Item2);
				boundDynamicObjectCreationExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDynamicObjectCreationExpression = node.Update(node.Name, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, boundObjectInitializerExpressionBase, updatedArray, node.Type);
			}
			return boundDynamicObjectCreationExpression;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000624D4 File Offset: 0x000606D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node)
		{
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundNoPiaObjectCreationExpression boundNoPiaObjectCreationExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundNoPiaObjectCreationExpression = node.Update(node.GuidString, boundObjectInitializerExpressionBase, valueTuple.Item2);
				boundNoPiaObjectCreationExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundNoPiaObjectCreationExpression = node.Update(node.GuidString, boundObjectInitializerExpressionBase, node.Type);
			}
			return boundNoPiaObjectCreationExpression;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0006253C File Offset: 0x0006073C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerExpression(BoundObjectInitializerExpression node)
		{
			BoundObjectOrCollectionValuePlaceholder boundObjectOrCollectionValuePlaceholder = (BoundObjectOrCollectionValuePlaceholder)this.Visit(node.Placeholder);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Initializers);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundObjectInitializerExpression boundObjectInitializerExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundObjectInitializerExpression = node.Update(boundObjectOrCollectionValuePlaceholder, immutableArray, valueTuple.Item2);
				boundObjectInitializerExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundObjectInitializerExpression = node.Update(boundObjectOrCollectionValuePlaceholder, immutableArray, node.Type);
			}
			return boundObjectInitializerExpression;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000625A8 File Offset: 0x000607A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			Symbol updatedSymbol = this.GetUpdatedSymbol<Symbol>(node, node.MemberSymbol);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.ReceiverType);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundObjectInitializerMember boundObjectInitializerMember;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundObjectInitializerMember = node.Update(updatedSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, updatedSymbol2, valueTuple.Item2);
				boundObjectInitializerMember.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundObjectInitializerMember = node.Update(updatedSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, updatedSymbol2, node.Type);
			}
			return boundObjectInitializerMember;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00062668 File Offset: 0x00060868
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.ReceiverType);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDynamicObjectInitializerMember boundDynamicObjectInitializerMember;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDynamicObjectInitializerMember = node.Update(node.MemberName, updatedSymbol, valueTuple.Item2);
				boundDynamicObjectInitializerMember.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDynamicObjectInitializerMember = node.Update(node.MemberName, updatedSymbol, node.Type);
			}
			return boundDynamicObjectInitializerMember;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000626CC File Offset: 0x000608CC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node)
		{
			BoundObjectOrCollectionValuePlaceholder boundObjectOrCollectionValuePlaceholder = (BoundObjectOrCollectionValuePlaceholder)this.Visit(node.Placeholder);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Initializers);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundCollectionInitializerExpression boundCollectionInitializerExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundCollectionInitializerExpression = node.Update(boundObjectOrCollectionValuePlaceholder, immutableArray, valueTuple.Item2);
				boundCollectionInitializerExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundCollectionInitializerExpression = node.Update(boundObjectOrCollectionValuePlaceholder, immutableArray, node.Type);
			}
			return boundCollectionInitializerExpression;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00062738 File Offset: 0x00060938
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCollectionElementInitializer(BoundCollectionElementInitializer node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.AddMethod);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ImplicitReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundCollectionElementInitializer boundCollectionElementInitializer;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundCollectionElementInitializer = node.Update(updatedSymbol, immutableArray, boundExpression, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.InvokedAsExtensionMethod, node.ResultKind, valueTuple.Item2);
				boundCollectionElementInitializer.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundCollectionElementInitializer = node.Update(updatedSymbol, immutableArray, boundExpression, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.InvokedAsExtensionMethod, node.ResultKind, node.Type);
			}
			return boundCollectionElementInitializer;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000627F0 File Offset: 0x000609F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node)
		{
			ImmutableArray<MethodSymbol> updatedArray = this.GetUpdatedArray<MethodSymbol>(node, node.ApplicableMethods);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDynamicCollectionElementInitializer boundDynamicCollectionElementInitializer;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDynamicCollectionElementInitializer = node.Update(updatedArray, boundExpression, immutableArray, valueTuple.Item2);
				boundDynamicCollectionElementInitializer.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDynamicCollectionElementInitializer = node.Update(updatedArray, boundExpression, immutableArray, node.Type);
			}
			return boundDynamicCollectionElementInitializer;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0006286C File Offset: 0x00060A6C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitImplicitReceiver(BoundImplicitReceiver node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundImplicitReceiver boundImplicitReceiver = node.Update(valueTuple.Item2);
			boundImplicitReceiver.TopLevelNullability = valueTuple.Item1;
			return boundImplicitReceiver;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000628A4 File Offset: 0x00060AA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.Constructor);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ImmutableArray<BoundAnonymousPropertyDeclaration> immutableArray2 = base.VisitList<BoundAnonymousPropertyDeclaration>(node.Declarations);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundAnonymousObjectCreationExpression boundAnonymousObjectCreationExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundAnonymousObjectCreationExpression = node.Update(updatedSymbol, immutableArray, immutableArray2, valueTuple.Item2);
				boundAnonymousObjectCreationExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundAnonymousObjectCreationExpression = node.Update(updatedSymbol, immutableArray, immutableArray2, node.Type);
			}
			return boundAnonymousObjectCreationExpression;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0006291C File Offset: 0x00060B1C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousPropertyDeclaration(BoundAnonymousPropertyDeclaration node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.Property);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundAnonymousPropertyDeclaration boundAnonymousPropertyDeclaration;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundAnonymousPropertyDeclaration = node.Update(updatedSymbol, valueTuple.Item2);
				boundAnonymousPropertyDeclaration.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundAnonymousPropertyDeclaration = node.Update(updatedSymbol, node.Type);
			}
			return boundAnonymousPropertyDeclaration;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00062974 File Offset: 0x00060B74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNewT(BoundNewT node)
		{
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundNewT boundNewT;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundNewT = node.Update(boundObjectInitializerExpressionBase, valueTuple.Item2);
				boundNewT.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundNewT = node.Update(boundObjectInitializerExpressionBase, node.Type);
			}
			return boundNewT;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000629D0 File Offset: 0x00060BD0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.MethodOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Argument);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDelegateCreationExpression boundDelegateCreationExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDelegateCreationExpression = node.Update(boundExpression, updatedSymbol, node.IsExtensionMethod, valueTuple.Item2);
				boundDelegateCreationExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDelegateCreationExpression = node.Update(boundExpression, updatedSymbol, node.IsExtensionMethod, node.Type);
			}
			return boundDelegateCreationExpression;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00062A48 File Offset: 0x00060C48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Bounds);
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.Visit(node.InitializerOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundArrayCreation boundArrayCreation;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundArrayCreation = node.Update(immutableArray, boundArrayInitialization, valueTuple.Item2);
				boundArrayCreation.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundArrayCreation = node.Update(immutableArray, boundArrayInitialization, node.Type);
			}
			return boundArrayCreation;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00062AB4 File Offset: 0x00060CB4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Initializers);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundArrayInitialization boundArrayInitialization;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundArrayInitialization = node.Update(immutableArray);
				boundArrayInitialization.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundArrayInitialization = node.Update(immutableArray);
			}
			return boundArrayInitialization;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00062B00 File Offset: 0x00060D00
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.ElementType);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Count);
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.Visit(node.InitializerOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundStackAllocArrayCreation boundStackAllocArrayCreation;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundStackAllocArrayCreation = node.Update(updatedSymbol, boundExpression, boundArrayInitialization, valueTuple.Item2);
				boundStackAllocArrayCreation.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundStackAllocArrayCreation = node.Update(updatedSymbol, boundExpression, boundArrayInitialization, node.Type);
			}
			return boundStackAllocArrayCreation;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00062B80 File Offset: 0x00060D80
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.ElementType);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Count);
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.Visit(node.InitializerOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundConvertedStackAllocExpression boundConvertedStackAllocExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundConvertedStackAllocExpression = node.Update(updatedSymbol, boundExpression, boundArrayInitialization, valueTuple.Item2);
				boundConvertedStackAllocExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundConvertedStackAllocExpression = node.Update(updatedSymbol, boundExpression, boundArrayInitialization, node.Type);
			}
			return boundConvertedStackAllocExpression;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00062C00 File Offset: 0x00060E00
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			FieldSymbol updatedSymbol = this.GetUpdatedSymbol<FieldSymbol>(node, node.FieldSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundFieldAccess boundFieldAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundFieldAccess = node.Update(boundExpression, updatedSymbol, node.ConstantValueOpt, node.ResultKind, node.IsByValue, node.IsDeclaration, valueTuple.Item2);
				boundFieldAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundFieldAccess = node.Update(boundExpression, updatedSymbol, node.ConstantValueOpt, node.ResultKind, node.IsByValue, node.IsDeclaration, node.Type);
			}
			return boundFieldAccess;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00062C9C File Offset: 0x00060E9C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitHoistedFieldAccess(BoundHoistedFieldAccess node)
		{
			FieldSymbol updatedSymbol = this.GetUpdatedSymbol<FieldSymbol>(node, node.FieldSymbol);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundHoistedFieldAccess boundHoistedFieldAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundHoistedFieldAccess = node.Update(updatedSymbol, valueTuple.Item2);
				boundHoistedFieldAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundHoistedFieldAccess = node.Update(updatedSymbol, node.Type);
			}
			return boundHoistedFieldAccess;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00062CF4 File Offset: 0x00060EF4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.PropertySymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundPropertyAccess boundPropertyAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundPropertyAccess = node.Update(boundExpression, updatedSymbol, node.ResultKind, valueTuple.Item2);
				boundPropertyAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundPropertyAccess = node.Update(boundExpression, updatedSymbol, node.ResultKind, node.Type);
			}
			return boundPropertyAccess;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00062D6C File Offset: 0x00060F6C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			EventSymbol updatedSymbol = this.GetUpdatedSymbol<EventSymbol>(node, node.EventSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundEventAccess boundEventAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundEventAccess = node.Update(boundExpression, updatedSymbol, node.IsUsableAsField, node.ResultKind, valueTuple.Item2);
				boundEventAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundEventAccess = node.Update(boundExpression, updatedSymbol, node.IsUsableAsField, node.ResultKind, node.Type);
			}
			return boundEventAccess;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00062DF0 File Offset: 0x00060FF0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.Indexer);
			this.GetUpdatedArray<PropertySymbol>(node, node.OriginalIndexersOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundIndexerAccess boundIndexerAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundIndexerAccess = node.Update(boundExpression, updatedSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.OriginalIndexersOpt, valueTuple.Item2);
				boundIndexerAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundIndexerAccess = node.Update(boundExpression, updatedSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.OriginalIndexersOpt, node.Type);
			}
			return boundIndexerAccess;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00062EC4 File Offset: 0x000610C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			PropertySymbol updatedSymbol = this.GetUpdatedSymbol<PropertySymbol>(node, node.LengthOrCountProperty);
			Symbol updatedSymbol2 = this.GetUpdatedSymbol<Symbol>(node, node.PatternSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Argument);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundIndexOrRangePatternIndexerAccess boundIndexOrRangePatternIndexerAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundIndexOrRangePatternIndexerAccess = node.Update(boundExpression, updatedSymbol, updatedSymbol2, boundExpression2, valueTuple.Item2);
				boundIndexOrRangePatternIndexerAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundIndexOrRangePatternIndexerAccess = node.Update(boundExpression, updatedSymbol, updatedSymbol2, boundExpression2, node.Type);
			}
			return boundIndexOrRangePatternIndexerAccess;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00062F58 File Offset: 0x00061158
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			ImmutableArray<PropertySymbol> updatedArray = this.GetUpdatedArray<PropertySymbol>(node, node.ApplicableIndexers);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundDynamicIndexerAccess boundDynamicIndexerAccess;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundDynamicIndexerAccess = node.Update(boundExpression, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, updatedArray, valueTuple.Item2);
				boundDynamicIndexerAccess.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundDynamicIndexerAccess = node.Update(boundExpression, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, updatedArray, node.Type);
			}
			return boundDynamicIndexerAccess;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00062FEC File Offset: 0x000611EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLambda(BoundLambda node)
		{
			LambdaSymbol updatedSymbol = this.GetUpdatedSymbol<LambdaSymbol>(node, node.Symbol);
			UnboundLambda unboundLambda = node.UnboundLambda;
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundLambda boundLambda;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundLambda = node.Update(unboundLambda, updatedSymbol, boundBlock, node.Diagnostics, node.Binder, valueTuple.Item2);
				boundLambda.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundLambda = node.Update(unboundLambda, updatedSymbol, boundBlock, node.Diagnostics, node.Binder, node.Type);
			}
			return boundLambda;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0006307C File Offset: 0x0006127C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnboundLambda(UnboundLambda node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			UnboundLambda unboundLambda = node.Update(node.Data, node.WithDependencies);
			unboundLambda.TopLevelNullability = valueTuple.Item1;
			return unboundLambda;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000630BC File Offset: 0x000612BC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			RangeVariableSymbol updatedSymbol = this.GetUpdatedSymbol<RangeVariableSymbol>(node, node.DefinedSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			BoundExpression operation = node.Operation;
			BoundExpression cast = node.Cast;
			BoundExpression unoptimizedForm = node.UnoptimizedForm;
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundQueryClause boundQueryClause;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundQueryClause = node.Update(boundExpression, updatedSymbol, operation, cast, node.Binder, unoptimizedForm, valueTuple.Item2);
				boundQueryClause.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundQueryClause = node.Update(boundExpression, updatedSymbol, operation, cast, node.Binder, unoptimizedForm, node.Type);
			}
			return boundQueryClause;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00063158 File Offset: 0x00061358
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNameOfOperator(BoundNameOfOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Argument);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundNameOfOperator boundNameOfOperator;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundNameOfOperator = node.Update(boundExpression, node.ConstantValueOpt, valueTuple.Item2);
				boundNameOfOperator.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundNameOfOperator = node.Update(boundExpression, node.ConstantValueOpt, node.Type);
			}
			return boundNameOfOperator;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000631C0 File Offset: 0x000613C0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Parts);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundUnconvertedInterpolatedString boundUnconvertedInterpolatedString;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundUnconvertedInterpolatedString = node.Update(immutableArray, node.ConstantValueOpt, valueTuple.Item2);
				boundUnconvertedInterpolatedString.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundUnconvertedInterpolatedString = node.Update(immutableArray, node.ConstantValueOpt, node.Type);
			}
			return boundUnconvertedInterpolatedString;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00063224 File Offset: 0x00061424
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitInterpolatedString(BoundInterpolatedString node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Parts);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundInterpolatedString boundInterpolatedString;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundInterpolatedString = node.Update(immutableArray, node.ConstantValueOpt, valueTuple.Item2);
				boundInterpolatedString.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundInterpolatedString = node.Update(immutableArray, node.ConstantValueOpt, node.Type);
			}
			return boundInterpolatedString;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00063288 File Offset: 0x00061488
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStringInsert(BoundStringInsert node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Alignment);
			BoundLiteral boundLiteral = (BoundLiteral)this.Visit(node.Format);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundStringInsert boundStringInsert;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundStringInsert = node.Update(boundExpression, boundExpression2, boundLiteral, valueTuple.Item2);
				boundStringInsert.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundStringInsert = node.Update(boundExpression, boundExpression2, boundLiteral, node.Type);
			}
			return boundStringInsert;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0006330C File Offset: 0x0006150C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Pattern);
			BoundDecisionDag decisionDag = node.DecisionDag;
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundIsPatternExpression boundIsPatternExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundIsPatternExpression = node.Update(boundExpression, boundPattern, node.IsNegated, decisionDag, node.WhenTrueLabel, node.WhenFalseLabel, valueTuple.Item2);
				boundIsPatternExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundIsPatternExpression = node.Update(boundExpression, boundPattern, node.IsNegated, decisionDag, node.WhenTrueLabel, node.WhenFalseLabel, node.Type);
			}
			return boundIsPatternExpression;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000633AC File Offset: 0x000615AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConstantPattern(BoundConstantPattern node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(boundExpression, node.ConstantValue, updatedSymbol, updatedSymbol2);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000633F8 File Offset: 0x000615F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDiscardPattern(BoundDiscardPattern node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			return node.Update(updatedSymbol, updatedSymbol2);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0006342C File Offset: 0x0006162C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeclarationPattern(BoundDeclarationPattern node)
		{
			Symbol updatedSymbol = this.GetUpdatedSymbol<Symbol>(node, node.Variable);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol3 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.VariableAccess);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredType);
			return node.Update(updatedSymbol, boundExpression, boundTypeExpression, node.IsVar, updatedSymbol2, updatedSymbol3);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0006349C File Offset: 0x0006169C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRecursivePattern(BoundRecursivePattern node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.DeconstructMethod);
			Symbol updatedSymbol2 = this.GetUpdatedSymbol<Symbol>(node, node.Variable);
			TypeSymbol updatedSymbol3 = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol4 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredType);
			ImmutableArray<BoundSubpattern> immutableArray = base.VisitList<BoundSubpattern>(node.Deconstruction);
			ImmutableArray<BoundSubpattern> immutableArray2 = base.VisitList<BoundSubpattern>(node.Properties);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.VariableAccess);
			return node.Update(boundTypeExpression, updatedSymbol, immutableArray, immutableArray2, updatedSymbol2, boundExpression, node.IsExplicitNotNullTest, updatedSymbol3, updatedSymbol4);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0006353C File Offset: 0x0006173C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitITuplePattern(BoundITuplePattern node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetLengthMethod);
			MethodSymbol updatedSymbol2 = this.GetUpdatedSymbol<MethodSymbol>(node, node.GetItemMethod);
			TypeSymbol updatedSymbol3 = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol4 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			ImmutableArray<BoundSubpattern> immutableArray = base.VisitList<BoundSubpattern>(node.Subpatterns);
			return node.Update(updatedSymbol, updatedSymbol2, immutableArray, updatedSymbol3, updatedSymbol4);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0006359C File Offset: 0x0006179C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSubpattern(BoundSubpattern node)
		{
			Symbol updatedSymbol = this.GetUpdatedSymbol<Symbol>(node, node.Symbol);
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Pattern);
			return node.Update(updatedSymbol, boundPattern);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000635D4 File Offset: 0x000617D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypePattern(BoundTypePattern node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredType);
			return node.Update(boundTypeExpression, node.IsExplicitNotNullTest, updatedSymbol, updatedSymbol2);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00063620 File Offset: 0x00061820
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBinaryPattern(BoundBinaryPattern node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Left);
			BoundPattern boundPattern2 = (BoundPattern)this.Visit(node.Right);
			return node.Update(node.Disjunction, boundPattern, boundPattern2, updatedSymbol, updatedSymbol2);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00063680 File Offset: 0x00061880
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNegatedPattern(BoundNegatedPattern node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Negated);
			return node.Update(boundPattern, updatedSymbol, updatedSymbol2);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000636C4 File Offset: 0x000618C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRelationalPattern(BoundRelationalPattern node)
		{
			TypeSymbol updatedSymbol = this.GetUpdatedSymbol<TypeSymbol>(node, node.InputType);
			TypeSymbol updatedSymbol2 = this.GetUpdatedSymbol<TypeSymbol>(node, node.NarrowedType);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(node.Relation, boundExpression, node.ConstantValue, updatedSymbol, updatedSymbol2);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00063714 File Offset: 0x00061914
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDiscardExpression(BoundDiscardExpression node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			BoundDiscardExpression boundDiscardExpression = node.Update(valueTuple.Item2);
			boundDiscardExpression.TopLevelNullability = valueTuple.Item1;
			return boundDiscardExpression;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0006374C File Offset: 0x0006194C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundThrowExpression boundThrowExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundThrowExpression = node.Update(boundExpression, valueTuple.Item2);
				boundThrowExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundThrowExpression = node.Update(boundExpression, node.Type);
			}
			return boundThrowExpression;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000637A8 File Offset: 0x000619A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitOutVariablePendingInference(OutVariablePendingInference node)
		{
			Symbol updatedSymbol = this.GetUpdatedSymbol<Symbol>(node, node.VariableSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			OutVariablePendingInference outVariablePendingInference;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				outVariablePendingInference = node.Update(updatedSymbol, boundExpression);
				outVariablePendingInference.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				outVariablePendingInference = node.Update(updatedSymbol, boundExpression);
			}
			return outVariablePendingInference;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00063808 File Offset: 0x00061A08
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node)
		{
			Symbol updatedSymbol = this.GetUpdatedSymbol<Symbol>(node, node.VariableSymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			DeconstructionVariablePendingInference deconstructionVariablePendingInference;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				deconstructionVariablePendingInference = node.Update(updatedSymbol, boundExpression);
				deconstructionVariablePendingInference.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				deconstructionVariablePendingInference = node.Update(updatedSymbol, boundExpression);
			}
			return deconstructionVariablePendingInference;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00063868 File Offset: 0x00061A68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node)
		{
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			if (!this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				return node;
			}
			OutDeconstructVarPendingInference outDeconstructVarPendingInference = node.Update();
			outDeconstructVarPendingInference.TopLevelNullability = valueTuple.Item1;
			return outDeconstructVarPendingInference;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0006389C File Offset: 0x00061A9C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConstructorMethodBody(BoundConstructorMethodBody node)
		{
			ImmutableArray<LocalSymbol> updatedArray = this.GetUpdatedArray<LocalSymbol>(node, node.Locals);
			BoundExpressionStatement boundExpressionStatement = (BoundExpressionStatement)this.Visit(node.Initializer);
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.BlockBody);
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.ExpressionBody);
			return node.Update(updatedArray, boundExpressionStatement, boundBlock, boundBlock2);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000638F8 File Offset: 0x00061AF8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExpressionWithNullability(BoundExpressionWithNullability node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundExpressionWithNullability boundExpressionWithNullability;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundExpressionWithNullability = node.Update(boundExpression, node.NullableAnnotation, valueTuple.Item2);
				boundExpressionWithNullability.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundExpressionWithNullability = node.Update(boundExpression, node.NullableAnnotation, node.Type);
			}
			return boundExpressionWithNullability;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00063960 File Offset: 0x00061B60
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWithExpression(BoundWithExpression node)
		{
			MethodSymbol updatedSymbol = this.GetUpdatedSymbol<MethodSymbol>(node, node.CloneMethod);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpression);
			ValueTuple<NullabilityInfo, TypeSymbol> valueTuple;
			BoundWithExpression boundWithExpression;
			if (this._updatedNullabilities.TryGetValue(node, out valueTuple))
			{
				boundWithExpression = node.Update(boundExpression, updatedSymbol, boundObjectInitializerExpressionBase, valueTuple.Item2);
				boundWithExpression.TopLevelNullability = valueTuple.Item1;
			}
			else
			{
				boundWithExpression = node.Update(boundExpression, updatedSymbol, boundObjectInitializerExpressionBase, node.Type);
			}
			return boundWithExpression;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000639E0 File Offset: 0x00061BE0
		[CompilerGenerated]
		private Symbol <GetUpdatedSymbol>g__remapLambda|4_0<T>(BoundLambda boundLambda, LambdaSymbol lambda) where T : Symbol
		{
			NullableWalker.SnapshotManager snapshotManager = this._snapshotManager;
			NamedTypeSymbol namedTypeSymbol = ((snapshotManager != null) ? snapshotManager.GetUpdatedDelegateTypeForLambda(lambda) : null);
			Symbol symbol;
			if (!this._remappedSymbols.TryGetValue(lambda.ContainingSymbol, out symbol) && namedTypeSymbol == null)
			{
				return lambda;
			}
			LambdaSymbol lambdaSymbol;
			if (namedTypeSymbol == null)
			{
				lambdaSymbol = boundLambda.CreateLambdaSymbol(symbol, lambda.ReturnTypeWithAnnotations, lambda.ParameterTypesWithAnnotations, lambda.ParameterRefKinds, lambda.RefKind);
			}
			else
			{
				lambdaSymbol = boundLambda.CreateLambdaSymbol(namedTypeSymbol, symbol ?? lambda.ContainingSymbol);
			}
			this._remappedSymbols.Add(lambda, lambdaSymbol);
			for (int i = 0; i < lambda.ParameterCount; i++)
			{
				this._remappedSymbols.Add(lambda.Parameters[i], lambdaSymbol.Parameters[i]);
			}
			return lambdaSymbol;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00063AA0 File Offset: 0x00061CA0
		[CompilerGenerated]
		private Symbol <GetUpdatedSymbol>g__remapLocal|4_1<T>(SourceLocalSymbol local) where T : Symbol
		{
			Symbol symbol;
			if (this._remappedSymbols.TryGetValue(local, out symbol))
			{
				return symbol;
			}
			NullableWalker.SnapshotManager snapshotManager = this._snapshotManager;
			TypeWithAnnotations? typeWithAnnotations = ((snapshotManager != null) ? snapshotManager.GetUpdatedTypeForLocalSymbol(local) : default(TypeWithAnnotations?));
			Symbol symbol2;
			if (!this._remappedSymbols.TryGetValue(local.ContainingSymbol, out symbol2) && typeWithAnnotations == null)
			{
				this._remappedSymbols.Add(local, local);
				return local;
			}
			symbol = new UpdatedContainingSymbolAndNullableAnnotationLocal(local, symbol2 ?? local.ContainingSymbol, typeWithAnnotations ?? local.TypeWithAnnotations);
			this._remappedSymbols.Add(local, symbol);
			return symbol;
		}

		// Token: 0x040005C5 RID: 1477
		[TupleElementNames(new string[] { "Info", "Type" })]
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0, 2 })]
		private readonly ImmutableDictionary<BoundExpression, ValueTuple<NullabilityInfo, TypeSymbol>> _updatedNullabilities;

		// Token: 0x040005C6 RID: 1478
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly NullableWalker.SnapshotManager _snapshotManager;

		// Token: 0x040005C7 RID: 1479
		private readonly ImmutableDictionary<Symbol, Symbol>.Builder _remappedSymbols;
	}
}
