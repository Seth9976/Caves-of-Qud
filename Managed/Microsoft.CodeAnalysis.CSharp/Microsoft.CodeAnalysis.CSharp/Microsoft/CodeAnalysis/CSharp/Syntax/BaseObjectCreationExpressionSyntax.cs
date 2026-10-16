using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002C1 RID: 705
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class BaseObjectCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06003FEB RID: 16363 RVA: 0x0010CBE2 File Offset: 0x0010ADE2
		internal BaseObjectCreationExpressionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06003FEC RID: 16364
		public abstract SyntaxToken NewKeyword { get; }

		// Token: 0x06003FED RID: 16365 RVA: 0x0010CBED File Offset: 0x0010ADED
		public BaseObjectCreationExpressionSyntax WithNewKeyword(SyntaxToken newKeyword)
		{
			return this.WithNewKeywordCore(newKeyword);
		}

		// Token: 0x06003FEE RID: 16366
		internal abstract BaseObjectCreationExpressionSyntax WithNewKeywordCore(SyntaxToken newKeyword);

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06003FEF RID: 16367
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract ArgumentListSyntax ArgumentList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x0010CBF6 File Offset: 0x0010ADF6
		public BaseObjectCreationExpressionSyntax WithArgumentList([global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList)
		{
			return this.WithArgumentListCore(argumentList);
		}

		// Token: 0x06003FF1 RID: 16369
		internal abstract BaseObjectCreationExpressionSyntax WithArgumentListCore([global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList);

		// Token: 0x06003FF2 RID: 16370 RVA: 0x0010CBFF File Offset: 0x0010ADFF
		public BaseObjectCreationExpressionSyntax AddArgumentListArguments(params ArgumentSyntax[] items)
		{
			return this.AddArgumentListArgumentsCore(items);
		}

		// Token: 0x06003FF3 RID: 16371
		internal abstract BaseObjectCreationExpressionSyntax AddArgumentListArgumentsCore(params ArgumentSyntax[] items);

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06003FF4 RID: 16372
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x0010CC08 File Offset: 0x0010AE08
		public BaseObjectCreationExpressionSyntax WithInitializer([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return this.WithInitializerCore(initializer);
		}

		// Token: 0x06003FF6 RID: 16374
		internal abstract BaseObjectCreationExpressionSyntax WithInitializerCore([global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer);
	}
}
