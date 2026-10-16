using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002FE RID: 766
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DefaultSwitchLabelSyntax : SwitchLabelSyntax
	{
		// Token: 0x06004297 RID: 17047 RVA: 0x00111169 File Offset: 0x0010F369
		internal DefaultSwitchLabelSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x00111174 File Offset: 0x0010F374
		public override SyntaxToken Keyword
		{
			get
			{
				return new SyntaxToken(this, ((DefaultSwitchLabelSyntax)base.Green).keyword, base.Position, 0);
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x00111193 File Offset: 0x0010F393
		public override SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((DefaultSwitchLabelSyntax)base.Green).colonToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x001111B9 File Offset: 0x0010F3B9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x001111BC File Offset: 0x0010F3BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x001111BF File Offset: 0x0010F3BF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefaultSwitchLabel(this);
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x001111C8 File Offset: 0x0010F3C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefaultSwitchLabel(this);
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x001111D4 File Offset: 0x0010F3D4
		public DefaultSwitchLabelSyntax Update(SyntaxToken keyword, SyntaxToken colonToken)
		{
			if (!(keyword != this.Keyword) && !(colonToken != this.ColonToken))
			{
				return this;
			}
			DefaultSwitchLabelSyntax defaultSwitchLabelSyntax = SyntaxFactory.DefaultSwitchLabel(keyword, colonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return defaultSwitchLabelSyntax;
			}
			return defaultSwitchLabelSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x0011121E File Offset: 0x0010F41E
		internal override SwitchLabelSyntax WithKeywordCore(SyntaxToken keyword)
		{
			return this.WithKeyword(keyword);
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x00111227 File Offset: 0x0010F427
		public new DefaultSwitchLabelSyntax WithKeyword(SyntaxToken keyword)
		{
			return this.Update(keyword, this.ColonToken);
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x00111236 File Offset: 0x0010F436
		internal override SwitchLabelSyntax WithColonTokenCore(SyntaxToken colonToken)
		{
			return this.WithColonToken(colonToken);
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x0011123F File Offset: 0x0010F43F
		public new DefaultSwitchLabelSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.Keyword, colonToken);
		}
	}
}
