using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200041A RID: 1050
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class DefaultValueOperation : Operation, IDefaultValueOperation, IOperation
	{
		// Token: 0x06002850 RID: 10320 RVA: 0x00076DD9 File Offset: 0x00074FD9
		internal DefaultValueOperation(SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00076DF4 File Offset: 0x00074FF4
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00076E07 File Offset: 0x00075007
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x00076E19 File Offset: 0x00075019
		public override ITypeSymbol Type { get; }

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x00076E21 File Offset: 0x00075021
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x00076E29 File Offset: 0x00075029
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.DefaultValue;
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x00076E2D File Offset: 0x0007502D
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitDefaultValue(this);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00076E36 File Offset: 0x00075036
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitDefaultValue(this, argument);
		}
	}
}
