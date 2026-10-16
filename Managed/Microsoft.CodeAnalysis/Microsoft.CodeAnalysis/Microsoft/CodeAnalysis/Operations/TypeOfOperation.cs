using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200041B RID: 1051
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TypeOfOperation : Operation, ITypeOfOperation, IOperation
	{
		// Token: 0x06002858 RID: 10328 RVA: 0x00076E40 File Offset: 0x00075040
		internal TypeOfOperation(ITypeSymbol typeOperand, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, [Nullable(2)] ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.TypeOperand = typeOperand;
			this.Type = type;
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x00076E5B File Offset: 0x0007505B
		public ITypeSymbol TypeOperand { get; }

		// Token: 0x0600285A RID: 10330 RVA: 0x00076E63 File Offset: 0x00075063
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00076E76 File Offset: 0x00075076
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x00076E88 File Offset: 0x00075088
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x00076E90 File Offset: 0x00075090
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x00076E93 File Offset: 0x00075093
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.TypeOf;
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x00076E97 File Offset: 0x00075097
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitTypeOf(this);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x00076EA0 File Offset: 0x000750A0
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitTypeOf(this, argument);
		}
	}
}
