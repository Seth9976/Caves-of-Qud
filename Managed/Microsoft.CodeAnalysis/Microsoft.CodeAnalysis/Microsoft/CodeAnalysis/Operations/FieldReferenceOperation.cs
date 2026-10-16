using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003FB RID: 1019
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class FieldReferenceOperation : BaseMemberReferenceOperation, IFieldReferenceOperation, IMemberReferenceOperation, IOperation
	{
		// Token: 0x06002713 RID: 10003 RVA: 0x0007501F File Offset: 0x0007321F
		internal FieldReferenceOperation([Nullable(1)] IFieldSymbol field, bool isDeclaration, IOperation instance, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, ConstantValue constantValue, bool isImplicit)
			: base(instance, semanticModel, syntax, isImplicit)
		{
			this.Field = field;
			this.IsDeclaration = isDeclaration;
			this.OperationConstantValue = constantValue;
			this.Type = type;
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x0007504C File Offset: 0x0007324C
		[Nullable(1)]
		public IFieldSymbol Field
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x00075054 File Offset: 0x00073254
		public bool IsDeclaration { get; }

		// Token: 0x06002716 RID: 10006 RVA: 0x0007505C File Offset: 0x0007325C
		[NullableContext(1)]
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot == 0 && base.Instance != null)
			{
				return base.Instance;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00075090 File Offset: 0x00073290
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
			else if (base.Instance != null)
			{
				return new ValueTuple<bool, int, int>(true, 0, 0);
			}
			return new ValueTuple<bool, int, int>(false, 1, 0);
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x000750C7 File Offset: 0x000732C7
		public override ITypeSymbol Type { get; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x000750CF File Offset: 0x000732CF
		internal override ConstantValue OperationConstantValue { get; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x000750D7 File Offset: 0x000732D7
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.FieldReference;
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000750DB File Offset: 0x000732DB
		[NullableContext(1)]
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitFieldReference(this);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000750E4 File Offset: 0x000732E4
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitFieldReference(this, argument);
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x000750EE File Offset: 0x000732EE
		[Nullable(1)]
		public override ISymbol Member
		{
			[NullableContext(1)]
			get
			{
				return this.Field;
			}
		}
	}
}
