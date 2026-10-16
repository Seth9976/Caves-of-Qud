using System;
using EnhancedUI.EnhancedScroller;
using Qud.UI;
using XRL.UI;

// Token: 0x02000092 RID: 146
public class ModCellView : EnhancedScrollerCellView
{
	// Token: 0x0600050D RID: 1293 RVA: 0x00021419 File Offset: 0x0001F619
	public void SetData(ModScrollerData data)
	{
		this.data = data;
		this.modPath.SetText(data.info.ID);
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00021439 File Offset: 0x0001F639
	public void OnSelected()
	{
		SingletonWindowBase<SteamWorkshopUploaderView>.instance.SetModInfo(this.data.info);
	}

	// Token: 0x040003E5 RID: 997
	public UITextSkin modPath;

	// Token: 0x040003E6 RID: 998
	public ModScrollerData data;
}
