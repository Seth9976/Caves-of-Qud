using System;

namespace System.IO
{
	// Token: 0x0200051F RID: 1311
	[Flags]
	internal enum FilterFlags : uint
	{
		// Token: 0x040016B8 RID: 5816
		ReadPoll = 4096U,
		// Token: 0x040016B9 RID: 5817
		ReadOutOfBand = 8192U,
		// Token: 0x040016BA RID: 5818
		ReadLowWaterMark = 1U,
		// Token: 0x040016BB RID: 5819
		WriteLowWaterMark = 1U,
		// Token: 0x040016BC RID: 5820
		NoteTrigger = 16777216U,
		// Token: 0x040016BD RID: 5821
		NoteFFNop = 0U,
		// Token: 0x040016BE RID: 5822
		NoteFFAnd = 1073741824U,
		// Token: 0x040016BF RID: 5823
		NoteFFOr = 2147483648U,
		// Token: 0x040016C0 RID: 5824
		NoteFFCopy = 3221225472U,
		// Token: 0x040016C1 RID: 5825
		NoteFFCtrlMask = 3221225472U,
		// Token: 0x040016C2 RID: 5826
		NoteFFlagsMask = 16777215U,
		// Token: 0x040016C3 RID: 5827
		VNodeDelete = 1U,
		// Token: 0x040016C4 RID: 5828
		VNodeWrite = 2U,
		// Token: 0x040016C5 RID: 5829
		VNodeExtend = 4U,
		// Token: 0x040016C6 RID: 5830
		VNodeAttrib = 8U,
		// Token: 0x040016C7 RID: 5831
		VNodeLink = 16U,
		// Token: 0x040016C8 RID: 5832
		VNodeRename = 32U,
		// Token: 0x040016C9 RID: 5833
		VNodeRevoke = 64U,
		// Token: 0x040016CA RID: 5834
		VNodeNone = 128U,
		// Token: 0x040016CB RID: 5835
		ProcExit = 2147483648U,
		// Token: 0x040016CC RID: 5836
		ProcFork = 1073741824U,
		// Token: 0x040016CD RID: 5837
		ProcExec = 536870912U,
		// Token: 0x040016CE RID: 5838
		ProcReap = 268435456U,
		// Token: 0x040016CF RID: 5839
		ProcSignal = 134217728U,
		// Token: 0x040016D0 RID: 5840
		ProcExitStatus = 67108864U,
		// Token: 0x040016D1 RID: 5841
		ProcResourceEnd = 33554432U,
		// Token: 0x040016D2 RID: 5842
		ProcAppactive = 8388608U,
		// Token: 0x040016D3 RID: 5843
		ProcAppBackground = 4194304U,
		// Token: 0x040016D4 RID: 5844
		ProcAppNonUI = 2097152U,
		// Token: 0x040016D5 RID: 5845
		ProcAppInactive = 1048576U,
		// Token: 0x040016D6 RID: 5846
		ProcAppAllStates = 15728640U,
		// Token: 0x040016D7 RID: 5847
		ProcPDataMask = 1048575U,
		// Token: 0x040016D8 RID: 5848
		ProcControlMask = 4293918720U,
		// Token: 0x040016D9 RID: 5849
		VMPressure = 2147483648U,
		// Token: 0x040016DA RID: 5850
		VMPressureTerminate = 1073741824U,
		// Token: 0x040016DB RID: 5851
		VMPressureSuddenTerminate = 536870912U,
		// Token: 0x040016DC RID: 5852
		VMError = 268435456U,
		// Token: 0x040016DD RID: 5853
		TimerSeconds = 1U,
		// Token: 0x040016DE RID: 5854
		TimerMicroSeconds = 2U,
		// Token: 0x040016DF RID: 5855
		TimerNanoSeconds = 4U,
		// Token: 0x040016E0 RID: 5856
		TimerAbsolute = 8U
	}
}
