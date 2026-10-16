using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000453 RID: 1107
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class UsingDeclarationOperation : Operation, IUsingDeclarationOperation, IOperation
	{
		// Token: 0x06002A66 RID: 10854 RVA: 0x00079C6F File Offset: 0x00077E6F
		internal UsingDeclarationOperation(IVariableDeclarationGroupOperation declarationGroup, bool isAsynchronous, DisposeOperationInfo disposeInfo, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.DeclarationGroup = Operation.SetParentOperation<IVariableDeclarationGroupOperation>(declarationGroup, this);
			this.IsAsynchronous = isAsynchronous;
			this.DisposeInfo = disposeInfo;
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x00079C98 File Offset: 0x00077E98
		public IVariableDeclarationGroupOperation DeclarationGroup { get; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x00079CA0 File Offset: 0x00077EA0
		public bool IsAsynchronous { get; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x00079CA8 File Offset: 0x00077EA8
		public DisposeOperationInfo DisposeInfo { get; }

		// Token: 0x06002A6A RID: 10858 RVA: 0x00079CB0 File Offset: 0x00077EB0
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && this.DeclarationGroup != null)
			{
				return this.DeclarationGroup;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x00079CE4 File Offset: 0x00077EE4
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			if (previousSlot != -1)
			{
				if (previousSlot > 1)
				{
					throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
				}
			}
			else if (this.DeclarationGroup != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002A6C RID: 10860 RVA: 0x00079D1B File Offset: 0x00077F1B
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x00079D1E File Offset: 0x00077F1E
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x00079D21 File Offset: 0x00077F21
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.UsingDeclaration;
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00079D25 File Offset: 0x00077F25
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitUsingDeclaration(this);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00079D2E File Offset: 0x00077F2E
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitUsingDeclaration(this, argument);
		}
	}
}
