using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FB RID: 507
	public struct StartDragArgs
	{
		// Token: 0x06000F8B RID: 3979 RVA: 0x00043E25 File Offset: 0x00042025
		public StartDragArgs(string title, DragVisualMode visualMode)
		{
			this.title = title;
			this.visualMode = visualMode;
			this.genericData = null;
			this.assetPaths = null;
			this.unityObjectReferences = null;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00043E4E File Offset: 0x0004204E
		internal StartDragArgs(string title, object target)
		{
			this.title = title;
			this.visualMode = DragVisualMode.Move;
			this.genericData = null;
			this.assetPaths = null;
			this.unityObjectReferences = null;
			this.SetGenericData("__unity-drag-and-drop__source-view", target);
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00043E84 File Offset: 0x00042084
		public readonly string title { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00043E8C File Offset: 0x0004208C
		public readonly DragVisualMode visualMode { get; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00043E94 File Offset: 0x00042094
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00043E9C File Offset: 0x0004209C
		internal Hashtable genericData { readonly get; private set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00043EA5 File Offset: 0x000420A5
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00043EAD File Offset: 0x000420AD
		internal IEnumerable<Object> unityObjectReferences { readonly get; private set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00043EB6 File Offset: 0x000420B6
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00043EBE File Offset: 0x000420BE
		internal string[] assetPaths { readonly get; private set; }

		// Token: 0x06000F95 RID: 3989 RVA: 0x00043EC8 File Offset: 0x000420C8
		public void SetGenericData(string key, object data)
		{
			if (this.genericData == null)
			{
				this.genericData = new Hashtable();
			}
			this.genericData[key] = data;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00043EFA File Offset: 0x000420FA
		public void SetUnityObjectReferences(IEnumerable<Object> references)
		{
			this.unityObjectReferences = references;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00043F05 File Offset: 0x00042105
		public void SetPaths(string[] paths)
		{
			this.assetPaths = paths;
		}
	}
}
