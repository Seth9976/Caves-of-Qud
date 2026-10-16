using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DC RID: 476
	internal class CursorManager : ICursorManager
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00040C7B File Offset: 0x0003EE7B
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x00040C83 File Offset: 0x0003EE83
		public bool isCursorOverriden { get; private set; }

		// Token: 0x06000E8A RID: 3722 RVA: 0x00040C8C File Offset: 0x0003EE8C
		public void SetCursor(Cursor cursor)
		{
			bool flag = cursor.texture != null;
			if (flag)
			{
				Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
				this.isCursorOverriden = true;
			}
			else
			{
				bool flag2 = cursor.defaultCursorId != 0;
				if (flag2)
				{
					Debug.LogWarning("Runtime cursors other than the default cursor need to be defined using a texture.");
				}
				this.ResetCursor();
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00040CF0 File Offset: 0x0003EEF0
		public void ResetCursor()
		{
			bool isCursorOverriden = this.isCursorOverriden;
			if (isCursorOverriden)
			{
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			}
			this.isCursorOverriden = false;
		}
	}
}
