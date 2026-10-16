using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003FD RID: 1021
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PropertyReferenceOperation : BaseMemberReferenceOperation, IPropertyReferenceOperation, IMemberReferenceOperation, IOperation
	{
		// Token: 0x06002729 RID: 10025 RVA: 0x000751C1 File Offset: 0x000733C1
		[NullableContext(2)]
		internal PropertyReferenceOperation([Nullable(1)] IPropertySymbol property, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IArgumentOperation> arguments, IOperation instance, SemanticModel semanticModel, [Nullable(1)] SyntaxNode syntax, ITypeSymbol type, bool isImplicit)
			: base(instance, semanticModel, syntax, isImplicit)
		{
			this.Property = property;
			this.Arguments = Operation.SetParentOperation<IArgumentOperation>(arguments, this);
			this.Type = type;
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x000751EC File Offset: 0x000733EC
		public IPropertySymbol Property { get; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x000751F4 File Offset: 0x000733F4
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000751FC File Offset: 0x000733FC
		protected override IOperation GetCurrent(int slot, int index)
		{
			if (slot != 0)
			{
				if (slot == 1)
				{
					if (index < this.Arguments.Length)
					{
						return this.Arguments[index];
					}
				}
			}
			else if (base.Instance != null)
			{
				return base.Instance;
			}
			throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(slot, index));
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x0007525C File Offset: 0x0007345C
		[NullableContext(0)]
		[return: TupleElementNames(new string[] { "hasNext", "nextSlot", "nextIndex" })]
		protected override ValueTuple<bool, int, int> MoveNext(int previousSlot, int previousIndex)
		{
			switch (previousSlot)
			{
			case -1:
				if (base.Instance != null)
				{
					return new ValueTuple<bool, int, int>(true, 0, 0);
				}
				break;
			case 0:
				break;
			case 1:
				if (previousIndex + 1 < this.Arguments.Length)
				{
					return new ValueTuple<bool, int, int>(true, 1, previousIndex + 1);
				}
				goto IL_0064;
			case 2:
				goto IL_0064;
			default:
				throw ExceptionUtilities.UnexpectedValue(new ValueTuple<int, int>(previousSlot, previousIndex));
			}
			if (!this.Arguments.IsEmpty)
			{
				return new ValueTuple<bool, int, int>(true, 1, 0);
			}
			IL_0064:
			return new ValueTuple<bool, int, int>(false, 2, 0);
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x000752E7 File Offset: 0x000734E7
		[Nullable(2)]
		public override ITypeSymbol Type
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x000752EF File Offset: 0x000734EF
		[Nullable(2)]
		internal override ConstantValue OperationConstantValue
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000752F2 File Offset: 0x000734F2
		public override OperationKind Kind
		{
			get
			{
				return OperationKind.PropertyReference;
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000752F6 File Offset: 0x000734F6
		public override void Accept(OperationVisitor visitor)
		{
			visitor.VisitPropertyReference(this);
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000752FF File Offset: 0x000734FF
		[NullableContext(2)]
		public override TResult Accept<TArgument, TResult>([Nullable(1)] OperationVisitor<TArgument, TResult> visitor, [Nullable(1)] TArgument argument)
		{
			return visitor.VisitPropertyReference(this, argument);
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x00075309 File Offset: 0x00073509
		public override ISymbol Member
		{
			get
			{
				return this.Property;
			}
		}
	}
}
