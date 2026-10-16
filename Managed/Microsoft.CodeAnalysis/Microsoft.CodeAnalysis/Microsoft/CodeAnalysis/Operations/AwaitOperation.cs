using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000409 RID: 1033
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AwaitOperation : Operation, IAwaitOperation, IOperation
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x00075E99 File Offset: 0x00074099
		internal AwaitOperation(IOperation operation, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operation = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(operation, this);
			this.Type = type;
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x00075EBA File Offset: 0x000740BA
		public IOperation Operation { get; }

		// Token: 0x060027B1 RID: 10161 RVA: 0x00075EC4 File Offset: 0x000740C4
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Operation != null)
			{
				return this.Operation;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x00075EF8 File Offset: 0x000740F8
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

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x00075F2F File Offset: 0x0007412F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x00075F37 File Offset: 0x00074137
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x00075F3A File Offset: 0x0007413A
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Await;
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00075F3E File Offset: 0x0007413E
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitAwait(this);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00075F47 File Offset: 0x00074147
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitAwait(this, argument);
		}
	}
}
