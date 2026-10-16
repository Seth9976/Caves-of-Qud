using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000263 RID: 611
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[UsedByNativeCode]
	public class HelpURLAttribute : Attribute
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x0003C4C3 File Offset: 0x0003A6C3
		public HelpURLAttribute(string url)
		{
			this.m_Url = url;
			this.m_DispatchingFieldName = "";
			this.m_Dispatcher = false;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0003C4E6 File Offset: 0x0003A6E6
		internal HelpURLAttribute(string defaultURL, string dispatchingFieldName)
		{
			this.m_Url = defaultURL;
			this.m_DispatchingFieldName = dispatchingFieldName;
			this.m_Dispatcher = !string.IsNullOrEmpty(dispatchingFieldName);
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0003C50D File Offset: 0x0003A70D
		public virtual string URL
		{
			get
			{
				return this.m_Url;
			}
		}

		// Token: 0x04000942 RID: 2370
		internal readonly string m_Url;

		// Token: 0x04000943 RID: 2371
		internal readonly bool m_Dispatcher;

		// Token: 0x04000944 RID: 2372
		internal readonly string m_DispatchingFieldName;
	}
}
