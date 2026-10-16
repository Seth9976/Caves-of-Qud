using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000332 RID: 818
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CrefParameterListSyntax : BaseCrefParameterListSyntax
	{
		// Token: 0x060045D5 RID: 17877 RVA: 0x0011757A File Offset: 0x0011577A
		internal CrefParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x00117585 File Offset: 0x00115785
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CrefParameterListSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x001175A4 File Offset: 0x001157A4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<CrefParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.parameters, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<CrefParameterSyntax>);
				}
				return new SeparatedSyntaxList<CrefParameterSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x001175D9 File Offset: 0x001157D9
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CrefParameterListSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x001175FF File Offset: 0x001157FF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.parameters, 1);
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x00117614 File Offset: 0x00115814
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x00117622 File Offset: 0x00115822
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCrefParameterList(this);
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x0011762B File Offset: 0x0011582B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCrefParameterList(this);
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x00117634 File Offset: 0x00115834
		public CrefParameterListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(parameters != this.Parameters) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			CrefParameterListSyntax crefParameterListSyntax = SyntaxFactory.CrefParameterList(openParenToken, parameters, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return crefParameterListSyntax;
			}
			return crefParameterListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x0011768D File Offset: 0x0011588D
		public CrefParameterListSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Parameters, this.CloseParenToken);
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x001176A2 File Offset: 0x001158A2
		internal override BaseCrefParameterListSyntax WithParametersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters)
		{
			return this.WithParameters(parameters);
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x001176AB File Offset: 0x001158AB
		public new CrefParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters)
		{
			return this.Update(this.OpenParenToken, parameters, this.CloseParenToken);
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x001176C0 File Offset: 0x001158C0
		public CrefParameterListSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Parameters, closeParenToken);
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x001176D5 File Offset: 0x001158D5
		internal override BaseCrefParameterListSyntax AddParametersCore(params CrefParameterSyntax[] items)
		{
			return this.AddParameters(items);
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x001176E0 File Offset: 0x001158E0
		public new CrefParameterListSyntax AddParameters(params CrefParameterSyntax[] items)
		{
			return this.WithParameters(this.Parameters.AddRange(items));
		}

		// Token: 0x04001519 RID: 5401
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode parameters;
	}
}
