using System;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000111 RID: 273
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class InputControlLayoutAttribute : Attribute
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0004C1DF File Offset: 0x0004A3DF
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0004C1E7 File Offset: 0x0004A3E7
		public Type stateType { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0004C1F0 File Offset: 0x0004A3F0
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x0004C1F8 File Offset: 0x0004A3F8
		public string stateFormat { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0004C201 File Offset: 0x0004A401
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0004C209 File Offset: 0x0004A409
		public string[] commonUsages { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0004C212 File Offset: 0x0004A412
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0004C21A File Offset: 0x0004A41A
		public string variants { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0004C223 File Offset: 0x0004A423
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0004C22B File Offset: 0x0004A42B
		public bool isNoisy { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0004C234 File Offset: 0x0004A434
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x0004C241 File Offset: 0x0004A441
		public bool canRunInBackground
		{
			get
			{
				return this.canRunInBackgroundInternal.Value;
			}
			set
			{
				this.canRunInBackgroundInternal = new bool?(value);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0004C24F File Offset: 0x0004A44F
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x0004C25C File Offset: 0x0004A45C
		public bool updateBeforeRender
		{
			get
			{
				return this.updateBeforeRenderInternal.Value;
			}
			set
			{
				this.updateBeforeRenderInternal = new bool?(value);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0004C26A File Offset: 0x0004A46A
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x0004C272 File Offset: 0x0004A472
		public bool isGenericTypeOfDevice { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0004C27B File Offset: 0x0004A47B
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0004C283 File Offset: 0x0004A483
		public string displayName { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0004C28C File Offset: 0x0004A48C
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0004C294 File Offset: 0x0004A494
		public string description { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0004C29D File Offset: 0x0004A49D
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0004C2A5 File Offset: 0x0004A4A5
		public bool hideInUI { get; set; }

		// Token: 0x04000696 RID: 1686
		internal bool? canRunInBackgroundInternal;

		// Token: 0x04000697 RID: 1687
		internal bool? updateBeforeRenderInternal;
	}
}
