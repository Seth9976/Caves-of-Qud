using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006A3 RID: 1699
	internal struct LayoutConfigData
	{
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000BBD08 File Offset: 0x000B9F08
		public static LayoutConfigData Default
		{
			get
			{
				return new LayoutConfigData
				{
					PointScaleFactor = 1f,
					ShouldLog = false
				};
			}
		}

		// Token: 0x0400185C RID: 6236
		public float PointScaleFactor;

		// Token: 0x0400185D RID: 6237
		[MarshalAs(UnmanagedType.U1)]
		public bool ShouldLog;
	}
}
