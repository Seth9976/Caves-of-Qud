using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003F8 RID: 1016
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class LocalReferenceOperation : Operation, ILocalReferenceOperation, IOperation
	{
		// Token: 0x060026FD RID: 9981 RVA: 0x00074F0D File Offset: 0x0007310D
		internal LocalReferenceOperation([Nullable(1)] ILocalSymbol local, bool isDeclaration, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Local = local;
			this.IsDeclaration = isDeclaration;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x00074F38 File Offset: 0x00073138
		[Nullable(1)]
		public ILocalSymbol Local
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00074F40 File Offset: 0x00073140
		public bool IsDeclaration { get; }

		// Token: 0x06002700 RID: 9984 RVA: 0x00074F48 File Offset: 0x00073148
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00074F5B File Offset: 0x0007315B
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			return new ValueTuple<bool, int, int>(false, int.MinValue, int.MinValue);
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x00074F6D File Offset: 0x0007316D
		public override ITypeSymbol Type { get; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x00074F75 File Offset: 0x00073175
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x00074F7D File Offset: 0x0007317D
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.LocalReference;
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x00074F81 File Offset: 0x00073181
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitLocalReference(this);
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x00074F8A File Offset: 0x0007318A
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitLocalReference(this, argument);
		}
	}
}
