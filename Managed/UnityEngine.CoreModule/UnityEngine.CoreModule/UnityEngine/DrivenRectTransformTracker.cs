using System;

namespace UnityEngine
{
	// Token: 0x020002F4 RID: 756
	public struct DrivenRectTransformTracker
	{
		// Token: 0x060024F7 RID: 9463 RVA: 0x000476E4 File Offset: 0x000458E4
		internal static bool CanRecordModifications()
		{
			return true;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00002925 File Offset: 0x00000B25
		public void Add(Object driver, RectTransform rectTransform, DrivenTransformProperties drivenProperties)
		{
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000476F7 File Offset: 0x000458F7
		[Obsolete("revertValues parameter is ignored. Please use Clear() instead.")]
		public void Clear(bool revertValues)
		{
			this.Clear();
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00002925 File Offset: 0x00000B25
		public void Clear()
		{
		}
	}
}
