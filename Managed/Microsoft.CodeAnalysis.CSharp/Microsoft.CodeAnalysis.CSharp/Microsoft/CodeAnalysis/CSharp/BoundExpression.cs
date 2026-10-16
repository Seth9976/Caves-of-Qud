using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200008A RID: 138
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundExpression : BoundNode
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x0005282E File Offset: 0x00050A2E
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal BoundExpression WithSuppression(bool suppress = true)
		{
			if (base.IsSuppressed == suppress)
			{
				return this;
			}
			BoundExpression boundExpression = (BoundExpression)base.MemberwiseClone();
			boundExpression.IsSuppressed = suppress;
			return boundExpression;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0005284D File Offset: 0x00050A4D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal BoundExpression WithWasConverted()
		{
			return this;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00052850 File Offset: 0x00050A50
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal new BoundExpression WithHasErrors()
		{
			return (BoundExpression)base.WithHasErrors();
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00052860 File Offset: 0x00050A60
		internal bool NeedsToBeConverted()
		{
			BoundKind kind = base.Kind;
			if (kind <= BoundKind.UnconvertedSwitchExpression)
			{
				if (kind != BoundKind.UnconvertedConditionalOperator && kind != BoundKind.DefaultLiteral && kind != BoundKind.UnconvertedSwitchExpression)
				{
					return false;
				}
			}
			else if (kind <= BoundKind.TupleLiteral)
			{
				if (kind != BoundKind.UnconvertedObjectCreationExpression && kind != BoundKind.TupleLiteral)
				{
					return false;
				}
			}
			else
			{
				if (kind == BoundKind.StackAllocArrayCreation)
				{
					return this.Type == null;
				}
				if (kind != BoundKind.UnconvertedInterpolatedString)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000528C1 File Offset: 0x00050AC1
		public virtual ConstantValue ConstantValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000528C4 File Offset: 0x00050AC4
		public virtual Symbol ExpressionSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000528C7 File Offset: 0x00050AC7
		public virtual LookupResultKind ResultKind
		{
			get
			{
				return LookupResultKind.Viable;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000528CB File Offset: 0x00050ACB
		public virtual bool SuppressVirtualCalls
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000528CE File Offset: 0x00050ACE
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x000528D6 File Offset: 0x00050AD6
		public new NullabilityInfo TopLevelNullability
		{
			get
			{
				return base.TopLevelNullability;
			}
			set
			{
				base.TopLevelNullability = value;
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000528E0 File Offset: 0x00050AE0
		public ITypeSymbol GetPublicTypeSymbol()
		{
			TypeSymbol type = this.Type;
			if (type == null)
			{
				return null;
			}
			return type.GetITypeSymbol(this.TopLevelNullability.FlowState.ToAnnotation());
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00052911 File Offset: 0x00050B11
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public virtual object Display
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.Type;
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00052919 File Offset: 0x00050B19
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BoundExpression(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors)
			: base(kind, syntax, hasErrors)
		{
			this.Type = type;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0005292C File Offset: 0x00050B2C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BoundExpression(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: base(kind, syntax)
		{
			this.Type = type;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0005293D File Offset: 0x00050B3D
		public TypeSymbol Type { get; }
	}
}
