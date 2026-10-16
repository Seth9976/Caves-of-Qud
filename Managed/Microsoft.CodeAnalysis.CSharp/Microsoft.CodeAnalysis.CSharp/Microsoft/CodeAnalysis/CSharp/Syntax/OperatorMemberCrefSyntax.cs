using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200032F RID: 815
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class OperatorMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x060045B3 RID: 17843 RVA: 0x001171F9 File Offset: 0x001153F9
		internal OperatorMemberCrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x060045B4 RID: 17844 RVA: 0x00117204 File Offset: 0x00115404
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return new SyntaxToken(this, ((OperatorMemberCrefSyntax)base.Green).operatorKeyword, base.Position, 0);
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x00117223 File Offset: 0x00115423
		public SyntaxToken OperatorToken
		{
			get
			{
				return new SyntaxToken(this, ((OperatorMemberCrefSyntax)base.Green).operatorToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x00117249 File Offset: 0x00115449
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<CrefParameterListSyntax>(ref this.parameters, 2);
			}
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x00117258 File Offset: 0x00115458
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return base.GetRed<CrefParameterListSyntax>(ref this.parameters, 2);
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x0011726D File Offset: 0x0011546D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 2)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x0011727B File Offset: 0x0011547B
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOperatorMemberCref(this);
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00117284 File Offset: 0x00115484
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOperatorMemberCref(this);
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00117290 File Offset: 0x00115490
		public OperatorMemberCrefSyntax Update(SyntaxToken operatorKeyword, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			if (!(operatorKeyword != this.OperatorKeyword) && !(operatorToken != this.OperatorToken) && parameters == this.Parameters)
			{
				return this;
			}
			OperatorMemberCrefSyntax operatorMemberCrefSyntax = SyntaxFactory.OperatorMemberCref(operatorKeyword, operatorToken, parameters);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return operatorMemberCrefSyntax;
			}
			return operatorMemberCrefSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x001172E4 File Offset: 0x001154E4
		public OperatorMemberCrefSyntax WithOperatorKeyword(SyntaxToken operatorKeyword)
		{
			return this.Update(operatorKeyword, this.OperatorToken, this.Parameters);
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x001172F9 File Offset: 0x001154F9
		public OperatorMemberCrefSyntax WithOperatorToken(SyntaxToken operatorToken)
		{
			return this.Update(this.OperatorKeyword, operatorToken, this.Parameters);
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x0011730E File Offset: 0x0011550E
		public OperatorMemberCrefSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			return this.Update(this.OperatorKeyword, this.OperatorToken, parameters);
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x00117324 File Offset: 0x00115524
		public OperatorMemberCrefSyntax AddParametersParameters(params CrefParameterSyntax[] items)
		{
			CrefParameterListSyntax crefParameterListSyntax;
			if ((crefParameterListSyntax = this.Parameters) == null)
			{
				crefParameterListSyntax = SyntaxFactory.CrefParameterList(default(SeparatedSyntaxList<CrefParameterSyntax>));
			}
			CrefParameterListSyntax crefParameterListSyntax2 = crefParameterListSyntax;
			return this.WithParameters(crefParameterListSyntax2.WithParameters(crefParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x04001516 RID: 5398
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CrefParameterListSyntax parameters;
	}
}
