using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativePropertyAttribute : NativeMethodAttribute
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002364 File Offset: 0x00000564
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000236C File Offset: 0x0000056C
		public TargetType TargetType { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002375 File Offset: 0x00000575
		public NativePropertyAttribute()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000237F File Offset: 0x0000057F
		public NativePropertyAttribute(string name)
			: base(name)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000238A File Offset: 0x0000058A
		public NativePropertyAttribute(string name, TargetType targetType)
			: base(name)
		{
			this.TargetType = targetType;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000239D File Offset: 0x0000059D
		public NativePropertyAttribute(string name, bool isFree, TargetType targetType)
			: base(name, isFree)
		{
			this.TargetType = targetType;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000023B1 File Offset: 0x000005B1
		public NativePropertyAttribute(string name, bool isFree, TargetType targetType, bool isThreadSafe)
			: base(name, isFree, isThreadSafe)
		{
			this.TargetType = targetType;
		}
	}
}
