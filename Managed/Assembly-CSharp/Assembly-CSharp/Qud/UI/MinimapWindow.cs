using System;
using UnityEngine;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000495 RID: 1173
	[ExecuteAlways]
	[HasGameBasedStaticCache]
	[UIView("Minimap", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "Minimap", UICanvasHost = 1)]
	public class MinimapWindow : MovableSceneFrameWindowBase<MinimapWindow>
	{
		// Token: 0x06002D01 RID: 11521 RVA: 0x000F2AE0 File Offset: 0x000F0CE0
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000F2AE3 File Offset: 0x000F0CE3
		public void TogglePreferredState()
		{
			base.Toggle();
			this.SaveOptions();
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000F2AF1 File Offset: 0x000F0CF1
		public void SaveOptions()
		{
			Options.SetOption("OptionOverlayMinimap", base.Visible);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000F2B03 File Offset: 0x000F0D03
		public void ShowIfEnabled()
		{
			if (GameManager.Instance.DisplayMinimap)
			{
				this.Show();
				return;
			}
			this.Hide();
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000F2B1E File Offset: 0x000F0D1E
		public override void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (GameManager.Instance.DockMovable > 0 && GameManager.MainCameraLetterbox)
			{
				GameManager.MainCameraLetterbox.GetScale();
			}
			else
			{
				this.Docked = false;
			}
			base.Update();
		}
	}
}
