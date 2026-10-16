using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003ED RID: 1005
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class TryOperation : Operation, ITryOperation, IOperation
	{
		// Token: 0x0600268A RID: 9866 RVA: 0x00074450 File Offset: 0x00072650
		internal TryOperation([Nullable(1)] IBlockOperation body, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ICatchClauseOperation> catches, IBlockOperation @finally, ILabelSymbol exitLabel, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Body = Operation.SetParentOperation<IBlockOperation>(body, this);
			this.Catches = Operation.SetParentOperation<ICatchClauseOperation>(catches, this);
			this.Finally = Operation.SetParentOperation<IBlockOperation>(@finally, this);
			this.ExitLabel = exitLabel;
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x0007448D File Offset: 0x0007268D
		[Nullable(1)]
		public IBlockOperation Body
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x00074495 File Offset: 0x00072695
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICatchClauseOperation> Catches
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x0007449D File Offset: 0x0007269D
		public IBlockOperation Finally { get; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000744A5 File Offset: 0x000726A5
		public ILabelSymbol ExitLabel { get; }

		// Token: 0x0600268F RID: 9871 RVA: 0x000744B0 File Offset: 0x000726B0
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			switch (slot)
			{
			case 0:
				if (this.Body != null)
				{
					return this.Body;
				}
				break;
			case 1:
				if (index < this.Catches.Length)
				{
					return this.Catches[index];
				}
				break;
			case 2:
				if (this.Finally != null)
				{
					return this.Finally;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0007452C File Offset: 0x0007272C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Body != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				if (previousIndex + 1 < this.Catches.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0068;
			case 2:
			case 3:
				IL_0079:
				return new ValueTuple<bool, int, int>(false, 3, 0);
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Catches.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0068:
			if (this.Finally != null)
			{
				return new ValueTuple<bool, int, int>(true, 2, 0);
			}
			goto IL_0079;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x000745CC File Offset: 0x000727CC
		public override ITypeSymbol Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000745CF File Offset: 0x000727CF
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x000745D2 File Offset: 0x000727D2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Try;
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000745D6 File Offset: 0x000727D6
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitTry(this);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000745DF File Offset: 0x000727DF
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitTry(this, argument);
		}
	}
}
