using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000245 RID: 581
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ResourceRequest : AsyncOperation
	{
		// Token: 0x06001EC4 RID: 7876 RVA: 0x0003B31C File Offset: 0x0003951C
		protected virtual Object GetResult()
		{
			return Resources.Load(this.m_Path, this.m_Type);
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0003B340 File Offset: 0x00039540
		public Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0003B358 File Offset: 0x00039558
		public ResourceRequest()
		{
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0003B362 File Offset: 0x00039562
		protected ResourceRequest(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x0400091C RID: 2332
		internal string m_Path;

		// Token: 0x0400091D RID: 2333
		internal Type m_Type;

		// Token: 0x02000246 RID: 582
		internal new static class BindingsMarshaller
		{
			// Token: 0x06001EC8 RID: 7880 RVA: 0x0003B36D File Offset: 0x0003956D
			public static ResourceRequest ConvertToManaged(IntPtr ptr)
			{
				return new ResourceRequest(ptr);
			}
		}
	}
}
