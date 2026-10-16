using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000272 RID: 626
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ParameterListSyntax : BaseParameterListSyntax
	{
		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x001031B4 File Offset: 0x001013B4
		internal int ParameterCount
		{
			get
			{
				int num = 0;
				SeparatedSyntaxList<ParameterSyntax>.Enumerator enumerator = this.Parameters.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsArgList)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x001031F0 File Offset: 0x001013F0
		internal ParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x001031FB File Offset: 0x001013FB
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParameterListSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x0010321C File Offset: 0x0010141C
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

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x00103251 File Offset: 0x00101451
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((ParameterListSyntax)base.Green).closeParenToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x00103277 File Offset: 0x00101477
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.parameters, 1);
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x0010328C File Offset: 0x0010148C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x0010329A File Offset: 0x0010149A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParameterList(this);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x001032A3 File Offset: 0x001014A3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParameterList(this);
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x001032AC File Offset: 0x001014AC
		public ParameterListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && !(parameters != this.Parameters) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			ParameterListSyntax parameterListSyntax = SyntaxFactory.ParameterList(openParenToken, parameters, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return parameterListSyntax;
			}
			return parameterListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x00103305 File Offset: 0x00101505
		public ParameterListSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Parameters, this.CloseParenToken);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x0010331A File Offset: 0x0010151A
		internal override BaseParameterListSyntax WithParametersCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters)
		{
			return this.WithParameters(parameters);
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x00103323 File Offset: 0x00101523
		public new ParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters)
		{
			return this.Update(this.OpenParenToken, parameters, this.CloseParenToken);
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x00103338 File Offset: 0x00101538
		public ParameterListSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Parameters, closeParenToken);
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x0010334D File Offset: 0x0010154D
		internal override BaseParameterListSyntax AddParametersCore(params ParameterSyntax[] items)
		{
			return this.AddParameters(items);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x00103358 File Offset: 0x00101558
		public new ParameterListSyntax AddParameters(params ParameterSyntax[] items)
		{
			return this.WithParameters(this.Parameters.AddRange(items));
		}

		// Token: 0x04001402 RID: 5122
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode parameters;
	}
}
