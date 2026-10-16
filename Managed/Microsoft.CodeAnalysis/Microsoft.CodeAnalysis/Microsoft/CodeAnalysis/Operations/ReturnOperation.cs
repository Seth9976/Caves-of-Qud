using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003EB RID: 1003
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ReturnOperation : Operation, IReturnOperation, IOperation
	{
		// Token: 0x06002676 RID: 9846 RVA: 0x00074283 File Offset: 0x00072483
		internal ReturnOperation(IOperation returnedValue, OperationKind kind, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.ReturnedValue = Operation.SetParentOperation<IOperation>(returnedValue, this);
			this.Kind = kind;
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000742A4 File Offset: 0x000724A4
		public IOperation ReturnedValue { get; }

		// Token: 0x06002678 RID: 9848 RVA: 0x000742AC File Offset: 0x000724AC
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.ReturnedValue != null)
			{
				return this.ReturnedValue;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000742E0 File Offset: 0x000724E0
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
			else if (this.ReturnedValue != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x00074317 File Offset: 0x00072517
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x0007431A File Offset: 0x0007251A
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x0007431D File Offset: 0x0007251D
		public override OperationKind Kind { get; }

		// Token: 0x0600267D RID: 9853 RVA: 0x00074325 File Offset: 0x00072525
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitReturn(this);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x0007432E File Offset: 0x0007252E
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitReturn(this, argument);
		}
	}
}
