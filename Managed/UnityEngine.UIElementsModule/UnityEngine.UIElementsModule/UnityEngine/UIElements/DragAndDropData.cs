using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F2 RID: 498
	public abstract class DragAndDropData : IDragAndDropData
	{
		// Token: 0x06000F5E RID: 3934
		public abstract object GetGenericData(string key);

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0004336F File Offset: 0x0004156F
		object IDragAndDropData.userData
		{
			get
			{
				return this.GetGenericData("__unity-drag-and-drop__source-view");
			}
		}

		// Token: 0x06000F60 RID: 3936
		public abstract void SetGenericData(string key, object data);

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000F61 RID: 3937
		public abstract object source { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000F62 RID: 3938
		public abstract DragVisualMode visualMode { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000F63 RID: 3939
		public abstract IEnumerable<Object> unityObjectReferences { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x00043CFB File Offset: 0x00041EFB
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00043D03 File Offset: 0x00041F03
		public virtual string[] paths { get; set; }

		// Token: 0x04000955 RID: 2389
		internal const string dragSourceKey = "__unity-drag-and-drop__source-view";
	}
}
