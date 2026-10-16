using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000412 RID: 1042
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class AnonymousObjectCreationOperation : Operation, IAnonymousObjectCreationOperation, IOperation
	{
		// Token: 0x06002803 RID: 10243 RVA: 0x00076675 File Offset: 0x00074875
		internal AnonymousObjectCreationOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> initializers, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Initializers = Operation.SetParentOperation<IOperation>(initializers, this);
			this.Type = type;
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x00076696 File Offset: 0x00074896
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> Initializers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000766A0 File Offset: 0x000748A0
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Initializers.Length)
			{
				return this.Initializers[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000766E8 File Offset: 0x000748E8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Initializers.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Initializers.Length)
				{
					return new ValueTuple<bool, int, int>(true, 0, previousIndex + 1);
				}
				break;
			case 1:
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x0007675E File Offset: 0x0007495E
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x00076766 File Offset: 0x00074966
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x00076769 File Offset: 0x00074969
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.AnonymousObjectCreation;
			}
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x0007676D File Offset: 0x0007496D
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitAnonymousObjectCreation(this);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x00076776 File Offset: 0x00074976
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitAnonymousObjectCreation(this, argument);
		}
	}
}
