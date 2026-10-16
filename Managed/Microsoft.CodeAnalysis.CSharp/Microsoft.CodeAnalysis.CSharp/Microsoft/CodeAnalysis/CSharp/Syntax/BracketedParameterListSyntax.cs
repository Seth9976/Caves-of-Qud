using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000324 RID: 804
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class BracketedParameterListSyntax : BaseParameterListSyntax
	{
		// Token: 0x0600453A RID: 17722 RVA: 0x001166FB File Offset: 0x001148FB
		internal BracketedParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x00116706 File Offset: 0x00114906
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((BracketedParameterListSyntax)base.Green).openBracketToken, base.Position, 0);
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x00116728 File Offset: 0x00114928
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<ParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.parameters, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<ParameterSyntax>);
				}
				return new SeparatedSyntaxList<ParameterSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x0011675D File Offset: 0x0011495D
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((BracketedParameterListSyntax)base.Green).closeBracketToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x00116783 File Offset: 0x00114983
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.parameters, 1);
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x00116798 File Offset: 0x00114998
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x001167A6 File Offset: 0x001149A6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBracketedParameterList(this);
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x001167AF File Offset: 0x001149AF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBracketedParameterList(this);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x001167B8 File Offset: 0x001149B8
		public BracketedParameterListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			if (!(openBracketToken != this.OpenBracketToken) && !(parameters != this.Parameters) && !(closeBracketToken != this.CloseBracketToken))
			{
				return this;
			}
			BracketedParameterListSyntax bracketedParameterListSyntax = SyntaxFactory.BracketedParameterList(openBracketToken, parameters, closeBracketToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return bracketedParameterListSyntax;
			}
			return bracketedParameterListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x00116811 File Offset: 0x00114A11
		public BracketedParameterListSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(openBracketToken, this.Parameters, this.CloseBracketToken);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x00116826 File Offset: 0x00114A26
		internal override BaseParameterListSyntax WithParametersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters)
		{
			return this.WithParameters(parameters);
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x0011682F File Offset: 0x00114A2F
		public new BracketedParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters)
		{
			return this.Update(this.OpenBracketToken, parameters, this.CloseBracketToken);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x00116844 File Offset: 0x00114A44
		public BracketedParameterListSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.OpenBracketToken, this.Parameters, closeBracketToken);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x00116859 File Offset: 0x00114A59
		internal override BaseParameterListSyntax AddParametersCore(params ParameterSyntax[] items)
		{
			return this.AddParameters(items);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x00116864 File Offset: 0x00114A64
		public new BracketedParameterListSyntax AddParameters(params ParameterSyntax[] items)
		{
			return this.WithParameters(this.Parameters.AddRange(items));
		}

		// Token: 0x0400150A RID: 5386
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode parameters;
	}
}
