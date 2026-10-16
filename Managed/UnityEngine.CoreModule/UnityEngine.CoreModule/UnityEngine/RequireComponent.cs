using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200025B RID: 603
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[RequiredByNativeCode]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x06001F54 RID: 8020 RVA: 0x0003C3AA File Offset: 0x0003A5AA
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0003C3BB File Offset: 0x0003A5BB
		public RequireComponent(Type requiredComponent, Type requiredComponent2)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0003C3D3 File Offset: 0x0003A5D3
		public RequireComponent(Type requiredComponent, Type requiredComponent2, Type requiredComponent3)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
			this.m_Type2 = requiredComponent3;
		}

		// Token: 0x04000937 RID: 2359
		public Type m_Type0;

		// Token: 0x04000938 RID: 2360
		public Type m_Type1;

		// Token: 0x04000939 RID: 2361
		public Type m_Type2;
	}
}
