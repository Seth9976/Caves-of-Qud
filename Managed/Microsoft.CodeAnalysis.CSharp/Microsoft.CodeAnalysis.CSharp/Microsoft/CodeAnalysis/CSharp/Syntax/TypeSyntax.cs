using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000288 RID: 648
	public abstract class TypeSyntax : ExpressionSyntax
	{
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x0010844B File Offset: 0x0010664B
		public bool IsVar
		{
			get
			{
				return ((TypeSyntax)base.Green).IsVar;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x0010845D File Offset: 0x0010665D
		public bool IsUnmanaged
		{
			get
			{
				return ((TypeSyntax)base.Green).IsUnmanaged;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x0010846F File Offset: 0x0010666F
		public bool IsNotNull
		{
			get
			{
				return ((TypeSyntax)base.Green).IsNotNull;
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x00108481 File Offset: 0x00106681
		public bool IsNint
		{
			get
			{
				return ((TypeSyntax)base.Green).IsNint;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06003D5D RID: 15709 RVA: 0x00108493 File Offset: 0x00106693
		public bool IsNuint
		{
			get
			{
				return ((TypeSyntax)base.Green).IsNuint;
			}
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x001084A5 File Offset: 0x001066A5
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal TypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
