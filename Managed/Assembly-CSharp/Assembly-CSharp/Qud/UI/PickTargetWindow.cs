using System;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x020004A6 RID: 1190
	[UIView("PickTargetFrame", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "PickTarget", UICanvasHost = 1)]
	public class PickTargetWindow : SingletonWindowBase<PickTargetWindow>
	{
		// Token: 0x06002D9A RID: 11674 RVA: 0x000F939F File Offset: 0x000F759F
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000F93A2 File Offset: 0x000F75A2
		public void Update()
		{
			if (this.text.text != PickTargetWindow.currentText)
			{
				this.text.SetText(PickTargetWindow.currentText);
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000F93CC File Offset: 0x000F75CC
		public override void Show()
		{
			base.Show();
			if (Options.ModernUI)
			{
				UIManager.getWindow("Stage").Show();
				UIManager.getWindow("PlayerStatusBar").Show();
				UIManager.getWindow("AbilityBar").Show();
				UIManager.getWindow("MessageLog").Hide();
				UIManager.getWindow("NearbyItems").Hide();
				UIManager.getWindow("Minimap").Hide();
			}
			this.Update();
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000F9448 File Offset: 0x000F7648
		public override void Hide()
		{
			base.Hide();
			UIManager.getWindow("Stage").Hide();
			UIManager.getWindow("PlayerStatusBar").Hide();
			UIManager.getWindow("AbilityBar").Hide();
			UIManager.getWindow("MessageLog").Hide();
			UIManager.getWindow("NearbyItems").Hide();
			UIManager.getWindow("Minimap").Hide();
		}

		// Token: 0x040019D7 RID: 6615
		public UITextSkin text;

		// Token: 0x040019D8 RID: 6616
		public static string currentText;

		// Token: 0x040019D9 RID: 6617
		public static PickTargetWindow.TargetMode currentMode;

		// Token: 0x0200190F RID: 6415
		public enum TargetMode
		{
			// Token: 0x0400649D RID: 25757
			PickDirection,
			// Token: 0x0400649E RID: 25758
			PickField,
			// Token: 0x0400649F RID: 25759
			PickCells
		}
	}
}
