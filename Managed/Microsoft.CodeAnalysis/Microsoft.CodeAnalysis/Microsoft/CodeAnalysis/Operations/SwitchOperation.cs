using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003E2 RID: 994
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SwitchOperation : Operation, ISwitchOperation, IOperation
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x000735FA File Offset: 0x000717FA
		internal SwitchOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalSymbol> locals, IOperation value, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ISwitchCaseOperation> cases, ILabelSymbol exitLabel, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Locals = locals;
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
			this.Cases = Operation.SetParentOperation<ISwitchCaseOperation>(cases, this);
			this.ExitLabel = exitLabel;
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x00073631 File Offset: 0x00071831
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00073639 File Offset: 0x00071839
		public IOperation Value { get; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x00073641 File Offset: 0x00071841
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISwitchCaseOperation> Cases
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x00073649 File Offset: 0x00071849
		public ILabelSymbol ExitLabel { get; }

		// Token: 0x06002611 RID: 9745 RVA: 0x00073654 File Offset: 0x00071854
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.Cases.Length)
					{
						return this.Cases[index];
					}
				}
			}
			else if (this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000736B4 File Offset: 0x000718B4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Value != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				if (previousIndex + 1 < this.Cases.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0064;
			case 2:
				goto IL_0064;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Cases.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0064:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x0007373F File Offset: 0x0007193F
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x00073742 File Offset: 0x00071942
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x00073745 File Offset: 0x00071945
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.Switch;
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x00073748 File Offset: 0x00071948
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitSwitch(this);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00073751 File Offset: 0x00071951
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitSwitch(this, argument);
		}
	}
}
