using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000452 RID: 1106
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WithStatementOperation : Operation, IWithStatementOperation, IOperation
	{
		// Token: 0x06002A5C RID: 10844 RVA: 0x00079B69 File Offset: 0x00077D69
		internal WithStatementOperation(IOperation body, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Body = Operation.SetParentOperation<IOperation>(body, this);
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x00079B90 File Offset: 0x00077D90
		public IOperation Body { get; }

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x00079B98 File Offset: 0x00077D98
		public IOperation Value { get; }

		// Token: 0x06002A5F RID: 10847 RVA: 0x00079BA0 File Offset: 0x00077DA0
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Body != null)
					{
						return this.Body;
					}
				}
			}
			else if (this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x00079BF0 File Offset: 0x00077DF0
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
			case 2:
				goto IL_003C;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (this.Body != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x00079C53 File Offset: 0x00077E53
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x00079C56 File Offset: 0x00077E56
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x00079C59 File Offset: 0x00077E59
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.None;
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x00079C5C File Offset: 0x00077E5C
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitWithStatement(this);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00079C65 File Offset: 0x00077E65
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitWithStatement(this, argument);
		}
	}
}
