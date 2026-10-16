using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000443 RID: 1091
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class CaughtExceptionOperation : Operation, ICaughtExceptionOperation, IOperation
	{
		// Token: 0x060029C5 RID: 10693 RVA: 0x00078D11 File Offset: 0x00076F11
		internal CaughtExceptionOperation(SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Type = type;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00078D24 File Offset: 0x00076F24
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x00078D37 File Offset: 0x00076F37
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x00078D49 File Offset: 0x00076F49
		public override ITypeSymbol Type { get; }

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x00078D51 File Offset: 0x00076F51
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x00078D54 File Offset: 0x00076F54
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.CaughtException;
			}
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00078D58 File Offset: 0x00076F58
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitCaughtException(this);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x00078D61 File Offset: 0x00076F61
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitCaughtException(this, argument);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00078D6B File Offset: 0x00076F6B
		[NullableContext(1)]
		public CaughtExceptionOperation(SyntaxNode syntax, ITypeSymbol type)
			: this(null, syntax, type, true)
		{
		}
	}
}
