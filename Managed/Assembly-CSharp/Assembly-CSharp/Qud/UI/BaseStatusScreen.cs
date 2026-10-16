using System;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000459 RID: 1113
	[RequireComponent(typeof(LayoutElement))]
	public class BaseStatusScreen<T> : SingletonWindowBase<T>, IStatusScreen where T : class, new()
	{
		// Token: 0x06002A24 RID: 10788 RVA: 0x000DD9D9 File Offset: 0x000DBBD9
		public virtual bool Exit()
		{
			return true;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000DD9DC File Offset: 0x000DBBDC
		public virtual string GetNavigationCategory()
		{
			return "StatusScreens";
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000DD9E3 File Offset: 0x000DBBE3
		public virtual bool WantsCategoryBar()
		{
			return false;
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000DD9E6 File Offset: 0x000DBBE6
		public virtual IRenderable GetTabIcon()
		{
			return null;
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000DD9E9 File Offset: 0x000DBBE9
		public virtual string GetTabString()
		{
			return "<override GetTabString>";
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000DD9F0 File Offset: 0x000DBBF0
		public virtual void HideScreen()
		{
			this.Hide();
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000DD9F8 File Offset: 0x000DBBF8
		public virtual void FilterUpdated(string filterText)
		{
			this.filterText = filterText;
			this.UpdateViewFromData();
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x000DDA07 File Offset: 0x000DBC07
		public virtual void UpdateViewFromData()
		{
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x000DDA09 File Offset: 0x000DBC09
		public virtual void HandleMenuOption(FrameworkDataElement data)
		{
			MetricsManager.LogWarning("Handling menu option in base. " + base.gameObject.name);
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000DDA25 File Offset: 0x000DBC25
		public virtual NavigationContext ShowScreen(XRL.World.GameObject GO, StatusScreensScreen parent)
		{
			this.Show();
			return null;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x000DDA2E File Offset: 0x000DBC2E
		public LayoutGroup layoutGroup
		{
			get
			{
				if (this._layoutGroup == null)
				{
					this._layoutGroup = base.GetComponent<LayoutGroup>();
				}
				return this._layoutGroup;
			}
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000DDA50 File Offset: 0x000DBC50
		public virtual void PrepareLayoutFrame()
		{
			this.LayoutFramesReset = true;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000DDA59 File Offset: 0x000DBC59
		public virtual void ResetLayoutFrame(int frameAmount = 3)
		{
			this.layoutGroup.enabled = true;
			this.lastStageScale = Options.StageScale;
			this.lastWidth = Screen.width;
			if (this.layoutGroupFrame < frameAmount)
			{
				this.layoutGroupFrame = frameAmount;
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x000DDA8D File Offset: 0x000DBC8D
		public virtual void CheckLayoutFrame()
		{
			if (!base.canvas.enabled)
			{
				return;
			}
			if (this.LayoutFramesReset || this.lastWidth != Screen.width || this.lastStageScale != Options.StageScale)
			{
				this.ResetLayoutFrame(3);
			}
			this.LayoutFramesReset = false;
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000DDAD0 File Offset: 0x000DBCD0
		public virtual void LateUpdate()
		{
			if (!base.canvas.enabled)
			{
				return;
			}
			if (this.layoutGroupFrame > 0)
			{
				this.layoutGroupFrame--;
				if (this.layoutGroupFrame <= 0)
				{
					this.layoutGroup.enabled = false;
				}
			}
			this.CheckLayoutFrame();
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000DDB1D File Offset: 0x000DBD1D
		public virtual void Prewarm()
		{
		}

		// Token: 0x0400160F RID: 5647
		protected string filterText;

		// Token: 0x04001610 RID: 5648
		public LayoutGroup _layoutGroup;

		// Token: 0x04001611 RID: 5649
		public int layoutGroupFrame;

		// Token: 0x04001612 RID: 5650
		public int lastWidth = int.MinValue;

		// Token: 0x04001613 RID: 5651
		public double lastStageScale;

		// Token: 0x04001614 RID: 5652
		public bool LayoutFramesReset;
	}
}
