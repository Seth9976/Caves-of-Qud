using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000420 RID: 1056
	public class ProgressBar : MonoBehaviour
	{
		// Token: 0x06002809 RID: 10249 RVA: 0x000CF113 File Offset: 0x000CD313
		public void Show()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000CF121 File Offset: 0x000CD321
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000CF130 File Offset: 0x000CD330
		public void Set(int Value, int MaxValue)
		{
			Value = Mathf.Min(Value, MaxValue);
			this.Number.SetText(Value.ToString() + " / " + MaxValue.ToString());
			this.Bar.maxValue = (float)MaxValue;
			this.Bar.value = (float)Value;
			this.Show();
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000CF18A File Offset: 0x000CD38A
		public void Set(AchievementInfo Achievement)
		{
			if (((Achievement != null) ? Achievement.Progress : null) == null)
			{
				this.Hide();
				return;
			}
			this.Set(Achievement.Progress.Value, Achievement.AchievedAt);
		}

		// Token: 0x040013DD RID: 5085
		public UITextSkin Number;

		// Token: 0x040013DE RID: 5086
		public Slider Bar;
	}
}
