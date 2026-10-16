using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200040F RID: 1039
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ConditionalAccessOperation : Operation, IConditionalAccessOperation, IOperation
	{
		// Token: 0x060027E8 RID: 10216 RVA: 0x000763F1 File Offset: 0x000745F1
		internal ConditionalAccessOperation(IOperation operation, IOperation whenNotNull, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Operation = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(operation, this);
			this.WhenNotNull = Microsoft.CodeAnalysis.Operation.SetParentOperation<IOperation>(whenNotNull, this);
			this.Type = type;
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x00076420 File Offset: 0x00074620
		public IOperation Operation { get; }

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x00076428 File Offset: 0x00074628
		public IOperation WhenNotNull { get; }

		// Token: 0x060027EB RID: 10219 RVA: 0x00076430 File Offset: 0x00074630
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (this.WhenNotNull != null)
					{
						return this.WhenNotNull;
					}
				}
			}
			else if (this.Operation != null)
			{
				return this.Operation;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x00076480 File Offset: 0x00074680
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (this.Operation != null)
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
			if (this.WhenNotNull != null)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_003C:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060027ED RID: 10221 RVA: 0x000764E3 File Offset: 0x000746E3
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000764EB File Offset: 0x000746EB
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000764EE File Offset: 0x000746EE
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ConditionalAccess;
			}
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000764F2 File Offset: 0x000746F2
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitConditionalAccess(this);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000764FB File Offset: 0x000746FB
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitConditionalAccess(this, argument);
		}
	}
}
