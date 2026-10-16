using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000BA RID: 186
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundPattern : BoundNode
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x00056C50 File Offset: 0x00054E50
		internal bool IsNegated(out BoundPattern innerPattern)
		{
			innerPattern = this;
			bool flag = false;
			for (;;)
			{
				BoundNegatedPattern boundNegatedPattern = innerPattern as BoundNegatedPattern;
				if (boundNegatedPattern == null)
				{
					break;
				}
				flag = !flag;
				innerPattern = boundNegatedPattern.Negated;
			}
			return flag;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00056C7D File Offset: 0x00054E7D
		protected BoundPattern(BoundKind kind, SyntaxNode syntax, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors)
			: base(kind, syntax, hasErrors)
		{
			this.InputType = inputType;
			this.NarrowedType = narrowedType;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00056C98 File Offset: 0x00054E98
		protected BoundPattern(BoundKind kind, SyntaxNode syntax, TypeSymbol inputType, TypeSymbol narrowedType)
			: base(kind, syntax)
		{
			this.InputType = inputType;
			this.NarrowedType = narrowedType;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00056CB1 File Offset: 0x00054EB1
		public TypeSymbol InputType { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00056CB9 File Offset: 0x00054EB9
		public TypeSymbol NarrowedType { get; }
	}
}
