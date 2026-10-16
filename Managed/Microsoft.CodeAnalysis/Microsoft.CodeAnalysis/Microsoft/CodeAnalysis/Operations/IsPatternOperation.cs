using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200041E RID: 1054
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class IsPatternOperation : Operation, IIsPatternOperation, IOperation
	{
		// Token: 0x06002873 RID: 10355 RVA: 0x00076FD9 File Offset: 0x000751D9
		internal IsPatternOperation(IOperation value, IPatternOperation pattern, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
			this.Pattern = Operation.SetParentOperation<IPatternOperation>(pattern, this);
			this.Type = type;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x00077008 File Offset: 0x00075208
		public IOperation Value { get; }

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x00077010 File Offset: 0x00075210
		public IPatternOperation Pattern { get; }

		// Token: 0x06002876 RID: 10358 RVA: 0x00077018 File Offset: 0x00075218
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.Pattern != null)
					{
						return this.Pattern;
					}
				}
			}
			else if (this.Value != null)
			{
				return this.Value;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00077068 File Offset: 0x00075268
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
			if (this.Pattern != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x000770CB File Offset: 0x000752CB
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x000770D3 File Offset: 0x000752D3
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x000770D6 File Offset: 0x000752D6
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.IsPattern;
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000770DA File Offset: 0x000752DA
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitIsPattern(this);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000770E3 File Offset: 0x000752E3
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitIsPattern(this, argument);
		}
	}
}
