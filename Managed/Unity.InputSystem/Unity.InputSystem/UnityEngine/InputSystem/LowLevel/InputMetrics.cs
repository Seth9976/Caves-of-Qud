using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public struct InputMetrics
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00047C75 File Offset: 0x00045E75
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x00047C7D File Offset: 0x00045E7D
		public int maxNumDevices { readonly get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00047C86 File Offset: 0x00045E86
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x00047C8E File Offset: 0x00045E8E
		public int currentNumDevices { readonly get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00047C97 File Offset: 0x00045E97
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x00047C9F File Offset: 0x00045E9F
		public int maxStateSizeInBytes { readonly get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00047CA8 File Offset: 0x00045EA8
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00047CB0 File Offset: 0x00045EB0
		public int currentStateSizeInBytes { readonly get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00047CB9 File Offset: 0x00045EB9
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x00047CC1 File Offset: 0x00045EC1
		public int currentControlCount { readonly get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00047CCA File Offset: 0x00045ECA
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x00047CD2 File Offset: 0x00045ED2
		public int currentLayoutCount { readonly get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00047CDB File Offset: 0x00045EDB
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x00047CE3 File Offset: 0x00045EE3
		public int totalEventBytes { readonly get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00047CEC File Offset: 0x00045EEC
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x00047CF4 File Offset: 0x00045EF4
		public int totalEventCount { readonly get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00047CFD File Offset: 0x00045EFD
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x00047D05 File Offset: 0x00045F05
		public int totalUpdateCount { readonly get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00047D0E File Offset: 0x00045F0E
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x00047D16 File Offset: 0x00045F16
		public double totalEventProcessingTime { readonly get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00047D1F File Offset: 0x00045F1F
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x00047D27 File Offset: 0x00045F27
		public double totalEventLagTime { readonly get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00047D30 File Offset: 0x00045F30
		public float averageEventBytesPerFrame
		{
			get
			{
				return (float)this.totalEventBytes / (float)this.totalUpdateCount;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00047D41 File Offset: 0x00045F41
		public double averageProcessingTimePerEvent
		{
			get
			{
				return this.totalEventProcessingTime / (double)this.totalEventCount;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00047D51 File Offset: 0x00045F51
		public double averageLagTimePerEvent
		{
			get
			{
				return this.totalEventLagTime / (double)this.totalEventCount;
			}
		}
	}
}
