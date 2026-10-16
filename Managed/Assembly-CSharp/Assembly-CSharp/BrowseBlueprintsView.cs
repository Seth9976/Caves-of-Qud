using System;
using Qud.UI;
using XRL.UI;

// Token: 0x0200008C RID: 140
[UIView("BrowseBlueprintsView", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "BrowseBlueprintsView", UICanvasHost = 1)]
public class BrowseBlueprintsView : SingletonWindowBase<BrowseBlueprintsView>
{
	// Token: 0x060004F4 RID: 1268 RVA: 0x00020E41 File Offset: 0x0001F041
	public override void Show()
	{
		this.mode = "old";
		base.Show();
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00020E54 File Offset: 0x0001F054
	public override void Hide()
	{
		this.mode == "new";
		base.Hide();
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x00020E6D File Offset: 0x0001F06D
	public void OnCommand(string Command)
	{
		if (Command == "Back")
		{
			UIManager.getWindow("BrowseBlueprintsView").Hide();
			UIManager.showWindow("MainMenu", false);
		}
	}

	// Token: 0x040003D7 RID: 983
	public string mode = "old";
}
