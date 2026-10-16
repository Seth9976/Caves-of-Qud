using System;
using System.Diagnostics;

namespace UnityEngine
{
	// Token: 0x02000244 RID: 580
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public class IconAttribute : Attribute
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0003B303 File Offset: 0x00039503
		public string path
		{
			get
			{
				return this.m_IconPath;
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00002082 File Offset: 0x00000282
		private IconAttribute()
		{
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0003B30B File Offset: 0x0003950B
		public IconAttribute(string path)
		{
			this.m_IconPath = path;
		}

		// Token: 0x0400091B RID: 2331
		private string m_IconPath;
	}
}
