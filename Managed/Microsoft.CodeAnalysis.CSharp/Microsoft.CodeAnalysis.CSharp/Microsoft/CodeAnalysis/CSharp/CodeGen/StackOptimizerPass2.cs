using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005FA RID: 1530
	internal sealed class StackOptimizerPass2 : BoundTreeRewriterWithStackGuard
	{
		// Token: 0x06008144 RID: 33092 RVA: 0x001D6C53 File Offset: 0x001D4E53
		private StackOptimizerPass2(Dictionary<LocalSymbol, LocalDefUseInfo> info)
		{
			this._info = info;
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x001D6C62 File Offset: 0x001D4E62
		public static BoundStatement Rewrite(BoundStatement src, Dictionary<LocalSymbol, LocalDefUseInfo> info)
		{
			return (BoundStatement)new StackOptimizerPass2(info).Visit(src);
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x001D6C78 File Offset: 0x001D4E78
		public override BoundNode Visit(BoundNode node)
		{
			BoundExpression boundExpression = node as BoundExpression;
			BoundNode boundNode;
			if (boundExpression != null && boundExpression.ConstantValue != null)
			{
				boundNode = node;
			}
			else
			{
				boundNode = base.Visit(node);
			}
			this._nodeCounter++;
			return boundNode;
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x001D6CB8 File Offset: 0x001D4EB8
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			BoundExpression boundExpression = node.Left;
			if (boundExpression.Kind != BoundKind.BinaryOperator || boundExpression.ConstantValue != null)
			{
				return base.VisitBinaryOperator(node);
			}
			ArrayBuilder<BoundBinaryOperator> instance = ArrayBuilder<BoundBinaryOperator>.GetInstance();
			instance.Push(node);
			BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)boundExpression;
			for (;;)
			{
				instance.Push(boundBinaryOperator);
				boundExpression = boundBinaryOperator.Left;
				if (boundExpression.Kind != BoundKind.BinaryOperator || boundExpression.ConstantValue != null)
				{
					break;
				}
				boundBinaryOperator = (BoundBinaryOperator)boundExpression;
			}
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(boundExpression);
			for (;;)
			{
				boundBinaryOperator = instance.Pop<BoundBinaryOperator>();
				BoundExpression boundExpression3 = (BoundExpression)this.Visit(boundBinaryOperator.Right);
				TypeSymbol typeSymbol = this.VisitType(boundBinaryOperator.Type);
				boundExpression2 = boundBinaryOperator.Update(boundBinaryOperator.OperatorKind, boundBinaryOperator.ConstantValueOpt, boundBinaryOperator.MethodOpt, boundBinaryOperator.ResultKind, boundExpression2, boundExpression3, typeSymbol);
				if (instance.Count == 0)
				{
					break;
				}
				this._nodeCounter++;
			}
			instance.Free();
			return boundExpression2;
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x001D6DA8 File Offset: 0x001D4FA8
		private static bool IsLastAccess(LocalDefUseInfo locInfo, int counter)
		{
			return locInfo.LocalDefs.Any<LocalDefUseSpan>((LocalDefUseSpan d) => counter == d.Start && counter == d.End);
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x001D6DDC File Offset: 0x001D4FDC
		public override BoundNode VisitLocal(BoundLocal node)
		{
			LocalDefUseInfo localDefUseInfo;
			if (!this._info.TryGetValue(node.LocalSymbol, ref localDefUseInfo))
			{
				return base.VisitLocal(node);
			}
			if (!StackOptimizerPass2.IsLastAccess(localDefUseInfo, this._nodeCounter))
			{
				return new BoundDup(node.Syntax, node.LocalSymbol.RefKind, node.Type);
			}
			return base.VisitLocal(node);
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x001D6E38 File Offset: 0x001D5038
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(node.Constructor, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ConstantValueOpt, null, typeSymbol);
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x001D6E94 File Offset: 0x001D5094
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			BoundLocal boundLocal = node.Left as BoundLocal;
			LocalDefUseInfo localDefUseInfo;
			if (boundLocal == null || !this._info.TryGetValue(boundLocal.LocalSymbol, ref localDefUseInfo))
			{
				return base.VisitAssignmentOperator(node);
			}
			if (boundLocal.LocalSymbol.RefKind != RefKind.None && !node.IsRef)
			{
				return base.VisitAssignmentOperator(node);
			}
			this._nodeCounter++;
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Right);
			if (StackOptimizerPass2.IsLastAccess(localDefUseInfo, this._nodeCounter))
			{
				return boundExpression;
			}
			return node.Update(boundLocal, boundExpression, node.IsRef, node.Type);
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x001D6F34 File Offset: 0x001D5134
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			BoundExpression boundExpression = node.ExceptionSourceOpt;
			TypeSymbol typeSymbol = node.ExceptionTypeOpt;
			BoundStatementList boundStatementList = node.ExceptionFilterPrologueOpt;
			BoundExpression boundExpression2 = node.ExceptionFilterOpt;
			BoundBlock boundBlock = node.Body;
			if (boundExpression != null)
			{
				this._nodeCounter++;
				if (boundExpression.Kind == BoundKind.Local)
				{
					LocalSymbol localSymbol = ((BoundLocal)boundExpression).LocalSymbol;
					LocalDefUseInfo localDefUseInfo;
					if (this._info.TryGetValue(localSymbol, ref localDefUseInfo) && StackOptimizerPass2.IsLastAccess(localDefUseInfo, this._nodeCounter))
					{
						boundExpression = null;
					}
				}
				else
				{
					boundExpression = (BoundExpression)this.Visit(boundExpression);
				}
				this._nodeCounter++;
			}
			boundStatementList = ((boundStatementList != null) ? ((BoundStatementList)this.Visit(boundStatementList)) : null);
			if (boundExpression2 != null)
			{
				boundExpression2 = (BoundExpression)this.Visit(boundExpression2);
				this._nodeCounter++;
			}
			boundBlock = (BoundBlock)this.Visit(boundBlock);
			typeSymbol = this.VisitType(typeSymbol);
			return node.Update(node.Locals, boundExpression, typeSymbol, boundStatementList, boundExpression2, boundBlock, node.IsSynthesizedAsyncCatchAll);
		}

		// Token: 0x04001E5D RID: 7773
		private int _nodeCounter;

		// Token: 0x04001E5E RID: 7774
		private readonly Dictionary<LocalSymbol, LocalDefUseInfo> _info;
	}
}
