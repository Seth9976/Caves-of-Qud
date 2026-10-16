using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C0 RID: 192
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundTreeRewriter : BoundTreeVisitor
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x000570CE File Offset: 0x000552CE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("type")]
		public virtual TypeSymbol VisitType(TypeSymbol type)
		{
			return type;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000570D1 File Offset: 0x000552D1
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> VisitList<T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<T> list) where T : BoundNode
		{
			if (list.IsDefault)
			{
				return list;
			}
			return this.DoVisitList<T>(list);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000570E8 File Offset: 0x000552E8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<T> DoVisitList<T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<T> list) where T : BoundNode
		{
			ArrayBuilder<T> arrayBuilder = null;
			for (int i = 0; i < list.Length; i++)
			{
				T t = list[i];
				BoundNode boundNode = this.Visit(t);
				if (arrayBuilder == null && t != boundNode)
				{
					arrayBuilder = ArrayBuilder<T>.GetInstance();
					if (i > 0)
					{
						arrayBuilder.AddRange(list, i);
					}
				}
				if (arrayBuilder != null && boundNode != null)
				{
					arrayBuilder.Add((T)((object)boundNode));
				}
			}
			if (arrayBuilder != null)
			{
				return arrayBuilder.ToImmutableAndFree();
			}
			return list;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0005715C File Offset: 0x0005535C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldEqualsValue(BoundFieldEqualsValue node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(node.Field, node.Locals, boundExpression);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00057190 File Offset: 0x00055390
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyEqualsValue(BoundPropertyEqualsValue node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(node.Property, node.Locals, boundExpression);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000571C4 File Offset: 0x000553C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameterEqualsValue(BoundParameterEqualsValue node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			return node.Update(node.Parameter, node.Locals, boundExpression);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000571F8 File Offset: 0x000553F8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitGlobalStatementInitializer(BoundGlobalStatementInitializer node)
		{
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Statement);
			return node.Update(boundStatement);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00057220 File Offset: 0x00055420
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.ValEscape, typeSymbol);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00057248 File Offset: 0x00055448
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleOperandPlaceholder(BoundTupleOperandPlaceholder node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0005726C File Offset: 0x0005546C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.ValEscape, typeSymbol);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00057294 File Offset: 0x00055494
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDisposableValuePlaceholder(BoundDisposableValuePlaceholder node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000572B8 File Offset: 0x000554B8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.IsNewInstance, typeSymbol);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000572E0 File Offset: 0x000554E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDup(BoundDup node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.RefKind, typeSymbol);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00057308 File Offset: 0x00055508
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPassByCopy(BoundPassByCopy node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, typeSymbol);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0005733C File Offset: 0x0005553C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.ChildBoundNodes);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.ResultKind, node.Symbols, immutableArray, typeSymbol);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00057378 File Offset: 0x00055578
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBadStatement(BoundBadStatement node)
		{
			ImmutableArray<BoundNode> immutableArray = this.VisitList<BoundNode>(node.ChildBoundNodes);
			return node.Update(immutableArray);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0005739C File Offset: 0x0005559C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExtractedFinallyBlock(BoundExtractedFinallyBlock node)
		{
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.FinallyBlock);
			return node.Update(boundBlock);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000573C4 File Offset: 0x000555C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeExpression(BoundTypeExpression node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.BoundContainingTypeOpt);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.BoundDimensionsOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.AliasOpt, boundTypeExpression, immutableArray, node.TypeWithAnnotations, typeSymbol);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00057414 File Offset: 0x00055614
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOrValueExpression(BoundTypeOrValueExpression node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Data, typeSymbol);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0005743B File Offset: 0x0005563B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNamespaceExpression(BoundNamespaceExpression node)
		{
			this.VisitType(node.Type);
			return node.Update(node.NamespaceSymbol, node.AliasOpt);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0005745C File Offset: 0x0005565C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.OperatorKind, boundExpression, node.ConstantValueOpt, node.MethodOpt, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, typeSymbol);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000574B0 File Offset: 0x000556B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.OperatorKind, boundExpression, node.MethodOpt, node.OperandConversion, node.ResultConversion, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, typeSymbol);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00057508 File Offset: 0x00055708
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.IsManaged, typeSymbol);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00057544 File Offset: 0x00055744
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedAddressOfOperator(BoundUnconvertedAddressOfOperator node)
		{
			BoundMethodGroup boundMethodGroup = (BoundMethodGroup)this.Visit(node.Operand);
			this.VisitType(node.Type);
			return node.Update(boundMethodGroup);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00057578 File Offset: 0x00055778
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerLoad(BoundFunctionPointerLoad node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.TargetMethod, typeSymbol);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000575A0 File Offset: 0x000557A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, typeSymbol);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000575D4 File Offset: 0x000557D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Index);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, node.Checked, typeSymbol);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00057624 File Offset: 0x00055824
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.InvokedExpression);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, immutableArray, node.ArgumentRefKindsOpt, node.ResultKind, typeSymbol);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00057674 File Offset: 0x00055874
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.GetTypeFromHandle, typeSymbol);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000576B0 File Offset: 0x000558B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMakeRefOperator(BoundMakeRefOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, typeSymbol);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000576E4 File Offset: 0x000558E4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRefValueOperator(BoundRefValueOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.NullableAnnotation, boundExpression, typeSymbol);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00057720 File Offset: 0x00055920
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.MethodOpt, typeSymbol);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0005775C File Offset: 0x0005595C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.LeftOperandOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.RightOperandOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, node.MethodOpt, typeSymbol);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000577AC File Offset: 0x000559AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.OperatorKind, node.ConstantValueOpt, node.MethodOpt, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, boundExpression, boundExpression2, typeSymbol);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00057814 File Offset: 0x00055A14
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, node.OperatorKind, node.Operators, typeSymbol);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00057868 File Offset: 0x00055A68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.OperatorKind, node.LogicalOperator, node.TrueOperator, node.FalseOperator, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, boundExpression, boundExpression2, typeSymbol);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000578D4 File Offset: 0x00055AD4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Operator, boundExpression, boundExpression2, node.LeftConversion, node.FinalConversion, node.ResultKind, node.OriginalUserDefinedOperatorsOpt, typeSymbol);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0005793C File Offset: 0x00055B3C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Left);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Right);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, node.IsRef, typeSymbol);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0005798C File Offset: 0x00055B8C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			BoundTupleExpression boundTupleExpression = (BoundTupleExpression)this.Visit(node.Left);
			BoundConversion boundConversion = (BoundConversion)this.Visit(node.Right);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundTupleExpression, boundConversion, node.IsUsed, typeSymbol);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000579DC File Offset: 0x00055BDC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.LeftOperand);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.RightOperand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, node.LeftConversion, node.OperatorResultKind, typeSymbol);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00057A30 File Offset: 0x00055C30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.LeftOperand);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.RightOperand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, typeSymbol);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00057A78 File Offset: 0x00055C78
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedConditionalOperator(BoundUnconvertedConditionalOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Consequence);
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.Alternative);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, node.NoCommonTypeError, typeSymbol);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00057AE0 File Offset: 0x00055CE0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Consequence);
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.Alternative);
			TypeSymbol typeSymbol = this.VisitType(node.NaturalTypeOpt);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			return node.Update(node.IsRef, boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, typeSymbol, node.WasTargetTyped, typeSymbol2);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00057B5C File Offset: 0x00055D5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Indices);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, immutableArray, typeSymbol);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00057BA0 File Offset: 0x00055DA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayLength(BoundArrayLength node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, typeSymbol);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00057BD4 File Offset: 0x00055DD4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitableInfo(BoundAwaitableInfo node)
		{
			BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = (BoundAwaitableValuePlaceholder)this.Visit(node.AwaitableInstancePlaceholder);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.GetAwaiter);
			return node.Update(boundAwaitableValuePlaceholder, node.IsDynamic, boundExpression, node.IsCompleted, node.GetResult);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00057C20 File Offset: 0x00055E20
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			BoundAwaitableInfo boundAwaitableInfo = (BoundAwaitableInfo)this.Visit(node.AwaitableInfo);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundAwaitableInfo, typeSymbol);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00057C68 File Offset: 0x00055E68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOfOperator(BoundTypeOfOperator node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.SourceType);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundTypeExpression, node.GetTypeFromHandle, typeSymbol);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00057CA4 File Offset: 0x00055EA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodDefIndex(BoundMethodDefIndex node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Method, typeSymbol);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00057CCC File Offset: 0x00055ECC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMaximumMethodDefIndex(BoundMaximumMethodDefIndex node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00057CF0 File Offset: 0x00055EF0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitInstrumentationPayloadRoot(BoundInstrumentationPayloadRoot node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.AnalysisKind, typeSymbol);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00057D18 File Offset: 0x00055F18
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitModuleVersionId(BoundModuleVersionId node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00057D3C File Offset: 0x00055F3C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitModuleVersionIdString(BoundModuleVersionIdString node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00057D60 File Offset: 0x00055F60
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSourceDocumentIndex(BoundSourceDocumentIndex node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Document, typeSymbol);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00057D88 File Offset: 0x00055F88
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodInfo(BoundMethodInfo node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Method, node.GetMethodFromHandle, typeSymbol);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00057DB8 File Offset: 0x00055FB8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldInfo(BoundFieldInfo node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Field, node.GetFieldFromHandle, typeSymbol);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00057DE5 File Offset: 0x00055FE5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultLiteral(BoundDefaultLiteral node)
		{
			this.VisitType(node.Type);
			return node.Update();
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00057DFC File Offset: 0x00055FFC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDefaultExpression(BoundDefaultExpression node)
		{
			BoundTypeExpression targetType = node.TargetType;
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(targetType, node.ConstantValueOpt, typeSymbol);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00057E2C File Offset: 0x0005602C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIsOperator(BoundIsOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.TargetType);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundTypeExpression, node.Conversion, typeSymbol);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00057E7C File Offset: 0x0005607C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAsOperator(BoundAsOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.TargetType);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundTypeExpression, node.Conversion, typeSymbol);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00057ECC File Offset: 0x000560CC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.SourceType);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundTypeExpression, node.ConstantValueOpt, typeSymbol);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00057F08 File Offset: 0x00056108
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConversion(BoundConversion node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.Conversion, node.IsBaseConversion, node.Checked, node.ExplicitCastInCode, node.ConstantValueOpt, node.ConversionGroupOpt, node.OriginalUserDefinedConversionsOpt, typeSymbol);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00057F68 File Offset: 0x00056168
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.ConversionMethod, typeSymbol);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00057FA4 File Offset: 0x000561A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgList(BoundArgList node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00057FC8 File Offset: 0x000561C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArgListOperator(BoundArgListOperator node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(immutableArray, node.ArgumentRefKindsOpt, typeSymbol);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00058000 File Offset: 0x00056200
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			TypeSymbol typeSymbol = this.VisitType(node.ElementPointerType);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			return node.Update(typeSymbol, node.ElementPointerTypeConversion, boundExpression, node.GetPinnableOpt, typeSymbol2);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00058050 File Offset: 0x00056250
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequencePoint(BoundSequencePoint node)
		{
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.StatementOpt);
			return node.Update(boundStatement);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00058078 File Offset: 0x00056278
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequencePointWithSpan(BoundSequencePointWithSpan node)
		{
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.StatementOpt);
			return node.Update(boundStatement, node.Span);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000580A4 File Offset: 0x000562A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSavePreviousSequencePoint(BoundSavePreviousSequencePoint node)
		{
			return node;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000580A7 File Offset: 0x000562A7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRestorePreviousSequencePoint(BoundRestorePreviousSequencePoint node)
		{
			return node;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000580AA File Offset: 0x000562AA
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStepThroughSequencePoint(BoundStepThroughSequencePoint node)
		{
			return node;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000580B0 File Offset: 0x000562B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBlock(BoundBlock node)
		{
			ImmutableArray<BoundStatement> immutableArray = this.VisitList<BoundStatement>(node.Statements);
			return node.Update(node.Locals, node.LocalFunctions, immutableArray);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x000580E0 File Offset: 0x000562E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitScope(BoundScope node)
		{
			ImmutableArray<BoundStatement> immutableArray = this.VisitList<BoundStatement>(node.Statements);
			return node.Update(node.Locals, immutableArray);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00058108 File Offset: 0x00056308
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStateMachineScope(BoundStateMachineScope node)
		{
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Statement);
			return node.Update(node.Fields, boundStatement);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00058134 File Offset: 0x00056334
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredTypeOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.InitializerOpt);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.ArgumentsOpt);
			return node.Update(node.LocalSymbol, boundTypeExpression, boundExpression, immutableArray, node.InferredType);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00058188 File Offset: 0x00056388
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMultipleLocalDeclarations(BoundMultipleLocalDeclarations node)
		{
			ImmutableArray<BoundLocalDeclaration> immutableArray = this.VisitList<BoundLocalDeclaration>(node.LocalDeclarations);
			return node.Update(immutableArray);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x000581AC File Offset: 0x000563AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node)
		{
			BoundAwaitableInfo boundAwaitableInfo = (BoundAwaitableInfo)this.Visit(node.AwaitOpt);
			ImmutableArray<BoundLocalDeclaration> immutableArray = this.VisitList<BoundLocalDeclaration>(node.LocalDeclarations);
			return node.Update(node.PatternDisposeInfoOpt, node.IDisposableConversion, boundAwaitableInfo, immutableArray);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000581EC File Offset: 0x000563EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.BlockBody);
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.ExpressionBody);
			return node.Update(node.Symbol, boundBlock, boundBlock2);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0005822B File Offset: 0x0005642B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNoOpStatement(BoundNoOpStatement node)
		{
			return node;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00058230 File Offset: 0x00056430
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExpressionOpt);
			return node.Update(node.RefKind, boundExpression);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0005825C File Offset: 0x0005645C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			return node.Update(boundExpression);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00058282 File Offset: 0x00056482
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitYieldBreakStatement(BoundYieldBreakStatement node)
		{
			return node;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00058288 File Offset: 0x00056488
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExpressionOpt);
			return node.Update(boundExpression);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000582B0 File Offset: 0x000564B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			return node.Update(boundExpression);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000582D6 File Offset: 0x000564D6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBreakStatement(BoundBreakStatement node)
		{
			return node;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000582D9 File Offset: 0x000564D9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitContinueStatement(BoundContinueStatement node)
		{
			return node;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000582DC File Offset: 0x000564DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchStatement(BoundSwitchStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundSwitchSection> immutableArray = this.VisitList<BoundSwitchSection>(node.SwitchSections);
			BoundDecisionDag decisionDag = node.DecisionDag;
			BoundSwitchLabel boundSwitchLabel = (BoundSwitchLabel)this.Visit(node.DefaultLabel);
			return node.Update(boundExpression, node.InnerLocals, node.InnerLocalFunctions, immutableArray, decisionDag, boundSwitchLabel, node.BreakLabel);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00058340 File Offset: 0x00056540
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchDispatch(BoundSwitchDispatch node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			return node.Update(boundExpression, node.Cases, node.DefaultLabel, node.EqualityMethod);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00058378 File Offset: 0x00056578
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIfStatement(BoundIfStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Consequence);
			BoundStatement boundStatement2 = (BoundStatement)this.Visit(node.AlternativeOpt);
			return node.Update(boundExpression, boundStatement, boundStatement2);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x000583C4 File Offset: 0x000565C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(node.Locals, boundExpression, boundStatement, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00058410 File Offset: 0x00056610
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(node.Locals, boundExpression, boundStatement, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0005845C File Offset: 0x0005665C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Initializer);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement2 = (BoundStatement)this.Visit(node.Increment);
			BoundStatement boundStatement3 = (BoundStatement)this.Visit(node.Body);
			return node.Update(node.OuterLocals, boundStatement, node.InnerLocals, boundExpression, boundStatement2, boundStatement3, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x000584D4 File Offset: 0x000566D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.IterationVariableType);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.IterationErrorExpressionOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Expression);
			BoundForEachDeconstructStep boundForEachDeconstructStep = (BoundForEachDeconstructStep)this.Visit(node.DeconstructionOpt);
			BoundAwaitableInfo boundAwaitableInfo = (BoundAwaitableInfo)this.Visit(node.AwaitOpt);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(node.EnumeratorInfoOpt, node.ElementConversion, boundTypeExpression, node.IterationVariables, boundExpression, boundExpression2, boundForEachDeconstructStep, boundAwaitableInfo, boundStatement, node.Checked, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00058584 File Offset: 0x00056784
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitForEachDeconstructStep(BoundForEachDeconstructStep node)
		{
			BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator = (BoundDeconstructionAssignmentOperator)this.Visit(node.DeconstructionAssignment);
			BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder = (BoundDeconstructValuePlaceholder)this.Visit(node.TargetPlaceholder);
			return node.Update(boundDeconstructionAssignmentOperator, boundDeconstructValuePlaceholder);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000585C0 File Offset: 0x000567C0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			BoundMultipleLocalDeclarations boundMultipleLocalDeclarations = (BoundMultipleLocalDeclarations)this.Visit(node.DeclarationsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExpressionOpt);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			BoundAwaitableInfo boundAwaitableInfo = (BoundAwaitableInfo)this.Visit(node.AwaitOpt);
			return node.Update(node.Locals, boundMultipleLocalDeclarations, boundExpression, node.IDisposableConversion, boundStatement, boundAwaitableInfo, node.PatternDisposeInfoOpt);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00058634 File Offset: 0x00056834
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			BoundMultipleLocalDeclarations boundMultipleLocalDeclarations = (BoundMultipleLocalDeclarations)this.Visit(node.Declarations);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(node.Locals, boundMultipleLocalDeclarations, boundStatement);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00058674 File Offset: 0x00056874
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLockStatement(BoundLockStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Argument);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(boundExpression, boundStatement);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000586B0 File Offset: 0x000568B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.TryBlock);
			ImmutableArray<BoundCatchBlock> immutableArray = this.VisitList<BoundCatchBlock>(node.CatchBlocks);
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.FinallyBlockOpt);
			return node.Update(boundBlock, immutableArray, boundBlock2, node.FinallyLabelOpt, node.PreferFaultHandler);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00058704 File Offset: 0x00056904
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExceptionSourceOpt);
			BoundStatementList boundStatementList = (BoundStatementList)this.Visit(node.ExceptionFilterPrologueOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.ExceptionFilterOpt);
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			TypeSymbol typeSymbol = this.VisitType(node.ExceptionTypeOpt);
			return node.Update(node.Locals, boundExpression, typeSymbol, boundStatementList, boundExpression2, boundBlock, node.IsSynthesizedAsyncCatchAll);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00058780 File Offset: 0x00056980
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLiteral(BoundLiteral node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.ConstantValueOpt, typeSymbol);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x000587A8 File Offset: 0x000569A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThisReference(BoundThisReference node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000587CC File Offset: 0x000569CC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000587F0 File Offset: 0x000569F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00058814 File Offset: 0x00056A14
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBaseReference(BoundBaseReference node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00058838 File Offset: 0x00056A38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocal(BoundLocal node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.LocalSymbol, node.DeclarationKind, node.ConstantValueOpt, node.IsNullableUnknown, typeSymbol);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00058874 File Offset: 0x00056A74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPseudoVariable(BoundPseudoVariable node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.LocalSymbol, node.EmitExpressions, typeSymbol);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000588A4 File Offset: 0x00056AA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.RangeVariableSymbol, boundExpression, typeSymbol);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x000588E0 File Offset: 0x00056AE0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitParameter(BoundParameter node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.ParameterSymbol, typeSymbol);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00058907 File Offset: 0x00056B07
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			return node;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0005890C File Offset: 0x00056B0C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.CaseExpressionOpt);
			BoundLabel boundLabel = (BoundLabel)this.Visit(node.LabelExpressionOpt);
			return node.Update(node.Label, boundExpression, boundLabel);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0005894C File Offset: 0x00056B4C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(node.Label, boundStatement);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00058978 File Offset: 0x00056B78
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabel(BoundLabel node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Label, typeSymbol);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000589A0 File Offset: 0x00056BA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStatementList(BoundStatementList node)
		{
			ImmutableArray<BoundStatement> immutableArray = this.VisitList<BoundStatement>(node.Statements);
			return node.Update(immutableArray);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000589C4 File Offset: 0x00056BC4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalGoto(BoundConditionalGoto node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			return node.Update(boundExpression, node.JumpIfTrue, node.Label);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000589F8 File Offset: 0x00056BF8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchExpressionArm(BoundSwitchExpressionArm node)
		{
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Pattern);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.WhenClause);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Value);
			return node.Update(node.Locals, boundPattern, boundExpression, boundExpression2, node.Label);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00058A50 File Offset: 0x00056C50
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundSwitchExpressionArm> immutableArray = this.VisitList<BoundSwitchExpressionArm>(node.SwitchArms);
			BoundDecisionDag decisionDag = node.DecisionDag;
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, immutableArray, decisionDag, node.DefaultLabel, node.ReportedNotExhaustive, typeSymbol);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00058AA8 File Offset: 0x00056CA8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundSwitchExpressionArm> immutableArray = this.VisitList<BoundSwitchExpressionArm>(node.SwitchArms);
			BoundDecisionDag decisionDag = node.DecisionDag;
			TypeSymbol typeSymbol = this.VisitType(node.NaturalTypeOpt);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			return node.Update(typeSymbol, node.WasTargetTyped, node.Conversion, boundExpression, immutableArray, decisionDag, node.DefaultLabel, node.ReportedNotExhaustive, typeSymbol2);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00058B1C File Offset: 0x00056D1C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDecisionDag(BoundDecisionDag node)
		{
			BoundDecisionDagNode boundDecisionDagNode = (BoundDecisionDagNode)this.Visit(node.RootNode);
			return node.Update(boundDecisionDagNode);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00058B44 File Offset: 0x00056D44
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEvaluationDecisionDagNode(BoundEvaluationDecisionDagNode node)
		{
			BoundDagEvaluation boundDagEvaluation = (BoundDagEvaluation)this.Visit(node.Evaluation);
			BoundDecisionDagNode boundDecisionDagNode = (BoundDecisionDagNode)this.Visit(node.Next);
			return node.Update(boundDagEvaluation, boundDecisionDagNode);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00058B80 File Offset: 0x00056D80
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTestDecisionDagNode(BoundTestDecisionDagNode node)
		{
			BoundDagTest boundDagTest = (BoundDagTest)this.Visit(node.Test);
			BoundDecisionDagNode boundDecisionDagNode = (BoundDecisionDagNode)this.Visit(node.WhenTrue);
			BoundDecisionDagNode boundDecisionDagNode2 = (BoundDecisionDagNode)this.Visit(node.WhenFalse);
			return node.Update(boundDagTest, boundDecisionDagNode, boundDecisionDagNode2);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00058BCC File Offset: 0x00056DCC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWhenDecisionDagNode(BoundWhenDecisionDagNode node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.WhenExpression);
			BoundDecisionDagNode boundDecisionDagNode = (BoundDecisionDagNode)this.Visit(node.WhenTrue);
			BoundDecisionDagNode boundDecisionDagNode2 = (BoundDecisionDagNode)this.Visit(node.WhenFalse);
			return node.Update(node.Bindings, boundExpression, boundDecisionDagNode, boundDecisionDagNode2);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00058C1E File Offset: 0x00056E1E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLeafDecisionDagNode(BoundLeafDecisionDagNode node)
		{
			return node;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00058C24 File Offset: 0x00056E24
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagTemp(BoundDagTemp node)
		{
			BoundDagEvaluation boundDagEvaluation = (BoundDagEvaluation)this.Visit(node.Source);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol, boundDagEvaluation, node.Index);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00058C60 File Offset: 0x00056E60
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagTypeTest(BoundDagTypeTest node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol, boundDagTemp);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00058C94 File Offset: 0x00056E94
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagNonNullTest(BoundDagNonNullTest node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(node.IsExplicitTest, boundDagTemp);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00058CC0 File Offset: 0x00056EC0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagExplicitNullTest(BoundDagExplicitNullTest node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(boundDagTemp);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00058CE8 File Offset: 0x00056EE8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagValueTest(BoundDagValueTest node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(node.Value, boundDagTemp);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00058D14 File Offset: 0x00056F14
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagRelationalTest(BoundDagRelationalTest node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(node.OperatorKind, node.Value, boundDagTemp);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00058D48 File Offset: 0x00056F48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagDeconstructEvaluation(BoundDagDeconstructEvaluation node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(node.DeconstructMethod, boundDagTemp);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00058D74 File Offset: 0x00056F74
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagTypeEvaluation(BoundDagTypeEvaluation node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol, boundDagTemp);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00058DA8 File Offset: 0x00056FA8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagFieldEvaluation(BoundDagFieldEvaluation node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(node.Field, boundDagTemp);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00058DD4 File Offset: 0x00056FD4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagPropertyEvaluation(BoundDagPropertyEvaluation node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(node.Property, boundDagTemp);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00058E00 File Offset: 0x00057000
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDagIndexEvaluation(BoundDagIndexEvaluation node)
		{
			BoundDagTemp boundDagTemp = (BoundDagTemp)this.Visit(node.Input);
			return node.Update(node.Property, node.Index, boundDagTemp);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00058E34 File Offset: 0x00057034
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchSection(BoundSwitchSection node)
		{
			ImmutableArray<BoundSwitchLabel> immutableArray = this.VisitList<BoundSwitchLabel>(node.SwitchLabels);
			ImmutableArray<BoundStatement> immutableArray2 = this.VisitList<BoundStatement>(node.Statements);
			return node.Update(node.Locals, immutableArray, immutableArray2);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00058E6C File Offset: 0x0005706C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSwitchLabel(BoundSwitchLabel node)
		{
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Pattern);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.WhenClause);
			return node.Update(node.Label, boundPattern, boundExpression);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00058EAC File Offset: 0x000570AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequencePointExpression(BoundSequencePointExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, typeSymbol);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00058EE0 File Offset: 0x000570E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSequence(BoundSequence node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.SideEffects);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Locals, immutableArray, boundExpression, typeSymbol);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00058F28 File Offset: 0x00057128
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSpillSequence(BoundSpillSequence node)
		{
			ImmutableArray<BoundStatement> immutableArray = this.VisitList<BoundStatement>(node.SideEffects);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Locals, immutableArray, boundExpression, typeSymbol);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00058F70 File Offset: 0x00057170
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.TypeArgumentsOpt, node.Name, node.Invoked, node.Indexed, typeSymbol);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00058FBC File Offset: 0x000571BC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.ApplicableMethods, boundExpression, immutableArray, typeSymbol);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00059010 File Offset: 0x00057210
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.AccessExpression);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, typeSymbol);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00059058 File Offset: 0x00057258
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.WhenNotNull);
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.WhenNullOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.HasValueMethodOpt, boundExpression2, boundExpression3, node.Id, typeSymbol);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000590C0 File Offset: 0x000572C0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Id, typeSymbol);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000590E8 File Offset: 0x000572E8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ValueTypeReceiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.ReferenceTypeReceiver);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, typeSymbol);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00059130 File Offset: 0x00057330
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMethodGroup(BoundMethodGroup node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			this.VisitType(node.Type);
			return node.Update(node.TypeArgumentsOpt, node.Name, node.Methods, node.LookupSymbolOpt, node.LookupError, node.Flags, boundExpression, node.ResultKind);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00059190 File Offset: 0x00057390
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyGroup(BoundPropertyGroup node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			this.VisitType(node.Type);
			return node.Update(node.Properties, boundExpression, node.ResultKind);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000591D0 File Offset: 0x000573D0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCall(BoundCall node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.Method, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.IsDelegateCall, node.Expanded, node.InvokedAsExtensionMethod, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, node.OriginalMethodsOpt, typeSymbol);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00059250 File Offset: 0x00057450
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Argument);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Event, node.IsAddition, node.IsDynamic, boundExpression, boundExpression2, typeSymbol);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000592AC File Offset: 0x000574AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAttribute(BoundAttribute node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.ConstructorArguments);
			ImmutableArray<BoundAssignmentOperator> immutableArray2 = this.VisitList<BoundAssignmentOperator>(node.NamedArguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Constructor, immutableArray, node.ConstructorArgumentNamesOpt, node.ConstructorArgumentsToParamsOpt, node.ConstructorExpanded, immutableArray2, node.ResultKind, typeSymbol);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00059308 File Offset: 0x00057508
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			this.VisitType(node.Type);
			return node.Update(immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.InitializerOpt);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00059348 File Offset: 0x00057548
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Constructor, node.ConstructorsGroup, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ConstantValueOpt, boundObjectInitializerExpressionBase, node.WasTargetTyped, typeSymbol);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000593C0 File Offset: 0x000575C0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(immutableArray, node.ArgumentNamesOpt, node.InferredNamesOpt, typeSymbol);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000593FC File Offset: 0x000575FC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			BoundTupleLiteral sourceTuple = node.SourceTuple;
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(sourceTuple, node.WasTargetTyped, immutableArray, node.ArgumentNamesOpt, node.InferredNamesOpt, typeSymbol);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00059448 File Offset: 0x00057648
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Name, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, boundObjectInitializerExpressionBase, node.ApplicableMethods, typeSymbol);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000594A4 File Offset: 0x000576A4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node)
		{
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.GuidString, boundObjectInitializerExpressionBase, typeSymbol);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000594E0 File Offset: 0x000576E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerExpression(BoundObjectInitializerExpression node)
		{
			BoundObjectOrCollectionValuePlaceholder boundObjectOrCollectionValuePlaceholder = (BoundObjectOrCollectionValuePlaceholder)this.Visit(node.Placeholder);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Initializers);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundObjectOrCollectionValuePlaceholder, immutableArray, typeSymbol);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00059524 File Offset: 0x00057724
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.ReceiverType);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			return node.Update(node.MemberSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0005958C File Offset: 0x0005778C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.ReceiverType);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			return node.Update(node.MemberName, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000595C4 File Offset: 0x000577C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node)
		{
			BoundObjectOrCollectionValuePlaceholder boundObjectOrCollectionValuePlaceholder = (BoundObjectOrCollectionValuePlaceholder)this.Visit(node.Placeholder);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Initializers);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundObjectOrCollectionValuePlaceholder, immutableArray, typeSymbol);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00059608 File Offset: 0x00057808
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCollectionElementInitializer(BoundCollectionElementInitializer node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ImplicitReceiverOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.AddMethod, immutableArray, boundExpression, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.InvokedAsExtensionMethod, node.ResultKind, typeSymbol);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00059670 File Offset: 0x00057870
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.ApplicableMethods, boundExpression, immutableArray, typeSymbol);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000596B8 File Offset: 0x000578B8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitImplicitReceiver(BoundImplicitReceiver node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000596DC File Offset: 0x000578DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			ImmutableArray<BoundAnonymousPropertyDeclaration> immutableArray2 = this.VisitList<BoundAnonymousPropertyDeclaration>(node.Declarations);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Constructor, immutableArray, immutableArray2, typeSymbol);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00059720 File Offset: 0x00057920
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitAnonymousPropertyDeclaration(BoundAnonymousPropertyDeclaration node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Property, typeSymbol);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00059748 File Offset: 0x00057948
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNewT(BoundNewT node)
		{
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpressionOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundObjectInitializerExpressionBase, typeSymbol);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0005977C File Offset: 0x0005797C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Argument);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.MethodOpt, node.IsExtensionMethod, typeSymbol);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000597BC File Offset: 0x000579BC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayCreation(BoundArrayCreation node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Bounds);
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.Visit(node.InitializerOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(immutableArray, boundArrayInitialization, typeSymbol);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00059800 File Offset: 0x00057A00
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitArrayInitialization(BoundArrayInitialization node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Initializers);
			this.VisitType(node.Type);
			return node.Update(immutableArray);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00059830 File Offset: 0x00057A30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Count);
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.Visit(node.InitializerOpt);
			TypeSymbol typeSymbol = this.VisitType(node.ElementType);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			return node.Update(typeSymbol, boundExpression, boundArrayInitialization, typeSymbol2);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00059888 File Offset: 0x00057A88
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Count);
			BoundArrayInitialization boundArrayInitialization = (BoundArrayInitialization)this.Visit(node.InitializerOpt);
			TypeSymbol typeSymbol = this.VisitType(node.ElementType);
			TypeSymbol typeSymbol2 = this.VisitType(node.Type);
			return node.Update(typeSymbol, boundExpression, boundArrayInitialization, typeSymbol2);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000598E0 File Offset: 0x00057AE0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.FieldSymbol, node.ConstantValueOpt, node.ResultKind, node.IsByValue, node.IsDeclaration, typeSymbol);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00059934 File Offset: 0x00057B34
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitHoistedFieldAccess(BoundHoistedFieldAccess node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.FieldSymbol, typeSymbol);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0005995C File Offset: 0x00057B5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.PropertySymbol, node.ResultKind, typeSymbol);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0005999C File Offset: 0x00057B9C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.EventSymbol, node.IsUsableAsField, node.ResultKind, typeSymbol);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000599E4 File Offset: 0x00057BE4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.Indexer, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.OriginalIndexersOpt, typeSymbol);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00059A50 File Offset: 0x00057C50
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Argument);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.LengthOrCountProperty, node.PatternSymbol, boundExpression2, typeSymbol);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00059AA4 File Offset: 0x00057CA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.ApplicableIndexers, typeSymbol);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00059AF8 File Offset: 0x00057CF8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLambda(BoundLambda node)
		{
			UnboundLambda unboundLambda = node.UnboundLambda;
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(unboundLambda, node.Symbol, boundBlock, node.Diagnostics, node.Binder, typeSymbol);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00059B46 File Offset: 0x00057D46
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnboundLambda(UnboundLambda node)
		{
			this.VisitType(node.Type);
			return node.Update(node.Data, node.WithDependencies);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00059B68 File Offset: 0x00057D68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			BoundExpression operation = node.Operation;
			BoundExpression cast = node.Cast;
			BoundExpression unoptimizedForm = node.UnoptimizedForm;
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.DefinedSymbol, operation, cast, node.Binder, unoptimizedForm, typeSymbol);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00059BC4 File Offset: 0x00057DC4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypeOrInstanceInitializers(BoundTypeOrInstanceInitializers node)
		{
			ImmutableArray<BoundStatement> immutableArray = this.VisitList<BoundStatement>(node.Statements);
			return node.Update(immutableArray);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00059BE8 File Offset: 0x00057DE8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNameOfOperator(BoundNameOfOperator node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Argument);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.ConstantValueOpt, typeSymbol);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00059C24 File Offset: 0x00057E24
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Parts);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(immutableArray, node.ConstantValueOpt, typeSymbol);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00059C5C File Offset: 0x00057E5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitInterpolatedString(BoundInterpolatedString node)
		{
			ImmutableArray<BoundExpression> immutableArray = this.VisitList<BoundExpression>(node.Parts);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(immutableArray, node.ConstantValueOpt, typeSymbol);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00059C94 File Offset: 0x00057E94
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitStringInsert(BoundStringInsert node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.Alignment);
			BoundLiteral boundLiteral = (BoundLiteral)this.Visit(node.Format);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundExpression2, boundLiteral, typeSymbol);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00059CF0 File Offset: 0x00057EF0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Pattern);
			BoundDecisionDag decisionDag = node.DecisionDag;
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, boundPattern, node.IsNegated, decisionDag, node.WhenTrueLabel, node.WhenFalseLabel, typeSymbol);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00059D54 File Offset: 0x00057F54
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConstantPattern(BoundConstantPattern node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(boundExpression, node.ConstantValue, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00059D9C File Offset: 0x00057F9C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDiscardPattern(BoundDiscardPattern node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00059DCC File Offset: 0x00057FCC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeclarationPattern(BoundDeclarationPattern node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.VariableAccess);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredType);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(node.Variable, boundExpression, boundTypeExpression, node.IsVar, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00059E30 File Offset: 0x00058030
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRecursivePattern(BoundRecursivePattern node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredType);
			ImmutableArray<BoundSubpattern> immutableArray = this.VisitList<BoundSubpattern>(node.Deconstruction);
			ImmutableArray<BoundSubpattern> immutableArray2 = this.VisitList<BoundSubpattern>(node.Properties);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.VariableAccess);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(boundTypeExpression, node.DeconstructMethod, immutableArray, immutableArray2, node.Variable, boundExpression, node.IsExplicitNotNullTest, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00059EB8 File Offset: 0x000580B8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitITuplePattern(BoundITuplePattern node)
		{
			ImmutableArray<BoundSubpattern> immutableArray = this.VisitList<BoundSubpattern>(node.Subpatterns);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(node.GetLengthMethod, node.GetItemMethod, immutableArray, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00059F04 File Offset: 0x00058104
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitSubpattern(BoundSubpattern node)
		{
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Pattern);
			return node.Update(node.Symbol, boundPattern);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00059F30 File Offset: 0x00058130
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitTypePattern(BoundTypePattern node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.DeclaredType);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(boundTypeExpression, node.IsExplicitNotNullTest, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00059F78 File Offset: 0x00058178
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitBinaryPattern(BoundBinaryPattern node)
		{
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Left);
			BoundPattern boundPattern2 = (BoundPattern)this.Visit(node.Right);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(node.Disjunction, boundPattern, boundPattern2, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00059FD4 File Offset: 0x000581D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNegatedPattern(BoundNegatedPattern node)
		{
			BoundPattern boundPattern = (BoundPattern)this.Visit(node.Negated);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(boundPattern, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0005A018 File Offset: 0x00058218
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitRelationalPattern(BoundRelationalPattern node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			TypeSymbol typeSymbol = this.VisitType(node.InputType);
			TypeSymbol typeSymbol2 = this.VisitType(node.NarrowedType);
			return node.Update(node.Relation, boundExpression, node.ConstantValue, typeSymbol, typeSymbol2);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0005A068 File Offset: 0x00058268
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDiscardExpression(BoundDiscardExpression node)
		{
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(typeSymbol);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0005A08C File Offset: 0x0005828C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitThrowExpression(BoundThrowExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, typeSymbol);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0005A0C0 File Offset: 0x000582C0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitOutVariablePendingInference(OutVariablePendingInference node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			this.VisitType(node.Type);
			return node.Update(node.VariableSymbol, boundExpression);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0005A0FC File Offset: 0x000582FC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			this.VisitType(node.Type);
			return node.Update(node.VariableSymbol, boundExpression);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0005A135 File Offset: 0x00058335
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node)
		{
			this.VisitType(node.Type);
			return node.Update();
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0005A14C File Offset: 0x0005834C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitNonConstructorMethodBody(BoundNonConstructorMethodBody node)
		{
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.BlockBody);
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.ExpressionBody);
			return node.Update(boundBlock, boundBlock2);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0005A188 File Offset: 0x00058388
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitConstructorMethodBody(BoundConstructorMethodBody node)
		{
			BoundExpressionStatement boundExpressionStatement = (BoundExpressionStatement)this.Visit(node.Initializer);
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.BlockBody);
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.ExpressionBody);
			return node.Update(node.Locals, boundExpressionStatement, boundBlock, boundBlock2);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0005A1DC File Offset: 0x000583DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitExpressionWithNullability(BoundExpressionWithNullability node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Expression);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.NullableAnnotation, typeSymbol);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0005A218 File Offset: 0x00058418
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitWithExpression(BoundWithExpression node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpressionBase)this.Visit(node.InitializerExpression);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, node.CloneMethod, boundObjectInitializerExpressionBase, typeSymbol);
		}
	}
}
