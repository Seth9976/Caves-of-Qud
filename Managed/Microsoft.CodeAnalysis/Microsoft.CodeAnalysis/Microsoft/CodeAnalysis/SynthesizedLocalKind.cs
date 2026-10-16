using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200028B RID: 651
	internal enum SynthesizedLocalKind
	{
		// Token: 0x04000A4B RID: 2635
		FrameCache = -5,
		// Token: 0x04000A4C RID: 2636
		OptimizerTemp = -3,
		// Token: 0x04000A4D RID: 2637
		LoweringTemp,
		// Token: 0x04000A4E RID: 2638
		EmitterTemp,
		// Token: 0x04000A4F RID: 2639
		UserDefined,
		// Token: 0x04000A50 RID: 2640
		ConditionalBranchDiscriminator,
		// Token: 0x04000A51 RID: 2641
		LockTaken,
		// Token: 0x04000A52 RID: 2642
		Lock,
		// Token: 0x04000A53 RID: 2643
		Using,
		// Token: 0x04000A54 RID: 2644
		ForEachEnumerator,
		// Token: 0x04000A55 RID: 2645
		ForEachArray,
		// Token: 0x04000A56 RID: 2646
		ForEachArrayLimit,
		// Token: 0x04000A57 RID: 2647
		ForEachArrayIndex,
		// Token: 0x04000A58 RID: 2648
		FixedReference,
		// Token: 0x04000A59 RID: 2649
		With,
		// Token: 0x04000A5A RID: 2650
		ForLimit,
		// Token: 0x04000A5B RID: 2651
		ForStep,
		// Token: 0x04000A5C RID: 2652
		ForInitialValue,
		// Token: 0x04000A5D RID: 2653
		ForDirection,
		// Token: 0x04000A5E RID: 2654
		SelectCaseValue,
		// Token: 0x04000A5F RID: 2655
		OnErrorActiveHandler,
		// Token: 0x04000A60 RID: 2656
		OnErrorResumeTarget,
		// Token: 0x04000A61 RID: 2657
		OnErrorCurrentStatement,
		// Token: 0x04000A62 RID: 2658
		OnErrorCurrentLine,
		// Token: 0x04000A63 RID: 2659
		AsyncMethodReturnValue,
		// Token: 0x04000A64 RID: 2660
		StateMachineReturnValue = 20,
		// Token: 0x04000A65 RID: 2661
		FunctionReturnValue,
		// Token: 0x04000A66 RID: 2662
		TryAwaitPendingException,
		// Token: 0x04000A67 RID: 2663
		TryAwaitPendingBranch,
		// Token: 0x04000A68 RID: 2664
		TryAwaitPendingCatch,
		// Token: 0x04000A69 RID: 2665
		TryAwaitPendingCaughtException,
		// Token: 0x04000A6A RID: 2666
		ExceptionFilterAwaitHoistedExceptionLocal,
		// Token: 0x04000A6B RID: 2667
		StateMachineCachedState,
		// Token: 0x04000A6C RID: 2668
		Spill,
		// Token: 0x04000A6D RID: 2669
		AwaitByRefSpill,
		// Token: 0x04000A6E RID: 2670
		LambdaDisplayClass,
		// Token: 0x04000A6F RID: 2671
		CachedAnonymousMethodDelegate,
		// Token: 0x04000A70 RID: 2672
		XmlInExpressionLambda,
		// Token: 0x04000A71 RID: 2673
		Awaiter,
		// Token: 0x04000A72 RID: 2674
		InstrumentationPayload,
		// Token: 0x04000A73 RID: 2675
		SwitchCasePatternMatching,
		// Token: 0x04000A74 RID: 2676
		MaxValidValueForLocalVariableSerializedToDebugInformation = 125,
		// Token: 0x04000A75 RID: 2677
		AwaiterField = 256,
		// Token: 0x04000A76 RID: 2678
		DelegateRelaxationReceiver
	}
}
