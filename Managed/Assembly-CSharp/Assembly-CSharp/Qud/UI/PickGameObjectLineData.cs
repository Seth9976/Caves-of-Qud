using System;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000456 RID: 1110
	public class PickGameObjectLineData : PooledFrameworkDataElement<PickGameObjectLineData>
	{
		// Token: 0x060029F4 RID: 10740 RVA: 0x000DC35B File Offset: 0x000DA55B
		public PickGameObjectLineData set(PickGameObjectLineDataType type = PickGameObjectLineDataType.Item, PickGameObjectLineDataStyle style = PickGameObjectLineDataStyle.Interact, GameObject go = null, string category = null, bool collapsed = false, bool indent = false, char quickKey = '\0', string hotkeyDescription = null)
		{
			this.type = type;
			this.style = style;
			this.go = go;
			this.category = category;
			this.collapsed = collapsed;
			this.indent = indent;
			this.quickKey = quickKey;
			this.hotkeyDescription = hotkeyDescription;
			return this;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000DC39B File Offset: 0x000DA59B
		public override void free()
		{
			this.go = null;
			base.free();
		}

		// Token: 0x040015DC RID: 5596
		public PickGameObjectLineDataType type;

		// Token: 0x040015DD RID: 5597
		public PickGameObjectLineDataStyle style;

		// Token: 0x040015DE RID: 5598
		public GameObject go;

		// Token: 0x040015DF RID: 5599
		public string category;

		// Token: 0x040015E0 RID: 5600
		public bool collapsed;

		// Token: 0x040015E1 RID: 5601
		public bool indent;

		// Token: 0x040015E2 RID: 5602
		public char quickKey;

		// Token: 0x040015E3 RID: 5603
		public string hotkeyDescription;
	}
}
