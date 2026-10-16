using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000420 RID: 1056
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ThrowOperation : Operation, IThrowOperation, IOperation
	{
		// Token: 0x0600288A RID: 10378 RVA: 0x000771FD File Offset: 0x000753FD
		internal ThrowOperation(IOperation exception, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Exception = Operation.SetParentOperation<IOperation>(exception, this);
			this.Type = type;
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x0007721E File Offset: 0x0007541E
		public IOperation Exception { get; }

		// Token: 0x0600288C RID: 10380 RVA: 0x00077228 File Offset: 0x00075428
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Exception != null)
			{
				return this.Exception;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0007725C File Offset: 0x0007545C
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
			else if (this.Exception != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x00077293 File Offset: 0x00075493
		public override ITypeSymbol Type { get; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x0007729B File Offset: 0x0007549B
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x0007729E File Offset: 0x0007549E
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Throw;
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000772A2 File Offset: 0x000754A2
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitThrow(this);
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000772AB File Offset: 0x000754AB
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitThrow(this, argument);
		}
	}
}
