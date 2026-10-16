using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200032E RID: 814
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class IndexerMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x060045A8 RID: 17832 RVA: 0x001170E5 File Offset: 0x001152E5
		internal IndexerMemberCrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x060045A9 RID: 17833 RVA: 0x001170F0 File Offset: 0x001152F0
		public SyntaxToken ThisKeyword
		{
			get
			{
				return new SyntaxToken(this, ((IndexerMemberCrefSyntax)base.Green).thisKeyword, base.Position, 0);
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x0011710F File Offset: 0x0011530F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefBracketedParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<CrefBracketedParameterListSyntax>(ref this.parameters, 1);
			}
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x0011711E File Offset: 0x0011531E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<CrefBracketedParameterListSyntax>(ref this.parameters, 1);
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x00117133 File Offset: 0x00115333
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00117141 File Offset: 0x00115341
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIndexerMemberCref(this);
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x0011714A File Offset: 0x0011534A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIndexerMemberCref(this);
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x00117154 File Offset: 0x00115354
		public IndexerMemberCrefSyntax Update(SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CrefBracketedParameterListSyntax parameters)
		{
			if (!(thisKeyword != this.ThisKeyword) && parameters == this.Parameters)
			{
				return this;
			}
			IndexerMemberCrefSyntax indexerMemberCrefSyntax = SyntaxFactory.IndexerMemberCref(thisKeyword, parameters);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return indexerMemberCrefSyntax;
			}
			return indexerMemberCrefSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x00117199 File Offset: 0x00115399
		public IndexerMemberCrefSyntax WithThisKeyword(SyntaxToken thisKeyword)
		{
			return this.Update(thisKeyword, this.Parameters);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x001171A8 File Offset: 0x001153A8
		public IndexerMemberCrefSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(2)] CrefBracketedParameterListSyntax parameters)
		{
			return this.Update(this.ThisKeyword, parameters);
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x001171B8 File Offset: 0x001153B8
		public IndexerMemberCrefSyntax AddParametersParameters(params CrefParameterSyntax[] items)
		{
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax;
			if ((crefBracketedParameterListSyntax = this.Parameters) == null)
			{
				crefBracketedParameterListSyntax = SyntaxFactory.CrefBracketedParameterList(default(SeparatedSyntaxList<CrefParameterSyntax>));
			}
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax2 = crefBracketedParameterListSyntax;
			return this.WithParameters(crefBracketedParameterListSyntax2.WithParameters(crefBracketedParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x04001515 RID: 5397
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CrefBracketedParameterListSyntax parameters;
	}
}
