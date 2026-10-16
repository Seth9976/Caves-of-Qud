using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000411 RID: 1041
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class InterpolatedStringOperation : Operation, IInterpolatedStringOperation, IOperation
	{
		// Token: 0x060027FA RID: 10234 RVA: 0x0007655F File Offset: 0x0007475F
		internal InterpolatedStringOperation([Nullable(new byte[] { 0, 1 })] ImmutableArray<IInterpolatedStringContentOperation> parts, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Parts = Operation.SetParentOperation<IInterpolatedStringContentOperation>(parts, this);
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00076588 File Offset: 0x00074788
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IInterpolatedStringContentOperation> Parts
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00076590 File Offset: 0x00074790
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && index < this.Parts.Length)
			{
				return this.Parts[index];
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000765D8 File Offset: 0x000747D8
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (!this.Parts.IsEmpty)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				if (previousIndex + 1 < this.Parts.Length)
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

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x0007664E File Offset: 0x0007484E
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x00076656 File Offset: 0x00074856
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x0007665E File Offset: 0x0007485E
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.InterpolatedString;
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x00076662 File Offset: 0x00074862
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitInterpolatedString(this);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0007666B File Offset: 0x0007486B
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitInterpolatedString(this, argument);
		}
	}
}
