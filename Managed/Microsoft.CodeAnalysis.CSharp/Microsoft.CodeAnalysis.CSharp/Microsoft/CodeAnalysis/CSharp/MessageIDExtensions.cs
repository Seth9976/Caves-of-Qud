using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000141 RID: 321
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class MessageIDExtensions
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x00087652 File Offset: 0x00085852
		public static LocalizableErrorArgument Localize(this MessageID id)
		{
			return new LocalizableErrorArgument(id);
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0008765A File Offset: 0x0008585A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static string RequiredFeature(this MessageID feature)
		{
			return null;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00087660 File Offset: 0x00085860
		internal static bool CheckFeatureAvailability(this MessageID feature, BindingDiagnosticBag diagnostics, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] Location location = null)
		{
			CSDiagnosticInfo featureAvailabilityDiagnosticInfo = feature.GetFeatureAvailabilityDiagnosticInfo((CSharpParseOptions)syntax.SyntaxTree.Options);
			if (featureAvailabilityDiagnosticInfo != null)
			{
				diagnostics.Add(featureAvailabilityDiagnosticInfo, location ?? syntax.GetLocation());
				return false;
			}
			return true;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0008769C File Offset: 0x0008589C
		internal static bool CheckFeatureAvailability(this MessageID feature, BindingDiagnosticBag diagnostics, Compilation compilation, Location location)
		{
			CSDiagnosticInfo featureAvailabilityDiagnosticInfo = feature.GetFeatureAvailabilityDiagnosticInfo((CSharpCompilation)compilation);
			if (featureAvailabilityDiagnosticInfo != null)
			{
				diagnostics.Add(featureAvailabilityDiagnosticInfo, location);
				return false;
			}
			return true;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000876C4 File Offset: 0x000858C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static CSDiagnosticInfo GetFeatureAvailabilityDiagnosticInfo(this MessageID feature, CSharpParseOptions options)
		{
			if (!options.IsFeatureEnabled(feature))
			{
				return MessageIDExtensions.GetDisabledFeatureDiagnosticInfo(feature, options.LanguageVersion);
			}
			return null;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000876DD File Offset: 0x000858DD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static CSDiagnosticInfo GetFeatureAvailabilityDiagnosticInfo(this MessageID feature, CSharpCompilation compilation)
		{
			if (!compilation.IsFeatureEnabled(feature))
			{
				return MessageIDExtensions.GetDisabledFeatureDiagnosticInfo(feature, compilation.LanguageVersion);
			}
			return null;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000876F8 File Offset: 0x000858F8
		private static CSDiagnosticInfo GetDisabledFeatureDiagnosticInfo(MessageID feature, LanguageVersion availableVersion)
		{
			string text = feature.RequiredFeature();
			if (text != null)
			{
				return new CSDiagnosticInfo(ErrorCode.ERR_FeatureIsExperimental, new object[]
				{
					feature.Localize(),
					text
				});
			}
			LanguageVersion languageVersion = feature.RequiredVersion();
			if (languageVersion != LanguageVersion.Preview.MapSpecifiedToEffectiveVersion())
			{
				return new CSDiagnosticInfo(availableVersion.GetErrorCode(), new object[]
				{
					feature.Localize(),
					new CSharpRequiredLanguageVersion(languageVersion)
				});
			}
			return new CSDiagnosticInfo(ErrorCode.ERR_FeatureInPreview, new object[] { feature.Localize() });
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00087790 File Offset: 0x00085990
		internal static LanguageVersion RequiredVersion(this MessageID feature)
		{
			if (feature > MessageID.IDS_FeatureDefault)
			{
				if (feature <= MessageID.IDS_FeatureThrowExpression)
				{
					if (feature == MessageID.IDS_FeatureNullable)
					{
						return LanguageVersion.CSharp2;
					}
					if (feature - MessageID.IDS_FeaturePatternMatching > 1)
					{
						goto IL_0352;
					}
				}
				else
				{
					if (feature - MessageID.IDS_FeatureImplicitArray <= 9)
					{
						return LanguageVersion.CSharp3;
					}
					switch (feature)
					{
					case MessageID.IDS_FeatureDynamic:
					case MessageID.IDS_FeatureTypeVariance:
					case MessageID.IDS_FeatureNamedArgument:
					case MessageID.IDS_FeatureOptionalParameter:
						return LanguageVersion.CSharp4;
					case MessageID.IDS_FeatureExceptionFilter:
					case MessageID.IDS_FeatureAutoPropertyInitializer:
					case MessageID.IDS_FeatureNullPropagatingOperator:
					case MessageID.IDS_FeatureExpressionBodiedMethod:
					case MessageID.IDS_FeatureExpressionBodiedProperty:
					case MessageID.IDS_FeatureExpressionBodiedIndexer:
					case MessageID.IDS_FeatureNameof:
					case MessageID.IDS_FeatureDictionaryInitializer:
					case MessageID.IDS_FeatureUsingStatic:
					case MessageID.IDS_FeatureInterpolatedStrings:
					case MessageID.IDS_AwaitInCatchAndFinally:
					case MessageID.IDS_FeatureReadonlyAutoImplementedProperties:
						return LanguageVersion.CSharp6;
					case (MessageID)13850:
					case (MessageID)13851:
					case MessageID.IDS_SK_TYPE_OR_NAMESPACE:
					case (MessageID)13853:
					case (MessageID)13854:
					case (MessageID)13855:
					case (MessageID)13856:
					case (MessageID)13857:
					case (MessageID)13858:
					case MessageID.IDS_Contravariant:
					case MessageID.IDS_Contravariantly:
					case MessageID.IDS_Covariant:
					case MessageID.IDS_Covariantly:
					case MessageID.IDS_Invariantly:
					case (MessageID)13864:
					case (MessageID)13865:
					case (MessageID)13866:
					case (MessageID)13867:
					case (MessageID)13870:
					case (MessageID)13871:
					case (MessageID)13872:
					case (MessageID)13873:
					case (MessageID)13874:
					case (MessageID)13875:
					case (MessageID)13876:
					case (MessageID)13877:
					case (MessageID)13878:
					case (MessageID)13879:
					case MessageID.IDS_LIB_ENV:
					case MessageID.IDS_LIB_OPTION:
					case MessageID.IDS_REFERENCEPATH_OPTION:
					case MessageID.IDS_DirectoryDoesNotExist:
					case MessageID.IDS_DirectoryHasInvalidPath:
					case MessageID.IDS_Namespace1:
					case MessageID.IDS_PathList:
					case MessageID.IDS_Text:
					case (MessageID)13894:
					case MessageID.IDS_ToolName:
					case MessageID.IDS_LogoLine1:
					case MessageID.IDS_LogoLine2:
					case MessageID.IDS_CSCHelp:
					case MessageID.IDS_OperationCausedStackOverflow:
					case (MessageID)13912:
					case (MessageID)13914:
					case MessageID.IDS_ThrowExpression:
					case MessageID.IDS_LangVersions:
					case (MessageID)13929:
					case MessageID.IDS_Disposable:
					case MessageID.IDS_AddressOfMethodGroup:
					case MessageID.IDS_Parameter:
					case MessageID.IDS_Return:
						goto IL_0352;
					case MessageID.IDS_FeatureAsync:
						return LanguageVersion.CSharp5;
					case MessageID.IDS_FeatureStaticAnonymousFunction:
					case MessageID.IDS_FeatureDefaultTypeParameterConstraint:
					case MessageID.IDS_FeatureLambdaDiscardParameters:
					case MessageID.IDS_FeatureLocalFunctionAttributes:
					case MessageID.IDS_FeatureExternLocalFunctions:
					case MessageID.IDS_FeatureMemberNotNull:
					case MessageID.IDS_FeatureNativeInt:
					case MessageID.IDS_FeatureImplicitObjectCreation:
					case MessageID.IDS_FeatureTypePattern:
					case MessageID.IDS_FeatureParenthesizedPattern:
					case MessageID.IDS_FeatureOrPattern:
					case MessageID.IDS_FeatureAndPattern:
					case MessageID.IDS_FeatureNotPattern:
					case MessageID.IDS_FeatureRelationalPattern:
					case MessageID.IDS_FeatureExtendedPartialMethods:
					case MessageID.IDS_TopLevelStatements:
					case MessageID.IDS_FeatureFunctionPointers:
					case MessageID.IDS_FeatureInitOnlySetters:
					case MessageID.IDS_FeatureRecords:
					case MessageID.IDS_FeatureModuleInitializers:
					case MessageID.IDS_FeatureTargetTypedConditional:
					case MessageID.IDS_FeatureCovariantReturnsForOverrides:
					case MessageID.IDS_FeatureExtensionGetEnumerator:
					case MessageID.IDS_FeatureExtensionGetAsyncEnumerator:
						return LanguageVersion.CSharp9;
					case MessageID.IDS_FeatureDiscards:
					case MessageID.IDS_FeatureBinaryLiteral:
					case MessageID.IDS_FeatureDigitSeparator:
					case MessageID.IDS_FeatureLocalFunctions:
					case MessageID.IDS_FeatureRefLocalsReturns:
					case MessageID.IDS_FeatureTuples:
					case MessageID.IDS_FeatureOutVar:
					case MessageID.IDS_FeatureExpressionBodiedAccessor:
					case MessageID.IDS_FeatureExpressionBodiedDeOrConstructor:
						break;
					case MessageID.IDS_FeatureNullableReferenceTypes:
					case MessageID.IDS_FeatureAltInterpolatedVerbatimStrings:
					case MessageID.IDS_FeatureCoalesceAssignmentExpression:
					case MessageID.IDS_FeatureUnconstrainedTypeParameterInNullCoalescingOperator:
					case MessageID.IDS_FeatureNotNullGenericTypeConstraint:
					case MessageID.IDS_FeatureIndexOperator:
					case MessageID.IDS_FeatureRangeOperator:
					case MessageID.IDS_FeatureAsyncStreams:
					case MessageID.IDS_FeatureRecursivePatterns:
					case MessageID.IDS_FeatureUsingDeclarations:
					case MessageID.IDS_FeatureStaticLocalFunctions:
					case MessageID.IDS_FeatureNameShadowingInNestedFunctions:
					case MessageID.IDS_FeatureUnmanagedConstructedTypes:
					case MessageID.IDS_FeatureObsoleteOnPropertyAccessor:
					case MessageID.IDS_FeatureReadOnlyMembers:
					case MessageID.IDS_DefaultInterfaceImplementation:
					case MessageID.IDS_OverrideWithConstraints:
					case MessageID.IDS_FeatureNestedStackalloc:
					case MessageID.IDS_FeatureSwitchExpression:
					case MessageID.IDS_FeatureAsyncUsing:
					case MessageID.IDS_FeatureNullPointerConstantPattern:
						return LanguageVersion.CSharp8;
					case MessageID.IDS_FeatureDefaultLiteral:
					case MessageID.IDS_FeatureInferredTupleNames:
					case MessageID.IDS_FeatureGenericPatternMatching:
					case MessageID.IDS_FeatureAsyncMain:
						return LanguageVersion.CSharp7_1;
					case MessageID.IDS_FeatureLeadingDigitSeparator:
					case MessageID.IDS_FeatureNonTrailingNamedArguments:
					case MessageID.IDS_FeatureReadOnlyReferences:
					case MessageID.IDS_FeatureRefStructs:
					case MessageID.IDS_FeatureReadOnlyStructs:
					case MessageID.IDS_FeatureRefExtensionMethods:
					case MessageID.IDS_FeaturePrivateProtected:
					case MessageID.IDS_FeatureRefConditional:
						return LanguageVersion.CSharp7_2;
					case MessageID.IDS_FeatureAttributesOnBackingFields:
					case MessageID.IDS_FeatureImprovedOverloadCandidates:
					case MessageID.IDS_FeatureRefReassignment:
					case MessageID.IDS_FeatureRefFor:
					case MessageID.IDS_FeatureRefForEach:
					case MessageID.IDS_FeatureEnumGenericTypeConstraint:
					case MessageID.IDS_FeatureDelegateGenericTypeConstraint:
					case MessageID.IDS_FeatureUnmanagedGenericTypeConstraint:
					case MessageID.IDS_FeatureStackAllocInitializer:
					case MessageID.IDS_FeatureTupleEquality:
					case MessageID.IDS_FeatureExpressionVariablesInQueriesAndInitializers:
					case MessageID.IDS_FeatureExtensibleFixedStatement:
					case MessageID.IDS_FeatureIndexingMovableFixedBuffers:
						return LanguageVersion.CSharp7_3;
					case MessageID.IDS_FeatureVarianceSafetyForStaticInterfaceMembers:
					case MessageID.IDS_FeatureConstantInterpolatedStrings:
						return LanguageVersion.Preview;
					case MessageID.IDS_FeatureMixedDeclarationsAndExpressionsInDeconstruction:
					case MessageID.IDS_FeatureSealedToStringInRecord:
					case MessageID.IDS_FeatureRecordStructs:
					case MessageID.IDS_FeatureWithOnStructs:
					case MessageID.IDS_FeaturePositionalFieldsInRecords:
					case MessageID.IDS_FeatureGlobalUsing:
					case MessageID.IDS_FeatureInferredDelegateType:
					case MessageID.IDS_FeatureLambdaAttributes:
						return LanguageVersion.Preview;
					default:
						goto IL_0352;
					}
				}
				return LanguageVersion.CSharp7;
			}
			switch (feature)
			{
			case MessageID.IDS_FeatureGenerics:
			case MessageID.IDS_FeatureAnonDelegates:
			case MessageID.IDS_FeatureGlobalNamespace:
			case MessageID.IDS_FeatureFixedBuffer:
			case MessageID.IDS_FeaturePragma:
			case MessageID.IDS_FeatureStaticClasses:
			case MessageID.IDS_FeaturePartialTypes:
				break;
			case MessageID.IDS_FeatureModuleAttrLoc:
				return LanguageVersion.CSharp1;
			case MessageID.IDS_FOREACHLOCAL:
			case MessageID.IDS_USINGLOCAL:
			case MessageID.IDS_FIXEDLOCAL:
			case (MessageID)13709:
			case (MessageID)13710:
				goto IL_0352;
			default:
				if (feature != MessageID.IDS_FeatureSwitchOnBool && feature - MessageID.IDS_FeaturePropertyAccessorMods > 3)
				{
					goto IL_0352;
				}
				break;
			}
			return LanguageVersion.CSharp2;
			IL_0352:
			throw ExceptionUtilities.UnexpectedValue(feature);
		}
	}
}
