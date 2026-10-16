using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200041D RID: 1053
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AddressOfOperation : Operation, IAddressOfOperation, IOperation
	{
		// Token: 0x0600286A RID: 10346 RVA: 0x00076F21 File Offset: 0x00075121
		internal AddressOfOperation(IOperation reference, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Reference = Operation.SetParentOperation<IOperation>(reference, this);
			this.Type = type;
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x00076F42 File Offset: 0x00075142
		public IOperation Reference { get; }

		// Token: 0x0600286C RID: 10348 RVA: 0x00076F4C File Offset: 0x0007514C
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.Reference != null)
			{
				return this.Reference;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x00076F80 File Offset: 0x00075180
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
			else if (this.Reference != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x00076FB7 File Offset: 0x000751B7
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x00076FBF File Offset: 0x000751BF
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x00076FC2 File Offset: 0x000751C2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.AddressOf;
			}
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00076FC6 File Offset: 0x000751C6
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitAddressOf(this);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00076FCF File Offset: 0x000751CF
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitAddressOf(this, argument);
		}
	}
}
