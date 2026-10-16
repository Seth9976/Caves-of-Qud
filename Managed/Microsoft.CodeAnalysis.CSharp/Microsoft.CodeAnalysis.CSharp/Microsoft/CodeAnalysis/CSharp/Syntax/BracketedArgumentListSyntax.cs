using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002BB RID: 699
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BracketedArgumentListSyntax : BaseArgumentListSyntax
	{
		// Token: 0x06003FA3 RID: 16291 RVA: 0x0010C47E File Offset: 0x0010A67E
		internal BracketedArgumentListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x0010C489 File Offset: 0x0010A689
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((BracketedArgumentListSyntax)base.Green).openBracketToken, base.Position, 0);
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x0010C4A8 File Offset: 0x0010A6A8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<ArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.arguments, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ArgumentSyntax>);
				}
				return new SeparatedSyntaxList<ArgumentSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x0010C4DD File Offset: 0x0010A6DD
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((BracketedArgumentListSyntax)base.Green).closeBracketToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0010C503 File Offset: 0x0010A703
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.arguments, 1);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x0010C518 File Offset: 0x0010A718
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.arguments;
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x0010C526 File Offset: 0x0010A726
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBracketedArgumentList(this);
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x0010C52F File Offset: 0x0010A72F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBracketedArgumentList(this);
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x0010C538 File Offset: 0x0010A738
		public BracketedArgumentListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
		{
			if (!(openBracketToken != this.OpenBracketToken) && !(arguments != this.Arguments) && !(closeBracketToken != this.CloseBracketToken))
			{
				return this;
			}
			BracketedArgumentListSyntax bracketedArgumentListSyntax = SyntaxFactory.BracketedArgumentList(openBracketToken, arguments, closeBracketToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return bracketedArgumentListSyntax;
			}
			return bracketedArgumentListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x0010C591 File Offset: 0x0010A791
		public BracketedArgumentListSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(openBracketToken, this.Arguments, this.CloseBracketToken);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x0010C5A6 File Offset: 0x0010A7A6
		internal override BaseArgumentListSyntax WithArgumentsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments)
		{
			return this.WithArguments(arguments);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0010C5AF File Offset: 0x0010A7AF
		public new BracketedArgumentListSyntax WithArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments)
		{
			return this.Update(this.OpenBracketToken, arguments, this.CloseBracketToken);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0010C5C4 File Offset: 0x0010A7C4
		public BracketedArgumentListSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.OpenBracketToken, this.Arguments, closeBracketToken);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0010C5D9 File Offset: 0x0010A7D9
		internal override BaseArgumentListSyntax AddArgumentsCore(params ArgumentSyntax[] items)
		{
			return this.AddArguments(items);
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x0010C5E4 File Offset: 0x0010A7E4
		public new BracketedArgumentListSyntax AddArguments(params ArgumentSyntax[] items)
		{
			return this.WithArguments(this.Arguments.AddRange(items));
		}

		// Token: 0x0400147A RID: 5242
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode arguments;
	}
}
