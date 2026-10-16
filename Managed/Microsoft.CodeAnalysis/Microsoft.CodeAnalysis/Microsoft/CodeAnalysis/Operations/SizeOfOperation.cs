using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200041C RID: 1052
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class SizeOfOperation : Operation, ISizeOfOperation, IOperation
	{
		// Token: 0x06002861 RID: 10337 RVA: 0x00076EAA File Offset: 0x000750AA
		internal SizeOfOperation([Nullable(1)] ITypeSymbol typeOperand, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.TypeOperand = typeOperand;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x00076ECD File Offset: 0x000750CD
		[Nullable(1)]
		public ITypeSymbol TypeOperand
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x00076ED5 File Offset: 0x000750D5
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x00076EE8 File Offset: 0x000750E8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x00076EFA File Offset: 0x000750FA
		public override ITypeSymbol Type { get; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002866 RID: 10342 RVA: 0x00076F02 File Offset: 0x00075102
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x00076F0A File Offset: 0x0007510A
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.SizeOf;
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x00076F0E File Offset: 0x0007510E
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitSizeOf(this);
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x00076F17 File Offset: 0x00075117
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitSizeOf(this, argument);
		}
	}
}
