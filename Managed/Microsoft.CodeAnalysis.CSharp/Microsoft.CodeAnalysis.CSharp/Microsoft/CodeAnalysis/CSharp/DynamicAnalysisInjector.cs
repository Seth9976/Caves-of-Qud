using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001FB RID: 507
	internal sealed class DynamicAnalysisInjector : CompoundInstrumenter
	{
		// Token: 0x0600230A RID: 8970 RVA: 0x000B7FB8 File Offset: 0x000B61B8
		public static DynamicAnalysisInjector TryCreate(MethodSymbol method, BoundStatement methodBody, SyntheticBoundNodeFactory methodBodyFactory, BindingDiagnosticBag diagnostics, DebugDocumentProvider debugDocumentProvider, Instrumenter previous)
		{
			if (method.IsImplicitlyDeclared && !method.IsImplicitConstructor)
			{
				return null;
			}
			if (DynamicAnalysisInjector.IsExcludedFromCodeCoverage(method))
			{
				return null;
			}
			MethodSymbol createPayloadOverload = DynamicAnalysisInjector.GetCreatePayloadOverload(methodBodyFactory.Compilation, WellKnownMember.Microsoft_CodeAnalysis_Runtime_Instrumentation__CreatePayloadForMethodsSpanningSingleFile, methodBody.Syntax, diagnostics);
			MethodSymbol createPayloadOverload2 = DynamicAnalysisInjector.GetCreatePayloadOverload(methodBodyFactory.Compilation, WellKnownMember.Microsoft_CodeAnalysis_Runtime_Instrumentation__CreatePayloadForMethodsSpanningMultipleFiles, methodBody.Syntax, diagnostics);
			if (createPayloadOverload == null || createPayloadOverload2 == null)
			{
				return null;
			}
			if (method.Equals(createPayloadOverload) || method.Equals(createPayloadOverload2))
			{
				return null;
			}
			return new DynamicAnalysisInjector(method, methodBody, methodBodyFactory, createPayloadOverload, createPayloadOverload2, diagnostics, debugDocumentProvider, previous);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000B803C File Offset: 0x000B623C
		private DynamicAnalysisInjector(MethodSymbol method, BoundStatement methodBody, SyntheticBoundNodeFactory methodBodyFactory, MethodSymbol createPayloadForMethodsSpanningSingleFile, MethodSymbol createPayloadForMethodsSpanningMultipleFiles, BindingDiagnosticBag diagnostics, DebugDocumentProvider debugDocumentProvider, Instrumenter previous)
			: base(previous)
		{
			this._createPayloadForMethodsSpanningSingleFile = createPayloadForMethodsSpanningSingleFile;
			this._createPayloadForMethodsSpanningMultipleFiles = createPayloadForMethodsSpanningMultipleFiles;
			this._method = method;
			this._methodBody = methodBody;
			this._spansBuilder = ArrayBuilder<SourceSpan>.GetInstance();
			TypeSymbol typeSymbol = methodBodyFactory.SpecialType(SpecialType.System_Boolean);
			this._payloadType = ArrayTypeSymbol.CreateCSharpArray(methodBodyFactory.Compilation.Assembly, TypeWithAnnotations.Create(typeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 1);
			this._diagnostics = diagnostics;
			this._debugDocumentProvider = debugDocumentProvider;
			this._methodBodyFactory = methodBodyFactory;
			MethodSymbol currentFunction = methodBodyFactory.CurrentFunction;
			methodBodyFactory.CurrentFunction = method;
			this._methodPayload = methodBodyFactory.SynthesizedLocal(this._payloadType, methodBody.Syntax, false, RefKind.None, SynthesizedLocalKind.InstrumentationPayload);
			SyntaxNode syntaxNode = DynamicAnalysisInjector.MethodDeclarationIfAvailable(methodBody.Syntax);
			if (!method.IsImplicitlyDeclared && !(method is SynthesizedSimpleProgramEntryPointSymbol))
			{
				this._methodEntryInstrumentation = this.AddAnalysisPoint(syntaxNode, DynamicAnalysisInjector.SkipAttributes(syntaxNode), methodBodyFactory);
			}
			methodBodyFactory.CurrentFunction = currentFunction;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000B812C File Offset: 0x000B632C
		private static bool IsExcludedFromCodeCoverage(MethodSymbol method)
		{
			for (NamedTypeSymbol namedTypeSymbol = method.ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
			{
				if (namedTypeSymbol.IsDirectlyExcludedFromCodeCoverage)
				{
					return true;
				}
			}
			if (method != null)
			{
				if (method.IsDirectlyExcludedFromCodeCoverage)
				{
					return true;
				}
				Symbol associatedSymbol = method.AssociatedSymbol;
				PropertySymbol propertySymbol = associatedSymbol as PropertySymbol;
				if (propertySymbol != null)
				{
					if (propertySymbol.IsDirectlyExcludedFromCodeCoverage)
					{
						return true;
					}
				}
				else
				{
					EventSymbol eventSymbol = associatedSymbol as EventSymbol;
					if (eventSymbol != null)
					{
						if (eventSymbol.IsDirectlyExcludedFromCodeCoverage)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000B81A4 File Offset: 0x000B63A4
		private static BoundExpressionStatement GetCreatePayloadStatement(ImmutableArray<SourceSpan> dynamicAnalysisSpans, SyntaxNode methodBodySyntax, LocalSymbol methodPayload, MethodSymbol createPayloadForMethodsSpanningSingleFile, MethodSymbol createPayloadForMethodsSpanningMultipleFiles, BoundExpression mvid, BoundExpression methodToken, BoundExpression payloadSlot, SyntheticBoundNodeFactory methodBodyFactory, DebugDocumentProvider debugDocumentProvider)
		{
			MethodSymbol methodSymbol;
			BoundExpression boundExpression;
			if (dynamicAnalysisSpans.IsEmpty)
			{
				methodSymbol = createPayloadForMethodsSpanningSingleFile;
				DebugSourceDocument sourceDocument = DynamicAnalysisInjector.GetSourceDocument(debugDocumentProvider, methodBodySyntax);
				boundExpression = methodBodyFactory.SourceDocumentIndex(sourceDocument);
			}
			else
			{
				PooledHashSet<DebugSourceDocument> instance = PooledHashSet<DebugSourceDocument>.GetInstance();
				ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
				foreach (SourceSpan sourceSpan in dynamicAnalysisSpans)
				{
					DebugSourceDocument document = sourceSpan.Document;
					if (instance.Add(document))
					{
						instance2.Add(methodBodyFactory.SourceDocumentIndex(document));
					}
				}
				instance.Free();
				if (instance2.Count == 1)
				{
					methodSymbol = createPayloadForMethodsSpanningSingleFile;
					boundExpression = Enumerable.Single<BoundExpression>(instance2);
				}
				else
				{
					methodSymbol = createPayloadForMethodsSpanningMultipleFiles;
					boundExpression = methodBodyFactory.Array(methodBodyFactory.SpecialType(SpecialType.System_Int32), instance2.ToImmutable());
				}
				instance2.Free();
			}
			return methodBodyFactory.Assignment(methodBodyFactory.Local(methodPayload), methodBodyFactory.Call(null, methodSymbol, new BoundExpression[]
			{
				mvid,
				methodToken,
				boundExpression,
				payloadSlot,
				methodBodyFactory.Literal(dynamicAnalysisSpans.Length)
			}), false);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000B82A0 File Offset: 0x000B64A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override BoundStatement CreateBlockPrologue([global::System.Runtime.CompilerServices.Nullable(1)] BoundBlock original, out LocalSymbol synthesizedLocal)
		{
			BoundStatement boundStatement = base.CreateBlockPrologue(original, out synthesizedLocal);
			if (this._methodBody == original)
			{
				this._dynamicAnalysisSpans = this._spansBuilder.ToImmutableAndFree();
				ArrayTypeSymbol arrayTypeSymbol = ArrayTypeSymbol.CreateCSharpArray(this._methodBodyFactory.Compilation.Assembly, TypeWithAnnotations.Create(this._payloadType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 1);
				BoundStatement boundStatement2 = this._methodBodyFactory.Assignment(this._methodBodyFactory.Local(this._methodPayload), this._methodBodyFactory.ArrayAccess(this._methodBodyFactory.InstrumentationPayloadRoot(0, arrayTypeSymbol), ImmutableArray.Create<BoundExpression>(this._methodBodyFactory.MethodDefIndex(this._method))), false);
				BoundExpression boundExpression = this._methodBodyFactory.ModuleVersionId();
				BoundExpression boundExpression2 = this._methodBodyFactory.MethodDefIndex(this._method);
				BoundExpression boundExpression3 = this._methodBodyFactory.ArrayAccess(this._methodBodyFactory.InstrumentationPayloadRoot(0, arrayTypeSymbol), ImmutableArray.Create<BoundExpression>(this._methodBodyFactory.MethodDefIndex(this._method)));
				BoundStatement createPayloadStatement = DynamicAnalysisInjector.GetCreatePayloadStatement(this._dynamicAnalysisSpans, this._methodBody.Syntax, this._methodPayload, this._createPayloadForMethodsSpanningSingleFile, this._createPayloadForMethodsSpanningMultipleFiles, boundExpression, boundExpression2, boundExpression3, this._methodBodyFactory, this._debugDocumentProvider);
				BoundExpression boundExpression4 = this._methodBodyFactory.Binary(BinaryOperatorKind.ObjectEqual, this._methodBodyFactory.SpecialType(SpecialType.System_Boolean), this._methodBodyFactory.Local(this._methodPayload), this._methodBodyFactory.Null(this._payloadType));
				BoundStatement boundStatement3 = this._methodBodyFactory.If(boundExpression4, createPayloadStatement, null);
				synthesizedLocal = this._methodPayload;
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance((boundStatement == null) ? 3 : 4);
				instance.Add(boundStatement2);
				instance.Add(boundStatement3);
				if (this._methodEntryInstrumentation != null)
				{
					instance.Add(this._methodEntryInstrumentation);
				}
				if (boundStatement != null)
				{
					instance.Add(boundStatement);
				}
				return this._methodBodyFactory.StatementList(instance.ToImmutableAndFree());
			}
			return boundStatement;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000B8483 File Offset: 0x000B6683
		public ImmutableArray<SourceSpan> DynamicAnalysisSpans
		{
			get
			{
				return this._dynamicAnalysisSpans;
			}
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000B848B File Offset: 0x000B668B
		public override BoundStatement InstrumentNoOpStatement(BoundNoOpStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentNoOpStatement(original, rewritten));
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000B849C File Offset: 0x000B669C
		public override BoundStatement InstrumentBreakStatement(BoundBreakStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentBreakStatement(original, rewritten));
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000B84AD File Offset: 0x000B66AD
		public override BoundStatement InstrumentContinueStatement(BoundContinueStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentContinueStatement(original, rewritten));
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000B84BE File Offset: 0x000B66BE
		public override BoundStatement InstrumentExpressionStatement(BoundExpressionStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentExpressionStatement(original, rewritten));
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000B84CF File Offset: 0x000B66CF
		public override BoundStatement InstrumentFieldOrPropertyInitializer(BoundStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentFieldOrPropertyInitializer(original, rewritten));
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000B84E0 File Offset: 0x000B66E0
		public override BoundStatement InstrumentGotoStatement(BoundGotoStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentGotoStatement(original, rewritten));
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000B84F1 File Offset: 0x000B66F1
		public override BoundStatement InstrumentThrowStatement(BoundThrowStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentThrowStatement(original, rewritten));
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000B8502 File Offset: 0x000B6702
		public override BoundStatement InstrumentYieldBreakStatement(BoundYieldBreakStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentYieldBreakStatement(original, rewritten));
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000B8513 File Offset: 0x000B6713
		public override BoundStatement InstrumentYieldReturnStatement(BoundYieldReturnStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentYieldReturnStatement(original, rewritten));
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000B8524 File Offset: 0x000B6724
		public override BoundStatement InstrumentForEachStatementIterationVarDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentForEachStatementIterationVarDeclaration(original, iterationVarDecl));
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000B8535 File Offset: 0x000B6735
		public override BoundStatement InstrumentForEachStatementDeconstructionVariablesDeclaration(BoundForEachStatement original, BoundStatement iterationVarDecl)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentForEachStatementDeconstructionVariablesDeclaration(original, iterationVarDecl));
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000B8546 File Offset: 0x000B6746
		public override BoundStatement InstrumentIfStatement(BoundIfStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentIfStatement(original, rewritten));
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000B8557 File Offset: 0x000B6757
		public override BoundStatement InstrumentWhileStatementConditionalGotoStartOrBreak(BoundWhileStatement original, BoundStatement ifConditionGotoStart)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentWhileStatementConditionalGotoStartOrBreak(original, ifConditionGotoStart));
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000B8568 File Offset: 0x000B6768
		public override BoundStatement InstrumentLocalInitialization(BoundLocalDeclaration original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentLocalInitialization(original, rewritten));
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000B8579 File Offset: 0x000B6779
		public override BoundStatement InstrumentLockTargetCapture(BoundLockStatement original, BoundStatement lockTargetCapture)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentLockTargetCapture(original, lockTargetCapture));
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000B858A File Offset: 0x000B678A
		public override BoundStatement InstrumentReturnStatement(BoundReturnStatement original, BoundStatement rewritten)
		{
			rewritten = base.InstrumentReturnStatement(original, rewritten);
			if (DynamicAnalysisInjector.ReturnsValueWithinExpressionBodiedConstruct(original))
			{
				return this.CollectDynamicAnalysis(original, rewritten);
			}
			return this.AddDynamicAnalysis(original, rewritten);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000B85B0 File Offset: 0x000B67B0
		private static bool ReturnsValueWithinExpressionBodiedConstruct(BoundReturnStatement returnStatement)
		{
			if (returnStatement.WasCompilerGenerated && returnStatement.ExpressionOpt != null && returnStatement.ExpressionOpt.Syntax != null)
			{
				SyntaxKind syntaxKind = returnStatement.ExpressionOpt.Syntax.Parent.Kind();
				if (syntaxKind - SyntaxKind.SimpleLambdaExpression <= 1 || syntaxKind == SyntaxKind.ArrowExpressionClause)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000B8605 File Offset: 0x000B6805
		public override BoundStatement InstrumentSwitchStatement(BoundSwitchStatement original, BoundStatement rewritten)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentSwitchStatement(original, rewritten));
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000B8618 File Offset: 0x000B6818
		public override BoundStatement InstrumentSwitchWhenClauseConditionalGotoBody(BoundExpression original, BoundStatement ifConditionGotoBody)
		{
			ifConditionGotoBody = base.InstrumentSwitchWhenClauseConditionalGotoBody(original, ifConditionGotoBody);
			WhenClauseSyntax whenClauseSyntax = original.Syntax.FirstAncestorOrSelf<WhenClauseSyntax>(null, true);
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this._method, whenClauseSyntax, this._methodBodyFactory.CompilationState, this._diagnostics);
			return syntheticBoundNodeFactory.StatementList(this.AddAnalysisPoint(whenClauseSyntax, syntheticBoundNodeFactory), ifConditionGotoBody);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000B866A File Offset: 0x000B686A
		public override BoundStatement InstrumentUsingTargetCapture(BoundUsingStatement original, BoundStatement usingTargetCapture)
		{
			return this.AddDynamicAnalysis(original, base.InstrumentUsingTargetCapture(original, usingTargetCapture));
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000B867B File Offset: 0x000B687B
		private BoundStatement AddDynamicAnalysis(BoundStatement original, BoundStatement rewritten)
		{
			if (!original.WasCompilerGenerated && (!original.IsConstructorInitializer() || original.Syntax.Kind() != SyntaxKind.ConstructorDeclaration))
			{
				return this.CollectDynamicAnalysis(original, rewritten);
			}
			return rewritten;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000B86AC File Offset: 0x000B68AC
		private BoundStatement CollectDynamicAnalysis(BoundStatement original, BoundStatement rewritten)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this._method, original.Syntax, this._methodBodyFactory.CompilationState, this._diagnostics);
			return syntheticBoundNodeFactory.StatementList(this.AddAnalysisPoint(DynamicAnalysisInjector.SyntaxForSpan(original), syntheticBoundNodeFactory), rewritten);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000B86F0 File Offset: 0x000B68F0
		private static DebugSourceDocument GetSourceDocument(DebugDocumentProvider debugDocumentProvider, SyntaxNode syntax)
		{
			return DynamicAnalysisInjector.GetSourceDocument(debugDocumentProvider, syntax, syntax.GetLocation().GetMappedLineSpan());
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000B8704 File Offset: 0x000B6904
		private static DebugSourceDocument GetSourceDocument(DebugDocumentProvider debugDocumentProvider, SyntaxNode syntax, FileLinePositionSpan span)
		{
			string text = span.Path;
			if (text.Length == 0)
			{
				text = syntax.SyntaxTree.FilePath;
			}
			return debugDocumentProvider(text, "");
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000B873C File Offset: 0x000B693C
		private BoundStatement AddAnalysisPoint(SyntaxNode syntaxForSpan, TextSpan alternateSpan, SyntheticBoundNodeFactory statementFactory)
		{
			return this.AddAnalysisPoint(syntaxForSpan, syntaxForSpan.SyntaxTree.GetMappedLineSpan(alternateSpan, default(CancellationToken)), statementFactory);
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000B8766 File Offset: 0x000B6966
		private BoundStatement AddAnalysisPoint(SyntaxNode syntaxForSpan, SyntheticBoundNodeFactory statementFactory)
		{
			return this.AddAnalysisPoint(syntaxForSpan, syntaxForSpan.GetLocation().GetMappedLineSpan(), statementFactory);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000B877C File Offset: 0x000B697C
		private BoundStatement AddAnalysisPoint(SyntaxNode syntaxForSpan, FileLinePositionSpan span, SyntheticBoundNodeFactory statementFactory)
		{
			int count = this._spansBuilder.Count;
			this._spansBuilder.Add(new SourceSpan(DynamicAnalysisInjector.GetSourceDocument(this._debugDocumentProvider, syntaxForSpan, span), span.StartLinePosition.Line, span.StartLinePosition.Character, span.EndLinePosition.Line, span.EndLinePosition.Character));
			BoundArrayAccess boundArrayAccess = statementFactory.ArrayAccess(statementFactory.Local(this._methodPayload), new BoundExpression[] { statementFactory.Literal(count) });
			return statementFactory.Assignment(boundArrayAccess, statementFactory.Literal(true), false);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000B8820 File Offset: 0x000B6A20
		private static SyntaxNode SyntaxForSpan(BoundStatement statement)
		{
			switch (statement.Kind)
			{
			case BoundKind.SwitchStatement:
				return ((BoundSwitchStatement)statement).Expression.Syntax;
			case BoundKind.IfStatement:
				return ((BoundIfStatement)statement).Condition.Syntax;
			case BoundKind.DoStatement:
				return ((BoundDoStatement)statement).Condition.Syntax;
			case BoundKind.WhileStatement:
				return ((BoundWhileStatement)statement).Condition.Syntax;
			case BoundKind.ForEachStatement:
				return ((BoundForEachStatement)statement).Expression.Syntax;
			case BoundKind.UsingStatement:
			{
				BoundUsingStatement boundUsingStatement = (BoundUsingStatement)statement;
				return (boundUsingStatement.ExpressionOpt ?? boundUsingStatement.DeclarationsOpt).Syntax;
			}
			case BoundKind.FixedStatement:
				return ((BoundFixedStatement)statement).Declarations.Syntax;
			case BoundKind.LockStatement:
				return ((BoundLockStatement)statement).Argument.Syntax;
			}
			return statement.Syntax;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000B8920 File Offset: 0x000B6B20
		private static MethodSymbol GetCreatePayloadOverload(CSharpCompilation compilation, WellKnownMember overload, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			return (MethodSymbol)Binder.GetWellKnownTypeMember(compilation, overload, diagnostics, null, syntax, false);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000B8934 File Offset: 0x000B6B34
		private static SyntaxNode MethodDeclarationIfAvailable(SyntaxNode body)
		{
			SyntaxNode parent = body.Parent;
			if (parent != null)
			{
				SyntaxKind syntaxKind = parent.Kind();
				if (syntaxKind <= SyntaxKind.ConstructorDeclaration)
				{
					if (syntaxKind - SyntaxKind.MethodDeclaration > 1 && syntaxKind != SyntaxKind.ConstructorDeclaration)
					{
						return body;
					}
				}
				else if (syntaxKind != SyntaxKind.PropertyDeclaration && syntaxKind - SyntaxKind.GetAccessorDeclaration > 1 && syntaxKind != SyntaxKind.InitAccessorDeclaration)
				{
					return body;
				}
				return parent;
			}
			return body;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000B898C File Offset: 0x000B6B8C
		private static TextSpan SkipAttributes(SyntaxNode syntax)
		{
			SyntaxKind syntaxKind = syntax.Kind();
			if (syntaxKind <= SyntaxKind.PropertyDeclaration)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.MethodDeclaration:
				{
					MethodDeclarationSyntax methodDeclarationSyntax = (MethodDeclarationSyntax)syntax;
					return DynamicAnalysisInjector.SkipAttributes(syntax, methodDeclarationSyntax.AttributeLists, methodDeclarationSyntax.Modifiers, default(SyntaxToken), methodDeclarationSyntax.ReturnType);
				}
				case SyntaxKind.OperatorDeclaration:
				{
					OperatorDeclarationSyntax operatorDeclarationSyntax = (OperatorDeclarationSyntax)syntax;
					return DynamicAnalysisInjector.SkipAttributes(syntax, operatorDeclarationSyntax.AttributeLists, operatorDeclarationSyntax.Modifiers, operatorDeclarationSyntax.OperatorKeyword, null);
				}
				case SyntaxKind.ConversionOperatorDeclaration:
					break;
				case SyntaxKind.ConstructorDeclaration:
				{
					ConstructorDeclarationSyntax constructorDeclarationSyntax = (ConstructorDeclarationSyntax)syntax;
					return DynamicAnalysisInjector.SkipAttributes(syntax, constructorDeclarationSyntax.AttributeLists, constructorDeclarationSyntax.Modifiers, constructorDeclarationSyntax.Identifier, null);
				}
				default:
					if (syntaxKind == SyntaxKind.PropertyDeclaration)
					{
						PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)syntax;
						return DynamicAnalysisInjector.SkipAttributes(syntax, propertyDeclarationSyntax.AttributeLists, propertyDeclarationSyntax.Modifiers, default(SyntaxToken), propertyDeclarationSyntax.Type);
					}
					break;
				}
			}
			else if (syntaxKind - SyntaxKind.GetAccessorDeclaration <= 1 || syntaxKind == SyntaxKind.InitAccessorDeclaration)
			{
				AccessorDeclarationSyntax accessorDeclarationSyntax = (AccessorDeclarationSyntax)syntax;
				return DynamicAnalysisInjector.SkipAttributes(syntax, accessorDeclarationSyntax.AttributeLists, accessorDeclarationSyntax.Modifiers, accessorDeclarationSyntax.Keyword, null);
			}
			return syntax.Span;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000B8AB0 File Offset: 0x000B6CB0
		private static TextSpan SkipAttributes(SyntaxNode syntax, SyntaxList<AttributeListSyntax> attributes, SyntaxTokenList modifiers, SyntaxToken keyword, TypeSyntax type)
		{
			TextSpan span = syntax.Span;
			if (attributes.Count > 0)
			{
				TextSpan textSpan = ((modifiers.Node != null) ? modifiers.Span : ((keyword.Node != null) ? keyword.Span : type.Span));
				return new TextSpan(textSpan.Start, span.Length - (textSpan.Start - span.Start));
			}
			return span;
		}

		// Token: 0x040010A8 RID: 4264
		private readonly MethodSymbol _method;

		// Token: 0x040010A9 RID: 4265
		private readonly BoundStatement _methodBody;

		// Token: 0x040010AA RID: 4266
		private readonly MethodSymbol _createPayloadForMethodsSpanningSingleFile;

		// Token: 0x040010AB RID: 4267
		private readonly MethodSymbol _createPayloadForMethodsSpanningMultipleFiles;

		// Token: 0x040010AC RID: 4268
		private readonly ArrayBuilder<SourceSpan> _spansBuilder;

		// Token: 0x040010AD RID: 4269
		private ImmutableArray<SourceSpan> _dynamicAnalysisSpans = ImmutableArray<SourceSpan>.Empty;

		// Token: 0x040010AE RID: 4270
		private readonly BoundStatement _methodEntryInstrumentation;

		// Token: 0x040010AF RID: 4271
		private readonly ArrayTypeSymbol _payloadType;

		// Token: 0x040010B0 RID: 4272
		private readonly LocalSymbol _methodPayload;

		// Token: 0x040010B1 RID: 4273
		private readonly BindingDiagnosticBag _diagnostics;

		// Token: 0x040010B2 RID: 4274
		private readonly DebugDocumentProvider _debugDocumentProvider;

		// Token: 0x040010B3 RID: 4275
		private readonly SyntheticBoundNodeFactory _methodBodyFactory;
	}
}
