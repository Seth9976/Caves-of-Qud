using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200029B RID: 667
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class OmittedTypeArgumentSyntax : TypeSyntax
	{
		// Token: 0x06003E50 RID: 15952 RVA: 0x0010A1B7 File Offset: 0x001083B7
		internal OmittedTypeArgumentSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x0010A1C2 File Offset: 0x001083C2
		public SyntaxToken OmittedTypeArgumentToken
		{
			get
			{
				return new SyntaxToken(this, ((OmittedTypeArgumentSyntax)base.Green).omittedTypeArgumentToken, base.Position, 0);
			}
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x0010A1E1 File Offset: 0x001083E1
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x0010A1E4 File Offset: 0x001083E4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x0010A1E7 File Offset: 0x001083E7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOmittedTypeArgument(this);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x0010A1F0 File Offset: 0x001083F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOmittedTypeArgument(this);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x0010A1FC File Offset: 0x001083FC
		public OmittedTypeArgumentSyntax Update(SyntaxToken omittedTypeArgumentToken)
		{
			if (!(omittedTypeArgumentToken != this.OmittedTypeArgumentToken))
			{
				return this;
			}
			OmittedTypeArgumentSyntax omittedTypeArgumentSyntax = SyntaxFactory.OmittedTypeArgument(omittedTypeArgumentToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return omittedTypeArgumentSyntax;
			}
			return omittedTypeArgumentSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x0010A237 File Offset: 0x00108437
		public OmittedTypeArgumentSyntax WithOmittedTypeArgumentToken(SyntaxToken omittedTypeArgumentToken)
		{
			return this.Update(omittedTypeArgumentToken);
		}
	}
}
