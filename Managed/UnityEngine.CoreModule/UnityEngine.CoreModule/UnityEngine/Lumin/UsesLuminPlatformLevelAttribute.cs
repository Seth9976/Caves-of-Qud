using System;

namespace UnityEngine.Lumin
{
	// Token: 0x02000439 RID: 1081
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[Obsolete("Lumin is no longer supported in Unity 2022.2")]
	public sealed class UsesLuminPlatformLevelAttribute : Attribute
	{
		// Token: 0x060029EB RID: 10731 RVA: 0x00050E92 File Offset: 0x0004F092
		public UsesLuminPlatformLevelAttribute(uint platformLevel)
		{
			this.m_PlatformLevel = platformLevel;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x00050EA4 File Offset: 0x0004F0A4
		public uint platformLevel
		{
			get
			{
				return this.m_PlatformLevel;
			}
		}

		// Token: 0x04000BF3 RID: 3059
		private readonly uint m_PlatformLevel;
	}
}
