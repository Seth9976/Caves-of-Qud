using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000360 RID: 864
	internal class LanguageParser : SyntaxParser
	{
		// Token: 0x060048A2 RID: 18594 RVA: 0x0011F32E File Offset: 0x0011D52E
		internal LanguageParser(Lexer lexer, CSharpSyntaxNode oldTree, IEnumerable<TextChangeRange> changes, LexerMode lexerMode = LexerMode.Syntax, CancellationToken cancellationToken = default(CancellationToken))
			: base(lexer, lexerMode, oldTree, changes, false, true, cancellationToken)
		{
			this._syntaxFactoryContext = new SyntaxFactoryContext();
			this._syntaxFactory = new ContextAwareSyntax(this._syntaxFactoryContext);
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x0011F366 File Offset: 0x0011D566
		private static bool IsSomeWord(SyntaxKind kind)
		{
			return kind == SyntaxKind.IdentifierToken || SyntaxFacts.IsKeywordKind(kind);
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x0011F378 File Offset: 0x0011D578
		private bool IsTerminator()
		{
			if (base.CurrentToken.Kind == SyntaxKind.EndOfFileToken)
			{
				return true;
			}
			for (int i = 1; i <= 67108864; i <<= 1)
			{
				LanguageParser.TerminatorState terminatorState = this._termState & (LanguageParser.TerminatorState)i;
				if (terminatorState <= LanguageParser.TerminatorState.IsEndOfFilterClause)
				{
					if (terminatorState <= LanguageParser.TerminatorState.IsPossibleEndOfVariableDeclaration)
					{
						if (terminatorState <= LanguageParser.TerminatorState.IsEndOfReturnType)
						{
							switch (terminatorState)
							{
							case LanguageParser.TerminatorState.IsNamespaceMemberStartOrStop:
								if (this.IsNamespaceMemberStartOrStop())
								{
									return true;
								}
								break;
							case LanguageParser.TerminatorState.IsAttributeDeclarationTerminator:
								if (this.IsAttributeDeclarationTerminator())
								{
									return true;
								}
								break;
							case LanguageParser.TerminatorState.IsNamespaceMemberStartOrStop | LanguageParser.TerminatorState.IsAttributeDeclarationTerminator:
								break;
							case LanguageParser.TerminatorState.IsPossibleAggregateClauseStartOrStop:
								if (this.IsPossibleAggregateClauseStartOrStop())
								{
									return true;
								}
								break;
							default:
								if (terminatorState != LanguageParser.TerminatorState.IsPossibleMemberStartOrStop)
								{
									if (terminatorState == LanguageParser.TerminatorState.IsEndOfReturnType)
									{
										if (this.IsEndOfReturnType())
										{
											return true;
										}
									}
								}
								else if (this.IsPossibleMemberStartOrStop())
								{
									return true;
								}
								break;
							}
						}
						else if (terminatorState != LanguageParser.TerminatorState.IsEndOfParameterList)
						{
							if (terminatorState != LanguageParser.TerminatorState.IsEndOfFieldDeclaration)
							{
								if (terminatorState == LanguageParser.TerminatorState.IsPossibleEndOfVariableDeclaration)
								{
									if (this.IsPossibleEndOfVariableDeclaration())
									{
										return true;
									}
								}
							}
							else if (this.IsEndOfFieldDeclaration())
							{
								return true;
							}
						}
						else if (this.IsEndOfParameterList())
						{
							return true;
						}
					}
					else if (terminatorState <= LanguageParser.TerminatorState.IsEndOfFixedStatement)
					{
						if (terminatorState != LanguageParser.TerminatorState.IsEndOfTypeArgumentList)
						{
							if (terminatorState != LanguageParser.TerminatorState.IsPossibleStatementStartOrStop)
							{
								if (terminatorState == LanguageParser.TerminatorState.IsEndOfFixedStatement)
								{
									if (this.IsEndOfFixedStatement())
									{
										return true;
									}
								}
							}
							else if (this.IsPossibleStatementStartOrStop())
							{
								return true;
							}
						}
						else if (this.IsEndOfTypeArgumentList())
						{
							return true;
						}
					}
					else if (terminatorState != LanguageParser.TerminatorState.IsEndOfTryBlock)
					{
						if (terminatorState != LanguageParser.TerminatorState.IsEndOfCatchClause)
						{
							if (terminatorState == LanguageParser.TerminatorState.IsEndOfFilterClause)
							{
								if (this.IsEndOfFilterClause())
								{
									return true;
								}
							}
						}
						else if (this.IsEndOfCatchClause())
						{
							return true;
						}
					}
					else if (this.IsEndOfTryBlock())
					{
						return true;
					}
				}
				else if (terminatorState <= LanguageParser.TerminatorState.IsSwitchSectionStart)
				{
					if (terminatorState <= LanguageParser.TerminatorState.IsEndOfForStatementArgument)
					{
						if (terminatorState != LanguageParser.TerminatorState.IsEndOfCatchBlock)
						{
							if (terminatorState != LanguageParser.TerminatorState.IsEndOfDoWhileExpression)
							{
								if (terminatorState == LanguageParser.TerminatorState.IsEndOfForStatementArgument)
								{
									if (this.IsEndOfForStatementArgument())
									{
										return true;
									}
								}
							}
							else if (this.IsEndOfDoWhileExpression())
							{
								return true;
							}
						}
						else if (this.IsEndOfCatchBlock())
						{
							return true;
						}
					}
					else if (terminatorState != LanguageParser.TerminatorState.IsEndOfDeclarationClause)
					{
						if (terminatorState != LanguageParser.TerminatorState.IsEndOfArgumentList)
						{
							if (terminatorState == LanguageParser.TerminatorState.IsSwitchSectionStart)
							{
								if (this.IsPossibleSwitchSection())
								{
									return true;
								}
							}
						}
						else if (this.IsEndOfArgumentList())
						{
							return true;
						}
					}
					else if (this.IsEndOfDeclarationClause())
					{
						return true;
					}
				}
				else if (terminatorState <= LanguageParser.TerminatorState.IsEndOfNameInExplicitInterface)
				{
					if (terminatorState != LanguageParser.TerminatorState.IsEndOfTypeParameterList)
					{
						if (terminatorState != LanguageParser.TerminatorState.IsEndOfMethodSignature)
						{
							if (terminatorState == LanguageParser.TerminatorState.IsEndOfNameInExplicitInterface)
							{
								if (this.IsEndOfNameInExplicitInterface())
								{
									return true;
								}
							}
						}
						else if (this.IsEndOfMethodSignature())
						{
							return true;
						}
					}
					else if (this.IsEndOfTypeParameterList())
					{
						return true;
					}
				}
				else if (terminatorState <= LanguageParser.TerminatorState.IsEndOfFunctionPointerParameterListErrored)
				{
					if (terminatorState != LanguageParser.TerminatorState.IsEndOfFunctionPointerParameterList)
					{
						if (terminatorState == LanguageParser.TerminatorState.IsEndOfFunctionPointerParameterListErrored)
						{
							if (this.IsEndOfFunctionPointerParameterList(true))
							{
								return true;
							}
						}
					}
					else if (this.IsEndOfFunctionPointerParameterList(false))
					{
						return true;
					}
				}
				else if (terminatorState != LanguageParser.TerminatorState.IsEndOfFunctionPointerCallingConvention)
				{
					if (terminatorState == LanguageParser.TerminatorState.IsEndOfRecordSignature)
					{
						if (this.IsEndOfRecordSignature())
						{
							return true;
						}
					}
				}
				else if (this.IsEndOfFunctionPointerCallingConvention())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x0011F69D File Offset: 0x0011D89D
		private static CSharpSyntaxNode GetOldParent(CSharpSyntaxNode node)
		{
			if (node == null)
			{
				return null;
			}
			return node.Parent;
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x0011F6AA File Offset: 0x0011D8AA
		internal CompilationUnitSyntax ParseCompilationUnit()
		{
			return this.ParseWithStackGuard<CompilationUnitSyntax>(new Func<CompilationUnitSyntax>(this.ParseCompilationUnitCore), () => SyntaxFactory.CompilationUnit(default(SyntaxList<ExternAliasDirectiveSyntax>), default(SyntaxList<UsingDirectiveSyntax>), default(SyntaxList<AttributeListSyntax>), default(SyntaxList<MemberDeclarationSyntax>), SyntaxFactory.Token(SyntaxKind.EndOfFileToken)));
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x0011F6E0 File Offset: 0x0011D8E0
		internal CompilationUnitSyntax ParseCompilationUnitCore()
		{
			SyntaxToken syntaxToken = null;
			SyntaxListBuilder syntaxListBuilder = null;
			LanguageParser.NamespaceBodyBuilder namespaceBodyBuilder = new LanguageParser.NamespaceBodyBuilder(this._pool);
			CompilationUnitSyntax compilationUnitSyntax2;
			try
			{
				this.ParseNamespaceBody(ref syntaxToken, ref namespaceBodyBuilder, ref syntaxListBuilder, SyntaxKind.CompilationUnit);
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.EndOfFileToken);
				CompilationUnitSyntax compilationUnitSyntax = this._syntaxFactory.CompilationUnit(namespaceBodyBuilder.Externs, namespaceBodyBuilder.Usings, namespaceBodyBuilder.Attributes, namespaceBodyBuilder.Members, syntaxToken2);
				if (syntaxListBuilder != null)
				{
					compilationUnitSyntax = base.AddLeadingSkippedSyntax<CompilationUnitSyntax>(compilationUnitSyntax, syntaxListBuilder.ToListNode());
					this._pool.Free(syntaxListBuilder);
				}
				compilationUnitSyntax2 = compilationUnitSyntax;
			}
			finally
			{
				namespaceBodyBuilder.Free(this._pool);
			}
			return compilationUnitSyntax2;
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0011F79C File Offset: 0x0011D99C
		internal TNode ParseWithStackGuard<TNode>(Func<TNode> parseFunc, Func<TNode> createEmptyNodeFunc) where TNode : CSharpSyntaxNode
		{
			TNode tnode;
			try
			{
				tnode = parseFunc.Invoke();
			}
			catch (InsufficientExecutionStackException)
			{
				tnode = this.CreateForGlobalFailure<TNode>(this.lexer.TextWindow.Position, createEmptyNodeFunc.Invoke());
			}
			return tnode;
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0011F7E4 File Offset: 0x0011D9E4
		private TNode CreateForGlobalFailure<TNode>(int position, TNode node) where TNode : CSharpSyntaxNode
		{
			SyntaxListBuilder syntaxListBuilder = new SyntaxListBuilder(1);
			syntaxListBuilder.Add(SyntaxFactory.BadToken(null, this.lexer.TextWindow.Text.ToString(), null));
			SkippedTokensTriviaSyntax skippedTokensTriviaSyntax = this._syntaxFactory.SkippedTokensTrivia(syntaxListBuilder.ToList<SyntaxToken>());
			node = base.AddLeadingSkippedSyntax<TNode>(node, skippedTokensTriviaSyntax);
			base.ForceEndOfFile();
			return base.AddError<TNode>(node, position, 0, ErrorCode.ERR_InsufficientStack, Array.Empty<object>());
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x0011F84F File Offset: 0x0011DA4F
		private NamespaceDeclarationSyntax ParseNamespaceDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxListBuilder modifiers)
		{
			this._recursionDepth++;
			StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
			NamespaceDeclarationSyntax namespaceDeclarationSyntax = this.ParseNamespaceDeclarationCore(attributeLists, modifiers);
			this._recursionDepth--;
			return namespaceDeclarationSyntax;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x0011F880 File Offset: 0x0011DA80
		private NamespaceDeclarationSyntax ParseNamespaceDeclarationCore(SyntaxList<AttributeListSyntax> attributeLists, SyntaxListBuilder modifiers)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.NamespaceKeyword);
			if (base.IsScript)
			{
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_NamespaceNotAllowedInScript);
			}
			NameSyntax nameSyntax = this.ParseQualifiedName(LanguageParser.NameOptions.None);
			SyntaxToken syntaxToken2;
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || this.IsPossibleNamespaceMemberDeclaration())
			{
				syntaxToken2 = base.EatToken(SyntaxKind.OpenBraceToken);
			}
			else
			{
				syntaxToken2 = base.EatTokenWithPrejudice(SyntaxKind.OpenBraceToken);
				syntaxToken2 = this.ConvertToMissingWithTrailingTrivia(syntaxToken2, SyntaxKind.OpenBraceToken);
			}
			LanguageParser.NamespaceBodyBuilder namespaceBodyBuilder = new LanguageParser.NamespaceBodyBuilder(this._pool);
			SyntaxListBuilder syntaxListBuilder = null;
			NamespaceDeclarationSyntax namespaceDeclarationSyntax;
			try
			{
				this.ParseNamespaceBody(ref syntaxToken2, ref namespaceBodyBuilder, ref syntaxListBuilder, SyntaxKind.NamespaceDeclaration);
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseBraceToken);
				SyntaxToken syntaxToken4 = base.TryEatToken(SyntaxKind.SemicolonToken);
				namespaceDeclarationSyntax = this._syntaxFactory.NamespaceDeclaration(attributeLists, modifiers.ToList(), syntaxToken, nameSyntax, syntaxToken2, namespaceBodyBuilder.Externs, namespaceBodyBuilder.Usings, namespaceBodyBuilder.Members, syntaxToken3, syntaxToken4);
			}
			finally
			{
				namespaceBodyBuilder.Free(this._pool);
			}
			return namespaceDeclarationSyntax;
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x0011F998 File Offset: 0x0011DB98
		private static bool IsPossibleStartOfTypeDeclaration(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.AbstractKeyword)
			{
				if (kind != SyntaxKind.OpenBracketToken)
				{
					switch (kind)
					{
					case SyntaxKind.PublicKeyword:
					case SyntaxKind.PrivateKeyword:
					case SyntaxKind.InternalKeyword:
					case SyntaxKind.ProtectedKeyword:
					case SyntaxKind.StaticKeyword:
					case SyntaxKind.SealedKeyword:
					case SyntaxKind.NewKeyword:
					case SyntaxKind.AbstractKeyword:
						break;
					case SyntaxKind.ReadOnlyKeyword:
					case SyntaxKind.ConstKeyword:
					case SyntaxKind.FixedKeyword:
					case SyntaxKind.StackAllocKeyword:
					case SyntaxKind.VolatileKeyword:
					case SyntaxKind.OverrideKeyword:
						return false;
					default:
						return false;
					}
				}
			}
			else if (kind - SyntaxKind.ClassKeyword > 4 && kind != SyntaxKind.UnsafeKeyword)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x0011FA10 File Offset: 0x0011DC10
		private void AddSkippedNamespaceText(ref SyntaxToken openBrace, ref LanguageParser.NamespaceBodyBuilder body, ref SyntaxListBuilder initialBadNodes, CSharpSyntaxNode skippedSyntax)
		{
			if (body.Members.Count > 0)
			{
				base.AddTrailingSkippedSyntax<MemberDeclarationSyntax>(body.Members, skippedSyntax);
				return;
			}
			if (body.Attributes.Count > 0)
			{
				base.AddTrailingSkippedSyntax<AttributeListSyntax>(body.Attributes, skippedSyntax);
				return;
			}
			if (body.Usings.Count > 0)
			{
				base.AddTrailingSkippedSyntax<UsingDirectiveSyntax>(body.Usings, skippedSyntax);
				return;
			}
			if (body.Externs.Count > 0)
			{
				base.AddTrailingSkippedSyntax<ExternAliasDirectiveSyntax>(body.Externs, skippedSyntax);
				return;
			}
			if (openBrace != null)
			{
				openBrace = base.AddTrailingSkippedSyntax<SyntaxToken>(openBrace, skippedSyntax);
				return;
			}
			if (initialBadNodes == null)
			{
				initialBadNodes = this._pool.Allocate();
			}
			initialBadNodes.AddRange(skippedSyntax);
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x0011FAC4 File Offset: 0x0011DCC4
		private void ParseNamespaceBody(ref SyntaxToken openBrace, ref LanguageParser.NamespaceBodyBuilder body, ref SyntaxListBuilder initialBadNodes, SyntaxKind parentKind)
		{
			bool flag = openBrace == null;
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsNamespaceMemberStartOrStop;
			LanguageParser.NamespaceParts namespaceParts = LanguageParser.NamespaceParts.None;
			SyntaxListBuilder<MemberDeclarationSyntax> syntaxListBuilder = this._pool.Allocate<MemberDeclarationSyntax>();
			bool flag2 = true;
			try
			{
				for (;;)
				{
					SyntaxKind kind = base.CurrentToken.Kind;
					if (kind <= SyntaxKind.ExternKeyword)
					{
						if (kind != SyntaxKind.CloseBraceToken)
						{
							if (kind != SyntaxKind.OpenBracketToken)
							{
								if (kind == SyntaxKind.ExternKeyword)
								{
									if (!flag || this.ScanExternAliasDirective())
									{
										this.ReduceIncompleteMembers(ref syntaxListBuilder, ref openBrace, ref body, ref initialBadNodes);
										ExternAliasDirectiveSyntax externAliasDirectiveSyntax = this.ParseExternAliasDirective();
										if (namespaceParts > LanguageParser.NamespaceParts.ExternAliases)
										{
											externAliasDirectiveSyntax = base.AddErrorToFirstToken<ExternAliasDirectiveSyntax>(externAliasDirectiveSyntax, ErrorCode.ERR_ExternAfterElements);
											this.AddSkippedNamespaceText(ref openBrace, ref body, ref initialBadNodes, externAliasDirectiveSyntax);
										}
										else
										{
											body.Externs.Add(externAliasDirectiveSyntax);
											namespaceParts = LanguageParser.NamespaceParts.ExternAliases;
										}
										flag2 = true;
										continue;
									}
								}
							}
							else if (this.IsPossibleGlobalAttributeDeclaration())
							{
								this.ReduceIncompleteMembers(ref syntaxListBuilder, ref openBrace, ref body, ref initialBadNodes);
								AttributeListSyntax attributeListSyntax = this.ParseAttributeDeclaration();
								if (!flag || namespaceParts > LanguageParser.NamespaceParts.GlobalAttributes)
								{
									attributeListSyntax = base.AddError<AttributeListSyntax>(attributeListSyntax, attributeListSyntax.Target.Identifier, ErrorCode.ERR_GlobalAttributesNotFirst, Array.Empty<object>());
									this.AddSkippedNamespaceText(ref openBrace, ref body, ref initialBadNodes, attributeListSyntax);
								}
								else
								{
									body.Attributes.Add(attributeListSyntax);
									namespaceParts = LanguageParser.NamespaceParts.GlobalAttributes;
								}
								flag2 = true;
								continue;
							}
						}
						else
						{
							if (flag)
							{
								this.ReduceIncompleteMembers(ref syntaxListBuilder, ref openBrace, ref body, ref initialBadNodes);
								SyntaxToken syntaxToken = base.EatToken();
								syntaxToken = base.AddError<SyntaxToken>(syntaxToken, base.IsScript ? ErrorCode.ERR_GlobalDefinitionOrStatementExpected : ErrorCode.ERR_EOFExpected);
								this.AddSkippedNamespaceText(ref openBrace, ref body, ref initialBadNodes, syntaxToken);
								flag2 = true;
								continue;
							}
							break;
						}
					}
					else if (kind <= SyntaxKind.UsingKeyword)
					{
						if (kind == SyntaxKind.NamespaceKeyword)
						{
							LanguageParser.AddIncompleteMembers(ref syntaxListBuilder, ref body);
							SyntaxListBuilder<AttributeListSyntax> syntaxListBuilder2 = this._pool.Allocate<AttributeListSyntax>();
							SyntaxListBuilder syntaxListBuilder3 = this._pool.Allocate();
							body.Members.Add(this.<ParseNamespaceBody>g__adjustStateAndReportStatementOutOfOrder|23_0(ref namespaceParts, this.ParseNamespaceDeclaration(syntaxListBuilder2, syntaxListBuilder3)));
							this._pool.Free(syntaxListBuilder2);
							this._pool.Free(syntaxListBuilder3);
							flag2 = true;
							continue;
						}
						if (kind == SyntaxKind.UsingKeyword)
						{
							if (!flag || (base.PeekToken(1).Kind != SyntaxKind.OpenParenToken && (base.IsScript || !this.IsPossibleTopLevelUsingLocalDeclarationStatement())))
							{
								this.<ParseNamespaceBody>g__parseUsingDirective|23_1(ref openBrace, ref body, ref initialBadNodes, ref namespaceParts, ref syntaxListBuilder);
								flag2 = true;
								continue;
							}
						}
					}
					else
					{
						if (kind == SyntaxKind.EndOfFileToken)
						{
							break;
						}
						if (kind == SyntaxKind.IdentifierToken)
						{
							if (base.CurrentToken.ContextualKind == SyntaxKind.GlobalKeyword && base.PeekToken(1).Kind == SyntaxKind.UsingKeyword)
							{
								this.<ParseNamespaceBody>g__parseUsingDirective|23_1(ref openBrace, ref body, ref initialBadNodes, ref namespaceParts, ref syntaxListBuilder);
								flag2 = true;
								continue;
							}
						}
					}
					MemberDeclarationSyntax memberDeclarationSyntax = (flag ? this.ParseMemberDeclarationOrStatement(parentKind) : this.ParseMemberDeclaration(parentKind));
					if (memberDeclarationSyntax == null)
					{
						this.ReduceIncompleteMembers(ref syntaxListBuilder, ref openBrace, ref body, ref initialBadNodes);
						SyntaxToken syntaxToken2 = base.EatToken();
						if (flag2 && !syntaxToken2.ContainsDiagnostics)
						{
							syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, base.IsScript ? ErrorCode.ERR_GlobalDefinitionOrStatementExpected : ErrorCode.ERR_EOFExpected);
							flag2 = false;
						}
						this.AddSkippedNamespaceText(ref openBrace, ref body, ref initialBadNodes, syntaxToken2);
					}
					else if (memberDeclarationSyntax.Kind == SyntaxKind.IncompleteMember && namespaceParts < LanguageParser.NamespaceParts.MembersAndStatements)
					{
						syntaxListBuilder.Add(memberDeclarationSyntax);
						flag2 = true;
					}
					else
					{
						LanguageParser.AddIncompleteMembers(ref syntaxListBuilder, ref body);
						body.Members.Add(this.<ParseNamespaceBody>g__adjustStateAndReportStatementOutOfOrder|23_0(ref namespaceParts, memberDeclarationSyntax));
						flag2 = true;
					}
				}
			}
			finally
			{
				this._termState = termState;
				LanguageParser.AddIncompleteMembers(ref syntaxListBuilder, ref body);
				this._pool.Free(syntaxListBuilder);
			}
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0011FE68 File Offset: 0x0011E068
		private GlobalStatementSyntax CheckTopLevelStatementsFeatureAvailability(GlobalStatementSyntax globalStatementSyntax)
		{
			if (base.IsScript || this._checkedTopLevelStatementsFeatureAvailability)
			{
				return globalStatementSyntax;
			}
			this._checkedTopLevelStatementsFeatureAvailability = true;
			return base.CheckFeatureAvailability<GlobalStatementSyntax>(globalStatementSyntax, MessageID.IDS_TopLevelStatements, false);
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0011FE90 File Offset: 0x0011E090
		private static void AddIncompleteMembers(ref SyntaxListBuilder<MemberDeclarationSyntax> incompleteMembers, ref LanguageParser.NamespaceBodyBuilder body)
		{
			if (incompleteMembers.Count > 0)
			{
				body.Members.AddRange(incompleteMembers);
				incompleteMembers.Clear();
			}
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x0011FEB8 File Offset: 0x0011E0B8
		private void ReduceIncompleteMembers(ref SyntaxListBuilder<MemberDeclarationSyntax> incompleteMembers, ref SyntaxToken openBrace, ref LanguageParser.NamespaceBodyBuilder body, ref SyntaxListBuilder initialBadNodes)
		{
			for (int i = 0; i < incompleteMembers.Count; i++)
			{
				this.AddSkippedNamespaceText(ref openBrace, ref body, ref initialBadNodes, incompleteMembers[i]);
			}
			incompleteMembers.Clear();
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0011FEF0 File Offset: 0x0011E0F0
		private bool IsPossibleNamespaceMemberDeclaration()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind == SyntaxKind.ExternKeyword || kind - SyntaxKind.NamespaceKeyword <= 1)
			{
				return true;
			}
			if (kind != SyntaxKind.IdentifierToken)
			{
				return LanguageParser.IsPossibleStartOfTypeDeclaration(base.CurrentToken.Kind);
			}
			return this.IsPartialInNamespaceMemberDeclaration();
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x0011FF3E File Offset: 0x0011E13E
		private bool IsPartialInNamespaceMemberDeclaration()
		{
			if (base.CurrentToken.ContextualKind == SyntaxKind.PartialKeyword)
			{
				if (this.IsPartialType())
				{
					return true;
				}
				if (base.PeekToken(1).Kind == SyntaxKind.NamespaceKeyword)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0011FF72 File Offset: 0x0011E172
		public bool IsEndOfNamespace()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseBraceToken;
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x0011FF86 File Offset: 0x0011E186
		public bool IsGobalAttributesTerminator()
		{
			return this.IsEndOfNamespace() || this.IsPossibleNamespaceMemberDeclaration();
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0011FF98 File Offset: 0x0011E198
		private bool IsNamespaceMemberStartOrStop()
		{
			return this.IsEndOfNamespace() || this.IsPossibleNamespaceMemberDeclaration();
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x0011FFAC File Offset: 0x0011E1AC
		private bool ScanExternAliasDirective()
		{
			return base.CurrentToken.Kind == SyntaxKind.ExternKeyword && base.PeekToken(1).Kind == SyntaxKind.IdentifierToken && base.PeekToken(1).ContextualKind == SyntaxKind.AliasKeyword && base.PeekToken(2).Kind == SyntaxKind.IdentifierToken && base.PeekToken(3).Kind == SyntaxKind.SemicolonToken;
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x0012001C File Offset: 0x0011E21C
		private ExternAliasDirectiveSyntax ParseExternAliasDirective()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.ExternAliasDirective)
			{
				return (ExternAliasDirectiveSyntax)base.EatNode();
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ExternKeyword);
			SyntaxToken syntaxToken2 = base.EatContextualToken(SyntaxKind.AliasKeyword, true);
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureExternAlias, false);
			SyntaxToken syntaxToken3 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.ExternAliasDirective(syntaxToken, syntaxToken2, syntaxToken3, syntaxToken4);
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x00120098 File Offset: 0x0011E298
		private NameEqualsSyntax ParseNameEquals()
		{
			return this._syntaxFactory.NameEquals(this._syntaxFactory.IdentifierName(this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected)), base.EatToken(SyntaxKind.EqualsToken));
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x001200C8 File Offset: 0x0011E2C8
		private UsingDirectiveSyntax ParseUsingDirective()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.UsingDirective)
			{
				return (UsingDirectiveSyntax)base.EatNode();
			}
			SyntaxToken syntaxToken = null;
			if (base.CurrentToken.ContextualKind == SyntaxKind.GlobalKeyword)
			{
				syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.UsingKeyword);
			SyntaxToken syntaxToken3 = base.TryEatToken(SyntaxKind.StaticKeyword);
			NameEqualsSyntax nameEqualsSyntax = (this.IsNamedAssignment() ? this.ParseNameEquals() : null);
			NameSyntax nameSyntax;
			SyntaxToken syntaxToken4;
			if (this.IsPossibleNamespaceMemberDeclaration())
			{
				nameSyntax = this.WithAdditionalDiagnostics<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), new DiagnosticInfo[] { this.GetExpectedTokenError(SyntaxKind.IdentifierToken, base.CurrentToken.Kind) });
				syntaxToken4 = SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken);
			}
			else
			{
				nameSyntax = this.ParseQualifiedName(LanguageParser.NameOptions.None);
				if (nameSyntax.IsMissing && base.PeekToken(1).Kind == SyntaxKind.SemicolonToken)
				{
					nameSyntax = base.AddTrailingSkippedSyntax<NameSyntax>(nameSyntax, base.EatToken());
				}
				syntaxToken4 = base.EatToken(SyntaxKind.SemicolonToken);
			}
			UsingDirectiveSyntax usingDirectiveSyntax = this._syntaxFactory.UsingDirective(syntaxToken, syntaxToken2, syntaxToken3, nameEqualsSyntax, nameSyntax, syntaxToken4);
			if (syntaxToken3 != null)
			{
				usingDirectiveSyntax = base.CheckFeatureAvailability<UsingDirectiveSyntax>(usingDirectiveSyntax, MessageID.IDS_FeatureUsingStatic, false);
			}
			if (syntaxToken != null)
			{
				usingDirectiveSyntax = base.CheckFeatureAvailability<UsingDirectiveSyntax>(usingDirectiveSyntax, MessageID.IDS_FeatureGlobalUsing, false);
			}
			return usingDirectiveSyntax;
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x00120202 File Offset: 0x0011E402
		private bool IsPossibleGlobalAttributeDeclaration()
		{
			return base.CurrentToken.Kind == SyntaxKind.OpenBracketToken && LanguageParser.IsGlobalAttributeTarget(base.PeekToken(1)) && base.PeekToken(2).Kind == SyntaxKind.ColonToken;
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0012023C File Offset: 0x0011E43C
		private static bool IsGlobalAttributeTarget(SyntaxToken token)
		{
			AttributeLocation attributeLocation = token.ToAttributeLocation();
			return attributeLocation - AttributeLocation.Assembly <= 1;
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x00120259 File Offset: 0x0011E459
		private bool IsPossibleAttributeDeclaration()
		{
			return base.CurrentToken.Kind == SyntaxKind.OpenBracketToken;
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x00120270 File Offset: 0x0011E470
		private SyntaxList<AttributeListSyntax> ParseAttributeDeclarations()
		{
			SyntaxListBuilder<AttributeListSyntax> syntaxListBuilder = this._pool.Allocate<AttributeListSyntax>();
			SyntaxList<AttributeListSyntax> syntaxList;
			try
			{
				LanguageParser.TerminatorState termState = this._termState;
				this._termState |= LanguageParser.TerminatorState.IsAttributeDeclarationTerminator;
				while (this.IsPossibleAttributeDeclaration())
				{
					AttributeListSyntax attributeListSyntax = this.ParseAttributeDeclaration();
					syntaxListBuilder.Add(attributeListSyntax);
				}
				this._termState = termState;
				syntaxList = syntaxListBuilder.ToList();
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return syntaxList;
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x001202EC File Offset: 0x0011E4EC
		private bool IsAttributeDeclarationTerminator()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseBracketToken || this.IsPossibleAttributeDeclaration();
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x00120308 File Offset: 0x0011E508
		private AttributeListSyntax ParseAttributeDeclaration()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.AttributeList)
			{
				return (AttributeListSyntax)base.EatNode();
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBracketToken);
			AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax = null;
			if (LanguageParser.IsSomeWord(base.CurrentToken.Kind) && base.PeekToken(1).Kind == SyntaxKind.ColonToken)
			{
				SyntaxToken syntaxToken2 = SyntaxParser.ConvertToKeyword(base.EatToken());
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.ColonToken);
				attributeTargetSpecifierSyntax = this._syntaxFactory.AttributeTargetSpecifier(syntaxToken2, syntaxToken3);
			}
			SeparatedSyntaxListBuilder<AttributeSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<AttributeSyntax>();
			AttributeListSyntax attributeListSyntax;
			try
			{
				if (attributeTargetSpecifierSyntax != null && attributeTargetSpecifierSyntax.Identifier.ToAttributeLocation() == AttributeLocation.Module)
				{
					attributeTargetSpecifierSyntax = base.CheckFeatureAvailability<AttributeTargetSpecifierSyntax>(attributeTargetSpecifierSyntax, MessageID.IDS_FeatureModuleAttrLoc, false);
				}
				this.ParseAttributes(separatedSyntaxListBuilder);
				SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseBracketToken);
				attributeListSyntax = this._syntaxFactory.AttributeList(syntaxToken, attributeTargetSpecifierSyntax, in separatedSyntaxListBuilder, syntaxToken4);
			}
			finally
			{
				this._pool.Free<AttributeSyntax>(in separatedSyntaxListBuilder);
			}
			return attributeListSyntax;
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x0012040C File Offset: 0x0011E60C
		private void ParseAttributes(SeparatedSyntaxListBuilder<AttributeSyntax> nodes)
		{
			nodes.Add(this.ParseAttribute());
			while (base.CurrentToken.Kind != SyntaxKind.CloseBracketToken)
			{
				if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
				{
					nodes.AddSeparator(base.EatToken());
					if (base.CurrentToken.Kind == SyntaxKind.CloseBracketToken)
					{
						break;
					}
					nodes.Add(this.ParseAttribute());
				}
				else if (this.IsPossibleAttribute())
				{
					nodes.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
					nodes.Add(this.ParseAttribute());
				}
				else if (this.SkipBadAttributeListTokens(nodes, SyntaxKind.IdentifierToken) == LanguageParser.PostSkipAction.Abort)
				{
					break;
				}
			}
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x001204B8 File Offset: 0x0011E6B8
		private LanguageParser.PostSkipAction SkipBadAttributeListTokens(SeparatedSyntaxListBuilder<AttributeSyntax> list, SyntaxKind expected)
		{
			SyntaxToken syntaxToken = null;
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, AttributeSyntax>(ref syntaxToken, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleAttribute(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseBracketToken || p.IsTerminator(), expected);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0012050F File Offset: 0x0011E70F
		private bool IsPossibleAttribute()
		{
			return this.IsTrueIdentifier();
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x00120518 File Offset: 0x0011E718
		private AttributeSyntax ParseAttribute()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.Attribute)
			{
				return (AttributeSyntax)base.EatNode();
			}
			NameSyntax nameSyntax = this.ParseQualifiedName(LanguageParser.NameOptions.None);
			AttributeArgumentListSyntax attributeArgumentListSyntax = this.ParseAttributeArgumentList();
			return this._syntaxFactory.Attribute(nameSyntax, attributeArgumentListSyntax);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x00120564 File Offset: 0x0011E764
		internal AttributeArgumentListSyntax ParseAttributeArgumentList()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.AttributeArgumentList)
			{
				return (AttributeArgumentListSyntax)base.EatNode();
			}
			AttributeArgumentListSyntax attributeArgumentListSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenParenToken);
				SeparatedSyntaxListBuilder<AttributeArgumentSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<AttributeArgumentSyntax>();
				try
				{
					while (base.CurrentToken.Kind != SyntaxKind.CloseParenToken)
					{
						if (this.IsPossibleAttributeArgument() || base.CurrentToken.Kind == SyntaxKind.CommaToken)
						{
							separatedSyntaxListBuilder.Add(this.ParseAttributeArgument());
							int num = -1;
							while (base.IsMakingProgress(ref num, true))
							{
								if (base.CurrentToken.Kind == SyntaxKind.CloseParenToken)
								{
									break;
								}
								if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleAttributeArgument())
								{
									separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
									separatedSyntaxListBuilder.Add(this.ParseAttributeArgument());
								}
								else if (this.SkipBadAttributeArgumentTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
								{
									break;
								}
							}
							break;
						}
						if (this.SkipBadAttributeArgumentTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.IdentifierToken) != LanguageParser.PostSkipAction.Continue)
						{
							break;
						}
					}
					SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseParenToken);
					attributeArgumentListSyntax = this._syntaxFactory.AttributeArgumentList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
				}
				finally
				{
					this._pool.Free<AttributeArgumentSyntax>(in separatedSyntaxListBuilder);
				}
			}
			return attributeArgumentListSyntax;
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x001206C4 File Offset: 0x0011E8C4
		private LanguageParser.PostSkipAction SkipBadAttributeArgumentTokens(ref SyntaxToken openParen, SeparatedSyntaxListBuilder<AttributeArgumentSyntax> list, SyntaxKind expected)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, AttributeArgumentSyntax>(ref openParen, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleAttributeArgument(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseParenToken || p.IsTerminator(), expected);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x00120718 File Offset: 0x0011E918
		private bool IsPossibleAttributeArgument()
		{
			return this.IsPossibleExpression();
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x00120720 File Offset: 0x0011E920
		private AttributeArgumentSyntax ParseAttributeArgument()
		{
			NameEqualsSyntax nameEqualsSyntax = null;
			NameColonSyntax nameColonSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken)
			{
				SyntaxKind kind = base.PeekToken(1).Kind;
				if (kind != SyntaxKind.EqualsToken)
				{
					if (kind == SyntaxKind.ColonToken)
					{
						IdentifierNameSyntax identifierNameSyntax = this.ParseIdentifierName(ErrorCode.ERR_IdentifierExpected);
						SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ColonToken);
						nameColonSyntax = this._syntaxFactory.NameColon(identifierNameSyntax, syntaxToken);
						nameColonSyntax = base.CheckFeatureAvailability<NameColonSyntax>(nameColonSyntax, MessageID.IDS_FeatureNamedArgument, false);
					}
				}
				else
				{
					SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
					SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.EqualsToken);
					nameEqualsSyntax = this._syntaxFactory.NameEquals(this._syntaxFactory.IdentifierName(syntaxToken2), syntaxToken3);
				}
			}
			return this._syntaxFactory.AttributeArgument(nameEqualsSyntax, nameColonSyntax, this.ParseExpressionCore());
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x001207E5 File Offset: 0x0011E9E5
		private static DeclarationModifiers GetModifier(SyntaxToken token)
		{
			return LanguageParser.GetModifier(token.Kind, token.ContextualKind);
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x001207F8 File Offset: 0x0011E9F8
		internal static DeclarationModifiers GetModifier(SyntaxKind kind, SyntaxKind contextualKind)
		{
			if (kind <= SyntaxKind.PartialKeyword)
			{
				switch (kind)
				{
				case SyntaxKind.PublicKeyword:
					return DeclarationModifiers.Public;
				case SyntaxKind.PrivateKeyword:
					return DeclarationModifiers.Private;
				case SyntaxKind.InternalKeyword:
					return DeclarationModifiers.Internal;
				case SyntaxKind.ProtectedKeyword:
					return DeclarationModifiers.Protected;
				case SyntaxKind.StaticKeyword:
					return DeclarationModifiers.Static;
				case SyntaxKind.ReadOnlyKeyword:
					return DeclarationModifiers.ReadOnly;
				case SyntaxKind.SealedKeyword:
					return DeclarationModifiers.Sealed;
				case SyntaxKind.ConstKeyword:
				case SyntaxKind.FixedKeyword:
				case SyntaxKind.StackAllocKeyword:
				case SyntaxKind.EventKeyword:
					break;
				case SyntaxKind.VolatileKeyword:
					return DeclarationModifiers.Volatile;
				case SyntaxKind.NewKeyword:
					return DeclarationModifiers.New;
				case SyntaxKind.OverrideKeyword:
					return DeclarationModifiers.Override;
				case SyntaxKind.AbstractKeyword:
					return DeclarationModifiers.Abstract;
				case SyntaxKind.VirtualKeyword:
					return DeclarationModifiers.Virtual;
				case SyntaxKind.ExternKeyword:
					return DeclarationModifiers.Extern;
				case SyntaxKind.RefKeyword:
					return DeclarationModifiers.Ref;
				default:
					if (kind == SyntaxKind.UnsafeKeyword)
					{
						return DeclarationModifiers.Unsafe;
					}
					if (kind == SyntaxKind.PartialKeyword)
					{
						return DeclarationModifiers.Partial;
					}
					break;
				}
			}
			else
			{
				if (kind == SyntaxKind.AsyncKeyword)
				{
					return DeclarationModifiers.Async;
				}
				if (kind == SyntaxKind.DataKeyword)
				{
					return DeclarationModifiers.Data;
				}
				if (kind == SyntaxKind.IdentifierToken)
				{
					if (contextualKind == SyntaxKind.PartialKeyword)
					{
						return DeclarationModifiers.Partial;
					}
					if (contextualKind == SyntaxKind.AsyncKeyword)
					{
						return DeclarationModifiers.Async;
					}
					if (contextualKind == SyntaxKind.DataKeyword)
					{
						return DeclarationModifiers.Data;
					}
				}
			}
			return DeclarationModifiers.None;
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x00120910 File Offset: 0x0011EB10
		private void ParseModifiers(SyntaxListBuilder tokens, bool forAccessors)
		{
			for (;;)
			{
				DeclarationModifiers modifier = LanguageParser.GetModifier(base.CurrentToken);
				if (modifier != DeclarationModifiers.None)
				{
					SyntaxToken syntaxToken;
					if (modifier <= DeclarationModifiers.Async)
					{
						if (modifier != DeclarationModifiers.Partial)
						{
							if (modifier != DeclarationModifiers.Async)
							{
								goto IL_0180;
							}
							if (!this.ShouldAsyncBeTreatedAsModifier(false))
							{
								return;
							}
							syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
							syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureAsync, false);
						}
						else
						{
							SyntaxToken syntaxToken2 = base.PeekToken(1);
							bool flag = this.IsPartialType();
							bool flag2 = this.IsPartialMember();
							if (flag || flag2)
							{
								syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
								syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, flag ? MessageID.IDS_FeaturePartialTypes : MessageID.IDS_FeaturePartialMethod, false);
							}
							else if (syntaxToken2.Kind == SyntaxKind.NamespaceKeyword)
							{
								syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
							}
							else
							{
								if (syntaxToken2.Kind != SyntaxKind.EnumKeyword && syntaxToken2.Kind != SyntaxKind.DelegateKeyword && (!LanguageParser.IsPossibleStartOfTypeDeclaration(syntaxToken2.Kind) || LanguageParser.GetModifier(syntaxToken2) == DeclarationModifiers.None))
								{
									break;
								}
								syntaxToken = base.AddError<SyntaxToken>(SyntaxParser.ConvertToKeyword(base.EatToken()), ErrorCode.ERR_PartialMisplaced);
							}
						}
					}
					else if (modifier != DeclarationModifiers.Ref)
					{
						if (modifier != DeclarationModifiers.Data)
						{
							goto IL_0180;
						}
						return;
					}
					else
					{
						SyntaxToken syntaxToken3 = base.PeekToken(1);
						if (this.<ParseModifiers>g__isStructOrRecordKeyword|52_0(syntaxToken3) || (syntaxToken3.ContextualKind == SyntaxKind.PartialKeyword && this.<ParseModifiers>g__isStructOrRecordKeyword|52_0(base.PeekToken(2))))
						{
							syntaxToken = base.EatToken();
							syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureRefStructs, false);
						}
						else
						{
							if (!forAccessors || !this.IsPossibleAccessorModifier())
							{
								return;
							}
							syntaxToken = base.EatToken();
						}
					}
					IL_0187:
					tokens.Add(syntaxToken);
					continue;
					IL_0180:
					syntaxToken = base.EatToken();
					goto IL_0187;
				}
				return;
			}
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x00120AB0 File Offset: 0x0011ECB0
		private bool ShouldAsyncBeTreatedAsModifier(bool parsingStatementNotDeclaration)
		{
			if (LanguageParser.IsNonContextualModifier(base.PeekToken(1)))
			{
				return true;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			try
			{
				base.EatToken();
				if (!parsingStatementNotDeclaration && base.CurrentToken.ContextualKind == SyntaxKind.PartialKeyword)
				{
					base.EatToken();
				}
				if (!parsingStatementNotDeclaration)
				{
					SyntaxKind kind = base.CurrentToken.Kind;
					if (LanguageParser.IsPossibleStartOfTypeDeclaration(kind) || kind == SyntaxKind.EventKeyword || ((kind == SyntaxKind.ExplicitKeyword || kind == SyntaxKind.ImplicitKeyword) && base.PeekToken(1).Kind == SyntaxKind.OperatorKeyword))
					{
						return true;
					}
				}
				if (this.ScanType(false) != LanguageParser.ScanTypeFlags.NotType)
				{
					if (this.IsPossibleMemberName())
					{
						return true;
					}
					SyntaxKind kind2 = base.CurrentToken.Kind;
					if (kind2 == SyntaxKind.EndOfFileToken)
					{
						return true;
					}
					if (kind2 == SyntaxKind.CloseBraceToken)
					{
						return true;
					}
					if (SyntaxFacts.IsPredefinedType(base.CurrentToken.Kind))
					{
						return true;
					}
					if (LanguageParser.IsNonContextualModifier(base.CurrentToken))
					{
						return true;
					}
					if (this.IsTypeDeclarationStart())
					{
						return true;
					}
					if (kind2 == SyntaxKind.NamespaceKeyword)
					{
						return true;
					}
					if (!parsingStatementNotDeclaration && kind2 == SyntaxKind.OperatorKeyword)
					{
						return true;
					}
				}
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return false;
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x00120BF4 File Offset: 0x0011EDF4
		private static bool IsNonContextualModifier(SyntaxToken nextToken)
		{
			return LanguageParser.GetModifier(nextToken) != DeclarationModifiers.None && !SyntaxFacts.IsContextualKeyword(nextToken.ContextualKind);
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x00120C10 File Offset: 0x0011EE10
		private bool IsPartialType()
		{
			SyntaxToken syntaxToken = base.PeekToken(1);
			SyntaxKind kind = syntaxToken.Kind;
			return kind - SyntaxKind.ClassKeyword <= 2 || (syntaxToken.ContextualKind == SyntaxKind.RecordKeyword && base.IsFeatureEnabled(MessageID.IDS_FeatureRecords));
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x00120C54 File Offset: 0x0011EE54
		private bool IsPartialMember()
		{
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag;
			try
			{
				base.EatToken();
				if (this.ScanType(false) == LanguageParser.ScanTypeFlags.NotType)
				{
					flag = false;
				}
				else
				{
					flag = this.IsPossibleMemberName();
				}
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag;
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x00120CA8 File Offset: 0x0011EEA8
		private bool IsPossibleMemberName()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			return kind == SyntaxKind.ThisKeyword || (kind == SyntaxKind.IdentifierToken && (base.CurrentToken.ContextualKind != SyntaxKind.GlobalKeyword || base.PeekToken(1).Kind != SyntaxKind.UsingKeyword));
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x00120D00 File Offset: 0x0011EF00
		private MemberDeclarationSyntax ParseTypeDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers)
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			SyntaxKind kind = base.CurrentToken.Kind;
			switch (kind)
			{
			case SyntaxKind.ClassKeyword:
				this.CheckForVersionSpecificModifiers(modifiers, SyntaxKind.StaticKeyword, MessageID.IDS_FeatureStaticClasses);
				return this.ParseClassOrStructOrInterfaceDeclaration(attributes, modifiers);
			case SyntaxKind.StructKeyword:
				this.CheckForVersionSpecificModifiers(modifiers, SyntaxKind.ReadOnlyKeyword, MessageID.IDS_FeatureReadOnlyStructs);
				return this.ParseClassOrStructOrInterfaceDeclaration(attributes, modifiers);
			case SyntaxKind.InterfaceKeyword:
				return this.ParseClassOrStructOrInterfaceDeclaration(attributes, modifiers);
			case SyntaxKind.EnumKeyword:
				return this.ParseEnumDeclaration(attributes, modifiers);
			case SyntaxKind.DelegateKeyword:
				return this.ParseDelegateDeclaration(attributes, modifiers);
			default:
				if (kind != SyntaxKind.IdentifierToken)
				{
					throw ExceptionUtilities.UnexpectedValue(base.CurrentToken.Kind);
				}
				return this.ParseClassOrStructOrInterfaceDeclaration(attributes, modifiers);
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x00120DC0 File Offset: 0x0011EFC0
		private void CheckForVersionSpecificModifiers(SyntaxListBuilder modifiers, SyntaxKind kind, MessageID feature)
		{
			int i = 0;
			int count = modifiers.Count;
			while (i < count)
			{
				if (modifiers[i].RawKind == (int)kind)
				{
					modifiers[i] = base.CheckFeatureAvailability<GreenNode>(modifiers[i], feature, false);
				}
				i++;
			}
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x00120E08 File Offset: 0x0011F008
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private TypeDeclarationSyntax ParseClassOrStructOrInterfaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers)
		{
			SyntaxToken syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
			LanguageParser.TerminatorState termState = this._termState;
			SyntaxToken syntaxToken2 = null;
			if (syntaxToken.Kind == SyntaxKind.RecordKeyword)
			{
				this._termState |= LanguageParser.TerminatorState.IsEndOfRecordSignature;
				syntaxToken2 = this.<ParseClassOrStructOrInterfaceDeclaration>g__eatRecordModifierIfAvailable|60_0();
			}
			LanguageParser.TerminatorState termState2 = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsPossibleAggregateClauseStartOrStop;
			SyntaxToken syntaxToken3 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			TypeParameterListSyntax typeParameterListSyntax = this.ParseTypeParameterList();
			ParameterListSyntax parameterListSyntax = ((syntaxToken.Kind == SyntaxKind.RecordKeyword && base.CurrentToken.Kind == SyntaxKind.OpenParenToken) ? this.ParseParenthesizedParameterList() : null);
			BaseListSyntax baseListSyntax = this.ParseBaseList();
			this._termState = termState2;
			bool flag = true;
			SyntaxListBuilder<MemberDeclarationSyntax> syntaxListBuilder = default(SyntaxListBuilder<MemberDeclarationSyntax>);
			SyntaxListBuilder<TypeParameterConstraintClauseSyntax> syntaxListBuilder2 = default(SyntaxListBuilder<TypeParameterConstraintClauseSyntax>);
			TypeDeclarationSyntax typeDeclarationSyntax;
			try
			{
				if (base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword)
				{
					syntaxListBuilder2 = this._pool.Allocate<TypeParameterConstraintClauseSyntax>();
					this.ParseTypeParameterConstraintClauses(syntaxListBuilder2);
				}
				this._termState = termState;
				SyntaxToken syntaxToken4;
				SyntaxToken syntaxToken5;
				SyntaxToken syntaxToken6;
				if (syntaxToken.Kind != SyntaxKind.RecordKeyword || base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
				{
					syntaxToken4 = base.EatToken(SyntaxKind.OpenBraceToken);
					if (syntaxToken3.IsMissing || syntaxToken4.IsMissing)
					{
						flag = false;
					}
					if (flag)
					{
						syntaxListBuilder = this._pool.Allocate<MemberDeclarationSyntax>();
						for (;;)
						{
							SyntaxKind kind = base.CurrentToken.Kind;
							if (LanguageParser.CanStartMember(kind))
							{
								LanguageParser.TerminatorState termState3 = this._termState;
								this._termState |= LanguageParser.TerminatorState.IsPossibleMemberStartOrStop;
								MemberDeclarationSyntax memberDeclarationSyntax = this.ParseMemberDeclaration(syntaxToken.Kind);
								if (memberDeclarationSyntax != null)
								{
									syntaxListBuilder.Add(memberDeclarationSyntax);
								}
								else
								{
									this.SkipBadMemberListTokens(ref syntaxToken4, syntaxListBuilder);
								}
								this._termState = termState3;
							}
							else
							{
								if (kind == SyntaxKind.CloseBraceToken || kind == SyntaxKind.EndOfFileToken || this.IsTerminator())
								{
									break;
								}
								this.SkipBadMemberListTokens(ref syntaxToken4, syntaxListBuilder);
							}
						}
					}
					if (syntaxToken4.IsMissing)
					{
						syntaxToken5 = SyntaxFactory.MissingToken(SyntaxKind.CloseBraceToken);
						syntaxToken5 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken5, new DiagnosticInfo[] { this.GetExpectedTokenError(SyntaxKind.CloseBraceToken, base.CurrentToken.Kind) });
					}
					else
					{
						syntaxToken5 = base.EatToken(SyntaxKind.CloseBraceToken);
					}
					syntaxToken6 = base.TryEatToken(SyntaxKind.SemicolonToken);
				}
				else
				{
					syntaxToken6 = base.CheckFeatureAvailability<SyntaxToken>(base.EatToken(SyntaxKind.SemicolonToken), MessageID.IDS_FeatureRecords, false);
					syntaxToken4 = null;
					syntaxToken5 = null;
				}
				typeDeclarationSyntax = LanguageParser.<ParseClassOrStructOrInterfaceDeclaration>g__constructTypeDeclaration|60_1(this._syntaxFactory, attributes, modifiers, syntaxToken, syntaxToken2, syntaxToken3, typeParameterListSyntax, parameterListSyntax, baseListSyntax, syntaxListBuilder2, syntaxToken4, syntaxListBuilder, syntaxToken5, syntaxToken6);
			}
			finally
			{
				if (!syntaxListBuilder.IsNull)
				{
					this._pool.Free(syntaxListBuilder);
				}
				if (!syntaxListBuilder2.IsNull)
				{
					this._pool.Free(syntaxListBuilder2);
				}
			}
			return typeDeclarationSyntax;
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x001210D8 File Offset: 0x0011F2D8
		private void SkipBadMemberListTokens(ref SyntaxToken openBrace, SyntaxListBuilder members)
		{
			if (members.Count > 0)
			{
				GreenNode greenNode = members[members.Count - 1];
				this.SkipBadMemberListTokens(ref greenNode);
				members[members.Count - 1] = greenNode;
				return;
			}
			GreenNode greenNode2 = openBrace;
			this.SkipBadMemberListTokens(ref greenNode2);
			openBrace = (SyntaxToken)greenNode2;
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x00121128 File Offset: 0x0011F328
		private void SkipBadMemberListTokens(ref GreenNode previousNode)
		{
			int num = 0;
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			try
			{
				bool flag = false;
				SyntaxToken syntaxToken = base.EatToken();
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_InvalidMemberDecl, new object[] { syntaxToken.Text });
				syntaxListBuilder.Add(syntaxToken);
				while (!flag)
				{
					SyntaxKind kind = base.CurrentToken.Kind;
					if (LanguageParser.CanStartMember(kind) && (kind != SyntaxKind.DelegateKeyword || (base.PeekToken(1).Kind != SyntaxKind.OpenBraceToken && base.PeekToken(1).Kind != SyntaxKind.OpenParenToken)))
					{
						flag = true;
					}
					else
					{
						if (kind != SyntaxKind.OpenBraceToken)
						{
							if (kind != SyntaxKind.CloseBraceToken)
							{
								if (kind == SyntaxKind.EndOfFileToken)
								{
									flag = true;
									continue;
								}
							}
							else if (num-- == 0)
							{
								flag = true;
								continue;
							}
						}
						else
						{
							num++;
						}
						syntaxListBuilder.Add(base.EatToken());
					}
				}
				previousNode = base.AddTrailingSkippedSyntax<CSharpSyntaxNode>((CSharpSyntaxNode)previousNode, syntaxListBuilder.ToListNode());
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x00121238 File Offset: 0x0011F438
		private bool IsPossibleMemberStartOrStop()
		{
			return this.IsPossibleMemberStart() || base.CurrentToken.Kind == SyntaxKind.CloseBraceToken;
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x00121256 File Offset: 0x0011F456
		private bool IsPossibleAggregateClauseStartOrStop()
		{
			return base.CurrentToken.Kind == SyntaxKind.ColonToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || this.IsCurrentTokenWhereOfConstraintClause();
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x00121284 File Offset: 0x0011F484
		private BaseListSyntax ParseBaseList()
		{
			if (base.CurrentToken.Kind != SyntaxKind.ColonToken)
			{
				return null;
			}
			SyntaxToken syntaxToken = base.EatToken();
			SeparatedSyntaxListBuilder<BaseTypeSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<BaseTypeSyntax>();
			BaseListSyntax baseListSyntax;
			try
			{
				TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
				ArgumentListSyntax argumentListSyntax = null;
				if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
				{
					argumentListSyntax = this.ParseParenthesizedArgumentList();
				}
				separatedSyntaxListBuilder.Add((argumentListSyntax != null) ? this._syntaxFactory.PrimaryConstructorBaseType(typeSyntax, argumentListSyntax) : this._syntaxFactory.SimpleBaseType(typeSyntax));
				while (base.CurrentToken.Kind != SyntaxKind.OpenBraceToken && ((this._termState & LanguageParser.TerminatorState.IsEndOfRecordSignature) == LanguageParser.TerminatorState.EndOfFile || base.CurrentToken.Kind != SyntaxKind.SemicolonToken) && !this.IsCurrentTokenWhereOfConstraintClause())
				{
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleType())
					{
						separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						separatedSyntaxListBuilder.Add(this._syntaxFactory.SimpleBaseType(this.ParseType(LanguageParser.ParseTypeMode.Normal)));
					}
					else if (this.SkipBadBaseListTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
					{
						break;
					}
				}
				baseListSyntax = this._syntaxFactory.BaseList(syntaxToken, in separatedSyntaxListBuilder);
			}
			finally
			{
				this._pool.Free<BaseTypeSyntax>(in separatedSyntaxListBuilder);
			}
			return baseListSyntax;
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x001213D4 File Offset: 0x0011F5D4
		private LanguageParser.PostSkipAction SkipBadBaseListTokens(ref SyntaxToken colon, SeparatedSyntaxListBuilder<BaseTypeSyntax> list, SyntaxKind expected)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, BaseTypeSyntax>(ref colon, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleAttribute(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.OpenBraceToken || p.IsCurrentTokenWhereOfConstraintClause() || p.IsTerminator(), expected);
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x00121428 File Offset: 0x0011F628
		private bool IsCurrentTokenWhereOfConstraintClause()
		{
			return base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword && base.PeekToken(1).Kind == SyntaxKind.IdentifierToken && base.PeekToken(2).Kind == SyntaxKind.ColonToken;
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x00121464 File Offset: 0x0011F664
		private void ParseTypeParameterConstraintClauses(SyntaxListBuilder list)
		{
			while (base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword)
			{
				list.Add(this.ParseTypeParameterConstraintClause());
			}
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x00121488 File Offset: 0x0011F688
		private TypeParameterConstraintClauseSyntax ParseTypeParameterConstraintClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.WhereKeyword, true);
			IdentifierNameSyntax identifierNameSyntax = ((!this.IsTrueIdentifier()) ? base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_IdentifierExpected) : this.ParseIdentifierName(ErrorCode.ERR_IdentifierExpected));
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.ColonToken);
			SeparatedSyntaxListBuilder<TypeParameterConstraintSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<TypeParameterConstraintSyntax>();
			TypeParameterConstraintClauseSyntax typeParameterConstraintClauseSyntax;
			try
			{
				if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || this.IsCurrentTokenWhereOfConstraintClause())
				{
					separatedSyntaxListBuilder.Add(this._syntaxFactory.TypeConstraint(base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_TypeExpected)));
				}
				else
				{
					separatedSyntaxListBuilder.Add(this.ParseTypeParameterConstraint());
					while (base.CurrentToken.Kind != SyntaxKind.OpenBraceToken && ((this._termState & LanguageParser.TerminatorState.IsEndOfRecordSignature) == LanguageParser.TerminatorState.EndOfFile || base.CurrentToken.Kind != SyntaxKind.SemicolonToken) && base.CurrentToken.Kind != SyntaxKind.EqualsGreaterThanToken && base.CurrentToken.ContextualKind != SyntaxKind.WhereKeyword)
					{
						if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleTypeParameterConstraint())
						{
							separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
							if (this.IsCurrentTokenWhereOfConstraintClause())
							{
								separatedSyntaxListBuilder.Add(this._syntaxFactory.TypeConstraint(base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_TypeExpected)));
								break;
							}
							separatedSyntaxListBuilder.Add(this.ParseTypeParameterConstraint());
						}
						else if (this.SkipBadTypeParameterConstraintTokens(separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
						{
							break;
						}
					}
				}
				typeParameterConstraintClauseSyntax = this._syntaxFactory.TypeParameterConstraintClause(syntaxToken, identifierNameSyntax, syntaxToken2, in separatedSyntaxListBuilder);
			}
			finally
			{
				this._pool.Free<TypeParameterConstraintSyntax>(in separatedSyntaxListBuilder);
			}
			return typeParameterConstraintClauseSyntax;
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x00121658 File Offset: 0x0011F858
		private bool IsPossibleTypeParameterConstraint()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.NewKeyword)
			{
				if (kind != SyntaxKind.DefaultKeyword && kind != SyntaxKind.NewKeyword)
				{
					goto IL_0043;
				}
			}
			else if (kind - SyntaxKind.ClassKeyword > 1)
			{
				if (kind != SyntaxKind.IdentifierToken)
				{
					goto IL_0043;
				}
				return this.IsTrueIdentifier();
			}
			return true;
			IL_0043:
			return LanguageParser.IsPredefinedType(base.CurrentToken.Kind);
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x001216B8 File Offset: 0x0011F8B8
		private TypeParameterConstraintSyntax ParseTypeParameterConstraint()
		{
			SyntaxToken syntaxToken = null;
			SyntaxKind kind = base.CurrentToken.Kind;
			SyntaxKind kind2 = base.CurrentToken.Kind;
			if (kind2 <= SyntaxKind.NewKeyword)
			{
				if (kind2 == SyntaxKind.DefaultKeyword)
				{
					SyntaxToken syntaxToken2 = base.EatToken();
					return base.CheckFeatureAvailability<DefaultConstraintSyntax>(this._syntaxFactory.DefaultConstraint(syntaxToken2), MessageID.IDS_FeatureDefaultTypeParameterConstraint, false);
				}
				if (kind2 == SyntaxKind.NewKeyword)
				{
					SyntaxToken syntaxToken3 = base.EatToken();
					SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.OpenParenToken);
					SyntaxToken syntaxToken5 = base.EatToken(SyntaxKind.CloseParenToken);
					return this._syntaxFactory.ConstructorConstraint(syntaxToken3, syntaxToken4, syntaxToken5);
				}
			}
			else
			{
				if (kind2 == SyntaxKind.ClassKeyword)
				{
					SyntaxToken syntaxToken6 = base.EatToken();
					syntaxToken = base.TryEatToken(SyntaxKind.QuestionToken);
					return this._syntaxFactory.ClassOrStructConstraint(SyntaxKind.ClassConstraint, syntaxToken6, syntaxToken);
				}
				if (kind2 == SyntaxKind.StructKeyword)
				{
					SyntaxToken syntaxToken7 = base.EatToken();
					if (base.CurrentToken.Kind == SyntaxKind.QuestionToken)
					{
						syntaxToken = base.EatToken();
						syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_UnexpectedToken, new object[] { syntaxToken.Text });
					}
					return this._syntaxFactory.ClassOrStructConstraint(SyntaxKind.StructConstraint, syntaxToken7, syntaxToken);
				}
			}
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			return this._syntaxFactory.TypeConstraint(typeSyntax);
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x001217FC File Offset: 0x0011F9FC
		private LanguageParser.PostSkipAction SkipBadTypeParameterConstraintTokens(SeparatedSyntaxListBuilder<TypeParameterConstraintSyntax> list, SyntaxKind expected)
		{
			CSharpSyntaxNode csharpSyntaxNode = null;
			return this.SkipBadSeparatedListTokensWithExpectedKind<CSharpSyntaxNode, TypeParameterConstraintSyntax>(ref csharpSyntaxNode, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleTypeParameterConstraint(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.OpenBraceToken || p.IsCurrentTokenWhereOfConstraintClause() || p.IsTerminator(), expected);
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x00121853 File Offset: 0x0011FA53
		private bool IsPossibleMemberStart()
		{
			return LanguageParser.CanStartMember(base.CurrentToken.Kind);
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x00121868 File Offset: 0x0011FA68
		private static bool CanStartMember(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.OpenParenToken)
			{
				if (kind != SyntaxKind.TildeToken && kind != SyntaxKind.OpenParenToken)
				{
					return false;
				}
			}
			else if (kind != SyntaxKind.OpenBracketToken)
			{
				switch (kind)
				{
				case SyntaxKind.BoolKeyword:
				case SyntaxKind.ByteKeyword:
				case SyntaxKind.SByteKeyword:
				case SyntaxKind.ShortKeyword:
				case SyntaxKind.UShortKeyword:
				case SyntaxKind.IntKeyword:
				case SyntaxKind.UIntKeyword:
				case SyntaxKind.LongKeyword:
				case SyntaxKind.ULongKeyword:
				case SyntaxKind.DoubleKeyword:
				case SyntaxKind.FloatKeyword:
				case SyntaxKind.DecimalKeyword:
				case SyntaxKind.StringKeyword:
				case SyntaxKind.CharKeyword:
				case SyntaxKind.VoidKeyword:
				case SyntaxKind.ObjectKeyword:
				case SyntaxKind.PublicKeyword:
				case SyntaxKind.PrivateKeyword:
				case SyntaxKind.InternalKeyword:
				case SyntaxKind.ProtectedKeyword:
				case SyntaxKind.StaticKeyword:
				case SyntaxKind.ReadOnlyKeyword:
				case SyntaxKind.SealedKeyword:
				case SyntaxKind.ConstKeyword:
				case SyntaxKind.FixedKeyword:
				case SyntaxKind.VolatileKeyword:
				case SyntaxKind.NewKeyword:
				case SyntaxKind.OverrideKeyword:
				case SyntaxKind.AbstractKeyword:
				case SyntaxKind.VirtualKeyword:
				case SyntaxKind.EventKeyword:
				case SyntaxKind.ExternKeyword:
				case SyntaxKind.RefKeyword:
				case SyntaxKind.ClassKeyword:
				case SyntaxKind.StructKeyword:
				case SyntaxKind.InterfaceKeyword:
				case SyntaxKind.EnumKeyword:
				case SyntaxKind.DelegateKeyword:
				case SyntaxKind.UnsafeKeyword:
				case SyntaxKind.ExplicitKeyword:
				case SyntaxKind.ImplicitKeyword:
					break;
				case SyntaxKind.TypeOfKeyword:
				case SyntaxKind.SizeOfKeyword:
				case SyntaxKind.NullKeyword:
				case SyntaxKind.TrueKeyword:
				case SyntaxKind.FalseKeyword:
				case SyntaxKind.IfKeyword:
				case SyntaxKind.ElseKeyword:
				case SyntaxKind.WhileKeyword:
				case SyntaxKind.ForKeyword:
				case SyntaxKind.ForEachKeyword:
				case SyntaxKind.DoKeyword:
				case SyntaxKind.SwitchKeyword:
				case SyntaxKind.CaseKeyword:
				case SyntaxKind.DefaultKeyword:
				case SyntaxKind.TryKeyword:
				case SyntaxKind.CatchKeyword:
				case SyntaxKind.FinallyKeyword:
				case SyntaxKind.LockKeyword:
				case SyntaxKind.GotoKeyword:
				case SyntaxKind.BreakKeyword:
				case SyntaxKind.ContinueKeyword:
				case SyntaxKind.ReturnKeyword:
				case SyntaxKind.ThrowKeyword:
				case SyntaxKind.StackAllocKeyword:
				case SyntaxKind.OutKeyword:
				case SyntaxKind.InKeyword:
				case SyntaxKind.IsKeyword:
				case SyntaxKind.AsKeyword:
				case SyntaxKind.ParamsKeyword:
				case SyntaxKind.ArgListKeyword:
				case SyntaxKind.MakeRefKeyword:
				case SyntaxKind.RefTypeKeyword:
				case SyntaxKind.RefValueKeyword:
				case SyntaxKind.ThisKeyword:
				case SyntaxKind.BaseKeyword:
				case SyntaxKind.NamespaceKeyword:
				case SyntaxKind.UsingKeyword:
				case SyntaxKind.CheckedKeyword:
				case SyntaxKind.UncheckedKeyword:
				case SyntaxKind.OperatorKeyword:
					return false;
				default:
					if (kind != SyntaxKind.IdentifierToken)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x00121A00 File Offset: 0x0011FC00
		private bool IsTypeDeclarationStart()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind - SyntaxKind.ClassKeyword > 3)
			{
				if (kind != SyntaxKind.DelegateKeyword)
				{
					if (kind == SyntaxKind.IdentifierToken)
					{
						return base.CurrentToken.ContextualKind == SyntaxKind.RecordKeyword && base.IsFeatureEnabled(MessageID.IDS_FeatureRecords);
					}
				}
				else if (!this.IsFunctionPointerStart())
				{
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x00121A60 File Offset: 0x0011FC60
		private bool CanReuseMemberDeclaration(SyntaxKind kind, bool isGlobal)
		{
			if (kind <= SyntaxKind.DelegateDeclaration)
			{
				if (kind == SyntaxKind.GlobalStatement)
				{
					return isGlobal;
				}
				if (kind != SyntaxKind.NamespaceDeclaration && kind - SyntaxKind.ClassDeclaration > 4)
				{
					return false;
				}
			}
			else if (kind <= SyntaxKind.IndexerDeclaration)
			{
				switch (kind)
				{
				case SyntaxKind.FieldDeclaration:
				case SyntaxKind.MethodDeclaration:
					return !isGlobal || base.IsScript || base.CurrentNode.Parent is CompilationUnitSyntax;
				case SyntaxKind.EventFieldDeclaration:
				case SyntaxKind.OperatorDeclaration:
				case SyntaxKind.ConversionOperatorDeclaration:
				case SyntaxKind.ConstructorDeclaration:
					break;
				default:
					if (kind - SyntaxKind.DestructorDeclaration > 3)
					{
						return false;
					}
					break;
				}
			}
			else if (kind != SyntaxKind.RecordDeclaration && kind != SyntaxKind.RecordStructDeclaration)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x00121AFF File Offset: 0x0011FCFF
		public MemberDeclarationSyntax ParseMemberDeclaration()
		{
			return this.ParseWithStackGuard<MemberDeclarationSyntax>(() => this.ParseMemberDeclaration(SyntaxKind.StructDeclaration), () => this.<ParseMemberDeclaration>g__createEmptyNodeFunc|77_2());
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x00121B1F File Offset: 0x0011FD1F
		internal MemberDeclarationSyntax ParseMemberDeclarationOrStatement(SyntaxKind parentKind)
		{
			this._recursionDepth++;
			StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
			MemberDeclarationSyntax memberDeclarationSyntax = this.ParseMemberDeclarationOrStatementCore(parentKind);
			this._recursionDepth--;
			return memberDeclarationSyntax;
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x00121B50 File Offset: 0x0011FD50
		private MemberDeclarationSyntax ParseMemberDeclarationOrStatementCore(SyntaxKind parentKind)
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			if (this.IsIncrementalAndFactoryContextMatches && this.CanReuseMemberDeclaration(base.CurrentNodeKind, true))
			{
				return (MemberDeclarationSyntax)base.EatNode();
			}
			LanguageParser.TerminatorState termState = this._termState;
			SyntaxList<AttributeListSyntax> syntaxList = this.ParseAttributeDeclarations();
			bool flag = syntaxList.Count > 0;
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			MemberDeclarationSyntax memberDeclarationSyntax2;
			try
			{
				if (!flag || !base.IsScript)
				{
					bool isInAsync = this.IsInAsync;
					if (!base.IsScript)
					{
						this.IsInAsync = true;
					}
					try
					{
						SyntaxKind kind = base.CurrentToken.Kind;
						if (kind <= SyntaxKind.NewKeyword)
						{
							if (kind != SyntaxKind.FixedKeyword)
							{
								if (kind == SyntaxKind.NewKeyword)
								{
									if (this.IsPossibleNewExpression())
									{
										return this.CheckTopLevelStatementsFeatureAvailability(this._syntaxFactory.GlobalStatement(this.ParseExpressionStatement(syntaxList)));
									}
								}
							}
							else if (base.PeekToken(1).Kind == SyntaxKind.OpenParenToken)
							{
								return this.CheckTopLevelStatementsFeatureAvailability(this._syntaxFactory.GlobalStatement(this.ParseFixedStatement(syntaxList)));
							}
						}
						else if (kind != SyntaxKind.DelegateKeyword)
						{
							if (kind == SyntaxKind.UnsafeKeyword)
							{
								if (base.PeekToken(1).Kind == SyntaxKind.OpenBraceToken)
								{
									return this.CheckTopLevelStatementsFeatureAvailability(this._syntaxFactory.GlobalStatement(this.ParseUnsafeStatement(syntaxList)));
								}
							}
						}
						else
						{
							SyntaxKind kind2 = base.PeekToken(1).Kind;
							if (kind2 == SyntaxKind.OpenParenToken || kind2 == SyntaxKind.OpenBraceToken)
							{
								return this.CheckTopLevelStatementsFeatureAvailability(this._syntaxFactory.GlobalStatement(this.ParseExpressionStatement(syntaxList)));
							}
						}
					}
					finally
					{
						this.IsInAsync = isInAsync;
					}
				}
				this.ParseModifiers(syntaxListBuilder, false);
				bool flag2 = syntaxListBuilder.Count > 0;
				if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken && base.PeekToken(1).Kind == SyntaxKind.OpenParenToken && (flag || flag2))
				{
					SyntaxToken syntaxToken = SyntaxFactory.MissingToken(SyntaxKind.VoidKeyword);
					syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_MemberNeedsType);
					PredefinedTypeSyntax predefinedTypeSyntax = this._syntaxFactory.PredefinedType(syntaxToken);
					if (base.IsScript)
					{
						SyntaxToken syntaxToken2 = base.EatToken();
						return this.ParseMethodDeclaration(syntaxList, syntaxListBuilder, predefinedTypeSyntax, null, syntaxToken2, null);
					}
					MemberDeclarationSyntax memberDeclarationSyntax;
					if (this.<ParseMemberDeclarationOrStatementCore>g__tryParseLocalDeclarationStatementFromStartPoint|79_1<LocalFunctionStatementSyntax>(syntaxList, ref resetPoint, out memberDeclarationSyntax))
					{
						return memberDeclarationSyntax;
					}
				}
				if (base.CurrentToken.Kind == SyntaxKind.ConstKeyword)
				{
					MemberDeclarationSyntax memberDeclarationSyntax;
					if (!base.IsScript && this.<ParseMemberDeclarationOrStatementCore>g__tryParseLocalDeclarationStatementFromStartPoint|79_1<LocalDeclarationStatementSyntax>(syntaxList, ref resetPoint, out memberDeclarationSyntax))
					{
						memberDeclarationSyntax2 = memberDeclarationSyntax;
					}
					else
					{
						memberDeclarationSyntax2 = this.ParseConstantFieldDeclaration(syntaxList, syntaxListBuilder, parentKind);
					}
				}
				else if (base.CurrentToken.Kind == SyntaxKind.EventKeyword)
				{
					memberDeclarationSyntax2 = this.ParseEventDeclaration(syntaxList, syntaxListBuilder, parentKind);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.FixedKeyword)
				{
					memberDeclarationSyntax2 = this.ParseFixedSizeBufferDeclaration(syntaxList, syntaxListBuilder, parentKind);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.ExplicitKeyword || base.CurrentToken.Kind == SyntaxKind.ImplicitKeyword || (base.CurrentToken.Kind == SyntaxKind.OperatorKeyword && !SyntaxFacts.IsAnyOverloadableOperator(base.PeekToken(1).Kind)))
				{
					memberDeclarationSyntax2 = this.ParseConversionOperatorDeclaration(syntaxList, syntaxListBuilder);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.NamespaceKeyword)
				{
					memberDeclarationSyntax2 = this.ParseNamespaceDeclaration(syntaxList, syntaxListBuilder);
				}
				else if (this.IsTypeDeclarationStart())
				{
					memberDeclarationSyntax2 = this.ParseTypeDeclaration(syntaxList, syntaxListBuilder);
				}
				else
				{
					TypeSyntax typeSyntax = this.ParseReturnType();
					LanguageParser.ResetPoint resetPoint2 = this.GetResetPoint();
					try
					{
						if ((!flag || !base.IsScript) && !flag2 && (typeSyntax.Kind == SyntaxKind.RefType || !this.IsOperatorKeyword()))
						{
							this.Reset(ref resetPoint);
							if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken && base.CurrentToken.Kind != SyntaxKind.EndOfFileToken && this.IsPossibleStatement(true))
							{
								LanguageParser.TerminatorState termState2 = this._termState;
								this._termState |= LanguageParser.TerminatorState.IsPossibleStatementStartOrStop;
								bool isInAsync2 = this.IsInAsync;
								if (!base.IsScript)
								{
									this.IsInAsync = true;
								}
								StatementSyntax statementSyntax = this.ParseStatementCore(syntaxList, true);
								this.IsInAsync = isInAsync2;
								this._termState = termState2;
								if (LanguageParser.<ParseMemberDeclarationOrStatementCore>g__isAcceptableNonDeclarationStatement|79_2(statementSyntax, base.IsScript))
								{
									return this.CheckTopLevelStatementsFeatureAvailability(this._syntaxFactory.GlobalStatement(statementSyntax));
								}
							}
							this.Reset(ref resetPoint2);
						}
						MemberDeclarationSyntax memberDeclarationSyntax;
						if (this.IsMisplacedModifier(syntaxListBuilder, syntaxList, typeSyntax, out memberDeclarationSyntax))
						{
							memberDeclarationSyntax2 = memberDeclarationSyntax;
						}
						else
						{
							ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax;
							SyntaxToken syntaxToken3;
							TypeParameterListSyntax typeParameterListSyntax;
							for (;;)
							{
								bool isRef = typeSyntax.IsRef;
								if (!isRef && this.IsOperatorKeyword())
								{
									break;
								}
								if ((!isRef || !base.IsScript) && this.IsFieldDeclaration(false))
								{
									LanguageParser.TerminatorState termState3 = this._termState;
									if ((!flag && !flag2) || !base.IsScript)
									{
										this._termState |= LanguageParser.TerminatorState.IsPossibleStatementStartOrStop;
										if (!base.IsScript)
										{
											this.Reset(ref resetPoint);
											if (this.<ParseMemberDeclarationOrStatementCore>g__tryParseLocalDeclarationStatement|79_0<LocalDeclarationStatementSyntax>(syntaxList, out memberDeclarationSyntax))
											{
												goto Block_52;
											}
											this.Reset(ref resetPoint2);
										}
									}
									if (!isRef)
									{
										goto Block_53;
									}
									this._termState = termState3;
								}
								this.ParseMemberName(out explicitInterfaceSpecifierSyntax, out syntaxToken3, out typeParameterListSyntax, false);
								if (this.IsNoneOrIncompleteMember(parentKind, syntaxList, syntaxListBuilder, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken3, typeParameterListSyntax, out memberDeclarationSyntax))
								{
									goto Block_54;
								}
								if (!this.ReconsideredTypeAsAsyncModifier(ref syntaxListBuilder, ref typeSyntax, ref resetPoint2, ref explicitInterfaceSpecifierSyntax, ref syntaxToken3, ref typeParameterListSyntax))
								{
									goto Block_55;
								}
							}
							return this.ParseOperatorDeclaration(syntaxList, syntaxListBuilder, typeSyntax);
							Block_52:
							return memberDeclarationSyntax;
							Block_53:
							return this.ParseNormalFieldDeclaration(syntaxList, syntaxListBuilder, typeSyntax, parentKind);
							Block_54:
							return memberDeclarationSyntax;
							Block_55:
							this.CheckForVersionSpecificModifiers(syntaxListBuilder, SyntaxKind.ReadOnlyKeyword, MessageID.IDS_FeatureReadOnlyMembers);
							if (this.TryParseIndexerOrPropertyDeclaration(syntaxList, syntaxListBuilder, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken3, typeParameterListSyntax, out memberDeclarationSyntax))
							{
								memberDeclarationSyntax2 = memberDeclarationSyntax;
							}
							else if (!base.IsScript && explicitInterfaceSpecifierSyntax == null && this.<ParseMemberDeclarationOrStatementCore>g__tryParseLocalDeclarationStatementFromStartPoint|79_1<LocalFunctionStatementSyntax>(syntaxList, ref resetPoint, out memberDeclarationSyntax))
							{
								memberDeclarationSyntax2 = memberDeclarationSyntax;
							}
							else
							{
								memberDeclarationSyntax2 = this.ParseMethodDeclaration(syntaxList, syntaxListBuilder, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken3, typeParameterListSyntax);
							}
						}
					}
					finally
					{
						this.Release(ref resetPoint2);
					}
				}
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
				this._termState = termState;
				this.Release(ref resetPoint);
			}
			return memberDeclarationSyntax2;
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x00122198 File Offset: 0x00120398
		private bool IsMisplacedModifier(SyntaxListBuilder modifiers, SyntaxList<AttributeListSyntax> attributes, TypeSyntax type, out MemberDeclarationSyntax result)
		{
			if (LanguageParser.GetModifier(base.CurrentToken) != DeclarationModifiers.None && base.CurrentToken.ContextualKind != SyntaxKind.PartialKeyword && base.CurrentToken.ContextualKind != SyntaxKind.AsyncKeyword && base.CurrentToken.ContextualKind != SyntaxKind.DataKeyword && LanguageParser.IsComplete(type))
			{
				SyntaxToken currentToken = base.CurrentToken;
				type = base.AddError<TypeSyntax>(type, type.FullWidth + currentToken.GetLeadingTriviaWidth(), currentToken.Width, ErrorCode.ERR_BadModifierLocation, new object[] { currentToken.Text });
				result = this._syntaxFactory.IncompleteMember(attributes, modifiers.ToList(), type);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x00122250 File Offset: 0x00120450
		private bool IsNoneOrIncompleteMember(SyntaxKind parentKind, SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceOpt, SyntaxToken identifierOrThisOpt, TypeParameterListSyntax typeParameterListOpt, out MemberDeclarationSyntax result)
		{
			if (explicitInterfaceOpt != null || identifierOrThisOpt != null || typeParameterListOpt != null)
			{
				result = null;
				return false;
			}
			if (attributes.Count == 0 && modifiers.Count == 0 && type.IsMissing && type.Kind != SyntaxKind.RefType)
			{
				result = null;
				return true;
			}
			IncompleteMemberSyntax incompleteMemberSyntax = this._syntaxFactory.IncompleteMember(attributes, modifiers.ToList(), type.IsMissing ? null : type);
			if (incompleteMemberSyntax.ContainsDiagnostics)
			{
				result = incompleteMemberSyntax;
			}
			else if (parentKind == SyntaxKind.NamespaceDeclaration || (parentKind == SyntaxKind.CompilationUnit && !base.IsScript))
			{
				result = base.AddErrorToLastToken<IncompleteMemberSyntax>(incompleteMemberSyntax, ErrorCode.ERR_NamespaceUnexpected);
			}
			else
			{
				result = base.AddError<IncompleteMemberSyntax>(incompleteMemberSyntax, incompleteMemberSyntax.FullWidth + base.CurrentToken.GetLeadingTriviaWidth(), base.CurrentToken.Width, ErrorCode.ERR_InvalidMemberDecl, new object[] { base.CurrentToken.Text });
			}
			return true;
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x00122344 File Offset: 0x00120544
		private bool ReconsideredTypeAsAsyncModifier(ref SyntaxListBuilder modifiers, ref TypeSyntax type, ref LanguageParser.ResetPoint afterTypeResetPoint, ref ExplicitInterfaceSpecifierSyntax explicitInterfaceOpt, ref SyntaxToken identifierOrThisOpt, ref TypeParameterListSyntax typeParameterListOpt)
		{
			if (type.Kind != SyntaxKind.RefType && identifierOrThisOpt != null && ((typeParameterListOpt != null && typeParameterListOpt.ContainsDiagnostics) || (base.CurrentToken.Kind != SyntaxKind.OpenParenToken && base.CurrentToken.Kind != SyntaxKind.OpenBraceToken && base.CurrentToken.Kind != SyntaxKind.EqualsGreaterThanToken)) && this.ReconsiderTypeAsAsyncModifier(ref modifiers, type, identifierOrThisOpt))
			{
				this.Reset(ref afterTypeResetPoint);
				explicitInterfaceOpt = null;
				identifierOrThisOpt = null;
				typeParameterListOpt = null;
				this.Release(ref afterTypeResetPoint);
				type = this.ParseReturnType();
				afterTypeResetPoint = this.GetResetPoint();
				return true;
			}
			return false;
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x001223F0 File Offset: 0x001205F0
		private bool TryParseIndexerOrPropertyDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceOpt, SyntaxToken identifierOrThisOpt, TypeParameterListSyntax typeParameterListOpt, out MemberDeclarationSyntax result)
		{
			if (identifierOrThisOpt.Kind == SyntaxKind.ThisKeyword)
			{
				result = this.ParseIndexerDeclaration(attributes, modifiers, type, explicitInterfaceOpt, identifierOrThisOpt, typeParameterListOpt);
				return true;
			}
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind == SyntaxKind.OpenBraceToken || kind == SyntaxKind.EqualsGreaterThanToken)
			{
				result = this.ParsePropertyDeclaration(attributes, modifiers, type, explicitInterfaceOpt, identifierOrThisOpt, typeParameterListOpt);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x00122454 File Offset: 0x00120654
		internal MemberDeclarationSyntax ParseMemberDeclaration(SyntaxKind parentKind)
		{
			this._recursionDepth++;
			StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
			MemberDeclarationSyntax memberDeclarationSyntax = this.ParseMemberDeclarationCore(parentKind);
			this._recursionDepth--;
			return memberDeclarationSyntax;
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x00122484 File Offset: 0x00120684
		private MemberDeclarationSyntax ParseMemberDeclarationCore(SyntaxKind parentKind)
		{
			this.cancellationToken.ThrowIfCancellationRequested();
			if (this.IsIncrementalAndFactoryContextMatches && this.CanReuseMemberDeclaration(base.CurrentNodeKind, false))
			{
				return (MemberDeclarationSyntax)base.EatNode();
			}
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			LanguageParser.TerminatorState termState = this._termState;
			MemberDeclarationSyntax memberDeclarationSyntax;
			try
			{
				SyntaxList<AttributeListSyntax> syntaxList = this.ParseAttributeDeclarations();
				this.ParseModifiers(syntaxListBuilder, false);
				if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken && base.PeekToken(1).Kind == SyntaxKind.OpenParenToken)
				{
					memberDeclarationSyntax = this.ParseConstructorDeclaration(syntaxList, syntaxListBuilder);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.TildeToken)
				{
					memberDeclarationSyntax = this.ParseDestructorDeclaration(syntaxList, syntaxListBuilder);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.ConstKeyword)
				{
					memberDeclarationSyntax = this.ParseConstantFieldDeclaration(syntaxList, syntaxListBuilder, parentKind);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.EventKeyword)
				{
					memberDeclarationSyntax = this.ParseEventDeclaration(syntaxList, syntaxListBuilder, parentKind);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.FixedKeyword)
				{
					memberDeclarationSyntax = this.ParseFixedSizeBufferDeclaration(syntaxList, syntaxListBuilder, parentKind);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.ExplicitKeyword || base.CurrentToken.Kind == SyntaxKind.ImplicitKeyword || (base.CurrentToken.Kind == SyntaxKind.OperatorKeyword && !SyntaxFacts.IsAnyOverloadableOperator(base.PeekToken(1).Kind)))
				{
					memberDeclarationSyntax = this.ParseConversionOperatorDeclaration(syntaxList, syntaxListBuilder);
				}
				else if (this.IsTypeDeclarationStart())
				{
					memberDeclarationSyntax = this.ParseTypeDeclaration(syntaxList, syntaxListBuilder);
				}
				else
				{
					TypeSyntax typeSyntax = this.ParseReturnType();
					LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
					try
					{
						MemberDeclarationSyntax memberDeclarationSyntax2;
						if (this.IsMisplacedModifier(syntaxListBuilder, syntaxList, typeSyntax, out memberDeclarationSyntax2))
						{
							memberDeclarationSyntax = memberDeclarationSyntax2;
						}
						else
						{
							ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax;
							SyntaxToken syntaxToken;
							TypeParameterListSyntax typeParameterListSyntax;
							for (;;)
							{
								if (typeSyntax.Kind != SyntaxKind.RefType)
								{
									if (this.IsOperatorKeyword())
									{
										break;
									}
									if (this.IsFieldDeclaration(false))
									{
										goto Block_20;
									}
								}
								this.ParseMemberName(out explicitInterfaceSpecifierSyntax, out syntaxToken, out typeParameterListSyntax, false);
								if (this.IsNoneOrIncompleteMember(parentKind, syntaxList, syntaxListBuilder, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken, typeParameterListSyntax, out memberDeclarationSyntax2))
								{
									goto Block_21;
								}
								if (!this.ReconsideredTypeAsAsyncModifier(ref syntaxListBuilder, ref typeSyntax, ref resetPoint, ref explicitInterfaceSpecifierSyntax, ref syntaxToken, ref typeParameterListSyntax))
								{
									goto Block_22;
								}
							}
							return this.ParseOperatorDeclaration(syntaxList, syntaxListBuilder, typeSyntax);
							Block_20:
							return this.ParseNormalFieldDeclaration(syntaxList, syntaxListBuilder, typeSyntax, parentKind);
							Block_21:
							return memberDeclarationSyntax2;
							Block_22:
							this.CheckForVersionSpecificModifiers(syntaxListBuilder, SyntaxKind.ReadOnlyKeyword, MessageID.IDS_FeatureReadOnlyMembers);
							if (this.TryParseIndexerOrPropertyDeclaration(syntaxList, syntaxListBuilder, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken, typeParameterListSyntax, out memberDeclarationSyntax2))
							{
								memberDeclarationSyntax = memberDeclarationSyntax2;
							}
							else
							{
								memberDeclarationSyntax = this.ParseMethodDeclaration(syntaxList, syntaxListBuilder, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken, typeParameterListSyntax);
							}
						}
					}
					finally
					{
						this.Release(ref resetPoint);
					}
				}
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
				this._termState = termState;
			}
			return memberDeclarationSyntax;
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x00122748 File Offset: 0x00120948
		private bool ReconsiderTypeAsAsyncModifier(ref SyntaxListBuilder modifiers, TypeSyntax type, SyntaxToken identifierOrThisOpt)
		{
			if (type.Kind != SyntaxKind.IdentifierName)
			{
				return false;
			}
			if (identifierOrThisOpt.Kind != SyntaxKind.IdentifierToken)
			{
				return false;
			}
			SyntaxToken identifier = ((IdentifierNameSyntax)type).Identifier;
			SyntaxKind contextualKind = identifier.ContextualKind;
			if (contextualKind != SyntaxKind.AsyncKeyword || modifiers.Any((int)contextualKind))
			{
				return false;
			}
			modifiers.Add(SyntaxParser.ConvertToKeyword(identifier));
			return true;
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x001227A8 File Offset: 0x001209A8
		private bool IsFieldDeclaration(bool isEvent)
		{
			if (base.CurrentToken.Kind != SyntaxKind.IdentifierToken)
			{
				return false;
			}
			if (base.CurrentToken.ContextualKind == SyntaxKind.GlobalKeyword && base.PeekToken(1).Kind == SyntaxKind.UsingKeyword)
			{
				return false;
			}
			SyntaxKind kind = base.PeekToken(1).Kind;
			if (kind <= SyntaxKind.LessThanToken)
			{
				if (kind == SyntaxKind.OpenParenToken)
				{
					return isEvent;
				}
				if (kind != SyntaxKind.OpenBraceToken && kind != SyntaxKind.LessThanToken)
				{
					return true;
				}
			}
			else if (kind != SyntaxKind.DotToken && kind != SyntaxKind.ColonColonToken && kind != SyntaxKind.EqualsGreaterThanToken)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x0012283C File Offset: 0x00120A3C
		private bool IsOperatorKeyword()
		{
			return base.CurrentToken.Kind == SyntaxKind.ImplicitKeyword || base.CurrentToken.Kind == SyntaxKind.ExplicitKeyword || base.CurrentToken.Kind == SyntaxKind.OperatorKeyword;
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x00122878 File Offset: 0x00120A78
		public static bool IsComplete(CSharpSyntaxNode node)
		{
			if (node == null)
			{
				return false;
			}
			foreach (GreenNode greenNode in node.ChildNodesAndTokens().Reverse())
			{
				SyntaxToken syntaxToken = greenNode as SyntaxToken;
				if (syntaxToken == null)
				{
					return LanguageParser.IsComplete((CSharpSyntaxNode)greenNode);
				}
				if (syntaxToken.IsMissing)
				{
					return false;
				}
				if (syntaxToken.Kind != SyntaxKind.None)
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x001228E4 File Offset: 0x00120AE4
		private ConstructorDeclarationSyntax ParseConstructorDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers)
		{
			SyntaxToken syntaxToken = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfMethodSignature;
			ConstructorDeclarationSyntax constructorDeclarationSyntax;
			try
			{
				ParameterListSyntax parameterListSyntax = this.ParseParenthesizedParameterList();
				ConstructorInitializerSyntax constructorInitializerSyntax = ((base.CurrentToken.Kind == SyntaxKind.ColonToken) ? this.ParseConstructorInitializer() : null);
				BlockSyntax blockSyntax;
				ArrowExpressionClauseSyntax arrowExpressionClauseSyntax;
				SyntaxToken syntaxToken2;
				this.ParseBlockAndExpressionBodiesWithSemicolon(out blockSyntax, out arrowExpressionClauseSyntax, out syntaxToken2, true, MessageID.IDS_FeatureExpressionBodiedDeOrConstructor);
				constructorDeclarationSyntax = this._syntaxFactory.ConstructorDeclaration(attributes, modifiers.ToList(), syntaxToken, parameterListSyntax, constructorInitializerSyntax, blockSyntax, arrowExpressionClauseSyntax, syntaxToken2);
			}
			finally
			{
				this._termState = termState;
			}
			return constructorDeclarationSyntax;
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x0012298C File Offset: 0x00120B8C
		private ConstructorInitializerSyntax ParseConstructorInitializer()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ColonToken);
			bool flag = true;
			SyntaxKind syntaxKind = ((base.CurrentToken.Kind == SyntaxKind.BaseKeyword) ? SyntaxKind.BaseConstructorInitializer : SyntaxKind.ThisConstructorInitializer);
			SyntaxToken syntaxToken2;
			if (base.CurrentToken.Kind == SyntaxKind.BaseKeyword || base.CurrentToken.Kind == SyntaxKind.ThisKeyword)
			{
				syntaxToken2 = base.EatToken();
			}
			else
			{
				syntaxToken2 = base.EatToken(SyntaxKind.ThisKeyword, ErrorCode.ERR_ThisOrBaseExpected, true);
				flag = false;
			}
			ArgumentListSyntax argumentListSyntax;
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				argumentListSyntax = this.ParseParenthesizedArgumentList();
			}
			else
			{
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.OpenParenToken, flag);
				SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseParenToken, flag);
				argumentListSyntax = this._syntaxFactory.ArgumentList(syntaxToken3, default(SeparatedSyntaxList<ArgumentSyntax>), syntaxToken4);
			}
			return this._syntaxFactory.ConstructorInitializer(syntaxKind, syntaxToken, syntaxToken2, argumentListSyntax);
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x00122A6C File Offset: 0x00120C6C
		private DestructorDeclarationSyntax ParseDestructorDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.TildeToken);
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.OpenParenToken);
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseParenToken);
			BlockSyntax blockSyntax;
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax;
			SyntaxToken syntaxToken5;
			this.ParseBlockAndExpressionBodiesWithSemicolon(out blockSyntax, out arrowExpressionClauseSyntax, out syntaxToken5, true, MessageID.IDS_FeatureExpressionBodiedDeOrConstructor);
			ParameterListSyntax parameterListSyntax = this._syntaxFactory.ParameterList(syntaxToken3, default(SeparatedSyntaxList<ParameterSyntax>), syntaxToken4);
			return this._syntaxFactory.DestructorDeclaration(attributes, modifiers.ToList(), syntaxToken, syntaxToken2, parameterListSyntax, blockSyntax, arrowExpressionClauseSyntax, syntaxToken5);
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x00122AF8 File Offset: 0x00120CF8
		private void ParseBlockAndExpressionBodiesWithSemicolon(out BlockSyntax blockBody, out ArrowExpressionClauseSyntax expressionBody, out SyntaxToken semicolon, bool parseSemicolonAfterBlock = true, MessageID requestedExpressionBodyFeature = MessageID.IDS_FeatureExpressionBodiedMethod)
		{
			if (base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
			{
				blockBody = null;
				expressionBody = null;
				semicolon = base.EatToken(SyntaxKind.SemicolonToken);
				return;
			}
			blockBody = null;
			expressionBody = null;
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
			{
				blockBody = this.ParseMethodOrAccessorBodyBlock(default(SyntaxList<AttributeListSyntax>), false);
			}
			if (base.CurrentToken.Kind == SyntaxKind.EqualsGreaterThanToken)
			{
				expressionBody = this.ParseArrowExpressionClause();
				expressionBody = base.CheckFeatureAvailability<ArrowExpressionClauseSyntax>(expressionBody, requestedExpressionBodyFeature, false);
			}
			semicolon = null;
			if (expressionBody != null || blockBody == null)
			{
				semicolon = base.EatToken(SyntaxKind.SemicolonToken);
				return;
			}
			if (parseSemicolonAfterBlock && base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
			{
				semicolon = base.EatTokenWithPrejudice(ErrorCode.ERR_UnexpectedSemicolon, Array.Empty<object>());
			}
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x00122BC0 File Offset: 0x00120DC0
		private bool IsEndOfTypeParameterList()
		{
			return base.CurrentToken.Kind == SyntaxKind.OpenParenToken || base.CurrentToken.Kind == SyntaxKind.ColonToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || this.IsCurrentTokenWhereOfConstraintClause();
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x00122C14 File Offset: 0x00120E14
		private bool IsEndOfMethodSignature()
		{
			return base.CurrentToken.Kind == SyntaxKind.SemicolonToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken;
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x00122C3C File Offset: 0x00120E3C
		private bool IsEndOfRecordSignature()
		{
			return base.CurrentToken.Kind == SyntaxKind.SemicolonToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken;
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x00122C64 File Offset: 0x00120E64
		private bool IsEndOfNameInExplicitInterface()
		{
			return base.CurrentToken.Kind == SyntaxKind.DotToken || base.CurrentToken.Kind == SyntaxKind.ColonColonToken;
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x00122C8C File Offset: 0x00120E8C
		private bool IsEndOfFunctionPointerParameterList(bool errored)
		{
			return base.CurrentToken.Kind == (errored ? SyntaxKind.CloseParenToken : SyntaxKind.GreaterThanToken);
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x00122CAA File Offset: 0x00120EAA
		private bool IsEndOfFunctionPointerCallingConvention()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseBracketToken;
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x00122CC0 File Offset: 0x00120EC0
		private MethodDeclarationSyntax ParseMethodDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceOpt, SyntaxToken identifier, TypeParameterListSyntax typeParameterList)
		{
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfMethodSignature;
			ParameterListSyntax parameterListSyntax = this.ParseParenthesizedParameterList();
			SyntaxListBuilder<TypeParameterConstraintClauseSyntax> syntaxListBuilder = default(SyntaxListBuilder<TypeParameterConstraintClauseSyntax>);
			MethodDeclarationSyntax methodDeclarationSyntax;
			try
			{
				if (base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword)
				{
					syntaxListBuilder = this._pool.Allocate<TypeParameterConstraintClauseSyntax>();
					this.ParseTypeParameterConstraintClauses(syntaxListBuilder);
				}
				else if (base.CurrentToken.Kind == SyntaxKind.ColonToken)
				{
					SyntaxToken currentToken = base.CurrentToken;
					ConstructorInitializerSyntax constructorInitializerSyntax = this.ParseConstructorInitializer();
					constructorInitializerSyntax = base.AddErrorToFirstToken<ConstructorInitializerSyntax>(constructorInitializerSyntax, ErrorCode.ERR_UnexpectedToken, new object[] { currentToken.Text });
					parameterListSyntax = base.AddTrailingSkippedSyntax<ParameterListSyntax>(parameterListSyntax, constructorInitializerSyntax);
				}
				this._termState = termState;
				this.IsInAsync = modifiers.Any(8435);
				BlockSyntax blockSyntax;
				ArrowExpressionClauseSyntax arrowExpressionClauseSyntax;
				SyntaxToken syntaxToken;
				this.ParseBlockAndExpressionBodiesWithSemicolon(out blockSyntax, out arrowExpressionClauseSyntax, out syntaxToken, true, MessageID.IDS_FeatureExpressionBodiedMethod);
				this.IsInAsync = false;
				methodDeclarationSyntax = this._syntaxFactory.MethodDeclaration(attributes, modifiers.ToList(), type, explicitInterfaceOpt, identifier, typeParameterList, parameterListSyntax, syntaxListBuilder, blockSyntax, arrowExpressionClauseSyntax, syntaxToken);
			}
			finally
			{
				if (!syntaxListBuilder.IsNull)
				{
					this._pool.Free(syntaxListBuilder);
				}
			}
			return methodDeclarationSyntax;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x00122DF8 File Offset: 0x00120FF8
		private TypeSyntax ParseReturnType()
		{
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfReturnType;
			TypeSyntax typeSyntax = this.ParseTypeOrVoid();
			this._termState = termState;
			return typeSyntax;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x00122E28 File Offset: 0x00121028
		private bool IsEndOfReturnType()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			return kind == SyntaxKind.OpenParenToken || kind == SyntaxKind.OpenBraceToken || kind == SyntaxKind.SemicolonToken;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00122E5C File Offset: 0x0012105C
		private ConversionOperatorDeclarationSyntax ParseConversionOperatorDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers)
		{
			SyntaxToken syntaxToken;
			if (base.CurrentToken.Kind == SyntaxKind.ImplicitKeyword || base.CurrentToken.Kind == SyntaxKind.ExplicitKeyword)
			{
				syntaxToken = base.EatToken();
			}
			else
			{
				syntaxToken = base.EatToken(SyntaxKind.ExplicitKeyword);
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OperatorKeyword);
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			ParameterListSyntax parameterListSyntax = this.ParseParenthesizedParameterList();
			BlockSyntax blockSyntax;
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax;
			SyntaxToken syntaxToken3;
			this.ParseBlockAndExpressionBodiesWithSemicolon(out blockSyntax, out arrowExpressionClauseSyntax, out syntaxToken3, true, MessageID.IDS_FeatureExpressionBodiedMethod);
			return this._syntaxFactory.ConversionOperatorDeclaration(attributes, modifiers.ToList(), syntaxToken, syntaxToken2, typeSyntax, parameterListSyntax, blockSyntax, arrowExpressionClauseSyntax, syntaxToken3);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x00122EF0 File Offset: 0x001210F0
		private OperatorDeclarationSyntax ParseOperatorDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, TypeSyntax type)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OperatorKeyword);
			SyntaxToken syntaxToken2;
			int num;
			int num2;
			if (SyntaxFacts.IsAnyOverloadableOperator(base.CurrentToken.Kind))
			{
				syntaxToken2 = base.EatToken();
				num = syntaxToken2.GetLeadingTriviaWidth();
				num2 = syntaxToken2.Width;
			}
			else if (base.CurrentToken.Kind == SyntaxKind.ImplicitKeyword || base.CurrentToken.Kind == SyntaxKind.ExplicitKeyword)
			{
				base.GetDiagnosticSpanForMissingToken(out num, out num2);
				syntaxToken2 = this.ConvertToMissingWithTrailingTrivia(base.EatToken(), SyntaxKind.PlusToken);
				if (type.IsMissing)
				{
					SyntaxDiagnosticInfo syntaxDiagnosticInfo = SyntaxParser.MakeError(num, num2, ErrorCode.ERR_BadOperatorSyntax, new object[] { SyntaxFacts.GetText(SyntaxKind.PlusToken) });
					syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { syntaxDiagnosticInfo });
				}
				else
				{
					type = base.AddError<TypeSyntax>(type, ErrorCode.ERR_BadOperatorSyntax, new object[] { SyntaxFacts.GetText(SyntaxKind.PlusToken) });
				}
			}
			else
			{
				syntaxToken2 = base.EatToken();
				num = syntaxToken2.GetLeadingTriviaWidth();
				num2 = syntaxToken2.Width;
			}
			SyntaxKind kind = syntaxToken2.Kind;
			SyntaxToken currentToken = base.CurrentToken;
			if (syntaxToken2.Kind == SyntaxKind.GreaterThanToken && currentToken.Kind == SyntaxKind.GreaterThanToken && syntaxToken2.GetTrailingTriviaWidth() == 0 && currentToken.GetLeadingTriviaWidth() == 0)
			{
				SyntaxToken syntaxToken3 = base.EatToken();
				syntaxToken2 = SyntaxFactory.Token(syntaxToken2.GetLeadingTrivia(), SyntaxKind.GreaterThanGreaterThanToken, syntaxToken3.GetTrailingTrivia());
			}
			ParameterListSyntax parameterListSyntax = this.ParseParenthesizedParameterList();
			int count = parameterListSyntax.Parameters.Count;
			if (count != 1)
			{
				if (count != 2)
				{
					if (syntaxToken2.IsMissing)
					{
						SyntaxDiagnosticInfo syntaxDiagnosticInfo2 = SyntaxParser.MakeError(num, num2, ErrorCode.ERR_OvlOperatorExpected);
						syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { syntaxDiagnosticInfo2 });
					}
					else if (SyntaxFacts.IsOverloadableBinaryOperator(kind))
					{
						syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_BadBinOpArgs, new object[] { SyntaxFacts.GetText(kind) });
					}
					else if (SyntaxFacts.IsOverloadableUnaryOperator(kind))
					{
						syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_BadUnOpArgs, new object[] { SyntaxFacts.GetText(kind) });
					}
					else
					{
						syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_OvlOperatorExpected);
					}
				}
				else if (syntaxToken2.IsMissing || !SyntaxFacts.IsOverloadableBinaryOperator(kind))
				{
					SyntaxDiagnosticInfo syntaxDiagnosticInfo3 = SyntaxParser.MakeError(num, num2, ErrorCode.ERR_OvlBinaryOperatorExpected);
					syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { syntaxDiagnosticInfo3 });
				}
			}
			else if (syntaxToken2.IsMissing || !SyntaxFacts.IsOverloadableUnaryOperator(kind))
			{
				SyntaxDiagnosticInfo syntaxDiagnosticInfo4 = SyntaxParser.MakeError(num, num2, ErrorCode.ERR_OvlUnaryOperatorExpected);
				syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { syntaxDiagnosticInfo4 });
			}
			BlockSyntax blockSyntax;
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax;
			SyntaxToken syntaxToken4;
			this.ParseBlockAndExpressionBodiesWithSemicolon(out blockSyntax, out arrowExpressionClauseSyntax, out syntaxToken4, true, MessageID.IDS_FeatureExpressionBodiedMethod);
			if (kind != SyntaxKind.IsKeyword && !SyntaxFacts.IsOverloadableUnaryOperator(kind) && !SyntaxFacts.IsOverloadableBinaryOperator(kind))
			{
				syntaxToken2 = this.ConvertToMissingWithTrailingTrivia(syntaxToken2, SyntaxKind.PlusToken);
			}
			return this._syntaxFactory.OperatorDeclaration(attributes, modifiers.ToList(), type, syntaxToken, syntaxToken2, parameterListSyntax, blockSyntax, arrowExpressionClauseSyntax, syntaxToken4);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x001231C8 File Offset: 0x001213C8
		private IndexerDeclarationSyntax ParseIndexerDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceOpt, SyntaxToken thisKeyword, TypeParameterListSyntax typeParameterList)
		{
			if (typeParameterList != null)
			{
				thisKeyword = base.AddTrailingSkippedSyntax<SyntaxToken>(thisKeyword, typeParameterList);
				thisKeyword = base.AddError<SyntaxToken>(thisKeyword, ErrorCode.ERR_UnexpectedGenericName);
			}
			BracketedParameterListSyntax bracketedParameterListSyntax = this.ParseBracketedParameterList();
			AccessorListSyntax accessorListSyntax = null;
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = null;
			SyntaxToken syntaxToken = null;
			if (base.CurrentToken.Kind == SyntaxKind.EqualsGreaterThanToken)
			{
				arrowExpressionClauseSyntax = this.ParseArrowExpressionClause();
				arrowExpressionClauseSyntax = base.CheckFeatureAvailability<ArrowExpressionClauseSyntax>(arrowExpressionClauseSyntax, MessageID.IDS_FeatureExpressionBodiedIndexer, false);
				syntaxToken = base.EatToken(SyntaxKind.SemicolonToken);
			}
			else
			{
				accessorListSyntax = this.ParseAccessorList(false);
				if (base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
				{
					syntaxToken = base.EatTokenWithPrejudice(ErrorCode.ERR_UnexpectedSemicolon, Array.Empty<object>());
				}
			}
			if (base.CurrentToken.Kind == SyntaxKind.EqualsGreaterThanToken && syntaxToken == null)
			{
				arrowExpressionClauseSyntax = this.ParseArrowExpressionClause();
				arrowExpressionClauseSyntax = base.CheckFeatureAvailability<ArrowExpressionClauseSyntax>(arrowExpressionClauseSyntax, MessageID.IDS_FeatureExpressionBodiedIndexer, false);
				syntaxToken = base.EatToken(SyntaxKind.SemicolonToken);
			}
			return this._syntaxFactory.IndexerDeclaration(attributes, modifiers.ToList(), type, explicitInterfaceOpt, thisKeyword, bracketedParameterListSyntax, accessorListSyntax, arrowExpressionClauseSyntax, syntaxToken);
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x001232B8 File Offset: 0x001214B8
		private PropertyDeclarationSyntax ParsePropertyDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceOpt, SyntaxToken identifier, TypeParameterListSyntax typeParameterList)
		{
			if (typeParameterList != null)
			{
				identifier = base.AddTrailingSkippedSyntax<SyntaxToken>(identifier, typeParameterList);
				identifier = base.AddError<SyntaxToken>(identifier, ErrorCode.ERR_UnexpectedGenericName);
			}
			AccessorListSyntax accessorListSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
			{
				accessorListSyntax = this.ParseAccessorList(false);
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = null;
			EqualsValueClauseSyntax equalsValueClauseSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.EqualsGreaterThanToken)
			{
				arrowExpressionClauseSyntax = this.ParseArrowExpressionClause();
				arrowExpressionClauseSyntax = base.CheckFeatureAvailability<ArrowExpressionClauseSyntax>(arrowExpressionClauseSyntax, MessageID.IDS_FeatureExpressionBodiedProperty, false);
			}
			else if (base.CurrentToken.Kind == SyntaxKind.EqualsToken)
			{
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EqualsToken);
				ExpressionSyntax expressionSyntax = this.ParseVariableInitializer();
				equalsValueClauseSyntax = this._syntaxFactory.EqualsValueClause(syntaxToken, expressionSyntax);
				equalsValueClauseSyntax = base.CheckFeatureAvailability<EqualsValueClauseSyntax>(equalsValueClauseSyntax, MessageID.IDS_FeatureAutoPropertyInitializer, false);
			}
			SyntaxToken syntaxToken2 = null;
			if (arrowExpressionClauseSyntax != null || equalsValueClauseSyntax != null)
			{
				syntaxToken2 = base.EatToken(SyntaxKind.SemicolonToken);
			}
			else if (base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
			{
				syntaxToken2 = base.EatTokenWithPrejudice(ErrorCode.ERR_UnexpectedSemicolon, Array.Empty<object>());
			}
			return this._syntaxFactory.PropertyDeclaration(attributes, modifiers.ToList(), type, explicitInterfaceOpt, identifier, accessorListSyntax, arrowExpressionClauseSyntax, equalsValueClauseSyntax, syntaxToken2);
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x001233CC File Offset: 0x001215CC
		private AccessorListSyntax ParseAccessorList(bool isEvent)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBraceToken);
			SyntaxList<AccessorDeclarationSyntax> syntaxList = default(SyntaxList<AccessorDeclarationSyntax>);
			if (!syntaxToken.IsMissing || !this.IsTerminator())
			{
				SyntaxListBuilder<AccessorDeclarationSyntax> syntaxListBuilder = this._pool.Allocate<AccessorDeclarationSyntax>();
				try
				{
					while (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
					{
						if (this.IsPossibleAccessor())
						{
							AccessorDeclarationSyntax accessorDeclarationSyntax = this.ParseAccessorDeclaration(isEvent);
							syntaxListBuilder.Add(accessorDeclarationSyntax);
						}
						else if (this.SkipBadAccessorListTokens(ref syntaxToken, syntaxListBuilder, isEvent ? ErrorCode.ERR_AddOrRemoveExpected : ErrorCode.ERR_GetOrSetExpected) == LanguageParser.PostSkipAction.Abort)
						{
							break;
						}
					}
					syntaxList = syntaxListBuilder.ToList();
				}
				finally
				{
					this._pool.Free(syntaxListBuilder);
				}
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBraceToken);
			return this._syntaxFactory.AccessorList(syntaxToken, syntaxList, syntaxToken2);
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x0012349C File Offset: 0x0012169C
		private ArrowExpressionClauseSyntax ParseArrowExpressionClause()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EqualsGreaterThanToken);
			return this._syntaxFactory.ArrowExpressionClause(syntaxToken, this.ParsePossibleRefExpression());
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x001234C8 File Offset: 0x001216C8
		private ExpressionSyntax ParsePossibleRefExpression()
		{
			SyntaxToken syntaxToken = null;
			if (base.CurrentToken.Kind == SyntaxKind.RefKeyword)
			{
				syntaxToken = base.EatToken();
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureRefLocalsReturns, false);
			}
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			if (syntaxToken != null)
			{
				expressionSyntax = this._syntaxFactory.RefExpression(syntaxToken, expressionSyntax);
			}
			return expressionSyntax;
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x00123518 File Offset: 0x00121718
		private LanguageParser.PostSkipAction SkipBadAccessorListTokens(ref SyntaxToken openBrace, SyntaxListBuilder<AccessorDeclarationSyntax> list, ErrorCode error)
		{
			return this.SkipBadListTokensWithErrorCode<SyntaxToken, AccessorDeclarationSyntax>(ref openBrace, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CloseBraceToken && !p.IsPossibleAccessor(), (LanguageParser p) => p.IsTerminator(), error);
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x0012356C File Offset: 0x0012176C
		private bool IsPossibleAccessor()
		{
			return base.CurrentToken.Kind == SyntaxKind.IdentifierToken || this.IsPossibleAttributeDeclaration() || SyntaxFacts.GetAccessorDeclarationKind(base.CurrentToken.ContextualKind) != SyntaxKind.None || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken || this.IsPossibleAccessorModifier();
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x001235D4 File Offset: 0x001217D4
		private bool IsPossibleAccessorModifier()
		{
			if (LanguageParser.GetModifier(base.CurrentToken) == DeclarationModifiers.None)
			{
				return false;
			}
			int num = 1;
			while (LanguageParser.GetModifier(base.PeekToken(num)) != DeclarationModifiers.None)
			{
				num++;
			}
			SyntaxToken syntaxToken = base.PeekToken(num);
			if (syntaxToken.Kind == SyntaxKind.CloseBraceToken || syntaxToken.Kind == SyntaxKind.EndOfFileToken)
			{
				return true;
			}
			SyntaxKind contextualKind = syntaxToken.ContextualKind;
			return contextualKind - SyntaxKind.GetKeyword <= 3 || contextualKind == SyntaxKind.InitKeyword;
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x00123648 File Offset: 0x00121848
		private LanguageParser.PostSkipAction SkipBadSeparatedListTokensWithExpectedKind<T, TNode>(ref T startToken, SeparatedSyntaxListBuilder<TNode> list, Func<LanguageParser, bool> isNotExpectedFunction, Func<LanguageParser, bool> abortFunction, SyntaxKind expected) where T : CSharpSyntaxNode where TNode : CSharpSyntaxNode
		{
			GreenNode greenNode;
			LanguageParser.PostSkipAction postSkipAction = this.SkipBadListTokensWithExpectedKindHelper(list.UnderlyingBuilder, isNotExpectedFunction, abortFunction, expected, out greenNode);
			if (greenNode != null)
			{
				startToken = base.AddTrailingSkippedSyntax<T>(startToken, greenNode);
			}
			return postSkipAction;
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x00123680 File Offset: 0x00121880
		private LanguageParser.PostSkipAction SkipBadListTokensWithErrorCode<T, TNode>(ref T startToken, SyntaxListBuilder<TNode> list, Func<LanguageParser, bool> isNotExpectedFunction, Func<LanguageParser, bool> abortFunction, ErrorCode error) where T : CSharpSyntaxNode where TNode : CSharpSyntaxNode
		{
			GreenNode greenNode;
			LanguageParser.PostSkipAction postSkipAction = this.SkipBadListTokensWithErrorCodeHelper<TNode>(list, isNotExpectedFunction, abortFunction, error, out greenNode);
			if (greenNode != null)
			{
				startToken = base.AddTrailingSkippedSyntax<T>(startToken, greenNode);
			}
			return postSkipAction;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x001236B4 File Offset: 0x001218B4
		private LanguageParser.PostSkipAction SkipBadListTokensWithExpectedKindHelper(SyntaxListBuilder list, Func<LanguageParser, bool> isNotExpectedFunction, Func<LanguageParser, bool> abortFunction, SyntaxKind expected, out GreenNode trailingTrivia)
		{
			if (list.Count == 0)
			{
				return this.SkipBadTokensWithExpectedKind(isNotExpectedFunction, abortFunction, expected, out trailingTrivia);
			}
			GreenNode greenNode;
			LanguageParser.PostSkipAction postSkipAction = this.SkipBadTokensWithExpectedKind(isNotExpectedFunction, abortFunction, expected, out greenNode);
			if (greenNode != null)
			{
				base.AddTrailingSkippedSyntax(list, greenNode);
			}
			trailingTrivia = null;
			return postSkipAction;
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x001236F4 File Offset: 0x001218F4
		private LanguageParser.PostSkipAction SkipBadListTokensWithErrorCodeHelper<TNode>(SyntaxListBuilder<TNode> list, Func<LanguageParser, bool> isNotExpectedFunction, Func<LanguageParser, bool> abortFunction, ErrorCode error, out GreenNode trailingTrivia) where TNode : CSharpSyntaxNode
		{
			if (list.Count == 0)
			{
				return this.SkipBadTokensWithErrorCode(isNotExpectedFunction, abortFunction, error, out trailingTrivia);
			}
			GreenNode greenNode;
			LanguageParser.PostSkipAction postSkipAction = this.SkipBadTokensWithErrorCode(isNotExpectedFunction, abortFunction, error, out greenNode);
			if (greenNode != null)
			{
				base.AddTrailingSkippedSyntax<TNode>(list, greenNode);
			}
			trailingTrivia = null;
			return postSkipAction;
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x00123734 File Offset: 0x00121934
		private LanguageParser.PostSkipAction SkipBadTokensWithExpectedKind(Func<LanguageParser, bool> isNotExpectedFunction, Func<LanguageParser, bool> abortFunction, SyntaxKind expected, out GreenNode trailingTrivia)
		{
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			LanguageParser.PostSkipAction postSkipAction2;
			try
			{
				bool flag = true;
				LanguageParser.PostSkipAction postSkipAction = LanguageParser.PostSkipAction.Continue;
				while (isNotExpectedFunction.Invoke(this))
				{
					if (abortFunction.Invoke(this))
					{
						postSkipAction = LanguageParser.PostSkipAction.Abort;
						break;
					}
					SyntaxToken syntaxToken = ((flag && !base.CurrentToken.ContainsDiagnostics) ? base.EatTokenWithPrejudice(expected) : base.EatToken());
					flag = false;
					syntaxListBuilder.Add(syntaxToken);
				}
				trailingTrivia = ((syntaxListBuilder.Count > 0) ? syntaxListBuilder.ToListNode() : null);
				postSkipAction2 = postSkipAction;
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return postSkipAction2;
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x001237CC File Offset: 0x001219CC
		private LanguageParser.PostSkipAction SkipBadTokensWithErrorCode(Func<LanguageParser, bool> isNotExpectedFunction, Func<LanguageParser, bool> abortFunction, ErrorCode errorCode, out GreenNode trailingTrivia)
		{
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			LanguageParser.PostSkipAction postSkipAction2;
			try
			{
				bool flag = true;
				LanguageParser.PostSkipAction postSkipAction = LanguageParser.PostSkipAction.Continue;
				while (isNotExpectedFunction.Invoke(this))
				{
					if (abortFunction.Invoke(this))
					{
						postSkipAction = LanguageParser.PostSkipAction.Abort;
						break;
					}
					SyntaxToken syntaxToken = ((flag && !base.CurrentToken.ContainsDiagnostics) ? base.EatTokenWithPrejudice(errorCode, Array.Empty<object>()) : base.EatToken());
					flag = false;
					syntaxListBuilder.Add(syntaxToken);
				}
				trailingTrivia = ((syntaxListBuilder.Count > 0) ? syntaxListBuilder.ToListNode() : null);
				postSkipAction2 = postSkipAction;
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return postSkipAction2;
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x00123868 File Offset: 0x00121A68
		private AccessorDeclarationSyntax ParseAccessorDeclaration(bool isEvent)
		{
			if (this.IsIncrementalAndFactoryContextMatches && SyntaxFacts.IsAccessorDeclaration(base.CurrentNodeKind))
			{
				return (AccessorDeclarationSyntax)base.EatNode();
			}
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			AccessorDeclarationSyntax accessorDeclarationSyntax;
			try
			{
				SyntaxList<AttributeListSyntax> syntaxList = this.ParseAttributeDeclarations();
				this.ParseModifiers(syntaxListBuilder, true);
				this.CheckForVersionSpecificModifiers(syntaxListBuilder, SyntaxKind.ReadOnlyKeyword, MessageID.IDS_FeatureReadOnlyMembers);
				if (!isEvent && syntaxListBuilder != null && syntaxListBuilder.Count > 0)
				{
					syntaxListBuilder[0] = base.CheckFeatureAvailability<GreenNode>(syntaxListBuilder[0], MessageID.IDS_FeaturePropertyAccessorMods, false);
				}
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.IdentifierToken, isEvent ? ErrorCode.ERR_AddOrRemoveExpected : ErrorCode.ERR_GetOrSetExpected, true);
				SyntaxKind accessorKind = this.GetAccessorKind(syntaxToken);
				if (accessorKind == SyntaxKind.UnknownAccessorDeclaration)
				{
					if (!syntaxToken.IsMissing)
					{
						syntaxToken = base.AddError<SyntaxToken>(syntaxToken, isEvent ? ErrorCode.ERR_AddOrRemoveExpected : ErrorCode.ERR_GetOrSetExpected);
					}
				}
				else
				{
					syntaxToken = SyntaxParser.ConvertToKeyword(syntaxToken);
				}
				BlockSyntax blockSyntax = null;
				ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = null;
				SyntaxToken syntaxToken2 = null;
				bool flag = base.CurrentToken.Kind == SyntaxKind.SemicolonToken;
				bool flag2 = base.CurrentToken.Kind == SyntaxKind.EqualsGreaterThanToken;
				if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || flag2)
				{
					this.ParseBlockAndExpressionBodiesWithSemicolon(out blockSyntax, out arrowExpressionClauseSyntax, out syntaxToken2, true, MessageID.IDS_FeatureExpressionBodiedAccessor);
				}
				else if (flag)
				{
					syntaxToken2 = this.EatAccessorSemicolon();
				}
				else if (accessorKind != SyntaxKind.UnknownAccessorDeclaration)
				{
					if (!this.IsTerminator())
					{
						blockSyntax = this.ParseMethodOrAccessorBodyBlock(default(SyntaxList<AttributeListSyntax>), true);
					}
					else
					{
						syntaxToken2 = this.EatAccessorSemicolon();
					}
				}
				accessorDeclarationSyntax = this._syntaxFactory.AccessorDeclaration(accessorKind, syntaxList, syntaxListBuilder.ToList(), syntaxToken, blockSyntax, arrowExpressionClauseSyntax, syntaxToken2);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return accessorDeclarationSyntax;
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x00123A24 File Offset: 0x00121C24
		private SyntaxToken EatAccessorSemicolon()
		{
			return base.EatToken(SyntaxKind.SemicolonToken, base.IsFeatureEnabled(MessageID.IDS_FeatureExpressionBodiedAccessor) ? ErrorCode.ERR_SemiOrLBraceOrArrowExpected : ErrorCode.ERR_SemiOrLBraceExpected, true);
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x00123A4C File Offset: 0x00121C4C
		private SyntaxKind GetAccessorKind(SyntaxToken accessorName)
		{
			SyntaxKind contextualKind = accessorName.ContextualKind;
			switch (contextualKind)
			{
			case SyntaxKind.GetKeyword:
				return SyntaxKind.GetAccessorDeclaration;
			case SyntaxKind.SetKeyword:
				return SyntaxKind.SetAccessorDeclaration;
			case SyntaxKind.AddKeyword:
				return SyntaxKind.AddAccessorDeclaration;
			case SyntaxKind.RemoveKeyword:
				return SyntaxKind.RemoveAccessorDeclaration;
			default:
				if (contextualKind != SyntaxKind.InitKeyword)
				{
					return SyntaxKind.UnknownAccessorDeclaration;
				}
				return SyntaxKind.InitAccessorDeclaration;
			}
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00123AAC File Offset: 0x00121CAC
		internal ParameterListSyntax ParseParenthesizedParameterList()
		{
			if (this.IsIncrementalAndFactoryContextMatches && LanguageParser.CanReuseParameterList(base.CurrentNode as ParameterListSyntax))
			{
				return (ParameterListSyntax)base.EatNode();
			}
			SeparatedSyntaxListBuilder<ParameterSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ParameterSyntax>();
			ParameterListSyntax parameterListSyntax;
			try
			{
				SyntaxKind syntaxKind = SyntaxKind.OpenParenToken;
				SyntaxKind syntaxKind2 = SyntaxKind.CloseParenToken;
				SyntaxToken syntaxToken;
				SyntaxToken syntaxToken2;
				this.ParseParameterList(out syntaxToken, separatedSyntaxListBuilder, out syntaxToken2, syntaxKind, syntaxKind2);
				parameterListSyntax = this._syntaxFactory.ParameterList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._pool.Free<ParameterSyntax>(in separatedSyntaxListBuilder);
			}
			return parameterListSyntax;
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00123B40 File Offset: 0x00121D40
		internal BracketedParameterListSyntax ParseBracketedParameterList()
		{
			if (this.IsIncrementalAndFactoryContextMatches && LanguageParser.CanReuseBracketedParameterList(base.CurrentNode as BracketedParameterListSyntax))
			{
				return (BracketedParameterListSyntax)base.EatNode();
			}
			SeparatedSyntaxListBuilder<ParameterSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ParameterSyntax>();
			BracketedParameterListSyntax bracketedParameterListSyntax;
			try
			{
				SyntaxKind syntaxKind = SyntaxKind.OpenBracketToken;
				SyntaxKind syntaxKind2 = SyntaxKind.CloseBracketToken;
				SyntaxToken syntaxToken;
				SyntaxToken syntaxToken2;
				this.ParseParameterList(out syntaxToken, separatedSyntaxListBuilder, out syntaxToken2, syntaxKind, syntaxKind2);
				bracketedParameterListSyntax = this._syntaxFactory.BracketedParameterList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._pool.Free<ParameterSyntax>(in separatedSyntaxListBuilder);
			}
			return bracketedParameterListSyntax;
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00123BD4 File Offset: 0x00121DD4
		private static bool CanReuseParameterList(ParameterListSyntax list)
		{
			if (list == null)
			{
				return false;
			}
			if (list.OpenParenToken.IsMissing)
			{
				return false;
			}
			if (list.CloseParenToken.IsMissing)
			{
				return false;
			}
			SeparatedSyntaxList<ParameterSyntax>.Enumerator enumerator = list.Parameters.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!LanguageParser.CanReuseParameter(enumerator.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00123C38 File Offset: 0x00121E38
		private static bool CanReuseBracketedParameterList(BracketedParameterListSyntax list)
		{
			if (list == null)
			{
				return false;
			}
			if (list.OpenBracketToken.IsMissing)
			{
				return false;
			}
			if (list.CloseBracketToken.IsMissing)
			{
				return false;
			}
			SeparatedSyntaxList<ParameterSyntax>.Enumerator enumerator = list.Parameters.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!LanguageParser.CanReuseParameter(enumerator.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x00123C9C File Offset: 0x00121E9C
		private void ParseParameterList(out SyntaxToken open, SeparatedSyntaxListBuilder<ParameterSyntax> nodes, out SyntaxToken close, SyntaxKind openKind, SyntaxKind closeKind)
		{
			open = base.EatToken(openKind);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfParameterList;
			if (base.CurrentToken.Kind != closeKind)
			{
				while (!this.IsPossibleParameter() && base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					if (this.SkipBadParameterListTokens(ref open, nodes, SyntaxKind.IdentifierToken, closeKind) != LanguageParser.PostSkipAction.Continue)
					{
						goto IL_00F5;
					}
				}
				ParameterSyntax parameterSyntax = this.ParseParameter();
				nodes.Add(parameterSyntax);
				while (base.CurrentToken.Kind != closeKind)
				{
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleParameter())
					{
						nodes.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						parameterSyntax = this.ParseParameter();
						if (parameterSyntax.IsMissing && this.IsPossibleParameter())
						{
							parameterSyntax = base.AddTrailingSkippedSyntax<ParameterSyntax>(parameterSyntax, base.EatToken());
						}
						nodes.Add(parameterSyntax);
					}
					else if (this.SkipBadParameterListTokens(ref open, nodes, SyntaxKind.CommaToken, closeKind) == LanguageParser.PostSkipAction.Abort)
					{
						break;
					}
				}
			}
			IL_00F5:
			this._termState = termState;
			close = base.EatToken(closeKind);
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x00123DAF File Offset: 0x00121FAF
		private bool IsEndOfParameterList()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.CloseBracketToken;
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x00123DD8 File Offset: 0x00121FD8
		private LanguageParser.PostSkipAction SkipBadParameterListTokens(ref SyntaxToken open, SeparatedSyntaxListBuilder<ParameterSyntax> list, SyntaxKind expected, SyntaxKind closeKind)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, ParameterSyntax>(ref open, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleParameter(), (LanguageParser p) => p.CurrentToken.Kind == closeKind || p.IsTerminator(), expected);
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00123E28 File Offset: 0x00122028
		private bool IsPossibleParameter()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.OpenBracketToken)
			{
				if (kind != SyntaxKind.OpenParenToken && kind != SyntaxKind.OpenBracketToken)
				{
					goto IL_0051;
				}
			}
			else if (kind != SyntaxKind.ArgListKeyword)
			{
				if (kind != SyntaxKind.DelegateKeyword)
				{
					if (kind != SyntaxKind.IdentifierToken)
					{
						goto IL_0051;
					}
					return this.IsTrueIdentifier();
				}
				else if (!this.IsFunctionPointerStart())
				{
					goto IL_0051;
				}
			}
			return true;
			IL_0051:
			return LanguageParser.IsParameterModifier(base.CurrentToken.Kind, false) || LanguageParser.IsPredefinedType(base.CurrentToken.Kind);
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00123EAC File Offset: 0x001220AC
		private static bool CanReuseParameter(ParameterSyntax parameter)
		{
			if (parameter == null)
			{
				return false;
			}
			if (parameter.Default != null)
			{
				return false;
			}
			CSharpSyntaxNode parent = parameter.Parent;
			if (parent != null)
			{
				if (parent.Kind() == SyntaxKind.SimpleLambdaExpression)
				{
					return false;
				}
				CSharpSyntaxNode parent2 = parent.Parent;
				if (parent2 != null && parent2.Kind() == SyntaxKind.ParenthesizedLambdaExpression)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x00123EFC File Offset: 0x001220FC
		private ParameterSyntax ParseParameter()
		{
			if (this.IsIncrementalAndFactoryContextMatches && LanguageParser.CanReuseParameter(base.CurrentNode as ParameterSyntax))
			{
				return (ParameterSyntax)base.EatNode();
			}
			SyntaxList<AttributeListSyntax> syntaxList = this.ParseAttributeDeclarations();
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			ParameterSyntax parameterSyntax;
			try
			{
				this.ParseParameterModifiers(syntaxListBuilder, false);
				TypeSyntax typeSyntax;
				SyntaxToken syntaxToken;
				if (base.CurrentToken.Kind != SyntaxKind.ArgListKeyword)
				{
					typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Parameter);
					syntaxToken = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
					if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken && base.PeekToken(1).Kind == SyntaxKind.CloseBracketToken)
					{
						SyntaxToken syntaxToken2 = base.EatToken();
						SyntaxToken syntaxToken3 = base.EatToken();
						syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_BadArraySyntax);
						syntaxToken = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken, SyntaxList.List(syntaxToken2, syntaxToken3));
					}
				}
				else
				{
					typeSyntax = null;
					syntaxToken = base.EatToken(SyntaxKind.ArgListKeyword);
				}
				EqualsValueClauseSyntax equalsValueClauseSyntax = null;
				if (base.CurrentToken.Kind == SyntaxKind.EqualsToken)
				{
					SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.EqualsToken);
					ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
					equalsValueClauseSyntax = this._syntaxFactory.EqualsValueClause(syntaxToken4, expressionSyntax);
					equalsValueClauseSyntax = base.CheckFeatureAvailability<EqualsValueClauseSyntax>(equalsValueClauseSyntax, MessageID.IDS_FeatureOptionalParameter, false);
				}
				parameterSyntax = this._syntaxFactory.Parameter(syntaxList, syntaxListBuilder.ToList(), typeSyntax, syntaxToken, equalsValueClauseSyntax);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return parameterSyntax;
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00124060 File Offset: 0x00122260
		private static bool IsParameterModifier(SyntaxKind kind, bool isFunctionPointerParameter = false)
		{
			if (kind <= SyntaxKind.InKeyword)
			{
				if (kind != SyntaxKind.ReadOnlyKeyword)
				{
					if (kind - SyntaxKind.RefKeyword > 2)
					{
						return false;
					}
				}
				else if (!isFunctionPointerParameter)
				{
					return false;
				}
			}
			else if (kind != SyntaxKind.ParamsKeyword && kind != SyntaxKind.ThisKeyword)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x001240A4 File Offset: 0x001222A4
		private void ParseParameterModifiers(SyntaxListBuilder modifiers, bool isFunctionPointerParameter = false)
		{
			while (LanguageParser.IsParameterModifier(base.CurrentToken.Kind, isFunctionPointerParameter))
			{
				SyntaxToken syntaxToken = base.EatToken();
				SyntaxKind kind = syntaxToken.Kind;
				if (kind != SyntaxKind.RefKeyword)
				{
					if (kind != SyntaxKind.InKeyword)
					{
						if (kind == SyntaxKind.ThisKeyword)
						{
							syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureExtensionMethod, false);
							if (base.CurrentToken.Kind == SyntaxKind.RefKeyword || base.CurrentToken.Kind == SyntaxKind.InKeyword)
							{
								syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureRefExtensionMethods, false);
							}
						}
					}
					else
					{
						syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureReadOnlyReferences, false);
						if (base.CurrentToken.Kind == SyntaxKind.ThisKeyword)
						{
							syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureRefExtensionMethods, false);
						}
					}
				}
				else if (base.CurrentToken.Kind == SyntaxKind.ThisKeyword)
				{
					syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureRefExtensionMethods, false);
				}
				modifiers.Add(syntaxToken);
			}
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x00124190 File Offset: 0x00122390
		private FieldDeclarationSyntax ParseFixedSizeBufferDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, SyntaxKind parentKind)
		{
			SyntaxToken syntaxToken = base.EatToken();
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureFixedBuffer, false);
			modifiers.Add(syntaxToken);
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfFieldDeclaration;
			SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<VariableDeclaratorSyntax>();
			FieldDeclarationSyntax fieldDeclarationSyntax;
			try
			{
				this.ParseVariableDeclarators(typeSyntax, LanguageParser.VariableFlags.Fixed, separatedSyntaxListBuilder, parentKind);
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.SemicolonToken);
				fieldDeclarationSyntax = this._syntaxFactory.FieldDeclaration(attributes, modifiers.ToList(), this._syntaxFactory.VariableDeclaration(typeSyntax, in separatedSyntaxListBuilder), syntaxToken2);
			}
			finally
			{
				this._termState = termState;
				this._pool.Free<VariableDeclaratorSyntax>(in separatedSyntaxListBuilder);
			}
			return fieldDeclarationSyntax;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x00124254 File Offset: 0x00122454
		private MemberDeclarationSyntax ParseEventDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, SyntaxKind parentKind)
		{
			SyntaxToken syntaxToken = base.EatToken();
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			if (this.IsFieldDeclaration(true))
			{
				return this.ParseEventFieldDeclaration(attributes, modifiers, syntaxToken, typeSyntax, parentKind);
			}
			return this.ParseEventDeclarationWithAccessors(attributes, modifiers, syntaxToken, typeSyntax);
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x00124290 File Offset: 0x00122490
		private EventDeclarationSyntax ParseEventDeclarationWithAccessors(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, SyntaxToken eventToken, TypeSyntax type)
		{
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax;
			SyntaxToken syntaxToken;
			TypeParameterListSyntax typeParameterListSyntax;
			this.ParseMemberName(out explicitInterfaceSpecifierSyntax, out syntaxToken, out typeParameterListSyntax, true);
			this.CheckForVersionSpecificModifiers(modifiers, SyntaxKind.ReadOnlyKeyword, MessageID.IDS_FeatureReadOnlyMembers);
			if (explicitInterfaceSpecifierSyntax != null && base.CurrentToken.Kind != SyntaxKind.OpenBraceToken && base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
			{
				SyntaxToken syntaxToken2 = ((syntaxToken == null) ? LanguageParser.CreateMissingIdentifierToken() : syntaxToken);
				AccessorListSyntax accessorListSyntax = this._syntaxFactory.AccessorList(SyntaxFactory.MissingToken(SyntaxKind.OpenBraceToken), default(SyntaxList<AccessorDeclarationSyntax>), SyntaxFactory.MissingToken(SyntaxKind.CloseBraceToken));
				return this._syntaxFactory.EventDeclaration(attributes, modifiers.ToList(), eventToken, type, explicitInterfaceSpecifierSyntax, syntaxToken2, accessorListSyntax, null);
			}
			SyntaxToken syntaxToken3;
			if (syntaxToken == null)
			{
				syntaxToken3 = LanguageParser.CreateMissingIdentifierToken();
			}
			else if (syntaxToken.Kind != SyntaxKind.IdentifierToken)
			{
				syntaxToken3 = this.ConvertToMissingWithTrailingTrivia(syntaxToken, SyntaxKind.IdentifierToken);
			}
			else
			{
				syntaxToken3 = syntaxToken;
			}
			if (syntaxToken3.IsMissing && !type.IsMissing)
			{
				syntaxToken3 = base.AddError<SyntaxToken>(syntaxToken3, ErrorCode.ERR_IdentifierExpected);
			}
			if (typeParameterListSyntax != null)
			{
				syntaxToken3 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken3, typeParameterListSyntax);
				syntaxToken3 = base.AddError<SyntaxToken>(syntaxToken3, ErrorCode.ERR_UnexpectedGenericName);
			}
			AccessorListSyntax accessorListSyntax2 = null;
			SyntaxToken syntaxToken4 = null;
			if (explicitInterfaceSpecifierSyntax != null && base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
			{
				syntaxToken4 = base.EatToken(SyntaxKind.SemicolonToken);
			}
			else
			{
				accessorListSyntax2 = this.ParseAccessorList(true);
			}
			EventDeclarationSyntax eventDeclarationSyntax = this._syntaxFactory.EventDeclaration(attributes, modifiers.ToList(), eventToken, type, explicitInterfaceSpecifierSyntax, syntaxToken3, accessorListSyntax2, syntaxToken4);
			return this.EatUnexpectedTrailingSemicolon<EventDeclarationSyntax>(eventDeclarationSyntax);
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x001243FC File Offset: 0x001225FC
		private TNode EatUnexpectedTrailingSemicolon<TNode>(TNode decl) where TNode : CSharpSyntaxNode
		{
			if (base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_UnexpectedSemicolon);
				decl = base.AddTrailingSkippedSyntax<TNode>(decl, syntaxToken);
			}
			return decl;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x0012443C File Offset: 0x0012263C
		private FieldDeclarationSyntax ParseNormalFieldDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, TypeSyntax type, SyntaxKind parentKind)
		{
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfFieldDeclaration;
			SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<VariableDeclaratorSyntax>();
			FieldDeclarationSyntax fieldDeclarationSyntax;
			try
			{
				this.ParseVariableDeclarators(type, (LanguageParser.VariableFlags)0, separatedSyntaxListBuilder, parentKind);
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.SemicolonToken);
				fieldDeclarationSyntax = this._syntaxFactory.FieldDeclaration(attributes, modifiers.ToList(), this._syntaxFactory.VariableDeclaration(type, in separatedSyntaxListBuilder), syntaxToken);
			}
			finally
			{
				this._termState = termState;
				this._pool.Free<VariableDeclaratorSyntax>(in separatedSyntaxListBuilder);
			}
			return fieldDeclarationSyntax;
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x001244D8 File Offset: 0x001226D8
		private EventFieldDeclarationSyntax ParseEventFieldDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, SyntaxToken eventToken, TypeSyntax type, SyntaxKind parentKind)
		{
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfFieldDeclaration;
			SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<VariableDeclaratorSyntax>();
			EventFieldDeclarationSyntax eventFieldDeclarationSyntax;
			try
			{
				this.ParseVariableDeclarators(type, (LanguageParser.VariableFlags)0, separatedSyntaxListBuilder, parentKind);
				if (base.CurrentToken.Kind == SyntaxKind.DotToken)
				{
					eventToken = base.AddError<SyntaxToken>(eventToken, ErrorCode.ERR_ExplicitEventFieldImpl);
				}
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.SemicolonToken);
				eventFieldDeclarationSyntax = this._syntaxFactory.EventFieldDeclaration(attributes, modifiers.ToList(), eventToken, this._syntaxFactory.VariableDeclaration(type, in separatedSyntaxListBuilder), syntaxToken);
			}
			finally
			{
				this._termState = termState;
				this._pool.Free<VariableDeclaratorSyntax>(in separatedSyntaxListBuilder);
			}
			return eventFieldDeclarationSyntax;
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x00124594 File Offset: 0x00122794
		private bool IsEndOfFieldDeclaration()
		{
			return base.CurrentToken.Kind == SyntaxKind.SemicolonToken;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x001245A8 File Offset: 0x001227A8
		private void ParseVariableDeclarators(TypeSyntax type, LanguageParser.VariableFlags flags, SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> variables, SyntaxKind parentKind)
		{
			bool flag = parentKind != SyntaxKind.NamespaceDeclaration && (parentKind != SyntaxKind.CompilationUnit || base.IsScript);
			LocalFunctionStatementSyntax localFunctionStatementSyntax;
			this.ParseVariableDeclarators(type, flags, variables, flag, false, default(SyntaxList<AttributeListSyntax>), default(SyntaxList<SyntaxToken>), out localFunctionStatementSyntax);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x001245F4 File Offset: 0x001227F4
		private void ParseVariableDeclarators(TypeSyntax type, LanguageParser.VariableFlags flags, SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> variables, bool variableDeclarationsExpected, bool allowLocalFunctions, SyntaxList<AttributeListSyntax> attributes, SyntaxList<SyntaxToken> mods, out LocalFunctionStatementSyntax localFunction)
		{
			variables.Add(this.ParseVariableDeclarator(type, flags, true, allowLocalFunctions, attributes, mods, out localFunction, false));
			if (localFunction != null)
			{
				return;
			}
			while (base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
			{
				if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
				{
					variables.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
					variables.Add(this.ParseVariableDeclarator(type, flags, false, false, attributes, mods, out localFunction, false));
				}
				else if (!variableDeclarationsExpected || this.SkipBadVariableListTokens(variables, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
				{
					break;
				}
			}
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x00124688 File Offset: 0x00122888
		private LanguageParser.PostSkipAction SkipBadVariableListTokens(SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> list, SyntaxKind expected)
		{
			CSharpSyntaxNode csharpSyntaxNode = null;
			return this.SkipBadSeparatedListTokensWithExpectedKind<CSharpSyntaxNode, VariableDeclaratorSyntax>(ref csharpSyntaxNode, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken, (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.SemicolonToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x001246E0 File Offset: 0x001228E0
		private static SyntaxTokenList GetOriginalModifiers(CSharpSyntaxNode decl)
		{
			if (decl != null)
			{
				SyntaxKind syntaxKind = decl.Kind();
				if (syntaxKind <= SyntaxKind.ConstructorDeclaration)
				{
					if (syntaxKind - SyntaxKind.ClassDeclaration > 2)
					{
						if (syntaxKind == SyntaxKind.DelegateDeclaration)
						{
							return ((DelegateDeclarationSyntax)decl).Modifiers;
						}
						switch (syntaxKind)
						{
						case SyntaxKind.FieldDeclaration:
							return ((FieldDeclarationSyntax)decl).Modifiers;
						case SyntaxKind.EventFieldDeclaration:
							return ((EventFieldDeclarationSyntax)decl).Modifiers;
						case SyntaxKind.MethodDeclaration:
							return ((MethodDeclarationSyntax)decl).Modifiers;
						case SyntaxKind.OperatorDeclaration:
						case SyntaxKind.ConversionOperatorDeclaration:
							goto IL_0116;
						case SyntaxKind.ConstructorDeclaration:
							return ((ConstructorDeclarationSyntax)decl).Modifiers;
						default:
							goto IL_0116;
						}
					}
				}
				else
				{
					if (syntaxKind <= SyntaxKind.InitAccessorDeclaration)
					{
						switch (syntaxKind)
						{
						case SyntaxKind.DestructorDeclaration:
							return ((DestructorDeclarationSyntax)decl).Modifiers;
						case SyntaxKind.PropertyDeclaration:
							return ((PropertyDeclarationSyntax)decl).Modifiers;
						case SyntaxKind.EventDeclaration:
						case SyntaxKind.IndexerDeclaration:
						case SyntaxKind.AccessorList:
							goto IL_0116;
						case SyntaxKind.GetAccessorDeclaration:
						case SyntaxKind.SetAccessorDeclaration:
						case SyntaxKind.AddAccessorDeclaration:
						case SyntaxKind.RemoveAccessorDeclaration:
							break;
						default:
							if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
							{
								goto IL_0116;
							}
							break;
						}
						return ((AccessorDeclarationSyntax)decl).Modifiers;
					}
					if (syntaxKind != SyntaxKind.RecordDeclaration && syntaxKind != SyntaxKind.RecordStructDeclaration)
					{
						goto IL_0116;
					}
				}
				return ((TypeDeclarationSyntax)decl).Modifiers;
			}
			IL_0116:
			return default(SyntaxTokenList);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x0012480C File Offset: 0x00122A0C
		private static bool WasFirstVariable(VariableDeclaratorSyntax variable)
		{
			VariableDeclarationSyntax variableDeclarationSyntax = LanguageParser.GetOldParent(variable) as VariableDeclarationSyntax;
			return variableDeclarationSyntax != null && variableDeclarationSyntax.Variables[0] == variable;
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x0012483C File Offset: 0x00122A3C
		private static LanguageParser.VariableFlags GetOriginalVariableFlags(VariableDeclaratorSyntax old)
		{
			CSharpSyntaxNode oldParent = LanguageParser.GetOldParent(old);
			SyntaxTokenList originalModifiers = LanguageParser.GetOriginalModifiers(oldParent);
			LanguageParser.VariableFlags variableFlags = (LanguageParser.VariableFlags)0;
			if (originalModifiers.Any(SyntaxKind.FixedKeyword))
			{
				variableFlags |= LanguageParser.VariableFlags.Fixed;
			}
			if (originalModifiers.Any(SyntaxKind.ConstKeyword))
			{
				variableFlags |= LanguageParser.VariableFlags.Const;
			}
			if (oldParent != null && (oldParent.Kind() == SyntaxKind.VariableDeclaration || oldParent.Kind() == SyntaxKind.LocalDeclarationStatement))
			{
				variableFlags |= LanguageParser.VariableFlags.Local;
			}
			return variableFlags;
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x0012489C File Offset: 0x00122A9C
		private static bool CanReuseVariableDeclarator(VariableDeclaratorSyntax old, LanguageParser.VariableFlags flags, bool isFirst)
		{
			SyntaxKind syntaxKind;
			return old != null && (flags == LanguageParser.GetOriginalVariableFlags(old) && isFirst == LanguageParser.WasFirstVariable(old) && old.Initializer == null && (syntaxKind = LanguageParser.GetOldParent(old).Kind()) != SyntaxKind.VariableDeclaration) && syntaxKind != SyntaxKind.LocalDeclarationStatement;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x001248EC File Offset: 0x00122AEC
		private VariableDeclaratorSyntax ParseVariableDeclarator(TypeSyntax parentType, LanguageParser.VariableFlags flags, bool isFirst, bool allowLocalFunctions, SyntaxList<AttributeListSyntax> attributes, SyntaxList<SyntaxToken> mods, out LocalFunctionStatementSyntax localFunction, bool isExpressionContext = false)
		{
			if (this.IsIncrementalAndFactoryContextMatches && LanguageParser.CanReuseVariableDeclarator(base.CurrentNode as VariableDeclaratorSyntax, flags, isFirst))
			{
				localFunction = null;
				return (VariableDeclaratorSyntax)base.EatNode();
			}
			if (!isExpressionContext)
			{
				LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
				try
				{
					SyntaxKind syntaxKind = base.CurrentToken.Kind;
					if (syntaxKind == SyntaxKind.IdentifierToken && !parentType.IsMissing && parentType.GetLastToken().TrailingTrivia.Any(8539))
					{
						int num;
						int num2;
						base.GetDiagnosticSpanForMissingToken(out num, out num2);
						base.EatToken();
						syntaxKind = base.CurrentToken.Kind;
						bool flag = syntaxKind != SyntaxKind.EqualsToken && SyntaxFacts.IsBinaryExpressionOperatorToken(syntaxKind);
						if ((syntaxKind == SyntaxKind.DotToken || syntaxKind == SyntaxKind.OpenParenToken || syntaxKind == SyntaxKind.MinusGreaterThanToken || flag) && ((syntaxKind != SyntaxKind.OpenParenToken && syntaxKind != SyntaxKind.LessThanToken) || !this.IsLocalFunctionAfterIdentifier()))
						{
							SyntaxToken syntaxToken = LanguageParser.CreateMissingIdentifierToken();
							syntaxToken = base.AddError<SyntaxToken>(syntaxToken, num, num2, ErrorCode.ERR_IdentifierExpected, Array.Empty<object>());
							localFunction = null;
							return this._syntaxFactory.VariableDeclarator(syntaxToken, null, null);
						}
					}
				}
				finally
				{
					this.Reset(ref resetPoint);
					this.Release(ref resetPoint);
				}
			}
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			BracketedArgumentListSyntax bracketedArgumentListSyntax = null;
			EqualsValueClauseSyntax equalsValueClauseSyntax = null;
			LanguageParser.TerminatorState termState = this._termState;
			bool flag2 = (flags & LanguageParser.VariableFlags.Fixed) > (LanguageParser.VariableFlags)0;
			bool flag3 = (flags & LanguageParser.VariableFlags.Const) > (LanguageParser.VariableFlags)0;
			bool flag4 = (flags & LanguageParser.VariableFlags.Local) > (LanguageParser.VariableFlags)0;
			if (!isFirst && this.IsTrueIdentifier())
			{
				syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_MultiTypeInDeclaration);
			}
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.EqualsToken)
			{
				if (kind == SyntaxKind.OpenParenToken)
				{
					if (allowLocalFunctions && isFirst)
					{
						localFunction = this.TryParseLocalFunctionStatementBody(attributes, mods, parentType, syntaxToken2);
						if (localFunction != null)
						{
							return null;
						}
					}
					this._termState |= LanguageParser.TerminatorState.IsPossibleEndOfVariableDeclaration;
					bracketedArgumentListSyntax = this.ParseBracketedArgumentList();
					this._termState = termState;
					bracketedArgumentListSyntax = base.AddError<BracketedArgumentListSyntax>(bracketedArgumentListSyntax, ErrorCode.ERR_BadVarDecl);
					goto IL_0415;
				}
				if (kind != SyntaxKind.EqualsToken)
				{
					goto IL_03E1;
				}
			}
			else
			{
				if (kind == SyntaxKind.OpenBracketToken)
				{
					goto IL_02B5;
				}
				if (kind != SyntaxKind.LessThanToken)
				{
					goto IL_03E1;
				}
				if (!allowLocalFunctions || !isFirst)
				{
					goto IL_03E1;
				}
				localFunction = this.TryParseLocalFunctionStatementBody(attributes, mods, parentType, syntaxToken2);
				if (localFunction != null)
				{
					return null;
				}
				goto IL_03E1;
			}
			IL_01CF:
			if (!flag2)
			{
				SyntaxToken syntaxToken3 = base.EatToken();
				SyntaxToken syntaxToken4 = null;
				if (flag4 && !flag3 && base.CurrentToken.Kind == SyntaxKind.RefKeyword)
				{
					syntaxToken4 = base.EatToken();
					syntaxToken4 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken4, MessageID.IDS_FeatureRefLocalsReturns, false);
				}
				ExpressionSyntax expressionSyntax = this.ParseVariableInitializer();
				if (syntaxToken4 != null)
				{
					expressionSyntax = this._syntaxFactory.RefExpression(syntaxToken4, expressionSyntax);
				}
				equalsValueClauseSyntax = this._syntaxFactory.EqualsValueClause(syntaxToken3, expressionSyntax);
				goto IL_0415;
			}
			goto IL_03E1;
			IL_02B5:
			this._termState |= LanguageParser.TerminatorState.IsPossibleEndOfVariableDeclaration;
			bool flag5;
			ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = this.ParseArrayRankSpecifier(out flag5);
			this._termState = termState;
			SyntaxToken openBracketToken = arrayRankSpecifierSyntax.OpenBracketToken;
			SeparatedSyntaxList<ExpressionSyntax> sizes = arrayRankSpecifierSyntax.Sizes;
			SyntaxToken syntaxToken5 = arrayRankSpecifierSyntax.CloseBracketToken;
			if (flag2 && !flag5)
			{
				syntaxToken5 = base.AddError<SyntaxToken>(syntaxToken5, ErrorCode.ERR_ValueExpected);
			}
			SeparatedSyntaxListBuilder<ArgumentSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ArgumentSyntax>();
			try
			{
				foreach (GreenNode greenNode in sizes.GetWithSeparators())
				{
					ExpressionSyntax expressionSyntax2 = greenNode as ExpressionSyntax;
					if (expressionSyntax2 != null)
					{
						bool flag6 = expressionSyntax2.Kind == SyntaxKind.OmittedArraySizeExpression;
						if (!flag2 && !flag6)
						{
							expressionSyntax2 = base.AddError<ExpressionSyntax>(expressionSyntax2, ErrorCode.ERR_ArraySizeInDeclaration);
						}
						separatedSyntaxListBuilder.Add(this._syntaxFactory.Argument(null, null, expressionSyntax2));
					}
					else
					{
						separatedSyntaxListBuilder.AddSeparator((SyntaxToken)greenNode);
					}
				}
				bracketedArgumentListSyntax = this._syntaxFactory.BracketedArgumentList(openBracketToken, in separatedSyntaxListBuilder, syntaxToken5);
				if (!flag2)
				{
					bracketedArgumentListSyntax = base.AddError<BracketedArgumentListSyntax>(bracketedArgumentListSyntax, ErrorCode.ERR_CStyleArray);
					if (base.CurrentToken.Kind == SyntaxKind.EqualsToken)
					{
						goto IL_01CF;
					}
				}
				goto IL_0415;
			}
			finally
			{
				this._pool.Free<ArgumentSyntax>(in separatedSyntaxListBuilder);
			}
			IL_03E1:
			if (flag3)
			{
				syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_ConstValueRequired);
			}
			else if (flag2)
			{
				if (parentType.Kind != SyntaxKind.ArrayType)
				{
					goto IL_02B5;
				}
				syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_FixedDimsRequired);
			}
			IL_0415:
			localFunction = null;
			return this._syntaxFactory.VariableDeclarator(syntaxToken2, bracketedArgumentListSyntax, equalsValueClauseSyntax);
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x00124D40 File Offset: 0x00122F40
		private bool IsLocalFunctionAfterIdentifier()
		{
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag;
			try
			{
				this.ParseTypeParameterList();
				if (!this.ParseParenthesizedParameterList().IsMissing && (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || base.CurrentToken.Kind == SyntaxKind.EqualsGreaterThanToken || base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword))
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag;
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x00124DC8 File Offset: 0x00122FC8
		private bool IsPossibleEndOfVariableDeclaration()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			return kind == SyntaxKind.SemicolonToken || kind == SyntaxKind.CommaToken;
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x00124DF4 File Offset: 0x00122FF4
		private ExpressionSyntax ParseVariableInitializer()
		{
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
			{
				return this.ParseArrayInitializer();
			}
			return this.ParseExpressionCore();
		}

		// Token: 0x0600492F RID: 18735 RVA: 0x00124E15 File Offset: 0x00123015
		private bool IsPossibleVariableInitializer()
		{
			return base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || this.IsPossibleExpression();
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x00124E34 File Offset: 0x00123034
		private FieldDeclarationSyntax ParseConstantFieldDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, SyntaxKind parentKind)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ConstKeyword);
			modifiers.Add(syntaxToken);
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<VariableDeclaratorSyntax>();
			FieldDeclarationSyntax fieldDeclarationSyntax;
			try
			{
				this.ParseVariableDeclarators(typeSyntax, LanguageParser.VariableFlags.Const, separatedSyntaxListBuilder, parentKind);
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.SemicolonToken);
				fieldDeclarationSyntax = this._syntaxFactory.FieldDeclaration(attributes, modifiers.ToList(), this._syntaxFactory.VariableDeclaration(typeSyntax, in separatedSyntaxListBuilder), syntaxToken2);
			}
			finally
			{
				this._pool.Free<VariableDeclaratorSyntax>(in separatedSyntaxListBuilder);
			}
			return fieldDeclarationSyntax;
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x00124ED0 File Offset: 0x001230D0
		private DelegateDeclarationSyntax ParseDelegateDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.DelegateKeyword);
			TypeSyntax typeSyntax = this.ParseReturnType();
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfMethodSignature;
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			TypeParameterListSyntax typeParameterListSyntax = this.ParseTypeParameterList();
			ParameterListSyntax parameterListSyntax = this.ParseParenthesizedParameterList();
			SyntaxListBuilder<TypeParameterConstraintClauseSyntax> syntaxListBuilder = default(SyntaxListBuilder<TypeParameterConstraintClauseSyntax>);
			DelegateDeclarationSyntax delegateDeclarationSyntax;
			try
			{
				if (base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword)
				{
					syntaxListBuilder = this._pool.Allocate<TypeParameterConstraintClauseSyntax>();
					this.ParseTypeParameterConstraintClauses(syntaxListBuilder);
				}
				this._termState = termState;
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.SemicolonToken);
				delegateDeclarationSyntax = this._syntaxFactory.DelegateDeclaration(attributes, modifiers.ToList(), syntaxToken, typeSyntax, syntaxToken2, typeParameterListSyntax, parameterListSyntax, syntaxListBuilder, syntaxToken3);
			}
			finally
			{
				if (!syntaxListBuilder.IsNull)
				{
					this._pool.Free(syntaxListBuilder);
				}
			}
			return delegateDeclarationSyntax;
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x00124FC8 File Offset: 0x001231C8
		private EnumDeclarationSyntax ParseEnumDeclaration(SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EnumKeyword);
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			TypeParameterListSyntax typeParameterListSyntax = this.ParseTypeParameterList();
			if (typeParameterListSyntax != null)
			{
				syntaxToken2 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken2, typeParameterListSyntax);
				syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_UnexpectedGenericName);
			}
			BaseListSyntax baseListSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.ColonToken)
			{
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.ColonToken);
				TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
				SeparatedSyntaxListBuilder<BaseTypeSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<BaseTypeSyntax>();
				separatedSyntaxListBuilder.Add(this._syntaxFactory.SimpleBaseType(typeSyntax));
				baseListSyntax = this._syntaxFactory.BaseList(syntaxToken3, in separatedSyntaxListBuilder);
				this._pool.Free<BaseTypeSyntax>(in separatedSyntaxListBuilder);
			}
			SeparatedSyntaxList<EnumMemberDeclarationSyntax> separatedSyntaxList = default(SeparatedSyntaxList<EnumMemberDeclarationSyntax>);
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.OpenBraceToken);
			if (!syntaxToken4.IsMissing)
			{
				SeparatedSyntaxListBuilder<EnumMemberDeclarationSyntax> separatedSyntaxListBuilder2 = this._pool.AllocateSeparated<EnumMemberDeclarationSyntax>();
				try
				{
					this.ParseEnumMemberDeclarations(ref syntaxToken4, separatedSyntaxListBuilder2);
					separatedSyntaxList = separatedSyntaxListBuilder2.ToList();
				}
				finally
				{
					this._pool.Free<EnumMemberDeclarationSyntax>(in separatedSyntaxListBuilder2);
				}
			}
			SyntaxToken syntaxToken5 = base.EatToken(SyntaxKind.CloseBraceToken);
			SyntaxToken syntaxToken6 = base.TryEatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.EnumDeclaration(attributes, modifiers.ToList(), syntaxToken, syntaxToken2, baseListSyntax, syntaxToken4, separatedSyntaxList, syntaxToken5, syntaxToken6);
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x00125118 File Offset: 0x00123318
		private void ParseEnumMemberDeclarations(ref SyntaxToken openBrace, SeparatedSyntaxListBuilder<EnumMemberDeclarationSyntax> members)
		{
			if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
			{
				for (;;)
				{
					IL_0015:
					if (this.IsPossibleEnumMemberDeclaration() || base.CurrentToken.Kind == SyntaxKind.CommaToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
					{
						members.Add(this.ParseEnumMemberDeclaration());
						while (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
						{
							if (base.CurrentToken.Kind == SyntaxKind.CommaToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken || this.IsPossibleEnumMemberDeclaration())
							{
								if (base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
								{
									members.AddSeparator(base.EatTokenWithPrejudice(SyntaxKind.CommaToken));
								}
								else
								{
									members.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
								}
								if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
								{
									break;
								}
								if (!this.IsPossibleEnumMemberDeclaration())
								{
									goto IL_0015;
								}
								members.Add(this.ParseEnumMemberDeclaration());
							}
							else if (this.SkipBadEnumMemberListTokens(ref openBrace, members, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
							{
								return;
							}
						}
						return;
					}
					if (this.SkipBadEnumMemberListTokens(ref openBrace, members, SyntaxKind.IdentifierToken) != LanguageParser.PostSkipAction.Continue)
					{
						return;
					}
				}
				return;
			}
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x00125248 File Offset: 0x00123448
		private LanguageParser.PostSkipAction SkipBadEnumMemberListTokens(ref SyntaxToken openBrace, SeparatedSyntaxListBuilder<EnumMemberDeclarationSyntax> list, SyntaxKind expected)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, EnumMemberDeclarationSyntax>(ref openBrace, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && p.CurrentToken.Kind != SyntaxKind.SemicolonToken && !p.IsPossibleEnumMemberDeclaration(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseBraceToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x0012529C File Offset: 0x0012349C
		private EnumMemberDeclarationSyntax ParseEnumMemberDeclaration()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.EnumMemberDeclaration)
			{
				return (EnumMemberDeclarationSyntax)base.EatNode();
			}
			SyntaxList<AttributeListSyntax> syntaxList = this.ParseAttributeDeclarations();
			SyntaxToken syntaxToken = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			EqualsValueClauseSyntax equalsValueClauseSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.EqualsToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.EqualsToken);
				ExpressionSyntax expressionSyntax;
				if (base.CurrentToken.Kind == SyntaxKind.CommaToken || base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
				{
					expressionSyntax = this.ParseIdentifierName(ErrorCode.ERR_ConstantExpected);
				}
				else
				{
					expressionSyntax = this.ParseExpressionCore();
				}
				equalsValueClauseSyntax = this._syntaxFactory.EqualsValueClause(syntaxToken2, expressionSyntax);
			}
			return this._syntaxFactory.EnumMemberDeclaration(syntaxList, default(SyntaxList<SyntaxToken>), syntaxToken, equalsValueClauseSyntax);
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x0012535F File Offset: 0x0012355F
		private bool IsPossibleEnumMemberDeclaration()
		{
			return base.CurrentToken.Kind == SyntaxKind.OpenBracketToken || this.IsTrueIdentifier();
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0012537B File Offset: 0x0012357B
		private bool IsDotOrColonColon()
		{
			return base.CurrentToken.Kind == SyntaxKind.DotToken || base.CurrentToken.Kind == SyntaxKind.ColonColonToken;
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x001253A3 File Offset: 0x001235A3
		public NameSyntax ParseName()
		{
			return this.ParseQualifiedName(LanguageParser.NameOptions.None);
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x001253AC File Offset: 0x001235AC
		private IdentifierNameSyntax CreateMissingIdentifierName()
		{
			return this._syntaxFactory.IdentifierName(LanguageParser.CreateMissingIdentifierToken());
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x001253BE File Offset: 0x001235BE
		private static SyntaxToken CreateMissingIdentifierToken()
		{
			return SyntaxFactory.MissingToken(SyntaxKind.IdentifierToken);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x001253CA File Offset: 0x001235CA
		private bool IsTrueIdentifier()
		{
			return base.CurrentToken.Kind == SyntaxKind.IdentifierToken && !this.IsCurrentTokenPartialKeywordOfPartialMethodOrType() && !this.IsCurrentTokenQueryKeywordInQuery() && !this.IsCurrentTokenWhereOfConstraintClause();
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x001253F9 File Offset: 0x001235F9
		private bool IsTrueIdentifier(SyntaxToken token)
		{
			return token.Kind == SyntaxKind.IdentifierToken && (!this.IsInQuery || !LanguageParser.IsTokenQueryContextualKeyword(token));
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x00125420 File Offset: 0x00123620
		private IdentifierNameSyntax ParseIdentifierName(ErrorCode code = ErrorCode.ERR_IdentifierExpected)
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.IdentifierName && !SyntaxFacts.IsContextualKeyword(((IdentifierNameSyntax)base.CurrentNode).Identifier.Kind()))
			{
				return (IdentifierNameSyntax)base.EatNode();
			}
			return SyntaxFactory.IdentifierName(this.ParseIdentifierToken(code));
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x00125478 File Offset: 0x00123678
		private SyntaxToken ParseIdentifierToken(ErrorCode code = ErrorCode.ERR_IdentifierExpected)
		{
			if (base.CurrentToken.Kind != SyntaxKind.IdentifierToken)
			{
				SyntaxToken syntaxToken = LanguageParser.CreateMissingIdentifierToken();
				return base.AddError<SyntaxToken>(syntaxToken, code);
			}
			if (this.IsCurrentTokenPartialKeywordOfPartialMethodOrType() || this.IsCurrentTokenQueryKeywordInQuery())
			{
				SyntaxToken syntaxToken2 = LanguageParser.CreateMissingIdentifierToken();
				return base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_InvalidExprTerm, new object[] { base.CurrentToken.Text });
			}
			SyntaxToken syntaxToken3 = base.EatToken();
			if (this.IsInAsync && syntaxToken3.ContextualKind == SyntaxKind.AwaitKeyword)
			{
				syntaxToken3 = base.AddError<SyntaxToken>(syntaxToken3, ErrorCode.ERR_BadAwaitAsIdentifier);
			}
			return syntaxToken3;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x0012550B File Offset: 0x0012370B
		private bool IsCurrentTokenQueryKeywordInQuery()
		{
			return this.IsInQuery && this.IsCurrentTokenQueryContextualKeyword;
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x0012551D File Offset: 0x0012371D
		private bool IsCurrentTokenPartialKeywordOfPartialMethodOrType()
		{
			return base.CurrentToken.ContextualKind == SyntaxKind.PartialKeyword && (this.IsPartialType() || this.IsPartialMember());
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00125544 File Offset: 0x00123744
		private TypeParameterListSyntax ParseTypeParameterList()
		{
			if (base.CurrentToken.Kind != SyntaxKind.LessThanToken)
			{
				return null;
			}
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfTypeParameterList;
			TypeParameterListSyntax typeParameterListSyntax;
			try
			{
				SeparatedSyntaxListBuilder<TypeParameterSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<TypeParameterSyntax>();
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.LessThanToken);
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureGenerics, false);
				separatedSyntaxListBuilder.Add(this.ParseTypeParameter());
				while (base.CurrentToken.Kind != SyntaxKind.GreaterThanToken && !this.IsCurrentTokenWhereOfConstraintClause())
				{
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
					{
						separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						separatedSyntaxListBuilder.Add(this.ParseTypeParameter());
					}
					else if (this.SkipBadTypeParameterListTokens(separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
					{
						break;
					}
				}
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.GreaterThanToken);
				typeParameterListSyntax = this._syntaxFactory.TypeParameterList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._termState = termState;
			}
			return typeParameterListSyntax;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x00125650 File Offset: 0x00123850
		private LanguageParser.PostSkipAction SkipBadTypeParameterListTokens(SeparatedSyntaxListBuilder<TypeParameterSyntax> list, SyntaxKind expected)
		{
			CSharpSyntaxNode csharpSyntaxNode = null;
			return this.SkipBadSeparatedListTokensWithExpectedKind<CSharpSyntaxNode, TypeParameterSyntax>(ref csharpSyntaxNode, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken, (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.GreaterThanToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x001256A8 File Offset: 0x001238A8
		private TypeParameterSyntax ParseTypeParameter()
		{
			if (this.IsCurrentTokenWhereOfConstraintClause())
			{
				return this._syntaxFactory.TypeParameter(default(SyntaxList<AttributeListSyntax>), null, base.AddError<SyntaxToken>(LanguageParser.CreateMissingIdentifierToken(), ErrorCode.ERR_IdentifierExpected));
			}
			SyntaxList<AttributeListSyntax> syntaxList = default(SyntaxList<AttributeListSyntax>);
			if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken && base.PeekToken(1).Kind != SyntaxKind.CloseBracketToken)
			{
				LanguageParser.TerminatorState termState = this._termState;
				this._termState = LanguageParser.TerminatorState.IsEndOfTypeArgumentList;
				syntaxList = this.ParseAttributeDeclarations();
				this._termState = termState;
			}
			SyntaxToken syntaxToken = null;
			if (base.CurrentToken.Kind == SyntaxKind.InKeyword || base.CurrentToken.Kind == SyntaxKind.OutKeyword)
			{
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(base.EatToken(), MessageID.IDS_FeatureTypeVariance, false);
			}
			return this._syntaxFactory.TypeParameter(syntaxList, syntaxToken, this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected));
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x00125784 File Offset: 0x00123984
		private SimpleNameSyntax ParseSimpleName(LanguageParser.NameOptions options = LanguageParser.NameOptions.None)
		{
			IdentifierNameSyntax identifierNameSyntax = this.ParseIdentifierName(ErrorCode.ERR_IdentifierExpected);
			if (identifierNameSyntax.Identifier.IsMissing)
			{
				return identifierNameSyntax;
			}
			SimpleNameSyntax simpleNameSyntax = identifierNameSyntax;
			if (base.CurrentToken.Kind == SyntaxKind.LessThanToken)
			{
				LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
				LanguageParser.ScanTypeArgumentListKind scanTypeArgumentListKind = this.ScanTypeArgumentList(options);
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
				if (scanTypeArgumentListKind == LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList || (scanTypeArgumentListKind == LanguageParser.ScanTypeArgumentListKind.PossibleTypeArgumentList && (options & LanguageParser.NameOptions.InTypeList) != LanguageParser.NameOptions.None))
				{
					SeparatedSyntaxListBuilder<TypeSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<TypeSyntax>();
					SyntaxToken syntaxToken;
					SyntaxToken syntaxToken2;
					this.ParseTypeArgumentList(out syntaxToken, separatedSyntaxListBuilder, out syntaxToken2);
					simpleNameSyntax = this._syntaxFactory.GenericName(identifierNameSyntax.Identifier, this._syntaxFactory.TypeArgumentList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2));
					this._pool.Free<TypeSyntax>(in separatedSyntaxListBuilder);
				}
			}
			return simpleNameSyntax;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x0012583C File Offset: 0x00123A3C
		private LanguageParser.ScanTypeArgumentListKind ScanTypeArgumentList(LanguageParser.NameOptions options)
		{
			if (base.CurrentToken.Kind != SyntaxKind.LessThanToken)
			{
				return LanguageParser.ScanTypeArgumentListKind.NotTypeArgumentList;
			}
			if ((options & LanguageParser.NameOptions.InExpression) == LanguageParser.NameOptions.None)
			{
				return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
			}
			SyntaxToken syntaxToken = null;
			bool flag;
			if (this.ScanPossibleTypeArgumentList(ref syntaxToken, out flag) == LanguageParser.ScanTypeFlags.NotType)
			{
				return LanguageParser.ScanTypeArgumentListKind.NotTypeArgumentList;
			}
			if (flag)
			{
				return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
			}
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.GreaterThanEqualsToken)
			{
				switch (kind)
				{
				case SyntaxKind.CaretToken:
				case SyntaxKind.OpenParenToken:
				case SyntaxKind.CloseParenToken:
				case SyntaxKind.CloseBraceToken:
				case SyntaxKind.CloseBracketToken:
				case SyntaxKind.BarToken:
				case SyntaxKind.ColonToken:
				case SyntaxKind.SemicolonToken:
				case SyntaxKind.CommaToken:
				case SyntaxKind.DotToken:
				case SyntaxKind.QuestionToken:
					break;
				case SyntaxKind.AmpersandToken:
				case SyntaxKind.OpenBracketToken:
				case SyntaxKind.LessThanToken:
					return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
				case SyntaxKind.AsteriskToken:
				case SyntaxKind.MinusToken:
				case SyntaxKind.PlusToken:
				case SyntaxKind.EqualsToken:
				case SyntaxKind.BackslashToken:
				case SyntaxKind.DoubleQuoteToken:
				case SyntaxKind.SingleQuoteToken:
					return LanguageParser.ScanTypeArgumentListKind.PossibleTypeArgumentList;
				case SyntaxKind.OpenBraceToken:
					return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
				case SyntaxKind.GreaterThanToken:
					if ((options & LanguageParser.NameOptions.AfterIs) != LanguageParser.NameOptions.None && base.PeekToken(1).Kind != SyntaxKind.GreaterThanToken)
					{
						return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
					}
					return LanguageParser.ScanTypeArgumentListKind.PossibleTypeArgumentList;
				default:
					switch (kind)
					{
					case SyntaxKind.BarBarToken:
					case SyntaxKind.AmpersandAmpersandToken:
					case SyntaxKind.LessThanEqualsToken:
						return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
					case SyntaxKind.MinusMinusToken:
					case SyntaxKind.PlusPlusToken:
					case SyntaxKind.ColonColonToken:
					case SyntaxKind.QuestionQuestionToken:
					case SyntaxKind.MinusGreaterThanToken:
						return LanguageParser.ScanTypeArgumentListKind.PossibleTypeArgumentList;
					case SyntaxKind.ExclamationEqualsToken:
					case SyntaxKind.EqualsEqualsToken:
						break;
					case SyntaxKind.EqualsGreaterThanToken:
						return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
					default:
						if (kind != SyntaxKind.GreaterThanEqualsToken)
						{
							return LanguageParser.ScanTypeArgumentListKind.PossibleTypeArgumentList;
						}
						return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
					}
					break;
				}
				return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
			}
			if (kind - SyntaxKind.IsKeyword > 1)
			{
				if (kind == SyntaxKind.EndOfFileToken)
				{
					return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
				}
				if (kind != SyntaxKind.IdentifierToken)
				{
					return LanguageParser.ScanTypeArgumentListKind.PossibleTypeArgumentList;
				}
				if ((options & (LanguageParser.NameOptions.AfterIs | LanguageParser.NameOptions.DefinitePattern | LanguageParser.NameOptions.AfterOut)) != LanguageParser.NameOptions.None || ((options & LanguageParser.NameOptions.AfterTupleComma) != LanguageParser.NameOptions.None && (base.PeekToken(1).Kind == SyntaxKind.CommaToken || base.PeekToken(1).Kind == SyntaxKind.CloseParenToken)) || ((options & LanguageParser.NameOptions.FirstElementOfPossibleTupleLiteral) != LanguageParser.NameOptions.None && base.PeekToken(1).Kind == SyntaxKind.CommaToken))
				{
					return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
				}
				return LanguageParser.ScanTypeArgumentListKind.PossibleTypeArgumentList;
			}
			return LanguageParser.ScanTypeArgumentListKind.DefiniteTypeArgumentList;
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x001259D0 File Offset: 0x00123BD0
		private LanguageParser.ScanTypeFlags ScanPossibleTypeArgumentList(ref SyntaxToken lastTokenOfList, out bool isDefinitelyTypeArgumentList)
		{
			isDefinitelyTypeArgumentList = false;
			if (base.CurrentToken.Kind != SyntaxKind.LessThanToken)
			{
				return LanguageParser.ScanTypeFlags.NonGenericTypeOrExpression;
			}
			LanguageParser.ScanTypeFlags scanTypeFlags = LanguageParser.ScanTypeFlags.GenericTypeOrExpression;
			for (;;)
			{
				lastTokenOfList = base.EatToken();
				if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
				{
					break;
				}
				if (base.CurrentToken.Kind == SyntaxKind.GreaterThanToken)
				{
					goto Block_3;
				}
				switch (this.ScanType(out lastTokenOfList, false))
				{
				case LanguageParser.ScanTypeFlags.NotType:
					goto IL_0082;
				case LanguageParser.ScanTypeFlags.MustBeType:
					isDefinitelyTypeArgumentList = this.DetermineIfDefinitelyTypeArgumentList(isDefinitelyTypeArgumentList);
					scanTypeFlags = LanguageParser.ScanTypeFlags.GenericTypeOrMethod;
					break;
				case LanguageParser.ScanTypeFlags.GenericTypeOrMethod:
					scanTypeFlags = LanguageParser.ScanTypeFlags.GenericTypeOrMethod;
					break;
				case LanguageParser.ScanTypeFlags.GenericTypeOrExpression:
					if (!isDefinitelyTypeArgumentList)
					{
						isDefinitelyTypeArgumentList = base.CurrentToken.Kind == SyntaxKind.CommaToken;
						scanTypeFlags = LanguageParser.ScanTypeFlags.GenericTypeOrMethod;
					}
					break;
				case LanguageParser.ScanTypeFlags.NullableType:
					isDefinitelyTypeArgumentList = this.DetermineIfDefinitelyTypeArgumentList(isDefinitelyTypeArgumentList);
					if (isDefinitelyTypeArgumentList)
					{
						scanTypeFlags = LanguageParser.ScanTypeFlags.GenericTypeOrMethod;
					}
					break;
				}
				if (base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					goto Block_7;
				}
			}
			lastTokenOfList = null;
			return LanguageParser.ScanTypeFlags.NotType;
			Block_3:
			lastTokenOfList = base.EatToken();
			return scanTypeFlags;
			IL_0082:
			lastTokenOfList = null;
			return LanguageParser.ScanTypeFlags.NotType;
			Block_7:
			if (base.CurrentToken.Kind != SyntaxKind.GreaterThanToken)
			{
				lastTokenOfList = null;
				return LanguageParser.ScanTypeFlags.NotType;
			}
			lastTokenOfList = base.EatToken();
			return scanTypeFlags;
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x00125AD9 File Offset: 0x00123CD9
		private bool DetermineIfDefinitelyTypeArgumentList(bool isDefinitelyTypeArgumentList)
		{
			if (!isDefinitelyTypeArgumentList)
			{
				isDefinitelyTypeArgumentList = base.CurrentToken.Kind == SyntaxKind.CommaToken || base.CurrentToken.Kind == SyntaxKind.GreaterThanToken;
			}
			return isDefinitelyTypeArgumentList;
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x00125B08 File Offset: 0x00123D08
		private void ParseTypeArgumentList(out SyntaxToken open, SeparatedSyntaxListBuilder<TypeSyntax> types, out SyntaxToken close)
		{
			open = base.EatToken(SyntaxKind.LessThanToken);
			open = base.CheckFeatureAvailability<SyntaxToken>(open, MessageID.IDS_FeatureGenerics, false);
			if (this.IsOpenName())
			{
				OmittedTypeArgumentSyntax omittedTypeArgumentSyntax = this._syntaxFactory.OmittedTypeArgument(SyntaxFactory.Token(SyntaxKind.OmittedTypeArgumentToken));
				types.Add(omittedTypeArgumentSyntax);
				while (base.CurrentToken.Kind == SyntaxKind.CommaToken)
				{
					types.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
					types.Add(omittedTypeArgumentSyntax);
				}
				close = base.EatToken(SyntaxKind.GreaterThanToken);
				return;
			}
			types.Add(this.ParseTypeArgument());
			while (base.CurrentToken.Kind != SyntaxKind.GreaterThanToken)
			{
				if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleType())
				{
					types.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
					types.Add(this.ParseTypeArgument());
				}
				else if (this.SkipBadTypeArgumentListTokens(types, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
				{
					break;
				}
			}
			close = base.EatToken(SyntaxKind.GreaterThanToken);
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x00125C10 File Offset: 0x00123E10
		private LanguageParser.PostSkipAction SkipBadTypeArgumentListTokens(SeparatedSyntaxListBuilder<TypeSyntax> list, SyntaxKind expected)
		{
			CSharpSyntaxNode csharpSyntaxNode = null;
			return this.SkipBadSeparatedListTokensWithExpectedKind<CSharpSyntaxNode, TypeSyntax>(ref csharpSyntaxNode, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleType(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.GreaterThanToken || p.IsTerminator(), expected);
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x00125C68 File Offset: 0x00123E68
		private TypeSyntax ParseTypeArgument()
		{
			SyntaxList<AttributeListSyntax> syntaxList = default(SyntaxList<AttributeListSyntax>);
			if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken && base.PeekToken(1).Kind != SyntaxKind.CloseBracketToken)
			{
				LanguageParser.TerminatorState termState = this._termState;
				this._termState = LanguageParser.TerminatorState.IsEndOfTypeArgumentList;
				syntaxList = this.ParseAttributeDeclarations();
				this._termState = termState;
			}
			SyntaxToken syntaxToken = null;
			if (base.CurrentToken.Kind == SyntaxKind.InKeyword || base.CurrentToken.Kind == SyntaxKind.OutKeyword)
			{
				syntaxToken = base.EatToken();
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureTypeVariance, false);
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_IllegalVarianceSyntax);
			}
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			SyntaxKind kind;
			if (typeSyntax.IsMissing && base.CurrentToken.Kind != SyntaxKind.CommaToken && base.CurrentToken.Kind != SyntaxKind.GreaterThanToken && ((kind = base.PeekToken(1).Kind) == SyntaxKind.CommaToken || kind == SyntaxKind.GreaterThanToken))
			{
				typeSyntax = base.AddTrailingSkippedSyntax<TypeSyntax>(typeSyntax, base.EatToken());
			}
			if (syntaxToken != null)
			{
				typeSyntax = base.AddLeadingSkippedSyntax<TypeSyntax>(typeSyntax, syntaxToken);
			}
			if (syntaxList.Count > 0)
			{
				typeSyntax = base.AddLeadingSkippedSyntax<TypeSyntax>(typeSyntax, syntaxList.Node);
				typeSyntax = base.AddError<TypeSyntax>(typeSyntax, ErrorCode.ERR_TypeExpected);
			}
			return typeSyntax;
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x00125DA0 File Offset: 0x00123FA0
		private bool IsEndOfTypeArgumentList()
		{
			return base.CurrentToken.Kind == SyntaxKind.GreaterThanToken;
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x00125DB4 File Offset: 0x00123FB4
		private bool IsOpenName()
		{
			bool flag = true;
			int num = 0;
			while (base.PeekToken(num).Kind == SyntaxKind.CommaToken)
			{
				num++;
			}
			if (base.PeekToken(num).Kind != SyntaxKind.GreaterThanToken)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x00125DF4 File Offset: 0x00123FF4
		private void ParseMemberName(out ExplicitInterfaceSpecifierSyntax explicitInterfaceOpt, out SyntaxToken identifierOrThisOpt, out TypeParameterListSyntax typeParameterListOpt, bool isEvent)
		{
			identifierOrThisOpt = null;
			explicitInterfaceOpt = null;
			typeParameterListOpt = null;
			if (!this.IsPossibleMemberName())
			{
				return;
			}
			NameSyntax nameSyntax = null;
			SyntaxToken syntaxToken = null;
			LanguageParser.ResetPoint resetPoint = default(LanguageParser.ResetPoint);
			bool flag = false;
			try
			{
				while (base.CurrentToken.Kind != SyntaxKind.ThisKeyword)
				{
					LanguageParser.ResetPoint resetPoint2 = this.GetResetPoint();
					bool flag2;
					try
					{
						this.ScanNamedTypePart();
						flag2 = !this.IsDotOrColonColon();
					}
					finally
					{
						this.Reset(ref resetPoint2);
						this.Release(ref resetPoint2);
					}
					if (flag2)
					{
						resetPoint = this.GetResetPoint();
						flag = true;
						if (syntaxToken != null && syntaxToken.Kind == SyntaxKind.ColonColonToken)
						{
							syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_AliasQualAsExpression);
							syntaxToken = this.ConvertToMissingWithTrailingTrivia(syntaxToken, SyntaxKind.DotToken);
						}
						identifierOrThisOpt = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
						typeParameterListOpt = this.ParseTypeParameterList();
						IL_0162:
						if (nameSyntax != null)
						{
							if (syntaxToken.Kind != SyntaxKind.DotToken)
							{
								syntaxToken = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken, new DiagnosticInfo[] { this.GetExpectedTokenError(SyntaxKind.DotToken, syntaxToken.Kind, syntaxToken.GetLeadingTriviaWidth(), syntaxToken.Width) });
								syntaxToken = this.ConvertToMissingWithTrailingTrivia(syntaxToken, SyntaxKind.DotToken);
							}
							if (isEvent && base.CurrentToken.Kind != SyntaxKind.OpenBraceToken && base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
							{
								explicitInterfaceOpt = this._syntaxFactory.ExplicitInterfaceSpecifier(nameSyntax, base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_ExplicitEventFieldImpl));
								if (syntaxToken.TrailingTrivia.Any(8539))
								{
									this.Reset(ref resetPoint);
									identifierOrThisOpt = null;
									typeParameterListOpt = null;
								}
							}
							else
							{
								explicitInterfaceOpt = this._syntaxFactory.ExplicitInterfaceSpecifier(nameSyntax, syntaxToken);
							}
						}
						return;
					}
					LanguageParser.TerminatorState termState = this._termState;
					this._termState |= LanguageParser.TerminatorState.IsEndOfNameInExplicitInterface;
					if (nameSyntax == null)
					{
						nameSyntax = this.ParseSimpleName(LanguageParser.NameOptions.InTypeList);
						syntaxToken = ((base.CurrentToken.Kind == SyntaxKind.ColonColonToken) ? base.EatToken() : base.EatToken(SyntaxKind.DotToken));
					}
					else
					{
						nameSyntax = this.ParseQualifiedNameRight(LanguageParser.NameOptions.InTypeList, nameSyntax, syntaxToken);
						syntaxToken = ((base.CurrentToken.Kind == SyntaxKind.ColonColonToken) ? this.ConvertToMissingWithTrailingTrivia(base.EatToken(), SyntaxKind.DotToken) : base.EatToken(SyntaxKind.DotToken));
					}
					this._termState = termState;
				}
				resetPoint = this.GetResetPoint();
				flag = true;
				identifierOrThisOpt = base.EatToken();
				typeParameterListOpt = this.ParseTypeParameterList();
				goto IL_0162;
			}
			finally
			{
				if (flag)
				{
					this.Release(ref resetPoint);
				}
			}
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x00126068 File Offset: 0x00124268
		private NameSyntax ParseAliasQualifiedName(LanguageParser.NameOptions allowedParts = LanguageParser.NameOptions.None)
		{
			NameSyntax nameSyntax = this.ParseSimpleName(allowedParts);
			if (base.CurrentToken.Kind == SyntaxKind.ColonColonToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				nameSyntax = this.ParseQualifiedNameRight(allowedParts, nameSyntax, syntaxToken);
			}
			return nameSyntax;
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x001260A4 File Offset: 0x001242A4
		private NameSyntax ParseQualifiedName(LanguageParser.NameOptions options = LanguageParser.NameOptions.None)
		{
			NameSyntax nameSyntax = this.ParseAliasQualifiedName(options);
			while ((this.IsDotOrColonColon() || base.CurrentToken.Kind == SyntaxKind.DotDotToken) && base.PeekToken(1).Kind != SyntaxKind.ThisKeyword)
			{
				SyntaxToken syntaxToken = base.EatToken();
				nameSyntax = this.ParseQualifiedNameRight(options, nameSyntax, syntaxToken);
			}
			return nameSyntax;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x001260FC File Offset: 0x001242FC
		private NameSyntax ParseQualifiedNameRight(LanguageParser.NameOptions options, NameSyntax left, SyntaxToken separator)
		{
			SimpleNameSyntax simpleNameSyntax = this.ParseSimpleName(options);
			SyntaxKind kind = separator.Kind;
			if (kind == SyntaxKind.DotToken)
			{
				return this._syntaxFactory.QualifiedName(left, separator, simpleNameSyntax);
			}
			if (kind == SyntaxKind.DotDotToken)
			{
				SyntaxToken syntaxToken = SyntaxFactory.Token(separator.LeadingTrivia.Node, SyntaxKind.DotToken, null);
				IdentifierNameSyntax identifierNameSyntax = base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_IdentifierExpected);
				SyntaxToken syntaxToken2 = SyntaxFactory.Token(null, SyntaxKind.DotToken, separator.TrailingTrivia.Node);
				return this._syntaxFactory.QualifiedName(this._syntaxFactory.QualifiedName(left, syntaxToken, identifierNameSyntax), syntaxToken2, simpleNameSyntax);
			}
			if (kind != SyntaxKind.ColonColonToken)
			{
				throw ExceptionUtilities.Unreachable;
			}
			if (left.Kind != SyntaxKind.IdentifierName)
			{
				separator = base.AddError<SyntaxToken>(separator, ErrorCode.ERR_UnexpectedAliasedName, new object[] { separator.ToString() });
			}
			IdentifierNameSyntax identifierNameSyntax2 = left as IdentifierNameSyntax;
			if (identifierNameSyntax2 == null)
			{
				separator = this.ConvertToMissingWithTrailingTrivia(separator, SyntaxKind.DotToken);
				return this._syntaxFactory.QualifiedName(left, separator, simpleNameSyntax);
			}
			if (identifierNameSyntax2.Identifier.ContextualKind == SyntaxKind.GlobalKeyword)
			{
				identifierNameSyntax2 = this._syntaxFactory.IdentifierName(SyntaxParser.ConvertToKeyword(identifierNameSyntax2.Identifier));
			}
			identifierNameSyntax2 = base.CheckFeatureAvailability<IdentifierNameSyntax>(identifierNameSyntax2, MessageID.IDS_FeatureGlobalNamespace, false);
			return this.WithAdditionalDiagnostics<AliasQualifiedNameSyntax>(this._syntaxFactory.AliasQualifiedName(identifierNameSyntax2, separator, simpleNameSyntax), left.GetDiagnostics());
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0012625C File Offset: 0x0012445C
		private SyntaxToken ConvertToMissingWithTrailingTrivia(SyntaxToken token, SyntaxKind expectedKind)
		{
			SyntaxToken syntaxToken = SyntaxFactory.MissingToken(expectedKind);
			return base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken, token);
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0012627A File Offset: 0x0012447A
		private bool IsPossibleType()
		{
			return LanguageParser.IsPredefinedType(base.CurrentToken.Kind) || this.IsTrueIdentifier();
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x00126298 File Offset: 0x00124498
		private LanguageParser.ScanTypeFlags ScanType(bool forPattern = false)
		{
			SyntaxToken syntaxToken;
			return this.ScanType(out syntaxToken, forPattern);
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x001262AE File Offset: 0x001244AE
		private LanguageParser.ScanTypeFlags ScanType(out SyntaxToken lastTokenOfType, bool forPattern = false)
		{
			return this.ScanType(forPattern ? LanguageParser.ParseTypeMode.DefinitePattern : LanguageParser.ParseTypeMode.Normal, out lastTokenOfType);
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x001262C0 File Offset: 0x001244C0
		private void ScanNamedTypePart()
		{
			SyntaxToken syntaxToken;
			this.ScanNamedTypePart(out syntaxToken);
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x001262D8 File Offset: 0x001244D8
		private LanguageParser.ScanTypeFlags ScanNamedTypePart(out SyntaxToken lastTokenOfType)
		{
			if (base.CurrentToken.Kind != SyntaxKind.IdentifierToken || !this.IsTrueIdentifier())
			{
				lastTokenOfType = null;
				return LanguageParser.ScanTypeFlags.NotType;
			}
			lastTokenOfType = base.EatToken();
			if (base.CurrentToken.Kind == SyntaxKind.LessThanToken)
			{
				bool flag;
				return this.ScanPossibleTypeArgumentList(ref lastTokenOfType, out flag);
			}
			return LanguageParser.ScanTypeFlags.NonGenericTypeOrExpression;
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0012632C File Offset: 0x0012452C
		private LanguageParser.ScanTypeFlags ScanType(LanguageParser.ParseTypeMode mode, out SyntaxToken lastTokenOfType)
		{
			if (base.CurrentToken.Kind == SyntaxKind.RefKeyword)
			{
				base.EatToken();
				if (base.CurrentToken.Kind == SyntaxKind.ReadOnlyKeyword)
				{
					base.EatToken();
				}
			}
			SyntaxKind syntaxKind = base.CurrentToken.Kind;
			LanguageParser.ScanTypeFlags scanTypeFlags;
			if (syntaxKind == SyntaxKind.IdentifierToken || syntaxKind == SyntaxKind.ColonColonToken)
			{
				bool flag;
				if (base.CurrentToken.Kind == SyntaxKind.ColonColonToken)
				{
					scanTypeFlags = LanguageParser.ScanTypeFlags.NonGenericTypeOrExpression;
					flag = true;
					lastTokenOfType = null;
				}
				else
				{
					flag = base.PeekToken(1).Kind == SyntaxKind.ColonColonToken;
					scanTypeFlags = this.ScanNamedTypePart(out lastTokenOfType);
					if (scanTypeFlags == LanguageParser.ScanTypeFlags.NotType)
					{
						return LanguageParser.ScanTypeFlags.NotType;
					}
				}
				bool flag2 = true;
				while (this.IsDotOrColonColon())
				{
					if (!flag2)
					{
						flag = false;
					}
					base.EatToken();
					scanTypeFlags = this.ScanNamedTypePart(out lastTokenOfType);
					if (scanTypeFlags == LanguageParser.ScanTypeFlags.NotType)
					{
						return LanguageParser.ScanTypeFlags.NotType;
					}
					flag2 = false;
				}
				if (flag)
				{
					scanTypeFlags = LanguageParser.ScanTypeFlags.AliasQualifiedName;
				}
			}
			else if (LanguageParser.IsPredefinedType(base.CurrentToken.Kind))
			{
				lastTokenOfType = base.EatToken();
				scanTypeFlags = LanguageParser.ScanTypeFlags.MustBeType;
			}
			else if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				lastTokenOfType = base.EatToken();
				scanTypeFlags = this.ScanTupleType(out lastTokenOfType);
				if (scanTypeFlags == LanguageParser.ScanTypeFlags.NotType || (mode == LanguageParser.ParseTypeMode.DefinitePattern && base.CurrentToken.Kind != SyntaxKind.OpenBracketToken))
				{
					return LanguageParser.ScanTypeFlags.NotType;
				}
			}
			else
			{
				if (!this.IsFunctionPointerStart())
				{
					lastTokenOfType = null;
					return LanguageParser.ScanTypeFlags.NotType;
				}
				scanTypeFlags = this.ScanFunctionPointerType(out lastTokenOfType);
			}
			int num = -1;
			while (base.IsMakingProgress(ref num, true))
			{
				syntaxKind = base.CurrentToken.Kind;
				if (syntaxKind != SyntaxKind.AsteriskToken)
				{
					if (syntaxKind != SyntaxKind.OpenBracketToken)
					{
						if (syntaxKind != SyntaxKind.QuestionToken || lastTokenOfType.Kind == SyntaxKind.QuestionToken || lastTokenOfType.Kind == SyntaxKind.AsteriskToken)
						{
							break;
						}
						lastTokenOfType = base.EatToken();
						scanTypeFlags = LanguageParser.ScanTypeFlags.NullableType;
					}
					else
					{
						base.EatToken();
						while (base.CurrentToken.Kind == SyntaxKind.CommaToken)
						{
							base.EatToken();
						}
						if (base.CurrentToken.Kind != SyntaxKind.CloseBracketToken)
						{
							lastTokenOfType = null;
							return LanguageParser.ScanTypeFlags.NotType;
						}
						lastTokenOfType = base.EatToken();
						scanTypeFlags = LanguageParser.ScanTypeFlags.MustBeType;
					}
				}
				else
				{
					if (lastTokenOfType.Kind == SyntaxKind.CloseBracketToken || mode == LanguageParser.ParseTypeMode.DefinitePattern || ((mode == LanguageParser.ParseTypeMode.AfterTupleComma || mode == LanguageParser.ParseTypeMode.FirstElementOfPossibleTupleLiteral) && !this.PointerTypeModsFollowedByRankAndDimensionSpecifier()))
					{
						break;
					}
					lastTokenOfType = base.EatToken();
					if (scanTypeFlags == LanguageParser.ScanTypeFlags.GenericTypeOrExpression || scanTypeFlags == LanguageParser.ScanTypeFlags.NonGenericTypeOrExpression)
					{
						scanTypeFlags = LanguageParser.ScanTypeFlags.PointerOrMultiplication;
					}
					else if (scanTypeFlags == LanguageParser.ScanTypeFlags.GenericTypeOrMethod)
					{
						scanTypeFlags = LanguageParser.ScanTypeFlags.MustBeType;
					}
				}
			}
			return scanTypeFlags;
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x0012655C File Offset: 0x0012475C
		private LanguageParser.ScanTypeFlags ScanTupleType(out SyntaxToken lastTokenOfType)
		{
			if (this.ScanType(out lastTokenOfType, false) != LanguageParser.ScanTypeFlags.NotType)
			{
				if (this.IsTrueIdentifier())
				{
					lastTokenOfType = base.EatToken();
				}
				if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
				{
					for (;;)
					{
						lastTokenOfType = base.EatToken();
						if (this.ScanType(out lastTokenOfType, false) == LanguageParser.ScanTypeFlags.NotType)
						{
							break;
						}
						if (this.IsTrueIdentifier())
						{
							lastTokenOfType = base.EatToken();
						}
						if (base.CurrentToken.Kind != SyntaxKind.CommaToken)
						{
							goto Block_5;
						}
					}
					lastTokenOfType = base.EatToken();
					return LanguageParser.ScanTypeFlags.NotType;
					Block_5:
					if (base.CurrentToken.Kind == SyntaxKind.CloseParenToken)
					{
						lastTokenOfType = base.EatToken();
						return LanguageParser.ScanTypeFlags.TupleType;
					}
				}
			}
			lastTokenOfType = null;
			return LanguageParser.ScanTypeFlags.NotType;
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x001265F4 File Offset: 0x001247F4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private LanguageParser.ScanTypeFlags ScanFunctionPointerType(out SyntaxToken lastTokenOfType)
		{
			base.EatToken(SyntaxKind.DelegateKeyword);
			lastTokenOfType = base.EatToken(SyntaxKind.AsteriskToken);
			LanguageParser.TerminatorState terminatorState;
			if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken)
			{
				SyntaxToken syntaxToken = base.PeekToken(1);
				SyntaxToken currentToken = base.CurrentToken;
				if (currentToken != null)
				{
					SyntaxKind contextualKind = currentToken.ContextualKind;
					if (contextualKind - SyntaxKind.ManagedKeyword <= 1)
					{
						goto IL_006B;
					}
				}
				if (!LanguageParser.IsPossibleFunctionPointerParameterListStart(syntaxToken) && syntaxToken.Kind != SyntaxKind.OpenBracketToken)
				{
					return LanguageParser.ScanTypeFlags.MustBeType;
				}
				IL_006B:
				lastTokenOfType = base.EatToken();
				if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
				{
					lastTokenOfType = base.EatToken(SyntaxKind.OpenBracketToken);
					terminatorState = this._termState;
					this._termState |= LanguageParser.TerminatorState.IsEndOfFunctionPointerCallingConvention;
					try
					{
						for (;;)
						{
							lastTokenOfType = base.TryEatToken(SyntaxKind.IdentifierToken) ?? lastTokenOfType;
							if (this.<ScanFunctionPointerType>g__skipBadFunctionPointerTokens|199_0() == LanguageParser.PostSkipAction.Abort)
							{
								break;
							}
							lastTokenOfType = base.EatToken();
						}
						lastTokenOfType = base.TryEatToken(SyntaxKind.CloseBracketToken) ?? lastTokenOfType;
					}
					finally
					{
						this._termState = terminatorState;
					}
				}
			}
			if (!LanguageParser.IsPossibleFunctionPointerParameterListStart(base.CurrentToken))
			{
				return LanguageParser.ScanTypeFlags.MustBeType;
			}
			bool flag = base.EatToken().Kind == SyntaxKind.LessThanToken;
			terminatorState = this._termState;
			this._termState |= (flag ? LanguageParser.TerminatorState.IsEndOfFunctionPointerParameterList : LanguageParser.TerminatorState.IsEndOfFunctionPointerParameterListErrored);
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
			try
			{
				for (;;)
				{
					this.ParseParameterModifiers(syntaxListBuilder, true);
					syntaxListBuilder.Clear();
					SyntaxToken currentToken;
					this.ScanType(out currentToken, false);
					if (this.<ScanFunctionPointerType>g__skipBadFunctionPointerTokens|199_0() == LanguageParser.PostSkipAction.Abort)
					{
						break;
					}
					base.EatToken(SyntaxKind.CommaToken);
				}
			}
			finally
			{
				this._termState = terminatorState;
				this._pool.Free(syntaxListBuilder);
			}
			if (!flag && base.CurrentToken.Kind == SyntaxKind.CloseParenToken)
			{
				lastTokenOfType = base.EatTokenAsKind(SyntaxKind.GreaterThanToken);
			}
			else
			{
				lastTokenOfType = base.EatToken(SyntaxKind.GreaterThanToken);
			}
			return LanguageParser.ScanTypeFlags.MustBeType;
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x001267E4 File Offset: 0x001249E4
		private static bool IsPredefinedType(SyntaxKind keyword)
		{
			return SyntaxFacts.IsPredefinedType(keyword);
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x001267EC File Offset: 0x001249EC
		public TypeSyntax ParseTypeName()
		{
			return this.ParseType(LanguageParser.ParseTypeMode.Normal);
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x001267F5 File Offset: 0x001249F5
		private TypeSyntax ParseTypeOrVoid()
		{
			if (base.CurrentToken.Kind == SyntaxKind.VoidKeyword && base.PeekToken(1).Kind != SyntaxKind.AsteriskToken)
			{
				return this._syntaxFactory.PredefinedType(base.EatToken());
			}
			return this.ParseType(LanguageParser.ParseTypeMode.Normal);
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x00126838 File Offset: 0x00124A38
		private TypeSyntax ParseType(LanguageParser.ParseTypeMode mode = LanguageParser.ParseTypeMode.Normal)
		{
			if (base.CurrentToken.Kind == SyntaxKind.RefKeyword)
			{
				SyntaxToken syntaxToken = base.EatToken();
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureRefLocalsReturns, false);
				SyntaxToken syntaxToken2 = null;
				if (base.CurrentToken.Kind == SyntaxKind.ReadOnlyKeyword)
				{
					syntaxToken2 = base.EatToken();
					syntaxToken2 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken2, MessageID.IDS_FeatureReadOnlyReferences, false);
				}
				TypeSyntax typeSyntax = this.ParseTypeCore(LanguageParser.ParseTypeMode.AfterRef);
				return this._syntaxFactory.RefType(syntaxToken, syntaxToken2, typeSyntax);
			}
			return this.ParseTypeCore(mode);
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x001268B4 File Offset: 0x00124AB4
		private TypeSyntax ParseTypeCore(LanguageParser.ParseTypeMode mode)
		{
			LanguageParser.<>c__DisplayClass205_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.mode = mode;
			LanguageParser.NameOptions nameOptions;
			switch (CS$<>8__locals1.mode)
			{
			case LanguageParser.ParseTypeMode.Normal:
			case LanguageParser.ParseTypeMode.Parameter:
			case LanguageParser.ParseTypeMode.AfterRef:
			case LanguageParser.ParseTypeMode.AsExpression:
			case LanguageParser.ParseTypeMode.NewExpression:
				nameOptions = LanguageParser.NameOptions.None;
				break;
			case LanguageParser.ParseTypeMode.AfterIs:
				nameOptions = LanguageParser.NameOptions.InExpression | LanguageParser.NameOptions.PossiblePattern | LanguageParser.NameOptions.AfterIs;
				break;
			case LanguageParser.ParseTypeMode.DefinitePattern:
				nameOptions = LanguageParser.NameOptions.InExpression | LanguageParser.NameOptions.PossiblePattern | LanguageParser.NameOptions.DefinitePattern;
				break;
			case LanguageParser.ParseTypeMode.AfterOut:
				nameOptions = LanguageParser.NameOptions.InExpression | LanguageParser.NameOptions.AfterOut;
				break;
			case LanguageParser.ParseTypeMode.AfterTupleComma:
				nameOptions = LanguageParser.NameOptions.InExpression | LanguageParser.NameOptions.AfterTupleComma;
				break;
			case LanguageParser.ParseTypeMode.FirstElementOfPossibleTupleLiteral:
				nameOptions = LanguageParser.NameOptions.InExpression | LanguageParser.NameOptions.FirstElementOfPossibleTupleLiteral;
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.mode);
			}
			CS$<>8__locals1.type = this.ParseUnderlyingType(CS$<>8__locals1.mode, nameOptions);
			int num = -1;
			while (base.IsMakingProgress(ref num, true))
			{
				SyntaxKind kind = base.CurrentToken.Kind;
				if (kind != SyntaxKind.AsteriskToken)
				{
					if (kind != SyntaxKind.OpenBracketToken)
					{
						if (kind != SyntaxKind.QuestionToken || !this.<ParseTypeCore>g__canBeNullableType|205_0(ref CS$<>8__locals1))
						{
							break;
						}
						SyntaxToken syntaxToken = this.EatNullableQualifierIfApplicable(CS$<>8__locals1.mode);
						if (syntaxToken == null)
						{
							break;
						}
						CS$<>8__locals1.type = this._syntaxFactory.NullableType(CS$<>8__locals1.type, syntaxToken);
					}
					else
					{
						SyntaxListBuilder<ArrayRankSpecifierSyntax> syntaxListBuilder = this._pool.Allocate<ArrayRankSpecifierSyntax>();
						try
						{
							while (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
							{
								bool flag;
								ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = this.ParseArrayRankSpecifier(out flag);
								syntaxListBuilder.Add(arrayRankSpecifierSyntax);
							}
							CS$<>8__locals1.type = this._syntaxFactory.ArrayType(CS$<>8__locals1.type, syntaxListBuilder);
						}
						finally
						{
							this._pool.Free(syntaxListBuilder);
						}
					}
				}
				else
				{
					if (CS$<>8__locals1.type.Kind == SyntaxKind.ArrayType)
					{
						break;
					}
					switch (CS$<>8__locals1.mode)
					{
					case LanguageParser.ParseTypeMode.Normal:
					case LanguageParser.ParseTypeMode.Parameter:
					case LanguageParser.ParseTypeMode.AfterOut:
					case LanguageParser.ParseTypeMode.AfterRef:
					case LanguageParser.ParseTypeMode.AsExpression:
					case LanguageParser.ParseTypeMode.NewExpression:
						CS$<>8__locals1.type = this.ParsePointerTypeMods(CS$<>8__locals1.type);
						break;
					case LanguageParser.ParseTypeMode.AfterIs:
					case LanguageParser.ParseTypeMode.DefinitePattern:
					case LanguageParser.ParseTypeMode.AfterTupleComma:
					case LanguageParser.ParseTypeMode.FirstElementOfPossibleTupleLiteral:
						if (!this.PointerTypeModsFollowedByRankAndDimensionSpecifier())
						{
							goto IL_01FD;
						}
						CS$<>8__locals1.type = this.ParsePointerTypeMods(CS$<>8__locals1.type);
						break;
					default:
						goto IL_01FD;
					}
				}
			}
			IL_01FD:
			return CS$<>8__locals1.type;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x00126AD4 File Offset: 0x00124CD4
		private SyntaxToken EatNullableQualifierIfApplicable(LanguageParser.ParseTypeMode mode)
		{
			LanguageParser.<>c__DisplayClass206_0 CS$<>8__locals1;
			CS$<>8__locals1.mode = mode;
			CS$<>8__locals1.<>4__this = this;
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			SyntaxToken syntaxToken2;
			try
			{
				SyntaxToken syntaxToken = base.EatToken();
				if (!this.<EatNullableQualifierIfApplicable>g__canFollowNullableType|206_0(ref CS$<>8__locals1))
				{
					this.Reset(ref resetPoint);
					syntaxToken2 = null;
				}
				else
				{
					syntaxToken2 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureNullable, false);
				}
			}
			finally
			{
				this.Release(ref resetPoint);
			}
			return syntaxToken2;
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x00126B40 File Offset: 0x00124D40
		private bool PointerTypeModsFollowedByRankAndDimensionSpecifier()
		{
			int num = 0;
			SyntaxKind kind;
			for (;;)
			{
				kind = base.PeekToken(num).Kind;
				if (kind != SyntaxKind.AsteriskToken)
				{
					break;
				}
				num++;
			}
			return kind == SyntaxKind.OpenBracketToken;
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x00126B78 File Offset: 0x00124D78
		private ArrayRankSpecifierSyntax ParseArrayRankSpecifier(out bool sawNonOmittedSize)
		{
			sawNonOmittedSize = false;
			bool flag = false;
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBracketToken);
			SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ExpressionSyntax>();
			ArrayRankSpecifierSyntax arrayRankSpecifierSyntax;
			try
			{
				OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax = this._syntaxFactory.OmittedArraySizeExpression(SyntaxFactory.Token(SyntaxKind.OmittedArraySizeExpressionToken));
				int num = -1;
				while (base.IsMakingProgress(ref num, true) && base.CurrentToken.Kind != SyntaxKind.CloseBracketToken)
				{
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
					{
						flag = true;
						separatedSyntaxListBuilder.Add(omittedArraySizeExpressionSyntax);
						separatedSyntaxListBuilder.AddSeparator(base.EatToken());
					}
					else if (this.IsPossibleExpression())
					{
						ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
						sawNonOmittedSize = true;
						separatedSyntaxListBuilder.Add(expressionSyntax);
						if (base.CurrentToken.Kind != SyntaxKind.CloseBracketToken)
						{
							separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						}
					}
					else if (this.SkipBadArrayRankSpecifierTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
					{
						break;
					}
				}
				if ((separatedSyntaxListBuilder.Count & 1) == 0)
				{
					flag = true;
					separatedSyntaxListBuilder.Add(omittedArraySizeExpressionSyntax);
				}
				if (flag & sawNonOmittedSize)
				{
					for (int i = 0; i < separatedSyntaxListBuilder.Count; i++)
					{
						if (separatedSyntaxListBuilder[i].RawKind == 8654)
						{
							int width = separatedSyntaxListBuilder[i].Width;
							int leadingTriviaWidth = separatedSyntaxListBuilder[i].GetLeadingTriviaWidth();
							separatedSyntaxListBuilder[i] = base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), leadingTriviaWidth, width, ErrorCode.ERR_ValueExpected, Array.Empty<object>());
						}
					}
				}
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBracketToken);
				arrayRankSpecifierSyntax = this._syntaxFactory.ArrayRankSpecifier(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._pool.Free<ExpressionSyntax>(in separatedSyntaxListBuilder);
			}
			return arrayRankSpecifierSyntax;
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x00126D3C File Offset: 0x00124F3C
		private TupleTypeSyntax ParseTupleType()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenParenToken);
			SeparatedSyntaxListBuilder<TupleElementSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<TupleElementSyntax>();
			TupleTypeSyntax tupleTypeSyntax2;
			try
			{
				if (base.CurrentToken.Kind != SyntaxKind.CloseParenToken)
				{
					TupleElementSyntax tupleElementSyntax = this.ParseTupleElement();
					separatedSyntaxListBuilder.Add(tupleElementSyntax);
					while (base.CurrentToken.Kind == SyntaxKind.CommaToken)
					{
						SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CommaToken);
						separatedSyntaxListBuilder.AddSeparator(syntaxToken2);
						tupleElementSyntax = this.ParseTupleElement();
						separatedSyntaxListBuilder.Add(tupleElementSyntax);
					}
				}
				if (separatedSyntaxListBuilder.Count < 2)
				{
					if (separatedSyntaxListBuilder.Count < 1)
					{
						separatedSyntaxListBuilder.Add(this._syntaxFactory.TupleElement(this.CreateMissingIdentifierName(), null));
					}
					separatedSyntaxListBuilder.AddSeparator(SyntaxFactory.MissingToken(SyntaxKind.CommaToken));
					IdentifierNameSyntax identifierNameSyntax = base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_TupleTooFewElements);
					separatedSyntaxListBuilder.Add(this._syntaxFactory.TupleElement(identifierNameSyntax, null));
				}
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
				TupleTypeSyntax tupleTypeSyntax = this._syntaxFactory.TupleType(syntaxToken, in separatedSyntaxListBuilder, syntaxToken3);
				tupleTypeSyntax = base.CheckFeatureAvailability<TupleTypeSyntax>(tupleTypeSyntax, MessageID.IDS_FeatureTuples, false);
				tupleTypeSyntax2 = tupleTypeSyntax;
			}
			finally
			{
				this._pool.Free<TupleElementSyntax>(in separatedSyntaxListBuilder);
			}
			return tupleTypeSyntax2;
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x00126E80 File Offset: 0x00125080
		private TupleElementSyntax ParseTupleElement()
		{
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			SyntaxToken syntaxToken = null;
			if (this.IsTrueIdentifier())
			{
				syntaxToken = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			}
			return this._syntaxFactory.TupleElement(typeSyntax, syntaxToken);
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x00126EB8 File Offset: 0x001250B8
		private LanguageParser.PostSkipAction SkipBadArrayRankSpecifierTokens(ref SyntaxToken openBracket, SeparatedSyntaxListBuilder<ExpressionSyntax> list, SyntaxKind expected)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, ExpressionSyntax>(ref openBracket, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleExpression(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseBracketToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x00126F0C File Offset: 0x0012510C
		private TypeSyntax ParseUnderlyingType(LanguageParser.ParseTypeMode mode, LanguageParser.NameOptions options = LanguageParser.NameOptions.None)
		{
			if (LanguageParser.IsPredefinedType(base.CurrentToken.Kind))
			{
				SyntaxToken syntaxToken = base.EatToken();
				if (syntaxToken.Kind == SyntaxKind.VoidKeyword && base.CurrentToken.Kind != SyntaxKind.AsteriskToken)
				{
					syntaxToken = base.AddError<SyntaxToken>(syntaxToken, (mode == LanguageParser.ParseTypeMode.Parameter) ? ErrorCode.ERR_NoVoidParameter : ErrorCode.ERR_NoVoidHere);
				}
				return this._syntaxFactory.PredefinedType(syntaxToken);
			}
			if (this.IsTrueIdentifier() || base.CurrentToken.Kind == SyntaxKind.ColonColonToken)
			{
				return this.ParseQualifiedName(options);
			}
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				return this.ParseTupleType();
			}
			if (this.IsFunctionPointerStart())
			{
				return this.ParseFunctionPointerTypeSyntax();
			}
			return base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), (mode == LanguageParser.ParseTypeMode.NewExpression) ? ErrorCode.ERR_BadNewExpr : ErrorCode.ERR_TypeExpected);
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x00126FDC File Offset: 0x001251DC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private FunctionPointerTypeSyntax ParseFunctionPointerTypeSyntax()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.DelegateKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.AsteriskToken);
			FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax = this.<ParseFunctionPointerTypeSyntax>g__parseCallingConvention|213_1();
			if (!LanguageParser.IsPossibleFunctionPointerParameterListStart(base.CurrentToken))
			{
				SyntaxToken syntaxToken3 = this.WithAdditionalDiagnostics<SyntaxToken>(SyntaxFactory.MissingToken(SyntaxKind.LessThanToken), new DiagnosticInfo[] { this.GetExpectedTokenError(SyntaxKind.LessThanToken, SyntaxKind.None) });
				SeparatedSyntaxListBuilder<FunctionPointerParameterSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<FunctionPointerParameterSyntax>();
				IdentifierNameSyntax identifierNameSyntax = this.CreateMissingIdentifierName();
				FunctionPointerParameterSyntax functionPointerParameterSyntax = SyntaxFactory.FunctionPointerParameter(default(SyntaxList<AttributeListSyntax>), default(SyntaxList<SyntaxToken>), identifierNameSyntax);
				separatedSyntaxListBuilder.Add(functionPointerParameterSyntax);
				SyntaxToken syntaxToken4 = base.TryEatToken(SyntaxKind.GreaterThanToken) ?? SyntaxFactory.MissingToken(SyntaxKind.GreaterThanToken);
				FunctionPointerParameterListSyntax functionPointerParameterListSyntax = SyntaxFactory.FunctionPointerParameterList(syntaxToken3, in separatedSyntaxListBuilder, syntaxToken4);
				FunctionPointerTypeSyntax functionPointerTypeSyntax = SyntaxFactory.FunctionPointerType(syntaxToken, syntaxToken2, functionPointerCallingConventionSyntax, functionPointerParameterListSyntax);
				this._pool.Free<FunctionPointerParameterSyntax>(in separatedSyntaxListBuilder);
				return functionPointerTypeSyntax;
			}
			SyntaxToken syntaxToken5 = base.EatTokenAsKind(SyntaxKind.LessThanToken);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= (syntaxToken5.IsMissing ? LanguageParser.TerminatorState.IsEndOfFunctionPointerParameterListErrored : LanguageParser.TerminatorState.IsEndOfFunctionPointerParameterList);
			SeparatedSyntaxListBuilder<FunctionPointerParameterSyntax> separatedSyntaxListBuilder2 = this._pool.AllocateSeparated<FunctionPointerParameterSyntax>();
			FunctionPointerTypeSyntax functionPointerTypeSyntax3;
			try
			{
				for (;;)
				{
					SyntaxListBuilder<SyntaxToken> syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
					try
					{
						this.ParseParameterModifiers(syntaxListBuilder, true);
						TypeSyntax typeSyntax = this.ParseTypeOrVoid();
						separatedSyntaxListBuilder2.Add(SyntaxFactory.FunctionPointerParameter(default(SyntaxList<AttributeListSyntax>), syntaxListBuilder, typeSyntax));
						if (this.<ParseFunctionPointerTypeSyntax>g__skipBadFunctionPointerTokens|213_0<FunctionPointerParameterSyntax>(separatedSyntaxListBuilder2) != LanguageParser.PostSkipAction.Abort)
						{
							separatedSyntaxListBuilder2.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
							continue;
						}
					}
					finally
					{
						this._pool.Free(syntaxListBuilder);
					}
					break;
				}
				SyntaxToken syntaxToken6;
				if (syntaxToken5.IsMissing && base.CurrentToken.Kind == SyntaxKind.CloseParenToken)
				{
					syntaxToken6 = base.EatTokenAsKind(SyntaxKind.GreaterThanToken);
				}
				else
				{
					syntaxToken6 = base.EatToken(SyntaxKind.GreaterThanToken);
				}
				FunctionPointerTypeSyntax functionPointerTypeSyntax2 = SyntaxFactory.FunctionPointerType(syntaxToken, syntaxToken2, functionPointerCallingConventionSyntax, SyntaxFactory.FunctionPointerParameterList(syntaxToken5, in separatedSyntaxListBuilder2, syntaxToken6));
				functionPointerTypeSyntax2 = base.CheckFeatureAvailability<FunctionPointerTypeSyntax>(functionPointerTypeSyntax2, MessageID.IDS_FeatureFunctionPointers, false);
				functionPointerTypeSyntax3 = functionPointerTypeSyntax2;
			}
			finally
			{
				this._termState = termState;
				this._pool.Free<FunctionPointerParameterSyntax>(in separatedSyntaxListBuilder2);
			}
			return functionPointerTypeSyntax3;
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x00127210 File Offset: 0x00125410
		private bool IsFunctionPointerStart()
		{
			return base.CurrentToken.Kind == SyntaxKind.DelegateKeyword && base.PeekToken(1).Kind == SyntaxKind.AsteriskToken;
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x00127239 File Offset: 0x00125439
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool IsPossibleFunctionPointerParameterListStart(SyntaxToken token)
		{
			return token.Kind == SyntaxKind.LessThanToken || token.Kind == SyntaxKind.OpenParenToken;
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x00127257 File Offset: 0x00125457
		private TypeSyntax ParsePointerTypeMods(TypeSyntax type)
		{
			while (base.CurrentToken.Kind == SyntaxKind.AsteriskToken)
			{
				type = this._syntaxFactory.PointerType(type, base.EatToken());
			}
			return type;
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x00127282 File Offset: 0x00125482
		public StatementSyntax ParseStatement()
		{
			return this.ParseWithStackGuard<StatementSyntax>(delegate
			{
				StatementSyntax statementSyntax;
				if ((statementSyntax = this.ParsePossiblyAttributedStatement()) == null)
				{
					statementSyntax = this.ParseExpressionStatement(default(SyntaxList<AttributeListSyntax>));
				}
				return statementSyntax;
			}, () => SyntaxFactory.EmptyStatement(default(SyntaxList<AttributeListSyntax>), SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken)));
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x001272B5 File Offset: 0x001254B5
		private StatementSyntax ParsePossiblyAttributedStatement()
		{
			return this.ParseStatementCore(this.ParseAttributeDeclarations(), false);
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x001272C4 File Offset: 0x001254C4
		private StatementSyntax ParseStatementCore(SyntaxList<AttributeListSyntax> attributes, bool isGlobal)
		{
			if (this.<ParseStatementCore>g__canReuseStatement|219_0(attributes, isGlobal))
			{
				return (StatementSyntax)base.EatNode();
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			StatementSyntax statementSyntax2;
			try
			{
				this._recursionDepth++;
				StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
				SyntaxKind kind = base.CurrentToken.Kind;
				if (kind <= SyntaxKind.FixedKeyword)
				{
					if (kind == SyntaxKind.OpenBraceToken)
					{
						return this.ParseBlock(attributes);
					}
					if (kind == SyntaxKind.SemicolonToken)
					{
						return this._syntaxFactory.EmptyStatement(attributes, base.EatToken());
					}
					switch (kind)
					{
					case SyntaxKind.IfKeyword:
						return this.ParseIfStatement(attributes);
					case SyntaxKind.ElseKeyword:
						return this.ParseMisplacedElse(attributes);
					case SyntaxKind.WhileKeyword:
						return this.ParseWhileStatement(attributes);
					case SyntaxKind.ForKeyword:
						return this.ParseForOrForEachStatement(attributes);
					case SyntaxKind.ForEachKeyword:
						return this.ParseForEachStatement(attributes, null);
					case SyntaxKind.DoKeyword:
						return this.ParseDoStatement(attributes);
					case SyntaxKind.SwitchKeyword:
						return this.ParseSwitchStatement(attributes);
					case SyntaxKind.TryKeyword:
					case SyntaxKind.CatchKeyword:
					case SyntaxKind.FinallyKeyword:
						return this.ParseTryStatement(attributes);
					case SyntaxKind.LockKeyword:
						return this.ParseLockStatement(attributes);
					case SyntaxKind.GotoKeyword:
						return this.ParseGotoStatement(attributes);
					case SyntaxKind.BreakKeyword:
						return this.ParseBreakStatement(attributes);
					case SyntaxKind.ContinueKeyword:
						return this.ParseContinueStatement(attributes);
					case SyntaxKind.ReturnKeyword:
						return this.ParseReturnStatement(attributes);
					case SyntaxKind.ThrowKeyword:
						return this.ParseThrowStatement(attributes);
					case SyntaxKind.FixedKeyword:
						return this.ParseFixedStatement(attributes);
					}
				}
				else if (kind <= SyntaxKind.UncheckedKeyword)
				{
					if (kind == SyntaxKind.UsingKeyword)
					{
						return this.ParseStatementStartingWithUsing(attributes);
					}
					if (kind - SyntaxKind.CheckedKeyword <= 1)
					{
						return this.ParseCheckedStatement(attributes);
					}
				}
				else if (kind != SyntaxKind.UnsafeKeyword)
				{
					if (kind == SyntaxKind.IdentifierToken)
					{
						StatementSyntax statementSyntax = this.TryParseStatementStartingWithIdentifier(attributes, isGlobal);
						if (statementSyntax != null)
						{
							return statementSyntax;
						}
					}
				}
				else
				{
					StatementSyntax statementSyntax = this.TryParseStatementStartingWithUnsafe(attributes);
					if (statementSyntax != null)
					{
						return statementSyntax;
					}
				}
				statementSyntax2 = this.ParseStatementCoreRest(attributes, isGlobal, ref resetPoint);
			}
			finally
			{
				this._recursionDepth--;
				this.Release(ref resetPoint);
			}
			return statementSyntax2;
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x00127544 File Offset: 0x00125744
		private StatementSyntax ParseStatementCoreRest(SyntaxList<AttributeListSyntax> attributes, bool isGlobal, ref LanguageParser.ResetPoint resetPointBeforeStatement)
		{
			isGlobal = isGlobal && base.IsScript;
			if (!this.IsPossibleLocalDeclarationStatement(isGlobal))
			{
				return this.ParseExpressionStatement(attributes);
			}
			if (isGlobal)
			{
				return null;
			}
			bool flag = base.CurrentToken.ContextualKind == SyntaxKind.AwaitKeyword;
			StatementSyntax statementSyntax = this.ParseLocalDeclarationStatement(attributes);
			if (statementSyntax == null)
			{
				this.Reset(ref resetPointBeforeStatement);
				return null;
			}
			if (statementSyntax.ContainsDiagnostics && flag && !this.IsInAsync)
			{
				this.Reset(ref resetPointBeforeStatement);
				this.IsInAsync = true;
				statementSyntax = this.ParseExpressionStatement(attributes);
				this.IsInAsync = false;
			}
			return statementSyntax;
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x001275CC File Offset: 0x001257CC
		private StatementSyntax TryParseStatementStartingWithIdentifier(SyntaxList<AttributeListSyntax> attributes, bool isGlobal)
		{
			if (base.CurrentToken.ContextualKind == SyntaxKind.AwaitKeyword && base.PeekToken(1).Kind == SyntaxKind.ForEachKeyword)
			{
				return this.ParseForEachStatement(attributes, this.ParseAwaitKeyword(MessageID.IDS_FeatureAsyncStreams));
			}
			if (this.IsPossibleAwaitUsing())
			{
				if (base.PeekToken(2).Kind == SyntaxKind.OpenParenToken)
				{
					return this.ParseUsingStatement(attributes, this.ParseAwaitKeyword(MessageID.IDS_FeatureAsyncUsing));
				}
			}
			else
			{
				if (this.IsPossibleLabeledStatement())
				{
					return this.ParseLabeledStatement(attributes);
				}
				if (this.IsPossibleYieldStatement())
				{
					return this.ParseYieldStatement(attributes);
				}
				if (this.IsPossibleAwaitExpressionStatement())
				{
					return this.ParseExpressionStatement(attributes);
				}
				if (this.IsQueryExpression(true, isGlobal && base.IsScript))
				{
					return this.ParseExpressionStatement(attributes, this.ParseQueryExpression(LanguageParser.Precedence.Expression));
				}
			}
			return null;
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x00127694 File Offset: 0x00125894
		private StatementSyntax ParseStatementStartingWithUsing(SyntaxList<AttributeListSyntax> attributes)
		{
			if (base.PeekToken(1).Kind != SyntaxKind.OpenParenToken)
			{
				return this.ParseLocalDeclarationStatement(attributes);
			}
			return this.ParseUsingStatement(attributes, null);
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x001276B9 File Offset: 0x001258B9
		private StatementSyntax TryParseStatementStartingWithUnsafe(SyntaxList<AttributeListSyntax> attributes)
		{
			if (!this.IsPossibleUnsafeStatement())
			{
				return null;
			}
			return this.ParseUnsafeStatement(attributes);
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x001276CC File Offset: 0x001258CC
		private SyntaxToken ParseAwaitKeyword(MessageID feature)
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.AwaitKeyword, true);
			if (feature == MessageID.None)
			{
				return syntaxToken;
			}
			return base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, feature, false);
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x001276F4 File Offset: 0x001258F4
		private bool IsPossibleAwaitUsing()
		{
			return base.CurrentToken.ContextualKind == SyntaxKind.AwaitKeyword && base.PeekToken(1).Kind == SyntaxKind.UsingKeyword;
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x0012771D File Offset: 0x0012591D
		private bool IsPossibleLabeledStatement()
		{
			return base.PeekToken(1).Kind == SyntaxKind.ColonToken && this.IsTrueIdentifier();
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0012773A File Offset: 0x0012593A
		private bool IsPossibleUnsafeStatement()
		{
			return base.PeekToken(1).Kind == SyntaxKind.OpenBraceToken;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0012774F File Offset: 0x0012594F
		private bool IsPossibleYieldStatement()
		{
			return base.CurrentToken.ContextualKind == SyntaxKind.YieldKeyword && (base.PeekToken(1).Kind == SyntaxKind.ReturnKeyword || base.PeekToken(1).Kind == SyntaxKind.BreakKeyword);
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x00127790 File Offset: 0x00125990
		private bool IsPossibleLocalDeclarationStatement(bool isGlobalScriptLevel)
		{
			SyntaxKind syntaxKind = base.CurrentToken.Kind;
			if (syntaxKind == SyntaxKind.RefKeyword || LanguageParser.IsDeclarationModifier(syntaxKind) || (SyntaxFacts.IsPredefinedType(syntaxKind) && base.PeekToken(1).Kind != SyntaxKind.DotToken && base.PeekToken(1).Kind != SyntaxKind.OpenParenToken))
			{
				return true;
			}
			if (syntaxKind == SyntaxKind.UsingKeyword)
			{
				return true;
			}
			if (this.IsPossibleAwaitUsing())
			{
				return true;
			}
			syntaxKind = base.CurrentToken.ContextualKind;
			return ((LanguageParser.IsAdditionalLocalFunctionModifier(syntaxKind) || syntaxKind == SyntaxKind.OpenBracketToken) && (syntaxKind != SyntaxKind.AsyncKeyword || this.ShouldAsyncBeTreatedAsModifier(true))) || this.IsPossibleFirstTypedIdentifierInLocaDeclarationStatement(isGlobalScriptLevel);
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x0012783C File Offset: 0x00125A3C
		private bool IsPossibleFirstTypedIdentifierInLocaDeclarationStatement(bool isGlobalScriptLevel)
		{
			bool? flag = this.IsPossibleTypedIdentifierStart(base.CurrentToken, base.PeekToken(1), false);
			if (flag != null)
			{
				return flag.Value;
			}
			if (base.CurrentToken.ContextualKind == SyntaxKind.IdentifierToken)
			{
				SyntaxToken syntaxToken = base.PeekToken(1);
				if (syntaxToken.Kind == SyntaxKind.DotToken && syntaxToken.TrailingTrivia.Any(8539) && base.PeekToken(2).Kind == SyntaxKind.IdentifierToken && base.PeekToken(3).Kind == SyntaxKind.IdentifierToken)
				{
					SyntaxKind kind = base.PeekToken(4).Kind;
					if (kind != SyntaxKind.SemicolonToken && kind != SyntaxKind.EqualsToken && kind != SyntaxKind.CommaToken && kind != SyntaxKind.OpenParenToken && kind != SyntaxKind.LessThanToken)
					{
						return false;
					}
				}
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag2;
			try
			{
				LanguageParser.ScanTypeFlags scanTypeFlags = this.ScanType(false);
				if (scanTypeFlags == LanguageParser.ScanTypeFlags.MustBeType && base.CurrentToken.Kind != SyntaxKind.DotToken && base.CurrentToken.Kind != SyntaxKind.OpenParenToken)
				{
					flag2 = true;
				}
				else if (scanTypeFlags == LanguageParser.ScanTypeFlags.NotType || base.CurrentToken.Kind != SyntaxKind.IdentifierToken)
				{
					flag2 = false;
				}
				else
				{
					if (isGlobalScriptLevel)
					{
						if (scanTypeFlags == LanguageParser.ScanTypeFlags.PointerOrMultiplication)
						{
							return false;
						}
						if (scanTypeFlags == LanguageParser.ScanTypeFlags.NullableType)
						{
							return this.IsPossibleDeclarationStatementFollowingNullableType();
						}
					}
					flag2 = true;
				}
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag2;
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x001279B0 File Offset: 0x00125BB0
		private bool IsPossibleTopLevelUsingLocalDeclarationStatement()
		{
			if (base.CurrentToken.Kind != SyntaxKind.UsingKeyword)
			{
				return false;
			}
			SyntaxKind kind = base.PeekToken(1).Kind;
			if (kind == SyntaxKind.RefKeyword)
			{
				return true;
			}
			if (LanguageParser.IsDeclarationModifier(kind))
			{
				if (kind != SyntaxKind.StaticKeyword)
				{
					return true;
				}
			}
			else if (SyntaxFacts.IsPredefinedType(kind))
			{
				return true;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag;
			try
			{
				base.EatToken();
				if (kind == SyntaxKind.StaticKeyword)
				{
					base.EatToken();
				}
				flag = this.IsPossibleFirstTypedIdentifierInLocaDeclarationStatement(false);
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag;
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x00127A50 File Offset: 0x00125C50
		private bool IsPossibleDeclarationStatementFollowingNullableType()
		{
			if (this.IsFieldDeclaration(false))
			{
				return this.IsPossibleFieldDeclarationFollowingNullableType();
			}
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax;
			SyntaxToken syntaxToken;
			TypeParameterListSyntax typeParameterListSyntax;
			this.ParseMemberName(out explicitInterfaceSpecifierSyntax, out syntaxToken, out typeParameterListSyntax, false);
			return (explicitInterfaceSpecifierSyntax != null || syntaxToken != null || typeParameterListSyntax != null) && (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || (syntaxToken.Kind != SyntaxKind.ThisKeyword && this.IsPossibleMethodDeclarationFollowingNullableType()));
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x00127AB0 File Offset: 0x00125CB0
		private bool IsPossibleFieldDeclarationFollowingNullableType()
		{
			if (base.CurrentToken.Kind != SyntaxKind.IdentifierToken)
			{
				return false;
			}
			base.EatToken();
			if (base.CurrentToken.Kind == SyntaxKind.EqualsToken)
			{
				LanguageParser.TerminatorState termState = this._termState;
				this._termState |= LanguageParser.TerminatorState.IsEndOfFieldDeclaration;
				base.EatToken();
				this.ParseVariableInitializer();
				this._termState = termState;
			}
			return base.CurrentToken.Kind == SyntaxKind.CommaToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken;
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00127B3C File Offset: 0x00125D3C
		private bool IsPossibleMethodDeclarationFollowingNullableType()
		{
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfMethodSignature;
			ParameterListSyntax parameterListSyntax = this.ParseParenthesizedParameterList();
			this._termState = termState;
			SyntaxList<GreenNode> withSeparators = parameterListSyntax.Parameters.GetWithSeparators();
			if (!parameterListSyntax.CloseParenToken.IsMissing)
			{
				if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword)
				{
					return true;
				}
				if (base.CurrentToken.Kind == SyntaxKind.ColonToken)
				{
					return false;
				}
			}
			if (withSeparators.Count == 0)
			{
				return false;
			}
			ParameterSyntax parameterSyntax = (ParameterSyntax)withSeparators[0];
			if (parameterSyntax.AttributeLists.Count > 0)
			{
				return true;
			}
			for (int i = 0; i < parameterSyntax.Modifiers.Count; i++)
			{
				if (parameterSyntax.Modifiers[i].Kind == SyntaxKind.ParamsKeyword)
				{
					return true;
				}
			}
			if (parameterSyntax.Type == null)
			{
				if (parameterSyntax.Identifier.Kind == SyntaxKind.ArgListKeyword)
				{
					return true;
				}
			}
			else if (parameterSyntax.Type.Kind == SyntaxKind.NullableType)
			{
				if (parameterSyntax.Modifiers.Count > 0)
				{
					return true;
				}
				if (!parameterSyntax.Identifier.IsMissing && ((withSeparators.Count >= 2 && !withSeparators[1].IsMissing) || (withSeparators.Count == 1 && !parameterListSyntax.CloseParenToken.IsMissing)))
				{
					return true;
				}
			}
			else
			{
				if (parameterSyntax.Type.Kind == SyntaxKind.IdentifierName && ((IdentifierNameSyntax)parameterSyntax.Type).Identifier.ContextualKind == SyntaxKind.FromKeyword)
				{
					return false;
				}
				if (!parameterSyntax.Identifier.IsMissing)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00127CF8 File Offset: 0x00125EF8
		private bool IsPossibleNewExpression()
		{
			SyntaxToken syntaxToken = base.PeekToken(1);
			SyntaxKind kind = syntaxToken.Kind;
			if (kind == SyntaxKind.OpenBraceToken || kind == SyntaxKind.OpenBracketToken)
			{
				return true;
			}
			if (SyntaxFacts.GetBaseTypeDeclarationKind(syntaxToken.Kind) != SyntaxKind.None)
			{
				return false;
			}
			DeclarationModifiers modifier = LanguageParser.GetModifier(syntaxToken);
			if (modifier == DeclarationModifiers.Partial)
			{
				if (SyntaxFacts.IsPredefinedType(base.PeekToken(2).Kind))
				{
					return false;
				}
				if (LanguageParser.IsPossibleStartOfTypeDeclaration(base.PeekToken(2).Kind))
				{
					return false;
				}
			}
			else if (modifier != DeclarationModifiers.None)
			{
				return false;
			}
			bool? flag = this.IsPossibleTypedIdentifierStart(syntaxToken, base.PeekToken(2), true);
			if (flag != null)
			{
				return !flag.Value;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag2;
			try
			{
				base.EatToken();
				LanguageParser.ScanTypeFlags scanTypeFlags = this.ScanType(false);
				flag2 = !this.IsPossibleMemberName() || scanTypeFlags == LanguageParser.ScanTypeFlags.NotType;
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag2;
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00127DE8 File Offset: 0x00125FE8
		private bool? IsPossibleTypedIdentifierStart(SyntaxToken current, SyntaxToken next, bool allowThisKeyword)
		{
			if (this.IsTrueIdentifier(current))
			{
				SyntaxKind kind = next.Kind;
				if (kind <= SyntaxKind.LessThanToken)
				{
					if (kind <= SyntaxKind.OpenParenToken)
					{
						if (kind != SyntaxKind.AsteriskToken)
						{
							if (kind != SyntaxKind.OpenParenToken)
							{
								goto IL_00AC;
							}
							if (current.IsIdentifierVar())
							{
								return default(bool?);
							}
							return new bool?(false);
						}
					}
					else if (kind != SyntaxKind.OpenBracketToken && kind != SyntaxKind.LessThanToken)
					{
						goto IL_00AC;
					}
				}
				else if (kind <= SyntaxKind.ColonColonToken)
				{
					if (kind - SyntaxKind.DotToken > 1 && kind != SyntaxKind.ColonColonToken)
					{
						goto IL_00AC;
					}
				}
				else
				{
					if (kind == SyntaxKind.ThisKeyword)
					{
						return new bool?(allowThisKeyword);
					}
					if (kind != SyntaxKind.IdentifierToken)
					{
						goto IL_00AC;
					}
					return new bool?(this.IsTrueIdentifier(next));
				}
				return default(bool?);
				IL_00AC:
				return new bool?(false);
			}
			return default(bool?);
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00127EB1 File Offset: 0x001260B1
		private BlockSyntax ParsePossiblyAttributedBlock()
		{
			return this.ParseBlock(this.ParseAttributeDeclarations());
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x00127EC0 File Offset: 0x001260C0
		private BlockSyntax ParseMethodOrAccessorBodyBlock(SyntaxList<AttributeListSyntax> attributes, bool isAccessorBody)
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.Block && attributes.Count == 0)
			{
				return (BlockSyntax)base.EatNode();
			}
			CSharpSyntaxNode csharpSyntaxNode = ((isAccessorBody && base.CurrentToken.Kind != SyntaxKind.OpenBraceToken) ? base.AddError<SyntaxToken>(SyntaxFactory.MissingToken(SyntaxKind.OpenBraceToken), base.IsFeatureEnabled(MessageID.IDS_FeatureExpressionBodiedAccessor) ? ErrorCode.ERR_SemiOrLBraceOrArrowExpected : ErrorCode.ERR_SemiOrLBraceExpected) : base.EatToken(SyntaxKind.OpenBraceToken));
			SyntaxListBuilder<StatementSyntax> syntaxListBuilder = this._pool.Allocate<StatementSyntax>();
			this.ParseStatements(ref csharpSyntaxNode, syntaxListBuilder, false);
			BlockSyntax blockSyntax = this._syntaxFactory.Block(attributes, (SyntaxToken)csharpSyntaxNode, LanguageParser.IsLargeEnoughNonEmptyStatementList(syntaxListBuilder) ? new SyntaxList<StatementSyntax>(SyntaxList.List(syntaxListBuilder.ToArray())) : syntaxListBuilder, base.EatToken(SyntaxKind.CloseBraceToken));
			this._pool.Free(syntaxListBuilder);
			return blockSyntax;
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x00127FAC File Offset: 0x001261AC
		private BlockSyntax ParseBlock(SyntaxList<AttributeListSyntax> attributes)
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.Block)
			{
				return (BlockSyntax)base.EatNode();
			}
			CSharpSyntaxNode csharpSyntaxNode = base.EatToken(SyntaxKind.OpenBraceToken);
			SyntaxListBuilder<StatementSyntax> syntaxListBuilder = this._pool.Allocate<StatementSyntax>();
			this.ParseStatements(ref csharpSyntaxNode, syntaxListBuilder, false);
			BlockSyntax blockSyntax = this._syntaxFactory.Block(attributes, (SyntaxToken)csharpSyntaxNode, syntaxListBuilder, base.EatToken(SyntaxKind.CloseBraceToken));
			this._pool.Free(syntaxListBuilder);
			return blockSyntax;
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x00128030 File Offset: 0x00126230
		private static bool IsLargeEnoughNonEmptyStatementList(SyntaxListBuilder<StatementSyntax> statements)
		{
			return statements.Count != 0 && (statements.Count != 1 || statements[0].Width > 60);
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x0012805C File Offset: 0x0012625C
		private void ParseStatements(ref CSharpSyntaxNode previousNode, SyntaxListBuilder<StatementSyntax> statements, bool stopOnSwitchSections)
		{
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsPossibleStatementStartOrStop;
			if (stopOnSwitchSections)
			{
				this._termState |= LanguageParser.TerminatorState.IsSwitchSectionStart;
			}
			int num = -1;
			while (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken && base.CurrentToken.Kind != SyntaxKind.EndOfFileToken && (!stopOnSwitchSections || !this.IsPossibleSwitchSection()) && base.IsMakingProgress(ref num, true))
			{
				if (this.IsPossibleStatement(true))
				{
					StatementSyntax statementSyntax = this.ParsePossiblyAttributedStatement();
					if (statementSyntax != null)
					{
						statements.Add(statementSyntax);
						continue;
					}
				}
				GreenNode greenNode;
				int num2 = (int)this.SkipBadStatementListTokens(statements, SyntaxKind.CloseBraceToken, out greenNode);
				if (greenNode != null)
				{
					previousNode = base.AddTrailingSkippedSyntax<CSharpSyntaxNode>(previousNode, greenNode);
				}
				if (num2 == 1)
				{
					break;
				}
			}
			this._termState = termState;
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x00128119 File Offset: 0x00126319
		private bool IsPossibleStatementStartOrStop()
		{
			return base.CurrentToken.Kind == SyntaxKind.SemicolonToken || this.IsPossibleStatement(true);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x00128138 File Offset: 0x00126338
		private LanguageParser.PostSkipAction SkipBadStatementListTokens(SyntaxListBuilder<StatementSyntax> statements, SyntaxKind expected, out GreenNode trailingTrivia)
		{
			return this.SkipBadListTokensWithExpectedKindHelper(statements, (LanguageParser p) => !p.IsPossibleStatement(false), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseBraceToken || p.IsTerminator(), expected, out trailingTrivia);
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x00128194 File Offset: 0x00126394
		private bool IsPossibleStatement(bool acceptAccessibilityMods)
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.OpenBracketToken)
			{
				if (kind != SyntaxKind.OpenBraceToken && kind != SyntaxKind.OpenBracketToken)
				{
					goto IL_0149;
				}
			}
			else if (kind != SyntaxKind.SemicolonToken)
			{
				switch (kind)
				{
				case SyntaxKind.IfKeyword:
				case SyntaxKind.ElseKeyword:
				case SyntaxKind.WhileKeyword:
				case SyntaxKind.ForKeyword:
				case SyntaxKind.ForEachKeyword:
				case SyntaxKind.DoKeyword:
				case SyntaxKind.SwitchKeyword:
				case SyntaxKind.TryKeyword:
				case SyntaxKind.LockKeyword:
				case SyntaxKind.GotoKeyword:
				case SyntaxKind.BreakKeyword:
				case SyntaxKind.ContinueKeyword:
				case SyntaxKind.ReturnKeyword:
				case SyntaxKind.ThrowKeyword:
				case SyntaxKind.StaticKeyword:
				case SyntaxKind.ReadOnlyKeyword:
				case SyntaxKind.ConstKeyword:
				case SyntaxKind.FixedKeyword:
				case SyntaxKind.VolatileKeyword:
				case SyntaxKind.ExternKeyword:
				case SyntaxKind.RefKeyword:
				case SyntaxKind.UsingKeyword:
				case SyntaxKind.CheckedKeyword:
				case SyntaxKind.UncheckedKeyword:
				case SyntaxKind.UnsafeKeyword:
					break;
				case SyntaxKind.CaseKeyword:
				case SyntaxKind.DefaultKeyword:
				case SyntaxKind.SealedKeyword:
				case SyntaxKind.StackAllocKeyword:
				case SyntaxKind.NewKeyword:
				case SyntaxKind.OverrideKeyword:
				case SyntaxKind.AbstractKeyword:
				case SyntaxKind.VirtualKeyword:
				case SyntaxKind.EventKeyword:
				case SyntaxKind.OutKeyword:
				case SyntaxKind.InKeyword:
				case SyntaxKind.IsKeyword:
				case SyntaxKind.AsKeyword:
				case SyntaxKind.ParamsKeyword:
				case SyntaxKind.ArgListKeyword:
				case SyntaxKind.MakeRefKeyword:
				case SyntaxKind.RefTypeKeyword:
				case SyntaxKind.RefValueKeyword:
				case SyntaxKind.ThisKeyword:
				case SyntaxKind.BaseKeyword:
				case SyntaxKind.NamespaceKeyword:
				case SyntaxKind.ClassKeyword:
				case SyntaxKind.StructKeyword:
				case SyntaxKind.InterfaceKeyword:
				case SyntaxKind.EnumKeyword:
				case SyntaxKind.DelegateKeyword:
					goto IL_0149;
				case SyntaxKind.CatchKeyword:
				case SyntaxKind.FinallyKeyword:
					return !this._isInTry;
				case SyntaxKind.PublicKeyword:
				case SyntaxKind.PrivateKeyword:
				case SyntaxKind.InternalKeyword:
				case SyntaxKind.ProtectedKeyword:
					return acceptAccessibilityMods;
				default:
					if (kind != SyntaxKind.IdentifierToken)
					{
						goto IL_0149;
					}
					return this.IsTrueIdentifier();
				}
			}
			return true;
			IL_0149:
			return LanguageParser.IsPredefinedType(kind) || this.IsPossibleExpression();
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x001282FC File Offset: 0x001264FC
		private FixedStatementSyntax ParseFixedStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.FixedKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfFixedStatement;
			VariableDeclarationSyntax variableDeclarationSyntax = this.ParseVariableDeclaration();
			this._termState = termState;
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
			return this._syntaxFactory.FixedStatement(attributes, syntaxToken, syntaxToken2, variableDeclarationSyntax, syntaxToken3, statementSyntax);
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x00128370 File Offset: 0x00126570
		private bool IsEndOfFixedStatement()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken;
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x001283AA File Offset: 0x001265AA
		private StatementSyntax ParseEmbeddedStatement()
		{
			return this.<ParseEmbeddedStatement>g__parseEmbeddedStatementRest|247_0(this.ParsePossiblyAttributedStatement());
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x001283B8 File Offset: 0x001265B8
		private BreakStatementSyntax ParseBreakStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.BreakKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.BreakStatement(attributes, syntaxToken, syntaxToken2);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x001283EC File Offset: 0x001265EC
		private ContinueStatementSyntax ParseContinueStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ContinueKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.ContinueStatement(attributes, syntaxToken, syntaxToken2);
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x00128420 File Offset: 0x00126620
		private TryStatementSyntax ParseTryStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			bool isInTry = this._isInTry;
			this._isInTry = true;
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.TryKeyword);
			BlockSyntax blockSyntax;
			if (syntaxToken.IsMissing)
			{
				blockSyntax = this._syntaxFactory.Block(default(SyntaxList<AttributeListSyntax>), base.EatToken(SyntaxKind.OpenBraceToken), default(SyntaxList<StatementSyntax>), base.EatToken(SyntaxKind.CloseBraceToken));
			}
			else
			{
				LanguageParser.TerminatorState termState = this._termState;
				this._termState |= LanguageParser.TerminatorState.IsEndOfTryBlock;
				blockSyntax = this.ParsePossiblyAttributedBlock();
				this._termState = termState;
			}
			SyntaxListBuilder<CatchClauseSyntax> syntaxListBuilder = default(SyntaxListBuilder<CatchClauseSyntax>);
			FinallyClauseSyntax finallyClauseSyntax = null;
			TryStatementSyntax tryStatementSyntax;
			try
			{
				bool flag = false;
				if (base.CurrentToken.Kind == SyntaxKind.CatchKeyword)
				{
					flag = true;
					syntaxListBuilder = this._pool.Allocate<CatchClauseSyntax>();
					while (base.CurrentToken.Kind == SyntaxKind.CatchKeyword)
					{
						syntaxListBuilder.Add(this.ParseCatchClause());
					}
				}
				if (base.CurrentToken.Kind == SyntaxKind.FinallyKeyword)
				{
					flag = true;
					SyntaxToken syntaxToken2 = base.EatToken();
					BlockSyntax blockSyntax2 = this.ParsePossiblyAttributedBlock();
					finallyClauseSyntax = this._syntaxFactory.FinallyClause(syntaxToken2, blockSyntax2);
				}
				if (!flag)
				{
					blockSyntax = base.AddErrorToLastToken<BlockSyntax>(blockSyntax, ErrorCode.ERR_ExpectedEndTry);
					finallyClauseSyntax = this._syntaxFactory.FinallyClause(SyntaxToken.CreateMissing(SyntaxKind.FinallyKeyword, null, null), this._syntaxFactory.Block(default(SyntaxList<AttributeListSyntax>), SyntaxToken.CreateMissing(SyntaxKind.OpenBraceToken, null, null), default(SyntaxList<StatementSyntax>), SyntaxToken.CreateMissing(SyntaxKind.CloseBraceToken, null, null)));
				}
				this._isInTry = isInTry;
				tryStatementSyntax = this._syntaxFactory.TryStatement(attributes, syntaxToken, blockSyntax, syntaxListBuilder, finallyClauseSyntax);
			}
			finally
			{
				if (!syntaxListBuilder.IsNull)
				{
					this._pool.Free(syntaxListBuilder);
				}
			}
			return tryStatementSyntax;
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x001285E8 File Offset: 0x001267E8
		private bool IsEndOfTryBlock()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseBraceToken || base.CurrentToken.Kind == SyntaxKind.CatchKeyword || base.CurrentToken.Kind == SyntaxKind.FinallyKeyword;
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x00128624 File Offset: 0x00126824
		private CatchClauseSyntax ParseCatchClause()
		{
			SyntaxToken syntaxToken = base.EatToken();
			CatchDeclarationSyntax catchDeclarationSyntax = null;
			LanguageParser.TerminatorState termState = this._termState;
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken();
				this._termState |= LanguageParser.TerminatorState.IsEndOfCatchClause;
				TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
				SyntaxToken syntaxToken3 = null;
				if (this.IsTrueIdentifier())
				{
					syntaxToken3 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
				}
				this._termState = termState;
				SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseParenToken);
				catchDeclarationSyntax = this._syntaxFactory.CatchDeclaration(syntaxToken2, typeSyntax, syntaxToken3, syntaxToken4);
			}
			CatchFilterClauseSyntax catchFilterClauseSyntax = null;
			SyntaxKind contextualKind = base.CurrentToken.ContextualKind;
			if (contextualKind == SyntaxKind.WhenKeyword || contextualKind == SyntaxKind.IfKeyword)
			{
				SyntaxToken syntaxToken5 = base.EatContextualToken(SyntaxKind.WhenKeyword, true);
				if (contextualKind == SyntaxKind.IfKeyword)
				{
					syntaxToken5 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken5, base.EatToken());
				}
				syntaxToken5 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken5, MessageID.IDS_FeatureExceptionFilter, false);
				this._termState |= LanguageParser.TerminatorState.IsEndOfFilterClause;
				SyntaxToken syntaxToken6 = base.EatToken(SyntaxKind.OpenParenToken);
				ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
				this._termState = termState;
				SyntaxToken syntaxToken7 = base.EatToken(SyntaxKind.CloseParenToken);
				catchFilterClauseSyntax = this._syntaxFactory.CatchFilterClause(syntaxToken5, syntaxToken6, expressionSyntax, syntaxToken7);
			}
			this._termState |= LanguageParser.TerminatorState.IsEndOfCatchBlock;
			BlockSyntax blockSyntax = this.ParsePossiblyAttributedBlock();
			this._termState = termState;
			return this._syntaxFactory.CatchClause(syntaxToken, catchDeclarationSyntax, catchFilterClauseSyntax, blockSyntax);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x00128794 File Offset: 0x00126994
		private bool IsEndOfCatchClause()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || base.CurrentToken.Kind == SyntaxKind.CloseBraceToken || base.CurrentToken.Kind == SyntaxKind.CatchKeyword || base.CurrentToken.Kind == SyntaxKind.FinallyKeyword;
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x00128800 File Offset: 0x00126A00
		private bool IsEndOfFilterClause()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken || base.CurrentToken.Kind == SyntaxKind.CloseBraceToken || base.CurrentToken.Kind == SyntaxKind.CatchKeyword || base.CurrentToken.Kind == SyntaxKind.FinallyKeyword;
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x00128869 File Offset: 0x00126A69
		private bool IsEndOfCatchBlock()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseBraceToken || base.CurrentToken.Kind == SyntaxKind.CatchKeyword || base.CurrentToken.Kind == SyntaxKind.FinallyKeyword;
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x001288A4 File Offset: 0x00126AA4
		private StatementSyntax ParseCheckedStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			if (base.PeekToken(1).Kind == SyntaxKind.OpenParenToken)
			{
				return this.ParseExpressionStatement(attributes);
			}
			SyntaxToken syntaxToken = base.EatToken();
			BlockSyntax blockSyntax = this.ParsePossiblyAttributedBlock();
			return this._syntaxFactory.CheckedStatement(SyntaxFacts.GetCheckStatement(syntaxToken.Kind), attributes, syntaxToken, blockSyntax);
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x001288F4 File Offset: 0x00126AF4
		private DoStatementSyntax ParseDoStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.DoKeyword);
			StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.WhileKeyword);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.OpenParenToken);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfDoWhileExpression;
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			this._termState = termState;
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseParenToken);
			SyntaxToken syntaxToken5 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.DoStatement(attributes, syntaxToken, statementSyntax, syntaxToken2, syntaxToken3, expressionSyntax, syntaxToken4, syntaxToken5);
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00128986 File Offset: 0x00126B86
		private bool IsEndOfDoWhileExpression()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken;
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x001289B0 File Offset: 0x00126BB0
		private StatementSyntax ParseForOrForEachStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			StatementSyntax statementSyntax;
			try
			{
				base.EatToken();
				if (base.EatToken().Kind == SyntaxKind.OpenParenToken && this.ScanType(false) != LanguageParser.ScanTypeFlags.NotType && base.EatToken().Kind == SyntaxKind.IdentifierToken && base.EatToken().Kind == SyntaxKind.InKeyword)
				{
					this.Reset(ref resetPoint);
					statementSyntax = this.ParseForEachStatement(attributes, null);
				}
				else
				{
					this.Reset(ref resetPoint);
					statementSyntax = this.ParseForStatement(attributes);
				}
			}
			finally
			{
				this.Release(ref resetPoint);
			}
			return statementSyntax;
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x00128A4C File Offset: 0x00126C4C
		private ForStatementSyntax ParseForStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ForKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfForStatementArgument;
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ExpressionSyntax>();
			SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder2 = this._pool.AllocateSeparated<ExpressionSyntax>();
			ForStatementSyntax forStatementSyntax;
			try
			{
				VariableDeclarationSyntax variableDeclarationSyntax = null;
				bool flag;
				if (base.CurrentToken.Kind == SyntaxKind.RefKeyword)
				{
					flag = true;
				}
				else
				{
					flag = !this.IsQueryExpression(true, false) && this.ScanType(false) != LanguageParser.ScanTypeFlags.NotType && this.IsTrueIdentifier();
					this.Reset(ref resetPoint);
				}
				if (flag)
				{
					variableDeclarationSyntax = this.ParseVariableDeclaration();
					if (variableDeclarationSyntax.Type.Kind == SyntaxKind.RefType)
					{
						variableDeclarationSyntax = variableDeclarationSyntax.Update(base.CheckFeatureAvailability<TypeSyntax>(variableDeclarationSyntax.Type, MessageID.IDS_FeatureRefFor, false), variableDeclarationSyntax.Variables);
					}
				}
				else if (base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
				{
					this.ParseForStatementExpressionList(ref syntaxToken2, separatedSyntaxListBuilder);
				}
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.SemicolonToken);
				ExpressionSyntax expressionSyntax = null;
				if (base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
				{
					expressionSyntax = this.ParseExpressionCore();
				}
				SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.SemicolonToken);
				if (base.CurrentToken.Kind != SyntaxKind.CloseParenToken)
				{
					this.ParseForStatementExpressionList(ref syntaxToken4, separatedSyntaxListBuilder2);
				}
				SyntaxToken syntaxToken5 = base.EatToken(SyntaxKind.CloseParenToken);
				StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
				forStatementSyntax = this._syntaxFactory.ForStatement(attributes, syntaxToken, syntaxToken2, variableDeclarationSyntax, in separatedSyntaxListBuilder, syntaxToken3, expressionSyntax, syntaxToken4, in separatedSyntaxListBuilder2, syntaxToken5, statementSyntax);
			}
			finally
			{
				this._termState = termState;
				this.Release(ref resetPoint);
				this._pool.Free<ExpressionSyntax>(in separatedSyntaxListBuilder2);
				this._pool.Free<ExpressionSyntax>(in separatedSyntaxListBuilder);
			}
			return forStatementSyntax;
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x00128C2C File Offset: 0x00126E2C
		private bool IsEndOfForStatementArgument()
		{
			return base.CurrentToken.Kind == SyntaxKind.SemicolonToken || base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken;
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x00128C68 File Offset: 0x00126E68
		private void ParseForStatementExpressionList(ref SyntaxToken startToken, SeparatedSyntaxListBuilder<ExpressionSyntax> list)
		{
			if (base.CurrentToken.Kind != SyntaxKind.CloseParenToken && base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
			{
				while (!this.IsPossibleExpression() && base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					if (this.SkipBadForStatementExpressionListTokens(ref startToken, list, SyntaxKind.IdentifierToken) != LanguageParser.PostSkipAction.Continue)
					{
						return;
					}
				}
				list.Add(this.ParseExpressionCore());
				int num = -1;
				while (base.IsMakingProgress(ref num, true))
				{
					if (base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
					{
						return;
					}
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleExpression())
					{
						list.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						list.Add(this.ParseExpressionCore());
					}
					else if (this.SkipBadForStatementExpressionListTokens(ref startToken, list, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
					{
						return;
					}
				}
				return;
			}
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x00128D5C File Offset: 0x00126F5C
		private LanguageParser.PostSkipAction SkipBadForStatementExpressionListTokens(ref SyntaxToken startToken, SeparatedSyntaxListBuilder<ExpressionSyntax> list, SyntaxKind expected)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, ExpressionSyntax>(ref startToken, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleExpression(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseParenToken || p.CurrentToken.Kind == SyntaxKind.SemicolonToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x00128DB0 File Offset: 0x00126FB0
		private CommonForEachStatementSyntax ParseForEachStatement(SyntaxList<AttributeListSyntax> attributes, SyntaxToken awaitTokenOpt)
		{
			SyntaxToken syntaxToken2;
			if (base.CurrentToken.Kind == SyntaxKind.ForKeyword)
			{
				SyntaxToken syntaxToken = base.EatToken();
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_SyntaxError, new object[]
				{
					SyntaxFacts.GetText(SyntaxKind.ForEachKeyword),
					SyntaxFacts.GetText(SyntaxKind.ForKeyword)
				});
				syntaxToken2 = this.ConvertToMissingWithTrailingTrivia(syntaxToken, SyntaxKind.ForEachKeyword);
			}
			else
			{
				syntaxToken2 = base.EatToken(SyntaxKind.ForEachKeyword);
			}
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.OpenParenToken);
			ExpressionSyntax expressionSyntax = this.ParseExpressionOrDeclaration(LanguageParser.ParseTypeMode.Normal, MessageID.IDS_FeatureTuples, true);
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.InKeyword, ErrorCode.ERR_InExpected, true);
			if (!LanguageParser.IsValidForeachVariable(expressionSyntax))
			{
				syntaxToken4 = base.AddError<SyntaxToken>(syntaxToken4, ErrorCode.ERR_BadForeachDecl);
			}
			ExpressionSyntax expressionSyntax2 = this.ParseExpressionCore();
			SyntaxToken syntaxToken5 = base.EatToken(SyntaxKind.CloseParenToken);
			StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
			DeclarationExpressionSyntax declarationExpressionSyntax = expressionSyntax as DeclarationExpressionSyntax;
			if (declarationExpressionSyntax != null)
			{
				if (declarationExpressionSyntax.Type.Kind == SyntaxKind.RefType)
				{
					declarationExpressionSyntax = declarationExpressionSyntax.Update(base.CheckFeatureAvailability<TypeSyntax>(declarationExpressionSyntax.Type, MessageID.IDS_FeatureRefForEach, false), declarationExpressionSyntax.Designation);
				}
				if (declarationExpressionSyntax.designation.Kind != SyntaxKind.ParenthesizedVariableDesignation)
				{
					SyntaxKind kind = declarationExpressionSyntax.designation.Kind;
					SyntaxToken syntaxToken6;
					if (kind != SyntaxKind.SingleVariableDesignation)
					{
						if (kind != SyntaxKind.DiscardDesignation)
						{
							throw ExceptionUtilities.UnexpectedValue(declarationExpressionSyntax.designation.Kind);
						}
						SyntaxToken underscoreToken = ((DiscardDesignationSyntax)declarationExpressionSyntax.designation).underscoreToken;
						syntaxToken6 = SyntaxToken.WithValue<string>(SyntaxKind.IdentifierToken, underscoreToken.LeadingTrivia.Node, underscoreToken.Text, underscoreToken.ValueText, underscoreToken.TrailingTrivia.Node);
					}
					else
					{
						syntaxToken6 = ((SingleVariableDesignationSyntax)declarationExpressionSyntax.designation).identifier;
					}
					return this._syntaxFactory.ForEachStatement(attributes, awaitTokenOpt, syntaxToken2, syntaxToken3, declarationExpressionSyntax.Type, syntaxToken6, syntaxToken4, expressionSyntax2, syntaxToken5, statementSyntax);
				}
			}
			return this._syntaxFactory.ForEachVariableStatement(attributes, awaitTokenOpt, syntaxToken2, syntaxToken3, expressionSyntax, syntaxToken4, expressionSyntax2, syntaxToken5, statementSyntax);
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00128FAF File Offset: 0x001271AF
		private ExpressionSyntax ParseExpressionOrDeclaration(LanguageParser.ParseTypeMode mode, MessageID feature, bool permitTupleDesignation)
		{
			if (!this.IsPossibleDeclarationExpression(mode, permitTupleDesignation))
			{
				return this.ParseSubExpression(LanguageParser.Precedence.Expression);
			}
			return this.ParseDeclarationExpression(mode, feature);
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x00128FCC File Offset: 0x001271CC
		private bool IsPossibleDeclarationExpression(LanguageParser.ParseTypeMode mode, bool permitTupleDesignation)
		{
			if (this.IsInAsync && base.CurrentToken.ContextualKind == SyntaxKind.AwaitKeyword)
			{
				return false;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag2;
			try
			{
				bool flag = this.IsVarType();
				SyntaxToken syntaxToken;
				if (this.ScanType(mode, out syntaxToken) == LanguageParser.ScanTypeFlags.NotType)
				{
					flag2 = false;
				}
				else if (!this.ScanDesignation(permitTupleDesignation && (flag || LanguageParser.IsPredefinedType(syntaxToken.Kind))))
				{
					flag2 = false;
				}
				else if (mode != LanguageParser.ParseTypeMode.AfterTupleComma)
				{
					if (mode == LanguageParser.ParseTypeMode.FirstElementOfPossibleTupleLiteral)
					{
						flag2 = base.CurrentToken.Kind == SyntaxKind.CommaToken;
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag2 = base.CurrentToken.Kind == SyntaxKind.CommaToken || base.CurrentToken.Kind == SyntaxKind.CloseParenToken;
				}
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag2;
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x001290A8 File Offset: 0x001272A8
		private bool IsVarType()
		{
			if (!base.CurrentToken.IsIdentifierVar())
			{
				return false;
			}
			SyntaxKind kind = base.PeekToken(1).Kind;
			if (kind <= SyntaxKind.OpenBracketToken)
			{
				if (kind != SyntaxKind.AsteriskToken && kind != SyntaxKind.OpenBracketToken)
				{
					return true;
				}
			}
			else if (kind != SyntaxKind.LessThanToken && kind - SyntaxKind.DotToken > 1 && kind != SyntaxKind.ColonColonToken)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x00129108 File Offset: 0x00127308
		private static bool IsValidForeachVariable(ExpressionSyntax variable)
		{
			SyntaxKind kind = variable.Kind;
			if (kind != SyntaxKind.IdentifierName)
			{
				return kind == SyntaxKind.TupleExpression || kind == SyntaxKind.DeclarationExpression;
			}
			return ((IdentifierNameSyntax)variable).Identifier.ContextualKind == SyntaxKind.UnderscoreToken;
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00129154 File Offset: 0x00127354
		private GotoStatementSyntax ParseGotoStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.GotoKeyword);
			SyntaxToken syntaxToken2 = null;
			ExpressionSyntax expressionSyntax = null;
			SyntaxKind syntaxKind;
			if (base.CurrentToken.Kind == SyntaxKind.CaseKeyword || base.CurrentToken.Kind == SyntaxKind.DefaultKeyword)
			{
				syntaxToken2 = base.EatToken();
				if (syntaxToken2.Kind == SyntaxKind.CaseKeyword)
				{
					syntaxKind = SyntaxKind.GotoCaseStatement;
					expressionSyntax = this.ParseExpressionCore();
				}
				else
				{
					syntaxKind = SyntaxKind.GotoDefaultStatement;
				}
			}
			else
			{
				syntaxKind = SyntaxKind.GotoStatement;
				expressionSyntax = this.ParseIdentifierName(ErrorCode.ERR_IdentifierExpected);
			}
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.GotoStatement(syntaxKind, attributes, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3);
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x001291F4 File Offset: 0x001273F4
		private IfStatementSyntax ParseIfStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			return this._syntaxFactory.IfStatement(attributes, base.EatToken(SyntaxKind.IfKeyword), base.EatToken(SyntaxKind.OpenParenToken), this.ParseExpressionCore(), base.EatToken(SyntaxKind.CloseParenToken), this.ParseEmbeddedStatement(), this.ParseElseClauseOpt());
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x00129240 File Offset: 0x00127440
		private IfStatementSyntax ParseMisplacedElse(SyntaxList<AttributeListSyntax> attributes)
		{
			return this._syntaxFactory.IfStatement(attributes, base.EatToken(SyntaxKind.IfKeyword, ErrorCode.ERR_ElseCannotStartStatement, true), base.EatToken(SyntaxKind.OpenParenToken), this.ParseExpressionCore(), base.EatToken(SyntaxKind.CloseParenToken), this.ParseExpressionStatement(default(SyntaxList<AttributeListSyntax>)), this.ParseElseClauseOpt());
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x0012929C File Offset: 0x0012749C
		private ElseClauseSyntax ParseElseClauseOpt()
		{
			if (base.CurrentToken.Kind != SyntaxKind.ElseKeyword)
			{
				return null;
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ElseKeyword);
			StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
			return this._syntaxFactory.ElseClause(syntaxToken, statementSyntax);
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x001292E0 File Offset: 0x001274E0
		private LockStatementSyntax ParseLockStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.LockKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
			return this._syntaxFactory.LockStatement(attributes, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, statementSyntax);
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x00129334 File Offset: 0x00127534
		private ReturnStatementSyntax ParseReturnStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ReturnKeyword);
			ExpressionSyntax expressionSyntax = null;
			if (base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
			{
				expressionSyntax = this.ParsePossibleRefExpression();
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.ReturnStatement(attributes, syntaxToken, expressionSyntax, syntaxToken2);
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x00129384 File Offset: 0x00127584
		private YieldStatementSyntax ParseYieldStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
			ExpressionSyntax expressionSyntax = null;
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureIterators, false);
			SyntaxKind syntaxKind;
			SyntaxToken syntaxToken2;
			if (base.CurrentToken.Kind == SyntaxKind.BreakKeyword)
			{
				syntaxKind = SyntaxKind.YieldBreakStatement;
				syntaxToken2 = base.EatToken();
			}
			else
			{
				syntaxKind = SyntaxKind.YieldReturnStatement;
				syntaxToken2 = base.EatToken(SyntaxKind.ReturnKeyword);
				if (base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
				{
					syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_EmptyYield);
				}
				else
				{
					expressionSyntax = this.ParseExpressionCore();
				}
			}
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.YieldStatement(syntaxKind, attributes, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3);
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00129428 File Offset: 0x00127628
		private SwitchStatementSyntax ParseSwitchStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.SwitchKeyword);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			SyntaxToken syntaxToken2;
			SyntaxToken syntaxToken3;
			if (expressionSyntax.Kind == SyntaxKind.ParenthesizedExpression)
			{
				ParenthesizedExpressionSyntax parenthesizedExpressionSyntax = (ParenthesizedExpressionSyntax)expressionSyntax;
				syntaxToken2 = parenthesizedExpressionSyntax.OpenParenToken;
				expressionSyntax = parenthesizedExpressionSyntax.Expression;
				syntaxToken3 = parenthesizedExpressionSyntax.CloseParenToken;
			}
			else if (expressionSyntax.Kind == SyntaxKind.TupleExpression)
			{
				syntaxToken3 = (syntaxToken2 = null);
			}
			else
			{
				syntaxToken2 = SyntaxFactory.MissingToken(SyntaxKind.OpenParenToken);
				expressionSyntax = base.AddError<ExpressionSyntax>(expressionSyntax, ErrorCode.ERR_SwitchGoverningExpressionRequiresParens);
				syntaxToken3 = SyntaxFactory.MissingToken(SyntaxKind.CloseParenToken);
			}
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.OpenBraceToken);
			SyntaxListBuilder<SwitchSectionSyntax> syntaxListBuilder = this._pool.Allocate<SwitchSectionSyntax>();
			SwitchStatementSyntax switchStatementSyntax;
			try
			{
				while (this.IsPossibleSwitchSection())
				{
					SwitchSectionSyntax switchSectionSyntax = this.ParseSwitchSection();
					syntaxListBuilder.Add(switchSectionSyntax);
				}
				SyntaxToken syntaxToken5 = base.EatToken(SyntaxKind.CloseBraceToken);
				switchStatementSyntax = this._syntaxFactory.SwitchStatement(attributes, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, syntaxToken4, syntaxListBuilder, syntaxToken5);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return switchStatementSyntax;
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x00129530 File Offset: 0x00127730
		private bool IsPossibleSwitchSection()
		{
			return base.CurrentToken.Kind == SyntaxKind.CaseKeyword || (base.CurrentToken.Kind == SyntaxKind.DefaultKeyword && base.PeekToken(1).Kind != SyntaxKind.OpenParenToken);
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x00129570 File Offset: 0x00127770
		private SwitchSectionSyntax ParseSwitchSection()
		{
			SyntaxListBuilder<SwitchLabelSyntax> syntaxListBuilder = this._pool.Allocate<SwitchLabelSyntax>();
			SyntaxListBuilder<StatementSyntax> syntaxListBuilder2 = this._pool.Allocate<StatementSyntax>();
			SwitchSectionSyntax switchSectionSyntax;
			try
			{
				do
				{
					SwitchLabelSyntax switchLabelSyntax;
					if (base.CurrentToken.Kind == SyntaxKind.CaseKeyword)
					{
						SyntaxToken syntaxToken = base.EatToken();
						if (base.CurrentToken.Kind == SyntaxKind.ColonToken)
						{
							ExpressionSyntax expressionSyntax = this.ParseIdentifierName(ErrorCode.ERR_ConstantExpected);
							SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.ColonToken);
							switchLabelSyntax = this._syntaxFactory.CaseSwitchLabel(syntaxToken, expressionSyntax, syntaxToken2);
						}
						else
						{
							CSharpSyntaxNode csharpSyntaxNode = this.ParseExpressionOrPatternForSwitchStatement();
							if (base.CurrentToken.ContextualKind == SyntaxKind.WhenKeyword)
							{
								ExpressionSyntax expressionSyntax2 = csharpSyntaxNode as ExpressionSyntax;
								if (expressionSyntax2 != null)
								{
									csharpSyntaxNode = this._syntaxFactory.ConstantPattern(expressionSyntax2);
								}
							}
							if (csharpSyntaxNode.Kind == SyntaxKind.DiscardPattern)
							{
								csharpSyntaxNode = base.AddError<CSharpSyntaxNode>(csharpSyntaxNode, ErrorCode.ERR_DiscardPatternInSwitchStatement);
							}
							PatternSyntax patternSyntax = csharpSyntaxNode as PatternSyntax;
							if (patternSyntax != null)
							{
								WhenClauseSyntax whenClauseSyntax = this.ParseWhenClause(LanguageParser.Precedence.Expression);
								SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.ColonToken);
								switchLabelSyntax = this._syntaxFactory.CasePatternSwitchLabel(syntaxToken, patternSyntax, whenClauseSyntax, syntaxToken2);
								switchLabelSyntax = base.CheckFeatureAvailability<SwitchLabelSyntax>(switchLabelSyntax, MessageID.IDS_FeaturePatternMatching, false);
							}
							else
							{
								SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.ColonToken);
								switchLabelSyntax = this._syntaxFactory.CaseSwitchLabel(syntaxToken, (ExpressionSyntax)csharpSyntaxNode, syntaxToken2);
							}
						}
					}
					else
					{
						SyntaxToken syntaxToken = base.EatToken(SyntaxKind.DefaultKeyword);
						SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.ColonToken);
						switchLabelSyntax = this._syntaxFactory.DefaultSwitchLabel(syntaxToken, syntaxToken2);
					}
					syntaxListBuilder.Add(switchLabelSyntax);
				}
				while (this.IsPossibleSwitchSection());
				CSharpSyntaxNode csharpSyntaxNode2 = syntaxListBuilder[syntaxListBuilder.Count - 1];
				this.ParseStatements(ref csharpSyntaxNode2, syntaxListBuilder2, true);
				syntaxListBuilder[syntaxListBuilder.Count - 1] = (SwitchLabelSyntax)csharpSyntaxNode2;
				switchSectionSyntax = this._syntaxFactory.SwitchSection(syntaxListBuilder, syntaxListBuilder2);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder2);
				this._pool.Free(syntaxListBuilder);
			}
			return switchSectionSyntax;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x00129784 File Offset: 0x00127984
		private ThrowStatementSyntax ParseThrowStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ThrowKeyword);
			ExpressionSyntax expressionSyntax = null;
			if (base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
			{
				expressionSyntax = this.ParseExpressionCore();
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.SemicolonToken);
			return this._syntaxFactory.ThrowStatement(attributes, syntaxToken, expressionSyntax, syntaxToken2);
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x001297D4 File Offset: 0x001279D4
		private UnsafeStatementSyntax ParseUnsafeStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.UnsafeKeyword);
			BlockSyntax blockSyntax = this.ParsePossiblyAttributedBlock();
			return this._syntaxFactory.UnsafeStatement(attributes, syntaxToken, blockSyntax);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x00129804 File Offset: 0x00127A04
		private UsingStatementSyntax ParseUsingStatement(SyntaxList<AttributeListSyntax> attributes, SyntaxToken awaitTokenOpt = null)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.UsingKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			VariableDeclarationSyntax variableDeclarationSyntax = null;
			ExpressionSyntax expressionSyntax = null;
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			this.ParseUsingExpression(ref variableDeclarationSyntax, ref expressionSyntax, ref resetPoint);
			this.Release(ref resetPoint);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
			return this._syntaxFactory.UsingStatement(attributes, awaitTokenOpt, syntaxToken, syntaxToken2, variableDeclarationSyntax, expressionSyntax, syntaxToken3, statementSyntax);
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x00129874 File Offset: 0x00127A74
		private void ParseUsingExpression(ref VariableDeclarationSyntax declaration, ref ExpressionSyntax expression, ref LanguageParser.ResetPoint resetPoint)
		{
			if (this.IsAwaitExpression())
			{
				expression = this.ParseExpressionCore();
				return;
			}
			LanguageParser.ScanTypeFlags scanTypeFlags;
			if (this.IsQueryExpression(true, false))
			{
				scanTypeFlags = LanguageParser.ScanTypeFlags.NotType;
			}
			else
			{
				scanTypeFlags = this.ScanType(false);
			}
			if (scanTypeFlags == LanguageParser.ScanTypeFlags.NullableType)
			{
				if (base.CurrentToken.Kind != SyntaxKind.IdentifierToken)
				{
					this.Reset(ref resetPoint);
					expression = this.ParseExpressionCore();
					return;
				}
				SyntaxKind kind = base.PeekToken(1).Kind;
				if (kind != SyntaxKind.CloseParenToken)
				{
					if (kind != SyntaxKind.EqualsToken)
					{
						if (kind != SyntaxKind.CommaToken)
						{
							this.Reset(ref resetPoint);
							expression = this.ParseExpressionCore();
							return;
						}
					}
					else
					{
						this.Reset(ref resetPoint);
						declaration = this.ParseVariableDeclaration();
						if (base.CurrentToken.Kind == SyntaxKind.ColonToken && declaration.Type.Kind == SyntaxKind.NullableType && SyntaxFacts.IsName(((NullableTypeSyntax)declaration.Type).ElementType.Kind) && declaration.Variables.Count == 1)
						{
							this.Reset(ref resetPoint);
							declaration = null;
							expression = this.ParseExpressionCore();
							return;
						}
						return;
					}
				}
				this.Reset(ref resetPoint);
				declaration = this.ParseVariableDeclaration();
				return;
			}
			else
			{
				if (this.IsUsingStatementVariableDeclaration(scanTypeFlags))
				{
					this.Reset(ref resetPoint);
					declaration = this.ParseVariableDeclaration();
					return;
				}
				this.Reset(ref resetPoint);
				expression = this.ParseExpressionCore();
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x001299B4 File Offset: 0x00127BB4
		private bool IsUsingStatementVariableDeclaration(LanguageParser.ScanTypeFlags st)
		{
			bool flag = st == LanguageParser.ScanTypeFlags.MustBeType && base.CurrentToken.Kind != SyntaxKind.DotToken;
			bool flag2 = st != LanguageParser.ScanTypeFlags.NotType && base.CurrentToken.Kind == SyntaxKind.IdentifierToken;
			bool flag3 = st == LanguageParser.ScanTypeFlags.NonGenericTypeOrExpression || base.PeekToken(1).Kind == SyntaxKind.EqualsToken;
			return flag || (flag2 && flag3);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x00129A18 File Offset: 0x00127C18
		private WhileStatementSyntax ParseWhileStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.WhileKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			StatementSyntax statementSyntax = this.ParseEmbeddedStatement();
			return this._syntaxFactory.WhileStatement(attributes, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, statementSyntax);
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x00129A6C File Offset: 0x00127C6C
		private LabeledStatementSyntax ParseLabeledStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			SyntaxToken syntaxToken = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.ColonToken);
			StatementSyntax statementSyntax;
			if ((statementSyntax = this.ParsePossiblyAttributedStatement()) == null)
			{
				statementSyntax = SyntaxFactory.EmptyStatement(default(SyntaxList<AttributeListSyntax>), base.EatToken(SyntaxKind.SemicolonToken));
			}
			StatementSyntax statementSyntax2 = statementSyntax;
			return this._syntaxFactory.LabeledStatement(attributes, syntaxToken, syntaxToken2, statementSyntax2);
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x00129AC4 File Offset: 0x00127CC4
		private StatementSyntax ParseLocalDeclarationStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			bool flag = false;
			SyntaxToken syntaxToken;
			SyntaxToken syntaxToken2;
			if (this.IsPossibleAwaitUsing())
			{
				syntaxToken = this.ParseAwaitKeyword(MessageID.None);
				syntaxToken2 = base.EatToken();
			}
			else if (base.CurrentToken.Kind == SyntaxKind.UsingKeyword)
			{
				syntaxToken = null;
				syntaxToken2 = base.EatToken();
			}
			else
			{
				syntaxToken = null;
				syntaxToken2 = null;
				flag = true;
			}
			if (syntaxToken2 != null)
			{
				syntaxToken2 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken2, MessageID.IDS_FeatureUsingDeclarations, false);
			}
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			this.ParseDeclarationModifiers(syntaxListBuilder);
			SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<VariableDeclaratorSyntax>();
			StatementSyntax statementSyntax;
			try
			{
				TypeSyntax typeSyntax;
				LocalFunctionStatementSyntax localFunctionStatementSyntax;
				this.ParseLocalDeclaration(separatedSyntaxListBuilder, flag, attributes, syntaxListBuilder.ToList(), out typeSyntax, out localFunctionStatementSyntax);
				if (localFunctionStatementSyntax != null)
				{
					statementSyntax = localFunctionStatementSyntax;
				}
				else if (flag && attributes.Count == 0 && syntaxListBuilder.Count > 0 && LanguageParser.IsAccessibilityModifier(((SyntaxToken)syntaxListBuilder[0]).ContextualKind))
				{
					statementSyntax = null;
				}
				else
				{
					for (int i = 0; i < syntaxListBuilder.Count; i++)
					{
						SyntaxToken syntaxToken3 = (SyntaxToken)syntaxListBuilder[i];
						if (LanguageParser.IsAdditionalLocalFunctionModifier(syntaxToken3.ContextualKind))
						{
							syntaxListBuilder[i] = base.AddError<SyntaxToken>(syntaxToken3, ErrorCode.ERR_BadMemberFlag, new object[] { syntaxToken3.Text });
						}
					}
					SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.SemicolonToken);
					statementSyntax = this._syntaxFactory.LocalDeclarationStatement(attributes, syntaxToken, syntaxToken2, syntaxListBuilder.ToList(), this._syntaxFactory.VariableDeclaration(typeSyntax, in separatedSyntaxListBuilder), syntaxToken4);
				}
			}
			finally
			{
				this._pool.Free<VariableDeclaratorSyntax>(in separatedSyntaxListBuilder);
				this._pool.Free(syntaxListBuilder);
			}
			return statementSyntax;
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x00129C5C File Offset: 0x00127E5C
		private VariableDesignationSyntax ParseDesignation(bool forPattern)
		{
			VariableDesignationSyntax variableDesignationSyntax;
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenParenToken);
				SeparatedSyntaxListBuilder<VariableDesignationSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<VariableDesignationSyntax>();
				bool flag = false;
				if (forPattern)
				{
					flag = base.CurrentToken.Kind == SyntaxKind.CloseParenToken;
				}
				else
				{
					separatedSyntaxListBuilder.Add(this.ParseDesignation(forPattern));
					separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
				}
				if (!flag)
				{
					for (;;)
					{
						separatedSyntaxListBuilder.Add(this.ParseDesignation(forPattern));
						if (base.CurrentToken.Kind != SyntaxKind.CommaToken)
						{
							break;
						}
						separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
					}
				}
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseParenToken);
				variableDesignationSyntax = this._syntaxFactory.ParenthesizedVariableDesignation(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
				this._pool.Free<VariableDesignationSyntax>(in separatedSyntaxListBuilder);
			}
			else
			{
				variableDesignationSyntax = this.ParseSimpleDesignation();
			}
			return variableDesignationSyntax;
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x00129D44 File Offset: 0x00127F44
		private VariableDesignationSyntax ParseSimpleDesignation()
		{
			if (base.CurrentToken.ContextualKind == SyntaxKind.UnderscoreToken)
			{
				SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.UnderscoreToken, true);
				return this._syntaxFactory.DiscardDesignation(syntaxToken);
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.IdentifierToken);
			return this._syntaxFactory.SingleVariableDesignation(syntaxToken2);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x00129D98 File Offset: 0x00127F98
		private WhenClauseSyntax ParseWhenClause(LanguageParser.Precedence precedence)
		{
			if (base.CurrentToken.ContextualKind != SyntaxKind.WhenKeyword)
			{
				return null;
			}
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.WhenKeyword, true);
			ExpressionSyntax expressionSyntax = this.ParseSubExpression(precedence);
			return this._syntaxFactory.WhenClause(syntaxToken, expressionSyntax);
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x00129DDC File Offset: 0x00127FDC
		private VariableDeclarationSyntax ParseVariableDeclaration()
		{
			SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<VariableDeclaratorSyntax>();
			TypeSyntax typeSyntax;
			LocalFunctionStatementSyntax localFunctionStatementSyntax;
			this.ParseLocalDeclaration(separatedSyntaxListBuilder, false, default(SyntaxList<AttributeListSyntax>), default(SyntaxList<SyntaxToken>), out typeSyntax, out localFunctionStatementSyntax);
			VariableDeclarationSyntax variableDeclarationSyntax = this._syntaxFactory.VariableDeclaration(typeSyntax, in separatedSyntaxListBuilder);
			this._pool.Free<VariableDeclaratorSyntax>(in separatedSyntaxListBuilder);
			return variableDeclarationSyntax;
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x00129E34 File Offset: 0x00128034
		private void ParseLocalDeclaration(SeparatedSyntaxListBuilder<VariableDeclaratorSyntax> variables, bool allowLocalFunctions, SyntaxList<AttributeListSyntax> attributes, SyntaxList<SyntaxToken> mods, out TypeSyntax type, out LocalFunctionStatementSyntax localFunction)
		{
			type = (allowLocalFunctions ? this.ParseReturnType() : this.ParseType(LanguageParser.ParseTypeMode.Normal));
			LanguageParser.VariableFlags variableFlags = LanguageParser.VariableFlags.Local;
			if (mods.Any(8350))
			{
				variableFlags |= LanguageParser.VariableFlags.Const;
			}
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfDeclarationClause;
			this.ParseVariableDeclarators(type, variableFlags, variables, true, allowLocalFunctions, attributes, mods, out localFunction);
			this._termState = termState;
			if (allowLocalFunctions && localFunction == null)
			{
				PredefinedTypeSyntax predefinedTypeSyntax = type as PredefinedTypeSyntax;
				if (predefinedTypeSyntax != null && predefinedTypeSyntax.Keyword.Kind == SyntaxKind.VoidKeyword)
				{
					type = base.AddError<TypeSyntax>(type, ErrorCode.ERR_NoVoidHere);
				}
			}
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x00129ED8 File Offset: 0x001280D8
		private bool IsEndOfDeclarationClause()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			return kind == SyntaxKind.CloseParenToken || kind - SyntaxKind.ColonToken <= 1;
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x00129F08 File Offset: 0x00128108
		private void ParseDeclarationModifiers(SyntaxListBuilder list)
		{
			SyntaxKind contextualKind;
			while (LanguageParser.IsDeclarationModifier(contextualKind = base.CurrentToken.ContextualKind) || LanguageParser.IsAdditionalLocalFunctionModifier(contextualKind))
			{
				SyntaxToken syntaxToken;
				if (contextualKind == SyntaxKind.AsyncKeyword)
				{
					LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
					bool flag = !LanguageParser.IsPossibleStartOfTypeDeclaration(base.EatToken().Kind) && !LanguageParser.IsDeclarationModifier(base.CurrentToken.Kind) && !LanguageParser.IsAdditionalLocalFunctionModifier(base.CurrentToken.Kind) && (this.ScanType(false) == LanguageParser.ScanTypeFlags.NotType || base.CurrentToken.Kind != SyntaxKind.IdentifierToken);
					this.Reset(ref resetPoint);
					this.Release(ref resetPoint);
					if (flag)
					{
						break;
					}
					syntaxToken = base.EatContextualToken(contextualKind, true);
					if (contextualKind == SyntaxKind.AsyncKeyword)
					{
						syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureAsync, false);
					}
				}
				else
				{
					syntaxToken = base.EatToken();
				}
				if (contextualKind == SyntaxKind.ReadOnlyKeyword || contextualKind == SyntaxKind.VolatileKeyword)
				{
					syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_BadMemberFlag, new object[] { syntaxToken.Text });
				}
				else if (list.Any(syntaxToken.RawKind))
				{
					syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_TypeExpected, new object[] { syntaxToken.Text });
				}
				list.Add(syntaxToken);
			}
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0012A041 File Offset: 0x00128241
		private static bool IsDeclarationModifier(SyntaxKind kind)
		{
			switch (kind)
			{
			case SyntaxKind.StaticKeyword:
			case SyntaxKind.ReadOnlyKeyword:
			case SyntaxKind.ConstKeyword:
			case SyntaxKind.VolatileKeyword:
				return true;
			}
			return false;
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0012A070 File Offset: 0x00128270
		private static bool IsAdditionalLocalFunctionModifier(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.ExternKeyword)
			{
				if (kind - SyntaxKind.PublicKeyword > 4 && kind != SyntaxKind.ExternKeyword)
				{
					return false;
				}
			}
			else if (kind != SyntaxKind.UnsafeKeyword && kind != SyntaxKind.AsyncKeyword)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x0012A0A1 File Offset: 0x001282A1
		private static bool IsAccessibilityModifier(SyntaxKind kind)
		{
			return kind - SyntaxKind.PublicKeyword <= 3;
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x0012A0B0 File Offset: 0x001282B0
		private LocalFunctionStatementSyntax TryParseLocalFunctionStatementBody(SyntaxList<AttributeListSyntax> attributes, SyntaxList<SyntaxToken> modifiers, TypeSyntax type, SyntaxToken identifier)
		{
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag = true;
			if (type.Kind == SyntaxKind.IdentifierName)
			{
				flag = ((IdentifierNameSyntax)type).Identifier.ContextualKind != SyntaxKind.AwaitKeyword;
			}
			bool isInAsync = this.IsInAsync;
			this.IsInAsync = false;
			SyntaxListBuilder syntaxListBuilder = null;
			int i = 0;
			while (i < modifiers.Count)
			{
				SyntaxToken syntaxToken = modifiers[i];
				SyntaxKind contextualKind = syntaxToken.ContextualKind;
				if (contextualKind <= SyntaxKind.VolatileKeyword)
				{
					if (contextualKind != SyntaxKind.StaticKeyword)
					{
						if (contextualKind != SyntaxKind.ReadOnlyKeyword && contextualKind != SyntaxKind.VolatileKeyword)
						{
							goto IL_00F1;
						}
					}
					else
					{
						syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureStaticLocalFunctions, false);
						if (syntaxToken != modifiers[i])
						{
							goto IL_010D;
						}
					}
				}
				else if (contextualKind != SyntaxKind.ExternKeyword)
				{
					if (contextualKind != SyntaxKind.UnsafeKeyword)
					{
						if (contextualKind != SyntaxKind.AsyncKeyword)
						{
							goto IL_00F1;
						}
						this.IsInAsync = true;
						flag = true;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureExternLocalFunctions, false);
					if (syntaxToken != modifiers[i])
					{
						goto IL_010D;
					}
				}
				IL_012D:
				i++;
				continue;
				IL_010D:
				if (syntaxListBuilder == null)
				{
					syntaxListBuilder = this._pool.Allocate();
					syntaxListBuilder.AddRange<SyntaxToken>(modifiers);
				}
				syntaxListBuilder[i] = syntaxToken;
				goto IL_012D;
				IL_00F1:
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_BadMemberFlag, new object[] { syntaxToken.Text });
				goto IL_010D;
			}
			if (syntaxListBuilder != null)
			{
				modifiers = syntaxListBuilder.ToList();
				this._pool.Free(syntaxListBuilder);
			}
			TypeParameterListSyntax typeParameterListSyntax = this.ParseTypeParameterList();
			ParameterListSyntax parameterListSyntax = this.ParseParenthesizedParameterList();
			if (!flag)
			{
				SeparatedSyntaxList<ParameterSyntax> parameters = parameterListSyntax.Parameters;
				for (int j = 0; j < parameters.Count; j++)
				{
					flag |= !parameters[j].ContainsDiagnostics;
					if (flag)
					{
						break;
					}
				}
			}
			SyntaxListBuilder<TypeParameterConstraintClauseSyntax> syntaxListBuilder2 = default(SyntaxListBuilder<TypeParameterConstraintClauseSyntax>);
			if (base.CurrentToken.ContextualKind == SyntaxKind.WhereKeyword)
			{
				syntaxListBuilder2 = this._pool.Allocate<TypeParameterConstraintClauseSyntax>();
				this.ParseTypeParameterConstraintClauses(syntaxListBuilder2);
				flag = true;
			}
			BlockSyntax blockSyntax;
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax;
			SyntaxToken syntaxToken2;
			this.ParseBlockAndExpressionBodiesWithSemicolon(out blockSyntax, out arrowExpressionClauseSyntax, out syntaxToken2, false, MessageID.IDS_FeatureExpressionBodiedMethod);
			this.IsInAsync = isInAsync;
			if (!flag && blockSyntax == null && arrowExpressionClauseSyntax == null)
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
				return null;
			}
			this.Release(ref resetPoint);
			identifier = base.CheckFeatureAvailability<SyntaxToken>(identifier, MessageID.IDS_FeatureLocalFunctions, false);
			return this._syntaxFactory.LocalFunctionStatement(attributes, modifiers, type, identifier, typeParameterListSyntax, parameterListSyntax, syntaxListBuilder2, blockSyntax, arrowExpressionClauseSyntax, syntaxToken2);
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x0012A308 File Offset: 0x00128508
		private ExpressionStatementSyntax ParseExpressionStatement(SyntaxList<AttributeListSyntax> attributes)
		{
			return this.ParseExpressionStatement(attributes, this.ParseExpressionCore());
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x0012A318 File Offset: 0x00128518
		private ExpressionStatementSyntax ParseExpressionStatement(SyntaxList<AttributeListSyntax> attributes, ExpressionSyntax expression)
		{
			SyntaxToken syntaxToken;
			if (base.IsScript && base.CurrentToken.Kind == SyntaxKind.EndOfFileToken)
			{
				syntaxToken = SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken);
			}
			else
			{
				syntaxToken = base.EatToken(SyntaxKind.SemicolonToken);
			}
			return this._syntaxFactory.ExpressionStatement(attributes, expression, syntaxToken);
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x0012A366 File Offset: 0x00128566
		public ExpressionSyntax ParseExpression()
		{
			return this.ParseWithStackGuard<ExpressionSyntax>(new Func<ExpressionSyntax>(this.ParseExpressionCore), new Func<ExpressionSyntax>(this.CreateMissingIdentifierName));
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x0012A386 File Offset: 0x00128586
		private ExpressionSyntax ParseExpressionCore()
		{
			return this.ParseSubExpression(LanguageParser.Precedence.Expression);
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x0012A38F File Offset: 0x0012858F
		private bool CanStartExpression()
		{
			return this.IsPossibleExpression(false, false, false);
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x0012A39A File Offset: 0x0012859A
		private bool IsPossibleExpression()
		{
			return this.IsPossibleExpression(true, true, true);
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0012A3A8 File Offset: 0x001285A8
		private bool IsPossibleExpression(bool allowBinaryExpressions, bool allowAssignmentExpressions, bool allowAttributes)
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.ThrowKeyword)
			{
				if (kind <= SyntaxKind.DotDotToken)
				{
					if (kind != SyntaxKind.OpenParenToken)
					{
						if (kind == SyntaxKind.OpenBracketToken)
						{
							return allowAttributes && this.IsPossibleLambdaExpression(LanguageParser.Precedence.Expression);
						}
						if (kind != SyntaxKind.DotDotToken)
						{
							goto IL_014E;
						}
					}
				}
				else if (kind <= SyntaxKind.FalseKeyword)
				{
					if (kind != SyntaxKind.ColonColonToken && kind - SyntaxKind.TypeOfKeyword > 4)
					{
						goto IL_014E;
					}
				}
				else if (kind != SyntaxKind.DefaultKeyword && kind != SyntaxKind.ThrowKeyword)
				{
					goto IL_014E;
				}
			}
			else if (kind <= SyntaxKind.RefKeyword)
			{
				if (kind <= SyntaxKind.StackAllocKeyword)
				{
					if (kind == SyntaxKind.StaticKeyword)
					{
						return this.IsPossibleAnonymousMethodExpression() || this.IsPossibleLambdaExpression(LanguageParser.Precedence.Expression);
					}
					if (kind != SyntaxKind.StackAllocKeyword)
					{
						goto IL_014E;
					}
				}
				else if (kind != SyntaxKind.NewKeyword && kind != SyntaxKind.RefKeyword)
				{
					goto IL_014E;
				}
			}
			else if (kind <= SyntaxKind.UncheckedKeyword)
			{
				if (kind - SyntaxKind.ArgListKeyword > 5 && kind - SyntaxKind.DelegateKeyword > 2)
				{
					goto IL_014E;
				}
			}
			else if (kind != SyntaxKind.InterpolatedStringStartToken)
			{
				switch (kind)
				{
				case SyntaxKind.IdentifierToken:
					return this.IsTrueIdentifier() || base.CurrentToken.ContextualKind == SyntaxKind.FromKeyword;
				case SyntaxKind.NumericLiteralToken:
				case SyntaxKind.CharacterLiteralToken:
				case SyntaxKind.StringLiteralToken:
				case SyntaxKind.InterpolatedStringToken:
					break;
				case SyntaxKind.XmlEntityLiteralToken:
				case SyntaxKind.XmlTextLiteralToken:
				case SyntaxKind.XmlTextLiteralNewLineToken:
					goto IL_014E;
				default:
					goto IL_014E;
				}
			}
			return true;
			IL_014E:
			return (LanguageParser.IsPredefinedType(kind) && kind != SyntaxKind.VoidKeyword) || SyntaxFacts.IsAnyUnaryExpression(kind) || (allowBinaryExpressions && SyntaxFacts.IsBinaryExpression(kind)) || (allowAssignmentExpressions && SyntaxFacts.IsAssignmentExpressionOperatorToken(kind));
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0012A534 File Offset: 0x00128734
		private static bool IsInvalidSubExpression(SyntaxKind kind)
		{
			switch (kind)
			{
			case SyntaxKind.IfKeyword:
			case SyntaxKind.ElseKeyword:
			case SyntaxKind.WhileKeyword:
			case SyntaxKind.ForKeyword:
			case SyntaxKind.ForEachKeyword:
			case SyntaxKind.DoKeyword:
			case SyntaxKind.SwitchKeyword:
			case SyntaxKind.CaseKeyword:
			case SyntaxKind.TryKeyword:
			case SyntaxKind.CatchKeyword:
			case SyntaxKind.FinallyKeyword:
			case SyntaxKind.LockKeyword:
			case SyntaxKind.GotoKeyword:
			case SyntaxKind.BreakKeyword:
			case SyntaxKind.ContinueKeyword:
			case SyntaxKind.ReturnKeyword:
			case SyntaxKind.ConstKeyword:
				break;
			case SyntaxKind.DefaultKeyword:
			case SyntaxKind.ThrowKeyword:
			case SyntaxKind.PublicKeyword:
			case SyntaxKind.PrivateKeyword:
			case SyntaxKind.InternalKeyword:
			case SyntaxKind.ProtectedKeyword:
			case SyntaxKind.StaticKeyword:
			case SyntaxKind.ReadOnlyKeyword:
			case SyntaxKind.SealedKeyword:
				return false;
			default:
				if (kind != SyntaxKind.UsingKeyword)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0012A5C0 File Offset: 0x001287C0
		internal static bool IsRightAssociative(SyntaxKind op)
		{
			return op == SyntaxKind.CoalesceExpression || op - SyntaxKind.SimpleAssignmentExpression <= 11;
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0012A5D8 File Offset: 0x001287D8
		private static LanguageParser.Precedence GetPrecedence(SyntaxKind op)
		{
			if (op <= SyntaxKind.SwitchExpression)
			{
				switch (op)
				{
				case SyntaxKind.IdentifierName:
				case SyntaxKind.GenericName:
				case SyntaxKind.AliasQualifiedName:
				case SyntaxKind.PredefinedType:
				case SyntaxKind.ParenthesizedExpression:
				case SyntaxKind.InvocationExpression:
				case SyntaxKind.ElementAccessExpression:
				case SyntaxKind.ObjectCreationExpression:
				case SyntaxKind.AnonymousObjectCreationExpression:
				case SyntaxKind.ArrayCreationExpression:
				case SyntaxKind.ImplicitArrayCreationExpression:
				case SyntaxKind.StackAllocArrayCreationExpression:
				case SyntaxKind.InterpolatedStringExpression:
				case SyntaxKind.ImplicitObjectCreationExpression:
				case SyntaxKind.SimpleMemberAccessExpression:
				case SyntaxKind.PointerMemberAccessExpression:
				case SyntaxKind.ConditionalAccessExpression:
				case SyntaxKind.PostIncrementExpression:
				case SyntaxKind.PostDecrementExpression:
				case SyntaxKind.ThisExpression:
				case SyntaxKind.BaseExpression:
				case SyntaxKind.ArgListExpression:
				case SyntaxKind.NumericLiteralExpression:
				case SyntaxKind.StringLiteralExpression:
				case SyntaxKind.CharacterLiteralExpression:
				case SyntaxKind.TrueLiteralExpression:
				case SyntaxKind.FalseLiteralExpression:
				case SyntaxKind.NullLiteralExpression:
				case SyntaxKind.DefaultLiteralExpression:
				case SyntaxKind.DefaultExpression:
					return LanguageParser.Precedence.Primary;
				case SyntaxKind.QualifiedName:
				case SyntaxKind.TypeArgumentList:
				case SyntaxKind.ArrayType:
				case SyntaxKind.ArrayRankSpecifier:
				case SyntaxKind.PointerType:
				case SyntaxKind.NullableType:
				case SyntaxKind.OmittedTypeArgument:
				case (SyntaxKind)8627:
				case (SyntaxKind)8628:
				case (SyntaxKind)8629:
				case (SyntaxKind)8630:
				case (SyntaxKind)8631:
				case SyntaxKind.ArgumentList:
				case SyntaxKind.BracketedArgumentList:
				case SyntaxKind.Argument:
				case SyntaxKind.NameColon:
				case SyntaxKind.ObjectInitializerExpression:
				case SyntaxKind.CollectionInitializerExpression:
				case SyntaxKind.ArrayInitializerExpression:
				case SyntaxKind.AnonymousObjectMemberDeclarator:
				case SyntaxKind.ComplexElementInitializerExpression:
				case SyntaxKind.OmittedArraySizeExpression:
				case SyntaxKind.ImplicitElementAccess:
				case (SyntaxKind)8660:
				case (SyntaxKind)8661:
				case (SyntaxKind)8662:
				case (SyntaxKind)8663:
				case (SyntaxKind)8664:
				case (SyntaxKind)8665:
				case (SyntaxKind)8666:
				case (SyntaxKind)8667:
				case (SyntaxKind)8692:
				case (SyntaxKind)8693:
				case (SyntaxKind)8694:
				case (SyntaxKind)8695:
				case (SyntaxKind)8696:
				case (SyntaxKind)8697:
				case (SyntaxKind)8698:
				case (SyntaxKind)8699:
				case (SyntaxKind)8700:
				case (SyntaxKind)8701:
				case (SyntaxKind)8702:
				case (SyntaxKind)8703:
				case (SyntaxKind)8704:
				case (SyntaxKind)8705:
				case (SyntaxKind)8706:
				case SyntaxKind.MemberBindingExpression:
				case SyntaxKind.ElementBindingExpression:
				case (SyntaxKind)8709:
				case (SyntaxKind)8710:
				case (SyntaxKind)8711:
				case (SyntaxKind)8712:
				case (SyntaxKind)8713:
				case (SyntaxKind)8726:
				case (SyntaxKind)8727:
				case (SyntaxKind)8728:
				case (SyntaxKind)8729:
				case (SyntaxKind)8742:
				case (SyntaxKind)8743:
				case (SyntaxKind)8744:
				case (SyntaxKind)8745:
				case (SyntaxKind)8756:
				case (SyntaxKind)8757:
				case (SyntaxKind)8758:
				case (SyntaxKind)8759:
				case (SyntaxKind)8768:
				case (SyntaxKind)8769:
				case (SyntaxKind)8770:
				case (SyntaxKind)8771:
				case (SyntaxKind)8772:
				case (SyntaxKind)8773:
					goto IL_030E;
				case SyntaxKind.ConditionalExpression:
					return LanguageParser.Precedence.Expression;
				case SyntaxKind.CastExpression:
					return LanguageParser.Precedence.Cast;
				case SyntaxKind.AnonymousMethodExpression:
				case SyntaxKind.SimpleLambdaExpression:
				case SyntaxKind.ParenthesizedLambdaExpression:
					return LanguageParser.Precedence.Expression;
				case SyntaxKind.IsPatternExpression:
				case SyntaxKind.LessThanExpression:
				case SyntaxKind.LessThanOrEqualExpression:
				case SyntaxKind.GreaterThanExpression:
				case SyntaxKind.GreaterThanOrEqualExpression:
				case SyntaxKind.IsExpression:
				case SyntaxKind.AsExpression:
					return LanguageParser.Precedence.Relational;
				case SyntaxKind.RangeExpression:
					return LanguageParser.Precedence.Range;
				case SyntaxKind.AddExpression:
				case SyntaxKind.SubtractExpression:
					return LanguageParser.Precedence.Additive;
				case SyntaxKind.MultiplyExpression:
				case SyntaxKind.DivideExpression:
				case SyntaxKind.ModuloExpression:
					return LanguageParser.Precedence.Mutiplicative;
				case SyntaxKind.LeftShiftExpression:
				case SyntaxKind.RightShiftExpression:
					return LanguageParser.Precedence.Shift;
				case SyntaxKind.LogicalOrExpression:
					return LanguageParser.Precedence.ConditionalOr;
				case SyntaxKind.LogicalAndExpression:
					return LanguageParser.Precedence.ConditionalAnd;
				case SyntaxKind.BitwiseOrExpression:
					return LanguageParser.Precedence.LogicalOr;
				case SyntaxKind.BitwiseAndExpression:
					return LanguageParser.Precedence.LogicalAnd;
				case SyntaxKind.ExclusiveOrExpression:
					return LanguageParser.Precedence.LogicalXor;
				case SyntaxKind.EqualsExpression:
				case SyntaxKind.NotEqualsExpression:
					return LanguageParser.Precedence.Equality;
				case SyntaxKind.CoalesceExpression:
					break;
				case SyntaxKind.SimpleAssignmentExpression:
				case SyntaxKind.AddAssignmentExpression:
				case SyntaxKind.SubtractAssignmentExpression:
				case SyntaxKind.MultiplyAssignmentExpression:
				case SyntaxKind.DivideAssignmentExpression:
				case SyntaxKind.ModuloAssignmentExpression:
				case SyntaxKind.AndAssignmentExpression:
				case SyntaxKind.ExclusiveOrAssignmentExpression:
				case SyntaxKind.OrAssignmentExpression:
				case SyntaxKind.LeftShiftAssignmentExpression:
				case SyntaxKind.RightShiftAssignmentExpression:
				case SyntaxKind.CoalesceAssignmentExpression:
					return LanguageParser.Precedence.Expression;
				case SyntaxKind.UnaryPlusExpression:
				case SyntaxKind.UnaryMinusExpression:
				case SyntaxKind.BitwiseNotExpression:
				case SyntaxKind.LogicalNotExpression:
				case SyntaxKind.PreIncrementExpression:
				case SyntaxKind.PreDecrementExpression:
				case SyntaxKind.AwaitExpression:
				case SyntaxKind.IndexExpression:
				case SyntaxKind.TypeOfExpression:
				case SyntaxKind.SizeOfExpression:
				case SyntaxKind.CheckedExpression:
				case SyntaxKind.UncheckedExpression:
				case SyntaxKind.MakeRefExpression:
				case SyntaxKind.RefValueExpression:
				case SyntaxKind.RefTypeExpression:
					return LanguageParser.Precedence.Unary;
				case SyntaxKind.PointerIndirectionExpression:
					return LanguageParser.Precedence.PointerIndirection;
				case SyntaxKind.AddressOfExpression:
					return LanguageParser.Precedence.AddressOf;
				case SyntaxKind.QueryExpression:
					return LanguageParser.Precedence.Expression;
				default:
					if (op == SyntaxKind.TupleExpression)
					{
						return LanguageParser.Precedence.Primary;
					}
					if (op != SyntaxKind.SwitchExpression)
					{
						goto IL_030E;
					}
					return LanguageParser.Precedence.Switch;
				}
			}
			else
			{
				if (op == SyntaxKind.DeclarationExpression)
				{
					return LanguageParser.Precedence.Primary;
				}
				switch (op)
				{
				case SyntaxKind.RefExpression:
				case SyntaxKind.ImplicitStackAllocArrayCreationExpression:
				case SyntaxKind.SuppressNullableWarningExpression:
					return LanguageParser.Precedence.Primary;
				case SyntaxKind.RefType:
					goto IL_030E;
				case SyntaxKind.ThrowExpression:
					break;
				default:
					if (op != SyntaxKind.WithExpression)
					{
						goto IL_030E;
					}
					return LanguageParser.Precedence.Switch;
				}
			}
			return LanguageParser.Precedence.Coalescing;
			IL_030E:
			throw ExceptionUtilities.UnexpectedValue(op);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0012A8FE File Offset: 0x00128AFE
		private static bool IsExpectedPrefixUnaryOperator(SyntaxKind kind)
		{
			return SyntaxFacts.IsPrefixUnaryExpression(kind) && kind != SyntaxKind.RefKeyword && kind != SyntaxKind.OutKeyword;
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0012A91D File Offset: 0x00128B1D
		private static bool IsExpectedBinaryOperator(SyntaxKind kind)
		{
			return SyntaxFacts.IsBinaryExpression(kind);
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x0012A925 File Offset: 0x00128B25
		private static bool IsExpectedAssignmentOperator(SyntaxKind kind)
		{
			return SyntaxFacts.IsAssignmentExpressionOperatorToken(kind);
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x0012A92D File Offset: 0x00128B2D
		private bool IsPossibleAwaitExpressionStatement()
		{
			return (base.IsScript || this.IsInAsync) && base.CurrentToken.ContextualKind == SyntaxKind.AwaitKeyword;
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0012A954 File Offset: 0x00128B54
		private bool IsAwaitExpression()
		{
			if (base.CurrentToken.ContextualKind == SyntaxKind.AwaitKeyword)
			{
				if (this.IsInAsync)
				{
					return true;
				}
				SyntaxToken syntaxToken = base.PeekToken(1);
				SyntaxKind kind = syntaxToken.Kind;
				if (kind <= SyntaxKind.NewKeyword)
				{
					if (kind <= SyntaxKind.FalseKeyword)
					{
						if (kind != SyntaxKind.TypeOfKeyword && kind - SyntaxKind.NullKeyword > 2)
						{
							return false;
						}
					}
					else if (kind != SyntaxKind.DefaultKeyword && kind != SyntaxKind.NewKeyword)
					{
						return false;
					}
				}
				else if (kind <= SyntaxKind.UncheckedKeyword)
				{
					if (kind - SyntaxKind.ThisKeyword > 1 && kind - SyntaxKind.DelegateKeyword > 2)
					{
						return false;
					}
				}
				else if (kind != SyntaxKind.InterpolatedStringStartToken)
				{
					switch (kind)
					{
					case SyntaxKind.IdentifierToken:
						return syntaxToken.ContextualKind != SyntaxKind.WithKeyword;
					case SyntaxKind.NumericLiteralToken:
					case SyntaxKind.CharacterLiteralToken:
					case SyntaxKind.StringLiteralToken:
					case SyntaxKind.InterpolatedStringToken:
						break;
					case SyntaxKind.XmlEntityLiteralToken:
					case SyntaxKind.XmlTextLiteralToken:
					case SyntaxKind.XmlTextLiteralNewLineToken:
						return false;
					default:
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0012AA30 File Offset: 0x00128C30
		private ExpressionSyntax ParseSubExpression(LanguageParser.Precedence precedence)
		{
			this._recursionDepth++;
			StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
			ExpressionSyntax expressionSyntax = this.ParseSubExpressionCore(precedence);
			this._recursionDepth--;
			return expressionSyntax;
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x0012AA60 File Offset: 0x00128C60
		private ExpressionSyntax ParseSubExpressionCore(LanguageParser.Precedence precedence)
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (LanguageParser.IsInvalidSubExpression(kind))
			{
				return base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_InvalidExprTerm, new object[] { SyntaxFacts.GetText(kind) });
			}
			ExpressionSyntax expressionSyntax2;
			if (LanguageParser.IsExpectedPrefixUnaryOperator(kind))
			{
				SyntaxKind prefixUnaryExpression = SyntaxFacts.GetPrefixUnaryExpression(kind);
				LanguageParser.Precedence precedence2 = LanguageParser.GetPrecedence(prefixUnaryExpression);
				SyntaxToken syntaxToken = base.EatToken();
				ExpressionSyntax expressionSyntax = this.ParseSubExpression(precedence2);
				expressionSyntax2 = this._syntaxFactory.PrefixUnaryExpression(prefixUnaryExpression, syntaxToken, expressionSyntax);
			}
			else if (kind == SyntaxKind.DotDotToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken();
				LanguageParser.Precedence precedence2 = LanguageParser.GetPrecedence(SyntaxKind.RangeExpression);
				ExpressionSyntax expressionSyntax3;
				if (this.CanStartExpression())
				{
					expressionSyntax3 = this.ParseSubExpression(precedence2);
				}
				else
				{
					expressionSyntax3 = null;
				}
				expressionSyntax2 = this._syntaxFactory.RangeExpression(null, syntaxToken2, expressionSyntax3);
			}
			else if (this.IsAwaitExpression())
			{
				LanguageParser.Precedence precedence2 = LanguageParser.GetPrecedence(SyntaxKind.AwaitExpression);
				SyntaxToken syntaxToken3 = base.EatContextualToken(SyntaxKind.AwaitKeyword, true);
				syntaxToken3 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken3, MessageID.IDS_FeatureAsync, false);
				ExpressionSyntax expressionSyntax4 = this.ParseSubExpression(precedence2);
				expressionSyntax2 = this._syntaxFactory.AwaitExpression(syntaxToken3, expressionSyntax4);
			}
			else if (this.IsQueryExpression(false, false))
			{
				expressionSyntax2 = this.ParseQueryExpression(precedence);
			}
			else if (base.CurrentToken.ContextualKind == SyntaxKind.FromKeyword && this.IsInQuery)
			{
				SyntaxToken syntaxToken4 = base.EatToken();
				syntaxToken4 = base.AddError<SyntaxToken>(syntaxToken4, ErrorCode.ERR_InvalidExprTerm, new object[] { base.CurrentToken.Text });
				expressionSyntax2 = base.AddTrailingSkippedSyntax<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), syntaxToken4);
			}
			else if (kind == SyntaxKind.ThrowKeyword)
			{
				ExpressionSyntax expressionSyntax5 = this.ParseThrowExpression();
				if (precedence > LanguageParser.Precedence.Coalescing)
				{
					return base.AddError<ExpressionSyntax>(expressionSyntax5, ErrorCode.ERR_InvalidExprTerm, new object[] { SyntaxFacts.GetText(kind) });
				}
				return expressionSyntax5;
			}
			else if (this.IsPossibleDeconstructionLeft(precedence))
			{
				expressionSyntax2 = this.ParseDeclarationExpression(LanguageParser.ParseTypeMode.Normal, MessageID.IDS_FeatureTuples);
			}
			else
			{
				expressionSyntax2 = this.ParseTerm(precedence);
			}
			return this.ParseExpressionContinued(expressionSyntax2, precedence);
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x0012AC40 File Offset: 0x00128E40
		private ExpressionSyntax ParseExpressionContinued(ExpressionSyntax leftOperand, LanguageParser.Precedence precedence)
		{
			for (;;)
			{
				SyntaxKind contextualKind = base.CurrentToken.ContextualKind;
				bool flag = false;
				SyntaxKind syntaxKind;
				if (LanguageParser.IsExpectedBinaryOperator(contextualKind))
				{
					syntaxKind = SyntaxFacts.GetBinaryExpression(contextualKind);
				}
				else if (LanguageParser.IsExpectedAssignmentOperator(contextualKind))
				{
					syntaxKind = SyntaxFacts.GetAssignmentExpression(contextualKind);
					flag = true;
				}
				else if (contextualKind == SyntaxKind.DotDotToken)
				{
					syntaxKind = SyntaxKind.RangeExpression;
				}
				else if (contextualKind == SyntaxKind.SwitchKeyword && base.PeekToken(1).Kind == SyntaxKind.OpenBraceToken)
				{
					syntaxKind = SyntaxKind.SwitchExpression;
				}
				else
				{
					if (contextualKind != SyntaxKind.WithKeyword || base.PeekToken(1).Kind != SyntaxKind.OpenBraceToken)
					{
						break;
					}
					syntaxKind = SyntaxKind.WithExpression;
				}
				LanguageParser.Precedence precedence2 = LanguageParser.GetPrecedence(syntaxKind);
				bool flag2 = false;
				if (contextualKind == SyntaxKind.GreaterThanToken && (base.PeekToken(1).Kind == SyntaxKind.GreaterThanToken || base.PeekToken(1).Kind == SyntaxKind.GreaterThanEqualsToken) && base.CurrentToken.GetTrailingTriviaWidth() == 0 && base.PeekToken(1).GetLeadingTriviaWidth() == 0)
				{
					if (base.PeekToken(1).Kind == SyntaxKind.GreaterThanToken)
					{
						syntaxKind = SyntaxFacts.GetBinaryExpression(SyntaxKind.GreaterThanGreaterThanToken);
					}
					else
					{
						syntaxKind = SyntaxFacts.GetAssignmentExpression(SyntaxKind.GreaterThanGreaterThanEqualsToken);
						flag = true;
					}
					precedence2 = LanguageParser.GetPrecedence(syntaxKind);
					flag2 = true;
				}
				if (precedence2 < precedence || (precedence2 == precedence && !LanguageParser.IsRightAssociative(syntaxKind)))
				{
					break;
				}
				SyntaxToken syntaxToken = base.EatContextualToken(contextualKind, true);
				LanguageParser.Precedence precedence3 = LanguageParser.GetPrecedence(leftOperand.Kind);
				if (precedence2 > precedence3)
				{
					ErrorCode errorCode = ((leftOperand.Kind == SyntaxKind.IsPatternExpression) ? ErrorCode.ERR_UnexpectedToken : ErrorCode.WRN_PrecedenceInversion);
					syntaxToken = base.AddError<SyntaxToken>(syntaxToken, errorCode, new object[] { syntaxToken.Text });
				}
				if (flag2)
				{
					SyntaxToken syntaxToken2 = base.EatToken();
					SyntaxKind syntaxKind2 = ((syntaxToken2.Kind == SyntaxKind.GreaterThanToken) ? SyntaxKind.GreaterThanGreaterThanToken : SyntaxKind.GreaterThanGreaterThanEqualsToken);
					syntaxToken = SyntaxFactory.Token(syntaxToken.GetLeadingTrivia(), syntaxKind2, syntaxToken2.GetTrailingTrivia());
				}
				if (syntaxKind == SyntaxKind.AsExpression)
				{
					TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.AsExpression);
					leftOperand = this._syntaxFactory.BinaryExpression(syntaxKind, leftOperand, syntaxToken, typeSyntax);
				}
				else if (syntaxKind == SyntaxKind.IsExpression)
				{
					leftOperand = this.ParseIsExpression(leftOperand, syntaxToken);
				}
				else if (flag)
				{
					ExpressionSyntax expressionSyntax = ((syntaxKind == SyntaxKind.SimpleAssignmentExpression && base.CurrentToken.Kind == SyntaxKind.RefKeyword) ? base.CheckFeatureAvailability<ExpressionSyntax>(this.ParsePossibleRefExpression(), MessageID.IDS_FeatureRefReassignment, false) : this.ParseSubExpression(precedence2));
					if (syntaxKind == SyntaxKind.CoalesceAssignmentExpression)
					{
						syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureCoalesceAssignmentExpression, false);
					}
					leftOperand = this._syntaxFactory.AssignmentExpression(syntaxKind, leftOperand, syntaxToken, expressionSyntax);
				}
				else if (syntaxKind == SyntaxKind.SwitchExpression)
				{
					leftOperand = this.ParseSwitchExpression(leftOperand, syntaxToken);
				}
				else if (syntaxKind == SyntaxKind.WithExpression)
				{
					leftOperand = this.ParseWithExpression(leftOperand, syntaxToken);
				}
				else if (contextualKind == SyntaxKind.DotDotToken)
				{
					ExpressionSyntax expressionSyntax2;
					if (this.CanStartExpression())
					{
						precedence2 = LanguageParser.GetPrecedence(syntaxKind);
						expressionSyntax2 = this.ParseSubExpression(precedence2);
					}
					else
					{
						expressionSyntax2 = null;
					}
					leftOperand = this._syntaxFactory.RangeExpression(leftOperand, syntaxToken, expressionSyntax2);
				}
				else
				{
					leftOperand = this._syntaxFactory.BinaryExpression(syntaxKind, leftOperand, syntaxToken, this.ParseSubExpression(precedence2));
				}
			}
			if (base.CurrentToken.Kind == SyntaxKind.QuestionToken && precedence <= LanguageParser.Precedence.Conditional)
			{
				SyntaxToken syntaxToken3 = base.EatToken();
				ExpressionSyntax expressionSyntax3 = this.ParsePossibleRefExpression();
				if (base.CurrentToken.Kind == SyntaxKind.EndOfFileToken && this.lexer.InterpolationFollowedByColon)
				{
					SyntaxToken syntaxToken4 = SyntaxFactory.MissingToken(SyntaxKind.ColonToken);
					IdentifierNameSyntax identifierNameSyntax = this._syntaxFactory.IdentifierName(SyntaxFactory.MissingToken(SyntaxKind.IdentifierToken));
					leftOperand = this._syntaxFactory.ConditionalExpression(leftOperand, syntaxToken3, expressionSyntax3, syntaxToken4, identifierNameSyntax);
					leftOperand = base.AddError<ExpressionSyntax>(leftOperand, ErrorCode.ERR_ConditionalInInterpolation);
				}
				else
				{
					SyntaxToken syntaxToken5 = base.EatToken(SyntaxKind.ColonToken);
					ExpressionSyntax expressionSyntax4 = this.ParsePossibleRefExpression();
					leftOperand = this._syntaxFactory.ConditionalExpression(leftOperand, syntaxToken3, expressionSyntax3, syntaxToken5, expressionSyntax4);
				}
			}
			return leftOperand;
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x0012B008 File Offset: 0x00129208
		private ExpressionSyntax ParseDeclarationExpression(LanguageParser.ParseTypeMode mode, MessageID feature)
		{
			TypeSyntax typeSyntax = this.ParseType(mode);
			VariableDesignationSyntax variableDesignationSyntax = this.ParseDesignation(false);
			if (feature != MessageID.None)
			{
				variableDesignationSyntax = base.CheckFeatureAvailability<VariableDesignationSyntax>(variableDesignationSyntax, feature, false);
			}
			return this._syntaxFactory.DeclarationExpression(typeSyntax, variableDesignationSyntax);
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x0012B040 File Offset: 0x00129240
		private ExpressionSyntax ParseThrowExpression()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ThrowKeyword);
			ExpressionSyntax expressionSyntax = this.ParseSubExpression(LanguageParser.Precedence.Coalescing);
			ThrowExpressionSyntax throwExpressionSyntax = this._syntaxFactory.ThrowExpression(syntaxToken, expressionSyntax);
			return base.CheckFeatureAvailability<ThrowExpressionSyntax>(throwExpressionSyntax, MessageID.IDS_FeatureThrowExpression, false);
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x0012B07C File Offset: 0x0012927C
		private ExpressionSyntax ParseIsExpression(ExpressionSyntax leftOperand, SyntaxToken opToken)
		{
			CSharpSyntaxNode csharpSyntaxNode = this.ParseTypeOrPatternForIsOperator();
			PatternSyntax patternSyntax = csharpSyntaxNode as PatternSyntax;
			if (patternSyntax != null)
			{
				IsPatternExpressionSyntax isPatternExpressionSyntax = this._syntaxFactory.IsPatternExpression(leftOperand, opToken, patternSyntax);
				return base.CheckFeatureAvailability<IsPatternExpressionSyntax>(isPatternExpressionSyntax, MessageID.IDS_FeaturePatternMatching, false);
			}
			TypeSyntax typeSyntax = csharpSyntaxNode as TypeSyntax;
			if (typeSyntax == null)
			{
				throw ExceptionUtilities.UnexpectedValue(csharpSyntaxNode);
			}
			return this._syntaxFactory.BinaryExpression(SyntaxKind.IsExpression, leftOperand, opToken, typeSyntax);
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x0012B0DD File Offset: 0x001292DD
		private ExpressionSyntax ParseTerm(LanguageParser.Precedence precedence)
		{
			return this.ParsePostFixExpression(this.ParseTermWithoutPostfix(precedence));
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x0012B0EC File Offset: 0x001292EC
		private ExpressionSyntax ParseTermWithoutPostfix(LanguageParser.Precedence precedence)
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.StaticKeyword)
			{
				if (kind <= SyntaxKind.ColonColonToken)
				{
					if (kind == SyntaxKind.OpenParenToken)
					{
						return this.ParseCastOrParenExpressionOrLambdaOrTuple(precedence);
					}
					if (kind != SyntaxKind.OpenBracketToken)
					{
						if (kind != SyntaxKind.ColonColonToken)
						{
							goto IL_02D4;
						}
						return this.ParseAliasQualifiedName(LanguageParser.NameOptions.InExpression);
					}
					else
					{
						if (this.IsPossibleLambdaExpression(precedence))
						{
							return this.ParseLambdaExpression();
						}
						goto IL_02D4;
					}
				}
				else if (kind <= SyntaxKind.FalseKeyword)
				{
					if (kind == SyntaxKind.EqualsGreaterThanToken)
					{
						return this.ParseLambdaExpression();
					}
					switch (kind)
					{
					case SyntaxKind.TypeOfKeyword:
						return this.ParseTypeOfExpression();
					case SyntaxKind.SizeOfKeyword:
						return this.ParseSizeOfExpression();
					case SyntaxKind.NullKeyword:
					case SyntaxKind.TrueKeyword:
					case SyntaxKind.FalseKeyword:
						break;
					default:
						goto IL_02D4;
					}
				}
				else
				{
					if (kind == SyntaxKind.DefaultKeyword)
					{
						return this.ParseDefaultExpression();
					}
					if (kind != SyntaxKind.StaticKeyword)
					{
						goto IL_02D4;
					}
					if (this.IsPossibleAnonymousMethodExpression())
					{
						return this.ParseAnonymousMethodExpression();
					}
					if (this.IsPossibleLambdaExpression(precedence))
					{
						return this.ParseLambdaExpression();
					}
					return base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_InvalidExprTerm, new object[] { base.CurrentToken.Text });
				}
			}
			else if (kind <= SyntaxKind.BaseKeyword)
			{
				if (kind == SyntaxKind.StackAllocKeyword)
				{
					return this.ParseStackAllocExpression();
				}
				if (kind == SyntaxKind.NewKeyword)
				{
					return this.ParseNewExpression();
				}
				switch (kind)
				{
				case SyntaxKind.RefKeyword:
					return base.AddError<ExpressionSyntax>(this.ParsePossibleRefExpression(), ErrorCode.ERR_InvalidExprTerm, new object[] { SyntaxFacts.GetText(kind) });
				case SyntaxKind.OutKeyword:
				case SyntaxKind.InKeyword:
				case SyntaxKind.IsKeyword:
				case SyntaxKind.AsKeyword:
				case SyntaxKind.ParamsKeyword:
					goto IL_02D4;
				case SyntaxKind.ArgListKeyword:
					break;
				case SyntaxKind.MakeRefKeyword:
					return this.ParseMakeRefExpression();
				case SyntaxKind.RefTypeKeyword:
					return this.ParseRefTypeExpression();
				case SyntaxKind.RefValueKeyword:
					return this.ParseRefValueExpression();
				case SyntaxKind.ThisKeyword:
					return this._syntaxFactory.ThisExpression(base.EatToken());
				case SyntaxKind.BaseKeyword:
					return this.ParseBaseExpression();
				default:
					goto IL_02D4;
				}
			}
			else if (kind <= SyntaxKind.UncheckedKeyword)
			{
				if (kind == SyntaxKind.DelegateKeyword)
				{
					return this.ParseAnonymousMethodExpression();
				}
				if (kind - SyntaxKind.CheckedKeyword > 1)
				{
					goto IL_02D4;
				}
				return this.ParseCheckedOrUncheckedExpression();
			}
			else
			{
				if (kind == SyntaxKind.InterpolatedStringStartToken)
				{
					throw new NotImplementedException();
				}
				switch (kind)
				{
				case SyntaxKind.IdentifierToken:
					if (!this.IsTrueIdentifier())
					{
						return base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_InvalidExprTerm, new object[] { base.CurrentToken.Text });
					}
					if (this.IsPossibleAnonymousMethodExpression())
					{
						return this.ParseAnonymousMethodExpression();
					}
					if (this.IsPossibleLambdaExpression(precedence))
					{
						return this.ParseLambdaExpression();
					}
					if (this.IsPossibleDeconstructionLeft(precedence))
					{
						return this.ParseDeclarationExpression(LanguageParser.ParseTypeMode.Normal, MessageID.IDS_FeatureTuples);
					}
					return this.ParseAliasQualifiedName(LanguageParser.NameOptions.InExpression);
				case SyntaxKind.NumericLiteralToken:
				case SyntaxKind.CharacterLiteralToken:
				case SyntaxKind.StringLiteralToken:
					break;
				case SyntaxKind.XmlEntityLiteralToken:
				case SyntaxKind.XmlTextLiteralToken:
				case SyntaxKind.XmlTextLiteralNewLineToken:
					goto IL_02D4;
				case SyntaxKind.InterpolatedStringToken:
					return this.ParseInterpolatedStringToken();
				default:
					goto IL_02D4;
				}
			}
			return this._syntaxFactory.LiteralExpression(SyntaxFacts.GetLiteralExpression(kind), base.EatToken());
			IL_02D4:
			if (LanguageParser.IsPredefinedType(kind))
			{
				PredefinedTypeSyntax predefinedTypeSyntax = this._syntaxFactory.PredefinedType(base.EatToken());
				if (base.CurrentToken.Kind != SyntaxKind.DotToken || kind == SyntaxKind.VoidKeyword)
				{
					predefinedTypeSyntax = base.AddError<PredefinedTypeSyntax>(predefinedTypeSyntax, ErrorCode.ERR_InvalidExprTerm, new object[] { SyntaxFacts.GetText(kind) });
				}
				return predefinedTypeSyntax;
			}
			IdentifierNameSyntax identifierNameSyntax = this.CreateMissingIdentifierName();
			if (kind == SyntaxKind.EndOfFileToken)
			{
				identifierNameSyntax = base.AddError<IdentifierNameSyntax>(identifierNameSyntax, ErrorCode.ERR_ExpressionExpected);
			}
			else
			{
				identifierNameSyntax = base.AddError<IdentifierNameSyntax>(identifierNameSyntax, ErrorCode.ERR_InvalidExprTerm, new object[] { SyntaxFacts.GetText(kind) });
			}
			return identifierNameSyntax;
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x0012B45A File Offset: 0x0012965A
		private ExpressionSyntax ParseBaseExpression()
		{
			return this._syntaxFactory.BaseExpression(base.EatToken());
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x0012B470 File Offset: 0x00129670
		private bool IsPossibleDeconstructionLeft(LanguageParser.Precedence precedence)
		{
			if (precedence > LanguageParser.Precedence.Expression || (!base.CurrentToken.IsIdentifierVar() && !LanguageParser.IsPredefinedType(base.CurrentToken.Kind)))
			{
				return false;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag;
			try
			{
				base.EatToken();
				flag = base.CurrentToken.Kind == SyntaxKind.OpenParenToken && this.ScanDesignator() && base.CurrentToken.Kind == SyntaxKind.EqualsToken;
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag;
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x0012B504 File Offset: 0x00129704
		private bool ScanDesignator()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind == SyntaxKind.OpenParenToken)
			{
				for (;;)
				{
					base.EatToken();
					if (!this.ScanDesignator())
					{
						break;
					}
					SyntaxKind kind2 = base.CurrentToken.Kind;
					if (kind2 == SyntaxKind.CloseParenToken)
					{
						goto IL_005C;
					}
					if (kind2 != SyntaxKind.CommaToken)
					{
						return false;
					}
				}
				return false;
				IL_005C:
				base.EatToken();
				return true;
			}
			if (kind == SyntaxKind.IdentifierToken && this.IsTrueIdentifier())
			{
				base.EatToken();
				return true;
			}
			return false;
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x0012B57C File Offset: 0x0012977C
		private bool IsPossibleAnonymousMethodExpression()
		{
			int num = 0;
			while (base.PeekToken(num).Kind == SyntaxKind.StaticKeyword || base.PeekToken(num).ContextualKind == SyntaxKind.AsyncKeyword)
			{
				num++;
			}
			return base.PeekToken(num).Kind == SyntaxKind.DelegateKeyword;
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x0012B5CC File Offset: 0x001297CC
		private ExpressionSyntax ParsePostFixExpression(ExpressionSyntax expr)
		{
			for (;;)
			{
				SyntaxKind kind = base.CurrentToken.Kind;
				if (kind <= SyntaxKind.OpenBracketToken)
				{
					if (kind != SyntaxKind.ExclamationToken)
					{
						if (kind != SyntaxKind.OpenParenToken)
						{
							if (kind != SyntaxKind.OpenBracketToken)
							{
								break;
							}
							expr = this._syntaxFactory.ElementAccessExpression(expr, this.ParseBracketedArgumentList());
						}
						else
						{
							expr = this._syntaxFactory.InvocationExpression(expr, this.ParseParenthesizedArgumentList());
						}
					}
					else
					{
						expr = this._syntaxFactory.PostfixUnaryExpression(SyntaxFacts.GetPostfixUnaryExpression(kind), expr, base.EatToken());
						expr = base.CheckFeatureAvailability<ExpressionSyntax>(expr, MessageID.IDS_FeatureNullableReferenceTypes, false);
					}
				}
				else if (kind != SyntaxKind.DotToken)
				{
					if (kind != SyntaxKind.QuestionToken)
					{
						switch (kind)
						{
						case SyntaxKind.MinusMinusToken:
						case SyntaxKind.PlusPlusToken:
							expr = this._syntaxFactory.PostfixUnaryExpression(SyntaxFacts.GetPostfixUnaryExpression(kind), expr, base.EatToken());
							continue;
						case SyntaxKind.ColonColonToken:
							if (base.PeekToken(1).Kind == SyntaxKind.IdentifierToken)
							{
								SyntaxToken syntaxToken = base.EatToken();
								syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_UnexpectedAliasedName);
								SyntaxToken syntaxToken2 = this.ConvertToMissingWithTrailingTrivia(syntaxToken, SyntaxKind.DotToken);
								expr = this._syntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expr, syntaxToken2, this.ParseSimpleName(LanguageParser.NameOptions.InExpression));
								continue;
							}
							expr = base.AddTrailingSkippedSyntax<ExpressionSyntax>(expr, base.EatTokenWithPrejudice(SyntaxKind.DotToken));
							continue;
						case SyntaxKind.MinusGreaterThanToken:
							expr = this._syntaxFactory.MemberAccessExpression(SyntaxKind.PointerMemberAccessExpression, expr, base.EatToken(), this.ParseSimpleName(LanguageParser.NameOptions.InExpression));
							continue;
						}
						break;
					}
					if (!LanguageParser.CanStartConsequenceExpression(base.PeekToken(1).Kind))
					{
						break;
					}
					SyntaxToken syntaxToken3 = base.EatToken();
					ExpressionSyntax expressionSyntax = this.ParseConsequenceSyntax();
					expr = this._syntaxFactory.ConditionalAccessExpression(expr, syntaxToken3, expressionSyntax);
					expr = base.CheckFeatureAvailability<ExpressionSyntax>(expr, MessageID.IDS_FeatureNullPropagatingOperator, false);
				}
				else
				{
					if (base.CurrentToken.TrailingTrivia.Any(8539) && base.PeekToken(1).Kind == SyntaxKind.IdentifierToken && base.PeekToken(2).ContextualKind == SyntaxKind.IdentifierToken)
					{
						goto Block_10;
					}
					expr = this._syntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expr, base.EatToken(), this.ParseSimpleName(LanguageParser.NameOptions.InExpression));
				}
			}
			return expr;
			Block_10:
			expr = this._syntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expr, base.EatToken(), base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_IdentifierExpected));
			return expr;
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x0012B834 File Offset: 0x00129A34
		private static bool CanStartConsequenceExpression(SyntaxKind kind)
		{
			return kind == SyntaxKind.DotToken || kind == SyntaxKind.OpenBracketToken;
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x0012B848 File Offset: 0x00129A48
		internal ExpressionSyntax ParseConsequenceSyntax()
		{
			SyntaxKind syntaxKind = base.CurrentToken.Kind;
			ExpressionSyntax expressionSyntax = null;
			if (syntaxKind != SyntaxKind.OpenBracketToken)
			{
				if (syntaxKind == SyntaxKind.DotToken)
				{
					expressionSyntax = this._syntaxFactory.MemberBindingExpression(base.EatToken(), this.ParseSimpleName(LanguageParser.NameOptions.InExpression));
				}
			}
			else
			{
				expressionSyntax = this._syntaxFactory.ElementBindingExpression(this.ParseBracketedArgumentList());
			}
			for (;;)
			{
				syntaxKind = base.CurrentToken.Kind;
				if (this.<ParseConsequenceSyntax>g__isOptionalExclamationsFollowedByConditionalOperation|328_0())
				{
					while (syntaxKind == SyntaxKind.ExclamationToken)
					{
						expressionSyntax = this._syntaxFactory.PostfixUnaryExpression(SyntaxKind.SuppressNullableWarningExpression, expressionSyntax, base.EatToken());
						expressionSyntax = base.CheckFeatureAvailability<ExpressionSyntax>(expressionSyntax, MessageID.IDS_FeatureNullableReferenceTypes, false);
						syntaxKind = base.CurrentToken.Kind;
					}
				}
				if (syntaxKind <= SyntaxKind.OpenBracketToken)
				{
					if (syntaxKind != SyntaxKind.OpenParenToken)
					{
						if (syntaxKind != SyntaxKind.OpenBracketToken)
						{
							break;
						}
						expressionSyntax = this._syntaxFactory.ElementAccessExpression(expressionSyntax, this.ParseBracketedArgumentList());
					}
					else
					{
						expressionSyntax = this._syntaxFactory.InvocationExpression(expressionSyntax, this.ParseParenthesizedArgumentList());
					}
				}
				else
				{
					if (syntaxKind != SyntaxKind.DotToken)
					{
						goto Block_7;
					}
					expressionSyntax = this._syntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, base.EatToken(), this.ParseSimpleName(LanguageParser.NameOptions.InExpression));
				}
			}
			return expressionSyntax;
			Block_7:
			if (syntaxKind == SyntaxKind.QuestionToken)
			{
				if (LanguageParser.CanStartConsequenceExpression(base.PeekToken(1).Kind))
				{
					SyntaxToken syntaxToken = base.EatToken();
					ExpressionSyntax expressionSyntax2 = this.ParseConsequenceSyntax();
					expressionSyntax = this._syntaxFactory.ConditionalAccessExpression(expressionSyntax, syntaxToken, expressionSyntax2);
				}
				return expressionSyntax;
			}
			return expressionSyntax;
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x0012B9AC File Offset: 0x00129BAC
		internal ArgumentListSyntax ParseParenthesizedArgumentList()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.ArgumentList)
			{
				return (ArgumentListSyntax)base.EatNode();
			}
			SyntaxToken syntaxToken;
			SeparatedSyntaxList<ArgumentSyntax> separatedSyntaxList;
			SyntaxToken syntaxToken2;
			this.ParseArgumentList(out syntaxToken, out separatedSyntaxList, out syntaxToken2, SyntaxKind.OpenParenToken, SyntaxKind.CloseParenToken);
			return this._syntaxFactory.ArgumentList(syntaxToken, separatedSyntaxList, syntaxToken2);
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x0012BA00 File Offset: 0x00129C00
		internal BracketedArgumentListSyntax ParseBracketedArgumentList()
		{
			if (this.IsIncrementalAndFactoryContextMatches && base.CurrentNodeKind == SyntaxKind.BracketedArgumentList)
			{
				return (BracketedArgumentListSyntax)base.EatNode();
			}
			SyntaxToken syntaxToken;
			SeparatedSyntaxList<ArgumentSyntax> separatedSyntaxList;
			SyntaxToken syntaxToken2;
			this.ParseArgumentList(out syntaxToken, out separatedSyntaxList, out syntaxToken2, SyntaxKind.OpenBracketToken, SyntaxKind.CloseBracketToken);
			return this._syntaxFactory.BracketedArgumentList(syntaxToken, separatedSyntaxList, syntaxToken2);
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x0012BA54 File Offset: 0x00129C54
		private void ParseArgumentList(out SyntaxToken openToken, out SeparatedSyntaxList<ArgumentSyntax> arguments, out SyntaxToken closeToken, SyntaxKind openKind, SyntaxKind closeKind)
		{
			bool flag = openKind == SyntaxKind.OpenBracketToken;
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken || base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
			{
				openToken = base.EatTokenAsKind(openKind);
			}
			else
			{
				openToken = base.EatToken(openKind);
			}
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfArgumentList;
			SeparatedSyntaxListBuilder<ArgumentSyntax> separatedSyntaxListBuilder = default(SeparatedSyntaxListBuilder<ArgumentSyntax>);
			try
			{
				if (base.CurrentToken.Kind != closeKind && base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
				{
					for (;;)
					{
						if (separatedSyntaxListBuilder.IsNull)
						{
							separatedSyntaxListBuilder = this._pool.AllocateSeparated<ArgumentSyntax>();
						}
						if (this.IsPossibleArgumentExpression() || base.CurrentToken.Kind == SyntaxKind.CommaToken)
						{
							break;
						}
						if (this.SkipBadArgumentListTokens(ref openToken, separatedSyntaxListBuilder, SyntaxKind.IdentifierToken, closeKind) != LanguageParser.PostSkipAction.Continue)
						{
							goto IL_01BE;
						}
					}
					separatedSyntaxListBuilder.Add(this.ParseArgumentExpression(flag));
					int num = -1;
					while (base.IsMakingProgress(ref num, true))
					{
						if (base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.CloseBracketToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
						{
							break;
						}
						if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleArgumentExpression())
						{
							separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
							separatedSyntaxListBuilder.Add(this.ParseArgumentExpression(flag));
						}
						else if (this.SkipBadArgumentListTokens(ref openToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken, closeKind) == LanguageParser.PostSkipAction.Abort)
						{
							break;
						}
					}
				}
				else if (flag && base.CurrentToken.Kind == closeKind)
				{
					if (separatedSyntaxListBuilder.IsNull)
					{
						separatedSyntaxListBuilder = this._pool.AllocateSeparated<ArgumentSyntax>();
					}
					separatedSyntaxListBuilder.Add(this.ParseArgumentExpression(flag));
				}
				IL_01BE:
				this._termState = termState;
				if (base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.CloseBracketToken)
				{
					closeToken = base.EatTokenAsKind(closeKind);
				}
				else
				{
					closeToken = base.EatToken(closeKind);
				}
				arguments = separatedSyntaxListBuilder.ToList();
			}
			finally
			{
				if (!separatedSyntaxListBuilder.IsNull)
				{
					this._pool.Free<ArgumentSyntax>(in separatedSyntaxListBuilder);
				}
			}
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x0012BCA4 File Offset: 0x00129EA4
		private LanguageParser.PostSkipAction SkipBadArgumentListTokens(ref SyntaxToken open, SeparatedSyntaxListBuilder<ArgumentSyntax> list, SyntaxKind expected, SyntaxKind closeKind)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, ArgumentSyntax>(ref open, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleArgumentExpression(), (LanguageParser p) => p.CurrentToken.Kind == closeKind || p.CurrentToken.Kind == SyntaxKind.SemicolonToken || p.IsTerminator(), expected);
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x0012BCF3 File Offset: 0x00129EF3
		private bool IsEndOfArgumentList()
		{
			return base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.CloseBracketToken;
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x0012BD1B File Offset: 0x00129F1B
		private bool IsPossibleArgumentExpression()
		{
			return LanguageParser.IsValidArgumentRefKindKeyword(base.CurrentToken.Kind) || this.IsPossibleExpression();
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x0012BD37 File Offset: 0x00129F37
		private static bool IsValidArgumentRefKindKeyword(SyntaxKind kind)
		{
			return kind - SyntaxKind.RefKeyword <= 2;
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x0012BD48 File Offset: 0x00129F48
		private ArgumentSyntax ParseArgumentExpression(bool isIndexer)
		{
			NameColonSyntax nameColonSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken && base.PeekToken(1).Kind == SyntaxKind.ColonToken)
			{
				IdentifierNameSyntax identifierNameSyntax = this.ParseIdentifierName(ErrorCode.ERR_IdentifierExpected);
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ColonToken);
				nameColonSyntax = this._syntaxFactory.NameColon(identifierNameSyntax, syntaxToken);
				nameColonSyntax = base.CheckFeatureAvailability<NameColonSyntax>(nameColonSyntax, MessageID.IDS_FeatureNamedArgument, false);
			}
			SyntaxToken syntaxToken2 = null;
			if (LanguageParser.IsValidArgumentRefKindKeyword(base.CurrentToken.Kind))
			{
				syntaxToken2 = base.EatToken();
			}
			ExpressionSyntax expressionSyntax;
			if (isIndexer && (base.CurrentToken.Kind == SyntaxKind.CommaToken || base.CurrentToken.Kind == SyntaxKind.CloseBracketToken))
			{
				expressionSyntax = this.ParseIdentifierName(ErrorCode.ERR_ValueExpected);
			}
			else if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
			{
				expressionSyntax = this.ParseIdentifierName(ErrorCode.ERR_MissingArgument);
			}
			else
			{
				if (syntaxToken2 != null && syntaxToken2.Kind == SyntaxKind.InKeyword)
				{
					syntaxToken2 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken2, MessageID.IDS_FeatureReadOnlyReferences, false);
				}
				expressionSyntax = ((syntaxToken2 != null && syntaxToken2.Kind == SyntaxKind.OutKeyword) ? this.ParseExpressionOrDeclaration(LanguageParser.ParseTypeMode.Normal, MessageID.IDS_FeatureOutVar, false) : this.ParseSubExpression(LanguageParser.Precedence.Expression));
			}
			return this._syntaxFactory.Argument(nameColonSyntax, syntaxToken2, expressionSyntax);
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x0012BE78 File Offset: 0x0012A078
		private TypeOfExpressionSyntax ParseTypeOfExpression()
		{
			SyntaxToken syntaxToken = base.EatToken();
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			TypeSyntax typeSyntax = this.ParseTypeOrVoid();
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			return this._syntaxFactory.TypeOfExpression(syntaxToken, syntaxToken2, typeSyntax, syntaxToken3);
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x0012BEBC File Offset: 0x0012A0BC
		private ExpressionSyntax ParseDefaultExpression()
		{
			SyntaxToken syntaxToken = base.EatToken();
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
				TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureDefault, false);
				return this._syntaxFactory.DefaultExpression(syntaxToken, syntaxToken2, typeSyntax, syntaxToken3);
			}
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureDefaultLiteral, false);
			return this._syntaxFactory.LiteralExpression(SyntaxKind.DefaultLiteralExpression, syntaxToken);
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x0012BF40 File Offset: 0x0012A140
		private SizeOfExpressionSyntax ParseSizeOfExpression()
		{
			SyntaxToken syntaxToken = base.EatToken();
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			return this._syntaxFactory.SizeOfExpression(syntaxToken, syntaxToken2, typeSyntax, syntaxToken3);
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x0012BF84 File Offset: 0x0012A184
		private MakeRefExpressionSyntax ParseMakeRefExpression()
		{
			SyntaxToken syntaxToken = base.EatToken();
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			ExpressionSyntax expressionSyntax = this.ParseSubExpression(LanguageParser.Precedence.Expression);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			return this._syntaxFactory.MakeRefExpression(syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3);
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x0012BFC8 File Offset: 0x0012A1C8
		private RefTypeExpressionSyntax ParseRefTypeExpression()
		{
			SyntaxToken syntaxToken = base.EatToken();
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			ExpressionSyntax expressionSyntax = this.ParseSubExpression(LanguageParser.Precedence.Expression);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			return this._syntaxFactory.RefTypeExpression(syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3);
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x0012C00C File Offset: 0x0012A20C
		private CheckedExpressionSyntax ParseCheckedOrUncheckedExpression()
		{
			SyntaxToken syntaxToken = base.EatToken();
			SyntaxKind syntaxKind = ((syntaxToken.Kind == SyntaxKind.CheckedKeyword) ? SyntaxKind.CheckedExpression : SyntaxKind.UncheckedExpression);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			ExpressionSyntax expressionSyntax = this.ParseSubExpression(LanguageParser.Precedence.Expression);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseParenToken);
			return this._syntaxFactory.CheckedExpression(syntaxKind, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3);
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0012C06C File Offset: 0x0012A26C
		private RefValueExpressionSyntax ParseRefValueExpression()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.RefValueKeyword);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenParenToken);
			ExpressionSyntax expressionSyntax = this.ParseSubExpression(LanguageParser.Precedence.Expression);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CommaToken);
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseParenToken);
			return this._syntaxFactory.RefValueExpression(syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, typeSyntax, syntaxToken4);
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0012C0CE File Offset: 0x0012A2CE
		private bool ScanParenthesizedLambda(LanguageParser.Precedence precedence)
		{
			return this.ScanParenthesizedImplicitlyTypedLambda(precedence) || this.ScanExplicitlyTypedLambda(precedence);
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x0012C0E4 File Offset: 0x0012A2E4
		private bool ScanParenthesizedImplicitlyTypedLambda(LanguageParser.Precedence precedence)
		{
			if (precedence > LanguageParser.Precedence.Expression)
			{
				return false;
			}
			if (base.PeekToken(1).Kind == SyntaxKind.IdentifierToken && (!this.IsInQuery || !LanguageParser.IsTokenQueryContextualKeyword(base.PeekToken(1))) && base.PeekToken(2).Kind == SyntaxKind.CommaToken)
			{
				int num = 3;
				SyntaxToken syntaxToken;
				do
				{
					syntaxToken = base.PeekToken(num++);
				}
				while (syntaxToken.Kind == SyntaxKind.IdentifierToken || SyntaxFacts.IsPredefinedType(syntaxToken.Kind) || syntaxToken.Kind == SyntaxKind.CommaToken || (!this.IsInQuery && LanguageParser.IsTokenQueryContextualKeyword(syntaxToken)));
				return base.PeekToken(num - 1).Kind == SyntaxKind.CloseParenToken && base.PeekToken(num).Kind == SyntaxKind.EqualsGreaterThanToken;
			}
			return (this.IsTrueIdentifier(base.PeekToken(1)) && base.PeekToken(2).Kind == SyntaxKind.CloseParenToken && base.PeekToken(3).Kind == SyntaxKind.EqualsGreaterThanToken) || (base.PeekToken(1).Kind == SyntaxKind.CloseParenToken && base.PeekToken(2).Kind == SyntaxKind.EqualsGreaterThanToken) || base.PeekToken(1).Kind == SyntaxKind.ParamsKeyword;
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x0012C220 File Offset: 0x0012A420
		private bool ScanExplicitlyTypedLambda(LanguageParser.Precedence precedence)
		{
			if (precedence > LanguageParser.Precedence.Expression)
			{
				return false;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag2;
			try
			{
				bool flag = false;
				SyntaxKind kind;
				for (;;)
				{
					base.EatToken();
					this.ParseAttributeDeclarations();
					switch (base.CurrentToken.Kind)
					{
					case SyntaxKind.RefKeyword:
						base.EatToken();
						flag = true;
						if (base.CurrentToken.Kind == SyntaxKind.ReadOnlyKeyword)
						{
							base.EatToken();
						}
						break;
					case SyntaxKind.OutKeyword:
					case SyntaxKind.InKeyword:
					case SyntaxKind.ParamsKeyword:
						base.EatToken();
						flag = true;
						break;
					}
					if (base.CurrentToken.Kind == SyntaxKind.EndOfFileToken)
					{
						break;
					}
					if (this.ScanType(false) == LanguageParser.ScanTypeFlags.NotType)
					{
						goto Block_7;
					}
					if (this.IsTrueIdentifier())
					{
						base.EatToken();
					}
					kind = base.CurrentToken.Kind;
					if (kind == SyntaxKind.CloseParenToken)
					{
						goto IL_00E0;
					}
					if (kind != SyntaxKind.CommaToken)
					{
						goto Block_10;
					}
					if (flag)
					{
						goto Block_12;
					}
				}
				return flag;
				Block_7:
				return false;
				Block_10:
				if (kind == SyntaxKind.EndOfFileToken)
				{
					return flag;
				}
				return false;
				Block_12:
				return true;
				IL_00E0:
				flag2 = base.PeekToken(1).Kind == SyntaxKind.EqualsGreaterThanToken;
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag2;
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x0012C34C File Offset: 0x0012A54C
		private ExpressionSyntax ParseCastOrParenExpressionOrLambdaOrTuple(LanguageParser.Precedence precedence)
		{
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			ExpressionSyntax expressionSyntax;
			try
			{
				if (this.ScanParenthesizedImplicitlyTypedLambda(precedence))
				{
					expressionSyntax = this.ParseLambdaExpression();
				}
				else if (this.ScanCast(false) && !this.IsCurrentTokenQueryKeywordInQuery())
				{
					this.Reset(ref resetPoint);
					SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenParenToken);
					TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
					SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseParenToken);
					ExpressionSyntax expressionSyntax2 = this.ParseSubExpression(LanguageParser.Precedence.Cast);
					expressionSyntax = this._syntaxFactory.CastExpression(syntaxToken, typeSyntax, syntaxToken2, expressionSyntax2);
				}
				else
				{
					this.Reset(ref resetPoint);
					if (this.ScanExplicitlyTypedLambda(precedence))
					{
						expressionSyntax = this.ParseLambdaExpression();
					}
					else
					{
						this.Reset(ref resetPoint);
						SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.OpenParenToken);
						ExpressionSyntax expressionSyntax3 = this.ParseExpressionOrDeclaration(LanguageParser.ParseTypeMode.FirstElementOfPossibleTupleLiteral, MessageID.None, true);
						if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
						{
							ArgumentSyntax argumentSyntax = this._syntaxFactory.Argument(null, null, expressionSyntax3);
							expressionSyntax = this.ParseTupleExpressionTail(syntaxToken3, argumentSyntax);
						}
						else if (expressionSyntax3.Kind == SyntaxKind.IdentifierName && base.CurrentToken.Kind == SyntaxKind.ColonToken)
						{
							NameColonSyntax nameColonSyntax = this._syntaxFactory.NameColon((IdentifierNameSyntax)expressionSyntax3, base.EatToken());
							expressionSyntax3 = this.ParseExpressionOrDeclaration(LanguageParser.ParseTypeMode.FirstElementOfPossibleTupleLiteral, MessageID.None, true);
							ArgumentSyntax argumentSyntax2 = this._syntaxFactory.Argument(nameColonSyntax, null, expressionSyntax3);
							expressionSyntax = this.ParseTupleExpressionTail(syntaxToken3, argumentSyntax2);
						}
						else
						{
							SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseParenToken);
							expressionSyntax = this._syntaxFactory.ParenthesizedExpression(syntaxToken3, expressionSyntax3, syntaxToken4);
						}
					}
				}
			}
			finally
			{
				this.Release(ref resetPoint);
			}
			return expressionSyntax;
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0012C4F0 File Offset: 0x0012A6F0
		private TupleExpressionSyntax ParseTupleExpressionTail(SyntaxToken openParen, ArgumentSyntax firstArg)
		{
			SeparatedSyntaxListBuilder<ArgumentSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ArgumentSyntax>();
			TupleExpressionSyntax tupleExpressionSyntax2;
			try
			{
				separatedSyntaxListBuilder.Add(firstArg);
				while (base.CurrentToken.Kind == SyntaxKind.CommaToken)
				{
					SyntaxToken syntaxToken = base.EatToken(SyntaxKind.CommaToken);
					separatedSyntaxListBuilder.AddSeparator(syntaxToken);
					ExpressionSyntax expressionSyntax = this.ParseExpressionOrDeclaration(LanguageParser.ParseTypeMode.AfterTupleComma, MessageID.None, true);
					ArgumentSyntax argumentSyntax;
					if (expressionSyntax.Kind == SyntaxKind.IdentifierName && base.CurrentToken.Kind == SyntaxKind.ColonToken)
					{
						NameColonSyntax nameColonSyntax = this._syntaxFactory.NameColon((IdentifierNameSyntax)expressionSyntax, base.EatToken());
						expressionSyntax = this.ParseExpressionOrDeclaration(LanguageParser.ParseTypeMode.AfterTupleComma, MessageID.None, true);
						argumentSyntax = this._syntaxFactory.Argument(nameColonSyntax, null, expressionSyntax);
					}
					else
					{
						argumentSyntax = this._syntaxFactory.Argument(null, null, expressionSyntax);
					}
					separatedSyntaxListBuilder.Add(argumentSyntax);
				}
				if (separatedSyntaxListBuilder.Count < 2)
				{
					separatedSyntaxListBuilder.AddSeparator(SyntaxFactory.MissingToken(SyntaxKind.CommaToken));
					IdentifierNameSyntax identifierNameSyntax = base.AddError<IdentifierNameSyntax>(this.CreateMissingIdentifierName(), ErrorCode.ERR_TupleTooFewElements);
					separatedSyntaxListBuilder.Add(this._syntaxFactory.Argument(null, null, identifierNameSyntax));
				}
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseParenToken);
				TupleExpressionSyntax tupleExpressionSyntax = this._syntaxFactory.TupleExpression(openParen, in separatedSyntaxListBuilder, syntaxToken2);
				tupleExpressionSyntax = base.CheckFeatureAvailability<TupleExpressionSyntax>(tupleExpressionSyntax, MessageID.IDS_FeatureTuples, false);
				tupleExpressionSyntax2 = tupleExpressionSyntax;
			}
			finally
			{
				this._pool.Free<ArgumentSyntax>(in separatedSyntaxListBuilder);
			}
			return tupleExpressionSyntax2;
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x0012C664 File Offset: 0x0012A864
		private bool ScanCast(bool forPattern = false)
		{
			if (base.CurrentToken.Kind != SyntaxKind.OpenParenToken)
			{
				return false;
			}
			base.EatToken();
			LanguageParser.ScanTypeFlags scanTypeFlags = this.ScanType(forPattern);
			if (scanTypeFlags == LanguageParser.ScanTypeFlags.NotType)
			{
				return false;
			}
			if (base.CurrentToken.Kind != SyntaxKind.CloseParenToken)
			{
				return false;
			}
			base.EatToken();
			if (forPattern && base.CurrentToken.Kind == SyntaxKind.IdentifierToken)
			{
				return !this.<ScanCast>g__isBinaryPattern|349_0();
			}
			switch (scanTypeFlags)
			{
			case LanguageParser.ScanTypeFlags.MustBeType:
			case LanguageParser.ScanTypeFlags.AliasQualifiedName:
			case LanguageParser.ScanTypeFlags.NullableType:
			case LanguageParser.ScanTypeFlags.PointerOrMultiplication:
			{
				bool flag = !forPattern;
				if (!flag)
				{
					SyntaxKind kind = base.CurrentToken.Kind;
					bool flag2 = kind - SyntaxKind.AmpersandToken <= 1 || kind - SyntaxKind.MinusToken <= 1 || kind == SyntaxKind.DotDotToken || LanguageParser.CanFollowCast(kind);
					flag = flag2;
				}
				return flag;
			}
			case LanguageParser.ScanTypeFlags.GenericTypeOrMethod:
			case LanguageParser.ScanTypeFlags.GenericTypeOrExpression:
			case LanguageParser.ScanTypeFlags.NonGenericTypeOrExpression:
			case LanguageParser.ScanTypeFlags.TupleType:
				return LanguageParser.CanFollowCast(base.CurrentToken.Kind);
			default:
				throw ExceptionUtilities.UnexpectedValue(scanTypeFlags);
			}
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x0012C758 File Offset: 0x0012A958
		private bool IsPossibleLambdaExpression(LanguageParser.Precedence precedence)
		{
			if (precedence > LanguageParser.Precedence.Expression)
			{
				return false;
			}
			if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
			{
				LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
				try
				{
					this.ParseAttributeDeclarations();
					SyntaxKind kind = base.CurrentToken.Kind;
					bool flag;
					if (kind != SyntaxKind.OpenParenToken)
					{
						flag = (kind == SyntaxKind.StaticKeyword || kind == SyntaxKind.IdentifierToken) && this.IsPossibleLambdaExpressionCore(precedence);
					}
					else
					{
						flag = this.ScanParenthesizedLambda(precedence);
					}
					return flag;
				}
				finally
				{
					this.Reset(ref resetPoint);
					this.Release(ref resetPoint);
				}
			}
			return this.IsPossibleLambdaExpressionCore(precedence);
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0012C7F4 File Offset: 0x0012A9F4
		private bool IsPossibleLambdaExpressionCore(LanguageParser.Precedence precedence)
		{
			int num;
			if (base.CurrentToken.Kind == SyntaxKind.StaticKeyword)
			{
				num = 1;
			}
			else if (base.CurrentToken.ContextualKind == SyntaxKind.AsyncKeyword && base.PeekToken(1).Kind == SyntaxKind.StaticKeyword)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
			if (base.PeekToken(num).Kind == SyntaxKind.EqualsGreaterThanToken)
			{
				return true;
			}
			if (base.PeekToken(num).Kind == SyntaxKind.IdentifierToken && base.PeekToken(num + 1).Kind == SyntaxKind.EqualsGreaterThanToken)
			{
				return true;
			}
			if (base.PeekToken(num).Kind == SyntaxKind.OpenParenToken)
			{
				return true;
			}
			if (base.PeekToken(num).ContextualKind != SyntaxKind.AsyncKeyword)
			{
				return false;
			}
			num++;
			if (base.PeekToken(num).Kind == SyntaxKind.IdentifierToken && base.PeekToken(num + 1).Kind == SyntaxKind.EqualsGreaterThanToken)
			{
				return true;
			}
			if (base.PeekToken(num).Kind != SyntaxKind.OpenParenToken)
			{
				return false;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			for (int i = 0; i < num; i++)
			{
				base.EatToken();
			}
			bool flag = this.ScanParenthesizedLambda(precedence);
			this.Reset(ref resetPoint);
			this.Release(ref resetPoint);
			return flag;
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x0012C920 File Offset: 0x0012AB20
		private static bool CanFollowCast(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.SwitchKeyword)
			{
				switch (kind)
				{
				case SyntaxKind.PercentToken:
				case SyntaxKind.CaretToken:
				case SyntaxKind.AmpersandToken:
				case SyntaxKind.AsteriskToken:
				case SyntaxKind.CloseParenToken:
				case SyntaxKind.MinusToken:
				case SyntaxKind.PlusToken:
				case SyntaxKind.EqualsToken:
				case SyntaxKind.OpenBraceToken:
				case SyntaxKind.CloseBraceToken:
				case SyntaxKind.OpenBracketToken:
				case SyntaxKind.CloseBracketToken:
				case SyntaxKind.BarToken:
				case SyntaxKind.ColonToken:
				case SyntaxKind.SemicolonToken:
				case SyntaxKind.LessThanToken:
				case SyntaxKind.CommaToken:
				case SyntaxKind.GreaterThanToken:
				case SyntaxKind.DotToken:
				case SyntaxKind.QuestionToken:
				case SyntaxKind.SlashToken:
				case SyntaxKind.DotDotToken:
				case SyntaxKind.BarBarToken:
				case SyntaxKind.AmpersandAmpersandToken:
				case SyntaxKind.MinusMinusToken:
				case SyntaxKind.PlusPlusToken:
				case SyntaxKind.QuestionQuestionToken:
				case SyntaxKind.MinusGreaterThanToken:
				case SyntaxKind.ExclamationEqualsToken:
				case SyntaxKind.EqualsEqualsToken:
				case SyntaxKind.EqualsGreaterThanToken:
				case SyntaxKind.LessThanEqualsToken:
				case SyntaxKind.LessThanLessThanToken:
				case SyntaxKind.LessThanLessThanEqualsToken:
				case SyntaxKind.GreaterThanEqualsToken:
				case SyntaxKind.GreaterThanGreaterThanToken:
				case SyntaxKind.GreaterThanGreaterThanEqualsToken:
				case SyntaxKind.SlashEqualsToken:
				case SyntaxKind.AsteriskEqualsToken:
				case SyntaxKind.BarEqualsToken:
				case SyntaxKind.AmpersandEqualsToken:
				case SyntaxKind.PlusEqualsToken:
				case SyntaxKind.MinusEqualsToken:
				case SyntaxKind.CaretEqualsToken:
				case SyntaxKind.PercentEqualsToken:
				case SyntaxKind.QuestionQuestionEqualsToken:
					break;
				case SyntaxKind.OpenParenToken:
				case SyntaxKind.BackslashToken:
				case SyntaxKind.DoubleQuoteToken:
				case SyntaxKind.SingleQuoteToken:
				case SyntaxKind.HashToken:
				case (SyntaxKind)8223:
				case (SyntaxKind)8224:
				case (SyntaxKind)8225:
				case (SyntaxKind)8226:
				case (SyntaxKind)8227:
				case (SyntaxKind)8228:
				case (SyntaxKind)8229:
				case (SyntaxKind)8230:
				case (SyntaxKind)8231:
				case SyntaxKind.SlashGreaterThanToken:
				case SyntaxKind.LessThanSlashToken:
				case SyntaxKind.XmlCommentStartToken:
				case SyntaxKind.XmlCommentEndToken:
				case SyntaxKind.XmlCDataStartToken:
				case SyntaxKind.XmlCDataEndToken:
				case SyntaxKind.XmlProcessingInstructionStartToken:
				case SyntaxKind.XmlProcessingInstructionEndToken:
				case (SyntaxKind)8240:
				case (SyntaxKind)8241:
				case (SyntaxKind)8242:
				case (SyntaxKind)8243:
				case (SyntaxKind)8244:
				case (SyntaxKind)8245:
				case (SyntaxKind)8246:
				case (SyntaxKind)8247:
				case (SyntaxKind)8248:
				case (SyntaxKind)8249:
				case (SyntaxKind)8250:
				case (SyntaxKind)8251:
				case (SyntaxKind)8252:
				case (SyntaxKind)8253:
				case (SyntaxKind)8254:
				case (SyntaxKind)8255:
				case (SyntaxKind)8256:
				case (SyntaxKind)8257:
				case (SyntaxKind)8258:
				case (SyntaxKind)8259:
				case SyntaxKind.ColonColonToken:
					return true;
				default:
					if (kind != SyntaxKind.SwitchKeyword)
					{
						return true;
					}
					break;
				}
			}
			else if (kind - SyntaxKind.IsKeyword > 1 && kind != SyntaxKind.EndOfFileToken)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0012CAC7 File Offset: 0x0012ACC7
		private ExpressionSyntax ParseNewExpression()
		{
			if (this.IsAnonymousType())
			{
				return this.ParseAnonymousTypeExpression();
			}
			if (this.IsImplicitlyTypedArray())
			{
				return this.ParseImplicitlyTypedArrayCreation();
			}
			return this.ParseArrayOrObjectCreationExpression();
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x0012CAED File Offset: 0x0012ACED
		private bool IsAnonymousType()
		{
			return base.CurrentToken.Kind == SyntaxKind.NewKeyword && base.PeekToken(1).Kind == SyntaxKind.OpenBraceToken;
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x0012CB18 File Offset: 0x0012AD18
		private AnonymousObjectCreationExpressionSyntax ParseAnonymousTypeExpression()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.NewKeyword);
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureAnonymousTypes, false);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenBraceToken);
			SeparatedSyntaxListBuilder<AnonymousObjectMemberDeclaratorSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<AnonymousObjectMemberDeclaratorSyntax>();
			this.ParseAnonymousTypeMemberInitializers(ref syntaxToken2, ref separatedSyntaxListBuilder);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseBraceToken);
			AnonymousObjectCreationExpressionSyntax anonymousObjectCreationExpressionSyntax = this._syntaxFactory.AnonymousObjectCreationExpression(syntaxToken, syntaxToken2, in separatedSyntaxListBuilder, syntaxToken3);
			this._pool.Free<AnonymousObjectMemberDeclaratorSyntax>(in separatedSyntaxListBuilder);
			return anonymousObjectCreationExpressionSyntax;
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x0012CB90 File Offset: 0x0012AD90
		private void ParseAnonymousTypeMemberInitializers(ref SyntaxToken openBrace, ref SeparatedSyntaxListBuilder<AnonymousObjectMemberDeclaratorSyntax> list)
		{
			if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
			{
				for (;;)
				{
					IL_0015:
					if (this.IsPossibleExpression() || base.CurrentToken.Kind == SyntaxKind.CommaToken)
					{
						list.Add(this.ParseAnonymousTypeMemberInitializer());
						int num = -1;
						while (base.IsMakingProgress(ref num, true))
						{
							if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
							{
								return;
							}
							if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleExpression())
							{
								list.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
								if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
								{
									return;
								}
								if (!this.IsPossibleExpression())
								{
									goto IL_0015;
								}
								list.Add(this.ParseAnonymousTypeMemberInitializer());
							}
							else if (this.SkipBadInitializerListTokens<AnonymousObjectMemberDeclaratorSyntax>(ref openBrace, list, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
							{
								return;
							}
						}
						break;
					}
					if (this.SkipBadInitializerListTokens<AnonymousObjectMemberDeclaratorSyntax>(ref openBrace, list, SyntaxKind.IdentifierToken) != LanguageParser.PostSkipAction.Continue)
					{
						return;
					}
				}
				return;
			}
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x0012CC8C File Offset: 0x0012AE8C
		private AnonymousObjectMemberDeclaratorSyntax ParseAnonymousTypeMemberInitializer()
		{
			NameEqualsSyntax nameEqualsSyntax = (this.IsNamedAssignment() ? this.ParseNameEquals() : null);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			return this._syntaxFactory.AnonymousObjectMemberDeclarator(nameEqualsSyntax, expressionSyntax);
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x0012CCBF File Offset: 0x0012AEBF
		private bool IsInitializerMember()
		{
			return this.IsComplexElementInitializer() || this.IsNamedAssignment() || this.IsDictionaryInitializer() || this.IsPossibleExpression();
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x0012CCE1 File Offset: 0x0012AEE1
		private bool IsComplexElementInitializer()
		{
			return base.CurrentToken.Kind == SyntaxKind.OpenBraceToken;
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x0012CCF5 File Offset: 0x0012AEF5
		private bool IsNamedAssignment()
		{
			return this.IsTrueIdentifier() && base.PeekToken(1).Kind == SyntaxKind.EqualsToken;
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0012CD14 File Offset: 0x0012AF14
		private bool IsDictionaryInitializer()
		{
			return base.CurrentToken.Kind == SyntaxKind.OpenBracketToken;
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x0012CD28 File Offset: 0x0012AF28
		private ExpressionSyntax ParseArrayOrObjectCreationExpression()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.NewKeyword);
			TypeSyntax typeSyntax = null;
			InitializerExpressionSyntax initializerExpressionSyntax = null;
			if (this.IsImplicitObjectCreation())
			{
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureImplicitObjectCreation, false);
			}
			else
			{
				typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.NewExpression);
				if (typeSyntax.Kind == SyntaxKind.ArrayType)
				{
					if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
					{
						initializerExpressionSyntax = this.ParseArrayInitializer();
					}
					return this._syntaxFactory.ArrayCreationExpression(syntaxToken, (ArrayTypeSyntax)typeSyntax, initializerExpressionSyntax);
				}
			}
			ArgumentListSyntax argumentListSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				argumentListSyntax = this.ParseParenthesizedArgumentList();
			}
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
			{
				initializerExpressionSyntax = this.ParseObjectOrCollectionInitializer();
			}
			if (argumentListSyntax == null && initializerExpressionSyntax == null)
			{
				argumentListSyntax = this._syntaxFactory.ArgumentList(base.EatToken(SyntaxKind.OpenParenToken, ErrorCode.ERR_BadNewExpr, typeSyntax != null && !typeSyntax.ContainsDiagnostics), default(SeparatedSyntaxList<ArgumentSyntax>), SyntaxFactory.MissingToken(SyntaxKind.CloseParenToken));
			}
			if (typeSyntax != null)
			{
				return this._syntaxFactory.ObjectCreationExpression(syntaxToken, typeSyntax, argumentListSyntax, initializerExpressionSyntax);
			}
			return this._syntaxFactory.ImplicitObjectCreationExpression(syntaxToken, argumentListSyntax, initializerExpressionSyntax);
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x0012CE3C File Offset: 0x0012B03C
		private bool IsImplicitObjectCreation()
		{
			if (base.CurrentToken.Kind != SyntaxKind.OpenParenToken)
			{
				return false;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag;
			try
			{
				base.EatToken();
				SyntaxToken syntaxToken;
				if (this.ScanTupleType(out syntaxToken) != LanguageParser.ScanTypeFlags.NotType)
				{
					SyntaxKind kind = base.CurrentToken.Kind;
					if (kind == SyntaxKind.OpenParenToken || kind == SyntaxKind.OpenBracketToken || kind == SyntaxKind.QuestionToken)
					{
						return false;
					}
				}
				flag = true;
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag;
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x0012CEC4 File Offset: 0x0012B0C4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private ExpressionSyntax ParseWithExpression(ExpressionSyntax receiverExpression, SyntaxToken withKeyword)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBraceToken);
			SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ExpressionSyntax>();
			if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
			{
				bool flag = true;
				while (!this.IsPossibleExpression() && base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					if (this.SkipBadInitializerListTokens<ExpressionSyntax>(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.IdentifierToken) == LanguageParser.PostSkipAction.Abort)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					separatedSyntaxListBuilder.Add(this.ParseExpressionCore());
					int num = -1;
					while (base.IsMakingProgress(ref num, true) && base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
					{
						if (this.IsPossibleExpression() || base.CurrentToken.Kind == SyntaxKind.CommaToken)
						{
							separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
							if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
							{
								break;
							}
							separatedSyntaxListBuilder.Add(this.ParseExpressionCore());
						}
						else if (this.SkipBadInitializerListTokens<ExpressionSyntax>(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
						{
							break;
						}
					}
				}
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBraceToken);
			InitializerExpressionSyntax initializerExpressionSyntax = this._syntaxFactory.InitializerExpression(SyntaxKind.WithInitializerExpression, syntaxToken, this._pool.ToListAndFree<ExpressionSyntax>(in separatedSyntaxListBuilder), syntaxToken2);
			withKeyword = base.CheckFeatureAvailability<SyntaxToken>(withKeyword, MessageID.IDS_FeatureRecords, false);
			return this._syntaxFactory.WithExpression(receiverExpression, withKeyword, initializerExpressionSyntax);
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x0012D014 File Offset: 0x0012B214
		private InitializerExpressionSyntax ParseObjectOrCollectionInitializer()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBraceToken);
			SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ExpressionSyntax>();
			InitializerExpressionSyntax initializerExpressionSyntax;
			try
			{
				bool flag;
				this.ParseObjectOrCollectionInitializerMembers(ref syntaxToken, separatedSyntaxListBuilder, out flag);
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, flag ? MessageID.IDS_FeatureObjectInitializer : MessageID.IDS_FeatureCollectionInitializer, false);
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBraceToken);
				initializerExpressionSyntax = this._syntaxFactory.InitializerExpression(flag ? SyntaxKind.ObjectInitializerExpression : SyntaxKind.CollectionInitializerExpression, syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._pool.Free<ExpressionSyntax>(in separatedSyntaxListBuilder);
			}
			return initializerExpressionSyntax;
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0012D0B0 File Offset: 0x0012B2B0
		private void ParseObjectOrCollectionInitializerMembers(ref SyntaxToken startToken, SeparatedSyntaxListBuilder<ExpressionSyntax> list, out bool isObjectInitializer)
		{
			isObjectInitializer = true;
			if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
			{
				while (!this.IsInitializerMember() && base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					if (this.SkipBadInitializerListTokens<ExpressionSyntax>(ref startToken, list, SyntaxKind.IdentifierToken) != LanguageParser.PostSkipAction.Continue)
					{
						return;
					}
				}
				isObjectInitializer = false;
				list.Add(this.ParseObjectOrCollectionInitializerMember(ref isObjectInitializer));
				int num = -1;
				while (base.IsMakingProgress(ref num, true))
				{
					if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
					{
						return;
					}
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsInitializerMember())
					{
						list.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
						{
							return;
						}
						list.Add(this.ParseObjectOrCollectionInitializerMember(ref isObjectInitializer));
					}
					else if (this.SkipBadInitializerListTokens<ExpressionSyntax>(ref startToken, list, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
					{
						return;
					}
				}
				return;
			}
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x0012D198 File Offset: 0x0012B398
		private ExpressionSyntax ParseObjectOrCollectionInitializerMember(ref bool isObjectInitializer)
		{
			if (this.IsComplexElementInitializer())
			{
				return this.ParseComplexElementInitializer();
			}
			if (this.IsDictionaryInitializer())
			{
				isObjectInitializer = true;
				ExpressionSyntax expressionSyntax = this.ParseDictionaryInitializer();
				return base.CheckFeatureAvailability<ExpressionSyntax>(expressionSyntax, MessageID.IDS_FeatureDictionaryInitializer, false);
			}
			if (this.IsNamedAssignment())
			{
				isObjectInitializer = true;
				return this.ParseObjectInitializerNamedAssignment();
			}
			return this.ParseExpressionCore();
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x0012D1F0 File Offset: 0x0012B3F0
		private LanguageParser.PostSkipAction SkipBadInitializerListTokens<T>(ref SyntaxToken startToken, SeparatedSyntaxListBuilder<T> list, SyntaxKind expected) where T : CSharpSyntaxNode
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, T>(ref startToken, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleExpression(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseBraceToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x0012D244 File Offset: 0x0012B444
		private ExpressionSyntax ParseObjectInitializerNamedAssignment()
		{
			IdentifierNameSyntax identifierNameSyntax = this.ParseIdentifierName(ErrorCode.ERR_IdentifierExpected);
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EqualsToken);
			ExpressionSyntax expressionSyntax;
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
			{
				expressionSyntax = this.ParseObjectOrCollectionInitializer();
			}
			else
			{
				expressionSyntax = this.ParseExpressionCore();
			}
			return this._syntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, identifierNameSyntax, syntaxToken, expressionSyntax);
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x0012D2A0 File Offset: 0x0012B4A0
		private ExpressionSyntax ParseDictionaryInitializer()
		{
			BracketedArgumentListSyntax bracketedArgumentListSyntax = this.ParseBracketedArgumentList();
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EqualsToken);
			ExpressionSyntax expressionSyntax = ((base.CurrentToken.Kind == SyntaxKind.OpenBraceToken) ? this.ParseObjectOrCollectionInitializer() : this.ParseExpressionCore());
			ImplicitElementAccessSyntax implicitElementAccessSyntax = this._syntaxFactory.ImplicitElementAccess(bracketedArgumentListSyntax);
			return this._syntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, implicitElementAccessSyntax, syntaxToken, expressionSyntax);
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x0012D304 File Offset: 0x0012B504
		private InitializerExpressionSyntax ParseComplexElementInitializer()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBraceToken);
			SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ExpressionSyntax>();
			InitializerExpressionSyntax initializerExpressionSyntax;
			try
			{
				DiagnosticInfo diagnosticInfo;
				this.ParseExpressionsForComplexElementInitializer(ref syntaxToken, separatedSyntaxListBuilder, out diagnosticInfo);
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBraceToken);
				if (diagnosticInfo != null)
				{
					syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { diagnosticInfo });
				}
				initializerExpressionSyntax = this._syntaxFactory.InitializerExpression(SyntaxKind.ComplexElementInitializerExpression, syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._pool.Free<ExpressionSyntax>(in separatedSyntaxListBuilder);
			}
			return initializerExpressionSyntax;
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x0012D394 File Offset: 0x0012B594
		private void ParseExpressionsForComplexElementInitializer(ref SyntaxToken openBrace, SeparatedSyntaxListBuilder<ExpressionSyntax> list, out DiagnosticInfo closeBraceError)
		{
			closeBraceError = null;
			if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
			{
				while (!this.IsPossibleExpression() && base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					if (this.SkipBadInitializerListTokens<ExpressionSyntax>(ref openBrace, list, SyntaxKind.IdentifierToken) != LanguageParser.PostSkipAction.Continue)
					{
						return;
					}
				}
				list.Add(this.ParseExpressionCore());
				int num = -1;
				while (base.IsMakingProgress(ref num, true))
				{
					if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
					{
						return;
					}
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleExpression())
					{
						list.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
						{
							closeBraceError = SyntaxParser.MakeError(base.CurrentToken, ErrorCode.ERR_ExpressionExpected, Array.Empty<object>());
							return;
						}
						list.Add(this.ParseExpressionCore());
					}
					else if (this.SkipBadInitializerListTokens<ExpressionSyntax>(ref openBrace, list, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
					{
						return;
					}
				}
				return;
			}
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x0012D497 File Offset: 0x0012B697
		private bool IsImplicitlyTypedArray()
		{
			return base.PeekToken(1).Kind == SyntaxKind.OpenBracketToken;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x0012D4AC File Offset: 0x0012B6AC
		private ImplicitArrayCreationExpressionSyntax ParseImplicitlyTypedArrayCreation()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.NewKeyword);
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureImplicitArray, false);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenBracketToken);
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			ImplicitArrayCreationExpressionSyntax implicitArrayCreationExpressionSyntax;
			try
			{
				int num = -1;
				while (base.IsMakingProgress(ref num, true))
				{
					if (this.IsPossibleExpression())
					{
						ExpressionSyntax expressionSyntax = base.AddError<ExpressionSyntax>(this.ParseExpressionCore(), ErrorCode.ERR_InvalidArray);
						if (syntaxListBuilder.Count == 0)
						{
							syntaxToken2 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken2, expressionSyntax);
						}
						else
						{
							base.AddTrailingSkippedSyntax(syntaxListBuilder, expressionSyntax);
						}
					}
					if (base.CurrentToken.Kind != SyntaxKind.CommaToken)
					{
						break;
					}
					syntaxListBuilder.Add(base.EatToken());
				}
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.CloseBracketToken);
				InitializerExpressionSyntax initializerExpressionSyntax = this.ParseArrayInitializer();
				implicitArrayCreationExpressionSyntax = this._syntaxFactory.ImplicitArrayCreationExpression(syntaxToken, syntaxToken2, syntaxListBuilder.ToList(), syntaxToken3, initializerExpressionSyntax);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return implicitArrayCreationExpressionSyntax;
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x0012D5A4 File Offset: 0x0012B7A4
		private InitializerExpressionSyntax ParseArrayInitializer()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBraceToken);
			SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ExpressionSyntax>();
			InitializerExpressionSyntax initializerExpressionSyntax;
			try
			{
				if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
				{
					do
					{
						IL_002D:
						if (this.IsPossibleVariableInitializer() || base.CurrentToken.Kind == SyntaxKind.CommaToken)
						{
							separatedSyntaxListBuilder.Add(this.ParseVariableInitializer());
							int num = -1;
							while (base.IsMakingProgress(ref num, true))
							{
								if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
								{
									break;
								}
								if (this.IsPossibleVariableInitializer() || base.CurrentToken.Kind == SyntaxKind.CommaToken)
								{
									separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
									if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
									{
										break;
									}
									if (!this.IsPossibleVariableInitializer())
									{
										goto IL_002D;
									}
									separatedSyntaxListBuilder.Add(this.ParseVariableInitializer());
								}
								else if (this.SkipBadArrayInitializerTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
								{
									break;
								}
							}
							break;
						}
					}
					while (this.SkipBadArrayInitializerTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Continue);
				}
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBraceToken);
				initializerExpressionSyntax = this._syntaxFactory.InitializerExpression(SyntaxKind.ArrayInitializerExpression, syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._pool.Free<ExpressionSyntax>(in separatedSyntaxListBuilder);
			}
			return initializerExpressionSyntax;
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x0012D704 File Offset: 0x0012B904
		private LanguageParser.PostSkipAction SkipBadArrayInitializerTokens(ref SyntaxToken openBrace, SeparatedSyntaxListBuilder<ExpressionSyntax> list, SyntaxKind expected)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, ExpressionSyntax>(ref openBrace, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleVariableInitializer(), (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseBraceToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x0012D758 File Offset: 0x0012B958
		private ExpressionSyntax ParseStackAllocExpression()
		{
			if (this.IsImplicitlyTypedArray())
			{
				return this.ParseImplicitlyTypedStackAllocExpression();
			}
			return this.ParseRegularStackAllocExpression();
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x0012D770 File Offset: 0x0012B970
		private ExpressionSyntax ParseImplicitlyTypedStackAllocExpression()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.StackAllocKeyword);
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureStackAllocInitializer, false);
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenBracketToken);
			int num = -1;
			while (base.IsMakingProgress(ref num, true))
			{
				if (this.IsPossibleExpression())
				{
					ExpressionSyntax expressionSyntax = base.AddError<ExpressionSyntax>(this.ParseExpressionCore(), ErrorCode.ERR_InvalidStackAllocArray);
					syntaxToken2 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken2, expressionSyntax);
				}
				if (base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					break;
				}
				SyntaxToken syntaxToken3 = base.AddError<SyntaxToken>(base.EatToken(), ErrorCode.ERR_InvalidStackAllocArray);
				syntaxToken2 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken2, syntaxToken3);
			}
			SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseBracketToken);
			InitializerExpressionSyntax initializerExpressionSyntax = this.ParseArrayInitializer();
			return this._syntaxFactory.ImplicitStackAllocArrayCreationExpression(syntaxToken, syntaxToken2, syntaxToken4, initializerExpressionSyntax);
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x0012D82C File Offset: 0x0012BA2C
		private ExpressionSyntax ParseRegularStackAllocExpression()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.StackAllocKeyword);
			TypeSyntax typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			InitializerExpressionSyntax initializerExpressionSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
			{
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureStackAllocInitializer, false);
				initializerExpressionSyntax = this.ParseArrayInitializer();
			}
			return this._syntaxFactory.StackAllocArrayCreationExpression(syntaxToken, typeSyntax, initializerExpressionSyntax);
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x0012D884 File Offset: 0x0012BA84
		private AnonymousMethodExpressionSyntax ParseAnonymousMethodExpression()
		{
			bool isInAsync = this.IsInAsync;
			AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax = this.<ParseAnonymousMethodExpression>g__parseAnonymousMethodExpressionWorker|380_0();
			this.IsInAsync = isInAsync;
			return anonymousMethodExpressionSyntax;
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0012D8A8 File Offset: 0x0012BAA8
		private SyntaxList<SyntaxToken> ParseAnonymousFunctionModifiers()
		{
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			for (;;)
			{
				if (base.CurrentToken.Kind == SyntaxKind.StaticKeyword)
				{
					SyntaxToken syntaxToken = base.EatToken(SyntaxKind.StaticKeyword);
					syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureStaticAnonymousFunction, false);
					syntaxListBuilder.Add(syntaxToken);
				}
				else
				{
					if (base.CurrentToken.ContextualKind != SyntaxKind.AsyncKeyword || base.PeekToken(1).Kind == SyntaxKind.EqualsGreaterThanToken)
					{
						break;
					}
					SyntaxToken syntaxToken2 = base.EatContextualToken(SyntaxKind.AsyncKeyword, true);
					syntaxToken2 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken2, MessageID.IDS_FeatureAsync, false);
					syntaxListBuilder.Add(syntaxToken2);
				}
			}
			SyntaxList<GreenNode> syntaxList = syntaxListBuilder.ToList();
			this._pool.Free(syntaxListBuilder);
			return syntaxList;
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x0012D958 File Offset: 0x0012BB58
		private LambdaExpressionSyntax ParseLambdaExpression()
		{
			LanguageParser.<>c__DisplayClass382_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.attributes = this.ParseAttributeDeclarations();
			bool isInAsync = this.IsInAsync;
			LambdaExpressionSyntax lambdaExpressionSyntax = this.<ParseLambdaExpression>g__parseLambdaExpressionWorker|382_0(ref CS$<>8__locals1);
			this.IsInAsync = isInAsync;
			return lambdaExpressionSyntax;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0012D990 File Offset: 0x0012BB90
		private ValueTuple<BlockSyntax, ExpressionSyntax> ParseLambdaBody()
		{
			if (base.CurrentToken.Kind != SyntaxKind.OpenBraceToken)
			{
				return new ValueTuple<BlockSyntax, ExpressionSyntax>(null, this.ParsePossibleRefExpression());
			}
			return new ValueTuple<BlockSyntax, ExpressionSyntax>(this.ParseBlock(default(SyntaxList<AttributeListSyntax>)), null);
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x0012D9D4 File Offset: 0x0012BBD4
		private ParameterListSyntax ParseLambdaParameterList()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenParenToken);
			LanguageParser.TerminatorState termState = this._termState;
			this._termState |= LanguageParser.TerminatorState.IsEndOfParameterList;
			SeparatedSyntaxListBuilder<ParameterSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ParameterSyntax>();
			ParameterListSyntax parameterListSyntax;
			try
			{
				if (base.CurrentToken.Kind != SyntaxKind.CloseParenToken)
				{
					while (base.CurrentToken.Kind != SyntaxKind.CommaToken && !this.IsPossibleLambdaParameter())
					{
						if (this.SkipBadLambdaParameterListTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.IdentifierToken, SyntaxKind.CloseParenToken) != LanguageParser.PostSkipAction.Continue)
						{
							goto IL_0104;
						}
					}
					ParameterSyntax parameterSyntax = this.ParseLambdaParameter();
					separatedSyntaxListBuilder.Add(parameterSyntax);
					int num = -1;
					while (base.IsMakingProgress(ref num, true))
					{
						if (base.CurrentToken.Kind == SyntaxKind.CloseParenToken)
						{
							break;
						}
						if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleLambdaParameter())
						{
							separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
							parameterSyntax = this.ParseLambdaParameter();
							separatedSyntaxListBuilder.Add(parameterSyntax);
						}
						else if (this.SkipBadLambdaParameterListTokens(ref syntaxToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken, SyntaxKind.CloseParenToken) == LanguageParser.PostSkipAction.Abort)
						{
							break;
						}
					}
				}
				IL_0104:
				this._termState = termState;
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseParenToken);
				parameterListSyntax = this._syntaxFactory.ParameterList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2);
			}
			finally
			{
				this._pool.Free<ParameterSyntax>(in separatedSyntaxListBuilder);
			}
			return parameterListSyntax;
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x0012DB3C File Offset: 0x0012BD3C
		private bool IsPossibleLambdaParameter()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.InKeyword)
			{
				if (kind != SyntaxKind.OpenParenToken && kind != SyntaxKind.OpenBracketToken && kind - SyntaxKind.RefKeyword > 2)
				{
					goto IL_005A;
				}
			}
			else if (kind != SyntaxKind.ParamsKeyword)
			{
				if (kind == SyntaxKind.DelegateKeyword)
				{
					return this.IsFunctionPointerStart();
				}
				if (kind != SyntaxKind.IdentifierToken)
				{
					goto IL_005A;
				}
				return this.IsTrueIdentifier();
			}
			return true;
			IL_005A:
			return LanguageParser.IsPredefinedType(base.CurrentToken.Kind);
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x0012DBB4 File Offset: 0x0012BDB4
		private LanguageParser.PostSkipAction SkipBadLambdaParameterListTokens(ref SyntaxToken openParen, SeparatedSyntaxListBuilder<ParameterSyntax> list, SyntaxKind expected, SyntaxKind closeKind)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, ParameterSyntax>(ref openParen, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleLambdaParameter(), (LanguageParser p) => p.CurrentToken.Kind == closeKind || p.IsTerminator(), expected);
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x0012DC04 File Offset: 0x0012BE04
		private ParameterSyntax ParseLambdaParameter()
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.ParseAttributeDeclarations();
			bool flag = LanguageParser.IsParameterModifier(base.CurrentToken.Kind, false);
			TypeSyntax typeSyntax = null;
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			if (this.ShouldParseLambdaParameterType(flag))
			{
				if (flag)
				{
					this.ParseParameterModifiers(syntaxListBuilder, false);
				}
				typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Parameter);
			}
			SyntaxToken syntaxToken = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			ParameterSyntax parameterSyntax = this._syntaxFactory.Parameter(syntaxList, syntaxListBuilder.ToList(), typeSyntax, syntaxToken, null);
			this._pool.Free(syntaxListBuilder);
			return parameterSyntax;
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x0012DC88 File Offset: 0x0012BE88
		private bool ShouldParseLambdaParameterType(bool hasModifier)
		{
			if (hasModifier)
			{
				return true;
			}
			if (LanguageParser.IsPredefinedType(base.CurrentToken.Kind))
			{
				return true;
			}
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				return true;
			}
			if (this.IsFunctionPointerStart())
			{
				return true;
			}
			if (this.IsTrueIdentifier(base.CurrentToken))
			{
				SyntaxToken syntaxToken = base.PeekToken(1);
				if (syntaxToken.Kind != SyntaxKind.CommaToken && syntaxToken.Kind != SyntaxKind.CloseParenToken && syntaxToken.Kind != SyntaxKind.EqualsGreaterThanToken && syntaxToken.Kind != SyntaxKind.OpenBraceToken)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x0012DD19 File Offset: 0x0012BF19
		private bool IsCurrentTokenQueryContextualKeyword
		{
			get
			{
				return LanguageParser.IsTokenQueryContextualKeyword(base.CurrentToken);
			}
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x0012DD28 File Offset: 0x0012BF28
		private static bool IsTokenQueryContextualKeyword(SyntaxToken token)
		{
			if (LanguageParser.IsTokenStartOfNewQueryClause(token))
			{
				return true;
			}
			SyntaxKind contextualKind = token.ContextualKind;
			return contextualKind == SyntaxKind.ByKeyword || contextualKind - SyntaxKind.OnKeyword <= 3;
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x0012DD5C File Offset: 0x0012BF5C
		private static bool IsTokenStartOfNewQueryClause(SyntaxToken token)
		{
			SyntaxKind contextualKind = token.ContextualKind;
			return contextualKind - SyntaxKind.WhereKeyword <= 5 || contextualKind - SyntaxKind.SelectKeyword <= 1;
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x0012DD87 File Offset: 0x0012BF87
		private bool IsQueryExpression(bool mayBeVariableDeclaration, bool mayBeMemberDeclaration)
		{
			return base.CurrentToken.ContextualKind == SyntaxKind.FromKeyword && this.IsQueryExpressionAfterFrom(mayBeVariableDeclaration, mayBeMemberDeclaration);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x0012DDA8 File Offset: 0x0012BFA8
		private bool IsQueryExpressionAfterFrom(bool mayBeVariableDeclaration, bool mayBeMemberDeclaration)
		{
			SyntaxKind kind = base.PeekToken(1).Kind;
			if (LanguageParser.IsPredefinedType(kind))
			{
				return true;
			}
			if (kind == SyntaxKind.IdentifierToken)
			{
				SyntaxKind kind2 = base.PeekToken(2).Kind;
				if (kind2 == SyntaxKind.InKeyword)
				{
					return true;
				}
				if (mayBeVariableDeclaration && (kind2 == SyntaxKind.SemicolonToken || kind2 == SyntaxKind.CommaToken || kind2 == SyntaxKind.EqualsToken))
				{
					return false;
				}
				if (!mayBeMemberDeclaration)
				{
					return true;
				}
				if (kind2 == SyntaxKind.OpenParenToken || kind2 == SyntaxKind.OpenBraceToken)
				{
					return false;
				}
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			try
			{
				base.EatToken();
				if (this.ScanType(false) != LanguageParser.ScanTypeFlags.NotType && (base.CurrentToken.Kind == SyntaxKind.IdentifierToken || base.CurrentToken.Kind == SyntaxKind.InKeyword))
				{
					return true;
				}
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return false;
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x0012DE84 File Offset: 0x0012C084
		private QueryExpressionSyntax ParseQueryExpression(LanguageParser.Precedence precedence)
		{
			this.EnterQuery();
			FromClauseSyntax fromClauseSyntax = this.ParseFromClause();
			fromClauseSyntax = base.CheckFeatureAvailability<FromClauseSyntax>(fromClauseSyntax, MessageID.IDS_FeatureQueryExpression, false);
			if (precedence > LanguageParser.Precedence.Expression)
			{
				fromClauseSyntax = base.AddError<FromClauseSyntax>(fromClauseSyntax, ErrorCode.WRN_PrecedenceInversion, new object[] { SyntaxFacts.GetText(SyntaxKind.FromKeyword) });
			}
			QueryBodySyntax queryBodySyntax = this.ParseQueryBody();
			this.LeaveQuery();
			return this._syntaxFactory.QueryExpression(fromClauseSyntax, queryBodySyntax);
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x0012DEEC File Offset: 0x0012C0EC
		private QueryBodySyntax ParseQueryBody()
		{
			SyntaxListBuilder<QueryClauseSyntax> syntaxListBuilder = this._pool.Allocate<QueryClauseSyntax>();
			QueryBodySyntax queryBodySyntax;
			try
			{
				QueryContinuationSyntax queryContinuationSyntax = null;
				for (;;)
				{
					switch (base.CurrentToken.ContextualKind)
					{
					case SyntaxKind.WhereKeyword:
						syntaxListBuilder.Add(this.ParseWhereClause());
						continue;
					case SyntaxKind.FromKeyword:
					{
						FromClauseSyntax fromClauseSyntax = this.ParseFromClause();
						syntaxListBuilder.Add(fromClauseSyntax);
						continue;
					}
					case SyntaxKind.JoinKeyword:
						syntaxListBuilder.Add(this.ParseJoinClause());
						continue;
					case SyntaxKind.LetKeyword:
						syntaxListBuilder.Add(this.ParseLetClause());
						continue;
					case SyntaxKind.OrderByKeyword:
						syntaxListBuilder.Add(this.ParseOrderByClause());
						continue;
					}
					break;
				}
				SyntaxKind contextualKind = base.CurrentToken.ContextualKind;
				SelectOrGroupClauseSyntax selectOrGroupClauseSyntax;
				if (contextualKind != SyntaxKind.GroupKeyword)
				{
					if (contextualKind == SyntaxKind.SelectKeyword)
					{
						selectOrGroupClauseSyntax = this.ParseSelectClause();
					}
					else
					{
						selectOrGroupClauseSyntax = this._syntaxFactory.SelectClause(base.EatToken(SyntaxKind.SelectKeyword, ErrorCode.ERR_ExpectedSelectOrGroup, true), this.CreateMissingIdentifierName());
					}
				}
				else
				{
					selectOrGroupClauseSyntax = this.ParseGroupClause();
				}
				if (base.CurrentToken.ContextualKind == SyntaxKind.IntoKeyword)
				{
					queryContinuationSyntax = this.ParseQueryContinuation();
				}
				queryBodySyntax = this._syntaxFactory.QueryBody(syntaxListBuilder, selectOrGroupClauseSyntax, queryContinuationSyntax);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return queryBodySyntax;
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x0012E058 File Offset: 0x0012C258
		private FromClauseSyntax ParseFromClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.FromKeyword, true);
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureQueryExpression, false);
			TypeSyntax typeSyntax = null;
			if (base.PeekToken(1).Kind != SyntaxKind.InKeyword)
			{
				typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			}
			SyntaxToken syntaxToken2;
			if (base.PeekToken(1).ContextualKind == SyntaxKind.InKeyword && (base.CurrentToken.Kind != SyntaxKind.IdentifierToken || SyntaxFacts.IsQueryContextualKeyword(base.CurrentToken.ContextualKind)))
			{
				syntaxToken2 = base.EatToken();
				syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { this.GetExpectedTokenError(SyntaxKind.IdentifierToken, syntaxToken2.ContextualKind, syntaxToken2.GetLeadingTriviaWidth(), syntaxToken2.Width) });
				syntaxToken2 = this.ConvertToMissingWithTrailingTrivia(syntaxToken2, SyntaxKind.IdentifierToken);
			}
			else
			{
				syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			}
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.InKeyword);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			return this._syntaxFactory.FromClause(syntaxToken, typeSyntax, syntaxToken2, syntaxToken3, expressionSyntax);
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x0012E14C File Offset: 0x0012C34C
		private JoinClauseSyntax ParseJoinClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.JoinKeyword, true);
			TypeSyntax typeSyntax = null;
			if (base.PeekToken(1).Kind != SyntaxKind.InKeyword)
			{
				typeSyntax = this.ParseType(LanguageParser.ParseTypeMode.Normal);
			}
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.InKeyword);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			SyntaxToken syntaxToken4 = base.EatContextualToken(SyntaxKind.OnKeyword, ErrorCode.ERR_ExpectedContextualKeywordOn, true);
			ExpressionSyntax expressionSyntax2 = this.ParseExpressionCore();
			SyntaxToken syntaxToken5 = base.EatContextualToken(SyntaxKind.EqualsKeyword, ErrorCode.ERR_ExpectedContextualKeywordEquals, true);
			ExpressionSyntax expressionSyntax3 = this.ParseExpressionCore();
			JoinIntoClauseSyntax joinIntoClauseSyntax = null;
			if (base.CurrentToken.ContextualKind == SyntaxKind.IntoKeyword)
			{
				SyntaxToken syntaxToken6 = SyntaxParser.ConvertToKeyword(base.EatToken());
				SyntaxToken syntaxToken7 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
				joinIntoClauseSyntax = this._syntaxFactory.JoinIntoClause(syntaxToken6, syntaxToken7);
			}
			return this._syntaxFactory.JoinClause(syntaxToken, typeSyntax, syntaxToken2, syntaxToken3, expressionSyntax, syntaxToken4, expressionSyntax2, syntaxToken5, expressionSyntax3, joinIntoClauseSyntax);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x0012E234 File Offset: 0x0012C434
		private LetClauseSyntax ParseLetClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.LetKeyword, true);
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.EqualsToken);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			return this._syntaxFactory.LetClause(syntaxToken, syntaxToken2, syntaxToken3, expressionSyntax);
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x0012E27C File Offset: 0x0012C47C
		private WhereClauseSyntax ParseWhereClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.WhereKeyword, true);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			return this._syntaxFactory.WhereClause(syntaxToken, expressionSyntax);
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x0012E2AC File Offset: 0x0012C4AC
		private OrderByClauseSyntax ParseOrderByClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.OrderByKeyword, true);
			SeparatedSyntaxListBuilder<OrderingSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<OrderingSyntax>();
			OrderByClauseSyntax orderByClauseSyntax;
			try
			{
				separatedSyntaxListBuilder.Add(this.ParseOrdering());
				while (base.CurrentToken.Kind == SyntaxKind.CommaToken && base.CurrentToken.Kind != SyntaxKind.CloseParenToken && base.CurrentToken.Kind != SyntaxKind.SemicolonToken)
				{
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
					{
						separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						separatedSyntaxListBuilder.Add(this.ParseOrdering());
					}
					else if (this.SkipBadOrderingListTokens(separatedSyntaxListBuilder, SyntaxKind.CommaToken) == LanguageParser.PostSkipAction.Abort)
					{
						break;
					}
				}
				orderByClauseSyntax = this._syntaxFactory.OrderByClause(syntaxToken, in separatedSyntaxListBuilder);
			}
			finally
			{
				this._pool.Free<OrderingSyntax>(in separatedSyntaxListBuilder);
			}
			return orderByClauseSyntax;
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0012E390 File Offset: 0x0012C590
		private LanguageParser.PostSkipAction SkipBadOrderingListTokens(SeparatedSyntaxListBuilder<OrderingSyntax> list, SyntaxKind expected)
		{
			CSharpSyntaxNode csharpSyntaxNode = null;
			return this.SkipBadSeparatedListTokensWithExpectedKind<CSharpSyntaxNode, OrderingSyntax>(ref csharpSyntaxNode, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken, (LanguageParser p) => p.CurrentToken.Kind == SyntaxKind.CloseParenToken || p.CurrentToken.Kind == SyntaxKind.SemicolonToken || p.IsCurrentTokenQueryContextualKeyword || p.IsTerminator(), expected);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0012E3E8 File Offset: 0x0012C5E8
		private OrderingSyntax ParseOrdering()
		{
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			SyntaxToken syntaxToken = null;
			SyntaxKind syntaxKind = SyntaxKind.AscendingOrdering;
			if (base.CurrentToken.ContextualKind == SyntaxKind.AscendingKeyword || base.CurrentToken.ContextualKind == SyntaxKind.DescendingKeyword)
			{
				syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
				if (syntaxToken.Kind == SyntaxKind.DescendingKeyword)
				{
					syntaxKind = SyntaxKind.DescendingOrdering;
				}
			}
			return this._syntaxFactory.Ordering(syntaxKind, expressionSyntax, syntaxToken);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x0012E458 File Offset: 0x0012C658
		private SelectClauseSyntax ParseSelectClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.SelectKeyword, true);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			return this._syntaxFactory.SelectClause(syntaxToken, expressionSyntax);
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x0012E488 File Offset: 0x0012C688
		private GroupClauseSyntax ParseGroupClause()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.GroupKeyword, true);
			ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
			SyntaxToken syntaxToken2 = base.EatContextualToken(SyntaxKind.ByKeyword, ErrorCode.ERR_ExpectedContextualKeywordBy, true);
			ExpressionSyntax expressionSyntax2 = this.ParseExpressionCore();
			return this._syntaxFactory.GroupClause(syntaxToken, expressionSyntax, syntaxToken2, expressionSyntax2);
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x0012E4D4 File Offset: 0x0012C6D4
		private QueryContinuationSyntax ParseQueryContinuation()
		{
			SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.IntoKeyword, true);
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			QueryBodySyntax queryBodySyntax = this.ParseQueryBody();
			return this._syntaxFactory.QueryContinuation(syntaxToken, syntaxToken2, queryBodySyntax);
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x0012E50F File Offset: 0x0012C70F
		[Obsolete("Use IsIncrementalAndFactoryContextMatches")]
		private new bool IsIncremental
		{
			get
			{
				throw new Exception("Use IsIncrementalAndFactoryContextMatches");
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x0012E51C File Offset: 0x0012C71C
		private bool IsIncrementalAndFactoryContextMatches
		{
			get
			{
				if (!base.IsIncremental)
				{
					return false;
				}
				CSharpSyntaxNode currentNode = base.CurrentNode;
				return currentNode != null && LanguageParser.MatchesFactoryContext(currentNode.Green, this._syntaxFactoryContext);
			}
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x0012E550 File Offset: 0x0012C750
		internal static bool MatchesFactoryContext(GreenNode green, SyntaxFactoryContext context)
		{
			return context.IsInAsync == green.ParsedInAsync && context.IsInQuery == green.ParsedInQuery;
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06004A29 RID: 18985 RVA: 0x0012E570 File Offset: 0x0012C770
		// (set) Token: 0x06004A2A RID: 18986 RVA: 0x0012E57D File Offset: 0x0012C77D
		private bool IsInAsync
		{
			get
			{
				return this._syntaxFactoryContext.IsInAsync;
			}
			set
			{
				this._syntaxFactoryContext.IsInAsync = value;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x0012E58B File Offset: 0x0012C78B
		private bool IsInQuery
		{
			get
			{
				return this._syntaxFactoryContext.IsInQuery;
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0012E598 File Offset: 0x0012C798
		private void EnterQuery()
		{
			this._syntaxFactoryContext.QueryDepth++;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0012E5AD File Offset: 0x0012C7AD
		private void LeaveQuery()
		{
			this._syntaxFactoryContext.QueryDepth--;
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x0012E5C2 File Offset: 0x0012C7C2
		private new LanguageParser.ResetPoint GetResetPoint()
		{
			return new LanguageParser.ResetPoint(base.GetResetPoint(), this._termState, this._isInTry, this._syntaxFactoryContext.IsInAsync, this._syntaxFactoryContext.QueryDepth);
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x0012E5F4 File Offset: 0x0012C7F4
		private void Reset(ref LanguageParser.ResetPoint state)
		{
			this._termState = state.TerminatorState;
			this._isInTry = state.IsInTry;
			this._syntaxFactoryContext.IsInAsync = state.IsInAsync;
			this._syntaxFactoryContext.QueryDepth = state.QueryDepth;
			base.Reset(ref state.BaseResetPoint);
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x0012E647 File Offset: 0x0012C847
		private void Release(ref LanguageParser.ResetPoint state)
		{
			base.Release(ref state.BaseResetPoint);
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x0012E658 File Offset: 0x0012C858
		internal TNode ConsumeUnexpectedTokens<TNode>(TNode node) where TNode : CSharpSyntaxNode
		{
			if (base.CurrentToken.Kind == SyntaxKind.EndOfFileToken)
			{
				return node;
			}
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
			while (base.CurrentToken.Kind != SyntaxKind.EndOfFileToken)
			{
				syntaxListBuilder.Add(base.EatToken());
			}
			SyntaxList<SyntaxToken> syntaxList = syntaxListBuilder.ToList();
			this._pool.Free(syntaxListBuilder);
			node = base.AddError<TNode>(node, ErrorCode.ERR_UnexpectedToken, new object[] { syntaxList[0].ToString() });
			node = base.AddTrailingSkippedSyntax<TNode>(node, syntaxList.Node);
			return node;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x0012E6F8 File Offset: 0x0012C8F8
		private string Substring(string s, int first, int last)
		{
			if (last >= s.Length)
			{
				last = s.Length - 1;
			}
			int num = last - first + 1;
			if (last <= s.Length && num > 0)
			{
				return s.Substring(first, num);
			}
			return string.Empty;
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x0012E73C File Offset: 0x0012C93C
		private ExpressionSyntax ParseInterpolatedStringToken()
		{
			SyntaxToken syntaxToken = base.EatToken();
			string valueText = syntaxToken.ValueText;
			bool flag = valueText.Length > 2 && valueText.get_Chars(0) == '@';
			bool flag2 = flag || (valueText.Length > 2 && valueText.get_Chars(1) == '@');
			ArrayBuilder<Lexer.Interpolation> instance = ArrayBuilder<Lexer.Interpolation>.GetInstance();
			SyntaxDiagnosticInfo syntaxDiagnosticInfo = null;
			bool flag3;
			using (Lexer lexer = new Lexer(SourceText.From(valueText, null, SourceHashAlgorithm.Sha1), base.Options, false, false))
			{
				Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
				lexer.ScanInterpolatedStringLiteralTop(instance, flag2, ref tokenInfo, ref syntaxDiagnosticInfo, out flag3);
			}
			int num = (flag2 ? 2 : 1);
			SyntaxKind syntaxKind = (flag2 ? SyntaxKind.InterpolatedVerbatimStringStartToken : SyntaxKind.InterpolatedStringStartToken);
			string text = (flag ? "@$\"" : (flag2 ? "$@\"" : "$\""));
			SyntaxToken syntaxToken2 = SyntaxFactory.Token(syntaxToken.GetLeadingTrivia(), syntaxKind, text, text, null);
			if (flag)
			{
				syntaxToken2 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken2, MessageID.IDS_FeatureAltInterpolatedVerbatimStrings, false);
			}
			int num2 = (flag3 ? valueText.Length : (valueText.Length - 1));
			SyntaxToken syntaxToken3 = (flag3 ? SyntaxFactory.MissingToken(SyntaxKind.InterpolatedStringEndToken).TokenWithTrailingTrivia(syntaxToken.GetTrailingTrivia()) : SyntaxFactory.Token(null, SyntaxKind.InterpolatedStringEndToken, syntaxToken.GetTrailingTrivia()));
			SyntaxListBuilder<InterpolatedStringContentSyntax> syntaxListBuilder = this._pool.Allocate<InterpolatedStringContentSyntax>();
			if (instance.Count == 0)
			{
				string text2 = this.Substring(valueText, num + 1, num2 - 1);
				if (text2.Length > 0)
				{
					SyntaxToken syntaxToken4 = this.MakeStringToken(text2, text2, flag2, SyntaxKind.InterpolatedStringTextToken);
					syntaxListBuilder.Add(SyntaxFactory.InterpolatedStringText(syntaxToken4));
				}
			}
			else
			{
				for (int i = 0; i < instance.Count; i++)
				{
					Lexer.Interpolation interpolation = instance[i];
					string text3 = this.Substring(valueText, (i == 0) ? (num + 1) : (instance[i - 1].CloseBracePosition + 1), interpolation.OpenBracePosition - 1);
					if (text3.Length > 0)
					{
						SyntaxToken syntaxToken5 = this.MakeStringToken(text3, text3, flag2, SyntaxKind.InterpolatedStringTextToken);
						syntaxListBuilder.Add(SyntaxFactory.InterpolatedStringText(syntaxToken5));
					}
					InterpolationSyntax interpolationSyntax = this.ParseInterpolation(valueText, interpolation, flag2);
					syntaxListBuilder.Add(interpolationSyntax);
				}
				string text4 = this.Substring(valueText, instance[instance.Count - 1].CloseBracePosition + 1, num2 - 1);
				if (text4.Length > 0)
				{
					SyntaxToken syntaxToken6 = this.MakeStringToken(text4, text4, flag2, SyntaxKind.InterpolatedStringTextToken);
					syntaxListBuilder.Add(SyntaxFactory.InterpolatedStringText(syntaxToken6));
				}
			}
			instance.Free();
			InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax = SyntaxFactory.InterpolatedStringExpression(syntaxToken2, syntaxListBuilder, syntaxToken3);
			this._pool.Free(syntaxListBuilder);
			if (syntaxDiagnosticInfo != null)
			{
				InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax2 = interpolatedStringExpressionSyntax;
				DiagnosticInfo[] array = new SyntaxDiagnosticInfo[] { syntaxDiagnosticInfo };
				interpolatedStringExpressionSyntax = interpolatedStringExpressionSyntax2.WithDiagnosticsGreen(array);
			}
			return base.CheckFeatureAvailability<InterpolatedStringExpressionSyntax>(interpolatedStringExpressionSyntax, MessageID.IDS_FeatureInterpolatedStrings, false);
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x0012EA18 File Offset: 0x0012CC18
		private InterpolationSyntax ParseInterpolation(string text, Lexer.Interpolation interpolation, bool isVerbatim)
		{
			InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax = null;
			InterpolationFormatClauseSyntax interpolationFormatClauseSyntax = null;
			SyntaxToken syntaxToken = (interpolation.CloseBraceMissing ? SyntaxFactory.MissingToken(SyntaxKind.CloseBraceToken) : SyntaxFactory.Token(SyntaxKind.CloseBraceToken));
			SyntaxToken syntaxToken3;
			ExpressionSyntax expressionSyntax2;
			using (Lexer lexer = new Lexer(SourceText.From(this.Substring(text, interpolation.OpenBracePosition, interpolation.HasColon ? (interpolation.ColonPosition - 1) : (interpolation.CloseBracePosition - 1)), null, SourceHashAlgorithm.Sha1), base.Options, false, interpolation.HasColon))
			{
				using (LanguageParser languageParser = new LanguageParser(lexer, null, null, LexerMode.Syntax, default(CancellationToken)))
				{
					SyntaxToken syntaxToken2 = null;
					ExpressionSyntax expressionSyntax = null;
					languageParser.ParseInterpolationStart(out syntaxToken3, out expressionSyntax2, out syntaxToken2, out expressionSyntax);
					if (expressionSyntax != null)
					{
						interpolationAlignmentClauseSyntax = SyntaxFactory.InterpolationAlignmentClause(syntaxToken2, expressionSyntax);
					}
					GreenNode leadingTrivia = languageParser.CurrentToken.GetLeadingTrivia();
					if (interpolation.HasColon)
					{
						SyntaxToken syntaxToken4 = SyntaxFactory.Token(SyntaxKind.ColonToken).TokenWithLeadingTrivia(leadingTrivia);
						string text2 = this.Substring(text, interpolation.ColonPosition + 1, interpolation.FormatEndPosition);
						SyntaxToken syntaxToken5 = this.MakeStringToken(text2, text2, isVerbatim, SyntaxKind.InterpolatedStringTextToken);
						interpolationFormatClauseSyntax = SyntaxFactory.InterpolationFormatClause(syntaxToken4, syntaxToken5);
					}
					else
					{
						syntaxToken = syntaxToken.TokenWithLeadingTrivia(leadingTrivia);
					}
				}
			}
			return SyntaxFactory.Interpolation(syntaxToken3, expressionSyntax2, interpolationAlignmentClauseSyntax, interpolationFormatClauseSyntax, syntaxToken);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x0012EB70 File Offset: 0x0012CD70
		private SyntaxToken MakeStringToken(string text, string bodyText, bool isVerbatim, SyntaxKind kind)
		{
			string text2 = (isVerbatim ? "@\"" : "\"");
			SyntaxToken syntaxToken3;
			using (Lexer lexer = new Lexer(SourceText.From(text2 + bodyText + "\"", null, SourceHashAlgorithm.Sha1), base.Options, false, false))
			{
				LexerMode lexerMode = LexerMode.Syntax;
				SyntaxToken syntaxToken = lexer.Lex(ref lexerMode);
				SyntaxToken syntaxToken2 = SyntaxFactory.Literal(null, text, kind, syntaxToken.ValueText, null);
				if (syntaxToken.ContainsDiagnostics)
				{
					syntaxToken2 = syntaxToken2.WithDiagnosticsGreen(LanguageParser.MoveDiagnostics(syntaxToken.GetDiagnostics(), -text2.Length));
				}
				syntaxToken3 = syntaxToken2;
			}
			return syntaxToken3;
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x0012EC10 File Offset: 0x0012CE10
		private static DiagnosticInfo[] MoveDiagnostics(DiagnosticInfo[] infos, int offset)
		{
			ArrayBuilder<DiagnosticInfo> instance = ArrayBuilder<DiagnosticInfo>.GetInstance();
			foreach (DiagnosticInfo diagnosticInfo in infos)
			{
				SyntaxDiagnosticInfo syntaxDiagnosticInfo = diagnosticInfo as SyntaxDiagnosticInfo;
				instance.Add(((syntaxDiagnosticInfo != null) ? syntaxDiagnosticInfo.WithOffset(syntaxDiagnosticInfo.Offset + offset) : null) ?? diagnosticInfo);
			}
			return instance.ToArrayAndFree();
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x0012EC68 File Offset: 0x0012CE68
		private void ParseInterpolationStart(out SyntaxToken openBraceToken, out ExpressionSyntax expr, out SyntaxToken commaToken, out ExpressionSyntax alignmentExpression)
		{
			openBraceToken = base.EatToken(SyntaxKind.OpenBraceToken);
			expr = this.ParseExpressionCore();
			if (base.CurrentToken.Kind == SyntaxKind.CommaToken)
			{
				commaToken = base.EatToken(SyntaxKind.CommaToken);
				alignmentExpression = this.ConsumeUnexpectedTokens<ExpressionSyntax>(this.ParseExpressionCore());
				return;
			}
			commaToken = null;
			alignmentExpression = null;
			expr = this.ConsumeUnexpectedTokens<ExpressionSyntax>(expr);
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x0012ECCA File Offset: 0x0012CECA
		private CSharpSyntaxNode ParseTypeOrPatternForIsOperator()
		{
			return this.CheckRecursivePatternFeature(this.ParseTypeOrPatternForIsOperatorCore());
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0012ECD8 File Offset: 0x0012CED8
		private CSharpSyntaxNode CheckRecursivePatternFeature(CSharpSyntaxNode node)
		{
			SyntaxKind kind = node.Kind;
			if (kind == SyntaxKind.RecursivePattern || kind == SyntaxKind.DiscardPattern || (kind == SyntaxKind.VarPattern && ((VarPatternSyntax)node).Designation.Kind == SyntaxKind.ParenthesizedVariableDesignation))
			{
				return base.CheckFeatureAvailability<CSharpSyntaxNode>(node, MessageID.IDS_FeatureRecursivePatterns, false);
			}
			return node;
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0012ED2C File Offset: 0x0012CF2C
		private CSharpSyntaxNode ParseTypeOrPatternForIsOperatorCore()
		{
			PatternSyntax patternSyntax = this.ParsePattern(LanguageParser.GetPrecedence(SyntaxKind.IsPatternExpression), true, false);
			ConstantPatternSyntax constantPatternSyntax = patternSyntax as ConstantPatternSyntax;
			if (constantPatternSyntax == null)
			{
				TypePatternSyntax typePatternSyntax = patternSyntax as TypePatternSyntax;
				if (typePatternSyntax != null)
				{
					return typePatternSyntax.Type;
				}
				DiscardPatternSyntax discardPatternSyntax = patternSyntax as DiscardPatternSyntax;
				if (discardPatternSyntax != null)
				{
					DiscardPatternSyntax discardPatternSyntax2 = discardPatternSyntax;
					return this._syntaxFactory.IdentifierName(SyntaxParser.ConvertToIdentifier(discardPatternSyntax2.UnderscoreToken));
				}
			}
			else
			{
				ConstantPatternSyntax constantPatternSyntax2 = constantPatternSyntax;
				NameSyntax nameSyntax;
				if (this.ConvertExpressionToType(constantPatternSyntax2.Expression, out nameSyntax))
				{
					return nameSyntax;
				}
			}
			return patternSyntax;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x0012EDB8 File Offset: 0x0012CFB8
		private bool ConvertExpressionToType(ExpressionSyntax expression, out NameSyntax type)
		{
			type = null;
			SimpleNameSyntax simpleNameSyntax = expression as SimpleNameSyntax;
			if (simpleNameSyntax == null)
			{
				MemberAccessExpressionSyntax memberAccessExpressionSyntax = expression as MemberAccessExpressionSyntax;
				if (memberAccessExpressionSyntax != null)
				{
					ExpressionSyntax expression2 = memberAccessExpressionSyntax.Expression;
					SyntaxToken operatorToken = memberAccessExpressionSyntax.OperatorToken;
					if (operatorToken != null && operatorToken.Kind == SyntaxKind.DotToken)
					{
						SimpleNameSyntax name = memberAccessExpressionSyntax.Name;
						ExpressionSyntax expressionSyntax = expression2;
						SyntaxToken syntaxToken = operatorToken;
						SimpleNameSyntax simpleNameSyntax2 = name;
						NameSyntax nameSyntax;
						if (this.ConvertExpressionToType(expressionSyntax, out nameSyntax))
						{
							type = this._syntaxFactory.QualifiedName(nameSyntax, syntaxToken, simpleNameSyntax2);
							return true;
						}
					}
				}
				else
				{
					AliasQualifiedNameSyntax aliasQualifiedNameSyntax = expression as AliasQualifiedNameSyntax;
					if (aliasQualifiedNameSyntax != null)
					{
						AliasQualifiedNameSyntax aliasQualifiedNameSyntax2 = aliasQualifiedNameSyntax;
						type = aliasQualifiedNameSyntax2;
						return true;
					}
				}
				return false;
			}
			SimpleNameSyntax simpleNameSyntax3 = simpleNameSyntax;
			type = simpleNameSyntax3;
			return true;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x0012EE5D File Offset: 0x0012D05D
		private PatternSyntax ParsePattern(LanguageParser.Precedence precedence, bool afterIs = false, bool whenIsKeyword = false)
		{
			return this.ParseDisjunctivePattern(precedence, afterIs, whenIsKeyword);
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x0012EE68 File Offset: 0x0012D068
		private PatternSyntax ParseDisjunctivePattern(LanguageParser.Precedence precedence, bool afterIs, bool whenIsKeyword)
		{
			PatternSyntax patternSyntax = this.ParseConjunctivePattern(precedence, afterIs, whenIsKeyword);
			while (base.CurrentToken.ContextualKind == SyntaxKind.OrKeyword)
			{
				SyntaxToken syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
				PatternSyntax patternSyntax2 = this.ParseConjunctivePattern(precedence, afterIs, whenIsKeyword);
				patternSyntax = this._syntaxFactory.BinaryPattern(SyntaxKind.OrPattern, patternSyntax, syntaxToken, patternSyntax2);
				patternSyntax = base.CheckFeatureAvailability<PatternSyntax>(patternSyntax, MessageID.IDS_FeatureOrPattern, false);
			}
			return patternSyntax;
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0012EECC File Offset: 0x0012D0CC
		private bool LooksLikeTypeOfPattern()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			return SyntaxFacts.IsPredefinedType(kind) || (kind == SyntaxKind.IdentifierToken && base.CurrentToken.ContextualKind != SyntaxKind.UnderscoreToken && (base.CurrentToken.ContextualKind != SyntaxKind.NameOfKeyword || base.PeekToken(1).Kind != SyntaxKind.OpenParenToken)) || this.LooksLikeTupleArrayType() || this.IsFunctionPointerStart();
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0012EF48 File Offset: 0x0012D148
		private PatternSyntax ParseConjunctivePattern(LanguageParser.Precedence precedence, bool afterIs, bool whenIsKeyword)
		{
			PatternSyntax patternSyntax = this.ParseNegatedPattern(precedence, afterIs, whenIsKeyword);
			while (base.CurrentToken.ContextualKind == SyntaxKind.AndKeyword)
			{
				SyntaxToken syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
				PatternSyntax patternSyntax2 = this.ParseNegatedPattern(precedence, afterIs, whenIsKeyword);
				patternSyntax = this._syntaxFactory.BinaryPattern(SyntaxKind.AndPattern, patternSyntax, syntaxToken, patternSyntax2);
				patternSyntax = base.CheckFeatureAvailability<PatternSyntax>(patternSyntax, MessageID.IDS_FeatureAndPattern, false);
			}
			return patternSyntax;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0012EFAC File Offset: 0x0012D1AC
		private bool ScanDesignation(bool permitTuple)
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind != SyntaxKind.OpenParenToken)
			{
				if (kind != SyntaxKind.IdentifierToken)
				{
					return false;
				}
				bool flag = this.IsTrueIdentifier();
				base.EatToken();
				return flag;
			}
			else
			{
				if (!permitTuple)
				{
					return false;
				}
				bool flag2 = false;
				for (;;)
				{
					base.EatToken();
					if (!this.ScanDesignation(true))
					{
						break;
					}
					SyntaxKind kind2 = base.CurrentToken.Kind;
					if (kind2 == SyntaxKind.CloseParenToken)
					{
						goto IL_0063;
					}
					if (kind2 != SyntaxKind.CommaToken)
					{
						return false;
					}
					flag2 = true;
				}
				return false;
				IL_0063:
				base.EatToken();
				return flag2;
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x0012F02C File Offset: 0x0012D22C
		private PatternSyntax ParseNegatedPattern(LanguageParser.Precedence precedence, bool afterIs, bool whenIsKeyword)
		{
			if (base.CurrentToken.ContextualKind == SyntaxKind.NotKeyword)
			{
				SyntaxToken syntaxToken = SyntaxParser.ConvertToKeyword(base.EatToken());
				PatternSyntax patternSyntax = this.ParseNegatedPattern(precedence, afterIs, whenIsKeyword);
				UnaryPatternSyntax unaryPatternSyntax = this._syntaxFactory.UnaryPattern(syntaxToken, patternSyntax);
				return base.CheckFeatureAvailability<UnaryPatternSyntax>(unaryPatternSyntax, MessageID.IDS_FeatureNotPattern, false);
			}
			return this.ParsePrimaryPattern(precedence, afterIs, whenIsKeyword);
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x0012F088 File Offset: 0x0012D288
		private PatternSyntax ParsePrimaryPattern(LanguageParser.Precedence precedence, bool afterIs, bool whenIsKeyword)
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.CloseBracketToken)
			{
				if (kind != SyntaxKind.CloseParenToken && kind != SyntaxKind.CloseBraceToken && kind != SyntaxKind.CloseBracketToken)
				{
					goto IL_005D;
				}
			}
			else if (kind != SyntaxKind.SemicolonToken && kind != SyntaxKind.CommaToken && kind != SyntaxKind.EqualsGreaterThanToken)
			{
				goto IL_005D;
			}
			return this._syntaxFactory.ConstantPattern(this.ParseIdentifierName(ErrorCode.ERR_MissingPattern));
			IL_005D:
			if (base.CurrentToken.ContextualKind == SyntaxKind.UnderscoreToken)
			{
				return this._syntaxFactory.DiscardPattern(base.EatContextualToken(SyntaxKind.UnderscoreToken, true));
			}
			SyntaxKind kind2 = base.CurrentToken.Kind;
			if (kind2 != SyntaxKind.LessThanToken && kind2 != SyntaxKind.GreaterThanToken)
			{
				switch (kind2)
				{
				case SyntaxKind.ExclamationEqualsToken:
				case SyntaxKind.EqualsEqualsToken:
				case SyntaxKind.LessThanEqualsToken:
				case SyntaxKind.GreaterThanEqualsToken:
					goto IL_00D1;
				}
				LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
				PatternSyntax patternSyntax2;
				try
				{
					TypeSyntax typeSyntax = null;
					if (this.LooksLikeTypeOfPattern())
					{
						typeSyntax = this.ParseType(afterIs ? LanguageParser.ParseTypeMode.AfterIs : LanguageParser.ParseTypeMode.DefinitePattern);
						if (typeSyntax.IsMissing || !this.CanTokenFollowTypeInPattern(precedence))
						{
							this.Reset(ref resetPoint);
							typeSyntax = null;
						}
					}
					PatternSyntax patternSyntax = this.ParsePatternContinued(typeSyntax, precedence, whenIsKeyword);
					if (patternSyntax != null)
					{
						patternSyntax2 = patternSyntax;
					}
					else
					{
						this.Reset(ref resetPoint);
						ExpressionSyntax expressionSyntax = this.ParseSubExpression(precedence);
						patternSyntax2 = this._syntaxFactory.ConstantPattern(expressionSyntax);
					}
				}
				finally
				{
					this.Release(ref resetPoint);
				}
				return patternSyntax2;
			}
			IL_00D1:
			SyntaxToken syntaxToken = base.EatToken();
			ExpressionSyntax expressionSyntax2 = this.ParseSubExpression(LanguageParser.Precedence.Relational);
			RelationalPatternSyntax relationalPatternSyntax = this._syntaxFactory.RelationalPattern(syntaxToken, expressionSyntax2);
			return base.CheckFeatureAvailability<RelationalPatternSyntax>(relationalPatternSyntax, MessageID.IDS_FeatureRelationalPattern, false);
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0012F228 File Offset: 0x0012D428
		private bool CanTokenFollowTypeInPattern(LanguageParser.Precedence precedence)
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.SemicolonToken)
			{
				switch (kind)
				{
				case SyntaxKind.OpenParenToken:
				case SyntaxKind.CloseParenToken:
				case SyntaxKind.OpenBraceToken:
				case SyntaxKind.CloseBraceToken:
				case SyntaxKind.CloseBracketToken:
					break;
				case SyntaxKind.MinusToken:
				case SyntaxKind.PlusToken:
				case SyntaxKind.EqualsToken:
				case SyntaxKind.OpenBracketToken:
					goto IL_006A;
				default:
					if (kind != SyntaxKind.SemicolonToken)
					{
						goto IL_006A;
					}
					break;
				}
			}
			else if (kind != SyntaxKind.CommaToken)
			{
				if (kind == SyntaxKind.DotToken)
				{
					return false;
				}
				if (kind != SyntaxKind.IdentifierToken)
				{
					goto IL_006A;
				}
			}
			return true;
			IL_006A:
			return !SyntaxFacts.IsBinaryExpressionOperatorToken(kind) || LanguageParser.GetPrecedence(SyntaxFacts.GetBinaryExpression(kind)) <= precedence;
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0012F2BC File Offset: 0x0012D4BC
		private PatternSyntax ParsePatternContinued(TypeSyntax type, LanguageParser.Precedence precedence, bool whenIsKeyword)
		{
			LanguageParser.<>c__DisplayClass442_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.whenIsKeyword = whenIsKeyword;
			if (type != null && type.Kind == SyntaxKind.IdentifierName)
			{
				SyntaxToken identifier = ((IdentifierNameSyntax)type).Identifier;
				if (identifier.ContextualKind == SyntaxKind.VarKeyword && (base.CurrentToken.Kind == SyntaxKind.OpenParenToken || this.IsValidPatternDesignation(CS$<>8__locals1.whenIsKeyword)))
				{
					SyntaxToken syntaxToken = SyntaxParser.ConvertToKeyword(identifier);
					VariableDesignationSyntax variableDesignationSyntax = this.ParseDesignation(true);
					return this._syntaxFactory.VarPattern(syntaxToken, variableDesignationSyntax);
				}
			}
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken && (type != null || !this.<ParsePatternContinued>g__looksLikeCast|442_2(ref CS$<>8__locals1)))
			{
				SyntaxToken syntaxToken2;
				SeparatedSyntaxList<SubpatternSyntax> separatedSyntaxList;
				SyntaxToken syntaxToken3;
				this.ParseSubpatternList(out syntaxToken2, out separatedSyntaxList, out syntaxToken3, SyntaxKind.OpenParenToken, SyntaxKind.CloseParenToken);
				PropertyPatternClauseSyntax propertyPatternClauseSyntax;
				this.<ParsePatternContinued>g__parsePropertyPatternClause|442_0(out propertyPatternClauseSyntax, ref CS$<>8__locals1);
				VariableDesignationSyntax variableDesignationSyntax2;
				this.<ParsePatternContinued>g__parseDesignation|442_1(out variableDesignationSyntax2, ref CS$<>8__locals1);
				if (type != null || propertyPatternClauseSyntax != null || variableDesignationSyntax2 != null || separatedSyntaxList.Count != 1 || separatedSyntaxList.SeparatorCount != 0 || separatedSyntaxList[0].NameColon != null)
				{
					PositionalPatternClauseSyntax positionalPatternClauseSyntax = this._syntaxFactory.PositionalPatternClause(syntaxToken2, separatedSyntaxList, syntaxToken3);
					return this._syntaxFactory.RecursivePattern(type, positionalPatternClauseSyntax, propertyPatternClauseSyntax, variableDesignationSyntax2);
				}
				ConstantPatternSyntax constantPatternSyntax = separatedSyntaxList[0].Pattern as ConstantPatternSyntax;
				if (constantPatternSyntax != null)
				{
					ExpressionSyntax expressionSyntax = this._syntaxFactory.ParenthesizedExpression(syntaxToken2, constantPatternSyntax.Expression, syntaxToken3);
					expressionSyntax = this.ParseExpressionContinued(expressionSyntax, precedence);
					return this._syntaxFactory.ConstantPattern(expressionSyntax);
				}
				ParenthesizedPatternSyntax parenthesizedPatternSyntax = this._syntaxFactory.ParenthesizedPattern(syntaxToken2, separatedSyntaxList[0].Pattern, syntaxToken3);
				return base.CheckFeatureAvailability<ParenthesizedPatternSyntax>(parenthesizedPatternSyntax, MessageID.IDS_FeatureParenthesizedPattern, false);
			}
			else
			{
				PropertyPatternClauseSyntax propertyPatternClauseSyntax2;
				if (this.<ParsePatternContinued>g__parsePropertyPatternClause|442_0(out propertyPatternClauseSyntax2, ref CS$<>8__locals1))
				{
					VariableDesignationSyntax variableDesignationSyntax3;
					this.<ParsePatternContinued>g__parseDesignation|442_1(out variableDesignationSyntax3, ref CS$<>8__locals1);
					return this._syntaxFactory.RecursivePattern(type, null, propertyPatternClauseSyntax2, variableDesignationSyntax3);
				}
				if (type == null)
				{
					return null;
				}
				VariableDesignationSyntax variableDesignationSyntax4;
				if (this.<ParsePatternContinued>g__parseDesignation|442_1(out variableDesignationSyntax4, ref CS$<>8__locals1))
				{
					return this._syntaxFactory.DeclarationPattern(type, variableDesignationSyntax4);
				}
				ExpressionSyntax expressionSyntax2;
				if (this.ConvertTypeToExpression(type, out expressionSyntax2, false))
				{
					expressionSyntax2 = this.ParseExpressionContinued(expressionSyntax2, precedence);
					return this._syntaxFactory.ConstantPattern(expressionSyntax2);
				}
				TypePatternSyntax typePatternSyntax = this._syntaxFactory.TypePattern(type);
				return base.CheckFeatureAvailability<TypePatternSyntax>(typePatternSyntax, MessageID.IDS_FeatureTypePattern, false);
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0012F4F4 File Offset: 0x0012D6F4
		private bool IsValidPatternDesignation(bool whenIsKeyword)
		{
			if (base.CurrentToken.Kind != SyntaxKind.IdentifierToken)
			{
				return false;
			}
			SyntaxKind contextualKind = base.CurrentToken.ContextualKind;
			if (contextualKind == SyntaxKind.WhenKeyword)
			{
				return !whenIsKeyword;
			}
			if (contextualKind - SyntaxKind.OrKeyword > 1)
			{
				if (contextualKind != SyntaxKind.UnderscoreToken)
				{
				}
				return true;
			}
			SyntaxKind kind = base.PeekToken(1).Kind;
			if (kind <= SyntaxKind.LessThanEqualsToken)
			{
				switch (kind)
				{
				case SyntaxKind.OpenParenToken:
				case SyntaxKind.OpenBraceToken:
				case SyntaxKind.LessThanToken:
				case SyntaxKind.GreaterThanToken:
					break;
				case SyntaxKind.CloseParenToken:
				case SyntaxKind.CloseBraceToken:
				case SyntaxKind.CloseBracketToken:
				case SyntaxKind.ColonToken:
				case SyntaxKind.SemicolonToken:
				case SyntaxKind.CommaToken:
				case SyntaxKind.QuestionToken:
					return true;
				case SyntaxKind.MinusToken:
				case SyntaxKind.PlusToken:
				case SyntaxKind.EqualsToken:
				case SyntaxKind.OpenBracketToken:
				case SyntaxKind.BarToken:
				case SyntaxKind.BackslashToken:
				case SyntaxKind.DoubleQuoteToken:
				case SyntaxKind.SingleQuoteToken:
				case SyntaxKind.DotToken:
					goto IL_00D9;
				default:
					if (kind != SyntaxKind.LessThanEqualsToken)
					{
						goto IL_00D9;
					}
					break;
				}
			}
			else if (kind != SyntaxKind.GreaterThanEqualsToken && kind != SyntaxKind.IdentifierToken)
			{
				goto IL_00D9;
			}
			return false;
			IL_00D9:
			if (SyntaxFacts.IsBinaryExpression(kind))
			{
				return true;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			base.EatToken();
			bool flag = !this.CanStartExpression();
			this.Reset(ref resetPoint);
			this.Release(ref resetPoint);
			return flag;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0012F60F File Offset: 0x0012D80F
		private CSharpSyntaxNode ParseExpressionOrPatternForSwitchStatement()
		{
			return this.CheckRecursivePatternFeature(this.ParseExpressionOrPatternForSwitchStatementCore());
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x0012F620 File Offset: 0x0012D820
		private CSharpSyntaxNode ParseExpressionOrPatternForSwitchStatementCore()
		{
			PatternSyntax patternSyntax = this.ParsePattern(LanguageParser.Precedence.Conditional, false, true);
			ConstantPatternSyntax constantPatternSyntax = patternSyntax as ConstantPatternSyntax;
			CSharpSyntaxNode csharpSyntaxNode;
			if (constantPatternSyntax == null)
			{
				TypePatternSyntax typePatternSyntax = patternSyntax as TypePatternSyntax;
				if (typePatternSyntax == null)
				{
					DiscardPatternSyntax discardPatternSyntax = patternSyntax as DiscardPatternSyntax;
					if (discardPatternSyntax != null)
					{
						DiscardPatternSyntax discardPatternSyntax2 = discardPatternSyntax;
						return this._syntaxFactory.IdentifierName(SyntaxParser.ConvertToIdentifier(discardPatternSyntax2.UnderscoreToken));
					}
				}
				else
				{
					TypePatternSyntax typePatternSyntax2 = typePatternSyntax;
					ExpressionSyntax expressionSyntax;
					if (this.ConvertTypeToExpression(typePatternSyntax2.Type, out expressionSyntax, false))
					{
						return expressionSyntax;
					}
				}
				csharpSyntaxNode = patternSyntax;
			}
			else
			{
				csharpSyntaxNode = constantPatternSyntax.Expression;
			}
			return csharpSyntaxNode;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x0012F6A4 File Offset: 0x0012D8A4
		private bool ConvertTypeToExpression(TypeSyntax type, out ExpressionSyntax expr, bool permitTypeArguments = false)
		{
			expr = null;
			GenericNameSyntax genericNameSyntax = type as GenericNameSyntax;
			if (genericNameSyntax != null)
			{
				expr = genericNameSyntax;
				return permitTypeArguments;
			}
			SimpleNameSyntax simpleNameSyntax = type as SimpleNameSyntax;
			if (simpleNameSyntax == null)
			{
				QualifiedNameSyntax qualifiedNameSyntax = type as QualifiedNameSyntax;
				if (qualifiedNameSyntax != null)
				{
					NameSyntax left = qualifiedNameSyntax.Left;
					SyntaxToken dotToken = qualifiedNameSyntax.dotToken;
					SimpleNameSyntax right = qualifiedNameSyntax.Right;
					if (permitTypeArguments || !(right is GenericNameSyntax))
					{
						ExpressionSyntax expressionSyntax2;
						ExpressionSyntax expressionSyntax = (this.ConvertTypeToExpression(left, out expressionSyntax2, true) ? expressionSyntax2 : left);
						expr = this._syntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expressionSyntax, dotToken, right);
						return true;
					}
				}
				return false;
			}
			expr = simpleNameSyntax;
			return true;
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x0012F73C File Offset: 0x0012D93C
		private bool LooksLikeTupleArrayType()
		{
			if (base.CurrentToken.Kind != SyntaxKind.OpenParenToken)
			{
				return false;
			}
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag;
			try
			{
				flag = this.ScanType(true) > LanguageParser.ScanTypeFlags.NotType;
			}
			finally
			{
				this.Reset(ref resetPoint);
				this.Release(ref resetPoint);
			}
			return flag;
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x0012F794 File Offset: 0x0012D994
		private PropertyPatternClauseSyntax ParsePropertyPatternClause()
		{
			SyntaxToken syntaxToken;
			SeparatedSyntaxList<SubpatternSyntax> separatedSyntaxList;
			SyntaxToken syntaxToken2;
			this.ParseSubpatternList(out syntaxToken, out separatedSyntaxList, out syntaxToken2, SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken);
			return this._syntaxFactory.PropertyPatternClause(syntaxToken, separatedSyntaxList, syntaxToken2);
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x0012F7C8 File Offset: 0x0012D9C8
		private void ParseSubpatternList(out SyntaxToken openToken, out SeparatedSyntaxList<SubpatternSyntax> subPatterns, out SyntaxToken closeToken, SyntaxKind openKind, SyntaxKind closeKind)
		{
			openToken = base.EatToken(openKind);
			SeparatedSyntaxListBuilder<SubpatternSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<SubpatternSyntax>();
			try
			{
				while (!this.IsPossibleSubpatternElement() && base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					if (this.SkipBadPatternListTokens(ref openToken, separatedSyntaxListBuilder, SyntaxKind.IdentifierToken, closeKind) != LanguageParser.PostSkipAction.Continue)
					{
						IL_010B:
						closeToken = base.EatToken(closeKind);
						subPatterns = separatedSyntaxListBuilder.ToList();
						return;
					}
				}
				separatedSyntaxListBuilder.Add(this.ParseSubpatternElement());
				int num = -1;
				while (base.IsMakingProgress(ref num, true))
				{
					if (base.CurrentToken.Kind == SyntaxKind.CloseParenToken || base.CurrentToken.Kind == SyntaxKind.CloseBraceToken || base.CurrentToken.Kind == SyntaxKind.SemicolonToken)
					{
						break;
					}
					if (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleSubpatternElement())
					{
						separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						if (base.CurrentToken.Kind == SyntaxKind.CloseBraceToken)
						{
							break;
						}
						separatedSyntaxListBuilder.Add(this.ParseSubpatternElement());
					}
					else if (this.SkipBadPatternListTokens(ref openToken, separatedSyntaxListBuilder, SyntaxKind.CommaToken, closeKind) == LanguageParser.PostSkipAction.Abort)
					{
						break;
					}
				}
				goto IL_010B;
			}
			finally
			{
				this._pool.Free<SubpatternSyntax>(in separatedSyntaxListBuilder);
			}
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x0012F924 File Offset: 0x0012DB24
		private SubpatternSyntax ParseSubpatternElement()
		{
			NameColonSyntax nameColonSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken && base.PeekToken(1).Kind == SyntaxKind.ColonToken)
			{
				IdentifierNameSyntax identifierNameSyntax = this.ParseIdentifierName(ErrorCode.ERR_IdentifierExpected);
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.ColonToken);
				nameColonSyntax = this._syntaxFactory.NameColon(identifierNameSyntax, syntaxToken);
			}
			PatternSyntax patternSyntax = this.ParsePattern(LanguageParser.Precedence.Conditional, false, false);
			return this._syntaxFactory.Subpattern(nameColonSyntax, patternSyntax);
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x0012F998 File Offset: 0x0012DB98
		private bool IsPossibleSubpatternElement()
		{
			bool flag = this.IsPossibleExpression(false, false, false);
			if (!flag)
			{
				SyntaxKind kind = base.CurrentToken.Kind;
				bool flag2;
				if (kind <= SyntaxKind.LessThanToken)
				{
					if (kind == SyntaxKind.OpenBraceToken)
					{
						flag2 = true;
						goto IL_0063;
					}
					if (kind == SyntaxKind.LessThanToken)
					{
						flag2 = true;
						goto IL_0063;
					}
				}
				else
				{
					if (kind == SyntaxKind.GreaterThanToken)
					{
						flag2 = true;
						goto IL_0063;
					}
					if (kind == SyntaxKind.LessThanEqualsToken)
					{
						flag2 = true;
						goto IL_0063;
					}
					if (kind == SyntaxKind.GreaterThanEqualsToken)
					{
						flag2 = true;
						goto IL_0063;
					}
				}
				flag2 = false;
				IL_0063:
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x0012FA0C File Offset: 0x0012DC0C
		private LanguageParser.PostSkipAction SkipBadPatternListTokens(ref SyntaxToken open, SeparatedSyntaxListBuilder<SubpatternSyntax> list, SyntaxKind expected, SyntaxKind closeKind)
		{
			return this.SkipBadSeparatedListTokensWithExpectedKind<SyntaxToken, SubpatternSyntax>(ref open, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken && !p.IsPossibleSubpatternElement(), (LanguageParser p) => p.CurrentToken.Kind == closeKind || p.CurrentToken.Kind == SyntaxKind.SemicolonToken || p.IsTerminator(), expected);
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x0012FA5C File Offset: 0x0012DC5C
		private ExpressionSyntax ParseSwitchExpression(ExpressionSyntax governingExpression, SyntaxToken switchKeyword)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.OpenBraceToken);
			SeparatedSyntaxList<SwitchExpressionArmSyntax> separatedSyntaxList = this.ParseSwitchExpressionArms();
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBraceToken);
			SwitchExpressionSyntax switchExpressionSyntax = this._syntaxFactory.SwitchExpression(governingExpression, switchKeyword, syntaxToken, separatedSyntaxList, syntaxToken2);
			return base.CheckFeatureAvailability<SwitchExpressionSyntax>(switchExpressionSyntax, MessageID.IDS_FeatureRecursivePatterns, false);
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x0012FAA8 File Offset: 0x0012DCA8
		private SeparatedSyntaxList<SwitchExpressionArmSyntax> ParseSwitchExpressionArms()
		{
			SeparatedSyntaxListBuilder<SwitchExpressionArmSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<SwitchExpressionArmSyntax>();
			while (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
			{
				PatternSyntax patternSyntax = this.ParsePattern(LanguageParser.Precedence.Coalescing, false, true);
				WhenClauseSyntax whenClauseSyntax = this.ParseWhenClause(LanguageParser.Precedence.Coalescing);
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EqualsGreaterThanToken);
				ExpressionSyntax expressionSyntax = this.ParseExpressionCore();
				SwitchExpressionArmSyntax switchExpressionArmSyntax = this._syntaxFactory.SwitchExpressionArm(patternSyntax, whenClauseSyntax, syntaxToken, expressionSyntax);
				if (switchExpressionArmSyntax.Width == 0 && base.CurrentToken.Kind != SyntaxKind.CommaToken)
				{
					break;
				}
				separatedSyntaxListBuilder.Add(switchExpressionArmSyntax);
				if (base.CurrentToken.Kind != SyntaxKind.CloseBraceToken)
				{
					SyntaxToken syntaxToken2 = ((base.CurrentToken.Kind == SyntaxKind.SemicolonToken) ? base.EatTokenAsKind(SyntaxKind.CommaToken) : base.EatToken(SyntaxKind.CommaToken));
					separatedSyntaxListBuilder.AddSeparator(syntaxToken2);
				}
			}
			SeparatedSyntaxList<SwitchExpressionArmSyntax> separatedSyntaxList = in separatedSyntaxListBuilder;
			this._pool.Free<SwitchExpressionArmSyntax>(in separatedSyntaxListBuilder);
			return separatedSyntaxList;
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x0012FB94 File Offset: 0x0012DD94
		[CompilerGenerated]
		private MemberDeclarationSyntax <ParseNamespaceBody>g__adjustStateAndReportStatementOutOfOrder|23_0(ref LanguageParser.NamespaceParts seen, MemberDeclarationSyntax memberOrStatement)
		{
			SyntaxKind kind = memberOrStatement.Kind;
			if (kind <= SyntaxKind.NamespaceDeclaration)
			{
				if (kind != SyntaxKind.GlobalStatement)
				{
					if (kind != SyntaxKind.NamespaceDeclaration)
					{
						goto IL_0071;
					}
				}
				else
				{
					if (seen < LanguageParser.NamespaceParts.MembersAndStatements)
					{
						seen = LanguageParser.NamespaceParts.MembersAndStatements;
						return memberOrStatement;
					}
					if (seen != LanguageParser.NamespaceParts.TypesAndNamespaces)
					{
						return memberOrStatement;
					}
					seen = LanguageParser.NamespaceParts.TopLevelStatementsAfterTypesAndNamespaces;
					if (!base.IsScript)
					{
						return base.AddError<MemberDeclarationSyntax>(memberOrStatement, ErrorCode.ERR_TopLevelStatementAfterNamespaceOrType);
					}
					return memberOrStatement;
				}
			}
			else if (kind - SyntaxKind.ClassDeclaration > 4 && kind != SyntaxKind.RecordDeclaration && kind != SyntaxKind.RecordStructDeclaration)
			{
				goto IL_0071;
			}
			if (seen < LanguageParser.NamespaceParts.TypesAndNamespaces)
			{
				seen = LanguageParser.NamespaceParts.TypesAndNamespaces;
				return memberOrStatement;
			}
			return memberOrStatement;
			IL_0071:
			if (seen < LanguageParser.NamespaceParts.MembersAndStatements)
			{
				seen = LanguageParser.NamespaceParts.MembersAndStatements;
			}
			return memberOrStatement;
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x0012FC1C File Offset: 0x0012DE1C
		[CompilerGenerated]
		private void <ParseNamespaceBody>g__parseUsingDirective|23_1(ref SyntaxToken openBrace, ref LanguageParser.NamespaceBodyBuilder body, ref SyntaxListBuilder initialBadNodes, ref LanguageParser.NamespaceParts seen, ref SyntaxListBuilder<MemberDeclarationSyntax> pendingIncompleteMembers)
		{
			this.ReduceIncompleteMembers(ref pendingIncompleteMembers, ref openBrace, ref body, ref initialBadNodes);
			UsingDirectiveSyntax usingDirectiveSyntax = this.ParseUsingDirective();
			if (seen > LanguageParser.NamespaceParts.Usings)
			{
				usingDirectiveSyntax = base.AddError<UsingDirectiveSyntax>(usingDirectiveSyntax, ErrorCode.ERR_UsingAfterElements);
				this.AddSkippedNamespaceText(ref openBrace, ref body, ref initialBadNodes, usingDirectiveSyntax);
				return;
			}
			body.Usings.Add(usingDirectiveSyntax);
			seen = LanguageParser.NamespaceParts.Usings;
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x0012FC6A File Offset: 0x0012DE6A
		[CompilerGenerated]
		private bool <ParseModifiers>g__isStructOrRecordKeyword|52_0(SyntaxToken token)
		{
			return token.Kind == SyntaxKind.StructKeyword || (token.ContextualKind == SyntaxKind.RecordKeyword && base.IsFeatureEnabled(MessageID.IDS_FeatureRecords));
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x0012FC98 File Offset: 0x0012DE98
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private SyntaxToken <ParseClassOrStructOrInterfaceDeclaration>g__eatRecordModifierIfAvailable|60_0()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind == SyntaxKind.ClassKeyword || kind == SyntaxKind.StructKeyword)
			{
				SyntaxToken syntaxToken = base.EatToken();
				return base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureRecordStructs, false);
			}
			return null;
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x0012FCDC File Offset: 0x0012DEDC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static TypeDeclarationSyntax <ParseClassOrStructOrInterfaceDeclaration>g__constructTypeDeclaration|60_1(ContextAwareSyntax syntaxFactory, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributes, SyntaxListBuilder modifiers, SyntaxToken keyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken recordModifier, SyntaxToken name, TypeParameterListSyntax typeParameters, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax paramList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxListBuilder<TypeParameterConstraintClauseSyntax> constraints, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken openBrace, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxListBuilder<MemberDeclarationSyntax> members, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken closeBrace, SyntaxToken semicolon)
		{
			SyntaxList<SyntaxToken> syntaxList = modifiers.ToList();
			SyntaxList<MemberDeclarationSyntax> syntaxList2 = members;
			SyntaxList<TypeParameterConstraintClauseSyntax> syntaxList3 = constraints;
			SyntaxKind kind = keyword.Kind;
			switch (kind)
			{
			case SyntaxKind.ClassKeyword:
				return syntaxFactory.ClassDeclaration(attributes, syntaxList, keyword, name, typeParameters, baseList, syntaxList3, openBrace, syntaxList2, closeBrace, semicolon);
			case SyntaxKind.StructKeyword:
				return syntaxFactory.StructDeclaration(attributes, syntaxList, keyword, name, typeParameters, baseList, syntaxList3, openBrace, syntaxList2, closeBrace, semicolon);
			case SyntaxKind.InterfaceKeyword:
				return syntaxFactory.InterfaceDeclaration(attributes, syntaxList, keyword, name, typeParameters, baseList, syntaxList3, openBrace, syntaxList2, closeBrace, semicolon);
			default:
			{
				if (kind != SyntaxKind.RecordKeyword)
				{
					throw ExceptionUtilities.UnexpectedValue(keyword.Kind);
				}
				SyntaxKind syntaxKind = ((recordModifier != null && recordModifier.Kind == SyntaxKind.StructKeyword) ? SyntaxKind.RecordStructDeclaration : SyntaxKind.RecordDeclaration);
				return syntaxFactory.RecordDeclaration(syntaxKind, attributes, modifiers.ToList(), keyword, recordModifier, name, typeParameters, paramList, baseList, constraints, openBrace, members, closeBrace, semicolon);
			}
			}
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x0012FDF4 File Offset: 0x0012DFF4
		[CompilerGenerated]
		private MemberDeclarationSyntax <ParseMemberDeclaration>g__createEmptyNodeFunc|77_2()
		{
			return this._syntaxFactory.IncompleteMember(default(SyntaxList<AttributeListSyntax>), default(SyntaxList<SyntaxToken>), this.CreateMissingIdentifierName());
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x0012FE24 File Offset: 0x0012E024
		[CompilerGenerated]
		private bool <ParseMemberDeclarationOrStatementCore>g__tryParseLocalDeclarationStatement|79_0<DeclarationSyntax>(SyntaxList<AttributeListSyntax> attributes, out MemberDeclarationSyntax result) where DeclarationSyntax : StatementSyntax
		{
			bool isInAsync = this.IsInAsync;
			this.IsInAsync = true;
			int num = -1;
			base.IsMakingProgress(ref num, true);
			StatementSyntax statementSyntax = this.ParseLocalDeclarationStatement(attributes);
			this.IsInAsync = isInAsync;
			DeclarationSyntax declarationSyntax = statementSyntax as DeclarationSyntax;
			if (declarationSyntax != null && base.IsMakingProgress(ref num, false))
			{
				result = this.CheckTopLevelStatementsFeatureAvailability(this._syntaxFactory.GlobalStatement(declarationSyntax));
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0012FE98 File Offset: 0x0012E098
		[CompilerGenerated]
		private bool <ParseMemberDeclarationOrStatementCore>g__tryParseLocalDeclarationStatementFromStartPoint|79_1<DeclarationSyntax>(SyntaxList<AttributeListSyntax> attributes, ref LanguageParser.ResetPoint startPoint, out MemberDeclarationSyntax result) where DeclarationSyntax : StatementSyntax
		{
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag;
			try
			{
				this.Reset(ref startPoint);
				if (this.<ParseMemberDeclarationOrStatementCore>g__tryParseLocalDeclarationStatement|79_0<DeclarationSyntax>(attributes, out result))
				{
					flag = true;
				}
				else
				{
					this.Reset(ref resetPoint);
					flag = false;
				}
			}
			finally
			{
				this.Release(ref resetPoint);
			}
			return flag;
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0012FEE8 File Offset: 0x0012E0E8
		[CompilerGenerated]
		internal static bool <ParseMemberDeclarationOrStatementCore>g__isAcceptableNonDeclarationStatement|79_2(StatementSyntax statement, bool isScript)
		{
			SyntaxKind? syntaxKind = ((statement != null) ? new SyntaxKind?(statement.Kind) : default(SyntaxKind?));
			if (syntaxKind != null)
			{
				SyntaxKind valueOrDefault = syntaxKind.GetValueOrDefault();
				if (valueOrDefault != SyntaxKind.LocalDeclarationStatement)
				{
					if (valueOrDefault != SyntaxKind.ExpressionStatement)
					{
						if (valueOrDefault == SyntaxKind.LocalFunctionStatement)
						{
							return false;
						}
					}
					else if (!isScript)
					{
						ExpressionStatementSyntax expressionStatementSyntax = (ExpressionStatementSyntax)statement;
						if (expressionStatementSyntax.Expression.Kind == SyntaxKind.IdentifierName && expressionStatementSyntax.SemicolonToken.IsMissing)
						{
							return false;
						}
					}
					return true;
				}
				return !isScript && ((LocalDeclarationStatementSyntax)statement).UsingKeyword != null;
			}
			return false;
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x0012FF7C File Offset: 0x0012E17C
		[CompilerGenerated]
		private LanguageParser.PostSkipAction <ScanFunctionPointerType>g__skipBadFunctionPointerTokens|199_0()
		{
			GreenNode greenNode;
			return this.SkipBadTokensWithExpectedKind((LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken, (LanguageParser p) => p.IsTerminator(), SyntaxKind.CommaToken, out greenNode);
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x0012FFD4 File Offset: 0x0012E1D4
		[CompilerGenerated]
		private bool <ParseTypeCore>g__canBeNullableType|205_0(ref LanguageParser.<>c__DisplayClass205_0 A_1)
		{
			return A_1.type.Kind != SyntaxKind.NullableType && A_1.type.Kind != SyntaxKind.PointerType && A_1.type.Kind != SyntaxKind.FunctionPointerType && (base.PeekToken(1).Kind == SyntaxKind.OpenBracketToken || A_1.mode == LanguageParser.ParseTypeMode.DefinitePattern || A_1.mode != LanguageParser.ParseTypeMode.NewExpression || A_1.type.Kind != SyntaxKind.TupleType || base.PeekToken(1).Kind == SyntaxKind.OpenParenToken || base.PeekToken(1).Kind == SyntaxKind.OpenBraceToken);
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00130080 File Offset: 0x0012E280
		[CompilerGenerated]
		private bool <EatNullableQualifierIfApplicable>g__canFollowNullableType|206_0(ref LanguageParser.<>c__DisplayClass206_0 A_1)
		{
			switch (A_1.mode)
			{
			case LanguageParser.ParseTypeMode.AfterIs:
			case LanguageParser.ParseTypeMode.DefinitePattern:
			case LanguageParser.ParseTypeMode.AsExpression:
				return !this.CanStartExpression();
			case LanguageParser.ParseTypeMode.NewExpression:
				return base.CurrentToken.Kind == SyntaxKind.OpenParenToken || base.CurrentToken.Kind == SyntaxKind.OpenBracketToken || base.CurrentToken.Kind == SyntaxKind.OpenBraceToken;
			}
			return true;
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x00130100 File Offset: 0x0012E300
		[CompilerGenerated]
		private LanguageParser.PostSkipAction <ParseFunctionPointerTypeSyntax>g__skipBadFunctionPointerTokens|213_0<T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxListBuilder<T> list) where T : CSharpSyntaxNode
		{
			CSharpSyntaxNode csharpSyntaxNode = null;
			return this.SkipBadSeparatedListTokensWithExpectedKind<CSharpSyntaxNode, T>(ref csharpSyntaxNode, list, (LanguageParser p) => p.CurrentToken.Kind != SyntaxKind.CommaToken, (LanguageParser p) => p.IsTerminator(), SyntaxKind.CommaToken);
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00130140 File Offset: 0x0012E340
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private FunctionPointerCallingConventionSyntax <ParseFunctionPointerTypeSyntax>g__parseCallingConvention|213_1()
		{
			if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken)
			{
				SyntaxToken syntaxToken = base.PeekToken(1);
				SyntaxToken currentToken = base.CurrentToken;
				SyntaxToken syntaxToken2;
				if (currentToken != null)
				{
					SyntaxKind contextualKind = currentToken.ContextualKind;
					if (contextualKind - SyntaxKind.ManagedKeyword <= 1)
					{
						syntaxToken2 = base.EatContextualToken(base.CurrentToken.ContextualKind, true);
						goto IL_0082;
					}
				}
				if (LanguageParser.IsPossibleFunctionPointerParameterListStart(syntaxToken))
				{
					syntaxToken2 = base.EatTokenAsKind(SyntaxKind.ManagedKeyword);
				}
				else
				{
					if (syntaxToken.Kind != SyntaxKind.OpenBracketToken)
					{
						return null;
					}
					syntaxToken2 = base.EatTokenAsKind(SyntaxKind.UnmanagedKeyword);
				}
				IL_0082:
				FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax = null;
				if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
				{
					SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.OpenBracketToken);
					SeparatedSyntaxListBuilder<FunctionPointerUnmanagedCallingConventionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<FunctionPointerUnmanagedCallingConventionSyntax>();
					LanguageParser.TerminatorState termState = this._termState;
					this._termState |= LanguageParser.TerminatorState.IsEndOfFunctionPointerCallingConvention;
					try
					{
						for (;;)
						{
							separatedSyntaxListBuilder.Add(SyntaxFactory.FunctionPointerUnmanagedCallingConvention(base.EatToken(SyntaxKind.IdentifierToken)));
							if (this.<ParseFunctionPointerTypeSyntax>g__skipBadFunctionPointerTokens|213_0<FunctionPointerUnmanagedCallingConventionSyntax>(separatedSyntaxListBuilder) == LanguageParser.PostSkipAction.Abort)
							{
								break;
							}
							separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
						}
						SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.CloseBracketToken);
						functionPointerUnmanagedCallingConventionListSyntax = SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(syntaxToken3, in separatedSyntaxListBuilder, syntaxToken4);
					}
					finally
					{
						this._termState = termState;
						this._pool.Free<FunctionPointerUnmanagedCallingConventionSyntax>(in separatedSyntaxListBuilder);
					}
				}
				if (syntaxToken2.Kind == SyntaxKind.ManagedKeyword && functionPointerUnmanagedCallingConventionListSyntax != null)
				{
					functionPointerUnmanagedCallingConventionListSyntax = base.AddError<FunctionPointerUnmanagedCallingConventionListSyntax>(functionPointerUnmanagedCallingConventionListSyntax, ErrorCode.ERR_CannotSpecifyManagedWithUnmanagedSpecifiers);
				}
				return SyntaxFactory.FunctionPointerCallingConvention(syntaxToken2, functionPointerUnmanagedCallingConventionListSyntax);
			}
			return null;
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x001302E6 File Offset: 0x0012E4E6
		[CompilerGenerated]
		private bool <ParseStatementCore>g__canReuseStatement|219_0(SyntaxList<AttributeListSyntax> attributes, bool isGlobal)
		{
			return this.IsIncrementalAndFactoryContextMatches && base.CurrentNode is StatementSyntax && !isGlobal && attributes.Count == 0;
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x0013030C File Offset: 0x0012E50C
		[CompilerGenerated]
		private StatementSyntax <ParseEmbeddedStatement>g__parseEmbeddedStatementRest|247_0(StatementSyntax statement)
		{
			if (statement == null)
			{
				return SyntaxFactory.EmptyStatement(default(SyntaxList<AttributeListSyntax>), base.EatToken(SyntaxKind.SemicolonToken));
			}
			if (statement.Kind == SyntaxKind.ExpressionStatement && base.IsScript)
			{
				ExpressionStatementSyntax expressionStatementSyntax = (ExpressionStatementSyntax)statement;
				SyntaxToken syntaxToken = expressionStatementSyntax.SemicolonToken;
				if (syntaxToken.IsMissing)
				{
					if (!syntaxToken.GetDiagnostics().Contains<DiagnosticInfo>((DiagnosticInfo diagnosticInfo) => diagnosticInfo.Code == 1002))
					{
						syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_SemicolonExpected);
						return expressionStatementSyntax.Update(expressionStatementSyntax.AttributeLists, expressionStatementSyntax.Expression, syntaxToken);
					}
				}
			}
			return statement;
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x001303B0 File Offset: 0x0012E5B0
		[CompilerGenerated]
		private bool <ParseConsequenceSyntax>g__isOptionalExclamationsFollowedByConditionalOperation|328_0()
		{
			SyntaxKind syntaxKind = base.CurrentToken.Kind;
			int num = 1;
			while (syntaxKind == SyntaxKind.ExclamationToken)
			{
				syntaxKind = base.PeekToken(num).Kind;
				num++;
			}
			return syntaxKind == SyntaxKind.OpenParenToken || syntaxKind == SyntaxKind.OpenBracketToken || syntaxKind == SyntaxKind.DotToken || syntaxKind == SyntaxKind.QuestionToken;
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x00130408 File Offset: 0x0012E608
		[CompilerGenerated]
		private bool <ScanCast>g__isBinaryPattern|349_0()
		{
			if (!this.<ScanCast>g__isBinaryPatternKeyword|349_1())
			{
				return false;
			}
			bool flag = true;
			base.EatToken();
			while (this.<ScanCast>g__isBinaryPatternKeyword|349_1())
			{
				flag = !flag;
				base.EatToken();
			}
			return flag == this.IsPossibleSubpatternElement();
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x00130448 File Offset: 0x0012E648
		[CompilerGenerated]
		private bool <ScanCast>g__isBinaryPatternKeyword|349_1()
		{
			SyntaxKind contextualKind = base.CurrentToken.ContextualKind;
			return contextualKind == SyntaxKind.OrKeyword || contextualKind == SyntaxKind.AndKeyword;
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x0013047C File Offset: 0x0012E67C
		[CompilerGenerated]
		private AnonymousMethodExpressionSyntax <ParseAnonymousMethodExpression>g__parseAnonymousMethodExpressionWorker|380_0()
		{
			SyntaxList<SyntaxToken> syntaxList = this.ParseAnonymousFunctionModifiers();
			if (syntaxList.Any(8435))
			{
				this.IsInAsync = true;
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.DelegateKeyword);
			syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureAnonDelegates, false);
			ParameterListSyntax parameterListSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				parameterListSyntax = this.ParseParenthesizedParameterList();
			}
			if (base.CurrentToken.Kind != SyntaxKind.OpenBraceToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OpenBraceToken);
				return this._syntaxFactory.AnonymousMethodExpression(syntaxList, syntaxToken, parameterListSyntax, this._syntaxFactory.Block(default(SyntaxList<AttributeListSyntax>), syntaxToken2, default(SyntaxList<StatementSyntax>), SyntaxFactory.MissingToken(SyntaxKind.CloseBraceToken)), null);
			}
			BlockSyntax blockSyntax = this.ParseBlock(default(SyntaxList<AttributeListSyntax>));
			return this._syntaxFactory.AnonymousMethodExpression(syntaxList, syntaxToken, parameterListSyntax, blockSyntax, null);
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x00130558 File Offset: 0x0012E758
		[CompilerGenerated]
		private LambdaExpressionSyntax <ParseLambdaExpression>g__parseLambdaExpressionWorker|382_0(ref LanguageParser.<>c__DisplayClass382_0 A_1)
		{
			SyntaxList<SyntaxToken> syntaxList = this.ParseAnonymousFunctionModifiers();
			if (syntaxList.Any(8435))
			{
				this.IsInAsync = true;
			}
			if (base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				ParameterListSyntax parameterListSyntax = this.ParseLambdaParameterList();
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EqualsGreaterThanToken);
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureLambda, false);
				ValueTuple<BlockSyntax, ExpressionSyntax> valueTuple = this.ParseLambdaBody();
				BlockSyntax item = valueTuple.Item1;
				ExpressionSyntax item2 = valueTuple.Item2;
				return this._syntaxFactory.ParenthesizedLambdaExpression(A_1.attributes, syntaxList, parameterListSyntax, syntaxToken, item, item2);
			}
			SyntaxToken syntaxToken2 = this.ParseIdentifierToken(ErrorCode.ERR_IdentifierExpected);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.EqualsGreaterThanToken);
			syntaxToken3 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken3, MessageID.IDS_FeatureLambda, false);
			ParameterSyntax parameterSyntax = this._syntaxFactory.Parameter(default(SyntaxList<AttributeListSyntax>), default(SyntaxList<SyntaxToken>), null, syntaxToken2, null);
			ValueTuple<BlockSyntax, ExpressionSyntax> valueTuple2 = this.ParseLambdaBody();
			BlockSyntax item3 = valueTuple2.Item1;
			ExpressionSyntax item4 = valueTuple2.Item2;
			return this._syntaxFactory.SimpleLambdaExpression(A_1.attributes, syntaxList, parameterSyntax, syntaxToken3, item3, item4);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x0013065E File Offset: 0x0012E85E
		[CompilerGenerated]
		private bool <ParsePatternContinued>g__parsePropertyPatternClause|442_0(out PropertyPatternClauseSyntax propertyPatternClauseResult, ref LanguageParser.<>c__DisplayClass442_0 A_2)
		{
			propertyPatternClauseResult = null;
			if (base.CurrentToken.Kind == SyntaxKind.OpenBraceToken)
			{
				propertyPatternClauseResult = this.ParsePropertyPatternClause();
				return true;
			}
			return false;
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x00130680 File Offset: 0x0012E880
		[CompilerGenerated]
		private bool <ParsePatternContinued>g__parseDesignation|442_1(out VariableDesignationSyntax designationResult, ref LanguageParser.<>c__DisplayClass442_0 A_2)
		{
			designationResult = null;
			if (this.IsTrueIdentifier() && this.IsValidPatternDesignation(A_2.whenIsKeyword))
			{
				designationResult = this.ParseSimpleDesignation();
				return true;
			}
			return false;
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x001306A8 File Offset: 0x0012E8A8
		[CompilerGenerated]
		private bool <ParsePatternContinued>g__looksLikeCast|442_2(ref LanguageParser.<>c__DisplayClass442_0 A_1)
		{
			LanguageParser.ResetPoint resetPoint = this.GetResetPoint();
			bool flag = this.ScanCast(true);
			this.Reset(ref resetPoint);
			this.Release(ref resetPoint);
			return flag;
		}

		// Token: 0x0400155A RID: 5466
		private readonly SyntaxListPool _pool = new SyntaxListPool();

		// Token: 0x0400155B RID: 5467
		private readonly SyntaxFactoryContext _syntaxFactoryContext;

		// Token: 0x0400155C RID: 5468
		private readonly ContextAwareSyntax _syntaxFactory;

		// Token: 0x0400155D RID: 5469
		private int _recursionDepth;

		// Token: 0x0400155E RID: 5470
		private LanguageParser.TerminatorState _termState;

		// Token: 0x0400155F RID: 5471
		private bool _isInTry;

		// Token: 0x04001560 RID: 5472
		private bool _checkedTopLevelStatementsFeatureAvailability;

		// Token: 0x04001561 RID: 5473
		private const int LastTerminatorState = 67108864;

		// Token: 0x02000794 RID: 1940
		[Flags]
		internal enum TerminatorState
		{
			// Token: 0x040023FD RID: 9213
			EndOfFile = 0,
			// Token: 0x040023FE RID: 9214
			IsNamespaceMemberStartOrStop = 1,
			// Token: 0x040023FF RID: 9215
			IsAttributeDeclarationTerminator = 2,
			// Token: 0x04002400 RID: 9216
			IsPossibleAggregateClauseStartOrStop = 4,
			// Token: 0x04002401 RID: 9217
			IsPossibleMemberStartOrStop = 8,
			// Token: 0x04002402 RID: 9218
			IsEndOfReturnType = 16,
			// Token: 0x04002403 RID: 9219
			IsEndOfParameterList = 32,
			// Token: 0x04002404 RID: 9220
			IsEndOfFieldDeclaration = 64,
			// Token: 0x04002405 RID: 9221
			IsPossibleEndOfVariableDeclaration = 128,
			// Token: 0x04002406 RID: 9222
			IsEndOfTypeArgumentList = 256,
			// Token: 0x04002407 RID: 9223
			IsPossibleStatementStartOrStop = 512,
			// Token: 0x04002408 RID: 9224
			IsEndOfFixedStatement = 1024,
			// Token: 0x04002409 RID: 9225
			IsEndOfTryBlock = 2048,
			// Token: 0x0400240A RID: 9226
			IsEndOfCatchClause = 4096,
			// Token: 0x0400240B RID: 9227
			IsEndOfFilterClause = 8192,
			// Token: 0x0400240C RID: 9228
			IsEndOfCatchBlock = 16384,
			// Token: 0x0400240D RID: 9229
			IsEndOfDoWhileExpression = 32768,
			// Token: 0x0400240E RID: 9230
			IsEndOfForStatementArgument = 65536,
			// Token: 0x0400240F RID: 9231
			IsEndOfDeclarationClause = 131072,
			// Token: 0x04002410 RID: 9232
			IsEndOfArgumentList = 262144,
			// Token: 0x04002411 RID: 9233
			IsSwitchSectionStart = 524288,
			// Token: 0x04002412 RID: 9234
			IsEndOfTypeParameterList = 1048576,
			// Token: 0x04002413 RID: 9235
			IsEndOfMethodSignature = 2097152,
			// Token: 0x04002414 RID: 9236
			IsEndOfNameInExplicitInterface = 4194304,
			// Token: 0x04002415 RID: 9237
			IsEndOfFunctionPointerParameterList = 8388608,
			// Token: 0x04002416 RID: 9238
			IsEndOfFunctionPointerParameterListErrored = 16777216,
			// Token: 0x04002417 RID: 9239
			IsEndOfFunctionPointerCallingConvention = 33554432,
			// Token: 0x04002418 RID: 9240
			IsEndOfRecordSignature = 67108864
		}

		// Token: 0x02000795 RID: 1941
		private struct NamespaceBodyBuilder
		{
			// Token: 0x060088C4 RID: 35012 RVA: 0x001EEAE7 File Offset: 0x001ECCE7
			public NamespaceBodyBuilder(SyntaxListPool pool)
			{
				this.Externs = pool.Allocate<ExternAliasDirectiveSyntax>();
				this.Usings = pool.Allocate<UsingDirectiveSyntax>();
				this.Attributes = pool.Allocate<AttributeListSyntax>();
				this.Members = pool.Allocate<MemberDeclarationSyntax>();
			}

			// Token: 0x060088C5 RID: 35013 RVA: 0x001EEB1C File Offset: 0x001ECD1C
			internal void Free(SyntaxListPool pool)
			{
				pool.Free(this.Members);
				pool.Free(this.Attributes);
				pool.Free(this.Usings);
				pool.Free(this.Externs);
			}

			// Token: 0x04002419 RID: 9241
			public SyntaxListBuilder<ExternAliasDirectiveSyntax> Externs;

			// Token: 0x0400241A RID: 9242
			public SyntaxListBuilder<UsingDirectiveSyntax> Usings;

			// Token: 0x0400241B RID: 9243
			public SyntaxListBuilder<AttributeListSyntax> Attributes;

			// Token: 0x0400241C RID: 9244
			public SyntaxListBuilder<MemberDeclarationSyntax> Members;
		}

		// Token: 0x02000796 RID: 1942
		private enum NamespaceParts
		{
			// Token: 0x0400241E RID: 9246
			None,
			// Token: 0x0400241F RID: 9247
			ExternAliases,
			// Token: 0x04002420 RID: 9248
			Usings,
			// Token: 0x04002421 RID: 9249
			GlobalAttributes,
			// Token: 0x04002422 RID: 9250
			MembersAndStatements,
			// Token: 0x04002423 RID: 9251
			TypesAndNamespaces,
			// Token: 0x04002424 RID: 9252
			TopLevelStatementsAfterTypesAndNamespaces
		}

		// Token: 0x02000797 RID: 1943
		private enum PostSkipAction
		{
			// Token: 0x04002426 RID: 9254
			Continue,
			// Token: 0x04002427 RID: 9255
			Abort
		}

		// Token: 0x02000798 RID: 1944
		[Flags]
		private enum VariableFlags
		{
			// Token: 0x04002429 RID: 9257
			Fixed = 1,
			// Token: 0x0400242A RID: 9258
			Const = 2,
			// Token: 0x0400242B RID: 9259
			Local = 4
		}

		// Token: 0x02000799 RID: 1945
		[Flags]
		private enum NameOptions
		{
			// Token: 0x0400242D RID: 9261
			None = 0,
			// Token: 0x0400242E RID: 9262
			InExpression = 1,
			// Token: 0x0400242F RID: 9263
			InTypeList = 2,
			// Token: 0x04002430 RID: 9264
			PossiblePattern = 4,
			// Token: 0x04002431 RID: 9265
			AfterIs = 8,
			// Token: 0x04002432 RID: 9266
			DefinitePattern = 16,
			// Token: 0x04002433 RID: 9267
			AfterOut = 32,
			// Token: 0x04002434 RID: 9268
			AfterTupleComma = 64,
			// Token: 0x04002435 RID: 9269
			FirstElementOfPossibleTupleLiteral = 128
		}

		// Token: 0x0200079A RID: 1946
		private enum ScanTypeArgumentListKind
		{
			// Token: 0x04002437 RID: 9271
			NotTypeArgumentList,
			// Token: 0x04002438 RID: 9272
			PossibleTypeArgumentList,
			// Token: 0x04002439 RID: 9273
			DefiniteTypeArgumentList
		}

		// Token: 0x0200079B RID: 1947
		private enum ScanTypeFlags
		{
			// Token: 0x0400243B RID: 9275
			NotType,
			// Token: 0x0400243C RID: 9276
			MustBeType,
			// Token: 0x0400243D RID: 9277
			GenericTypeOrMethod,
			// Token: 0x0400243E RID: 9278
			GenericTypeOrExpression,
			// Token: 0x0400243F RID: 9279
			NonGenericTypeOrExpression,
			// Token: 0x04002440 RID: 9280
			AliasQualifiedName,
			// Token: 0x04002441 RID: 9281
			NullableType,
			// Token: 0x04002442 RID: 9282
			PointerOrMultiplication,
			// Token: 0x04002443 RID: 9283
			TupleType
		}

		// Token: 0x0200079C RID: 1948
		private enum ParseTypeMode
		{
			// Token: 0x04002445 RID: 9285
			Normal,
			// Token: 0x04002446 RID: 9286
			Parameter,
			// Token: 0x04002447 RID: 9287
			AfterIs,
			// Token: 0x04002448 RID: 9288
			DefinitePattern,
			// Token: 0x04002449 RID: 9289
			AfterOut,
			// Token: 0x0400244A RID: 9290
			AfterRef,
			// Token: 0x0400244B RID: 9291
			AfterTupleComma,
			// Token: 0x0400244C RID: 9292
			AsExpression,
			// Token: 0x0400244D RID: 9293
			NewExpression,
			// Token: 0x0400244E RID: 9294
			FirstElementOfPossibleTupleLiteral
		}

		// Token: 0x0200079D RID: 1949
		private enum Precedence : uint
		{
			// Token: 0x04002450 RID: 9296
			Expression,
			// Token: 0x04002451 RID: 9297
			Assignment = 0U,
			// Token: 0x04002452 RID: 9298
			Lambda = 0U,
			// Token: 0x04002453 RID: 9299
			Conditional,
			// Token: 0x04002454 RID: 9300
			Coalescing,
			// Token: 0x04002455 RID: 9301
			ConditionalOr,
			// Token: 0x04002456 RID: 9302
			ConditionalAnd,
			// Token: 0x04002457 RID: 9303
			LogicalOr,
			// Token: 0x04002458 RID: 9304
			LogicalXor,
			// Token: 0x04002459 RID: 9305
			LogicalAnd,
			// Token: 0x0400245A RID: 9306
			Equality,
			// Token: 0x0400245B RID: 9307
			Relational,
			// Token: 0x0400245C RID: 9308
			Shift,
			// Token: 0x0400245D RID: 9309
			Additive,
			// Token: 0x0400245E RID: 9310
			Mutiplicative,
			// Token: 0x0400245F RID: 9311
			Switch,
			// Token: 0x04002460 RID: 9312
			Range,
			// Token: 0x04002461 RID: 9313
			Unary,
			// Token: 0x04002462 RID: 9314
			Cast,
			// Token: 0x04002463 RID: 9315
			PointerIndirection,
			// Token: 0x04002464 RID: 9316
			AddressOf,
			// Token: 0x04002465 RID: 9317
			Primary
		}

		// Token: 0x0200079E RID: 1950
		private new struct ResetPoint
		{
			// Token: 0x060088C6 RID: 35014 RVA: 0x001EEB6D File Offset: 0x001ECD6D
			internal ResetPoint(SyntaxParser.ResetPoint resetPoint, LanguageParser.TerminatorState terminatorState, bool isInTry, bool isInAsync, int queryDepth)
			{
				this.BaseResetPoint = resetPoint;
				this.TerminatorState = terminatorState;
				this.IsInTry = isInTry;
				this.IsInAsync = isInAsync;
				this.QueryDepth = queryDepth;
			}

			// Token: 0x04002466 RID: 9318
			internal SyntaxParser.ResetPoint BaseResetPoint;

			// Token: 0x04002467 RID: 9319
			internal readonly LanguageParser.TerminatorState TerminatorState;

			// Token: 0x04002468 RID: 9320
			internal readonly bool IsInTry;

			// Token: 0x04002469 RID: 9321
			internal readonly bool IsInAsync;

			// Token: 0x0400246A RID: 9322
			internal readonly int QueryDepth;
		}
	}
}
