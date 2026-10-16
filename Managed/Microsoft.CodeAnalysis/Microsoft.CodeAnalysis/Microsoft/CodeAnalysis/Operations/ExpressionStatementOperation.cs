using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003EF RID: 1007
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ExpressionStatementOperation : Operation, IExpressionStatementOperation, IOperation
	{
		// Token: 0x060026A3 RID: 9891 RVA: 0x00074720 File Offset: 0x00072920
		internal ExpressionStatementOperation(IOperation operation, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operation = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(operation, this);
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x00074739 File Offset: 0x00072939
		public IOperation Operation { get; }

		// Token: 0x060026A5 RID: 9893 RVA: 0x00074744 File Offset: 0x00072944
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operation != null)
			{
				return this.Operation;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x00074778 File Offset: 0x00072978
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			if (previousSlot != -1)
			{
				if (previousSlot > 1)
				{
					throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
				}
			}
			else if (this.Operation != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000747AF File Offset: 0x000729AF
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000747B2 File Offset: 0x000729B2
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000747B5 File Offset: 0x000729B5
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ExpressionStatement;
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000747B9 File Offset: 0x000729B9
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitExpressionStatement(this);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000747C2 File Offset: 0x000729C2
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitExpressionStatement(this, argument);
		}
	}
}
