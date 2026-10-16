using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200032D RID: 813
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class NameMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x0600459D RID: 17821 RVA: 0x00116FA1 File Offset: 0x001151A1
		internal NameMemberCrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x00116FAC File Offset: 0x001151AC
		public TypeSyntax Name
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.name);
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x00116FBA File Offset: 0x001151BA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<CrefParameterListSyntax>(ref this.parameters, 1);
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x00116FCC File Offset: 0x001151CC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<CrefParameterListSyntax>(ref this.parameters, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<TypeSyntax>(ref this.name);
			}
			return syntaxNode;
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x00117004 File Offset: 0x00115204
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.parameters;
				}
			}
			else
			{
				syntaxNode = this.name;
			}
			return syntaxNode;
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x0011702F File Offset: 0x0011522F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNameMemberCref(this);
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x00117038 File Offset: 0x00115238
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNameMemberCref(this);
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00117044 File Offset: 0x00115244
		public NameMemberCrefSyntax Update(TypeSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			if (name == this.Name && parameters == this.Parameters)
			{
				return this;
			}
			NameMemberCrefSyntax nameMemberCrefSyntax = SyntaxFactory.NameMemberCref(name, parameters);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return nameMemberCrefSyntax;
			}
			return nameMemberCrefSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x00117084 File Offset: 0x00115284
		public NameMemberCrefSyntax WithName(TypeSyntax name)
		{
			return this.Update(name, this.Parameters);
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00117093 File Offset: 0x00115293
		public NameMemberCrefSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			return this.Update(this.Name, parameters);
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x001170A4 File Offset: 0x001152A4
		public NameMemberCrefSyntax AddParametersParameters(params CrefParameterSyntax[] items)
		{
			CrefParameterListSyntax crefParameterListSyntax;
			if ((crefParameterListSyntax = this.Parameters) == null)
			{
				crefParameterListSyntax = SyntaxFactory.CrefParameterList(default(SeparatedSyntaxList<CrefParameterSyntax>));
			}
			CrefParameterListSyntax crefParameterListSyntax2 = crefParameterListSyntax;
			return this.WithParameters(crefParameterListSyntax2.WithParameters(crefParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x04001513 RID: 5395
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax name;

		// Token: 0x04001514 RID: 5396
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CrefParameterListSyntax parameters;
	}
}
