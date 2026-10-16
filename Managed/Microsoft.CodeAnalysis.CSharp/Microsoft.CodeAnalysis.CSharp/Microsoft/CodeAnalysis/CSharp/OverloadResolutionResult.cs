using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200006C RID: 108
	internal class OverloadResolutionResult<TMember> where TMember : Symbol
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0004BE92 File Offset: 0x0004A092
		internal OverloadResolutionResult()
		{
			this.ResultsBuilder = new ArrayBuilder<MemberResolutionResult<TMember>>();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0004BEA5 File Offset: 0x0004A0A5
		internal void Clear()
		{
			this._bestResult = default(MemberResolutionResult<TMember>);
			this._bestResultState = ThreeState.Unknown;
			this.ResultsBuilder.Clear();
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0004BEC8 File Offset: 0x0004A0C8
		public bool Succeeded
		{
			get
			{
				this.EnsureBestResultLoaded();
				return this._bestResultState == ThreeState.True && this._bestResult.Result.IsValid;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0004BEF9 File Offset: 0x0004A0F9
		public MemberResolutionResult<TMember> ValidResult
		{
			get
			{
				this.EnsureBestResultLoaded();
				return this._bestResult;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0004BF07 File Offset: 0x0004A107
		private void EnsureBestResultLoaded()
		{
			if (!this._bestResultState.HasValue())
			{
				this._bestResultState = OverloadResolutionResult<TMember>.TryGetBestResult(this.ResultsBuilder, out this._bestResult);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0004BF2D File Offset: 0x0004A12D
		public MemberResolutionResult<TMember> BestResult
		{
			get
			{
				this.EnsureBestResultLoaded();
				return this._bestResult;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0004BF3B File Offset: 0x0004A13B
		public ImmutableArray<MemberResolutionResult<TMember>> Results
		{
			get
			{
				return this.ResultsBuilder.ToImmutable();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0004BF48 File Offset: 0x0004A148
		internal bool HasAnyApplicableMember
		{
			get
			{
				foreach (MemberResolutionResult<TMember> memberResolutionResult in this.ResultsBuilder)
				{
					if (memberResolutionResult.Result.IsApplicable)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0004BF88 File Offset: 0x0004A188
		internal ImmutableArray<TMember> GetAllApplicableMembers()
		{
			ArrayBuilder<TMember> instance = ArrayBuilder<TMember>.GetInstance();
			foreach (MemberResolutionResult<TMember> memberResolutionResult in this.ResultsBuilder)
			{
				if (memberResolutionResult.Result.IsApplicable)
				{
					instance.Add(memberResolutionResult.Member);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0004BFE0 File Offset: 0x0004A1E0
		private static ThreeState TryGetBestResult(ArrayBuilder<MemberResolutionResult<TMember>> allResults, out MemberResolutionResult<TMember> best)
		{
			best = default(MemberResolutionResult<TMember>);
			ThreeState threeState = ThreeState.False;
			foreach (MemberResolutionResult<TMember> memberResolutionResult in allResults)
			{
				if (memberResolutionResult.Result.IsValid)
				{
					if (threeState == ThreeState.True)
					{
						best = default(MemberResolutionResult<TMember>);
						return ThreeState.False;
					}
					threeState = ThreeState.True;
					best = memberResolutionResult;
				}
			}
			return threeState;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0004C038 File Offset: 0x0004A238
		internal void ReportDiagnostics<T>(Binder binder, Location location, SyntaxNode nodeOpt, BindingDiagnosticBag diagnostics, string name, BoundExpression receiver, SyntaxNode invokedExpression, AnalyzedArguments arguments, ImmutableArray<T> memberGroup, NamedTypeSymbol typeContainingConstructor, NamedTypeSymbol delegateTypeBeingInvoked, CSharpSyntaxNode queryClause = null, bool isMethodGroupConversion = false, RefKind? returnRefKind = null, TypeSymbol delegateOrFunctionPointerType = null) where T : Symbol
		{
			ImmutableArray<Symbol> immutableArray = StaticCast<Symbol>.From<T>(memberGroup);
			if (this.HadAmbiguousBestMethods(diagnostics, immutableArray, location))
			{
				return;
			}
			if (this.HadAmbiguousWorseMethods(diagnostics, immutableArray, location, queryClause != null, receiver, name))
			{
				return;
			}
			if (OverloadResolutionResult<TMember>.HadLambdaConversionError(diagnostics, arguments))
			{
				return;
			}
			if (this.HadStaticInstanceMismatch(diagnostics, immutableArray, ((invokedExpression != null) ? invokedExpression.GetLocation() : null) ?? location, binder, receiver, nodeOpt, delegateOrFunctionPointerType))
			{
				return;
			}
			if (isMethodGroupConversion && returnRefKind != null && this.HadReturnMismatch(location, diagnostics, returnRefKind.GetValueOrDefault(), delegateOrFunctionPointerType))
			{
				return;
			}
			if (this.HadConstraintFailure(location, diagnostics))
			{
				return;
			}
			if (this.HadBadArguments(diagnostics, binder, name, arguments, immutableArray, location, binder.Flags, isMethodGroupConversion))
			{
				return;
			}
			if (this.HadConstructedParameterFailedConstraintCheck(binder.Conversions, binder.Compilation, diagnostics, location))
			{
				return;
			}
			if (this.InaccessibleTypeArgument(diagnostics, immutableArray, location))
			{
				return;
			}
			if (this.TypeInferenceFailed(binder, diagnostics, immutableArray, receiver, arguments, location, queryClause))
			{
				return;
			}
			if (this.UseSiteError())
			{
				return;
			}
			bool flag = false;
			MemberResolutionResult<TMember> memberResolutionResult = default(MemberResolutionResult<TMember>);
			MemberResolutionResult<TMember> memberResolutionResult2 = default(MemberResolutionResult<TMember>);
			MemberResolutionResult<TMember>[] array = new MemberResolutionResult<TMember>[7];
			foreach (MemberResolutionResult<TMember> memberResolutionResult3 in this.ResultsBuilder)
			{
				switch (memberResolutionResult3.Result.Kind)
				{
				case MemberResolutionKind.NoCorrespondingParameter:
					if (array[4].IsNull)
					{
						array[4] = memberResolutionResult3;
						continue;
					}
					continue;
				case MemberResolutionKind.NoCorrespondingNamedParameter:
					if (array[3].IsNull || memberResolutionResult3.Result.BadArgumentsOpt[0] > array[3].Result.BadArgumentsOpt[0])
					{
						array[3] = memberResolutionResult3;
						continue;
					}
					continue;
				case MemberResolutionKind.DuplicateNamedArgument:
					if (array[0].IsNull || memberResolutionResult3.Result.BadArgumentsOpt[0] > array[0].Result.BadArgumentsOpt[0])
					{
						array[0] = memberResolutionResult3;
						continue;
					}
					continue;
				case MemberResolutionKind.RequiredParameterMissing:
					if (array[1].IsNull)
					{
						array[1] = memberResolutionResult3;
						continue;
					}
					flag = true;
					continue;
				case MemberResolutionKind.NameUsedForPositional:
					if (array[2].IsNull || memberResolutionResult3.Result.BadArgumentsOpt[0] > array[2].Result.BadArgumentsOpt[0])
					{
						array[2] = memberResolutionResult3;
						continue;
					}
					continue;
				case MemberResolutionKind.BadNonTrailingNamedArgument:
					if (array[5].IsNull || memberResolutionResult3.Result.BadArgumentsOpt[0] > array[5].Result.BadArgumentsOpt[0])
					{
						array[5] = memberResolutionResult3;
						continue;
					}
					continue;
				case MemberResolutionKind.UnsupportedMetadata:
					if (memberResolutionResult.IsNull)
					{
						memberResolutionResult2 = memberResolutionResult3;
						continue;
					}
					continue;
				case MemberResolutionKind.WrongCallingConvention:
					if (array[6].IsNull)
					{
						array[6] = memberResolutionResult3;
						continue;
					}
					continue;
				}
				throw ExceptionUtilities.UnexpectedValue(memberResolutionResult3.Result.Kind);
			}
			foreach (MemberResolutionResult<TMember> memberResolutionResult4 in array)
			{
				if (memberResolutionResult4.IsNotNull)
				{
					memberResolutionResult = memberResolutionResult4;
					break;
				}
			}
			if (memberResolutionResult.IsNotNull)
			{
				if (memberResolutionResult.Member is FunctionPointerMethodSymbol && memberResolutionResult.Result.Kind == MemberResolutionKind.NoCorrespondingNamedParameter)
				{
					int num = memberResolutionResult.Result.BadArgumentsOpt[0];
					IdentifierNameSyntax identifierNameSyntax = arguments.Names[num];
					diagnostics.Add(ErrorCode.ERR_FunctionPointersCannotBeCalledWithNamedArguments, identifierNameSyntax.Location);
					return;
				}
				if ((memberResolutionResult.Result.Kind != MemberResolutionKind.RequiredParameterMissing || !flag) && !isMethodGroupConversion && !(memberResolutionResult.Member is FunctionPointerMethodSymbol))
				{
					switch (memberResolutionResult.Result.Kind)
					{
					case MemberResolutionKind.NoCorrespondingNamedParameter:
						OverloadResolutionResult<TMember>.ReportNoCorrespondingNamedParameter(memberResolutionResult, name, diagnostics, arguments, delegateTypeBeingInvoked, immutableArray);
						return;
					case MemberResolutionKind.DuplicateNamedArgument:
						OverloadResolutionResult<TMember>.ReportDuplicateNamedArgument(memberResolutionResult, diagnostics, arguments);
						return;
					case MemberResolutionKind.RequiredParameterMissing:
						OverloadResolutionResult<TMember>.ReportMissingRequiredParameter(memberResolutionResult, diagnostics, delegateTypeBeingInvoked, immutableArray, location);
						return;
					case MemberResolutionKind.NameUsedForPositional:
						OverloadResolutionResult<TMember>.ReportNameUsedForPositional(memberResolutionResult, diagnostics, arguments, immutableArray);
						return;
					case MemberResolutionKind.BadNonTrailingNamedArgument:
						OverloadResolutionResult<TMember>.ReportBadNonTrailingNamedArgument(memberResolutionResult, diagnostics, arguments, immutableArray);
						return;
					}
				}
				else if (memberResolutionResult.Result.Kind == MemberResolutionKind.WrongCallingConvention)
				{
					OverloadResolutionResult<TMember>.ReportWrongCallingConvention(location, diagnostics, immutableArray, memberResolutionResult, ((FunctionPointerTypeSymbol)delegateOrFunctionPointerType).Signature);
					return;
				}
			}
			else if (memberResolutionResult2.IsNotNull)
			{
				OverloadResolutionResult<TMember>.ReportUnsupportedMetadata(location, diagnostics, immutableArray, memberResolutionResult2);
				return;
			}
			if (!isMethodGroupConversion)
			{
				OverloadResolutionResult<TMember>.ReportBadParameterCount(diagnostics, name, arguments, immutableArray, location, typeContainingConstructor, delegateTypeBeingInvoked);
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0004C52C File Offset: 0x0004A72C
		private static void ReportUnsupportedMetadata(Location location, BindingDiagnosticBag diagnostics, ImmutableArray<Symbol> symbols, MemberResolutionResult<TMember> firstUnsupported)
		{
			DiagnosticInfo diagnosticInfo = firstUnsupported.Member.GetUseSiteInfo().DiagnosticInfo;
			diagnosticInfo = new DiagnosticInfoWithSymbols((ErrorCode)diagnosticInfo.Code, diagnosticInfo.Arguments, symbols);
			Symbol.ReportUseSiteDiagnostic(diagnosticInfo, diagnostics, location);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0004C56C File Offset: 0x0004A76C
		private static void ReportWrongCallingConvention(Location location, BindingDiagnosticBag diagnostics, ImmutableArray<Symbol> symbols, MemberResolutionResult<TMember> firstSupported, MethodSymbol target)
		{
			diagnostics.Add(new DiagnosticInfoWithSymbols(ErrorCode.ERR_WrongFuncPtrCallingConvention, new object[] { firstSupported.Member, target.CallingConvention }, symbols), location);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0004C5A4 File Offset: 0x0004A7A4
		private bool UseSiteError()
		{
			return !this.GetFirstMemberKind(MemberResolutionKind.UseSiteError).IsNull;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0004C5C8 File Offset: 0x0004A7C8
		private bool InaccessibleTypeArgument(BindingDiagnosticBag diagnostics, ImmutableArray<Symbol> symbols, Location location)
		{
			MemberResolutionResult<TMember> firstMemberKind = this.GetFirstMemberKind(MemberResolutionKind.InaccessibleTypeArgument);
			if (firstMemberKind.IsNull)
			{
				return false;
			}
			diagnostics.Add(new DiagnosticInfoWithSymbols(ErrorCode.ERR_BadAccess, new object[] { firstMemberKind.Member }, symbols), location);
			return true;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0004C610 File Offset: 0x0004A810
		private bool HadStaticInstanceMismatch(BindingDiagnosticBag diagnostics, ImmutableArray<Symbol> symbols, Location location, Binder binder, BoundExpression receiverOpt, SyntaxNode nodeOpt, TypeSymbol delegateOrFunctionPointerType)
		{
			MemberResolutionResult<TMember> firstMemberKind = this.GetFirstMemberKind(MemberResolutionKind.StaticInstanceMismatch);
			if (firstMemberKind.IsNull)
			{
				return false;
			}
			Symbol symbol = firstMemberKind.Member;
			if (receiverOpt != null && receiverOpt.Kind == BoundKind.QueryClause)
			{
				diagnostics.Add(ErrorCode.ERR_QueryNoProvider, location, new object[] { receiverOpt.Type, symbol.Name });
			}
			else if (binder.Flags.Includes(BinderFlags.CollectionInitializerAddMethod))
			{
				diagnostics.Add(ErrorCode.ERR_InitializerAddHasWrongSignature, location, new object[] { symbol });
			}
			else if (nodeOpt != null && nodeOpt.Kind() == SyntaxKind.AwaitExpression && symbol.Name == "GetAwaiter")
			{
				diagnostics.Add(ErrorCode.ERR_BadAwaitArg, location, new object[] { receiverOpt.Type });
			}
			else if (delegateOrFunctionPointerType is FunctionPointerTypeSymbol)
			{
				diagnostics.Add(ErrorCode.ERR_FuncPtrMethMustBeStatic, location, new object[] { symbol });
			}
			else
			{
				ErrorCode errorCode = (symbol.RequiresInstanceReceiver() ? ((Binder.WasImplicitReceiver(receiverOpt) && binder.InFieldInitializer && !binder.BindingTopLevelScriptCode) ? ErrorCode.ERR_FieldInitRefNonstatic : ErrorCode.ERR_ObjectRequired) : ErrorCode.ERR_ObjectProhibited);
				diagnostics.Add(new DiagnosticInfoWithSymbols(errorCode, new object[] { symbol }, symbols), location);
			}
			return true;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0004C75C File Offset: 0x0004A95C
		private bool HadReturnMismatch(Location location, BindingDiagnosticBag diagnostics, RefKind refKind, TypeSymbol delegateOrFunctionPointerType)
		{
			MemberResolutionResult<TMember> memberResolutionResult = this.GetFirstMemberKind(MemberResolutionKind.WrongRefKind);
			if (!memberResolutionResult.IsNull)
			{
				diagnostics.Add(delegateOrFunctionPointerType.IsFunctionPointer() ? ErrorCode.ERR_FuncPtrRefMismatch : ErrorCode.ERR_DelegateRefMismatch, location, new object[] { memberResolutionResult.Member, delegateOrFunctionPointerType });
				return true;
			}
			memberResolutionResult = this.GetFirstMemberKind(MemberResolutionKind.WrongReturnType);
			if (!memberResolutionResult.IsNull)
			{
				MethodSymbol methodSymbol = (MethodSymbol)((object)memberResolutionResult.Member);
				diagnostics.Add(ErrorCode.ERR_BadRetType, location, new object[] { methodSymbol, methodSymbol.ReturnType });
				return true;
			}
			return false;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0004C7FC File Offset: 0x0004A9FC
		private bool HadConstraintFailure(Location location, BindingDiagnosticBag diagnostics)
		{
			MemberResolutionResult<TMember> firstMemberKind = this.GetFirstMemberKind(MemberResolutionKind.ConstraintFailure);
			if (firstMemberKind.IsNull)
			{
				return false;
			}
			foreach (TypeParameterDiagnosticInfo typeParameterDiagnosticInfo in firstMemberKind.Result.ConstraintFailureDiagnostics)
			{
				if (typeParameterDiagnosticInfo.UseSiteInfo.DiagnosticInfo != null)
				{
					diagnostics.Add(new CSDiagnostic(typeParameterDiagnosticInfo.UseSiteInfo.DiagnosticInfo, location, false));
				}
			}
			return true;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0004C86C File Offset: 0x0004AA6C
		private bool TypeInferenceFailed(Binder binder, BindingDiagnosticBag diagnostics, ImmutableArray<Symbol> symbols, BoundExpression receiver, AnalyzedArguments arguments, Location location, CSharpSyntaxNode queryClause = null)
		{
			MemberResolutionResult<TMember> memberResolutionResult = this.GetFirstMemberKind(MemberResolutionKind.TypeInferenceFailed);
			if (memberResolutionResult.IsNotNull)
			{
				if (queryClause != null)
				{
					Binder.ReportQueryInferenceFailed(queryClause, memberResolutionResult.Member.Name, receiver, arguments, symbols, diagnostics);
				}
				else
				{
					diagnostics.Add(new DiagnosticInfoWithSymbols(ErrorCode.ERR_CantInferMethTypeArgs, new object[] { memberResolutionResult.Member }, symbols), location);
				}
				return true;
			}
			memberResolutionResult = this.GetFirstMemberKind(MemberResolutionKind.TypeInferenceExtensionInstanceArgument);
			if (memberResolutionResult.IsNotNull)
			{
				BoundExpression boundExpression = arguments.Arguments[0];
				if (queryClause != null)
				{
					binder.ReportQueryLookupFailed(queryClause, boundExpression, memberResolutionResult.Member.Name, symbols, diagnostics);
				}
				else
				{
					diagnostics.Add(new DiagnosticInfoWithSymbols(ErrorCode.ERR_NoSuchMemberOrExtension, new object[]
					{
						boundExpression.Type,
						memberResolutionResult.Member.Name
					}, symbols), location);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0004C954 File Offset: 0x0004AB54
		private static void ReportNameUsedForPositional(MemberResolutionResult<TMember> bad, BindingDiagnosticBag diagnostics, AnalyzedArguments arguments, ImmutableArray<Symbol> symbols)
		{
			int num = bad.Result.BadArgumentsOpt[0];
			IdentifierNameSyntax identifierNameSyntax = arguments.Names[num];
			Location location = new SourceLocation(identifierNameSyntax);
			diagnostics.Add(new DiagnosticInfoWithSymbols(ErrorCode.ERR_NamedArgumentUsedInPositional, new object[] { identifierNameSyntax.Identifier.ValueText }, symbols), location);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0004C9B8 File Offset: 0x0004ABB8
		private static void ReportBadNonTrailingNamedArgument(MemberResolutionResult<TMember> bad, BindingDiagnosticBag diagnostics, AnalyzedArguments arguments, ImmutableArray<Symbol> symbols)
		{
			int num = bad.Result.BadArgumentsOpt[0];
			IdentifierNameSyntax identifierNameSyntax = arguments.Names[num];
			Location location = new SourceLocation(identifierNameSyntax);
			diagnostics.Add(new DiagnosticInfoWithSymbols(ErrorCode.ERR_BadNonTrailingNamedArgument, new object[] { identifierNameSyntax.Identifier.ValueText }, symbols), location);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0004CA1C File Offset: 0x0004AC1C
		private static void ReportDuplicateNamedArgument(MemberResolutionResult<TMember> result, BindingDiagnosticBag diagnostics, AnalyzedArguments arguments)
		{
			IdentifierNameSyntax identifierNameSyntax = arguments.Names[result.Result.BadArgumentsOpt[0]];
			diagnostics.Add(new CSDiagnosticInfo(ErrorCode.ERR_DuplicateNamedArgument, new object[] { identifierNameSyntax.Identifier.Text }), identifierNameSyntax.Location);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0004CA78 File Offset: 0x0004AC78
		private static void ReportNoCorrespondingNamedParameter(MemberResolutionResult<TMember> bad, string methodName, BindingDiagnosticBag diagnostics, AnalyzedArguments arguments, NamedTypeSymbol delegateTypeBeingInvoked, ImmutableArray<Symbol> symbols)
		{
			int num = bad.Result.BadArgumentsOpt[0];
			IdentifierNameSyntax identifierNameSyntax = arguments.Names[num];
			Location location = new SourceLocation(identifierNameSyntax);
			ErrorCode errorCode = ((delegateTypeBeingInvoked != null) ? ErrorCode.ERR_BadNamedArgumentForDelegateInvoke : ErrorCode.ERR_BadNamedArgument);
			object obj = delegateTypeBeingInvoked ?? methodName;
			diagnostics.Add(new DiagnosticInfoWithSymbols(errorCode, new object[]
			{
				obj,
				identifierNameSyntax.Identifier.ValueText
			}, symbols), location);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0004CAF8 File Offset: 0x0004ACF8
		private static void ReportMissingRequiredParameter(MemberResolutionResult<TMember> bad, BindingDiagnosticBag diagnostics, NamedTypeSymbol delegateTypeBeingInvoked, ImmutableArray<Symbol> symbols, Location location)
		{
			TMember member = bad.Member;
			ImmutableArray<ParameterSymbol> parameters = member.GetParameters();
			int badParameter = bad.Result.BadParameter;
			string text;
			if (badParameter == parameters.Length)
			{
				text = SyntaxFacts.GetText(SyntaxKind.ArgListKeyword);
			}
			else
			{
				text = parameters[badParameter].Name;
			}
			object obj = delegateTypeBeingInvoked ?? member;
			diagnostics.Add(new DiagnosticInfoWithSymbols(ErrorCode.ERR_NoCorrespondingArgument, new object[] { text, obj }, symbols), location);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0004CB7C File Offset: 0x0004AD7C
		private static void ReportBadParameterCount(BindingDiagnosticBag diagnostics, string name, AnalyzedArguments arguments, ImmutableArray<Symbol> symbols, Location location, NamedTypeSymbol typeContainingConstructor, NamedTypeSymbol delegateTypeBeingInvoked)
		{
			FunctionPointerMethodSymbol functionPointerMethodSymbol = ((symbols.IsDefault || symbols.Length != 1) ? null : (symbols[0] as FunctionPointerMethodSymbol));
			ValueTuple<ErrorCode, object> valueTuple;
			if (typeContainingConstructor == null)
			{
				if (delegateTypeBeingInvoked == null)
				{
					object obj = functionPointerMethodSymbol;
					if (obj == null)
					{
						valueTuple = new ValueTuple<ErrorCode, object>(ErrorCode.ERR_BadArgCount, name);
					}
					else
					{
						valueTuple = new ValueTuple<ErrorCode, object>(ErrorCode.ERR_BadFuncPointerArgCount, obj);
					}
				}
				else
				{
					valueTuple = new ValueTuple<ErrorCode, object>(ErrorCode.ERR_BadDelArgCount, delegateTypeBeingInvoked);
				}
			}
			else
			{
				valueTuple = new ValueTuple<ErrorCode, object>(ErrorCode.ERR_BadCtorArgCount, typeContainingConstructor);
			}
			ValueTuple<ErrorCode, object> valueTuple2 = valueTuple;
			ErrorCode item = valueTuple2.Item1;
			object item2 = valueTuple2.Item2;
			int num = arguments.Arguments.Count;
			if (arguments.IsExtensionMethodInvocation)
			{
				num--;
			}
			diagnostics.Add(new DiagnosticInfoWithSymbols(item, new object[] { item2, num }, symbols), location);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0004CC4C File Offset: 0x0004AE4C
		private bool HadConstructedParameterFailedConstraintCheck(ConversionsBase conversions, CSharpCompilation compilation, BindingDiagnosticBag diagnostics, Location location)
		{
			MemberResolutionResult<TMember> firstMemberKind = this.GetFirstMemberKind(MemberResolutionKind.ConstructedParameterFailedConstraintCheck);
			if (firstMemberKind.IsNull)
			{
				return false;
			}
			MethodSymbol methodSymbol = (MethodSymbol)((object)firstMemberKind.Member);
			MethodSymbol methodSymbol2 = methodSymbol;
			ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(compilation, conversions, false, location, diagnostics);
			if (!methodSymbol2.CheckConstraints(in checkConstraintsArgs))
			{
				return true;
			}
			methodSymbol.GetParameterType(firstMemberKind.Result.BadParameter).CheckAllConstraints(new ConstraintsHelper.CheckConstraintsArgsBoxed(compilation, conversions, false, location, diagnostics));
			return true;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0004CCBC File Offset: 0x0004AEBC
		private static bool HadLambdaConversionError(BindingDiagnosticBag diagnostics, AnalyzedArguments arguments)
		{
			bool flag = false;
			foreach (BoundExpression boundExpression in arguments.Arguments)
			{
				if (boundExpression.Kind == BoundKind.UnboundLambda)
				{
					flag |= ((UnboundLambda)boundExpression).GenerateSummaryErrors(diagnostics);
				}
			}
			return flag;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0004CD08 File Offset: 0x0004AF08
		private bool HadBadArguments(BindingDiagnosticBag diagnostics, Binder binder, string name, AnalyzedArguments arguments, ImmutableArray<Symbol> symbols, Location location, BinderFlags flags, bool isMethodGroupConversion)
		{
			MemberResolutionResult<TMember> firstMemberKind = this.GetFirstMemberKind(MemberResolutionKind.BadArgumentConversion);
			if (firstMemberKind.IsNull)
			{
				return false;
			}
			if (isMethodGroupConversion)
			{
				return true;
			}
			TMember member = firstMemberKind.Member;
			if (flags.Includes(BinderFlags.CollectionInitializerAddMethod))
			{
				ImmutableArray<ParameterSymbol>.Enumerator enumerator = member.GetParameters().GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.RefKind != RefKind.None)
					{
						diagnostics.Add(ErrorCode.ERR_InitializerAddHasParamModifiers, location, symbols, new object[] { member });
						return true;
					}
				}
				diagnostics.Add(ErrorCode.ERR_BadArgTypesForCollectionAdd, location, symbols, new object[] { member });
			}
			foreach (int num in firstMemberKind.Result.BadArgumentsOpt)
			{
				OverloadResolutionResult<TMember>.ReportBadArgumentError(diagnostics, binder, name, arguments, symbols, location, firstMemberKind, member, num);
			}
			return true;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0004CDF0 File Offset: 0x0004AFF0
		private static void ReportBadArgumentError(BindingDiagnosticBag diagnostics, Binder binder, string name, AnalyzedArguments arguments, ImmutableArray<Symbol> symbols, Location location, MemberResolutionResult<TMember> badArg, TMember method, int arg)
		{
			BoundExpression boundExpression = arguments.Argument(arg);
			if (boundExpression.HasAnyErrors)
			{
				return;
			}
			int num = badArg.Result.ParameterFromArgument(arg);
			SourceLocation sourceLocation = new SourceLocation(boundExpression.Syntax);
			if (method.GetIsVararg() && num == method.GetParameterCount())
			{
				diagnostics.Add(ErrorCode.ERR_BadArgType, sourceLocation, symbols, new object[]
				{
					arg + 1,
					boundExpression.Display,
					"__arglist"
				});
				return;
			}
			ParameterSymbol parameterSymbol = method.GetParameters()[num];
			bool flag = method.GetParameterCount() == num + 1;
			RefKind refKind = arguments.RefKind(arg);
			RefKind refKind2 = parameterSymbol.RefKind;
			if (arguments.IsExtensionMethodThisArgument(arg) && (refKind2 == RefKind.Ref || refKind2 == RefKind.In))
			{
				refKind = refKind2;
			}
			if (!boundExpression.HasExpressionType() && boundExpression.Kind != BoundKind.OutDeconstructVarPendingInference && boundExpression.Kind != BoundKind.OutVariablePendingInference && boundExpression.Kind != BoundKind.DiscardExpression)
			{
				TypeSymbol typeSymbol = OverloadResolutionResult<TMember>.UnwrapIfParamsArray(parameterSymbol, flag) as TypeSymbol;
				TypeSymbol typeSymbol2 = ((typeSymbol != null) ? typeSymbol : parameterSymbol.Type);
				if (boundExpression.Kind == BoundKind.UnboundLambda && refKind == refKind2)
				{
					((UnboundLambda)boundExpression).GenerateAnonymousFunctionConversionError(diagnostics, typeSymbol2);
					return;
				}
				if (boundExpression.Kind != BoundKind.MethodGroup || typeSymbol2.TypeKind != TypeKind.Delegate || !binder.Conversions.ReportDelegateOrFunctionPointerMethodGroupDiagnostics(binder, (BoundMethodGroup)boundExpression, typeSymbol2, diagnostics))
				{
					if (boundExpression.Kind == BoundKind.MethodGroup && typeSymbol2.TypeKind == TypeKind.FunctionPointer)
					{
						diagnostics.Add(ErrorCode.ERR_MissingAddressOf, sourceLocation);
						return;
					}
					if (boundExpression.Kind != BoundKind.UnconvertedAddressOfOperator || !binder.Conversions.ReportDelegateOrFunctionPointerMethodGroupDiagnostics(binder, ((BoundUnconvertedAddressOfOperator)boundExpression).Operand, typeSymbol2, diagnostics))
					{
						diagnostics.Add(ErrorCode.ERR_BadArgType, sourceLocation, symbols, new object[]
						{
							arg + 1,
							boundExpression.Display,
							OverloadResolutionResult<TMember>.UnwrapIfParamsArray(parameterSymbol, flag)
						});
						return;
					}
				}
			}
			else if (refKind != refKind2 && (refKind != RefKind.None || refKind2 != RefKind.In))
			{
				if (refKind2 == RefKind.None || refKind2 == RefKind.In)
				{
					diagnostics.Add(ErrorCode.ERR_BadArgExtraRef, sourceLocation, symbols, new object[]
					{
						arg + 1,
						refKind.ToArgumentDisplayString()
					});
					return;
				}
				diagnostics.Add(ErrorCode.ERR_BadArgRef, sourceLocation, symbols, new object[]
				{
					arg + 1,
					refKind2.ToParameterDisplayString()
				});
				return;
			}
			else
			{
				if (arguments.IsExtensionMethodThisArgument(arg))
				{
					diagnostics.Add(ErrorCode.ERR_BadInstanceArgType, sourceLocation, symbols, new object[] { boundExpression.Display, name, method, parameterSymbol });
					return;
				}
				TypeSymbol typeSymbol3 = boundExpression.Display as TypeSymbol;
				if (typeSymbol3 != null)
				{
					SignatureOnlyParameterSymbol signatureOnlyParameterSymbol = new SignatureOnlyParameterSymbol(TypeWithAnnotations.Create(typeSymbol3, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ImmutableArray<CustomModifier>.Empty, false, refKind);
					SymbolDistinguisher symbolDistinguisher = new SymbolDistinguisher(binder.Compilation, signatureOnlyParameterSymbol, OverloadResolutionResult<TMember>.UnwrapIfParamsArray(parameterSymbol, flag));
					diagnostics.Add(ErrorCode.ERR_BadArgType, sourceLocation, symbols, new object[]
					{
						arg + 1,
						symbolDistinguisher.First,
						symbolDistinguisher.Second
					});
					return;
				}
				diagnostics.Add(ErrorCode.ERR_BadArgType, sourceLocation, symbols, new object[]
				{
					arg + 1,
					boundExpression.Display,
					OverloadResolutionResult<TMember>.UnwrapIfParamsArray(parameterSymbol, flag)
				});
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0004D164 File Offset: 0x0004B364
		private static Symbol UnwrapIfParamsArray(ParameterSymbol parameter, bool isLastParameter)
		{
			if (parameter.IsParams && isLastParameter)
			{
				ArrayTypeSymbol arrayTypeSymbol = parameter.Type as ArrayTypeSymbol;
				if (arrayTypeSymbol != null && arrayTypeSymbol.IsSZArray)
				{
					return arrayTypeSymbol.ElementType;
				}
			}
			return parameter;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0004D19C File Offset: 0x0004B39C
		private bool HadAmbiguousWorseMethods(BindingDiagnosticBag diagnostics, ImmutableArray<Symbol> symbols, Location location, bool isQuery, BoundExpression receiver, string name)
		{
			MemberResolutionResult<TMember> memberResolutionResult;
			MemberResolutionResult<TMember> memberResolutionResult2;
			if (this.TryGetFirstTwoWorseResults(out memberResolutionResult, out memberResolutionResult2) <= 1)
			{
				return false;
			}
			if (isQuery)
			{
				diagnostics.Add(ErrorCode.ERR_QueryMultipleProviders, location, new object[] { receiver.Type, name });
			}
			else
			{
				diagnostics.Add(OverloadResolutionResult<TMember>.CreateAmbiguousCallDiagnosticInfo(memberResolutionResult.LeastOverriddenMember.OriginalDefinition, memberResolutionResult2.LeastOverriddenMember.OriginalDefinition, symbols), location);
			}
			return true;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0004D210 File Offset: 0x0004B410
		private int TryGetFirstTwoWorseResults(out MemberResolutionResult<TMember> first, out MemberResolutionResult<TMember> second)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			first = default(MemberResolutionResult<TMember>);
			second = default(MemberResolutionResult<TMember>);
			foreach (MemberResolutionResult<TMember> memberResolutionResult in this.ResultsBuilder)
			{
				if (memberResolutionResult.Result.Kind == MemberResolutionKind.Worse)
				{
					num++;
					if (!flag)
					{
						first = memberResolutionResult;
						flag = true;
					}
					else if (!flag2)
					{
						second = memberResolutionResult;
						flag2 = true;
					}
				}
			}
			return num;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0004D284 File Offset: 0x0004B484
		private bool HadAmbiguousBestMethods(BindingDiagnosticBag diagnostics, ImmutableArray<Symbol> symbols, Location location)
		{
			MemberResolutionResult<TMember> memberResolutionResult;
			MemberResolutionResult<TMember> memberResolutionResult2;
			if (this.TryGetFirstTwoValidResults(out memberResolutionResult, out memberResolutionResult2) <= 1)
			{
				return false;
			}
			diagnostics.Add(OverloadResolutionResult<TMember>.CreateAmbiguousCallDiagnosticInfo(memberResolutionResult.LeastOverriddenMember.OriginalDefinition, memberResolutionResult2.LeastOverriddenMember.OriginalDefinition, symbols), location);
			return true;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0004D2D0 File Offset: 0x0004B4D0
		private int TryGetFirstTwoValidResults(out MemberResolutionResult<TMember> first, out MemberResolutionResult<TMember> second)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			first = default(MemberResolutionResult<TMember>);
			second = default(MemberResolutionResult<TMember>);
			foreach (MemberResolutionResult<TMember> memberResolutionResult in this.ResultsBuilder)
			{
				if (memberResolutionResult.Result.IsValid)
				{
					num++;
					if (!flag)
					{
						first = memberResolutionResult;
						flag = true;
					}
					else if (!flag2)
					{
						second = memberResolutionResult;
						flag2 = true;
					}
				}
			}
			return num;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0004D344 File Offset: 0x0004B544
		private static DiagnosticInfoWithSymbols CreateAmbiguousCallDiagnosticInfo(Symbol first, Symbol second, ImmutableArray<Symbol> symbols)
		{
			object[] array2;
			if (!(first.ContainingNamespace != second.ContainingNamespace))
			{
				object[] array = new object[2];
				array[0] = first;
				array2 = array;
				array[1] = second;
			}
			else
			{
				object[] array3 = new object[2];
				array3[0] = new FormattedSymbol(first, SymbolDisplayFormat.CSharpErrorMessageFormat);
				array2 = array3;
				array3[1] = new FormattedSymbol(second, SymbolDisplayFormat.CSharpErrorMessageFormat);
			}
			object[] array4 = array2;
			return new DiagnosticInfoWithSymbols(ErrorCode.ERR_AmbigCall, array4, symbols);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		[Conditional("DEBUG")]
		private void AssertNone(MemberResolutionKind kind)
		{
			foreach (MemberResolutionResult<TMember> memberResolutionResult in this.ResultsBuilder)
			{
				if (memberResolutionResult.Result.Kind == kind)
				{
					throw ExceptionUtilities.UnexpectedValue(kind);
				}
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0004D3E8 File Offset: 0x0004B5E8
		private MemberResolutionResult<TMember> GetFirstMemberKind(MemberResolutionKind kind)
		{
			foreach (MemberResolutionResult<TMember> memberResolutionResult in this.ResultsBuilder)
			{
				if (memberResolutionResult.Result.Kind == kind)
				{
					return memberResolutionResult;
				}
			}
			return default(MemberResolutionResult<TMember>);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0004D42E File Offset: 0x0004B62E
		internal static OverloadResolutionResult<TMember> GetInstance()
		{
			return OverloadResolutionResult<TMember>.s_pool.Allocate();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0004D43A File Offset: 0x0004B63A
		internal void Free()
		{
			this.Clear();
			OverloadResolutionResult<TMember>.s_pool.Free(this);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0004D44D File Offset: 0x0004B64D
		private static ObjectPool<OverloadResolutionResult<TMember>> CreatePool()
		{
			return new ObjectPool<OverloadResolutionResult<TMember>>(() => new OverloadResolutionResult<TMember>(), 10);
		}

		// Token: 0x0400045B RID: 1115
		private MemberResolutionResult<TMember> _bestResult;

		// Token: 0x0400045C RID: 1116
		private ThreeState _bestResultState;

		// Token: 0x0400045D RID: 1117
		internal readonly ArrayBuilder<MemberResolutionResult<TMember>> ResultsBuilder;

		// Token: 0x0400045E RID: 1118
		private static readonly ObjectPool<OverloadResolutionResult<TMember>> s_pool = OverloadResolutionResult<TMember>.CreatePool();
	}
}
