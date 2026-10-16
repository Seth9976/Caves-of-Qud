using System;
using Overlay.MapEditor;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000494 RID: 1172
	[UIView("MapEditor", true, false, false, "Menu", "MapEditor", false, 0, false, UICanvasHost = 1)]
	public class MapEditorWindow : LegacyViewWindow<MapEditorView>
	{
		// Token: 0x06002CFC RID: 11516 RVA: 0x000F2A93 File Offset: 0x000F0C93
		public override void Show()
		{
			base.Show();
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000F2AA7 File Offset: 0x000F0CA7
		public override void Hide()
		{
			base.Hide();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000F2ABB File Offset: 0x000F0CBB
		public void OnUpdateFilterUsed()
		{
			this.baseView.FilterUsedUpdated();
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000F2AC8 File Offset: 0x000F0CC8
		public void OnCommand(string command)
		{
			this.baseView.OnCommand(command, 0);
		}

		// Token: 0x04001920 RID: 6432
		public bool setup;
	}
}
