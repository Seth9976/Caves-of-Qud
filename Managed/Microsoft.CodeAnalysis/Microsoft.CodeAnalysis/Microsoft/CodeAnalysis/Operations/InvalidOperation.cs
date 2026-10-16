using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200046A RID: 1130
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class InvalidOperation : Operation, IInvalidOperation, IOperation
	{
		// Token: 0x06002C45 RID: 11333 RVA: 0x0007D33C File Offset: 0x0007B53C
		public InvalidOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> children, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Children = Operation.SetParentOperation<IOperation>(children, this);
			this.Type = type;
			this.OperationConstantValue = constantValue;
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x0007D365 File Offset: 0x0007B565
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IOperation> Children
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x0007D370 File Offset: 0x0007B570
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Children.Length)
			{
				return this.Children[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0007D3B8 File Offset: 0x0007B5B8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Children.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Children.Length)
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

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x0007D42E File Offset: 0x0007B62E
		public override ITypeSymbol Type { get; }

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x0007D436 File Offset: 0x0007B636
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x0007D43E File Offset: 0x0007B63E
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Invalid;
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x0007D441 File Offset: 0x0007B641
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitInvalid(this);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0007D44A File Offset: 0x0007B64A
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitInvalid(this, argument);
		}
	}
}
