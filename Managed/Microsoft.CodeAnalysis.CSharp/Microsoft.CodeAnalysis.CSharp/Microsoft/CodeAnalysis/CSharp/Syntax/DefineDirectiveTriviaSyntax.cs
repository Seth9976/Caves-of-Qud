using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200034E RID: 846
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class DefineDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004765 RID: 18277 RVA: 0x00119DBD File Offset: 0x00117FBD
		internal DefineDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x00119DC8 File Offset: 0x00117FC8
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((DefineDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06004767 RID: 18279 RVA: 0x00119DE7 File Offset: 0x00117FE7
		public SyntaxToken DefineKeyword
		{
			get
			{
				return new SyntaxToken(this, ((DefineDirectiveTriviaSyntax)base.Green).defineKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x00119E0D File Offset: 0x0011800D
		public SyntaxToken Name
		{
			get
			{
				return new SyntaxToken(this, ((DefineDirectiveTriviaSyntax)base.Green).name, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06004769 RID: 18281 RVA: 0x00119E33 File Offset: 0x00118033
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((DefineDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x00119E59 File Offset: 0x00118059
		public override bool IsActive
		{
			get
			{
				return ((DefineDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x00119E6B File Offset: 0x0011806B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x00119E6E File Offset: 0x0011806E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x00119E71 File Offset: 0x00118071
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefineDirectiveTrivia(this);
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x00119E7A File Offset: 0x0011807A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefineDirectiveTrivia(this);
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00119E84 File Offset: 0x00118084
		public DefineDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(defineKeyword != this.DefineKeyword) && !(name != this.Name) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			DefineDirectiveTriviaSyntax defineDirectiveTriviaSyntax = SyntaxFactory.DefineDirectiveTrivia(hashToken, defineKeyword, name, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return defineDirectiveTriviaSyntax;
			}
			return defineDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x00119EF0 File Offset: 0x001180F0
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x00119EF9 File Offset: 0x001180F9
		public new DefineDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.DefineKeyword, this.Name, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x00119F1A File Offset: 0x0011811A
		public DefineDirectiveTriviaSyntax WithDefineKeyword(SyntaxToken defineKeyword)
		{
			return this.Update(this.HashToken, defineKeyword, this.Name, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x00119F3B File Offset: 0x0011813B
		public DefineDirectiveTriviaSyntax WithName(SyntaxToken name)
		{
			return this.Update(this.HashToken, this.DefineKeyword, name, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x00119F5C File Offset: 0x0011815C
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x00119F65 File Offset: 0x00118165
		public new DefineDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.DefineKeyword, this.Name, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x00119F86 File Offset: 0x00118186
		public DefineDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.DefineKeyword, this.Name, this.EndOfDirectiveToken, isActive);
		}
	}
}
