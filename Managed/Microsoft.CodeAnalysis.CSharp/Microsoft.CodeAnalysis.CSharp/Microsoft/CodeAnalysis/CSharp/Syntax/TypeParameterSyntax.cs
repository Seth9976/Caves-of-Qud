using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200030C RID: 780
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypeParameterSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004352 RID: 17234 RVA: 0x0011262A File Offset: 0x0011082A
		internal TypeParameterSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x00112635 File Offset: 0x00110835
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x0011264C File Offset: 0x0011084C
		public SyntaxToken VarianceKeyword
		{
			get
			{
				SyntaxToken varianceKeyword = ((TypeParameterSyntax)base.Green).varianceKeyword;
				if (varianceKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, varianceKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x0011268C File Offset: 0x0011088C
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((TypeParameterSyntax)base.Green).identifier, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x001126B2 File Offset: 0x001108B2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero(ref this.attributeLists);
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x001126C5 File Offset: 0x001108C5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.attributeLists;
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x001126D2 File Offset: 0x001108D2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeParameter(this);
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x001126DB File Offset: 0x001108DB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameter(this);
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x001126E4 File Offset: 0x001108E4
		public TypeParameterSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken varianceKeyword, SyntaxToken identifier)
		{
			if (!(attributeLists != this.AttributeLists) && !(varianceKeyword != this.VarianceKeyword) && !(identifier != this.Identifier))
			{
				return this;
			}
			TypeParameterSyntax typeParameterSyntax = SyntaxFactory.TypeParameter(attributeLists, varianceKeyword, identifier);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typeParameterSyntax;
			}
			return typeParameterSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x0011273D File Offset: 0x0011093D
		public TypeParameterSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.VarianceKeyword, this.Identifier);
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x00112752 File Offset: 0x00110952
		public TypeParameterSyntax WithVarianceKeyword(SyntaxToken varianceKeyword)
		{
			return this.Update(this.AttributeLists, varianceKeyword, this.Identifier);
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x00112767 File Offset: 0x00110967
		public TypeParameterSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.VarianceKeyword, identifier);
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x0011277C File Offset: 0x0011097C
		public TypeParameterSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x040014DE RID: 5342
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;
	}
}
