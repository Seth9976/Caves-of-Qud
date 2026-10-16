using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.RuntimeMembers;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000204 RID: 516
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LocalRewriter : BoundTreeRewriterWithStackGuard
	{
		// Token: 0x060023B9 RID: 9145 RVA: 0x000BA158 File Offset: 0x000B8358
		private LocalRewriter(CSharpCompilation compilation, MethodSymbol containingMethod, int containingMethodOrdinal, BoundStatement rootStatement, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol containingType, SyntheticBoundNodeFactory factory, SynthesizedSubmissionFields previousSubmissionFields, bool allowOmissionOfConditionalCalls, BindingDiagnosticBag diagnostics, Instrumenter instrumenter)
		{
			this._compilation = compilation;
			this._factory = factory;
			this._factory.CurrentFunction = containingMethod;
			this._dynamicFactory = new LoweredDynamicOperationFactory(factory, containingMethodOrdinal, -1);
			this._previousSubmissionFields = previousSubmissionFields;
			this._allowOmissionOfConditionalCalls = allowOmissionOfConditionalCalls;
			this._diagnostics = diagnostics;
			this._instrumenter = instrumenter;
			this._rootStatement = rootStatement;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000BA1C0 File Offset: 0x000B83C0
		public static BoundStatement Rewrite(CSharpCompilation compilation, MethodSymbol method, int methodOrdinal, NamedTypeSymbol containingType, BoundStatement statement, TypeCompilationState compilationState, SynthesizedSubmissionFields previousSubmissionFields, bool allowOmissionOfConditionalCalls, bool instrumentForDynamicAnalysis, [global::System.Runtime.CompilerServices.Nullable(0)] ref ImmutableArray<SourceSpan> dynamicAnalysisSpans, DebugDocumentProvider debugDocumentProvider, BindingDiagnosticBag diagnostics, out bool sawLambdas, out bool sawLocalFunctions, out bool sawAwaitInExceptionHandler)
		{
			BoundStatement boundStatement2;
			try
			{
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(method, statement.Syntax, compilationState, diagnostics);
				DynamicAnalysisInjector dynamicAnalysisInjector = (instrumentForDynamicAnalysis ? DynamicAnalysisInjector.TryCreate(method, statement, syntheticBoundNodeFactory, diagnostics, debugDocumentProvider, Instrumenter.NoOp) : null);
				LocalRewriter localRewriter = new LocalRewriter(compilation, method, methodOrdinal, statement, containingType, syntheticBoundNodeFactory, previousSubmissionFields, allowOmissionOfConditionalCalls, diagnostics, (dynamicAnalysisInjector != null) ? new DebugInfoInjector(dynamicAnalysisInjector) : DebugInfoInjector.Singleton);
				BoundStatement boundStatement = localRewriter.VisitStatement(statement);
				sawLambdas = localRewriter._sawLambdas;
				sawLocalFunctions = localRewriter._availableLocalFunctionOrdinal != 0;
				sawAwaitInExceptionHandler = localRewriter._sawAwaitInExceptionHandler;
				if (localRewriter._needsSpilling && !boundStatement.HasErrors)
				{
					boundStatement = SpillSequenceSpiller.Rewrite(boundStatement, method, compilationState, diagnostics);
				}
				if (dynamicAnalysisInjector != null)
				{
					dynamicAnalysisSpans = dynamicAnalysisInjector.DynamicAnalysisSpans;
				}
				boundStatement2 = boundStatement;
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				sawLambdas = (sawLocalFunctions = (sawAwaitInExceptionHandler = false));
				boundStatement2 = new BoundBadStatement(statement.Syntax, ImmutableArray.Create<BoundNode>(statement), true);
			}
			return boundStatement2;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000BA2C4 File Offset: 0x000B84C4
		private bool Instrument
		{
			get
			{
				return !this._inExpressionLambda;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000BA2CF File Offset: 0x000B84CF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private PEModuleBuilder EmitModule
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._factory.CompilationState.ModuleBuilderOpt;
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000BA2E4 File Offset: 0x000B84E4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override BoundNode Visit(BoundNode node)
		{
			if (node == null)
			{
				return node;
			}
			BoundExpression boundExpression = node as BoundExpression;
			if (boundExpression != null)
			{
				return this.VisitExpressionImpl(boundExpression);
			}
			return node.Accept(this);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000BA30F File Offset: 0x000B850F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("node")]
		private BoundExpression VisitExpression(BoundExpression node)
		{
			if (node == null)
			{
				return node;
			}
			return this.VisitExpressionImpl(node);
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000BA31D File Offset: 0x000B851D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private BoundStatement VisitStatement(BoundStatement node)
		{
			if (node == null)
			{
				return node;
			}
			return (BoundStatement)node.Accept(this);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000BA330 File Offset: 0x000B8530
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression VisitExpressionImpl(BoundExpression node)
		{
			ConstantValue constantValue = node.ConstantValue;
			if (constantValue != null)
			{
				TypeSymbol type = node.Type;
				if (type == null || !type.IsNullableType())
				{
					return this.MakeLiteral(node.Syntax, constantValue, type, null);
				}
			}
			BoundExpression boundExpression = base.VisitExpressionWithStackGuard(node);
			if (boundExpression != null && boundExpression != node && node.Kind != BoundKind.ImplicitReceiver && node.Kind != BoundKind.ObjectOrCollectionValuePlaceholder && !LocalRewriter.CanBePassedByReference(node) && LocalRewriter.CanBePassedByReference(boundExpression))
			{
				boundExpression = LocalRewriter.RefAccessMustMakeCopy(boundExpression);
			}
			return boundExpression;
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000BA3B2 File Offset: 0x000B85B2
		private static BoundExpression RefAccessMustMakeCopy(BoundExpression visited)
		{
			visited = new BoundPassByCopy(visited.Syntax, visited, visited.Type, false);
			return visited;
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000BA3CA File Offset: 0x000B85CA
		private static bool IsUnusedDeconstruction(BoundExpression node)
		{
			return node.Kind == BoundKind.DeconstructionAssignmentOperator && !((BoundDeconstructionAssignmentOperator)node).IsUsed;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000BA3E8 File Offset: 0x000B85E8
		public override BoundNode VisitLambda(BoundLambda node)
		{
			this._sawLambdas = true;
			LambdaSymbol symbol = node.Symbol;
			this.CheckRefReadOnlySymbols(symbol);
			MethodSymbol currentFunction = this._factory.CurrentFunction;
			Instrumenter instrumenter = this._instrumenter;
			BoundNode boundNode;
			try
			{
				this._factory.CurrentFunction = symbol;
				if (symbol.IsDirectlyExcludedFromCodeCoverage)
				{
					this._instrumenter = LocalRewriter.RemoveDynamicAnalysisInjectors(instrumenter);
				}
				boundNode = base.VisitLambda(node);
			}
			finally
			{
				this._factory.CurrentFunction = currentFunction;
				this._instrumenter = instrumenter;
			}
			return boundNode;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000BA46C File Offset: 0x000B866C
		public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
		{
			int availableLocalFunctionOrdinal = this._availableLocalFunctionOrdinal;
			this._availableLocalFunctionOrdinal = availableLocalFunctionOrdinal + 1;
			int num = availableLocalFunctionOrdinal;
			LocalFunctionSymbol symbol = node.Symbol;
			this.CheckRefReadOnlySymbols(symbol);
			PEModuleBuilder moduleBuilderOpt = this._factory.CompilationState.ModuleBuilderOpt;
			if (moduleBuilderOpt != null)
			{
				ImmutableArray<TypeParameterSymbol> typeParameters = symbol.TypeParameters;
				if (typeParameters.Any<TypeParameterSymbol>((TypeParameterSymbol typeParameter) => typeParameter.HasUnmanagedTypeConstraint))
				{
					moduleBuilderOpt.EnsureIsUnmanagedAttributeExists();
				}
				if (!LocalRewriter.<VisitLocalFunctionStatement>g__hasReturnTypeOrParameter|28_3(symbol, (TypeWithAnnotations t) => t.ContainsNativeInteger()))
				{
					if (!typeParameters.Any<TypeParameterSymbol>((TypeParameterSymbol t) => t.ConstraintTypesNoUseSiteDiagnostics.Any<TypeWithAnnotations>((TypeWithAnnotations t) => t.ContainsNativeInteger())))
					{
						goto IL_00C5;
					}
				}
				moduleBuilderOpt.EnsureNativeIntegerAttributeExists();
				IL_00C5:
				if (this._factory.CompilationState.Compilation.ShouldEmitNullableAttributes(symbol))
				{
					if (!typeParameters.Any<TypeParameterSymbol>((TypeParameterSymbol typeParameter) => ((SourceTypeParameterSymbolBase)typeParameter).ConstraintsNeedNullableAttribute()))
					{
						if (!LocalRewriter.<VisitLocalFunctionStatement>g__hasReturnTypeOrParameter|28_3(symbol, (TypeWithAnnotations t) => t.NeedsNullableAttribute()))
						{
							goto IL_0132;
						}
					}
					moduleBuilderOpt.EnsureNullableAttributeExists();
				}
			}
			IL_0132:
			MethodSymbol currentFunction = this._factory.CurrentFunction;
			Instrumenter instrumenter = this._instrumenter;
			LoweredDynamicOperationFactory dynamicFactory = this._dynamicFactory;
			BoundNode boundNode;
			try
			{
				this._factory.CurrentFunction = symbol;
				if (symbol.IsDirectlyExcludedFromCodeCoverage)
				{
					this._instrumenter = LocalRewriter.RemoveDynamicAnalysisInjectors(instrumenter);
				}
				if (symbol.IsGenericMethod)
				{
					this._dynamicFactory = new LoweredDynamicOperationFactory(this._factory, this._dynamicFactory.MethodOrdinal, num);
				}
				boundNode = base.VisitLocalFunctionStatement(node);
			}
			finally
			{
				this._factory.CurrentFunction = currentFunction;
				this._instrumenter = instrumenter;
				this._dynamicFactory = dynamicFactory;
			}
			return boundNode;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000BA648 File Offset: 0x000B8848
		private static Instrumenter RemoveDynamicAnalysisInjectors(Instrumenter instrumenter)
		{
			DynamicAnalysisInjector dynamicAnalysisInjector = instrumenter as DynamicAnalysisInjector;
			if (dynamicAnalysisInjector != null)
			{
				Instrumenter previous = dynamicAnalysisInjector.Previous;
				return LocalRewriter.RemoveDynamicAnalysisInjectors(previous);
			}
			DebugInfoInjector debugInfoInjector = instrumenter as DebugInfoInjector;
			if (debugInfoInjector != null)
			{
				Instrumenter previous2 = debugInfoInjector.Previous;
				Instrumenter instrumenter2 = LocalRewriter.RemoveDynamicAnalysisInjectors(previous2);
				if (instrumenter2 == previous2)
				{
					return debugInfoInjector;
				}
				if (instrumenter2 == Instrumenter.NoOp)
				{
					return DebugInfoInjector.Singleton;
				}
				return new DebugInfoInjector(previous2);
			}
			else
			{
				CompoundInstrumenter compoundInstrumenter = instrumenter as CompoundInstrumenter;
				if (compoundInstrumenter == null)
				{
					return instrumenter;
				}
				throw ExceptionUtilities.UnexpectedValue(compoundInstrumenter);
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000BA6BE File Offset: 0x000B88BE
		public override BoundNode VisitDefaultLiteral(BoundDefaultLiteral node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000BA6C5 File Offset: 0x000B88C5
		public override BoundNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000BA6CC File Offset: 0x000B88CC
		public override BoundNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node)
		{
			return this.PlaceholderReplacement(node);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000BA6D5 File Offset: 0x000B88D5
		public override BoundNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node)
		{
			if (this._inExpressionLambda)
			{
				return node;
			}
			return this.PlaceholderReplacement(node);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000BA6E8 File Offset: 0x000B88E8
		private BoundExpression PlaceholderReplacement(BoundValuePlaceholderBase placeholder)
		{
			return this._placeholderReplacementMapDoNotUseDirectly[placeholder];
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000BA6F6 File Offset: 0x000B88F6
		[Conditional("DEBUG")]
		private static void AssertPlaceholderReplacement(BoundValuePlaceholderBase placeholder, BoundExpression value)
		{
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000BA6F8 File Offset: 0x000B88F8
		private void AddPlaceholderReplacement(BoundValuePlaceholderBase placeholder, BoundExpression value)
		{
			if (this._placeholderReplacementMapDoNotUseDirectly == null)
			{
				this._placeholderReplacementMapDoNotUseDirectly = new Dictionary<BoundValuePlaceholderBase, BoundExpression>();
			}
			this._placeholderReplacementMapDoNotUseDirectly.Add(placeholder, value);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000BA71A File Offset: 0x000B891A
		private void RemovePlaceholderReplacement(BoundValuePlaceholderBase placeholder)
		{
			this._placeholderReplacementMapDoNotUseDirectly.Remove(placeholder);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000BA729 File Offset: 0x000B8929
		public sealed override BoundNode VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000BA730 File Offset: 0x000B8930
		public override BoundNode VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000BA737 File Offset: 0x000B8937
		public override BoundNode VisitBadExpression(BoundBadExpression node)
		{
			return node;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000BA73A File Offset: 0x000B893A
		private static BoundExpression BadExpression(BoundExpression node)
		{
			return LocalRewriter.BadExpression(node.Syntax, node.Type, ImmutableArray.Create<BoundExpression>(node));
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000BA753 File Offset: 0x000B8953
		private static BoundExpression BadExpression(SyntaxNode syntax, TypeSymbol resultType, BoundExpression child)
		{
			return LocalRewriter.BadExpression(syntax, resultType, ImmutableArray.Create<BoundExpression>(child));
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000BA762 File Offset: 0x000B8962
		private static BoundExpression BadExpression(SyntaxNode syntax, TypeSymbol resultType, BoundExpression child1, BoundExpression child2)
		{
			return LocalRewriter.BadExpression(syntax, resultType, ImmutableArray.Create<BoundExpression>(child1, child2));
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000BA772 File Offset: 0x000B8972
		private static BoundExpression BadExpression(SyntaxNode syntax, TypeSymbol resultType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> children)
		{
			return new BoundBadExpression(syntax, LookupResultKind.NotReferencable, ImmutableArray<Symbol>.Empty, children, resultType);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000BA784 File Offset: 0x000B8984
		private bool TryGetWellKnownTypeMember<[global::System.Runtime.CompilerServices.Nullable(0)] TSymbol>([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode syntax, WellKnownMember member, out TSymbol symbol, bool isOptional = false, [global::System.Runtime.CompilerServices.Nullable(2)] Location location = null) where TSymbol : Symbol
		{
			symbol = (TSymbol)((object)Binder.GetWellKnownTypeMember(this._compilation, member, this._diagnostics, location, syntax, isOptional));
			return symbol != null;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000BA7C5 File Offset: 0x000B89C5
		private MethodSymbol UnsafeGetSpecialTypeMethod(SyntaxNode syntax, SpecialMember specialMember)
		{
			return LocalRewriter.UnsafeGetSpecialTypeMethod(syntax, specialMember, this._compilation, this._diagnostics);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000BA7DC File Offset: 0x000B89DC
		private static MethodSymbol UnsafeGetSpecialTypeMethod(SyntaxNode syntax, SpecialMember specialMember, CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			MethodSymbol methodSymbol;
			if (LocalRewriter.TryGetSpecialTypeMethod(syntax, specialMember, compilation, diagnostics, out methodSymbol))
			{
				return methodSymbol;
			}
			MemberDescriptor descriptor = SpecialMembers.GetDescriptor(specialMember);
			SpecialType specialType = (SpecialType)descriptor.DeclaringTypeId;
			TypeSymbol specialType2 = compilation.Assembly.GetSpecialType(specialType);
			TypeSymbol typeSymbol = new ExtendedErrorTypeSymbol(compilation, descriptor.Name, (int)descriptor.Arity, null, false, false);
			return new ErrorMethodSymbol(specialType2, typeSymbol, "Missing");
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000BA834 File Offset: 0x000B8A34
		private bool TryGetSpecialTypeMethod(SyntaxNode syntax, SpecialMember specialMember, out MethodSymbol method)
		{
			return LocalRewriter.TryGetSpecialTypeMethod(syntax, specialMember, this._compilation, this._diagnostics, out method);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000BA84A File Offset: 0x000B8A4A
		private static bool TryGetSpecialTypeMethod(SyntaxNode syntax, SpecialMember specialMember, CSharpCompilation compilation, BindingDiagnosticBag diagnostics, out MethodSymbol method)
		{
			return Binder.TryGetSpecialTypeMember<MethodSymbol>(compilation, specialMember, syntax, diagnostics, out method);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000BA858 File Offset: 0x000B8A58
		public override BoundNode VisitTypeOfOperator(BoundTypeOfOperator node)
		{
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.Visit(node.SourceType);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			MethodSymbol methodSymbol;
			if (!this.TryGetWellKnownTypeMember<MethodSymbol>(node.Syntax, WellKnownMember.System_Type__GetTypeFromHandle, out methodSymbol, false, null))
			{
				return new BoundTypeOfOperator(node.Syntax, boundTypeExpression, null, typeSymbol, true);
			}
			return node.Update(boundTypeExpression, methodSymbol, typeSymbol);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000BA8B4 File Offset: 0x000B8AB4
		public override BoundNode VisitRefTypeOperator(BoundRefTypeOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Operand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			MethodSymbol methodSymbol;
			if (!this.TryGetWellKnownTypeMember<MethodSymbol>(node.Syntax, WellKnownMember.System_Type__GetTypeFromHandle, out methodSymbol, false, null))
			{
				return new BoundRefTypeOperator(node.Syntax, boundExpression, null, typeSymbol, true);
			}
			return node.Update(boundExpression, methodSymbol, typeSymbol);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000BA908 File Offset: 0x000B8B08
		public override BoundNode VisitTypeOrInstanceInitializers(BoundTypeOrInstanceInitializers node)
		{
			ImmutableArray<BoundStatement> statements = node.Statements;
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance(node.Statements.Length);
			foreach (BoundStatement boundStatement in statements)
			{
				if (LocalRewriter.IsFieldOrPropertyInitializer(boundStatement))
				{
					if (boundStatement.Kind == BoundKind.Block)
					{
						BoundBlock boundBlock = (BoundBlock)boundStatement;
						BoundStatement boundStatement2 = this.RewriteExpressionStatement((BoundExpressionStatement)boundBlock.Statements.Single<BoundStatement>(), true);
						instance.Add(boundBlock.Update(boundBlock.Locals, boundBlock.LocalFunctions, ImmutableArray.Create<BoundStatement>(boundStatement2)));
					}
					else
					{
						instance.Add(this.RewriteExpressionStatement((BoundExpressionStatement)boundStatement, true));
					}
				}
				else
				{
					instance.Add(this.VisitStatement(boundStatement));
				}
			}
			int num = 0;
			bool flag = this._compilation.Options.OptimizationLevel == OptimizationLevel.Release;
			for (int i = 0; i < instance.Count; i++)
			{
				BoundStatement boundStatement3 = instance[i];
				if (boundStatement3 == null || (flag && LocalRewriter.IsFieldOrPropertyInitializer(statements[i]) && LocalRewriter.ShouldOptimizeOutInitializer(boundStatement3)))
				{
					num++;
					MethodSymbol currentFunction = this._factory.CurrentFunction;
					if (currentFunction != null && !currentFunction.IsStatic)
					{
						instance[i] = null;
					}
				}
			}
			ImmutableArray<BoundStatement> immutableArray;
			if (num == instance.Count)
			{
				immutableArray = ImmutableArray<BoundStatement>.Empty;
				instance.Free();
			}
			else
			{
				int num2 = 0;
				for (int j = 0; j < instance.Count; j++)
				{
					BoundStatement boundStatement4 = instance[j];
					if (boundStatement4 != null)
					{
						if (LocalRewriter.IsFieldOrPropertyInitializer(statements[j]))
						{
							BoundStatement boundStatement5 = statements[j];
							if (this.Instrument && !boundStatement5.WasCompilerGenerated)
							{
								boundStatement4 = this._instrumenter.InstrumentFieldOrPropertyInitializer(boundStatement5, boundStatement4);
							}
						}
						instance[num2] = boundStatement4;
						num2++;
					}
				}
				instance.Count = num2;
				immutableArray = instance.ToImmutableAndFree();
			}
			return new BoundStatementList(node.Syntax, immutableArray, node.HasErrors);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000BAB04 File Offset: 0x000B8D04
		public override BoundNode VisitArrayAccess(BoundArrayAccess node)
		{
			if (node.Indices.Length != 1)
			{
				return base.VisitArrayAccess(node);
			}
			TypeSymbol typeSymbol = this.VisitType(node.Indices[0].Type);
			SyntheticBoundNodeFactory factory = this._factory;
			BoundNode boundNode;
			if (TypeSymbol.Equals(typeSymbol, this._compilation.GetWellKnownType(WellKnownType.System_Index), TypeCompareKind.ConsiderEverything))
			{
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = factory.StoreToTemp(this.VisitExpression(node.Expression), out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				bool flag;
				BoundExpression boundExpression = this.MakePatternIndexOffsetExpression(node.Indices[0], factory.ArrayLength(boundLocal), out flag);
				boundNode = factory.Sequence(ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), factory.ArrayAccess(boundLocal, ImmutableArray.Create<BoundExpression>(boundExpression)));
			}
			else if (TypeSymbol.Equals(typeSymbol, this._compilation.GetWellKnownType(WellKnownType.System_Range), TypeCompareKind.ConsiderEverything))
			{
				TypeWithAnnotations elementTypeWithAnnotations = ((ArrayTypeSymbol)node.Expression.Type).ElementTypeWithAnnotations;
				boundNode = factory.Call(null, factory.WellKnownMethod(WellKnownMember.System_Runtime_CompilerServices_RuntimeHelpers__GetSubArray_T).Construct(ImmutableArray.Create<TypeWithAnnotations>(elementTypeWithAnnotations)), ImmutableArray.Create<BoundExpression>(this.VisitExpression(node.Expression), this.VisitExpression(node.Indices[0])));
			}
			else
			{
				boundNode = base.VisitArrayAccess(node);
			}
			return boundNode;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000BAC48 File Offset: 0x000B8E48
		internal static bool IsFieldOrPropertyInitializer(BoundStatement initializer)
		{
			SyntaxNode syntax = initializer.Syntax;
			if (syntax.IsKind(SyntaxKind.Parameter))
			{
				return true;
			}
			ExpressionSyntax expressionSyntax = syntax as ExpressionSyntax;
			if (expressionSyntax != null)
			{
				CSharpSyntaxNode parent = expressionSyntax.Parent;
				if (parent != null && parent.Kind() == SyntaxKind.EqualsValueClause)
				{
					SyntaxKind syntaxKind = parent.Parent.Kind();
					if (syntaxKind == SyntaxKind.VariableDeclarator || syntaxKind == SyntaxKind.PropertyDeclaration)
					{
						BoundKind kind = initializer.Kind;
						if (kind != BoundKind.Block)
						{
							if (kind != BoundKind.ExpressionStatement)
							{
								return false;
							}
						}
						else
						{
							BoundBlock boundBlock = (BoundBlock)initializer;
							if (boundBlock.Statements.Length != 1)
							{
								return false;
							}
							initializer = boundBlock.Statements.First<BoundStatement>();
							if (initializer.Kind != BoundKind.ExpressionStatement)
							{
								return false;
							}
						}
						return ((BoundExpressionStatement)initializer).Expression.Kind == BoundKind.AssignmentOperator;
					}
				}
			}
			return false;
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000BAD10 File Offset: 0x000B8F10
		private static bool ShouldOptimizeOutInitializer(BoundStatement initializer)
		{
			if (initializer.Kind != BoundKind.ExpressionStatement)
			{
				return false;
			}
			BoundAssignmentOperator boundAssignmentOperator = ((BoundExpressionStatement)initializer).Expression as BoundAssignmentOperator;
			if (boundAssignmentOperator == null)
			{
				return false;
			}
			FieldSymbol fieldSymbol = ((BoundFieldAccess)boundAssignmentOperator.Left).FieldSymbol;
			return (fieldSymbol.IsStatic || !fieldSymbol.ContainingType.IsStructType()) && boundAssignmentOperator.Right.IsDefaultValue();
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000BAD74 File Offset: 0x000B8F74
		internal static bool CanBePassedByReference(BoundExpression expr)
		{
			if (expr.ConstantValue != null)
			{
				return false;
			}
			BoundKind kind = expr.Kind;
			if (kind <= BoundKind.ArrayAccess)
			{
				if (kind <= BoundKind.RefValueOperator)
				{
					if (kind == BoundKind.DeconstructValuePlaceholder)
					{
						return true;
					}
					if (kind - BoundKind.PointerIndirectionOperator > 1 && kind != BoundKind.RefValueOperator)
					{
						return false;
					}
				}
				else
				{
					if (kind == BoundKind.AssignmentOperator)
					{
						return ((BoundAssignmentOperator)expr).IsRef;
					}
					if (kind == BoundKind.ConditionalOperator)
					{
						return ((BoundConditionalOperator)expr).IsRef;
					}
					if (kind != BoundKind.ArrayAccess)
					{
						return false;
					}
				}
			}
			else if (kind <= BoundKind.Parameter)
			{
				if (kind != BoundKind.ThisReference && kind - BoundKind.Local > 1 && kind != BoundKind.Parameter)
				{
					return false;
				}
			}
			else if (kind <= BoundKind.Call)
			{
				if (kind == BoundKind.Sequence)
				{
					return LocalRewriter.CanBePassedByReference(((BoundSequence)expr).Value);
				}
				if (kind != BoundKind.Call)
				{
					return false;
				}
				return ((BoundCall)expr).Method.RefKind > RefKind.None;
			}
			else
			{
				switch (kind)
				{
				case BoundKind.FieldAccess:
				{
					BoundFieldAccess boundFieldAccess = (BoundFieldAccess)expr;
					return boundFieldAccess.FieldSymbol.IsStatic || LocalRewriter.CanBePassedByReference(boundFieldAccess.ReceiverOpt);
				}
				case BoundKind.HoistedFieldAccess:
					return false;
				case BoundKind.PropertyAccess:
					return ((BoundPropertyAccess)expr).PropertySymbol.RefKind > RefKind.None;
				case BoundKind.EventAccess:
				{
					BoundEventAccess boundEventAccess = (BoundEventAccess)expr;
					return boundEventAccess.IsUsableAsField && (boundEventAccess.EventSymbol.IsStatic || LocalRewriter.CanBePassedByReference(boundEventAccess.ReceiverOpt));
				}
				case BoundKind.IndexerAccess:
					return ((BoundIndexerAccess)expr).Indexer.RefKind > RefKind.None;
				case BoundKind.IndexOrRangePatternIndexerAccess:
				{
					BoundIndexOrRangePatternIndexerAccess boundIndexOrRangePatternIndexerAccess = (BoundIndexOrRangePatternIndexerAccess)expr;
					Symbol patternSymbol = boundIndexOrRangePatternIndexerAccess.PatternSymbol;
					PropertySymbol propertySymbol = patternSymbol as PropertySymbol;
					RefKind refKind;
					if (propertySymbol == null)
					{
						MethodSymbol methodSymbol = patternSymbol as MethodSymbol;
						if (methodSymbol == null)
						{
							throw ExceptionUtilities.UnexpectedValue(boundIndexOrRangePatternIndexerAccess.PatternSymbol);
						}
						refKind = methodSymbol.RefKind;
					}
					else
					{
						refKind = propertySymbol.RefKind;
					}
					return refKind > RefKind.None;
				}
				default:
					if (kind != BoundKind.DiscardExpression)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000BAF54 File Offset: 0x000B9154
		private void CheckRefReadOnlySymbols(MethodSymbol symbol)
		{
			if (!symbol.ReturnsByRefReadonly)
			{
				if (!symbol.Parameters.Any<ParameterSymbol>((ParameterSymbol p) => p.RefKind == RefKind.In))
				{
					return;
				}
			}
			PEModuleBuilder moduleBuilderOpt = this._factory.CompilationState.ModuleBuilderOpt;
			if (moduleBuilderOpt == null)
			{
				return;
			}
			moduleBuilderOpt.EnsureIsReadOnlyAttributeExists();
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000BAFAF File Offset: 0x000B91AF
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private CompoundUseSiteInfo<AssemblySymbol> GetNewCompoundUseSiteInfo()
		{
			return new CompoundUseSiteInfo<AssemblySymbol>(this._diagnostics, this._compilation.Assembly);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000BAFC8 File Offset: 0x000B91C8
		public override BoundNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			return new BoundObjectCreationExpression(node.Syntax, node.Constructor, immutableArray, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), default(BitVector), null, null, node.Type, false);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000BB024 File Offset: 0x000B9224
		public override BoundNode VisitAsOperator(BoundAsOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Operand);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.VisitTypeExpression(node.TargetType);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return this.MakeAsOperator(node, node.Syntax, boundExpression, boundTypeExpression, node.Conversion, typeSymbol);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000BB073 File Offset: 0x000B9273
		public override BoundNode VisitTypeExpression(BoundTypeExpression node)
		{
			return base.VisitTypeExpression(node);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000BB07C File Offset: 0x000B927C
		private BoundExpression MakeAsOperator(BoundAsOperator oldNode, SyntaxNode syntax, BoundExpression rewrittenOperand, BoundTypeExpression rewrittenTargetType, Conversion conversion, TypeSymbol rewrittenType)
		{
			if (!this._inExpressionLambda)
			{
				ConstantValue asOperatorConstantResult = Binder.GetAsOperatorConstantResult(rewrittenOperand.Type, rewrittenType, conversion.Kind, rewrittenOperand.ConstantValue);
				if (asOperatorConstantResult != null)
				{
					BoundExpression boundExpression = (rewrittenType.IsNullableType() ? new BoundDefaultExpression(syntax, rewrittenType, false) : this.MakeLiteral(syntax, asOperatorConstantResult, rewrittenType, null));
					if (rewrittenOperand.ConstantValue != null)
					{
						return boundExpression;
					}
					return new BoundSequence(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(rewrittenOperand), boundExpression, rewrittenType, false);
				}
				else if (conversion.IsImplicit)
				{
					return this.MakeConversionNode(syntax, rewrittenOperand, conversion, rewrittenType, false, false, null);
				}
			}
			return oldNode.Update(rewrittenOperand, rewrittenTargetType, conversion, rewrittenType);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000BB124 File Offset: 0x000B9324
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			return this.VisitAssignmentOperator(node, true);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000BB130 File Offset: 0x000B9330
		private BoundExpression VisitAssignmentOperator(BoundAssignmentOperator node, bool used)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Right);
			BoundExpression left = node.Left;
			BoundKind kind = left.Kind;
			if (kind != BoundKind.DynamicMemberAccess)
			{
				BoundExpression boundExpression2;
				switch (kind)
				{
				case BoundKind.PropertyAccess:
					boundExpression2 = this.VisitPropertyAccess((BoundPropertyAccess)left, true);
					goto IL_0131;
				case BoundKind.EventAccess:
				{
					BoundEventAccess boundEventAccess = (BoundEventAccess)left;
					if (boundEventAccess.EventSymbol.IsWindowsRuntimeEvent)
					{
						return this.VisitWindowsRuntimeEventFieldAssignmentOperator(node.Syntax, boundEventAccess, boundExpression);
					}
					break;
				}
				case BoundKind.IndexerAccess:
					boundExpression2 = this.VisitIndexerAccess((BoundIndexerAccess)left, true);
					goto IL_0131;
				case BoundKind.IndexOrRangePatternIndexerAccess:
					boundExpression2 = this.VisitIndexOrRangePatternIndexerAccess((BoundIndexOrRangePatternIndexerAccess)left, true);
					goto IL_0131;
				case BoundKind.DynamicIndexerAccess:
				{
					BoundDynamicIndexerAccess boundDynamicIndexerAccess = (BoundDynamicIndexerAccess)left;
					BoundExpression boundExpression3 = this.VisitExpression(boundDynamicIndexerAccess.Receiver);
					ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(boundDynamicIndexerAccess.Arguments);
					return this.MakeDynamicSetIndex(boundDynamicIndexerAccess, boundExpression3, immutableArray, boundDynamicIndexerAccess.ArgumentNamesOpt, boundDynamicIndexerAccess.ArgumentRefKindsOpt, boundExpression, false, false);
				}
				}
				boundExpression2 = this.VisitExpression(left);
				IL_0131:
				return this.MakeStaticAssignmentOperator(node.Syntax, boundExpression2, boundExpression, node.IsRef, node.Type, used);
			}
			BoundDynamicMemberAccess boundDynamicMemberAccess = (BoundDynamicMemberAccess)left;
			BoundExpression boundExpression4 = this.VisitExpression(boundDynamicMemberAccess.Receiver);
			return this._dynamicFactory.MakeDynamicSetMember(boundExpression4, boundDynamicMemberAccess.Name, boundExpression, false, false).ToExpression();
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000BB28C File Offset: 0x000B948C
		private BoundExpression MakeAssignmentOperator(SyntaxNode syntax, BoundExpression rewrittenLeft, BoundExpression rewrittenRight, TypeSymbol type, bool used, bool isChecked, bool isCompoundAssignment)
		{
			BoundKind kind = rewrittenLeft.Kind;
			if (kind == BoundKind.DynamicMemberAccess)
			{
				BoundDynamicMemberAccess boundDynamicMemberAccess = (BoundDynamicMemberAccess)rewrittenLeft;
				return this._dynamicFactory.MakeDynamicSetMember(boundDynamicMemberAccess.Receiver, boundDynamicMemberAccess.Name, rewrittenRight, isCompoundAssignment, isChecked).ToExpression();
			}
			if (kind != BoundKind.EventAccess)
			{
				if (kind == BoundKind.DynamicIndexerAccess)
				{
					BoundDynamicIndexerAccess boundDynamicIndexerAccess = (BoundDynamicIndexerAccess)rewrittenLeft;
					return this.MakeDynamicSetIndex(boundDynamicIndexerAccess, boundDynamicIndexerAccess.Receiver, boundDynamicIndexerAccess.Arguments, boundDynamicIndexerAccess.ArgumentNamesOpt, boundDynamicIndexerAccess.ArgumentRefKindsOpt, rewrittenRight, isCompoundAssignment, isChecked);
				}
				return this.MakeStaticAssignmentOperator(syntax, rewrittenLeft, rewrittenRight, false, type, used);
			}
			else
			{
				BoundEventAccess boundEventAccess = (BoundEventAccess)rewrittenLeft;
				if (boundEventAccess.EventSymbol.IsWindowsRuntimeEvent)
				{
					return this.RewriteWindowsRuntimeEventAssignmentOperator(boundEventAccess.Syntax, boundEventAccess.EventSymbol, LocalRewriter.EventAssignmentKind.Assignment, false, boundEventAccess.ReceiverOpt, rewrittenRight);
				}
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000BB358 File Offset: 0x000B9558
		private BoundExpression MakeDynamicSetIndex(BoundDynamicIndexerAccess indexerAccess, BoundExpression loweredReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds, BoundExpression loweredRight, bool isCompoundAssignment = false, bool isChecked = false)
		{
			this.EmbedIfNeedTo(loweredReceiver, indexerAccess.ApplicableIndexers, indexerAccess.Syntax);
			return this._dynamicFactory.MakeDynamicSetIndex(this.MakeDynamicIndexerAccessReceiver(indexerAccess, loweredReceiver), loweredArguments, argumentNames, refKinds, loweredRight, isCompoundAssignment, isChecked).ToExpression();
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000BB3A0 File Offset: 0x000B95A0
		private BoundExpression MakeStaticAssignmentOperator(SyntaxNode syntax, BoundExpression rewrittenLeft, BoundExpression rewrittenRight, bool isRef, TypeSymbol type, bool used)
		{
			BoundKind kind = rewrittenLeft.Kind;
			if (kind > BoundKind.Sequence)
			{
				if (kind != BoundKind.DynamicMemberAccess)
				{
					switch (kind)
					{
					case BoundKind.PropertyAccess:
					{
						BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)rewrittenLeft;
						BoundExpression receiverOpt = boundPropertyAccess.ReceiverOpt;
						PropertySymbol propertySymbol = boundPropertyAccess.PropertySymbol;
						return this.MakePropertyAssignment(syntax, receiverOpt, propertySymbol, ImmutableArray<BoundExpression>.Empty, default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), rewrittenRight, type, used);
					}
					case BoundKind.EventAccess:
					case BoundKind.IndexOrRangePatternIndexerAccess:
						goto IL_015F;
					case BoundKind.IndexerAccess:
					{
						BoundIndexerAccess boundIndexerAccess = (BoundIndexerAccess)rewrittenLeft;
						BoundExpression receiverOpt2 = boundIndexerAccess.ReceiverOpt;
						ImmutableArray<BoundExpression> arguments = boundIndexerAccess.Arguments;
						PropertySymbol indexer = boundIndexerAccess.Indexer;
						return this.MakePropertyAssignment(syntax, receiverOpt2, indexer, arguments, boundIndexerAccess.ArgumentRefKindsOpt, boundIndexerAccess.Expanded, boundIndexerAccess.ArgsToParamsOpt, rewrittenRight, type, used);
					}
					case BoundKind.DynamicIndexerAccess:
						break;
					default:
						if (kind != BoundKind.DiscardExpression)
						{
							goto IL_015F;
						}
						return rewrittenRight;
					}
				}
				throw ExceptionUtilities.UnexpectedValue(rewrittenLeft.Kind);
			}
			if (kind == BoundKind.Local)
			{
				return new BoundAssignmentOperator(syntax, rewrittenLeft, rewrittenRight, type, isRef, false);
			}
			if (kind == BoundKind.Parameter)
			{
				return new BoundAssignmentOperator(syntax, rewrittenLeft, rewrittenRight, isRef, type, false);
			}
			if (kind == BoundKind.Sequence)
			{
				BoundSequence boundSequence = (BoundSequence)rewrittenLeft;
				if (boundSequence.Value.Kind == BoundKind.IndexerAccess)
				{
					return boundSequence.Update(boundSequence.Locals, boundSequence.SideEffects, this.MakeStaticAssignmentOperator(syntax, boundSequence.Value, rewrittenRight, isRef, type, used), type);
				}
			}
			IL_015F:
			return new BoundAssignmentOperator(syntax, rewrittenLeft, rewrittenRight, type, false, false);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000BB518 File Offset: 0x000B9718
		private BoundExpression MakePropertyAssignment(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, PropertySymbol property, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BoundExpression rewrittenRight, TypeSymbol type, bool used)
		{
			MethodSymbol ownOrInheritedSetMethod = property.GetOwnOrInheritedSetMethod();
			if (ownOrInheritedSetMethod == null)
			{
				SynthesizedBackingFieldSymbol backingField = ((SourcePropertySymbolBase)property.OriginalDefinition).BackingField;
				return this._factory.AssignmentExpression(this._factory.Field(rewrittenReceiver, backingField), rewrittenRight, false);
			}
			ImmutableArray<LocalSymbol> immutableArray;
			rewrittenArguments = this.MakeArguments(syntax, rewrittenArguments, property, expanded, argsToParamsOpt, ref argumentRefKindsOpt, out immutableArray, false, ThreeState.True);
			if (used)
			{
				TypeSymbol type2 = rewrittenRight.Type;
				LocalSymbol localSymbol = this._factory.SynthesizedLocal(type2, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				BoundExpression boundExpression = new BoundLocal(syntax, localSymbol, null, type2, false);
				BoundExpression boundExpression2 = new BoundAssignmentOperator(syntax, boundExpression, rewrittenRight, type2, false, false);
				BoundExpression boundExpression3 = BoundCall.Synthesized(syntax, rewrittenReceiver, ownOrInheritedSetMethod, LocalRewriter.AppendToPossibleNull<BoundExpression>(rewrittenArguments, boundExpression2));
				return new BoundSequence(syntax, LocalRewriter.AppendToPossibleNull<LocalSymbol>(immutableArray, localSymbol), ImmutableArray.Create<BoundExpression>(boundExpression3), boundExpression, type, false);
			}
			BoundCall boundCall = BoundCall.Synthesized(syntax, rewrittenReceiver, ownOrInheritedSetMethod, LocalRewriter.AppendToPossibleNull<BoundExpression>(rewrittenArguments, rewrittenRight));
			if (immutableArray.IsDefaultOrEmpty)
			{
				return boundCall;
			}
			return new BoundSequence(syntax, immutableArray, ImmutableArray<BoundExpression>.Empty, boundCall, ownOrInheritedSetMethod.ReturnType, false);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000BB610 File Offset: 0x000B9810
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<T> AppendToPossibleNull<T>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<T> possibleNull, T newElement)
		{
			return possibleNull.NullToEmpty<T>().Add(newElement);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000BB62C File Offset: 0x000B982C
		public override BoundNode VisitAwaitExpression(BoundAwaitExpression node)
		{
			return this.VisitAwaitExpression(node, true);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000BB636 File Offset: 0x000B9836
		public BoundExpression VisitAwaitExpression(BoundAwaitExpression node, bool used)
		{
			return this.RewriteAwaitExpression((BoundExpression)base.VisitAwaitExpression(node), used);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000BB64B File Offset: 0x000B984B
		private BoundExpression RewriteAwaitExpression(SyntaxNode syntax, BoundExpression rewrittenExpression, BoundAwaitableInfo awaitableInfo, TypeSymbol type, bool used)
		{
			return this.RewriteAwaitExpression(new BoundAwaitExpression(syntax, rewrittenExpression, awaitableInfo, type, false)
			{
				WasCompilerGenerated = true
			}, used);
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000BB668 File Offset: 0x000B9868
		private BoundExpression RewriteAwaitExpression(BoundExpression rewrittenAwait, bool used)
		{
			this._sawAwait = true;
			if (!used)
			{
				return rewrittenAwait;
			}
			this._needsSpilling = true;
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(rewrittenAwait, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.Spill, rewrittenAwait.Syntax);
			return new BoundSpillSequence(rewrittenAwait.Syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundLocal, boundLocal.Type, false);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000BB6C4 File Offset: 0x000B98C4
		public override BoundNode VisitBinaryOperator(BoundBinaryOperator node)
		{
			return this.VisitBinaryOperator(node, null);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000BB6D0 File Offset: 0x000B98D0
		public override BoundNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node)
		{
			SyntaxNode syntax = node.Syntax;
			BinaryOperatorKind operatorKind = node.OperatorKind;
			TypeSymbol type = node.Type;
			BoundExpression boundExpression = this.VisitExpression(node.Left);
			BoundExpression boundExpression2 = this.VisitExpression(node.Right);
			if (this._inExpressionLambda)
			{
				return node.Update(operatorKind, node.LogicalOperator, node.TrueOperator, node.FalseOperator, node.ResultKind, boundExpression, boundExpression2, type);
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundCall boundCall = BoundCall.Synthesized(syntax, null, (operatorKind.Operator() == BinaryOperatorKind.And) ? node.FalseOperator : node.TrueOperator, boundLocal);
			BoundExpression boundExpression3 = this.LowerUserDefinedBinaryOperator(syntax, operatorKind & ~BinaryOperatorKind.Logical, boundLocal, boundExpression2, type, node.LogicalOperator);
			BoundExpression boundExpression4 = LocalRewriter.RewriteConditionalOperator(syntax, boundCall, boundLocal, boundExpression3, null, type, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression4, type, false);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000BB7BC File Offset: 0x000B99BC
		public BoundExpression VisitBinaryOperator(BoundBinaryOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] BoundUnaryOperator applyParentUnaryOperator)
		{
			ArrayBuilder<BoundBinaryOperator> instance = ArrayBuilder<BoundBinaryOperator>.GetInstance();
			BoundBinaryOperator boundBinaryOperator = node;
			while (boundBinaryOperator != null && boundBinaryOperator.ConstantValue == null)
			{
				instance.Push(boundBinaryOperator);
				boundBinaryOperator = boundBinaryOperator.Left as BoundBinaryOperator;
			}
			BoundExpression boundExpression = this.VisitExpression(instance.Peek<BoundBinaryOperator>().Left);
			while (instance.Count > 0)
			{
				BoundBinaryOperator boundBinaryOperator2 = instance.Pop<BoundBinaryOperator>();
				BoundExpression boundExpression2 = this.VisitExpression(boundBinaryOperator2.Right);
				boundExpression = this.MakeBinaryOperator(boundBinaryOperator2, boundBinaryOperator2.Syntax, boundBinaryOperator2.OperatorKind, boundExpression, boundExpression2, boundBinaryOperator2.Type, boundBinaryOperator2.MethodOpt, false, false, (instance.Count == 0) ? applyParentUnaryOperator : null);
			}
			instance.Free();
			return boundExpression;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000BB864 File Offset: 0x000B9A64
		private BoundExpression MakeBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, bool isPointerElementAccess = false, bool isCompoundAssignment = false, [global::System.Runtime.CompilerServices.Nullable(2)] BoundUnaryOperator applyParentUnaryOperator = null)
		{
			return this.MakeBinaryOperator(null, syntax, operatorKind, loweredLeft, loweredRight, type, method, isPointerElementAccess, isCompoundAssignment, applyParentUnaryOperator);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000BB888 File Offset: 0x000B9A88
		private BoundExpression MakeBinaryOperator([global::System.Runtime.CompilerServices.Nullable(2)] BoundBinaryOperator oldNode, SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, bool isPointerElementAccess = false, bool isCompoundAssignment = false, [global::System.Runtime.CompilerServices.Nullable(2)] BoundUnaryOperator applyParentUnaryOperator = null)
		{
			if (this._inExpressionLambda)
			{
				BinaryOperatorKind binaryOperatorKind = operatorKind.Operator() | operatorKind.OperandTypes();
				if (binaryOperatorKind <= BinaryOperatorKind.DelegateCombination)
				{
					if (binaryOperatorKind - BinaryOperatorKind.StringConcatenation <= 2)
					{
						return this.RewriteStringConcatenation(syntax, operatorKind, loweredLeft, loweredRight, type);
					}
					if (binaryOperatorKind == BinaryOperatorKind.DelegateCombination)
					{
						return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__Combine);
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.DelegateRemoval)
					{
						return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__Remove);
					}
					if (binaryOperatorKind == BinaryOperatorKind.DelegateEqual)
					{
						return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__op_Equality);
					}
					if (binaryOperatorKind == BinaryOperatorKind.DelegateNotEqual)
					{
						return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__op_Inequality);
					}
				}
			}
			else if (operatorKind.IsDynamic())
			{
				if (operatorKind.IsLogical())
				{
					return this.MakeDynamicLogicalBinaryOperator(syntax, operatorKind, loweredLeft, loweredRight, method, type, isCompoundAssignment, applyParentUnaryOperator);
				}
				return this._dynamicFactory.MakeDynamicBinaryOperator(operatorKind, loweredLeft, loweredRight, isCompoundAssignment, type).ToExpression();
			}
			else
			{
				if (operatorKind.IsLifted())
				{
					return this.RewriteLiftedBinaryOperator(syntax, operatorKind, loweredLeft, loweredRight, type, method);
				}
				if (operatorKind.IsUserDefined())
				{
					return this.LowerUserDefinedBinaryOperator(syntax, operatorKind, loweredLeft, loweredRight, type, method);
				}
				BinaryOperatorKind binaryOperatorKind = operatorKind.OperatorWithLogical() | operatorKind.OperandTypes();
				if (binaryOperatorKind <= BinaryOperatorKind.DelegateEqual)
				{
					if (binaryOperatorKind > BinaryOperatorKind.PointerAndULongSubtraction)
					{
						if (binaryOperatorKind <= BinaryOperatorKind.ULongLeftShift)
						{
							if (binaryOperatorKind <= BinaryOperatorKind.DecimalRemainder)
							{
								if (binaryOperatorKind != BinaryOperatorKind.DecimalDivision && binaryOperatorKind != BinaryOperatorKind.DecimalRemainder)
								{
									goto IL_09D1;
								}
								goto IL_07E7;
							}
							else if (binaryOperatorKind - BinaryOperatorKind.IntLeftShift > 1)
							{
								if (binaryOperatorKind - BinaryOperatorKind.LongLeftShift > 1)
								{
									goto IL_09D1;
								}
								goto IL_07D5;
							}
						}
						else if (binaryOperatorKind <= BinaryOperatorKind.ULongRightShift)
						{
							if (binaryOperatorKind - BinaryOperatorKind.IntRightShift > 1)
							{
								if (binaryOperatorKind - BinaryOperatorKind.LongRightShift > 1)
								{
									goto IL_09D1;
								}
								goto IL_07D5;
							}
						}
						else
						{
							if (binaryOperatorKind == BinaryOperatorKind.IntEqual)
							{
								goto IL_0989;
							}
							switch (binaryOperatorKind)
							{
							case BinaryOperatorKind.DecimalEqual:
								goto IL_07E7;
							case BinaryOperatorKind.BoolEqual:
								if (loweredLeft.ConstantValue == ConstantValue.True)
								{
									return loweredRight;
								}
								if (loweredRight.ConstantValue == ConstantValue.True)
								{
									return loweredLeft;
								}
								if (loweredLeft.ConstantValue == ConstantValue.False)
								{
									return this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, loweredRight, loweredRight.Type);
								}
								if (loweredRight.ConstantValue == ConstantValue.False)
								{
									return this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, loweredLeft, loweredLeft.Type);
								}
								goto IL_09D1;
							case BinaryOperatorKind.ObjectEqual:
								goto IL_09D1;
							case BinaryOperatorKind.StringEqual:
								return this.RewriteStringEquality(oldNode, syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_String__op_Equality);
							default:
								if (binaryOperatorKind != BinaryOperatorKind.DelegateEqual)
								{
									goto IL_09D1;
								}
								return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__op_Equality);
							}
						}
						return this.RewriteBuiltInShiftOperation(oldNode, syntax, operatorKind, loweredLeft, loweredRight, type, 31);
						IL_07D5:
						return this.RewriteBuiltInShiftOperation(oldNode, syntax, operatorKind, loweredLeft, loweredRight, type, 63);
					}
					if (binaryOperatorKind <= BinaryOperatorKind.ULongAndPointerAddition)
					{
						if (binaryOperatorKind <= BinaryOperatorKind.DecimalMultiplication)
						{
							if (binaryOperatorKind - BinaryOperatorKind.IntMultiplication > 3)
							{
								if (binaryOperatorKind != BinaryOperatorKind.DecimalMultiplication)
								{
									goto IL_09D1;
								}
								goto IL_07E7;
							}
							else
							{
								if (loweredLeft.IsDefaultValue())
								{
									return this._factory.MakeSequence(new BoundExpression[] { loweredRight, loweredLeft });
								}
								if (loweredRight.IsDefaultValue())
								{
									return this._factory.MakeSequence(new BoundExpression[] { loweredLeft, loweredRight });
								}
								ConstantValue constantValue = loweredLeft.ConstantValue;
								if (constantValue != null && constantValue.UInt64Value == 1UL)
								{
									return loweredRight;
								}
								ConstantValue constantValue2 = loweredRight.ConstantValue;
								if (constantValue2 != null && constantValue2.UInt64Value == 1UL)
								{
									return loweredLeft;
								}
								goto IL_09D1;
							}
						}
						else
						{
							switch (binaryOperatorKind)
							{
							case BinaryOperatorKind.IntAddition:
							case BinaryOperatorKind.UIntAddition:
							case BinaryOperatorKind.LongAddition:
							case BinaryOperatorKind.ULongAddition:
								if (loweredLeft.IsDefaultValue())
								{
									return loweredRight;
								}
								if (loweredRight.IsDefaultValue())
								{
									return loweredLeft;
								}
								goto IL_09D1;
							case BinaryOperatorKind.NIntAddition:
							case BinaryOperatorKind.NUIntAddition:
							case (BinaryOperatorKind)4363:
							case BinaryOperatorKind.FloatAddition:
							case BinaryOperatorKind.DoubleAddition:
							case (BinaryOperatorKind)4367:
							case (BinaryOperatorKind)4368:
								goto IL_09D1;
							case BinaryOperatorKind.DecimalAddition:
								goto IL_07E7;
							case BinaryOperatorKind.StringConcatenation:
							case BinaryOperatorKind.StringAndObjectConcatenation:
							case BinaryOperatorKind.ObjectAndStringConcatenation:
								return this.RewriteStringConcatenation(syntax, operatorKind, loweredLeft, loweredRight, type);
							default:
								switch (binaryOperatorKind)
								{
								case BinaryOperatorKind.DelegateCombination:
									return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__Combine);
								case (BinaryOperatorKind)4376:
								case (BinaryOperatorKind)4378:
								case (BinaryOperatorKind)4379:
								case (BinaryOperatorKind)4380:
								case (BinaryOperatorKind)4381:
								case (BinaryOperatorKind)4382:
								case (BinaryOperatorKind)4383:
									goto IL_09D1;
								case BinaryOperatorKind.PointerAndIntAddition:
								case BinaryOperatorKind.PointerAndUIntAddition:
								case BinaryOperatorKind.PointerAndLongAddition:
								case BinaryOperatorKind.PointerAndULongAddition:
									break;
								case BinaryOperatorKind.IntAndPointerAddition:
								case BinaryOperatorKind.UIntAndPointerAddition:
								case BinaryOperatorKind.LongAndPointerAddition:
								case BinaryOperatorKind.ULongAndPointerAddition:
									if (loweredLeft.IsDefaultValue())
									{
										return loweredRight;
									}
									return this.RewritePointerNumericOperator(syntax, operatorKind, loweredLeft, loweredRight, type, isPointerElementAccess, false);
								default:
									goto IL_09D1;
								}
								break;
							}
						}
					}
					else if (binaryOperatorKind <= BinaryOperatorKind.DecimalSubtraction)
					{
						if (binaryOperatorKind - BinaryOperatorKind.IntSubtraction > 3)
						{
							if (binaryOperatorKind != BinaryOperatorKind.DecimalSubtraction)
							{
								goto IL_09D1;
							}
							goto IL_07E7;
						}
						else
						{
							if (loweredRight.IsDefaultValue())
							{
								return loweredLeft;
							}
							goto IL_09D1;
						}
					}
					else
					{
						switch (binaryOperatorKind)
						{
						case BinaryOperatorKind.DelegateRemoval:
							return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__Remove);
						case BinaryOperatorKind.PointerSubtraction:
							return this.RewritePointerSubtraction(operatorKind, loweredLeft, loweredRight, type);
						case BinaryOperatorKind.PointerAndIntSubtraction:
							break;
						default:
							if (binaryOperatorKind - BinaryOperatorKind.PointerAndUIntSubtraction > 2)
							{
								goto IL_09D1;
							}
							break;
						}
					}
					if (loweredRight.IsDefaultValue())
					{
						return loweredLeft;
					}
					return this.RewritePointerNumericOperator(syntax, operatorKind, loweredLeft, loweredRight, type, isPointerElementAccess, true);
				}
				else
				{
					if (binaryOperatorKind <= BinaryOperatorKind.DecimalLessThan)
					{
						if (binaryOperatorKind <= BinaryOperatorKind.DelegateNotEqual)
						{
							if (binaryOperatorKind <= BinaryOperatorKind.IntNotEqual)
							{
								if (binaryOperatorKind != BinaryOperatorKind.NullableNullEqual)
								{
									if (binaryOperatorKind != BinaryOperatorKind.IntNotEqual)
									{
										goto IL_09D1;
									}
									goto IL_0989;
								}
							}
							else
							{
								switch (binaryOperatorKind)
								{
								case BinaryOperatorKind.DecimalNotEqual:
									goto IL_07E7;
								case BinaryOperatorKind.BoolNotEqual:
									if (loweredLeft.ConstantValue == ConstantValue.False)
									{
										return loweredRight;
									}
									if (loweredRight.ConstantValue == ConstantValue.False)
									{
										return loweredLeft;
									}
									if (loweredLeft.ConstantValue == ConstantValue.True)
									{
										return this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, loweredRight, loweredRight.Type);
									}
									if (loweredRight.ConstantValue == ConstantValue.True)
									{
										return this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, loweredLeft, loweredLeft.Type);
									}
									goto IL_09D1;
								case BinaryOperatorKind.ObjectNotEqual:
									goto IL_09D1;
								case BinaryOperatorKind.StringNotEqual:
									return this.RewriteStringEquality(oldNode, syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_String__op_Inequality);
								default:
									if (binaryOperatorKind != BinaryOperatorKind.DelegateNotEqual)
									{
										goto IL_09D1;
									}
									return this.RewriteDelegateOperation(syntax, operatorKind, loweredLeft, loweredRight, type, SpecialMember.System_Delegate__op_Inequality);
								}
							}
						}
						else if (binaryOperatorKind <= BinaryOperatorKind.IntGreaterThan)
						{
							if (binaryOperatorKind != BinaryOperatorKind.NullableNullNotEqual)
							{
								if (binaryOperatorKind != BinaryOperatorKind.IntGreaterThan)
								{
									goto IL_09D1;
								}
								goto IL_08ED;
							}
						}
						else
						{
							if (binaryOperatorKind == BinaryOperatorKind.DecimalGreaterThan)
							{
								goto IL_07E7;
							}
							if (binaryOperatorKind == BinaryOperatorKind.IntLessThan)
							{
								goto IL_093E;
							}
							if (binaryOperatorKind != BinaryOperatorKind.DecimalLessThan)
							{
								goto IL_09D1;
							}
							goto IL_07E7;
						}
						return this.RewriteNullableNullEquality(syntax, operatorKind, loweredLeft, loweredRight, type);
					}
					if (binaryOperatorKind <= BinaryOperatorKind.DecimalLessThanOrEqual)
					{
						if (binaryOperatorKind <= BinaryOperatorKind.DecimalGreaterThanOrEqual)
						{
							if (binaryOperatorKind == BinaryOperatorKind.IntGreaterThanOrEqual)
							{
								goto IL_093E;
							}
							if (binaryOperatorKind != BinaryOperatorKind.DecimalGreaterThanOrEqual)
							{
								goto IL_09D1;
							}
							goto IL_07E7;
						}
						else if (binaryOperatorKind != BinaryOperatorKind.IntLessThanOrEqual)
						{
							if (binaryOperatorKind != BinaryOperatorKind.DecimalLessThanOrEqual)
							{
								goto IL_09D1;
							}
							goto IL_07E7;
						}
					}
					else if (binaryOperatorKind <= BinaryOperatorKind.BoolXor)
					{
						if (binaryOperatorKind != BinaryOperatorKind.BoolAnd)
						{
							if (binaryOperatorKind != BinaryOperatorKind.BoolXor)
							{
								goto IL_09D1;
							}
							if (loweredLeft.ConstantValue == ConstantValue.False)
							{
								return loweredRight;
							}
							if (loweredRight.ConstantValue == ConstantValue.False)
							{
								return loweredLeft;
							}
							if (loweredLeft.ConstantValue == ConstantValue.True)
							{
								return this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, loweredRight, loweredRight.Type);
							}
							if (loweredRight.ConstantValue == ConstantValue.True)
							{
								return this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, loweredLeft, loweredLeft.Type);
							}
							goto IL_09D1;
						}
						else
						{
							if (loweredRight.ConstantValue == ConstantValue.True)
							{
								return loweredLeft;
							}
							if (loweredLeft.ConstantValue == ConstantValue.True)
							{
								return loweredRight;
							}
							if (loweredLeft.IsDefaultValue())
							{
								return this._factory.MakeSequence(new BoundExpression[] { loweredRight, loweredLeft });
							}
							if (loweredRight.IsDefaultValue())
							{
								return this._factory.MakeSequence(new BoundExpression[] { loweredLeft, loweredRight });
							}
							goto IL_09D1;
						}
					}
					else if (binaryOperatorKind != BinaryOperatorKind.BoolOr)
					{
						if (binaryOperatorKind != BinaryOperatorKind.LogicalBoolAnd)
						{
							if (binaryOperatorKind != BinaryOperatorKind.LogicalBoolOr)
							{
								goto IL_09D1;
							}
							if (loweredRight.ConstantValue == ConstantValue.False)
							{
								return loweredLeft;
							}
							if (loweredLeft.ConstantValue == ConstantValue.False)
							{
								return loweredRight;
							}
							if (loweredLeft.ConstantValue == ConstantValue.True)
							{
								return loweredLeft;
							}
							if (loweredRight.Kind == BoundKind.Local || loweredRight.Kind == BoundKind.Parameter)
							{
								operatorKind &= ~BinaryOperatorKind.Logical;
								goto IL_09D1;
							}
							goto IL_09D1;
						}
						else
						{
							if (loweredRight.ConstantValue == ConstantValue.True)
							{
								return loweredLeft;
							}
							if (loweredLeft.ConstantValue == ConstantValue.True)
							{
								return loweredRight;
							}
							if (loweredLeft.ConstantValue == ConstantValue.False)
							{
								return loweredLeft;
							}
							if (loweredRight.Kind == BoundKind.Local || loweredRight.Kind == BoundKind.Parameter)
							{
								operatorKind &= ~BinaryOperatorKind.Logical;
								goto IL_09D1;
							}
							goto IL_09D1;
						}
					}
					else
					{
						if (loweredRight.ConstantValue == ConstantValue.False)
						{
							return loweredLeft;
						}
						if (loweredLeft.ConstantValue == ConstantValue.False)
						{
							return loweredRight;
						}
						goto IL_09D1;
					}
					IL_08ED:
					if (loweredLeft.Kind == BoundKind.ArrayLength && loweredRight.IsDefaultValue())
					{
						BinaryOperatorKind binaryOperatorKind2 = ((operatorKind == BinaryOperatorKind.IntGreaterThan) ? BinaryOperatorKind.NotEqual : BinaryOperatorKind.Equal);
						operatorKind &= ~BinaryOperatorKind.OpMask;
						operatorKind |= binaryOperatorKind2;
						loweredLeft = this.UnconvertArrayLength((BoundArrayLength)loweredLeft);
						goto IL_09D1;
					}
					goto IL_09D1;
					IL_093E:
					if (loweredRight.Kind == BoundKind.ArrayLength && loweredLeft.IsDefaultValue())
					{
						BinaryOperatorKind binaryOperatorKind3 = ((operatorKind == BinaryOperatorKind.IntLessThan) ? BinaryOperatorKind.NotEqual : BinaryOperatorKind.Equal);
						operatorKind &= ~BinaryOperatorKind.OpMask;
						operatorKind |= binaryOperatorKind3;
						loweredRight = this.UnconvertArrayLength((BoundArrayLength)loweredRight);
						goto IL_09D1;
					}
					goto IL_09D1;
				}
				IL_07E7:
				return this.RewriteDecimalBinaryOperation(syntax, loweredLeft, loweredRight, operatorKind);
				IL_0989:
				if (loweredLeft.Kind == BoundKind.ArrayLength && loweredRight.IsDefaultValue())
				{
					loweredLeft = this.UnconvertArrayLength((BoundArrayLength)loweredLeft);
				}
				else if (loweredRight.Kind == BoundKind.ArrayLength && loweredLeft.IsDefaultValue())
				{
					loweredRight = this.UnconvertArrayLength((BoundArrayLength)loweredRight);
				}
			}
			IL_09D1:
			if (oldNode == null)
			{
				return new BoundBinaryOperator(syntax, operatorKind, null, null, LookupResultKind.Viable, loweredLeft, loweredRight, type, false);
			}
			return oldNode.Update(operatorKind, oldNode.ConstantValueOpt, oldNode.MethodOpt, oldNode.ResultKind, loweredLeft, loweredRight, type);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000BC29C File Offset: 0x000BA49C
		private BoundExpression RewriteLiftedBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			BoundLoweredConditionalAccess boundLoweredConditionalAccess = loweredLeft as BoundLoweredConditionalAccess;
			bool flag = boundLoweredConditionalAccess != null && operatorKind != BinaryOperatorKind.LiftedBoolOr && operatorKind != BinaryOperatorKind.LiftedBoolAnd && !LocalRewriter.ReadIsSideeffecting(loweredRight) && (boundLoweredConditionalAccess.WhenNullOpt == null || boundLoweredConditionalAccess.WhenNullOpt.IsDefaultValue());
			if (flag)
			{
				loweredLeft = boundLoweredConditionalAccess.WhenNotNull;
			}
			BoundExpression boundExpression = (operatorKind.IsComparison() ? (operatorKind.IsUserDefined() ? this.LowerLiftedUserDefinedComparisonOperator(syntax, operatorKind, loweredLeft, loweredRight, method) : this.LowerLiftedBuiltInComparisonOperator(syntax, operatorKind, loweredLeft, loweredRight)) : this.LowerLiftedBinaryArithmeticOperator(syntax, operatorKind, loweredLeft, loweredRight, type, method));
			if (flag)
			{
				BoundExpression boundExpression2 = null;
				if (operatorKind.Operator() == BinaryOperatorKind.NotEqual || operatorKind.Operator() == BinaryOperatorKind.Equal)
				{
					boundExpression2 = this.RewriteLiftedBinaryOperator(syntax, operatorKind, this._factory.Default(loweredLeft.Type), loweredRight, type, method);
				}
				boundExpression = boundLoweredConditionalAccess.Update(boundLoweredConditionalAccess.Receiver, boundLoweredConditionalAccess.HasValueMethodOpt, boundExpression, boundExpression2, boundLoweredConditionalAccess.Id, boundExpression.Type);
			}
			return boundExpression;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000BC38C File Offset: 0x000BA58C
		private BoundExpression UnconvertArrayLength(BoundArrayLength arrLength)
		{
			return arrLength.Update(arrLength.Expression, this._factory.SpecialType(SpecialType.System_UIntPtr));
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000BC3A8 File Offset: 0x000BA5A8
		private BoundExpression MakeDynamicLogicalBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol leftTruthOperator, TypeSymbol type, bool isCompoundAssignment, [global::System.Runtime.CompilerServices.Nullable(2)] BoundUnaryOperator applyParentUnaryOperator)
		{
			bool flag = operatorKind.Operator() == BinaryOperatorKind.And;
			UnaryOperatorKind unaryOperatorKind = (flag ? UnaryOperatorKind.DynamicFalse : UnaryOperatorKind.DynamicTrue);
			ConstantValue constantValue = loweredLeft.ConstantValue ?? LocalRewriter.UnboxConstant(loweredLeft);
			if ((unaryOperatorKind == UnaryOperatorKind.DynamicFalse && constantValue == ConstantValue.False) || (unaryOperatorKind == UnaryOperatorKind.DynamicTrue && constantValue == ConstantValue.True))
			{
				if (applyParentUnaryOperator != null)
				{
					return this._factory.Literal(true);
				}
				return this.MakeConversionNode(loweredLeft, type, false, false);
			}
			else
			{
				NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
				BoundAssignmentOperator boundAssignmentOperator2;
				BoundLocal boundLocal2;
				if (constantValue == null && loweredLeft.Kind != BoundKind.Local && loweredLeft.Kind != BoundKind.Parameter)
				{
					BoundAssignmentOperator boundAssignmentOperator;
					BoundLocal boundLocal = (loweredLeft = this._factory.StoreToTemp(loweredLeft, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null));
					boundAssignmentOperator2 = boundAssignmentOperator;
					boundLocal2 = boundLocal;
				}
				else
				{
					boundAssignmentOperator2 = null;
					boundLocal2 = null;
				}
				BoundExpression boundExpression = this._dynamicFactory.MakeDynamicBinaryOperator(operatorKind, loweredLeft, loweredRight, isCompoundAssignment, type).ToExpression();
				bool flag2 = (unaryOperatorKind == UnaryOperatorKind.DynamicFalse && constantValue == ConstantValue.True) || (unaryOperatorKind == UnaryOperatorKind.DynamicTrue && constantValue == ConstantValue.False);
				BoundExpression boundExpression2;
				if (applyParentUnaryOperator != null)
				{
					boundExpression2 = this._dynamicFactory.MakeDynamicUnaryOperator(unaryOperatorKind, boundExpression, specialType).ToExpression();
					if (!flag2)
					{
						BoundExpression boundExpression3 = this.MakeTruthTestForDynamicLogicalOperator(syntax, loweredLeft, specialType, leftTruthOperator, flag);
						boundExpression2 = this._factory.Binary(BinaryOperatorKind.LogicalOr, specialType, boundExpression3, boundExpression2);
					}
				}
				else if (flag2)
				{
					boundExpression2 = boundExpression;
				}
				else
				{
					BoundExpression boundExpression4 = this.MakeTruthTestForDynamicLogicalOperator(syntax, loweredLeft, specialType, leftTruthOperator, flag);
					BoundExpression boundExpression5 = this.MakeConversionNode(loweredLeft, type, false, false);
					boundExpression2 = this._factory.Conditional(boundExpression4, boundExpression5, boundExpression, type);
				}
				if (boundAssignmentOperator2 != null)
				{
					return this._factory.Sequence(ImmutableArray.Create<LocalSymbol>(boundLocal2.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator2), boundExpression2);
				}
				return boundExpression2;
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000BC570 File Offset: 0x000BA770
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static ConstantValue UnboxConstant(BoundExpression expression)
		{
			if (expression.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)expression;
				if (boundConversion.ConversionKind == ConversionKind.Boxing)
				{
					return boundConversion.Operand.ConstantValue;
				}
			}
			return null;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000BC5A8 File Offset: 0x000BA7A8
		private BoundExpression MakeTruthTestForDynamicLogicalOperator(SyntaxNode syntax, BoundExpression loweredLeft, TypeSymbol boolean, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol leftTruthOperator, bool negative)
		{
			if (loweredLeft.HasDynamicType())
			{
				return this._dynamicFactory.MakeDynamicUnaryOperator(negative ? UnaryOperatorKind.DynamicFalse : UnaryOperatorKind.DynamicTrue, loweredLeft, boolean).ToExpression();
			}
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = this.GetNewCompoundUseSiteInfo();
			Conversion conversion = this._compilation.Conversions.ClassifyConversionFromExpression(loweredLeft, boolean, ref newCompoundUseSiteInfo, false);
			this._diagnostics.Add(loweredLeft.Syntax, newCompoundUseSiteInfo);
			if (!conversion.IsImplicit)
			{
				return BoundCall.Synthesized(syntax, null, leftTruthOperator, loweredLeft);
			}
			BoundExpression boundExpression = this.MakeConversionNode(loweredLeft, boolean, false, false);
			if (negative)
			{
				return new BoundUnaryOperator(syntax, UnaryOperatorKind.BoolLogicalNegation, boundExpression, null, null, LookupResultKind.Viable, boolean, false)
				{
					WasCompilerGenerated = true
				};
			}
			return boundExpression;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000BC651 File Offset: 0x000BA851
		private BoundExpression LowerUserDefinedBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			if (operatorKind.IsLifted())
			{
				return this.RewriteLiftedBinaryOperator(syntax, operatorKind, loweredLeft, loweredRight, type, method);
			}
			return BoundCall.Synthesized(syntax, null, method, loweredLeft, loweredRight);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000BC678 File Offset: 0x000BA878
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression TrivialLiftedComparisonOperatorOptimizations(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression left, BoundExpression right, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			bool flag = LocalRewriter.NullableNeverHasValue(left);
			bool flag2 = LocalRewriter.NullableNeverHasValue(right);
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			if (flag && flag2)
			{
				return this.MakeLiteral(syntax, ConstantValue.Create(kind.Operator() == BinaryOperatorKind.Equal), specialType, null);
			}
			BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(left);
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(right);
			if (boundExpression != null && boundExpression2 != null)
			{
				return this.MakeBinaryOperator(syntax, kind.Unlifted(), boundExpression, boundExpression2, specialType, method, false, false, null);
			}
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			if ((flag && boundExpression2 != null) || (flag2 && boundExpression != null))
			{
				BoundExpression boundExpression3 = this.MakeLiteral(syntax, ConstantValue.Create(binaryOperatorKind == BinaryOperatorKind.NotEqual), specialType, null);
				BoundExpression boundExpression4 = (flag ? boundExpression2 : boundExpression);
				if (LocalRewriter.ReadIsSideeffecting(boundExpression4))
				{
					boundExpression3 = new BoundSequence(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundExpression4), boundExpression3, specialType, false);
				}
				return boundExpression3;
			}
			if (!flag && !flag2)
			{
				return null;
			}
			BoundExpression boundExpression5 = (flag ? right : left);
			if (binaryOperatorKind != BinaryOperatorKind.Equal && binaryOperatorKind != BinaryOperatorKind.NotEqual)
			{
				BoundExpression boundExpression6 = this.MakeBooleanConstant(syntax, binaryOperatorKind == BinaryOperatorKind.NotEqual);
				return this._factory.MakeSequence(new BoundExpression[] { boundExpression5, boundExpression6 });
			}
			BoundExpression boundExpression7 = this.MakeNullableHasValue(syntax, boundExpression5);
			if (binaryOperatorKind != BinaryOperatorKind.Equal)
			{
				return boundExpression7;
			}
			return this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, boundExpression7, specialType);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000BC7C4 File Offset: 0x000BA9C4
		private BoundExpression MakeOptimizedGetValueOrDefault(SyntaxNode syntax, BoundExpression expression)
		{
			if (expression.Type.IsNullableType())
			{
				return BoundCall.Synthesized(syntax, expression, this.UnsafeGetNullableMethod(syntax, expression.Type, SpecialMember.System_Nullable_T_GetValueOrDefault));
			}
			return expression;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000BC7EB File Offset: 0x000BA9EB
		private BoundExpression MakeBooleanConstant(SyntaxNode syntax, bool value)
		{
			return this.MakeLiteral(syntax, ConstantValue.Create(value), this._compilation.GetSpecialType(SpecialType.System_Boolean), null);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000BC807 File Offset: 0x000BAA07
		private BoundExpression MakeOptimizedHasValue(SyntaxNode syntax, BoundExpression expression)
		{
			if (expression.Type.IsNullableType())
			{
				return this.MakeNullableHasValue(syntax, expression);
			}
			return this.MakeBooleanConstant(syntax, true);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000BC827 File Offset: 0x000BAA27
		private BoundExpression MakeNullableHasValue(SyntaxNode syntax, BoundExpression expression)
		{
			return BoundCall.Synthesized(syntax, expression, this.UnsafeGetNullableMethod(syntax, expression.Type, SpecialMember.System_Nullable_T_get_HasValue));
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000BC840 File Offset: 0x000BAA40
		private BoundExpression LowerLiftedBuiltInComparisonOperator(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression loweredLeft, BoundExpression loweredRight)
		{
			BoundExpression boundExpression = this.TrivialLiftedComparisonOperatorOptimizations(syntax, kind, loweredLeft, loweredRight, null);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(loweredLeft);
			BoundExpression boundExpression3 = LocalRewriter.NullableAlwaysHasValue(loweredRight);
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression2 ?? loweredLeft, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundAssignmentOperator boundAssignmentOperator2;
			BoundLocal boundLocal2 = this._factory.StoreToTemp(boundExpression3 ?? loweredRight, out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundExpression boundExpression4 = this.MakeOptimizedGetValueOrDefault(syntax, boundLocal);
			BoundExpression boundExpression5 = this.MakeOptimizedGetValueOrDefault(syntax, boundLocal2);
			BoundExpression boundExpression6 = this.MakeOptimizedHasValue(syntax, boundLocal);
			BoundExpression boundExpression7 = this.MakeOptimizedHasValue(syntax, boundLocal2);
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			BinaryOperatorKind binaryOperatorKind2;
			BinaryOperatorKind binaryOperatorKind3;
			if (binaryOperatorKind == BinaryOperatorKind.Equal || binaryOperatorKind == BinaryOperatorKind.NotEqual)
			{
				binaryOperatorKind2 = BinaryOperatorKind.Equal;
				binaryOperatorKind3 = BinaryOperatorKind.BoolEqual;
			}
			else
			{
				binaryOperatorKind2 = binaryOperatorKind;
				binaryOperatorKind3 = BinaryOperatorKind.BoolAnd;
			}
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			BoundExpression boundExpression8 = this.MakeBinaryOperator(syntax, binaryOperatorKind2.WithType(kind.OperandTypes()), boundExpression4, boundExpression5, specialType, null, false, false, null);
			BoundExpression boundExpression9 = this.MakeBinaryOperator(syntax, binaryOperatorKind3, boundExpression6, boundExpression7, specialType, null, false, false, null);
			BoundExpression boundExpression10 = this.MakeBinaryOperator(syntax, BinaryOperatorKind.BoolAnd, boundExpression8, boundExpression9, specialType, null, false, false, null);
			if (binaryOperatorKind == BinaryOperatorKind.NotEqual)
			{
				boundExpression10 = this._factory.Not(boundExpression10);
			}
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol, boundLocal2.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator, boundAssignmentOperator2), boundExpression10, specialType, false);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000BC998 File Offset: 0x000BAB98
		private BoundExpression LowerLiftedUserDefinedComparisonOperator(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression loweredLeft, BoundExpression loweredRight, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			BoundExpression boundExpression = this.TrivialLiftedComparisonOperatorOptimizations(syntax, kind, loweredLeft, loweredRight, method);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(loweredLeft);
			BoundExpression boundExpression3 = LocalRewriter.NullableAlwaysHasValue(loweredRight);
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression2 ?? loweredLeft, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundAssignmentOperator boundAssignmentOperator2;
			BoundLocal boundLocal2 = this._factory.StoreToTemp(boundExpression3 ?? loweredRight, out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundExpression boundExpression4 = this.MakeOptimizedGetValueOrDefault(syntax, boundLocal);
			BoundExpression boundExpression5 = this.MakeOptimizedGetValueOrDefault(syntax, boundLocal2);
			BoundExpression boundExpression6 = this.MakeOptimizedHasValue(syntax, boundLocal);
			BoundExpression boundExpression7 = this.MakeOptimizedHasValue(syntax, boundLocal2);
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			BinaryOperatorKind binaryOperatorKind2;
			if (binaryOperatorKind == BinaryOperatorKind.Equal || binaryOperatorKind == BinaryOperatorKind.NotEqual)
			{
				binaryOperatorKind2 = BinaryOperatorKind.BoolEqual;
			}
			else
			{
				binaryOperatorKind2 = BinaryOperatorKind.BoolAnd;
			}
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			BoundExpression boundExpression8 = this.MakeBinaryOperator(syntax, binaryOperatorKind2, boundExpression6, boundExpression7, specialType, null, false, false, null);
			BoundExpression boundExpression9 = this.MakeBinaryOperator(syntax, kind.Unlifted(), boundExpression4, boundExpression5, specialType, method, false, false, null);
			BoundExpression boundExpression10;
			if (binaryOperatorKind == BinaryOperatorKind.Equal || binaryOperatorKind == BinaryOperatorKind.NotEqual)
			{
				boundExpression10 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression6, boundExpression9, this.MakeLiteral(syntax, ConstantValue.Create(binaryOperatorKind == BinaryOperatorKind.Equal), specialType, null), null, specialType, false);
			}
			else
			{
				boundExpression10 = boundExpression9;
			}
			BoundExpression boundExpression11 = this.MakeBooleanConstant(syntax, binaryOperatorKind == BinaryOperatorKind.NotEqual);
			BoundExpression boundExpression12 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression8, boundExpression10, boundExpression11, null, specialType, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol, boundLocal2.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator, boundAssignmentOperator2), boundExpression12, specialType, false);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000BCB18 File Offset: 0x000BAD18
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression TrivialLiftedBinaryArithmeticOptimizations(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression left, BoundExpression right, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			bool flag = LocalRewriter.NullableNeverHasValue(left);
			bool flag2 = LocalRewriter.NullableNeverHasValue(right);
			if (flag && flag2)
			{
				return new BoundDefaultExpression(syntax, type, false);
			}
			BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(left);
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(right);
			if (boundExpression != null && boundExpression2 != null)
			{
				return this.MakeLiftedBinaryOperatorConsequence(syntax, kind, boundExpression, boundExpression2, type, method);
			}
			return null;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000BCB68 File Offset: 0x000BAD68
		private BoundExpression MakeLiftedBinaryOperatorConsequence(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression left, BoundExpression right, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			BoundExpression boundExpression = this.MakeBinaryOperator(syntax, kind.Unlifted(), left, right, type.GetNullableUnderlyingType(), method, false, false, null);
			return new BoundObjectCreationExpression(syntax, this.UnsafeGetNullableMethod(syntax, type, SpecialMember.System_Nullable_T__ctor), new BoundExpression[] { boundExpression });
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000BCBB0 File Offset: 0x000BADB0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static BoundExpression OptimizeLiftedArithmeticOperatorOneNull(SyntaxNode syntax, BoundExpression left, BoundExpression right, TypeSymbol type)
		{
			bool flag = LocalRewriter.NullableNeverHasValue(left);
			bool flag2 = LocalRewriter.NullableNeverHasValue(right);
			if (!flag && !flag2)
			{
				return null;
			}
			BoundExpression boundExpression = (flag ? right : left);
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(boundExpression) ?? boundExpression;
			if (boundExpression2.ConstantValue != null)
			{
				return new BoundDefaultExpression(syntax, type, false);
			}
			return new BoundSequence(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundExpression2), new BoundDefaultExpression(syntax, type, false), type, false);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000BCC18 File Offset: 0x000BAE18
		private BoundExpression LowerLiftedBinaryArithmeticOperator(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			BoundExpression boundExpression = this.OptimizeLiftedBinaryArithmetic(syntax, kind, loweredLeft, loweredRight, type, method);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(loweredLeft);
			BoundExpression boundExpression3 = LocalRewriter.NullableAlwaysHasValue(loweredRight);
			BoundExpression boundExpression4 = boundExpression2 ?? loweredLeft;
			boundExpression4 = this.CaptureExpressionInTempIfNeeded(boundExpression4, instance, instance2, SynthesizedLocalKind.LoweringTemp);
			BoundExpression boundExpression5 = boundExpression3 ?? loweredRight;
			boundExpression5 = this.CaptureExpressionInTempIfNeeded(boundExpression5, instance, instance2, SynthesizedLocalKind.LoweringTemp);
			BoundExpression boundExpression6 = this.MakeOptimizedGetValueOrDefault(syntax, boundExpression4);
			BoundExpression boundExpression7 = this.MakeOptimizedGetValueOrDefault(syntax, boundExpression5);
			BoundExpression boundExpression8 = this.MakeOptimizedHasValue(syntax, boundExpression4);
			BoundExpression boundExpression9 = this.MakeOptimizedHasValue(syntax, boundExpression5);
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			BoundExpression boundExpression10 = this.MakeBinaryOperator(syntax, BinaryOperatorKind.BoolAnd, boundExpression8, boundExpression9, specialType, null, false, false, null);
			BoundExpression boundExpression11 = this.MakeLiftedBinaryOperatorConsequence(syntax, kind, boundExpression6, boundExpression7, type, method);
			BoundExpression boundExpression12 = new BoundDefaultExpression(syntax, type, false);
			BoundExpression boundExpression13 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression10, boundExpression11, boundExpression12, null, type, false);
			return new BoundSequence(syntax, instance2.ToImmutableAndFree(), instance.ToImmutableAndFree(), boundExpression13, type, false);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000BCD18 File Offset: 0x000BAF18
		private BoundExpression CaptureExpressionInTempIfNeeded(BoundExpression operand, ArrayBuilder<BoundExpression> sideeffects, ArrayBuilder<LocalSymbol> locals, SynthesizedLocalKind kind = SynthesizedLocalKind.LoweringTemp)
		{
			if (LocalRewriter.CanChangeValueBetweenReads(operand, true, false))
			{
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(operand, out boundAssignmentOperator, RefKind.None, kind, null);
				sideeffects.Add(boundAssignmentOperator);
				locals.Add(boundLocal.LocalSymbol);
				operand = boundLocal;
			}
			return operand;
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000BCD5C File Offset: 0x000BAF5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression OptimizeLiftedBinaryArithmetic(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression left, BoundExpression right, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method)
		{
			BoundExpression boundExpression = this.TrivialLiftedBinaryArithmeticOptimizations(syntax, kind, left, right, type, method);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			if (kind == BinaryOperatorKind.LiftedBoolAnd || kind == BinaryOperatorKind.LiftedBoolOr)
			{
				return this.LowerLiftedBooleanOperator(syntax, kind, left, right);
			}
			boundExpression = LocalRewriter.OptimizeLiftedArithmeticOperatorOneNull(syntax, left, right, type);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(right);
			if (boundExpression2 != null && boundExpression2.ConstantValue != null && left.Kind == BoundKind.Sequence)
			{
				BoundSequence boundSequence = (BoundSequence)left;
				if (boundSequence.Value.Kind == BoundKind.ConditionalOperator)
				{
					BoundConditionalOperator boundConditionalOperator = (BoundConditionalOperator)boundSequence.Value;
					if (LocalRewriter.NullableAlwaysHasValue(boundConditionalOperator.Consequence) != null && LocalRewriter.NullableNeverHasValue(boundConditionalOperator.Alternative))
					{
						return new BoundSequence(syntax, boundSequence.Locals, boundSequence.SideEffects, LocalRewriter.RewriteConditionalOperator(syntax, boundConditionalOperator.Condition, this.MakeBinaryOperator(syntax, kind, boundConditionalOperator.Consequence, right, type, method, false, false, null), this.MakeBinaryOperator(syntax, kind, boundConditionalOperator.Alternative, right, type, method, false, false, null), null, type, false), type, false);
					}
				}
			}
			return null;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000BCE6C File Offset: 0x000BB06C
		private BoundExpression MakeNewNullableBoolean(SyntaxNode syntax, bool? value)
		{
			NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Nullable_T);
			TypeSymbol specialType2 = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			NamedTypeSymbol namedTypeSymbol = specialType.Construct(new TypeSymbol[] { specialType2 });
			if (value == null)
			{
				return new BoundDefaultExpression(syntax, namedTypeSymbol, false);
			}
			return new BoundObjectCreationExpression(syntax, this.UnsafeGetNullableMethod(syntax, namedTypeSymbol, SpecialMember.System_Nullable_T__ctor), new BoundExpression[] { this.MakeBooleanConstant(syntax, value.GetValueOrDefault()) });
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000BCEDC File Offset: 0x000BB0DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression OptimizeLiftedBooleanOperatorOneNull(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression left, BoundExpression right)
		{
			bool flag = LocalRewriter.NullableNeverHasValue(left);
			bool flag2 = LocalRewriter.NullableNeverHasValue(right);
			if (!flag && !flag2)
			{
				return null;
			}
			BoundExpression boundExpression = (flag ? left : right);
			BoundExpression boundExpression2 = (flag ? right : left);
			BoundExpression boundExpression3 = LocalRewriter.NullableAlwaysHasValue(boundExpression2);
			BoundExpression boundExpression4 = new BoundDefaultExpression(syntax, boundExpression.Type, false);
			if (boundExpression3 != null)
			{
				BoundExpression boundExpression5 = this.MakeNewNullableBoolean(syntax, new bool?(kind == BinaryOperatorKind.LiftedBoolOr));
				return LocalRewriter.RewriteConditionalOperator(syntax, boundExpression3, (kind == BinaryOperatorKind.LiftedBoolAnd) ? boundExpression4 : boundExpression5, (kind == BinaryOperatorKind.LiftedBoolAnd) ? boundExpression5 : boundExpression4, null, boundExpression.Type, false);
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression2, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundExpression boundExpression6 = this.MakeOptimizedGetValueOrDefault(syntax, boundLocal);
			BoundExpression boundExpression7 = ((kind == BinaryOperatorKind.LiftedBoolAnd) ? boundExpression4 : boundLocal);
			BoundExpression boundExpression8 = ((kind == BinaryOperatorKind.LiftedBoolAnd) ? boundLocal : boundExpression4);
			BoundExpression boundExpression9 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression6, boundExpression7, boundExpression8, null, boundExpression.Type, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression9, boundExpression9.Type, false);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000BCFE8 File Offset: 0x000BB1E8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression OptimizeLiftedBooleanOperatorOneNonNull(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression left, BoundExpression right)
		{
			BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(left);
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(right);
			if (boundExpression == null && boundExpression2 == null)
			{
				return null;
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression ?? left, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundAssignmentOperator boundAssignmentOperator2;
			BoundLocal boundLocal2 = this._factory.StoreToTemp(boundExpression2 ?? right, out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundExpression boundExpression3 = ((boundExpression == null) ? boundLocal2 : boundLocal);
			BoundExpression boundExpression4 = ((boundExpression == null) ? boundLocal : boundLocal2);
			BoundExpression boundExpression5 = boundExpression3;
			BoundExpression boundExpression6 = this.MakeNewNullableBoolean(syntax, new bool?(kind == BinaryOperatorKind.LiftedBoolOr));
			BoundExpression boundExpression7 = ((kind == BinaryOperatorKind.LiftedBoolOr) ? boundExpression6 : boundExpression4);
			BoundExpression boundExpression8 = ((kind == BinaryOperatorKind.LiftedBoolOr) ? boundExpression4 : boundExpression6);
			BoundExpression boundExpression9 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression5, boundExpression7, boundExpression8, null, boundExpression6.Type, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol, boundLocal2.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator, boundAssignmentOperator2), boundExpression9, boundExpression9.Type, false);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000BD0CC File Offset: 0x000BB2CC
		private BoundExpression LowerLiftedBooleanOperator(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression loweredLeft, BoundExpression loweredRight)
		{
			BoundExpression boundExpression = this.OptimizeLiftedBooleanOperatorOneNull(syntax, kind, loweredLeft, loweredRight);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			boundExpression = this.OptimizeLiftedBooleanOperatorOneNonNull(syntax, kind, loweredLeft, loweredRight);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(loweredLeft, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundAssignmentOperator boundAssignmentOperator2;
			BoundLocal boundLocal2 = this._factory.StoreToTemp(loweredRight, out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, boundLocal.Type, SpecialMember.System_Nullable_T_GetValueOrDefault);
			MethodSymbol methodSymbol2 = this.UnsafeGetNullableMethod(syntax, boundLocal2.Type, SpecialMember.System_Nullable_T_GetValueOrDefault);
			BoundExpression boundExpression2 = BoundCall.Synthesized(syntax, boundLocal, methodSymbol);
			BoundExpression boundExpression3 = BoundCall.Synthesized(syntax, boundLocal2, methodSymbol2);
			BoundExpression boundExpression4 = this.MakeNullableHasValue(syntax, boundLocal);
			BoundExpression boundExpression5 = this.MakeBinaryOperator(syntax, BinaryOperatorKind.LogicalBoolOr, boundExpression3, boundExpression4, specialType, null, false, false, null);
			BoundExpression boundExpression6 = this.MakeUnaryOperator(UnaryOperatorKind.BoolLogicalNegation, syntax, null, boundExpression5, specialType);
			BoundExpression boundExpression7 = this.MakeBinaryOperator(syntax, BinaryOperatorKind.LogicalBoolOr, boundExpression2, boundExpression6, specialType, null, false, false, null);
			BoundExpression boundExpression8 = ((kind == BinaryOperatorKind.LiftedBoolAnd) ? boundLocal2 : boundLocal);
			BoundExpression boundExpression9 = ((kind == BinaryOperatorKind.LiftedBoolAnd) ? boundLocal : boundLocal2);
			BoundExpression boundExpression10 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression7, boundExpression8, boundExpression9, null, boundExpression9.Type, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol, boundLocal2.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator, boundAssignmentOperator2), boundExpression10, boundExpression10.Type, false);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000BD216 File Offset: 0x000BB416
		private MethodSymbol UnsafeGetNullableMethod(SyntaxNode syntax, TypeSymbol nullableType, SpecialMember member)
		{
			return LocalRewriter.UnsafeGetNullableMethod(syntax, nullableType, member, this._compilation, this._diagnostics);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000BD22C File Offset: 0x000BB42C
		private static MethodSymbol UnsafeGetNullableMethod(SyntaxNode syntax, TypeSymbol nullableType, SpecialMember member, CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol namedTypeSymbol = nullableType as NamedTypeSymbol;
			return LocalRewriter.UnsafeGetSpecialTypeMethod(syntax, member, compilation, diagnostics).AsMember(namedTypeSymbol);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000BD250 File Offset: 0x000BB450
		private bool TryGetNullableMethod(SyntaxNode syntax, TypeSymbol nullableType, SpecialMember member, out MethodSymbol result)
		{
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)nullableType;
			if (this.TryGetSpecialTypeMethod(syntax, member, out result))
			{
				result = result.AsMember(namedTypeSymbol);
				return true;
			}
			return false;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000BD280 File Offset: 0x000BB480
		private BoundExpression RewriteNullableNullEquality(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol returnType)
		{
			BoundExpression boundExpression = (loweredRight.IsLiteralNull() ? loweredLeft : loweredRight);
			if (LocalRewriter.NullableNeverHasValue(boundExpression))
			{
				return this.MakeLiteral(syntax, ConstantValue.Create(kind == BinaryOperatorKind.NullableNullEqual), returnType, null);
			}
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(boundExpression);
			if (boundExpression2 != null)
			{
				return new BoundSequence(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundExpression2), this.MakeBooleanConstant(syntax, kind == BinaryOperatorKind.NullableNullNotEqual), returnType, false);
			}
			BoundLoweredConditionalAccess boundLoweredConditionalAccess = boundExpression as BoundLoweredConditionalAccess;
			if (boundLoweredConditionalAccess != null && (boundLoweredConditionalAccess.WhenNullOpt == null || boundLoweredConditionalAccess.WhenNullOpt.IsDefaultValue()))
			{
				BoundExpression boundExpression3 = this.RewriteNullableNullEquality(syntax, kind, boundLoweredConditionalAccess.WhenNotNull, loweredLeft.IsLiteralNull() ? loweredLeft : loweredRight, returnType);
				BoundExpression boundExpression4 = ((kind == BinaryOperatorKind.NullableNullEqual) ? this.MakeBooleanConstant(syntax, true) : null);
				return boundLoweredConditionalAccess.Update(boundLoweredConditionalAccess.Receiver, boundLoweredConditionalAccess.HasValueMethodOpt, boundExpression3, boundExpression4, boundLoweredConditionalAccess.Id, boundExpression3.Type);
			}
			BoundExpression boundExpression5 = this.MakeNullableHasValue(syntax, boundExpression);
			if (kind != BinaryOperatorKind.NullableNullNotEqual)
			{
				return new BoundUnaryOperator(syntax, UnaryOperatorKind.BoolLogicalNegation, boundExpression5, null, null, LookupResultKind.Viable, returnType, false);
			}
			return boundExpression5;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000BD388 File Offset: 0x000BB588
		private BoundExpression RewriteStringEquality([global::System.Runtime.CompilerServices.Nullable(2)] BoundBinaryOperator oldNode, SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, SpecialMember member)
		{
			if (oldNode != null && (loweredLeft.ConstantValue == ConstantValue.Null || loweredRight.ConstantValue == ConstantValue.Null))
			{
				return oldNode.Update(operatorKind, oldNode.ConstantValueOpt, oldNode.MethodOpt, oldNode.ResultKind, loweredLeft, loweredRight, type);
			}
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, member);
			return BoundCall.Synthesized(syntax, null, methodSymbol, loweredLeft, loweredRight);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000BD3F4 File Offset: 0x000BB5F4
		private BoundExpression RewriteDelegateOperation(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, SpecialMember member)
		{
			MethodSymbol methodSymbol;
			if (operatorKind == BinaryOperatorKind.DelegateEqual || operatorKind == BinaryOperatorKind.DelegateNotEqual)
			{
				methodSymbol = (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(member);
				if (loweredRight.IsLiteralNull() || loweredLeft.IsLiteralNull() || (methodSymbol = (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(member)) == null)
				{
					operatorKind = (operatorKind & (BinaryOperatorKind)(-24)) | BinaryOperatorKind.Object;
					return new BoundBinaryOperator(syntax, operatorKind, null, null, LookupResultKind.Empty, loweredLeft, loweredRight, type, false);
				}
			}
			else
			{
				methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, member);
			}
			BoundExpression boundExpression = (this._inExpressionLambda ? new BoundBinaryOperator(syntax, operatorKind, null, methodSymbol, LookupResultKind.Empty, loweredLeft, loweredRight, methodSymbol.ReturnType, false) : BoundCall.Synthesized(syntax, null, methodSymbol, loweredLeft, loweredRight));
			if (methodSymbol.ReturnType.SpecialType != SpecialType.System_Delegate)
			{
				return boundExpression;
			}
			return this.MakeConversionNode(syntax, boundExpression, Conversion.ExplicitReference, type, false, false, null);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000BD4C8 File Offset: 0x000BB6C8
		private BoundExpression RewriteDecimalBinaryOperation(SyntaxNode syntax, BoundExpression loweredLeft, BoundExpression loweredRight, BinaryOperatorKind operatorKind)
		{
			SpecialMember specialMember;
			if (operatorKind <= BinaryOperatorKind.DecimalRemainder)
			{
				if (operatorKind <= BinaryOperatorKind.DecimalAddition)
				{
					if (operatorKind == BinaryOperatorKind.DecimalMultiplication)
					{
						specialMember = SpecialMember.System_Decimal__op_Multiply;
						goto IL_00CD;
					}
					if (operatorKind == BinaryOperatorKind.DecimalAddition)
					{
						specialMember = SpecialMember.System_Decimal__op_Addition;
						goto IL_00CD;
					}
				}
				else
				{
					if (operatorKind == BinaryOperatorKind.DecimalSubtraction)
					{
						specialMember = SpecialMember.System_Decimal__op_Subtraction;
						goto IL_00CD;
					}
					if (operatorKind == BinaryOperatorKind.DecimalDivision)
					{
						specialMember = SpecialMember.System_Decimal__op_Division;
						goto IL_00CD;
					}
					if (operatorKind == BinaryOperatorKind.DecimalRemainder)
					{
						specialMember = SpecialMember.System_Decimal__op_Modulus;
						goto IL_00CD;
					}
				}
			}
			else if (operatorKind <= BinaryOperatorKind.DecimalGreaterThan)
			{
				if (operatorKind == BinaryOperatorKind.DecimalEqual)
				{
					specialMember = SpecialMember.System_Decimal__op_Equality;
					goto IL_00CD;
				}
				if (operatorKind == BinaryOperatorKind.DecimalNotEqual)
				{
					specialMember = SpecialMember.System_Decimal__op_Inequality;
					goto IL_00CD;
				}
				if (operatorKind == BinaryOperatorKind.DecimalGreaterThan)
				{
					specialMember = SpecialMember.System_Decimal__op_GreaterThan;
					goto IL_00CD;
				}
			}
			else
			{
				if (operatorKind == BinaryOperatorKind.DecimalLessThan)
				{
					specialMember = SpecialMember.System_Decimal__op_LessThan;
					goto IL_00CD;
				}
				if (operatorKind == BinaryOperatorKind.DecimalGreaterThanOrEqual)
				{
					specialMember = SpecialMember.System_Decimal__op_GreaterThanOrEqual;
					goto IL_00CD;
				}
				if (operatorKind == BinaryOperatorKind.DecimalLessThanOrEqual)
				{
					specialMember = SpecialMember.System_Decimal__op_LessThanOrEqual;
					goto IL_00CD;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(operatorKind);
			IL_00CD:
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, specialMember);
			return BoundCall.Synthesized(syntax, null, methodSymbol, loweredLeft, loweredRight);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000BD5B8 File Offset: 0x000BB7B8
		private BoundExpression MakeNullCheck(SyntaxNode syntax, BoundExpression rewrittenExpr, BinaryOperatorKind operatorKind)
		{
			TypeSymbol type = rewrittenExpr.Type;
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			if (rewrittenExpr.ConstantValue != null)
			{
				if (operatorKind == BinaryOperatorKind.Equal)
				{
					return this.MakeLiteral(syntax, ConstantValue.Create(rewrittenExpr.ConstantValue.IsNull, ConstantValueTypeDiscriminator.Boolean), specialType, null);
				}
				if (operatorKind == BinaryOperatorKind.NotEqual)
				{
					return this.MakeLiteral(syntax, ConstantValue.Create(!rewrittenExpr.ConstantValue.IsNull, ConstantValueTypeDiscriminator.Boolean), specialType, null);
				}
			}
			TypeSymbol specialType2 = this._compilation.GetSpecialType(SpecialType.System_Object);
			if (type != null)
			{
				if (type.Kind == SymbolKind.TypeParameter)
				{
					rewrittenExpr = this.MakeConversionNode(syntax, rewrittenExpr, Conversion.Boxing, specialType2, false, false, null);
				}
				else if (type.IsNullableType())
				{
					operatorKind |= BinaryOperatorKind.NullableNull;
				}
			}
			return this.MakeBinaryOperator(syntax, operatorKind, rewrittenExpr, this.MakeLiteral(syntax, ConstantValue.Null, specialType2, null), specialType, null, false, false, null);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000BD698 File Offset: 0x000BB898
		private BoundExpression RewriteBuiltInShiftOperation([global::System.Runtime.CompilerServices.Nullable(2)] BoundBinaryOperator oldNode, SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type, int rightMask)
		{
			SyntaxNode syntax2 = loweredRight.Syntax;
			ConstantValue constantValue = loweredRight.ConstantValue;
			TypeSymbol type2 = loweredRight.Type;
			if (constantValue != null && constantValue.IsIntegral)
			{
				int num = constantValue.Int32Value & rightMask;
				if (num == 0)
				{
					return loweredLeft;
				}
				loweredRight = this.MakeLiteral(syntax2, ConstantValue.Create(num), type2, null);
			}
			else
			{
				BinaryOperatorKind binaryOperatorKind = (operatorKind & ~BinaryOperatorKind.OpMask) | BinaryOperatorKind.And;
				loweredRight = new BoundBinaryOperator(syntax2, binaryOperatorKind, null, null, LookupResultKind.Viable, loweredRight, this.MakeLiteral(syntax2, ConstantValue.Create(rightMask), type2, null), type2, false);
			}
			if (oldNode != null)
			{
				return oldNode.Update(operatorKind, null, null, oldNode.ResultKind, loweredLeft, loweredRight, type);
			}
			return new BoundBinaryOperator(syntax, operatorKind, null, null, LookupResultKind.Viable, loweredLeft, loweredRight, type, false);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000BD750 File Offset: 0x000BB950
		private BoundExpression RewritePointerNumericOperator(SyntaxNode syntax, BinaryOperatorKind kind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol returnType, bool isPointerElementAccess, bool isLeftPointer)
		{
			if (isLeftPointer)
			{
				loweredRight = this.MakeSizeOfMultiplication(loweredRight, (PointerTypeSymbol)loweredLeft.Type, kind.IsChecked());
			}
			else
			{
				loweredLeft = this.MakeSizeOfMultiplication(loweredLeft, (PointerTypeSymbol)loweredRight.Type, kind.IsChecked());
			}
			if (isPointerElementAccess)
			{
				kind &= ~BinaryOperatorKind.Checked;
			}
			return new BoundBinaryOperator(syntax, kind, null, null, LookupResultKind.Viable, loweredLeft, loweredRight, returnType, false);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000BD7B8 File Offset: 0x000BB9B8
		private BoundExpression MakeSizeOfMultiplication(BoundExpression numericOperand, PointerTypeSymbol pointerType, bool isChecked)
		{
			BoundExpression boundExpression = this._factory.Sizeof(pointerType.PointedAtType);
			ConstantValue constantValue = numericOperand.ConstantValue;
			if (constantValue != null && constantValue.UInt64Value == 1UL)
			{
				return boundExpression;
			}
			SpecialType specialType = numericOperand.Type.SpecialType;
			ConstantValue constantValue2 = boundExpression.ConstantValue;
			if (constantValue2 != null && constantValue2.Int32Value == 1)
			{
				SpecialType specialType2 = specialType;
				switch (specialType)
				{
				case SpecialType.System_Int32:
					if (isChecked)
					{
						ConstantValue constantValue3 = numericOperand.ConstantValue;
						if (constantValue3 == null || constantValue3.Int32Value < 0)
						{
							specialType2 = SpecialType.System_IntPtr;
						}
					}
					break;
				case SpecialType.System_UInt32:
				{
					ConstantValue constantValue4 = numericOperand.ConstantValue;
					if (constantValue4 == null || constantValue4.UInt32Value > 2147483647U)
					{
						specialType2 = SpecialType.System_UIntPtr;
					}
					break;
				}
				case SpecialType.System_Int64:
					specialType2 = SpecialType.System_IntPtr;
					break;
				case SpecialType.System_UInt64:
					specialType2 = SpecialType.System_UIntPtr;
					break;
				default:
					throw ExceptionUtilities.UnexpectedValue(specialType);
				}
				if (specialType2 != specialType)
				{
					return this._factory.Convert(this._factory.SpecialType(specialType2), numericOperand, Conversion.IntegerToPointer, false);
				}
				return numericOperand;
			}
			else
			{
				BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.Multiplication;
				TypeSymbol typeSymbol2;
				TypeSymbol typeSymbol3;
				switch (specialType)
				{
				case SpecialType.System_Int32:
				{
					TypeSymbol typeSymbol = this._factory.SpecialType(SpecialType.System_IntPtr);
					numericOperand = this._factory.Convert(typeSymbol, numericOperand, Conversion.IntegerToPointer, isChecked);
					binaryOperatorKind |= BinaryOperatorKind.Int;
					typeSymbol2 = typeSymbol;
					typeSymbol3 = typeSymbol;
					break;
				}
				case SpecialType.System_UInt32:
				{
					TypeSymbol typeSymbol4 = this._factory.SpecialType(SpecialType.System_Int64);
					TypeSymbol typeSymbol5 = this._factory.SpecialType(SpecialType.System_IntPtr);
					numericOperand = this._factory.Convert(typeSymbol4, numericOperand, Conversion.ExplicitNumeric, isChecked);
					boundExpression = this._factory.Convert(typeSymbol4, boundExpression, Conversion.ExplicitNumeric, isChecked);
					binaryOperatorKind |= BinaryOperatorKind.Long;
					typeSymbol2 = typeSymbol4;
					typeSymbol3 = typeSymbol5;
					break;
				}
				case SpecialType.System_Int64:
				{
					TypeSymbol typeSymbol6 = this._factory.SpecialType(SpecialType.System_Int64);
					TypeSymbol typeSymbol7 = this._factory.SpecialType(SpecialType.System_IntPtr);
					boundExpression = this._factory.Convert(typeSymbol6, boundExpression, Conversion.ExplicitNumeric, isChecked);
					binaryOperatorKind |= BinaryOperatorKind.Long;
					typeSymbol2 = typeSymbol6;
					typeSymbol3 = typeSymbol7;
					break;
				}
				case SpecialType.System_UInt64:
				{
					TypeSymbol typeSymbol8 = this._factory.SpecialType(SpecialType.System_UInt64);
					TypeSymbol typeSymbol9 = this._factory.SpecialType(SpecialType.System_UIntPtr);
					boundExpression = this._factory.Convert(typeSymbol8, boundExpression, Conversion.ExplicitNumeric, isChecked);
					binaryOperatorKind |= BinaryOperatorKind.ULong;
					typeSymbol2 = typeSymbol8;
					typeSymbol3 = typeSymbol9;
					break;
				}
				default:
					throw ExceptionUtilities.UnexpectedValue(specialType);
				}
				if (isChecked)
				{
					binaryOperatorKind |= BinaryOperatorKind.Checked;
				}
				BoundBinaryOperator boundBinaryOperator = this._factory.Binary(binaryOperatorKind, typeSymbol2, numericOperand, boundExpression);
				if (!TypeSymbol.Equals(typeSymbol3, typeSymbol2, TypeCompareKind.ConsiderEverything))
				{
					return this._factory.Convert(typeSymbol3, boundBinaryOperator, Conversion.IntegerToPointer, false);
				}
				return boundBinaryOperator;
			}
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000BDA30 File Offset: 0x000BBC30
		private BoundExpression RewritePointerSubtraction(BinaryOperatorKind kind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol returnType)
		{
			PointerTypeSymbol pointerTypeSymbol = (PointerTypeSymbol)loweredLeft.Type;
			BoundExpression boundExpression = this._factory.Sizeof(pointerTypeSymbol.PointedAtType);
			return this._factory.Convert(returnType, this._factory.Binary(BinaryOperatorKind.Division, this._factory.SpecialType(SpecialType.System_IntPtr), this._factory.Binary(kind & ~BinaryOperatorKind.Checked, returnType, loweredLeft, loweredRight), boundExpression), Conversion.PointerToInteger, false);
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000BDAA4 File Offset: 0x000BBCA4
		public override BoundNode VisitBlock(BoundBlock node)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			this.VisitStatementSubList(instance, node.Statements, 0);
			if (!this.Instrument || (node != this._rootStatement && (node.WasCompilerGenerated || node.Syntax.Kind() != SyntaxKind.Block)))
			{
				return node.Update(node.Locals, node.LocalFunctions, instance.ToImmutableAndFree());
			}
			LocalSymbol localSymbol;
			BoundStatement boundStatement = this._instrumenter.CreateBlockPrologue(node, out localSymbol);
			if (boundStatement != null)
			{
				instance.Insert(0, boundStatement);
			}
			else if (node == this._rootStatement && this._factory.TopLevelMethod is SynthesizedSimpleProgramEntryPointSymbol)
			{
				instance.Insert(0, this._factory.HiddenSequencePoint(null));
			}
			BoundStatement boundStatement2 = this._instrumenter.CreateBlockEpilogue(node);
			if (boundStatement2 != null)
			{
				instance.Add(boundStatement2);
			}
			return new BoundBlock(node.Syntax, (localSymbol == null) ? node.Locals : node.Locals.Add(localSymbol), node.LocalFunctions, instance.ToImmutableAndFree(), node.HasErrors);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000BDBA8 File Offset: 0x000BBDA8
		public void VisitStatementSubList(ArrayBuilder<BoundStatement> builder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, int startIndex = 0)
		{
			for (int i = startIndex; i < statements.Length; i++)
			{
				bool flag;
				BoundStatement boundStatement = this.VisitPossibleUsingDeclaration(statements[i], statements, i, out flag);
				if (boundStatement != null)
				{
					builder.Add(boundStatement);
				}
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000BDBE8 File Offset: 0x000BBDE8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundStatement VisitPossibleUsingDeclaration(BoundStatement node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, int statementIndex, out bool replacedLocalDeclarations)
		{
			BoundKind kind = node.Kind;
			if (kind == BoundKind.UsingLocalDeclarations)
			{
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
				this.VisitStatementSubList(instance, statements, statementIndex + 1);
				replacedLocalDeclarations = true;
				return this.MakeLocalUsingDeclarationStatement((BoundUsingLocalDeclarations)node, instance.ToImmutableAndFree());
			}
			if (kind == BoundKind.LabeledStatement)
			{
				BoundLabeledStatement boundLabeledStatement = (BoundLabeledStatement)node;
				return this.MakeLabeledStatement(boundLabeledStatement, this.VisitPossibleUsingDeclaration(boundLabeledStatement.Body, statements, statementIndex, out replacedLocalDeclarations));
			}
			replacedLocalDeclarations = false;
			return this.VisitStatement(node);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000BDC58 File Offset: 0x000BBE58
		public override BoundNode VisitNoOpStatement(BoundNoOpStatement node)
		{
			if (!node.WasCompilerGenerated && this.Instrument)
			{
				return this._instrumenter.InstrumentNoOpStatement(node, node);
			}
			return new BoundBlock(node.Syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray<BoundStatement>.Empty, false);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000BDC90 File Offset: 0x000BBE90
		public override BoundNode VisitBreakStatement(BoundBreakStatement node)
		{
			BoundStatement boundStatement = new BoundGotoStatement(node.Syntax, node.Label, node.HasErrors);
			if (this.Instrument && !node.WasCompilerGenerated)
			{
				boundStatement = this._instrumenter.InstrumentBreakStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000BDCD4 File Offset: 0x000BBED4
		public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
		{
			return this.VisitDynamicInvocation(node, false);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000BDCE0 File Offset: 0x000BBEE0
		public BoundExpression VisitDynamicInvocation(BoundDynamicInvocation node, bool resultDiscarded)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			BoundKind kind = node.Expression.Kind;
			ImmutableArray<TypeWithAnnotations> immutableArray2;
			string text;
			bool flag;
			BoundExpression boundExpression2;
			if (kind != BoundKind.DynamicMemberAccess)
			{
				if (kind != BoundKind.MethodGroup)
				{
					BoundExpression boundExpression = this.VisitExpression(node.Expression);
					return this._dynamicFactory.MakeDynamicInvocation(boundExpression, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, resultDiscarded).ToExpression();
				}
				BoundMethodGroup boundMethodGroup = (BoundMethodGroup)node.Expression;
				immutableArray2 = boundMethodGroup.TypeArgumentsOpt;
				text = boundMethodGroup.Name;
				BoundMethodGroupFlags? boundMethodGroupFlags = boundMethodGroup.Flags & BoundMethodGroupFlags.HasImplicitReceiver;
				BoundMethodGroupFlags boundMethodGroupFlags2 = BoundMethodGroupFlags.None;
				flag = !((boundMethodGroupFlags.GetValueOrDefault() == boundMethodGroupFlags2) & (boundMethodGroupFlags != null));
				if (boundMethodGroup.ReceiverOpt == null)
				{
					NamedTypeSymbol containingType = node.ApplicableMethods.First<MethodSymbol>().ContainingType;
					boundExpression2 = new BoundTypeExpression(node.Syntax, null, containingType, false);
				}
				else
				{
					if (flag)
					{
						MethodSymbol topLevelMethod = this._factory.TopLevelMethod;
						if (topLevelMethod != null && !topLevelMethod.RequiresInstanceReceiver)
						{
							boundExpression2 = new BoundTypeExpression(node.Syntax, null, this._factory.CurrentType, false);
							goto IL_010B;
						}
					}
					boundExpression2 = this.VisitExpression(boundMethodGroup.ReceiverOpt);
				}
				IL_010B:
				this.EmbedIfNeedTo(boundExpression2, boundMethodGroup.Methods, node.Syntax);
			}
			else
			{
				BoundDynamicMemberAccess boundDynamicMemberAccess = (BoundDynamicMemberAccess)node.Expression;
				text = boundDynamicMemberAccess.Name;
				immutableArray2 = boundDynamicMemberAccess.TypeArgumentsOpt;
				boundExpression2 = this.VisitExpression(boundDynamicMemberAccess.Receiver);
				flag = false;
			}
			return this._dynamicFactory.MakeDynamicMemberInvocation(text, boundExpression2, immutableArray2, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, flag, resultDiscarded).ToExpression();
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000BDE98 File Offset: 0x000BC098
		private void EmbedIfNeedTo(BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> methods, SyntaxNode syntaxNode)
		{
			PEModuleBuilder emitModule = this.EmitModule;
			if (emitModule != null && receiver != null && receiver.Type != null)
			{
				AssemblySymbol containingAssembly = receiver.Type.ContainingAssembly;
				if (containingAssembly != null && containingAssembly.IsLinked)
				{
					foreach (MethodSymbol methodSymbol in methods)
					{
						emitModule.EmbeddedTypesManagerOpt.EmbedMethodIfNeedTo(methodSymbol.OriginalDefinition.GetCciAdapter(), syntaxNode, this._diagnostics.DiagnosticBag);
					}
				}
			}
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000BDF10 File Offset: 0x000BC110
		private void EmbedIfNeedTo(BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> properties, SyntaxNode syntaxNode)
		{
			PEModuleBuilder emitModule = this.EmitModule;
			if (emitModule != null && receiver != null && receiver.Type != null)
			{
				AssemblySymbol containingAssembly = receiver.Type.ContainingAssembly;
				if (containingAssembly != null && containingAssembly.IsLinked)
				{
					foreach (PropertySymbol propertySymbol in properties)
					{
						emitModule.EmbeddedTypesManagerOpt.EmbedPropertyIfNeedTo(propertySymbol.OriginalDefinition.GetCciAdapter(), syntaxNode, this._diagnostics.DiagnosticBag);
					}
				}
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000BDF88 File Offset: 0x000BC188
		public override BoundNode VisitCall(BoundCall node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.ReceiverOpt);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			return this.MakeCall(node.Syntax, boundExpression, node.Method, immutableArray, node.ArgumentRefKindsOpt, node.Expanded, node.InvokedAsExtensionMethod, node.ArgsToParamsOpt, node.ResultKind, node.Type, node);
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000BDFE8 File Offset: 0x000BC1E8
		private BoundExpression MakeCall(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, bool invokedAsExtensionMethod, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, LookupResultKind resultKind, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] BoundCall nodeOpt = null)
		{
			ImmutableArray<LocalSymbol> immutableArray;
			rewrittenArguments = this.MakeArguments(syntax, rewrittenArguments, method, expanded, argsToParamsOpt, ref argumentRefKindsOpt, out immutableArray, invokedAsExtensionMethod, ThreeState.Unknown);
			return this.MakeCall(nodeOpt, syntax, rewrittenReceiver, method, rewrittenArguments, argumentRefKindsOpt, invokedAsExtensionMethod, resultKind, type, immutableArray);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000BE024 File Offset: 0x000BC224
		private BoundExpression MakeCall([global::System.Runtime.CompilerServices.Nullable(2)] BoundCall node, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKinds, bool invokedAsExtensionMethod, LookupResultKind resultKind, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> temps = default(ImmutableArray<LocalSymbol>))
		{
			BoundExpression boundExpression;
			if (method.IsStatic && method.ContainingType.IsObjectType() && !this._inExpressionLambda && method == this._compilation.GetSpecialTypeMember(SpecialMember.System_Object__ReferenceEquals))
			{
				boundExpression = new BoundBinaryOperator(syntax, BinaryOperatorKind.ObjectEqual, null, null, resultKind, rewrittenArguments[0], rewrittenArguments[1], type, false);
			}
			else if (node == null)
			{
				boundExpression = new BoundCall(syntax, rewrittenReceiver, method, rewrittenArguments, default(ImmutableArray<string>), argumentRefKinds, false, false, invokedAsExtensionMethod, default(ImmutableArray<int>), default(BitVector), resultKind, type, false);
			}
			else
			{
				boundExpression = node.Update(rewrittenReceiver, method, rewrittenArguments, default(ImmutableArray<string>), argumentRefKinds, node.IsDelegateCall, false, node.InvokedAsExtensionMethod, default(ImmutableArray<int>), default(BitVector), node.ResultKind, node.Type);
			}
			if (!temps.IsDefaultOrEmpty)
			{
				return new BoundSequence(syntax, temps, ImmutableArray<BoundExpression>.Empty, boundExpression, type, false);
			}
			return boundExpression;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000BE11C File Offset: 0x000BC31C
		private BoundExpression MakeCall(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, TypeSymbol type)
		{
			return this.MakeCall(null, syntax, rewrittenReceiver, method, rewrittenArguments, default(ImmutableArray<RefKind>), false, LookupResultKind.Viable, type, default(ImmutableArray<LocalSymbol>));
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000BE14C File Offset: 0x000BC34C
		private static bool IsSafeForReordering(BoundExpression expression, RefKind kind)
		{
			BoundExpression boundExpression = expression;
			while (!(boundExpression.ConstantValue != null))
			{
				BoundKind kind2 = boundExpression.Kind;
				if (kind2 <= BoundKind.Conversion)
				{
					if (kind2 == BoundKind.PassByCopy)
					{
						return LocalRewriter.IsSafeForReordering(((BoundPassByCopy)boundExpression).Expression, kind);
					}
					if (kind2 == BoundKind.Conversion)
					{
						BoundConversion boundConversion = (BoundConversion)boundExpression;
						switch (boundConversion.ConversionKind)
						{
						case ConversionKind.Identity:
						case ConversionKind.ImplicitNumeric:
						case ConversionKind.ImplicitEnumeration:
						case ConversionKind.ImplicitNullable:
						case ConversionKind.ImplicitReference:
						case ConversionKind.Boxing:
						case ConversionKind.ImplicitPointerToVoid:
						case ConversionKind.ImplicitNullToPointer:
						case ConversionKind.ImplicitDynamic:
						case ConversionKind.ExplicitDynamic:
						case ConversionKind.ExplicitNumeric:
						case ConversionKind.ExplicitEnumeration:
						case ConversionKind.ExplicitNullable:
						case ConversionKind.ExplicitReference:
						case ConversionKind.Unboxing:
						case ConversionKind.ExplicitPointerToPointer:
						case ConversionKind.ExplicitIntegerToPointer:
						case ConversionKind.ExplicitPointerToInteger:
							boundExpression = boundConversion.Operand;
							continue;
						case ConversionKind.ImplicitThrow:
						case ConversionKind.ImplicitUserDefined:
						case ConversionKind.ExplicitUserDefined:
						case ConversionKind.IntPtr:
							return false;
						case ConversionKind.NullLiteral:
						case ConversionKind.ImplicitConstant:
						case ConversionKind.AnonymousFunction:
						case ConversionKind.MethodGroup:
						case ConversionKind.DefaultLiteral:
							return true;
						}
						return false;
					}
				}
				else if (kind2 == BoundKind.Local || kind2 == BoundKind.Parameter)
				{
					return kind > RefKind.None;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000BE26C File Offset: 0x000BC46C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> MakeArguments([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(1)] Symbol methodOrIndexer, bool expanded, ImmutableArray<int> argsToParamsOpt, ref ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<LocalSymbol> temps, bool invokedAsExtensionMethod = false, ThreeState enableCallerInfo = ThreeState.Unknown)
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			rewrittenArguments = this._factory.MakeTempsForDiscardArguments(rewrittenArguments, instance);
			ImmutableArray<ParameterSymbol> parameters = methodOrIndexer.GetParameters();
			bool flag;
			if (LocalRewriter.CanSkipRewriting(rewrittenArguments, methodOrIndexer, expanded, argsToParamsOpt, invokedAsExtensionMethod, false, out flag))
			{
				temps = instance.ToImmutableAndFree();
				argumentRefKindsOpt = LocalRewriter.GetEffectiveArgumentRefKinds(argumentRefKindsOpt, parameters);
				return rewrittenArguments;
			}
			BoundExpression[] array = new BoundExpression[parameters.Length];
			ArrayBuilder<BoundAssignmentOperator> instance2 = ArrayBuilder<BoundAssignmentOperator>.GetInstance(rewrittenArguments.Length);
			ArrayBuilder<RefKind> instance3 = ArrayBuilder<RefKind>.GetInstance(parameters.Length, RefKind.None);
			this.BuildStoresToTemps(expanded, argsToParamsOpt, parameters, argumentRefKindsOpt, rewrittenArguments, false, array, instance3, instance2);
			LocalRewriter.OptimizeTemporaries(array, instance2, instance);
			if (expanded)
			{
				array[array.Length - 1] = this.BuildParamsArray(syntax, methodOrIndexer, argsToParamsOpt, rewrittenArguments, parameters, array[array.Length - 1]);
			}
			if (flag)
			{
				this.RewriteArgumentsForComCall(parameters, array, instance3, instance);
			}
			temps = instance.ToImmutableAndFree();
			instance2.Free();
			argumentRefKindsOpt = LocalRewriter.GetRefKindsOrNull(instance3);
			instance3.Free();
			return array.AsImmutableOrNull<BoundExpression>();
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000BE374 File Offset: 0x000BC574
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static ImmutableArray<RefKind> GetEffectiveArgumentRefKinds(ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters)
		{
			ArrayBuilder<RefKind> arrayBuilder = null;
			for (int i = 0; i < parameters.Length; i++)
			{
				RefKind refKind = parameters[i].RefKind;
				if (refKind == RefKind.In)
				{
					RefKind refKind2 = (argumentRefKindsOpt.IsDefault ? RefKind.None : argumentRefKindsOpt[i]);
					if (arrayBuilder == null)
					{
						if (!argumentRefKindsOpt.IsDefault)
						{
							arrayBuilder = ArrayBuilder<RefKind>.GetInstance(parameters.Length);
							arrayBuilder.AddRange(argumentRefKindsOpt);
						}
						else
						{
							arrayBuilder = ArrayBuilder<RefKind>.GetInstance(parameters.Length, RefKind.None);
						}
					}
					arrayBuilder[i] = ((refKind2 == RefKind.None) ? refKind : ((RefKind)4));
				}
			}
			if (arrayBuilder != null)
			{
				argumentRefKindsOpt = arrayBuilder.ToImmutableAndFree();
			}
			return argumentRefKindsOpt;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000BE408 File Offset: 0x000BC608
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<IArgumentOperation> MakeArgumentsInEvaluationOrder(CSharpOperationFactory operationFactory, CSharpCompilation compilation, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, Symbol methodOrIndexer, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, bool invokedAsExtensionMethod)
		{
			bool flag;
			if (LocalRewriter.CanSkipRewriting(arguments, methodOrIndexer, expanded, argsToParamsOpt, invokedAsExtensionMethod, true, out flag))
			{
				ImmutableArray<ParameterSymbol> parameters = methodOrIndexer.GetParameters();
				ArrayBuilder<IArgumentOperation> instance = ArrayBuilder<IArgumentOperation>.GetInstance(arguments.Length);
				int i;
				for (i = 0; i < parameters.Length; i++)
				{
					ArgumentKind argumentKind = (defaultArguments[i] ? ArgumentKind.DefaultValue : ArgumentKind.Explicit);
					instance.Add(operationFactory.CreateArgumentOperation(argumentKind, parameters[i].GetPublicSymbol(), arguments[i]));
				}
				while (i < arguments.Length)
				{
					ArgumentKind argumentKind2 = (defaultArguments[i] ? ArgumentKind.DefaultValue : ArgumentKind.Explicit);
					instance.Add(operationFactory.CreateArgumentOperation(argumentKind2, null, arguments[i]));
					i++;
				}
				return instance.ToImmutableAndFree();
			}
			return LocalRewriter.BuildArgumentsInEvaluationOrder(operationFactory, syntax, methodOrIndexer, expanded, argsToParamsOpt, defaultArguments, arguments, compilation);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000BE4D4 File Offset: 0x000BC6D4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool CanSkipRewriting([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(1)] Symbol methodOrIndexer, bool expanded, ImmutableArray<int> argsToParamsOpt, bool invokedAsExtensionMethod, bool ignoreComReceiver, out bool isComReceiver)
		{
			isComReceiver = false;
			if (methodOrIndexer.GetIsVararg())
			{
				return true;
			}
			if (!ignoreComReceiver)
			{
				NamedTypeSymbol namedTypeSymbol = (invokedAsExtensionMethod ? (((MethodSymbol)methodOrIndexer).Parameters[0].Type as NamedTypeSymbol) : methodOrIndexer.ContainingType);
				isComReceiver = namedTypeSymbol != null && namedTypeSymbol.IsComImport;
			}
			return rewrittenArguments.Length == methodOrIndexer.GetParameterCount() && argsToParamsOpt.IsDefault && !expanded && !isComReceiver;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000BE550 File Offset: 0x000BC750
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static ImmutableArray<RefKind> GetRefKindsOrNull([global::System.Runtime.CompilerServices.Nullable(1)] ArrayBuilder<RefKind> refKinds)
		{
			ArrayBuilder<RefKind>.Enumerator enumerator = refKinds.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current != RefKind.None)
				{
					return refKinds.ToImmutable();
				}
			}
			return default(ImmutableArray<RefKind>);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000BE588 File Offset: 0x000BC788
		private void BuildStoresToTemps(bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKinds, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, bool forceLambdaSpilling, BoundExpression[] arguments, ArrayBuilder<RefKind> refKinds, ArrayBuilder<BoundAssignmentOperator> storesToTemps)
		{
			for (int i = 0; i < rewrittenArguments.Length; i++)
			{
				BoundExpression boundExpression = rewrittenArguments[i];
				int num = ((!argsToParamsOpt.IsDefault) ? argsToParamsOpt[i] : i);
				RefKind refKind = argumentRefKinds.RefKinds(i);
				RefKind refKind2 = parameters[num].RefKind;
				int num2;
				if (LocalRewriter.IsBeginningOfParamArray(num, i, expanded, arguments.Length, rewrittenArguments, argsToParamsOpt, out num2) && i + num2 == rewrittenArguments.Length)
				{
					return;
				}
				if ((!forceLambdaSpilling || !LocalRewriter.<BuildStoresToTemps>g__isLambdaConversion|136_0(boundExpression)) && LocalRewriter.IsSafeForReordering(boundExpression, refKind))
				{
					arguments[num] = boundExpression;
				}
				else
				{
					BoundAssignmentOperator boundAssignmentOperator;
					BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, (refKind2 == RefKind.In) ? RefKind.In : refKind, SynthesizedLocalKind.LoweringTemp, null);
					storesToTemps.Add(boundAssignmentOperator);
					arguments[num] = boundLocal;
				}
				if (refKind2 == RefKind.In)
				{
					refKind = ((refKind == RefKind.None) ? RefKind.In : ((RefKind)4));
				}
				refKinds[num] = refKind;
			}
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000BE664 File Offset: 0x000BC864
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<IArgumentOperation> BuildArgumentsInEvaluationOrder(CSharpOperationFactory operationFactory, SyntaxNode syntax, Symbol methodOrIndexer, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, CSharpCompilation compilation)
		{
			ImmutableArray<ParameterSymbol> parameters = methodOrIndexer.GetParameters();
			ArrayBuilder<IArgumentOperation> instance = ArrayBuilder<IArgumentOperation>.GetInstance(parameters.Length);
			bool flag = false;
			for (int i = 0; i < arguments.Length; i++)
			{
				BoundExpression boundExpression = arguments[i];
				int num = ((!argsToParamsOpt.IsDefault) ? argsToParamsOpt[i] : i);
				ParameterSymbol parameterSymbol = parameters[num];
				if (!flag)
				{
					flag = num == parameters.Length - 1;
				}
				ArgumentKind argumentKind = (defaultArguments[i] ? ArgumentKind.DefaultValue : ArgumentKind.Explicit);
				int num2;
				if (LocalRewriter.IsBeginningOfParamArray(num, i, expanded, parameters.Length, arguments, argsToParamsOpt, out num2))
				{
					int num3 = i + num2;
					argumentKind = ArgumentKind.ParamArray;
					ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance(num2);
					for (int j = i; j < num3; j++)
					{
						instance2.Add(arguments[j]);
					}
					i = num3 - 1;
					boundExpression = LocalRewriter.CreateParamArrayArgument(syntax, parameterSymbol.Type, instance2.ToImmutableAndFree(), compilation, null);
				}
				instance.Add(operationFactory.CreateArgumentOperation(argumentKind, parameterSymbol.GetPublicSymbol(), boundExpression));
			}
			ParameterSymbol parameterSymbol2;
			if (parameters.IsEmpty)
			{
				parameterSymbol2 = null;
			}
			else
			{
				int num4 = parameters.Length - 1;
				parameterSymbol2 = parameters[num4];
			}
			ParameterSymbol parameterSymbol3 = parameterSymbol2;
			if (expanded && parameterSymbol3 != null && !flag)
			{
				BoundExpression boundExpression2 = LocalRewriter.CreateParamArrayArgument(syntax, parameterSymbol3.Type, ImmutableArray<BoundExpression>.Empty, compilation, null);
				ArgumentKind argumentKind2 = ArgumentKind.ParamArray;
				instance.Add(operationFactory.CreateArgumentOperation(argumentKind2, parameterSymbol3.GetPublicSymbol(), boundExpression2));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000BE7D8 File Offset: 0x000BC9D8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool IsBeginningOfParamArray(int parameterIndex, int argumentIndex, bool expanded, int parameterCount, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ImmutableArray<int> argsToParamsOpt, out int numberOfParamArrayArguments)
		{
			numberOfParamArrayArguments = 0;
			if (expanded && parameterIndex == parameterCount - 1)
			{
				int num = argumentIndex + 1;
				while (num < arguments.Length && ((!argsToParamsOpt.IsDefault) ? argsToParamsOpt[num] : num) == parameterCount - 1)
				{
					num++;
				}
				numberOfParamArrayArguments = num - argumentIndex;
				return true;
			}
			return false;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000BE828 File Offset: 0x000BCA28
		private BoundExpression BuildParamsArray(SyntaxNode syntax, Symbol methodOrIndexer, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters, BoundExpression tempStoreArgument)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			int num = parameters.Length - 1;
			if (tempStoreArgument != null)
			{
				instance.Add(tempStoreArgument);
			}
			else
			{
				for (int i = 0; i < rewrittenArguments.Length; i++)
				{
					BoundExpression boundExpression = rewrittenArguments[i];
					if (((!argsToParamsOpt.IsDefault) ? argsToParamsOpt[i] : i) == num)
					{
						instance.Add(boundExpression);
					}
				}
			}
			TypeSymbol type = parameters[num].Type;
			ImmutableArray<BoundExpression> immutableArray = instance.ToImmutableAndFree();
			if (immutableArray.Length == 0 && !this._inExpressionLambda)
			{
				ArrayTypeSymbol arrayTypeSymbol = type as ArrayTypeSymbol;
				if (arrayTypeSymbol != null && !arrayTypeSymbol.ElementType.IsPointerOrFunctionPointer())
				{
					MethodSymbol methodSymbol = this._compilation.GetWellKnownTypeMember(WellKnownMember.System_Array__Empty) as MethodSymbol;
					if (methodSymbol != null)
					{
						this._diagnostics.ReportUseSite(methodSymbol, syntax);
						methodSymbol = methodSymbol.Construct(ImmutableArray.Create<TypeSymbol>(arrayTypeSymbol.ElementType));
						return new BoundCall(syntax, null, methodSymbol, ImmutableArray<BoundExpression>.Empty, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, false, false, default(ImmutableArray<int>), default(BitVector), LookupResultKind.Viable, methodSymbol.ReturnType, false);
					}
				}
			}
			return LocalRewriter.CreateParamArrayArgument(syntax, type, immutableArray, this._compilation, this);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000BE978 File Offset: 0x000BCB78
		private static BoundExpression CreateParamArrayArgument(SyntaxNode syntax, TypeSymbol paramArrayType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arrayArgs, CSharpCompilation compilation, [global::System.Runtime.CompilerServices.Nullable(2)] LocalRewriter localRewriter)
		{
			TypeSymbol specialType = compilation.GetSpecialType(SpecialType.System_Int32);
			BoundExpression boundExpression = LocalRewriter.MakeLiteral(syntax, ConstantValue.Create(arrayArgs.Length), specialType, localRewriter);
			return new BoundArrayCreation(syntax, ImmutableArray.Create<BoundExpression>(boundExpression), new BoundArrayInitialization(syntax, arrayArgs, false)
			{
				WasCompilerGenerated = true
			}, paramArrayType, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000BE9C8 File Offset: 0x000BCBC8
		private static BoundExpression MakeLiteral(SyntaxNode syntax, ConstantValue constantValue, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] LocalRewriter localRewriter)
		{
			if (localRewriter != null)
			{
				return localRewriter.MakeLiteral(syntax, constantValue, type, null);
			}
			return new BoundLiteral(syntax, constantValue, type, constantValue.IsBad)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000BE9F0 File Offset: 0x000BCBF0
		private static void OptimizeTemporaries(BoundExpression[] arguments, ArrayBuilder<BoundAssignmentOperator> storesToTemps, ArrayBuilder<LocalSymbol> temporariesBuilder)
		{
			if (storesToTemps.Count > 0 && LocalRewriter.MergeArgumentsAndSideEffects(arguments, storesToTemps) > 0)
			{
				foreach (BoundAssignmentOperator boundAssignmentOperator in storesToTemps)
				{
					if (boundAssignmentOperator != null)
					{
						temporariesBuilder.Add(((BoundLocal)boundAssignmentOperator.Left).LocalSymbol);
					}
				}
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000BEA44 File Offset: 0x000BCC44
		private static int MergeArgumentsAndSideEffects(BoundExpression[] arguments, ArrayBuilder<BoundAssignmentOperator> tempStores)
		{
			int num = tempStores.Count;
			int num2 = 0;
			for (int i = 0; i < arguments.Length; i++)
			{
				BoundExpression boundExpression = arguments[i];
				if (boundExpression != null && boundExpression.Kind == BoundKind.Local)
				{
					int num3 = -1;
					for (int j = num2; j < tempStores.Count; j++)
					{
						if (tempStores[j].Left == boundExpression)
						{
							num3 = j;
							break;
						}
					}
					if (num3 != -1)
					{
						BoundExpression right = tempStores[num3].Right;
						tempStores[num3] = null;
						num--;
						if (num3 == num2)
						{
							arguments[i] = right;
						}
						else
						{
							BoundExpression[] array = new BoundExpression[num3 - num2];
							for (int k = 0; k < array.Length; k++)
							{
								array[k] = tempStores[num2 + k];
							}
							arguments[i] = new BoundSequence(right.Syntax, ImmutableArray<LocalSymbol>.Empty, array.AsImmutableOrNull<BoundExpression>(), right, right.Type, false);
						}
						num2 = num3 + 1;
					}
				}
			}
			return num;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000BEB38 File Offset: 0x000BCD38
		private void RewriteArgumentsForComCall([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters, BoundExpression[] actualArguments, ArrayBuilder<RefKind> argsRefKindsBuilder, ArrayBuilder<LocalSymbol> temporariesBuilder)
		{
			int num = actualArguments.Length;
			for (int i = 0; i < num; i++)
			{
				RefKind refKind = parameters[i].RefKind;
				if (argsRefKindsBuilder[i] == RefKind.None && refKind == RefKind.Ref)
				{
					BoundExpression boundExpression = actualArguments[i];
					if (boundExpression.Kind != BoundKind.Local || ((BoundLocal)boundExpression).LocalSymbol.RefKind != RefKind.Ref)
					{
						BoundAssignmentOperator boundAssignmentOperator;
						BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
						actualArguments[i] = new BoundSequence(boundExpression.Syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundLocal, boundLocal.Type, false);
						argsRefKindsBuilder[i] = RefKind.Ref;
						temporariesBuilder.Add(boundLocal.LocalSymbol);
					}
				}
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000BEBE8 File Offset: 0x000BCDE8
		public override BoundNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node)
		{
			if (node.Invoked)
			{
				return node;
			}
			BoundExpression boundExpression = this.VisitExpression(node.Receiver);
			return this._dynamicFactory.MakeDynamicGetMember(boundExpression, node.Name, node.Indexed).ToExpression();
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000BEC2C File Offset: 0x000BCE2C
		public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
		{
			return this.VisitCompoundAssignmentOperator(node, true);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000BEC38 File Offset: 0x000BCE38
		private BoundExpression VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node, bool used)
		{
			LocalRewriter.<>c__DisplayClass147_0 CS$<>8__locals1;
			CS$<>8__locals1.node = node;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.used = used;
			CS$<>8__locals1.loweredRight = this.VisitExpression(CS$<>8__locals1.node.Right);
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			BinaryOperatorKind kind = CS$<>8__locals1.node.Operator.Kind;
			CS$<>8__locals1.isChecked = kind.IsChecked();
			CS$<>8__locals1.isDynamic = kind.IsDynamic();
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			CS$<>8__locals1.transformedLHS = this.TransformCompoundAssignmentLHS(CS$<>8__locals1.node.Left, instance2, instance, CS$<>8__locals1.isDynamic);
			BoundExpression boundExpression = this.MakeRValue(CS$<>8__locals1.transformedLHS);
			BoundExpression boundExpression2;
			if (CS$<>8__locals1.node.Left.Kind == BoundKind.DynamicMemberAccess && (binaryOperatorKind == BinaryOperatorKind.Addition || binaryOperatorKind == BinaryOperatorKind.Subtraction))
			{
				ArrayBuilder<LocalSymbol> instance3 = ArrayBuilder<LocalSymbol>.GetInstance();
				ArrayBuilder<BoundExpression> instance4 = ArrayBuilder<BoundExpression>.GetInstance();
				BoundDynamicMemberAccess boundDynamicMemberAccess = (BoundDynamicMemberAccess)CS$<>8__locals1.transformedLHS;
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(this._dynamicFactory.MakeDynamicIsEventTest(boundDynamicMemberAccess.Name, boundDynamicMemberAccess.Receiver).ToExpression(), out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				instance3.Add(boundLocal.LocalSymbol);
				instance4.Add(boundAssignmentOperator);
				BoundAssignmentOperator boundAssignmentOperator2;
				boundExpression = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				instance3.Add(((BoundLocal)boundExpression).LocalSymbol);
				BoundAssignmentOperator boundAssignmentOperator3;
				BoundLocal boundLocal2 = this._factory.StoreToTemp(this._factory.Conditional(this._factory.Not(boundLocal), boundAssignmentOperator2, this._factory.Null(boundAssignmentOperator2.Type), boundAssignmentOperator2.Type), out boundAssignmentOperator3, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				instance3.Add(boundLocal2.LocalSymbol);
				instance4.Add(boundAssignmentOperator3);
				if (LocalRewriter.CanChangeValueBetweenReads(CS$<>8__locals1.loweredRight, true, false))
				{
					BoundAssignmentOperator boundAssignmentOperator4;
					CS$<>8__locals1.loweredRight = this._factory.StoreToTemp(CS$<>8__locals1.loweredRight, out boundAssignmentOperator4, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
					instance3.Add(((BoundLocal)CS$<>8__locals1.loweredRight).LocalSymbol);
					instance4.Add(boundAssignmentOperator4);
				}
				LoweredDynamicOperation loweredDynamicOperation = this._dynamicFactory.MakeDynamicEventAccessorInvocation(((binaryOperatorKind == BinaryOperatorKind.Addition) ? "add_" : "remove_") + boundDynamicMemberAccess.Name, boundDynamicMemberAccess.Receiver, CS$<>8__locals1.loweredRight);
				boundExpression2 = this.<VisitCompoundAssignmentOperator>g__rewriteAssignment|147_0(boundExpression, ref CS$<>8__locals1);
				BoundExpression boundExpression3 = this._factory.Conditional(boundLocal, loweredDynamicOperation.ToExpression(), boundExpression2, boundExpression2.Type);
				boundExpression2 = new BoundSequence(CS$<>8__locals1.node.Syntax, instance3.ToImmutableAndFree(), instance4.ToImmutableAndFree(), boundExpression3, boundExpression3.Type, false);
			}
			else
			{
				boundExpression2 = this.<VisitCompoundAssignmentOperator>g__rewriteAssignment|147_0(boundExpression, ref CS$<>8__locals1);
			}
			BoundExpression boundExpression4 = ((instance.Count == 0 && instance2.Count == 0) ? boundExpression2 : new BoundSequence(CS$<>8__locals1.node.Syntax, instance.ToImmutable(), instance2.ToImmutable(), boundExpression2, boundExpression2.Type, false));
			instance.Free();
			instance2.Free();
			return boundExpression4;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000BEF30 File Offset: 0x000BD130
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression TransformPropertyOrEventReceiver(Symbol propertyOrEvent, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps)
		{
			if (receiverOpt == null || propertyOrEvent.IsStatic || !LocalRewriter.CanChangeValueBetweenReads(receiverOpt, true, false))
			{
				return receiverOpt;
			}
			BoundExpression boundExpression = this.VisitExpression(receiverOpt);
			bool flag = boundExpression.Type.IsValueType || boundExpression.Type.Kind == SymbolKind.TypeParameter;
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, flag ? RefKind.Ref : RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			stores.Add(boundAssignmentOperator);
			temps.Add(boundLocal.LocalSymbol);
			return boundLocal;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000BEFAC File Offset: 0x000BD1AC
		private BoundDynamicMemberAccess TransformDynamicMemberAccess(BoundDynamicMemberAccess memberAccess, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps)
		{
			if (!LocalRewriter.CanChangeValueBetweenReads(memberAccess.Receiver, true, false))
			{
				return memberAccess;
			}
			BoundExpression boundExpression = this.VisitExpression(memberAccess.Receiver);
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			stores.Add(boundAssignmentOperator);
			temps.Add(boundLocal.LocalSymbol);
			return new BoundDynamicMemberAccess(memberAccess.Syntax, boundLocal, memberAccess.TypeArgumentsOpt, memberAccess.Name, memberAccess.Invoked, memberAccess.Indexed, memberAccess.Type, false);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000BF028 File Offset: 0x000BD228
		private BoundIndexerAccess TransformIndexerAccess(BoundIndexerAccess indexerAccess, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps)
		{
			BoundExpression receiverOpt = indexerAccess.ReceiverOpt;
			BoundExpression boundExpression2;
			if (LocalRewriter.CanChangeValueBetweenReads(receiverOpt, true, false))
			{
				BoundExpression boundExpression = this.VisitExpression(receiverOpt);
				bool flag = boundExpression.Type.IsValueType || boundExpression.Type.Kind == SymbolKind.TypeParameter;
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, flag ? RefKind.Ref : RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				boundExpression2 = boundLocal;
				stores.Add(boundAssignmentOperator);
				temps.Add(boundLocal.LocalSymbol);
			}
			else
			{
				boundExpression2 = this.VisitExpression(receiverOpt);
			}
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(indexerAccess.Arguments);
			SyntaxNode syntax = indexerAccess.Syntax;
			PropertySymbol indexer = indexerAccess.Indexer;
			ImmutableArray<RefKind> immutableArray2 = indexerAccess.ArgumentRefKindsOpt;
			bool expanded = indexerAccess.Expanded;
			ImmutableArray<int> argsToParamsOpt = indexerAccess.ArgsToParamsOpt;
			ImmutableArray<ParameterSymbol> parameters = indexer.Parameters;
			BoundExpression[] array = new BoundExpression[parameters.Length];
			ArrayBuilder<BoundAssignmentOperator> instance = ArrayBuilder<BoundAssignmentOperator>.GetInstance(immutableArray.Length);
			ArrayBuilder<RefKind> instance2 = ArrayBuilder<RefKind>.GetInstance(parameters.Length, RefKind.None);
			this.BuildStoresToTemps(expanded, argsToParamsOpt, parameters, immutableArray2, immutableArray, true, array, instance2, instance);
			if (expanded)
			{
				BoundExpression boundExpression3 = this.BuildParamsArray(syntax, indexer, argsToParamsOpt, immutableArray, parameters, array[array.Length - 1]);
				BoundAssignmentOperator boundAssignmentOperator2;
				BoundLocal boundLocal2 = this._factory.StoreToTemp(boundExpression3, out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				stores.Add(boundAssignmentOperator2);
				temps.Add(boundLocal2.LocalSymbol);
				array[array.Length - 1] = boundLocal2;
			}
			if (indexer.GetOwnOrInheritedGetMethod() == null)
			{
				indexer.GetOwnOrInheritedSetMethod();
			}
			if (indexer.ContainingType.IsComImport)
			{
				this.RewriteArgumentsForComCall(parameters, array, instance2, temps);
			}
			immutableArray = array.AsImmutableOrNull<BoundExpression>();
			foreach (BoundAssignmentOperator boundAssignmentOperator3 in instance)
			{
				temps.Add(((BoundLocal)boundAssignmentOperator3.Left).LocalSymbol);
				stores.Add(boundAssignmentOperator3);
			}
			instance.Free();
			immutableArray2 = LocalRewriter.GetRefKindsOrNull(instance2);
			instance2.Free();
			return new BoundIndexerAccess(syntax, boundExpression2, indexer, immutableArray, default(ImmutableArray<string>), immutableArray2, false, default(ImmutableArray<int>), default(BitVector), indexerAccess.Type, false);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000BF23C File Offset: 0x000BD43C
		private BoundExpression TransformPatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess indexerAccess, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps, bool isDynamicAssignment)
		{
			BoundSequence boundSequence = this.VisitIndexOrRangePatternIndexerAccess(indexerAccess, true);
			stores.AddRange(boundSequence.SideEffects);
			temps.AddRange(boundSequence.Locals);
			return this.TransformCompoundAssignmentLHS(boundSequence.Value, stores, temps, isDynamicAssignment);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000BF27C File Offset: 0x000BD47C
		private bool TransformCompoundAssignmentFieldOrEventAccessReceiver(Symbol fieldOrEvent, [global::System.Runtime.CompilerServices.Nullable(2)] ref BoundExpression receiver, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps)
		{
			if (fieldOrEvent.IsStatic)
			{
				return true;
			}
			if (!LocalRewriter.CanChangeValueBetweenReads(receiver, true, false))
			{
				return true;
			}
			if (!receiver.Type.IsReferenceType)
			{
				return false;
			}
			BoundExpression boundExpression = this.VisitExpression(receiver);
			if (boundExpression.Type.IsTypeParameter())
			{
				NamedTypeSymbol containingType = fieldOrEvent.ContainingType;
				boundExpression = this.BoxReceiver(boundExpression, containingType);
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			stores.Add(boundAssignmentOperator);
			temps.Add(boundLocal.LocalSymbol);
			receiver = boundLocal;
			return true;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000BF304 File Offset: 0x000BD504
		private BoundDynamicIndexerAccess TransformDynamicIndexerAccess(BoundDynamicIndexerAccess indexerAccess, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps)
		{
			BoundExpression boundExpression;
			if (LocalRewriter.CanChangeValueBetweenReads(indexerAccess.Receiver, true, false))
			{
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(this.VisitExpression(indexerAccess.Receiver), out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				stores.Add(boundAssignmentOperator);
				temps.Add(boundLocal.LocalSymbol);
				boundExpression = boundLocal;
			}
			else
			{
				boundExpression = indexerAccess.Receiver;
			}
			ImmutableArray<BoundExpression> arguments = indexerAccess.Arguments;
			BoundExpression[] array = new BoundExpression[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				if (LocalRewriter.CanChangeValueBetweenReads(arguments[i], true, false))
				{
					BoundAssignmentOperator boundAssignmentOperator2;
					BoundLocal boundLocal2 = this._factory.StoreToTemp(this.VisitExpression(arguments[i]), out boundAssignmentOperator2, (indexerAccess.ArgumentRefKindsOpt.RefKinds(i) != RefKind.None) ? RefKind.Ref : RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
					stores.Add(boundAssignmentOperator2);
					temps.Add(boundLocal2.LocalSymbol);
					array[i] = boundLocal2;
				}
				else
				{
					array[i] = arguments[i];
				}
			}
			return new BoundDynamicIndexerAccess(indexerAccess.Syntax, boundExpression, array.AsImmutableOrNull<BoundExpression>(), indexerAccess.ArgumentNamesOpt, indexerAccess.ArgumentRefKindsOpt, indexerAccess.ApplicableIndexers, indexerAccess.Type, false);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000BF428 File Offset: 0x000BD628
		private BoundExpression TransformCompoundAssignmentLHS(BoundExpression originalLHS, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps, bool isDynamicAssignment)
		{
			BoundKind kind = originalLHS.Kind;
			if (kind <= BoundKind.ThisReference)
			{
				if (kind <= BoundKind.AssignmentOperator)
				{
					switch (kind)
					{
					case BoundKind.PointerIndirectionOperator:
					case BoundKind.PointerElementAccess:
					case BoundKind.FunctionPointerInvocation:
					case BoundKind.RefValueOperator:
						goto IL_02F5;
					case BoundKind.RefTypeOperator:
					case BoundKind.MakeRefOperator:
						goto IL_02E4;
					default:
						if (kind != BoundKind.AssignmentOperator)
						{
							goto IL_02E4;
						}
						goto IL_02F5;
					}
				}
				else
				{
					if (kind == BoundKind.ConditionalOperator)
					{
						goto IL_02F5;
					}
					if (kind != BoundKind.ArrayAccess)
					{
						if (kind != BoundKind.ThisReference)
						{
							goto IL_02E4;
						}
					}
					else
					{
						BoundArrayAccess boundArrayAccess = (BoundArrayAccess)originalLHS;
						if (isDynamicAssignment || !LocalRewriter.IsInvariantArray(boundArrayAccess.Expression.Type))
						{
							BoundExpression boundExpression = this.VisitExpression(boundArrayAccess.Expression);
							ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(boundArrayAccess.Indices);
							return this.SpillArrayElementAccess(boundExpression, immutableArray, stores, temps);
						}
						goto IL_02F5;
					}
				}
			}
			else if (kind <= BoundKind.Parameter)
			{
				if (kind - BoundKind.Local > 1 && kind != BoundKind.Parameter)
				{
					goto IL_02E4;
				}
			}
			else
			{
				if (kind == BoundKind.DynamicMemberAccess)
				{
					return this.TransformDynamicMemberAccess((BoundDynamicMemberAccess)originalLHS, stores, temps);
				}
				if (kind == BoundKind.Call)
				{
					goto IL_02F5;
				}
				switch (kind)
				{
				case BoundKind.FieldAccess:
				{
					BoundFieldAccess boundFieldAccess = (BoundFieldAccess)originalLHS;
					BoundExpression receiverOpt = boundFieldAccess.ReceiverOpt;
					if (this.TransformCompoundAssignmentFieldOrEventAccessReceiver(boundFieldAccess.FieldSymbol, ref receiverOpt, stores, temps))
					{
						return this.MakeFieldAccess(boundFieldAccess.Syntax, receiverOpt, boundFieldAccess.FieldSymbol, boundFieldAccess.ConstantValueOpt, boundFieldAccess.ResultKind, boundFieldAccess.Type, boundFieldAccess);
					}
					goto IL_02F5;
				}
				case BoundKind.HoistedFieldAccess:
					goto IL_02E4;
				case BoundKind.PropertyAccess:
				{
					BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)originalLHS;
					if (boundPropertyAccess.PropertySymbol.RefKind == RefKind.None)
					{
						return boundPropertyAccess.Update(this.TransformPropertyOrEventReceiver(boundPropertyAccess.PropertySymbol, boundPropertyAccess.ReceiverOpt, stores, temps), boundPropertyAccess.PropertySymbol, boundPropertyAccess.ResultKind, boundPropertyAccess.Type);
					}
					goto IL_02F5;
				}
				case BoundKind.EventAccess:
				{
					BoundEventAccess boundEventAccess = (BoundEventAccess)originalLHS;
					BoundExpression receiverOpt2 = boundEventAccess.ReceiverOpt;
					if (boundEventAccess.EventSymbol.IsWindowsRuntimeEvent)
					{
						return boundEventAccess.Update(this.TransformPropertyOrEventReceiver(boundEventAccess.EventSymbol, boundEventAccess.ReceiverOpt, stores, temps), boundEventAccess.EventSymbol, boundEventAccess.IsUsableAsField, boundEventAccess.ResultKind, boundEventAccess.Type);
					}
					if (this.TransformCompoundAssignmentFieldOrEventAccessReceiver(boundEventAccess.EventSymbol, ref receiverOpt2, stores, temps))
					{
						return this.MakeEventAccess(boundEventAccess.Syntax, receiverOpt2, boundEventAccess.EventSymbol, boundEventAccess.ConstantValue, boundEventAccess.ResultKind, boundEventAccess.Type);
					}
					goto IL_02F5;
				}
				case BoundKind.IndexerAccess:
					if (((BoundIndexerAccess)originalLHS).Indexer.RefKind == RefKind.None)
					{
						return this.TransformIndexerAccess((BoundIndexerAccess)originalLHS, stores, temps);
					}
					goto IL_02F5;
				case BoundKind.IndexOrRangePatternIndexerAccess:
				{
					BoundIndexOrRangePatternIndexerAccess boundIndexOrRangePatternIndexerAccess = (BoundIndexOrRangePatternIndexerAccess)originalLHS;
					Symbol patternSymbol = boundIndexOrRangePatternIndexerAccess.PatternSymbol;
					PropertySymbol propertySymbol = patternSymbol as PropertySymbol;
					RefKind refKind;
					if (propertySymbol == null)
					{
						MethodSymbol methodSymbol = patternSymbol as MethodSymbol;
						if (methodSymbol == null)
						{
							throw ExceptionUtilities.UnexpectedValue(patternSymbol);
						}
						refKind = methodSymbol.RefKind;
					}
					else
					{
						refKind = propertySymbol.RefKind;
					}
					if (refKind == RefKind.None)
					{
						return this.TransformPatternIndexerAccess(boundIndexOrRangePatternIndexerAccess, stores, temps, isDynamicAssignment);
					}
					goto IL_02F5;
				}
				case BoundKind.DynamicIndexerAccess:
					return this.TransformDynamicIndexerAccess((BoundDynamicIndexerAccess)originalLHS, stores, temps);
				default:
					goto IL_02E4;
				}
			}
			return originalLHS;
			IL_02E4:
			throw ExceptionUtilities.UnexpectedValue(originalLHS.Kind);
			IL_02F5:
			BoundExpression boundExpression2 = this.VisitExpression(originalLHS);
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression2, out boundAssignmentOperator, RefKind.Ref, SynthesizedLocalKind.LoweringTemp, null);
			stores.Add(boundAssignmentOperator);
			temps.Add(boundLocal.LocalSymbol);
			return boundLocal;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000BF759 File Offset: 0x000BD959
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool IsInvariantArray(TypeSymbol type)
		{
			ArrayTypeSymbol arrayTypeSymbol = type as ArrayTypeSymbol;
			return arrayTypeSymbol != null && arrayTypeSymbol.ElementType.IsSealed;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000BF771 File Offset: 0x000BD971
		private BoundExpression BoxReceiver(BoundExpression rewrittenReceiver, NamedTypeSymbol memberContainingType)
		{
			return this.MakeConversionNode(rewrittenReceiver.Syntax, rewrittenReceiver, Conversion.Boxing, memberContainingType, false, false, rewrittenReceiver.ConstantValue);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000BF790 File Offset: 0x000BD990
		private BoundExpression SpillArrayElementAccess(BoundExpression loweredExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredIndices, ArrayBuilder<BoundExpression> stores, ArrayBuilder<LocalSymbol> temps)
		{
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(loweredExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			stores.Add(boundAssignmentOperator);
			temps.Add(boundLocal.LocalSymbol);
			BoundLocal boundLocal2 = boundLocal;
			BoundExpression[] array = new BoundExpression[loweredIndices.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (LocalRewriter.CanChangeValueBetweenReads(loweredIndices[i], true, false))
				{
					BoundAssignmentOperator boundAssignmentOperator2;
					BoundLocal boundLocal3 = this._factory.StoreToTemp(loweredIndices[i], out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
					stores.Add(boundAssignmentOperator2);
					temps.Add(boundLocal3.LocalSymbol);
					array[i] = boundLocal3;
				}
				else
				{
					array[i] = loweredIndices[i];
				}
			}
			return this._factory.ArrayAccess(boundLocal2, array);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000BF84C File Offset: 0x000BDA4C
		internal static bool CanChangeValueBetweenReads(BoundExpression expression, bool localsMayBeAssignedOrCaptured = true, bool structThisCanChangeValueBetweenReads = false)
		{
			if (expression.IsDefaultValue())
			{
				return false;
			}
			if (expression.ConstantValue != null)
			{
				return !LocalRewriter.ConstantValueIsTrivial(expression.Type);
			}
			BoundKind kind = expression.Kind;
			if (kind != BoundKind.TypeExpression)
			{
				switch (kind)
				{
				case BoundKind.Literal:
					return !LocalRewriter.ConstantValueIsTrivial(expression.Type);
				case BoundKind.ThisReference:
					return structThisCanChangeValueBetweenReads && ((BoundThisReference)expression).Type.IsStructType();
				case BoundKind.BaseReference:
					return false;
				case BoundKind.Local:
					return localsMayBeAssignedOrCaptured || ((BoundLocal)expression).LocalSymbol.RefKind > RefKind.None;
				case BoundKind.Parameter:
					return localsMayBeAssignedOrCaptured || ((BoundParameter)expression).ParameterSymbol.RefKind > RefKind.None;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000BF91C File Offset: 0x000BDB1C
		internal static bool ReadIsSideeffecting(BoundExpression expression)
		{
			if (expression.ConstantValue != null)
			{
				return false;
			}
			if (expression.IsDefaultValue())
			{
				return false;
			}
			BoundKind kind = expression.Kind;
			if (kind <= BoundKind.Parameter)
			{
				if (kind == BoundKind.PassByCopy)
				{
					return LocalRewriter.ReadIsSideeffecting(((BoundPassByCopy)expression).Expression);
				}
				if (kind == BoundKind.Conversion)
				{
					BoundConversion boundConversion = (BoundConversion)expression;
					return boundConversion.ConversionHasSideEffects() || LocalRewriter.ReadIsSideeffecting(boundConversion.Operand);
				}
				switch (kind)
				{
				case BoundKind.Literal:
				case BoundKind.ThisReference:
				case BoundKind.BaseReference:
				case BoundKind.Local:
				case BoundKind.Parameter:
					break;
				case BoundKind.PreviousSubmissionReference:
				case BoundKind.HostObjectMemberReference:
				case BoundKind.PseudoVariable:
				case BoundKind.RangeVariable:
					return true;
				default:
					return true;
				}
			}
			else
			{
				if (kind == BoundKind.Call)
				{
					BoundCall boundCall = (BoundCall)expression;
					MethodSymbol method = boundCall.Method;
					NamedTypeSymbol containingType = method.ContainingType;
					return containingType == null || !containingType.IsNullableType() || (!LocalRewriter.IsSpecialMember(method, SpecialMember.System_Nullable_T_GetValueOrDefault) && !LocalRewriter.IsSpecialMember(method, SpecialMember.System_Nullable_T_get_HasValue)) || LocalRewriter.ReadIsSideeffecting(boundCall.ReceiverOpt);
				}
				if (kind != BoundKind.ObjectCreationExpression)
				{
					if (kind != BoundKind.Lambda)
					{
						return true;
					}
				}
				else
				{
					if (expression.Type.IsNullableType())
					{
						BoundObjectCreationExpression boundObjectCreationExpression = (BoundObjectCreationExpression)expression;
						return boundObjectCreationExpression.Arguments.Length == 1 && LocalRewriter.ReadIsSideeffecting(boundObjectCreationExpression.Arguments[0]);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000BFA61 File Offset: 0x000BDC61
		private static bool IsSpecialMember(MethodSymbol method, SpecialMember specialMember)
		{
			method = method.OriginalDefinition;
			AssemblySymbol containingAssembly = method.ContainingAssembly;
			return ((containingAssembly != null) ? containingAssembly.GetSpecialTypeMember(specialMember) : null) == method;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000BFA84 File Offset: 0x000BDC84
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool ConstantValueIsTrivial(TypeSymbol type)
		{
			return type == null || type.SpecialType.IsClrInteger() || type.IsReferenceType || type.IsEnumType();
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000BFAA6 File Offset: 0x000BDCA6
		public override BoundNode VisitConditionalAccess(BoundConditionalAccess node)
		{
			return this.RewriteConditionalAccess(node, true);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000BFAB0 File Offset: 0x000BDCB0
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000BFAB8 File Offset: 0x000BDCB8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal BoundExpression RewriteConditionalAccess(BoundConditionalAccess node, bool used)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Receiver);
			TypeSymbol type = boundExpression.Type;
			if (boundExpression.IsDefaultValue() && type.IsReferenceType)
			{
				return this._factory.Default(node.Type);
			}
			LocalRewriter.ConditionalAccessLoweringKind conditionalAccessLoweringKind;
			if (!node.AccessExpression.Type.IsDynamic())
			{
				conditionalAccessLoweringKind = LocalRewriter.ConditionalAccessLoweringKind.LoweredConditionalAccess;
			}
			else if (LocalRewriter.CanChangeValueBetweenReads(boundExpression, true, false))
			{
				conditionalAccessLoweringKind = LocalRewriter.ConditionalAccessLoweringKind.ConditionalCaptureReceiverByVal;
			}
			else
			{
				conditionalAccessLoweringKind = LocalRewriter.ConditionalAccessLoweringKind.Conditional;
			}
			BoundExpression currentConditionalAccessTarget = this._currentConditionalAccessTarget;
			int num = this._currentConditionalAccessID + 1;
			this._currentConditionalAccessID = num;
			int num2 = num;
			LocalSymbol localSymbol = null;
			switch (conditionalAccessLoweringKind)
			{
			case LocalRewriter.ConditionalAccessLoweringKind.LoweredConditionalAccess:
				this._currentConditionalAccessTarget = new BoundConditionalReceiver(boundExpression.Syntax, num2, type);
				break;
			case LocalRewriter.ConditionalAccessLoweringKind.Conditional:
				this._currentConditionalAccessTarget = boundExpression;
				break;
			case LocalRewriter.ConditionalAccessLoweringKind.ConditionalCaptureReceiverByVal:
				localSymbol = this._factory.SynthesizedLocal(type, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				this._currentConditionalAccessTarget = this._factory.Local(localSymbol);
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(conditionalAccessLoweringKind);
			}
			BoundExpression boundExpression2;
			if (used)
			{
				boundExpression2 = this.VisitExpression(node.AccessExpression);
			}
			else
			{
				boundExpression2 = this.VisitUnusedExpression(node.AccessExpression);
				if (boundExpression2 == null)
				{
					return null;
				}
			}
			this._currentConditionalAccessTarget = currentConditionalAccessTarget;
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			TypeSymbol typeSymbol2 = node.Type;
			TypeSymbol type2 = boundExpression2.Type;
			if (type2.IsVoidType())
			{
				typeSymbol2 = (typeSymbol = type2);
			}
			if (!TypeSymbol.Equals(type2, typeSymbol2, TypeCompareKind.ConsiderEverything) && typeSymbol2.IsNullableType())
			{
				boundExpression2 = this._factory.New((NamedTypeSymbol)typeSymbol2, new BoundExpression[] { boundExpression2 });
			}
			NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Object);
			switch (conditionalAccessLoweringKind)
			{
			case LocalRewriter.ConditionalAccessLoweringKind.LoweredConditionalAccess:
				return new BoundLoweredConditionalAccess(node.Syntax, boundExpression, type.IsNullableType() ? this.UnsafeGetNullableMethod(node.Syntax, boundExpression.Type, SpecialMember.System_Nullable_T_get_HasValue) : null, boundExpression2, null, num2, typeSymbol, false);
			case LocalRewriter.ConditionalAccessLoweringKind.Conditional:
				break;
			case LocalRewriter.ConditionalAccessLoweringKind.ConditionalCaptureReceiverByVal:
				boundExpression = this._factory.MakeSequence(new BoundExpression[]
				{
					this._factory.AssignmentExpression(this._factory.Local(localSymbol), boundExpression, false),
					this._factory.Local(localSymbol)
				});
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(conditionalAccessLoweringKind);
			}
			BoundBinaryOperator boundBinaryOperator = this._factory.ObjectNotEqual(this._factory.Convert(specialType, boundExpression), this._factory.Null(specialType));
			BoundExpression boundExpression3 = boundExpression2;
			BoundExpression boundExpression4 = LocalRewriter.RewriteConditionalOperator(node.Syntax, boundBinaryOperator, boundExpression3, this._factory.Default(typeSymbol2), null, typeSymbol2, false);
			if (localSymbol != null)
			{
				boundExpression4 = this._factory.MakeSequence(localSymbol, new BoundExpression[] { boundExpression4 });
			}
			return boundExpression4;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000BFD5C File Offset: 0x000BDF5C
		public override BoundNode VisitConditionalReceiver(BoundConditionalReceiver node)
		{
			BoundExpression boundExpression = this._currentConditionalAccessTarget;
			if (boundExpression.Type.IsNullableType())
			{
				boundExpression = this.MakeOptimizedGetValueOrDefault(node.Syntax, boundExpression);
			}
			return boundExpression;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000BFD8C File Offset: 0x000BDF8C
		public override BoundNode VisitConditionalOperator(BoundConditionalOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Condition);
			BoundExpression boundExpression2 = this.VisitExpression(node.Consequence);
			BoundExpression boundExpression3 = this.VisitExpression(node.Alternative);
			if (boundExpression.ConstantValue == null)
			{
				return node.Update(node.IsRef, boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, node.NaturalTypeOpt, node.WasTargetTyped, node.Type);
			}
			return LocalRewriter.RewriteConditionalOperator(node.Syntax, boundExpression, boundExpression2, boundExpression3, node.ConstantValueOpt, node.Type, node.IsRef);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000BFE18 File Offset: 0x000BE018
		private static BoundExpression RewriteConditionalOperator(SyntaxNode syntax, BoundExpression rewrittenCondition, BoundExpression rewrittenConsequence, BoundExpression rewrittenAlternative, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol rewrittenType, bool isRef)
		{
			ConstantValue constantValue = rewrittenCondition.ConstantValue;
			if (constantValue == ConstantValue.True)
			{
				return rewrittenConsequence;
			}
			if (constantValue == ConstantValue.False)
			{
				return rewrittenAlternative;
			}
			return new BoundConditionalOperator(syntax, isRef, rewrittenCondition, rewrittenConsequence, rewrittenAlternative, constantValueOpt, rewrittenType, false, rewrittenType, false);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000BFE60 File Offset: 0x000BE060
		public override BoundNode VisitContinueStatement(BoundContinueStatement node)
		{
			BoundStatement boundStatement = new BoundGotoStatement(node.Syntax, node.Label, node.HasErrors);
			if (this.Instrument && !node.WasCompilerGenerated)
			{
				boundStatement = this._instrumenter.InstrumentContinueStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000BFEA4 File Offset: 0x000BE0A4
		public override BoundNode VisitConversion(BoundConversion node)
		{
			if (node.ConversionKind == ConversionKind.InterpolatedString)
			{
				return this.RewriteInterpolatedStringConversion(node);
			}
			ConversionKind conversionKind = node.ConversionKind;
			if (conversionKind == ConversionKind.SwitchExpression || conversionKind == ConversionKind.ConditionalExpression)
			{
				return this.Visit(node.Operand);
			}
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			bool inExpressionLambda = this._inExpressionLambda;
			this._inExpressionLambda = this._inExpressionLambda || (node.ConversionKind == ConversionKind.AnonymousFunction && !inExpressionLambda && typeSymbol.IsExpressionTree());
			BoundExpression boundExpression = this.VisitExpression(node.Operand);
			this._inExpressionLambda = inExpressionLambda;
			BoundNode boundNode = this.MakeConversionNode(node, node.Syntax, boundExpression, node.Conversion, node.Checked, node.ExplicitCastInCode, node.ConstantValue, typeSymbol);
			TypeSymbol type = node.Type;
			return boundNode;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000BFF60 File Offset: 0x000BE160
		private static bool IsFloatingPointExpressionOfUnknownPrecision(BoundExpression rewrittenNode)
		{
			if (rewrittenNode == null)
			{
				return false;
			}
			if (rewrittenNode.ConstantValue != null)
			{
				return false;
			}
			TypeSymbol type = rewrittenNode.Type;
			if (type.SpecialType != SpecialType.System_Double && type.SpecialType != SpecialType.System_Single)
			{
				return false;
			}
			BoundKind kind = rewrittenNode.Kind;
			if (kind != BoundKind.Conversion)
			{
				return kind != BoundKind.Sequence || LocalRewriter.IsFloatingPointExpressionOfUnknownPrecision(((BoundSequence)rewrittenNode).Value);
			}
			BoundConversion boundConversion = (BoundConversion)rewrittenNode;
			return boundConversion.ConversionKind == ConversionKind.Identity && !boundConversion.ExplicitCastInCode;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000BFFE4 File Offset: 0x000BE1E4
		private BoundExpression MakeConversionNode([global::System.Runtime.CompilerServices.Nullable(2)] BoundConversion oldNodeOpt, SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol rewrittenType)
		{
			BoundExpression boundExpression = this.MakeConversionNodeCore(oldNodeOpt, syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, constantValueOpt, rewrittenType);
			if (!this._inExpressionLambda && explicitCastInCode && LocalRewriter.IsFloatingPointExpressionOfUnknownPrecision(boundExpression))
			{
				boundExpression = new BoundConversion(syntax, boundExpression, Conversion.Identity, false, false, true, null, null, boundExpression.Type, false);
			}
			return boundExpression;
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000C0038 File Offset: 0x000BE238
		private BoundExpression MakeConversionNodeCore([global::System.Runtime.CompilerServices.Nullable(2)] BoundConversion oldNodeOpt, SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol rewrittenType)
		{
			if (this._inExpressionLambda)
			{
				@checked = @checked && LocalRewriter.NeedsCheckedConversionInExpressionTree(rewrittenOperand.Type, rewrittenType, explicitCastInCode);
			}
			switch (conversion.Kind)
			{
			case ConversionKind.Identity:
				if (this._inExpressionLambda || !rewrittenOperand.Type.Equals(rewrittenType, TypeCompareKind.ConsiderEverything))
				{
					goto IL_0430;
				}
				if (!explicitCastInCode)
				{
					return rewrittenOperand;
				}
				if (!LocalRewriter.IsFloatingPointExpressionOfUnknownPrecision(rewrittenOperand))
				{
					return rewrittenOperand;
				}
				goto IL_0430;
			case ConversionKind.ImplicitNumeric:
			case ConversionKind.ExplicitNumeric:
				break;
			case ConversionKind.ImplicitEnumeration:
				if (rewrittenType.IsNullableType())
				{
					BoundExpression boundExpression = this.MakeConversionNode(oldNodeOpt, syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, constantValueOpt, rewrittenType.GetNullableUnderlyingType());
					Conversion conversion2 = new Conversion(ConversionKind.ImplicitNullable, Conversion.IdentityUnderlying);
					return this.MakeConversionNode(oldNodeOpt, syntax, boundExpression, conversion2, @checked, explicitCastInCode, constantValueOpt, rewrittenType);
				}
				goto IL_0278;
			case ConversionKind.ImplicitThrow:
			{
				BoundThrowExpression boundThrowExpression = (BoundThrowExpression)rewrittenOperand;
				return this._factory.ThrowExpression(boundThrowExpression.Expression, rewrittenType);
			}
			case ConversionKind.ImplicitTupleLiteral:
			case ConversionKind.ExplicitTupleLiteral:
				return rewrittenOperand;
			case ConversionKind.ImplicitTuple:
			case ConversionKind.ExplicitTuple:
				return this.RewriteTupleConversion(syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, (NamedTypeSymbol)rewrittenType);
			case ConversionKind.ImplicitNullable:
			case ConversionKind.ExplicitNullable:
				return this.RewriteNullableConversion(syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, rewrittenType);
			case ConversionKind.NullLiteral:
			case ConversionKind.DefaultLiteral:
				if (!this._inExpressionLambda || !explicitCastInCode)
				{
					return new BoundDefaultExpression(syntax, rewrittenType, false);
				}
				goto IL_0430;
			case ConversionKind.ImplicitReference:
			case ConversionKind.ExplicitReference:
				if (rewrittenOperand.IsDefaultValue() && (!this._inExpressionLambda || !explicitCastInCode))
				{
					return new BoundDefaultExpression(syntax, rewrittenType, false);
				}
				goto IL_0430;
			case ConversionKind.Boxing:
			{
				if (this._inExpressionLambda)
				{
					goto IL_0430;
				}
				if (LocalRewriter.NullableNeverHasValue(rewrittenOperand))
				{
					return new BoundDefaultExpression(syntax, rewrittenType, false);
				}
				BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(rewrittenOperand);
				if (boundExpression2 != null)
				{
					return this.MakeConversionNode(oldNodeOpt, syntax, boundExpression2, conversion, @checked, explicitCastInCode, constantValueOpt, rewrittenType);
				}
				goto IL_0430;
			}
			case ConversionKind.ImplicitPointerToVoid:
			case ConversionKind.ImplicitNullToPointer:
			case ConversionKind.ImplicitPointer:
			case ConversionKind.AnonymousFunction:
			case ConversionKind.Unboxing:
			case ConversionKind.ExplicitPointerToPointer:
			case ConversionKind.ExplicitIntegerToPointer:
			case ConversionKind.ExplicitPointerToInteger:
			case ConversionKind.InterpolatedString:
			case ConversionKind.SwitchExpression:
			case ConversionKind.ConditionalExpression:
			case ConversionKind.Deconstruction:
			case ConversionKind.StackAllocToPointerType:
			case ConversionKind.StackAllocToSpanType:
			case ConversionKind.PinnedObjectToPointer:
				goto IL_0430;
			case ConversionKind.ImplicitDynamic:
			case ConversionKind.ExplicitDynamic:
				return this._dynamicFactory.MakeDynamicConversion(rewrittenOperand, explicitCastInCode || conversion.Kind == ConversionKind.ExplicitDynamic, conversion.IsArrayIndex, @checked, rewrittenType).ToExpression();
			case ConversionKind.ImplicitConstant:
				conversion = Conversion.ExplicitNumeric;
				@checked = false;
				break;
			case ConversionKind.ImplicitUserDefined:
			case ConversionKind.ExplicitUserDefined:
				return this.RewriteUserDefinedConversion(syntax, rewrittenOperand, conversion, rewrittenType);
			case ConversionKind.MethodGroup:
			{
				if (oldNodeOpt != null)
				{
					TypeSymbol type = oldNodeOpt.Type;
					if (type != null && type.TypeKind == TypeKind.FunctionPointer)
					{
						BoundMethodGroup boundMethodGroup = (BoundMethodGroup)rewrittenOperand;
						return new BoundFunctionPointerLoad(oldNodeOpt.Syntax, oldNodeOpt.SymbolOpt, type, false);
					}
				}
				BoundMethodGroup boundMethodGroup2 = (BoundMethodGroup)rewrittenOperand;
				MethodSymbol symbolOpt = oldNodeOpt.SymbolOpt;
				SyntaxNode syntax2 = this._factory.Syntax;
				this._factory.Syntax = (boundMethodGroup2.ReceiverOpt ?? boundMethodGroup2).Syntax;
				BoundExpression boundExpression3 = ((!symbolOpt.RequiresInstanceReceiver && !oldNodeOpt.IsExtensionMethod) ? this._factory.Type(symbolOpt.ContainingType) : boundMethodGroup2.ReceiverOpt);
				this._factory.Syntax = syntax2;
				return new BoundDelegateCreationExpression(syntax, boundExpression3, symbolOpt, oldNodeOpt.IsExtensionMethod, rewrittenType, false);
			}
			case ConversionKind.ExplicitEnumeration:
				goto IL_0278;
			case ConversionKind.IntPtr:
				return this.RewriteIntPtrConversion(oldNodeOpt, syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, constantValueOpt, rewrittenType);
			default:
				goto IL_0430;
			}
			if (rewrittenOperand.IsDefaultValue() && (!this._inExpressionLambda || !explicitCastInCode))
			{
				return new BoundDefaultExpression(syntax, rewrittenType, false);
			}
			if (rewrittenType.SpecialType == SpecialType.System_Decimal || rewrittenOperand.Type.SpecialType == SpecialType.System_Decimal)
			{
				return this.RewriteDecimalConversion(syntax, rewrittenOperand, rewrittenOperand.Type, rewrittenType, @checked, conversion.Kind.IsImplicitConversion(), constantValueOpt);
			}
			goto IL_0430;
			IL_0278:
			if (!rewrittenType.IsNullableType() && rewrittenOperand.IsDefaultValue() && (!this._inExpressionLambda || !explicitCastInCode))
			{
				return new BoundDefaultExpression(syntax, rewrittenType, false);
			}
			if (rewrittenType.SpecialType == SpecialType.System_Decimal)
			{
				NamedTypeSymbol enumUnderlyingType = rewrittenOperand.Type.GetEnumUnderlyingType();
				rewrittenOperand = this.MakeConversionNode(rewrittenOperand, enumUnderlyingType, false, false);
				return this.RewriteDecimalConversion(syntax, rewrittenOperand, enumUnderlyingType, rewrittenType, @checked, false, constantValueOpt);
			}
			ConversionGroup conversionGroup;
			if (rewrittenOperand.Type.SpecialType == SpecialType.System_Decimal)
			{
				NamedTypeSymbol enumUnderlyingType2 = rewrittenType.GetEnumUnderlyingType();
				BoundExpression boundExpression4 = this.RewriteDecimalConversion(syntax, rewrittenOperand, rewrittenOperand.Type, enumUnderlyingType2, @checked, false, constantValueOpt);
				BoundExpression boundExpression5 = boundExpression4;
				Conversion conversion3 = conversion;
				bool flag = false;
				bool flag2 = false;
				conversionGroup = ((oldNodeOpt != null) ? oldNodeOpt.ConversionGroupOpt : null);
				return new BoundConversion(syntax, boundExpression5, conversion3, flag, flag2, explicitCastInCode, constantValueOpt, conversionGroup, rewrittenType, false);
			}
			IL_0430:
			if (oldNodeOpt == null)
			{
				return new BoundConversion(syntax, rewrittenOperand, conversion, false, @checked, explicitCastInCode, constantValueOpt, null, rewrittenType, false);
			}
			BoundExpression boundExpression6 = rewrittenOperand;
			Conversion conversion4 = conversion;
			bool isBaseConversion = oldNodeOpt.IsBaseConversion;
			bool flag3 = @checked;
			conversionGroup = oldNodeOpt.ConversionGroupOpt;
			return oldNodeOpt.Update(boundExpression6, conversion4, isBaseConversion, flag3, explicitCastInCode, constantValueOpt, conversionGroup, rewrittenType);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000C04B0 File Offset: 0x000BE6B0
		private static bool NeedsCheckedConversionInExpressionTree([global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol source, TypeSymbol target, bool explicitCastInCode)
		{
			if (source == null)
			{
				return false;
			}
			SpecialType specialType = LocalRewriter.<NeedsCheckedConversionInExpressionTree>g__GetUnderlyingSpecialType|176_0(source);
			SpecialType specialType2 = LocalRewriter.<NeedsCheckedConversionInExpressionTree>g__GetUnderlyingSpecialType|176_0(target);
			return (explicitCastInCode || specialType != specialType2) && LocalRewriter.<NeedsCheckedConversionInExpressionTree>g__IsInRange|176_1(specialType, SpecialType.System_Char, SpecialType.System_Double) && LocalRewriter.<NeedsCheckedConversionInExpressionTree>g__IsInRange|176_1(specialType2, SpecialType.System_Char, SpecialType.System_UInt64);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000C04F0 File Offset: 0x000BE6F0
		private BoundExpression MakeConversionNode(BoundExpression rewrittenOperand, TypeSymbol rewrittenType, bool @checked, bool acceptFailingConversion = false)
		{
			Conversion conversion = LocalRewriter.MakeConversion(rewrittenOperand, rewrittenType, this._compilation, this._diagnostics, acceptFailingConversion);
			if (!conversion.IsValid)
			{
				return this._factory.NullOrDefault(rewrittenType);
			}
			return this.MakeConversionNode(rewrittenOperand.Syntax, rewrittenOperand, conversion, rewrittenType, @checked, false, null);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000C053C File Offset: 0x000BE73C
		private static Conversion MakeConversion(BoundExpression rewrittenOperand, TypeSymbol rewrittenType, CSharpCompilation compilation, BindingDiagnosticBag diagnostics, bool acceptFailingConversion)
		{
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, compilation.Assembly);
			Conversion conversion = compilation.Conversions.ClassifyConversionFromType(rewrittenOperand.Type, rewrittenType, ref compoundUseSiteInfo, false);
			diagnostics.Add(rewrittenOperand.Syntax, compoundUseSiteInfo);
			if (!conversion.IsValid && (!acceptFailingConversion || (rewrittenOperand.Type.SpecialType != SpecialType.System_Decimal && rewrittenOperand.Type.SpecialType != SpecialType.System_DateTime)))
			{
				diagnostics.Add(ErrorCode.ERR_NoImplicitConv, rewrittenOperand.Syntax.Location, new object[] { rewrittenOperand.Type, rewrittenType });
			}
			return conversion;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000C05D0 File Offset: 0x000BE7D0
		private static BoundExpression MakeConversionForIOperation(BoundExpression operand, TypeSymbol type, SyntaxNode syntax, CSharpCompilation compilation, BindingDiagnosticBag diagnostics, bool @checked, bool acceptFailingConversion = false)
		{
			Conversion conversion = LocalRewriter.MakeConversion(operand, type, compilation, diagnostics, acceptFailingConversion);
			if (conversion.IsIdentity)
			{
				return operand;
			}
			return new BoundConversion(syntax, operand, conversion, @checked, false, null, null, type, !conversion.IsValid)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000C0614 File Offset: 0x000BE814
		private BoundExpression MakeImplicitConversion(BoundExpression rewrittenOperand, TypeSymbol rewrittenType)
		{
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = this.GetNewCompoundUseSiteInfo();
			Conversion conversion = this._compilation.Conversions.ClassifyConversionFromType(rewrittenOperand.Type, rewrittenType, ref newCompoundUseSiteInfo, false);
			this._diagnostics.Add(rewrittenOperand.Syntax, newCompoundUseSiteInfo);
			if (!conversion.IsImplicit)
			{
				this._diagnostics.Add(ErrorCode.ERR_NoImplicitConv, rewrittenOperand.Syntax.Location, new object[] { rewrittenOperand.Type, rewrittenType });
				return this._factory.NullOrDefault(rewrittenType);
			}
			return this.MakeConversionNode(rewrittenOperand.Syntax, rewrittenOperand, conversion, rewrittenType, false, false, null);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000C06AC File Offset: 0x000BE8AC
		private BoundExpression MakeConversionNode(SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, TypeSymbol rewrittenType, bool @checked, bool explicitCastInCode = false, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt = null)
		{
			if (conversion.Kind.IsUserDefinedConversion())
			{
				if (!TypeSymbol.Equals(rewrittenOperand.Type, conversion.BestUserDefinedConversionAnalysis.FromType, TypeCompareKind.ConsiderEverything))
				{
					rewrittenOperand = this.MakeConversionNode(syntax, rewrittenOperand, conversion.UserDefinedFromConversion, conversion.BestUserDefinedConversionAnalysis.FromType, @checked, false, null);
				}
				if (!TypeSymbol.Equals(rewrittenOperand.Type, conversion.Method.GetParameterType(0), TypeCompareKind.ConsiderEverything))
				{
					rewrittenOperand = this.MakeConversionNode(rewrittenOperand, conversion.BestUserDefinedConversionAnalysis.FromType, @checked, false);
				}
				TypeSymbol typeSymbol = conversion.Method.ReturnType;
				if (rewrittenOperand.Type.IsNullableType() && conversion.Method.GetParameterType(0).Equals(rewrittenOperand.Type.GetNullableUnderlyingType(), TypeCompareKind.AllIgnoreOptions) && !typeSymbol.IsNullableType() && typeSymbol.IsValueType)
				{
					typeSymbol = ((NamedTypeSymbol)rewrittenOperand.Type.OriginalDefinition).Construct(new TypeSymbol[] { typeSymbol });
				}
				BoundExpression boundExpression = this.RewriteUserDefinedConversion(syntax, rewrittenOperand, conversion, typeSymbol);
				if (!TypeSymbol.Equals(boundExpression.Type, conversion.BestUserDefinedConversionAnalysis.ToType, TypeCompareKind.ConsiderEverything))
				{
					boundExpression = this.MakeConversionNode(boundExpression, conversion.BestUserDefinedConversionAnalysis.ToType, @checked, false);
				}
				if (!TypeSymbol.Equals(boundExpression.Type, rewrittenType, TypeCompareKind.ConsiderEverything))
				{
					boundExpression = this.MakeConversionNode(syntax, boundExpression, conversion.UserDefinedToConversion, rewrittenType, @checked, false, null);
				}
				return boundExpression;
			}
			return this.MakeConversionNode(null, syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, constantValueOpt, rewrittenType);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000C081C File Offset: 0x000BEA1C
		private BoundExpression RewriteTupleConversion(SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, bool @checked, bool explicitCastInCode, NamedTypeSymbol rewrittenType)
		{
			ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = rewrittenType.TupleElementTypesWithAnnotations;
			int length = tupleElementTypesWithAnnotations.Length;
			ImmutableArray<FieldSymbol> tupleElements = ((NamedTypeSymbol)rewrittenOperand.Type).TupleElements;
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(length);
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(rewrittenOperand, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			ImmutableArray<Conversion> underlyingConversions = conversion.UnderlyingConversions;
			for (int i = 0; i < length; i++)
			{
				BoundExpression boundExpression = this.MakeTupleFieldAccessAndReportUseSiteDiagnostics(boundLocal, syntax, tupleElements[i]);
				BoundExpression boundExpression2 = this.MakeConversionNode(syntax, boundExpression, underlyingConversions[i], tupleElementTypesWithAnnotations[i].Type, @checked, explicitCastInCode, null);
				instance.Add(boundExpression2);
			}
			BoundExpression boundExpression3 = this.MakeTupleCreationExpression(syntax, rewrittenType, instance.ToImmutableAndFree());
			return this._factory.MakeSequence(boundLocal.LocalSymbol, new BoundExpression[] { boundAssignmentOperator, boundExpression3 });
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000C08F8 File Offset: 0x000BEAF8
		private static bool NullableNeverHasValue(BoundExpression expression)
		{
			return expression.NullableNeverHasValue();
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000C0900 File Offset: 0x000BEB00
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static BoundExpression NullableAlwaysHasValue(BoundExpression expression)
		{
			if (!expression.Type.IsNullableType())
			{
				return null;
			}
			BoundObjectCreationExpression boundObjectCreationExpression = expression as BoundObjectCreationExpression;
			if (boundObjectCreationExpression != null)
			{
				ImmutableArray<BoundExpression> arguments = boundObjectCreationExpression.Arguments;
				if (arguments.Length == 1)
				{
					ImmutableArray<BoundExpression> immutableArray = arguments;
					return immutableArray[0];
				}
			}
			else
			{
				BoundConversion boundConversion = expression as BoundConversion;
				if (boundConversion != null)
				{
					Conversion conversion = boundConversion.Conversion;
					if (conversion.Kind == ConversionKind.ImplicitNullable)
					{
						BoundExpression operand = boundConversion.Operand;
						BoundExpression boundExpression = operand;
						if (boundExpression.Type.Equals(expression.Type.StrippedType(), TypeCompareKind.AllIgnoreOptions))
						{
							return boundExpression;
						}
						ImmutableArray<Conversion> underlyingConversions = conversion.UnderlyingConversions;
						ImmutableArray<Conversion> immutableArray2 = underlyingConversions;
						BoundExpression boundExpression2 = operand;
						BoundConversion boundConversion2 = boundConversion;
						if (immutableArray2.Length == 1 && immutableArray2[0].Kind == ConversionKind.ImplicitTuple && !boundExpression2.Type.IsNullableType())
						{
							return new BoundConversion(expression.Syntax, boundExpression2, immutableArray2[0], boundConversion2.Checked, boundConversion2.ExplicitCastInCode, null, null, boundConversion2.Type.StrippedType(), boundConversion2.HasErrors);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000C0A18 File Offset: 0x000BEC18
		private BoundExpression RewriteNullableConversion(SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, bool @checked, bool explicitCastInCode, TypeSymbol rewrittenType)
		{
			if (this._inExpressionLambda)
			{
				return this.RewriteLiftedConversionInExpressionTree(syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, rewrittenType);
			}
			TypeSymbol type = rewrittenOperand.Type;
			if (type.IsNullableType() && rewrittenType.IsNullableType())
			{
				return this.RewriteFullyLiftedBuiltInConversion(syntax, rewrittenOperand, conversion, @checked, rewrittenType);
			}
			if (rewrittenType.IsNullableType())
			{
				BoundExpression boundExpression = this.MakeConversionNode(syntax, rewrittenOperand, conversion.UnderlyingConversions[0], rewrittenType.GetNullableUnderlyingType(), @checked, false, null);
				MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, rewrittenType, SpecialMember.System_Nullable_T__ctor);
				return new BoundObjectCreationExpression(syntax, methodSymbol, new BoundExpression[] { boundExpression });
			}
			BoundExpression boundExpression2 = LocalRewriter.NullableAlwaysHasValue(rewrittenOperand);
			if (boundExpression2 == null)
			{
				MethodSymbol methodSymbol2 = this.UnsafeGetNullableMethod(syntax, type, SpecialMember.System_Nullable_T_get_Value);
				boundExpression2 = BoundCall.Synthesized(syntax, rewrittenOperand, methodSymbol2);
			}
			return this.MakeConversionNode(syntax, boundExpression2, conversion.UnderlyingConversions[0], rewrittenType, @checked, false, null);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000C0AF0 File Offset: 0x000BECF0
		private BoundExpression RewriteLiftedConversionInExpressionTree(SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, bool @checked, bool explicitCastInCode, TypeSymbol rewrittenType)
		{
			TypeSymbol typeSymbol = rewrittenOperand.Type;
			ConversionGroup conversionGroup = null;
			TypeSymbol typeSymbol2 = typeSymbol.StrippedType();
			TypeSymbol typeSymbol3 = rewrittenType.StrippedType();
			if (!TypeSymbol.Equals(typeSymbol2, typeSymbol3, TypeCompareKind.ConsiderEverything) && (typeSymbol2.SpecialType == SpecialType.System_Decimal || typeSymbol3.SpecialType == SpecialType.System_Decimal))
			{
				TypeSymbol typeSymbol4 = typeSymbol2;
				TypeSymbol typeSymbol5 = typeSymbol3;
				if (typeSymbol2.IsEnumType())
				{
					typeSymbol4 = typeSymbol2.GetEnumUnderlyingType();
					typeSymbol = (typeSymbol.IsNullableType() ? ((NamedTypeSymbol)typeSymbol.OriginalDefinition).Construct(new TypeSymbol[] { typeSymbol4 }) : typeSymbol4);
					rewrittenOperand = BoundConversion.SynthesizedNonUserDefined(syntax, rewrittenOperand, Conversion.ImplicitEnumeration, typeSymbol, null);
				}
				else if (typeSymbol3.IsEnumType())
				{
					typeSymbol5 = typeSymbol3.GetEnumUnderlyingType();
				}
				MethodSymbol methodSymbol = (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(LocalRewriter.DecimalConversionMethod(typeSymbol4, typeSymbol5));
				ConversionKind conversionKind = (conversion.Kind.IsImplicitConversion() ? ConversionKind.ImplicitUserDefined : ConversionKind.ExplicitUserDefined);
				return new BoundConversion(syntax, rewrittenOperand, new Conversion(conversionKind, methodSymbol, false), @checked, explicitCastInCode, conversionGroup, null, rewrittenType, false);
			}
			return new BoundConversion(syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, conversionGroup, null, rewrittenType, false);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000C0BFC File Offset: 0x000BEDFC
		private BoundExpression RewriteFullyLiftedBuiltInConversion(SyntaxNode syntax, BoundExpression operand, Conversion conversion, bool @checked, TypeSymbol type)
		{
			BoundExpression boundExpression = this.OptimizeLiftedBuiltInConversion(syntax, operand, conversion, @checked, type);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(operand, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			MethodSymbol methodSymbol;
			if (!this.TryGetNullableMethod(syntax, boundLocal.Type, SpecialMember.System_Nullable_T_GetValueOrDefault, out methodSymbol))
			{
				return LocalRewriter.BadExpression(syntax, type, operand);
			}
			BoundExpression boundExpression2 = this.MakeNullableHasValue(syntax, boundLocal);
			BoundExpression boundExpression3 = new BoundObjectCreationExpression(syntax, this.UnsafeGetNullableMethod(syntax, type, SpecialMember.System_Nullable_T__ctor), new BoundExpression[] { this.MakeConversionNode(syntax, BoundCall.Synthesized(syntax, boundLocal, methodSymbol), conversion.UnderlyingConversions[0], type.GetNullableUnderlyingType(), @checked, false, null) });
			BoundExpression boundExpression4 = new BoundDefaultExpression(syntax, type, false);
			BoundExpression boundExpression5 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression2, boundExpression3, boundExpression4, null, type, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression5, type, false);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000C0CD8 File Offset: 0x000BEED8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression OptimizeLiftedUserDefinedConversion(SyntaxNode syntax, BoundExpression operand, Conversion conversion, TypeSymbol type)
		{
			if (LocalRewriter.NullableNeverHasValue(operand))
			{
				return new BoundDefaultExpression(syntax, type, false);
			}
			BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(operand);
			if (boundExpression != null)
			{
				return this.MakeLiftedUserDefinedConversionConsequence(BoundCall.Synthesized(syntax, null, conversion.Method, boundExpression), type);
			}
			return this.DistributeLiftedConversionIntoLiftedOperand(syntax, operand, conversion, false, type);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000C0D28 File Offset: 0x000BEF28
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression OptimizeLiftedBuiltInConversion(SyntaxNode syntax, BoundExpression operand, Conversion conversion, bool @checked, TypeSymbol type)
		{
			if (LocalRewriter.NullableNeverHasValue(operand))
			{
				return new BoundDefaultExpression(syntax, type, false);
			}
			BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(operand);
			if (boundExpression != null)
			{
				return new BoundObjectCreationExpression(syntax, this.UnsafeGetNullableMethod(syntax, type, SpecialMember.System_Nullable_T__ctor), new BoundExpression[] { this.MakeConversionNode(syntax, boundExpression, conversion.UnderlyingConversions[0], type.GetNullableUnderlyingType(), @checked, false, null) });
			}
			return this.DistributeLiftedConversionIntoLiftedOperand(syntax, operand, conversion, @checked, type);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000C0D9C File Offset: 0x000BEF9C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression DistributeLiftedConversionIntoLiftedOperand(SyntaxNode syntax, BoundExpression operand, Conversion conversion, bool @checked, TypeSymbol type)
		{
			if (operand.Kind == BoundKind.Sequence)
			{
				BoundSequence boundSequence = (BoundSequence)operand;
				if (boundSequence.Value.Kind == BoundKind.ConditionalOperator)
				{
					BoundConditionalOperator boundConditionalOperator = (BoundConditionalOperator)boundSequence.Value;
					if (LocalRewriter.NullableAlwaysHasValue(boundConditionalOperator.Consequence) != null && LocalRewriter.NullableNeverHasValue(boundConditionalOperator.Alternative))
					{
						return new BoundSequence(boundSequence.Syntax, boundSequence.Locals, boundSequence.SideEffects, LocalRewriter.RewriteConditionalOperator(boundConditionalOperator.Syntax, boundConditionalOperator.Condition, this.MakeConversionNode(null, syntax, boundConditionalOperator.Consequence, conversion, @checked, false, null, type), this.MakeConversionNode(null, syntax, boundConditionalOperator.Alternative, conversion, @checked, false, null, type), null, type, false), type, false);
					}
				}
			}
			return null;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000C0E54 File Offset: 0x000BF054
		private BoundExpression RewriteUserDefinedConversion(SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, TypeSymbol rewrittenType)
		{
			if (rewrittenOperand.Type.IsNullableType())
			{
				TypeSymbol parameterType = conversion.Method.GetParameterType(0);
				if (parameterType.Equals(rewrittenOperand.Type.GetNullableUnderlyingType(), TypeCompareKind.AllIgnoreOptions) && !parameterType.IsNullableType() && parameterType.IsValueType)
				{
					return this.RewriteLiftedUserDefinedConversion(syntax, rewrittenOperand, conversion, rewrittenType);
				}
			}
			if (this._inExpressionLambda)
			{
				return BoundConversion.Synthesized(syntax, rewrittenOperand, conversion, false, true, null, null, rewrittenType, false);
			}
			if (rewrittenOperand.Type.IsArray() && this._compilation.IsReadOnlySpanType(rewrittenType))
			{
				return new BoundReadOnlySpanFromArray(syntax, rewrittenOperand, conversion.Method, rewrittenType, false)
				{
					WasCompilerGenerated = true
				};
			}
			return BoundCall.Synthesized(syntax, null, conversion.Method, rewrittenOperand);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000C0F0C File Offset: 0x000BF10C
		private BoundExpression MakeLiftedUserDefinedConversionConsequence(BoundCall call, TypeSymbol resultType)
		{
			if (call.Method.ReturnType.IsNonNullableValueType())
			{
				MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(call.Syntax, resultType, SpecialMember.System_Nullable_T__ctor);
				return new BoundObjectCreationExpression(call.Syntax, methodSymbol, new BoundExpression[] { call });
			}
			return call;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000C0F54 File Offset: 0x000BF154
		private BoundExpression RewriteLiftedUserDefinedConversion(SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, TypeSymbol rewrittenType)
		{
			if (this._inExpressionLambda)
			{
				Conversion conversion2 = this.TryMakeConversion(syntax, conversion, rewrittenOperand.Type, rewrittenType);
				return BoundConversion.Synthesized(syntax, rewrittenOperand, conversion2, false, true, null, null, rewrittenType, false);
			}
			BoundExpression boundExpression = this.OptimizeLiftedUserDefinedConversion(syntax, rewrittenOperand, conversion, rewrittenType);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(rewrittenOperand, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, boundLocal.Type, SpecialMember.System_Nullable_T_GetValueOrDefault);
			BoundExpression boundExpression2 = this.MakeNullableHasValue(syntax, boundLocal);
			BoundCall boundCall = BoundCall.Synthesized(syntax, boundLocal, methodSymbol);
			BoundCall boundCall2 = BoundCall.Synthesized(syntax, null, conversion.Method, boundCall);
			BoundExpression boundExpression3 = this.MakeLiftedUserDefinedConversionConsequence(boundCall2, rewrittenType);
			BoundExpression boundExpression4 = new BoundDefaultExpression(syntax, rewrittenType, false);
			BoundExpression boundExpression5 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression2, boundExpression3, boundExpression4, null, rewrittenType, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression5, rewrittenType, false);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000C102C File Offset: 0x000BF22C
		private BoundExpression RewriteIntPtrConversion([global::System.Runtime.CompilerServices.Nullable(2)] BoundConversion oldNode, SyntaxNode syntax, BoundExpression rewrittenOperand, Conversion conversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol rewrittenType)
		{
			TypeSymbol type = rewrittenOperand.Type;
			SpecialMember intPtrConversionMethod = LocalRewriter.GetIntPtrConversionMethod(type, rewrittenType);
			MethodSymbol methodSymbol;
			if (!this.TryGetSpecialTypeMethod(syntax, intPtrConversionMethod, out methodSymbol))
			{
				return LocalRewriter.BadExpression(syntax, rewrittenType, rewrittenOperand);
			}
			conversion = conversion.SetConversionMethod(methodSymbol);
			if (type.IsNullableType() && rewrittenType.IsNullableType())
			{
				return this.RewriteLiftedUserDefinedConversion(syntax, rewrittenOperand, conversion, rewrittenType);
			}
			if (type.IsNullableType())
			{
				rewrittenOperand = this.MakeConversionNode(rewrittenOperand, type.StrippedType(), @checked, false);
			}
			rewrittenOperand = this.MakeConversionNode(rewrittenOperand, methodSymbol.GetParameterType(0), @checked, false);
			TypeSymbol returnType = methodSymbol.ReturnType;
			if (this._inExpressionLambda)
			{
				return BoundConversion.Synthesized(syntax, rewrittenOperand, conversion, @checked, explicitCastInCode, null, constantValueOpt, rewrittenType, false);
			}
			BoundExpression boundExpression = this.MakeCall(syntax, null, methodSymbol, ImmutableArray.Create<BoundExpression>(rewrittenOperand), returnType);
			return this.MakeConversionNode(boundExpression, rewrittenType, @checked, false);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000C10FC File Offset: 0x000BF2FC
		public static SpecialMember GetIntPtrConversionMethod(TypeSymbol source, TypeSymbol target)
		{
			TypeSymbol typeSymbol = target.StrippedType();
			TypeSymbol typeSymbol2 = source.StrippedType();
			SpecialType specialType = (typeSymbol.IsEnumType() ? typeSymbol.GetEnumUnderlyingType().SpecialType : typeSymbol.SpecialType);
			SpecialType specialType2 = (typeSymbol2.IsEnumType() ? typeSymbol2.GetEnumUnderlyingType().SpecialType : typeSymbol2.SpecialType);
			if (specialType == SpecialType.System_IntPtr)
			{
				if (source.IsPointerOrFunctionPointer())
				{
					return SpecialMember.System_IntPtr__op_Explicit_FromPointer;
				}
				if (specialType2 - SpecialType.System_Char <= 5)
				{
					return SpecialMember.System_IntPtr__op_Explicit_FromInt32;
				}
				if (specialType2 - SpecialType.System_UInt32 <= 5)
				{
					return SpecialMember.System_IntPtr__op_Explicit_FromInt64;
				}
			}
			else if (specialType == SpecialType.System_UIntPtr)
			{
				if (source.IsPointerOrFunctionPointer())
				{
					return SpecialMember.System_UIntPtr__op_Explicit_FromPointer;
				}
				switch (specialType2)
				{
				case SpecialType.System_Char:
				case SpecialType.System_Byte:
				case SpecialType.System_UInt16:
				case SpecialType.System_UInt32:
					return SpecialMember.System_UIntPtr__op_Explicit_FromUInt32;
				case SpecialType.System_SByte:
				case SpecialType.System_Int16:
				case SpecialType.System_Int32:
				case SpecialType.System_Int64:
				case SpecialType.System_UInt64:
				case SpecialType.System_Decimal:
				case SpecialType.System_Single:
				case SpecialType.System_Double:
					return SpecialMember.System_UIntPtr__op_Explicit_FromUInt64;
				}
			}
			else if (specialType2 == SpecialType.System_IntPtr)
			{
				if (target.IsPointerOrFunctionPointer())
				{
					return SpecialMember.System_IntPtr__op_Explicit_ToPointer;
				}
				if (specialType - SpecialType.System_Char <= 6)
				{
					return SpecialMember.System_IntPtr__op_Explicit_ToInt32;
				}
				if (specialType - SpecialType.System_Int64 <= 4)
				{
					return SpecialMember.System_IntPtr__op_Explicit_ToInt64;
				}
			}
			else if (specialType2 == SpecialType.System_UIntPtr)
			{
				if (target.IsPointerOrFunctionPointer())
				{
					return SpecialMember.System_UIntPtr__op_Explicit_ToPointer;
				}
				if (specialType - SpecialType.System_Char <= 6)
				{
					return SpecialMember.System_UIntPtr__op_Explicit_ToUInt32;
				}
				if (specialType - SpecialType.System_Int64 <= 4)
				{
					return SpecialMember.System_UIntPtr__op_Explicit_ToUInt64;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000C1218 File Offset: 0x000BF418
		private static SpecialMember DecimalConversionMethod(TypeSymbol typeFrom, TypeSymbol typeTo)
		{
			if (typeFrom.SpecialType == SpecialType.System_Decimal)
			{
				switch (typeTo.SpecialType)
				{
				case SpecialType.System_Char:
					return SpecialMember.System_Decimal__op_Explicit_ToChar;
				case SpecialType.System_SByte:
					return SpecialMember.System_Decimal__op_Explicit_ToSByte;
				case SpecialType.System_Byte:
					return SpecialMember.System_Decimal__op_Explicit_ToByte;
				case SpecialType.System_Int16:
					return SpecialMember.System_Decimal__op_Explicit_ToInt16;
				case SpecialType.System_UInt16:
					return SpecialMember.System_Decimal__op_Explicit_ToUInt16;
				case SpecialType.System_Int32:
					return SpecialMember.System_Decimal__op_Explicit_ToInt32;
				case SpecialType.System_UInt32:
					return SpecialMember.System_Decimal__op_Explicit_ToUInt32;
				case SpecialType.System_Int64:
					return SpecialMember.System_Decimal__op_Explicit_ToInt64;
				case SpecialType.System_UInt64:
					return SpecialMember.System_Decimal__op_Explicit_ToUInt64;
				case SpecialType.System_Single:
					return SpecialMember.System_Decimal__op_Explicit_ToSingle;
				case SpecialType.System_Double:
					return SpecialMember.System_Decimal__op_Explicit_ToDouble;
				}
				throw ExceptionUtilities.UnexpectedValue(typeTo.SpecialType);
			}
			switch (typeFrom.SpecialType)
			{
			case SpecialType.System_Char:
				return SpecialMember.System_Decimal__op_Implicit_FromChar;
			case SpecialType.System_SByte:
				return SpecialMember.System_Decimal__op_Implicit_FromSByte;
			case SpecialType.System_Byte:
				return SpecialMember.System_Decimal__op_Implicit_FromByte;
			case SpecialType.System_Int16:
				return SpecialMember.System_Decimal__op_Implicit_FromInt16;
			case SpecialType.System_UInt16:
				return SpecialMember.System_Decimal__op_Implicit_FromUInt16;
			case SpecialType.System_Int32:
				return SpecialMember.System_Decimal__op_Implicit_FromInt32;
			case SpecialType.System_UInt32:
				return SpecialMember.System_Decimal__op_Implicit_FromUInt32;
			case SpecialType.System_Int64:
				return SpecialMember.System_Decimal__op_Implicit_FromInt64;
			case SpecialType.System_UInt64:
				return SpecialMember.System_Decimal__op_Implicit_FromUInt64;
			case SpecialType.System_Single:
				return SpecialMember.System_Decimal__op_Explicit_FromSingle;
			case SpecialType.System_Double:
				return SpecialMember.System_Decimal__op_Explicit_FromDouble;
			}
			throw ExceptionUtilities.UnexpectedValue(typeFrom.SpecialType);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000C1314 File Offset: 0x000BF514
		private BoundExpression RewriteDecimalConversion(SyntaxNode syntax, BoundExpression operand, TypeSymbol fromType, TypeSymbol toType, bool @checked, bool isImplicit, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt)
		{
			if (fromType.SpecialType == SpecialType.System_Decimal)
			{
				SpecialType specialType = toType.SpecialType;
				if (specialType - SpecialType.System_IntPtr <= 1)
				{
					operand = this.RewriteDecimalConversionCore(syntax, operand, fromType, LocalRewriter.<RewriteDecimalConversion>g__get64BitType|197_0(this._compilation, toType.SpecialType == SpecialType.System_IntPtr), isImplicit, constantValueOpt);
					return this.MakeConversionNode(operand, toType, @checked, false);
				}
			}
			else
			{
				SpecialType specialType = fromType.SpecialType;
				if (specialType - SpecialType.System_IntPtr <= 1)
				{
					operand = this.MakeConversionNode(operand, LocalRewriter.<RewriteDecimalConversion>g__get64BitType|197_0(this._compilation, fromType.SpecialType == SpecialType.System_IntPtr), @checked, false);
					return this.RewriteDecimalConversionCore(syntax, operand, operand.Type, toType, isImplicit, constantValueOpt);
				}
			}
			return this.RewriteDecimalConversionCore(syntax, operand, fromType, toType, isImplicit, constantValueOpt);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000C13C0 File Offset: 0x000BF5C0
		private BoundExpression RewriteDecimalConversionCore(SyntaxNode syntax, BoundExpression operand, TypeSymbol fromType, TypeSymbol toType, bool isImplicit, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt)
		{
			SpecialMember specialMember = LocalRewriter.DecimalConversionMethod(fromType, toType);
			MethodSymbol methodSymbol = (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(specialMember);
			if (this._inExpressionLambda)
			{
				ConversionKind conversionKind = (isImplicit ? ConversionKind.ImplicitUserDefined : ConversionKind.ExplicitUserDefined);
				Conversion conversion = new Conversion(conversionKind, methodSymbol, false);
				return new BoundConversion(syntax, operand, conversion, false, false, null, constantValueOpt, toType, false);
			}
			return BoundCall.Synthesized(syntax, null, methodSymbol, operand);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000C1424 File Offset: 0x000BF624
		private Conversion TryMakeConversion(SyntaxNode syntax, Conversion conversion, TypeSymbol fromType, TypeSymbol toType)
		{
			ConversionKind kind = conversion.Kind;
			if (kind <= ConversionKind.ImplicitEnumeration)
			{
				if (kind != ConversionKind.ImplicitNumeric)
				{
					if (kind != ConversionKind.ImplicitEnumeration)
					{
						return conversion;
					}
					goto IL_016C;
				}
			}
			else
			{
				switch (kind)
				{
				case ConversionKind.ImplicitUserDefined:
					break;
				case ConversionKind.AnonymousFunction:
				case ConversionKind.MethodGroup:
					return conversion;
				case ConversionKind.ExplicitNumeric:
					goto IL_0124;
				case ConversionKind.ExplicitEnumeration:
					goto IL_016C;
				default:
					if (kind != ConversionKind.ExplicitUserDefined)
					{
						if (kind != ConversionKind.IntPtr)
						{
							return conversion;
						}
						SpecialMember intPtrConversionMethod = LocalRewriter.GetIntPtrConversionMethod(fromType, toType);
						MethodSymbol methodSymbol;
						if (!this.TryGetSpecialTypeMethod(syntax, intPtrConversionMethod, out methodSymbol))
						{
							return Conversion.NoConversion;
						}
						return this.TryMakeUserDefinedConversion(syntax, methodSymbol, fromType, toType, conversion.IsImplicit);
					}
					break;
				}
				MethodSymbol method = conversion.Method;
				Conversion conversion2 = this.TryMakeConversion(syntax, conversion.UserDefinedFromConversion, fromType, method.Parameters[0].Type);
				if (!conversion2.Exists)
				{
					return Conversion.NoConversion;
				}
				Conversion conversion3 = this.TryMakeConversion(syntax, conversion.UserDefinedToConversion, method.ReturnType, toType);
				if (!conversion3.Exists)
				{
					return Conversion.NoConversion;
				}
				if (conversion2 == conversion.UserDefinedFromConversion && conversion3 == conversion.UserDefinedToConversion)
				{
					return conversion;
				}
				return new Conversion(UserDefinedConversionResult.Valid(ImmutableArray.Create<UserDefinedConversionAnalysis>(UserDefinedConversionAnalysis.Normal(method, conversion2, conversion3, fromType, toType)), 0), conversion.IsImplicit);
			}
			IL_0124:
			if (fromType.SpecialType != SpecialType.System_Decimal && toType.SpecialType != SpecialType.System_Decimal)
			{
				return conversion;
			}
			SpecialMember specialMember = LocalRewriter.DecimalConversionMethod(fromType, toType);
			MethodSymbol methodSymbol2;
			if (!this.TryGetSpecialTypeMethod(syntax, specialMember, out methodSymbol2))
			{
				return Conversion.NoConversion;
			}
			return this.TryMakeUserDefinedConversion(syntax, methodSymbol2, fromType, toType, conversion.IsImplicit);
			IL_016C:
			if (fromType.SpecialType == SpecialType.System_Decimal)
			{
				NamedTypeSymbol enumUnderlyingType = toType.GetEnumUnderlyingType();
				SpecialMember specialMember2 = LocalRewriter.DecimalConversionMethod(fromType, enumUnderlyingType);
				MethodSymbol methodSymbol3;
				if (!this.TryGetSpecialTypeMethod(syntax, specialMember2, out methodSymbol3))
				{
					return Conversion.NoConversion;
				}
				return this.TryMakeUserDefinedConversion(syntax, methodSymbol3, fromType, toType, conversion.IsImplicit);
			}
			else
			{
				if (toType.SpecialType != SpecialType.System_Decimal)
				{
					return conversion;
				}
				SpecialMember specialMember3 = LocalRewriter.DecimalConversionMethod(fromType.GetEnumUnderlyingType(), toType);
				MethodSymbol methodSymbol4;
				if (!this.TryGetSpecialTypeMethod(syntax, specialMember3, out methodSymbol4))
				{
					return Conversion.NoConversion;
				}
				return this.TryMakeUserDefinedConversion(syntax, methodSymbol4, fromType, toType, conversion.IsImplicit);
			}
			return conversion;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000C1628 File Offset: 0x000BF828
		private Conversion TryMakeConversion(SyntaxNode syntax, TypeSymbol fromType, TypeSymbol toType)
		{
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = this.GetNewCompoundUseSiteInfo();
			Conversion conversion = this.TryMakeConversion(syntax, this._compilation.Conversions.ClassifyConversionFromType(fromType, toType, ref newCompoundUseSiteInfo, false), fromType, toType);
			this._diagnostics.Add(syntax, newCompoundUseSiteInfo);
			return conversion;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000C1668 File Offset: 0x000BF868
		private Conversion TryMakeUserDefinedConversion(SyntaxNode syntax, MethodSymbol meth, TypeSymbol fromType, TypeSymbol toType, bool isImplicit = true)
		{
			Conversion conversion = this.TryMakeConversion(syntax, fromType, meth.Parameters[0].Type);
			if (!conversion.Exists)
			{
				return Conversion.NoConversion;
			}
			Conversion conversion2 = this.TryMakeConversion(syntax, meth.ReturnType, toType);
			if (!conversion2.Exists)
			{
				return Conversion.NoConversion;
			}
			return new Conversion(UserDefinedConversionResult.Valid(ImmutableArray.Create<UserDefinedConversionAnalysis>(UserDefinedConversionAnalysis.Normal(meth, conversion, conversion2, fromType, toType)), 0), isImplicit);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000C16E0 File Offset: 0x000BF8E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
		{
			BoundConversion right = node.Right;
			return this.RewriteDeconstruction(node.Left, right.Conversion, right.Operand, node.IsUsed);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000C1714 File Offset: 0x000BF914
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression RewriteDeconstruction(BoundTupleExpression left, Conversion conversion, BoundExpression right, bool isUsed)
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<Binder.DeconstructionVariable> assignmentTargetsAndSideEffects = this.GetAssignmentTargetsAndSideEffects(left, instance, instance2);
			BoundExpression boundExpression = this.RewriteDeconstruction(assignmentTargetsAndSideEffects, conversion, left.Type, right, isUsed);
			Binder.DeconstructionVariable.FreeDeconstructionVariables(assignmentTargetsAndSideEffects);
			if (boundExpression == null)
			{
				instance.Free();
				instance2.Free();
				return null;
			}
			return this._factory.Sequence(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree(), boundExpression);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000C1778 File Offset: 0x000BF978
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression RewriteDeconstruction(ArrayBuilder<Binder.DeconstructionVariable> lhsTargets, Conversion conversion, TypeSymbol leftType, BoundExpression right, bool isUsed)
		{
			if (right.Kind == BoundKind.ConditionalOperator)
			{
				BoundConditionalOperator boundConditionalOperator = (BoundConditionalOperator)right;
				return boundConditionalOperator.Update(boundConditionalOperator.IsRef, this.VisitExpression(boundConditionalOperator.Condition), this.RewriteDeconstruction(lhsTargets, conversion, leftType, boundConditionalOperator.Consequence, true), this.RewriteDeconstruction(lhsTargets, conversion, leftType, boundConditionalOperator.Alternative, true), boundConditionalOperator.ConstantValue, leftType, true, leftType);
			}
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			LocalRewriter.DeconstructionSideEffects instance2 = LocalRewriter.DeconstructionSideEffects.GetInstance();
			BoundExpression boundExpression = this.ApplyDeconstructionConversion(lhsTargets, right, conversion, instance, instance2, isUsed, true);
			instance2.Consolidate();
			if (isUsed)
			{
				if (!boundExpression.HasErrors)
				{
					boundExpression = this.VisitExpression(boundExpression);
				}
				return this._factory.Sequence(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree(), boundExpression);
			}
			BoundExpression boundExpression2 = instance2.PopLast();
			if (boundExpression2 == null)
			{
				instance.Free();
				instance2.Free();
				return null;
			}
			return this._factory.Sequence(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree(), boundExpression2);
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000C185C File Offset: 0x000BFA5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression ApplyDeconstructionConversion(ArrayBuilder<Binder.DeconstructionVariable> leftTargets, BoundExpression right, Conversion conversion, ArrayBuilder<LocalSymbol> temps, LocalRewriter.DeconstructionSideEffects effects, bool isUsed, bool inInit)
		{
			ImmutableArray<BoundExpression> rightParts = this.GetRightParts(right, conversion, temps, effects, ref inInit);
			ImmutableArray<Conversion> underlyingConversions = conversion.UnderlyingConversions;
			ArrayBuilder<BoundExpression> arrayBuilder = (isUsed ? ArrayBuilder<BoundExpression>.GetInstance(leftTargets.Count) : null);
			for (int i = 0; i < leftTargets.Count; i++)
			{
				ArrayBuilder<Binder.DeconstructionVariable> nestedVariables = leftTargets[i].NestedVariables;
				BoundExpression boundExpression;
				if (nestedVariables != null)
				{
					boundExpression = this.ApplyDeconstructionConversion(nestedVariables, rightParts[i], underlyingConversions[i], temps, effects, isUsed, inInit);
				}
				else
				{
					BoundExpression boundExpression2 = rightParts[i];
					if (inInit)
					{
						boundExpression2 = this.EvaluateSideEffectingArgumentToTemp(boundExpression2, effects.init, temps);
					}
					BoundExpression single = leftTargets[i].Single;
					boundExpression = this.EvaluateConversionToTemp(boundExpression2, underlyingConversions[i], single.Type, temps, effects.conversions);
					if (single.Kind != BoundKind.DiscardExpression)
					{
						effects.assignments.Add(this.MakeAssignmentOperator(boundExpression.Syntax, single, boundExpression, single.Type, true, false, false));
					}
				}
				if (arrayBuilder != null)
				{
					arrayBuilder.Add(boundExpression);
				}
			}
			if (isUsed)
			{
				NamedTypeSymbol namedTypeSymbol = NamedTypeSymbol.CreateTuple(null, arrayBuilder.SelectAsArray<BoundExpression, TypeWithAnnotations>((BoundExpression e) => TypeWithAnnotations.Create(e.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), default(ImmutableArray<Location>), default(ImmutableArray<string>), this._compilation, false, false, default(ImmutableArray<bool>), (CSharpSyntaxNode)right.Syntax, this._diagnostics);
				return new BoundConvertedTupleLiteral(right.Syntax, null, false, arrayBuilder.ToImmutableAndFree(), default(ImmutableArray<string>), default(ImmutableArray<bool>), namedTypeSymbol, false);
			}
			return null;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000C1A0C File Offset: 0x000BFC0C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> GetRightParts(BoundExpression right, Conversion conversion, ArrayBuilder<LocalSymbol> temps, LocalRewriter.DeconstructionSideEffects effects, ref bool inInit)
		{
			DeconstructMethodInfo deconstructionInfo = conversion.DeconstructionInfo;
			if (!deconstructionInfo.IsDefault)
			{
				BoundExpression boundExpression = this.EvaluateSideEffectingArgumentToTemp(right, inInit ? effects.init : effects.deconstructions, temps);
				inInit = false;
				return this.InvokeDeconstructMethod(deconstructionInfo, boundExpression, effects.deconstructions, temps);
			}
			if (LocalRewriter.IsTupleExpression(right.Kind))
			{
				return ((BoundTupleExpression)right).Arguments;
			}
			if (right.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)right;
				if ((boundConversion.Conversion.Kind == ConversionKind.ImplicitTupleLiteral || boundConversion.Conversion.Kind == ConversionKind.Identity) && LocalRewriter.IsTupleExpression(boundConversion.Operand.Kind))
				{
					return ((BoundTupleExpression)boundConversion.Operand).Arguments;
				}
			}
			if (right.Type.IsTupleType)
			{
				inInit = false;
				return this.AccessTupleFields(this.VisitExpression(right), temps, effects.deconstructions);
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000C1AF8 File Offset: 0x000BFCF8
		private static bool IsTupleExpression(BoundKind kind)
		{
			return kind == BoundKind.TupleLiteral || kind == BoundKind.ConvertedTupleLiteral;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000C1B0C File Offset: 0x000BFD0C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> AccessTupleFields(BoundExpression expression, ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> effects)
		{
			TypeSymbol type = expression.Type;
			int length = type.TupleElementTypesWithAnnotations.Length;
			BoundExpression boundExpression;
			if (LocalRewriter.CanChangeValueBetweenReads(expression, true, false))
			{
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(expression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				effects.Add(boundAssignmentOperator);
				temps.Add(boundLocal.LocalSymbol);
				boundExpression = boundLocal;
			}
			else
			{
				boundExpression = expression;
			}
			ImmutableArray<FieldSymbol> tupleElements = type.TupleElements;
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(length);
			for (int i = 0; i < length; i++)
			{
				BoundExpression boundExpression2 = this.MakeTupleFieldAccessAndReportUseSiteDiagnostics(boundExpression, expression.Syntax, tupleElements[i]);
				instance.Add(boundExpression2);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000C1BB0 File Offset: 0x000BFDB0
		private BoundExpression EvaluateConversionToTemp(BoundExpression expression, Conversion conversion, TypeSymbol destinationType, ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> effects)
		{
			if (conversion.IsIdentity)
			{
				return expression;
			}
			BoundExpression boundExpression = this.MakeConversionNode(expression.Syntax, expression, conversion, destinationType, false, false, null);
			return this.EvaluateSideEffectingArgumentToTemp(boundExpression, effects, temps);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000C1BE8 File Offset: 0x000BFDE8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> InvokeDeconstructMethod(DeconstructMethodInfo deconstruction, BoundExpression target, ArrayBuilder<BoundExpression> effects, ArrayBuilder<LocalSymbol> temps)
		{
			this.AddPlaceholderReplacement(deconstruction.InputPlaceholder, target);
			ImmutableArray<BoundDeconstructValuePlaceholder> outputPlaceholders = deconstruction.OutputPlaceholders;
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(outputPlaceholders.Length);
			foreach (BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder in outputPlaceholders)
			{
				SynthesizedLocal synthesizedLocal = new SynthesizedLocal(this._factory.CurrentFunction, TypeWithAnnotations.Create(boundDeconstructValuePlaceholder.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.LoweringTemp, null, false, RefKind.None);
				BoundLocal boundLocal = new BoundLocal(target.Syntax, synthesizedLocal, null, boundDeconstructValuePlaceholder.Type, false)
				{
					WasCompilerGenerated = true
				};
				temps.Add(synthesizedLocal);
				this.AddPlaceholderReplacement(boundDeconstructValuePlaceholder, boundLocal);
				instance.Add(boundLocal);
			}
			effects.Add(this.VisitExpression(deconstruction.Invocation));
			this.RemovePlaceholderReplacement(deconstruction.InputPlaceholder);
			foreach (BoundDeconstructValuePlaceholder boundDeconstructValuePlaceholder2 in outputPlaceholders)
			{
				this.RemovePlaceholderReplacement(boundDeconstructValuePlaceholder2);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000C1CE0 File Offset: 0x000BFEE0
		private BoundExpression EvaluateSideEffectingArgumentToTemp(BoundExpression arg, ArrayBuilder<BoundExpression> effects, ArrayBuilder<LocalSymbol> temps)
		{
			BoundExpression boundExpression = this.VisitExpression(arg);
			if (LocalRewriter.CanChangeValueBetweenReads(boundExpression, true, true))
			{
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				temps.Add(boundLocal.LocalSymbol);
				effects.Add(boundAssignmentOperator);
				return boundLocal;
			}
			return boundExpression;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000C1D28 File Offset: 0x000BFF28
		private ArrayBuilder<Binder.DeconstructionVariable> GetAssignmentTargetsAndSideEffects(BoundTupleExpression variables, ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> effects)
		{
			ArrayBuilder<Binder.DeconstructionVariable> instance = ArrayBuilder<Binder.DeconstructionVariable>.GetInstance(variables.Arguments.Length);
			foreach (BoundExpression boundExpression in variables.Arguments)
			{
				BoundKind kind = boundExpression.Kind;
				if (kind - BoundKind.TupleLiteral > 1)
				{
					if (kind == BoundKind.DiscardExpression)
					{
						instance.Add(new Binder.DeconstructionVariable(boundExpression, boundExpression.Syntax));
					}
					else
					{
						BoundExpression boundExpression2 = this.TransformCompoundAssignmentLHS(boundExpression, effects, temps, boundExpression.Type.IsDynamic());
						instance.Add(new Binder.DeconstructionVariable(boundExpression2, boundExpression.Syntax));
					}
				}
				else
				{
					BoundTupleExpression boundTupleExpression = (BoundTupleExpression)boundExpression;
					instance.Add(new Binder.DeconstructionVariable(this.GetAssignmentTargetsAndSideEffects(boundTupleExpression, temps, effects), boundTupleExpression.Syntax));
				}
			}
			return instance;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000C1DF4 File Offset: 0x000BFFF4
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			if (node.Argument.HasDynamicType())
			{
				BoundExpression boundExpression = this.VisitExpression(node.Argument);
				BoundExpression boundExpression2 = this._dynamicFactory.MakeDynamicConversion(boundExpression, false, false, false, node.Type).ToExpression();
				return new BoundDelegateCreationExpression(node.Syntax, boundExpression2, null, false, node.Type, false);
			}
			if (node.Argument.Kind == BoundKind.MethodGroup)
			{
				BoundMethodGroup boundMethodGroup = (BoundMethodGroup)node.Argument;
				MethodSymbol methodOpt = node.MethodOpt;
				SyntaxNode syntax = this._factory.Syntax;
				this._factory.Syntax = (boundMethodGroup.ReceiverOpt ?? boundMethodGroup).Syntax;
				BoundExpression boundExpression3 = ((!methodOpt.RequiresInstanceReceiver && !node.IsExtensionMethod) ? this._factory.Type(methodOpt.ContainingType) : this.VisitExpression(boundMethodGroup.ReceiverOpt));
				this._factory.Syntax = syntax;
				return node.Update(boundExpression3, methodOpt, node.IsExtensionMethod, node.Type);
			}
			return base.VisitDelegateCreationExpression(node);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000C1F00 File Offset: 0x000C0100
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Condition);
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("start");
			SyntaxNode syntax = node.Syntax;
			if (!node.WasCompilerGenerated && this.Instrument)
			{
				boundExpression = this._instrumenter.InstrumentDoStatementCondition(node, boundExpression, this._factory);
			}
			BoundStatement boundStatement2 = new BoundConditionalGoto(syntax, boundExpression, true, generatedLabelSymbol, false);
			if (!node.WasCompilerGenerated && this.Instrument)
			{
				boundStatement2 = this._instrumenter.InstrumentDoStatementConditionalGotoStart(node, boundStatement2);
			}
			if (node.Locals.IsEmpty)
			{
				return BoundStatementList.Synthesized(syntax, node.HasErrors, new BoundStatement[]
				{
					new BoundLabelStatement(syntax, generatedLabelSymbol),
					boundStatement,
					new BoundLabelStatement(syntax, node.ContinueLabel),
					boundStatement2,
					new BoundLabelStatement(syntax, node.BreakLabel)
				});
			}
			return BoundStatementList.Synthesized(syntax, node.HasErrors, new BoundStatement[]
			{
				new BoundLabelStatement(syntax, generatedLabelSymbol),
				new BoundBlock(syntax, node.Locals, ImmutableArray.Create<BoundStatement>(boundStatement, new BoundLabelStatement(syntax, node.ContinueLabel), boundStatement2), false),
				new BoundLabelStatement(syntax, node.BreakLabel)
			});
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000C2030 File Offset: 0x000C0230
		public override BoundNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.ReceiverOpt);
			BoundExpression boundExpression2 = this.VisitExpression(node.Argument);
			if (boundExpression != null && node.Event.ContainingAssembly.IsLinked && node.Event.ContainingType.IsInterfaceType())
			{
				NamedTypeSymbol containingType = node.Event.ContainingType;
				foreach (CSharpAttributeData csharpAttributeData in containingType.GetAttributes())
				{
					if (csharpAttributeData.IsTargetAttribute(containingType, AttributeDescription.ComEventInterfaceAttribute) && csharpAttributeData.CommonConstructorArguments.Length == 2)
					{
						return this.RewriteNoPiaEventAssignmentOperator(node, boundExpression, boundExpression2);
					}
				}
			}
			if (node.Event.IsWindowsRuntimeEvent)
			{
				LocalRewriter.EventAssignmentKind eventAssignmentKind = (node.IsAddition ? LocalRewriter.EventAssignmentKind.Addition : LocalRewriter.EventAssignmentKind.Subtraction);
				return this.RewriteWindowsRuntimeEventAssignmentOperator(node.Syntax, node.Event, eventAssignmentKind, node.IsDynamic, boundExpression, boundExpression2);
			}
			ImmutableArray<BoundExpression> immutableArray = ImmutableArray.Create<BoundExpression>(boundExpression2);
			MethodSymbol methodSymbol = (node.IsAddition ? node.Event.AddMethod : node.Event.RemoveMethod);
			return this.MakeCall(node.Syntax, boundExpression, methodSymbol, immutableArray, node.Type);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000C2158 File Offset: 0x000C0358
		private BoundExpression RewriteWindowsRuntimeEventAssignmentOperator(SyntaxNode syntax, EventSymbol eventSymbol, LocalRewriter.EventAssignmentKind kind, bool isDynamic, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiverOpt, BoundExpression rewrittenArgument)
		{
			BoundAssignmentOperator boundAssignmentOperator = null;
			BoundLocal boundLocal = null;
			if (!eventSymbol.IsStatic && LocalRewriter.CanChangeValueBetweenReads(rewrittenReceiverOpt, true, false))
			{
				boundLocal = this._factory.StoreToTemp(rewrittenReceiverOpt, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			}
			NamedTypeSymbol namedTypeSymbol = this._factory.WellKnownType(WellKnownType.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationToken);
			this._factory.WellKnownType(WellKnownType.System_Runtime_InteropServices_WindowsRuntime_WindowsRuntimeMarshal);
			NamedTypeSymbol namedTypeSymbol2 = this._factory.WellKnownType(WellKnownType.System_Action_T).Construct(new TypeSymbol[] { namedTypeSymbol });
			TypeSymbol type = eventSymbol.Type;
			BoundExpression boundExpression = boundLocal ?? rewrittenReceiverOpt ?? this._factory.Type(type);
			BoundDelegateCreationExpression boundDelegateCreationExpression = new BoundDelegateCreationExpression(syntax, boundExpression, eventSymbol.RemoveMethod, false, namedTypeSymbol2, false);
			BoundExpression boundExpression2 = null;
			if (kind == LocalRewriter.EventAssignmentKind.Assignment)
			{
				MethodSymbol methodSymbol;
				if (this.TryGetWellKnownTypeMember<MethodSymbol>(syntax, WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_WindowsRuntimeMarshal__RemoveAllEventHandlers, out methodSymbol, false, null))
				{
					boundExpression2 = this.MakeCall(syntax, null, methodSymbol, ImmutableArray.Create<BoundExpression>(boundDelegateCreationExpression), methodSymbol.ReturnType);
				}
				else
				{
					boundExpression2 = new BoundBadExpression(syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(boundDelegateCreationExpression), ErrorTypeSymbol.UnknownResultType);
				}
			}
			WellKnownMember wellKnownMember;
			ImmutableArray<BoundExpression> immutableArray;
			if (kind == LocalRewriter.EventAssignmentKind.Subtraction)
			{
				wellKnownMember = WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_WindowsRuntimeMarshal__RemoveEventHandler_T;
				immutableArray = ImmutableArray.Create<BoundExpression>(boundDelegateCreationExpression, rewrittenArgument);
			}
			else
			{
				NamedTypeSymbol namedTypeSymbol3 = this._factory.WellKnownType(WellKnownType.System_Func_T2).Construct(new TypeSymbol[] { type, namedTypeSymbol });
				BoundExpression boundExpression3 = new BoundDelegateCreationExpression(syntax, boundExpression, eventSymbol.AddMethod, false, namedTypeSymbol3, false);
				wellKnownMember = WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_WindowsRuntimeMarshal__AddEventHandler_T;
				immutableArray = ImmutableArray.Create<BoundExpression>(boundExpression3, boundDelegateCreationExpression, rewrittenArgument);
			}
			MethodSymbol methodSymbol2;
			BoundExpression boundExpression4;
			if (this.TryGetWellKnownTypeMember<MethodSymbol>(syntax, wellKnownMember, out methodSymbol2, false, null))
			{
				methodSymbol2 = methodSymbol2.Construct(new TypeSymbol[] { type });
				boundExpression4 = this.MakeCall(syntax, null, methodSymbol2, immutableArray, methodSymbol2.ReturnType);
			}
			else
			{
				boundExpression4 = new BoundBadExpression(syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, immutableArray, ErrorTypeSymbol.UnknownResultType);
			}
			if (boundLocal == null && boundExpression2 == null)
			{
				return boundExpression4;
			}
			ImmutableArray<LocalSymbol> immutableArray2 = ((boundLocal == null) ? ImmutableArray<LocalSymbol>.Empty : ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol));
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(2);
			if (boundExpression2 != null)
			{
				instance.Add(boundExpression2);
			}
			if (boundAssignmentOperator != null)
			{
				instance.Add(boundAssignmentOperator);
			}
			return new BoundSequence(syntax, immutableArray2, instance.ToImmutableAndFree(), boundExpression4, boundExpression4.Type, false);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000C235C File Offset: 0x000C055C
		private BoundExpression VisitWindowsRuntimeEventFieldAssignmentOperator(SyntaxNode syntax, BoundEventAccess left, BoundExpression rewrittenRight)
		{
			EventSymbol eventSymbol = left.EventSymbol;
			BoundExpression boundExpression = this.VisitExpression(left.ReceiverOpt);
			return this.RewriteWindowsRuntimeEventAssignmentOperator(syntax, eventSymbol, LocalRewriter.EventAssignmentKind.Assignment, false, boundExpression, rewrittenRight);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000C238C File Offset: 0x000C058C
		public override BoundNode VisitEventAccess(BoundEventAccess node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.ReceiverOpt);
			return this.MakeEventAccess(node.Syntax, boundExpression, node.EventSymbol, node.ConstantValue, node.ResultKind, node.Type);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000C23CC File Offset: 0x000C05CC
		private BoundExpression MakeEventAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, EventSymbol eventSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, LookupResultKind resultKind, TypeSymbol type)
		{
			FieldSymbol associatedField = eventSymbol.AssociatedField;
			if (!eventSymbol.IsWindowsRuntimeEvent)
			{
				return this.MakeFieldAccess(syntax, rewrittenReceiver, associatedField, constantValueOpt, resultKind, type, null);
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)associatedField.Type;
			BoundFieldAccess boundFieldAccess = new BoundFieldAccess(syntax, associatedField.IsStatic ? null : rewrittenReceiver, associatedField, null, false)
			{
				WasCompilerGenerated = true
			};
			MethodSymbol methodSymbol;
			BoundExpression boundExpression;
			if (this.TryGetWellKnownTypeMember<MethodSymbol>(syntax, WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T__GetOrCreateEventRegistrationTokenTable, out methodSymbol, false, null))
			{
				methodSymbol = methodSymbol.AsMember(namedTypeSymbol);
				boundExpression = BoundCall.Synthesized(syntax, null, methodSymbol, boundFieldAccess);
			}
			else
			{
				boundExpression = new BoundBadExpression(syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(boundFieldAccess), ErrorTypeSymbol.UnknownResultType);
			}
			PropertySymbol propertySymbol;
			if (this.TryGetWellKnownTypeMember<PropertySymbol>(syntax, WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T__InvocationList, out propertySymbol, false, null))
			{
				MethodSymbol methodSymbol2 = propertySymbol.GetMethod;
				if (methodSymbol2 != null)
				{
					methodSymbol2 = methodSymbol2.AsMember(namedTypeSymbol);
					return this._factory.Call(boundExpression, methodSymbol2);
				}
				string accessorName = SourcePropertyAccessorSymbol.GetAccessorName(propertySymbol.Name, true, propertySymbol.IsCompilationOutputWinMdObj());
				this._diagnostics.Add(new CSDiagnosticInfo(ErrorCode.ERR_MissingPredefinedMember, new object[] { propertySymbol.ContainingType, accessorName }), syntax.Location);
			}
			return new BoundBadExpression(syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(boundExpression), ErrorTypeSymbol.UnknownResultType);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000C24F8 File Offset: 0x000C06F8
		private BoundExpression RewriteNoPiaEventAssignmentOperator(BoundEventAssignmentOperator node, BoundExpression rewrittenReceiver, BoundExpression rewrittenArgument)
		{
			BoundExpression boundExpression = null;
			SyntaxNode syntax = this._factory.Syntax;
			this._factory.Syntax = node.Syntax;
			MethodSymbol methodSymbol = this._factory.WellKnownMethod(WellKnownMember.System_Runtime_InteropServices_ComAwareEventInfo__ctor);
			if (methodSymbol != null)
			{
				MethodSymbol methodSymbol2 = this._factory.WellKnownMethod(node.IsAddition ? WellKnownMember.System_Runtime_InteropServices_ComAwareEventInfo__AddEventHandler : WellKnownMember.System_Runtime_InteropServices_ComAwareEventInfo__RemoveEventHandler);
				if (methodSymbol2 != null)
				{
					BoundExpression boundExpression2 = this._factory.New(methodSymbol, new BoundExpression[]
					{
						this._factory.Typeof(node.Event.ContainingType),
						this._factory.Literal(node.Event.MetadataName)
					});
					boundExpression = this._factory.Call(boundExpression2, methodSymbol2, this._factory.Convert(methodSymbol2.Parameters[0].Type, rewrittenReceiver), this._factory.Convert(methodSymbol2.Parameters[1].Type, rewrittenArgument));
				}
			}
			this._factory.Syntax = syntax;
			PEModuleBuilder emitModule = this.EmitModule;
			if (emitModule != null)
			{
				emitModule.EmbeddedTypesManagerOpt.EmbedEventIfNeedTo(node.Event.GetCciAdapter(), node.Syntax, this._diagnostics.DiagnosticBag, true);
			}
			if (boundExpression != null)
			{
				return boundExpression;
			}
			return new BoundBadExpression(node.Syntax, LookupResultKind.NotCreatable, ImmutableArray.Create<Symbol>(node.Event), ImmutableArray.Create<BoundExpression>(rewrittenReceiver, rewrittenArgument), ErrorTypeSymbol.UnknownResultType);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000C265A File Offset: 0x000C085A
		public override BoundNode VisitExpressionStatement(BoundExpressionStatement node)
		{
			return this.RewriteExpressionStatement(node, false) ?? BoundStatementList.Synthesized(node.Syntax, Array.Empty<BoundStatement>());
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000C2678 File Offset: 0x000C0878
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundStatement RewriteExpressionStatement(BoundExpressionStatement node, bool suppressInstrumentation = false)
		{
			BoundExpression boundExpression = this.VisitUnusedExpression(node.Expression);
			if (boundExpression == null)
			{
				return null;
			}
			BoundStatement boundStatement = node.Update(boundExpression);
			if (!suppressInstrumentation && this.Instrument && !node.WasCompilerGenerated)
			{
				boundStatement = this._instrumenter.InstrumentExpressionStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000C26C4 File Offset: 0x000C08C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression VisitUnusedExpression(BoundExpression expression)
		{
			if (expression.HasErrors)
			{
				return expression;
			}
			BoundKind kind = expression.Kind;
			if (kind <= BoundKind.AwaitExpression)
			{
				if (kind == BoundKind.CompoundAssignmentOperator)
				{
					return this.VisitCompoundAssignmentOperator((BoundCompoundAssignmentOperator)expression, false);
				}
				if (kind == BoundKind.AssignmentOperator)
				{
					return this.VisitAssignmentOperator((BoundAssignmentOperator)expression, false);
				}
				if (kind == BoundKind.AwaitExpression)
				{
					return this.VisitAwaitExpression((BoundAwaitExpression)expression, false);
				}
			}
			else
			{
				if (kind == BoundKind.DynamicInvocation)
				{
					return this.VisitDynamicInvocation((BoundDynamicInvocation)expression, true);
				}
				if (kind == BoundKind.ConditionalAccess)
				{
					return this.RewriteConditionalAccess((BoundConditionalAccess)expression, false);
				}
				if (kind == BoundKind.Call)
				{
					if (this._allowOmissionOfConditionalCalls)
					{
						BoundCall boundCall = (BoundCall)expression;
						if (boundCall.Method.CallsAreOmitted(boundCall.SyntaxTree))
						{
							return null;
						}
					}
				}
			}
			return this.VisitExpression(expression);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000C2788 File Offset: 0x000C0988
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.ReceiverOpt);
			return this.MakeFieldAccess(node.Syntax, boundExpression, node.FieldSymbol, node.ConstantValue, node.ResultKind, node.Type, node);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000C27C8 File Offset: 0x000C09C8
		private BoundExpression MakeFieldAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, FieldSymbol fieldSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, LookupResultKind resultKind, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] BoundFieldAccess oldNodeOpt = null)
		{
			if (fieldSymbol.ContainingType.IsTupleType)
			{
				return this.MakeTupleFieldAccess(syntax, fieldSymbol, rewrittenReceiver);
			}
			BoundExpression boundExpression = ((oldNodeOpt != null) ? oldNodeOpt.Update(rewrittenReceiver, fieldSymbol, constantValueOpt, resultKind, type) : new BoundFieldAccess(syntax, rewrittenReceiver, fieldSymbol, constantValueOpt, resultKind, type, false));
			if (fieldSymbol.IsFixedSizeBuffer)
			{
				boundExpression = new BoundAddressOfOperator(syntax, boundExpression, type, false);
			}
			return boundExpression;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000C2828 File Offset: 0x000C0A28
		private BoundExpression MakeTupleFieldAccess(SyntaxNode syntax, FieldSymbol tupleField, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver)
		{
			NamedTypeSymbol namedTypeSymbol = tupleField.ContainingType;
			FieldSymbol tupleUnderlyingField = tupleField.TupleUnderlyingField;
			if (tupleUnderlyingField == null)
			{
				return this._factory.BadExpression(tupleField.Type);
			}
			if (rewrittenReceiver != null && rewrittenReceiver.Kind == BoundKind.DefaultExpression)
			{
				return new BoundDefaultExpression(syntax, tupleField.Type, false);
			}
			if (!TypeSymbol.Equals(tupleUnderlyingField.ContainingType, namedTypeSymbol, TypeCompareKind.ConsiderEverything))
			{
				WellKnownMember tupleTypeMember = NamedTypeSymbol.GetTupleTypeMember(8, 8);
				FieldSymbol fieldSymbol = (FieldSymbol)NamedTypeSymbol.GetWellKnownMemberInType(namedTypeSymbol.OriginalDefinition, tupleTypeMember, this._diagnostics, syntax);
				if (fieldSymbol == null)
				{
					return this._factory.BadExpression(tupleField.Type);
				}
				do
				{
					FieldSymbol fieldSymbol2 = fieldSymbol.AsMember(namedTypeSymbol);
					rewrittenReceiver = this._factory.Field(rewrittenReceiver, fieldSymbol2);
					namedTypeSymbol = (NamedTypeSymbol)namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type;
				}
				while (!TypeSymbol.Equals(tupleUnderlyingField.ContainingType, namedTypeSymbol, TypeCompareKind.ConsiderEverything));
			}
			return this._factory.Field(rewrittenReceiver, tupleUnderlyingField);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000C290C File Offset: 0x000C0B0C
		private BoundExpression MakeTupleFieldAccessAndReportUseSiteDiagnostics(BoundExpression tuple, SyntaxNode syntax, FieldSymbol field)
		{
			field = field.CorrespondingTupleField ?? field;
			UseSiteInfo<AssemblySymbol> useSiteInfo = field.GetUseSiteInfo();
			DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
			if (diagnosticInfo == null || diagnosticInfo.Severity != DiagnosticSeverity.Error)
			{
				useSiteInfo = useSiteInfo.AdjustDiagnosticInfo(null);
			}
			this._diagnostics.Add(useSiteInfo, syntax.Location);
			return this.MakeTupleFieldAccess(syntax, field, tuple);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000C296C File Offset: 0x000C0B6C
		public override BoundNode VisitFixedStatement(BoundFixedStatement node)
		{
			ImmutableArray<BoundLocalDeclaration> localDeclarations = node.Declarations.LocalDeclarations;
			int length = localDeclarations.Length;
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance(node.Locals.Length);
			instance.AddRange(node.Locals);
			ArrayBuilder<BoundStatement> instance2 = ArrayBuilder<BoundStatement>.GetInstance(length + 1 + 1);
			BoundStatement[] array = new BoundStatement[length];
			for (int i = 0; i < length; i++)
			{
				BoundLocalDeclaration boundLocalDeclaration = localDeclarations[i];
				LocalSymbol localSymbol;
				instance2.Add(this.InitializeFixedStatementLocal(boundLocalDeclaration, this._factory, out localSymbol));
				instance.Add(localSymbol);
				if (localSymbol.RefKind == RefKind.None)
				{
					array[i] = this._factory.Assignment(this._factory.Local(localSymbol), this._factory.Null(localSymbol.Type), false);
				}
				else
				{
					array[i] = this._factory.Assignment(this._factory.Local(localSymbol), new BoundPointerIndirectionOperator(this._factory.Syntax, this._factory.Default(new PointerTypeSymbol(localSymbol.TypeWithAnnotations)), localSymbol.Type, false), true);
				}
			}
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			instance2.Add(boundStatement);
			instance2.Add(this._factory.HiddenSequencePoint(null));
			if (LocalRewriter.IsInTryBlock(node) || this.HasGotoOut(boundStatement))
			{
				return this._factory.Block(instance.ToImmutableAndFree(), new BoundStatement[]
				{
					new BoundTryStatement(this._factory.Syntax, this._factory.Block(instance2.ToImmutableAndFree()), ImmutableArray<BoundCatchBlock>.Empty, this._factory.Block(array), null)
				});
			}
			instance2.AddRange(array);
			return this._factory.Block(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree());
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000C2B30 File Offset: 0x000C0D30
		private static bool IsInTryBlock(BoundFixedStatement boundFixed)
		{
			SyntaxNode syntaxNode = boundFixed.Syntax.Parent;
			while (syntaxNode != null)
			{
				SyntaxKind syntaxKind = syntaxNode.Kind();
				if (syntaxKind <= SyntaxKind.ForEachStatement)
				{
					if (syntaxKind - SyntaxKind.AnonymousMethodExpression <= 2)
					{
						return false;
					}
					if (syntaxKind == SyntaxKind.ForEachStatement)
					{
						return true;
					}
				}
				else
				{
					if (syntaxKind != SyntaxKind.UsingStatement)
					{
						switch (syntaxKind)
						{
						case SyntaxKind.TryStatement:
							return true;
						case SyntaxKind.CatchClause:
							if (((TryStatementSyntax)syntaxNode.Parent).Finally != null)
							{
								return true;
							}
							break;
						case SyntaxKind.CatchDeclaration:
						case SyntaxKind.CatchFilterClause:
							goto IL_0092;
						case SyntaxKind.FinallyClause:
							break;
						default:
							if (syntaxKind != SyntaxKind.ForEachVariableStatement)
							{
								goto IL_0092;
							}
							return true;
						}
						syntaxNode = syntaxNode.Parent;
						syntaxNode = syntaxNode.Parent;
						continue;
					}
					return true;
				}
				IL_0092:
				if (syntaxNode is MemberDeclarationSyntax)
				{
					return false;
				}
				syntaxNode = syntaxNode.Parent;
				continue;
			}
			return false;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000C2BE8 File Offset: 0x000C0DE8
		private bool HasGotoOut(BoundNode node)
		{
			if (this._lazyUnmatchedLabelCache == null)
			{
				this._lazyUnmatchedLabelCache = new Dictionary<BoundNode, HashSet<LabelSymbol>>();
			}
			HashSet<LabelSymbol> hashSet = UnmatchedGotoFinder.Find(node, this._lazyUnmatchedLabelCache, base.RecursionDepth);
			this._lazyUnmatchedLabelCache.Add(node, hashSet);
			return hashSet != null && hashSet.Count > 0;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000C2C36 File Offset: 0x000C0E36
		public override BoundNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000C2C40 File Offset: 0x000C0E40
		private BoundStatement InitializeFixedStatementLocal(BoundLocalDeclaration localDecl, SyntheticBoundNodeFactory factory, out LocalSymbol pinnedTemp)
		{
			BoundExpression initializerOpt = localDecl.InitializerOpt;
			LocalSymbol localSymbol = localDecl.LocalSymbol;
			BoundFixedLocalCollectionInitializer boundFixedLocalCollectionInitializer = (BoundFixedLocalCollectionInitializer)initializerOpt;
			if (boundFixedLocalCollectionInitializer.GetPinnableOpt != null)
			{
				return this.InitializeFixedStatementGetPinnable(localDecl, localSymbol, boundFixedLocalCollectionInitializer, factory, out pinnedTemp);
			}
			TypeSymbol typeSymbol = boundFixedLocalCollectionInitializer.Expression.Type;
			if (typeSymbol != null && typeSymbol.SpecialType == SpecialType.System_String)
			{
				return this.InitializeFixedStatementStringLocal(localDecl, localSymbol, boundFixedLocalCollectionInitializer, factory, out pinnedTemp);
			}
			typeSymbol = boundFixedLocalCollectionInitializer.Expression.Type;
			if (typeSymbol != null && typeSymbol.TypeKind == TypeKind.Array)
			{
				return this.InitializeFixedStatementArrayLocal(localDecl, localSymbol, boundFixedLocalCollectionInitializer, factory, out pinnedTemp);
			}
			return this.InitializeFixedStatementRegularLocal(localDecl, localSymbol, boundFixedLocalCollectionInitializer, factory, out pinnedTemp);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000C2CC8 File Offset: 0x000C0EC8
		private BoundStatement InitializeFixedStatementRegularLocal(BoundLocalDeclaration localDecl, LocalSymbol localSymbol, BoundFixedLocalCollectionInitializer fixedInitializer, SyntheticBoundNodeFactory factory, out LocalSymbol pinnedTemp)
		{
			TypeSymbol type = localSymbol.Type;
			BoundExpression boundExpression = this.VisitExpression(fixedInitializer.Expression);
			TypeSymbol pointedAtType = ((PointerTypeSymbol)boundExpression.Type).PointedAtType;
			boundExpression = ((BoundAddressOfOperator)boundExpression).Operand;
			VariableDeclaratorSyntax variableDeclaratorSyntax = fixedInitializer.Syntax.FirstAncestorOrSelf<VariableDeclaratorSyntax>(null, true);
			pinnedTemp = factory.SynthesizedLocal(pointedAtType, variableDeclaratorSyntax, true, RefKind.In, SynthesizedLocalKind.FixedReference);
			BoundStatement boundStatement = factory.Assignment(factory.Local(pinnedTemp), boundExpression, true);
			BoundAddressOfOperator boundAddressOfOperator = new BoundAddressOfOperator(factory.Syntax, factory.Local(pinnedTemp), fixedInitializer.ElementPointerType, false);
			BoundExpression boundExpression2 = factory.Convert(type, boundAddressOfOperator, fixedInitializer.ElementPointerTypeConversion, false);
			BoundStatement boundStatement2 = this.InstrumentLocalDeclarationIfNecessary(localDecl, localSymbol, factory.Assignment(factory.Local(localSymbol), boundExpression2, false));
			return factory.Block(new BoundStatement[] { boundStatement, boundStatement2 });
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000C2DA0 File Offset: 0x000C0FA0
		private BoundStatement InitializeFixedStatementGetPinnable(BoundLocalDeclaration localDecl, LocalSymbol localSymbol, BoundFixedLocalCollectionInitializer fixedInitializer, SyntheticBoundNodeFactory factory, out LocalSymbol pinnedTemp)
		{
			TypeSymbol type = localSymbol.Type;
			BoundExpression boundExpression = this.VisitExpression(fixedInitializer.Expression);
			TypeSymbol type2 = boundExpression.Type;
			SyntaxNode syntax = boundExpression.Syntax;
			MethodSymbol getPinnableOpt = fixedInitializer.GetPinnableOpt;
			VariableDeclaratorSyntax variableDeclaratorSyntax = fixedInitializer.Syntax.FirstAncestorOrSelf<VariableDeclaratorSyntax>(null, true);
			pinnedTemp = factory.SynthesizedLocal(getPinnableOpt.ReturnType, variableDeclaratorSyntax, true, RefKind.In, SynthesizedLocalKind.FixedReference);
			int num = 0;
			bool flag = !type2.IsValueType;
			BoundExpression boundExpression2;
			if (flag)
			{
				int num2 = this._currentConditionalAccessID + 1;
				this._currentConditionalAccessID = num2;
				num = num2;
				boundExpression2 = new BoundConditionalReceiver(syntax, num, type2);
			}
			else
			{
				boundExpression2 = boundExpression;
			}
			BoundCall boundCall = (getPinnableOpt.IsStatic ? factory.Call(null, getPinnableOpt, boundExpression2) : factory.Call(boundExpression2, getPinnableOpt));
			BoundAssignmentOperator boundAssignmentOperator = factory.AssignmentExpression(factory.Local(pinnedTemp), boundCall, true);
			BoundAddressOfOperator boundAddressOfOperator = new BoundAddressOfOperator(factory.Syntax, factory.Local(pinnedTemp), fixedInitializer.ElementPointerType, false);
			BoundExpression boundExpression3 = factory.Convert(type, boundAddressOfOperator, fixedInitializer.ElementPointerTypeConversion, false);
			BoundExpression boundExpression4 = factory.Sequence(ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression3);
			if (flag)
			{
				boundExpression4 = new BoundLoweredConditionalAccess(syntax, boundExpression, null, boundExpression4, null, num, type, false);
			}
			return this.InstrumentLocalDeclarationIfNecessary(localDecl, localSymbol, factory.Assignment(factory.Local(localSymbol), boundExpression4, false));
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000C2EE4 File Offset: 0x000C10E4
		private BoundStatement InitializeFixedStatementStringLocal(BoundLocalDeclaration localDecl, LocalSymbol localSymbol, BoundFixedLocalCollectionInitializer fixedInitializer, SyntheticBoundNodeFactory factory, out LocalSymbol pinnedTemp)
		{
			TypeSymbol type = localSymbol.Type;
			BoundExpression boundExpression = this.VisitExpression(fixedInitializer.Expression);
			TypeSymbol type2 = boundExpression.Type;
			VariableDeclaratorSyntax variableDeclaratorSyntax = fixedInitializer.Syntax.FirstAncestorOrSelf<VariableDeclaratorSyntax>(null, true);
			pinnedTemp = factory.SynthesizedLocal(type2, variableDeclaratorSyntax, true, RefKind.None, SynthesizedLocalKind.FixedReference);
			BoundStatement boundStatement = factory.Assignment(factory.Local(pinnedTemp), boundExpression, false);
			BoundExpression boundExpression2 = factory.Convert(fixedInitializer.ElementPointerType, factory.Local(pinnedTemp), Conversion.PinnedObjectToPointer, false);
			BoundExpression boundExpression3 = factory.Convert(type, boundExpression2, fixedInitializer.ElementPointerTypeConversion, false);
			BoundStatement boundStatement2 = this.InstrumentLocalDeclarationIfNecessary(localDecl, localSymbol, factory.Assignment(factory.Local(localSymbol), boundExpression3, false));
			BoundExpression boundExpression4 = this.MakeNullCheck(factory.Syntax, factory.Local(localSymbol), BinaryOperatorKind.NotEqual);
			MethodSymbol methodSymbol;
			BoundExpression boundExpression5;
			if (this.TryGetWellKnownTypeMember<MethodSymbol>(fixedInitializer.Syntax, WellKnownMember.System_Runtime_CompilerServices_RuntimeHelpers__get_OffsetToStringData, out methodSymbol, false, null))
			{
				boundExpression5 = factory.Call(null, methodSymbol);
			}
			else
			{
				boundExpression5 = new BoundBadExpression(fixedInitializer.Syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray<BoundExpression>.Empty, ErrorTypeSymbol.UnknownResultType);
			}
			BoundExpression boundExpression6 = factory.Binary(BinaryOperatorKind.PointerAndIntAddition, type, factory.Local(localSymbol), boundExpression5);
			BoundStatement boundStatement3 = factory.If(boundExpression4, factory.Assignment(factory.Local(localSymbol), boundExpression6, false), null);
			return factory.Block(new BoundStatement[] { boundStatement, boundStatement2, boundStatement3 });
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000C303C File Offset: 0x000C123C
		private BoundStatement InitializeFixedStatementArrayLocal(BoundLocalDeclaration localDecl, LocalSymbol localSymbol, BoundFixedLocalCollectionInitializer fixedInitializer, SyntheticBoundNodeFactory factory, out LocalSymbol pinnedTemp)
		{
			TypeSymbol type = localSymbol.Type;
			BoundExpression boundExpression = this.VisitExpression(fixedInitializer.Expression);
			TypeSymbol type2 = boundExpression.Type;
			pinnedTemp = factory.SynthesizedLocal(type2, null, true, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)pinnedTemp.Type;
			TypeWithAnnotations elementTypeWithAnnotations = arrayTypeSymbol.ElementTypeWithAnnotations;
			BoundExpression boundExpression2 = factory.AssignmentExpression(factory.Local(pinnedTemp), boundExpression, false);
			BoundExpression boundExpression3 = this.MakeNullCheck(factory.Syntax, boundExpression2, BinaryOperatorKind.NotEqual);
			BoundExpression boundExpression4;
			MethodSymbol methodSymbol;
			if (arrayTypeSymbol.IsSZArray)
			{
				boundExpression4 = factory.ArrayLength(factory.Local(pinnedTemp));
			}
			else if (this.TryGetWellKnownTypeMember<MethodSymbol>(fixedInitializer.Syntax, WellKnownMember.System_Array__get_Length, out methodSymbol, false, null))
			{
				boundExpression4 = factory.Call(factory.Local(pinnedTemp), methodSymbol);
			}
			else
			{
				boundExpression4 = new BoundBadExpression(fixedInitializer.Syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(factory.Local(pinnedTemp)), ErrorTypeSymbol.UnknownResultType);
			}
			BoundExpression boundExpression5 = factory.Binary(BinaryOperatorKind.IntNotEqual, factory.SpecialType(SpecialType.System_Boolean), boundExpression4, factory.Literal(0));
			BoundExpression boundExpression6 = factory.Binary(BinaryOperatorKind.LogicalBoolAnd, factory.SpecialType(SpecialType.System_Boolean), boundExpression3, boundExpression5);
			BoundExpression boundExpression7 = factory.ArrayAccessFirstElement(factory.Local(pinnedTemp));
			BoundExpression boundExpression8 = new BoundAddressOfOperator(factory.Syntax, boundExpression7, new PointerTypeSymbol(elementTypeWithAnnotations), false);
			BoundExpression boundExpression9 = factory.Convert(type, boundExpression8, fixedInitializer.ElementPointerTypeConversion, false);
			BoundExpression boundExpression10 = factory.AssignmentExpression(factory.Local(localSymbol), boundExpression9, false);
			BoundExpression boundExpression11 = factory.AssignmentExpression(factory.Local(localSymbol), factory.Null(type), false);
			BoundStatement boundStatement = factory.ExpressionStatement(new BoundConditionalOperator(factory.Syntax, false, boundExpression6, boundExpression10, boundExpression11, null, type, false, type, false));
			return this.InstrumentLocalDeclarationIfNecessary(localDecl, localSymbol, boundStatement);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000C31F4 File Offset: 0x000C13F4
		public override BoundNode VisitForEachStatement(BoundForEachStatement node)
		{
			if (node.HasErrors)
			{
				return node;
			}
			TypeSymbol type = LocalRewriter.GetUnconvertedCollectionExpression(node).Type;
			if (type.Kind == SymbolKind.ArrayType)
			{
				if (((ArrayTypeSymbol)type).IsSZArray)
				{
					return this.RewriteSingleDimensionalArrayForEachStatement(node);
				}
				return this.RewriteMultiDimensionalArrayForEachStatement(node);
			}
			else
			{
				MethodSymbol methodSymbol;
				MethodSymbol methodSymbol2;
				if (node.AwaitOpt == null && this.CanRewriteForEachAsFor(node.Syntax, type, out methodSymbol, out methodSymbol2))
				{
					return this.RewriteForEachStatementAsFor(node, methodSymbol, methodSymbol2);
				}
				return this.RewriteEnumeratorForEachStatement(node);
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000C326C File Offset: 0x000C146C
		private bool CanRewriteForEachAsFor(SyntaxNode forEachSyntax, TypeSymbol nodeExpressionType, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out MethodSymbol indexerGet, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out MethodSymbol lengthGet)
		{
			MethodSymbol methodSymbol;
			indexerGet = (methodSymbol = null);
			lengthGet = methodSymbol;
			TypeSymbol originalDefinition = nodeExpressionType.OriginalDefinition;
			if (originalDefinition.SpecialType == SpecialType.System_String)
			{
				lengthGet = this.UnsafeGetSpecialTypeMethod(forEachSyntax, SpecialMember.System_String__Length);
				indexerGet = this.UnsafeGetSpecialTypeMethod(forEachSyntax, SpecialMember.System_String__Chars);
			}
			else if (originalDefinition == this._compilation.GetWellKnownType(WellKnownType.System_Span_T))
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)nodeExpressionType;
				Symbol symbol = this._factory.WellKnownMember(WellKnownMember.System_Span_T__get_Length, true);
				lengthGet = (MethodSymbol)((symbol != null) ? symbol.SymbolAsMember(namedTypeSymbol) : null);
				Symbol symbol2 = this._factory.WellKnownMember(WellKnownMember.System_Span_T__get_Item, true);
				indexerGet = (MethodSymbol)((symbol2 != null) ? symbol2.SymbolAsMember(namedTypeSymbol) : null);
			}
			else if (originalDefinition == this._compilation.GetWellKnownType(WellKnownType.System_ReadOnlySpan_T))
			{
				NamedTypeSymbol namedTypeSymbol2 = (NamedTypeSymbol)nodeExpressionType;
				Symbol symbol3 = this._factory.WellKnownMember(WellKnownMember.System_ReadOnlySpan_T__get_Length, true);
				lengthGet = (MethodSymbol)((symbol3 != null) ? symbol3.SymbolAsMember(namedTypeSymbol2) : null);
				Symbol symbol4 = this._factory.WellKnownMember(WellKnownMember.System_ReadOnlySpan_T__get_Item, true);
				indexerGet = (MethodSymbol)((symbol4 != null) ? symbol4.SymbolAsMember(namedTypeSymbol2) : null);
			}
			return lengthGet != null && indexerGet != null;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000C3388 File Offset: 0x000C1588
		private BoundStatement RewriteEnumeratorForEachStatement(BoundForEachStatement node)
		{
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)node.Syntax;
			bool flag = node.AwaitOpt != null;
			ForEachEnumeratorInfo enumeratorInfoOpt = node.EnumeratorInfoOpt;
			BoundExpression unconvertedCollectionExpression = LocalRewriter.GetUnconvertedCollectionExpression(node);
			BoundExpression boundExpression = this.VisitExpression(unconvertedCollectionExpression);
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			MethodArgumentInfo methodArgumentInfo = enumeratorInfoOpt.GetEnumeratorInfo;
			TypeSymbol returnType = methodArgumentInfo.Method.ReturnType;
			TypeSymbol elementType = enumeratorInfoOpt.ElementType;
			LocalSymbol localSymbol = this._factory.SynthesizedLocal(returnType, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachEnumerator);
			BoundLocal boundLocal = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, localSymbol, returnType);
			BoundExpression boundExpression2 = this.ConvertReceiverForInvocation(commonForEachStatementSyntax, boundExpression, methodArgumentInfo.Method, enumeratorInfoOpt.CollectionConversion, enumeratorInfoOpt.CollectionType);
			if (methodArgumentInfo.Method.IsExtensionMethod)
			{
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(methodArgumentInfo.Arguments.Length);
				instance.Add(boundExpression2);
				instance.AddRange(methodArgumentInfo.Arguments, 1, methodArgumentInfo.Arguments.Length - 1);
				MethodArgumentInfo methodArgumentInfo2 = methodArgumentInfo.<Clone>$();
				methodArgumentInfo2.Arguments = instance.ToImmutableAndFree();
				methodArgumentInfo = methodArgumentInfo2;
				boundExpression2 = null;
			}
			BoundExpression boundExpression3 = this.SynthesizeCall(methodArgumentInfo, commonForEachStatementSyntax, boundExpression2, flag || methodArgumentInfo.Method.IsExtensionMethod, false);
			BoundStatement boundStatement2 = this.MakeLocalDeclaration(commonForEachStatementSyntax, localSymbol, boundExpression3);
			this.InstrumentForEachStatementCollectionVarDeclaration(node, ref boundStatement2);
			BoundExpression boundExpression4 = this.MakeConversionNode(commonForEachStatementSyntax, this.MakeConversionNode(commonForEachStatementSyntax, BoundCall.Synthesized(commonForEachStatementSyntax, boundLocal, enumeratorInfoOpt.CurrentPropertyGetter), enumeratorInfoOpt.CurrentConversion, elementType, node.Checked, false, null), node.ElementConversion, node.IterationVariableType.Type, node.Checked, false, null);
			ImmutableArray<LocalSymbol> iterationVariables = node.IterationVariables;
			BoundStatement boundStatement3 = this.LocalOrDeconstructionDeclaration(node, iterationVariables, boundExpression4);
			this.InstrumentForEachStatementIterationVarDeclaration(node, ref boundStatement3);
			BoundBlock boundBlock = LocalRewriter.CreateBlockDeclaringIterationVariables(iterationVariables, boundStatement3, boundStatement, commonForEachStatementSyntax);
			BoundExpression boundExpression5 = this.SynthesizeCall(enumeratorInfoOpt.MoveNextInfo, commonForEachStatementSyntax, boundLocal, flag, true);
			if (flag)
			{
				boundExpression5 = this.RewriteAwaitExpression(commonForEachStatementSyntax, boundExpression5, node.AwaitOpt, node.AwaitOpt.GetResult.ReturnType, true);
			}
			BoundStatement boundStatement4 = this.RewriteWhileStatement(node, boundExpression5, boundBlock, node.BreakLabel, node.ContinueLabel, false);
			BoundStatement boundStatement6;
			if (enumeratorInfoOpt.NeedsDisposal)
			{
				BoundStatement boundStatement5 = this.WrapWithTryFinallyDispose(commonForEachStatementSyntax, enumeratorInfoOpt, returnType, boundLocal, boundStatement4);
				boundStatement6 = new BoundBlock(commonForEachStatementSyntax, ImmutableArray.Create<LocalSymbol>(localSymbol), ImmutableArray.Create<BoundStatement>(boundStatement2, boundStatement5), false);
			}
			else
			{
				boundStatement6 = new BoundBlock(commonForEachStatementSyntax, ImmutableArray.Create<LocalSymbol>(localSymbol), ImmutableArray.Create<BoundStatement>(boundStatement2, boundStatement4), false);
			}
			this.InstrumentForEachStatement(node, ref boundStatement6);
			return boundStatement6;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000C35E8 File Offset: 0x000C17E8
		private bool TryGetDisposeMethod(CommonForEachStatementSyntax forEachSyntax, ForEachEnumeratorInfo enumeratorInfo, out MethodSymbol disposeMethod)
		{
			if (enumeratorInfo.IsAsync)
			{
				disposeMethod = (MethodSymbol)Binder.GetWellKnownTypeMember(this._compilation, WellKnownMember.System_IAsyncDisposable__DisposeAsync, this._diagnostics, null, forEachSyntax, false);
				return disposeMethod != null;
			}
			return Binder.TryGetSpecialTypeMember<MethodSymbol>(this._compilation, SpecialMember.System_IDisposable__Dispose, forEachSyntax, this._diagnostics, out disposeMethod);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000C3638 File Offset: 0x000C1838
		private BoundStatement WrapWithTryFinallyDispose(CommonForEachStatementSyntax forEachSyntax, ForEachEnumeratorInfo enumeratorInfo, TypeSymbol enumeratorType, BoundLocal boundEnumeratorVar, BoundStatement rewrittenBody)
		{
			NamedTypeSymbol namedTypeSymbol = null;
			bool flag = false;
			MethodArgumentInfo patternDisposeInfo = enumeratorInfo.PatternDisposeInfo;
			MethodSymbol methodSymbol = ((patternDisposeInfo != null) ? patternDisposeInfo.Method : null);
			if (methodSymbol == null)
			{
				this.TryGetDisposeMethod(forEachSyntax, enumeratorInfo, out methodSymbol);
				if (methodSymbol == null)
				{
					return rewrittenBody;
				}
				namedTypeSymbol = methodSymbol.ContainingType;
				ConversionsBase conversionsBase = new TypeConversions(this._factory.CurrentFunction.ContainingAssembly.CorLibrary, false);
				CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = this.GetNewCompoundUseSiteInfo();
				flag = conversionsBase.ClassifyImplicitConversionFromType(enumeratorType, namedTypeSymbol, ref newCompoundUseSiteInfo).IsImplicit;
				this._diagnostics.Add(forEachSyntax, newCompoundUseSiteInfo);
			}
			Binder.ReportDiagnosticsIfObsolete(this._diagnostics, methodSymbol, forEachSyntax, false, this._factory.CurrentFunction, this._factory.CurrentType, enumeratorInfo.Location);
			BoundBlock boundBlock;
			if (flag || enumeratorInfo.PatternDisposeInfo != null)
			{
				Conversion conversion = (enumeratorType.IsStructType() ? Conversion.Boxing : Conversion.ImplicitReference);
				MethodArgumentInfo methodArgumentInfo = enumeratorInfo.PatternDisposeInfo;
				BoundExpression boundExpression;
				if (methodArgumentInfo == null)
				{
					methodArgumentInfo = MethodArgumentInfo.CreateParameterlessMethod(methodSymbol);
					boundExpression = this.ConvertReceiverForInvocation(forEachSyntax, boundEnumeratorVar, methodSymbol, conversion, namedTypeSymbol);
				}
				else
				{
					boundExpression = boundEnumeratorVar;
				}
				BoundExpression boundExpression2 = this.MakeCallWithNoExplicitArgument(methodArgumentInfo, forEachSyntax, boundExpression, true);
				BoundAwaitableInfo disposeAwaitableInfo = enumeratorInfo.DisposeAwaitableInfo;
				BoundStatement boundStatement;
				if (disposeAwaitableInfo != null)
				{
					boundStatement = this.WrapWithAwait(forEachSyntax, boundExpression2, disposeAwaitableInfo);
					this._sawAwaitInExceptionHandler = true;
				}
				else
				{
					boundStatement = new BoundExpressionStatement(forEachSyntax, boundExpression2, false);
				}
				BoundStatement boundStatement2;
				if (enumeratorType.IsValueType)
				{
					boundStatement2 = boundStatement;
				}
				else
				{
					boundStatement2 = LocalRewriter.RewriteIfStatement(forEachSyntax, new BoundBinaryOperator(forEachSyntax, BinaryOperatorKind.NotEqual, null, null, LookupResultKind.Viable, this.MakeConversionNode(forEachSyntax, boundEnumeratorVar, enumeratorInfo.EnumeratorConversion, this._compilation.GetSpecialType(SpecialType.System_Object), false, false, null), this.MakeLiteral(forEachSyntax, ConstantValue.Null, null, null), this._compilation.GetSpecialType(SpecialType.System_Boolean), false), boundStatement, null, false);
				}
				boundBlock = new BoundBlock(forEachSyntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundStatement>(boundStatement2), false);
			}
			else
			{
				LocalSymbol localSymbol = this._factory.SynthesizedLocal(namedTypeSymbol, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				BoundLocal boundLocal = LocalRewriter.MakeBoundLocal(forEachSyntax, localSymbol, namedTypeSymbol);
				BoundTypeExpression boundTypeExpression = new BoundTypeExpression(forEachSyntax, null, namedTypeSymbol, false);
				BoundExpression boundExpression3 = new BoundAsOperator(forEachSyntax, boundEnumeratorVar, boundTypeExpression, Conversion.ExplicitReference, namedTypeSymbol, false);
				BoundStatement boundStatement3 = this.MakeLocalDeclaration(forEachSyntax, localSymbol, boundExpression3);
				BoundExpression boundExpression4 = BoundCall.Synthesized(forEachSyntax, boundLocal, methodSymbol);
				BoundStatement boundStatement4 = new BoundExpressionStatement(forEachSyntax, boundExpression4, false);
				BoundStatement boundStatement5 = LocalRewriter.RewriteIfStatement(forEachSyntax, new BoundBinaryOperator(forEachSyntax, BinaryOperatorKind.NotEqual, null, null, LookupResultKind.Viable, boundLocal, this.MakeLiteral(forEachSyntax, ConstantValue.Null, null, null), this._compilation.GetSpecialType(SpecialType.System_Boolean), false), boundStatement4, null, false);
				boundBlock = new BoundBlock(forEachSyntax, ImmutableArray.Create<LocalSymbol>(localSymbol), ImmutableArray.Create<BoundStatement>(boundStatement3, boundStatement5), false);
			}
			return new BoundTryStatement(forEachSyntax, new BoundBlock(forEachSyntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundStatement>(rewrittenBody), false), ImmutableArray<BoundCatchBlock>.Empty, boundBlock, null);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000C38C0 File Offset: 0x000C1AC0
		private BoundStatement WrapWithAwait(CommonForEachStatementSyntax forEachSyntax, BoundExpression disposeCall, BoundAwaitableInfo disposeAwaitableInfoOpt)
		{
			MethodSymbol getResult = disposeAwaitableInfoOpt.GetResult;
			TypeSymbol typeSymbol = ((getResult != null) ? getResult.ReturnType : null) ?? this._compilation.DynamicType;
			BoundExpression boundExpression = this.RewriteAwaitExpression(forEachSyntax, disposeCall, disposeAwaitableInfoOpt, typeSymbol, false);
			return new BoundExpressionStatement(forEachSyntax, boundExpression, false);
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000C3903 File Offset: 0x000C1B03
		private BoundExpression ConvertReceiverForInvocation(CSharpSyntaxNode syntax, BoundExpression receiver, MethodSymbol method, Conversion receiverConversion, TypeSymbol convertedReceiverType)
		{
			if (receiver.Type.IsReferenceType || !method.ContainingType.IsInterface)
			{
				receiver = this.MakeConversionNode(syntax, receiver, receiverConversion, convertedReceiverType, false, false, null);
			}
			return receiver;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000C3931 File Offset: 0x000C1B31
		private BoundExpression SynthesizeCall(MethodArgumentInfo methodArgumentInfo, CSharpSyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, bool allowExtensionAndOptionalParameters, bool assertParametersAreOptional = true)
		{
			if (allowExtensionAndOptionalParameters)
			{
				return this.MakeCallWithNoExplicitArgument(methodArgumentInfo, syntax, receiver, assertParametersAreOptional);
			}
			return BoundCall.Synthesized(syntax, receiver, methodArgumentInfo.Method, ImmutableArray<BoundExpression>.Empty);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000C3958 File Offset: 0x000C1B58
		private BoundStatement RewriteForEachStatementAsFor(BoundForEachStatement node, MethodSymbol indexerGet, MethodSymbol lengthGet)
		{
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)node.Syntax;
			BoundExpression unconvertedCollectionExpression = LocalRewriter.GetUnconvertedCollectionExpression(node);
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)unconvertedCollectionExpression.Type;
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Int32);
			TypeSymbol specialType2 = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			BoundExpression boundExpression = this.VisitExpression(unconvertedCollectionExpression);
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			LocalSymbol localSymbol = this._factory.SynthesizedLocal(namedTypeSymbol, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachArray);
			BoundStatement boundStatement2 = this.MakeLocalDeclaration(commonForEachStatementSyntax, localSymbol, boundExpression);
			this.InstrumentForEachStatementCollectionVarDeclaration(node, ref boundStatement2);
			BoundLocal boundLocal = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, localSymbol, namedTypeSymbol);
			LocalSymbol localSymbol2 = this._factory.SynthesizedLocal(specialType, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachArrayIndex);
			BoundLocal boundLocal2 = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, localSymbol2, specialType);
			BoundStatement boundStatement3 = this.MakeLocalDeclaration(commonForEachStatementSyntax, localSymbol2, this.MakeLiteral(commonForEachStatementSyntax, ConstantValue.Default(SpecialType.System_Int32), specialType, null));
			BoundExpression boundExpression2 = this.MakeConversionNode(commonForEachStatementSyntax, BoundCall.Synthesized(commonForEachStatementSyntax, boundLocal, indexerGet, boundLocal2), node.ElementConversion, node.IterationVariableType.Type, node.Checked, false, null);
			ImmutableArray<LocalSymbol> iterationVariables = node.IterationVariables;
			BoundStatement boundStatement4 = this.LocalOrDeconstructionDeclaration(node, iterationVariables, boundExpression2);
			this.InstrumentForEachStatementIterationVarDeclaration(node, ref boundStatement4);
			BoundStatement boundStatement5 = new BoundStatementList(commonForEachStatementSyntax, ImmutableArray.Create<BoundStatement>(boundStatement2, boundStatement3), false);
			BoundExpression boundExpression3 = BoundCall.Synthesized(commonForEachStatementSyntax, boundLocal, lengthGet);
			BoundExpression boundExpression4 = new BoundBinaryOperator(commonForEachStatementSyntax, BinaryOperatorKind.IntLessThan, null, null, LookupResultKind.Viable, boundLocal2, boundExpression3, specialType2, false);
			BoundStatement boundStatement6 = this.MakePositionIncrement(commonForEachStatementSyntax, boundLocal2, specialType);
			BoundStatement boundStatement7 = LocalRewriter.CreateBlockDeclaringIterationVariables(iterationVariables, boundStatement4, boundStatement, commonForEachStatementSyntax);
			BoundStatement boundStatement8 = this.RewriteForStatementWithoutInnerLocals(node, ImmutableArray.Create<LocalSymbol>(localSymbol, localSymbol2), boundStatement5, boundExpression4, boundStatement6, boundStatement7, node.BreakLabel, node.ContinueLabel, node.HasErrors);
			this.InstrumentForEachStatement(node, ref boundStatement8);
			return boundStatement8;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000C3AF4 File Offset: 0x000C1CF4
		private BoundStatement LocalOrDeconstructionDeclaration(BoundForEachStatement forEachBound, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> iterationVariables, BoundExpression iterationVarValue)
		{
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)forEachBound.Syntax;
			BoundForEachDeconstructStep deconstructionOpt = forEachBound.DeconstructionOpt;
			BoundStatement boundStatement;
			if (deconstructionOpt == null)
			{
				boundStatement = this.MakeLocalDeclaration(commonForEachStatementSyntax, iterationVariables[0], iterationVarValue);
			}
			else
			{
				BoundDeconstructionAssignmentOperator deconstructionAssignment = deconstructionOpt.DeconstructionAssignment;
				this.AddPlaceholderReplacement(deconstructionOpt.TargetPlaceholder, iterationVarValue);
				BoundExpression boundExpression = this.VisitExpression(deconstructionAssignment);
				boundStatement = new BoundExpressionStatement(deconstructionAssignment.Syntax, boundExpression, false);
				this.RemovePlaceholderReplacement(deconstructionOpt.TargetPlaceholder);
			}
			return boundStatement;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000C3B63 File Offset: 0x000C1D63
		private static BoundBlock CreateBlockDeclaringIterationVariables([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> iterationVariables, BoundStatement iteratorVariableInitialization, BoundStatement rewrittenBody, CommonForEachStatementSyntax forEachSyntax)
		{
			return new BoundBlock(forEachSyntax, iterationVariables, ImmutableArray.Create<BoundStatement>(iteratorVariableInitialization, rewrittenBody), false);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000C3B74 File Offset: 0x000C1D74
		private static BoundBlock CreateBlockDeclaringIterationVariables([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> iterationVariables, BoundStatement iteratorVariableInitialization, BoundStatement checkAndBreak, BoundStatement rewrittenBody, LabelSymbol continueLabel, CommonForEachStatementSyntax forEachSyntax)
		{
			return new BoundBlock(forEachSyntax, iterationVariables, ImmutableArray.Create<BoundStatement>(iteratorVariableInitialization, checkAndBreak, rewrittenBody, new BoundLabelStatement(forEachSyntax, continueLabel)), false);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000C3B90 File Offset: 0x000C1D90
		private BoundStatement RewriteSingleDimensionalArrayForEachStatement(BoundForEachStatement node)
		{
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)node.Syntax;
			BoundExpression unconvertedCollectionExpression = LocalRewriter.GetUnconvertedCollectionExpression(node);
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)unconvertedCollectionExpression.Type;
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Int32);
			TypeSymbol specialType2 = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			BoundExpression boundExpression = this.VisitExpression(unconvertedCollectionExpression);
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			LocalSymbol localSymbol = this._factory.SynthesizedLocal(arrayTypeSymbol, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachArray);
			BoundStatement boundStatement2 = this.MakeLocalDeclaration(commonForEachStatementSyntax, localSymbol, boundExpression);
			this.InstrumentForEachStatementCollectionVarDeclaration(node, ref boundStatement2);
			BoundLocal boundLocal = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, localSymbol, arrayTypeSymbol);
			LocalSymbol localSymbol2 = this._factory.SynthesizedLocal(specialType, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachArrayIndex);
			BoundLocal boundLocal2 = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, localSymbol2, specialType);
			BoundStatement boundStatement3 = this.MakeLocalDeclaration(commonForEachStatementSyntax, localSymbol2, this.MakeLiteral(commonForEachStatementSyntax, ConstantValue.Default(SpecialType.System_Int32), specialType, null));
			BoundExpression boundExpression2 = this.MakeConversionNode(commonForEachStatementSyntax, new BoundArrayAccess(commonForEachStatementSyntax, boundLocal, ImmutableArray.Create<BoundExpression>(boundLocal2), arrayTypeSymbol.ElementType, false), node.ElementConversion, node.IterationVariableType.Type, node.Checked, false, null);
			ImmutableArray<LocalSymbol> iterationVariables = node.IterationVariables;
			BoundStatement boundStatement4 = this.LocalOrDeconstructionDeclaration(node, iterationVariables, boundExpression2);
			this.InstrumentForEachStatementIterationVarDeclaration(node, ref boundStatement4);
			BoundStatement boundStatement5 = new BoundStatementList(commonForEachStatementSyntax, ImmutableArray.Create<BoundStatement>(boundStatement2, boundStatement3), false);
			BoundExpression boundExpression3 = new BoundArrayLength(commonForEachStatementSyntax, boundLocal, specialType, false);
			BoundExpression boundExpression4 = new BoundBinaryOperator(commonForEachStatementSyntax, BinaryOperatorKind.IntLessThan, null, null, LookupResultKind.Viable, boundLocal2, boundExpression3, specialType2, false);
			BoundStatement boundStatement6 = this.MakePositionIncrement(commonForEachStatementSyntax, boundLocal2, specialType);
			BoundStatement boundStatement7 = LocalRewriter.CreateBlockDeclaringIterationVariables(iterationVariables, boundStatement4, boundStatement, commonForEachStatementSyntax);
			BoundStatement boundStatement8 = this.RewriteForStatementWithoutInnerLocals(node, ImmutableArray.Create<LocalSymbol>(localSymbol, localSymbol2), boundStatement5, boundExpression4, boundStatement6, boundStatement7, node.BreakLabel, node.ContinueLabel, node.HasErrors);
			this.InstrumentForEachStatement(node, ref boundStatement8);
			return boundStatement8;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000C3D38 File Offset: 0x000C1F38
		private BoundStatement RewriteMultiDimensionalArrayForEachStatement(BoundForEachStatement node)
		{
			CommonForEachStatementSyntax commonForEachStatementSyntax = (CommonForEachStatementSyntax)node.Syntax;
			BoundExpression unconvertedCollectionExpression = LocalRewriter.GetUnconvertedCollectionExpression(node);
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)unconvertedCollectionExpression.Type;
			int rank = arrayTypeSymbol.Rank;
			TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Int32);
			TypeSymbol specialType2 = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(commonForEachStatementSyntax, SpecialMember.System_Array__GetLowerBound);
			MethodSymbol methodSymbol2 = this.UnsafeGetSpecialTypeMethod(commonForEachStatementSyntax, SpecialMember.System_Array__GetUpperBound);
			BoundExpression boundExpression = this.VisitExpression(unconvertedCollectionExpression);
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			LocalSymbol localSymbol = this._factory.SynthesizedLocal(arrayTypeSymbol, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachArray);
			BoundLocal boundLocal = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, localSymbol, arrayTypeSymbol);
			BoundStatement boundStatement2 = this.MakeLocalDeclaration(commonForEachStatementSyntax, localSymbol, boundExpression);
			this.InstrumentForEachStatementCollectionVarDeclaration(node, ref boundStatement2);
			LocalSymbol[] array = new LocalSymbol[rank];
			BoundLocal[] array2 = new BoundLocal[rank];
			BoundStatement[] array3 = new BoundStatement[rank];
			for (int i = 0; i < rank; i++)
			{
				array[i] = this._factory.SynthesizedLocal(specialType, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachArrayLimit);
				array2[i] = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, array[i], specialType);
				ImmutableArray<BoundExpression> immutableArray = ImmutableArray.Create<BoundExpression>(this.MakeLiteral(commonForEachStatementSyntax, ConstantValue.Create(i, ConstantValueTypeDiscriminator.Int32), specialType, null));
				BoundExpression boundExpression2 = BoundCall.Synthesized(commonForEachStatementSyntax, boundLocal, methodSymbol2, immutableArray);
				array3[i] = this.MakeLocalDeclaration(commonForEachStatementSyntax, array[i], boundExpression2);
			}
			LocalSymbol[] array4 = new LocalSymbol[rank];
			BoundLocal[] array5 = new BoundLocal[rank];
			for (int j = 0; j < rank; j++)
			{
				array4[j] = this._factory.SynthesizedLocal(specialType, commonForEachStatementSyntax, false, RefKind.None, SynthesizedLocalKind.ForEachArrayIndex);
				array5[j] = LocalRewriter.MakeBoundLocal(commonForEachStatementSyntax, array4[j], specialType);
			}
			SyntaxNode syntaxNode = commonForEachStatementSyntax;
			SyntaxNode syntaxNode2 = commonForEachStatementSyntax;
			BoundExpression boundExpression3 = boundLocal;
			BoundExpression[] array6 = array5;
			BoundExpression boundExpression4 = this.MakeConversionNode(syntaxNode, new BoundArrayAccess(syntaxNode2, boundExpression3, ImmutableArray.Create<BoundExpression>(array6), arrayTypeSymbol.ElementType, false), node.ElementConversion, node.IterationVariableType.Type, node.Checked, false, null);
			ImmutableArray<LocalSymbol> iterationVariables = node.IterationVariables;
			BoundStatement boundStatement3 = this.LocalOrDeconstructionDeclaration(node, iterationVariables, boundExpression4);
			this.InstrumentForEachStatementIterationVarDeclaration(node, ref boundStatement3);
			BoundStatement boundStatement4 = LocalRewriter.CreateBlockDeclaringIterationVariables(iterationVariables, boundStatement3, boundStatement, commonForEachStatementSyntax);
			BoundStatement boundStatement5 = null;
			for (int k = rank - 1; k >= 0; k--)
			{
				ImmutableArray<BoundExpression> immutableArray2 = ImmutableArray.Create<BoundExpression>(this.MakeLiteral(commonForEachStatementSyntax, ConstantValue.Create(k, ConstantValueTypeDiscriminator.Int32), specialType, null));
				BoundExpression boundExpression5 = BoundCall.Synthesized(commonForEachStatementSyntax, boundLocal, methodSymbol, immutableArray2);
				BoundStatement boundStatement6 = this.MakeLocalDeclaration(commonForEachStatementSyntax, array4[k], boundExpression5);
				GeneratedLabelSymbol generatedLabelSymbol = ((k == 0) ? node.BreakLabel : new GeneratedLabelSymbol("break"));
				BoundExpression boundExpression6 = new BoundBinaryOperator(commonForEachStatementSyntax, BinaryOperatorKind.IntLessThanOrEqual, null, null, LookupResultKind.Viable, array5[k], array2[k], specialType2, false);
				BoundStatement boundStatement7 = this.MakePositionIncrement(commonForEachStatementSyntax, array5[k], specialType);
				BoundStatement boundStatement8;
				GeneratedLabelSymbol generatedLabelSymbol2;
				if (boundStatement5 == null)
				{
					boundStatement8 = boundStatement4;
					generatedLabelSymbol2 = node.ContinueLabel;
				}
				else
				{
					boundStatement8 = boundStatement5;
					generatedLabelSymbol2 = new GeneratedLabelSymbol("continue");
				}
				boundStatement5 = this.RewriteForStatementWithoutInnerLocals(node, ImmutableArray.Create<LocalSymbol>(array4[k]), boundStatement6, boundExpression6, boundStatement7, boundStatement8, generatedLabelSymbol, generatedLabelSymbol2, node.HasErrors);
			}
			BoundStatement boundStatement9 = new BoundBlock(commonForEachStatementSyntax, ImmutableArray.Create<LocalSymbol>(localSymbol).Concat<LocalSymbol>(array.AsImmutableOrNull<LocalSymbol>()), ImmutableArray.Create<BoundStatement>(boundStatement2).Concat<BoundStatement>(array3.AsImmutableOrNull<BoundStatement>()).Add(boundStatement5), false);
			this.InstrumentForEachStatement(node, ref boundStatement9);
			return boundStatement9;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000C4050 File Offset: 0x000C2250
		private static BoundExpression GetUnconvertedCollectionExpression(BoundForEachStatement node)
		{
			BoundExpression expression = node.Expression;
			if (expression.Kind == BoundKind.Conversion)
			{
				return ((BoundConversion)expression).Operand;
			}
			return expression;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000C407B File Offset: 0x000C227B
		private static BoundLocal MakeBoundLocal(CSharpSyntaxNode syntax, LocalSymbol local, TypeSymbol type)
		{
			return new BoundLocal(syntax, local, null, type, false);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000C4087 File Offset: 0x000C2287
		private BoundStatement MakeLocalDeclaration(CSharpSyntaxNode syntax, LocalSymbol local, BoundExpression rewrittenInitialValue)
		{
			return this.RewriteLocalDeclaration(null, syntax, local, rewrittenInitialValue, false);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000C4094 File Offset: 0x000C2294
		private BoundStatement MakePositionIncrement(CSharpSyntaxNode syntax, BoundLocal boundPositionVar, TypeSymbol intType)
		{
			return BoundSequencePoint.CreateHidden(new BoundExpressionStatement(syntax, new BoundAssignmentOperator(syntax, boundPositionVar, new BoundBinaryOperator(syntax, BinaryOperatorKind.IntAddition, null, null, LookupResultKind.Viable, boundPositionVar, this.MakeLiteral(syntax, ConstantValue.Create(1), intType, null), intType, false), intType, false, false), false), false);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000C40D9 File Offset: 0x000C22D9
		private void InstrumentForEachStatementCollectionVarDeclaration(BoundForEachStatement original, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullIfNotNull("collectionVarDecl")] ref BoundStatement collectionVarDecl)
		{
			if (this.Instrument)
			{
				collectionVarDecl = this._instrumenter.InstrumentForEachStatementCollectionVarDeclaration(original, collectionVarDecl);
			}
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000C40F3 File Offset: 0x000C22F3
		private void InstrumentForEachStatementIterationVarDeclaration(BoundForEachStatement original, ref BoundStatement iterationVarDecl)
		{
			if (this.Instrument)
			{
				if (((CommonForEachStatementSyntax)original.Syntax) is ForEachVariableStatementSyntax)
				{
					iterationVarDecl = this._instrumenter.InstrumentForEachStatementDeconstructionVariablesDeclaration(original, iterationVarDecl);
					return;
				}
				iterationVarDecl = this._instrumenter.InstrumentForEachStatementIterationVarDeclaration(original, iterationVarDecl);
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000C4130 File Offset: 0x000C2330
		private void InstrumentForEachStatement(BoundForEachStatement original, ref BoundStatement result)
		{
			if (this.Instrument)
			{
				result = this._instrumenter.InstrumentForEachStatement(original, result);
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000C414C File Offset: 0x000C234C
		private BoundStatement MakeWhileTrueLoop(BoundForEachStatement loop, BoundBlock body)
		{
			SyntaxNode syntax = loop.Syntax;
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("still-true");
			BoundStatement boundStatement = new BoundLabelStatement(syntax, generatedLabelSymbol);
			if (this.Instrument)
			{
				boundStatement = BoundSequencePoint.CreateHidden(boundStatement, false);
			}
			return BoundStatementList.Synthesized(syntax, false, new BoundStatement[]
			{
				boundStatement,
				body,
				new BoundGotoStatement(syntax, generatedLabelSymbol, false)
			});
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000C41A4 File Offset: 0x000C23A4
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			BoundStatement boundStatement = this.VisitStatement(node.Initializer);
			BoundExpression boundExpression = this.VisitExpression(node.Condition);
			BoundStatement boundStatement2 = this.VisitStatement(node.Increment);
			BoundStatement boundStatement3 = this.VisitStatement(node.Body);
			if (boundExpression != null && this.Instrument)
			{
				boundExpression = this._instrumenter.InstrumentForStatementCondition(node, boundExpression, this._factory);
			}
			return this.RewriteForStatement(node, boundStatement, boundExpression, boundStatement2, boundStatement3);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000C4210 File Offset: 0x000C2410
		private BoundStatement RewriteForStatementWithoutInnerLocals(BoundLoopStatement original, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> outerLocals, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement rewrittenInitializer, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenCondition, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement rewrittenIncrement, BoundStatement rewrittenBody, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors)
		{
			SyntaxNode syntax = original.Syntax;
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			if (rewrittenInitializer != null)
			{
				instance.Add(rewrittenInitializer);
			}
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("start");
			GeneratedLabelSymbol generatedLabelSymbol2 = new GeneratedLabelSymbol("end");
			BoundStatement boundStatement = new BoundGotoStatement(syntax, generatedLabelSymbol2, false);
			if (this.Instrument)
			{
				boundStatement = BoundSequencePoint.CreateHidden(boundStatement, false);
			}
			instance.Add(boundStatement);
			instance.Add(new BoundLabelStatement(syntax, generatedLabelSymbol));
			instance.Add(rewrittenBody);
			instance.Add(new BoundLabelStatement(syntax, continueLabel));
			if (rewrittenIncrement != null)
			{
				instance.Add(rewrittenIncrement);
			}
			instance.Add(new BoundLabelStatement(syntax, generatedLabelSymbol2));
			BoundStatement boundStatement2;
			if (rewrittenCondition != null)
			{
				boundStatement2 = new BoundConditionalGoto(rewrittenCondition.Syntax, rewrittenCondition, true, generatedLabelSymbol, false);
			}
			else
			{
				boundStatement2 = new BoundGotoStatement(syntax, generatedLabelSymbol, false);
			}
			if (this.Instrument)
			{
				BoundKind kind = original.Kind;
				if (kind != BoundKind.ForStatement)
				{
					if (kind != BoundKind.ForEachStatement)
					{
						throw ExceptionUtilities.UnexpectedValue(original.Kind);
					}
					boundStatement2 = this._instrumenter.InstrumentForEachStatementConditionalGotoStart((BoundForEachStatement)original, boundStatement2);
				}
				else
				{
					boundStatement2 = this._instrumenter.InstrumentForStatementConditionalGotoStartOrBreak((BoundForStatement)original, boundStatement2);
				}
			}
			instance.Add(boundStatement2);
			instance.Add(new BoundLabelStatement(syntax, breakLabel));
			ImmutableArray<BoundStatement> immutableArray = instance.ToImmutableAndFree();
			return new BoundBlock(syntax, outerLocals, immutableArray, hasErrors);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000C4354 File Offset: 0x000C2554
		private BoundStatement RewriteForStatement(BoundForStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement rewrittenInitializer, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenCondition, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement rewrittenIncrement, BoundStatement rewrittenBody)
		{
			if (node.InnerLocals.IsEmpty)
			{
				return this.RewriteForStatementWithoutInnerLocals(node, node.OuterLocals, rewrittenInitializer, rewrittenCondition, rewrittenIncrement, rewrittenBody, node.BreakLabel, node.ContinueLabel, node.HasErrors);
			}
			SyntaxNode syntax = node.Syntax;
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			if (rewrittenInitializer != null)
			{
				instance.Add(rewrittenInitializer);
			}
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("start");
			BoundStatement boundStatement = new BoundLabelStatement(syntax, generatedLabelSymbol);
			if (this.Instrument)
			{
				boundStatement = BoundSequencePoint.CreateHidden(boundStatement, false);
			}
			instance.Add(boundStatement);
			ArrayBuilder<BoundStatement> instance2 = ArrayBuilder<BoundStatement>.GetInstance();
			if (rewrittenCondition != null)
			{
				BoundStatement boundStatement2 = new BoundConditionalGoto(rewrittenCondition.Syntax, rewrittenCondition, false, node.BreakLabel, false);
				if (this.Instrument)
				{
					boundStatement2 = this._instrumenter.InstrumentForStatementConditionalGotoStartOrBreak(node, boundStatement2);
				}
				instance2.Add(boundStatement2);
			}
			instance2.Add(rewrittenBody);
			instance2.Add(new BoundLabelStatement(syntax, node.ContinueLabel));
			if (rewrittenIncrement != null)
			{
				instance2.Add(rewrittenIncrement);
			}
			instance2.Add(new BoundGotoStatement(syntax, generatedLabelSymbol, false));
			instance.Add(new BoundBlock(syntax, node.InnerLocals, instance2.ToImmutableAndFree(), false));
			instance.Add(new BoundLabelStatement(syntax, node.BreakLabel));
			ImmutableArray<BoundStatement> immutableArray = instance.ToImmutableAndFree();
			return new BoundBlock(syntax, node.OuterLocals, immutableArray, node.HasErrors);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000C449C File Offset: 0x000C269C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.InvokedExpression);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			MethodSymbol signature = node.FunctionPointer.Signature;
			ImmutableArray<RefKind> argumentRefKindsOpt = node.ArgumentRefKindsOpt;
			ImmutableArray<LocalSymbol> immutableArray2;
			immutableArray = this.MakeArguments(node.Syntax, immutableArray, signature, false, default(ImmutableArray<int>), ref argumentRefKindsOpt, out immutableArray2, false, ThreeState.False);
			node = node.Update(boundExpression, immutableArray, argumentRefKindsOpt, node.ResultKind, node.Type);
			if (immutableArray2.IsDefaultOrEmpty)
			{
				return node;
			}
			return new BoundSequence(node.Syntax, immutableArray2, ImmutableArray<BoundExpression>.Empty, node, node.Type, false);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000C4534 File Offset: 0x000C2734
		public override BoundNode VisitGotoStatement(BoundGotoStatement node)
		{
			BoundExpression boundExpression = null;
			BoundLabel boundLabel = null;
			BoundStatement boundStatement = node.Update(node.Label, boundExpression, boundLabel);
			if (this.Instrument && !node.WasCompilerGenerated)
			{
				boundStatement = this._instrumenter.InstrumentGotoStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000C4573 File Offset: 0x000C2773
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLabel(BoundLabel node)
		{
			return null;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000C4578 File Offset: 0x000C2778
		public override BoundNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node)
		{
			SyntaxNode syntax = node.Syntax;
			FieldSymbol hostObjectField = this._previousSubmissionFields.GetHostObjectField();
			BoundThisReference boundThisReference = new BoundThisReference(syntax, this._factory.CurrentType);
			return new BoundFieldAccess(syntax, boundThisReference, hostObjectField, null, false);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000C45B4 File Offset: 0x000C27B4
		public override BoundNode VisitIfStatement(BoundIfStatement node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Condition);
			BoundStatement boundStatement = this.VisitStatement(node.Consequence);
			BoundStatement boundStatement2 = this.VisitStatement(node.AlternativeOpt);
			SyntaxNode syntaxNode = (IfStatementSyntax)node.Syntax;
			if (this.Instrument && !node.WasCompilerGenerated)
			{
				boundExpression = this._instrumenter.InstrumentIfStatementCondition(node, boundExpression, this._factory);
			}
			BoundStatement boundStatement3 = LocalRewriter.RewriteIfStatement(syntaxNode, boundExpression, boundStatement, boundStatement2, node.HasErrors);
			if (this.Instrument && !node.WasCompilerGenerated)
			{
				boundStatement3 = this._instrumenter.InstrumentIfStatement(node, boundStatement3);
			}
			return boundStatement3;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000C4648 File Offset: 0x000C2848
		private static BoundStatement RewriteIfStatement(SyntaxNode syntax, BoundExpression rewrittenCondition, BoundStatement rewrittenConsequence, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement rewrittenAlternativeOpt, bool hasErrors)
		{
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("afterif");
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			if (rewrittenAlternativeOpt == null)
			{
				instance.Add(new BoundConditionalGoto(rewrittenCondition.Syntax, rewrittenCondition, false, generatedLabelSymbol, false));
				instance.Add(rewrittenConsequence);
				instance.Add(BoundSequencePoint.CreateHidden(null, false));
				instance.Add(new BoundLabelStatement(syntax, generatedLabelSymbol));
				ImmutableArray<BoundStatement> immutableArray = instance.ToImmutableAndFree();
				return new BoundStatementList(syntax, immutableArray, hasErrors);
			}
			GeneratedLabelSymbol generatedLabelSymbol2 = new GeneratedLabelSymbol("alternative");
			instance.Add(new BoundConditionalGoto(rewrittenCondition.Syntax, rewrittenCondition, false, generatedLabelSymbol2, false));
			instance.Add(rewrittenConsequence);
			instance.Add(BoundSequencePoint.CreateHidden(null, false));
			instance.Add(new BoundGotoStatement(syntax, generatedLabelSymbol, false));
			instance.Add(new BoundLabelStatement(syntax, generatedLabelSymbol2));
			instance.Add(rewrittenAlternativeOpt);
			instance.Add(BoundSequencePoint.CreateHidden(null, false));
			instance.Add(new BoundLabelStatement(syntax, generatedLabelSymbol));
			return new BoundStatementList(syntax, instance.ToImmutableAndFree(), hasErrors);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000C4730 File Offset: 0x000C2930
		public override BoundNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node)
		{
			NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
			BoundExpression boundExpression = this.MakeLiteral(node.Syntax, ConstantValue.Create(true), specialType, null);
			BoundExpression boundExpression2 = this.VisitExpression(node.Operand);
			if (LocalRewriter.NullableNeverHasValue(boundExpression2))
			{
				boundExpression2 = new BoundDefaultExpression(boundExpression2.Syntax, boundExpression2.Type.GetNullableUnderlyingType(), false);
			}
			boundExpression2 = LocalRewriter.NullableAlwaysHasValue(boundExpression2) ?? boundExpression2;
			if (!node.Type.IsNullableType())
			{
				return new BoundObjectCreationExpression(node.Syntax, node.MethodOpt, new BoundExpression[] { boundExpression2, boundExpression });
			}
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
			boundExpression2 = this.CaptureExpressionInTempIfNeeded(boundExpression2, instance, instance2, SynthesizedLocalKind.LoweringTemp);
			BoundExpression boundExpression3 = this.MakeOptimizedHasValue(boundExpression2.Syntax, boundExpression2);
			BoundExpression boundExpression4 = this.MakeOptimizedGetValueOrDefault(boundExpression2.Syntax, boundExpression2);
			BoundExpression boundExpression5 = new BoundObjectCreationExpression(node.Syntax, node.MethodOpt, new BoundExpression[] { boundExpression4, boundExpression });
			MethodSymbol methodSymbol;
			if (!this.TryGetNullableMethod(node.Syntax, node.Type, SpecialMember.System_Nullable_T__ctor, out methodSymbol))
			{
				return LocalRewriter.BadExpression(node.Syntax, node.Type, boundExpression2);
			}
			BoundExpression boundExpression6 = new BoundObjectCreationExpression(node.Syntax, methodSymbol, new BoundExpression[] { boundExpression5 });
			BoundExpression boundExpression7 = new BoundDefaultExpression(node.Syntax, node.Type, false);
			BoundExpression boundExpression8 = LocalRewriter.RewriteConditionalOperator(node.Syntax, boundExpression3, boundExpression6, boundExpression7, null, node.Type, false);
			return new BoundSequence(node.Syntax, instance2.ToImmutableAndFree(), instance.ToImmutableAndFree(), boundExpression8, node.Type, false);
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000C48B4 File Offset: 0x000C2AB4
		private BoundExpression MakeDynamicIndexerAccessReceiver(BoundDynamicIndexerAccess indexerAccess, BoundExpression loweredReceiver)
		{
			string text = indexerAccess.TryGetIndexedPropertyName();
			BoundExpression boundExpression;
			if (text != null)
			{
				boundExpression = this._dynamicFactory.MakeDynamicGetMember(loweredReceiver, text, true).ToExpression();
			}
			else
			{
				boundExpression = loweredReceiver;
			}
			return boundExpression;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000C48E8 File Offset: 0x000C2AE8
		public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Receiver);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			return this.MakeDynamicGetIndex(node, boundExpression, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000C4924 File Offset: 0x000C2B24
		private BoundExpression MakeDynamicGetIndex(BoundDynamicIndexerAccess node, BoundExpression loweredReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNames, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds)
		{
			this.EmbedIfNeedTo(loweredReceiver, node.ApplicableIndexers, node.Syntax);
			return this._dynamicFactory.MakeDynamicGetIndex(this.MakeDynamicIndexerAccessReceiver(node, loweredReceiver), loweredArguments, argumentNames, refKinds).ToExpression();
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000C4964 File Offset: 0x000C2B64
		public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
		{
			return this.VisitIndexerAccess(node, false);
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000C4970 File Offset: 0x000C2B70
		private BoundExpression VisitIndexerAccess(BoundIndexerAccess node, bool isLeftOfAssignment)
		{
			PropertySymbol indexer = node.Indexer;
			BoundExpression boundExpression = this.VisitExpression(node.ReceiverOpt);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			return this.MakeIndexerAccess(node.Syntax, boundExpression, indexer, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.Type, node, isLeftOfAssignment);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000C49D4 File Offset: 0x000C2BD4
		private BoundExpression MakeIndexerAccess(SyntaxNode syntax, BoundExpression rewrittenReceiver, PropertySymbol indexer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] BoundIndexerAccess oldNodeOpt, bool isLeftOfAssignment)
		{
			if (isLeftOfAssignment && indexer.RefKind == RefKind.None)
			{
				if (oldNodeOpt == null)
				{
					return new BoundIndexerAccess(syntax, rewrittenReceiver, indexer, rewrittenArguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, type, false);
				}
				return oldNodeOpt.Update(rewrittenReceiver, indexer, rewrittenArguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, type);
			}
			else
			{
				MethodSymbol ownOrInheritedGetMethod = indexer.GetOwnOrInheritedGetMethod();
				ImmutableArray<LocalSymbol> immutableArray;
				rewrittenArguments = this.MakeArguments(syntax, rewrittenArguments, indexer, expanded, argsToParamsOpt, ref argumentRefKindsOpt, out immutableArray, false, ThreeState.True);
				BoundExpression boundExpression = this.MakePropertyGetAccess(syntax, rewrittenReceiver, indexer, rewrittenArguments, ownOrInheritedGetMethod, null);
				if (immutableArray.IsDefaultOrEmpty)
				{
					return boundExpression;
				}
				return new BoundSequence(syntax, immutableArray, ImmutableArray<BoundExpression>.Empty, boundExpression, type, false);
			}
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000C4A67 File Offset: 0x000C2C67
		public override BoundNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node)
		{
			return this.VisitIndexOrRangePatternIndexerAccess(node, false);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000C4A74 File Offset: 0x000C2C74
		private BoundSequence VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node, bool isLeftOfAssignment)
		{
			if (TypeSymbol.Equals(node.Argument.Type, this._compilation.GetWellKnownType(WellKnownType.System_Index), TypeCompareKind.ConsiderEverything))
			{
				return this.VisitIndexPatternIndexerAccess(node.Syntax, node.Receiver, node.LengthOrCountProperty, (PropertySymbol)node.PatternSymbol, node.Argument, isLeftOfAssignment);
			}
			return this.VisitRangePatternIndexerAccess(node.Receiver, node.LengthOrCountProperty, (MethodSymbol)node.PatternSymbol, node.Argument);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000C4AF4 File Offset: 0x000C2CF4
		private BoundSequence VisitIndexPatternIndexerAccess(SyntaxNode syntax, BoundExpression receiver, PropertySymbol lengthOrCountProperty, PropertySymbol intIndexer, BoundExpression argument, bool isLeftOfAssignment)
		{
			SyntheticBoundNodeFactory factory = this._factory;
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = factory.StoreToTemp(this.VisitExpression(receiver), out boundAssignmentOperator, receiver.Type.IsReferenceType ? RefKind.None : RefKind.Ref, SynthesizedLocalKind.LoweringTemp, null);
			BoundAssignmentOperator boundAssignmentOperator2;
			BoundLocal boundLocal2 = factory.StoreToTemp(factory.Property(boundLocal, lengthOrCountProperty), out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			bool flag;
			BoundAssignmentOperator boundAssignmentOperator3;
			BoundLocal boundLocal3 = factory.StoreToTemp(this.MakePatternIndexOffsetExpression(argument, boundLocal2, out flag), out boundAssignmentOperator3, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance(3);
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance(3);
			instance.Add(boundLocal.LocalSymbol);
			instance2.Add(boundAssignmentOperator);
			if (flag)
			{
				instance.Add(boundLocal2.LocalSymbol);
				instance2.Add(boundAssignmentOperator2);
			}
			instance.Add(boundLocal3.LocalSymbol);
			instance2.Add(boundAssignmentOperator3);
			return (BoundSequence)factory.Sequence(instance.ToImmutable(), instance2.ToImmutable(), this.MakeIndexerAccess(syntax, boundLocal, intIndexer, ImmutableArray.Create<BoundExpression>(boundLocal3), default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), default(BitVector), intIndexer.Type, null, isLeftOfAssignment));
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000C4C0C File Offset: 0x000C2E0C
		private BoundExpression MakePatternIndexOffsetExpression(BoundExpression unloweredExpr, BoundExpression lengthAccess, out bool usedLength)
		{
			SyntheticBoundNodeFactory factory = this._factory;
			BoundFromEndIndexExpression boundFromEndIndexExpression = unloweredExpr as BoundFromEndIndexExpression;
			if (boundFromEndIndexExpression != null)
			{
				usedLength = true;
				return factory.IntSubtract(lengthAccess, this.VisitExpression(boundFromEndIndexExpression.Operand));
			}
			BoundConversion boundConversion = unloweredExpr as BoundConversion;
			if (boundConversion != null)
			{
				BoundExpression operand = boundConversion.Operand;
				if (operand != null)
				{
					TypeSymbol type = operand.Type;
					if (type != null && type.SpecialType == SpecialType.System_Int32)
					{
						usedLength = false;
						return this.VisitExpression(operand);
					}
				}
			}
			usedLength = true;
			return factory.Call(this.VisitExpression(unloweredExpr), WellKnownMember.System_Index__GetOffset, lengthAccess);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000C4C90 File Offset: 0x000C2E90
		private BoundSequence VisitRangePatternIndexerAccess(BoundExpression receiver, PropertySymbol lengthOrCountProperty, MethodSymbol sliceMethod, BoundExpression rangeArg)
		{
			SyntheticBoundNodeFactory factory = this._factory;
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = factory.StoreToTemp(this.VisitExpression(receiver), out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			BoundAssignmentOperator boundAssignmentOperator2;
			BoundLocal boundLocal2 = factory.StoreToTemp(factory.Property(boundLocal, lengthOrCountProperty), out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			instance.Add(boundLocal.LocalSymbol);
			instance2.Add(boundAssignmentOperator);
			BoundRangeExpression boundRangeExpression = rangeArg as BoundRangeExpression;
			BoundExpression boundExpression;
			BoundExpression boundExpression3;
			if (boundRangeExpression != null)
			{
				bool flag = false;
				if (boundRangeExpression.LeftOperandOpt != null)
				{
					BoundAssignmentOperator boundAssignmentOperator3;
					BoundLocal boundLocal3 = factory.StoreToTemp(this.MakePatternIndexOffsetExpression(boundRangeExpression.LeftOperandOpt, boundLocal2, out flag), out boundAssignmentOperator3, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
					instance.Add(boundLocal3.LocalSymbol);
					instance2.Add(boundAssignmentOperator3);
					boundExpression = boundLocal3;
				}
				else
				{
					boundExpression = factory.Literal(0);
				}
				BoundExpression rightOperandOpt = boundRangeExpression.RightOperandOpt;
				BoundExpression boundExpression2;
				if (rightOperandOpt != null)
				{
					bool flag2;
					boundExpression2 = this.MakePatternIndexOffsetExpression(rightOperandOpt, boundLocal2, out flag2);
					flag = flag || flag2;
				}
				else
				{
					flag = true;
					boundExpression2 = boundLocal2;
				}
				if (flag)
				{
					instance.Insert(1, boundLocal2.LocalSymbol);
					instance2.Insert(1, boundAssignmentOperator2);
				}
				BoundAssignmentOperator boundAssignmentOperator4;
				BoundLocal boundLocal4 = factory.StoreToTemp(factory.IntSubtract(boundExpression2, boundExpression), out boundAssignmentOperator4, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				instance.Add(boundLocal4.LocalSymbol);
				instance2.Add(boundAssignmentOperator4);
				boundExpression3 = boundLocal4;
			}
			else
			{
				BoundAssignmentOperator boundAssignmentOperator5;
				BoundLocal boundLocal5 = factory.StoreToTemp(this.VisitExpression(rangeArg), out boundAssignmentOperator5, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				instance.Add(boundLocal2.LocalSymbol);
				instance2.Add(boundAssignmentOperator2);
				instance.Add(boundLocal5.LocalSymbol);
				instance2.Add(boundAssignmentOperator5);
				BoundAssignmentOperator boundAssignmentOperator6;
				BoundLocal boundLocal6 = factory.StoreToTemp(factory.Call(factory.Call(boundLocal5, factory.WellKnownMethod(WellKnownMember.System_Range__get_Start)), factory.WellKnownMethod(WellKnownMember.System_Index__GetOffset), boundLocal2), out boundAssignmentOperator6, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				instance.Add(boundLocal6.LocalSymbol);
				instance2.Add(boundAssignmentOperator6);
				boundExpression = boundLocal6;
				BoundAssignmentOperator boundAssignmentOperator7;
				BoundLocal boundLocal7 = factory.StoreToTemp(factory.IntSubtract(factory.Call(factory.Call(boundLocal5, factory.WellKnownMethod(WellKnownMember.System_Range__get_End)), factory.WellKnownMethod(WellKnownMember.System_Index__GetOffset), boundLocal2), boundExpression), out boundAssignmentOperator7, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				instance.Add(boundLocal7.LocalSymbol);
				instance2.Add(boundAssignmentOperator7);
				boundExpression3 = boundLocal7;
			}
			return (BoundSequence)factory.Sequence(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree(), factory.Call(boundLocal, sliceMethod, boundExpression, boundExpression3));
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000C4ED0 File Offset: 0x000C30D0
		public override BoundNode VisitIsOperator(BoundIsOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Operand);
			BoundTypeExpression boundTypeExpression = (BoundTypeExpression)this.VisitTypeExpression(node.TargetType);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return this.MakeIsOperator(node, node.Syntax, boundExpression, boundTypeExpression, node.Conversion, typeSymbol);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000C4F20 File Offset: 0x000C3120
		private BoundExpression MakeIsOperator(BoundIsOperator oldNode, SyntaxNode syntax, BoundExpression rewrittenOperand, BoundTypeExpression rewrittenTargetType, Conversion conversion, TypeSymbol rewrittenType)
		{
			if (rewrittenOperand.Kind != BoundKind.MethodGroup)
			{
				TypeSymbol type = rewrittenOperand.Type;
				TypeSymbol type2 = rewrittenTargetType.Type;
				if (!this._inExpressionLambda)
				{
					ConstantValue isOperatorConstantResult = Binder.GetIsOperatorConstantResult(type, type2, conversion.Kind, rewrittenOperand.ConstantValue, true);
					if (isOperatorConstantResult != null)
					{
						return this.RewriteConstantIsOperator(syntax, rewrittenOperand, isOperatorConstantResult, rewrittenType);
					}
					if (conversion.IsImplicit)
					{
						return this.MakeNullCheck(syntax, rewrittenOperand, BinaryOperatorKind.NotEqual);
					}
				}
				return oldNode.Update(rewrittenOperand, rewrittenTargetType, conversion, rewrittenType);
			}
			BoundExpression receiverOpt = ((BoundMethodGroup)rewrittenOperand).ReceiverOpt;
			if (receiverOpt != null && receiverOpt.Kind != BoundKind.ThisReference)
			{
				return this.RewriteConstantIsOperator(receiverOpt.Syntax, receiverOpt, ConstantValue.False, rewrittenType);
			}
			return this.MakeLiteral(syntax, ConstantValue.False, rewrittenType, null);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000C4FDE File Offset: 0x000C31DE
		private BoundExpression RewriteConstantIsOperator(SyntaxNode syntax, BoundExpression loweredOperand, ConstantValue constantValue, TypeSymbol type)
		{
			return new BoundSequence(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(loweredOperand), this.MakeLiteral(syntax, constantValue, type, null), type, false);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000C5000 File Offset: 0x000C3200
		public override BoundNode VisitIsPatternExpression(BoundIsPatternExpression node)
		{
			bool flag = node.IsNegated;
			BoundExpression boundExpression;
			if (LocalRewriter.<VisitIsPatternExpression>g__canProduceLinearSequence|286_0(node.DecisionDag.RootNode, node.WhenTrueLabel, node.WhenFalseLabel))
			{
				LocalRewriter.IsPatternExpressionLinearLocalRewriter isPatternExpressionLinearLocalRewriter = new LocalRewriter.IsPatternExpressionLinearLocalRewriter(node, this);
				boundExpression = isPatternExpressionLinearLocalRewriter.LowerIsPatternAsLinearTestSequence(node, node.WhenTrueLabel, node.WhenFalseLabel);
				isPatternExpressionLinearLocalRewriter.Free();
			}
			else if (LocalRewriter.<VisitIsPatternExpression>g__canProduceLinearSequence|286_0(node.DecisionDag.RootNode, node.WhenFalseLabel, node.WhenTrueLabel))
			{
				flag = !flag;
				LocalRewriter.IsPatternExpressionLinearLocalRewriter isPatternExpressionLinearLocalRewriter2 = new LocalRewriter.IsPatternExpressionLinearLocalRewriter(node, this);
				boundExpression = isPatternExpressionLinearLocalRewriter2.LowerIsPatternAsLinearTestSequence(node, node.WhenFalseLabel, node.WhenTrueLabel);
				isPatternExpressionLinearLocalRewriter2.Free();
			}
			else
			{
				LocalRewriter.IsPatternExpressionGeneralLocalRewriter isPatternExpressionGeneralLocalRewriter = new LocalRewriter.IsPatternExpressionGeneralLocalRewriter(node.Syntax, this);
				boundExpression = isPatternExpressionGeneralLocalRewriter.LowerGeneralIsPattern(node);
				isPatternExpressionGeneralLocalRewriter.Free();
			}
			if (flag)
			{
				boundExpression = this._factory.Not(boundExpression);
			}
			return boundExpression;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000C50C4 File Offset: 0x000C32C4
		private static bool IsFailureNode(BoundDecisionDagNode node, LabelSymbol whenFalseLabel)
		{
			BoundWhenDecisionDagNode boundWhenDecisionDagNode = node as BoundWhenDecisionDagNode;
			if (boundWhenDecisionDagNode != null)
			{
				node = boundWhenDecisionDagNode.WhenTrue;
			}
			BoundLeafDecisionDagNode boundLeafDecisionDagNode = node as BoundLeafDecisionDagNode;
			return boundLeafDecisionDagNode != null && boundLeafDecisionDagNode.Label == whenFalseLabel;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000C50FC File Offset: 0x000C32FC
		public override BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			return this.MakeLabeledStatement(node, boundStatement);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000C5120 File Offset: 0x000C3320
		private BoundStatement MakeLabeledStatement(BoundLabeledStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement rewrittenBody)
		{
			BoundStatement boundStatement = new BoundLabelStatement(node.Syntax, node.Label);
			if (this.Instrument && node.Syntax is LabeledStatementSyntax)
			{
				boundStatement = this._instrumenter.InstrumentLabelStatement(node, boundStatement);
			}
			if (rewrittenBody == null)
			{
				return boundStatement;
			}
			return BoundStatementList.Synthesized(node.Syntax, new BoundStatement[] { boundStatement, rewrittenBody });
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000C5180 File Offset: 0x000C3380
		public override BoundNode VisitLiteral(BoundLiteral node)
		{
			return this.MakeLiteral(node.Syntax, node.ConstantValue, node.Type, node);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000C519B File Offset: 0x000C339B
		private BoundExpression MakeLiteral(SyntaxNode syntax, ConstantValue constantValue, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] BoundLiteral oldNodeOpt = null)
		{
			if (constantValue.IsDecimal)
			{
				return this.MakeDecimalLiteral(syntax, constantValue);
			}
			if (constantValue.IsDateTime)
			{
				return this.MakeDateTimeLiteral(syntax, constantValue);
			}
			if (oldNodeOpt != null)
			{
				return oldNodeOpt.Update(constantValue, type);
			}
			return new BoundLiteral(syntax, constantValue, type, constantValue.IsBad);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000C51DC File Offset: 0x000C33DC
		private BoundExpression MakeDecimalLiteral(SyntaxNode syntax, ConstantValue constantValue)
		{
			decimal decimalValue = constantValue.DecimalValue;
			bool flag;
			byte b;
			uint num;
			uint num2;
			uint num3;
			decimalValue.GetBits(out flag, out b, out num, out num2, out num3);
			ArrayBuilder<BoundExpression> arrayBuilder = new ArrayBuilder<BoundExpression>();
			SpecialMember specialMember;
			if (b == 0 && -2147483648m <= decimalValue && decimalValue <= 2147483647m)
			{
				MethodSymbol currentFunction = this._factory.CurrentFunction;
				if ((currentFunction.MethodKind != MethodKind.StaticConstructor || currentFunction.ContainingType.SpecialType != SpecialType.System_Decimal) && !this._inExpressionLambda)
				{
					Symbol symbol = null;
					if (decimalValue == 0m)
					{
						symbol = this._compilation.GetSpecialTypeMember(SpecialMember.System_Decimal__Zero);
					}
					else if (decimalValue == 1m)
					{
						symbol = this._compilation.GetSpecialTypeMember(SpecialMember.System_Decimal__One);
					}
					else if (decimalValue == -1m)
					{
						symbol = this._compilation.GetSpecialTypeMember(SpecialMember.System_Decimal__MinusOne);
					}
					if (symbol != null && !symbol.HasUseSiteError)
					{
						NamedTypeSymbol containingType = symbol.ContainingType;
						if (containingType != null && !containingType.HasUseSiteError)
						{
							FieldSymbol fieldSymbol = (FieldSymbol)symbol;
							return new BoundFieldAccess(syntax, null, fieldSymbol, constantValue, false);
						}
					}
				}
				specialMember = SpecialMember.System_Decimal__CtorInt32;
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create((int)decimalValue), this._compilation.GetSpecialType(SpecialType.System_Int32)));
			}
			else if (b == 0 && 0m <= decimalValue && decimalValue <= 4294967295m)
			{
				specialMember = SpecialMember.System_Decimal__CtorUInt32;
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create((uint)decimalValue), this._compilation.GetSpecialType(SpecialType.System_UInt32)));
			}
			else if (b == 0 && -9223372036854775808m <= decimalValue && decimalValue <= 9223372036854775807m)
			{
				specialMember = SpecialMember.System_Decimal__CtorInt64;
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create((long)decimalValue), this._compilation.GetSpecialType(SpecialType.System_Int64)));
			}
			else if (b == 0 && 0m <= decimalValue && decimalValue <= 18446744073709551615m)
			{
				specialMember = SpecialMember.System_Decimal__CtorUInt64;
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create((ulong)decimalValue), this._compilation.GetSpecialType(SpecialType.System_UInt64)));
			}
			else
			{
				specialMember = SpecialMember.System_Decimal__CtorInt32Int32Int32BooleanByte;
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create(num), this._compilation.GetSpecialType(SpecialType.System_Int32)));
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create(num2), this._compilation.GetSpecialType(SpecialType.System_Int32)));
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create(num3), this._compilation.GetSpecialType(SpecialType.System_Int32)));
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create(flag), this._compilation.GetSpecialType(SpecialType.System_Boolean)));
				arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create(b), this._compilation.GetSpecialType(SpecialType.System_Byte)));
			}
			MethodSymbol methodSymbol = (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(specialMember);
			return new BoundObjectCreationExpression(syntax, methodSymbol, arrayBuilder.ToImmutableAndFree(), default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), default(BitVector), constantValue, null, methodSymbol.ContainingType, false);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000C5524 File Offset: 0x000C3724
		private BoundExpression MakeDateTimeLiteral(SyntaxNode syntax, ConstantValue constantValue)
		{
			ArrayBuilder<BoundExpression> arrayBuilder = new ArrayBuilder<BoundExpression>();
			arrayBuilder.Add(new BoundLiteral(syntax, ConstantValue.Create(constantValue.DateTimeValue.Ticks), this._compilation.GetSpecialType(SpecialType.System_Int64)));
			MethodSymbol methodSymbol = (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(SpecialMember.System_DateTime__CtorInt64);
			return new BoundObjectCreationExpression(syntax, methodSymbol, arrayBuilder.ToImmutableAndFree(), default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), default(BitVector), null, null, methodSymbol.ContainingType, false);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000C55B9 File Offset: 0x000C37B9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitLocalDeclaration(BoundLocalDeclaration node)
		{
			return this.RewriteLocalDeclaration(node, node.Syntax, node.LocalSymbol, this.VisitExpression(node.InitializerOpt), node.HasErrors);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000C55E0 File Offset: 0x000C37E0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private BoundStatement RewriteLocalDeclaration(BoundLocalDeclaration originalOpt, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(1)] LocalSymbol localSymbol, BoundExpression rewrittenInitializer, bool hasErrors = false)
		{
			if (rewrittenInitializer == null)
			{
				return null;
			}
			if (localSymbol.IsConst)
			{
				if (localSymbol.Type.IsReferenceType || localSymbol.ConstantValue != null)
				{
					return null;
				}
				hasErrors = true;
			}
			LocalDeclarationStatementSyntax localDeclarationStatementSyntax = syntax as LocalDeclarationStatementSyntax;
			if (localDeclarationStatementSyntax != null)
			{
				syntax = localDeclarationStatementSyntax.Declaration.Variables[0];
			}
			BoundStatement boundStatement = new BoundExpressionStatement(syntax, new BoundAssignmentOperator(syntax, new BoundLocal(syntax, localSymbol, null, localSymbol.Type, false), rewrittenInitializer, localSymbol.Type, localSymbol.IsRef, false), hasErrors);
			return this.InstrumentLocalDeclarationIfNecessary(originalOpt, localSymbol, boundStatement);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000C5670 File Offset: 0x000C3870
		private BoundStatement InstrumentLocalDeclarationIfNecessary([global::System.Runtime.CompilerServices.Nullable(2)] BoundLocalDeclaration originalOpt, LocalSymbol localSymbol, BoundStatement rewrittenLocalDeclaration)
		{
			if (this.Instrument && originalOpt != null && !originalOpt.WasCompilerGenerated && !localSymbol.IsConst && (originalOpt.Syntax.Kind() == SyntaxKind.VariableDeclarator || (originalOpt.Syntax.Kind() == SyntaxKind.LocalDeclarationStatement && ((LocalDeclarationStatementSyntax)originalOpt.Syntax).Declaration.Variables.Count == 1)))
			{
				rewrittenLocalDeclaration = this._instrumenter.InstrumentLocalInitialization(originalOpt, rewrittenLocalDeclaration);
			}
			return rewrittenLocalDeclaration;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000C56EC File Offset: 0x000C38EC
		public sealed override BoundNode VisitOutVariablePendingInference(OutVariablePendingInference node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000C56F4 File Offset: 0x000C38F4
		public override BoundNode VisitLockStatement(BoundLockStatement node)
		{
			LockStatementSyntax lockStatementSyntax = (LockStatementSyntax)node.Syntax;
			BoundExpression boundExpression = this.VisitExpression(node.Argument);
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			TypeSymbol typeSymbol = boundExpression.Type;
			if (typeSymbol == null)
			{
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Object);
				boundExpression = this.MakeLiteral(boundExpression.Syntax, boundExpression.ConstantValue, typeSymbol, null);
			}
			if (typeSymbol.Kind == SymbolKind.TypeParameter)
			{
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Object);
				boundExpression = this.MakeConversionNode(boundExpression.Syntax, boundExpression, Conversion.Boxing, typeSymbol, false, false, boundExpression.ConstantValue);
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.Lock, lockStatementSyntax);
			BoundStatement boundStatement2 = new BoundExpressionStatement(lockStatementSyntax, boundAssignmentOperator, false);
			MethodSymbol methodSymbol;
			BoundExpression boundExpression2;
			if (this.TryGetWellKnownTypeMember<MethodSymbol>(lockStatementSyntax, WellKnownMember.System_Threading_Monitor__Exit, out methodSymbol, false, null))
			{
				boundExpression2 = BoundCall.Synthesized(lockStatementSyntax, null, methodSymbol, boundLocal);
			}
			else
			{
				boundExpression2 = new BoundBadExpression(lockStatementSyntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(boundLocal), ErrorTypeSymbol.UnknownResultType);
			}
			BoundStatement boundStatement3 = new BoundExpressionStatement(lockStatementSyntax, boundExpression2, false);
			MethodSymbol methodSymbol2;
			if ((this.TryGetWellKnownTypeMember<MethodSymbol>(lockStatementSyntax, WellKnownMember.System_Threading_Monitor__Enter2, out methodSymbol2, true, null) || this.TryGetWellKnownTypeMember<MethodSymbol>(lockStatementSyntax, WellKnownMember.System_Threading_Monitor__Enter, out methodSymbol2, false, null)) && methodSymbol2.ParameterCount == 2)
			{
				TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
				BoundAssignmentOperator boundAssignmentOperator2;
				BoundLocal boundLocal2 = this._factory.StoreToTemp(this.MakeLiteral(boundExpression.Syntax, ConstantValue.False, specialType, null), out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LockTaken, lockStatementSyntax);
				BoundStatement boundStatement4 = new BoundExpressionStatement(lockStatementSyntax, boundAssignmentOperator2, false);
				BoundStatement boundStatement5 = new BoundExpressionStatement(lockStatementSyntax, BoundCall.Synthesized(lockStatementSyntax, null, methodSymbol2, boundLocal, boundLocal2), false);
				boundStatement3 = LocalRewriter.RewriteIfStatement(lockStatementSyntax, boundLocal2, boundStatement3, null, node.HasErrors);
				return new BoundBlock(lockStatementSyntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol, boundLocal2.LocalSymbol), ImmutableArray.Create<BoundStatement>(this.InstrumentLockTargetCapture(node, boundStatement2), boundStatement4, new BoundTryStatement(lockStatementSyntax, BoundBlock.SynthesizedNoLocals(lockStatementSyntax, ImmutableArray.Create<BoundStatement>(boundStatement5, boundStatement)), ImmutableArray<BoundCatchBlock>.Empty, BoundBlock.SynthesizedNoLocals(lockStatementSyntax, boundStatement3), null)), false);
			}
			BoundExpression boundExpression3;
			if (methodSymbol2 != null)
			{
				boundExpression3 = BoundCall.Synthesized(lockStatementSyntax, null, methodSymbol2, boundLocal);
			}
			else
			{
				boundExpression3 = new BoundBadExpression(lockStatementSyntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(boundLocal), ErrorTypeSymbol.UnknownResultType);
			}
			BoundStatement boundStatement6 = new BoundExpressionStatement(lockStatementSyntax, boundExpression3, false);
			return new BoundBlock(lockStatementSyntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundStatement>(this.InstrumentLockTargetCapture(node, boundStatement2), boundStatement6, new BoundTryStatement(lockStatementSyntax, BoundBlock.SynthesizedNoLocals(lockStatementSyntax, boundStatement), ImmutableArray<BoundCatchBlock>.Empty, BoundBlock.SynthesizedNoLocals(lockStatementSyntax, boundStatement3), null)), false);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000C594B File Offset: 0x000C3B4B
		private BoundStatement InstrumentLockTargetCapture(BoundLockStatement original, BoundStatement lockTargetCapture)
		{
			if (!this.Instrument)
			{
				return lockTargetCapture;
			}
			return this._instrumenter.InstrumentLockTargetCapture(original, lockTargetCapture);
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000C5964 File Offset: 0x000C3B64
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitMultipleLocalDeclarations(BoundMultipleLocalDeclarations node)
		{
			return this.VisitMultipleLocalDeclarationsBase(node);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000C596D File Offset: 0x000C3B6D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node)
		{
			return this.VisitMultipleLocalDeclarationsBase(node);
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000C5978 File Offset: 0x000C3B78
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundNode VisitMultipleLocalDeclarationsBase(BoundMultipleLocalDeclarationsBase node)
		{
			ArrayBuilder<BoundStatement> arrayBuilder = null;
			foreach (BoundLocalDeclaration boundLocalDeclaration in node.LocalDeclarations)
			{
				BoundNode boundNode = this.VisitLocalDeclaration(boundLocalDeclaration);
				if (boundNode != null)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<BoundStatement>.GetInstance();
					}
					arrayBuilder.Add((BoundStatement)boundNode);
				}
			}
			if (arrayBuilder != null)
			{
				return BoundStatementList.Synthesized(node.Syntax, node.HasErrors, arrayBuilder.ToImmutableAndFree());
			}
			return null;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000C59E8 File Offset: 0x000C3BE8
		public override BoundNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node)
		{
			LocalRewriter.<>c__DisplayClass305_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.node = node;
			CS$<>8__locals1.syntax = CS$<>8__locals1.node.Syntax;
			CS$<>8__locals1.temps = ArrayBuilder<LocalSymbol>.GetInstance();
			CS$<>8__locals1.stores = ArrayBuilder<BoundExpression>.GetInstance();
			CS$<>8__locals1.transformedLHS = this.TransformCompoundAssignmentLHS(CS$<>8__locals1.node.LeftOperand, CS$<>8__locals1.stores, CS$<>8__locals1.temps, CS$<>8__locals1.node.LeftOperand.HasDynamicType());
			CS$<>8__locals1.lhsRead = this.MakeRValue(CS$<>8__locals1.transformedLHS);
			CS$<>8__locals1.loweredRight = this.VisitExpression(CS$<>8__locals1.node.RightOperand);
			if (!CS$<>8__locals1.node.IsNullableValueTypeAssignment)
			{
				return this.<VisitNullCoalescingAssignmentOperator>g__rewriteNullCoalscingAssignmentStandard|305_0(ref CS$<>8__locals1);
			}
			return this.<VisitNullCoalescingAssignmentOperator>g__rewriteNullCoalescingAssignmentForValueType|305_1(ref CS$<>8__locals1);
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000C5AAC File Offset: 0x000C3CAC
		public override BoundNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.LeftOperand);
			BoundExpression boundExpression2 = this.VisitExpression(node.RightOperand);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return this.MakeNullCoalescingOperator(node.Syntax, boundExpression, boundExpression2, node.LeftConversion, node.OperatorResultKind, typeSymbol);
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000C5AFC File Offset: 0x000C3CFC
		private BoundExpression MakeNullCoalescingOperator(SyntaxNode syntax, BoundExpression rewrittenLeft, BoundExpression rewrittenRight, Conversion leftConversion, BoundNullCoalescingOperatorResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol rewrittenResultType)
		{
			if (this._inExpressionLambda)
			{
				TypeSymbol typeSymbol = rewrittenLeft.Type.StrippedType();
				Conversion conversion = this.TryMakeConversion(syntax, leftConversion, typeSymbol, rewrittenResultType);
				if (!conversion.Exists)
				{
					return LocalRewriter.BadExpression(syntax, rewrittenResultType, rewrittenLeft, rewrittenRight);
				}
				return new BoundNullCoalescingOperator(syntax, rewrittenLeft, rewrittenRight, conversion, resultKind, rewrittenResultType, false);
			}
			else
			{
				TypeSymbol type = rewrittenLeft.Type;
				if (type == null || type.IsReferenceType || type.IsValueType)
				{
					if (rewrittenLeft.IsDefaultValue())
					{
						return rewrittenRight;
					}
					if (rewrittenLeft.ConstantValue != null)
					{
						return this.GetConvertedLeftForNullCoalescingOperator(rewrittenLeft, leftConversion, rewrittenResultType);
					}
				}
				if (this.IsStringConcat(rewrittenLeft))
				{
					return this.GetConvertedLeftForNullCoalescingOperator(rewrittenLeft, leftConversion, rewrittenResultType);
				}
				if (rewrittenLeft.Type.IsReferenceType && leftConversion.IsImplicit && !leftConversion.IsUserDefined)
				{
					if (!leftConversion.IsIdentity)
					{
						rewrittenLeft = this.MakeConversionNode(rewrittenLeft.Syntax, rewrittenLeft, leftConversion, rewrittenResultType, false, false, null);
					}
					return new BoundNullCoalescingOperator(syntax, rewrittenLeft, rewrittenRight, Conversion.Identity, resultKind, rewrittenResultType, false);
				}
				if (leftConversion.IsIdentity || leftConversion.Kind == ConversionKind.ExplicitNullable)
				{
					BoundLoweredConditionalAccess boundLoweredConditionalAccess = rewrittenLeft as BoundLoweredConditionalAccess;
					if (boundLoweredConditionalAccess != null && (boundLoweredConditionalAccess.WhenNullOpt == null || LocalRewriter.NullableNeverHasValue(boundLoweredConditionalAccess.WhenNullOpt)))
					{
						BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(boundLoweredConditionalAccess.WhenNotNull);
						if (boundExpression != null)
						{
							BoundExpression boundExpression2 = rewrittenRight;
							if (boundExpression2.Type.IsNullableType())
							{
								boundExpression = boundLoweredConditionalAccess.WhenNotNull;
							}
							if (boundExpression2.IsDefaultValue() && boundExpression2.Type.SpecialType != SpecialType.System_Decimal)
							{
								boundExpression2 = null;
							}
							return boundLoweredConditionalAccess.Update(boundLoweredConditionalAccess.Receiver, boundLoweredConditionalAccess.HasValueMethodOpt, boundExpression, boundExpression2, boundLoweredConditionalAccess.Id, rewrittenResultType);
						}
					}
				}
				MethodSymbol methodSymbol;
				if (rewrittenLeft.Type.IsNullableType() && LocalRewriter.RemoveIdentityConversions(rewrittenRight).IsDefaultValue() && rewrittenRight.Type.Equals(rewrittenLeft.Type.GetNullableUnderlyingType(), TypeCompareKind.AllIgnoreOptions) && this.TryGetNullableMethod(rewrittenLeft.Syntax, rewrittenLeft.Type, SpecialMember.System_Nullable_T_GetValueOrDefault, out methodSymbol))
				{
					return BoundCall.Synthesized(rewrittenLeft.Syntax, rewrittenLeft, methodSymbol);
				}
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(rewrittenLeft, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				BoundExpression boundExpression3 = this.MakeNullCheck(syntax, boundLocal, BinaryOperatorKind.NotEqual);
				BoundExpression convertedLeftForNullCoalescingOperator = this.GetConvertedLeftForNullCoalescingOperator(boundLocal, leftConversion, rewrittenResultType);
				BoundExpression boundExpression4 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression3, convertedLeftForNullCoalescingOperator, rewrittenRight, null, rewrittenResultType, false);
				return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression4, rewrittenResultType, false);
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000C5D5C File Offset: 0x000C3F5C
		private bool IsStringConcat(BoundExpression expression)
		{
			if (expression.Kind != BoundKind.Call)
			{
				return false;
			}
			MethodSymbol method = ((BoundCall)expression).Method;
			return method.IsStatic && method.ContainingType.SpecialType == SpecialType.System_String && (method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringString) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringStringString) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringStringStringString) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatObject) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatObjectObject) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatObjectObjectObject) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringArray) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatObjectArray));
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000C5E1C File Offset: 0x000C401C
		private static BoundExpression RemoveIdentityConversions(BoundExpression expression)
		{
			while (expression.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)expression;
				if (boundConversion.ConversionKind != ConversionKind.Identity)
				{
					return expression;
				}
				expression = boundConversion.Operand;
			}
			return expression;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000C5E50 File Offset: 0x000C4050
		private BoundExpression GetConvertedLeftForNullCoalescingOperator(BoundExpression rewrittenLeft, Conversion leftConversion, TypeSymbol rewrittenResultType)
		{
			TypeSymbol type = rewrittenLeft.Type;
			if (!TypeSymbol.Equals(type, rewrittenResultType, TypeCompareKind.ConsiderEverything) && type.IsNullableType())
			{
				TypeSymbol nullableUnderlyingType = type.GetNullableUnderlyingType();
				MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(rewrittenLeft.Syntax, type, SpecialMember.System_Nullable_T_GetValueOrDefault);
				rewrittenLeft = BoundCall.Synthesized(rewrittenLeft.Syntax, rewrittenLeft, methodSymbol);
				if (TypeSymbol.Equals(nullableUnderlyingType, rewrittenResultType, TypeCompareKind.ConsiderEverything))
				{
					return rewrittenLeft;
				}
			}
			return this.MakeConversionNode(rewrittenLeft.Syntax, rewrittenLeft, leftConversion, rewrittenResultType, false, false, null);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000C5EB8 File Offset: 0x000C40B8
		public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			BoundExpression boundExpression = this._dynamicFactory.MakeDynamicConstructorInvocation(node.Syntax, node.Type, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt).ToExpression();
			if (node.InitializerExpressionOpt == null || node.InitializerExpressionOpt.HasErrors)
			{
				return boundExpression;
			}
			return this.MakeExpressionWithInitializer(node.Syntax, boundExpression, node.InitializerExpressionOpt, node.Type);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000C5F30 File Offset: 0x000C4130
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			ImmutableArray<RefKind> argumentRefKindsOpt = node.ArgumentRefKindsOpt;
			ImmutableArray<LocalSymbol> immutableArray2;
			immutableArray = this.MakeArguments(node.Syntax, immutableArray, node.Constructor, node.Expanded, node.ArgsToParamsOpt, ref argumentRefKindsOpt, out immutableArray2, false, ThreeState.Unknown);
			if (this._inExpressionLambda)
			{
				if (!immutableArray2.IsDefaultOrEmpty)
				{
					throw ExceptionUtilities.UnexpectedValue(immutableArray2.Length);
				}
				BoundExpression boundExpression = node.UpdateArgumentsAndInitializer(immutableArray, argumentRefKindsOpt, this.MakeObjectCreationInitializerForExpressionTree(node.InitializerExpressionOpt), node.Constructor.ContainingType);
				if (node.Type.IsInterfaceType())
				{
					boundExpression = this.MakeConversionNode(boundExpression, node.Type, false, false);
				}
				return boundExpression;
			}
			else
			{
				BoundExpression boundExpression = node.UpdateArgumentsAndInitializer(immutableArray, argumentRefKindsOpt, null, node.Constructor.ContainingType);
				if (node.Constructor.IsDefaultValueTypeConstructor())
				{
					boundExpression = new BoundDefaultExpression(boundExpression.Syntax, boundExpression.Type, false);
				}
				if (!immutableArray2.IsDefaultOrEmpty)
				{
					boundExpression = new BoundSequence(node.Syntax, immutableArray2, ImmutableArray<BoundExpression>.Empty, boundExpression, node.Type, false);
				}
				if (node.Type.IsInterfaceType())
				{
					boundExpression = this.MakeConversionNode(boundExpression, node.Type, false, false);
				}
				if (node.InitializerExpressionOpt == null || node.InitializerExpressionOpt.HasErrors)
				{
					return boundExpression;
				}
				return this.MakeExpressionWithInitializer(node.Syntax, boundExpression, node.InitializerExpressionOpt, node.Type);
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000C6080 File Offset: 0x000C4280
		public override BoundNode VisitWithExpression(BoundWithExpression withExpr)
		{
			BoundExpression boundExpression;
			if (withExpr.Type.IsValueType)
			{
				boundExpression = this.VisitExpression(withExpr.Receiver);
			}
			else
			{
				boundExpression = this._factory.Convert(withExpr.Type, this._factory.Call(this.VisitExpression(withExpr.Receiver), withExpr.CloneMethod));
			}
			return this.MakeExpressionWithInitializer(withExpr.Syntax, boundExpression, withExpr.InitializerExpression, withExpr.Type);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000C60F4 File Offset: 0x000C42F4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("initializerExpressionOpt")]
		private BoundObjectInitializerExpressionBase MakeObjectCreationInitializerForExpressionTree(BoundObjectInitializerExpressionBase initializerExpressionOpt)
		{
			if (initializerExpressionOpt != null && !initializerExpressionOpt.HasErrors)
			{
				ImmutableArray<BoundExpression> immutableArray = this.MakeObjectOrCollectionInitializersForExpressionTree(initializerExpressionOpt);
				return LocalRewriter.UpdateInitializers(initializerExpressionOpt, immutableArray);
			}
			return null;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000C6120 File Offset: 0x000C4320
		private BoundExpression MakeExpressionWithInitializer(SyntaxNode syntax, BoundExpression rewrittenExpression, BoundExpression initializerExpression, TypeSymbol type)
		{
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(rewrittenExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			ArrayBuilder<BoundExpression> arrayBuilder = null;
			ArrayBuilder<LocalSymbol> arrayBuilder2 = null;
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			this.AddObjectOrCollectionInitializers(ref arrayBuilder, ref arrayBuilder2, instance, boundLocal, initializerExpression);
			int num = ((arrayBuilder != null) ? arrayBuilder.Count : 0);
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance(1 + num + instance.Count);
			instance2.Add(boundAssignmentOperator);
			if (num > 0)
			{
				instance2.AddRange(arrayBuilder);
				arrayBuilder.Free();
			}
			instance2.AddRange(instance);
			instance.Free();
			ImmutableArray<LocalSymbol> immutableArray;
			if (arrayBuilder2 == null)
			{
				immutableArray = ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol);
			}
			else
			{
				arrayBuilder2.Insert(0, boundLocal.LocalSymbol);
				immutableArray = arrayBuilder2.ToImmutableAndFree();
			}
			return new BoundSequence(syntax, immutableArray, instance2.ToImmutableAndFree(), boundLocal, type, false);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000C61E0 File Offset: 0x000C43E0
		public override BoundNode VisitNewT(BoundNewT node)
		{
			if (this._inExpressionLambda)
			{
				return node.Update(this.MakeObjectCreationInitializerForExpressionTree(node.InitializerExpressionOpt), node.Type);
			}
			BoundExpression boundExpression = this.MakeNewT(node.Syntax, (TypeParameterSymbol)node.Type);
			if (node.InitializerExpressionOpt == null || node.InitializerExpressionOpt.HasErrors)
			{
				return boundExpression;
			}
			return this.MakeExpressionWithInitializer(node.Syntax, boundExpression, node.InitializerExpressionOpt, boundExpression.Type);
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000C6258 File Offset: 0x000C4458
		private BoundExpression MakeNewT(SyntaxNode syntax, TypeParameterSymbol typeParameter)
		{
			MethodSymbol methodSymbol;
			if (!this.TryGetWellKnownTypeMember<MethodSymbol>(syntax, WellKnownMember.System_Activator__CreateInstance_T, out methodSymbol, false, null))
			{
				return new BoundDefaultExpression(syntax, typeParameter, true);
			}
			methodSymbol = methodSymbol.Construct(ImmutableArray.Create<TypeSymbol>(typeParameter));
			return new BoundCall(syntax, null, methodSymbol, ImmutableArray<BoundExpression>.Empty, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, false, false, default(ImmutableArray<int>), default(BitVector), LookupResultKind.Viable, typeParameter, false);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000C62C8 File Offset: 0x000C44C8
		public override BoundNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node)
		{
			SyntaxNode syntax = this._factory.Syntax;
			this._factory.Syntax = node.Syntax;
			MethodSymbol methodSymbol = this._factory.WellKnownMethod(WellKnownMember.System_Guid__ctor);
			BoundExpression boundExpression;
			if (methodSymbol != null)
			{
				boundExpression = this._factory.New(methodSymbol, new BoundExpression[] { this._factory.Literal(node.GuidString) });
			}
			else
			{
				boundExpression = new BoundBadExpression(node.Syntax, LookupResultKind.NotCreatable, ImmutableArray<Symbol>.Empty, ImmutableArray<BoundExpression>.Empty, ErrorTypeSymbol.UnknownResultType);
			}
			MethodSymbol methodSymbol2 = this._factory.WellKnownMethod(WellKnownMember.System_Runtime_InteropServices_Marshal__GetTypeFromCLSID, true);
			if (methodSymbol2 == null)
			{
				methodSymbol2 = this._factory.WellKnownMethod(WellKnownMember.System_Type__GetTypeFromCLSID);
			}
			BoundExpression boundExpression2;
			if (methodSymbol2 != null)
			{
				boundExpression2 = this._factory.Call(null, methodSymbol2, boundExpression);
			}
			else
			{
				boundExpression2 = new BoundBadExpression(node.Syntax, LookupResultKind.OverloadResolutionFailure, ImmutableArray<Symbol>.Empty, ImmutableArray<BoundExpression>.Empty, ErrorTypeSymbol.UnknownResultType);
			}
			MethodSymbol methodSymbol3 = this._factory.WellKnownMethod(WellKnownMember.System_Activator__CreateInstance);
			BoundExpression boundExpression3;
			if (methodSymbol3 != null)
			{
				boundExpression3 = this._factory.Convert(node.Type, this._factory.Call(null, methodSymbol3, boundExpression2));
			}
			else
			{
				boundExpression3 = new BoundBadExpression(node.Syntax, LookupResultKind.OverloadResolutionFailure, ImmutableArray<Symbol>.Empty, ImmutableArray<BoundExpression>.Empty, node.Type);
			}
			this._factory.Syntax = syntax;
			if (node.InitializerExpressionOpt == null || node.InitializerExpressionOpt.HasErrors)
			{
				return boundExpression3;
			}
			return this.MakeExpressionWithInitializer(node.Syntax, boundExpression3, node.InitializerExpressionOpt, node.Type);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000C6434 File Offset: 0x000C4634
		private static BoundObjectInitializerExpressionBase UpdateInitializers(BoundObjectInitializerExpressionBase initializerExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> newInitializers)
		{
			BoundObjectInitializerExpression boundObjectInitializerExpression = initializerExpression as BoundObjectInitializerExpression;
			if (boundObjectInitializerExpression != null)
			{
				return boundObjectInitializerExpression.Update(boundObjectInitializerExpression.Placeholder, newInitializers, initializerExpression.Type);
			}
			BoundCollectionInitializerExpression boundCollectionInitializerExpression = initializerExpression as BoundCollectionInitializerExpression;
			if (boundCollectionInitializerExpression == null)
			{
				throw ExceptionUtilities.UnexpectedValue(initializerExpression.Kind);
			}
			return boundCollectionInitializerExpression.Update(boundCollectionInitializerExpression.Placeholder, newInitializers, initializerExpression.Type);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000C6490 File Offset: 0x000C4690
		private void AddObjectOrCollectionInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<BoundExpression> dynamicSiteInitializers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> result, BoundExpression rewrittenReceiver, BoundExpression initializerExpression)
		{
			BoundObjectInitializerExpression boundObjectInitializerExpression = initializerExpression as BoundObjectInitializerExpression;
			if (boundObjectInitializerExpression != null)
			{
				BoundObjectOrCollectionValuePlaceholder placeholder = boundObjectInitializerExpression.Placeholder;
				this.AddPlaceholderReplacement(placeholder, rewrittenReceiver);
				this.AddObjectInitializers(ref dynamicSiteInitializers, ref temps, result, rewrittenReceiver, boundObjectInitializerExpression.Initializers);
				this.RemovePlaceholderReplacement(placeholder);
				return;
			}
			BoundCollectionInitializerExpression boundCollectionInitializerExpression = initializerExpression as BoundCollectionInitializerExpression;
			if (boundCollectionInitializerExpression == null)
			{
				throw ExceptionUtilities.UnexpectedValue(initializerExpression.Kind);
			}
			BoundObjectOrCollectionValuePlaceholder placeholder2 = boundCollectionInitializerExpression.Placeholder;
			this.AddPlaceholderReplacement(placeholder2, rewrittenReceiver);
			this.AddCollectionInitializers(ref dynamicSiteInitializers, result, rewrittenReceiver, boundCollectionInitializerExpression.Initializers);
			this.RemovePlaceholderReplacement(placeholder2);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000C6518 File Offset: 0x000C4718
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> MakeObjectOrCollectionInitializersForExpressionTree(BoundExpression initializerExpression)
		{
			BoundKind kind = initializerExpression.Kind;
			if (kind == BoundKind.ObjectInitializerExpression)
			{
				return base.VisitList<BoundExpression>(((BoundObjectInitializerExpression)initializerExpression).Initializers);
			}
			if (kind != BoundKind.CollectionInitializerExpression)
			{
				throw ExceptionUtilities.UnexpectedValue(initializerExpression.Kind);
			}
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<BoundExpression> arrayBuilder = null;
			this.AddCollectionInitializers(ref arrayBuilder, instance, null, ((BoundCollectionInitializerExpression)initializerExpression).Initializers);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000C6584 File Offset: 0x000C4784
		private void AddCollectionInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<BoundExpression> dynamicSiteInitializers, ArrayBuilder<BoundExpression> result, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers)
		{
			foreach (BoundExpression boundExpression in initializers)
			{
				BoundExpression boundExpression2;
				if (boundExpression.Kind == BoundKind.CollectionElementInitializer)
				{
					boundExpression2 = this.MakeCollectionInitializer(rewrittenReceiver, (BoundCollectionElementInitializer)boundExpression);
				}
				else
				{
					boundExpression2 = this.MakeDynamicCollectionInitializer(rewrittenReceiver, (BoundDynamicCollectionElementInitializer)boundExpression);
				}
				if (boundExpression2 != null)
				{
					result.Add(boundExpression2);
				}
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000C65E4 File Offset: 0x000C47E4
		private BoundExpression MakeDynamicCollectionInitializer(BoundExpression rewrittenReceiver, BoundDynamicCollectionElementInitializer initializer)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(initializer.Arguments);
			this.EmbedIfNeedTo(rewrittenReceiver, initializer.ApplicableMethods, initializer.Syntax);
			return this._dynamicFactory.MakeDynamicMemberInvocation("Add", rewrittenReceiver, ImmutableArray<TypeWithAnnotations>.Empty, immutableArray, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, true).ToExpression();
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000C6644 File Offset: 0x000C4844
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private BoundExpression MakeCollectionInitializer(BoundExpression rewrittenReceiver, [global::System.Runtime.CompilerServices.Nullable(1)] BoundCollectionElementInitializer initializer)
		{
			MethodSymbol addMethod = initializer.AddMethod;
			SyntaxNode syntax = initializer.Syntax;
			if (this._allowOmissionOfConditionalCalls && addMethod.CallsAreOmitted(initializer.SyntaxTree))
			{
				return null;
			}
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(initializer.Arguments);
			TypeSymbol typeSymbol = this.VisitType(initializer.Type);
			ImmutableArray<RefKind> immutableArray2 = default(ImmutableArray<RefKind>);
			if (addMethod.Parameters[0].RefKind == RefKind.Ref)
			{
				ArrayBuilder<RefKind> instance = ArrayBuilder<RefKind>.GetInstance(addMethod.Parameters.Length, RefKind.None);
				instance[0] = RefKind.Ref;
				immutableArray2 = instance.ToImmutableAndFree();
			}
			ImmutableArray<LocalSymbol> immutableArray3;
			immutableArray = this.MakeArguments(syntax, immutableArray, addMethod, initializer.Expanded, initializer.ArgsToParamsOpt, ref immutableArray2, out immutableArray3, false, ThreeState.True);
			if (initializer.InvokedAsExtensionMethod)
			{
				rewrittenReceiver = null;
			}
			if (this._inExpressionLambda)
			{
				return initializer.Update(addMethod, immutableArray, rewrittenReceiver, false, default(ImmutableArray<int>), default(BitVector), initializer.InvokedAsExtensionMethod, initializer.ResultKind, typeSymbol);
			}
			return this.MakeCall(null, syntax, rewrittenReceiver, addMethod, immutableArray, immutableArray2, initializer.InvokedAsExtensionMethod, initializer.ResultKind, addMethod.ReturnType, immutableArray3);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000C6754 File Offset: 0x000C4954
		private void AddObjectInitializers([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<BoundExpression> dynamicSiteInitializers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> result, BoundExpression rewrittenReceiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers)
		{
			foreach (BoundExpression boundExpression in initializers)
			{
				this.AddObjectInitializer(ref dynamicSiteInitializers, ref temps, result, rewrittenReceiver, (BoundAssignmentOperator)boundExpression);
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000C6790 File Offset: 0x000C4990
		private void AddObjectInitializer([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<BoundExpression> dynamicSiteInitializers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> result, BoundExpression rewrittenReceiver, BoundAssignmentOperator assignment)
		{
			BoundExpression boundExpression = null;
			if (assignment.Left.Kind != BoundKind.PointerElementAccess)
			{
				boundExpression = this.VisitExpression(assignment.Left);
			}
			BoundKind kind = assignment.Right.Kind;
			bool flag = kind == BoundKind.ObjectInitializerExpression || kind == BoundKind.CollectionInitializerExpression;
			BoundKind kind2 = (boundExpression ?? assignment.Left).Kind;
			BoundExpression boundExpression2;
			if (kind2 <= BoundKind.ArrayAccess)
			{
				if (kind2 != BoundKind.PointerElementAccess)
				{
					if (kind2 == BoundKind.ArrayAccess)
					{
						BoundArrayAccess boundArrayAccess = (BoundArrayAccess)boundExpression;
						ImmutableArray<BoundExpression> immutableArray = this.EvaluateSideEffectingArgumentsToTemps(boundArrayAccess.Indices, default(ImmutableArray<RefKind>), result, ref temps);
						boundExpression2 = boundArrayAccess.Update(rewrittenReceiver, immutableArray, boundArrayAccess.Type);
						if (!flag)
						{
							BoundExpression boundExpression3 = this.VisitExpression(assignment.Right);
							result.Add(this.MakeStaticAssignmentOperator(assignment.Syntax, boundExpression2, boundExpression3, false, assignment.Type, false));
							return;
						}
						goto IL_03D5;
					}
				}
				else
				{
					BoundPointerElementAccess boundPointerElementAccess = (BoundPointerElementAccess)assignment.Left;
					BoundExpression boundExpression4 = this.VisitExpression(boundPointerElementAccess.Index);
					if (LocalRewriter.CanChangeValueBetweenReads(boundExpression4, true, false))
					{
						BoundAssignmentOperator boundAssignmentOperator;
						BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression4, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
						boundExpression4 = boundLocal;
						if (temps == null)
						{
							temps = ArrayBuilder<LocalSymbol>.GetInstance();
						}
						temps.Add(boundLocal.LocalSymbol);
						result.Add(boundAssignmentOperator);
					}
					boundExpression2 = this.RewritePointerElementAccess(boundPointerElementAccess, rewrittenReceiver, boundExpression4);
					if (!flag)
					{
						BoundExpression boundExpression5 = this.VisitExpression(assignment.Right);
						result.Add(this.MakeStaticAssignmentOperator(assignment.Syntax, boundExpression2, boundExpression5, false, assignment.Type, false));
						return;
					}
					goto IL_03D5;
				}
			}
			else if (kind2 != BoundKind.ObjectInitializerMember)
			{
				if (kind2 == BoundKind.DynamicObjectInitializerMember)
				{
					if (dynamicSiteInitializers == null)
					{
						dynamicSiteInitializers = ArrayBuilder<BoundExpression>.GetInstance();
					}
					BoundDynamicObjectInitializerMember boundDynamicObjectInitializerMember = (BoundDynamicObjectInitializerMember)boundExpression;
					if (!flag)
					{
						BoundExpression boundExpression6 = this.VisitExpression(assignment.Right);
						LoweredDynamicOperation loweredDynamicOperation = this._dynamicFactory.MakeDynamicSetMember(rewrittenReceiver, boundDynamicObjectInitializerMember.MemberName, boundExpression6, false, false);
						dynamicSiteInitializers.Add(loweredDynamicOperation.SiteInitialization);
						result.Add(loweredDynamicOperation.SiteInvocation);
						return;
					}
					LoweredDynamicOperation loweredDynamicOperation2 = this._dynamicFactory.MakeDynamicGetMember(rewrittenReceiver, boundDynamicObjectInitializerMember.MemberName, false);
					dynamicSiteInitializers.Add(loweredDynamicOperation2.SiteInitialization);
					boundExpression2 = loweredDynamicOperation2.SiteInvocation;
					goto IL_03D5;
				}
			}
			else
			{
				BoundObjectInitializerMember boundObjectInitializerMember = (BoundObjectInitializerMember)boundExpression;
				if (!boundObjectInitializerMember.Arguments.IsDefaultOrEmpty)
				{
					ImmutableArray<BoundExpression> arguments = boundObjectInitializerMember.Arguments;
					Symbol memberSymbol = boundObjectInitializerMember.MemberSymbol;
					ImmutableArray<BoundExpression> immutableArray2 = this.EvaluateSideEffectingArgumentsToTemps(arguments, (memberSymbol != null) ? memberSymbol.GetParameterRefKinds() : default(ImmutableArray<RefKind>), result, ref temps);
					boundObjectInitializerMember = boundObjectInitializerMember.Update(boundObjectInitializerMember.MemberSymbol, immutableArray2, boundObjectInitializerMember.ArgumentNamesOpt, boundObjectInitializerMember.ArgumentRefKindsOpt, boundObjectInitializerMember.Expanded, boundObjectInitializerMember.ArgsToParamsOpt, boundObjectInitializerMember.DefaultArguments, boundObjectInitializerMember.ResultKind, boundObjectInitializerMember.ReceiverType, boundObjectInitializerMember.Type);
				}
				if (boundObjectInitializerMember.MemberSymbol == null && boundObjectInitializerMember.Type.IsDynamic())
				{
					if (dynamicSiteInitializers == null)
					{
						dynamicSiteInitializers = ArrayBuilder<BoundExpression>.GetInstance();
					}
					if (!flag)
					{
						BoundExpression boundExpression7 = this.VisitExpression(assignment.Right);
						LoweredDynamicOperation loweredDynamicOperation3 = this._dynamicFactory.MakeDynamicSetIndex(rewrittenReceiver, boundObjectInitializerMember.Arguments, boundObjectInitializerMember.ArgumentNamesOpt, boundObjectInitializerMember.ArgumentRefKindsOpt, boundExpression7, false, false);
						dynamicSiteInitializers.Add(loweredDynamicOperation3.SiteInitialization);
						result.Add(loweredDynamicOperation3.SiteInvocation);
						return;
					}
					LoweredDynamicOperation loweredDynamicOperation4 = this._dynamicFactory.MakeDynamicGetIndex(rewrittenReceiver, boundObjectInitializerMember.Arguments, boundObjectInitializerMember.ArgumentNamesOpt, boundObjectInitializerMember.ArgumentRefKindsOpt);
					dynamicSiteInitializers.Add(loweredDynamicOperation4.SiteInitialization);
					boundExpression2 = loweredDynamicOperation4.SiteInvocation;
					goto IL_03D5;
				}
				else
				{
					boundExpression2 = this.MakeObjectInitializerMemberAccess(rewrittenReceiver, boundObjectInitializerMember, flag);
					if (!flag)
					{
						BoundExpression boundExpression8 = this.VisitExpression(assignment.Right);
						result.Add(this.MakeStaticAssignmentOperator(assignment.Syntax, boundExpression2, boundExpression8, false, assignment.Type, false));
						return;
					}
					goto IL_03D5;
				}
			}
			throw ExceptionUtilities.UnexpectedValue((boundExpression ?? assignment.Left).Kind);
			IL_03D5:
			this.AddObjectOrCollectionInitializers(ref dynamicSiteInitializers, ref temps, result, boundExpression2, assignment.Right);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000C6B84 File Offset: 0x000C4D84
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<BoundExpression> EvaluateSideEffectingArgumentsToTemps([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args, ImmutableArray<RefKind> paramRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(1)] ArrayBuilder<BoundExpression> sideeffects, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ref ArrayBuilder<LocalSymbol> temps)
		{
			ArrayBuilder<BoundExpression> arrayBuilder = null;
			for (int i = 0; i < args.Length; i++)
			{
				BoundExpression boundExpression = args[i];
				if (LocalRewriter.CanChangeValueBetweenReads(boundExpression, true, false))
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<BoundExpression>.GetInstance(args.Length);
						arrayBuilder.AddRange(args, i);
					}
					RefKind refKind = paramRefKindsOpt.RefKinds(i);
					BoundAssignmentOperator boundAssignmentOperator;
					BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, refKind, SynthesizedLocalKind.LoweringTemp, null);
					arrayBuilder.Add(boundLocal);
					if (temps == null)
					{
						temps = ArrayBuilder<LocalSymbol>.GetInstance();
					}
					temps.Add(boundLocal.LocalSymbol);
					sideeffects.Add(boundAssignmentOperator);
				}
				else if (arrayBuilder != null)
				{
					arrayBuilder.Add(boundExpression);
				}
			}
			if (arrayBuilder == null)
			{
				return args;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000C6C38 File Offset: 0x000C4E38
		private BoundExpression MakeObjectInitializerMemberAccess(BoundExpression rewrittenReceiver, BoundObjectInitializerMember rewrittenLeft, bool isRhsNestedInitializer)
		{
			Symbol memberSymbol = rewrittenLeft.MemberSymbol;
			SymbolKind kind = memberSymbol.Kind;
			if (kind == SymbolKind.Event)
			{
				EventSymbol eventSymbol = (EventSymbol)memberSymbol;
				return this.MakeEventAccess(rewrittenLeft.Syntax, rewrittenReceiver, eventSymbol, null, rewrittenLeft.ResultKind, eventSymbol.Type);
			}
			if (kind == SymbolKind.Field)
			{
				FieldSymbol fieldSymbol = (FieldSymbol)memberSymbol;
				return this.MakeFieldAccess(rewrittenLeft.Syntax, rewrittenReceiver, fieldSymbol, null, rewrittenLeft.ResultKind, fieldSymbol.Type, null);
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(memberSymbol.Kind);
			}
			PropertySymbol propertySymbol = (PropertySymbol)memberSymbol;
			if (!rewrittenLeft.Arguments.IsEmpty || propertySymbol.IsIndexedProperty)
			{
				return this.MakeIndexerAccess(rewrittenLeft.Syntax, rewrittenReceiver, propertySymbol, rewrittenLeft.Arguments, rewrittenLeft.ArgumentNamesOpt, rewrittenLeft.ArgumentRefKindsOpt, rewrittenLeft.Expanded, rewrittenLeft.ArgsToParamsOpt, rewrittenLeft.DefaultArguments, propertySymbol.Type, null, !isRhsNestedInitializer);
			}
			return this.MakePropertyAccess(rewrittenLeft.Syntax, rewrittenReceiver, propertySymbol, rewrittenLeft.ResultKind, propertySymbol.Type, !isRhsNestedInitializer, null);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000C6D43 File Offset: 0x000C4F43
		public override BoundNode VisitSwitchStatement(BoundSwitchStatement node)
		{
			return LocalRewriter.SwitchStatementLocalRewriter.Rewrite(this, node);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000C6D4C File Offset: 0x000C4F4C
		public override BoundNode VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			BoundExpression boundExpression = this.LowerReceiverOfPointerElementAccess(node.Expression);
			BoundExpression boundExpression2 = this.VisitExpression(node.Index);
			return this.RewritePointerElementAccess(node, boundExpression, boundExpression2);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000C6D7C File Offset: 0x000C4F7C
		private BoundExpression LowerReceiverOfPointerElementAccess(BoundExpression receiver)
		{
			BoundFieldAccess boundFieldAccess = receiver as BoundFieldAccess;
			if (boundFieldAccess != null && boundFieldAccess.FieldSymbol.IsFixedSizeBuffer)
			{
				BoundExpression boundExpression = this.VisitExpression(boundFieldAccess.ReceiverOpt);
				boundFieldAccess = boundFieldAccess.Update(boundExpression, boundFieldAccess.FieldSymbol, boundFieldAccess.ConstantValueOpt, boundFieldAccess.ResultKind, boundFieldAccess.Type);
				return new BoundAddressOfOperator(receiver.Syntax, boundFieldAccess, true, boundFieldAccess.Type, false);
			}
			return this.VisitExpression(receiver);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000C6DEC File Offset: 0x000C4FEC
		private BoundExpression RewritePointerElementAccess(BoundPointerElementAccess node, BoundExpression rewrittenExpression, BoundExpression rewrittenIndex)
		{
			if (rewrittenIndex.IsDefaultValue())
			{
				return new BoundPointerIndirectionOperator(node.Syntax, rewrittenExpression, node.Type, false);
			}
			BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.Addition;
			switch (rewrittenIndex.Type.SpecialType)
			{
			case SpecialType.System_Int32:
				binaryOperatorKind |= BinaryOperatorKind.PointerAndIntAddition;
				break;
			case SpecialType.System_UInt32:
				binaryOperatorKind |= BinaryOperatorKind.PointerAndUIntAddition;
				break;
			case SpecialType.System_Int64:
				binaryOperatorKind |= BinaryOperatorKind.PointerAndLongAddition;
				break;
			case SpecialType.System_UInt64:
				binaryOperatorKind |= BinaryOperatorKind.PointerAndULongAddition;
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(rewrittenIndex.Type.SpecialType);
			}
			if (node.Checked)
			{
				binaryOperatorKind |= BinaryOperatorKind.Checked;
			}
			return new BoundPointerIndirectionOperator(node.Syntax, this.MakeBinaryOperator(node.Syntax, binaryOperatorKind, rewrittenExpression, rewrittenIndex, rewrittenExpression.Type, null, true, false, null), node.Type, false);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000C6EBC File Offset: 0x000C50BC
		public override BoundNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node)
		{
			ImplicitNamedTypeSymbol implicitNamedTypeSymbol = (ImplicitNamedTypeSymbol)node.Type;
			SyntaxNode syntax = node.Syntax;
			FieldSymbol orMakeField = this._previousSubmissionFields.GetOrMakeField(implicitNamedTypeSymbol);
			BoundThisReference boundThisReference = new BoundThisReference(syntax, this._factory.CurrentType);
			return new BoundFieldAccess(syntax, boundThisReference, orMakeField, null, false);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000C6F03 File Offset: 0x000C5103
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			return this.VisitPropertyAccess(node, false);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000C6F10 File Offset: 0x000C5110
		private BoundExpression VisitPropertyAccess(BoundPropertyAccess node, bool isLeftOfAssignment)
		{
			BoundExpression boundExpression = this.VisitExpression(node.ReceiverOpt);
			return this.MakePropertyAccess(node.Syntax, boundExpression, node.PropertySymbol, node.ResultKind, node.Type, isLeftOfAssignment, node);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000C6F4C File Offset: 0x000C514C
		private BoundExpression MakePropertyAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiverOpt, PropertySymbol propertySymbol, LookupResultKind resultKind, TypeSymbol type, bool isLeftOfAssignment, [global::System.Runtime.CompilerServices.Nullable(2)] BoundPropertyAccess oldNodeOpt = null)
		{
			if (rewrittenReceiverOpt != null)
			{
				TypeSymbol type2 = rewrittenReceiverOpt.Type;
				if (type2 != null && type2.TypeKind == TypeKind.Array && !isLeftOfAssignment && ((ArrayTypeSymbol)rewrittenReceiverOpt.Type).IsSZArray && (propertySymbol == this._compilation.GetSpecialTypeMember(SpecialMember.System_Array__Length) || (!this._inExpressionLambda && propertySymbol == this._compilation.GetSpecialTypeMember(SpecialMember.System_Array__LongLength))))
				{
					return new BoundArrayLength(syntax, rewrittenReceiverOpt, type, false);
				}
			}
			if (!isLeftOfAssignment || propertySymbol.RefKind != RefKind.None)
			{
				return this.MakePropertyGetAccess(syntax, rewrittenReceiverOpt, propertySymbol, oldNodeOpt);
			}
			if (oldNodeOpt == null)
			{
				return new BoundPropertyAccess(syntax, rewrittenReceiverOpt, propertySymbol, resultKind, type, false);
			}
			return oldNodeOpt.Update(rewrittenReceiverOpt, propertySymbol, resultKind, type);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000C6FEF File Offset: 0x000C51EF
		private BoundExpression MakePropertyGetAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, PropertySymbol property, [global::System.Runtime.CompilerServices.Nullable(2)] BoundPropertyAccess oldNodeOpt)
		{
			return this.MakePropertyGetAccess(syntax, rewrittenReceiver, property, ImmutableArray<BoundExpression>.Empty, null, oldNodeOpt);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000C7004 File Offset: 0x000C5204
		private BoundExpression MakePropertyGetAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression rewrittenReceiver, PropertySymbol property, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getMethodOpt = null, [global::System.Runtime.CompilerServices.Nullable(2)] BoundPropertyAccess oldNodeOpt = null)
		{
			if (!this._inExpressionLambda || !rewrittenArguments.IsEmpty)
			{
				MethodSymbol methodSymbol = getMethodOpt ?? property.GetOwnOrInheritedGetMethod();
				return BoundCall.Synthesized(syntax, rewrittenReceiver, methodSymbol, rewrittenArguments);
			}
			if (oldNodeOpt == null)
			{
				return new BoundPropertyAccess(syntax, rewrittenReceiver, property, LookupResultKind.Viable, property.Type, false);
			}
			return oldNodeOpt.Update(rewrittenReceiver, property, LookupResultKind.Viable, property.Type);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000C7061 File Offset: 0x000C5261
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			return this.VisitExpression(node.Value);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000C706F File Offset: 0x000C526F
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			return this.VisitExpression(node.Value);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000C7080 File Offset: 0x000C5280
		public override BoundNode VisitRangeExpression(BoundRangeExpression node)
		{
			LocalRewriter.<>c__DisplayClass342_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.needLifting = false;
			SyntheticBoundNodeFactory factory = this._factory;
			BoundExpression boundExpression = node.LeftOperandOpt;
			if (boundExpression != null)
			{
				boundExpression = this.<VisitRangeExpression>g__tryOptimizeOperand|342_0(boundExpression, ref CS$<>8__locals1);
			}
			BoundExpression boundExpression2 = node.RightOperandOpt;
			if (boundExpression2 != null)
			{
				boundExpression2 = this.<VisitRangeExpression>g__tryOptimizeOperand|342_0(boundExpression2, ref CS$<>8__locals1);
			}
			if (CS$<>8__locals1.needLifting)
			{
				return this.LiftRangeExpression(node, boundExpression, boundExpression2);
			}
			BoundExpression boundExpression3 = this.MakeRangeExpression(node.MethodOpt, boundExpression, boundExpression2);
			if (!node.Type.IsNullableType())
			{
				return boundExpression3;
			}
			MethodSymbol methodSymbol;
			if (!this.TryGetNullableMethod(node.Syntax, node.Type, SpecialMember.System_Nullable_T__ctor, out methodSymbol))
			{
				return LocalRewriter.BadExpression(node.Syntax, node.Type, node);
			}
			return new BoundObjectCreationExpression(node.Syntax, methodSymbol, new BoundExpression[] { boundExpression3 });
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000C7140 File Offset: 0x000C5340
		private BoundExpression LiftRangeExpression(BoundRangeExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression left, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression right)
		{
			LocalRewriter.<>c__DisplayClass343_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.node = node;
			CS$<>8__locals1.sideeffects = ArrayBuilder<BoundExpression>.GetInstance();
			CS$<>8__locals1.locals = ArrayBuilder<LocalSymbol>.GetInstance();
			CS$<>8__locals1.condition = null;
			left = this.<LiftRangeExpression>g__getIndexFromPossibleNullable|343_0(left, ref CS$<>8__locals1);
			right = this.<LiftRangeExpression>g__getIndexFromPossibleNullable|343_0(right, ref CS$<>8__locals1);
			BoundExpression boundExpression = this.MakeRangeExpression(CS$<>8__locals1.node.MethodOpt, left, right);
			MethodSymbol methodSymbol;
			if (!this.TryGetNullableMethod(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.node.Type, SpecialMember.System_Nullable_T__ctor, out methodSymbol))
			{
				return LocalRewriter.BadExpression(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.node.Type, CS$<>8__locals1.node);
			}
			BoundExpression boundExpression2 = new BoundObjectCreationExpression(CS$<>8__locals1.node.Syntax, methodSymbol, new BoundExpression[] { boundExpression });
			BoundExpression boundExpression3 = new BoundDefaultExpression(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.node.Type, false);
			BoundExpression boundExpression4 = LocalRewriter.RewriteConditionalOperator(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.condition, boundExpression2, boundExpression3, null, CS$<>8__locals1.node.Type, false);
			return new BoundSequence(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.locals.ToImmutableAndFree(), CS$<>8__locals1.sideeffects.ToImmutableAndFree(), boundExpression4, CS$<>8__locals1.node.Type, false);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000C7284 File Offset: 0x000C5484
		private BoundExpression MakeRangeExpression(MethodSymbol constructionMethod, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression left, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression right)
		{
			LocalRewriter.<>c__DisplayClass344_0 CS$<>8__locals1;
			CS$<>8__locals1.F = this._factory;
			MethodKind methodKind = constructionMethod.MethodKind;
			if (methodKind == MethodKind.Constructor)
			{
				left = left ?? LocalRewriter.<MakeRangeExpression>g__newIndexZero|344_0(false, ref CS$<>8__locals1);
				right = right ?? LocalRewriter.<MakeRangeExpression>g__newIndexZero|344_0(true, ref CS$<>8__locals1);
				return CS$<>8__locals1.F.New(constructionMethod, ImmutableArray.Create<BoundExpression>(left, right));
			}
			if (methodKind == MethodKind.Ordinary)
			{
				BoundExpression boundExpression = left ?? right;
				return CS$<>8__locals1.F.StaticCall(constructionMethod, ImmutableArray.Create<BoundExpression>(boundExpression));
			}
			if (methodKind != MethodKind.PropertyGet)
			{
				throw ExceptionUtilities.UnexpectedValue(constructionMethod.MethodKind);
			}
			return CS$<>8__locals1.F.StaticCall(constructionMethod, ImmutableArray<BoundExpression>.Empty);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000C7324 File Offset: 0x000C5524
		public override BoundNode VisitReturnStatement(BoundReturnStatement node)
		{
			BoundStatement boundStatement = (BoundStatement)base.VisitReturnStatement(node);
			if (this.Instrument)
			{
				if (node.WasCompilerGenerated)
				{
					bool flag;
					if (node.ExpressionOpt == null)
					{
						if (node.Syntax.Kind() == SyntaxKind.Block)
						{
							MethodSymbol currentFunction = this._factory.CurrentFunction;
							flag = currentFunction != null && !currentFunction.IsAsync;
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = this.IsLambdaOrExpressionBodiedMember;
					}
					if (!flag)
					{
						return boundStatement;
					}
				}
				boundStatement = this._instrumenter.InstrumentReturnStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000C73A0 File Offset: 0x000C55A0
		private bool IsLambdaOrExpressionBodiedMember
		{
			get
			{
				MethodSymbol currentFunction = this._factory.CurrentFunction;
				if (currentFunction is LambdaSymbol)
				{
					return true;
				}
				SourceMemberMethodSymbol sourceMemberMethodSymbol = currentFunction as SourceMemberMethodSymbol;
				if (sourceMemberMethodSymbol == null)
				{
					LocalFunctionSymbol localFunctionSymbol = currentFunction as LocalFunctionSymbol;
					return localFunctionSymbol != null && localFunctionSymbol.IsExpressionBodied;
				}
				return sourceMemberMethodSymbol.IsExpressionBodied;
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x000C73E4 File Offset: 0x000C55E4
		public override BoundNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression stackAllocNode)
		{
			return this.VisitStackAllocArrayCreationBase(stackAllocNode);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000C73ED File Offset: 0x000C55ED
		public override BoundNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation stackAllocNode)
		{
			return this.VisitStackAllocArrayCreationBase(stackAllocNode);
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000C73F8 File Offset: 0x000C55F8
		private BoundNode VisitStackAllocArrayCreationBase(BoundStackAllocArrayCreationBase stackAllocNode)
		{
			BoundExpression boundExpression = this.VisitExpression(stackAllocNode.Count);
			TypeSymbol type = stackAllocNode.Type;
			ConstantValue constantValue = boundExpression.ConstantValue;
			if (constantValue != null && constantValue.Int32Value == 0)
			{
				return this._factory.Default(type);
			}
			TypeSymbol elementType = stackAllocNode.ElementType;
			BoundArrayInitialization boundArrayInitialization = stackAllocNode.InitializerOpt;
			if (boundArrayInitialization != null)
			{
				boundArrayInitialization = boundArrayInitialization.Update(base.VisitList<BoundExpression>(boundArrayInitialization.Initializers));
			}
			if (type.IsPointerType())
			{
				BoundExpression boundExpression2 = this.RewriteStackAllocCountToSize(boundExpression, elementType);
				return new BoundConvertedStackAllocExpression(stackAllocNode.Syntax, elementType, boundExpression2, boundArrayInitialization, type, false);
			}
			if (TypeSymbol.Equals(type.OriginalDefinition, this._compilation.GetWellKnownType(WellKnownType.System_Span_T), TypeCompareKind.ConsiderEverything))
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type;
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
				ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
				BoundExpression boundExpression3 = this.CaptureExpressionInTempIfNeeded(boundExpression, instance, instance2, SynthesizedLocalKind.Spill);
				BoundExpression boundExpression4 = this.RewriteStackAllocCountToSize(boundExpression3, elementType);
				stackAllocNode = new BoundConvertedStackAllocExpression(stackAllocNode.Syntax, elementType, boundExpression4, boundArrayInitialization, this._compilation.CreatePointerTypeSymbol(elementType, NullableAnnotation.Oblivious), false);
				MethodSymbol methodSymbol;
				BoundExpression boundExpression5;
				if (this.TryGetWellKnownTypeMember<MethodSymbol>(stackAllocNode.Syntax, WellKnownMember.System_Span_T__ctor, out methodSymbol, false, null))
				{
					boundExpression5 = this._factory.New((MethodSymbol)methodSymbol.SymbolAsMember(namedTypeSymbol), new BoundExpression[] { stackAllocNode, boundExpression3 });
				}
				else
				{
					boundExpression5 = new BoundBadExpression(stackAllocNode.Syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray<BoundExpression>.Empty, ErrorTypeSymbol.UnknownResultType);
				}
				this._needsSpilling = true;
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression5, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, stackAllocNode.Syntax);
				instance.Add(boundAssignmentOperator);
				instance2.Add(boundLocal.LocalSymbol);
				return new BoundSpillSequence(stackAllocNode.Syntax, instance2.ToImmutableAndFree(), instance.ToImmutableAndFree(), boundLocal, namedTypeSymbol, false);
			}
			throw ExceptionUtilities.UnexpectedValue(type);
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000C75B4 File Offset: 0x000C57B4
		private BoundExpression RewriteStackAllocCountToSize(BoundExpression countExpression, TypeSymbol elementType)
		{
			TypeSymbol typeSymbol = this._factory.SpecialType(SpecialType.System_UInt32);
			TypeSymbol typeSymbol2 = this._factory.SpecialType(SpecialType.System_UIntPtr);
			BoundExpression boundExpression = this._factory.Sizeof(elementType);
			ConstantValue constantValue = boundExpression.ConstantValue;
			if (constantValue != null)
			{
				int int32Value = constantValue.Int32Value;
				ConstantValue constantValue2 = countExpression.ConstantValue;
				if (constantValue2 != null)
				{
					long num = (long)((ulong)constantValue2.Int32Value * (ulong)((long)int32Value));
					if (num < (long)((ulong)(-1)))
					{
						return this._factory.Convert(typeSymbol2, this._factory.Literal((uint)num), Conversion.IntegerToPointer, false);
					}
				}
			}
			BoundExpression boundExpression2 = this._factory.Convert(typeSymbol, countExpression, Conversion.ExplicitNumeric, false);
			boundExpression2 = this._factory.Convert(typeSymbol2, boundExpression2, Conversion.IntegerToPointer, false);
			if (constantValue != null && constantValue.Int32Value == 1)
			{
				return boundExpression2;
			}
			BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.UInt | BinaryOperatorKind.Multiplication | BinaryOperatorKind.Checked;
			return this._factory.Binary(binaryOperatorKind, typeSymbol2, boundExpression2, boundExpression);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000C769C File Offset: 0x000C589C
		private BoundExpression RewriteStringConcatenation(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type)
		{
			if (this._inExpressionLambda)
			{
				return this.RewriteStringConcatInExpressionLambda(syntax, operatorKind, loweredLeft, loweredRight, type);
			}
			loweredLeft = this.ConvertConcatExprToString(syntax, loweredLeft);
			loweredRight = this.ConvertConcatExprToString(syntax, loweredRight);
			BoundExpression boundExpression = this.TryFoldTwoConcatOperands(syntax, loweredLeft, loweredRight);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			this.FlattenConcatArg(loweredLeft, instance);
			this.FlattenConcatArg(loweredRight, instance2);
			if (instance.Any() && instance2.Any())
			{
				boundExpression = this.TryFoldTwoConcatOperands(syntax, instance.Last(), instance2.First());
				if (boundExpression != null)
				{
					instance2[0] = boundExpression;
					instance.RemoveLast();
				}
			}
			instance.AddRange(instance2);
			instance2.Free();
			BoundExpression boundExpression2;
			switch (instance.Count)
			{
			case 0:
				boundExpression2 = this._factory.StringLiteral(string.Empty);
				break;
			case 1:
				boundExpression2 = instance[0];
				break;
			case 2:
			{
				BoundExpression boundExpression3 = instance[0];
				BoundExpression boundExpression4 = instance[1];
				boundExpression2 = this.RewriteStringConcatenationTwoExprs(syntax, boundExpression3, boundExpression4);
				break;
			}
			case 3:
			{
				BoundExpression boundExpression5 = instance[0];
				BoundExpression boundExpression6 = instance[1];
				BoundExpression boundExpression7 = instance[2];
				boundExpression2 = this.RewriteStringConcatenationThreeExprs(syntax, boundExpression5, boundExpression6, boundExpression7);
				break;
			}
			case 4:
			{
				BoundExpression boundExpression8 = instance[0];
				BoundExpression boundExpression9 = instance[1];
				BoundExpression boundExpression10 = instance[2];
				BoundExpression boundExpression11 = instance[3];
				boundExpression2 = this.RewriteStringConcatenationFourExprs(syntax, boundExpression8, boundExpression9, boundExpression10, boundExpression11);
				break;
			}
			default:
				boundExpression2 = this.RewriteStringConcatenationManyExprs(syntax, instance.ToImmutable());
				break;
			}
			instance.Free();
			return boundExpression2;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000C7824 File Offset: 0x000C5A24
		private void FlattenConcatArg(BoundExpression lowered, ArrayBuilder<BoundExpression> flattened)
		{
			ImmutableArray<BoundExpression> immutableArray;
			if (this.TryExtractStringConcatArgs(lowered, out immutableArray))
			{
				flattened.AddRange(immutableArray);
				return;
			}
			flattened.Add(lowered);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000C784C File Offset: 0x000C5A4C
		private bool TryExtractStringConcatArgs(BoundExpression lowered, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<BoundExpression> arguments)
		{
			BoundKind kind = lowered.Kind;
			if (kind != BoundKind.NullCoalescingOperator)
			{
				if (kind == BoundKind.Call)
				{
					BoundCall boundCall = (BoundCall)lowered;
					MethodSymbol method = boundCall.Method;
					if (method.IsStatic && method.ContainingType.SpecialType == SpecialType.System_String)
					{
						if (method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringString) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringStringString) || method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringStringStringString))
						{
							arguments = boundCall.Arguments;
							return true;
						}
						if (method == this._compilation.GetSpecialTypeMember(SpecialMember.System_String__ConcatStringArray))
						{
							BoundArrayCreation boundArrayCreation = boundCall.Arguments[0] as BoundArrayCreation;
							if (boundArrayCreation != null)
							{
								BoundArrayInitialization initializerOpt = boundArrayCreation.InitializerOpt;
								if (initializerOpt != null)
								{
									arguments = initializerOpt.Initializers;
									return true;
								}
							}
						}
					}
				}
			}
			else
			{
				BoundNullCoalescingOperator boundNullCoalescingOperator = (BoundNullCoalescingOperator)lowered;
				if (boundNullCoalescingOperator.LeftConversion.IsIdentity)
				{
					ConstantValue constantValue = boundNullCoalescingOperator.RightOperand.ConstantValue;
					if (constantValue != null && constantValue.IsString && constantValue.StringValue.Length == 0)
					{
						arguments = ImmutableArray.Create<BoundExpression>(boundNullCoalescingOperator.LeftOperand);
						return true;
					}
				}
			}
			arguments = default(ImmutableArray<BoundExpression>);
			return false;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000C7984 File Offset: 0x000C5B84
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression TryFoldTwoConcatOperands(SyntaxNode syntax, BoundExpression loweredLeft, BoundExpression loweredRight)
		{
			ConstantValue constantValue = loweredLeft.ConstantValue;
			ConstantValue constantValue2 = loweredRight.ConstantValue;
			if (constantValue != null && constantValue2 != null)
			{
				ConstantValue constantValue3 = LocalRewriter.TryFoldTwoConcatConsts(constantValue, constantValue2);
				if (constantValue3 != null)
				{
					return this._factory.StringLiteral(constantValue3);
				}
			}
			if (LocalRewriter.IsNullOrEmptyStringConstant(loweredLeft))
			{
				if (LocalRewriter.IsNullOrEmptyStringConstant(loweredRight))
				{
					return this._factory.Literal(string.Empty);
				}
				return this.RewriteStringConcatenationOneExpr(syntax, loweredRight);
			}
			else
			{
				if (LocalRewriter.IsNullOrEmptyStringConstant(loweredRight))
				{
					return this.RewriteStringConcatenationOneExpr(syntax, loweredLeft);
				}
				return null;
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000C7A0B File Offset: 0x000C5C0B
		private static bool IsNullOrEmptyStringConstant(BoundExpression operand)
		{
			return (operand.ConstantValue != null && string.IsNullOrEmpty(operand.ConstantValue.StringValue)) || operand.IsDefaultValue();
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000C7A38 File Offset: 0x000C5C38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static ConstantValue TryFoldTwoConcatConsts(ConstantValue leftConst, ConstantValue rightConst)
		{
			string stringValue = leftConst.StringValue;
			string stringValue2 = rightConst.StringValue;
			if (!leftConst.IsDefaultValue && !rightConst.IsDefaultValue && stringValue.Length + stringValue2.Length < 0)
			{
				return null;
			}
			return ConstantValue.Create(stringValue + stringValue2);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000C7A84 File Offset: 0x000C5C84
		private BoundExpression RewriteStringConcatenationOneExpr(SyntaxNode syntax, BoundExpression loweredOperand)
		{
			ImmutableArray<BoundExpression> immutableArray;
			if (this.TryExtractStringConcatArgs(loweredOperand, out immutableArray))
			{
				return loweredOperand;
			}
			return this._factory.Coalesce(loweredOperand, this._factory.Literal(""));
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000C7ABC File Offset: 0x000C5CBC
		private BoundExpression RewriteStringConcatenationTwoExprs(SyntaxNode syntax, BoundExpression loweredLeft, BoundExpression loweredRight)
		{
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, SpecialMember.System_String__ConcatStringString);
			return BoundCall.Synthesized(syntax, null, methodSymbol, loweredLeft, loweredRight);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000C7ADC File Offset: 0x000C5CDC
		private BoundExpression RewriteStringConcatenationThreeExprs(SyntaxNode syntax, BoundExpression loweredFirst, BoundExpression loweredSecond, BoundExpression loweredThird)
		{
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, SpecialMember.System_String__ConcatStringStringString);
			return BoundCall.Synthesized(syntax, null, methodSymbol, ImmutableArray.Create<BoundExpression>(loweredFirst, loweredSecond, loweredThird));
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000C7B04 File Offset: 0x000C5D04
		private BoundExpression RewriteStringConcatenationFourExprs(SyntaxNode syntax, BoundExpression loweredFirst, BoundExpression loweredSecond, BoundExpression loweredThird, BoundExpression loweredFourth)
		{
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, SpecialMember.System_String__ConcatStringStringStringString);
			return BoundCall.Synthesized(syntax, null, methodSymbol, ImmutableArray.Create<BoundExpression>(loweredFirst, loweredSecond, loweredThird, loweredFourth));
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000C7B30 File Offset: 0x000C5D30
		private BoundExpression RewriteStringConcatenationManyExprs(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> loweredArgs)
		{
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, SpecialMember.System_String__ConcatStringArray);
			BoundExpression boundExpression = this._factory.ArrayOrEmpty(this._factory.SpecialType(SpecialType.System_String), loweredArgs);
			return BoundCall.Synthesized(syntax, null, methodSymbol, boundExpression);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000C7B6C File Offset: 0x000C5D6C
		private BoundExpression RewriteStringConcatInExpressionLambda(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression loweredLeft, BoundExpression loweredRight, TypeSymbol type)
		{
			SpecialMember specialMember = ((operatorKind == BinaryOperatorKind.StringConcatenation) ? SpecialMember.System_String__ConcatStringString : SpecialMember.System_String__ConcatObjectObject);
			MethodSymbol methodSymbol = this.UnsafeGetSpecialTypeMethod(syntax, specialMember);
			return new BoundBinaryOperator(syntax, operatorKind, null, methodSymbol, LookupResultKind.Empty, loweredLeft, loweredRight, type, false);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000C7BA0 File Offset: 0x000C5DA0
		private BoundExpression ConvertConcatExprToString(SyntaxNode syntax, BoundExpression expr)
		{
			LocalRewriter.<>c__DisplayClass364_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.syntax = syntax;
			CS$<>8__locals1.expr = expr;
			if (CS$<>8__locals1.expr.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)CS$<>8__locals1.expr;
				if (boundConversion.ConversionKind == ConversionKind.Boxing)
				{
					CS$<>8__locals1.expr = boundConversion.Operand;
				}
			}
			if (CS$<>8__locals1.expr != null)
			{
				ConstantValue constantValue = CS$<>8__locals1.expr.ConstantValue;
				if (constantValue != null)
				{
					if (constantValue.SpecialType == SpecialType.System_Char)
					{
						return this._factory.StringLiteral(constantValue.CharValue.ToString());
					}
					if (constantValue.IsNull)
					{
						return CS$<>8__locals1.expr;
					}
				}
			}
			if (CS$<>8__locals1.expr.Type.IsStringType())
			{
				return CS$<>8__locals1.expr;
			}
			CS$<>8__locals1.objectToStringMethod = this.UnsafeGetSpecialTypeMethod(CS$<>8__locals1.syntax, SpecialMember.System_Object__ToString);
			MethodSymbol methodSymbol = null;
			if (CS$<>8__locals1.expr.Type.IsValueType && !CS$<>8__locals1.expr.Type.IsTypeParameter())
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)CS$<>8__locals1.expr.Type;
				foreach (Symbol symbol in namedTypeSymbol.GetMembers(CS$<>8__locals1.objectToStringMethod.Name))
				{
					MethodSymbol methodSymbol2 = symbol as MethodSymbol;
					if (methodSymbol2 != null && methodSymbol2.GetLeastOverriddenMethod(namedTypeSymbol) == CS$<>8__locals1.objectToStringMethod)
					{
						methodSymbol = methodSymbol2;
						break;
					}
				}
			}
			if (methodSymbol != null && CS$<>8__locals1.expr.Type.SpecialType != SpecialType.None && !LocalRewriter.<ConvertConcatExprToString>g__isFieldOfMarshalByRef|364_1(CS$<>8__locals1.expr, this._compilation))
			{
				return BoundCall.Synthesized(CS$<>8__locals1.expr.Syntax, CS$<>8__locals1.expr, methodSymbol);
			}
			bool flag = CS$<>8__locals1.expr.Type.IsReferenceType || CS$<>8__locals1.expr.ConstantValue != null || (methodSymbol == null && !CS$<>8__locals1.expr.Type.IsTypeParameter()) || (methodSymbol != null && methodSymbol.IsEffectivelyReadOnly);
			if (CS$<>8__locals1.expr.Type.IsValueType)
			{
				if (!flag)
				{
					CS$<>8__locals1.expr = new BoundPassByCopy(CS$<>8__locals1.expr.Syntax, CS$<>8__locals1.expr, CS$<>8__locals1.expr.Type, false);
				}
				return BoundCall.Synthesized(CS$<>8__locals1.expr.Syntax, CS$<>8__locals1.expr, CS$<>8__locals1.objectToStringMethod);
			}
			if (flag)
			{
				return this.<ConvertConcatExprToString>g__makeConditionalAccess|364_0(CS$<>8__locals1.expr, ref CS$<>8__locals1);
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(CS$<>8__locals1.expr, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			return this._factory.Sequence(ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), this.<ConvertConcatExprToString>g__makeConditionalAccess|364_0(boundLocal, ref CS$<>8__locals1));
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000C7E40 File Offset: 0x000C6040
		private BoundExpression RewriteInterpolatedStringConversion(BoundConversion conversion)
		{
			BoundExpression boundExpression;
			ArrayBuilder<BoundExpression> arrayBuilder;
			this.MakeInterpolatedStringFormat((BoundInterpolatedString)conversion.Operand, out boundExpression, out arrayBuilder);
			arrayBuilder.Insert(0, boundExpression);
			NamedTypeSymbol namedTypeSymbol = this._factory.WellKnownType(WellKnownType.System_Runtime_CompilerServices_FormattableStringFactory);
			BoundExpression boundExpression2 = this._factory.StaticCall(namedTypeSymbol, "Create", arrayBuilder.ToImmutableAndFree(), false);
			if (!boundExpression2.HasAnyErrors)
			{
				boundExpression2 = this.VisitExpression(boundExpression2);
				boundExpression2 = this.MakeImplicitConversion(boundExpression2, conversion.Type);
			}
			return boundExpression2;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000C7EB0 File Offset: 0x000C60B0
		private bool CanLowerToStringConcatenation(BoundInterpolatedString node)
		{
			foreach (BoundExpression boundExpression in node.Parts)
			{
				BoundStringInsert boundStringInsert = boundExpression as BoundStringInsert;
				if (boundStringInsert != null)
				{
					if (!this._inExpressionLambda && !boundStringInsert.HasErrors)
					{
						TypeSymbol type = boundStringInsert.Value.Type;
						if (type != null && type.SpecialType == SpecialType.System_String && boundStringInsert.Alignment == null && boundStringInsert.Format == null)
						{
							continue;
						}
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000C7F2C File Offset: 0x000C612C
		private void MakeInterpolatedStringFormat(BoundInterpolatedString node, out BoundExpression format, out ArrayBuilder<BoundExpression> expressions)
		{
			this._factory.Syntax = node.Syntax;
			int num = node.Parts.Length - 1;
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			expressions = ArrayBuilder<BoundExpression>.GetInstance(num + 1);
			int num2 = 0;
			for (int i = 0; i <= num; i++)
			{
				BoundExpression boundExpression = node.Parts[i];
				BoundStringInsert boundStringInsert = boundExpression as BoundStringInsert;
				if (boundStringInsert == null)
				{
					builder.Append(boundExpression.ConstantValue.StringValue);
				}
				else
				{
					builder.Append('{').Append(num2++);
					if (boundStringInsert.Alignment != null && !boundStringInsert.Alignment.HasErrors)
					{
						builder.Append(',').Append(boundStringInsert.Alignment.ConstantValue.Int64Value);
					}
					if (boundStringInsert.Format != null && !boundStringInsert.Format.HasErrors)
					{
						builder.Append(':').Append(boundStringInsert.Format.ConstantValue.StringValue);
					}
					builder.Append('}');
					BoundExpression boundExpression2 = boundStringInsert.Value;
					TypeSymbol type = boundExpression2.Type;
					if (type != null && type.TypeKind == TypeKind.Dynamic)
					{
						boundExpression2 = this.MakeConversionNode(boundExpression2, this._compilation.ObjectType, false, false);
					}
					expressions.Add(boundExpression2);
				}
			}
			format = this._factory.StringLiteral(instance.ToStringAndFree());
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000C80A0 File Offset: 0x000C62A0
		public override BoundNode VisitInterpolatedString(BoundInterpolatedString node)
		{
			BoundExpression boundExpression;
			if (this.CanLowerToStringConcatenation(node))
			{
				int length = node.Parts.Length;
				if (length == 0)
				{
					return this._factory.StringLiteral("");
				}
				boundExpression = null;
				for (int i = 0; i < length; i++)
				{
					BoundExpression boundExpression2 = node.Parts[i];
					BoundStringInsert boundStringInsert = boundExpression2 as BoundStringInsert;
					if (boundStringInsert != null)
					{
						boundExpression2 = boundStringInsert.Value;
					}
					else
					{
						boundExpression2 = this._factory.StringLiteral(ConstantValueUtils.UnescapeInterpolatedStringLiteral(boundExpression2.ConstantValue.StringValue));
					}
					boundExpression = ((boundExpression == null) ? boundExpression2 : this._factory.Binary(BinaryOperatorKind.StringConcatenation, node.Type, boundExpression, boundExpression2));
				}
				if (length == 1)
				{
					boundExpression = this._factory.Coalesce(boundExpression, this._factory.StringLiteral(""));
				}
			}
			else
			{
				BoundExpression boundExpression3;
				ArrayBuilder<BoundExpression> arrayBuilder;
				this.MakeInterpolatedStringFormat(node, out boundExpression3, out arrayBuilder);
				arrayBuilder.Insert(0, boundExpression3);
				TypeSymbol type = node.Type;
				boundExpression = this._factory.StaticCall(type, "Format", arrayBuilder.ToImmutableAndFree(), false);
			}
			if (!boundExpression.HasAnyErrors)
			{
				boundExpression = this.VisitExpression(boundExpression);
				boundExpression = this.MakeImplicitConversion(boundExpression, node.Type);
			}
			return boundExpression;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000C81CC File Offset: 0x000C63CC
		public override BoundNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node)
		{
			this._needsSpilling = true;
			return LocalRewriter.SwitchExpressionLocalRewriter.Rewrite(this, node);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000C81DC File Offset: 0x000C63DC
		public override BoundNode VisitThrowStatement(BoundThrowStatement node)
		{
			BoundStatement boundStatement = (BoundStatement)base.VisitThrowStatement(node);
			if (this.Instrument && !node.WasCompilerGenerated)
			{
				boundStatement = this._instrumenter.InstrumentThrowStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000C8218 File Offset: 0x000C6418
		public override BoundNode VisitTryStatement(BoundTryStatement node)
		{
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.TryBlock);
			bool sawAwait = this._sawAwait;
			this._sawAwait = false;
			bool flag = this._compilation.Options.OptimizationLevel == OptimizationLevel.Release;
			ImmutableArray<BoundCatchBlock> immutableArray = ((flag && !LocalRewriter.HasSideEffects(boundBlock)) ? ImmutableArray<BoundCatchBlock>.Empty : base.VisitList<BoundCatchBlock>(node.CatchBlocks));
			BoundBlock boundBlock2 = (BoundBlock)this.Visit(node.FinallyBlockOpt);
			this._sawAwaitInExceptionHandler |= this._sawAwait;
			this._sawAwait = this._sawAwait || sawAwait;
			if (flag && !LocalRewriter.HasSideEffects(boundBlock2))
			{
				boundBlock2 = null;
			}
			if (!immutableArray.IsDefaultOrEmpty || boundBlock2 != null)
			{
				return node.Update(boundBlock, immutableArray, boundBlock2, node.FinallyLabelOpt, node.PreferFaultHandler);
			}
			return boundBlock;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000C82DC File Offset: 0x000C64DC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool HasSideEffects([NotNullWhen(true)] BoundStatement statement)
		{
			if (statement == null)
			{
				return false;
			}
			BoundKind kind = statement.Kind;
			if (kind <= BoundKind.SequencePointWithSpan)
			{
				if (kind == BoundKind.SequencePoint)
				{
					return LocalRewriter.HasSideEffects(((BoundSequencePoint)statement).StatementOpt);
				}
				if (kind == BoundKind.SequencePointWithSpan)
				{
					return LocalRewriter.HasSideEffects(((BoundSequencePointWithSpan)statement).StatementOpt);
				}
			}
			else
			{
				if (kind == BoundKind.Block)
				{
					ImmutableArray<BoundStatement>.Enumerator enumerator = ((BoundBlock)statement).Statements.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (LocalRewriter.HasSideEffects(enumerator.Current))
						{
							return true;
						}
					}
					return false;
				}
				if (kind == BoundKind.NoOpStatement)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000C8368 File Offset: 0x000C6568
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			if (node.ExceptionFilterOpt == null)
			{
				return base.VisitCatchBlock(node);
			}
			ConstantValue constantValue = node.ExceptionFilterOpt.ConstantValue;
			if (constantValue != null && !constantValue.BooleanValue)
			{
				return null;
			}
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExceptionSourceOpt);
			BoundStatementList boundStatementList = (BoundStatementList)this.Visit(node.ExceptionFilterPrologueOpt);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.ExceptionFilterOpt);
			BoundBlock boundBlock = (BoundBlock)this.Visit(node.Body);
			TypeSymbol typeSymbol = this.VisitType(node.ExceptionTypeOpt);
			if (boundExpression2 != null && !node.WasCompilerGenerated && this.Instrument)
			{
				boundExpression2 = this._instrumenter.InstrumentCatchClauseFilter(node, boundExpression2, this._factory);
			}
			return node.Update(node.Locals, boundExpression, typeSymbol, boundStatementList, boundExpression2, boundBlock, node.IsSynthesizedAsyncCatchAll);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000C8438 File Offset: 0x000C6638
		public override BoundNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node)
		{
			TypeSymbol type = node.Type;
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
			BoundExpression boundExpression = this.ReplaceTerminalElementsWithTemps(node.Left, node.Operators, instance, instance2);
			BoundExpression boundExpression2 = this.ReplaceTerminalElementsWithTemps(node.Right, node.Operators, instance, instance2);
			BoundExpression boundExpression3 = this.RewriteTupleNestedOperators(node.Operators, boundExpression, boundExpression2, type, instance2, node.OperatorKind);
			return this._factory.Sequence(instance2.ToImmutableAndFree(), instance.ToImmutableAndFree(), boundExpression3);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000C84B8 File Offset: 0x000C66B8
		private bool IsLikeTupleExpression(BoundExpression expr, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out BoundTupleExpression tuple)
		{
			BoundTupleExpression boundTupleExpression = expr as BoundTupleExpression;
			if (boundTupleExpression == null)
			{
				BoundConversion boundConversion = expr as BoundConversion;
				if (boundConversion != null)
				{
					Conversion conversion = boundConversion.Conversion;
					ConversionKind kind = conversion.Kind;
					BoundExpression boundExpression;
					if (kind == ConversionKind.Identity)
					{
						boundExpression = boundConversion.Operand;
						BoundExpression boundExpression2 = boundExpression;
						return this.IsLikeTupleExpression(boundExpression2, out tuple);
					}
					if (kind == ConversionKind.ImplicitTupleLiteral)
					{
						boundExpression = boundConversion.Operand;
						BoundExpression boundExpression3 = boundExpression;
						return this.IsLikeTupleExpression(boundExpression3, out tuple);
					}
					boundExpression = boundConversion.Operand;
					Conversion conversion2 = conversion;
					BoundExpression boundExpression4 = boundExpression;
					BoundConversion boundConversion2 = boundConversion;
					if (conversion2.IsTupleConversion || conversion2.IsTupleLiteralConversion)
					{
						if (!this.IsLikeTupleExpression(boundExpression4, out tuple))
						{
							return false;
						}
						ImmutableArray<Conversion> underlyingConversions = conversion2.UnderlyingConversions;
						ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = boundConversion2.Type.TupleElementTypesWithAnnotations;
						ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(tuple.Arguments.Length);
						for (int i = 0; i < tuple.Arguments.Length; i++)
						{
							BoundExpression boundExpression5 = tuple.Arguments[i];
							Conversion conversion3 = underlyingConversions[i];
							TypeSymbol type = tupleElementTypesWithAnnotations[i].Type;
							BoundConversion boundConversion3 = new BoundConversion(expr.Syntax, boundExpression5, conversion3, boundConversion2.Checked, boundConversion2.ExplicitCastInCode, null, null, type, boundConversion2.HasErrors);
							instance.Add(boundConversion3);
						}
						ImmutableArray<BoundExpression> immutableArray = instance.ToImmutableAndFree();
						tuple = new BoundConvertedTupleLiteral(tuple.Syntax, null, true, immutableArray, ImmutableArray<string>.Empty, ImmutableArray<bool>.Empty, boundConversion2.Type, boundConversion2.HasErrors);
						return true;
					}
					else
					{
						ConversionKind conversionKind = kind;
						BoundExpression boundExpression6 = boundExpression;
						if (conversionKind == ConversionKind.ImplicitNullable || conversionKind == ConversionKind.ExplicitNullable)
						{
							TypeSymbol type2 = expr.Type;
							if (type2 != null && type2.IsNullableType() && type2.StrippedType().Equals(boundExpression6.Type, TypeCompareKind.AllIgnoreOptions))
							{
								return this.IsLikeTupleExpression(boundExpression6, out tuple);
							}
						}
					}
				}
				tuple = null;
				return false;
			}
			BoundTupleExpression boundTupleExpression2 = boundTupleExpression;
			tuple = boundTupleExpression2;
			return true;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000C86A4 File Offset: 0x000C68A4
		private BoundExpression PushDownImplicitTupleConversion(BoundExpression expr, ArrayBuilder<BoundExpression> initEffects, ArrayBuilder<LocalSymbol> temps)
		{
			BoundConversion boundConversion = expr as BoundConversion;
			if (boundConversion != null && boundConversion.ConversionKind == ConversionKind.ImplicitTuple)
			{
				Conversion conversion = boundConversion.Conversion;
				SyntaxNode syntax = boundConversion.Syntax;
				ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = expr.Type.TupleElementTypesWithAnnotations;
				int length = tupleElementTypesWithAnnotations.Length;
				ImmutableArray<FieldSymbol> tupleElements = boundConversion.Operand.Type.TupleElements;
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(length);
				BoundExpression boundExpression = this.DeferSideEffectingArgumentToTempForTupleEquality(this.LowerConversions(boundConversion.Operand), initEffects, temps, false);
				ImmutableArray<Conversion> underlyingConversions = conversion.UnderlyingConversions;
				for (int i = 0; i < length; i++)
				{
					BoundExpression boundExpression2 = this.MakeTupleFieldAccessAndReportUseSiteDiagnostics(boundExpression, syntax, tupleElements[i]);
					BoundConversion boundConversion2 = new BoundConversion(syntax, boundExpression2, underlyingConversions[i], boundConversion.Checked, boundConversion.ExplicitCastInCode, null, null, tupleElementTypesWithAnnotations[i].Type, boundConversion.HasErrors);
					instance.Add(boundConversion2);
				}
				return new BoundConvertedTupleLiteral(syntax, null, true, instance.ToImmutableAndFree(), ImmutableArray<string>.Empty, ImmutableArray<bool>.Empty, expr.Type, expr.HasErrors);
			}
			return expr;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000C87B8 File Offset: 0x000C69B8
		private BoundExpression ReplaceTerminalElementsWithTemps(BoundExpression expr, TupleBinaryOperatorInfo operators, ArrayBuilder<BoundExpression> initEffects, ArrayBuilder<LocalSymbol> temps)
		{
			if (operators.InfoKind == TupleBinaryOperatorInfoKind.Multiple)
			{
				expr = this.PushDownImplicitTupleConversion(expr, initEffects, temps);
				BoundTupleExpression boundTupleExpression;
				if (this.IsLikeTupleExpression(expr, out boundTupleExpression))
				{
					TupleBinaryOperatorInfo.Multiple multiple = (TupleBinaryOperatorInfo.Multiple)operators;
					ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(boundTupleExpression.Arguments.Length);
					for (int i = 0; i < boundTupleExpression.Arguments.Length; i++)
					{
						BoundExpression boundExpression = boundTupleExpression.Arguments[i];
						BoundExpression boundExpression2 = this.ReplaceTerminalElementsWithTemps(boundExpression, multiple.Operators[i], initEffects, temps);
						instance.Add(boundExpression2);
					}
					ImmutableArray<BoundExpression> immutableArray = instance.ToImmutableAndFree();
					return new BoundConvertedTupleLiteral(boundTupleExpression.Syntax, null, false, immutableArray, ImmutableArray<string>.Empty, ImmutableArray<bool>.Empty, boundTupleExpression.Type, boundTupleExpression.HasErrors);
				}
			}
			return this.DeferSideEffectingArgumentToTempForTupleEquality(expr, initEffects, temps, false);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000C8898 File Offset: 0x000C6A98
		private BoundExpression DeferSideEffectingArgumentToTempForTupleEquality(BoundExpression expr, ArrayBuilder<BoundExpression> effects, ArrayBuilder<LocalSymbol> temps, bool enclosingConversionWasExplicit = false)
		{
			if (expr != null)
			{
				if (expr.ConstantValue != null)
				{
					return this.VisitExpression(expr);
				}
				BoundConversion boundConversion = expr as BoundConversion;
				if (boundConversion != null)
				{
					Conversion conversion = boundConversion.Conversion;
					ConversionKind kind = conversion.Kind;
					if (kind == ConversionKind.DefaultLiteral)
					{
						return this.EvaluateSideEffectingArgumentToTemp(expr, effects, temps);
					}
					ConversionKind conversionKind = kind;
					if (LocalRewriter.<DeferSideEffectingArgumentToTempForTupleEquality>g__conversionMustBePerformedOnOriginalExpression|379_0(boundConversion, conversionKind))
					{
						return this.EvaluateSideEffectingArgumentToTemp(expr, effects, temps);
					}
					if (conversion.IsUserDefined)
					{
						if (boundConversion.ExplicitCastInCode || enclosingConversionWasExplicit)
						{
							return this.EvaluateSideEffectingArgumentToTemp(expr, effects, temps);
						}
					}
					BoundConversion boundConversion2 = boundConversion;
					BoundExpression boundExpression = this.DeferSideEffectingArgumentToTempForTupleEquality(boundConversion2.Operand, effects, temps, boundConversion2.ExplicitCastInCode || enclosingConversionWasExplicit);
					return boundConversion2.UpdateOperand(boundExpression);
				}
				else
				{
					BoundObjectCreationExpression boundObjectCreationExpression = expr as BoundObjectCreationExpression;
					if (boundObjectCreationExpression != null)
					{
						int length = boundObjectCreationExpression.Arguments.Length;
						if (length != 0)
						{
							if (length == 1)
							{
								TypeSymbol typeSymbol = boundObjectCreationExpression.Type;
								if (typeSymbol != null)
								{
									TypeSymbol typeSymbol2 = typeSymbol;
									BoundObjectCreationExpression boundObjectCreationExpression2 = boundObjectCreationExpression;
									if (typeSymbol2.IsNullableType())
									{
										BoundExpression boundExpression2 = this.DeferSideEffectingArgumentToTempForTupleEquality(boundObjectCreationExpression2.Arguments[0], effects, temps, true);
										return new BoundConversion(expr.Syntax, boundExpression2, Conversion.MakeNullableConversion(ConversionKind.ImplicitNullable, Conversion.Identity), false, true, null, null, typeSymbol2, expr.HasErrors);
									}
								}
							}
						}
						else
						{
							TypeSymbol typeSymbol = boundObjectCreationExpression.Type;
							if (typeSymbol != null)
							{
								if (typeSymbol.IsNullableType())
								{
									return new BoundLiteral(expr.Syntax, ConstantValue.Null, expr.Type);
								}
							}
						}
					}
				}
			}
			return this.EvaluateSideEffectingArgumentToTemp(expr, effects, temps);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000C8A2C File Offset: 0x000C6C2C
		private BoundExpression RewriteTupleOperator(TupleBinaryOperatorInfo @operator, BoundExpression left, BoundExpression right, TypeSymbol boolType, ArrayBuilder<LocalSymbol> temps, BinaryOperatorKind operatorKind)
		{
			switch (@operator.InfoKind)
			{
			case TupleBinaryOperatorInfoKind.Single:
				return this.RewriteTupleSingleOperator((TupleBinaryOperatorInfo.Single)@operator, left, right, boolType, operatorKind);
			case TupleBinaryOperatorInfoKind.NullNull:
			{
				TupleBinaryOperatorInfo.NullNull nullNull = (TupleBinaryOperatorInfo.NullNull)@operator;
				return new BoundLiteral(left.Syntax, ConstantValue.Create(nullNull.Kind == BinaryOperatorKind.Equal), boolType);
			}
			case TupleBinaryOperatorInfoKind.Multiple:
				return this.RewriteTupleNestedOperators((TupleBinaryOperatorInfo.Multiple)@operator, left, right, boolType, temps, operatorKind);
			default:
				throw ExceptionUtilities.UnexpectedValue(@operator.InfoKind);
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000C8AB4 File Offset: 0x000C6CB4
		private BoundExpression RewriteTupleNestedOperators(TupleBinaryOperatorInfo.Multiple operators, BoundExpression left, BoundExpression right, TypeSymbol boolType, ArrayBuilder<LocalSymbol> temps, BinaryOperatorKind operatorKind)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			BoundExpression boundExpression;
			BoundExpression boundExpression2;
			bool flag;
			this.MakeNullableParts(left, temps, instance2, instance, true, out boundExpression, out boundExpression2, out flag);
			BoundExpression boundExpression3;
			BoundExpression boundExpression4;
			bool flag2;
			this.MakeNullableParts(right, temps, instance2, instance, false, out boundExpression3, out boundExpression4, out flag2);
			BoundExpression boundExpression5 = this.RewriteNonNullableNestedTupleOperators(operators, boundExpression2, boundExpression4, boolType, temps, instance2, operatorKind);
			BoundExpression boundExpression6 = this._factory.Sequence(ImmutableArray<LocalSymbol>.Empty, instance2.ToImmutableAndFree(), boundExpression5);
			if (!flag && !flag2)
			{
				return boundExpression6;
			}
			bool flag3 = operatorKind == BinaryOperatorKind.Equal;
			if (boundExpression3.ConstantValue == ConstantValue.False)
			{
				return this._factory.Sequence(ImmutableArray<LocalSymbol>.Empty, instance.ToImmutableAndFree(), flag3 ? this._factory.Not(boundExpression) : boundExpression);
			}
			if (boundExpression.ConstantValue == ConstantValue.False)
			{
				return this._factory.Sequence(ImmutableArray<LocalSymbol>.Empty, instance.ToImmutableAndFree(), flag3 ? this._factory.Not(boundExpression3) : boundExpression3);
			}
			return this._factory.Sequence(ImmutableArray<LocalSymbol>.Empty, instance.ToImmutableAndFree(), this._factory.Conditional(this._factory.Binary(BinaryOperatorKind.Equal, boolType, boundExpression, boundExpression3), this._factory.Conditional(boundExpression, boundExpression6, this.MakeBooleanConstant(right.Syntax, flag3), boolType), this.MakeBooleanConstant(right.Syntax, !flag3), boolType));
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000C8C18 File Offset: 0x000C6E18
		private void MakeNullableParts(BoundExpression expr, ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> innerEffects, ArrayBuilder<BoundExpression> outerEffects, bool saveHasValue, out BoundExpression hasValue, out BoundExpression value, out bool isNullable)
		{
			isNullable = !(expr is BoundTupleExpression) && expr.Type != null && expr.Type.IsNullableType();
			if (!isNullable)
			{
				hasValue = this.MakeBooleanConstant(expr.Syntax, true);
				expr = this.PushDownImplicitTupleConversion(expr, innerEffects, temps);
				value = expr;
				return;
			}
			if (LocalRewriter.NullableNeverHasValue(expr))
			{
				hasValue = this.MakeBooleanConstant(expr.Syntax, false);
				value = new BoundDefaultExpression(expr.Syntax, expr.Type.StrippedType(), false);
				return;
			}
			BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(expr);
			if (boundExpression != null)
			{
				hasValue = this.MakeBooleanConstant(expr.Syntax, true);
				value = this.PushDownImplicitTupleConversion(boundExpression, innerEffects, temps);
				value = this.LowerConversions(value);
				isNullable = false;
				return;
			}
			hasValue = this.<MakeNullableParts>g__makeNullableHasValue|382_0(expr);
			if (saveHasValue)
			{
				hasValue = this.MakeTemp(hasValue, temps, outerEffects);
			}
			value = this.MakeValueOrDefaultTemp(expr, temps, innerEffects);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000C8D00 File Offset: 0x000C6F00
		private BoundLocal MakeTemp(BoundExpression loweredExpression, ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> effects)
		{
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(loweredExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			effects.Add(boundAssignmentOperator);
			temps.Add(boundLocal.LocalSymbol);
			return boundLocal;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000C8D34 File Offset: 0x000C6F34
		private BoundExpression MakeValueOrDefaultTemp(BoundExpression expr, ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> effects)
		{
			BoundConversion boundConversion = expr as BoundConversion;
			if (boundConversion != null)
			{
				Conversion conversion = boundConversion.Conversion;
				if (conversion.IsIdentity)
				{
					BoundExpression boundExpression = boundConversion.Operand;
					BoundExpression boundExpression2 = boundExpression;
					return this.MakeValueOrDefaultTemp(boundExpression2, temps, effects);
				}
				if (conversion.IsNullable)
				{
					ImmutableArray<Conversion> underlyingConversions = conversion.UnderlyingConversions;
					BoundExpression boundExpression = boundConversion.Operand;
					ImmutableArray<Conversion> immutableArray = underlyingConversions;
					BoundExpression boundExpression3 = boundExpression;
					BoundConversion boundConversion2 = boundConversion;
					TypeSymbol type = expr.Type;
					if (type != null && type.IsNullableType() && boundExpression3.Type != null && boundExpression3.Type.IsNullableType())
					{
						Conversion conversion2 = immutableArray[0];
						if (conversion2.IsTupleConversion)
						{
							BoundExpression boundExpression4 = this.MakeValueOrDefaultTemp(boundExpression3, temps, effects);
							ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = expr.Type.GetNullableUnderlyingType().TupleElementTypesWithAnnotations;
							int length = boundExpression4.Type.TupleElementTypesWithAnnotations.Length;
							ImmutableArray<Conversion> underlyingConversions2 = conversion2.UnderlyingConversions;
							ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(length);
							for (int i = 0; i < length; i++)
							{
								instance.Add(LocalRewriter.<MakeValueOrDefaultTemp>g__MakeBoundConversion|384_0(this.GetTuplePart(boundExpression4, i), underlyingConversions2[i], tupleElementTypesWithAnnotations[i], boundConversion2));
							}
							return new BoundConvertedTupleLiteral(boundExpression4.Syntax, null, false, instance.ToImmutableAndFree(), ImmutableArray<string>.Empty, ImmutableArray<bool>.Empty, expr.Type, expr.HasErrors).WithSuppression(expr.IsSuppressed);
						}
					}
				}
			}
			BoundExpression boundExpression5 = this.MakeOptimizedGetValueOrDefault(expr.Syntax, expr);
			return this.MakeTemp(boundExpression5, temps, effects);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000C8EC0 File Offset: 0x000C70C0
		private BoundExpression RewriteNonNullableNestedTupleOperators(TupleBinaryOperatorInfo.Multiple operators, BoundExpression left, BoundExpression right, TypeSymbol type, ArrayBuilder<LocalSymbol> temps, ArrayBuilder<BoundExpression> effects, BinaryOperatorKind operatorKind)
		{
			ImmutableArray<TupleBinaryOperatorInfo> operators2 = operators.Operators;
			BoundExpression boundExpression = null;
			for (int i = 0; i < operators2.Length; i++)
			{
				BoundExpression tuplePart = this.GetTuplePart(left, i);
				BoundExpression tuplePart2 = this.GetTuplePart(right, i);
				BoundExpression boundExpression2 = this.RewriteTupleOperator(operators2[i], tuplePart, tuplePart2, type, temps, operatorKind);
				if (boundExpression == null)
				{
					boundExpression = boundExpression2;
				}
				else
				{
					BinaryOperatorKind binaryOperatorKind = ((operatorKind == BinaryOperatorKind.Equal) ? BinaryOperatorKind.LogicalBoolAnd : BinaryOperatorKind.LogicalBoolOr);
					boundExpression = this._factory.Binary(binaryOperatorKind, type, boundExpression, boundExpression2);
				}
			}
			return boundExpression;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000C8F48 File Offset: 0x000C7148
		private BoundExpression GetTuplePart(BoundExpression tuple, int i)
		{
			BoundTupleExpression boundTupleExpression = tuple as BoundTupleExpression;
			if (boundTupleExpression != null)
			{
				return boundTupleExpression.Arguments[i];
			}
			return this.MakeTupleFieldAccessAndReportUseSiteDiagnostics(tuple, tuple.Syntax, tuple.Type.TupleElements[i]);
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000C8F90 File Offset: 0x000C7190
		private BoundExpression RewriteTupleSingleOperator(TupleBinaryOperatorInfo.Single single, BoundExpression left, BoundExpression right, TypeSymbol boolType, BinaryOperatorKind operatorKind)
		{
			left = this.LowerConversions(left);
			right = this.LowerConversions(right);
			if (single.Kind.IsDynamic())
			{
				BoundExpression boundExpression = this._dynamicFactory.MakeDynamicBinaryOperator(single.Kind, left, right, false, this._compilation.DynamicType).ToExpression();
				if (operatorKind == BinaryOperatorKind.Equal)
				{
					return this._factory.Not(this.MakeUnaryOperator(UnaryOperatorKind.DynamicFalse, left.Syntax, null, boundExpression, boolType));
				}
				return this.MakeUnaryOperator(UnaryOperatorKind.DynamicTrue, left.Syntax, null, boundExpression, boolType);
			}
			else
			{
				if (left.IsLiteralNull() && right.IsLiteralNull())
				{
					return new BoundLiteral(left.Syntax, ConstantValue.Create(operatorKind == BinaryOperatorKind.Equal), boolType);
				}
				SyntaxNode syntax = this._factory.Syntax;
				BinaryOperatorKind kind = single.Kind;
				BoundExpression boundExpression2 = left;
				BoundExpression boundExpression3 = right;
				MethodSymbol methodSymbolOpt = single.MethodSymbolOpt;
				BoundExpression boundExpression4 = this.MakeBinaryOperator(syntax, kind, boundExpression2, boundExpression3, ((methodSymbolOpt != null) ? methodSymbolOpt.ReturnType : null) ?? boolType, single.MethodSymbolOpt, false, false, null);
				UnaryOperatorSignature boolOperator = single.BoolOperator;
				Conversion conversionForBool = single.ConversionForBool;
				BoundExpression boundExpression6;
				if (boolOperator.Kind != UnaryOperatorKind.Error)
				{
					BoundExpression boundExpression5 = this.MakeConversionNode(this._factory.Syntax, boundExpression4, conversionForBool, boolOperator.OperandType, false, false, null);
					boundExpression6 = this.MakeUnaryOperator(boolOperator.Kind, boundExpression4.Syntax, boolOperator.Method, boundExpression5, boolType);
					if (operatorKind == BinaryOperatorKind.Equal)
					{
						boundExpression6 = this._factory.Not(boundExpression6);
					}
				}
				else if (!conversionForBool.IsIdentity)
				{
					boundExpression6 = this.MakeConversionNode(this._factory.Syntax, boundExpression4, conversionForBool, boolType, false, false, null);
				}
				else
				{
					boundExpression6 = boundExpression4;
				}
				return boundExpression6;
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000C9120 File Offset: 0x000C7320
		private BoundExpression LowerConversions(BoundExpression expr)
		{
			BoundConversion boundConversion = expr as BoundConversion;
			if (boundConversion == null)
			{
				return expr;
			}
			return this.MakeConversionNode(boundConversion, boundConversion.Syntax, this.LowerConversions(boundConversion.Operand), boundConversion.Conversion, boundConversion.Checked, boundConversion.ExplicitCastInCode, boundConversion.ConstantValue, boundConversion.Type);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000C9170 File Offset: 0x000C7370
		public override BoundNode VisitTupleLiteral(BoundTupleLiteral node)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000C9177 File Offset: 0x000C7377
		public override BoundNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node)
		{
			return this.VisitTupleExpression(node);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000C9180 File Offset: 0x000C7380
		private BoundNode VisitTupleExpression(BoundTupleExpression node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			return this.RewriteTupleCreationExpression(node, immutableArray);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000C91A2 File Offset: 0x000C73A2
		private BoundExpression RewriteTupleCreationExpression(BoundTupleExpression node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments)
		{
			return this.MakeTupleCreationExpression(node.Syntax, (NamedTypeSymbol)node.Type, rewrittenArguments);
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000C91BC File Offset: 0x000C73BC
		private BoundExpression MakeTupleCreationExpression(SyntaxNode syntax, NamedTypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> rewrittenArguments)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			NamedTypeSymbol.GetUnderlyingTypeChain(type, instance);
			BoundExpression boundExpression;
			try
			{
				NamedTypeSymbol namedTypeSymbol = instance.Pop<NamedTypeSymbol>();
				ImmutableArray<BoundExpression> immutableArray = ImmutableArray.Create<BoundExpression>(rewrittenArguments, instance.Count * 7, namedTypeSymbol.Arity);
				MethodSymbol methodSymbol = (MethodSymbol)NamedTypeSymbol.GetWellKnownMemberInType(namedTypeSymbol.OriginalDefinition, NamedTypeSymbol.GetTupleCtor(namedTypeSymbol.Arity), this._diagnostics, syntax);
				if (methodSymbol == null)
				{
					boundExpression = this._factory.BadExpression(type);
				}
				else
				{
					MethodSymbol methodSymbol2 = methodSymbol.AsMember(namedTypeSymbol);
					BoundObjectCreationExpression boundObjectCreationExpression = new BoundObjectCreationExpression(syntax, methodSymbol2, immutableArray);
					if (instance.Count > 0)
					{
						MethodSymbol methodSymbol3 = (MethodSymbol)NamedTypeSymbol.GetWellKnownMemberInType(instance.Peek<NamedTypeSymbol>().OriginalDefinition, NamedTypeSymbol.GetTupleCtor(8), this._diagnostics, syntax);
						if (methodSymbol3 == null)
						{
							return this._factory.BadExpression(type);
						}
						do
						{
							ImmutableArray<BoundExpression> immutableArray2 = ImmutableArray.Create<BoundExpression>(rewrittenArguments, (instance.Count - 1) * 7, 7).Add(boundObjectCreationExpression);
							MethodSymbol methodSymbol4 = methodSymbol3.AsMember(instance.Pop<NamedTypeSymbol>());
							boundObjectCreationExpression = new BoundObjectCreationExpression(syntax, methodSymbol4, immutableArray2);
						}
						while (instance.Count > 0);
					}
					boundObjectCreationExpression = boundObjectCreationExpression.Update(boundObjectCreationExpression.Constructor, boundObjectCreationExpression.Arguments, boundObjectCreationExpression.ArgumentNamesOpt, boundObjectCreationExpression.ArgumentRefKindsOpt, boundObjectCreationExpression.Expanded, boundObjectCreationExpression.ArgsToParamsOpt, boundObjectCreationExpression.DefaultArguments, boundObjectCreationExpression.ConstantValue, boundObjectCreationExpression.InitializerExpressionOpt, type);
					boundExpression = boundObjectCreationExpression;
				}
			}
			finally
			{
				instance.Free();
			}
			return boundExpression;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000C933C File Offset: 0x000C753C
		public override BoundNode VisitUnaryOperator(BoundUnaryOperator node)
		{
			UnaryOperatorKind unaryOperatorKind = node.OperatorKind.Operator();
			if (unaryOperatorKind <= UnaryOperatorKind.PostfixDecrement)
			{
				if (unaryOperatorKind != UnaryOperatorKind.PostfixIncrement && unaryOperatorKind != UnaryOperatorKind.PostfixDecrement)
				{
					goto IL_003E;
				}
			}
			else if (unaryOperatorKind != UnaryOperatorKind.PrefixIncrement && unaryOperatorKind != UnaryOperatorKind.PrefixDecrement)
			{
				goto IL_003E;
			}
			return base.VisitUnaryOperator(node);
			IL_003E:
			if (node.Operand.Kind == BoundKind.BinaryOperator)
			{
				BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)node.Operand;
				if ((node.OperatorKind == UnaryOperatorKind.DynamicTrue && boundBinaryOperator.OperatorKind == BinaryOperatorKind.DynamicLogicalOr) || (node.OperatorKind == UnaryOperatorKind.DynamicFalse && boundBinaryOperator.OperatorKind == BinaryOperatorKind.DynamicLogicalAnd))
				{
					return this.VisitBinaryOperator(boundBinaryOperator, node);
				}
			}
			BoundExpression boundExpression = this.VisitExpression(node.Operand);
			return this.MakeUnaryOperator(node, node.OperatorKind, node.Syntax, node.MethodOpt, boundExpression, node.Type);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000C940C File Offset: 0x000C760C
		private BoundExpression MakeUnaryOperator(UnaryOperatorKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, BoundExpression loweredOperand, TypeSymbol type)
		{
			return this.MakeUnaryOperator(null, kind, syntax, method, loweredOperand, type);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000C941C File Offset: 0x000C761C
		private BoundExpression MakeUnaryOperator([global::System.Runtime.CompilerServices.Nullable(2)] BoundUnaryOperator oldNode, UnaryOperatorKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, BoundExpression loweredOperand, TypeSymbol type)
		{
			if (kind.IsDynamic())
			{
				ConstantValue constantValue = LocalRewriter.UnboxConstant(loweredOperand);
				if (constantValue == ConstantValue.True || constantValue == ConstantValue.False)
				{
					if (kind == UnaryOperatorKind.DynamicLogicalNegation)
					{
						return this.MakeConversionNode(this._factory.Literal(!constantValue.BooleanValue), type, false, false);
					}
					if (kind == UnaryOperatorKind.DynamicTrue)
					{
						return this._factory.Literal(constantValue.BooleanValue);
					}
				}
				return this._dynamicFactory.MakeDynamicUnaryOperator(kind, loweredOperand, type).ToExpression();
			}
			if (kind.IsLifted())
			{
				if (!this._inExpressionLambda)
				{
					return this.LowerLiftedUnaryOperator(kind, syntax, method, loweredOperand, type);
				}
			}
			else if (kind.IsUserDefined())
			{
				if (!this._inExpressionLambda || kind == UnaryOperatorKind.UserDefinedTrue || kind == UnaryOperatorKind.UserDefinedFalse)
				{
					return BoundCall.Synthesized(syntax, null, method, loweredOperand);
				}
			}
			else if (kind.Operator() == UnaryOperatorKind.UnaryPlus)
			{
				return loweredOperand;
			}
			if (kind == UnaryOperatorKind.EnumBitwiseComplement)
			{
				NamedTypeSymbol enumUnderlyingType = loweredOperand.Type.GetEnumUnderlyingType();
				SpecialType enumPromotedType = Binder.GetEnumPromotedType(enumUnderlyingType.SpecialType);
				NamedTypeSymbol namedTypeSymbol = ((enumPromotedType == enumUnderlyingType.SpecialType) ? enumUnderlyingType : this._compilation.GetSpecialType(enumPromotedType));
				BoundExpression boundExpression = this.MakeConversionNode(loweredOperand, namedTypeSymbol, false, false);
				UnaryOperatorKind unaryOperatorKind = kind.Operator().WithType(enumPromotedType);
				BoundUnaryOperator boundUnaryOperator = ((oldNode != null) ? oldNode.Update(unaryOperatorKind, boundExpression, oldNode.ConstantValueOpt, method, boundExpression.ResultKind, namedTypeSymbol) : new BoundUnaryOperator(syntax, unaryOperatorKind, boundExpression, null, method, LookupResultKind.Viable, namedTypeSymbol, false));
				return this.MakeConversionNode(boundUnaryOperator.Syntax, boundUnaryOperator, Conversion.ExplicitEnumeration, type, false, false, null);
			}
			if (kind == UnaryOperatorKind.DecimalUnaryMinus)
			{
				method = (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(SpecialMember.System_Decimal__op_UnaryNegation);
				if (!this._inExpressionLambda)
				{
					return BoundCall.Synthesized(syntax, null, method, loweredOperand);
				}
			}
			if (oldNode == null)
			{
				return new BoundUnaryOperator(syntax, kind, loweredOperand, null, method, LookupResultKind.Viable, type, false);
			}
			return oldNode.Update(kind, loweredOperand, oldNode.ConstantValueOpt, method, oldNode.ResultKind, type);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000C9614 File Offset: 0x000C7814
		private BoundExpression LowerLiftedUnaryOperator(UnaryOperatorKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, BoundExpression loweredOperand, TypeSymbol type)
		{
			BoundExpression boundExpression = this.OptimizeLiftedUnaryOperator(kind, syntax, method, loweredOperand, type);
			if (boundExpression != null)
			{
				return boundExpression;
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(loweredOperand, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, boundLocal.Type, SpecialMember.System_Nullable_T_GetValueOrDefault);
			BoundExpression boundExpression2 = this.MakeNullableHasValue(syntax, boundLocal);
			BoundExpression boundExpression3 = BoundCall.Synthesized(syntax, boundLocal, methodSymbol);
			BoundExpression liftedUnaryOperatorConsequence = this.GetLiftedUnaryOperatorConsequence(kind, syntax, method, type, boundExpression3);
			BoundExpression boundExpression4 = new BoundDefaultExpression(syntax, type, false);
			BoundExpression boundExpression5 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression2, liftedUnaryOperatorConsequence, boundExpression4, null, type, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression5, type, false);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000C96B4 File Offset: 0x000C78B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression OptimizeLiftedUnaryOperator(UnaryOperatorKind operatorKind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, BoundExpression loweredOperand, TypeSymbol type)
		{
			if (LocalRewriter.NullableNeverHasValue(loweredOperand))
			{
				return new BoundDefaultExpression(syntax, type, false);
			}
			BoundExpression boundExpression = LocalRewriter.NullableAlwaysHasValue(loweredOperand);
			if (boundExpression != null)
			{
				return this.GetLiftedUnaryOperatorConsequence(operatorKind, syntax, method, type, boundExpression);
			}
			BoundLoweredConditionalAccess boundLoweredConditionalAccess = loweredOperand as BoundLoweredConditionalAccess;
			if (boundLoweredConditionalAccess != null && (boundLoweredConditionalAccess.WhenNullOpt == null || boundLoweredConditionalAccess.WhenNullOpt.IsDefaultValue()))
			{
				BoundExpression boundExpression2 = this.LowerLiftedUnaryOperator(operatorKind, syntax, method, boundLoweredConditionalAccess.WhenNotNull, type);
				return boundLoweredConditionalAccess.Update(boundLoweredConditionalAccess.Receiver, boundLoweredConditionalAccess.HasValueMethodOpt, boundExpression2, null, boundLoweredConditionalAccess.Id, boundExpression2.Type);
			}
			if (loweredOperand.Kind == BoundKind.Sequence)
			{
				BoundSequence boundSequence = (BoundSequence)loweredOperand;
				if (boundSequence.Value.Kind == BoundKind.ConditionalOperator)
				{
					BoundConditionalOperator boundConditionalOperator = (BoundConditionalOperator)boundSequence.Value;
					if (LocalRewriter.NullableAlwaysHasValue(boundConditionalOperator.Consequence) != null && LocalRewriter.NullableNeverHasValue(boundConditionalOperator.Alternative))
					{
						return new BoundSequence(syntax, boundSequence.Locals, boundSequence.SideEffects, LocalRewriter.RewriteConditionalOperator(syntax, boundConditionalOperator.Condition, this.MakeUnaryOperator(operatorKind, syntax, method, boundConditionalOperator.Consequence, type), this.MakeUnaryOperator(operatorKind, syntax, method, boundConditionalOperator.Alternative, type), null, type, false), type, false);
					}
				}
			}
			return null;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000C97E4 File Offset: 0x000C79E4
		private BoundExpression GetLiftedUnaryOperatorConsequence(UnaryOperatorKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol method, TypeSymbol type, BoundExpression nonNullOperand)
		{
			MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, type, SpecialMember.System_Nullable_T__ctor);
			BoundExpression boundExpression = this.MakeUnaryOperator(null, kind.Unlifted(), syntax, method, nonNullOperand, type.GetNullableUnderlyingType());
			return new BoundObjectCreationExpression(syntax, methodSymbol, new BoundExpression[] { boundExpression });
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000C9828 File Offset: 0x000C7A28
		private static bool IsIncrement(BoundIncrementOperator node)
		{
			UnaryOperatorKind unaryOperatorKind = node.OperatorKind.Operator();
			return unaryOperatorKind == UnaryOperatorKind.PostfixIncrement || unaryOperatorKind == UnaryOperatorKind.PrefixIncrement;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000C9854 File Offset: 0x000C7A54
		private static bool IsPrefix(BoundIncrementOperator node)
		{
			UnaryOperatorKind unaryOperatorKind = node.OperatorKind.Operator();
			return unaryOperatorKind == UnaryOperatorKind.PrefixIncrement || unaryOperatorKind == UnaryOperatorKind.PrefixDecrement;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000C9880 File Offset: 0x000C7A80
		public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
		{
			bool flag = LocalRewriter.IsPrefix(node);
			bool flag2 = node.OperatorKind.IsDynamic();
			bool flag3 = node.OperatorKind.IsChecked();
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			SyntaxNode syntax = node.Syntax;
			BoundExpression boundExpression = this.TransformCompoundAssignmentLHS(node.Operand, instance2, instance, flag2);
			TypeSymbol type = boundExpression.Type;
			LocalSymbol localSymbol = this._factory.SynthesizedLocal(type, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			instance.Add(localSymbol);
			BoundExpression boundExpression2 = new BoundLocal(syntax, localSymbol, null, type, false);
			BoundExpression boundExpression3 = this.MakeIncrementOperator(node, flag ? this.MakeRValue(boundExpression) : boundExpression2);
			if (LocalRewriter.IsIndirectOrInstanceField(boundExpression))
			{
				return this.RewriteWithRefOperand(flag, flag3, instance, instance2, syntax, boundExpression, type, boundExpression2, boundExpression3);
			}
			return this.RewriteWithNotRefOperand(flag, flag3, instance, instance2, syntax, boundExpression, type, boundExpression2, boundExpression3);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000C9954 File Offset: 0x000C7B54
		private static bool IsIndirectOrInstanceField(BoundExpression expression)
		{
			BoundKind kind = expression.Kind;
			if (kind == BoundKind.Local)
			{
				return ((BoundLocal)expression).LocalSymbol.RefKind > RefKind.None;
			}
			if (kind != BoundKind.Parameter)
			{
				return kind == BoundKind.FieldAccess && !((BoundFieldAccess)expression).FieldSymbol.IsStatic;
			}
			return ((BoundParameter)expression).ParameterSymbol.RefKind > RefKind.None;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000C99BC File Offset: 0x000C7BBC
		private BoundNode RewriteWithNotRefOperand(bool isPrefix, bool isChecked, ArrayBuilder<LocalSymbol> tempSymbols, ArrayBuilder<BoundExpression> tempInitializers, SyntaxNode syntax, BoundExpression transformedLHS, TypeSymbol operandType, BoundExpression boundTemp, BoundExpression newValue)
		{
			ImmutableArray<BoundExpression> immutableArray = ImmutableArray.Create<BoundExpression>(this.MakeAssignmentOperator(syntax, boundTemp, isPrefix ? newValue : this.MakeRValue(transformedLHS), operandType, false, isChecked, false), this.MakeAssignmentOperator(syntax, transformedLHS, isPrefix ? boundTemp : newValue, operandType, false, isChecked, false));
			return new BoundSequence(syntax, tempSymbols.ToImmutableAndFree(), tempInitializers.ToImmutableAndFree().Concat<BoundExpression>(immutableArray), boundTemp, operandType, false);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000C9A24 File Offset: 0x000C7C24
		private BoundNode RewriteWithRefOperand(bool isPrefix, bool isChecked, ArrayBuilder<LocalSymbol> tempSymbols, ArrayBuilder<BoundExpression> tempInitializers, SyntaxNode syntax, BoundExpression operand, TypeSymbol operandType, BoundExpression boundTemp, BoundExpression newValue)
		{
			BoundExpression boundExpression = (isPrefix ? newValue : this.MakeRValue(operand));
			BoundExpression boundExpression2 = this.MakeAssignmentOperator(syntax, boundTemp, boundExpression, operandType, false, isChecked, false);
			BoundExpression boundExpression3 = (isPrefix ? boundTemp : newValue);
			BoundSequence boundSequence = new BoundSequence(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundExpression2), boundExpression3, boundExpression.Type, false);
			BoundExpression boundExpression4 = this.MakeAssignmentOperator(syntax, operand, boundSequence, operandType, false, isChecked, false);
			tempInitializers.Add(boundExpression4);
			return new BoundSequence(syntax, tempSymbols.ToImmutableAndFree(), tempInitializers.ToImmutableAndFree(), boundTemp, operandType, false);
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000C9AAC File Offset: 0x000C7CAC
		private BoundExpression MakeIncrementOperator(BoundIncrementOperator node, BoundExpression rewrittenValueToIncrement)
		{
			if (node.OperatorKind.IsDynamic())
			{
				return this._dynamicFactory.MakeDynamicUnaryOperator(node.OperatorKind, rewrittenValueToIncrement, node.Type).ToExpression();
			}
			BoundExpression boundExpression;
			if (node.OperatorKind.OperandTypes() == UnaryOperatorKind.UserDefined)
			{
				boundExpression = this.MakeUserDefinedIncrementOperator(node, rewrittenValueToIncrement);
			}
			else
			{
				boundExpression = this.MakeBuiltInIncrementOperator(node, rewrittenValueToIncrement);
			}
			if (!node.ResultConversion.IsIdentity)
			{
				boundExpression = this.MakeConversionNode(node.Syntax, boundExpression, node.ResultConversion, node.Type, node.OperatorKind.IsChecked(), false, null);
			}
			return boundExpression;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000C9B44 File Offset: 0x000C7D44
		private BoundExpression MakeUserDefinedIncrementOperator(BoundIncrementOperator node, BoundExpression rewrittenValueToIncrement)
		{
			bool flag = node.OperatorKind.IsLifted();
			bool flag2 = node.OperatorKind.IsChecked();
			BoundExpression boundExpression = rewrittenValueToIncrement;
			SyntaxNode syntax = node.Syntax;
			TypeSymbol typeSymbol = node.MethodOpt.GetParameterType(0);
			if (flag)
			{
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(new TypeSymbol[] { typeSymbol });
			}
			if (!node.OperandConversion.IsIdentity)
			{
				boundExpression = this.MakeConversionNode(syntax, rewrittenValueToIncrement, node.OperandConversion, typeSymbol, flag2, false, null);
			}
			if (!flag)
			{
				return BoundCall.Synthesized(syntax, null, node.MethodOpt, boundExpression);
			}
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, typeSymbol, SpecialMember.System_Nullable_T_GetValueOrDefault);
			MethodSymbol methodSymbol2 = this.UnsafeGetNullableMethod(syntax, typeSymbol, SpecialMember.System_Nullable_T__ctor);
			BoundExpression boundExpression2 = this.MakeNullableHasValue(node.Syntax, boundLocal);
			BoundExpression boundExpression3 = BoundCall.Synthesized(syntax, boundLocal, methodSymbol);
			BoundExpression boundExpression4 = BoundCall.Synthesized(syntax, null, node.MethodOpt, boundExpression3);
			BoundExpression boundExpression5 = new BoundObjectCreationExpression(syntax, methodSymbol2, new BoundExpression[] { boundExpression4 });
			BoundExpression boundExpression6 = new BoundDefaultExpression(syntax, typeSymbol, false);
			BoundExpression boundExpression7 = LocalRewriter.RewriteConditionalOperator(syntax, boundExpression2, boundExpression5, boundExpression6, null, typeSymbol, false);
			return new BoundSequence(syntax, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundExpression7, typeSymbol, false);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000C9C84 File Offset: 0x000C7E84
		private BoundExpression MakeBuiltInIncrementOperator(BoundIncrementOperator node, BoundExpression rewrittenValueToIncrement)
		{
			TypeSymbol unaryOperatorType = this.GetUnaryOperatorType(node);
			BinaryOperatorKind binaryOperatorKind = LocalRewriter.GetCorrespondingBinaryOperator(node);
			binaryOperatorKind |= (LocalRewriter.IsIncrement(node) ? BinaryOperatorKind.Addition : BinaryOperatorKind.Subtraction);
			ValueTuple<TypeSymbol, ConstantValue> constantOneForIncrement = LocalRewriter.GetConstantOneForIncrement(this._compilation, binaryOperatorKind);
			TypeSymbol typeSymbol = constantOneForIncrement.Item1;
			ConstantValue item = constantOneForIncrement.Item2;
			BoundExpression boundExpression = this.MakeLiteral(node.Syntax, item, typeSymbol, null);
			if (binaryOperatorKind.IsLifted())
			{
				typeSymbol = this._compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(new TypeSymbol[] { typeSymbol });
				MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(node.Syntax, typeSymbol, SpecialMember.System_Nullable_T__ctor);
				boundExpression = new BoundObjectCreationExpression(node.Syntax, methodSymbol, new BoundExpression[] { boundExpression });
			}
			BoundExpression boundExpression2 = rewrittenValueToIncrement;
			bool flag = node.OperatorKind.IsChecked();
			if (!node.OperandConversion.IsIdentity)
			{
				boundExpression2 = this.MakeConversionNode(node.Syntax, boundExpression2, node.OperandConversion, unaryOperatorType, flag, false, null);
			}
			if (node.OperatorKind.OperandTypes() == UnaryOperatorKind.Pointer)
			{
				return this.MakeBinaryOperator(node.Syntax, binaryOperatorKind, boundExpression2, boundExpression, boundExpression2.Type, null, false, false, null);
			}
			boundExpression2 = this.MakeConversionNode(boundExpression2, typeSymbol, flag, false);
			BoundExpression boundExpression3;
			if (unaryOperatorType.SpecialType == SpecialType.System_Decimal)
			{
				boundExpression3 = this.MakeDecimalIncDecOperator(node.Syntax, binaryOperatorKind, boundExpression2);
			}
			else if (unaryOperatorType.IsNullableType() && unaryOperatorType.GetNullableUnderlyingType().SpecialType == SpecialType.System_Decimal)
			{
				boundExpression3 = this.MakeLiftedDecimalIncDecOperator(node.Syntax, binaryOperatorKind, boundExpression2);
			}
			else
			{
				boundExpression3 = this.MakeBinaryOperator(node.Syntax, binaryOperatorKind, boundExpression2, boundExpression, typeSymbol, null, false, false, null);
			}
			return this.MakeConversionNode(boundExpression3, unaryOperatorType, flag, false);
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000C9E14 File Offset: 0x000C8014
		private MethodSymbol GetDecimalIncDecOperator(BinaryOperatorKind oper)
		{
			BinaryOperatorKind binaryOperatorKind = oper.Operator();
			SpecialMember specialMember;
			if (binaryOperatorKind != BinaryOperatorKind.Addition)
			{
				if (binaryOperatorKind != BinaryOperatorKind.Subtraction)
				{
					throw ExceptionUtilities.UnexpectedValue(oper.Operator());
				}
				specialMember = SpecialMember.System_Decimal__op_Decrement;
			}
			else
			{
				specialMember = SpecialMember.System_Decimal__op_Increment;
			}
			return (MethodSymbol)this._compilation.Assembly.GetSpecialTypeMember(specialMember);
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000C9E6C File Offset: 0x000C806C
		private BoundExpression MakeDecimalIncDecOperator(SyntaxNode syntax, BinaryOperatorKind oper, BoundExpression operand)
		{
			MethodSymbol decimalIncDecOperator = this.GetDecimalIncDecOperator(oper);
			return BoundCall.Synthesized(syntax, null, decimalIncDecOperator, operand);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000C9E8C File Offset: 0x000C808C
		private BoundExpression MakeLiftedDecimalIncDecOperator(SyntaxNode syntax, BinaryOperatorKind oper, BoundExpression operand)
		{
			MethodSymbol decimalIncDecOperator = this.GetDecimalIncDecOperator(oper);
			MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, operand.Type, SpecialMember.System_Nullable_T_GetValueOrDefault);
			MethodSymbol methodSymbol2 = this.UnsafeGetNullableMethod(syntax, operand.Type, SpecialMember.System_Nullable_T__ctor);
			BoundExpression boundExpression = this.MakeNullableHasValue(syntax, operand);
			BoundExpression boundExpression2 = BoundCall.Synthesized(syntax, operand, methodSymbol);
			BoundExpression boundExpression3 = BoundCall.Synthesized(syntax, null, decimalIncDecOperator, boundExpression2);
			BoundExpression boundExpression4 = new BoundObjectCreationExpression(syntax, methodSymbol2, new BoundExpression[] { boundExpression3 });
			BoundExpression boundExpression5 = new BoundDefaultExpression(syntax, operand.Type, false);
			return LocalRewriter.RewriteConditionalOperator(syntax, boundExpression, boundExpression4, boundExpression5, null, operand.Type, false);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000C9F18 File Offset: 0x000C8118
		private BoundExpression MakeRValue(BoundExpression transformedExpression)
		{
			BoundKind kind = transformedExpression.Kind;
			if (kind != BoundKind.DynamicMemberAccess)
			{
				switch (kind)
				{
				case BoundKind.PropertyAccess:
				{
					BoundPropertyAccess boundPropertyAccess = (BoundPropertyAccess)transformedExpression;
					return this.MakePropertyGetAccess(transformedExpression.Syntax, boundPropertyAccess.ReceiverOpt, boundPropertyAccess.PropertySymbol, boundPropertyAccess);
				}
				case BoundKind.IndexerAccess:
				{
					BoundIndexerAccess boundIndexerAccess = (BoundIndexerAccess)transformedExpression;
					return this.MakePropertyGetAccess(transformedExpression.Syntax, boundIndexerAccess.ReceiverOpt, boundIndexerAccess.Indexer, boundIndexerAccess.Arguments, null, null);
				}
				case BoundKind.DynamicIndexerAccess:
				{
					BoundDynamicIndexerAccess boundDynamicIndexerAccess = (BoundDynamicIndexerAccess)transformedExpression;
					return this.MakeDynamicGetIndex(boundDynamicIndexerAccess, boundDynamicIndexerAccess.Receiver, boundDynamicIndexerAccess.Arguments, boundDynamicIndexerAccess.ArgumentNamesOpt, boundDynamicIndexerAccess.ArgumentRefKindsOpt);
				}
				}
				return transformedExpression;
			}
			BoundDynamicMemberAccess boundDynamicMemberAccess = (BoundDynamicMemberAccess)transformedExpression;
			return this._dynamicFactory.MakeDynamicGetMember(boundDynamicMemberAccess.Receiver, boundDynamicMemberAccess.Name, false).ToExpression();
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000C9FF8 File Offset: 0x000C81F8
		private TypeSymbol GetUnaryOperatorType(BoundIncrementOperator node)
		{
			UnaryOperatorKind unaryOperatorKind = node.OperatorKind.OperandTypes();
			if (unaryOperatorKind == UnaryOperatorKind.Enum)
			{
				return node.Type;
			}
			SpecialType specialType;
			switch (unaryOperatorKind)
			{
			case UnaryOperatorKind.SByte:
				specialType = SpecialType.System_SByte;
				goto IL_00DF;
			case UnaryOperatorKind.Byte:
				specialType = SpecialType.System_Byte;
				goto IL_00DF;
			case UnaryOperatorKind.Short:
				specialType = SpecialType.System_Int16;
				goto IL_00DF;
			case UnaryOperatorKind.UShort:
				specialType = SpecialType.System_UInt16;
				goto IL_00DF;
			case UnaryOperatorKind.Int:
				specialType = SpecialType.System_Int32;
				goto IL_00DF;
			case UnaryOperatorKind.UInt:
				specialType = SpecialType.System_UInt32;
				goto IL_00DF;
			case UnaryOperatorKind.Long:
				specialType = SpecialType.System_Int64;
				goto IL_00DF;
			case UnaryOperatorKind.ULong:
				specialType = SpecialType.System_UInt64;
				goto IL_00DF;
			case UnaryOperatorKind.NInt:
				specialType = SpecialType.System_IntPtr;
				goto IL_00DF;
			case UnaryOperatorKind.NUInt:
				specialType = SpecialType.System_UIntPtr;
				goto IL_00DF;
			case UnaryOperatorKind.Char:
				specialType = SpecialType.System_Char;
				goto IL_00DF;
			case UnaryOperatorKind.Float:
				specialType = SpecialType.System_Single;
				goto IL_00DF;
			case UnaryOperatorKind.Double:
				specialType = SpecialType.System_Double;
				goto IL_00DF;
			case UnaryOperatorKind.Decimal:
				specialType = SpecialType.System_Decimal;
				goto IL_00DF;
			case UnaryOperatorKind.Bool:
			case UnaryOperatorKind._Object:
			case UnaryOperatorKind._String:
			case UnaryOperatorKind._StringAndObject:
			case UnaryOperatorKind._ObjectAndString:
			case UnaryOperatorKind.Enum:
			case UnaryOperatorKind._EnumAndUnderlying:
			case UnaryOperatorKind._UnderlyingAndEnum:
			case UnaryOperatorKind._Delegate:
				break;
			case UnaryOperatorKind.Pointer:
				return node.Type;
			default:
				if (unaryOperatorKind != UnaryOperatorKind.UserDefined)
				{
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(unaryOperatorKind);
			IL_00DF:
			NamedTypeSymbol namedTypeSymbol = this._compilation.GetSpecialType(specialType);
			if (node.OperatorKind.IsLifted())
			{
				namedTypeSymbol = this._compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(new TypeSymbol[] { namedTypeSymbol });
			}
			return namedTypeSymbol;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000CA11C File Offset: 0x000C831C
		private static BinaryOperatorKind GetCorrespondingBinaryOperator(BoundIncrementOperator node)
		{
			UnaryOperatorKind operatorKind = node.OperatorKind;
			UnaryOperatorKind unaryOperatorKind = operatorKind.OperandTypes();
			BinaryOperatorKind binaryOperatorKind;
			switch (unaryOperatorKind)
			{
			case UnaryOperatorKind.SByte:
			case UnaryOperatorKind.Short:
			case UnaryOperatorKind.Int:
				binaryOperatorKind = BinaryOperatorKind.Int;
				goto IL_014F;
			case UnaryOperatorKind.Byte:
			case UnaryOperatorKind.UShort:
			case UnaryOperatorKind.UInt:
			case UnaryOperatorKind.Char:
				binaryOperatorKind = BinaryOperatorKind.UInt;
				goto IL_014F;
			case UnaryOperatorKind.Long:
				binaryOperatorKind = BinaryOperatorKind.Long;
				goto IL_014F;
			case UnaryOperatorKind.ULong:
				binaryOperatorKind = BinaryOperatorKind.ULong;
				goto IL_014F;
			case UnaryOperatorKind.NInt:
				binaryOperatorKind = BinaryOperatorKind.NInt;
				goto IL_014F;
			case UnaryOperatorKind.NUInt:
				binaryOperatorKind = BinaryOperatorKind.NUInt;
				goto IL_014F;
			case UnaryOperatorKind.Float:
				binaryOperatorKind = BinaryOperatorKind.Float;
				goto IL_014F;
			case UnaryOperatorKind.Double:
				binaryOperatorKind = BinaryOperatorKind.Double;
				goto IL_014F;
			case UnaryOperatorKind.Decimal:
				binaryOperatorKind = BinaryOperatorKind.Decimal;
				goto IL_014F;
			case UnaryOperatorKind.Bool:
			case UnaryOperatorKind._Object:
			case UnaryOperatorKind._String:
			case UnaryOperatorKind._StringAndObject:
			case UnaryOperatorKind._ObjectAndString:
			case UnaryOperatorKind._EnumAndUnderlying:
			case UnaryOperatorKind._UnderlyingAndEnum:
			case UnaryOperatorKind._Delegate:
				break;
			case UnaryOperatorKind.Enum:
			{
				TypeSymbol typeSymbol = node.Type;
				if (typeSymbol.IsNullableType())
				{
					typeSymbol = typeSymbol.GetNullableUnderlyingType();
				}
				typeSymbol = typeSymbol.GetEnumUnderlyingType();
				switch (typeSymbol.SpecialType)
				{
				case SpecialType.System_SByte:
				case SpecialType.System_Int16:
				case SpecialType.System_Int32:
					binaryOperatorKind = BinaryOperatorKind.Int;
					goto IL_014F;
				case SpecialType.System_Byte:
				case SpecialType.System_UInt16:
				case SpecialType.System_UInt32:
					binaryOperatorKind = BinaryOperatorKind.UInt;
					goto IL_014F;
				case SpecialType.System_Int64:
					binaryOperatorKind = BinaryOperatorKind.Long;
					goto IL_014F;
				case SpecialType.System_UInt64:
					binaryOperatorKind = BinaryOperatorKind.ULong;
					goto IL_014F;
				default:
					throw ExceptionUtilities.UnexpectedValue(typeSymbol.SpecialType);
				}
				break;
			}
			case UnaryOperatorKind.Pointer:
				binaryOperatorKind = BinaryOperatorKind.PointerAndInt;
				goto IL_014F;
			default:
				if (unaryOperatorKind != UnaryOperatorKind.UserDefined)
				{
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(operatorKind.OperandTypes());
			IL_014F:
			if (binaryOperatorKind - BinaryOperatorKind.Int <= 5 || binaryOperatorKind == BinaryOperatorKind.PointerAndInt)
			{
				binaryOperatorKind |= (BinaryOperatorKind)operatorKind.OverflowChecks();
			}
			if (operatorKind.IsLifted())
			{
				binaryOperatorKind |= BinaryOperatorKind.Lifted;
			}
			return binaryOperatorKind;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000CA2A0 File Offset: 0x000C84A0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
		private static ValueTuple<TypeSymbol, ConstantValue> GetConstantOneForIncrement(CSharpCompilation compilation, BinaryOperatorKind binaryOperatorKind)
		{
			BinaryOperatorKind binaryOperatorKind2 = binaryOperatorKind.OperandTypes();
			ConstantValue constantValue;
			switch (binaryOperatorKind2)
			{
			case BinaryOperatorKind.Int:
				break;
			case BinaryOperatorKind.UInt:
				constantValue = ConstantValue.Create(1U);
				goto IL_00CB;
			case BinaryOperatorKind.Long:
				constantValue = ConstantValue.Create(1L);
				goto IL_00CB;
			case BinaryOperatorKind.ULong:
				constantValue = ConstantValue.Create(1UL);
				goto IL_00CB;
			case BinaryOperatorKind.NInt:
				constantValue = ConstantValue.Create(1);
				return new ValueTuple<TypeSymbol, ConstantValue>(compilation.CreateNativeIntegerTypeSymbol(true), constantValue);
			case BinaryOperatorKind.NUInt:
				constantValue = ConstantValue.Create(1U);
				return new ValueTuple<TypeSymbol, ConstantValue>(compilation.CreateNativeIntegerTypeSymbol(false), constantValue);
			case BinaryOperatorKind.Char:
				goto IL_00BA;
			case BinaryOperatorKind.Float:
				constantValue = ConstantValue.Create(1f);
				goto IL_00CB;
			case BinaryOperatorKind.Double:
				constantValue = ConstantValue.Create(1.0);
				goto IL_00CB;
			case BinaryOperatorKind.Decimal:
				constantValue = ConstantValue.Create(1m);
				goto IL_00CB;
			default:
				if (binaryOperatorKind2 != BinaryOperatorKind.PointerAndInt)
				{
					goto IL_00BA;
				}
				break;
			}
			constantValue = ConstantValue.Create(1);
			goto IL_00CB;
			IL_00BA:
			throw ExceptionUtilities.UnexpectedValue(binaryOperatorKind.OperandTypes());
			IL_00CB:
			return new ValueTuple<TypeSymbol, ConstantValue>(compilation.GetSpecialType(constantValue.SpecialType), constantValue);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000CA38C File Offset: 0x000C858C
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			BoundBlock boundBlock = ((boundStatement.Kind == BoundKind.Block) ? ((BoundBlock)boundStatement) : BoundBlock.SynthesizedNoLocals(node.Syntax, boundStatement));
			if (node.ExpressionOpt != null)
			{
				return this.MakeExpressionUsingStatement(node, boundBlock);
			}
			SyntaxToken syntaxToken = ((node.Syntax.Kind() == SyntaxKind.UsingStatement) ? ((UsingStatementSyntax)node.Syntax).AwaitKeyword : default(SyntaxToken));
			return this.MakeDeclarationUsingStatement(node.Syntax, boundBlock, node.Locals, node.DeclarationsOpt.LocalDeclarations, node.IDisposableConversion, node.PatternDisposeInfoOpt, node.AwaitOpt, syntaxToken);
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000CA438 File Offset: 0x000C8638
		private BoundStatement MakeDeclarationUsingStatement(SyntaxNode syntax, BoundBlock body, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundLocalDeclaration> declarations, Conversion iDisposableConversion, [global::System.Runtime.CompilerServices.Nullable(2)] MethodArgumentInfo patternDisposeInfo, [global::System.Runtime.CompilerServices.Nullable(2)] BoundAwaitableInfo awaitOpt, SyntaxToken awaitKeyword)
		{
			BoundBlock boundBlock = body;
			for (int i = declarations.Length - 1; i >= 0; i--)
			{
				boundBlock = this.RewriteDeclarationUsingStatement(syntax, declarations[i], boundBlock, iDisposableConversion, awaitKeyword, awaitOpt, patternDisposeInfo);
			}
			return new BoundBlock(syntax, locals, ImmutableArray.Create<BoundStatement>(boundBlock), false);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000CA484 File Offset: 0x000C8684
		private BoundStatement MakeLocalUsingDeclarationStatement(BoundUsingLocalDeclarations usingDeclarations, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			LocalDeclarationStatementSyntax localDeclarationStatementSyntax = (LocalDeclarationStatementSyntax)usingDeclarations.Syntax;
			BoundBlock boundBlock = new BoundBlock(localDeclarationStatementSyntax, ImmutableArray<LocalSymbol>.Empty, statements, false);
			return this.MakeDeclarationUsingStatement(localDeclarationStatementSyntax, boundBlock, ImmutableArray<LocalSymbol>.Empty, usingDeclarations.LocalDeclarations, usingDeclarations.IDisposableConversion, usingDeclarations.PatternDisposeInfoOpt, usingDeclarations.AwaitOpt, localDeclarationStatementSyntax.AwaitKeyword);
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000CA4D8 File Offset: 0x000C86D8
		private BoundBlock MakeExpressionUsingStatement(BoundUsingStatement node, BoundBlock tryBlock)
		{
			BoundExpression boundExpression = this.VisitExpression(node.ExpressionOpt);
			if (boundExpression.ConstantValue == ConstantValue.Null)
			{
				return tryBlock;
			}
			TypeSymbol type = boundExpression.Type;
			SyntaxNode syntax = boundExpression.Syntax;
			UsingStatementSyntax usingStatementSyntax = (UsingStatementSyntax)node.Syntax;
			BoundAssignmentOperator boundAssignmentOperator;
			BoundLocal boundLocal;
			if (type == null || type.IsDynamic())
			{
				TypeSymbol typeSymbol = ((node.AwaitOpt == null) ? this._compilation.GetSpecialType(SpecialType.System_IDisposable) : this._compilation.GetWellKnownType(WellKnownType.System_IAsyncDisposable));
				this._diagnostics.ReportUseSite(typeSymbol, usingStatementSyntax);
				BoundExpression boundExpression2 = this.MakeConversionNode(syntax, boundExpression, Conversion.GetTrivialConversion(node.IDisposableConversion.Kind), typeSymbol, false, false, boundExpression.ConstantValue);
				boundLocal = this._factory.StoreToTemp(boundExpression2, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.Using, null);
			}
			else
			{
				boundLocal = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.Using, usingStatementSyntax);
			}
			BoundStatement boundStatement = new BoundExpressionStatement(syntax, boundAssignmentOperator, false);
			if (this.Instrument)
			{
				boundStatement = this._instrumenter.InstrumentUsingTargetCapture(node, boundStatement);
			}
			BoundStatement boundStatement2 = this.RewriteUsingStatementTryFinally(usingStatementSyntax, tryBlock, boundLocal, usingStatementSyntax.AwaitKeyword, node.AwaitOpt, node.PatternDisposeInfoOpt);
			return new BoundBlock(usingStatementSyntax, node.Locals.Add(boundLocal.LocalSymbol), ImmutableArray.Create<BoundStatement>(boundStatement, boundStatement2), false);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000CA61C File Offset: 0x000C881C
		private BoundBlock RewriteDeclarationUsingStatement(SyntaxNode usingSyntax, BoundLocalDeclaration localDeclaration, BoundBlock tryBlock, Conversion iDisposableConversion, SyntaxToken awaitKeywordOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundAwaitableInfo awaitOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodArgumentInfo patternDisposeInfo)
		{
			SyntaxNode syntax = localDeclaration.Syntax;
			LocalSymbol localSymbol = localDeclaration.LocalSymbol;
			TypeSymbol type = localSymbol.Type;
			BoundLocal boundLocal = new BoundLocal(syntax, localSymbol, localDeclaration.InitializerOpt.ConstantValue, type, false);
			BoundStatement boundStatement = this.VisitStatement(localDeclaration);
			if (boundLocal.ConstantValue == ConstantValue.Null)
			{
				return BoundBlock.SynthesizedNoLocals(usingSyntax, new BoundStatement[] { boundStatement, tryBlock });
			}
			if (type.IsDynamic())
			{
				TypeSymbol typeSymbol = ((awaitOpt == null) ? this._compilation.GetSpecialType(SpecialType.System_IDisposable) : this._compilation.GetWellKnownType(WellKnownType.System_IAsyncDisposable));
				this._diagnostics.ReportUseSite(typeSymbol, usingSyntax);
				BoundExpression boundExpression = this.MakeConversionNode(syntax, boundLocal, iDisposableConversion, typeSymbol, false, false, null);
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal2 = this._factory.StoreToTemp(boundExpression, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.Using, null);
				BoundStatement boundStatement2 = this.RewriteUsingStatementTryFinally(usingSyntax, tryBlock, boundLocal2, awaitKeywordOpt, awaitOpt, patternDisposeInfo);
				return new BoundBlock(usingSyntax, ImmutableArray.Create<LocalSymbol>(boundLocal2.LocalSymbol), ImmutableArray.Create<BoundStatement>(boundStatement, new BoundExpressionStatement(syntax, boundAssignmentOperator, false), boundStatement2), false);
			}
			BoundStatement boundStatement3 = this.RewriteUsingStatementTryFinally(usingSyntax, tryBlock, boundLocal, awaitKeywordOpt, awaitOpt, patternDisposeInfo);
			return BoundBlock.SynthesizedNoLocals(usingSyntax, new BoundStatement[] { boundStatement, boundStatement3 });
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000CA748 File Offset: 0x000C8948
		private BoundStatement RewriteUsingStatementTryFinally(SyntaxNode syntax, BoundBlock tryBlock, BoundLocal local, SyntaxToken awaitKeywordOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundAwaitableInfo awaitOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodArgumentInfo patternDisposeInfo)
		{
			bool flag = local.Type.IsNullableType();
			BoundExpression boundExpression;
			if (flag)
			{
				MethodSymbol methodSymbol = this.UnsafeGetNullableMethod(syntax, local.Type, SpecialMember.System_Nullable_T_GetValueOrDefault);
				boundExpression = BoundCall.Synthesized(syntax, local, methodSymbol);
			}
			else
			{
				boundExpression = local;
			}
			BoundExpression boundExpression2 = this.GenerateDisposeCall(syntax, boundExpression, patternDisposeInfo, awaitOpt, awaitKeywordOpt);
			BoundStatement boundStatement = new BoundExpressionStatement(syntax, boundExpression2, false);
			BoundExpression boundExpression3;
			if (flag)
			{
				boundExpression3 = this.MakeNullableHasValue(syntax, local);
			}
			else if (local.Type.IsValueType)
			{
				boundExpression3 = null;
			}
			else
			{
				boundExpression3 = this.MakeNullCheck(syntax, local, BinaryOperatorKind.NotEqual);
			}
			BoundStatement boundStatement2;
			if (boundExpression3 == null)
			{
				boundStatement2 = boundStatement;
			}
			else
			{
				boundStatement2 = LocalRewriter.RewriteIfStatement(syntax, boundExpression3, boundStatement, null, false);
			}
			return new BoundTryStatement(syntax, tryBlock, ImmutableArray<BoundCatchBlock>.Empty, BoundBlock.SynthesizedNoLocals(syntax, boundStatement2), null);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000CA7F0 File Offset: 0x000C89F0
		private BoundExpression GenerateDisposeCall(SyntaxNode syntax, BoundExpression disposedExpression, [global::System.Runtime.CompilerServices.Nullable(2)] MethodArgumentInfo disposeInfo, [global::System.Runtime.CompilerServices.Nullable(2)] BoundAwaitableInfo awaitOpt, SyntaxToken awaitKeyword)
		{
			MethodSymbol methodSymbol = ((disposeInfo != null) ? disposeInfo.Method : null);
			if (methodSymbol == null)
			{
				if (awaitOpt == null)
				{
					Binder.TryGetSpecialTypeMember<MethodSymbol>(this._compilation, SpecialMember.System_IDisposable__Dispose, syntax, this._diagnostics, out methodSymbol);
				}
				else
				{
					this.TryGetWellKnownTypeMember<MethodSymbol>(null, WellKnownMember.System_IAsyncDisposable__DisposeAsync, out methodSymbol, false, awaitKeyword.GetLocation());
				}
			}
			BoundExpression boundExpression;
			if (methodSymbol == null)
			{
				boundExpression = new BoundBadExpression(syntax, LookupResultKind.NotInvocable, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(disposedExpression), ErrorTypeSymbol.UnknownResultType);
			}
			else
			{
				if (disposeInfo == null)
				{
					disposeInfo = MethodArgumentInfo.CreateParameterlessMethod(methodSymbol);
				}
				boundExpression = this.MakeCallWithNoExplicitArgument(disposeInfo, syntax, disposedExpression, true);
				if (awaitOpt != null)
				{
					this._sawAwaitInExceptionHandler = true;
					MethodSymbol getResult = awaitOpt.GetResult;
					TypeSymbol typeSymbol = ((getResult != null) ? getResult.ReturnType : null) ?? this._compilation.DynamicType;
					boundExpression = this.RewriteAwaitExpression(syntax, boundExpression, awaitOpt, typeSymbol, false);
				}
			}
			return boundExpression;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000CA8B4 File Offset: 0x000C8AB4
		private BoundExpression MakeCallWithNoExplicitArgument(MethodArgumentInfo methodArgumentInfo, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression expression, bool assertParametersAreOptional = true)
		{
			MethodSymbol method = methodArgumentInfo.Method;
			return this.MakeCall(syntax, expression, method, methodArgumentInfo.Arguments, default(ImmutableArray<RefKind>), methodArgumentInfo.Expanded, method.IsExtensionMethod, methodArgumentInfo.ArgsToParamsOpt, LookupResultKind.Viable, method.ReturnType, null);
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000CA8FC File Offset: 0x000C8AFC
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			BoundExpression boundExpression = this.VisitExpression(node.Condition);
			BoundStatement boundStatement = this.VisitStatement(node.Body);
			if (!node.WasCompilerGenerated && this.Instrument)
			{
				boundExpression = this._instrumenter.InstrumentWhileStatementCondition(node, boundExpression, this._factory);
			}
			return this.RewriteWhileStatement(node, node.Locals, boundExpression, boundStatement, node.BreakLabel, node.ContinueLabel, node.HasErrors);
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000CA968 File Offset: 0x000C8B68
		private BoundStatement RewriteWhileStatement(BoundLoopStatement loop, BoundExpression rewrittenCondition, BoundStatement rewrittenBody, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors)
		{
			SyntaxNode syntax = loop.Syntax;
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("start");
			BoundStatement boundStatement = new BoundConditionalGoto(rewrittenCondition.Syntax, rewrittenCondition, true, generatedLabelSymbol, false);
			BoundStatement boundStatement2 = new BoundGotoStatement(syntax, continueLabel, false);
			if (this.Instrument && !loop.WasCompilerGenerated)
			{
				BoundKind kind = loop.Kind;
				if (kind != BoundKind.WhileStatement)
				{
					if (kind != BoundKind.ForEachStatement)
					{
						throw ExceptionUtilities.UnexpectedValue(loop.Kind);
					}
					boundStatement = this._instrumenter.InstrumentForEachStatementConditionalGotoStart((BoundForEachStatement)loop, boundStatement);
				}
				else
				{
					boundStatement = this._instrumenter.InstrumentWhileStatementConditionalGotoStartOrBreak((BoundWhileStatement)loop, boundStatement);
				}
				boundStatement2 = BoundSequencePoint.CreateHidden(boundStatement2, false);
			}
			return BoundStatementList.Synthesized(syntax, hasErrors, new BoundStatement[]
			{
				boundStatement2,
				new BoundLabelStatement(syntax, generatedLabelSymbol),
				rewrittenBody,
				new BoundLabelStatement(syntax, continueLabel),
				boundStatement,
				new BoundLabelStatement(syntax, breakLabel)
			});
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000CAA44 File Offset: 0x000C8C44
		private BoundStatement RewriteWhileStatement(BoundWhileStatement loop, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression rewrittenCondition, BoundStatement rewrittenBody, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors)
		{
			if (locals.IsEmpty)
			{
				return this.RewriteWhileStatement(loop, rewrittenCondition, rewrittenBody, breakLabel, continueLabel, hasErrors);
			}
			SyntaxNode syntax = loop.Syntax;
			BoundStatement boundStatement = new BoundLabelStatement(syntax, continueLabel);
			BoundStatement boundStatement2 = new BoundConditionalGoto(rewrittenCondition.Syntax, rewrittenCondition, false, breakLabel, false);
			if (this.Instrument && !loop.WasCompilerGenerated)
			{
				boundStatement2 = this._instrumenter.InstrumentWhileStatementConditionalGotoStartOrBreak(loop, boundStatement2);
				boundStatement = BoundSequencePoint.CreateHidden(boundStatement, false);
			}
			return BoundStatementList.Synthesized(syntax, hasErrors, new BoundStatement[]
			{
				boundStatement,
				new BoundBlock(syntax, locals, ImmutableArray.Create<BoundStatement>(boundStatement2, rewrittenBody, new BoundGotoStatement(syntax, continueLabel, false)), false),
				new BoundLabelStatement(syntax, breakLabel)
			});
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000CAAEC File Offset: 0x000C8CEC
		public override BoundNode VisitYieldBreakStatement(BoundYieldBreakStatement node)
		{
			BoundStatement boundStatement = (BoundStatement)base.VisitYieldBreakStatement(node);
			if (this.Instrument)
			{
				if (node.WasCompilerGenerated)
				{
					if (node.Syntax.Kind() != SyntaxKind.Block)
					{
						return boundStatement;
					}
					MethodSymbol currentFunction = this._factory.CurrentFunction;
					if (currentFunction == null || currentFunction.IsAsync)
					{
						return boundStatement;
					}
				}
				boundStatement = this._instrumenter.InstrumentYieldBreakStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000CAB54 File Offset: 0x000C8D54
		public override BoundNode VisitYieldReturnStatement(BoundYieldReturnStatement node)
		{
			BoundStatement boundStatement = (BoundStatement)base.VisitYieldReturnStatement(node);
			if (this.Instrument && !node.WasCompilerGenerated)
			{
				boundStatement = this._instrumenter.InstrumentYieldReturnStatement(node, boundStatement);
			}
			return boundStatement;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000CAB8D File Offset: 0x000C8D8D
		[CompilerGenerated]
		internal static bool <VisitLocalFunctionStatement>g__hasReturnTypeOrParameter|28_3(LocalFunctionSymbol localFunction, Func<TypeWithAnnotations, bool> predicate)
		{
			return predicate.Invoke(localFunction.ReturnTypeWithAnnotations) || localFunction.ParameterTypesWithAnnotations.Any<TypeWithAnnotations>(predicate);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000CABAC File Offset: 0x000C8DAC
		[CompilerGenerated]
		internal static bool <BuildStoresToTemps>g__isLambdaConversion|136_0(BoundExpression expr)
		{
			BoundConversion boundConversion = expr as BoundConversion;
			return boundConversion != null && boundConversion.ConversionKind == ConversionKind.AnonymousFunction;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000CABD0 File Offset: 0x000C8DD0
		[CompilerGenerated]
		private BoundExpression <VisitCompoundAssignmentOperator>g__rewriteAssignment|147_0(BoundExpression leftRead, ref LocalRewriter.<>c__DisplayClass147_0 A_2)
		{
			SyntaxNode syntax = A_2.node.Syntax;
			BoundExpression boundExpression = (A_2.isDynamic ? leftRead : this.MakeConversionNode(syntax, leftRead, A_2.node.LeftConversion, A_2.node.Operator.LeftType, A_2.isChecked, false, null));
			BoundExpression boundExpression2 = this.MakeBinaryOperator(syntax, A_2.node.Operator.Kind, boundExpression, A_2.loweredRight, A_2.node.Operator.ReturnType, A_2.node.Operator.Method, false, true, null);
			SyntaxNode syntaxNode = syntax;
			BoundExpression boundExpression3 = boundExpression2;
			Conversion finalConversion = A_2.node.FinalConversion;
			TypeSymbol type = A_2.node.Left.Type;
			bool isDynamic = A_2.isDynamic;
			BoundExpression boundExpression4 = this.MakeConversionNode(syntaxNode, boundExpression3, finalConversion, type, A_2.isChecked, isDynamic, null);
			return this.MakeAssignmentOperator(syntax, A_2.transformedLHS, boundExpression4, A_2.node.Left.Type, A_2.used, A_2.isChecked, true);
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000CACC2 File Offset: 0x000C8EC2
		[CompilerGenerated]
		internal static SpecialType <NeedsCheckedConversionInExpressionTree>g__GetUnderlyingSpecialType|176_0(TypeSymbol type)
		{
			return type.StrippedType().EnumUnderlyingTypeOrSelf().SpecialType;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000CACD4 File Offset: 0x000C8ED4
		[CompilerGenerated]
		internal static bool <NeedsCheckedConversionInExpressionTree>g__IsInRange|176_1(SpecialType type, SpecialType low, SpecialType high)
		{
			return low <= type && type <= high;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000CACE3 File Offset: 0x000C8EE3
		[CompilerGenerated]
		internal static TypeSymbol <RewriteDecimalConversion>g__get64BitType|197_0(CSharpCompilation compilation, bool signed)
		{
			return compilation.GetSpecialType(signed ? SpecialType.System_Int64 : SpecialType.System_UInt64);
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000CACF4 File Offset: 0x000C8EF4
		[CompilerGenerated]
		internal static bool <VisitIsPatternExpression>g__canProduceLinearSequence|286_0(BoundDecisionDagNode node, LabelSymbol whenTrueLabel, LabelSymbol whenFalseLabel)
		{
			BoundLeafDecisionDagNode boundLeafDecisionDagNode;
			for (;;)
			{
				BoundWhenDecisionDagNode boundWhenDecisionDagNode = node as BoundWhenDecisionDagNode;
				if (boundWhenDecisionDagNode == null)
				{
					boundLeafDecisionDagNode = node as BoundLeafDecisionDagNode;
					if (boundLeafDecisionDagNode != null)
					{
						goto IL_0036;
					}
					BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = node as BoundEvaluationDecisionDagNode;
					if (boundEvaluationDecisionDagNode == null)
					{
						BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
						if (boundTestDecisionDagNode == null)
						{
							break;
						}
						bool flag = LocalRewriter.IsFailureNode(boundTestDecisionDagNode.WhenFalse, whenFalseLabel);
						if (flag == LocalRewriter.IsFailureNode(boundTestDecisionDagNode.WhenTrue, whenFalseLabel))
						{
							return false;
						}
						node = (flag ? boundTestDecisionDagNode.WhenTrue : boundTestDecisionDagNode.WhenFalse);
					}
					else
					{
						node = boundEvaluationDecisionDagNode.Next;
					}
				}
				else
				{
					node = boundWhenDecisionDagNode.WhenTrue;
				}
			}
			throw ExceptionUtilities.UnexpectedValue(node);
			IL_0036:
			return boundLeafDecisionDagNode.Label == whenTrueLabel;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000CAD90 File Offset: 0x000C8F90
		[CompilerGenerated]
		private BoundExpression <VisitNullCoalescingAssignmentOperator>g__rewriteNullCoalscingAssignmentStandard|305_0(ref LocalRewriter.<>c__DisplayClass305_0 A_1)
		{
			BoundExpression boundExpression = this.MakeAssignmentOperator(A_1.syntax, A_1.transformedLHS, A_1.loweredRight, A_1.node.LeftOperand.Type, true, false, false);
			BoundExpression boundExpression2 = this.MakeNullCoalescingOperator(A_1.syntax, A_1.lhsRead, boundExpression, Conversion.Identity, BoundNullCoalescingOperatorResultKind.LeftType, A_1.node.LeftOperand.Type);
			if (A_1.temps.Count != 0 || A_1.stores.Count != 0)
			{
				return new BoundSequence(A_1.syntax, A_1.temps.ToImmutableAndFree(), A_1.stores.ToImmutableAndFree(), boundExpression2, boundExpression2.Type, false);
			}
			return boundExpression2;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000CAE38 File Offset: 0x000C9038
		[CompilerGenerated]
		private BoundExpression <VisitNullCoalescingAssignmentOperator>g__rewriteNullCoalescingAssignmentForValueType|305_1(ref LocalRewriter.<>c__DisplayClass305_0 A_1)
		{
			BoundExpression leftOperand = A_1.node.LeftOperand;
			MethodSymbol methodSymbol;
			if (!this.TryGetNullableMethod(leftOperand.Syntax, leftOperand.Type, SpecialMember.System_Nullable_T_GetValueOrDefault, out methodSymbol))
			{
				return LocalRewriter.BadExpression(A_1.node);
			}
			MethodSymbol methodSymbol2;
			if (!this.TryGetNullableMethod(leftOperand.Syntax, leftOperand.Type, SpecialMember.System_Nullable_T_get_HasValue, out methodSymbol2))
			{
				return LocalRewriter.BadExpression(A_1.node);
			}
			if (A_1.lhsRead.Kind == BoundKind.Call)
			{
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal = this._factory.StoreToTemp(A_1.lhsRead, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				A_1.stores.Add(boundAssignmentOperator);
				A_1.temps.Add(boundLocal.LocalSymbol);
				A_1.lhsRead = boundLocal;
			}
			BoundAssignmentOperator boundAssignmentOperator2;
			BoundLocal boundLocal2 = this._factory.StoreToTemp(BoundCall.Synthesized(leftOperand.Syntax, A_1.lhsRead, methodSymbol), out boundAssignmentOperator2, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
			A_1.stores.Add(boundAssignmentOperator2);
			A_1.temps.Add(boundLocal2.LocalSymbol);
			BoundExpression boundExpression = this.MakeAssignmentOperator(A_1.node.Syntax, boundLocal2, A_1.loweredRight, A_1.node.Type, true, false, false);
			BoundExpression boundExpression2 = this.MakeAssignmentOperator(A_1.node.Syntax, A_1.transformedLHS, this.MakeConversionNode(boundLocal2, A_1.transformedLHS.Type, false, false), A_1.node.LeftOperand.Type, true, false, false);
			BoundCall boundCall = BoundCall.Synthesized(leftOperand.Syntax, A_1.lhsRead, methodSymbol2);
			BoundExpression boundExpression3 = this._factory.Sequence(ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundExpression>(boundExpression, boundExpression2), boundLocal2);
			BoundExpression boundExpression4 = this._factory.Conditional(boundCall, boundLocal2, boundExpression3, boundLocal2.Type);
			return this._factory.Sequence(A_1.temps.ToImmutableAndFree(), A_1.stores.ToImmutableAndFree(), boundExpression4);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000CB000 File Offset: 0x000C9200
		[CompilerGenerated]
		private BoundExpression <VisitRangeExpression>g__tryOptimizeOperand|342_0(BoundExpression operand, ref LocalRewriter.<>c__DisplayClass342_0 A_2)
		{
			operand = this.VisitExpression(operand);
			if (LocalRewriter.NullableNeverHasValue(operand))
			{
				operand = new BoundDefaultExpression(operand.Syntax, operand.Type.GetNullableUnderlyingType(), false);
			}
			else
			{
				operand = LocalRewriter.NullableAlwaysHasValue(operand) ?? operand;
				if (operand.Type.IsNullableType())
				{
					A_2.needLifting = true;
				}
			}
			return operand;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000CB05C File Offset: 0x000C925C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[CompilerGenerated]
		private BoundExpression <LiftRangeExpression>g__getIndexFromPossibleNullable|343_0(BoundExpression arg, ref LocalRewriter.<>c__DisplayClass343_0 A_2)
		{
			if (arg == null)
			{
				return null;
			}
			BoundExpression boundExpression = this.CaptureExpressionInTempIfNeeded(arg, A_2.sideeffects, A_2.locals, SynthesizedLocalKind.LoweringTemp);
			if (boundExpression.Type.IsNullableType())
			{
				BoundExpression boundExpression2 = this.MakeOptimizedHasValue(boundExpression.Syntax, boundExpression);
				if (A_2.condition == null)
				{
					A_2.condition = boundExpression2;
				}
				else
				{
					TypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Boolean);
					A_2.condition = this.MakeBinaryOperator(A_2.node.Syntax, BinaryOperatorKind.BoolAnd, A_2.condition, boundExpression2, specialType, null, false, false, null);
				}
				return this.MakeOptimizedGetValueOrDefault(boundExpression.Syntax, boundExpression);
			}
			return boundExpression;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000CB0F4 File Offset: 0x000C92F4
		[CompilerGenerated]
		internal static BoundExpression <MakeRangeExpression>g__newIndexZero|344_0(bool fromEnd, ref LocalRewriter.<>c__DisplayClass344_0 A_1)
		{
			return A_1.F.New(WellKnownMember.System_Index__ctor, ImmutableArray.Create<BoundExpression>(A_1.F.Literal(0), A_1.F.Literal(fromEnd)));
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000CB124 File Offset: 0x000C9324
		[CompilerGenerated]
		private BoundExpression <ConvertConcatExprToString>g__makeConditionalAccess|364_0(BoundExpression receiver, ref LocalRewriter.<>c__DisplayClass364_0 A_2)
		{
			int num = this._currentConditionalAccessID + 1;
			this._currentConditionalAccessID = num;
			int num2 = num;
			return new BoundLoweredConditionalAccess(A_2.syntax, receiver, null, BoundCall.Synthesized(A_2.syntax, new BoundConditionalReceiver(A_2.syntax, num2, A_2.expr.Type), A_2.objectToStringMethod), null, num2, this._compilation.GetSpecialType(SpecialType.System_String), false);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000CB188 File Offset: 0x000C9388
		[CompilerGenerated]
		internal static bool <ConvertConcatExprToString>g__isFieldOfMarshalByRef|364_1(BoundExpression expr, CSharpCompilation compilation)
		{
			BoundFieldAccess boundFieldAccess = expr as BoundFieldAccess;
			return boundFieldAccess != null && DiagnosticsPass.IsNonAgileFieldAccess(boundFieldAccess, compilation);
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000CB1A8 File Offset: 0x000C93A8
		[CompilerGenerated]
		internal static bool <DeferSideEffectingArgumentToTempForTupleEquality>g__conversionMustBePerformedOnOriginalExpression|379_0(BoundConversion expr, ConversionKind kind)
		{
			if (kind - ConversionKind.AnonymousFunction > 1)
			{
				switch (kind)
				{
				case ConversionKind.InterpolatedString:
				case ConversionKind.SwitchExpression:
				case ConversionKind.ConditionalExpression:
				case ConversionKind.StackAllocToPointerType:
				case ConversionKind.StackAllocToSpanType:
				case ConversionKind.ObjectCreation:
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000CB1E4 File Offset: 0x000C93E4
		[CompilerGenerated]
		private BoundExpression <MakeNullableParts>g__makeNullableHasValue|382_0(BoundExpression expr)
		{
			BoundConversion boundConversion = expr as BoundConversion;
			if (boundConversion != null)
			{
				Conversion conversion = boundConversion.Conversion;
				if (conversion.IsIdentity)
				{
					BoundExpression boundExpression = boundConversion.Operand;
					BoundExpression boundExpression2 = boundExpression;
					return this.<MakeNullableParts>g__makeNullableHasValue|382_0(boundExpression2);
				}
				if (conversion.IsNullable)
				{
					ImmutableArray<Conversion> underlyingConversions = conversion.UnderlyingConversions;
					BoundExpression boundExpression = boundConversion.Operand;
					ImmutableArray<Conversion> immutableArray = underlyingConversions;
					BoundExpression boundExpression3 = boundExpression;
					if (expr.Type.IsNullableType() && boundExpression3.Type != null && boundExpression3.Type.IsNullableType() && !immutableArray[0].IsUserDefined)
					{
						return this.<MakeNullableParts>g__makeNullableHasValue|382_0(boundExpression3);
					}
				}
			}
			return this.MakeNullableHasValue(expr.Syntax, expr);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000CB294 File Offset: 0x000C9494
		[CompilerGenerated]
		internal static BoundExpression <MakeValueOrDefaultTemp>g__MakeBoundConversion|384_0(BoundExpression expr, Conversion conversion, TypeWithAnnotations type, BoundConversion enclosing)
		{
			return new BoundConversion(expr.Syntax, expr, conversion, enclosing.Checked, enclosing.ExplicitCastInCode, null, null, type.Type, false);
		}

		// Token: 0x040010C7 RID: 4295
		private readonly CSharpCompilation _compilation;

		// Token: 0x040010C8 RID: 4296
		private readonly SyntheticBoundNodeFactory _factory;

		// Token: 0x040010C9 RID: 4297
		private readonly SynthesizedSubmissionFields _previousSubmissionFields;

		// Token: 0x040010CA RID: 4298
		private readonly bool _allowOmissionOfConditionalCalls;

		// Token: 0x040010CB RID: 4299
		private LoweredDynamicOperationFactory _dynamicFactory;

		// Token: 0x040010CC RID: 4300
		private bool _sawLambdas;

		// Token: 0x040010CD RID: 4301
		private int _availableLocalFunctionOrdinal;

		// Token: 0x040010CE RID: 4302
		private bool _inExpressionLambda;

		// Token: 0x040010CF RID: 4303
		private bool _sawAwait;

		// Token: 0x040010D0 RID: 4304
		private bool _sawAwaitInExceptionHandler;

		// Token: 0x040010D1 RID: 4305
		private bool _needsSpilling;

		// Token: 0x040010D2 RID: 4306
		private readonly BindingDiagnosticBag _diagnostics;

		// Token: 0x040010D3 RID: 4307
		private Instrumenter _instrumenter;

		// Token: 0x040010D4 RID: 4308
		private readonly BoundStatement _rootStatement;

		// Token: 0x040010D5 RID: 4309
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private Dictionary<BoundValuePlaceholderBase, BoundExpression> _placeholderReplacementMapDoNotUseDirectly;

		// Token: 0x040010D6 RID: 4310
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BoundExpression _currentConditionalAccessTarget;

		// Token: 0x040010D7 RID: 4311
		private int _currentConditionalAccessID;

		// Token: 0x040010D8 RID: 4312
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1 })]
		private Dictionary<BoundNode, HashSet<LabelSymbol>> _lazyUnmatchedLabelCache;

		// Token: 0x02000729 RID: 1833
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private abstract class DecisionDagRewriter : LocalRewriter.PatternLocalRewriter
		{
			// Token: 0x060085EB RID: 34283
			protected abstract ArrayBuilder<BoundStatement> BuilderForSection(SyntaxNode section);

			// Token: 0x060085EC RID: 34284 RVA: 0x001E51AA File Offset: 0x001E33AA
			protected DecisionDagRewriter(SyntaxNode node, LocalRewriter localRewriter, bool generateInstrumentation)
				: base(node, localRewriter, generateInstrumentation)
			{
			}

			// Token: 0x060085ED RID: 34285 RVA: 0x001E51C0 File Offset: 0x001E33C0
			private void ComputeLabelSet(BoundDecisionDag decisionDag)
			{
				LocalRewriter.DecisionDagRewriter.<>c__DisplayClass4_0 CS$<>8__locals1;
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.hasPredecessor = PooledHashSet<BoundDecisionDagNode>.GetInstance();
				foreach (BoundDecisionDagNode boundDecisionDagNode in decisionDag.TopologicallySortedNodes)
				{
					BoundWhenDecisionDagNode boundWhenDecisionDagNode = boundDecisionDagNode as BoundWhenDecisionDagNode;
					if (boundWhenDecisionDagNode == null)
					{
						BoundLeafDecisionDagNode boundLeafDecisionDagNode = boundDecisionDagNode as BoundLeafDecisionDagNode;
						if (boundLeafDecisionDagNode == null)
						{
							BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = boundDecisionDagNode as BoundEvaluationDecisionDagNode;
							if (boundEvaluationDecisionDagNode == null)
							{
								BoundTestDecisionDagNode boundTestDecisionDagNode = boundDecisionDagNode as BoundTestDecisionDagNode;
								if (boundTestDecisionDagNode == null)
								{
									throw ExceptionUtilities.UnexpectedValue(boundDecisionDagNode.Kind);
								}
								this.<ComputeLabelSet>g__notePredecessor|4_0(boundTestDecisionDagNode.WhenTrue, ref CS$<>8__locals1);
								this.<ComputeLabelSet>g__notePredecessor|4_0(boundTestDecisionDagNode.WhenFalse, ref CS$<>8__locals1);
							}
							else
							{
								this.<ComputeLabelSet>g__notePredecessor|4_0(boundEvaluationDecisionDagNode.Next, ref CS$<>8__locals1);
							}
						}
						else
						{
							this._dagNodeLabels[boundDecisionDagNode] = boundLeafDecisionDagNode.Label;
						}
					}
					else
					{
						this.GetDagNodeLabel(boundDecisionDagNode);
						if (boundWhenDecisionDagNode.WhenFalse != null)
						{
							this.GetDagNodeLabel(boundWhenDecisionDagNode.WhenFalse);
						}
					}
				}
				CS$<>8__locals1.hasPredecessor.Free();
			}

			// Token: 0x060085EE RID: 34286 RVA: 0x001E52BE File Offset: 0x001E34BE
			protected new void Free()
			{
				this._dagNodeLabels.Free();
				base.Free();
			}

			// Token: 0x060085EF RID: 34287 RVA: 0x001E52D4 File Offset: 0x001E34D4
			protected virtual LabelSymbol GetDagNodeLabel(BoundDecisionDagNode dag)
			{
				LabelSymbol labelSymbol;
				if (!this._dagNodeLabels.TryGetValue(dag, ref labelSymbol))
				{
					Dictionary<BoundDecisionDagNode, LabelSymbol> dagNodeLabels = this._dagNodeLabels;
					BoundLeafDecisionDagNode boundLeafDecisionDagNode = dag as BoundLeafDecisionDagNode;
					dagNodeLabels.Add(dag, labelSymbol = ((boundLeafDecisionDagNode != null) ? boundLeafDecisionDagNode.Label : this._factory.GenerateLabel("dagNode")));
				}
				return labelSymbol;
			}

			// Token: 0x060085F0 RID: 34288 RVA: 0x001E5324 File Offset: 0x001E3524
			protected BoundDecisionDag ShareTempsIfPossibleAndEvaluateInput(BoundDecisionDag decisionDag, BoundExpression loweredSwitchGoverningExpression, ArrayBuilder<BoundStatement> result, out BoundExpression savedInputExpression)
			{
				LocalRewriter.DecisionDagRewriter.WhenClauseMightAssignPatternVariableWalker mightAssignWalker = new LocalRewriter.DecisionDagRewriter.WhenClauseMightAssignPatternVariableWalker();
				if (!decisionDag.TopologicallySortedNodes.Any<BoundDecisionDagNode>(delegate(BoundDecisionDagNode node)
				{
					BoundWhenDecisionDagNode boundWhenDecisionDagNode = node as BoundWhenDecisionDagNode;
					return boundWhenDecisionDagNode != null && mightAssignWalker.MightAssignSomething(boundWhenDecisionDagNode.WhenExpression);
				}))
				{
					decisionDag = base.ShareTempsAndEvaluateInput(loweredSwitchGoverningExpression, decisionDag, delegate(BoundExpression expr)
					{
						result.Add(this._factory.ExpressionStatement(expr));
					}, out savedInputExpression);
				}
				else
				{
					BoundExpression temp = this._tempAllocator.GetTemp(BoundDagTemp.ForOriginalInput(loweredSwitchGoverningExpression));
					result.Add(this._factory.Assignment(temp, loweredSwitchGoverningExpression, false));
					savedInputExpression = temp;
				}
				return decisionDag;
			}

			// Token: 0x060085F1 RID: 34289 RVA: 0x001E53B8 File Offset: 0x001E35B8
			protected ImmutableArray<BoundStatement> LowerDecisionDagCore(BoundDecisionDag decisionDag)
			{
				this._loweredDecisionDag = ArrayBuilder<BoundStatement>.GetInstance();
				this.ComputeLabelSet(decisionDag);
				ImmutableArray<BoundDecisionDagNode> topologicallySortedNodes = decisionDag.TopologicallySortedNodes;
				BoundDecisionDagNode boundDecisionDagNode = topologicallySortedNodes[0];
				if (boundDecisionDagNode is BoundWhenDecisionDagNode || boundDecisionDagNode is BoundLeafDecisionDagNode)
				{
					this._loweredDecisionDag.Add(this._factory.Goto(this.GetDagNodeLabel(boundDecisionDagNode)));
				}
				foreach (BoundDecisionDagNode boundDecisionDagNode2 in topologicallySortedNodes)
				{
					BoundWhenDecisionDagNode boundWhenDecisionDagNode = boundDecisionDagNode2 as BoundWhenDecisionDagNode;
					if (boundWhenDecisionDagNode != null)
					{
						this.LowerWhenClause(boundWhenDecisionDagNode);
					}
				}
				ImmutableArray<BoundDecisionDagNode> immutableArray = topologicallySortedNodes.WhereAsArray<BoundDecisionDagNode>((BoundDecisionDagNode n) => n.Kind != BoundKind.WhenDecisionDagNode && n.Kind != BoundKind.LeafDecisionDagNode);
				PooledHashSet<BoundDecisionDagNode> instance = PooledHashSet<BoundDecisionDagNode>.GetInstance();
				int i = 0;
				int length = immutableArray.Length;
				while (i < length)
				{
					BoundDecisionDagNode boundDecisionDagNode3 = immutableArray[i];
					bool flag = instance.Contains(boundDecisionDagNode3);
					LabelSymbol labelSymbol;
					if (!flag || this._dagNodeLabels.TryGetValue(boundDecisionDagNode3, ref labelSymbol))
					{
						LabelSymbol labelSymbol2;
						if (this._dagNodeLabels.TryGetValue(boundDecisionDagNode3, ref labelSymbol2))
						{
							this._loweredDecisionDag.Add(this._factory.Label(labelSymbol2));
						}
						if ((flag || !this.GenerateSwitchDispatch(boundDecisionDagNode3, instance)) && !this.GenerateTypeTestAndCast(boundDecisionDagNode3, instance, immutableArray, i))
						{
							BoundDecisionDagNode boundDecisionDagNode4 = ((i + 1 < length) ? immutableArray[i + 1] : null);
							if (boundDecisionDagNode4 != null && instance.Contains(boundDecisionDagNode4))
							{
								boundDecisionDagNode4 = null;
							}
							this.LowerDecisionDagNode(boundDecisionDagNode3, boundDecisionDagNode4);
						}
					}
					i++;
				}
				instance.Free();
				ImmutableArray<BoundStatement> immutableArray2 = this._loweredDecisionDag.ToImmutableAndFree();
				this._loweredDecisionDag = null;
				return immutableArray2;
			}

			// Token: 0x060085F2 RID: 34290 RVA: 0x001E554C File Offset: 0x001E374C
			private bool GenerateTypeTestAndCast(BoundDecisionDagNode node, HashSet<BoundDecisionDagNode> loweredNodes, ImmutableArray<BoundDecisionDagNode> nodesToLower, int indexOfNode)
			{
				BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
				if (boundTestDecisionDagNode != null)
				{
					BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = boundTestDecisionDagNode.WhenTrue as BoundEvaluationDecisionDagNode;
					BoundExpression boundExpression;
					BoundExpression boundExpression2;
					if (boundEvaluationDecisionDagNode != null && base.TryLowerTypeTestAndCast(boundTestDecisionDagNode.Test, boundEvaluationDecisionDagNode.Evaluation, out boundExpression, out boundExpression2))
					{
						BoundDecisionDagNode next = boundEvaluationDecisionDagNode.Next;
						BoundDecisionDagNode whenFalse = boundTestDecisionDagNode.WhenFalse;
						bool flag = !this._dagNodeLabels.ContainsKey(boundEvaluationDecisionDagNode);
						if (flag)
						{
							loweredNodes.Add(boundEvaluationDecisionDagNode);
						}
						BoundDecisionDagNode boundDecisionDagNode = ((indexOfNode + 2 < nodesToLower.Length && flag && nodesToLower[indexOfNode + 1] == boundEvaluationDecisionDagNode && !loweredNodes.Contains(nodesToLower[indexOfNode + 2])) ? nodesToLower[indexOfNode + 2] : null);
						this._loweredDecisionDag.Add(this._factory.ExpressionStatement(boundExpression));
						this.GenerateTest(boundExpression2, next, whenFalse, boundDecisionDagNode);
						return true;
					}
				}
				return false;
			}

			// Token: 0x060085F3 RID: 34291 RVA: 0x001E562C File Offset: 0x001E382C
			private void GenerateTest(BoundExpression test, BoundDecisionDagNode whenTrue, BoundDecisionDagNode whenFalse, BoundDecisionDagNode nextNode)
			{
				this._factory.Syntax = test.Syntax;
				if (nextNode == whenFalse)
				{
					this._loweredDecisionDag.Add(this._factory.ConditionalGoto(test, this.GetDagNodeLabel(whenTrue), true));
					return;
				}
				if (nextNode == whenTrue)
				{
					this._loweredDecisionDag.Add(this._factory.ConditionalGoto(test, this.GetDagNodeLabel(whenFalse), false));
					return;
				}
				this._loweredDecisionDag.Add(this._factory.ConditionalGoto(test, this.GetDagNodeLabel(whenTrue), true));
				this._loweredDecisionDag.Add(this._factory.Goto(this.GetDagNodeLabel(whenFalse)));
			}

			// Token: 0x060085F4 RID: 34292 RVA: 0x001E56D0 File Offset: 0x001E38D0
			private bool GenerateSwitchDispatch(BoundDecisionDagNode node, HashSet<BoundDecisionDagNode> loweredNodes)
			{
				if (!this.<GenerateSwitchDispatch>g__canGenerateSwitchDispatch|12_0(node))
				{
					return false;
				}
				BoundDagTemp input = ((BoundTestDecisionDagNode)node).Test.Input;
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode = this.GatherValueDispatchNodes(node, loweredNodes, input);
				this.LowerValueDispatchNode(valueDispatchNode, this._tempAllocator.GetTemp(input));
				return true;
			}

			// Token: 0x060085F5 RID: 34293 RVA: 0x001E5718 File Offset: 0x001E3918
			private LocalRewriter.DecisionDagRewriter.ValueDispatchNode GatherValueDispatchNodes(BoundDecisionDagNode node, HashSet<BoundDecisionDagNode> loweredNodes, BoundDagTemp input)
			{
				IValueSetFactory valueSetFactory = ValueSetFactory.ForType(input.Type);
				return this.GatherValueDispatchNodes(node, loweredNodes, input, valueSetFactory);
			}

			// Token: 0x060085F6 RID: 34294 RVA: 0x001E573C File Offset: 0x001E393C
			private LocalRewriter.DecisionDagRewriter.ValueDispatchNode GatherValueDispatchNodes(BoundDecisionDagNode node, HashSet<BoundDecisionDagNode> loweredNodes, BoundDagTemp input, IValueSetFactory fac)
			{
				if (loweredNodes.Contains(node))
				{
					LabelSymbol labelSymbol;
					this._dagNodeLabels.TryGetValue(node, ref labelSymbol);
					return new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode(node.Syntax, labelSymbol);
				}
				BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
				if (boundTestDecisionDagNode == null || !boundTestDecisionDagNode.Test.Input.Equals(input))
				{
					LabelSymbol dagNodeLabel = this.GetDagNodeLabel(node);
					return new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode(node.Syntax, dagNodeLabel);
				}
				BoundDagTest test = boundTestDecisionDagNode.Test;
				BoundDagRelationalTest boundDagRelationalTest = test as BoundDagRelationalTest;
				if (boundDagRelationalTest != null)
				{
					loweredNodes.Add(boundTestDecisionDagNode);
					LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode = this.GatherValueDispatchNodes(boundTestDecisionDagNode.WhenTrue, loweredNodes, input, fac);
					LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode2 = this.GatherValueDispatchNodes(boundTestDecisionDagNode.WhenFalse, loweredNodes, input, fac);
					return LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.CreateBalanced(boundTestDecisionDagNode.Syntax, boundDagRelationalTest.Value, boundDagRelationalTest.OperatorKind, valueDispatchNode, valueDispatchNode2);
				}
				BoundDagValueTest boundDagValueTest = test as BoundDagValueTest;
				if (boundDagValueTest == null)
				{
					LabelSymbol dagNodeLabel2 = this.GetDagNodeLabel(node);
					return new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode(node.Syntax, dagNodeLabel2);
				}
				loweredNodes.Add(boundTestDecisionDagNode);
				ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>> instance = ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>>.GetInstance();
				instance.Add(new ValueTuple<ConstantValue, LabelSymbol>(boundDagValueTest.Value, this.GetDagNodeLabel(boundTestDecisionDagNode.WhenTrue)));
				BoundTestDecisionDagNode boundTestDecisionDagNode2 = boundTestDecisionDagNode;
				for (;;)
				{
					BoundTestDecisionDagNode boundTestDecisionDagNode3 = boundTestDecisionDagNode2.WhenFalse as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode3 == null)
					{
						break;
					}
					BoundDagValueTest boundDagValueTest2 = boundTestDecisionDagNode3.Test as BoundDagValueTest;
					if (boundDagValueTest2 == null || !boundDagValueTest2.Input.Equals(input) || this._dagNodeLabels.ContainsKey(boundTestDecisionDagNode3) || loweredNodes.Contains(boundTestDecisionDagNode3))
					{
						break;
					}
					instance.Add(new ValueTuple<ConstantValue, LabelSymbol>(boundDagValueTest2.Value, this.GetDagNodeLabel(boundTestDecisionDagNode3.WhenTrue)));
					loweredNodes.Add(boundTestDecisionDagNode3);
					boundTestDecisionDagNode2 = boundTestDecisionDagNode3;
				}
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode3 = this.GatherValueDispatchNodes(boundTestDecisionDagNode2.WhenFalse, loweredNodes, input, fac);
				return this.PushEqualityTestsIntoTree(boundDagValueTest.Syntax, valueDispatchNode3, instance.ToImmutableAndFree(), fac);
			}

			// Token: 0x060085F7 RID: 34295 RVA: 0x001E58FC File Offset: 0x001E3AFC
			private LocalRewriter.DecisionDagRewriter.ValueDispatchNode PushEqualityTestsIntoTree(SyntaxNode syntax, LocalRewriter.DecisionDagRewriter.ValueDispatchNode otherwise, [TupleElementNames(new string[] { "value", "label" })] ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> cases, IValueSetFactory fac)
			{
				LocalRewriter.DecisionDagRewriter.<>c__DisplayClass15_0 CS$<>8__locals1;
				CS$<>8__locals1.fac = fac;
				if (cases.IsEmpty)
				{
					return otherwise;
				}
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode leafDispatchNode = otherwise as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode;
				if (leafDispatchNode != null)
				{
					return new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.SwitchDispatch(syntax, cases, leafDispatchNode.Label);
				}
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.SwitchDispatch switchDispatch = otherwise as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.SwitchDispatch;
				if (switchDispatch != null)
				{
					return new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.SwitchDispatch(switchDispatch.Syntax, switchDispatch.Cases.Concat<ValueTuple<ConstantValue, LabelSymbol>>(cases), switchDispatch.Otherwise);
				}
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch = otherwise as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch;
				if (relationalDispatch != null)
				{
					BinaryOperatorKind @operator = relationalDispatch.Operator;
					ConstantValue value = relationalDispatch.Value;
					LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode = relationalDispatch.WhenTrue;
					LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode2 = relationalDispatch.WhenFalse;
					ValueTuple<ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>>, ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>>> valueTuple = LocalRewriter.DecisionDagRewriter.<PushEqualityTestsIntoTree>g__splitCases|15_0(cases, @operator, value, ref CS$<>8__locals1);
					ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> item = valueTuple.Item1;
					ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> item2 = valueTuple.Item2;
					valueDispatchNode = this.PushEqualityTestsIntoTree(syntax, valueDispatchNode, item, CS$<>8__locals1.fac);
					valueDispatchNode2 = this.PushEqualityTestsIntoTree(syntax, valueDispatchNode2, item2, CS$<>8__locals1.fac);
					return relationalDispatch.WithTrueAndFalseChildren(valueDispatchNode, valueDispatchNode2);
				}
				throw ExceptionUtilities.UnexpectedValue(otherwise);
			}

			// Token: 0x060085F8 RID: 34296 RVA: 0x001E59E8 File Offset: 0x001E3BE8
			private void LowerValueDispatchNode(LocalRewriter.DecisionDagRewriter.ValueDispatchNode n, BoundExpression input)
			{
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode leafDispatchNode = n as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode;
				if (leafDispatchNode != null)
				{
					this._loweredDecisionDag.Add(this._factory.Goto(leafDispatchNode.Label));
					return;
				}
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.SwitchDispatch switchDispatch = n as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.SwitchDispatch;
				if (switchDispatch != null)
				{
					this.LowerSwitchDispatchNode(switchDispatch, input);
					return;
				}
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch = n as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch;
				if (relationalDispatch == null)
				{
					throw ExceptionUtilities.UnexpectedValue(n);
				}
				this.LowerRelationalDispatchNode(relationalDispatch, input);
			}

			// Token: 0x060085F9 RID: 34297 RVA: 0x001E5A4C File Offset: 0x001E3C4C
			private void LowerRelationalDispatchNode(LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch rel, BoundExpression input)
			{
				BoundExpression boundExpression = base.MakeRelationalTest(rel.Syntax, input, rel.Operator, rel.Value);
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode leafDispatchNode = rel.WhenTrue as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode;
				if (leafDispatchNode != null)
				{
					LabelSymbol label = leafDispatchNode.Label;
					this._loweredDecisionDag.Add(this._factory.ConditionalGoto(boundExpression, label, true));
					this.LowerValueDispatchNode(rel.WhenFalse, input);
					return;
				}
				LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode leafDispatchNode2 = rel.WhenFalse as LocalRewriter.DecisionDagRewriter.ValueDispatchNode.LeafDispatchNode;
				if (leafDispatchNode2 != null)
				{
					LabelSymbol label2 = leafDispatchNode2.Label;
					this._loweredDecisionDag.Add(this._factory.ConditionalGoto(boundExpression, label2, false));
					this.LowerValueDispatchNode(rel.WhenTrue, input);
					return;
				}
				LabelSymbol labelSymbol = this._factory.GenerateLabel("relationalDispatch");
				this._loweredDecisionDag.Add(this._factory.ConditionalGoto(boundExpression, labelSymbol, false));
				this.LowerValueDispatchNode(rel.WhenTrue, input);
				this._loweredDecisionDag.Add(this._factory.Label(labelSymbol));
				this.LowerValueDispatchNode(rel.WhenFalse, input);
			}

			// Token: 0x060085FA RID: 34298 RVA: 0x001E5B50 File Offset: 0x001E3D50
			private void LowerSwitchDispatchNode(LocalRewriter.DecisionDagRewriter.ValueDispatchNode.SwitchDispatch node, BoundExpression input)
			{
				LocalRewriter.DecisionDagRewriter.<>c__DisplayClass19_0 CS$<>8__locals1;
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.node = node;
				CS$<>8__locals1.input = input;
				CS$<>8__locals1.defaultLabel = CS$<>8__locals1.node.Otherwise;
				if (CS$<>8__locals1.input.Type.IsValidV6SwitchGoverningType(false))
				{
					MethodSymbol methodSymbol = null;
					if (CS$<>8__locals1.input.Type.SpecialType == SpecialType.System_String)
					{
						this.EnsureStringHashFunction(CS$<>8__locals1.node.Cases.Length, CS$<>8__locals1.node.Syntax);
						methodSymbol = this._localRewriter.UnsafeGetSpecialTypeMethod(CS$<>8__locals1.node.Syntax, SpecialMember.System_String__op_Equality);
					}
					BoundSwitchDispatch boundSwitchDispatch = new BoundSwitchDispatch(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.input, CS$<>8__locals1.node.Cases, CS$<>8__locals1.defaultLabel, methodSymbol, false);
					this._loweredDecisionDag.Add(boundSwitchDispatch);
					return;
				}
				if (CS$<>8__locals1.input.Type.IsNativeIntegerType)
				{
					SpecialType specialType = CS$<>8__locals1.input.Type.SpecialType;
					ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> immutableArray;
					if (specialType != SpecialType.System_IntPtr)
					{
						if (specialType != SpecialType.System_UIntPtr)
						{
							throw ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.input.Type);
						}
						CS$<>8__locals1.input = this._factory.Convert(this._factory.SpecialType(SpecialType.System_UInt64), CS$<>8__locals1.input);
						immutableArray = CS$<>8__locals1.node.Cases.SelectAsArray<ValueTuple<ConstantValue, LabelSymbol>, ValueTuple<ConstantValue, LabelSymbol>>(([TupleElementNames(new string[] { "value", "label" })] ValueTuple<ConstantValue, LabelSymbol> p) => new ValueTuple<ConstantValue, LabelSymbol>(ConstantValue.Create((ulong)p.Item1.UInt32Value), p.Item2));
					}
					else
					{
						CS$<>8__locals1.input = this._factory.Convert(this._factory.SpecialType(SpecialType.System_Int64), CS$<>8__locals1.input);
						immutableArray = CS$<>8__locals1.node.Cases.SelectAsArray<ValueTuple<ConstantValue, LabelSymbol>, ValueTuple<ConstantValue, LabelSymbol>>(([TupleElementNames(new string[] { "value", "label" })] ValueTuple<ConstantValue, LabelSymbol> p) => new ValueTuple<ConstantValue, LabelSymbol>(ConstantValue.Create((long)p.Item1.Int32Value), p.Item2));
					}
					BoundSwitchDispatch boundSwitchDispatch2 = new BoundSwitchDispatch(CS$<>8__locals1.node.Syntax, CS$<>8__locals1.input, immutableArray, CS$<>8__locals1.defaultLabel, null, false);
					this._loweredDecisionDag.Add(boundSwitchDispatch2);
					return;
				}
				BinaryOperatorKind binaryOperatorKind;
				switch (CS$<>8__locals1.input.Type.SpecialType)
				{
				case SpecialType.System_Decimal:
					binaryOperatorKind = BinaryOperatorKind.DecimalLessThanOrEqual;
					break;
				case SpecialType.System_Single:
					binaryOperatorKind = BinaryOperatorKind.FloatLessThanOrEqual;
					break;
				case SpecialType.System_Double:
					binaryOperatorKind = BinaryOperatorKind.DoubleLessThanOrEqual;
					break;
				default:
					throw ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.input.Type.SpecialType);
				}
				LocalRewriter.DecisionDagRewriter.<>c__DisplayClass19_1 CS$<>8__locals2;
				CS$<>8__locals2.lessThanOrEqualOperator = binaryOperatorKind;
				CS$<>8__locals2.cases = CS$<>8__locals1.node.Cases.Sort(new LocalRewriter.DecisionDagRewriter.CasesComparer(CS$<>8__locals1.input.Type));
				this.<LowerSwitchDispatchNode>g__lowerFloatDispatch|19_2(0, CS$<>8__locals2.cases.Length, ref CS$<>8__locals1, ref CS$<>8__locals2);
			}

			// Token: 0x060085FB RID: 34299 RVA: 0x001E5DFC File Offset: 0x001E3FFC
			private void EnsureStringHashFunction(int labelsCount, SyntaxNode syntaxNode)
			{
				PEModuleBuilder emitModule = this._localRewriter.EmitModule;
				if (emitModule == null)
				{
					return;
				}
				if (!SwitchStringJumpTableEmitter.ShouldGenerateHashTableSwitch(emitModule, labelsCount))
				{
					return;
				}
				PrivateImplementationDetails privateImplClass = emitModule.GetPrivateImplClass(syntaxNode, this._localRewriter._diagnostics.DiagnosticBag);
				if (privateImplClass.GetMethod("ComputeStringHash") != null)
				{
					return;
				}
				Symbol specialTypeMember = this._localRewriter._compilation.GetSpecialTypeMember(SpecialMember.System_String__Chars);
				if (specialTypeMember == null || specialTypeMember.HasUseSiteError)
				{
					return;
				}
				TypeSymbol typeSymbol = this._factory.SpecialType(SpecialType.System_UInt32);
				TypeSymbol typeSymbol2 = this._factory.SpecialType(SpecialType.System_String);
				SynthesizedStringSwitchHashMethod synthesizedStringSwitchHashMethod = new SynthesizedStringSwitchHashMethod(emitModule.SourceModule, privateImplClass, typeSymbol, typeSymbol2);
				privateImplClass.TryAddSynthesizedMethod(synthesizedStringSwitchHashMethod.GetCciAdapter());
			}

			// Token: 0x060085FC RID: 34300 RVA: 0x001E5EA4 File Offset: 0x001E40A4
			private void LowerWhenClause(BoundWhenDecisionDagNode whenClause)
			{
				BoundLeafDecisionDagNode boundLeafDecisionDagNode = (BoundLeafDecisionDagNode)whenClause.WhenTrue;
				LabelSymbol dagNodeLabel = this.GetDagNodeLabel(whenClause);
				ArrayBuilder<BoundStatement> arrayBuilder = this.BuilderForSection(whenClause.Syntax);
				arrayBuilder.Add(this._factory.Label(dagNodeLabel));
				foreach (BoundPatternBinding boundPatternBinding in whenClause.Bindings)
				{
					BoundExpression boundExpression = this._localRewriter.VisitExpression(boundPatternBinding.VariableAccess);
					BoundExpression temp = this._tempAllocator.GetTemp(boundPatternBinding.TempContainingValue);
					if (boundExpression != temp)
					{
						arrayBuilder.Add(this._factory.Assignment(boundExpression, temp, false));
					}
				}
				BoundDecisionDagNode whenFalse = whenClause.WhenFalse;
				LabelSymbol dagNodeLabel2 = this.GetDagNodeLabel(boundLeafDecisionDagNode);
				if (whenClause.WhenExpression != null && whenClause.WhenExpression.ConstantValue != ConstantValue.True)
				{
					this._factory.Syntax = whenClause.Syntax;
					BoundStatement boundStatement = this._factory.ConditionalGoto(this._localRewriter.VisitExpression(whenClause.WhenExpression), dagNodeLabel2, true);
					if (base.GenerateInstrumentation && !whenClause.WhenExpression.WasCompilerGenerated)
					{
						boundStatement = this._localRewriter._instrumenter.InstrumentSwitchWhenClauseConditionalGotoBody(whenClause.WhenExpression, boundStatement);
					}
					arrayBuilder.Add(boundStatement);
					BoundStatement boundStatement2 = this._factory.Goto(this.GetDagNodeLabel(whenFalse));
					arrayBuilder.Add(base.GenerateInstrumentation ? this._factory.HiddenSequencePoint(boundStatement2) : boundStatement2);
					return;
				}
				arrayBuilder.Add(this._factory.Goto(dagNodeLabel2));
			}

			// Token: 0x060085FD RID: 34301 RVA: 0x001E6034 File Offset: 0x001E4234
			private void LowerDecisionDagNode(BoundDecisionDagNode node, BoundDecisionDagNode nextNode)
			{
				this._factory.Syntax = node.Syntax;
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = node as BoundEvaluationDecisionDagNode;
				if (boundEvaluationDecisionDagNode == null)
				{
					BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode == null)
					{
						throw ExceptionUtilities.UnexpectedValue(node.Kind);
					}
					BoundExpression boundExpression = base.LowerTest(boundTestDecisionDagNode.Test);
					this.GenerateTest(boundExpression, boundTestDecisionDagNode.WhenTrue, boundTestDecisionDagNode.WhenFalse, nextNode);
					return;
				}
				else
				{
					BoundExpression boundExpression2 = base.LowerEvaluation(boundEvaluationDecisionDagNode.Evaluation);
					this._loweredDecisionDag.Add(this._factory.ExpressionStatement(boundExpression2));
					if (base.GenerateInstrumentation)
					{
						this._loweredDecisionDag.Add(this._factory.HiddenSequencePoint(null));
					}
					if (nextNode != boundEvaluationDecisionDagNode.Next)
					{
						this._loweredDecisionDag.Add(this._factory.Goto(this.GetDagNodeLabel(boundEvaluationDecisionDagNode.Next)));
						return;
					}
					return;
				}
			}

			// Token: 0x060085FE RID: 34302 RVA: 0x001E610D File Offset: 0x001E430D
			[CompilerGenerated]
			private void <ComputeLabelSet>g__notePredecessor|4_0(BoundDecisionDagNode successor, ref LocalRewriter.DecisionDagRewriter.<>c__DisplayClass4_0 A_2)
			{
				if (successor != null && !A_2.hasPredecessor.Add(successor))
				{
					this.GetDagNodeLabel(successor);
				}
			}

			// Token: 0x060085FF RID: 34303 RVA: 0x001E6128 File Offset: 0x001E4328
			[CompilerGenerated]
			private bool <GenerateSwitchDispatch>g__canGenerateSwitchDispatch|12_0(BoundDecisionDagNode node)
			{
				BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
				if (boundTestDecisionDagNode != null)
				{
					BoundTestDecisionDagNode boundTestDecisionDagNode2 = boundTestDecisionDagNode.WhenFalse as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode2 != null)
					{
						return this.<GenerateSwitchDispatch>g__canDispatch|12_1(boundTestDecisionDagNode, boundTestDecisionDagNode2);
					}
					BoundTestDecisionDagNode boundTestDecisionDagNode3 = boundTestDecisionDagNode.WhenTrue as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode3 != null)
					{
						BoundTestDecisionDagNode boundTestDecisionDagNode4 = boundTestDecisionDagNode;
						return this.<GenerateSwitchDispatch>g__canDispatch|12_1(boundTestDecisionDagNode4, boundTestDecisionDagNode3);
					}
				}
				return false;
			}

			// Token: 0x06008600 RID: 34304 RVA: 0x001E6174 File Offset: 0x001E4374
			[CompilerGenerated]
			private bool <GenerateSwitchDispatch>g__canDispatch|12_1(BoundTestDecisionDagNode test1, BoundTestDecisionDagNode test2)
			{
				if (this._dagNodeLabels.ContainsKey(test2))
				{
					return false;
				}
				BoundDagTest test3 = test1.Test;
				BoundDagTest test4 = test2.Test;
				return (test3 is BoundDagValueTest || test3 is BoundDagRelationalTest) && (test4 is BoundDagValueTest || test4 is BoundDagRelationalTest) && test3.Input.Equals(test4.Input);
			}

			// Token: 0x06008601 RID: 34305 RVA: 0x001E61DC File Offset: 0x001E43DC
			[CompilerGenerated]
			[return: TupleElementNames(new string[] { "whenTrueCases", "whenFalseCases", "value", "label", "value", "label" })]
			internal static ValueTuple<ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>>, ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>>> <PushEqualityTestsIntoTree>g__splitCases|15_0([TupleElementNames(new string[] { "value", "label" })] ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> cases, BinaryOperatorKind op, ConstantValue value, ref LocalRewriter.DecisionDagRewriter.<>c__DisplayClass15_0 A_3)
			{
				ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>> instance = ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>>.GetInstance();
				ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>> instance2 = ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>>.GetInstance();
				op = op.Operator();
				foreach (ValueTuple<ConstantValue, LabelSymbol> valueTuple in cases)
				{
					(A_3.fac.Related(op, valueTuple.Item1, value) ? instance : instance2).Add(valueTuple);
				}
				return new ValueTuple<ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>>, ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>>>(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree());
			}

			// Token: 0x06008602 RID: 34306 RVA: 0x001E6248 File Offset: 0x001E4448
			[CompilerGenerated]
			private void <LowerSwitchDispatchNode>g__lowerFloatDispatch|19_2(int firstIndex, int count, ref LocalRewriter.DecisionDagRewriter.<>c__DisplayClass19_0 A_3, ref LocalRewriter.DecisionDagRewriter.<>c__DisplayClass19_1 A_4)
			{
				if (count <= 3)
				{
					int i = firstIndex;
					int num = firstIndex + count;
					while (i < num)
					{
						this._loweredDecisionDag.Add(this._factory.ConditionalGoto(base.MakeValueTest(A_3.node.Syntax, A_3.input, A_4.cases[i].Item1), A_4.cases[i].Item2, true));
						i++;
					}
					this._loweredDecisionDag.Add(this._factory.Goto(A_3.defaultLabel));
					return;
				}
				int num2 = count / 2;
				GeneratedLabelSymbol generatedLabelSymbol = this._factory.GenerateLabel("greaterThanMidpoint");
				this._loweredDecisionDag.Add(this._factory.ConditionalGoto(base.MakeRelationalTest(A_3.node.Syntax, A_3.input, A_4.lessThanOrEqualOperator, A_4.cases[firstIndex + num2 - 1].Item1), generatedLabelSymbol, false));
				this.<LowerSwitchDispatchNode>g__lowerFloatDispatch|19_2(firstIndex, num2, ref A_3, ref A_4);
				this._loweredDecisionDag.Add(this._factory.Label(generatedLabelSymbol));
				this.<LowerSwitchDispatchNode>g__lowerFloatDispatch|19_2(firstIndex + num2, count - num2, ref A_3, ref A_4);
			}

			// Token: 0x040022B5 RID: 8885
			private ArrayBuilder<BoundStatement> _loweredDecisionDag;

			// Token: 0x040022B6 RID: 8886
			protected readonly PooledDictionary<BoundDecisionDagNode, LabelSymbol> _dagNodeLabels = PooledDictionary<BoundDecisionDagNode, LabelSymbol>.GetInstance();

			// Token: 0x020009DD RID: 2525
			protected sealed class WhenClauseMightAssignPatternVariableWalker : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
			{
				// Token: 0x0600936E RID: 37742 RVA: 0x00202C84 File Offset: 0x00200E84
				public bool MightAssignSomething(BoundExpression expr)
				{
					if (expr == null)
					{
						return false;
					}
					this._mightAssignSomething = false;
					this.Visit(expr);
					return this._mightAssignSomething;
				}

				// Token: 0x0600936F RID: 37743 RVA: 0x00202CA0 File Offset: 0x00200EA0
				public override BoundNode Visit(BoundNode node)
				{
					BoundExpression boundExpression = node as BoundExpression;
					if (boundExpression != null && boundExpression.ConstantValue != null)
					{
						return null;
					}
					if (!this._mightAssignSomething)
					{
						return base.Visit(node);
					}
					return null;
				}

				// Token: 0x06009370 RID: 37744 RVA: 0x00202CD4 File Offset: 0x00200ED4
				public override BoundNode VisitCall(BoundCall node)
				{
					if (node.Method.MethodKind == MethodKind.LocalFunction || !node.ArgumentRefKindsOpt.IsDefault || LocalRewriter.DecisionDagRewriter.WhenClauseMightAssignPatternVariableWalker.MethodMayMutateReceiver(node.ReceiverOpt, node.Method))
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitCall(node);
					}
					return null;
				}

				// Token: 0x06009371 RID: 37745 RVA: 0x00202D28 File Offset: 0x00200F28
				private static bool MethodMayMutateReceiver(BoundExpression receiver, MethodSymbol method)
				{
					if (method != null && !method.IsStatic && !method.IsEffectivelyReadOnly)
					{
						TypeSymbol type = receiver.Type;
						if (type != null && !type.IsReferenceType)
						{
							return !method.ContainingType.SpecialType.IsPrimitiveRecursiveStruct();
						}
					}
					return false;
				}

				// Token: 0x06009372 RID: 37746 RVA: 0x00202D7A File Offset: 0x00200F7A
				public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
				{
					if (LocalRewriter.DecisionDagRewriter.WhenClauseMightAssignPatternVariableWalker.MethodMayMutateReceiver(node.ReceiverOpt, node.PropertySymbol.GetMethod))
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitPropertyAccess(node);
					}
					return null;
				}

				// Token: 0x06009373 RID: 37747 RVA: 0x00202DA6 File Offset: 0x00200FA6
				public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
				{
					this._mightAssignSomething = true;
					return null;
				}

				// Token: 0x06009374 RID: 37748 RVA: 0x00202DB0 File Offset: 0x00200FB0
				public override BoundNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node)
				{
					this._mightAssignSomething = true;
					return null;
				}

				// Token: 0x06009375 RID: 37749 RVA: 0x00202DBA File Offset: 0x00200FBA
				public override BoundNode VisitConversion(BoundConversion node)
				{
					this.<VisitConversion>g__visitConversion|8_0(node.Conversion);
					if (!this._mightAssignSomething)
					{
						base.VisitConversion(node);
					}
					return null;
				}

				// Token: 0x06009376 RID: 37750 RVA: 0x00202DD9 File Offset: 0x00200FD9
				public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
				{
					MethodSymbol methodOpt = node.MethodOpt;
					if (methodOpt != null && methodOpt.MethodKind == MethodKind.LocalFunction)
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitDelegateCreationExpression(node);
					}
					return null;
				}

				// Token: 0x06009377 RID: 37751 RVA: 0x00202E05 File Offset: 0x00201005
				public override BoundNode VisitAddressOfOperator(BoundAddressOfOperator node)
				{
					this._mightAssignSomething = true;
					return null;
				}

				// Token: 0x06009378 RID: 37752 RVA: 0x00202E0F File Offset: 0x0020100F
				public override BoundNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node)
				{
					this._mightAssignSomething = true;
					return null;
				}

				// Token: 0x06009379 RID: 37753 RVA: 0x00202E19 File Offset: 0x00201019
				public override BoundNode VisitIncrementOperator(BoundIncrementOperator node)
				{
					this._mightAssignSomething = true;
					return null;
				}

				// Token: 0x0600937A RID: 37754 RVA: 0x00202E24 File Offset: 0x00201024
				public override BoundNode VisitDynamicInvocation(BoundDynamicInvocation node)
				{
					if (!node.ArgumentRefKindsOpt.IsDefault)
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitDynamicInvocation(node);
					}
					return null;
				}

				// Token: 0x0600937B RID: 37755 RVA: 0x00202E54 File Offset: 0x00201054
				public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
				{
					if (!node.ArgumentRefKindsOpt.IsDefault)
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitObjectCreationExpression(node);
					}
					return null;
				}

				// Token: 0x0600937C RID: 37756 RVA: 0x00202E84 File Offset: 0x00201084
				public override BoundNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node)
				{
					if (!node.ArgumentRefKindsOpt.IsDefault)
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitDynamicObjectCreationExpression(node);
					}
					return null;
				}

				// Token: 0x0600937D RID: 37757 RVA: 0x00202EB4 File Offset: 0x002010B4
				public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
				{
					if (!node.ArgumentRefKindsOpt.IsDefault)
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitObjectInitializerMember(node);
					}
					return null;
				}

				// Token: 0x0600937E RID: 37758 RVA: 0x00202EE4 File Offset: 0x002010E4
				public override BoundNode VisitIndexerAccess(BoundIndexerAccess node)
				{
					if (!node.ArgumentRefKindsOpt.IsDefault || LocalRewriter.DecisionDagRewriter.WhenClauseMightAssignPatternVariableWalker.MethodMayMutateReceiver(node.ReceiverOpt, node.Indexer.GetMethod))
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitIndexerAccess(node);
					}
					return null;
				}

				// Token: 0x0600937F RID: 37759 RVA: 0x00202F30 File Offset: 0x00201130
				public override BoundNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node)
				{
					if (!node.ArgumentRefKindsOpt.IsDefault)
					{
						this._mightAssignSomething = true;
					}
					else
					{
						base.VisitDynamicIndexerAccess(node);
					}
					return null;
				}

				// Token: 0x06009381 RID: 37761 RVA: 0x00202F68 File Offset: 0x00201168
				[CompilerGenerated]
				private void <VisitConversion>g__visitConversion|8_0(Conversion conversion)
				{
					if (conversion.Kind == ConversionKind.MethodGroup)
					{
						if (conversion.Method.MethodKind == MethodKind.LocalFunction)
						{
							this._mightAssignSomething = true;
							return;
						}
					}
					else if (!conversion.UnderlyingConversions.IsDefault)
					{
						foreach (Conversion conversion2 in conversion.UnderlyingConversions)
						{
							this.<VisitConversion>g__visitConversion|8_0(conversion2);
							if (this._mightAssignSomething)
							{
								return;
							}
						}
					}
				}

				// Token: 0x040029FA RID: 10746
				private bool _mightAssignSomething;
			}

			// Token: 0x020009DE RID: 2526
			private sealed class CasesComparer : IComparer<ValueTuple<ConstantValue, LabelSymbol>>
			{
				// Token: 0x06009382 RID: 37762 RVA: 0x00202FDC File Offset: 0x002011DC
				public CasesComparer(TypeSymbol type)
				{
					this._fac = ValueSetFactory.ForType(type);
				}

				// Token: 0x06009383 RID: 37763 RVA: 0x00202FF0 File Offset: 0x002011F0
				int IComparer<ValueTuple<ConstantValue, LabelSymbol>>.Compare([TupleElementNames(new string[] { "value", "label" })] ValueTuple<ConstantValue, LabelSymbol> left, [TupleElementNames(new string[] { "value", "label" })] ValueTuple<ConstantValue, LabelSymbol> right)
				{
					ConstantValue item = left.Item1;
					ConstantValue item2 = right.Item1;
					if (LocalRewriter.DecisionDagRewriter.CasesComparer.<System.Collections.Generic.IComparer<(Microsoft.CodeAnalysis.ConstantValuevalue,Microsoft.CodeAnalysis.CSharp.Symbols.LabelSymbollabel)>.Compare>g__isNaN|2_0(item))
					{
						return 1;
					}
					if (LocalRewriter.DecisionDagRewriter.CasesComparer.<System.Collections.Generic.IComparer<(Microsoft.CodeAnalysis.ConstantValuevalue,Microsoft.CodeAnalysis.CSharp.Symbols.LabelSymbollabel)>.Compare>g__isNaN|2_0(item2))
					{
						return -1;
					}
					if (!this._fac.Related(BinaryOperatorKind.LessThanOrEqual, item, item2))
					{
						return 1;
					}
					if (!this._fac.Related(BinaryOperatorKind.LessThanOrEqual, item2, item))
					{
						return -1;
					}
					return 0;
				}

				// Token: 0x06009384 RID: 37764 RVA: 0x0020304C File Offset: 0x0020124C
				[CompilerGenerated]
				internal static bool <System.Collections.Generic.IComparer<(Microsoft.CodeAnalysis.ConstantValuevalue,Microsoft.CodeAnalysis.CSharp.Symbols.LabelSymbollabel)>.Compare>g__isNaN|2_0(ConstantValue value)
				{
					return (value.Discriminator == ConstantValueTypeDiscriminator.Single || value.Discriminator == ConstantValueTypeDiscriminator.Double) && double.IsNaN(value.DoubleValue);
				}

				// Token: 0x040029FB RID: 10747
				private readonly IValueSetFactory _fac;
			}

			// Token: 0x020009DF RID: 2527
			private abstract class ValueDispatchNode
			{
				// Token: 0x1700293D RID: 10557
				// (get) Token: 0x06009385 RID: 37765 RVA: 0x0020306F File Offset: 0x0020126F
				protected virtual int Height
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x06009386 RID: 37766 RVA: 0x00203072 File Offset: 0x00201272
				public ValueDispatchNode(SyntaxNode syntax)
				{
					this.Syntax = syntax;
				}

				// Token: 0x040029FC RID: 10748
				public readonly SyntaxNode Syntax;

				// Token: 0x02000A13 RID: 2579
				internal sealed class SwitchDispatch : LocalRewriter.DecisionDagRewriter.ValueDispatchNode
				{
					// Token: 0x06009427 RID: 37927 RVA: 0x00203E74 File Offset: 0x00202074
					public SwitchDispatch(SyntaxNode syntax, [TupleElementNames(new string[] { "value", "label" })] ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> dispatches, LabelSymbol otherwise)
						: base(syntax)
					{
						this.Cases = dispatches;
						this.Otherwise = otherwise;
					}

					// Token: 0x06009428 RID: 37928 RVA: 0x00203E8C File Offset: 0x0020208C
					public override string ToString()
					{
						return "[" + string.Join<ConstantValue>(",", this.Cases.Select<ValueTuple<ConstantValue, LabelSymbol>, ConstantValue>(([TupleElementNames(new string[] { "value", "label" })] ValueTuple<ConstantValue, LabelSymbol> c) => c.Item1)) + "]";
					}

					// Token: 0x04002A7A RID: 10874
					[TupleElementNames(new string[] { "value", "label" })]
					public readonly ImmutableArray<ValueTuple<ConstantValue, LabelSymbol>> Cases;

					// Token: 0x04002A7B RID: 10875
					public readonly LabelSymbol Otherwise;
				}

				// Token: 0x02000A14 RID: 2580
				internal sealed class LeafDispatchNode : LocalRewriter.DecisionDagRewriter.ValueDispatchNode
				{
					// Token: 0x06009429 RID: 37929 RVA: 0x00203EDC File Offset: 0x002020DC
					public LeafDispatchNode(SyntaxNode syntax, LabelSymbol Label)
						: base(syntax)
					{
						this.Label = Label;
					}

					// Token: 0x0600942A RID: 37930 RVA: 0x00203EEC File Offset: 0x002020EC
					public override string ToString()
					{
						return "Leaf";
					}

					// Token: 0x04002A7C RID: 10876
					public readonly LabelSymbol Label;
				}

				// Token: 0x02000A15 RID: 2581
				internal sealed class RelationalDispatch : LocalRewriter.DecisionDagRewriter.ValueDispatchNode
				{
					// Token: 0x17002949 RID: 10569
					// (get) Token: 0x0600942B RID: 37931 RVA: 0x00203EF3 File Offset: 0x002020F3
					protected override int Height
					{
						get
						{
							return this._height;
						}
					}

					// Token: 0x1700294A RID: 10570
					// (get) Token: 0x0600942C RID: 37932 RVA: 0x00203EFB File Offset: 0x002020FB
					// (set) Token: 0x0600942D RID: 37933 RVA: 0x00203F03 File Offset: 0x00202103
					private LocalRewriter.DecisionDagRewriter.ValueDispatchNode Left { get; set; }

					// Token: 0x1700294B RID: 10571
					// (get) Token: 0x0600942E RID: 37934 RVA: 0x00203F0C File Offset: 0x0020210C
					// (set) Token: 0x0600942F RID: 37935 RVA: 0x00203F14 File Offset: 0x00202114
					private LocalRewriter.DecisionDagRewriter.ValueDispatchNode Right { get; set; }

					// Token: 0x06009430 RID: 37936 RVA: 0x00203F1D File Offset: 0x0020211D
					private RelationalDispatch(SyntaxNode syntax, ConstantValue value, BinaryOperatorKind op, LocalRewriter.DecisionDagRewriter.ValueDispatchNode left, LocalRewriter.DecisionDagRewriter.ValueDispatchNode right)
						: base(syntax)
					{
						this.Value = value;
						this.Operator = op;
						this.WithLeftAndRight(left, right);
					}

					// Token: 0x1700294C RID: 10572
					// (get) Token: 0x06009431 RID: 37937 RVA: 0x00203F3F File Offset: 0x0020213F
					public LocalRewriter.DecisionDagRewriter.ValueDispatchNode WhenTrue
					{
						get
						{
							if (!LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.IsReversed(this.Operator))
							{
								return this.Left;
							}
							return this.Right;
						}
					}

					// Token: 0x1700294D RID: 10573
					// (get) Token: 0x06009432 RID: 37938 RVA: 0x00203F5B File Offset: 0x0020215B
					public LocalRewriter.DecisionDagRewriter.ValueDispatchNode WhenFalse
					{
						get
						{
							if (!LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.IsReversed(this.Operator))
							{
								return this.Right;
							}
							return this.Left;
						}
					}

					// Token: 0x06009433 RID: 37939 RVA: 0x00203F78 File Offset: 0x00202178
					public override string ToString()
					{
						return string.Format("RelationalDispatch.{0}({1} {2} {3} {4})", new object[]
						{
							this.Height,
							this.Left,
							this.Operator.Operator(),
							this.Value,
							this.Right
						});
					}

					// Token: 0x06009434 RID: 37940 RVA: 0x00203FD4 File Offset: 0x002021D4
					private static bool IsReversed(BinaryOperatorKind op)
					{
						BinaryOperatorKind binaryOperatorKind = op.Operator();
						return binaryOperatorKind == BinaryOperatorKind.GreaterThan || binaryOperatorKind == BinaryOperatorKind.GreaterThanOrEqual;
					}

					// Token: 0x06009435 RID: 37941 RVA: 0x00204008 File Offset: 0x00202208
					private LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch WithLeftAndRight(LocalRewriter.DecisionDagRewriter.ValueDispatchNode left, LocalRewriter.DecisionDagRewriter.ValueDispatchNode right)
					{
						int height = left.Height;
						int height2 = right.Height;
						this.Left = left;
						this.Right = right;
						this._height = Math.Max(height, height2) + 1;
						return this;
					}

					// Token: 0x06009436 RID: 37942 RVA: 0x00204044 File Offset: 0x00202244
					public LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch WithTrueAndFalseChildren(LocalRewriter.DecisionDagRewriter.ValueDispatchNode whenTrue, LocalRewriter.DecisionDagRewriter.ValueDispatchNode whenFalse)
					{
						if (whenTrue == this.WhenTrue && whenFalse == this.WhenFalse)
						{
							return this;
						}
						LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode;
						LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode2;
						if (!LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.IsReversed(this.Operator))
						{
							valueDispatchNode = whenTrue;
							valueDispatchNode2 = whenFalse;
						}
						else
						{
							valueDispatchNode = whenFalse;
							valueDispatchNode2 = whenTrue;
						}
						return this.WithLeftAndRight(valueDispatchNode, valueDispatchNode2);
					}

					// Token: 0x06009437 RID: 37943 RVA: 0x0020408C File Offset: 0x0020228C
					public static LocalRewriter.DecisionDagRewriter.ValueDispatchNode CreateBalanced(SyntaxNode syntax, ConstantValue value, BinaryOperatorKind op, LocalRewriter.DecisionDagRewriter.ValueDispatchNode whenTrue, LocalRewriter.DecisionDagRewriter.ValueDispatchNode whenFalse)
					{
						LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode;
						LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode2;
						if (!LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.IsReversed(op))
						{
							valueDispatchNode = whenTrue;
							valueDispatchNode2 = whenFalse;
						}
						else
						{
							valueDispatchNode = whenFalse;
							valueDispatchNode2 = whenTrue;
						}
						return LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.CreateBalancedCore(syntax, value, op, valueDispatchNode, valueDispatchNode2);
					}

					// Token: 0x06009438 RID: 37944 RVA: 0x002040C0 File Offset: 0x002022C0
					private static LocalRewriter.DecisionDagRewriter.ValueDispatchNode CreateBalancedCore(SyntaxNode syntax, ConstantValue value, BinaryOperatorKind op, LocalRewriter.DecisionDagRewriter.ValueDispatchNode left, LocalRewriter.DecisionDagRewriter.ValueDispatchNode right)
					{
						if (left.Height > right.Height + 1)
						{
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch = (LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch)left;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode = LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.CreateBalancedCore(syntax, value, op, relationalDispatch.Right, right);
							SyntaxNode syntaxNode = relationalDispatch.Syntax;
							ConstantValue constantValue = relationalDispatch.Value;
							BinaryOperatorKind binaryOperatorKind = relationalDispatch.Operator;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode left2 = relationalDispatch.Left;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode2 = valueDispatchNode;
							syntax = syntaxNode;
							value = constantValue;
							op = binaryOperatorKind;
							left = left2;
							right = valueDispatchNode2;
						}
						else if (right.Height > left.Height + 1)
						{
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch2 = (LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch)right;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode3 = LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch.CreateBalancedCore(syntax, value, op, left, relationalDispatch2.Left);
							SyntaxNode syntaxNode = relationalDispatch2.Syntax;
							ConstantValue constantValue = relationalDispatch2.Value;
							BinaryOperatorKind binaryOperatorKind = relationalDispatch2.Operator;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode2 = valueDispatchNode3;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode right2 = relationalDispatch2.Right;
							syntax = syntaxNode;
							value = constantValue;
							op = binaryOperatorKind;
							left = valueDispatchNode2;
							right = right2;
						}
						if (left.Height == right.Height + 2)
						{
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch3 = (LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch)left;
							if (relationalDispatch3.Left.Height == right.Height)
							{
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch4 = relationalDispatch3;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode left3 = relationalDispatch4.Left;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch5 = (LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch)relationalDispatch4.Right;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode left4 = relationalDispatch5.Left;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode right3 = relationalDispatch5.Right;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode4 = right;
								return relationalDispatch5.WithLeftAndRight(relationalDispatch4.WithLeftAndRight(left3, left4), new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch(syntax, value, op, right3, valueDispatchNode4));
							}
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch6 = relationalDispatch3;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode left5 = relationalDispatch6.Left;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode right4 = relationalDispatch6.Right;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode5 = right;
							return relationalDispatch6.WithLeftAndRight(left5, new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch(syntax, value, op, right4, valueDispatchNode5));
						}
						else
						{
							if (right.Height != left.Height + 2)
							{
								return new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch(syntax, value, op, left, right);
							}
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch7 = (LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch)right;
							if (relationalDispatch7.Right.Height == left.Height)
							{
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode6 = left;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch8 = relationalDispatch7;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch9 = (LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch)relationalDispatch8.Left;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode left6 = relationalDispatch9.Left;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode right5 = relationalDispatch9.Right;
								LocalRewriter.DecisionDagRewriter.ValueDispatchNode right6 = relationalDispatch8.Right;
								return relationalDispatch9.WithLeftAndRight(new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch(syntax, value, op, valueDispatchNode6, left6), relationalDispatch8.WithLeftAndRight(right5, right6));
							}
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode valueDispatchNode7 = left;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch relationalDispatch10 = relationalDispatch7;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode left7 = relationalDispatch10.Left;
							LocalRewriter.DecisionDagRewriter.ValueDispatchNode right7 = relationalDispatch10.Right;
							return relationalDispatch10.WithLeftAndRight(new LocalRewriter.DecisionDagRewriter.ValueDispatchNode.RelationalDispatch(syntax, value, op, valueDispatchNode7, left7), right7);
						}
					}

					// Token: 0x04002A7D RID: 10877
					private int _height;

					// Token: 0x04002A7E RID: 10878
					public readonly ConstantValue Value;

					// Token: 0x04002A7F RID: 10879
					public readonly BinaryOperatorKind Operator;
				}
			}
		}

		// Token: 0x0200072A RID: 1834
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private abstract class PatternLocalRewriter
		{
			// Token: 0x06008603 RID: 34307 RVA: 0x001E636A File Offset: 0x001E456A
			public PatternLocalRewriter(SyntaxNode node, LocalRewriter localRewriter, bool generateInstrumentation)
			{
				this._localRewriter = localRewriter;
				this._factory = localRewriter._factory;
				this.GenerateInstrumentation = generateInstrumentation;
				this._tempAllocator = new LocalRewriter.PatternLocalRewriter.DagTempAllocator(this._factory, node, generateInstrumentation);
			}

			// Token: 0x170026C6 RID: 9926
			// (get) Token: 0x06008604 RID: 34308 RVA: 0x001E639F File Offset: 0x001E459F
			protected bool GenerateInstrumentation { get; }

			// Token: 0x06008605 RID: 34309 RVA: 0x001E63A7 File Offset: 0x001E45A7
			public void Free()
			{
				this._tempAllocator.Free();
			}

			// Token: 0x06008606 RID: 34310 RVA: 0x001E63B4 File Offset: 0x001E45B4
			protected BoundExpression LowerEvaluation(BoundDagEvaluation evaluation)
			{
				BoundExpression boundExpression = this._tempAllocator.GetTemp(evaluation.Input);
				BoundDagFieldEvaluation boundDagFieldEvaluation = evaluation as BoundDagFieldEvaluation;
				if (boundDagFieldEvaluation != null)
				{
					FieldSymbol field = boundDagFieldEvaluation.Field;
					BoundDagTemp boundDagTemp = new BoundDagTemp(boundDagFieldEvaluation.Syntax, field.Type, boundDagFieldEvaluation);
					BoundExpression temp = this._tempAllocator.GetTemp(boundDagTemp);
					BoundExpression boundExpression2 = this._localRewriter.MakeFieldAccess(boundDagFieldEvaluation.Syntax, boundExpression, field, null, LookupResultKind.Viable, field.Type, null);
					boundExpression2.WasCompilerGenerated = true;
					return this._factory.AssignmentExpression(temp, boundExpression2, false);
				}
				BoundDagPropertyEvaluation boundDagPropertyEvaluation = evaluation as BoundDagPropertyEvaluation;
				if (boundDagPropertyEvaluation != null)
				{
					PropertySymbol property = boundDagPropertyEvaluation.Property;
					BoundDagTemp boundDagTemp2 = new BoundDagTemp(boundDagPropertyEvaluation.Syntax, property.Type, boundDagPropertyEvaluation);
					BoundExpression temp2 = this._tempAllocator.GetTemp(boundDagTemp2);
					return this._factory.AssignmentExpression(temp2, this._factory.Property(boundExpression, property), false);
				}
				BoundDagDeconstructEvaluation boundDagDeconstructEvaluation = evaluation as BoundDagDeconstructEvaluation;
				if (boundDagDeconstructEvaluation != null)
				{
					MethodSymbol deconstructMethod = boundDagDeconstructEvaluation.DeconstructMethod;
					LocalRewriter.PatternLocalRewriter.<>c__DisplayClass9_0 CS$<>8__locals1;
					CS$<>8__locals1.refKindBuilder = ArrayBuilder<RefKind>.GetInstance();
					CS$<>8__locals1.argBuilder = ArrayBuilder<BoundExpression>.GetInstance();
					BoundExpression boundExpression3;
					int num;
					if (deconstructMethod.IsStatic)
					{
						boundExpression3 = this._factory.Type(deconstructMethod.ContainingType);
						LocalRewriter.PatternLocalRewriter.<LowerEvaluation>g__addArg|9_0(deconstructMethod.ParameterRefKinds[0], boundExpression, ref CS$<>8__locals1);
						num = 1;
					}
					else
					{
						boundExpression3 = boundExpression;
						num = 0;
					}
					for (int i = num; i < deconstructMethod.ParameterCount; i++)
					{
						ParameterSymbol parameterSymbol = deconstructMethod.Parameters[i];
						BoundDagTemp boundDagTemp3 = new BoundDagTemp(boundDagDeconstructEvaluation.Syntax, parameterSymbol.Type, boundDagDeconstructEvaluation, i - num, false);
						LocalRewriter.PatternLocalRewriter.<LowerEvaluation>g__addArg|9_0(RefKind.Out, this._tempAllocator.GetTemp(boundDagTemp3), ref CS$<>8__locals1);
					}
					return this._factory.Call(boundExpression3, deconstructMethod, CS$<>8__locals1.refKindBuilder.ToImmutableAndFree(), CS$<>8__locals1.argBuilder.ToImmutableAndFree());
				}
				BoundDagTypeEvaluation boundDagTypeEvaluation = evaluation as BoundDagTypeEvaluation;
				if (boundDagTypeEvaluation != null)
				{
					TypeSymbol typeSymbol = boundExpression.Type;
					if (typeSymbol.IsDynamic())
					{
						typeSymbol = this._factory.SpecialType(SpecialType.System_Object);
						boundExpression = this._factory.Convert(typeSymbol, boundExpression);
					}
					TypeSymbol type = boundDagTypeEvaluation.Type;
					BoundDagTemp boundDagTemp4 = new BoundDagTemp(boundDagTypeEvaluation.Syntax, type, boundDagTypeEvaluation);
					BoundExpression temp3 = this._tempAllocator.GetTemp(boundDagTemp4);
					CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = this._localRewriter.GetNewCompoundUseSiteInfo();
					Conversion conversion = this._factory.Compilation.Conversions.ClassifyBuiltInConversion(typeSymbol, temp3.Type, ref newCompoundUseSiteInfo);
					this._localRewriter._diagnostics.Add(boundDagTypeEvaluation.Syntax, newCompoundUseSiteInfo);
					BoundExpression boundExpression4;
					if (conversion.Exists)
					{
						MethodSymbol methodSymbol;
						if (conversion.Kind == ConversionKind.ExplicitNullable && typeSymbol.GetNullableUnderlyingType().Equals(temp3.Type, TypeCompareKind.AllIgnoreOptions) && this._localRewriter.TryGetNullableMethod(boundDagTypeEvaluation.Syntax, typeSymbol, SpecialMember.System_Nullable_T_GetValueOrDefault, out methodSymbol))
						{
							boundExpression4 = this._factory.Call(boundExpression, methodSymbol);
						}
						else
						{
							boundExpression4 = this._factory.Convert(type, boundExpression, conversion, false);
						}
					}
					else
					{
						boundExpression4 = this._factory.As(boundExpression, type);
					}
					return this._factory.AssignmentExpression(temp3, boundExpression4, false);
				}
				BoundDagIndexEvaluation boundDagIndexEvaluation = evaluation as BoundDagIndexEvaluation;
				if (boundDagIndexEvaluation == null)
				{
					throw ExceptionUtilities.UnexpectedValue(evaluation);
				}
				TypeSymbol returnType = boundDagIndexEvaluation.Property.GetMethod.ReturnType;
				BoundDagTemp boundDagTemp5 = new BoundDagTemp(boundDagIndexEvaluation.Syntax, returnType, boundDagIndexEvaluation);
				BoundExpression temp4 = this._tempAllocator.GetTemp(boundDagTemp5);
				return this._factory.AssignmentExpression(temp4, this._factory.Call(boundExpression, boundDagIndexEvaluation.Property.GetMethod, this._factory.Literal(boundDagIndexEvaluation.Index)), false);
			}

			// Token: 0x06008607 RID: 34311 RVA: 0x001E6750 File Offset: 0x001E4950
			protected BoundExpression LowerTest(BoundDagTest test)
			{
				this._factory.Syntax = test.Syntax;
				BoundExpression temp = this._tempAllocator.GetTemp(test.Input);
				BoundDagNonNullTest boundDagNonNullTest = test as BoundDagNonNullTest;
				if (boundDagNonNullTest != null)
				{
					return this.MakeNullCheck(boundDagNonNullTest.Syntax, temp, temp.Type.IsNullableType() ? BinaryOperatorKind.NullableNullNotEqual : BinaryOperatorKind.NotEqual);
				}
				BoundDagTypeTest boundDagTypeTest = test as BoundDagTypeTest;
				if (boundDagTypeTest != null)
				{
					return this._factory.Is(temp, boundDagTypeTest.Type);
				}
				BoundDagExplicitNullTest boundDagExplicitNullTest = test as BoundDagExplicitNullTest;
				if (boundDagExplicitNullTest != null)
				{
					return this.MakeNullCheck(boundDagExplicitNullTest.Syntax, temp, temp.Type.IsNullableType() ? BinaryOperatorKind.NullableNullEqual : BinaryOperatorKind.Equal);
				}
				BoundDagValueTest boundDagValueTest = test as BoundDagValueTest;
				if (boundDagValueTest != null)
				{
					return this.MakeValueTest(boundDagValueTest.Syntax, temp, boundDagValueTest.Value);
				}
				BoundDagRelationalTest boundDagRelationalTest = test as BoundDagRelationalTest;
				if (boundDagRelationalTest == null)
				{
					throw ExceptionUtilities.UnexpectedValue(test);
				}
				return this.MakeRelationalTest(boundDagRelationalTest.Syntax, temp, boundDagRelationalTest.OperatorKind, boundDagRelationalTest.Value);
			}

			// Token: 0x06008608 RID: 34312 RVA: 0x001E6858 File Offset: 0x001E4A58
			private BoundExpression MakeNullCheck(SyntaxNode syntax, BoundExpression rewrittenExpr, BinaryOperatorKind operatorKind)
			{
				if (rewrittenExpr.Type.IsPointerOrFunctionPointer())
				{
					TypeSymbol typeSymbol = this._factory.SpecialType(SpecialType.System_Object);
					PointerTypeSymbol pointerTypeSymbol = new PointerTypeSymbol(TypeWithAnnotations.Create(this._factory.SpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
					return this._localRewriter.MakeBinaryOperator(syntax, operatorKind, this._factory.Convert(pointerTypeSymbol, rewrittenExpr), this._factory.Convert(pointerTypeSymbol, new BoundLiteral(syntax, ConstantValue.Null, typeSymbol)), this._factory.SpecialType(SpecialType.System_Boolean), null, false, false, null);
				}
				return this._localRewriter.MakeNullCheck(syntax, rewrittenExpr, operatorKind);
			}

			// Token: 0x06008609 RID: 34313 RVA: 0x001E68F4 File Offset: 0x001E4AF4
			protected BoundExpression MakeValueTest(SyntaxNode syntax, BoundExpression input, ConstantValue value)
			{
				BinaryOperatorKind binaryOperatorKind = Binder.RelationalOperatorType(input.Type.EnumUnderlyingTypeOrSelf());
				BinaryOperatorKind binaryOperatorKind2 = BinaryOperatorKind.Equal | binaryOperatorKind;
				return this.MakeRelationalTest(syntax, input, binaryOperatorKind2, value);
			}

			// Token: 0x0600860A RID: 34314 RVA: 0x001E6924 File Offset: 0x001E4B24
			protected BoundExpression MakeRelationalTest(SyntaxNode syntax, BoundExpression input, BinaryOperatorKind operatorKind, ConstantValue value)
			{
				if ((input.Type.SpecialType == SpecialType.System_Double && double.IsNaN(value.DoubleValue)) || (input.Type.SpecialType == SpecialType.System_Single && float.IsNaN(value.SingleValue)))
				{
					return this._factory.MakeIsNotANumberTest(input);
				}
				BoundExpression boundExpression = this._localRewriter.MakeLiteral(syntax, value, input.Type, null);
				TypeSymbol typeSymbol = input.Type.EnumUnderlyingTypeOrSelf();
				if (operatorKind.OperandTypes() == BinaryOperatorKind.Int && typeSymbol.SpecialType != SpecialType.System_Int32)
				{
					typeSymbol = this._factory.SpecialType(SpecialType.System_Int32);
					input = this._factory.Convert(typeSymbol, input);
					boundExpression = this._factory.Convert(typeSymbol, boundExpression);
				}
				return this._localRewriter.MakeBinaryOperator(this._factory.Syntax, operatorKind, input, boundExpression, this._factory.SpecialType(SpecialType.System_Boolean), null, false, false, null);
			}

			// Token: 0x0600860B RID: 34315 RVA: 0x001E6A04 File Offset: 0x001E4C04
			protected bool TryLowerTypeTestAndCast(BoundDagTest test, BoundDagEvaluation evaluation, [NotNullWhen(true)] out BoundExpression sideEffect, [NotNullWhen(true)] out BoundExpression testExpression)
			{
				CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = this._localRewriter.GetNewCompoundUseSiteInfo();
				BoundDagTypeTest boundDagTypeTest = test as BoundDagTypeTest;
				if (boundDagTypeTest != null)
				{
					BoundDagTypeEvaluation boundDagTypeEvaluation = evaluation as BoundDagTypeEvaluation;
					if (boundDagTypeEvaluation != null && boundDagTypeTest.Type.IsReferenceType && boundDagTypeEvaluation.Type.Equals(boundDagTypeTest.Type, TypeCompareKind.AllIgnoreOptions) && boundDagTypeEvaluation.Input == boundDagTypeTest.Input)
					{
						BoundExpression temp = this._tempAllocator.GetTemp(test.Input);
						BoundExpression temp2 = this._tempAllocator.GetTemp(new BoundDagTemp(evaluation.Syntax, boundDagTypeEvaluation.Type, evaluation));
						sideEffect = this._factory.AssignmentExpression(temp2, this._factory.As(temp, boundDagTypeEvaluation.Type), false);
						testExpression = this._factory.ObjectNotEqual(temp2, this._factory.Null(temp2.Type));
						return true;
					}
				}
				BoundDagNonNullTest boundDagNonNullTest = test as BoundDagNonNullTest;
				if (boundDagNonNullTest != null)
				{
					BoundDagTypeEvaluation boundDagTypeEvaluation2 = evaluation as BoundDagTypeEvaluation;
					if (boundDagTypeEvaluation2 != null)
					{
						Conversion conversion = this._factory.Compilation.Conversions.ClassifyBuiltInConversion(test.Input.Type, boundDagTypeEvaluation2.Type, ref newCompoundUseSiteInfo);
						if ((conversion.IsIdentity || conversion.Kind == ConversionKind.ImplicitReference || conversion.IsBoxing) && boundDagTypeEvaluation2.Input == boundDagNonNullTest.Input)
						{
							BoundExpression temp3 = this._tempAllocator.GetTemp(test.Input);
							TypeSymbol type = boundDagTypeEvaluation2.Type;
							BoundExpression temp4 = this._tempAllocator.GetTemp(new BoundDagTemp(evaluation.Syntax, type, evaluation));
							sideEffect = this._factory.AssignmentExpression(temp4, this._factory.Convert(type, temp3), false);
							testExpression = this._factory.ObjectNotEqual(temp4, this._factory.Null(type));
							this._localRewriter._diagnostics.Add(test.Syntax, newCompoundUseSiteInfo);
							return true;
						}
					}
				}
				BoundExpression boundExpression;
				testExpression = (boundExpression = null);
				sideEffect = boundExpression;
				return false;
			}

			// Token: 0x0600860C RID: 34316 RVA: 0x001E6BF8 File Offset: 0x001E4DF8
			protected BoundDecisionDag ShareTempsAndEvaluateInput(BoundExpression loweredInput, BoundDecisionDag decisionDag, Action<BoundExpression> addCode, out BoundExpression savedInputExpression)
			{
				bool flag = decisionDag.TopologicallySortedNodes.Any<BoundDecisionDagNode>(delegate(BoundDecisionDagNode node)
				{
					BoundWhenDecisionDagNode boundWhenDecisionDagNode2 = node as BoundWhenDecisionDagNode;
					if (boundWhenDecisionDagNode2 != null)
					{
						BoundExpression whenExpression = boundWhenDecisionDagNode2.WhenExpression;
						if (whenExpression != null)
						{
							return whenExpression.ConstantValue == null;
						}
					}
					return false;
				});
				BoundDagTemp boundDagTemp = BoundDagTemp.ForOriginalInput(loweredInput);
				if ((loweredInput.Kind == BoundKind.Local || loweredInput.Kind == BoundKind.Parameter) && loweredInput.GetRefKind() == RefKind.None && !flag)
				{
					this._tempAllocator.TrySetTemp(boundDagTemp, loweredInput);
				}
				foreach (BoundDecisionDagNode boundDecisionDagNode in decisionDag.TopologicallySortedNodes)
				{
					BoundWhenDecisionDagNode boundWhenDecisionDagNode = boundDecisionDagNode as BoundWhenDecisionDagNode;
					if (boundWhenDecisionDagNode != null)
					{
						foreach (BoundPatternBinding boundPatternBinding in boundWhenDecisionDagNode.Bindings)
						{
							if (boundPatternBinding.VariableAccess is BoundLocal)
							{
								this._tempAllocator.TrySetTemp(boundPatternBinding.TempContainingValue, boundPatternBinding.VariableAccess);
							}
						}
					}
				}
				if (loweredInput.Type.IsTupleType && !loweredInput.Type.OriginalDefinition.Equals(this._factory.Compilation.GetWellKnownType(WellKnownType.System_ValueTuple_TRest)) && loweredInput.Syntax.Kind() == SyntaxKind.TupleExpression)
				{
					BoundObjectCreationExpression boundObjectCreationExpression = loweredInput as BoundObjectCreationExpression;
					if (boundObjectCreationExpression != null)
					{
						if (!decisionDag.TopologicallySortedNodes.Any<BoundDecisionDagNode>((BoundDecisionDagNode n) => LocalRewriter.PatternLocalRewriter.<ShareTempsAndEvaluateInput>g__usesOriginalInput|15_2(n)))
						{
							return this.RewriteTupleInput(decisionDag, boundObjectCreationExpression, addCode, !flag, out savedInputExpression);
						}
					}
				}
				BoundExpression temp = this._tempAllocator.GetTemp(boundDagTemp);
				savedInputExpression = temp;
				if (temp != loweredInput)
				{
					addCode.Invoke(this._factory.AssignmentExpression(temp, loweredInput, false));
				}
				return decisionDag;
			}

			// Token: 0x0600860D RID: 34317 RVA: 0x001E6DA0 File Offset: 0x001E4FA0
			private BoundDecisionDag RewriteTupleInput(BoundDecisionDag decisionDag, BoundObjectCreationExpression loweredInput, Action<BoundExpression> addCode, bool canShareInputs, out BoundExpression savedInputExpression)
			{
				LocalRewriter.PatternLocalRewriter.<>c__DisplayClass16_0 CS$<>8__locals1;
				CS$<>8__locals1.canShareInputs = canShareInputs;
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.addCode = addCode;
				int length = loweredInput.Arguments.Length;
				BoundDagTemp boundDagTemp = BoundDagTemp.ForOriginalInput(loweredInput.Syntax, loweredInput.Type);
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance(loweredInput.Arguments.Length);
				for (int i = 0; i < length; i++)
				{
					FieldSymbol correspondingTupleField = loweredInput.Type.TupleElements[i].CorrespondingTupleField;
					BoundExpression boundExpression = loweredInput.Arguments[i];
					BoundDagFieldEvaluation boundDagFieldEvaluation = new BoundDagFieldEvaluation(boundExpression.Syntax, correspondingTupleField, boundDagTemp, false);
					BoundDagTemp boundDagTemp2 = new BoundDagTemp(boundExpression.Syntax, boundExpression.Type, boundDagFieldEvaluation);
					this.<RewriteTupleInput>g__storeToTemp|16_0(boundDagTemp2, boundExpression, ref CS$<>8__locals1);
					instance.Add(this._tempAllocator.GetTemp(boundDagTemp2));
				}
				BoundDecisionDag boundDecisionDag = decisionDag.Rewrite(new Func<BoundDecisionDagNode, Func<BoundDecisionDagNode, BoundDecisionDagNode>, BoundDecisionDagNode>(LocalRewriter.PatternLocalRewriter.<RewriteTupleInput>g__makeReplacement|16_1));
				savedInputExpression = loweredInput.Update(loweredInput.Constructor, instance.ToImmutableAndFree(), loweredInput.ArgumentNamesOpt, loweredInput.ArgumentRefKindsOpt, loweredInput.Expanded, loweredInput.ArgsToParamsOpt, loweredInput.DefaultArguments, loweredInput.ConstantValueOpt, loweredInput.InitializerExpressionOpt, loweredInput.Type);
				return boundDecisionDag;
			}

			// Token: 0x0600860E RID: 34318 RVA: 0x001E6EDF File Offset: 0x001E50DF
			[CompilerGenerated]
			internal static void <LowerEvaluation>g__addArg|9_0(RefKind refKind, BoundExpression expression, ref LocalRewriter.PatternLocalRewriter.<>c__DisplayClass9_0 A_2)
			{
				A_2.refKindBuilder.Add(refKind);
				A_2.argBuilder.Add(expression);
			}

			// Token: 0x0600860F RID: 34319 RVA: 0x001E6EFC File Offset: 0x001E50FC
			[CompilerGenerated]
			internal static bool <ShareTempsAndEvaluateInput>g__usesOriginalInput|15_2(BoundDecisionDagNode node)
			{
				BoundWhenDecisionDagNode boundWhenDecisionDagNode = node as BoundWhenDecisionDagNode;
				if (boundWhenDecisionDagNode != null)
				{
					return boundWhenDecisionDagNode.Bindings.Any<BoundPatternBinding>((BoundPatternBinding b) => b.TempContainingValue.IsOriginalInput);
				}
				BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
				if (boundTestDecisionDagNode != null)
				{
					return boundTestDecisionDagNode.Test.Input.IsOriginalInput;
				}
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = node as BoundEvaluationDecisionDagNode;
				if (boundEvaluationDecisionDagNode == null)
				{
					return false;
				}
				BoundDagFieldEvaluation boundDagFieldEvaluation = boundEvaluationDecisionDagNode.Evaluation as BoundDagFieldEvaluation;
				if (boundDagFieldEvaluation != null)
				{
					return boundDagFieldEvaluation.Input.IsOriginalInput && !boundDagFieldEvaluation.Field.IsTupleElement();
				}
				return boundEvaluationDecisionDagNode.Evaluation.Input.IsOriginalInput;
			}

			// Token: 0x06008610 RID: 34320 RVA: 0x001E6FA4 File Offset: 0x001E51A4
			[CompilerGenerated]
			private void <RewriteTupleInput>g__storeToTemp|16_0(BoundDagTemp temp, BoundExpression expr, ref LocalRewriter.PatternLocalRewriter.<>c__DisplayClass16_0 A_3)
			{
				if (!A_3.canShareInputs || (expr.Kind != BoundKind.Parameter && expr.Kind != BoundKind.Local) || !this._tempAllocator.TrySetTemp(temp, expr))
				{
					BoundExpression temp2 = this._tempAllocator.GetTemp(temp);
					A_3.addCode.Invoke(this._factory.AssignmentExpression(temp2, expr, false));
				}
			}

			// Token: 0x06008611 RID: 34321 RVA: 0x001E7004 File Offset: 0x001E5204
			[CompilerGenerated]
			internal static BoundDecisionDagNode <RewriteTupleInput>g__makeReplacement|16_1(BoundDecisionDagNode node, Func<BoundDecisionDagNode, BoundDecisionDagNode> replacement)
			{
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = node as BoundEvaluationDecisionDagNode;
				if (boundEvaluationDecisionDagNode == null)
				{
					if (!(node is BoundTestDecisionDagNode))
					{
					}
				}
				else
				{
					BoundDagFieldEvaluation boundDagFieldEvaluation = boundEvaluationDecisionDagNode.Evaluation as BoundDagFieldEvaluation;
					if (boundDagFieldEvaluation != null && boundDagFieldEvaluation.Input.IsOriginalInput)
					{
						FieldSymbol field = boundDagFieldEvaluation.Field;
						if (field.CorrespondingTupleField != null)
						{
							int tupleElementIndex = field.TupleElementIndex;
							return replacement.Invoke(boundEvaluationDecisionDagNode.Next);
						}
					}
				}
				return BoundDecisionDag.TrivialReplacement(node, replacement);
			}

			// Token: 0x040022B7 RID: 8887
			protected readonly LocalRewriter _localRewriter;

			// Token: 0x040022B8 RID: 8888
			protected readonly SyntheticBoundNodeFactory _factory;

			// Token: 0x040022B9 RID: 8889
			protected readonly LocalRewriter.PatternLocalRewriter.DagTempAllocator _tempAllocator;

			// Token: 0x020009E6 RID: 2534
			public sealed class DagTempAllocator
			{
				// Token: 0x0600938F RID: 37775 RVA: 0x0020313F File Offset: 0x0020133F
				public DagTempAllocator(SyntheticBoundNodeFactory factory, SyntaxNode node, bool generateSequencePoints)
				{
					this._factory = factory;
					this._node = node;
					this._generateSequencePoints = generateSequencePoints;
				}

				// Token: 0x06009390 RID: 37776 RVA: 0x00203172 File Offset: 0x00201372
				public void Free()
				{
					this._temps.Free();
					this._map.Free();
				}

				// Token: 0x06009391 RID: 37777 RVA: 0x0020318C File Offset: 0x0020138C
				public BoundExpression GetTemp(BoundDagTemp dagTemp)
				{
					BoundExpression boundExpression;
					if (!this._map.TryGetValue(dagTemp, ref boundExpression))
					{
						SynthesizedLocalKind synthesizedLocalKind = (this._generateSequencePoints ? SynthesizedLocalKind.SwitchCasePatternMatching : SynthesizedLocalKind.LoweringTemp);
						LocalSymbol localSymbol = this._factory.SynthesizedLocal(dagTemp.Type, this._node, false, RefKind.None, synthesizedLocalKind);
						boundExpression = this._factory.Local(localSymbol);
						this._map.Add(dagTemp, boundExpression);
						this._temps.Add(localSymbol);
					}
					return boundExpression;
				}

				// Token: 0x06009392 RID: 37778 RVA: 0x002031FA File Offset: 0x002013FA
				public bool TrySetTemp(BoundDagTemp dagTemp, BoundExpression translation)
				{
					if (!this._map.ContainsKey(dagTemp))
					{
						this._map.Add(dagTemp, translation);
						return true;
					}
					return false;
				}

				// Token: 0x06009393 RID: 37779 RVA: 0x0020321A File Offset: 0x0020141A
				public ImmutableArray<LocalSymbol> AllTemps()
				{
					return this._temps.ToImmutableArray<LocalSymbol>();
				}

				// Token: 0x04002A0D RID: 10765
				private readonly SyntheticBoundNodeFactory _factory;

				// Token: 0x04002A0E RID: 10766
				private readonly PooledDictionary<BoundDagTemp, BoundExpression> _map = PooledDictionary<BoundDagTemp, BoundExpression>.GetInstance();

				// Token: 0x04002A0F RID: 10767
				private readonly ArrayBuilder<LocalSymbol> _temps = ArrayBuilder<LocalSymbol>.GetInstance();

				// Token: 0x04002A10 RID: 10768
				private readonly SyntaxNode _node;

				// Token: 0x04002A11 RID: 10769
				private readonly bool _generateSequencePoints;
			}
		}

		// Token: 0x0200072B RID: 1835
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private abstract class BaseSwitchLocalRewriter : LocalRewriter.DecisionDagRewriter
		{
			// Token: 0x06008612 RID: 34322 RVA: 0x001E7074 File Offset: 0x001E5274
			protected override ArrayBuilder<BoundStatement> BuilderForSection(SyntaxNode whenClauseSyntax)
			{
				SwitchLabelSyntax switchLabelSyntax = whenClauseSyntax as SwitchLabelSyntax;
				SyntaxNode syntaxNode = ((switchLabelSyntax != null) ? switchLabelSyntax.Parent : whenClauseSyntax);
				ArrayBuilder<BoundStatement> arrayBuilder;
				if (!this._switchArms.TryGetValue(syntaxNode, ref arrayBuilder) || arrayBuilder == null)
				{
					throw new InvalidOperationException();
				}
				return arrayBuilder;
			}

			// Token: 0x06008613 RID: 34323 RVA: 0x001E70B0 File Offset: 0x001E52B0
			protected BaseSwitchLocalRewriter(SyntaxNode node, LocalRewriter localRewriter, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxNode> arms, bool generateInstrumentation)
				: base(node, localRewriter, generateInstrumentation)
			{
				foreach (SyntaxNode syntaxNode in arms)
				{
					ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
					if (base.GenerateInstrumentation)
					{
						instance.Add(this._factory.HiddenSequencePoint(null));
					}
					this._switchArms.Add(syntaxNode, instance);
				}
			}

			// Token: 0x06008614 RID: 34324 RVA: 0x001E711A File Offset: 0x001E531A
			protected new void Free()
			{
				this._switchArms.Free();
				base.Free();
			}

			// Token: 0x06008615 RID: 34325 RVA: 0x001E7130 File Offset: 0x001E5330
			[return: TupleElementNames(new string[] { "loweredDag", "switchSections" })]
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 1, 1, 0, 1 })]
			protected ValueTuple<ImmutableArray<BoundStatement>, ImmutableDictionary<SyntaxNode, ImmutableArray<BoundStatement>>> LowerDecisionDag(BoundDecisionDag decisionDag)
			{
				ImmutableArray<BoundStatement> immutableArray = base.LowerDecisionDagCore(decisionDag);
				ImmutableDictionary<SyntaxNode, ImmutableArray<BoundStatement>> immutableDictionary = this._switchArms.ToImmutableDictionary<KeyValuePair<SyntaxNode, ArrayBuilder<BoundStatement>>, SyntaxNode, ImmutableArray<BoundStatement>>((KeyValuePair<SyntaxNode, ArrayBuilder<BoundStatement>> kv) => kv.Key, (KeyValuePair<SyntaxNode, ArrayBuilder<BoundStatement>> kv) => kv.Value.ToImmutableAndFree());
				this._switchArms.Clear();
				return new ValueTuple<ImmutableArray<BoundStatement>, ImmutableDictionary<SyntaxNode, ImmutableArray<BoundStatement>>>(immutableArray, immutableDictionary);
			}

			// Token: 0x040022BB RID: 8891
			private readonly PooledDictionary<SyntaxNode, ArrayBuilder<BoundStatement>> _switchArms = PooledDictionary<SyntaxNode, ArrayBuilder<BoundStatement>>.GetInstance();
		}

		// Token: 0x0200072C RID: 1836
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private enum ConditionalAccessLoweringKind
		{
			// Token: 0x040022BD RID: 8893
			LoweredConditionalAccess,
			// Token: 0x040022BE RID: 8894
			Conditional,
			// Token: 0x040022BF RID: 8895
			ConditionalCaptureReceiverByVal
		}

		// Token: 0x0200072D RID: 1837
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class DeconstructionSideEffects
		{
			// Token: 0x06008616 RID: 34326 RVA: 0x001E719F File Offset: 0x001E539F
			internal static LocalRewriter.DeconstructionSideEffects GetInstance()
			{
				return new LocalRewriter.DeconstructionSideEffects
				{
					init = ArrayBuilder<BoundExpression>.GetInstance(),
					deconstructions = ArrayBuilder<BoundExpression>.GetInstance(),
					conversions = ArrayBuilder<BoundExpression>.GetInstance(),
					assignments = ArrayBuilder<BoundExpression>.GetInstance()
				};
			}

			// Token: 0x06008617 RID: 34327 RVA: 0x001E71D4 File Offset: 0x001E53D4
			internal void Consolidate()
			{
				this.init.AddRange(this.deconstructions);
				this.init.AddRange(this.conversions);
				this.init.AddRange(this.assignments);
				this.deconstructions.Free();
				this.conversions.Free();
				this.assignments.Free();
			}

			// Token: 0x06008618 RID: 34328 RVA: 0x001E7235 File Offset: 0x001E5435
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			internal BoundExpression PopLast()
			{
				if (this.init.Count == 0)
				{
					return null;
				}
				BoundExpression boundExpression = this.init.Last();
				this.init.RemoveLast();
				return boundExpression;
			}

			// Token: 0x06008619 RID: 34329 RVA: 0x001E725C File Offset: 0x001E545C
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			internal ImmutableArray<BoundExpression> ToImmutableAndFree()
			{
				return this.init.ToImmutableAndFree();
			}

			// Token: 0x0600861A RID: 34330 RVA: 0x001E7269 File Offset: 0x001E5469
			internal void Free()
			{
				this.init.Free();
			}

			// Token: 0x040022C0 RID: 8896
			internal ArrayBuilder<BoundExpression> init;

			// Token: 0x040022C1 RID: 8897
			internal ArrayBuilder<BoundExpression> deconstructions;

			// Token: 0x040022C2 RID: 8898
			internal ArrayBuilder<BoundExpression> conversions;

			// Token: 0x040022C3 RID: 8899
			internal ArrayBuilder<BoundExpression> assignments;
		}

		// Token: 0x0200072E RID: 1838
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private enum EventAssignmentKind
		{
			// Token: 0x040022C5 RID: 8901
			Assignment,
			// Token: 0x040022C6 RID: 8902
			Addition,
			// Token: 0x040022C7 RID: 8903
			Subtraction
		}

		// Token: 0x0200072F RID: 1839
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class IsPatternExpressionGeneralLocalRewriter : LocalRewriter.DecisionDagRewriter
		{
			// Token: 0x0600861C RID: 34332 RVA: 0x001E727E File Offset: 0x001E547E
			public IsPatternExpressionGeneralLocalRewriter(SyntaxNode node, LocalRewriter localRewriter)
				: base(node, localRewriter, false)
			{
			}

			// Token: 0x0600861D RID: 34333 RVA: 0x001E7294 File Offset: 0x001E5494
			protected override ArrayBuilder<BoundStatement> BuilderForSection(SyntaxNode section)
			{
				return this._statements;
			}

			// Token: 0x0600861E RID: 34334 RVA: 0x001E729C File Offset: 0x001E549C
			public new void Free()
			{
				base.Free();
				this._statements.Free();
			}

			// Token: 0x0600861F RID: 34335 RVA: 0x001E72B0 File Offset: 0x001E54B0
			internal BoundExpression LowerGeneralIsPattern(BoundIsPatternExpression node)
			{
				this._factory.Syntax = node.Syntax;
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
				BoundExpression boundExpression = this._localRewriter.VisitExpression(node.Expression);
				BoundExpression boundExpression2;
				BoundDecisionDag boundDecisionDag = base.ShareTempsIfPossibleAndEvaluateInput(node.DecisionDag, boundExpression, instance, out boundExpression2);
				ImmutableArray<BoundStatement> immutableArray = base.LowerDecisionDagCore(boundDecisionDag);
				instance.Add(this._factory.Block(immutableArray));
				LocalSymbol localSymbol = this._factory.SynthesizedLocal(node.Type, node.Syntax, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				LabelSymbol labelSymbol = this._factory.GenerateLabel("afterIsPatternExpression");
				LabelSymbol whenTrueLabel = node.WhenTrueLabel;
				LabelSymbol whenFalseLabel = node.WhenFalseLabel;
				if (this._statements.Count != 0)
				{
					instance.Add(this._factory.Block(this._statements.ToArray()));
				}
				instance.Add(this._factory.Label(whenTrueLabel));
				instance.Add(this._factory.Assignment(this._factory.Local(localSymbol), this._factory.Literal(true), false));
				instance.Add(this._factory.Goto(labelSymbol));
				instance.Add(this._factory.Label(whenFalseLabel));
				instance.Add(this._factory.Assignment(this._factory.Local(localSymbol), this._factory.Literal(false), false));
				instance.Add(this._factory.Label(labelSymbol));
				this._localRewriter._needsSpilling = true;
				return this._factory.SpillSequence(this._tempAllocator.AllTemps().Add(localSymbol), instance.ToImmutableAndFree(), this._factory.Local(localSymbol));
			}

			// Token: 0x040022C8 RID: 8904
			private readonly ArrayBuilder<BoundStatement> _statements = ArrayBuilder<BoundStatement>.GetInstance();
		}

		// Token: 0x02000730 RID: 1840
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class IsPatternExpressionLinearLocalRewriter : LocalRewriter.PatternLocalRewriter
		{
			// Token: 0x06008620 RID: 34336 RVA: 0x001E745B File Offset: 0x001E565B
			public IsPatternExpressionLinearLocalRewriter(BoundIsPatternExpression node, LocalRewriter localRewriter)
				: base(node.Syntax, localRewriter, false)
			{
				this._conjunctBuilder = ArrayBuilder<BoundExpression>.GetInstance();
				this._sideEffectBuilder = ArrayBuilder<BoundExpression>.GetInstance();
			}

			// Token: 0x06008621 RID: 34337 RVA: 0x001E7481 File Offset: 0x001E5681
			public new void Free()
			{
				this._conjunctBuilder.Free();
				this._sideEffectBuilder.Free();
				base.Free();
			}

			// Token: 0x06008622 RID: 34338 RVA: 0x001E74A0 File Offset: 0x001E56A0
			private void AddConjunct(BoundExpression test)
			{
				TypeSymbol type = test.Type;
				if (type == null || type.IsErrorType())
				{
					return;
				}
				if (this._sideEffectBuilder.Count != 0)
				{
					test = this._factory.Sequence(ImmutableArray<LocalSymbol>.Empty, this._sideEffectBuilder.ToImmutable(), test);
					this._sideEffectBuilder.Clear();
				}
				this._conjunctBuilder.Add(test);
			}

			// Token: 0x06008623 RID: 34339 RVA: 0x001E7504 File Offset: 0x001E5704
			private void LowerOneTest(BoundDagTest test, bool invert = false)
			{
				this._factory.Syntax = test.Syntax;
				BoundDagEvaluation boundDagEvaluation = test as BoundDagEvaluation;
				if (boundDagEvaluation != null)
				{
					BoundExpression boundExpression = base.LowerEvaluation(boundDagEvaluation);
					this._sideEffectBuilder.Add(boundExpression);
					return;
				}
				BoundExpression boundExpression2 = base.LowerTest(test);
				if (boundExpression2 != null)
				{
					if (invert)
					{
						boundExpression2 = this._factory.Not(boundExpression2);
					}
					this.AddConjunct(boundExpression2);
				}
			}

			// Token: 0x06008624 RID: 34340 RVA: 0x001E7564 File Offset: 0x001E5764
			public BoundExpression LowerIsPatternAsLinearTestSequence(BoundIsPatternExpression isPatternExpression, LabelSymbol whenTrueLabel, LabelSymbol whenFalseLabel)
			{
				BoundDecisionDag boundDecisionDag = isPatternExpression.DecisionDag;
				BoundExpression boundExpression = this._localRewriter.VisitExpression(isPatternExpression.Expression);
				BoundExpression boundExpression2;
				boundDecisionDag = base.ShareTempsAndEvaluateInput(boundExpression, boundDecisionDag, delegate(BoundExpression expr)
				{
					this._sideEffectBuilder.Add(expr);
				}, out boundExpression2);
				BoundDecisionDagNode rootNode = boundDecisionDag.RootNode;
				return this.ProduceLinearTestSequence(rootNode, whenTrueLabel, whenFalseLabel);
			}

			// Token: 0x06008625 RID: 34341 RVA: 0x001E75B4 File Offset: 0x001E57B4
			private BoundExpression ProduceLinearTestSequence(BoundDecisionDagNode node, LabelSymbol whenTrueLabel, LabelSymbol whenFalseLabel)
			{
				while (node.Kind != BoundKind.LeafDecisionDagNode && node.Kind != BoundKind.WhenDecisionDagNode)
				{
					BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = node as BoundEvaluationDecisionDagNode;
					if (boundEvaluationDecisionDagNode == null)
					{
						BoundTestDecisionDagNode boundTestDecisionDagNode = node as BoundTestDecisionDagNode;
						if (boundTestDecisionDagNode != null)
						{
							BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode2 = boundTestDecisionDagNode.WhenTrue as BoundEvaluationDecisionDagNode;
							BoundExpression boundExpression;
							BoundExpression boundExpression2;
							if (boundEvaluationDecisionDagNode2 != null && base.TryLowerTypeTestAndCast(boundTestDecisionDagNode.Test, boundEvaluationDecisionDagNode2.Evaluation, out boundExpression, out boundExpression2))
							{
								this._sideEffectBuilder.Add(boundExpression);
								this.AddConjunct(boundExpression2);
								node = boundEvaluationDecisionDagNode2.Next;
							}
							else
							{
								bool flag = LocalRewriter.IsFailureNode(boundTestDecisionDagNode.WhenTrue, whenFalseLabel);
								this.LowerOneTest(boundTestDecisionDagNode.Test, flag);
								node = (flag ? boundTestDecisionDagNode.WhenFalse : boundTestDecisionDagNode.WhenTrue);
							}
						}
					}
					else
					{
						this.LowerOneTest(boundEvaluationDecisionDagNode.Evaluation, false);
						node = boundEvaluationDecisionDagNode.Next;
					}
				}
				if (!(node is BoundLeafDecisionDagNode))
				{
					BoundWhenDecisionDagNode boundWhenDecisionDagNode = node as BoundWhenDecisionDagNode;
					if (boundWhenDecisionDagNode == null)
					{
						throw ExceptionUtilities.UnexpectedValue(node.Kind);
					}
					foreach (BoundPatternBinding boundPatternBinding in boundWhenDecisionDagNode.Bindings)
					{
						BoundExpression boundExpression3 = this._localRewriter.VisitExpression(boundPatternBinding.VariableAccess);
						BoundExpression temp = this._tempAllocator.GetTemp(boundPatternBinding.TempContainingValue);
						if (boundExpression3 != temp)
						{
							this._sideEffectBuilder.Add(this._factory.AssignmentExpression(boundExpression3, temp, false));
						}
					}
				}
				if (this._sideEffectBuilder.Count > 0 || this._conjunctBuilder.Count == 0)
				{
					this.AddConjunct(this._factory.Literal(true));
				}
				BoundExpression boundExpression4 = null;
				foreach (BoundExpression boundExpression5 in this._conjunctBuilder)
				{
					boundExpression4 = ((boundExpression4 == null) ? boundExpression5 : this._factory.LogicalAnd(boundExpression4, boundExpression5));
				}
				this._conjunctBuilder.Clear();
				ImmutableArray<LocalSymbol> immutableArray = this._tempAllocator.AllTemps();
				if (immutableArray.Length > 0)
				{
					boundExpression4 = this._factory.Sequence(immutableArray, ImmutableArray<BoundExpression>.Empty, boundExpression4);
				}
				return boundExpression4;
			}

			// Token: 0x040022C9 RID: 8905
			private readonly ArrayBuilder<BoundExpression> _sideEffectBuilder;

			// Token: 0x040022CA RID: 8906
			private readonly ArrayBuilder<BoundExpression> _conjunctBuilder;
		}

		// Token: 0x02000731 RID: 1841
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class SwitchStatementLocalRewriter : LocalRewriter.BaseSwitchLocalRewriter
		{
			// Token: 0x06008627 RID: 34343 RVA: 0x001E77CC File Offset: 0x001E59CC
			public static BoundStatement Rewrite(LocalRewriter localRewriter, BoundSwitchStatement node)
			{
				LocalRewriter.SwitchStatementLocalRewriter switchStatementLocalRewriter = new LocalRewriter.SwitchStatementLocalRewriter(node, localRewriter);
				BoundStatement boundStatement = switchStatementLocalRewriter.LowerSwitchStatement(node);
				switchStatementLocalRewriter.Free();
				return boundStatement;
			}

			// Token: 0x06008628 RID: 34344 RVA: 0x001E77F0 File Offset: 0x001E59F0
			protected override LabelSymbol GetDagNodeLabel(BoundDecisionDagNode dag)
			{
				LabelSymbol dagNodeLabel = base.GetDagNodeLabel(dag);
				BoundLeafDecisionDagNode boundLeafDecisionDagNode = dag as BoundLeafDecisionDagNode;
				if (boundLeafDecisionDagNode != null)
				{
					SyntaxNode parent = boundLeafDecisionDagNode.Syntax.Parent;
					if (parent != null && parent.Kind() == SyntaxKind.SwitchSection)
					{
						LabelSymbol labelSymbol;
						if (this._sectionLabels.TryGetValue(parent, ref labelSymbol))
						{
							return labelSymbol;
						}
						this._sectionLabels.Add(parent, dagNodeLabel);
					}
				}
				return dagNodeLabel;
			}

			// Token: 0x06008629 RID: 34345 RVA: 0x001E784C File Offset: 0x001E5A4C
			private SwitchStatementLocalRewriter(BoundSwitchStatement node, LocalRewriter localRewriter)
				: base(node.Syntax, localRewriter, node.SwitchSections.SelectAsArray<BoundSwitchSection, SyntaxNode>((BoundSwitchSection section) => section.Syntax), localRewriter.Instrument && !node.WasCompilerGenerated)
			{
			}

			// Token: 0x0600862A RID: 34346 RVA: 0x001E78B0 File Offset: 0x001E5AB0
			private BoundStatement LowerSwitchStatement(BoundSwitchStatement node)
			{
				this._factory.Syntax = node.Syntax;
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
				ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
				BoundExpression boundExpression = this._localRewriter.VisitExpression(node.Expression);
				if (!node.WasCompilerGenerated && this._localRewriter.Instrument)
				{
					BoundExpression boundExpression2 = this._localRewriter._instrumenter.InstrumentSwitchStatementExpression(node, boundExpression, this._factory);
					if (boundExpression.ConstantValue == null)
					{
						boundExpression = boundExpression2;
					}
					else
					{
						instance.Add(this._factory.ExpressionStatement(boundExpression2));
					}
				}
				instance2.AddRange(node.InnerLocals);
				BoundExpression boundExpression3;
				BoundDecisionDag boundDecisionDag = base.ShareTempsIfPossibleAndEvaluateInput(node.DecisionDag, boundExpression, instance, out boundExpression3);
				if (base.GenerateInstrumentation)
				{
					if (instance.Count == 0)
					{
						instance.Add(this._factory.NoOp(NoOpStatementFlavor.Default));
					}
					instance.Add(this._factory.HiddenSequencePoint(null));
				}
				ValueTuple<ImmutableArray<BoundStatement>, ImmutableDictionary<SyntaxNode, ImmutableArray<BoundStatement>>> valueTuple = base.LowerDecisionDag(boundDecisionDag);
				ImmutableArray<BoundStatement> item = valueTuple.Item1;
				ImmutableDictionary<SyntaxNode, ImmutableArray<BoundStatement>> item2 = valueTuple.Item2;
				instance.Add(this._factory.Block(item));
				foreach (BoundSwitchSection boundSwitchSection in node.SwitchSections)
				{
					this._factory.Syntax = boundSwitchSection.Syntax;
					ArrayBuilder<BoundStatement> instance3 = ArrayBuilder<BoundStatement>.GetInstance();
					instance3.AddRange(item2[boundSwitchSection.Syntax]);
					foreach (BoundSwitchLabel boundSwitchLabel in boundSwitchSection.SwitchLabels)
					{
						instance3.Add(this._factory.Label(boundSwitchLabel.Label));
					}
					instance3.AddRange(this._localRewriter.VisitList<BoundStatement>(boundSwitchSection.Statements));
					ImmutableArray<BoundStatement> immutableArray = instance3.ToImmutableAndFree();
					if (boundSwitchSection.Locals.IsEmpty)
					{
						instance.Add(this._factory.StatementList(immutableArray));
					}
					else
					{
						instance2.AddRange(boundSwitchSection.Locals);
						instance.Add(new BoundScope(boundSwitchSection.Syntax, boundSwitchSection.Locals, immutableArray, false));
					}
				}
				instance2.AddRange(this._tempAllocator.AllTemps());
				this._factory.Syntax = node.Syntax;
				if (base.GenerateInstrumentation)
				{
					instance.Add(this._factory.HiddenSequencePoint(null));
				}
				instance.Add(this._factory.Label(node.BreakLabel));
				BoundStatement boundStatement = this._factory.Block(instance2.ToImmutableAndFree(), node.InnerLocalFunctions, instance.ToImmutableAndFree());
				if (base.GenerateInstrumentation)
				{
					boundStatement = this._localRewriter._instrumenter.InstrumentSwitchStatement(node, boundStatement);
				}
				return boundStatement;
			}

			// Token: 0x040022CB RID: 8907
			private readonly Dictionary<SyntaxNode, LabelSymbol> _sectionLabels = PooledDictionary<SyntaxNode, LabelSymbol>.GetInstance();
		}

		// Token: 0x02000732 RID: 1842
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class SwitchExpressionLocalRewriter : LocalRewriter.BaseSwitchLocalRewriter
		{
			// Token: 0x0600862B RID: 34347 RVA: 0x001E7B58 File Offset: 0x001E5D58
			private SwitchExpressionLocalRewriter(BoundConvertedSwitchExpression node, LocalRewriter localRewriter)
				: base(node.Syntax, localRewriter, node.SwitchArms.SelectAsArray<BoundSwitchExpressionArm, SyntaxNode>((BoundSwitchExpressionArm arm) => arm.Syntax), !node.WasCompilerGenerated && localRewriter.Instrument)
			{
			}

			// Token: 0x0600862C RID: 34348 RVA: 0x001E7BB0 File Offset: 0x001E5DB0
			public static BoundExpression Rewrite(LocalRewriter localRewriter, BoundConvertedSwitchExpression node)
			{
				LocalRewriter.SwitchExpressionLocalRewriter switchExpressionLocalRewriter = new LocalRewriter.SwitchExpressionLocalRewriter(node, localRewriter);
				BoundExpression boundExpression = switchExpressionLocalRewriter.LowerSwitchExpression(node);
				switchExpressionLocalRewriter.Free();
				return boundExpression;
			}

			// Token: 0x0600862D RID: 34349 RVA: 0x001E7BD4 File Offset: 0x001E5DD4
			private BoundExpression LowerSwitchExpression(BoundConvertedSwitchExpression node)
			{
				bool flag = base.GenerateInstrumentation && this._localRewriter._compilation.Options.OptimizationLevel != OptimizationLevel.Release;
				this._factory.Syntax = node.Syntax;
				ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
				ArrayBuilder<LocalSymbol> instance2 = ArrayBuilder<LocalSymbol>.GetInstance();
				BoundExpression boundExpression = this._localRewriter.VisitExpression(node.Expression);
				BoundExpression boundExpression2;
				BoundDecisionDag boundDecisionDag = base.ShareTempsIfPossibleAndEvaluateInput(node.DecisionDag, boundExpression, instance, out boundExpression2);
				object obj = new object();
				object obj2 = new object();
				ValueTuple<ImmutableArray<BoundStatement>, ImmutableDictionary<SyntaxNode, ImmutableArray<BoundStatement>>> valueTuple = base.LowerDecisionDag(boundDecisionDag);
				ImmutableArray<BoundStatement> item = valueTuple.Item1;
				ImmutableDictionary<SyntaxNode, ImmutableArray<BoundStatement>> item2 = valueTuple.Item2;
				if (flag)
				{
					SwitchExpressionSyntax switchExpressionSyntax = (SwitchExpressionSyntax)node.Syntax;
					instance.Add(new BoundSavePreviousSequencePoint(switchExpressionSyntax, obj));
					int start = switchExpressionSyntax.SwitchKeyword.Span.Start;
					int end = switchExpressionSyntax.Span.End;
					TextSpan textSpan = new TextSpan(start, end - start);
					instance.Add(new BoundStepThroughSequencePoint(node.Syntax, textSpan));
					instance.Add(new BoundSavePreviousSequencePoint(switchExpressionSyntax, obj2));
				}
				instance.Add(this._factory.Block(item));
				LocalSymbol localSymbol = this._factory.SynthesizedLocal(node.Type, node.Syntax, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				LabelSymbol labelSymbol = this._factory.GenerateLabel("afterSwitchExpression");
				foreach (BoundSwitchExpressionArm boundSwitchExpressionArm in node.SwitchArms)
				{
					this._factory.Syntax = boundSwitchExpressionArm.Syntax;
					ArrayBuilder<BoundStatement> instance3 = ArrayBuilder<BoundStatement>.GetInstance();
					instance3.AddRange(item2[boundSwitchExpressionArm.Syntax]);
					instance3.Add(this._factory.Label(boundSwitchExpressionArm.Label));
					BoundExpression boundExpression3 = this._localRewriter.VisitExpression(boundSwitchExpressionArm.Value);
					if (base.GenerateInstrumentation)
					{
						boundExpression3 = this._localRewriter._instrumenter.InstrumentSwitchExpressionArmExpression(boundSwitchExpressionArm.Value, boundExpression3, this._factory);
					}
					instance3.Add(this._factory.Assignment(this._factory.Local(localSymbol), boundExpression3, false));
					instance3.Add(this._factory.Goto(labelSymbol));
					ImmutableArray<BoundStatement> immutableArray = instance3.ToImmutableAndFree();
					if (boundSwitchExpressionArm.Locals.IsEmpty)
					{
						instance.Add(this._factory.StatementList(immutableArray));
					}
					else
					{
						instance2.AddRange(boundSwitchExpressionArm.Locals);
						instance.Add(new BoundScope(boundSwitchExpressionArm.Syntax, boundSwitchExpressionArm.Locals, immutableArray, false));
					}
				}
				this._factory.Syntax = node.Syntax;
				if (node.DefaultLabel != null)
				{
					instance.Add(this._factory.Label(node.DefaultLabel));
					if (flag)
					{
						instance.Add(new BoundRestorePreviousSequencePoint(node.Syntax, obj2));
					}
					NamedTypeSymbol namedTypeSymbol = this._factory.SpecialType(SpecialType.System_Object);
					BoundObjectCreationExpression boundObjectCreationExpression;
					if (this.<LowerSwitchExpression>g__implicitConversionExists|2_0(boundExpression2, namedTypeSymbol))
					{
						MethodSymbol methodSymbol = this._factory.WellKnownMember(WellKnownMember.System_Runtime_CompilerServices_SwitchExpressionException__ctorObject, true) as MethodSymbol;
						if (methodSymbol != null)
						{
							boundObjectCreationExpression = this._factory.New(methodSymbol, new BoundExpression[] { this._factory.Convert(namedTypeSymbol, boundExpression2) });
							goto IL_037D;
						}
					}
					MethodSymbol methodSymbol2 = this._factory.WellKnownMember(WellKnownMember.System_Runtime_CompilerServices_SwitchExpressionException__ctor, true) as MethodSymbol;
					boundObjectCreationExpression = ((methodSymbol2 != null) ? this._factory.New(methodSymbol2, Array.Empty<BoundExpression>()) : this._factory.New(this._factory.WellKnownMethod(WellKnownMember.System_InvalidOperationException__ctor), Array.Empty<BoundExpression>()));
					IL_037D:
					BoundObjectCreationExpression boundObjectCreationExpression2 = boundObjectCreationExpression;
					instance.Add(this._factory.Throw(boundObjectCreationExpression2));
				}
				if (base.GenerateInstrumentation)
				{
					instance.Add(this._factory.HiddenSequencePoint(null));
				}
				instance.Add(this._factory.Label(labelSymbol));
				if (flag)
				{
					instance.Add(new BoundRestorePreviousSequencePoint(node.Syntax, obj));
				}
				instance2.Add(localSymbol);
				instance2.AddRange(this._tempAllocator.AllTemps());
				return this._factory.SpillSequence(instance2.ToImmutableAndFree(), instance.ToImmutableAndFree(), this._factory.Local(localSymbol));
			}

			// Token: 0x0600862E RID: 34350 RVA: 0x001E7FF4 File Offset: 0x001E61F4
			[CompilerGenerated]
			private bool <LowerSwitchExpression>g__implicitConversionExists|2_0(BoundExpression expression, TypeSymbol type)
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				return this._localRewriter._compilation.Conversions.ClassifyConversionFromExpression(expression, type, ref discarded, false).IsImplicit;
			}
		}
	}
}
