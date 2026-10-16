using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000013 RID: 19
	public abstract class SubsystemDescriptorWithProvider : ISubsystemDescriptor
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002C2F File Offset: 0x00000E2F
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002C37 File Offset: 0x00000E37
		public string id { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002C40 File Offset: 0x00000E40
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002C48 File Offset: 0x00000E48
		protected internal Type providerType { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C51 File Offset: 0x00000E51
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002C59 File Offset: 0x00000E59
		protected internal Type subsystemTypeOverride { get; set; }

		// Token: 0x06000068 RID: 104
		internal abstract ISubsystem CreateImpl();

		// Token: 0x06000069 RID: 105 RVA: 0x00002C62 File Offset: 0x00000E62
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x0600006A RID: 106
		internal abstract void ThrowIfInvalid();
	}
}
