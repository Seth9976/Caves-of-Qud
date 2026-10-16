using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200044B RID: 1099
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SwitchExpressionOperation : Operation, ISwitchExpressionOperation, IOperation
	{
		// Token: 0x06002A15 RID: 10773 RVA: 0x0007946B File Offset: 0x0007766B
		internal SwitchExpressionOperation(IOperation value, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ISwitchExpressionArmOperation> arms, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
			this.Arms = Operation.SetParentOperation<ISwitchExpressionArmOperation>(arms, this);
			this.Type = type;
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x0007949A File Offset: 0x0007769A
		public IOperation Value { get; }

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x000794A2 File Offset: 0x000776A2
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISwitchExpressionArmOperation> Arms
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000794AC File Offset: 0x000776AC
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.Arms.Length)
					{
						return this.Arms[index];
					}
				}
			}
			else if (this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x0007950C File Offset: 0x0007770C
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
				if (previousIndex + 1 < this.Arms.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0064;
			case 2:
				goto IL_0064;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Arms.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0064:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x00079597 File Offset: 0x00077797
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x0007959F File Offset: 0x0007779F
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000795A2 File Offset: 0x000777A2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.SwitchExpression;
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000795A6 File Offset: 0x000777A6
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitSwitchExpression(this);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000795AF File Offset: 0x000777AF
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitSwitchExpression(this, argument);
		}
	}
}
