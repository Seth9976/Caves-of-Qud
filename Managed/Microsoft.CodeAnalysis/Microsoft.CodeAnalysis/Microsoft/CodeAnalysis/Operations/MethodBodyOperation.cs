using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200043D RID: 1085
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class MethodBodyOperation : BaseMethodBodyBaseOperation, IMethodBodyOperation, IMethodBodyBaseOperation, IOperation
	{
		// Token: 0x0600298B RID: 10635 RVA: 0x00078864 File Offset: 0x00076A64
		internal MethodBodyOperation(IBlockOperation blockBody, IBlockOperation expressionBody, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(blockBody, expressionBody, semanticModel, syntax, isImplicit)
		{
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x00078874 File Offset: 0x00076A74
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (base.ExpressionBody != null)
					{
						return base.ExpressionBody;
					}
				}
			}
			else if (base.BlockBody != null)
			{
				return base.BlockBody;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000788C4 File Offset: 0x00076AC4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (base.BlockBody != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (base.ExpressionBody != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x00078927 File Offset: 0x00076B27
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x0007892A File Offset: 0x00076B2A
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x0007892D File Offset: 0x00076B2D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.MethodBody;
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00078931 File Offset: 0x00076B31
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitMethodBodyOperation(this);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0007893A File Offset: 0x00076B3A
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitMethodBodyOperation(this, argument);
		}
	}
}
