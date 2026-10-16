using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000138 RID: 312
	internal static class ErrorFacts
	{
		// Token: 0x060018FA RID: 6394 RVA: 0x0008563C File Offset: 0x0008383C
		static ErrorFacts()
		{
			ImmutableHashSet<string>.Builder builder = ImmutableHashSet.CreateBuilder<string>();
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullReferenceAssignment));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullReferenceReceiver));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullReferenceReturn));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullReferenceArgument));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_UninitializedNonNullableField));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInAssignment));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInArgument));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInArgumentForOutput));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInReturnTypeOfTargetDelegate));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInParameterTypeOfTargetDelegate));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullAsNonNullable));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullableValueTypeMayBeNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInTypeParameterConstraint));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInTypeParameterNotNullConstraint));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ThrowPossibleNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_UnboxPossibleNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_SwitchExpressionNotExhaustiveForNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_SwitchExpressionNotExhaustiveForNullWithWhen));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ConvertingNullableToNonNullable));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_DisallowNullAttributeForbidsMaybeNullAssignment));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ParameterConditionallyDisallowsNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ShouldNotReturn));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInTypeOnOverride));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInReturnTypeOnOverride));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInReturnTypeOnPartial));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInParameterTypeOnOverride));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInParameterTypeOnPartial));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInTypeOnImplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInTypeOnExplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInConstraintsOnImplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInExplicitlyImplementedInterface));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInInterfaceImplementedByBase));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullabilityMismatchInConstraintsOnPartialImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_NullReferenceInitializer));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ShouldNotReturn));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_DoesNotReturnMismatch));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_MemberNotNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_MemberNotNullBadMember));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_MemberNotNullWhen));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ParameterDisallowsNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ParameterNotNullIfNotNull));
			builder.Add(ErrorFacts.GetId(ErrorCode.WRN_ReturnNotNullIfNotNull));
			ErrorFacts.NullableWarnings = builder.ToImmutable();
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00085A04 File Offset: 0x00083C04
		private static string GetId(ErrorCode errorCode)
		{
			return MessageProvider.Instance.GetIdForErrorCode((int)errorCode);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00085A11 File Offset: 0x00083C11
		private static ImmutableDictionary<ErrorCode, string> CreateCategoriesMap()
		{
			return new Dictionary<ErrorCode, string>().ToImmutableDictionary<ErrorCode, string>();
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00085A1D File Offset: 0x00083C1D
		internal static DiagnosticSeverity GetSeverity(ErrorCode code)
		{
			if (code == ErrorCode.Void)
			{
				return (DiagnosticSeverity)(-2);
			}
			if (code == ErrorCode.Unknown)
			{
				return (DiagnosticSeverity)(-1);
			}
			if (ErrorFacts.IsWarning(code))
			{
				return DiagnosticSeverity.Warning;
			}
			if (ErrorFacts.IsInfo(code))
			{
				return DiagnosticSeverity.Info;
			}
			if (ErrorFacts.IsHidden(code))
			{
				return DiagnosticSeverity.Hidden;
			}
			return DiagnosticSeverity.Error;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00085A4C File Offset: 0x00083C4C
		public static string GetMessage(MessageID code, CultureInfo culture)
		{
			return ErrorFacts.ResourceManager.GetString(code.ToString(), culture);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00085A66 File Offset: 0x00083C66
		public static string GetMessage(ErrorCode code, CultureInfo culture)
		{
			return ErrorFacts.ResourceManager.GetString(code.ToString(), culture);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00085A80 File Offset: 0x00083C80
		public static LocalizableResourceString GetMessageFormat(ErrorCode code)
		{
			return new LocalizableResourceString(code.ToString(), ErrorFacts.ResourceManager, typeof(ErrorFacts));
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00085AA3 File Offset: 0x00083CA3
		public static LocalizableResourceString GetTitle(ErrorCode code)
		{
			return new LocalizableResourceString(code.ToString() + "_Title", ErrorFacts.ResourceManager, typeof(ErrorFacts));
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00085AD0 File Offset: 0x00083CD0
		public static LocalizableResourceString GetDescription(ErrorCode code)
		{
			return new LocalizableResourceString(code.ToString() + "_Description", ErrorFacts.ResourceManager, typeof(ErrorFacts));
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00085AFD File Offset: 0x00083CFD
		public static string GetHelpLink(ErrorCode code)
		{
			return "https://msdn.microsoft.com/query/roslyn.query?appId=roslyn&k=k(" + ErrorFacts.GetId(code) + ")";
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00085B14 File Offset: 0x00083D14
		public static string GetCategory(ErrorCode code)
		{
			string text;
			if (ErrorFacts.s_categoriesMap.Value.TryGetValue(code, out text))
			{
				return text;
			}
			return "Compiler";
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00085B3C File Offset: 0x00083D3C
		public static string GetMessage(XmlParseErrorCode id, CultureInfo culture)
		{
			return ErrorFacts.ResourceManager.GetString(id.ToString(), culture);
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x00085B56 File Offset: 0x00083D56
		private static ResourceManager ResourceManager
		{
			get
			{
				if (ErrorFacts.s_resourceManager == null)
				{
					ErrorFacts.s_resourceManager = new ResourceManager(typeof(CSharpResources).FullName, IntrospectionExtensions.GetTypeInfo(typeof(ErrorCode)).Assembly);
				}
				return ErrorFacts.s_resourceManager;
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00085B94 File Offset: 0x00083D94
		internal static int GetWarningLevel(ErrorCode code)
		{
			if (ErrorFacts.IsInfo(code) || ErrorFacts.IsHidden(code))
			{
				return 1;
			}
			if (code > ErrorCode.WRN_DotOnDefault)
			{
				if (code <= ErrorCode.WRN_UnqualifiedNestedTypeInCref)
				{
					if (code <= ErrorCode.WRN_BadUILang)
					{
						if (code <= ErrorCode.WRN_DynamicDispatchToConditionalMethod)
						{
							if (code <= ErrorCode.WRN_ReferencedAssemblyReferencesLinkedPIA)
							{
								if (code == ErrorCode.WRN_BadXMLRefTypeVar)
								{
									return 1;
								}
								if (code - ErrorCode.WRN_UnmatchedParamRefTag <= 1)
								{
									return 2;
								}
								if (code != ErrorCode.WRN_ReferencedAssemblyReferencesLinkedPIA)
								{
									return 0;
								}
								return 1;
							}
							else
							{
								if (code == ErrorCode.WRN_CantHaveManifestForModule)
								{
									return 2;
								}
								if (code - ErrorCode.WRN_MultipleRuntimeImplementationMatches <= 1)
								{
									return 1;
								}
								if (code != ErrorCode.WRN_DynamicDispatchToConditionalMethod)
								{
									return 0;
								}
								return 2;
							}
						}
						else if (code <= ErrorCode.WRN_FileAlreadyIncluded)
						{
							if (code == ErrorCode.WRN_IsDynamicIsConfusing)
							{
								return 3;
							}
							if (code != ErrorCode.WRN_AsyncLacksAwaits && code != ErrorCode.WRN_FileAlreadyIncluded)
							{
								return 0;
							}
							return 1;
						}
						else if (code <= ErrorCode.WRN_NoConfigNotOnCommandLine)
						{
							if (code == ErrorCode.WRN_NoSources)
							{
								return 2;
							}
							if (code != ErrorCode.WRN_NoConfigNotOnCommandLine)
							{
								return 0;
							}
							return 1;
						}
						else
						{
							if (code != ErrorCode.WRN_DefineIdentifierRequired && code != ErrorCode.WRN_BadUILang)
							{
								return 0;
							}
							return 1;
						}
					}
					else if (code <= ErrorCode.WRN_InvalidVersionFormat)
					{
						if (code <= ErrorCode.WRN_CallerMemberNameParamForUnconsumedLocation)
						{
							switch (code)
							{
							case ErrorCode.WRN_CLS_NoVarArgs:
							case ErrorCode.WRN_CLS_BadArgType:
							case ErrorCode.WRN_CLS_BadReturnType:
							case ErrorCode.WRN_CLS_BadFieldPropType:
							case ErrorCode.WRN_CLS_BadIdentifierCase:
							case ErrorCode.WRN_CLS_OverloadRefOut:
							case ErrorCode.WRN_CLS_OverloadUnnamed:
							case ErrorCode.WRN_CLS_BadIdentifier:
							case ErrorCode.WRN_CLS_BadBase:
							case ErrorCode.WRN_CLS_BadInterfaceMember:
							case ErrorCode.WRN_CLS_NoAbstractMembers:
							case ErrorCode.WRN_CLS_NotOnModules:
							case ErrorCode.WRN_CLS_ModuleMissingCLS:
							case ErrorCode.WRN_CLS_AssemblyNotCLS:
							case ErrorCode.WRN_CLS_BadAttributeType:
							case ErrorCode.WRN_CLS_ArrayArgumentToAttribute:
							case ErrorCode.WRN_CLS_NotOnModules2:
							case ErrorCode.WRN_CLS_IllegalTrueInFalse:
							case ErrorCode.WRN_CLS_MeaninglessOnParam:
							case ErrorCode.WRN_CLS_MeaninglessOnReturn:
							case ErrorCode.WRN_CLS_BadTypeVar:
							case ErrorCode.WRN_CLS_VolatileField:
							case ErrorCode.WRN_CLS_BadInterface:
								return 1;
							case (ErrorCode)3004:
							case (ErrorCode)3020:
							case (ErrorCode)3025:
								return 0;
							case ErrorCode.WRN_CLS_MeaninglessOnPrivateType:
							case ErrorCode.WRN_CLS_AssemblyNotCLS2:
								return 2;
							default:
								if (code != ErrorCode.WRN_UnobservedAwaitableExpression && code - ErrorCode.WRN_CallerLineNumberParamForUnconsumedLocation > 2)
								{
									return 0;
								}
								return 1;
							}
						}
						else if (code <= ErrorCode.WRN_StaticInAsOrIs)
						{
							if (code == ErrorCode.WRN_MainIgnored)
							{
								return 2;
							}
							if (code != ErrorCode.WRN_StaticInAsOrIs)
							{
								return 0;
							}
						}
						else
						{
							if (code == ErrorCode.WRN_DelaySignButNoKey)
							{
								return 1;
							}
							if (code != ErrorCode.WRN_InvalidVersionFormat)
							{
								return 0;
							}
							return 4;
						}
					}
					else if (code <= ErrorCode.WRN_FilterIsConstantTrue)
					{
						if (code - ErrorCode.WRN_CallerFilePathPreferredOverCallerMemberName > 2 && code != ErrorCode.WRN_AssemblyAttributeFromModuleIsOverridden && code != ErrorCode.WRN_FilterIsConstantTrue)
						{
							return 0;
						}
						return 1;
					}
					else if (code <= ErrorCode.WRN_RefCultureMismatch)
					{
						if (code - ErrorCode.WRN_UnimplementedCommandLineSwitch > 1 && code != ErrorCode.WRN_RefCultureMismatch)
						{
							return 0;
						}
						return 1;
					}
					else
					{
						if (code == ErrorCode.WRN_ConflictingMachineAssembly)
						{
							return 1;
						}
						if (code != ErrorCode.WRN_UnqualifiedNestedTypeInCref)
						{
							return 0;
						}
						return 2;
					}
				}
				else if (code <= ErrorCode.WRN_UndecoratedCancellationTokenParameter)
				{
					if (code <= ErrorCode.WRN_TupleLiteralNameMismatch)
					{
						if (code <= ErrorCode.WRN_UnableToLoadAnalyzer)
						{
							if (code == ErrorCode.WRN_NoRuntimeMetadataVersion)
							{
								return 2;
							}
							if (code == ErrorCode.WRN_PdbLocalNameTooLong)
							{
								return 3;
							}
							if (code - ErrorCode.WRN_AnalyzerCannotBeCreated > 2)
							{
								return 0;
							}
							return 1;
						}
						else if (code <= ErrorCode.WRN_AlignmentMagnitude)
						{
							if (code != ErrorCode.WRN_NubExprIsConstBool2)
							{
								if (code != ErrorCode.WRN_AlignmentMagnitude)
								{
									return 0;
								}
								return 1;
							}
						}
						else
						{
							if (code != ErrorCode.WRN_AttributeIgnoredWhenPublicSigning && code != ErrorCode.WRN_TupleLiteralNameMismatch)
							{
								return 0;
							}
							return 1;
						}
					}
					else if (code <= ErrorCode.WRN_FilterIsConstantFalseRedundantTryCatch)
					{
						if (code == ErrorCode.WRN_Experimental)
						{
							return 1;
						}
						if (code == ErrorCode.WRN_UnreferencedLocalFunction)
						{
							return 3;
						}
						if (code - ErrorCode.WRN_FilterIsConstantFalse > 1)
						{
							return 0;
						}
						return 1;
					}
					else if (code <= ErrorCode.WRN_TupleBinopLiteralNameMismatch)
					{
						if (code != ErrorCode.WRN_AttributesOnBackingFieldsNotAvailable && code != ErrorCode.WRN_TupleBinopLiteralNameMismatch)
						{
							return 0;
						}
						return 1;
					}
					else
					{
						if (code != ErrorCode.WRN_TypeParameterSameAsOuterMethodTypeParameter && code - ErrorCode.WRN_UnconsumedEnumeratorCancellationAttributeUsage > 1)
						{
							return 0;
						}
						return 1;
					}
				}
				else if (code <= ErrorCode.WRN_IsPatternAlways)
				{
					if (code <= ErrorCode.WRN_GivenExpressionAlwaysMatchesConstant)
					{
						if (code != ErrorCode.WRN_SwitchExpressionNotExhaustive && code - ErrorCode.WRN_CaseConstantNamedUnderscore > 1 && code - ErrorCode.WRN_GivenExpressionNeverMatchesPattern > 1)
						{
							return 0;
						}
						return 1;
					}
					else if (code <= ErrorCode.WRN_NullReferenceInitializer)
					{
						if (code == ErrorCode.WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue)
						{
							return 1;
						}
						switch (code)
						{
						case ErrorCode.WRN_ThrowPossibleNull:
						case ErrorCode.WRN_ConvertingNullableToNonNullable:
						case ErrorCode.WRN_NullReferenceAssignment:
						case ErrorCode.WRN_NullReferenceReceiver:
						case ErrorCode.WRN_NullReferenceReturn:
						case ErrorCode.WRN_NullReferenceArgument:
						case ErrorCode.WRN_UnboxPossibleNull:
						case ErrorCode.WRN_DisallowNullAttributeForbidsMaybeNullAssignment:
						case ErrorCode.WRN_NullabilityMismatchInTypeOnOverride:
						case ErrorCode.WRN_NullabilityMismatchInReturnTypeOnOverride:
						case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnOverride:
						case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnPartial:
						case ErrorCode.WRN_NullabilityMismatchInTypeOnImplicitImplementation:
						case ErrorCode.WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation:
						case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation:
						case ErrorCode.WRN_NullabilityMismatchInTypeOnExplicitImplementation:
						case ErrorCode.WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation:
						case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation:
						case ErrorCode.WRN_UninitializedNonNullableField:
						case ErrorCode.WRN_NullabilityMismatchInAssignment:
						case ErrorCode.WRN_NullabilityMismatchInArgument:
						case ErrorCode.WRN_NullabilityMismatchInReturnTypeOfTargetDelegate:
						case ErrorCode.WRN_NullabilityMismatchInParameterTypeOfTargetDelegate:
						case ErrorCode.WRN_NullabilityMismatchInArgumentForOutput:
						case ErrorCode.WRN_NullAsNonNullable:
						case ErrorCode.WRN_NullableValueTypeMayBeNull:
						case ErrorCode.WRN_NullabilityMismatchInTypeParameterConstraint:
						case ErrorCode.WRN_MissingNonNullTypesContextForAnnotation:
						case ErrorCode.WRN_NullabilityMismatchInConstraintsOnImplicitImplementation:
						case ErrorCode.WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint:
						case ErrorCode.WRN_NullabilityMismatchInExplicitlyImplementedInterface:
						case ErrorCode.WRN_NullabilityMismatchInInterfaceImplementedByBase:
						case ErrorCode.WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList:
						case ErrorCode.WRN_SwitchExpressionNotExhaustiveForNull:
						case ErrorCode.WRN_ImplicitCopyInReadOnlyMember:
						case ErrorCode.WRN_NullabilityMismatchInConstraintsOnPartialImplementation:
						case ErrorCode.WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode:
						case ErrorCode.WRN_NullReferenceInitializer:
							return 1;
						case ErrorCode.ERR_IllegalSuppression:
						case (ErrorCode)8599:
						case (ErrorCode)8606:
						case ErrorCode.ERR_ExplicitNullableAttribute:
						case (ErrorCode)8626:
						case ErrorCode.ERR_NullableUnconstrainedTypeParameter:
						case ErrorCode.ERR_AnnotationDisallowedInObjectCreation:
						case ErrorCode.ERR_NullableOptionNotAvailable:
						case ErrorCode.ERR_TripleDotNotAllowed:
						case ErrorCode.ERR_BadNullableContextOption:
						case ErrorCode.ERR_NullableDirectiveQualifierExpected:
						case (ErrorCode)8638:
						case ErrorCode.ERR_BadNullableTypeof:
						case ErrorCode.ERR_ExpressionTreeCantContainRefStruct:
						case ErrorCode.ERR_ElseCannotStartStatement:
						case ErrorCode.ERR_ExpressionTreeCantContainNullCoalescingAssignment:
						case ErrorCode.ERR_DuplicateExplicitImpl:
						case ErrorCode.ERR_UsingVarInSwitchCase:
						case ErrorCode.ERR_GoToForwardJumpOverUsingVar:
						case ErrorCode.ERR_GoToBackwardJumpOverUsingVar:
						case ErrorCode.ERR_IsNullableType:
						case ErrorCode.ERR_AsNullableType:
						case ErrorCode.ERR_FeatureInPreview:
						case (ErrorCode)8653:
						case (ErrorCode)8654:
						case ErrorCode.ERR_StaticMemberCantBeReadOnly:
						case ErrorCode.ERR_AutoSetterCantBeReadOnly:
						case ErrorCode.ERR_AutoPropertyWithSetterCantBeReadOnly:
						case ErrorCode.ERR_InvalidPropertyReadOnlyMods:
						case ErrorCode.ERR_DuplicatePropertyReadOnlyMods:
						case ErrorCode.ERR_FieldLikeEventCantBeReadOnly:
						case ErrorCode.ERR_PartialMethodReadOnlyDifference:
						case ErrorCode.ERR_ReadOnlyModMissingAccessor:
						case ErrorCode.ERR_OverrideRefConstraintNotSatisfied:
						case ErrorCode.ERR_OverrideValConstraintNotSatisfied:
						case ErrorCode.ERR_NullableDirectiveTargetExpected:
							return 0;
						default:
							return 0;
						}
					}
					else
					{
						if (code == ErrorCode.WRN_NullabilityMismatchInTypeParameterNotNullConstraint)
						{
							return 1;
						}
						switch (code)
						{
						case ErrorCode.WRN_ParameterConditionallyDisallowsNull:
						case ErrorCode.WRN_ShouldNotReturn:
						case ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride:
						case ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride:
						case ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation:
						case ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation:
						case ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation:
						case ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation:
						case ErrorCode.WRN_DoesNotReturnMismatch:
						case ErrorCode.WRN_MemberNotNull:
						case ErrorCode.WRN_MemberNotNullWhen:
						case ErrorCode.WRN_MemberNotNullBadMember:
						case ErrorCode.WRN_ParameterDisallowsNull:
						case ErrorCode.WRN_ConstOutOfRangeChecked:
						case ErrorCode.WRN_GeneratorFailedDuringInitialization:
						case ErrorCode.WRN_GeneratorFailedDuringGeneration:
						case ErrorCode.WRN_GivenExpressionAlwaysMatchesPattern:
						case ErrorCode.WRN_IsPatternAlways:
							return 1;
						case ErrorCode.ERR_NoOutputDirectory:
						case ErrorCode.ERR_StdInOptionProvidedButConsoleInputIsNotRedirected:
						case ErrorCode.ERR_FeatureNotAvailableInVersion9:
						case ErrorCode.ERR_DuplicateInterfaceWithDifferencesInBaseList:
						case ErrorCode.ERR_DesignatorBeneathPatternCombinator:
						case ErrorCode.ERR_UnsupportedTypeForRelationalPattern:
						case ErrorCode.ERR_RelationalPatternWithNaN:
						case ErrorCode.ERR_ConditionalOnLocalFunction:
						case ErrorCode.ERR_WrongFuncPtrCallingConvention:
						case ErrorCode.ERR_MissingAddressOf:
						case ErrorCode.ERR_CannotUseReducedExtensionMethodInAddressOf:
						case ErrorCode.ERR_CannotUseFunctionPointerAsFixedLocal:
						case ErrorCode.ERR_ExpressionTreeContainsPatternIndexOrRangeIndexer:
						case ErrorCode.ERR_ExpressionTreeContainsFromEndIndexExpression:
						case ErrorCode.ERR_ExpressionTreeContainsRangeExpression:
							return 0;
						default:
							return 0;
						}
					}
				}
				else if (code <= ErrorCode.WRN_RecordEqualsWithoutGetHashCode)
				{
					if (code == ErrorCode.WRN_NullabilityMismatchInReturnTypeOnPartial || code - ErrorCode.WRN_ParameterNotNullIfNotNull <= 1)
					{
						return 1;
					}
					switch (code)
					{
					case ErrorCode.WRN_SwitchExpressionNotExhaustiveWithWhen:
					case ErrorCode.WRN_SwitchExpressionNotExhaustiveForNullWithWhen:
					case ErrorCode.WRN_AnalyzerReferencesFramework:
						return 1;
					case ErrorCode.WRN_PrecedenceInversion:
						break;
					case ErrorCode.ERR_ExpressionTreeContainsWithExpression:
						return 0;
					case ErrorCode.WRN_RecordEqualsWithoutGetHashCode:
						return 3;
					default:
						return 0;
					}
				}
				else if (code <= ErrorCode.WRN_ReturnTypeIsStaticClass)
				{
					if (code == ErrorCode.WRN_RecordNamedDisallowed)
					{
						return 1;
					}
					switch (code)
					{
					case ErrorCode.WRN_UnassignedThisAutoProperty:
					case ErrorCode.WRN_UnassignedThis:
					case ErrorCode.WRN_ParamUnassigned:
					case ErrorCode.WRN_UseDefViolationProperty:
					case ErrorCode.WRN_UseDefViolationField:
					case ErrorCode.WRN_UseDefViolationThis:
					case ErrorCode.WRN_UseDefViolationOut:
					case ErrorCode.WRN_UseDefViolation:
					case ErrorCode.WRN_SyncAndAsyncEntryPoints:
					case ErrorCode.WRN_ParameterIsStaticClass:
					case ErrorCode.WRN_ReturnTypeIsStaticClass:
						break;
					case ErrorCode.ERR_CannotSpecifyManagedWithUnmanagedSpecifiers:
					case ErrorCode.ERR_RuntimeDoesNotSupportUnmanagedDefaultCallConv:
					case ErrorCode.ERR_TypeNotFound:
					case ErrorCode.ERR_TypeMustBePublic:
					case ErrorCode.ERR_InvalidUnmanagedCallersOnlyCallConv:
					case ErrorCode.ERR_CannotUseManagedTypeInUnmanagedCallersOnly:
					case ErrorCode.ERR_UnmanagedCallersOnlyMethodOrTypeCannotBeGeneric:
					case ErrorCode.ERR_UnmanagedCallersOnlyRequiresStatic:
						return 0;
					default:
						return 0;
					}
				}
				else
				{
					if (code != ErrorCode.WRN_UnreadRecordParameter && code != ErrorCode.WRN_DoNotCompareFunctionPointers)
					{
						return 0;
					}
					return 1;
				}
				return 5;
			}
			if (code <= ErrorCode.WRN_DeprecatedSymbolStr)
			{
				if (code <= ErrorCode.WRN_PatternBadSignature)
				{
					if (code <= ErrorCode.WRN_UnreachableCode)
					{
						if (code <= ErrorCode.WRN_LowercaseEllSuffix)
						{
							if (code != ErrorCode.WRN_InvalidMainSig)
							{
								if (code == ErrorCode.WRN_UnreferencedEvent)
								{
									return 3;
								}
								if (code != ErrorCode.WRN_LowercaseEllSuffix)
								{
									return 0;
								}
							}
						}
						else
						{
							switch (code)
							{
							case ErrorCode.WRN_DuplicateUsing:
								return 3;
							case ErrorCode.ERR_BadMemberFlag:
							case ErrorCode.ERR_BadMemberProtection:
								return 0;
							case ErrorCode.WRN_NewRequired:
								return 2;
							case ErrorCode.WRN_NewNotRequired:
								break;
							default:
								if (code != ErrorCode.WRN_NewOrOverrideExpected && code != ErrorCode.WRN_UnreachableCode)
								{
									return 0;
								}
								return 2;
							}
						}
					}
					else if (code <= ErrorCode.WRN_IsAlwaysFalse)
					{
						if (code == ErrorCode.WRN_UnreferencedLabel)
						{
							return 2;
						}
						if (code - ErrorCode.WRN_UnreferencedVar <= 1)
						{
							return 3;
						}
						if (code - ErrorCode.WRN_IsAlwaysTrue > 1)
						{
							return 0;
						}
						return 1;
					}
					else if (code <= ErrorCode.WRN_UnreferencedVarAssg)
					{
						if (code == ErrorCode.WRN_ByRefNonAgileField)
						{
							return 1;
						}
						if (code != ErrorCode.WRN_UnreferencedVarAssg)
						{
							return 0;
						}
						return 3;
					}
					else
					{
						if (code - ErrorCode.WRN_NegativeArrayIndex > 2 && code - ErrorCode.WRN_PatternIsAmbiguous > 2)
						{
							return 0;
						}
						return 2;
					}
				}
				else if (code <= ErrorCode.WRN_GlobalAliasDefn)
				{
					if (code <= ErrorCode.WRN_UnreferencedFieldAssg)
					{
						if (code == ErrorCode.WRN_SequentialOnPartialClass)
						{
							return 3;
						}
						if (code != ErrorCode.WRN_MainCantBeGeneric)
						{
							if (code != ErrorCode.WRN_UnreferencedFieldAssg)
							{
								return 0;
							}
							return 3;
						}
					}
					else if (code <= ErrorCode.WRN_VolatileByRef)
					{
						if (code == ErrorCode.WRN_AmbiguousXMLReference)
						{
							return 3;
						}
						if (code != ErrorCode.WRN_VolatileByRef)
						{
							return 0;
						}
						return 1;
					}
					else
					{
						if (code - ErrorCode.WRN_SameFullNameThisNsAgg > 2 && code != ErrorCode.WRN_GlobalAliasDefn)
						{
							return 0;
						}
						return 2;
					}
				}
				else if (code <= ErrorCode.WRN_FinalizeMethod)
				{
					if (code == ErrorCode.WRN_AlwaysNull || code == ErrorCode.WRN_CmpAlwaysFalse)
					{
						return 2;
					}
					if (code != ErrorCode.WRN_FinalizeMethod)
					{
						return 0;
					}
					return 1;
				}
				else if (code <= ErrorCode.WRN_ExplicitImplCollision)
				{
					if (code != ErrorCode.WRN_GotoCaseShouldConvert && code - ErrorCode.WRN_NubExprIsConstBool > 1)
					{
						return 0;
					}
					return 2;
				}
				else
				{
					if (code == ErrorCode.WRN_DeprecatedSymbol)
					{
						return 1;
					}
					if (code != ErrorCode.WRN_DeprecatedSymbolStr)
					{
						return 0;
					}
					return 2;
				}
			}
			else if (code <= ErrorCode.WRN_ExternCtorNoImplementation)
			{
				if (code <= ErrorCode.WRN_NonObsoleteOverridingObsolete)
				{
					if (code <= ErrorCode.WRN_PossibleMistakenNullStatement)
					{
						if (code == ErrorCode.WRN_ExternMethodNoImplementation)
						{
							return 1;
						}
						if (code != ErrorCode.WRN_ProtectedInSealed)
						{
							if (code != ErrorCode.WRN_PossibleMistakenNullStatement)
							{
								return 0;
							}
							return 3;
						}
					}
					else
					{
						switch (code)
						{
						case ErrorCode.WRN_UnassignedInternalField:
							break;
						case ErrorCode.ERR_CStyleArray:
						case (ErrorCode)651:
						case ErrorCode.ERR_AbstractAttributeClass:
						case (ErrorCode)654:
						case ErrorCode.ERR_BadNamedAttributeArgumentType:
						case ErrorCode.ERR_MissingPredefinedMember:
							return 0;
						case ErrorCode.WRN_VacuousIntegralComp:
							return 2;
						case ErrorCode.WRN_AttributeLocationOnBadDeclaration:
						case ErrorCode.WRN_InvalidAttributeLocation:
							return 1;
						case ErrorCode.WRN_EqualsWithoutGetHashCode:
						case ErrorCode.WRN_EqualityOpWithoutEquals:
						case ErrorCode.WRN_EqualityOpWithoutGetHashCode:
							return 3;
						default:
							if (code == ErrorCode.WRN_IncorrectBooleanAssg)
							{
								return 3;
							}
							if (code != ErrorCode.WRN_NonObsoleteOverridingObsolete)
							{
								return 0;
							}
							return 1;
						}
					}
				}
				else if (code <= ErrorCode.WRN_TypeParameterSameAsOuterTypeParameter)
				{
					if (code == ErrorCode.WRN_BitwiseOrSignExtend)
					{
						return 3;
					}
					if (code == ErrorCode.WRN_CoClassWithoutComImport)
					{
						return 1;
					}
					if (code != ErrorCode.WRN_TypeParameterSameAsOuterTypeParameter)
					{
						return 0;
					}
					return 3;
				}
				else if (code <= ErrorCode.WRN_ObsoleteOverridingNonObsolete)
				{
					if (code == ErrorCode.WRN_AssignmentToLockOrDispose)
					{
						return 2;
					}
					if (code != ErrorCode.WRN_ObsoleteOverridingNonObsolete)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					if (code == ErrorCode.WRN_DebugFullNameTooLong)
					{
						return 3;
					}
					if (code != ErrorCode.WRN_ExternCtorNoImplementation)
					{
						return 0;
					}
					return 1;
				}
			}
			else if (code <= ErrorCode.WRN_ALinkWarn)
			{
				if (code <= ErrorCode.WRN_DefaultValueForUnconsumedLocation)
				{
					if (code == ErrorCode.WRN_WarningDirective || code == ErrorCode.WRN_UnreachableGeneralCatch)
					{
						return 1;
					}
					switch (code)
					{
					case ErrorCode.WRN_DeprecatedCollectionInitAddStr:
					case ErrorCode.WRN_DeprecatedCollectionInitAdd:
						return 2;
					case ErrorCode.ERR_DeprecatedCollectionInitAddStr:
					case ErrorCode.ERR_DefaultValueNotAllowed:
						return 0;
					case ErrorCode.WRN_DefaultValueForUnconsumedLocation:
						return 1;
					default:
						return 0;
					}
				}
				else if (code <= ErrorCode.WRN_EmptySwitch)
				{
					if (code != ErrorCode.WRN_IdentifierOrNumericLiteralExpected && code != ErrorCode.WRN_EmptySwitch)
					{
						return 0;
					}
					return 1;
				}
				else
				{
					switch (code)
					{
					case ErrorCode.WRN_XMLParseError:
					case ErrorCode.WRN_BadXMLRef:
					case ErrorCode.WRN_BadXMLRefParamType:
					case ErrorCode.WRN_BadXMLRefReturnType:
					case ErrorCode.WRN_BadXMLRefSyntax:
					case ErrorCode.WRN_FailedInclude:
					case ErrorCode.WRN_InvalidInclude:
					case ErrorCode.WRN_XMLParseIncludeError:
						return 1;
					case ErrorCode.WRN_DuplicateParamTag:
					case ErrorCode.WRN_UnmatchedParamTag:
					case ErrorCode.WRN_UnprocessedXMLComment:
						return 2;
					case ErrorCode.WRN_MissingParamTag:
					case ErrorCode.WRN_MissingXMLComment:
						break;
					case ErrorCode.ERR_BadStackAllocExpr:
					case ErrorCode.ERR_InvalidLineNumber:
					case (ErrorCode)1577:
					case ErrorCode.ERR_MissingPPFile:
					case ErrorCode.ERR_ForEachMissingMember:
					case (ErrorCode)1582:
					case ErrorCode.ERR_BadWin32Res:
					case ErrorCode.ERR_BadModifierLocation:
					case ErrorCode.ERR_MissingArraySize:
					case (ErrorCode)1588:
						return 0;
					default:
						if (code != ErrorCode.WRN_ALinkWarn)
						{
							return 0;
						}
						return 1;
					}
				}
			}
			else if (code <= ErrorCode.WRN_NonECMAFeature)
			{
				if (code != ErrorCode.WRN_CmdOptionConflictsSource && code - ErrorCode.WRN_IllegalPragma > 2 && code != ErrorCode.WRN_NonECMAFeature)
				{
					return 0;
				}
				return 1;
			}
			else if (code <= ErrorCode.WRN_InvalidSearchPathDir)
			{
				if (code == ErrorCode.WRN_ErrorOverride)
				{
					return 1;
				}
				if (code != ErrorCode.WRN_InvalidSearchPathDir)
				{
					return 0;
				}
				return 2;
			}
			else
			{
				switch (code)
				{
				case ErrorCode.WRN_MultiplePredefTypes:
				case ErrorCode.WRN_TooManyLinesForDebugger:
				case ErrorCode.WRN_CallOnNonAgileField:
				case ErrorCode.WRN_InvalidNumber:
				case ErrorCode.WRN_IllegalPPChecksum:
				case ErrorCode.WRN_EndOfPPLineExpected:
				case ErrorCode.WRN_ConflictingChecksum:
					return 1;
				case ErrorCode.ERR_LocalCantBeFixedAndHoisted:
				case ErrorCode.ERR_CantConvAnonMethNoParams:
				case ErrorCode.ERR_ConditionalOnNonAttributeClass:
				case (ErrorCode)1691:
				case (ErrorCode)1693:
				case (ErrorCode)1694:
				case (ErrorCode)1698:
				case (ErrorCode)1699:
					return 0;
				case ErrorCode.WRN_InvalidAssemblyName:
				case ErrorCode.WRN_UnifyReferenceBldRev:
					return 3;
				case ErrorCode.WRN_UnifyReferenceMajMin:
					return 2;
				default:
					switch (code)
					{
					case ErrorCode.WRN_DuplicateTypeParamTag:
					case ErrorCode.WRN_UnmatchedTypeParamTag:
						return 2;
					case ErrorCode.WRN_MissingTypeParamTag:
						break;
					case (ErrorCode)1713:
					case (ErrorCode)1714:
					case ErrorCode.ERR_CantChangeTypeOnOverride:
					case ErrorCode.ERR_DoNotUseFixedBufferAttr:
					case ErrorCode.ERR_CantOpenWin32Res:
						return 0;
					case ErrorCode.WRN_AssignmentToSelf:
					case ErrorCode.WRN_ComparisonToSelf:
						return 3;
					case ErrorCode.WRN_DotOnDefault:
						return 1;
					default:
						return 0;
					}
					break;
				}
			}
			return 4;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00086690 File Offset: 0x00084890
		internal static bool PreventsSuccessfulDelegateConversion(ErrorCode code)
		{
			if (code == ErrorCode.Void || code == ErrorCode.Unknown)
			{
				return false;
			}
			if (ErrorFacts.IsWarning(code))
			{
				return false;
			}
			if (code <= ErrorCode.ERR_LocalSameNameAsTypeParam)
			{
				if (code <= ErrorCode.ERR_LocalDuplicate)
				{
					if (code != ErrorCode.ERR_DuplicateParamName && code != ErrorCode.ERR_LocalDuplicate)
					{
						return true;
					}
				}
				else if (code != ErrorCode.ERR_LocalIllegallyOverrides && code != ErrorCode.ERR_LocalSameNameAsTypeParam)
				{
					return true;
				}
			}
			else if (code <= ErrorCode.ERR_DeprecatedCollectionInitAddStr)
			{
				if (code != ErrorCode.ERR_DeprecatedSymbolStr && code != ErrorCode.ERR_DeprecatedCollectionInitAddStr)
				{
					return true;
				}
			}
			else if (code != ErrorCode.ERR_QueryRangeVariableOverrides && code != ErrorCode.ERR_QueryRangeVariableSameAsTypeParam)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00086710 File Offset: 0x00084910
		internal static bool PreventsSuccessfulDelegateConversion(DiagnosticBag diagnostics)
		{
			using (IEnumerator<Diagnostic> enumerator = diagnostics.AsEnumerable().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ErrorFacts.PreventsSuccessfulDelegateConversion((ErrorCode)enumerator.Current.Code))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00086768 File Offset: 0x00084968
		internal static bool PreventsSuccessfulDelegateConversion(ImmutableArray<Diagnostic> diagnostics)
		{
			ImmutableArray<Diagnostic>.Enumerator enumerator = diagnostics.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (ErrorFacts.PreventsSuccessfulDelegateConversion((ErrorCode)enumerator.Current.Code))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0008679E File Offset: 0x0008499E
		internal static ErrorCode GetStaticClassParameterCode(bool useWarning)
		{
			if (!useWarning)
			{
				return ErrorCode.ERR_ParameterIsStaticClass;
			}
			return ErrorCode.WRN_ParameterIsStaticClass;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000867AE File Offset: 0x000849AE
		internal static ErrorCode GetStaticClassReturnCode(bool useWarning)
		{
			if (!useWarning)
			{
				return ErrorCode.ERR_ReturnTypeIsStaticClass;
			}
			return ErrorCode.WRN_ReturnTypeIsStaticClass;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000867C0 File Offset: 0x000849C0
		public static bool IsWarning(ErrorCode code)
		{
			if (code <= ErrorCode.WRN_BadXMLRefTypeVar)
			{
				if (code <= ErrorCode.WRN_ExternMethodNoImplementation)
				{
					if (code <= ErrorCode.WRN_BadRefCompareRight)
					{
						if (code <= ErrorCode.WRN_NewOrOverrideExpected)
						{
							if (code <= ErrorCode.WRN_LowercaseEllSuffix)
							{
								if (code != ErrorCode.WRN_InvalidMainSig && code != ErrorCode.WRN_UnreferencedEvent && code != ErrorCode.WRN_LowercaseEllSuffix)
								{
									return false;
								}
							}
							else if (code != ErrorCode.WRN_DuplicateUsing && code - ErrorCode.WRN_NewRequired > 1 && code != ErrorCode.WRN_NewOrOverrideExpected)
							{
								return false;
							}
						}
						else if (code <= ErrorCode.WRN_UnreferencedField)
						{
							if (code != ErrorCode.WRN_UnreachableCode && code != ErrorCode.WRN_UnreferencedLabel && code - ErrorCode.WRN_UnreferencedVar > 1)
							{
								return false;
							}
						}
						else if (code <= ErrorCode.WRN_ByRefNonAgileField)
						{
							if (code - ErrorCode.WRN_IsAlwaysTrue > 1 && code != ErrorCode.WRN_ByRefNonAgileField)
							{
								return false;
							}
						}
						else if (code != ErrorCode.WRN_UnreferencedVarAssg && code - ErrorCode.WRN_NegativeArrayIndex > 2)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_GlobalAliasDefn)
					{
						if (code <= ErrorCode.WRN_MainCantBeGeneric)
						{
							if (code - ErrorCode.WRN_PatternIsAmbiguous > 2 && code != ErrorCode.WRN_SequentialOnPartialClass && code != ErrorCode.WRN_MainCantBeGeneric)
							{
								return false;
							}
						}
						else if (code <= ErrorCode.WRN_VolatileByRef)
						{
							if (code != ErrorCode.WRN_UnreferencedFieldAssg && code - ErrorCode.WRN_AmbiguousXMLReference > 1)
							{
								return false;
							}
						}
						else if (code - ErrorCode.WRN_SameFullNameThisNsAgg > 2 && code != ErrorCode.WRN_GlobalAliasDefn)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_GotoCaseShouldConvert)
					{
						if (code != ErrorCode.WRN_AlwaysNull && code - ErrorCode.WRN_CmpAlwaysFalse > 1 && code != ErrorCode.WRN_GotoCaseShouldConvert)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_DeprecatedSymbol)
					{
						if (code - ErrorCode.WRN_NubExprIsConstBool > 1 && code != ErrorCode.WRN_DeprecatedSymbol)
						{
							return false;
						}
					}
					else if (code != ErrorCode.WRN_DeprecatedSymbolStr && code != ErrorCode.WRN_ExternMethodNoImplementation)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_WarningDirective)
				{
					if (code <= ErrorCode.WRN_BitwiseOrSignExtend)
					{
						if (code <= ErrorCode.WRN_EqualityOpWithoutGetHashCode)
						{
							if (code != ErrorCode.WRN_ProtectedInSealed && code != ErrorCode.WRN_PossibleMistakenNullStatement)
							{
								switch (code)
								{
								case ErrorCode.WRN_UnassignedInternalField:
								case ErrorCode.WRN_VacuousIntegralComp:
								case ErrorCode.WRN_AttributeLocationOnBadDeclaration:
								case ErrorCode.WRN_InvalidAttributeLocation:
								case ErrorCode.WRN_EqualsWithoutGetHashCode:
								case ErrorCode.WRN_EqualityOpWithoutEquals:
								case ErrorCode.WRN_EqualityOpWithoutGetHashCode:
									break;
								case ErrorCode.ERR_CStyleArray:
								case (ErrorCode)651:
								case ErrorCode.ERR_AbstractAttributeClass:
								case (ErrorCode)654:
								case ErrorCode.ERR_BadNamedAttributeArgumentType:
								case ErrorCode.ERR_MissingPredefinedMember:
									return false;
								default:
									return false;
								}
							}
						}
						else if (code != ErrorCode.WRN_IncorrectBooleanAssg && code != ErrorCode.WRN_NonObsoleteOverridingObsolete && code != ErrorCode.WRN_BitwiseOrSignExtend)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_AssignmentToLockOrDispose)
					{
						if (code != ErrorCode.WRN_CoClassWithoutComImport && code != ErrorCode.WRN_TypeParameterSameAsOuterTypeParameter && code != ErrorCode.WRN_AssignmentToLockOrDispose)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_DebugFullNameTooLong)
					{
						if (code != ErrorCode.WRN_ObsoleteOverridingNonObsolete && code != ErrorCode.WRN_DebugFullNameTooLong)
						{
							return false;
						}
					}
					else if (code != ErrorCode.WRN_ExternCtorNoImplementation && code != ErrorCode.WRN_WarningDirective)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_CmdOptionConflictsSource)
				{
					if (code <= ErrorCode.WRN_IdentifierOrNumericLiteralExpected)
					{
						if (code != ErrorCode.WRN_UnreachableGeneralCatch)
						{
							switch (code)
							{
							case ErrorCode.WRN_DeprecatedCollectionInitAddStr:
							case ErrorCode.WRN_DeprecatedCollectionInitAdd:
							case ErrorCode.WRN_DefaultValueForUnconsumedLocation:
								break;
							case ErrorCode.ERR_DeprecatedCollectionInitAddStr:
							case ErrorCode.ERR_DefaultValueNotAllowed:
								return false;
							default:
								if (code != ErrorCode.WRN_IdentifierOrNumericLiteralExpected)
								{
									return false;
								}
								break;
							}
						}
					}
					else if (code <= ErrorCode.WRN_XMLParseIncludeError)
					{
						if (code != ErrorCode.WRN_EmptySwitch)
						{
							switch (code)
							{
							case ErrorCode.WRN_XMLParseError:
							case ErrorCode.WRN_DuplicateParamTag:
							case ErrorCode.WRN_UnmatchedParamTag:
							case ErrorCode.WRN_MissingParamTag:
							case ErrorCode.WRN_BadXMLRef:
							case ErrorCode.WRN_BadXMLRefParamType:
							case ErrorCode.WRN_BadXMLRefReturnType:
							case ErrorCode.WRN_BadXMLRefSyntax:
							case ErrorCode.WRN_UnprocessedXMLComment:
							case ErrorCode.WRN_FailedInclude:
							case ErrorCode.WRN_InvalidInclude:
							case ErrorCode.WRN_MissingXMLComment:
							case ErrorCode.WRN_XMLParseIncludeError:
								break;
							case ErrorCode.ERR_BadStackAllocExpr:
							case ErrorCode.ERR_InvalidLineNumber:
							case (ErrorCode)1577:
							case ErrorCode.ERR_MissingPPFile:
							case ErrorCode.ERR_ForEachMissingMember:
							case (ErrorCode)1582:
							case ErrorCode.ERR_BadWin32Res:
							case ErrorCode.ERR_BadModifierLocation:
							case ErrorCode.ERR_MissingArraySize:
							case (ErrorCode)1588:
								return false;
							default:
								return false;
							}
						}
					}
					else if (code != ErrorCode.WRN_ALinkWarn && code != ErrorCode.WRN_CmdOptionConflictsSource)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_ErrorOverride)
				{
					if (code - ErrorCode.WRN_IllegalPragma > 2 && code != ErrorCode.WRN_NonECMAFeature && code != ErrorCode.WRN_ErrorOverride)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_UnifyReferenceBldRev)
				{
					if (code != ErrorCode.WRN_InvalidSearchPathDir)
					{
						switch (code)
						{
						case ErrorCode.WRN_MultiplePredefTypes:
						case ErrorCode.WRN_TooManyLinesForDebugger:
						case ErrorCode.WRN_CallOnNonAgileField:
						case ErrorCode.WRN_InvalidNumber:
						case ErrorCode.WRN_IllegalPPChecksum:
						case ErrorCode.WRN_EndOfPPLineExpected:
						case ErrorCode.WRN_ConflictingChecksum:
						case ErrorCode.WRN_InvalidAssemblyName:
						case ErrorCode.WRN_UnifyReferenceMajMin:
						case ErrorCode.WRN_UnifyReferenceBldRev:
							break;
						case ErrorCode.ERR_LocalCantBeFixedAndHoisted:
						case ErrorCode.ERR_CantConvAnonMethNoParams:
						case ErrorCode.ERR_ConditionalOnNonAttributeClass:
						case (ErrorCode)1691:
						case (ErrorCode)1693:
						case (ErrorCode)1694:
						case (ErrorCode)1698:
						case (ErrorCode)1699:
							return false;
						default:
							return false;
						}
					}
				}
				else
				{
					switch (code)
					{
					case ErrorCode.WRN_DuplicateTypeParamTag:
					case ErrorCode.WRN_UnmatchedTypeParamTag:
					case ErrorCode.WRN_MissingTypeParamTag:
					case ErrorCode.WRN_AssignmentToSelf:
					case ErrorCode.WRN_ComparisonToSelf:
					case ErrorCode.WRN_DotOnDefault:
						break;
					case (ErrorCode)1713:
					case (ErrorCode)1714:
					case ErrorCode.ERR_CantChangeTypeOnOverride:
					case ErrorCode.ERR_DoNotUseFixedBufferAttr:
					case ErrorCode.ERR_CantOpenWin32Res:
						return false;
					default:
						if (code != ErrorCode.WRN_BadXMLRefTypeVar)
						{
							return false;
						}
						break;
					}
				}
			}
			else if (code <= ErrorCode.WRN_PdbLocalNameTooLong)
			{
				if (code <= ErrorCode.WRN_CLS_BadInterface)
				{
					if (code <= ErrorCode.WRN_IsDynamicIsConfusing)
					{
						if (code <= ErrorCode.WRN_CantHaveManifestForModule)
						{
							if (code - ErrorCode.WRN_UnmatchedParamRefTag > 1 && code != ErrorCode.WRN_ReferencedAssemblyReferencesLinkedPIA && code != ErrorCode.WRN_CantHaveManifestForModule)
							{
								return false;
							}
						}
						else if (code - ErrorCode.WRN_MultipleRuntimeImplementationMatches > 1 && code != ErrorCode.WRN_DynamicDispatchToConditionalMethod && code != ErrorCode.WRN_IsDynamicIsConfusing)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_NoSources)
					{
						if (code != ErrorCode.WRN_AsyncLacksAwaits && code != ErrorCode.WRN_FileAlreadyIncluded && code != ErrorCode.WRN_NoSources)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_DefineIdentifierRequired)
					{
						if (code != ErrorCode.WRN_NoConfigNotOnCommandLine && code != ErrorCode.WRN_DefineIdentifierRequired)
						{
							return false;
						}
					}
					else if (code != ErrorCode.WRN_BadUILang)
					{
						switch (code)
						{
						case ErrorCode.WRN_CLS_NoVarArgs:
						case ErrorCode.WRN_CLS_BadArgType:
						case ErrorCode.WRN_CLS_BadReturnType:
						case ErrorCode.WRN_CLS_BadFieldPropType:
						case ErrorCode.WRN_CLS_BadIdentifierCase:
						case ErrorCode.WRN_CLS_OverloadRefOut:
						case ErrorCode.WRN_CLS_OverloadUnnamed:
						case ErrorCode.WRN_CLS_BadIdentifier:
						case ErrorCode.WRN_CLS_BadBase:
						case ErrorCode.WRN_CLS_BadInterfaceMember:
						case ErrorCode.WRN_CLS_NoAbstractMembers:
						case ErrorCode.WRN_CLS_NotOnModules:
						case ErrorCode.WRN_CLS_ModuleMissingCLS:
						case ErrorCode.WRN_CLS_AssemblyNotCLS:
						case ErrorCode.WRN_CLS_BadAttributeType:
						case ErrorCode.WRN_CLS_ArrayArgumentToAttribute:
						case ErrorCode.WRN_CLS_NotOnModules2:
						case ErrorCode.WRN_CLS_IllegalTrueInFalse:
						case ErrorCode.WRN_CLS_MeaninglessOnPrivateType:
						case ErrorCode.WRN_CLS_AssemblyNotCLS2:
						case ErrorCode.WRN_CLS_MeaninglessOnParam:
						case ErrorCode.WRN_CLS_MeaninglessOnReturn:
						case ErrorCode.WRN_CLS_BadTypeVar:
						case ErrorCode.WRN_CLS_VolatileField:
						case ErrorCode.WRN_CLS_BadInterface:
							break;
						case (ErrorCode)3004:
						case (ErrorCode)3020:
						case (ErrorCode)3025:
							return false;
						default:
							return false;
						}
					}
				}
				else if (code <= ErrorCode.WRN_AssemblyAttributeFromModuleIsOverridden)
				{
					if (code <= ErrorCode.WRN_StaticInAsOrIs)
					{
						if (code != ErrorCode.WRN_UnobservedAwaitableExpression && code - ErrorCode.WRN_CallerLineNumberParamForUnconsumedLocation > 2 && code - ErrorCode.WRN_MainIgnored > 1)
						{
							return false;
						}
					}
					else if (code <= ErrorCode.WRN_InvalidVersionFormat)
					{
						if (code != ErrorCode.WRN_DelaySignButNoKey && code != ErrorCode.WRN_InvalidVersionFormat)
						{
							return false;
						}
					}
					else if (code - ErrorCode.WRN_CallerFilePathPreferredOverCallerMemberName > 2 && code != ErrorCode.WRN_AssemblyAttributeFromModuleIsOverridden)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_RefCultureMismatch)
				{
					if (code != ErrorCode.WRN_FilterIsConstantTrue && code - ErrorCode.WRN_UnimplementedCommandLineSwitch > 1 && code != ErrorCode.WRN_RefCultureMismatch)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_UnqualifiedNestedTypeInCref)
				{
					if (code != ErrorCode.WRN_ConflictingMachineAssembly && code != ErrorCode.WRN_UnqualifiedNestedTypeInCref)
					{
						return false;
					}
				}
				else if (code != ErrorCode.WRN_NoRuntimeMetadataVersion && code != ErrorCode.WRN_PdbLocalNameTooLong)
				{
					return false;
				}
			}
			else if (code <= ErrorCode.WRN_SwitchExpressionNotExhaustive)
			{
				if (code <= ErrorCode.WRN_Experimental)
				{
					if (code <= ErrorCode.WRN_AlignmentMagnitude)
					{
						if (code - ErrorCode.WRN_AnalyzerCannotBeCreated > 2 && code != ErrorCode.WRN_NubExprIsConstBool2 && code != ErrorCode.WRN_AlignmentMagnitude)
						{
							return false;
						}
					}
					else if (code != ErrorCode.WRN_AttributeIgnoredWhenPublicSigning && code != ErrorCode.WRN_TupleLiteralNameMismatch && code != ErrorCode.WRN_Experimental)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_AttributesOnBackingFieldsNotAvailable)
				{
					if (code != ErrorCode.WRN_UnreferencedLocalFunction && code - ErrorCode.WRN_FilterIsConstantFalse > 1 && code != ErrorCode.WRN_AttributesOnBackingFieldsNotAvailable)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_TypeParameterSameAsOuterMethodTypeParameter)
				{
					if (code != ErrorCode.WRN_TupleBinopLiteralNameMismatch && code != ErrorCode.WRN_TypeParameterSameAsOuterMethodTypeParameter)
					{
						return false;
					}
				}
				else if (code - ErrorCode.WRN_UnconsumedEnumeratorCancellationAttributeUsage > 1 && code != ErrorCode.WRN_SwitchExpressionNotExhaustive)
				{
					return false;
				}
			}
			else if (code <= ErrorCode.WRN_NullabilityMismatchInReturnTypeOnPartial)
			{
				if (code <= ErrorCode.WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue)
				{
					if (code - ErrorCode.WRN_CaseConstantNamedUnderscore > 1 && code - ErrorCode.WRN_GivenExpressionNeverMatchesPattern > 1 && code != ErrorCode.WRN_SwitchExpressionNotExhaustiveWithUnnamedEnumValue)
					{
						return false;
					}
				}
				else if (code <= ErrorCode.WRN_NullabilityMismatchInTypeParameterNotNullConstraint)
				{
					switch (code)
					{
					case ErrorCode.WRN_ThrowPossibleNull:
					case ErrorCode.WRN_ConvertingNullableToNonNullable:
					case ErrorCode.WRN_NullReferenceAssignment:
					case ErrorCode.WRN_NullReferenceReceiver:
					case ErrorCode.WRN_NullReferenceReturn:
					case ErrorCode.WRN_NullReferenceArgument:
					case ErrorCode.WRN_UnboxPossibleNull:
					case ErrorCode.WRN_DisallowNullAttributeForbidsMaybeNullAssignment:
					case ErrorCode.WRN_NullabilityMismatchInTypeOnOverride:
					case ErrorCode.WRN_NullabilityMismatchInReturnTypeOnOverride:
					case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnOverride:
					case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnPartial:
					case ErrorCode.WRN_NullabilityMismatchInTypeOnImplicitImplementation:
					case ErrorCode.WRN_NullabilityMismatchInReturnTypeOnImplicitImplementation:
					case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnImplicitImplementation:
					case ErrorCode.WRN_NullabilityMismatchInTypeOnExplicitImplementation:
					case ErrorCode.WRN_NullabilityMismatchInReturnTypeOnExplicitImplementation:
					case ErrorCode.WRN_NullabilityMismatchInParameterTypeOnExplicitImplementation:
					case ErrorCode.WRN_UninitializedNonNullableField:
					case ErrorCode.WRN_NullabilityMismatchInAssignment:
					case ErrorCode.WRN_NullabilityMismatchInArgument:
					case ErrorCode.WRN_NullabilityMismatchInReturnTypeOfTargetDelegate:
					case ErrorCode.WRN_NullabilityMismatchInParameterTypeOfTargetDelegate:
					case ErrorCode.WRN_NullabilityMismatchInArgumentForOutput:
					case ErrorCode.WRN_NullAsNonNullable:
					case ErrorCode.WRN_NullableValueTypeMayBeNull:
					case ErrorCode.WRN_NullabilityMismatchInTypeParameterConstraint:
					case ErrorCode.WRN_MissingNonNullTypesContextForAnnotation:
					case ErrorCode.WRN_NullabilityMismatchInConstraintsOnImplicitImplementation:
					case ErrorCode.WRN_NullabilityMismatchInTypeParameterReferenceTypeConstraint:
					case ErrorCode.WRN_NullabilityMismatchInExplicitlyImplementedInterface:
					case ErrorCode.WRN_NullabilityMismatchInInterfaceImplementedByBase:
					case ErrorCode.WRN_DuplicateInterfaceWithNullabilityMismatchInBaseList:
					case ErrorCode.WRN_SwitchExpressionNotExhaustiveForNull:
					case ErrorCode.WRN_ImplicitCopyInReadOnlyMember:
					case ErrorCode.WRN_NullabilityMismatchInConstraintsOnPartialImplementation:
					case ErrorCode.WRN_MissingNonNullTypesContextForAnnotationInGeneratedCode:
					case ErrorCode.WRN_NullReferenceInitializer:
						break;
					case ErrorCode.ERR_IllegalSuppression:
					case (ErrorCode)8599:
					case (ErrorCode)8606:
					case ErrorCode.ERR_ExplicitNullableAttribute:
					case (ErrorCode)8626:
					case ErrorCode.ERR_NullableUnconstrainedTypeParameter:
					case ErrorCode.ERR_AnnotationDisallowedInObjectCreation:
					case ErrorCode.ERR_NullableOptionNotAvailable:
					case ErrorCode.ERR_TripleDotNotAllowed:
					case ErrorCode.ERR_BadNullableContextOption:
					case ErrorCode.ERR_NullableDirectiveQualifierExpected:
					case (ErrorCode)8638:
					case ErrorCode.ERR_BadNullableTypeof:
					case ErrorCode.ERR_ExpressionTreeCantContainRefStruct:
					case ErrorCode.ERR_ElseCannotStartStatement:
					case ErrorCode.ERR_ExpressionTreeCantContainNullCoalescingAssignment:
					case ErrorCode.ERR_DuplicateExplicitImpl:
					case ErrorCode.ERR_UsingVarInSwitchCase:
					case ErrorCode.ERR_GoToForwardJumpOverUsingVar:
					case ErrorCode.ERR_GoToBackwardJumpOverUsingVar:
					case ErrorCode.ERR_IsNullableType:
					case ErrorCode.ERR_AsNullableType:
					case ErrorCode.ERR_FeatureInPreview:
					case (ErrorCode)8653:
					case (ErrorCode)8654:
					case ErrorCode.ERR_StaticMemberCantBeReadOnly:
					case ErrorCode.ERR_AutoSetterCantBeReadOnly:
					case ErrorCode.ERR_AutoPropertyWithSetterCantBeReadOnly:
					case ErrorCode.ERR_InvalidPropertyReadOnlyMods:
					case ErrorCode.ERR_DuplicatePropertyReadOnlyMods:
					case ErrorCode.ERR_FieldLikeEventCantBeReadOnly:
					case ErrorCode.ERR_PartialMethodReadOnlyDifference:
					case ErrorCode.ERR_ReadOnlyModMissingAccessor:
					case ErrorCode.ERR_OverrideRefConstraintNotSatisfied:
					case ErrorCode.ERR_OverrideValConstraintNotSatisfied:
					case ErrorCode.ERR_NullableDirectiveTargetExpected:
						return false;
					default:
						if (code != ErrorCode.WRN_NullabilityMismatchInTypeParameterNotNullConstraint)
						{
							return false;
						}
						break;
					}
				}
				else
				{
					switch (code)
					{
					case ErrorCode.WRN_ParameterConditionallyDisallowsNull:
					case ErrorCode.WRN_ShouldNotReturn:
					case ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnOverride:
					case ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnOverride:
					case ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnImplicitImplementation:
					case ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnImplicitImplementation:
					case ErrorCode.WRN_TopLevelNullabilityMismatchInReturnTypeOnExplicitImplementation:
					case ErrorCode.WRN_TopLevelNullabilityMismatchInParameterTypeOnExplicitImplementation:
					case ErrorCode.WRN_DoesNotReturnMismatch:
					case ErrorCode.WRN_MemberNotNull:
					case ErrorCode.WRN_MemberNotNullWhen:
					case ErrorCode.WRN_MemberNotNullBadMember:
					case ErrorCode.WRN_ParameterDisallowsNull:
					case ErrorCode.WRN_ConstOutOfRangeChecked:
					case ErrorCode.WRN_GeneratorFailedDuringInitialization:
					case ErrorCode.WRN_GeneratorFailedDuringGeneration:
					case ErrorCode.WRN_GivenExpressionAlwaysMatchesPattern:
					case ErrorCode.WRN_IsPatternAlways:
						break;
					case ErrorCode.ERR_NoOutputDirectory:
					case ErrorCode.ERR_StdInOptionProvidedButConsoleInputIsNotRedirected:
					case ErrorCode.ERR_FeatureNotAvailableInVersion9:
					case ErrorCode.ERR_DuplicateInterfaceWithDifferencesInBaseList:
					case ErrorCode.ERR_DesignatorBeneathPatternCombinator:
					case ErrorCode.ERR_UnsupportedTypeForRelationalPattern:
					case ErrorCode.ERR_RelationalPatternWithNaN:
					case ErrorCode.ERR_ConditionalOnLocalFunction:
					case ErrorCode.ERR_WrongFuncPtrCallingConvention:
					case ErrorCode.ERR_MissingAddressOf:
					case ErrorCode.ERR_CannotUseReducedExtensionMethodInAddressOf:
					case ErrorCode.ERR_CannotUseFunctionPointerAsFixedLocal:
					case ErrorCode.ERR_ExpressionTreeContainsPatternIndexOrRangeIndexer:
					case ErrorCode.ERR_ExpressionTreeContainsFromEndIndexExpression:
					case ErrorCode.ERR_ExpressionTreeContainsRangeExpression:
						return false;
					default:
						if (code != ErrorCode.WRN_NullabilityMismatchInReturnTypeOnPartial)
						{
							return false;
						}
						break;
					}
				}
			}
			else if (code <= ErrorCode.WRN_RecordEqualsWithoutGetHashCode)
			{
				if (code - ErrorCode.WRN_ParameterNotNullIfNotNull > 1 && code - ErrorCode.WRN_SwitchExpressionNotExhaustiveWithWhen > 2 && code - ErrorCode.WRN_AnalyzerReferencesFramework > 1)
				{
					return false;
				}
			}
			else if (code <= ErrorCode.WRN_ReturnTypeIsStaticClass)
			{
				if (code != ErrorCode.WRN_RecordNamedDisallowed)
				{
					switch (code)
					{
					case ErrorCode.WRN_UnassignedThisAutoProperty:
					case ErrorCode.WRN_UnassignedThis:
					case ErrorCode.WRN_ParamUnassigned:
					case ErrorCode.WRN_UseDefViolationProperty:
					case ErrorCode.WRN_UseDefViolationField:
					case ErrorCode.WRN_UseDefViolationThis:
					case ErrorCode.WRN_UseDefViolationOut:
					case ErrorCode.WRN_UseDefViolation:
					case ErrorCode.WRN_SyncAndAsyncEntryPoints:
					case ErrorCode.WRN_ParameterIsStaticClass:
					case ErrorCode.WRN_ReturnTypeIsStaticClass:
						break;
					case ErrorCode.ERR_CannotSpecifyManagedWithUnmanagedSpecifiers:
					case ErrorCode.ERR_RuntimeDoesNotSupportUnmanagedDefaultCallConv:
					case ErrorCode.ERR_TypeNotFound:
					case ErrorCode.ERR_TypeMustBePublic:
					case ErrorCode.ERR_InvalidUnmanagedCallersOnlyCallConv:
					case ErrorCode.ERR_CannotUseManagedTypeInUnmanagedCallersOnly:
					case ErrorCode.ERR_UnmanagedCallersOnlyMethodOrTypeCannotBeGeneric:
					case ErrorCode.ERR_UnmanagedCallersOnlyRequiresStatic:
						return false;
					default:
						return false;
					}
				}
			}
			else if (code != ErrorCode.WRN_UnreadRecordParameter && code != ErrorCode.WRN_DoNotCompareFunctionPointers)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00087254 File Offset: 0x00085454
		public static bool IsFatal(ErrorCode code)
		{
			if (code <= ErrorCode.FTL_BadCodepage)
			{
				if (code != ErrorCode.FTL_MetadataCantOpenFile && code != ErrorCode.FTL_DebugEmitFailure && code != ErrorCode.FTL_BadCodepage)
				{
					return false;
				}
			}
			else if (code <= ErrorCode.FTL_InvalidInputFileName)
			{
				if (code != ErrorCode.FTL_InvalidTarget && code != ErrorCode.FTL_InvalidInputFileName)
				{
					return false;
				}
			}
			else if (code != ErrorCode.FTL_OutputFileExists && code != ErrorCode.FTL_BadChecksumAlgorithm)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000872AA File Offset: 0x000854AA
		public static bool IsInfo(ErrorCode code)
		{
			return code == ErrorCode.INF_UnableToLoadSomeTypesInAnalyzer;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000872B7 File Offset: 0x000854B7
		public static bool IsHidden(ErrorCode code)
		{
			return code - ErrorCode.HDN_UnusedUsingDirective <= 1;
		}

		// Token: 0x04000D00 RID: 3328
		private const string s_titleSuffix = "_Title";

		// Token: 0x04000D01 RID: 3329
		private const string s_descriptionSuffix = "_Description";

		// Token: 0x04000D02 RID: 3330
		private static readonly Lazy<ImmutableDictionary<ErrorCode, string>> s_categoriesMap = new Lazy<ImmutableDictionary<ErrorCode, string>>(new Func<ImmutableDictionary<ErrorCode, string>>(ErrorFacts.CreateCategoriesMap));

		// Token: 0x04000D03 RID: 3331
		public static readonly ImmutableHashSet<string> NullableWarnings;

		// Token: 0x04000D04 RID: 3332
		private static ResourceManager s_resourceManager;
	}
}
