using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000469 RID: 1129
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class NoneOperation : Operation
	{
		// Token: 0x06002C3C RID: 11324 RVA: 0x0007D227 File Offset: 0x0007B427
		public NoneOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> children, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Children = Operation.SetParentOperation<IOperation>(children, this);
			this.Type = type;
			this.OperationConstantValue = constantValue;
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x0007D250 File Offset: 0x0007B450
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<IOperation> Children
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x0007D258 File Offset: 0x0007B458
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Children.Length)
			{
				return this.Children[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x0007D2A0 File Offset: 0x0007B4A0
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

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x0007D316 File Offset: 0x0007B516
		public override ITypeSymbol Type { get; }

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x0007D31E File Offset: 0x0007B51E
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x0007D326 File Offset: 0x0007B526
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.None;
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x0007D329 File Offset: 0x0007B529
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitNoneOperation(this);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x0007D332 File Offset: 0x0007B532
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitNoneOperation(this, argument);
		}
	}
}
