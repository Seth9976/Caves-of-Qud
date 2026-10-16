using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000330 RID: 816
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ConversionOperatorMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x060045C0 RID: 17856 RVA: 0x00117365 File Offset: 0x00115565
		internal ConversionOperatorMemberCrefSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x00117370 File Offset: 0x00115570
		public SyntaxToken ImplicitOrExplicitKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ConversionOperatorMemberCrefSyntax)base.Green).implicitOrExplicitKeyword, base.Position, 0);
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x0011738F File Offset: 0x0011558F
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return new SyntaxToken(this, ((ConversionOperatorMemberCrefSyntax)base.Green).operatorKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x001173B5 File Offset: 0x001155B5
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x001173C4 File Offset: 0x001155C4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<CrefParameterListSyntax>(ref this.parameters, 3);
			}
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x001173D4 File Offset: 0x001155D4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 2)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<CrefParameterListSyntax>(ref this.parameters, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRed<TypeSyntax>(ref this.type, 2);
			}
			return syntaxNode;
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x00117410 File Offset: 0x00115610
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 2)
			{
				if (index != 3)
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
				syntaxNode = this.type;
			}
			return syntaxNode;
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x0011743C File Offset: 0x0011563C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConversionOperatorMemberCref(this);
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x00117445 File Offset: 0x00115645
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConversionOperatorMemberCref(this);
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x00117450 File Offset: 0x00115650
		public ConversionOperatorMemberCrefSyntax Update(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			if (!(implicitOrExplicitKeyword != this.ImplicitOrExplicitKeyword) && !(operatorKeyword != this.OperatorKeyword) && type == this.Type && parameters == this.Parameters)
			{
				return this;
			}
			ConversionOperatorMemberCrefSyntax conversionOperatorMemberCrefSyntax = SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, operatorKeyword, type, parameters);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return conversionOperatorMemberCrefSyntax;
			}
			return conversionOperatorMemberCrefSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x001174B0 File Offset: 0x001156B0
		public ConversionOperatorMemberCrefSyntax WithImplicitOrExplicitKeyword(SyntaxToken implicitOrExplicitKeyword)
		{
			return this.Update(implicitOrExplicitKeyword, this.OperatorKeyword, this.Type, this.Parameters);
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x001174CB File Offset: 0x001156CB
		public ConversionOperatorMemberCrefSyntax WithOperatorKeyword(SyntaxToken operatorKeyword)
		{
			return this.Update(this.ImplicitOrExplicitKeyword, operatorKeyword, this.Type, this.Parameters);
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x001174E6 File Offset: 0x001156E6
		public ConversionOperatorMemberCrefSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.ImplicitOrExplicitKeyword, this.OperatorKeyword, type, this.Parameters);
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x00117501 File Offset: 0x00115701
		public ConversionOperatorMemberCrefSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			return this.Update(this.ImplicitOrExplicitKeyword, this.OperatorKeyword, this.Type, parameters);
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x0011751C File Offset: 0x0011571C
		public ConversionOperatorMemberCrefSyntax AddParametersParameters(params CrefParameterSyntax[] items)
		{
			CrefParameterListSyntax crefParameterListSyntax;
			if ((crefParameterListSyntax = this.Parameters) == null)
			{
				crefParameterListSyntax = SyntaxFactory.CrefParameterList(default(SeparatedSyntaxList<CrefParameterSyntax>));
			}
			CrefParameterListSyntax crefParameterListSyntax2 = crefParameterListSyntax;
			return this.WithParameters(crefParameterListSyntax2.WithParameters(crefParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x04001517 RID: 5399
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x04001518 RID: 5400
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CrefParameterListSyntax parameters;
	}
}
