using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000410 RID: 1040
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ConditionalAccessInstanceOperation : Operation, IConditionalAccessInstanceOperation, IOperation
	{
		// Token: 0x060027F2 RID: 10226 RVA: 0x00076505 File Offset: 0x00074705
		internal ConditionalAccessInstanceOperation(SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Type = type;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x00076518 File Offset: 0x00074718
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0007652B File Offset: 0x0007472B
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x0007653D File Offset: 0x0007473D
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00076545 File Offset: 0x00074745
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00076548 File Offset: 0x00074748
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.ConditionalAccessInstance;
			}
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0007654C File Offset: 0x0007474C
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitConditionalAccessInstance(this);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00076555 File Offset: 0x00074755
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitConditionalAccessInstance(this, argument);
		}
	}
}
