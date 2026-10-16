using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000021 RID: 33
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	internal class NativeThrowsAttribute : Attribute, IBindingsThrowsProviderAttribute, IBindingsAttribute
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000253A File Offset: 0x0000073A
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002542 File Offset: 0x00000742
		public bool ThrowsException { get; set; }

		// Token: 0x06000067 RID: 103 RVA: 0x0000254B File Offset: 0x0000074B
		public NativeThrowsAttribute()
		{
			this.ThrowsException = true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000255D File Offset: 0x0000075D
		public NativeThrowsAttribute(bool throwsException)
		{
			this.ThrowsException = throwsException;
		}
	}
}
