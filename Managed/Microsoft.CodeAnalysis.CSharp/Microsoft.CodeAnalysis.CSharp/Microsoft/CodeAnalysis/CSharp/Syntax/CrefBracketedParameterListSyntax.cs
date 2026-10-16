using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000333 RID: 819
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CrefBracketedParameterListSyntax : BaseCrefParameterListSyntax
	{
		// Token: 0x060045E4 RID: 17892 RVA: 0x00117702 File Offset: 0x00115902
		internal CrefBracketedParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x0011770D File Offset: 0x0011590D
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((CrefBracketedParameterListSyntax)base.Green).openBracketToken, base.Position, 0);
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x0011772C File Offset: 0x0011592C
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

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x00117761 File Offset: 0x00115961
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((CrefBracketedParameterListSyntax)base.Green).closeBracketToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x00117787 File Offset: 0x00115987
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.parameters, 1);
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x0011779C File Offset: 0x0011599C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x001177AA File Offset: 0x001159AA
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCrefBracketedParameterList(this);
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x001177B3 File Offset: 0x001159B3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCrefBracketedParameterList(this);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x001177BC File Offset: 0x001159BC
		public CrefBracketedParameterListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			if (!(openBracketToken != this.OpenBracketToken) && !(parameters != this.Parameters) && !(closeBracketToken != this.CloseBracketToken))
			{
				return this;
			}
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax = SyntaxFactory.CrefBracketedParameterList(openBracketToken, parameters, closeBracketToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return crefBracketedParameterListSyntax;
			}
			return crefBracketedParameterListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00117815 File Offset: 0x00115A15
		public CrefBracketedParameterListSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(openBracketToken, this.Parameters, this.CloseBracketToken);
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x0011782A File Offset: 0x00115A2A
		internal override BaseCrefParameterListSyntax WithParametersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters)
		{
			return this.WithParameters(parameters);
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x00117833 File Offset: 0x00115A33
		public new CrefBracketedParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters)
		{
			return this.Update(this.OpenBracketToken, parameters, this.CloseBracketToken);
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x00117848 File Offset: 0x00115A48
		public CrefBracketedParameterListSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.OpenBracketToken, this.Parameters, closeBracketToken);
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x0011785D File Offset: 0x00115A5D
		internal override BaseCrefParameterListSyntax AddParametersCore(params CrefParameterSyntax[] items)
		{
			return this.AddParameters(items);
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x00117868 File Offset: 0x00115A68
		public new CrefBracketedParameterListSyntax AddParameters(params CrefParameterSyntax[] items)
		{
			return this.WithParameters(this.Parameters.AddRange(items));
		}

		// Token: 0x0400151A RID: 5402
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode parameters;
	}
}
