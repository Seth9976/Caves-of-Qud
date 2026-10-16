using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004A8 RID: 1192
	internal abstract class MethodToClassRewriter : BoundTreeRewriterWithStackGuard
	{
		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x0600629D RID: 25245
		protected abstract TypeMap TypeMap { get; }

		// Token: 0x0600629E RID: 25246
		protected abstract BoundExpression FramePointer(SyntaxNode syntax, NamedTypeSymbol frameClass);

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x0600629F RID: 25247
		protected abstract MethodSymbol CurrentMethod { get; }

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060062A0 RID: 25248
		protected abstract NamedTypeSymbol ContainingType { get; }

		// Token: 0x060062A1 RID: 25249 RVA: 0x0017A82F File Offset: 0x00178A2F
		protected MethodToClassRewriter(VariableSlotAllocator slotAllocatorOpt, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			this.CompilationState = compilationState;
			this.Diagnostics = diagnostics;
			this.slotAllocatorOpt = slotAllocatorOpt;
			this._placeholderMap = new Dictionary<BoundValuePlaceholderBase, BoundExpression>();
		}

		// Token: 0x060062A2 RID: 25250
		protected abstract bool NeedsProxy(Symbol localOrParameter);

		// Token: 0x060062A3 RID: 25251 RVA: 0x0017A870 File Offset: 0x00178A70
		protected void RewriteLocals(ImmutableArray<LocalSymbol> locals, ArrayBuilder<LocalSymbol> newLocals)
		{
			foreach (LocalSymbol localSymbol in locals)
			{
				LocalSymbol localSymbol2;
				if (this.TryRewriteLocal(localSymbol, out localSymbol2))
				{
					newLocals.Add(localSymbol2);
				}
			}
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x0017A8AC File Offset: 0x00178AAC
		protected bool TryRewriteLocal(LocalSymbol local, out LocalSymbol newLocal)
		{
			if (this.NeedsProxy(local))
			{
				newLocal = null;
				return false;
			}
			if (this.localMap.TryGetValue(local, ref newLocal))
			{
				return true;
			}
			TypeSymbol typeSymbol = this.VisitType(local.Type);
			if (TypeSymbol.Equals(typeSymbol, local.Type, TypeCompareKind.ConsiderEverything))
			{
				newLocal = local;
			}
			else
			{
				newLocal = new TypeSubstitutedLocalSymbol(local, TypeWithAnnotations.Create(typeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), this.CurrentMethod);
				this.localMap.Add(local, newLocal);
			}
			return true;
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x0017A928 File Offset: 0x00178B28
		private ImmutableArray<LocalSymbol> RewriteLocals(ImmutableArray<LocalSymbol> locals)
		{
			if (locals.IsEmpty)
			{
				return locals;
			}
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			this.RewriteLocals(locals, instance);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x0017A954 File Offset: 0x00178B54
		public override BoundNode VisitCatchBlock(BoundCatchBlock node)
		{
			if (!node.Locals.IsDefaultOrEmpty)
			{
				ImmutableArray<LocalSymbol> immutableArray = this.RewriteLocals(node.Locals);
				return node.Update(immutableArray, (BoundExpression)this.Visit(node.ExceptionSourceOpt), this.VisitType(node.ExceptionTypeOpt), (BoundStatementList)this.Visit(node.ExceptionFilterPrologueOpt), (BoundExpression)this.Visit(node.ExceptionFilterOpt), (BoundBlock)this.Visit(node.Body), node.IsSynthesizedAsyncCatchAll);
			}
			return base.VisitCatchBlock(node);
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x0017A9E4 File Offset: 0x00178BE4
		public override BoundNode VisitBlock(BoundBlock node)
		{
			ImmutableArray<LocalSymbol> immutableArray = this.RewriteLocals(node.Locals);
			ImmutableArray<LocalFunctionSymbol> localFunctions = node.LocalFunctions;
			ImmutableArray<BoundStatement> immutableArray2 = base.VisitList<BoundStatement>(node.Statements);
			return node.Update(immutableArray, localFunctions, immutableArray2);
		}

		// Token: 0x060062A8 RID: 25256
		public abstract override BoundNode VisitScope(BoundScope node);

		// Token: 0x060062A9 RID: 25257 RVA: 0x0017AA1C File Offset: 0x00178C1C
		public override BoundNode VisitSequence(BoundSequence node)
		{
			ImmutableArray<LocalSymbol> immutableArray = this.RewriteLocals(node.Locals);
			ImmutableArray<BoundExpression> immutableArray2 = base.VisitList<BoundExpression>(node.SideEffects);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Value);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(immutableArray, immutableArray2, boundExpression, typeSymbol);
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x0017AA6C File Offset: 0x00178C6C
		public override BoundNode VisitForStatement(BoundForStatement node)
		{
			ImmutableArray<LocalSymbol> immutableArray = this.RewriteLocals(node.OuterLocals);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Initializer);
			ImmutableArray<LocalSymbol> immutableArray2 = this.RewriteLocals(node.InnerLocals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement2 = (BoundStatement)this.Visit(node.Increment);
			BoundStatement boundStatement3 = (BoundStatement)this.Visit(node.Body);
			return node.Update(immutableArray, boundStatement, immutableArray2, boundExpression, boundStatement2, boundStatement3, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x0017AAF8 File Offset: 0x00178CF8
		public override BoundNode VisitDoStatement(BoundDoStatement node)
		{
			ImmutableArray<LocalSymbol> immutableArray = this.RewriteLocals(node.Locals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(immutableArray, boundExpression, boundStatement, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x0017AB4C File Offset: 0x00178D4C
		public override BoundNode VisitWhileStatement(BoundWhileStatement node)
		{
			ImmutableArray<LocalSymbol> immutableArray = this.RewriteLocals(node.Locals);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Condition);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			return node.Update(immutableArray, boundExpression, boundStatement, node.BreakLabel, node.ContinueLabel);
		}

		// Token: 0x060062AD RID: 25261 RVA: 0x0017ABA0 File Offset: 0x00178DA0
		public override BoundNode VisitUsingStatement(BoundUsingStatement node)
		{
			ImmutableArray<LocalSymbol> immutableArray = this.RewriteLocals(node.Locals);
			BoundMultipleLocalDeclarations boundMultipleLocalDeclarations = (BoundMultipleLocalDeclarations)this.Visit(node.DeclarationsOpt);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ExpressionOpt);
			BoundStatement boundStatement = (BoundStatement)this.Visit(node.Body);
			Conversion conversion = this.RewriteConversion(node.IDisposableConversion);
			return node.Update(immutableArray, boundMultipleLocalDeclarations, boundExpression, conversion, boundStatement, node.AwaitOpt, node.PatternDisposeInfoOpt);
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x0017AC18 File Offset: 0x00178E18
		private Conversion RewriteConversion(Conversion conversion)
		{
			ConversionKind kind = conversion.Kind;
			if (kind != ConversionKind.ImplicitUserDefined)
			{
				if (kind == ConversionKind.MethodGroup)
				{
					throw ExceptionUtilities.UnexpectedValue(conversion.Kind);
				}
				if (kind != ConversionKind.ExplicitUserDefined)
				{
					return conversion;
				}
			}
			return new Conversion(conversion.Kind, this.VisitMethodSymbol(conversion.Method), conversion.IsExtensionMethod);
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x0017AC70 File Offset: 0x00178E70
		public sealed override TypeSymbol VisitType(TypeSymbol type)
		{
			return this.TypeMap.SubstituteType(type).Type;
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x0017AC94 File Offset: 0x00178E94
		public override BoundNode VisitMethodInfo(BoundMethodInfo node)
		{
			MethodSymbol methodSymbol = this.VisitMethodSymbol(node.Method);
			return node.Update(methodSymbol, node.GetMethodFromHandle, node.Type);
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x0017ACC4 File Offset: 0x00178EC4
		public override BoundNode VisitPropertyAccess(BoundPropertyAccess node)
		{
			PropertySymbol propertySymbol = this.VisitPropertySymbol(node.PropertySymbol);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			return node.Update(boundExpression, propertySymbol, node.ResultKind, this.VisitType(node.Type));
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x0017AD0C File Offset: 0x00178F0C
		public override BoundNode VisitCall(BoundCall node)
		{
			MethodSymbol methodSymbol = this.VisitMethodSymbol(node.Method);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			if (MethodToClassRewriter.BaseReferenceInReceiverWasRewritten(node.ReceiverOpt, boundExpression) && node.Method.IsMetadataVirtual(false))
			{
				methodSymbol = this.GetMethodWrapperForBaseNonVirtualCall(methodSymbol, node.Syntax);
			}
			return node.Update(boundExpression, methodSymbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.IsDelegateCall, node.Expanded, node.InvokedAsExtensionMethod, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, typeSymbol);
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x0017ADB8 File Offset: 0x00178FB8
		private MethodSymbol GetMethodWrapperForBaseNonVirtualCall(MethodSymbol methodBeingCalled, SyntaxNode syntax)
		{
			MethodSymbol orCreateBaseFunctionWrapper = this.GetOrCreateBaseFunctionWrapper(methodBeingCalled, syntax);
			if (!orCreateBaseFunctionWrapper.IsGenericMethod)
			{
				return orCreateBaseFunctionWrapper;
			}
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotations = methodBeingCalled.TypeArgumentsWithAnnotations;
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(typeArgumentsWithAnnotations.Length);
			foreach (TypeWithAnnotations typeWithAnnotations in typeArgumentsWithAnnotations)
			{
				instance.Add(typeWithAnnotations.WithTypeAndModifiers(this.VisitType(typeWithAnnotations.Type), typeWithAnnotations.CustomModifiers));
			}
			return orCreateBaseFunctionWrapper.Construct(instance.ToImmutableAndFree());
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x0017AE38 File Offset: 0x00179038
		private MethodSymbol GetOrCreateBaseFunctionWrapper(MethodSymbol methodBeingWrapped, SyntaxNode syntax)
		{
			methodBeingWrapped = methodBeingWrapped.ConstructedFrom;
			MethodSymbol methodSymbol = this.CompilationState.GetMethodWrapper(methodBeingWrapped);
			if (methodSymbol != null)
			{
				return methodSymbol;
			}
			NamedTypeSymbol containingType = this.ContainingType;
			string text = GeneratedNames.MakeBaseMethodWrapperName(this.CompilationState.NextWrapperMethodIndex);
			methodSymbol = new MethodToClassRewriter.BaseMethodWrapperSymbol(containingType, methodBeingWrapped, syntax, text);
			if (this.CompilationState.Emitting)
			{
				this.CompilationState.ModuleBuilderOpt.AddSynthesizedDefinition(containingType, methodSymbol.GetCciAdapter());
			}
			methodSymbol.GenerateMethodBody(this.CompilationState, this.Diagnostics);
			return methodSymbol;
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x0017AEB8 File Offset: 0x001790B8
		private bool TryReplaceWithProxy(Symbol parameterOrLocal, SyntaxNode syntax, out BoundNode replacement)
		{
			CapturedSymbolReplacement capturedSymbolReplacement;
			if (this.proxies.TryGetValue(parameterOrLocal, ref capturedSymbolReplacement))
			{
				replacement = capturedSymbolReplacement.Replacement(syntax, (NamedTypeSymbol frameType) => this.FramePointer(syntax, frameType));
				return true;
			}
			replacement = null;
			return false;
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x0017AF0C File Offset: 0x0017910C
		public sealed override BoundNode VisitParameter(BoundParameter node)
		{
			BoundNode boundNode;
			if (this.TryReplaceWithProxy(node.ParameterSymbol, node.Syntax, out boundNode))
			{
				return boundNode;
			}
			return this.VisitUnhoistedParameter(node);
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x0017AF38 File Offset: 0x00179138
		protected virtual BoundNode VisitUnhoistedParameter(BoundParameter node)
		{
			return base.VisitParameter(node);
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x0017AF44 File Offset: 0x00179144
		public sealed override BoundNode VisitLocal(BoundLocal node)
		{
			BoundNode boundNode;
			if (this.TryReplaceWithProxy(node.LocalSymbol, node.Syntax, out boundNode))
			{
				return boundNode;
			}
			return this.VisitUnhoistedLocal(node);
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x0017AF70 File Offset: 0x00179170
		private BoundNode VisitUnhoistedLocal(BoundLocal node)
		{
			LocalSymbol localSymbol;
			if (this.localMap.TryGetValue(node.LocalSymbol, ref localSymbol))
			{
				return new BoundLocal(node.Syntax, localSymbol, node.ConstantValueOpt, localSymbol.Type, node.HasErrors);
			}
			return base.VisitLocal(node);
		}

		// Token: 0x060062BA RID: 25274 RVA: 0x0017AFB8 File Offset: 0x001791B8
		public override BoundNode VisitAwaitableInfo(BoundAwaitableInfo node)
		{
			BoundAwaitableValuePlaceholder awaitableInstancePlaceholder = node.AwaitableInstancePlaceholder;
			if (awaitableInstancePlaceholder == null)
			{
				return node;
			}
			BoundAwaitableValuePlaceholder boundAwaitableValuePlaceholder = awaitableInstancePlaceholder.Update(awaitableInstancePlaceholder.ValEscape, this.VisitType(awaitableInstancePlaceholder.Type));
			this._placeholderMap.Add(awaitableInstancePlaceholder, boundAwaitableValuePlaceholder);
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.GetAwaiter);
			PropertySymbol propertySymbol = this.VisitPropertySymbol(node.IsCompleted);
			MethodSymbol methodSymbol = this.VisitMethodSymbol(node.GetResult);
			this._placeholderMap.Remove(awaitableInstancePlaceholder);
			return node.Update(boundAwaitableValuePlaceholder, node.IsDynamic, boundExpression, propertySymbol, methodSymbol);
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x0017B042 File Offset: 0x00179242
		public override BoundNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node)
		{
			return this._placeholderMap[node];
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x0017B050 File Offset: 0x00179250
		public override BoundNode VisitAssignmentOperator(BoundAssignmentOperator node)
		{
			BoundExpression left = node.Left;
			if (left.Kind != BoundKind.Local)
			{
				return base.VisitAssignmentOperator(node);
			}
			BoundLocal boundLocal = (BoundLocal)left;
			BoundExpression right = node.Right;
			if (boundLocal.LocalSymbol.RefKind != RefKind.None && node.IsRef && this.NeedsProxy(boundLocal.LocalSymbol))
			{
				throw ExceptionUtilities.Unreachable;
			}
			if (this.NeedsProxy(boundLocal.LocalSymbol) && !this.proxies.ContainsKey(boundLocal.LocalSymbol))
			{
				throw ExceptionUtilities.Unreachable;
			}
			BoundExpression boundExpression = (BoundExpression)this.Visit(boundLocal);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(right);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			if (boundExpression.Kind != BoundKind.Local && right.Kind == BoundKind.ConvertedStackAllocExpression)
			{
				BoundAssignmentOperator boundAssignmentOperator;
				BoundLocal boundLocal2 = new SyntheticBoundNodeFactory(this.CurrentMethod, boundExpression.Syntax, this.CompilationState, this.Diagnostics).StoreToTemp(boundExpression2, out boundAssignmentOperator, RefKind.None, SynthesizedLocalKind.LoweringTemp, null);
				BoundAssignmentOperator boundAssignmentOperator2 = node.Update(boundExpression, boundLocal2, node.IsRef, typeSymbol);
				return new BoundSequence(node.Syntax, ImmutableArray.Create<LocalSymbol>(boundLocal2.LocalSymbol), ImmutableArray.Create<BoundExpression>(boundAssignmentOperator), boundAssignmentOperator2, typeSymbol, false);
			}
			return node.Update(boundExpression, boundExpression2, node.IsRef, typeSymbol);
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x0017B188 File Offset: 0x00179388
		public override BoundNode VisitFieldInfo(BoundFieldInfo node)
		{
			FieldSymbol fieldSymbol = node.Field.OriginalDefinition.AsMember((NamedTypeSymbol)this.VisitType(node.Field.ContainingType));
			return node.Update(fieldSymbol, node.GetFieldFromHandle, node.Type);
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x0017B1D0 File Offset: 0x001793D0
		public override BoundNode VisitFieldAccess(BoundFieldAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.ReceiverOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			FieldSymbol fieldSymbol = node.FieldSymbol.OriginalDefinition.AsMember((NamedTypeSymbol)this.VisitType(node.FieldSymbol.ContainingType));
			return node.Update(boundExpression, fieldSymbol, node.ConstantValueOpt, node.ResultKind, typeSymbol);
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x0017B238 File Offset: 0x00179438
		public override BoundNode VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			BoundObjectCreationExpression boundObjectCreationExpression = (BoundObjectCreationExpression)base.VisitObjectCreationExpression(node);
			if (!TypeSymbol.Equals(boundObjectCreationExpression.Type, node.Type, TypeCompareKind.ConsiderEverything) && node.Constructor != null)
			{
				MethodSymbol methodSymbol = this.VisitMethodSymbol(node.Constructor);
				boundObjectCreationExpression = boundObjectCreationExpression.Update(methodSymbol, boundObjectCreationExpression.Arguments, boundObjectCreationExpression.ArgumentNamesOpt, boundObjectCreationExpression.ArgumentRefKindsOpt, boundObjectCreationExpression.Expanded, boundObjectCreationExpression.ArgsToParamsOpt, boundObjectCreationExpression.DefaultArguments, boundObjectCreationExpression.ConstantValueOpt, boundObjectCreationExpression.InitializerExpressionOpt, boundObjectCreationExpression.Type);
			}
			return boundObjectCreationExpression;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x0017B2BC File Offset: 0x001794BC
		public override BoundNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			BoundExpression argument = node.Argument;
			BoundExpression boundExpression = (BoundExpression)this.Visit(argument);
			MethodSymbol methodSymbol = node.MethodOpt;
			if (MethodToClassRewriter.BaseReferenceInReceiverWasRewritten(argument, boundExpression) && methodSymbol.IsMetadataVirtual(false))
			{
				methodSymbol = this.GetMethodWrapperForBaseNonVirtualCall(methodSymbol, argument.Syntax);
			}
			methodSymbol = this.VisitMethodSymbol(methodSymbol);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, methodSymbol, node.IsExtensionMethod, typeSymbol);
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0017B328 File Offset: 0x00179528
		public override BoundNode VisitFunctionPointerLoad(BoundFunctionPointerLoad node)
		{
			return node.Update(this.VisitMethodSymbol(node.TargetMethod), this.VisitType(node.Type));
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0017B348 File Offset: 0x00179548
		public override BoundNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Receiver);
			BoundExpression boundExpression2 = (BoundExpression)this.Visit(node.WhenNotNull);
			BoundExpression boundExpression3 = (BoundExpression)this.Visit(node.WhenNullOpt);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, this.VisitMethodSymbol(node.HasValueMethodOpt), boundExpression2, boundExpression3, node.Id, typeSymbol);
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x0017B3B4 File Offset: 0x001795B4
		protected MethodSymbol VisitMethodSymbol(MethodSymbol method)
		{
			if (method == null)
			{
				return null;
			}
			if (!method.ContainingType.IsAnonymousType)
			{
				return method.OriginalDefinition.AsMember((NamedTypeSymbol)this.TypeMap.SubstituteType(method.ContainingType).AsTypeSymbolOnly()).ConstructIfGeneric(this.TypeMap.SubstituteTypes(method.TypeArgumentsWithAnnotations));
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.TypeMap.SubstituteType(method.ContainingType).AsTypeSymbolOnly();
			if (namedTypeSymbol == method.ContainingType)
			{
				return method;
			}
			foreach (Symbol symbol in namedTypeSymbol.GetMembers(method.Name))
			{
				if (symbol.Kind == SymbolKind.Method)
				{
					return (MethodSymbol)symbol;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x0017B480 File Offset: 0x00179680
		private PropertySymbol VisitPropertySymbol(PropertySymbol property)
		{
			if (property == null)
			{
				return null;
			}
			if (!property.ContainingType.IsAnonymousType)
			{
				return property.OriginalDefinition.AsMember((NamedTypeSymbol)this.TypeMap.SubstituteType(property.ContainingType).AsTypeSymbolOnly());
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)this.TypeMap.SubstituteType(property.ContainingType).AsTypeSymbolOnly();
			if (namedTypeSymbol == property.ContainingType)
			{
				return property;
			}
			foreach (Symbol symbol in namedTypeSymbol.GetMembers(property.Name))
			{
				if (symbol.Kind == SymbolKind.Property)
				{
					return (PropertySymbol)symbol;
				}
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x0017B534 File Offset: 0x00179734
		private FieldSymbol VisitFieldSymbol(FieldSymbol field)
		{
			return field.OriginalDefinition.AsMember((NamedTypeSymbol)this.TypeMap.SubstituteType(field.ContainingType).AsTypeSymbolOnly());
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x0017B56C File Offset: 0x0017976C
		public override BoundNode VisitObjectInitializerMember(BoundObjectInitializerMember node)
		{
			ImmutableArray<BoundExpression> immutableArray = base.VisitList<BoundExpression>(node.Arguments);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			TypeSymbol typeSymbol2 = this.VisitType(node.ReceiverType);
			Symbol symbol = node.MemberSymbol;
			SymbolKind kind = symbol.Kind;
			if (kind != SymbolKind.Field)
			{
				if (kind == SymbolKind.Property)
				{
					symbol = this.VisitPropertySymbol((PropertySymbol)symbol);
				}
			}
			else
			{
				symbol = this.VisitFieldSymbol((FieldSymbol)symbol);
			}
			return node.Update(symbol, immutableArray, node.ArgumentNamesOpt, node.ArgumentRefKindsOpt, node.Expanded, node.ArgsToParamsOpt, node.DefaultArguments, node.ResultKind, typeSymbol2, typeSymbol);
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x0017B608 File Offset: 0x00179808
		public override BoundNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node)
		{
			BoundExpression boundExpression = (BoundExpression)this.Visit(node.Operand);
			MethodSymbol methodSymbol = this.VisitMethodSymbol(node.ConversionMethod);
			TypeSymbol typeSymbol = this.VisitType(node.Type);
			return node.Update(boundExpression, methodSymbol, typeSymbol);
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x0017B64A File Offset: 0x0017984A
		private static bool BaseReferenceInReceiverWasRewritten(BoundExpression originalReceiver, BoundExpression rewrittenReceiver)
		{
			return originalReceiver != null && originalReceiver.Kind == BoundKind.BaseReference && rewrittenReceiver != null && rewrittenReceiver.Kind != BoundKind.BaseReference;
		}

		// Token: 0x04001983 RID: 6531
		protected Dictionary<Symbol, CapturedSymbolReplacement> proxies = new Dictionary<Symbol, CapturedSymbolReplacement>();

		// Token: 0x04001984 RID: 6532
		protected readonly Dictionary<LocalSymbol, LocalSymbol> localMap = new Dictionary<LocalSymbol, LocalSymbol>();

		// Token: 0x04001985 RID: 6533
		protected readonly TypeCompilationState CompilationState;

		// Token: 0x04001986 RID: 6534
		protected readonly BindingDiagnosticBag Diagnostics;

		// Token: 0x04001987 RID: 6535
		protected readonly VariableSlotAllocator slotAllocatorOpt;

		// Token: 0x04001988 RID: 6536
		private readonly Dictionary<BoundValuePlaceholderBase, BoundExpression> _placeholderMap;

		// Token: 0x020008C9 RID: 2249
		private sealed class BaseMethodWrapperSymbol : SynthesizedMethodBaseSymbol
		{
			// Token: 0x1700282F RID: 10287
			// (get) Token: 0x06008E0B RID: 36363 RVA: 0x001F5A45 File Offset: 0x001F3C45
			internal sealed override bool GenerateDebugInfo
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002830 RID: 10288
			// (get) Token: 0x06008E0C RID: 36364 RVA: 0x001F5A48 File Offset: 0x001F3C48
			internal override bool SynthesizesLoweredBoundBody
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06008E0D RID: 36365 RVA: 0x001F5A4C File Offset: 0x001F3C4C
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			{
				SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.GetNonNullSyntaxNode(), compilationState, diagnostics);
				syntheticBoundNodeFactory.CurrentFunction = this.OriginalDefinition;
				try
				{
					MethodSymbol methodSymbol = this.BaseMethod;
					if (this.Arity > 0)
					{
						methodSymbol = methodSymbol.ConstructedFrom.Construct(StaticCast<TypeSymbol>.From<TypeParameterSymbol>(this.TypeParameters));
					}
					BoundBlock boundBlock = MethodBodySynthesizer.ConstructSingleInvocationMethodBody(syntheticBoundNodeFactory, methodSymbol, true);
					if (boundBlock.Kind != BoundKind.Block)
					{
						boundBlock = syntheticBoundNodeFactory.Block(new BoundStatement[] { boundBlock });
					}
					syntheticBoundNodeFactory.CompilationState.AddMethodWrapper(methodSymbol, this, boundBlock);
				}
				catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
				{
					diagnostics.Add(missingPredefinedMember.Diagnostic);
				}
			}

			// Token: 0x06008E0E RID: 36366 RVA: 0x001F5AF0 File Offset: 0x001F3CF0
			internal BaseMethodWrapperSymbol(NamedTypeSymbol containingType, MethodSymbol methodBeingWrapped, SyntaxNode syntax, string name)
				: base(containingType, methodBeingWrapped, syntax.SyntaxTree.GetReference(syntax), syntax.GetLocation(), name, DeclarationModifiers.Private)
			{
				SubstitutedNamedTypeSymbol substitutedNamedTypeSymbol = methodBeingWrapped.ContainingType as SubstitutedNamedTypeSymbol;
				TypeMap typeMap = ((substitutedNamedTypeSymbol == null) ? TypeMap.Empty : substitutedNamedTypeSymbol.TypeSubstitution);
				ImmutableArray<TypeParameterSymbol> empty;
				if (!methodBeingWrapped.IsGenericMethod)
				{
					empty = ImmutableArray<TypeParameterSymbol>.Empty;
				}
				else
				{
					typeMap = typeMap.WithAlphaRename(methodBeingWrapped, this, out empty);
				}
				base.AssignTypeMapAndTypeParameters(typeMap, empty);
			}

			// Token: 0x06008E0F RID: 36367 RVA: 0x001F5B60 File Offset: 0x001F3D60
			internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
			{
				base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
				Symbol.AddSynthesizedAttribute(ref attributes, this.DeclaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerHiddenAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}

			// Token: 0x17002831 RID: 10289
			// (get) Token: 0x06008E10 RID: 36368 RVA: 0x001F5B9C File Offset: 0x001F3D9C
			internal override TypeWithAnnotations IteratorElementTypeWithAnnotations
			{
				get
				{
					return default(TypeWithAnnotations);
				}
			}

			// Token: 0x17002832 RID: 10290
			// (get) Token: 0x06008E11 RID: 36369 RVA: 0x001F5BB2 File Offset: 0x001F3DB2
			internal override bool IsIterator
			{
				get
				{
					return false;
				}
			}
		}
	}
}
