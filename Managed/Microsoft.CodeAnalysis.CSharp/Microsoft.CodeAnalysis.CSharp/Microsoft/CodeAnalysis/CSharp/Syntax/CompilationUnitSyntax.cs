using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200024E RID: 590
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CompilationUnitSyntax : CSharpSyntaxNode, ICompilationUnitSyntax
	{
		// Token: 0x06003858 RID: 14424 RVA: 0x000FA8A2 File Offset: 0x000F8AA2
		public IList<ReferenceDirectiveTriviaSyntax> GetReferenceDirectives()
		{
			return this.GetReferenceDirectives(null);
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000FA8AC File Offset: 0x000F8AAC
		internal IList<ReferenceDirectiveTriviaSyntax> GetReferenceDirectives([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<ReferenceDirectiveTriviaSyntax, bool> filter)
		{
			return base.GetFirstToken(true, false, false, false).GetDirectives<ReferenceDirectiveTriviaSyntax>(filter);
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000FA8D4 File Offset: 0x000F8AD4
		public IList<LoadDirectiveTriviaSyntax> GetLoadDirectives()
		{
			return base.GetFirstToken(true, false, false, false).GetDirectives<LoadDirectiveTriviaSyntax>(null);
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000FA8FC File Offset: 0x000F8AFC
		internal DirectiveStack GetConditionalDirectivesStack()
		{
			IEnumerable<DirectiveTriviaSyntax> directives = base.GetDirectives(new Func<DirectiveTriviaSyntax, bool>(CompilationUnitSyntax.IsActiveConditionalDirective));
			DirectiveStack directiveStack = DirectiveStack.Empty;
			foreach (DirectiveTriviaSyntax directiveTriviaSyntax in directives)
			{
				directiveStack = ((DirectiveTriviaSyntax)directiveTriviaSyntax.Green).ApplyDirectives(directiveStack);
			}
			return directiveStack;
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000FA968 File Offset: 0x000F8B68
		private static bool IsActiveConditionalDirective(DirectiveTriviaSyntax directive)
		{
			SyntaxKind syntaxKind = directive.Kind();
			if (syntaxKind != SyntaxKind.DefineDirectiveTrivia)
			{
				return syntaxKind == SyntaxKind.UndefDirectiveTrivia && ((UndefDirectiveTriviaSyntax)directive).IsActive;
			}
			return ((DefineDirectiveTriviaSyntax)directive).IsActive;
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000FA9A7 File Offset: 0x000F8BA7
		internal CompilationUnitSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000FA9B2 File Offset: 0x000F8BB2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<ExternAliasDirectiveSyntax> Externs
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<ExternAliasDirectiveSyntax>(base.GetRed(ref this.externs, 0));
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000FA9C6 File Offset: 0x000F8BC6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<UsingDirectiveSyntax> Usings
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<UsingDirectiveSyntax>(base.GetRed(ref this.usings, 1));
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000FA9DA File Offset: 0x000F8BDA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 2));
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000FA9EE File Offset: 0x000F8BEE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<MemberDeclarationSyntax>(base.GetRed(ref this.members, 3));
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000FAA02 File Offset: 0x000F8C02
		public SyntaxToken EndOfFileToken
		{
			get
			{
				return new SyntaxToken(this, ((CompilationUnitSyntax)base.Green).endOfFileToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000FAA28 File Offset: 0x000F8C28
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = base.GetRedAtZero(ref this.externs);
				break;
			case 1:
				syntaxNode = base.GetRed(ref this.usings, 1);
				break;
			case 2:
				syntaxNode = base.GetRed(ref this.attributeLists, 2);
				break;
			case 3:
				syntaxNode = base.GetRed(ref this.members, 3);
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000FAA90 File Offset: 0x000F8C90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			switch (index)
			{
			case 0:
				syntaxNode = this.externs;
				break;
			case 1:
				syntaxNode = this.usings;
				break;
			case 2:
				syntaxNode = this.attributeLists;
				break;
			case 3:
				syntaxNode = this.members;
				break;
			default:
				syntaxNode = null;
				break;
			}
			return syntaxNode;
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000FAADC File Offset: 0x000F8CDC
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCompilationUnit(this);
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000FAAE5 File Offset: 0x000F8CE5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCompilationUnit(this);
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000FAAF0 File Offset: 0x000F8CF0
		public CompilationUnitSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
		{
			if (!(externs != this.Externs) && !(usings != this.Usings) && !(attributeLists != this.AttributeLists) && !(members != this.Members) && !(endOfFileToken != this.EndOfFileToken))
			{
				return this;
			}
			CompilationUnitSyntax compilationUnitSyntax = SyntaxFactory.CompilationUnit(externs, usings, attributeLists, members, endOfFileToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return compilationUnitSyntax;
			}
			return compilationUnitSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000FAB6B File Offset: 0x000F8D6B
		public CompilationUnitSyntax WithExterns([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs)
		{
			return this.Update(externs, this.Usings, this.AttributeLists, this.Members, this.EndOfFileToken);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000FAB8C File Offset: 0x000F8D8C
		public CompilationUnitSyntax WithUsings([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings)
		{
			return this.Update(this.Externs, usings, this.AttributeLists, this.Members, this.EndOfFileToken);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000FABAD File Offset: 0x000F8DAD
		public CompilationUnitSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(this.Externs, this.Usings, attributeLists, this.Members, this.EndOfFileToken);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000FABCE File Offset: 0x000F8DCE
		public CompilationUnitSyntax WithMembers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members)
		{
			return this.Update(this.Externs, this.Usings, this.AttributeLists, members, this.EndOfFileToken);
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000FABEF File Offset: 0x000F8DEF
		public CompilationUnitSyntax WithEndOfFileToken(SyntaxToken endOfFileToken)
		{
			return this.Update(this.Externs, this.Usings, this.AttributeLists, this.Members, endOfFileToken);
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000FAC10 File Offset: 0x000F8E10
		public CompilationUnitSyntax AddExterns(params ExternAliasDirectiveSyntax[] items)
		{
			return this.WithExterns(this.Externs.AddRange(items));
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000FAC34 File Offset: 0x000F8E34
		public CompilationUnitSyntax AddUsings(params UsingDirectiveSyntax[] items)
		{
			return this.WithUsings(this.Usings.AddRange(items));
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000FAC58 File Offset: 0x000F8E58
		public CompilationUnitSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000FAC7C File Offset: 0x000F8E7C
		public CompilationUnitSyntax AddMembers(params MemberDeclarationSyntax[] items)
		{
			return this.WithMembers(this.Members.AddRange(items));
		}

		// Token: 0x0400139E RID: 5022
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode externs;

		// Token: 0x0400139F RID: 5023
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode usings;

		// Token: 0x040013A0 RID: 5024
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013A1 RID: 5025
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode members;
	}
}
