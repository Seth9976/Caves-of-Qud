using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000234 RID: 564
	public enum SyntaxKind : ushort
	{
		// Token: 0x0400115D RID: 4445
		None,
		// Token: 0x0400115E RID: 4446
		List,
		// Token: 0x0400115F RID: 4447
		TildeToken = 8193,
		// Token: 0x04001160 RID: 4448
		ExclamationToken,
		// Token: 0x04001161 RID: 4449
		DollarToken,
		// Token: 0x04001162 RID: 4450
		PercentToken,
		// Token: 0x04001163 RID: 4451
		CaretToken,
		// Token: 0x04001164 RID: 4452
		AmpersandToken,
		// Token: 0x04001165 RID: 4453
		AsteriskToken,
		// Token: 0x04001166 RID: 4454
		OpenParenToken,
		// Token: 0x04001167 RID: 4455
		CloseParenToken,
		// Token: 0x04001168 RID: 4456
		MinusToken,
		// Token: 0x04001169 RID: 4457
		PlusToken,
		// Token: 0x0400116A RID: 4458
		EqualsToken,
		// Token: 0x0400116B RID: 4459
		OpenBraceToken,
		// Token: 0x0400116C RID: 4460
		CloseBraceToken,
		// Token: 0x0400116D RID: 4461
		OpenBracketToken,
		// Token: 0x0400116E RID: 4462
		CloseBracketToken,
		// Token: 0x0400116F RID: 4463
		BarToken,
		// Token: 0x04001170 RID: 4464
		BackslashToken,
		// Token: 0x04001171 RID: 4465
		ColonToken,
		// Token: 0x04001172 RID: 4466
		SemicolonToken,
		// Token: 0x04001173 RID: 4467
		DoubleQuoteToken,
		// Token: 0x04001174 RID: 4468
		SingleQuoteToken,
		// Token: 0x04001175 RID: 4469
		LessThanToken,
		// Token: 0x04001176 RID: 4470
		CommaToken,
		// Token: 0x04001177 RID: 4471
		GreaterThanToken,
		// Token: 0x04001178 RID: 4472
		DotToken,
		// Token: 0x04001179 RID: 4473
		QuestionToken,
		// Token: 0x0400117A RID: 4474
		HashToken,
		// Token: 0x0400117B RID: 4475
		SlashToken,
		// Token: 0x0400117C RID: 4476
		DotDotToken,
		// Token: 0x0400117D RID: 4477
		SlashGreaterThanToken = 8232,
		// Token: 0x0400117E RID: 4478
		LessThanSlashToken,
		// Token: 0x0400117F RID: 4479
		XmlCommentStartToken,
		// Token: 0x04001180 RID: 4480
		XmlCommentEndToken,
		// Token: 0x04001181 RID: 4481
		XmlCDataStartToken,
		// Token: 0x04001182 RID: 4482
		XmlCDataEndToken,
		// Token: 0x04001183 RID: 4483
		XmlProcessingInstructionStartToken,
		// Token: 0x04001184 RID: 4484
		XmlProcessingInstructionEndToken,
		// Token: 0x04001185 RID: 4485
		BarBarToken = 8260,
		// Token: 0x04001186 RID: 4486
		AmpersandAmpersandToken,
		// Token: 0x04001187 RID: 4487
		MinusMinusToken,
		// Token: 0x04001188 RID: 4488
		PlusPlusToken,
		// Token: 0x04001189 RID: 4489
		ColonColonToken,
		// Token: 0x0400118A RID: 4490
		QuestionQuestionToken,
		// Token: 0x0400118B RID: 4491
		MinusGreaterThanToken,
		// Token: 0x0400118C RID: 4492
		ExclamationEqualsToken,
		// Token: 0x0400118D RID: 4493
		EqualsEqualsToken,
		// Token: 0x0400118E RID: 4494
		EqualsGreaterThanToken,
		// Token: 0x0400118F RID: 4495
		LessThanEqualsToken,
		// Token: 0x04001190 RID: 4496
		LessThanLessThanToken,
		// Token: 0x04001191 RID: 4497
		LessThanLessThanEqualsToken,
		// Token: 0x04001192 RID: 4498
		GreaterThanEqualsToken,
		// Token: 0x04001193 RID: 4499
		GreaterThanGreaterThanToken,
		// Token: 0x04001194 RID: 4500
		GreaterThanGreaterThanEqualsToken,
		// Token: 0x04001195 RID: 4501
		SlashEqualsToken,
		// Token: 0x04001196 RID: 4502
		AsteriskEqualsToken,
		// Token: 0x04001197 RID: 4503
		BarEqualsToken,
		// Token: 0x04001198 RID: 4504
		AmpersandEqualsToken,
		// Token: 0x04001199 RID: 4505
		PlusEqualsToken,
		// Token: 0x0400119A RID: 4506
		MinusEqualsToken,
		// Token: 0x0400119B RID: 4507
		CaretEqualsToken,
		// Token: 0x0400119C RID: 4508
		PercentEqualsToken,
		// Token: 0x0400119D RID: 4509
		QuestionQuestionEqualsToken,
		// Token: 0x0400119E RID: 4510
		BoolKeyword = 8304,
		// Token: 0x0400119F RID: 4511
		ByteKeyword,
		// Token: 0x040011A0 RID: 4512
		SByteKeyword,
		// Token: 0x040011A1 RID: 4513
		ShortKeyword,
		// Token: 0x040011A2 RID: 4514
		UShortKeyword,
		// Token: 0x040011A3 RID: 4515
		IntKeyword,
		// Token: 0x040011A4 RID: 4516
		UIntKeyword,
		// Token: 0x040011A5 RID: 4517
		LongKeyword,
		// Token: 0x040011A6 RID: 4518
		ULongKeyword,
		// Token: 0x040011A7 RID: 4519
		DoubleKeyword,
		// Token: 0x040011A8 RID: 4520
		FloatKeyword,
		// Token: 0x040011A9 RID: 4521
		DecimalKeyword,
		// Token: 0x040011AA RID: 4522
		StringKeyword,
		// Token: 0x040011AB RID: 4523
		CharKeyword,
		// Token: 0x040011AC RID: 4524
		VoidKeyword,
		// Token: 0x040011AD RID: 4525
		ObjectKeyword,
		// Token: 0x040011AE RID: 4526
		TypeOfKeyword,
		// Token: 0x040011AF RID: 4527
		SizeOfKeyword,
		// Token: 0x040011B0 RID: 4528
		NullKeyword,
		// Token: 0x040011B1 RID: 4529
		TrueKeyword,
		// Token: 0x040011B2 RID: 4530
		FalseKeyword,
		// Token: 0x040011B3 RID: 4531
		IfKeyword,
		// Token: 0x040011B4 RID: 4532
		ElseKeyword,
		// Token: 0x040011B5 RID: 4533
		WhileKeyword,
		// Token: 0x040011B6 RID: 4534
		ForKeyword,
		// Token: 0x040011B7 RID: 4535
		ForEachKeyword,
		// Token: 0x040011B8 RID: 4536
		DoKeyword,
		// Token: 0x040011B9 RID: 4537
		SwitchKeyword,
		// Token: 0x040011BA RID: 4538
		CaseKeyword,
		// Token: 0x040011BB RID: 4539
		DefaultKeyword,
		// Token: 0x040011BC RID: 4540
		TryKeyword,
		// Token: 0x040011BD RID: 4541
		CatchKeyword,
		// Token: 0x040011BE RID: 4542
		FinallyKeyword,
		// Token: 0x040011BF RID: 4543
		LockKeyword,
		// Token: 0x040011C0 RID: 4544
		GotoKeyword,
		// Token: 0x040011C1 RID: 4545
		BreakKeyword,
		// Token: 0x040011C2 RID: 4546
		ContinueKeyword,
		// Token: 0x040011C3 RID: 4547
		ReturnKeyword,
		// Token: 0x040011C4 RID: 4548
		ThrowKeyword,
		// Token: 0x040011C5 RID: 4549
		PublicKeyword,
		// Token: 0x040011C6 RID: 4550
		PrivateKeyword,
		// Token: 0x040011C7 RID: 4551
		InternalKeyword,
		// Token: 0x040011C8 RID: 4552
		ProtectedKeyword,
		// Token: 0x040011C9 RID: 4553
		StaticKeyword,
		// Token: 0x040011CA RID: 4554
		ReadOnlyKeyword,
		// Token: 0x040011CB RID: 4555
		SealedKeyword,
		// Token: 0x040011CC RID: 4556
		ConstKeyword,
		// Token: 0x040011CD RID: 4557
		FixedKeyword,
		// Token: 0x040011CE RID: 4558
		StackAllocKeyword,
		// Token: 0x040011CF RID: 4559
		VolatileKeyword,
		// Token: 0x040011D0 RID: 4560
		NewKeyword,
		// Token: 0x040011D1 RID: 4561
		OverrideKeyword,
		// Token: 0x040011D2 RID: 4562
		AbstractKeyword,
		// Token: 0x040011D3 RID: 4563
		VirtualKeyword,
		// Token: 0x040011D4 RID: 4564
		EventKeyword,
		// Token: 0x040011D5 RID: 4565
		ExternKeyword,
		// Token: 0x040011D6 RID: 4566
		RefKeyword,
		// Token: 0x040011D7 RID: 4567
		OutKeyword,
		// Token: 0x040011D8 RID: 4568
		InKeyword,
		// Token: 0x040011D9 RID: 4569
		IsKeyword,
		// Token: 0x040011DA RID: 4570
		AsKeyword,
		// Token: 0x040011DB RID: 4571
		ParamsKeyword,
		// Token: 0x040011DC RID: 4572
		ArgListKeyword,
		// Token: 0x040011DD RID: 4573
		MakeRefKeyword,
		// Token: 0x040011DE RID: 4574
		RefTypeKeyword,
		// Token: 0x040011DF RID: 4575
		RefValueKeyword,
		// Token: 0x040011E0 RID: 4576
		ThisKeyword,
		// Token: 0x040011E1 RID: 4577
		BaseKeyword,
		// Token: 0x040011E2 RID: 4578
		NamespaceKeyword,
		// Token: 0x040011E3 RID: 4579
		UsingKeyword,
		// Token: 0x040011E4 RID: 4580
		ClassKeyword,
		// Token: 0x040011E5 RID: 4581
		StructKeyword,
		// Token: 0x040011E6 RID: 4582
		InterfaceKeyword,
		// Token: 0x040011E7 RID: 4583
		EnumKeyword,
		// Token: 0x040011E8 RID: 4584
		DelegateKeyword,
		// Token: 0x040011E9 RID: 4585
		CheckedKeyword,
		// Token: 0x040011EA RID: 4586
		UncheckedKeyword,
		// Token: 0x040011EB RID: 4587
		UnsafeKeyword,
		// Token: 0x040011EC RID: 4588
		OperatorKeyword,
		// Token: 0x040011ED RID: 4589
		ExplicitKeyword,
		// Token: 0x040011EE RID: 4590
		ImplicitKeyword,
		// Token: 0x040011EF RID: 4591
		YieldKeyword = 8405,
		// Token: 0x040011F0 RID: 4592
		PartialKeyword,
		// Token: 0x040011F1 RID: 4593
		AliasKeyword,
		// Token: 0x040011F2 RID: 4594
		GlobalKeyword,
		// Token: 0x040011F3 RID: 4595
		AssemblyKeyword,
		// Token: 0x040011F4 RID: 4596
		ModuleKeyword,
		// Token: 0x040011F5 RID: 4597
		TypeKeyword,
		// Token: 0x040011F6 RID: 4598
		FieldKeyword,
		// Token: 0x040011F7 RID: 4599
		MethodKeyword,
		// Token: 0x040011F8 RID: 4600
		ParamKeyword,
		// Token: 0x040011F9 RID: 4601
		PropertyKeyword,
		// Token: 0x040011FA RID: 4602
		TypeVarKeyword,
		// Token: 0x040011FB RID: 4603
		GetKeyword,
		// Token: 0x040011FC RID: 4604
		SetKeyword,
		// Token: 0x040011FD RID: 4605
		AddKeyword,
		// Token: 0x040011FE RID: 4606
		RemoveKeyword,
		// Token: 0x040011FF RID: 4607
		WhereKeyword,
		// Token: 0x04001200 RID: 4608
		FromKeyword,
		// Token: 0x04001201 RID: 4609
		GroupKeyword,
		// Token: 0x04001202 RID: 4610
		JoinKeyword,
		// Token: 0x04001203 RID: 4611
		IntoKeyword,
		// Token: 0x04001204 RID: 4612
		LetKeyword,
		// Token: 0x04001205 RID: 4613
		ByKeyword,
		// Token: 0x04001206 RID: 4614
		SelectKeyword,
		// Token: 0x04001207 RID: 4615
		OrderByKeyword,
		// Token: 0x04001208 RID: 4616
		OnKeyword,
		// Token: 0x04001209 RID: 4617
		EqualsKeyword,
		// Token: 0x0400120A RID: 4618
		AscendingKeyword,
		// Token: 0x0400120B RID: 4619
		DescendingKeyword,
		// Token: 0x0400120C RID: 4620
		NameOfKeyword,
		// Token: 0x0400120D RID: 4621
		AsyncKeyword,
		// Token: 0x0400120E RID: 4622
		AwaitKeyword,
		// Token: 0x0400120F RID: 4623
		WhenKeyword,
		// Token: 0x04001210 RID: 4624
		OrKeyword,
		// Token: 0x04001211 RID: 4625
		AndKeyword,
		// Token: 0x04001212 RID: 4626
		NotKeyword,
		// Token: 0x04001213 RID: 4627
		DataKeyword,
		// Token: 0x04001214 RID: 4628
		WithKeyword,
		// Token: 0x04001215 RID: 4629
		InitKeyword,
		// Token: 0x04001216 RID: 4630
		RecordKeyword,
		// Token: 0x04001217 RID: 4631
		ManagedKeyword,
		// Token: 0x04001218 RID: 4632
		UnmanagedKeyword,
		// Token: 0x04001219 RID: 4633
		ElifKeyword = 8467,
		// Token: 0x0400121A RID: 4634
		EndIfKeyword,
		// Token: 0x0400121B RID: 4635
		RegionKeyword,
		// Token: 0x0400121C RID: 4636
		EndRegionKeyword,
		// Token: 0x0400121D RID: 4637
		DefineKeyword,
		// Token: 0x0400121E RID: 4638
		UndefKeyword,
		// Token: 0x0400121F RID: 4639
		WarningKeyword,
		// Token: 0x04001220 RID: 4640
		ErrorKeyword,
		// Token: 0x04001221 RID: 4641
		LineKeyword,
		// Token: 0x04001222 RID: 4642
		PragmaKeyword,
		// Token: 0x04001223 RID: 4643
		HiddenKeyword,
		// Token: 0x04001224 RID: 4644
		ChecksumKeyword,
		// Token: 0x04001225 RID: 4645
		DisableKeyword,
		// Token: 0x04001226 RID: 4646
		RestoreKeyword,
		// Token: 0x04001227 RID: 4647
		ReferenceKeyword,
		// Token: 0x04001228 RID: 4648
		InterpolatedStringStartToken,
		// Token: 0x04001229 RID: 4649
		InterpolatedStringEndToken,
		// Token: 0x0400122A RID: 4650
		InterpolatedVerbatimStringStartToken,
		// Token: 0x0400122B RID: 4651
		LoadKeyword,
		// Token: 0x0400122C RID: 4652
		NullableKeyword,
		// Token: 0x0400122D RID: 4653
		EnableKeyword,
		// Token: 0x0400122E RID: 4654
		WarningsKeyword,
		// Token: 0x0400122F RID: 4655
		AnnotationsKeyword,
		// Token: 0x04001230 RID: 4656
		VarKeyword,
		// Token: 0x04001231 RID: 4657
		UnderscoreToken,
		// Token: 0x04001232 RID: 4658
		OmittedTypeArgumentToken,
		// Token: 0x04001233 RID: 4659
		OmittedArraySizeExpressionToken,
		// Token: 0x04001234 RID: 4660
		EndOfDirectiveToken,
		// Token: 0x04001235 RID: 4661
		EndOfDocumentationCommentToken,
		// Token: 0x04001236 RID: 4662
		EndOfFileToken,
		// Token: 0x04001237 RID: 4663
		BadToken = 8507,
		// Token: 0x04001238 RID: 4664
		IdentifierToken,
		// Token: 0x04001239 RID: 4665
		NumericLiteralToken,
		// Token: 0x0400123A RID: 4666
		CharacterLiteralToken,
		// Token: 0x0400123B RID: 4667
		StringLiteralToken,
		// Token: 0x0400123C RID: 4668
		XmlEntityLiteralToken,
		// Token: 0x0400123D RID: 4669
		XmlTextLiteralToken,
		// Token: 0x0400123E RID: 4670
		XmlTextLiteralNewLineToken,
		// Token: 0x0400123F RID: 4671
		InterpolatedStringToken,
		// Token: 0x04001240 RID: 4672
		InterpolatedStringTextToken = 8517,
		// Token: 0x04001241 RID: 4673
		EndOfLineTrivia = 8539,
		// Token: 0x04001242 RID: 4674
		WhitespaceTrivia,
		// Token: 0x04001243 RID: 4675
		SingleLineCommentTrivia,
		// Token: 0x04001244 RID: 4676
		MultiLineCommentTrivia,
		// Token: 0x04001245 RID: 4677
		DocumentationCommentExteriorTrivia,
		// Token: 0x04001246 RID: 4678
		SingleLineDocumentationCommentTrivia,
		// Token: 0x04001247 RID: 4679
		MultiLineDocumentationCommentTrivia,
		// Token: 0x04001248 RID: 4680
		DisabledTextTrivia,
		// Token: 0x04001249 RID: 4681
		PreprocessingMessageTrivia,
		// Token: 0x0400124A RID: 4682
		IfDirectiveTrivia,
		// Token: 0x0400124B RID: 4683
		ElifDirectiveTrivia,
		// Token: 0x0400124C RID: 4684
		ElseDirectiveTrivia,
		// Token: 0x0400124D RID: 4685
		EndIfDirectiveTrivia,
		// Token: 0x0400124E RID: 4686
		RegionDirectiveTrivia,
		// Token: 0x0400124F RID: 4687
		EndRegionDirectiveTrivia,
		// Token: 0x04001250 RID: 4688
		DefineDirectiveTrivia,
		// Token: 0x04001251 RID: 4689
		UndefDirectiveTrivia,
		// Token: 0x04001252 RID: 4690
		ErrorDirectiveTrivia,
		// Token: 0x04001253 RID: 4691
		WarningDirectiveTrivia,
		// Token: 0x04001254 RID: 4692
		LineDirectiveTrivia,
		// Token: 0x04001255 RID: 4693
		PragmaWarningDirectiveTrivia,
		// Token: 0x04001256 RID: 4694
		PragmaChecksumDirectiveTrivia,
		// Token: 0x04001257 RID: 4695
		ReferenceDirectiveTrivia,
		// Token: 0x04001258 RID: 4696
		BadDirectiveTrivia,
		// Token: 0x04001259 RID: 4697
		SkippedTokensTrivia,
		// Token: 0x0400125A RID: 4698
		ConflictMarkerTrivia,
		// Token: 0x0400125B RID: 4699
		XmlElement = 8574,
		// Token: 0x0400125C RID: 4700
		XmlElementStartTag,
		// Token: 0x0400125D RID: 4701
		XmlElementEndTag,
		// Token: 0x0400125E RID: 4702
		XmlEmptyElement,
		// Token: 0x0400125F RID: 4703
		XmlTextAttribute,
		// Token: 0x04001260 RID: 4704
		XmlCrefAttribute,
		// Token: 0x04001261 RID: 4705
		XmlNameAttribute,
		// Token: 0x04001262 RID: 4706
		XmlName,
		// Token: 0x04001263 RID: 4707
		XmlPrefix,
		// Token: 0x04001264 RID: 4708
		XmlText,
		// Token: 0x04001265 RID: 4709
		XmlCDataSection,
		// Token: 0x04001266 RID: 4710
		XmlComment,
		// Token: 0x04001267 RID: 4711
		XmlProcessingInstruction,
		// Token: 0x04001268 RID: 4712
		TypeCref = 8597,
		// Token: 0x04001269 RID: 4713
		QualifiedCref,
		// Token: 0x0400126A RID: 4714
		NameMemberCref,
		// Token: 0x0400126B RID: 4715
		IndexerMemberCref,
		// Token: 0x0400126C RID: 4716
		OperatorMemberCref,
		// Token: 0x0400126D RID: 4717
		ConversionOperatorMemberCref,
		// Token: 0x0400126E RID: 4718
		CrefParameterList,
		// Token: 0x0400126F RID: 4719
		CrefBracketedParameterList,
		// Token: 0x04001270 RID: 4720
		CrefParameter,
		// Token: 0x04001271 RID: 4721
		IdentifierName = 8616,
		// Token: 0x04001272 RID: 4722
		QualifiedName,
		// Token: 0x04001273 RID: 4723
		GenericName,
		// Token: 0x04001274 RID: 4724
		TypeArgumentList,
		// Token: 0x04001275 RID: 4725
		AliasQualifiedName,
		// Token: 0x04001276 RID: 4726
		PredefinedType,
		// Token: 0x04001277 RID: 4727
		ArrayType,
		// Token: 0x04001278 RID: 4728
		ArrayRankSpecifier,
		// Token: 0x04001279 RID: 4729
		PointerType,
		// Token: 0x0400127A RID: 4730
		NullableType,
		// Token: 0x0400127B RID: 4731
		OmittedTypeArgument,
		// Token: 0x0400127C RID: 4732
		ParenthesizedExpression = 8632,
		// Token: 0x0400127D RID: 4733
		ConditionalExpression,
		// Token: 0x0400127E RID: 4734
		InvocationExpression,
		// Token: 0x0400127F RID: 4735
		ElementAccessExpression,
		// Token: 0x04001280 RID: 4736
		ArgumentList,
		// Token: 0x04001281 RID: 4737
		BracketedArgumentList,
		// Token: 0x04001282 RID: 4738
		Argument,
		// Token: 0x04001283 RID: 4739
		NameColon,
		// Token: 0x04001284 RID: 4740
		CastExpression,
		// Token: 0x04001285 RID: 4741
		AnonymousMethodExpression,
		// Token: 0x04001286 RID: 4742
		SimpleLambdaExpression,
		// Token: 0x04001287 RID: 4743
		ParenthesizedLambdaExpression,
		// Token: 0x04001288 RID: 4744
		ObjectInitializerExpression,
		// Token: 0x04001289 RID: 4745
		CollectionInitializerExpression,
		// Token: 0x0400128A RID: 4746
		ArrayInitializerExpression,
		// Token: 0x0400128B RID: 4747
		AnonymousObjectMemberDeclarator,
		// Token: 0x0400128C RID: 4748
		ComplexElementInitializerExpression,
		// Token: 0x0400128D RID: 4749
		ObjectCreationExpression,
		// Token: 0x0400128E RID: 4750
		AnonymousObjectCreationExpression,
		// Token: 0x0400128F RID: 4751
		ArrayCreationExpression,
		// Token: 0x04001290 RID: 4752
		ImplicitArrayCreationExpression,
		// Token: 0x04001291 RID: 4753
		StackAllocArrayCreationExpression,
		// Token: 0x04001292 RID: 4754
		OmittedArraySizeExpression,
		// Token: 0x04001293 RID: 4755
		InterpolatedStringExpression,
		// Token: 0x04001294 RID: 4756
		ImplicitElementAccess,
		// Token: 0x04001295 RID: 4757
		IsPatternExpression,
		// Token: 0x04001296 RID: 4758
		RangeExpression,
		// Token: 0x04001297 RID: 4759
		ImplicitObjectCreationExpression,
		// Token: 0x04001298 RID: 4760
		AddExpression = 8668,
		// Token: 0x04001299 RID: 4761
		SubtractExpression,
		// Token: 0x0400129A RID: 4762
		MultiplyExpression,
		// Token: 0x0400129B RID: 4763
		DivideExpression,
		// Token: 0x0400129C RID: 4764
		ModuloExpression,
		// Token: 0x0400129D RID: 4765
		LeftShiftExpression,
		// Token: 0x0400129E RID: 4766
		RightShiftExpression,
		// Token: 0x0400129F RID: 4767
		LogicalOrExpression,
		// Token: 0x040012A0 RID: 4768
		LogicalAndExpression,
		// Token: 0x040012A1 RID: 4769
		BitwiseOrExpression,
		// Token: 0x040012A2 RID: 4770
		BitwiseAndExpression,
		// Token: 0x040012A3 RID: 4771
		ExclusiveOrExpression,
		// Token: 0x040012A4 RID: 4772
		EqualsExpression,
		// Token: 0x040012A5 RID: 4773
		NotEqualsExpression,
		// Token: 0x040012A6 RID: 4774
		LessThanExpression,
		// Token: 0x040012A7 RID: 4775
		LessThanOrEqualExpression,
		// Token: 0x040012A8 RID: 4776
		GreaterThanExpression,
		// Token: 0x040012A9 RID: 4777
		GreaterThanOrEqualExpression,
		// Token: 0x040012AA RID: 4778
		IsExpression,
		// Token: 0x040012AB RID: 4779
		AsExpression,
		// Token: 0x040012AC RID: 4780
		CoalesceExpression,
		// Token: 0x040012AD RID: 4781
		SimpleMemberAccessExpression,
		// Token: 0x040012AE RID: 4782
		PointerMemberAccessExpression,
		// Token: 0x040012AF RID: 4783
		ConditionalAccessExpression,
		// Token: 0x040012B0 RID: 4784
		MemberBindingExpression = 8707,
		// Token: 0x040012B1 RID: 4785
		ElementBindingExpression,
		// Token: 0x040012B2 RID: 4786
		SimpleAssignmentExpression = 8714,
		// Token: 0x040012B3 RID: 4787
		AddAssignmentExpression,
		// Token: 0x040012B4 RID: 4788
		SubtractAssignmentExpression,
		// Token: 0x040012B5 RID: 4789
		MultiplyAssignmentExpression,
		// Token: 0x040012B6 RID: 4790
		DivideAssignmentExpression,
		// Token: 0x040012B7 RID: 4791
		ModuloAssignmentExpression,
		// Token: 0x040012B8 RID: 4792
		AndAssignmentExpression,
		// Token: 0x040012B9 RID: 4793
		ExclusiveOrAssignmentExpression,
		// Token: 0x040012BA RID: 4794
		OrAssignmentExpression,
		// Token: 0x040012BB RID: 4795
		LeftShiftAssignmentExpression,
		// Token: 0x040012BC RID: 4796
		RightShiftAssignmentExpression,
		// Token: 0x040012BD RID: 4797
		CoalesceAssignmentExpression,
		// Token: 0x040012BE RID: 4798
		UnaryPlusExpression = 8730,
		// Token: 0x040012BF RID: 4799
		UnaryMinusExpression,
		// Token: 0x040012C0 RID: 4800
		BitwiseNotExpression,
		// Token: 0x040012C1 RID: 4801
		LogicalNotExpression,
		// Token: 0x040012C2 RID: 4802
		PreIncrementExpression,
		// Token: 0x040012C3 RID: 4803
		PreDecrementExpression,
		// Token: 0x040012C4 RID: 4804
		PointerIndirectionExpression,
		// Token: 0x040012C5 RID: 4805
		AddressOfExpression,
		// Token: 0x040012C6 RID: 4806
		PostIncrementExpression,
		// Token: 0x040012C7 RID: 4807
		PostDecrementExpression,
		// Token: 0x040012C8 RID: 4808
		AwaitExpression,
		// Token: 0x040012C9 RID: 4809
		IndexExpression,
		// Token: 0x040012CA RID: 4810
		ThisExpression = 8746,
		// Token: 0x040012CB RID: 4811
		BaseExpression,
		// Token: 0x040012CC RID: 4812
		ArgListExpression,
		// Token: 0x040012CD RID: 4813
		NumericLiteralExpression,
		// Token: 0x040012CE RID: 4814
		StringLiteralExpression,
		// Token: 0x040012CF RID: 4815
		CharacterLiteralExpression,
		// Token: 0x040012D0 RID: 4816
		TrueLiteralExpression,
		// Token: 0x040012D1 RID: 4817
		FalseLiteralExpression,
		// Token: 0x040012D2 RID: 4818
		NullLiteralExpression,
		// Token: 0x040012D3 RID: 4819
		DefaultLiteralExpression,
		// Token: 0x040012D4 RID: 4820
		TypeOfExpression = 8760,
		// Token: 0x040012D5 RID: 4821
		SizeOfExpression,
		// Token: 0x040012D6 RID: 4822
		CheckedExpression,
		// Token: 0x040012D7 RID: 4823
		UncheckedExpression,
		// Token: 0x040012D8 RID: 4824
		DefaultExpression,
		// Token: 0x040012D9 RID: 4825
		MakeRefExpression,
		// Token: 0x040012DA RID: 4826
		RefValueExpression,
		// Token: 0x040012DB RID: 4827
		RefTypeExpression,
		// Token: 0x040012DC RID: 4828
		QueryExpression = 8774,
		// Token: 0x040012DD RID: 4829
		QueryBody,
		// Token: 0x040012DE RID: 4830
		FromClause,
		// Token: 0x040012DF RID: 4831
		LetClause,
		// Token: 0x040012E0 RID: 4832
		JoinClause,
		// Token: 0x040012E1 RID: 4833
		JoinIntoClause,
		// Token: 0x040012E2 RID: 4834
		WhereClause,
		// Token: 0x040012E3 RID: 4835
		OrderByClause,
		// Token: 0x040012E4 RID: 4836
		AscendingOrdering,
		// Token: 0x040012E5 RID: 4837
		DescendingOrdering,
		// Token: 0x040012E6 RID: 4838
		SelectClause,
		// Token: 0x040012E7 RID: 4839
		GroupClause,
		// Token: 0x040012E8 RID: 4840
		QueryContinuation,
		// Token: 0x040012E9 RID: 4841
		Block = 8792,
		// Token: 0x040012EA RID: 4842
		LocalDeclarationStatement,
		// Token: 0x040012EB RID: 4843
		VariableDeclaration,
		// Token: 0x040012EC RID: 4844
		VariableDeclarator,
		// Token: 0x040012ED RID: 4845
		EqualsValueClause,
		// Token: 0x040012EE RID: 4846
		ExpressionStatement,
		// Token: 0x040012EF RID: 4847
		EmptyStatement,
		// Token: 0x040012F0 RID: 4848
		LabeledStatement,
		// Token: 0x040012F1 RID: 4849
		GotoStatement,
		// Token: 0x040012F2 RID: 4850
		GotoCaseStatement,
		// Token: 0x040012F3 RID: 4851
		GotoDefaultStatement,
		// Token: 0x040012F4 RID: 4852
		BreakStatement,
		// Token: 0x040012F5 RID: 4853
		ContinueStatement,
		// Token: 0x040012F6 RID: 4854
		ReturnStatement,
		// Token: 0x040012F7 RID: 4855
		YieldReturnStatement,
		// Token: 0x040012F8 RID: 4856
		YieldBreakStatement,
		// Token: 0x040012F9 RID: 4857
		ThrowStatement,
		// Token: 0x040012FA RID: 4858
		WhileStatement,
		// Token: 0x040012FB RID: 4859
		DoStatement,
		// Token: 0x040012FC RID: 4860
		ForStatement,
		// Token: 0x040012FD RID: 4861
		ForEachStatement,
		// Token: 0x040012FE RID: 4862
		UsingStatement,
		// Token: 0x040012FF RID: 4863
		FixedStatement,
		// Token: 0x04001300 RID: 4864
		CheckedStatement,
		// Token: 0x04001301 RID: 4865
		UncheckedStatement,
		// Token: 0x04001302 RID: 4866
		UnsafeStatement,
		// Token: 0x04001303 RID: 4867
		LockStatement,
		// Token: 0x04001304 RID: 4868
		IfStatement,
		// Token: 0x04001305 RID: 4869
		ElseClause,
		// Token: 0x04001306 RID: 4870
		SwitchStatement,
		// Token: 0x04001307 RID: 4871
		SwitchSection,
		// Token: 0x04001308 RID: 4872
		CaseSwitchLabel,
		// Token: 0x04001309 RID: 4873
		DefaultSwitchLabel,
		// Token: 0x0400130A RID: 4874
		TryStatement,
		// Token: 0x0400130B RID: 4875
		CatchClause,
		// Token: 0x0400130C RID: 4876
		CatchDeclaration,
		// Token: 0x0400130D RID: 4877
		CatchFilterClause,
		// Token: 0x0400130E RID: 4878
		FinallyClause,
		// Token: 0x0400130F RID: 4879
		LocalFunctionStatement,
		// Token: 0x04001310 RID: 4880
		CompilationUnit = 8840,
		// Token: 0x04001311 RID: 4881
		GlobalStatement,
		// Token: 0x04001312 RID: 4882
		NamespaceDeclaration,
		// Token: 0x04001313 RID: 4883
		UsingDirective,
		// Token: 0x04001314 RID: 4884
		ExternAliasDirective,
		// Token: 0x04001315 RID: 4885
		AttributeList = 8847,
		// Token: 0x04001316 RID: 4886
		AttributeTargetSpecifier,
		// Token: 0x04001317 RID: 4887
		Attribute,
		// Token: 0x04001318 RID: 4888
		AttributeArgumentList,
		// Token: 0x04001319 RID: 4889
		AttributeArgument,
		// Token: 0x0400131A RID: 4890
		NameEquals,
		// Token: 0x0400131B RID: 4891
		ClassDeclaration = 8855,
		// Token: 0x0400131C RID: 4892
		StructDeclaration,
		// Token: 0x0400131D RID: 4893
		InterfaceDeclaration,
		// Token: 0x0400131E RID: 4894
		EnumDeclaration,
		// Token: 0x0400131F RID: 4895
		DelegateDeclaration,
		// Token: 0x04001320 RID: 4896
		BaseList = 8864,
		// Token: 0x04001321 RID: 4897
		SimpleBaseType,
		// Token: 0x04001322 RID: 4898
		TypeParameterConstraintClause,
		// Token: 0x04001323 RID: 4899
		ConstructorConstraint,
		// Token: 0x04001324 RID: 4900
		ClassConstraint,
		// Token: 0x04001325 RID: 4901
		StructConstraint,
		// Token: 0x04001326 RID: 4902
		TypeConstraint,
		// Token: 0x04001327 RID: 4903
		ExplicitInterfaceSpecifier,
		// Token: 0x04001328 RID: 4904
		EnumMemberDeclaration,
		// Token: 0x04001329 RID: 4905
		FieldDeclaration,
		// Token: 0x0400132A RID: 4906
		EventFieldDeclaration,
		// Token: 0x0400132B RID: 4907
		MethodDeclaration,
		// Token: 0x0400132C RID: 4908
		OperatorDeclaration,
		// Token: 0x0400132D RID: 4909
		ConversionOperatorDeclaration,
		// Token: 0x0400132E RID: 4910
		ConstructorDeclaration,
		// Token: 0x0400132F RID: 4911
		BaseConstructorInitializer = 8889,
		// Token: 0x04001330 RID: 4912
		ThisConstructorInitializer,
		// Token: 0x04001331 RID: 4913
		DestructorDeclaration,
		// Token: 0x04001332 RID: 4914
		PropertyDeclaration,
		// Token: 0x04001333 RID: 4915
		EventDeclaration,
		// Token: 0x04001334 RID: 4916
		IndexerDeclaration,
		// Token: 0x04001335 RID: 4917
		AccessorList,
		// Token: 0x04001336 RID: 4918
		GetAccessorDeclaration,
		// Token: 0x04001337 RID: 4919
		SetAccessorDeclaration,
		// Token: 0x04001338 RID: 4920
		AddAccessorDeclaration,
		// Token: 0x04001339 RID: 4921
		RemoveAccessorDeclaration,
		// Token: 0x0400133A RID: 4922
		UnknownAccessorDeclaration,
		// Token: 0x0400133B RID: 4923
		ParameterList = 8906,
		// Token: 0x0400133C RID: 4924
		BracketedParameterList,
		// Token: 0x0400133D RID: 4925
		Parameter,
		// Token: 0x0400133E RID: 4926
		TypeParameterList,
		// Token: 0x0400133F RID: 4927
		TypeParameter,
		// Token: 0x04001340 RID: 4928
		IncompleteMember = 8916,
		// Token: 0x04001341 RID: 4929
		ArrowExpressionClause,
		// Token: 0x04001342 RID: 4930
		Interpolation,
		// Token: 0x04001343 RID: 4931
		InterpolatedStringText,
		// Token: 0x04001344 RID: 4932
		InterpolationAlignmentClause,
		// Token: 0x04001345 RID: 4933
		InterpolationFormatClause,
		// Token: 0x04001346 RID: 4934
		ShebangDirectiveTrivia,
		// Token: 0x04001347 RID: 4935
		LoadDirectiveTrivia,
		// Token: 0x04001348 RID: 4936
		TupleType,
		// Token: 0x04001349 RID: 4937
		TupleElement,
		// Token: 0x0400134A RID: 4938
		TupleExpression,
		// Token: 0x0400134B RID: 4939
		SingleVariableDesignation,
		// Token: 0x0400134C RID: 4940
		ParenthesizedVariableDesignation,
		// Token: 0x0400134D RID: 4941
		ForEachVariableStatement,
		// Token: 0x0400134E RID: 4942
		DeclarationPattern = 9000,
		// Token: 0x0400134F RID: 4943
		ConstantPattern = 9002,
		// Token: 0x04001350 RID: 4944
		CasePatternSwitchLabel = 9009,
		// Token: 0x04001351 RID: 4945
		WhenClause = 9013,
		// Token: 0x04001352 RID: 4946
		DiscardDesignation,
		// Token: 0x04001353 RID: 4947
		RecursivePattern = 9020,
		// Token: 0x04001354 RID: 4948
		PropertyPatternClause,
		// Token: 0x04001355 RID: 4949
		Subpattern,
		// Token: 0x04001356 RID: 4950
		PositionalPatternClause,
		// Token: 0x04001357 RID: 4951
		DiscardPattern,
		// Token: 0x04001358 RID: 4952
		SwitchExpression,
		// Token: 0x04001359 RID: 4953
		SwitchExpressionArm,
		// Token: 0x0400135A RID: 4954
		VarPattern,
		// Token: 0x0400135B RID: 4955
		ParenthesizedPattern,
		// Token: 0x0400135C RID: 4956
		RelationalPattern,
		// Token: 0x0400135D RID: 4957
		TypePattern,
		// Token: 0x0400135E RID: 4958
		OrPattern,
		// Token: 0x0400135F RID: 4959
		AndPattern,
		// Token: 0x04001360 RID: 4960
		NotPattern,
		// Token: 0x04001361 RID: 4961
		DeclarationExpression = 9040,
		// Token: 0x04001362 RID: 4962
		RefExpression = 9050,
		// Token: 0x04001363 RID: 4963
		RefType,
		// Token: 0x04001364 RID: 4964
		ThrowExpression,
		// Token: 0x04001365 RID: 4965
		ImplicitStackAllocArrayCreationExpression,
		// Token: 0x04001366 RID: 4966
		SuppressNullableWarningExpression,
		// Token: 0x04001367 RID: 4967
		NullableDirectiveTrivia,
		// Token: 0x04001368 RID: 4968
		FunctionPointerType,
		// Token: 0x04001369 RID: 4969
		FunctionPointerParameter,
		// Token: 0x0400136A RID: 4970
		FunctionPointerParameterList,
		// Token: 0x0400136B RID: 4971
		FunctionPointerCallingConvention,
		// Token: 0x0400136C RID: 4972
		InitAccessorDeclaration,
		// Token: 0x0400136D RID: 4973
		WithExpression,
		// Token: 0x0400136E RID: 4974
		WithInitializerExpression,
		// Token: 0x0400136F RID: 4975
		RecordDeclaration,
		// Token: 0x04001370 RID: 4976
		DefaultConstraint,
		// Token: 0x04001371 RID: 4977
		PrimaryConstructorBaseType,
		// Token: 0x04001372 RID: 4978
		FunctionPointerUnmanagedCallingConventionList,
		// Token: 0x04001373 RID: 4979
		FunctionPointerUnmanagedCallingConvention,
		// Token: 0x04001374 RID: 4980
		RecordStructDeclaration
	}
}
