using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200013F RID: 319
	internal enum MessageID
	{
		// Token: 0x04000D13 RID: 3347
		None,
		// Token: 0x04000D14 RID: 3348
		MessageBase = 1200,
		// Token: 0x04000D15 RID: 3349
		IDS_SK_METHOD = 3200,
		// Token: 0x04000D16 RID: 3350
		IDS_SK_TYPE,
		// Token: 0x04000D17 RID: 3351
		IDS_SK_NAMESPACE,
		// Token: 0x04000D18 RID: 3352
		IDS_SK_FIELD,
		// Token: 0x04000D19 RID: 3353
		IDS_SK_PROPERTY,
		// Token: 0x04000D1A RID: 3354
		IDS_SK_UNKNOWN,
		// Token: 0x04000D1B RID: 3355
		IDS_SK_VARIABLE,
		// Token: 0x04000D1C RID: 3356
		IDS_SK_EVENT,
		// Token: 0x04000D1D RID: 3357
		IDS_SK_TYVAR,
		// Token: 0x04000D1E RID: 3358
		IDS_SK_ALIAS = 3210,
		// Token: 0x04000D1F RID: 3359
		IDS_SK_LABEL = 3212,
		// Token: 0x04000D20 RID: 3360
		IDS_SK_CONSTRUCTOR,
		// Token: 0x04000D21 RID: 3361
		IDS_NULL = 11201,
		// Token: 0x04000D22 RID: 3362
		IDS_XMLIGNORED = 11204,
		// Token: 0x04000D23 RID: 3363
		IDS_XMLIGNORED2,
		// Token: 0x04000D24 RID: 3364
		IDS_XMLFAILEDINCLUDE,
		// Token: 0x04000D25 RID: 3365
		IDS_XMLBADINCLUDE,
		// Token: 0x04000D26 RID: 3366
		IDS_XMLNOINCLUDE,
		// Token: 0x04000D27 RID: 3367
		IDS_XMLMISSINGINCLUDEFILE,
		// Token: 0x04000D28 RID: 3368
		IDS_XMLMISSINGINCLUDEPATH,
		// Token: 0x04000D29 RID: 3369
		IDS_GlobalNamespace,
		// Token: 0x04000D2A RID: 3370
		IDS_FeatureGenerics = 13700,
		// Token: 0x04000D2B RID: 3371
		IDS_FeatureAnonDelegates,
		// Token: 0x04000D2C RID: 3372
		IDS_FeatureModuleAttrLoc,
		// Token: 0x04000D2D RID: 3373
		IDS_FeatureGlobalNamespace,
		// Token: 0x04000D2E RID: 3374
		IDS_FeatureFixedBuffer,
		// Token: 0x04000D2F RID: 3375
		IDS_FeaturePragma,
		// Token: 0x04000D30 RID: 3376
		IDS_FOREACHLOCAL,
		// Token: 0x04000D31 RID: 3377
		IDS_USINGLOCAL,
		// Token: 0x04000D32 RID: 3378
		IDS_FIXEDLOCAL,
		// Token: 0x04000D33 RID: 3379
		IDS_FeatureStaticClasses = 13711,
		// Token: 0x04000D34 RID: 3380
		IDS_FeaturePartialTypes,
		// Token: 0x04000D35 RID: 3381
		IDS_MethodGroup,
		// Token: 0x04000D36 RID: 3382
		IDS_AnonMethod,
		// Token: 0x04000D37 RID: 3383
		IDS_FeatureSwitchOnBool = 13717,
		// Token: 0x04000D38 RID: 3384
		IDS_Collection = 13720,
		// Token: 0x04000D39 RID: 3385
		IDS_FeaturePropertyAccessorMods = 13722,
		// Token: 0x04000D3A RID: 3386
		IDS_FeatureExternAlias,
		// Token: 0x04000D3B RID: 3387
		IDS_FeatureIterators,
		// Token: 0x04000D3C RID: 3388
		IDS_FeatureDefault,
		// Token: 0x04000D3D RID: 3389
		IDS_FeatureNullable = 13728,
		// Token: 0x04000D3E RID: 3390
		IDS_Lambda = 13731,
		// Token: 0x04000D3F RID: 3391
		IDS_FeaturePatternMatching,
		// Token: 0x04000D40 RID: 3392
		IDS_FeatureThrowExpression,
		// Token: 0x04000D41 RID: 3393
		IDS_FeatureImplicitArray = 13757,
		// Token: 0x04000D42 RID: 3394
		IDS_FeatureImplicitLocal,
		// Token: 0x04000D43 RID: 3395
		IDS_FeatureAnonymousTypes,
		// Token: 0x04000D44 RID: 3396
		IDS_FeatureAutoImplementedProperties,
		// Token: 0x04000D45 RID: 3397
		IDS_FeatureObjectInitializer,
		// Token: 0x04000D46 RID: 3398
		IDS_FeatureCollectionInitializer,
		// Token: 0x04000D47 RID: 3399
		IDS_FeatureLambda,
		// Token: 0x04000D48 RID: 3400
		IDS_FeatureQueryExpression,
		// Token: 0x04000D49 RID: 3401
		IDS_FeatureExtensionMethod,
		// Token: 0x04000D4A RID: 3402
		IDS_FeaturePartialMethod,
		// Token: 0x04000D4B RID: 3403
		IDS_FeatureDynamic = 13844,
		// Token: 0x04000D4C RID: 3404
		IDS_FeatureTypeVariance,
		// Token: 0x04000D4D RID: 3405
		IDS_FeatureNamedArgument,
		// Token: 0x04000D4E RID: 3406
		IDS_FeatureOptionalParameter,
		// Token: 0x04000D4F RID: 3407
		IDS_FeatureExceptionFilter,
		// Token: 0x04000D50 RID: 3408
		IDS_FeatureAutoPropertyInitializer,
		// Token: 0x04000D51 RID: 3409
		IDS_SK_TYPE_OR_NAMESPACE = 13852,
		// Token: 0x04000D52 RID: 3410
		IDS_Contravariant = 13859,
		// Token: 0x04000D53 RID: 3411
		IDS_Contravariantly,
		// Token: 0x04000D54 RID: 3412
		IDS_Covariant,
		// Token: 0x04000D55 RID: 3413
		IDS_Covariantly,
		// Token: 0x04000D56 RID: 3414
		IDS_Invariantly,
		// Token: 0x04000D57 RID: 3415
		IDS_FeatureAsync = 13868,
		// Token: 0x04000D58 RID: 3416
		IDS_FeatureStaticAnonymousFunction,
		// Token: 0x04000D59 RID: 3417
		IDS_LIB_ENV = 13880,
		// Token: 0x04000D5A RID: 3418
		IDS_LIB_OPTION,
		// Token: 0x04000D5B RID: 3419
		IDS_REFERENCEPATH_OPTION,
		// Token: 0x04000D5C RID: 3420
		IDS_DirectoryDoesNotExist,
		// Token: 0x04000D5D RID: 3421
		IDS_DirectoryHasInvalidPath,
		// Token: 0x04000D5E RID: 3422
		IDS_Namespace1,
		// Token: 0x04000D5F RID: 3423
		IDS_PathList,
		// Token: 0x04000D60 RID: 3424
		IDS_Text,
		// Token: 0x04000D61 RID: 3425
		IDS_FeatureDiscards,
		// Token: 0x04000D62 RID: 3426
		IDS_FeatureDefaultTypeParameterConstraint,
		// Token: 0x04000D63 RID: 3427
		IDS_FeatureNullPropagatingOperator,
		// Token: 0x04000D64 RID: 3428
		IDS_FeatureExpressionBodiedMethod,
		// Token: 0x04000D65 RID: 3429
		IDS_FeatureExpressionBodiedProperty,
		// Token: 0x04000D66 RID: 3430
		IDS_FeatureExpressionBodiedIndexer,
		// Token: 0x04000D67 RID: 3431
		IDS_FeatureNameof = 13895,
		// Token: 0x04000D68 RID: 3432
		IDS_FeatureDictionaryInitializer,
		// Token: 0x04000D69 RID: 3433
		IDS_ToolName,
		// Token: 0x04000D6A RID: 3434
		IDS_LogoLine1,
		// Token: 0x04000D6B RID: 3435
		IDS_LogoLine2,
		// Token: 0x04000D6C RID: 3436
		IDS_CSCHelp,
		// Token: 0x04000D6D RID: 3437
		IDS_FeatureUsingStatic,
		// Token: 0x04000D6E RID: 3438
		IDS_FeatureInterpolatedStrings,
		// Token: 0x04000D6F RID: 3439
		IDS_OperationCausedStackOverflow,
		// Token: 0x04000D70 RID: 3440
		IDS_AwaitInCatchAndFinally,
		// Token: 0x04000D71 RID: 3441
		IDS_FeatureReadonlyAutoImplementedProperties,
		// Token: 0x04000D72 RID: 3442
		IDS_FeatureBinaryLiteral,
		// Token: 0x04000D73 RID: 3443
		IDS_FeatureDigitSeparator,
		// Token: 0x04000D74 RID: 3444
		IDS_FeatureLocalFunctions,
		// Token: 0x04000D75 RID: 3445
		IDS_FeatureNullableReferenceTypes,
		// Token: 0x04000D76 RID: 3446
		IDS_FeatureRefLocalsReturns,
		// Token: 0x04000D77 RID: 3447
		IDS_FeatureTuples,
		// Token: 0x04000D78 RID: 3448
		IDS_FeatureOutVar = 13913,
		// Token: 0x04000D79 RID: 3449
		IDS_FeatureExpressionBodiedAccessor = 13915,
		// Token: 0x04000D7A RID: 3450
		IDS_FeatureExpressionBodiedDeOrConstructor,
		// Token: 0x04000D7B RID: 3451
		IDS_ThrowExpression,
		// Token: 0x04000D7C RID: 3452
		IDS_FeatureDefaultLiteral,
		// Token: 0x04000D7D RID: 3453
		IDS_FeatureInferredTupleNames,
		// Token: 0x04000D7E RID: 3454
		IDS_FeatureGenericPatternMatching,
		// Token: 0x04000D7F RID: 3455
		IDS_FeatureAsyncMain,
		// Token: 0x04000D80 RID: 3456
		IDS_LangVersions,
		// Token: 0x04000D81 RID: 3457
		IDS_FeatureLeadingDigitSeparator,
		// Token: 0x04000D82 RID: 3458
		IDS_FeatureNonTrailingNamedArguments,
		// Token: 0x04000D83 RID: 3459
		IDS_FeatureReadOnlyReferences,
		// Token: 0x04000D84 RID: 3460
		IDS_FeatureRefStructs,
		// Token: 0x04000D85 RID: 3461
		IDS_FeatureReadOnlyStructs,
		// Token: 0x04000D86 RID: 3462
		IDS_FeatureRefExtensionMethods,
		// Token: 0x04000D87 RID: 3463
		IDS_FeaturePrivateProtected = 13930,
		// Token: 0x04000D88 RID: 3464
		IDS_FeatureRefConditional,
		// Token: 0x04000D89 RID: 3465
		IDS_FeatureAttributesOnBackingFields,
		// Token: 0x04000D8A RID: 3466
		IDS_FeatureImprovedOverloadCandidates,
		// Token: 0x04000D8B RID: 3467
		IDS_FeatureRefReassignment,
		// Token: 0x04000D8C RID: 3468
		IDS_FeatureRefFor,
		// Token: 0x04000D8D RID: 3469
		IDS_FeatureRefForEach,
		// Token: 0x04000D8E RID: 3470
		IDS_FeatureEnumGenericTypeConstraint,
		// Token: 0x04000D8F RID: 3471
		IDS_FeatureDelegateGenericTypeConstraint,
		// Token: 0x04000D90 RID: 3472
		IDS_FeatureUnmanagedGenericTypeConstraint,
		// Token: 0x04000D91 RID: 3473
		IDS_FeatureStackAllocInitializer,
		// Token: 0x04000D92 RID: 3474
		IDS_FeatureTupleEquality,
		// Token: 0x04000D93 RID: 3475
		IDS_FeatureExpressionVariablesInQueriesAndInitializers,
		// Token: 0x04000D94 RID: 3476
		IDS_FeatureExtensibleFixedStatement,
		// Token: 0x04000D95 RID: 3477
		IDS_FeatureIndexingMovableFixedBuffers,
		// Token: 0x04000D96 RID: 3478
		IDS_FeatureAltInterpolatedVerbatimStrings,
		// Token: 0x04000D97 RID: 3479
		IDS_FeatureCoalesceAssignmentExpression,
		// Token: 0x04000D98 RID: 3480
		IDS_FeatureUnconstrainedTypeParameterInNullCoalescingOperator,
		// Token: 0x04000D99 RID: 3481
		IDS_FeatureNotNullGenericTypeConstraint,
		// Token: 0x04000D9A RID: 3482
		IDS_FeatureIndexOperator,
		// Token: 0x04000D9B RID: 3483
		IDS_FeatureRangeOperator,
		// Token: 0x04000D9C RID: 3484
		IDS_FeatureAsyncStreams,
		// Token: 0x04000D9D RID: 3485
		IDS_FeatureRecursivePatterns,
		// Token: 0x04000D9E RID: 3486
		IDS_Disposable,
		// Token: 0x04000D9F RID: 3487
		IDS_FeatureUsingDeclarations,
		// Token: 0x04000DA0 RID: 3488
		IDS_FeatureStaticLocalFunctions,
		// Token: 0x04000DA1 RID: 3489
		IDS_FeatureNameShadowingInNestedFunctions,
		// Token: 0x04000DA2 RID: 3490
		IDS_FeatureUnmanagedConstructedTypes,
		// Token: 0x04000DA3 RID: 3491
		IDS_FeatureObsoleteOnPropertyAccessor,
		// Token: 0x04000DA4 RID: 3492
		IDS_FeatureReadOnlyMembers,
		// Token: 0x04000DA5 RID: 3493
		IDS_DefaultInterfaceImplementation,
		// Token: 0x04000DA6 RID: 3494
		IDS_OverrideWithConstraints,
		// Token: 0x04000DA7 RID: 3495
		IDS_FeatureNestedStackalloc,
		// Token: 0x04000DA8 RID: 3496
		IDS_FeatureSwitchExpression,
		// Token: 0x04000DA9 RID: 3497
		IDS_FeatureAsyncUsing,
		// Token: 0x04000DAA RID: 3498
		IDS_FeatureLambdaDiscardParameters,
		// Token: 0x04000DAB RID: 3499
		IDS_FeatureLocalFunctionAttributes,
		// Token: 0x04000DAC RID: 3500
		IDS_FeatureExternLocalFunctions,
		// Token: 0x04000DAD RID: 3501
		IDS_FeatureMemberNotNull,
		// Token: 0x04000DAE RID: 3502
		IDS_FeatureNativeInt,
		// Token: 0x04000DAF RID: 3503
		IDS_FeatureImplicitObjectCreation,
		// Token: 0x04000DB0 RID: 3504
		IDS_FeatureTypePattern,
		// Token: 0x04000DB1 RID: 3505
		IDS_FeatureParenthesizedPattern,
		// Token: 0x04000DB2 RID: 3506
		IDS_FeatureOrPattern,
		// Token: 0x04000DB3 RID: 3507
		IDS_FeatureAndPattern,
		// Token: 0x04000DB4 RID: 3508
		IDS_FeatureNotPattern,
		// Token: 0x04000DB5 RID: 3509
		IDS_FeatureRelationalPattern,
		// Token: 0x04000DB6 RID: 3510
		IDS_FeatureExtendedPartialMethods,
		// Token: 0x04000DB7 RID: 3511
		IDS_TopLevelStatements,
		// Token: 0x04000DB8 RID: 3512
		IDS_FeatureFunctionPointers,
		// Token: 0x04000DB9 RID: 3513
		IDS_AddressOfMethodGroup,
		// Token: 0x04000DBA RID: 3514
		IDS_FeatureInitOnlySetters,
		// Token: 0x04000DBB RID: 3515
		IDS_FeatureRecords,
		// Token: 0x04000DBC RID: 3516
		IDS_FeatureNullPointerConstantPattern,
		// Token: 0x04000DBD RID: 3517
		IDS_FeatureModuleInitializers,
		// Token: 0x04000DBE RID: 3518
		IDS_FeatureTargetTypedConditional,
		// Token: 0x04000DBF RID: 3519
		IDS_FeatureCovariantReturnsForOverrides,
		// Token: 0x04000DC0 RID: 3520
		IDS_FeatureExtensionGetEnumerator,
		// Token: 0x04000DC1 RID: 3521
		IDS_FeatureExtensionGetAsyncEnumerator,
		// Token: 0x04000DC2 RID: 3522
		IDS_Parameter,
		// Token: 0x04000DC3 RID: 3523
		IDS_Return,
		// Token: 0x04000DC4 RID: 3524
		IDS_FeatureVarianceSafetyForStaticInterfaceMembers,
		// Token: 0x04000DC5 RID: 3525
		IDS_FeatureConstantInterpolatedStrings,
		// Token: 0x04000DC6 RID: 3526
		IDS_FeatureMixedDeclarationsAndExpressionsInDeconstruction,
		// Token: 0x04000DC7 RID: 3527
		IDS_FeatureSealedToStringInRecord,
		// Token: 0x04000DC8 RID: 3528
		IDS_FeatureRecordStructs,
		// Token: 0x04000DC9 RID: 3529
		IDS_FeatureWithOnStructs,
		// Token: 0x04000DCA RID: 3530
		IDS_FeaturePositionalFieldsInRecords,
		// Token: 0x04000DCB RID: 3531
		IDS_FeatureGlobalUsing,
		// Token: 0x04000DCC RID: 3532
		IDS_FeatureInferredDelegateType,
		// Token: 0x04000DCD RID: 3533
		IDS_FeatureLambdaAttributes
	}
}
