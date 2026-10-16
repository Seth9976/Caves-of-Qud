using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x0200010E RID: 270
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public sealed class InputControlAttribute : PropertyAttribute
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0004AD43 File Offset: 0x00048F43
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0004AD4B File Offset: 0x00048F4B
		public string layout { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0004AD54 File Offset: 0x00048F54
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0004AD5C File Offset: 0x00048F5C
		public string variants { get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0004AD65 File Offset: 0x00048F65
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0004AD6D File Offset: 0x00048F6D
		public string name { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0004AD76 File Offset: 0x00048F76
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x0004AD7E File Offset: 0x00048F7E
		public string format { get; set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0004AD87 File Offset: 0x00048F87
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0004AD8F File Offset: 0x00048F8F
		public string usage { get; set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0004AD98 File Offset: 0x00048F98
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0004ADA0 File Offset: 0x00048FA0
		public string[] usages { get; set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0004ADA9 File Offset: 0x00048FA9
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0004ADB1 File Offset: 0x00048FB1
		public string parameters { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0004ADBA File Offset: 0x00048FBA
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x0004ADC2 File Offset: 0x00048FC2
		public string processors { get; set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0004ADCB File Offset: 0x00048FCB
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x0004ADD3 File Offset: 0x00048FD3
		public string alias { get; set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0004ADDC File Offset: 0x00048FDC
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x0004ADE4 File Offset: 0x00048FE4
		public string[] aliases { get; set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0004ADED File Offset: 0x00048FED
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0004ADF5 File Offset: 0x00048FF5
		public string useStateFrom { get; set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0004ADFE File Offset: 0x00048FFE
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0004AE06 File Offset: 0x00049006
		public uint bit { get; set; } = uint.MaxValue;

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0004AE0F File Offset: 0x0004900F
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0004AE17 File Offset: 0x00049017
		public uint offset { get; set; } = uint.MaxValue;

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0004AE20 File Offset: 0x00049020
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0004AE28 File Offset: 0x00049028
		public uint sizeInBits { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0004AE31 File Offset: 0x00049031
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0004AE39 File Offset: 0x00049039
		public int arraySize { get; set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0004AE42 File Offset: 0x00049042
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0004AE4A File Offset: 0x0004904A
		public string displayName { get; set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0004AE53 File Offset: 0x00049053
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x0004AE5B File Offset: 0x0004905B
		public string shortDisplayName { get; set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0004AE64 File Offset: 0x00049064
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x0004AE6C File Offset: 0x0004906C
		public bool noisy { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0004AE75 File Offset: 0x00049075
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x0004AE7D File Offset: 0x0004907D
		public bool synthetic { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0004AE86 File Offset: 0x00049086
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x0004AE8E File Offset: 0x0004908E
		public bool dontReset { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0004AE97 File Offset: 0x00049097
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0004AE9F File Offset: 0x0004909F
		public object defaultState { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0004AEA8 File Offset: 0x000490A8
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0004AEB0 File Offset: 0x000490B0
		public object minValue { get; set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0004AEB9 File Offset: 0x000490B9
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0004AEC1 File Offset: 0x000490C1
		public object maxValue { get; set; }
	}
}
