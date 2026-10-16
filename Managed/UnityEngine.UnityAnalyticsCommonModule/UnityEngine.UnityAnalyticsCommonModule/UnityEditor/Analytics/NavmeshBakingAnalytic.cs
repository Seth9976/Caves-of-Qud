using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000019 RID: 25
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class NavmeshBakingAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000233F File Offset: 0x0000053F
		public NavmeshBakingAnalytic()
			: base("navigation_navmesh_baking", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002358 File Offset: 0x00000558
		[RequiredByNativeCode]
		internal static NavmeshBakingAnalytic CreateNavmeshBakingAnalytic()
		{
			return new NavmeshBakingAnalytic();
		}

		// Token: 0x04000043 RID: 67
		private bool new_nav_api;

		// Token: 0x04000044 RID: 68
		private bool bake_at_runtime;

		// Token: 0x04000045 RID: 69
		private int height_meshes_count;

		// Token: 0x04000046 RID: 70
		private int offmesh_links_count;
	}
}
