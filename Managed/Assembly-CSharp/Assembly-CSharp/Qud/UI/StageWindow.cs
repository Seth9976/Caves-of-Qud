using System;
using System.Collections;
using UnityEngine;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x020004A5 RID: 1189
	[UIView("Stage", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "Stage", UICanvasHost = 1)]
	public class StageWindow : SingletonWindowBase<StageWindow>
	{
		// Token: 0x06002D92 RID: 11666 RVA: 0x000F9213 File Offset: 0x000F7413
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000F9218 File Offset: 0x000F7418
		public override void Show()
		{
			base.Show();
			UIManager.getWindow("PlayerStatusBar").Show();
			UIManager.getWindow("AbilityBar").Show();
			UIManager.getWindow("MessageLog").Show();
			UIManager.getWindow<NearbyItemsWindow>("NearbyItems").ShowIfEnabled();
			UIManager.getWindow<MinimapWindow>("Minimap").ShowIfEnabled();
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000F9278 File Offset: 0x000F7478
		public override void Hide()
		{
			if (GameManager.Instance.TargetZoomFactor == -2f)
			{
				base.canvasGroup.alpha = 0f;
			}
			else
			{
				base.Hide();
			}
			UIManager.getWindow("PlayerStatusBar").Hide();
			UIManager.getWindow("AbilityBar").Hide();
			UIManager.getWindow("MessageLog").Hide();
			UIManager.getWindow("NearbyItems").Hide();
			UIManager.getWindow("Minimap").Hide();
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000F92FC File Offset: 0x000F74FC
		public static async void FadeOut(float time)
		{
			await The.UiContext;
			SingletonWindowBase<StageWindow>.instance.StartCoroutine(SingletonWindowBase<StageWindow>.instance._FadeOut(time));
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000F9334 File Offset: 0x000F7534
		public static async void FadeIn(float time)
		{
			await The.UiContext;
			SingletonWindowBase<StageWindow>.instance.StartCoroutine(SingletonWindowBase<StageWindow>.instance._FadeIn(time));
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000F936B File Offset: 0x000F756B
		private IEnumerator _FadeOut(float time)
		{
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			float start = time;
			while (time > 0f)
			{
				time -= Time.deltaTime;
				canvasGroup.alpha = Mathf.Lerp(1f, 0f, 1f - time / start);
				yield return null;
			}
			canvasGroup.alpha = 0f;
			yield break;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000F9381 File Offset: 0x000F7581
		public IEnumerator _FadeIn(float time)
		{
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			float start = time;
			while (time > 0f)
			{
				time -= Time.deltaTime;
				canvasGroup.alpha = Mathf.Lerp(0f, 1f, 1f - time / start);
				yield return null;
			}
			canvasGroup.alpha = 1f;
			yield break;
		}

		// Token: 0x040019D6 RID: 6614
		public GameObject dock;
	}
}
