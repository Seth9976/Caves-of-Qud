using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200015E RID: 350
	internal enum BoundKind : byte
	{
		// Token: 0x04000E6A RID: 3690
		FieldEqualsValue,
		// Token: 0x04000E6B RID: 3691
		PropertyEqualsValue,
		// Token: 0x04000E6C RID: 3692
		ParameterEqualsValue,
		// Token: 0x04000E6D RID: 3693
		GlobalStatementInitializer,
		// Token: 0x04000E6E RID: 3694
		DeconstructValuePlaceholder,
		// Token: 0x04000E6F RID: 3695
		TupleOperandPlaceholder,
		// Token: 0x04000E70 RID: 3696
		AwaitableValuePlaceholder,
		// Token: 0x04000E71 RID: 3697
		DisposableValuePlaceholder,
		// Token: 0x04000E72 RID: 3698
		ObjectOrCollectionValuePlaceholder,
		// Token: 0x04000E73 RID: 3699
		Dup,
		// Token: 0x04000E74 RID: 3700
		PassByCopy,
		// Token: 0x04000E75 RID: 3701
		BadExpression,
		// Token: 0x04000E76 RID: 3702
		BadStatement,
		// Token: 0x04000E77 RID: 3703
		ExtractedFinallyBlock,
		// Token: 0x04000E78 RID: 3704
		TypeExpression,
		// Token: 0x04000E79 RID: 3705
		TypeOrValueExpression,
		// Token: 0x04000E7A RID: 3706
		NamespaceExpression,
		// Token: 0x04000E7B RID: 3707
		UnaryOperator,
		// Token: 0x04000E7C RID: 3708
		IncrementOperator,
		// Token: 0x04000E7D RID: 3709
		AddressOfOperator,
		// Token: 0x04000E7E RID: 3710
		UnconvertedAddressOfOperator,
		// Token: 0x04000E7F RID: 3711
		FunctionPointerLoad,
		// Token: 0x04000E80 RID: 3712
		PointerIndirectionOperator,
		// Token: 0x04000E81 RID: 3713
		PointerElementAccess,
		// Token: 0x04000E82 RID: 3714
		FunctionPointerInvocation,
		// Token: 0x04000E83 RID: 3715
		RefTypeOperator,
		// Token: 0x04000E84 RID: 3716
		MakeRefOperator,
		// Token: 0x04000E85 RID: 3717
		RefValueOperator,
		// Token: 0x04000E86 RID: 3718
		FromEndIndexExpression,
		// Token: 0x04000E87 RID: 3719
		RangeExpression,
		// Token: 0x04000E88 RID: 3720
		BinaryOperator,
		// Token: 0x04000E89 RID: 3721
		TupleBinaryOperator,
		// Token: 0x04000E8A RID: 3722
		UserDefinedConditionalLogicalOperator,
		// Token: 0x04000E8B RID: 3723
		CompoundAssignmentOperator,
		// Token: 0x04000E8C RID: 3724
		AssignmentOperator,
		// Token: 0x04000E8D RID: 3725
		DeconstructionAssignmentOperator,
		// Token: 0x04000E8E RID: 3726
		NullCoalescingOperator,
		// Token: 0x04000E8F RID: 3727
		NullCoalescingAssignmentOperator,
		// Token: 0x04000E90 RID: 3728
		UnconvertedConditionalOperator,
		// Token: 0x04000E91 RID: 3729
		ConditionalOperator,
		// Token: 0x04000E92 RID: 3730
		ArrayAccess,
		// Token: 0x04000E93 RID: 3731
		ArrayLength,
		// Token: 0x04000E94 RID: 3732
		AwaitableInfo,
		// Token: 0x04000E95 RID: 3733
		AwaitExpression,
		// Token: 0x04000E96 RID: 3734
		TypeOfOperator,
		// Token: 0x04000E97 RID: 3735
		MethodDefIndex,
		// Token: 0x04000E98 RID: 3736
		MaximumMethodDefIndex,
		// Token: 0x04000E99 RID: 3737
		InstrumentationPayloadRoot,
		// Token: 0x04000E9A RID: 3738
		ModuleVersionId,
		// Token: 0x04000E9B RID: 3739
		ModuleVersionIdString,
		// Token: 0x04000E9C RID: 3740
		SourceDocumentIndex,
		// Token: 0x04000E9D RID: 3741
		MethodInfo,
		// Token: 0x04000E9E RID: 3742
		FieldInfo,
		// Token: 0x04000E9F RID: 3743
		DefaultLiteral,
		// Token: 0x04000EA0 RID: 3744
		DefaultExpression,
		// Token: 0x04000EA1 RID: 3745
		IsOperator,
		// Token: 0x04000EA2 RID: 3746
		AsOperator,
		// Token: 0x04000EA3 RID: 3747
		SizeOfOperator,
		// Token: 0x04000EA4 RID: 3748
		Conversion,
		// Token: 0x04000EA5 RID: 3749
		ReadOnlySpanFromArray,
		// Token: 0x04000EA6 RID: 3750
		ArgList,
		// Token: 0x04000EA7 RID: 3751
		ArgListOperator,
		// Token: 0x04000EA8 RID: 3752
		FixedLocalCollectionInitializer,
		// Token: 0x04000EA9 RID: 3753
		SequencePoint,
		// Token: 0x04000EAA RID: 3754
		SequencePointWithSpan,
		// Token: 0x04000EAB RID: 3755
		SavePreviousSequencePoint,
		// Token: 0x04000EAC RID: 3756
		RestorePreviousSequencePoint,
		// Token: 0x04000EAD RID: 3757
		StepThroughSequencePoint,
		// Token: 0x04000EAE RID: 3758
		Block,
		// Token: 0x04000EAF RID: 3759
		Scope,
		// Token: 0x04000EB0 RID: 3760
		StateMachineScope,
		// Token: 0x04000EB1 RID: 3761
		LocalDeclaration,
		// Token: 0x04000EB2 RID: 3762
		MultipleLocalDeclarations,
		// Token: 0x04000EB3 RID: 3763
		UsingLocalDeclarations,
		// Token: 0x04000EB4 RID: 3764
		LocalFunctionStatement,
		// Token: 0x04000EB5 RID: 3765
		NoOpStatement,
		// Token: 0x04000EB6 RID: 3766
		ReturnStatement,
		// Token: 0x04000EB7 RID: 3767
		YieldReturnStatement,
		// Token: 0x04000EB8 RID: 3768
		YieldBreakStatement,
		// Token: 0x04000EB9 RID: 3769
		ThrowStatement,
		// Token: 0x04000EBA RID: 3770
		ExpressionStatement,
		// Token: 0x04000EBB RID: 3771
		BreakStatement,
		// Token: 0x04000EBC RID: 3772
		ContinueStatement,
		// Token: 0x04000EBD RID: 3773
		SwitchStatement,
		// Token: 0x04000EBE RID: 3774
		SwitchDispatch,
		// Token: 0x04000EBF RID: 3775
		IfStatement,
		// Token: 0x04000EC0 RID: 3776
		DoStatement,
		// Token: 0x04000EC1 RID: 3777
		WhileStatement,
		// Token: 0x04000EC2 RID: 3778
		ForStatement,
		// Token: 0x04000EC3 RID: 3779
		ForEachStatement,
		// Token: 0x04000EC4 RID: 3780
		ForEachDeconstructStep,
		// Token: 0x04000EC5 RID: 3781
		UsingStatement,
		// Token: 0x04000EC6 RID: 3782
		FixedStatement,
		// Token: 0x04000EC7 RID: 3783
		LockStatement,
		// Token: 0x04000EC8 RID: 3784
		TryStatement,
		// Token: 0x04000EC9 RID: 3785
		CatchBlock,
		// Token: 0x04000ECA RID: 3786
		Literal,
		// Token: 0x04000ECB RID: 3787
		ThisReference,
		// Token: 0x04000ECC RID: 3788
		PreviousSubmissionReference,
		// Token: 0x04000ECD RID: 3789
		HostObjectMemberReference,
		// Token: 0x04000ECE RID: 3790
		BaseReference,
		// Token: 0x04000ECF RID: 3791
		Local,
		// Token: 0x04000ED0 RID: 3792
		PseudoVariable,
		// Token: 0x04000ED1 RID: 3793
		RangeVariable,
		// Token: 0x04000ED2 RID: 3794
		Parameter,
		// Token: 0x04000ED3 RID: 3795
		LabelStatement,
		// Token: 0x04000ED4 RID: 3796
		GotoStatement,
		// Token: 0x04000ED5 RID: 3797
		LabeledStatement,
		// Token: 0x04000ED6 RID: 3798
		Label,
		// Token: 0x04000ED7 RID: 3799
		StatementList,
		// Token: 0x04000ED8 RID: 3800
		ConditionalGoto,
		// Token: 0x04000ED9 RID: 3801
		SwitchExpressionArm,
		// Token: 0x04000EDA RID: 3802
		UnconvertedSwitchExpression,
		// Token: 0x04000EDB RID: 3803
		ConvertedSwitchExpression,
		// Token: 0x04000EDC RID: 3804
		DecisionDag,
		// Token: 0x04000EDD RID: 3805
		EvaluationDecisionDagNode,
		// Token: 0x04000EDE RID: 3806
		TestDecisionDagNode,
		// Token: 0x04000EDF RID: 3807
		WhenDecisionDagNode,
		// Token: 0x04000EE0 RID: 3808
		LeafDecisionDagNode,
		// Token: 0x04000EE1 RID: 3809
		DagTemp,
		// Token: 0x04000EE2 RID: 3810
		DagTypeTest,
		// Token: 0x04000EE3 RID: 3811
		DagNonNullTest,
		// Token: 0x04000EE4 RID: 3812
		DagExplicitNullTest,
		// Token: 0x04000EE5 RID: 3813
		DagValueTest,
		// Token: 0x04000EE6 RID: 3814
		DagRelationalTest,
		// Token: 0x04000EE7 RID: 3815
		DagDeconstructEvaluation,
		// Token: 0x04000EE8 RID: 3816
		DagTypeEvaluation,
		// Token: 0x04000EE9 RID: 3817
		DagFieldEvaluation,
		// Token: 0x04000EEA RID: 3818
		DagPropertyEvaluation,
		// Token: 0x04000EEB RID: 3819
		DagIndexEvaluation,
		// Token: 0x04000EEC RID: 3820
		SwitchSection,
		// Token: 0x04000EED RID: 3821
		SwitchLabel,
		// Token: 0x04000EEE RID: 3822
		SequencePointExpression,
		// Token: 0x04000EEF RID: 3823
		Sequence,
		// Token: 0x04000EF0 RID: 3824
		SpillSequence,
		// Token: 0x04000EF1 RID: 3825
		DynamicMemberAccess,
		// Token: 0x04000EF2 RID: 3826
		DynamicInvocation,
		// Token: 0x04000EF3 RID: 3827
		ConditionalAccess,
		// Token: 0x04000EF4 RID: 3828
		LoweredConditionalAccess,
		// Token: 0x04000EF5 RID: 3829
		ConditionalReceiver,
		// Token: 0x04000EF6 RID: 3830
		ComplexConditionalReceiver,
		// Token: 0x04000EF7 RID: 3831
		MethodGroup,
		// Token: 0x04000EF8 RID: 3832
		PropertyGroup,
		// Token: 0x04000EF9 RID: 3833
		Call,
		// Token: 0x04000EFA RID: 3834
		EventAssignmentOperator,
		// Token: 0x04000EFB RID: 3835
		Attribute,
		// Token: 0x04000EFC RID: 3836
		UnconvertedObjectCreationExpression,
		// Token: 0x04000EFD RID: 3837
		ObjectCreationExpression,
		// Token: 0x04000EFE RID: 3838
		TupleLiteral,
		// Token: 0x04000EFF RID: 3839
		ConvertedTupleLiteral,
		// Token: 0x04000F00 RID: 3840
		DynamicObjectCreationExpression,
		// Token: 0x04000F01 RID: 3841
		NoPiaObjectCreationExpression,
		// Token: 0x04000F02 RID: 3842
		ObjectInitializerExpression,
		// Token: 0x04000F03 RID: 3843
		ObjectInitializerMember,
		// Token: 0x04000F04 RID: 3844
		DynamicObjectInitializerMember,
		// Token: 0x04000F05 RID: 3845
		CollectionInitializerExpression,
		// Token: 0x04000F06 RID: 3846
		CollectionElementInitializer,
		// Token: 0x04000F07 RID: 3847
		DynamicCollectionElementInitializer,
		// Token: 0x04000F08 RID: 3848
		ImplicitReceiver,
		// Token: 0x04000F09 RID: 3849
		AnonymousObjectCreationExpression,
		// Token: 0x04000F0A RID: 3850
		AnonymousPropertyDeclaration,
		// Token: 0x04000F0B RID: 3851
		NewT,
		// Token: 0x04000F0C RID: 3852
		DelegateCreationExpression,
		// Token: 0x04000F0D RID: 3853
		ArrayCreation,
		// Token: 0x04000F0E RID: 3854
		ArrayInitialization,
		// Token: 0x04000F0F RID: 3855
		StackAllocArrayCreation,
		// Token: 0x04000F10 RID: 3856
		ConvertedStackAllocExpression,
		// Token: 0x04000F11 RID: 3857
		FieldAccess,
		// Token: 0x04000F12 RID: 3858
		HoistedFieldAccess,
		// Token: 0x04000F13 RID: 3859
		PropertyAccess,
		// Token: 0x04000F14 RID: 3860
		EventAccess,
		// Token: 0x04000F15 RID: 3861
		IndexerAccess,
		// Token: 0x04000F16 RID: 3862
		IndexOrRangePatternIndexerAccess,
		// Token: 0x04000F17 RID: 3863
		DynamicIndexerAccess,
		// Token: 0x04000F18 RID: 3864
		Lambda,
		// Token: 0x04000F19 RID: 3865
		UnboundLambda,
		// Token: 0x04000F1A RID: 3866
		QueryClause,
		// Token: 0x04000F1B RID: 3867
		TypeOrInstanceInitializers,
		// Token: 0x04000F1C RID: 3868
		NameOfOperator,
		// Token: 0x04000F1D RID: 3869
		UnconvertedInterpolatedString,
		// Token: 0x04000F1E RID: 3870
		InterpolatedString,
		// Token: 0x04000F1F RID: 3871
		StringInsert,
		// Token: 0x04000F20 RID: 3872
		IsPatternExpression,
		// Token: 0x04000F21 RID: 3873
		ConstantPattern,
		// Token: 0x04000F22 RID: 3874
		DiscardPattern,
		// Token: 0x04000F23 RID: 3875
		DeclarationPattern,
		// Token: 0x04000F24 RID: 3876
		RecursivePattern,
		// Token: 0x04000F25 RID: 3877
		ITuplePattern,
		// Token: 0x04000F26 RID: 3878
		Subpattern,
		// Token: 0x04000F27 RID: 3879
		TypePattern,
		// Token: 0x04000F28 RID: 3880
		BinaryPattern,
		// Token: 0x04000F29 RID: 3881
		NegatedPattern,
		// Token: 0x04000F2A RID: 3882
		RelationalPattern,
		// Token: 0x04000F2B RID: 3883
		DiscardExpression,
		// Token: 0x04000F2C RID: 3884
		ThrowExpression,
		// Token: 0x04000F2D RID: 3885
		OutVariablePendingInference,
		// Token: 0x04000F2E RID: 3886
		DeconstructionVariablePendingInference,
		// Token: 0x04000F2F RID: 3887
		OutDeconstructVarPendingInference,
		// Token: 0x04000F30 RID: 3888
		NonConstructorMethodBody,
		// Token: 0x04000F31 RID: 3889
		ConstructorMethodBody,
		// Token: 0x04000F32 RID: 3890
		ExpressionWithNullability,
		// Token: 0x04000F33 RID: 3891
		WithExpression
	}
}
