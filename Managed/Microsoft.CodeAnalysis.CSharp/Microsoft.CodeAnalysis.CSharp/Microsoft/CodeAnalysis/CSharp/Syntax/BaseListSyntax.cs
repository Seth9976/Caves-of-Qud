using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000312 RID: 786
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BaseListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004434 RID: 17460 RVA: 0x00114846 File Offset: 0x00112A46
		internal BaseListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x00114851 File Offset: 0x00112A51
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((BaseListSyntax)base.Green).colonToken, base.Position, 0);
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x00114870 File Offset: 0x00112A70
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<BaseTypeSyntax> Types
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.types, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<BaseTypeSyntax>);
				}
				return new SeparatedSyntaxList<BaseTypeSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x001148A5 File Offset: 0x00112AA5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.types, 1);
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x001148BA File Offset: 0x00112ABA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.types;
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x001148C8 File Offset: 0x00112AC8
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBaseList(this);
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x001148D1 File Offset: 0x00112AD1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBaseList(this);
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x001148DC File Offset: 0x00112ADC
		public BaseListSyntax Update(SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<BaseTypeSyntax> types)
		{
			if (!(colonToken != this.ColonToken) && !(types != this.Types))
			{
				return this;
			}
			BaseListSyntax baseListSyntax = SyntaxFactory.BaseList(colonToken, types);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return baseListSyntax;
			}
			return baseListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x00114926 File Offset: 0x00112B26
		public BaseListSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(colonToken, this.Types);
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00114935 File Offset: 0x00112B35
		public BaseListSyntax WithTypes([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<BaseTypeSyntax> types)
		{
			return this.Update(this.ColonToken, types);
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x00114944 File Offset: 0x00112B44
		public BaseListSyntax AddTypes(params BaseTypeSyntax[] items)
		{
			return this.WithTypes(this.Types.AddRange(items));
		}

		// Token: 0x040014F1 RID: 5361
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode types;
	}
}
