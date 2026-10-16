using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002BA RID: 698
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ArgumentListSyntax : BaseArgumentListSyntax
	{
		// Token: 0x06003F94 RID: 16276 RVA: 0x0010C2F3 File Offset: 0x0010A4F3
		internal ArgumentListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x0010C2FE File Offset: 0x0010A4FE
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ArgumentListSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x0010C320 File Offset: 0x0010A520
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

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x0010C355 File Offset: 0x0010A555
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ArgumentListSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0010C37B File Offset: 0x0010A57B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.arguments, 1);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x0010C390 File Offset: 0x0010A590
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.arguments;
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x0010C39E File Offset: 0x0010A59E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArgumentList(this);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x0010C3A7 File Offset: 0x0010A5A7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArgumentList(this);
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x0010C3B0 File Offset: 0x0010A5B0
		public ArgumentListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(arguments != this.Arguments) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			ArgumentListSyntax argumentListSyntax = SyntaxFactory.ArgumentList(openParenToken, arguments, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return argumentListSyntax;
			}
			return argumentListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x0010C409 File Offset: 0x0010A609
		public ArgumentListSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Arguments, this.CloseParenToken);
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x0010C41E File Offset: 0x0010A61E
		internal override BaseArgumentListSyntax WithArgumentsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments)
		{
			return this.WithArguments(arguments);
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0010C427 File Offset: 0x0010A627
		public new ArgumentListSyntax WithArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments)
		{
			return this.Update(this.OpenParenToken, arguments, this.CloseParenToken);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0010C43C File Offset: 0x0010A63C
		public ArgumentListSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Arguments, closeParenToken);
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x0010C451 File Offset: 0x0010A651
		internal override BaseArgumentListSyntax AddArgumentsCore(params ArgumentSyntax[] items)
		{
			return this.AddArguments(items);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0010C45C File Offset: 0x0010A65C
		public new ArgumentListSyntax AddArguments(params ArgumentSyntax[] items)
		{
			return this.WithArguments(this.Arguments.AddRange(items));
		}

		// Token: 0x04001479 RID: 5241
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode arguments;
	}
}
