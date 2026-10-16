using System;
using Kobold;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200042E RID: 1070
	public class AchievementViewRow : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x000D22AC File Offset: 0x000D04AC
		public FrameworkContext FrameworkContext
		{
			get
			{
				FrameworkContext frameworkContext;
				if ((frameworkContext = this._FrameworkContext) == null)
				{
					frameworkContext = (this._FrameworkContext = base.GetComponent<FrameworkContext>());
				}
				return frameworkContext;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x000D22D2 File Offset: 0x000D04D2
		public NavigationContext NavigationContext
		{
			get
			{
				if (!this.FrameworkContext)
				{
					return null;
				}
				return this._FrameworkContext.context;
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000D22EE File Offset: 0x000D04EE
		public void SetupContexts(ScrollChildContext context)
		{
			this.FrameworkContext.context = context;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000D22FC File Offset: 0x000D04FC
		public void setData(FrameworkDataElement Data)
		{
			AchievementInfoData achievementInfoData = Data as AchievementInfoData;
			if (achievementInfoData != null)
			{
				this.SetAchievementData(achievementInfoData);
			}
			else
			{
				HiddenAchievementData hiddenAchievementData = Data as HiddenAchievementData;
				if (hiddenAchievementData != null)
				{
					this.SetHiddenData(hiddenAchievementData);
				}
			}
			this.Icon.Sync(true);
			this.WasActive = null;
			this.Update();
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000D234C File Offset: 0x000D054C
		public void SetAchievementData(AchievementInfoData Data)
		{
			AchievementInfo achievement = Data.Achievement;
			this.Name.SetText(achievement.Name);
			this.Description.SetText(achievement.Description);
			if (!achievement.Achieved)
			{
				this.SetLocked(true);
				this.Progress.Set(achievement);
				this.Icon.sprite = SpriteManager.GetUnitySprite(achievement.IconLocked);
				this.Date.SetText("");
				return;
			}
			this.SetLocked(false);
			this.Progress.Hide();
			this.Icon.sprite = SpriteManager.GetUnitySprite(achievement.IconUnlocked);
			if (achievement.TimeStamp != DateTime.MinValue)
			{
				this.Date.SetText("Unlocked " + achievement.TimeStamp.ToString("f"));
				return;
			}
			this.Date.SetText("");
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000D243C File Offset: 0x000D063C
		public void SetHiddenData(HiddenAchievementData Data)
		{
			int amount = Data.Amount;
			this.SetLocked(true);
			this.Progress.Hide();
			this.Icon.sprite = SpriteManager.GetUnitySprite("UI/Achievements/hidden.png");
			this.Name.SetText(amount.ToString() + " hidden achievements remaining");
			this.Description.SetText("Details will be revealed once unlocked.");
			this.Date.SetText("");
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000D24B8 File Offset: 0x000D06B8
		public void SetLocked(bool Value)
		{
			if (Value)
			{
				this.Name.color = The.Color.DarkCyan;
				this.Description.color = The.Color.Gray;
				this.Date.color = The.Color.Black;
				this.Icon.borderColor = The.Color.Black;
				this.Background.color = this.Background.color.WithAlpha(0f);
				return;
			}
			this.Name.color = The.Color.Yellow;
			this.Description.color = The.Color.Gray;
			this.Date.color = The.Color.Black;
			this.Icon.borderColor = The.Color.Yellow;
			this.Background.color = this.Background.color.WithAlpha(0.25f);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000D25B4 File Offset: 0x000D07B4
		public void Update()
		{
			NavigationContext navigationContext = this.NavigationContext;
			bool flag = navigationContext != null && navigationContext.IsActive(true);
			bool flag2 = flag;
			bool? wasActive = this.WasActive;
			if (!((flag2 == wasActive.GetValueOrDefault()) & (wasActive != null)))
			{
				this.WasActive = new bool?(flag);
				this.Background.color = this.Background.color.WithAlpha(flag ? 0.25f : 0f);
			}
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000D2626 File Offset: 0x000D0826
		public NavigationContext GetNavigationContext()
		{
			return null;
		}

		// Token: 0x04001440 RID: 5184
		public ImageTinyFrame Icon;

		// Token: 0x04001441 RID: 5185
		public UITextSkin Name;

		// Token: 0x04001442 RID: 5186
		public UITextSkin Description;

		// Token: 0x04001443 RID: 5187
		public UITextSkin Date;

		// Token: 0x04001444 RID: 5188
		public Image Background;

		// Token: 0x04001445 RID: 5189
		public ProgressBar Progress;

		// Token: 0x04001446 RID: 5190
		private FrameworkContext _FrameworkContext;

		// Token: 0x04001447 RID: 5191
		private bool? WasActive;
	}
}
