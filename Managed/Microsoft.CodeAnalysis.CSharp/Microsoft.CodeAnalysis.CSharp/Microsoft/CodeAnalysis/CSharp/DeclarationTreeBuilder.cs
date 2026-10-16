using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000126 RID: 294
	internal sealed class DeclarationTreeBuilder : CSharpSyntaxVisitor<SingleNamespaceOrTypeDeclaration>
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x000835E4 File Offset: 0x000817E4
		private DeclarationTreeBuilder(SyntaxTree syntaxTree, string scriptClassName, bool isSubmission)
		{
			this._syntaxTree = syntaxTree;
			this._scriptClassName = scriptClassName;
			this._isSubmission = isSubmission;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00083604 File Offset: 0x00081804
		public static RootSingleNamespaceDeclaration ForTree(SyntaxTree syntaxTree, string scriptClassName, bool isSubmission)
		{
			return (RootSingleNamespaceDeclaration)new DeclarationTreeBuilder(syntaxTree, scriptClassName, isSubmission).Visit(syntaxTree.GetRoot(default(CancellationToken)));
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00083634 File Offset: 0x00081834
		private ImmutableArray<SingleNamespaceOrTypeDeclaration> VisitNamespaceChildren(CSharpSyntaxNode node, SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members, SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax> internalMembers)
		{
			if (members.Count == 0)
			{
				return ImmutableArray<SingleNamespaceOrTypeDeclaration>.Empty;
			}
			bool flag = false;
			bool flag2 = node.Kind() == SyntaxKind.CompilationUnit && this._syntaxTree.Options.Kind == SourceCodeKind.Regular;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax globalStatementSyntax = null;
			ArrayBuilder<SingleNamespaceOrTypeDeclaration> instance = ArrayBuilder<SingleNamespaceOrTypeDeclaration>.GetInstance();
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax memberDeclarationSyntax in members)
			{
				SingleNamespaceOrTypeDeclaration singleNamespaceOrTypeDeclaration = this.Visit(memberDeclarationSyntax);
				if (singleNamespaceOrTypeDeclaration != null)
				{
					instance.Add(singleNamespaceOrTypeDeclaration);
				}
				else if (flag2 && memberDeclarationSyntax.IsKind(SyntaxKind.GlobalStatement))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax globalStatementSyntax2 = (Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax)memberDeclarationSyntax;
					if (globalStatementSyntax == null)
					{
						globalStatementSyntax = globalStatementSyntax2;
					}
					Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement = globalStatementSyntax2.Statement;
					if (!flag3)
					{
						flag3 = SyntaxFacts.HasAwaitOperations(statement);
					}
					if (!flag4)
					{
						flag4 = SyntaxFacts.HasYieldOperations(statement);
					}
					if (!flag5)
					{
						flag5 = SyntaxFacts.HasReturnWithExpression(statement);
					}
				}
				else if (!flag && memberDeclarationSyntax.Kind() != SyntaxKind.IncompleteMember)
				{
					flag = true;
				}
			}
			if (globalStatementSyntax != null)
			{
				instance.Add(DeclarationTreeBuilder.CreateSimpleProgram(globalStatementSyntax, flag3, flag4, flag5));
			}
			if (flag)
			{
				SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags = SingleTypeDeclaration.TypeDeclarationFlags.None;
				ImmutableHashSet<string> nonTypeMemberNames = DeclarationTreeBuilder.GetNonTypeMemberNames(internalMembers, ref typeDeclarationFlags, flag2);
				SyntaxReference reference = this._syntaxTree.GetReference(node);
				instance.Add(DeclarationTreeBuilder.CreateImplicitClass(nonTypeMemberNames, reference, typeDeclarationFlags));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00083774 File Offset: 0x00081974
		private static SingleNamespaceOrTypeDeclaration CreateImplicitClass(ImmutableHashSet<string> memberNames, SyntaxReference container, SingleTypeDeclaration.TypeDeclarationFlags declFlags)
		{
			return new SingleTypeDeclaration(DeclarationKind.ImplicitClass, "<invalid-global-code>", 0, DeclarationModifiers.Sealed | DeclarationModifiers.Internal | DeclarationModifiers.Partial, declFlags, container, new SourceLocation(container), memberNames, ImmutableArray<SingleTypeDeclaration>.Empty, ImmutableArray<Diagnostic>.Empty);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000837A8 File Offset: 0x000819A8
		private static SingleNamespaceOrTypeDeclaration CreateSimpleProgram(Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax firstGlobalStatement, bool hasAwaitExpressions, bool isIterator, bool hasReturnWithExpression)
		{
			DeclarationKind declarationKind = DeclarationKind.SimpleProgram;
			string text = "<Program>$";
			int num = 0;
			DeclarationModifiers declarationModifiers = DeclarationModifiers.Static | DeclarationModifiers.Internal | DeclarationModifiers.Partial;
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags = (hasAwaitExpressions ? SingleTypeDeclaration.TypeDeclarationFlags.HasAwaitExpressions : SingleTypeDeclaration.TypeDeclarationFlags.None) | (isIterator ? SingleTypeDeclaration.TypeDeclarationFlags.IsIterator : SingleTypeDeclaration.TypeDeclarationFlags.None) | (hasReturnWithExpression ? SingleTypeDeclaration.TypeDeclarationFlags.HasReturnWithExpression : SingleTypeDeclaration.TypeDeclarationFlags.None);
			SyntaxReference reference = firstGlobalStatement.SyntaxTree.GetReference(firstGlobalStatement.Parent);
			SyntaxToken firstToken = firstGlobalStatement.GetFirstToken(false, false, false, false);
			return new SingleTypeDeclaration(declarationKind, text, num, declarationModifiers, typeDeclarationFlags, reference, new SourceLocation(in firstToken), ImmutableHashSet<string>.Empty, ImmutableArray<SingleTypeDeclaration>.Empty, ImmutableArray<Diagnostic>.Empty);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0008381C File Offset: 0x00081A1C
		private RootSingleNamespaceDeclaration CreateScriptRootDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnit)
		{
			SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax> members = compilationUnit.Members;
			ArrayBuilder<SingleNamespaceOrTypeDeclaration> instance = ArrayBuilder<SingleNamespaceOrTypeDeclaration>.GetInstance();
			ArrayBuilder<SingleTypeDeclaration> instance2 = ArrayBuilder<SingleTypeDeclaration>.GetInstance();
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax memberDeclarationSyntax in members)
			{
				SingleNamespaceOrTypeDeclaration singleNamespaceOrTypeDeclaration = this.Visit(memberDeclarationSyntax);
				if (singleNamespaceOrTypeDeclaration != null)
				{
					if (singleNamespaceOrTypeDeclaration.Kind == DeclarationKind.Namespace)
					{
						instance.Add(singleNamespaceOrTypeDeclaration);
					}
					else
					{
						instance2.Add((SingleTypeDeclaration)singleNamespaceOrTypeDeclaration);
					}
				}
			}
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags = SingleTypeDeclaration.TypeDeclarationFlags.None;
			ImmutableHashSet<string> nonTypeMemberNames = DeclarationTreeBuilder.GetNonTypeMemberNames(((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CompilationUnitSyntax)compilationUnit.Green).Members, ref typeDeclarationFlags, false);
			instance.Add(this.CreateScriptClass(compilationUnit, instance2.ToImmutableAndFree(), nonTypeMemberNames, typeDeclarationFlags));
			return this.CreateRootSingleNamespaceDeclaration(compilationUnit, instance.ToImmutableAndFree(), true);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000838C8 File Offset: 0x00081AC8
		private static ImmutableArray<ReferenceDirective> GetReferenceDirectives(Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnit)
		{
			IList<Microsoft.CodeAnalysis.CSharp.Syntax.ReferenceDirectiveTriviaSyntax> referenceDirectives = compilationUnit.GetReferenceDirectives((Microsoft.CodeAnalysis.CSharp.Syntax.ReferenceDirectiveTriviaSyntax d) => !d.File.ContainsDiagnostics && !string.IsNullOrEmpty(d.File.ValueText));
			if (referenceDirectives.Count == 0)
			{
				return ImmutableArray<ReferenceDirective>.Empty;
			}
			ArrayBuilder<ReferenceDirective> instance = ArrayBuilder<ReferenceDirective>.GetInstance(referenceDirectives.Count);
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.ReferenceDirectiveTriviaSyntax referenceDirectiveTriviaSyntax in referenceDirectives)
			{
				instance.Add(new ReferenceDirective(referenceDirectiveTriviaSyntax.File.ValueText, new SourceLocation(referenceDirectiveTriviaSyntax)));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00083970 File Offset: 0x00081B70
		private SingleNamespaceOrTypeDeclaration CreateScriptClass(Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax parent, ImmutableArray<SingleTypeDeclaration> children, ImmutableHashSet<string> memberNames, SingleTypeDeclaration.TypeDeclarationFlags declFlags)
		{
			SyntaxReference reference = this._syntaxTree.GetReference(parent);
			string[] array = this._scriptClassName.Split(new char[] { '.' });
			SingleNamespaceOrTypeDeclaration singleNamespaceOrTypeDeclaration = new SingleTypeDeclaration(this._isSubmission ? DeclarationKind.Submission : DeclarationKind.Script, Enumerable.Last<string>(array), 0, DeclarationModifiers.Sealed | DeclarationModifiers.Internal | DeclarationModifiers.Partial, declFlags, reference, new SourceLocation(reference), memberNames, children, ImmutableArray<Diagnostic>.Empty);
			for (int i = array.Length - 2; i >= 0; i--)
			{
				singleNamespaceOrTypeDeclaration = SingleNamespaceDeclaration.Create(array[i], false, false, reference, new SourceLocation(reference), ImmutableArray.Create<SingleNamespaceOrTypeDeclaration>(singleNamespaceOrTypeDeclaration), ImmutableArray<Diagnostic>.Empty);
			}
			return singleNamespaceOrTypeDeclaration;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00083A00 File Offset: 0x00081C00
		public override SingleNamespaceOrTypeDeclaration VisitCompilationUnit(Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnit)
		{
			if (this._syntaxTree.Options.Kind != SourceCodeKind.Regular)
			{
				return this.CreateScriptRootDeclaration(compilationUnit);
			}
			ImmutableArray<SingleNamespaceOrTypeDeclaration> immutableArray = this.VisitNamespaceChildren(compilationUnit, compilationUnit.Members, ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CompilationUnitSyntax)compilationUnit.Green).Members);
			return this.CreateRootSingleNamespaceDeclaration(compilationUnit, immutableArray, false);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00083A50 File Offset: 0x00081C50
		private RootSingleNamespaceDeclaration CreateRootSingleNamespaceDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax compilationUnit, ImmutableArray<SingleNamespaceOrTypeDeclaration> children, bool isForScript)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax usingDirectiveSyntax in compilationUnit.Usings)
			{
				if (usingDirectiveSyntax.GlobalKeyword.IsKind(SyntaxKind.GlobalKeyword))
				{
					flag2 = true;
					if (flag && !flag3)
					{
						flag3 = true;
						instance.Add(ErrorCode.ERR_GlobalUsingOutOfOrder, usingDirectiveSyntax.GlobalKeyword.GetLocation());
					}
				}
				else
				{
					flag = true;
				}
			}
			return new RootSingleNamespaceDeclaration(flag2, flag, compilationUnit.Externs.Any(), this._syntaxTree.GetReference(compilationUnit), children, isForScript ? DeclarationTreeBuilder.GetReferenceDirectives(compilationUnit) : ImmutableArray<ReferenceDirective>.Empty, compilationUnit.AttributeLists.Any(), instance.ToReadOnlyAndFree());
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00083B14 File Offset: 0x00081D14
		public override SingleNamespaceOrTypeDeclaration VisitNamespaceDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax node)
		{
			ImmutableArray<SingleNamespaceOrTypeDeclaration> immutableArray = this.VisitNamespaceChildren(node, node.Members, node.Green.Members);
			bool flag = node.Usings.Any();
			bool flag2 = node.Externs.Any();
			Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax nameSyntax = node.Name;
			CSharpSyntaxNode csharpSyntaxNode = node;
			Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax qualifiedNameSyntax;
			while ((qualifiedNameSyntax = nameSyntax as Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax) != null)
			{
				SingleNamespaceDeclaration singleNamespaceDeclaration = SingleNamespaceDeclaration.Create(qualifiedNameSyntax.Right.Identifier.ValueText, flag, flag2, this._syntaxTree.GetReference(csharpSyntaxNode), new SourceLocation(qualifiedNameSyntax.Right), immutableArray, ImmutableArray<Diagnostic>.Empty);
				SingleNamespaceOrTypeDeclaration[] array = new SingleNamespaceDeclaration[] { singleNamespaceDeclaration };
				immutableArray = array.AsImmutableOrNull<SingleNamespaceOrTypeDeclaration>();
				nameSyntax = (csharpSyntaxNode = qualifiedNameSyntax.Left);
				flag = false;
				flag2 = false;
			}
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			if (DeclarationTreeBuilder.ContainsGeneric(node.Name))
			{
				instance.Add(ErrorCode.ERR_UnexpectedGenericName, node.Name.GetLocation());
			}
			if (DeclarationTreeBuilder.ContainsAlias(node.Name))
			{
				instance.Add(ErrorCode.ERR_UnexpectedAliasedName, node.Name.GetLocation());
			}
			if (node.AttributeLists.Count > 0)
			{
				instance.Add(ErrorCode.ERR_BadModifiersOnNamespace, node.AttributeLists[0].GetLocation());
			}
			if (node.Modifiers.Count > 0)
			{
				instance.Add(ErrorCode.ERR_BadModifiersOnNamespace, node.Modifiers[0].GetLocation());
			}
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax usingDirectiveSyntax in node.Usings)
			{
				if (usingDirectiveSyntax.GlobalKeyword.IsKind(SyntaxKind.GlobalKeyword))
				{
					instance.Add(ErrorCode.ERR_GlobalUsingInNamespace, usingDirectiveSyntax.GlobalKeyword.GetLocation());
					break;
				}
			}
			return SingleNamespaceDeclaration.Create(nameSyntax.GetUnqualifiedName().Identifier.ValueText, flag, flag2, this._syntaxTree.GetReference(csharpSyntaxNode), new SourceLocation(nameSyntax), immutableArray, instance.ToReadOnlyAndFree());
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00083D1C File Offset: 0x00081F1C
		private static bool ContainsAlias(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			switch (name.Kind())
			{
			case SyntaxKind.QualifiedName:
				return DeclarationTreeBuilder.ContainsAlias(((Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)name).Left);
			case SyntaxKind.GenericName:
				return false;
			case SyntaxKind.AliasQualifiedName:
				return true;
			}
			return false;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00083D64 File Offset: 0x00081F64
		private static bool ContainsGeneric(Microsoft.CodeAnalysis.CSharp.Syntax.NameSyntax name)
		{
			switch (name.Kind())
			{
			case SyntaxKind.QualifiedName:
			{
				Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax qualifiedNameSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)name;
				return DeclarationTreeBuilder.ContainsGeneric(qualifiedNameSyntax.Left) || DeclarationTreeBuilder.ContainsGeneric(qualifiedNameSyntax.Right);
			}
			case SyntaxKind.GenericName:
				return true;
			case SyntaxKind.AliasQualifiedName:
				return DeclarationTreeBuilder.ContainsGeneric(((Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax)name).Name);
			}
			return false;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00083DCC File Offset: 0x00081FCC
		public override SingleNamespaceOrTypeDeclaration VisitClassDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax node)
		{
			return this.VisitTypeDeclaration(node, DeclarationKind.Class);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00083DD6 File Offset: 0x00081FD6
		public override SingleNamespaceOrTypeDeclaration VisitStructDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.StructDeclarationSyntax node)
		{
			return this.VisitTypeDeclaration(node, DeclarationKind.Struct);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00083DE0 File Offset: 0x00081FE0
		public override SingleNamespaceOrTypeDeclaration VisitInterfaceDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.InterfaceDeclarationSyntax node)
		{
			return this.VisitTypeDeclaration(node, DeclarationKind.Interface);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x00083DEC File Offset: 0x00081FEC
		public override SingleNamespaceOrTypeDeclaration VisitRecordDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax node)
		{
			SyntaxKind syntaxKind = node.Kind();
			DeclarationKind declarationKind;
			if (syntaxKind != SyntaxKind.RecordDeclaration)
			{
				if (syntaxKind != SyntaxKind.RecordStructDeclaration)
				{
					throw ExceptionUtilities.UnexpectedValue(node.Kind());
				}
				declarationKind = DeclarationKind.RecordStruct;
			}
			else
			{
				declarationKind = DeclarationKind.Record;
			}
			DeclarationKind declarationKind2 = declarationKind;
			return this.VisitTypeDeclaration(node, declarationKind2);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00083E38 File Offset: 0x00082038
		private SingleNamespaceOrTypeDeclaration VisitTypeDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.TypeDeclarationSyntax node, DeclarationKind kind)
		{
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags = (node.AttributeLists.Any() ? SingleTypeDeclaration.TypeDeclarationFlags.HasAnyAttributes : SingleTypeDeclaration.TypeDeclarationFlags.None);
			if (node.BaseList != null)
			{
				typeDeclarationFlags |= SingleTypeDeclaration.TypeDeclarationFlags.HasBaseDeclarations;
			}
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			if (node.Arity == 0)
			{
				Symbol.ReportErrorIfHasConstraints(node.ConstraintClauses, instance);
			}
			ImmutableHashSet<string> nonTypeMemberNames = DeclarationTreeBuilder.GetNonTypeMemberNames(((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.TypeDeclarationSyntax)node.Green).Members, ref typeDeclarationFlags, false);
			if ((typeDeclarationFlags & SingleTypeDeclaration.TypeDeclarationFlags.HasAnyNontypeMembers) == SingleTypeDeclaration.TypeDeclarationFlags.None)
			{
				Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax recordDeclarationSyntax = node as Microsoft.CodeAnalysis.CSharp.Syntax.RecordDeclarationSyntax;
				if (recordDeclarationSyntax != null && recordDeclarationSyntax.ParameterList != null)
				{
					typeDeclarationFlags |= SingleTypeDeclaration.TypeDeclarationFlags.HasAnyNontypeMembers;
				}
			}
			DeclarationModifiers declarationModifiers = node.Modifiers.ToDeclarationModifiers(instance);
			SyntaxToken syntaxToken = node.Identifier;
			string valueText = syntaxToken.ValueText;
			DeclarationModifiers declarationModifiers2 = declarationModifiers;
			int arity = node.Arity;
			DeclarationModifiers declarationModifiers3 = declarationModifiers2;
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags2 = typeDeclarationFlags;
			SyntaxReference reference = this._syntaxTree.GetReference(node);
			syntaxToken = node.Identifier;
			return new SingleTypeDeclaration(kind, valueText, arity, declarationModifiers3, typeDeclarationFlags2, reference, new SourceLocation(in syntaxToken), nonTypeMemberNames, this.VisitTypeChildren(node), instance.ToReadOnlyAndFree());
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00083F14 File Offset: 0x00082114
		private ImmutableArray<SingleTypeDeclaration> VisitTypeChildren(Microsoft.CodeAnalysis.CSharp.Syntax.TypeDeclarationSyntax node)
		{
			if (node.Members.Count == 0)
			{
				return ImmutableArray<SingleTypeDeclaration>.Empty;
			}
			ArrayBuilder<SingleTypeDeclaration> instance = ArrayBuilder<SingleTypeDeclaration>.GetInstance();
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax memberDeclarationSyntax in node.Members)
			{
				SingleTypeDeclaration singleTypeDeclaration = this.Visit(memberDeclarationSyntax) as SingleTypeDeclaration;
				if (singleTypeDeclaration != null)
				{
					instance.Add(singleTypeDeclaration);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00083F80 File Offset: 0x00082180
		public override SingleNamespaceOrTypeDeclaration VisitDelegateDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax node)
		{
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags = (node.AttributeLists.Any() ? SingleTypeDeclaration.TypeDeclarationFlags.HasAnyAttributes : SingleTypeDeclaration.TypeDeclarationFlags.None);
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			if (node.Arity == 0)
			{
				Symbol.ReportErrorIfHasConstraints(node.ConstraintClauses, instance);
			}
			typeDeclarationFlags |= SingleTypeDeclaration.TypeDeclarationFlags.HasAnyNontypeMembers;
			DeclarationModifiers declarationModifiers = node.Modifiers.ToDeclarationModifiers(instance);
			DeclarationKind declarationKind = DeclarationKind.Delegate;
			SyntaxToken syntaxToken = node.Identifier;
			string valueText = syntaxToken.ValueText;
			DeclarationModifiers declarationModifiers2 = declarationModifiers;
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags2 = typeDeclarationFlags;
			int arity = node.Arity;
			DeclarationModifiers declarationModifiers3 = declarationModifiers2;
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags3 = typeDeclarationFlags2;
			SyntaxReference reference = this._syntaxTree.GetReference(node);
			syntaxToken = node.Identifier;
			return new SingleTypeDeclaration(declarationKind, valueText, arity, declarationModifiers3, typeDeclarationFlags3, reference, new SourceLocation(in syntaxToken), ImmutableHashSet<string>.Empty, ImmutableArray<SingleTypeDeclaration>.Empty, instance.ToReadOnlyAndFree());
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00084020 File Offset: 0x00082220
		public override SingleNamespaceOrTypeDeclaration VisitEnumDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.EnumDeclarationSyntax node)
		{
			SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax> members = node.Members;
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags = (node.AttributeLists.Any() ? SingleTypeDeclaration.TypeDeclarationFlags.HasAnyAttributes : SingleTypeDeclaration.TypeDeclarationFlags.None);
			if (node.BaseList != null)
			{
				typeDeclarationFlags |= SingleTypeDeclaration.TypeDeclarationFlags.HasBaseDeclarations;
			}
			ImmutableHashSet<string> enumMemberNames = DeclarationTreeBuilder.GetEnumMemberNames(members, ref typeDeclarationFlags);
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			DeclarationModifiers declarationModifiers = node.Modifiers.ToDeclarationModifiers(instance);
			DeclarationKind declarationKind = DeclarationKind.Enum;
			SyntaxToken syntaxToken = node.Identifier;
			string valueText = syntaxToken.ValueText;
			int num = 0;
			DeclarationModifiers declarationModifiers2 = declarationModifiers;
			SingleTypeDeclaration.TypeDeclarationFlags typeDeclarationFlags2 = typeDeclarationFlags;
			SyntaxReference reference = this._syntaxTree.GetReference(node);
			syntaxToken = node.Identifier;
			return new SingleTypeDeclaration(declarationKind, valueText, num, declarationModifiers2, typeDeclarationFlags2, reference, new SourceLocation(in syntaxToken), enumMemberNames, ImmutableArray<SingleTypeDeclaration>.Empty, instance.ToReadOnlyAndFree());
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x000840AF File Offset: 0x000822AF
		private static ImmutableHashSet<string> ToImmutableAndFree(ImmutableHashSet<string>.Builder builder)
		{
			ImmutableHashSet<string> immutableHashSet = builder.ToImmutable();
			builder.Clear();
			DeclarationTreeBuilder.s_memberNameBuilderPool.Free(builder);
			return immutableHashSet;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000840C8 File Offset: 0x000822C8
		private static ImmutableHashSet<string> GetEnumMemberNames(SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax> members, ref SingleTypeDeclaration.TypeDeclarationFlags declFlags)
		{
			bool count = members.Count != 0;
			ImmutableHashSet<string>.Builder builder = DeclarationTreeBuilder.s_memberNameBuilderPool.Allocate();
			if (count)
			{
				declFlags |= SingleTypeDeclaration.TypeDeclarationFlags.HasAnyNontypeMembers;
			}
			bool flag = false;
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax enumMemberDeclarationSyntax in members)
			{
				builder.Add(enumMemberDeclarationSyntax.Identifier.ValueText);
				if (!flag && enumMemberDeclarationSyntax.AttributeLists.Any())
				{
					flag = true;
				}
			}
			if (flag)
			{
				declFlags |= SingleTypeDeclaration.TypeDeclarationFlags.AnyMemberHasAttributes;
			}
			return DeclarationTreeBuilder.ToImmutableAndFree(builder);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0008414C File Offset: 0x0008234C
		private static ImmutableHashSet<string> GetNonTypeMemberNames(SyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax> members, ref SingleTypeDeclaration.TypeDeclarationFlags declFlags, bool skipGlobalStatements = false)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			ImmutableHashSet<string>.Builder builder = DeclarationTreeBuilder.s_memberNameBuilderPool.Allocate();
			foreach (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MemberDeclarationSyntax memberDeclarationSyntax in members)
			{
				DeclarationTreeBuilder.AddNonTypeMemberNames(memberDeclarationSyntax, builder, ref flag3, skipGlobalStatements);
				if (!flag && DeclarationTreeBuilder.CheckMethodMemberForExtensionSyntax(memberDeclarationSyntax))
				{
					flag = true;
				}
				if (!flag2 && DeclarationTreeBuilder.CheckMemberForAttributes(memberDeclarationSyntax))
				{
					flag2 = true;
				}
			}
			if (flag)
			{
				declFlags |= SingleTypeDeclaration.TypeDeclarationFlags.AnyMemberHasExtensionMethodSyntax;
			}
			if (flag2)
			{
				declFlags |= SingleTypeDeclaration.TypeDeclarationFlags.AnyMemberHasAttributes;
			}
			if (flag3)
			{
				declFlags |= SingleTypeDeclaration.TypeDeclarationFlags.HasAnyNontypeMembers;
			}
			return DeclarationTreeBuilder.ToImmutableAndFree(builder);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000841D4 File Offset: 0x000823D4
		private static bool CheckMethodMemberForExtensionSyntax(CSharpSyntaxNode member)
		{
			if (member.Kind == SyntaxKind.MethodDeclaration)
			{
				Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterListSyntax parameterList = ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MethodDeclarationSyntax)member).parameterList;
				if (parameterList != null)
				{
					SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ParameterSyntax> parameters = parameterList.Parameters;
					if (parameters.Count != 0)
					{
						SyntaxList<SyntaxToken>.Enumerator enumerator = parameters[0].Modifiers.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Kind == SyntaxKind.ThisKeyword)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00084244 File Offset: 0x00082444
		private static bool CheckMemberForAttributes(CSharpSyntaxNode member)
		{
			SyntaxKind kind = member.Kind;
			if (kind <= SyntaxKind.EventFieldDeclaration)
			{
				if (kind <= SyntaxKind.EnumDeclaration)
				{
					if (kind == SyntaxKind.CompilationUnit)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.CompilationUnitSyntax)member).AttributeLists.Any();
					}
					if (kind - SyntaxKind.ClassDeclaration > 3)
					{
						return false;
					}
				}
				else
				{
					if (kind == SyntaxKind.DelegateDeclaration)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.DelegateDeclarationSyntax)member).AttributeLists.Any();
					}
					if (kind - SyntaxKind.FieldDeclaration > 1)
					{
						return false;
					}
					return ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseFieldDeclarationSyntax)member).AttributeLists.Any();
				}
			}
			else if (kind <= SyntaxKind.DestructorDeclaration)
			{
				if (kind - SyntaxKind.MethodDeclaration > 3 && kind != SyntaxKind.DestructorDeclaration)
				{
					return false;
				}
				return ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseMethodDeclarationSyntax)member).AttributeLists.Any();
			}
			else
			{
				if (kind - SyntaxKind.PropertyDeclaration <= 2)
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BasePropertyDeclarationSyntax basePropertyDeclarationSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BasePropertyDeclarationSyntax)member;
					bool flag = basePropertyDeclarationSyntax.AttributeLists.Any();
					if (!flag && basePropertyDeclarationSyntax.AccessorList != null)
					{
						foreach (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.AccessorDeclarationSyntax accessorDeclarationSyntax in basePropertyDeclarationSyntax.AccessorList.Accessors)
						{
							flag |= accessorDeclarationSyntax.AttributeLists.Any();
						}
					}
					return flag;
				}
				if (kind != SyntaxKind.RecordDeclaration && kind != SyntaxKind.RecordStructDeclaration)
				{
					return false;
				}
			}
			return ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.BaseTypeDeclarationSyntax)member).AttributeLists.Any();
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00084398 File Offset: 0x00082598
		private static void AddNonTypeMemberNames(CSharpSyntaxNode member, ImmutableHashSet<string>.Builder set, ref bool anyNonTypeMembers, bool skipGlobalStatements)
		{
			SyntaxKind kind = member.Kind;
			if (kind != SyntaxKind.GlobalStatement)
			{
				switch (kind)
				{
				case SyntaxKind.FieldDeclaration:
				{
					anyNonTypeMembers = true;
					SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclaratorSyntax> variables = ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.FieldDeclarationSyntax)member).Declaration.Variables;
					int count = variables.Count;
					for (int i = 0; i < count; i++)
					{
						set.Add(variables[i].Identifier.ValueText);
					}
					return;
				}
				case SyntaxKind.EventFieldDeclaration:
				{
					anyNonTypeMembers = true;
					SeparatedSyntaxList<Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.VariableDeclaratorSyntax> variables2 = ((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EventFieldDeclarationSyntax)member).Declaration.Variables;
					int count2 = variables2.Count;
					for (int j = 0; j < count2; j++)
					{
						set.Add(variables2[j].Identifier.ValueText);
					}
					return;
				}
				case SyntaxKind.MethodDeclaration:
				{
					anyNonTypeMembers = true;
					Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MethodDeclarationSyntax methodDeclarationSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.MethodDeclarationSyntax)member;
					if (methodDeclarationSyntax.ExplicitInterfaceSpecifier == null)
					{
						set.Add(methodDeclarationSyntax.Identifier.ValueText);
						return;
					}
					break;
				}
				case SyntaxKind.OperatorDeclaration:
				{
					anyNonTypeMembers = true;
					string text = OperatorFacts.OperatorNameFromDeclaration((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.OperatorDeclarationSyntax)member);
					set.Add(text);
					return;
				}
				case SyntaxKind.ConversionOperatorDeclaration:
					anyNonTypeMembers = true;
					set.Add((((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ConversionOperatorDeclarationSyntax)member).ImplicitOrExplicitKeyword.Kind == SyntaxKind.ImplicitKeyword) ? "op_Implicit" : "op_Explicit");
					return;
				case SyntaxKind.ConstructorDeclaration:
					anyNonTypeMembers = true;
					set.Add(((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.ConstructorDeclarationSyntax)member).Modifiers.Any(8347) ? ".cctor" : ".ctor");
					return;
				default:
					switch (kind)
					{
					case SyntaxKind.DestructorDeclaration:
						anyNonTypeMembers = true;
						set.Add("Finalize");
						return;
					case SyntaxKind.PropertyDeclaration:
					{
						anyNonTypeMembers = true;
						Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PropertyDeclarationSyntax propertyDeclarationSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.PropertyDeclarationSyntax)member;
						if (propertyDeclarationSyntax.ExplicitInterfaceSpecifier == null)
						{
							set.Add(propertyDeclarationSyntax.Identifier.ValueText);
							return;
						}
						break;
					}
					case SyntaxKind.EventDeclaration:
					{
						anyNonTypeMembers = true;
						Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EventDeclarationSyntax eventDeclarationSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.EventDeclarationSyntax)member;
						if (eventDeclarationSyntax.ExplicitInterfaceSpecifier == null)
						{
							set.Add(eventDeclarationSyntax.Identifier.ValueText);
							return;
						}
						break;
					}
					case SyntaxKind.IndexerDeclaration:
						anyNonTypeMembers = true;
						set.Add("this[]");
						return;
					default:
						return;
					}
					break;
				}
			}
			else if (!skipGlobalStatements)
			{
				anyNonTypeMembers = true;
			}
		}

		// Token: 0x040006C4 RID: 1732
		private readonly SyntaxTree _syntaxTree;

		// Token: 0x040006C5 RID: 1733
		private readonly string _scriptClassName;

		// Token: 0x040006C6 RID: 1734
		private readonly bool _isSubmission;

		// Token: 0x040006C7 RID: 1735
		private static readonly ObjectPool<ImmutableHashSet<string>.Builder> s_memberNameBuilderPool = new ObjectPool<ImmutableHashSet<string>.Builder>(() => ImmutableHashSet.CreateBuilder<string>());
	}
}
