using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000407 RID: 1031
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class InstanceReferenceOperation : Operation, IInstanceReferenceOperation, IOperation
	{
		// Token: 0x0600279B RID: 10139 RVA: 0x00075D59 File Offset: 0x00073F59
		internal InstanceReferenceOperation(InstanceReferenceKind referenceKind, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.ReferenceKind = referenceKind;
			this.Type = type;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x00075D74 File Offset: 0x00073F74
		public InstanceReferenceKind ReferenceKind { get; }

		// Token: 0x0600279D RID: 10141 RVA: 0x00075D7C File Offset: 0x00073F7C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00075D8F File Offset: 0x00073F8F
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00075DA1 File Offset: 0x00073FA1
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x00075DA9 File Offset: 0x00073FA9
		internal override ConstantValue OperationConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x00075DAC File Offset: 0x00073FAC
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.InstanceReference;
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00075DB0 File Offset: 0x00073FB0
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitInstanceReference(this);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x00075DB9 File Offset: 0x00073FB9
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitInstanceReference(this, argument);
		}
	}
}
