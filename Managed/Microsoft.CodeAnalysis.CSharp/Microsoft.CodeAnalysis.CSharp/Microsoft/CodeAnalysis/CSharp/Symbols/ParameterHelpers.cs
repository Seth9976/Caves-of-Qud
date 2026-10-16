using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000525 RID: 1317
	internal static class ParameterHelpers
	{
		// Token: 0x06006EAE RID: 28334 RVA: 0x00196454 File Offset: 0x00194654
		public static ImmutableArray<ParameterSymbol> MakeParameters(Binder binder, Symbol owner, BaseParameterListSyntax syntax, out SyntaxToken arglistToken, BindingDiagnosticBag diagnostics, bool allowRefOrOut, bool allowThis, bool addRefReadOnlyModifier)
		{
			return ParameterHelpers.MakeParameters<ParameterSyntax, ParameterSymbol, Symbol>(binder, owner, syntax.Parameters, out arglistToken, diagnostics, allowRefOrOut, allowThis, addRefReadOnlyModifier, false, syntax.Parameters.Count - 1, (Binder context, Symbol owner, TypeWithAnnotations parameterType, ParameterSyntax syntax, RefKind refKind, int ordinal, SyntaxToken paramsKeyword, SyntaxToken thisKeyword, bool addRefReadOnlyModifier, BindingDiagnosticBag declarationDiagnostics) => SourceParameterSymbol.Create(context, owner, parameterType, syntax, refKind, syntax.Identifier, ordinal, paramsKeyword.Kind() > SyntaxKind.None, ordinal == 0 && thisKeyword.Kind() > SyntaxKind.None, addRefReadOnlyModifier, declarationDiagnostics), false);
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x001964A8 File Offset: 0x001946A8
		public static ImmutableArray<FunctionPointerParameterSymbol> MakeFunctionPointerParameters(Binder binder, FunctionPointerMethodSymbol owner, SeparatedSyntaxList<FunctionPointerParameterSyntax> parametersList, BindingDiagnosticBag diagnostics, bool suppressUseSiteDiagnostics)
		{
			SyntaxToken syntaxToken;
			return ParameterHelpers.MakeParameters<FunctionPointerParameterSyntax, FunctionPointerParameterSymbol, FunctionPointerMethodSymbol>(binder, owner, parametersList, out syntaxToken, diagnostics, true, false, true, suppressUseSiteDiagnostics, parametersList.Count - 2, delegate(Binder binder, FunctionPointerMethodSymbol owner, TypeWithAnnotations parameterType, FunctionPointerParameterSyntax syntax, RefKind refKind, int ordinal, SyntaxToken paramsKeyword, SyntaxToken thisKeyword, bool addRefReadOnlyModifier, BindingDiagnosticBag diagnostics)
			{
				ImmutableArray<CustomModifier> immutableArray;
				if (refKind != RefKind.Out)
				{
					if (refKind == RefKind.In)
					{
						immutableArray = ParameterHelpers.CreateInModifiers(binder, diagnostics, syntax);
					}
					else
					{
						immutableArray = ImmutableArray<CustomModifier>.Empty;
					}
				}
				else
				{
					immutableArray = ParameterHelpers.CreateOutModifiers(binder, diagnostics, syntax);
				}
				ImmutableArray<CustomModifier> immutableArray2 = immutableArray;
				if (parameterType.IsVoidType())
				{
					diagnostics.Add(ErrorCode.ERR_NoVoidParameter, syntax.Type.Location);
				}
				return new FunctionPointerParameterSymbol(parameterType, refKind, ordinal, owner, immutableArray2);
			}, true);
		}

		// Token: 0x06006EB0 RID: 28336 RVA: 0x001964F0 File Offset: 0x001946F0
		private static ImmutableArray<TParameterSymbol> MakeParameters<TParameterSyntax, TParameterSymbol, TOwningSymbol>(Binder binder, TOwningSymbol owner, SeparatedSyntaxList<TParameterSyntax> parametersList, out SyntaxToken arglistToken, BindingDiagnosticBag diagnostics, bool allowRefOrOut, bool allowThis, bool addRefReadOnlyModifier, bool suppressUseSiteDiagnostics, int lastIndex, Func<Binder, TOwningSymbol, TypeWithAnnotations, TParameterSyntax, RefKind, int, SyntaxToken, SyntaxToken, bool, BindingDiagnosticBag, TParameterSymbol> parameterCreationFunc, bool parsingFunctionPointer = false) where TParameterSyntax : BaseParameterSyntax where TParameterSymbol : ParameterSymbol where TOwningSymbol : Symbol
		{
			arglistToken = default(SyntaxToken);
			int num = 0;
			int num2 = -1;
			ArrayBuilder<TParameterSymbol> instance = ArrayBuilder<TParameterSymbol>.GetInstance();
			ParameterSyntax parameterSyntax = null;
			foreach (TParameterSyntax tparameterSyntax in parametersList)
			{
				if (num > lastIndex)
				{
					break;
				}
				ParameterHelpers.CheckParameterModifiers(tparameterSyntax, diagnostics, parsingFunctionPointer);
				SyntaxToken syntaxToken;
				SyntaxToken syntaxToken2;
				SyntaxToken syntaxToken3;
				RefKind modifiers = ParameterHelpers.GetModifiers(tparameterSyntax.Modifiers, out syntaxToken, out syntaxToken2, out syntaxToken3);
				if (syntaxToken3.Kind() != SyntaxKind.None && !allowThis)
				{
					diagnostics.Add(ErrorCode.ERR_ThisInBadContext, syntaxToken3.GetLocation());
				}
				ParameterSyntax parameterSyntax2 = tparameterSyntax as ParameterSyntax;
				if (parameterSyntax2 != null)
				{
					if (parameterSyntax == null && (parameterSyntax2.Modifiers.Any(SyntaxKind.ParamsKeyword) || parameterSyntax2.Identifier.Kind() == SyntaxKind.ArgListKeyword))
					{
						parameterSyntax = parameterSyntax2;
					}
					if (parameterSyntax2.IsArgList)
					{
						arglistToken = parameterSyntax2.Identifier;
						if (syntaxToken2.Kind() != SyntaxKind.None || syntaxToken.Kind() != SyntaxKind.None || syntaxToken3.Kind() != SyntaxKind.None)
						{
							diagnostics.Add(ErrorCode.ERR_IllegalVarArgs, arglistToken.GetLocation());
							continue;
						}
						continue;
					}
					else if (parameterSyntax2.Default != null && num2 == -1)
					{
						num2 = num;
					}
				}
				TypeWithAnnotations typeWithAnnotations = binder.BindType(tparameterSyntax.Type, diagnostics, null, suppressUseSiteDiagnostics);
				if (!allowRefOrOut && (modifiers == RefKind.Ref || modifiers == RefKind.Out))
				{
					diagnostics.Add(ErrorCode.ERR_IllegalRefParam, syntaxToken.GetLocation());
				}
				TParameterSymbol tparameterSymbol = parameterCreationFunc.Invoke(binder, owner, typeWithAnnotations, tparameterSyntax, modifiers, num, syntaxToken2, syntaxToken3, addRefReadOnlyModifier, diagnostics);
				ParameterHelpers.ReportParameterErrors(owner, tparameterSyntax, tparameterSymbol, syntaxToken3, syntaxToken2, num2, diagnostics);
				instance.Add(tparameterSymbol);
				num++;
			}
			if (parameterSyntax != null && parameterSyntax != parametersList[lastIndex])
			{
				diagnostics.Add((parameterSyntax.Identifier.Kind() == SyntaxKind.ArgListKeyword) ? ErrorCode.ERR_VarargsLast : ErrorCode.ERR_ParamsLast, parameterSyntax.GetLocation());
			}
			ImmutableArray<TParameterSymbol> immutableArray = instance.ToImmutableAndFree();
			if (!parsingFunctionPointer)
			{
				MethodSymbol methodSymbol = owner as MethodSymbol;
				ImmutableArray<TypeParameterSymbol> immutableArray2 = ((methodSymbol != null) ? methodSymbol.TypeParameters : default(ImmutableArray<TypeParameterSymbol>));
				bool flag = binder.Compilation.IsFeatureEnabled(MessageID.IDS_FeatureNameShadowingInNestedFunctions) && methodSymbol != null && methodSymbol.MethodKind == MethodKind.LocalFunction;
				binder.ValidateParameterNameConflicts(immutableArray2, immutableArray.Cast<TParameterSymbol, ParameterSymbol>(), flag, diagnostics);
			}
			return immutableArray;
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x00196750 File Offset: 0x00194950
		internal static void EnsureIsReadOnlyAttributeExists(CSharpCompilation compilation, ImmutableArray<ParameterSymbol> parameters, BindingDiagnosticBag diagnostics, bool modifyCompilation)
		{
			if (compilation == null)
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (parameterSymbol.RefKind == RefKind.In)
				{
					compilation.EnsureIsReadOnlyAttributeExists(diagnostics, ParameterHelpers.GetParameterLocation(parameterSymbol), modifyCompilation);
				}
			}
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x00196794 File Offset: 0x00194994
		internal static void EnsureNativeIntegerAttributeExists(CSharpCompilation compilation, ImmutableArray<ParameterSymbol> parameters, BindingDiagnosticBag diagnostics, bool modifyCompilation)
		{
			if (compilation == null)
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (parameterSymbol.TypeWithAnnotations.ContainsNativeInteger())
				{
					compilation.EnsureNativeIntegerAttributeExists(diagnostics, ParameterHelpers.GetParameterLocation(parameterSymbol), modifyCompilation);
				}
			}
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x001967DC File Offset: 0x001949DC
		internal static void EnsureNullableAttributeExists(CSharpCompilation compilation, Symbol container, ImmutableArray<ParameterSymbol> parameters, BindingDiagnosticBag diagnostics, bool modifyCompilation)
		{
			if (compilation == null)
			{
				return;
			}
			if (parameters.Length > 0 && compilation.ShouldEmitNullableAttributes(container))
			{
				foreach (ParameterSymbol parameterSymbol in parameters)
				{
					if (parameterSymbol.TypeWithAnnotations.NeedsNullableAttribute())
					{
						compilation.EnsureNullableAttributeExists(diagnostics, ParameterHelpers.GetParameterLocation(parameterSymbol), modifyCompilation);
					}
				}
			}
		}

		// Token: 0x06006EB4 RID: 28340 RVA: 0x0019683A File Offset: 0x00194A3A
		private static Location GetParameterLocation(ParameterSymbol parameter)
		{
			return parameter.GetNonNullSyntaxNode().Location;
		}

		// Token: 0x06006EB5 RID: 28341 RVA: 0x00196848 File Offset: 0x00194A48
		private static void CheckParameterModifiers(BaseParameterSyntax parameter, BindingDiagnosticBag diagnostics, bool parsingFunctionPointerParams)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			foreach (SyntaxToken syntaxToken in parameter.Modifiers)
			{
				SyntaxKind syntaxKind = syntaxToken.Kind();
				if (syntaxKind != SyntaxKind.ReadOnlyKeyword)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.RefKeyword:
						if (flag2)
						{
							diagnostics.Add(ErrorCode.ERR_DupParamMod, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.RefKeyword) });
							continue;
						}
						if (flag4)
						{
							diagnostics.Add(ErrorCode.ERR_ParamsCantBeWithModifier, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.RefKeyword) });
							continue;
						}
						if (flag3)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.RefKeyword),
								SyntaxFacts.GetText(SyntaxKind.OutKeyword)
							});
							continue;
						}
						if (flag5)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.RefKeyword),
								SyntaxFacts.GetText(SyntaxKind.InKeyword)
							});
							continue;
						}
						flag2 = true;
						continue;
					case SyntaxKind.OutKeyword:
						if (flag3)
						{
							diagnostics.Add(ErrorCode.ERR_DupParamMod, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.OutKeyword) });
							continue;
						}
						if (flag)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.OutKeyword),
								SyntaxFacts.GetText(SyntaxKind.ThisKeyword)
							});
							continue;
						}
						if (flag4)
						{
							diagnostics.Add(ErrorCode.ERR_ParamsCantBeWithModifier, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.OutKeyword) });
							continue;
						}
						if (flag2)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.OutKeyword),
								SyntaxFacts.GetText(SyntaxKind.RefKeyword)
							});
							continue;
						}
						if (flag5)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.OutKeyword),
								SyntaxFacts.GetText(SyntaxKind.InKeyword)
							});
							continue;
						}
						flag3 = true;
						continue;
					case SyntaxKind.InKeyword:
						if (flag5)
						{
							diagnostics.Add(ErrorCode.ERR_DupParamMod, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.InKeyword) });
							continue;
						}
						if (flag3)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.InKeyword),
								SyntaxFacts.GetText(SyntaxKind.OutKeyword)
							});
							continue;
						}
						if (flag2)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.InKeyword),
								SyntaxFacts.GetText(SyntaxKind.RefKeyword)
							});
							continue;
						}
						if (flag4)
						{
							diagnostics.Add(ErrorCode.ERR_ParamsCantBeWithModifier, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.InKeyword) });
							continue;
						}
						flag5 = true;
						continue;
					case SyntaxKind.IsKeyword:
					case SyntaxKind.AsKeyword:
						goto IL_04F6;
					case SyntaxKind.ParamsKeyword:
						if (!parsingFunctionPointerParams)
						{
							if (flag4)
							{
								diagnostics.Add(ErrorCode.ERR_DupParamMod, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.ParamsKeyword) });
								continue;
							}
							if (flag)
							{
								diagnostics.Add(ErrorCode.ERR_BadParamModThis, syntaxToken.GetLocation());
								continue;
							}
							if (flag2)
							{
								diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
								{
									SyntaxFacts.GetText(SyntaxKind.ParamsKeyword),
									SyntaxFacts.GetText(SyntaxKind.RefKeyword)
								});
								continue;
							}
							if (flag5)
							{
								diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
								{
									SyntaxFacts.GetText(SyntaxKind.ParamsKeyword),
									SyntaxFacts.GetText(SyntaxKind.InKeyword)
								});
								continue;
							}
							if (flag3)
							{
								diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
								{
									SyntaxFacts.GetText(SyntaxKind.ParamsKeyword),
									SyntaxFacts.GetText(SyntaxKind.OutKeyword)
								});
								continue;
							}
							flag4 = true;
							continue;
						}
						else if (!parsingFunctionPointerParams)
						{
							goto IL_04F6;
						}
						break;
					default:
						if (syntaxKind != SyntaxKind.ThisKeyword)
						{
							goto IL_04F6;
						}
						if (flag)
						{
							diagnostics.Add(ErrorCode.ERR_DupParamMod, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(SyntaxKind.ThisKeyword) });
							continue;
						}
						if (flag3)
						{
							diagnostics.Add(ErrorCode.ERR_BadParameterModifiers, syntaxToken.GetLocation(), new object[]
							{
								SyntaxFacts.GetText(SyntaxKind.ThisKeyword),
								SyntaxFacts.GetText(SyntaxKind.OutKeyword)
							});
							continue;
						}
						if (flag4)
						{
							diagnostics.Add(ErrorCode.ERR_BadParamModThis, syntaxToken.GetLocation());
							continue;
						}
						flag = true;
						continue;
					}
				}
				else if (!parsingFunctionPointerParams)
				{
					goto IL_04F6;
				}
				diagnostics.Add(ErrorCode.ERR_BadFuncPointerParamModifier, syntaxToken.GetLocation(), new object[] { SyntaxFacts.GetText(syntaxToken.Kind()) });
				continue;
				IL_04F6:
				throw ExceptionUtilities.UnexpectedValue(syntaxToken.Kind());
			}
		}

		// Token: 0x06006EB6 RID: 28342 RVA: 0x00196D6C File Offset: 0x00194F6C
		private static void ReportParameterErrors(Symbol owner, BaseParameterSyntax parameterSyntax, ParameterSymbol parameter, SyntaxToken thisKeyword, SyntaxToken paramsKeyword, int firstDefault, BindingDiagnosticBag diagnostics)
		{
			int ordinal = parameter.Ordinal;
			ParameterSyntax parameterSyntax2 = parameterSyntax as ParameterSyntax;
			bool flag = parameterSyntax2 != null && parameterSyntax2.Default != null;
			if (thisKeyword.Kind() == SyntaxKind.ThisKeyword && ordinal != 0)
			{
				diagnostics.Add(ErrorCode.ERR_BadThisParam, thisKeyword.GetLocation(), new object[] { owner.Name });
				return;
			}
			if (parameter.IsParams && owner.IsOperator())
			{
				diagnostics.Add(ErrorCode.ERR_IllegalParams, paramsKeyword.GetLocation());
				return;
			}
			if (parameter.IsParams && !parameter.TypeWithAnnotations.IsSZArray())
			{
				diagnostics.Add(ErrorCode.ERR_ParamsMustBeArray, paramsKeyword.GetLocation());
				return;
			}
			if (parameter.TypeWithAnnotations.IsStatic)
			{
				NamedTypeSymbol containingType = parameter.ContainingSymbol.ContainingType;
				diagnostics.Add(ErrorFacts.GetStaticClassParameterCode(containingType != null && containingType.IsInterfaceType()), owner.Locations.IsEmpty ? parameterSyntax.GetLocation() : owner.Locations[0], new object[] { parameter.Type });
				return;
			}
			if (firstDefault != -1 && ordinal > firstDefault && !flag && !parameter.IsParams)
			{
				Location location = ((ParameterSyntax)parameterSyntax).Identifier.GetNextToken(true, false, false, false).GetLocation();
				diagnostics.Add(ErrorCode.ERR_DefaultValueBeforeRequiredValue, location);
				return;
			}
			if (parameter.RefKind != RefKind.None && parameter.TypeWithAnnotations.IsRestrictedType(true))
			{
				diagnostics.Add(ErrorCode.ERR_MethodArgCantBeRefAny, parameterSyntax.Location, new object[] { parameter.Type });
			}
		}

		// Token: 0x06006EB7 RID: 28343 RVA: 0x00196F10 File Offset: 0x00195110
		internal static bool ReportDefaultParameterErrors(Binder binder, Symbol owner, ParameterSyntax parameterSyntax, SourceParameterSymbol parameter, BoundExpression defaultExpression, BoundExpression convertedExpression, BindingDiagnosticBag diagnostics)
		{
			bool flag = false;
			TypeSymbol type = parameter.Type;
			CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = binder.GetNewCompoundUseSiteInfo(diagnostics);
			Conversion conversion = binder.Conversions.ClassifyImplicitConversionFromExpression(defaultExpression, type, ref newCompoundUseSiteInfo);
			diagnostics.Add(defaultExpression.Syntax, newCompoundUseSiteInfo);
			SyntaxToken syntaxToken;
			SyntaxToken syntaxToken2;
			SyntaxToken syntaxToken3;
			RefKind modifiers = ParameterHelpers.GetModifiers(parameterSyntax.Modifiers, out syntaxToken, out syntaxToken2, out syntaxToken3);
			if (modifiers == RefKind.Ref || modifiers == RefKind.Out)
			{
				diagnostics.Add(ErrorCode.ERR_RefOutDefaultValue, syntaxToken.GetLocation());
				flag = true;
			}
			else if (syntaxToken2.Kind() == SyntaxKind.ParamsKeyword)
			{
				diagnostics.Add(ErrorCode.ERR_DefaultValueForParamsParameter, syntaxToken2.GetLocation());
				flag = true;
			}
			else if (syntaxToken3.Kind() == SyntaxKind.ThisKeyword)
			{
				if (parameter.Ordinal == 0)
				{
					diagnostics.Add(ErrorCode.ERR_DefaultValueForExtensionParameter, syntaxToken3.GetLocation());
					flag = true;
				}
			}
			else if (!defaultExpression.HasAnyErrors && !ParameterHelpers.IsValidDefaultValue(defaultExpression.IsImplicitObjectCreation() ? convertedExpression : defaultExpression))
			{
				diagnostics.Add(ErrorCode.ERR_DefaultValueMustBeConstant, parameterSyntax.Default.Value.Location, new object[] { parameterSyntax.Identifier.ValueText });
				flag = true;
			}
			else if (!conversion.Exists || conversion.IsUserDefined || (conversion.IsIdentity && type.SpecialType == SpecialType.System_Object && defaultExpression.Type.IsDynamic()))
			{
				diagnostics.Add(ErrorCode.ERR_NoConversionForDefaultParam, parameterSyntax.Identifier.GetLocation(), new object[] { defaultExpression.Display, type });
				flag = true;
			}
			else if ((conversion.IsReference && (type.SpecialType == SpecialType.System_Object || type.Kind == SymbolKind.DynamicType) && defaultExpression.Type != null && defaultExpression.Type.SpecialType == SpecialType.System_String) || conversion.IsBoxing)
			{
				diagnostics.Add(ErrorCode.ERR_NotNullRefDefaultParameter, parameterSyntax.Identifier.GetLocation(), new object[]
				{
					parameterSyntax.Identifier.ValueText,
					type
				});
				flag = true;
			}
			else if (conversion.IsNullable && !defaultExpression.Type.IsNullableType() && !type.GetNullableUnderlyingType().IsEnumType() && !type.GetNullableUnderlyingType().IsIntrinsicType())
			{
				diagnostics.Add(ErrorCode.ERR_NoConversionForNubDefaultParam, parameterSyntax.Identifier.GetLocation(), new object[]
				{
					defaultExpression.Type,
					parameterSyntax.Identifier.ValueText
				});
				flag = true;
			}
			ConstantValueUtils.CheckLangVersionForConstantValue(convertedExpression, diagnostics);
			if (owner.IsExplicitInterfaceImplementation() || owner.IsPartialImplementation() || owner.IsOperator())
			{
				diagnostics.Add(ErrorCode.WRN_DefaultValueForUnconsumedLocation, parameterSyntax.Identifier.GetLocation(), new object[] { parameterSyntax.Identifier.ValueText });
			}
			return flag;
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x001971EC File Offset: 0x001953EC
		private static bool IsValidDefaultValue(BoundExpression expression)
		{
			if (expression.ConstantValue != null)
			{
				return true;
			}
			BoundKind kind = expression.Kind;
			return kind - BoundKind.DefaultLiteral <= 1 || (kind == BoundKind.ObjectCreationExpression && ParameterHelpers.IsValidDefaultValue((BoundObjectCreationExpression)expression));
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x00197230 File Offset: 0x00195430
		private static bool IsValidDefaultValue(BoundObjectCreationExpression expression)
		{
			return expression.Constructor.IsDefaultValueTypeConstructor() && expression.InitializerExpressionOpt == null;
		}

		// Token: 0x06006EBA RID: 28346 RVA: 0x0019724C File Offset: 0x0019544C
		internal static MethodSymbol FindContainingGenericMethod(Symbol symbol)
		{
			for (Symbol symbol2 = symbol; symbol2 != null; symbol2 = symbol2.ContainingSymbol)
			{
				if (symbol2.Kind == SymbolKind.Method)
				{
					MethodSymbol methodSymbol = (MethodSymbol)symbol2;
					if (methodSymbol.MethodKind != MethodKind.AnonymousFunction)
					{
						if (!methodSymbol.IsGenericMethod)
						{
							return null;
						}
						return methodSymbol;
					}
				}
			}
			return null;
		}

		// Token: 0x06006EBB RID: 28347 RVA: 0x00197290 File Offset: 0x00195490
		private static RefKind GetModifiers(SyntaxTokenList modifiers, out SyntaxToken refnessKeyword, out SyntaxToken paramsKeyword, out SyntaxToken thisKeyword)
		{
			RefKind refKind = RefKind.None;
			refnessKeyword = default(SyntaxToken);
			paramsKeyword = default(SyntaxToken);
			thisKeyword = default(SyntaxToken);
			foreach (SyntaxToken syntaxToken in modifiers)
			{
				SyntaxKind syntaxKind = syntaxToken.Kind();
				switch (syntaxKind)
				{
				case SyntaxKind.RefKeyword:
					if (refKind == RefKind.None)
					{
						refnessKeyword = syntaxToken;
						refKind = RefKind.Ref;
					}
					break;
				case SyntaxKind.OutKeyword:
					if (refKind == RefKind.None)
					{
						refnessKeyword = syntaxToken;
						refKind = RefKind.Out;
					}
					break;
				case SyntaxKind.InKeyword:
					if (refKind == RefKind.None)
					{
						refnessKeyword = syntaxToken;
						refKind = RefKind.In;
					}
					break;
				case SyntaxKind.IsKeyword:
				case SyntaxKind.AsKeyword:
					break;
				case SyntaxKind.ParamsKeyword:
					paramsKeyword = syntaxToken;
					break;
				default:
					if (syntaxKind == SyntaxKind.ThisKeyword)
					{
						thisKeyword = syntaxToken;
					}
					break;
				}
			}
			return refKind;
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x0019733F File Offset: 0x0019553F
		internal static ImmutableArray<CustomModifier> ConditionallyCreateInModifiers(RefKind refKind, bool addRefReadOnlyModifier, Binder binder, BindingDiagnosticBag diagnostics, SyntaxNode syntax)
		{
			if (addRefReadOnlyModifier && refKind == RefKind.In)
			{
				return ParameterHelpers.CreateInModifiers(binder, diagnostics, syntax);
			}
			return ImmutableArray<CustomModifier>.Empty;
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x00197357 File Offset: 0x00195557
		internal static ImmutableArray<CustomModifier> CreateInModifiers(Binder binder, BindingDiagnosticBag diagnostics, SyntaxNode syntax)
		{
			return ParameterHelpers.CreateModifiers(WellKnownType.System_Runtime_InteropServices_InAttribute, binder, diagnostics, syntax);
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x00197366 File Offset: 0x00195566
		internal static ImmutableArray<CustomModifier> CreateOutModifiers(Binder binder, BindingDiagnosticBag diagnostics, SyntaxNode syntax)
		{
			return ParameterHelpers.CreateModifiers(WellKnownType.System_Runtime_InteropServices_OutAttribute, binder, diagnostics, syntax);
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x00197375 File Offset: 0x00195575
		private static ImmutableArray<CustomModifier> CreateModifiers(WellKnownType modifier, Binder binder, BindingDiagnosticBag diagnostics, SyntaxNode syntax)
		{
			return ImmutableArray.Create<CustomModifier>(CSharpCustomModifier.CreateRequired(binder.GetWellKnownType(modifier, diagnostics, syntax)));
		}
	}
}
