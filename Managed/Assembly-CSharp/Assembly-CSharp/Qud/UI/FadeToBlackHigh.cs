using System;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;
using XRL;

namespace Qud.UI
{
	// Token: 0x02000492 RID: 1170
	public class FadeToBlackHigh : SingletonWindowBase<FadeToBlackHigh>
	{
		// Token: 0x06002CE9 RID: 11497 RVA: 0x000F25F5 File Offset: 0x000F07F5
		public static void FadeOut(float Duration, bool FadingUIManager = false)
		{
			FadeToBlackHigh.FadeOut(Duration, The.Color.DarkBlack);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000F2608 File Offset: 0x000F0808
		public static void FadeNow(float Duration, float? From = null, float? To = null, Color? FromColor = null, Color? ToColor = null)
		{
			CanvasGroup group = SingletonWindowBase<FadeToBlackHigh>.instance.Group;
			float num = From ?? group.alpha;
			float num2 = To ?? group.alpha;
			SingletonWindowBase<FadeToBlackHigh>.instance.FromColor = FromColor ?? SingletonWindowBase<FadeToBlackHigh>.instance.image.color;
			SingletonWindowBase<FadeToBlackHigh>.instance.ToColor = ToColor ?? SingletonWindowBase<FadeToBlackHigh>.instance.FromColor;
			SingletonWindowBase<FadeToBlackHigh>.instance.image.color = SingletonWindowBase<FadeToBlackHigh>.instance.FromColor;
			SingletonWindowBase<FadeToBlackHigh>.instance.WantsToBeSeen = num2 > 0f;
			SingletonWindowBase<FadeToBlackHigh>.instance.Start = WindowBase.gameTimeMS;
			SingletonWindowBase<FadeToBlackHigh>.instance.Duration = Duration;
			SingletonWindowBase<FadeToBlackHigh>.instance.Active = true;
			SingletonWindowBase<FadeToBlackHigh>.instance.From = num;
			SingletonWindowBase<FadeToBlackHigh>.instance.To = num2;
			group.alpha = num;
			FadeToBlackHigh.stage = ((num2 > num) ? FadeToBlackHigh.FadeToBlackStage.FadingOut : FadeToBlackHigh.FadeToBlackStage.FadingIn);
			SingletonWindowBase<FadeToBlackHigh>.instance.Show();
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000F2732 File Offset: 0x000F0932
		public static void Fade(float Duration, float? From = null, float? To = null, Color? FromColor = null, Color? ToColor = null)
		{
			WindowBase.queueUIAction(delegate
			{
				CanvasGroup group = SingletonWindowBase<FadeToBlackHigh>.instance.Group;
				float num = From ?? group.alpha;
				float num2 = To ?? group.alpha;
				SingletonWindowBase<FadeToBlackHigh>.instance.FromColor = FromColor ?? SingletonWindowBase<FadeToBlackHigh>.instance.image.color;
				SingletonWindowBase<FadeToBlackHigh>.instance.ToColor = ToColor ?? SingletonWindowBase<FadeToBlackHigh>.instance.FromColor;
				SingletonWindowBase<FadeToBlackHigh>.instance.image.color = SingletonWindowBase<FadeToBlackHigh>.instance.FromColor;
				SingletonWindowBase<FadeToBlackHigh>.instance.WantsToBeSeen = num2 > 0f;
				SingletonWindowBase<FadeToBlackHigh>.instance.Start = WindowBase.gameTimeMS;
				SingletonWindowBase<FadeToBlackHigh>.instance.Duration = Duration;
				SingletonWindowBase<FadeToBlackHigh>.instance.Active = true;
				SingletonWindowBase<FadeToBlackHigh>.instance.From = num;
				SingletonWindowBase<FadeToBlackHigh>.instance.To = num2;
				group.alpha = num;
				FadeToBlackHigh.stage = ((num2 > num) ? FadeToBlackHigh.FadeToBlackStage.FadingOut : FadeToBlackHigh.FadeToBlackStage.FadingIn);
				SingletonWindowBase<FadeToBlackHigh>.instance.Show();
			});
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000F276D File Offset: 0x000F096D
		public static void FadeOut(float Duration, Color Color)
		{
			FadeToBlackHigh.stage = FadeToBlackHigh.FadeToBlackStage.FadingOut;
			WindowBase.queueUIAction(delegate
			{
				FadeToBlackHigh.stage = FadeToBlackHigh.FadeToBlackStage.FadingOut;
				SingletonWindowBase<FadeToBlackHigh>.instance.ToColor = (SingletonWindowBase<FadeToBlackHigh>.instance.FromColor = Color);
				SingletonWindowBase<FadeToBlackHigh>.instance.image.color = Color;
				SingletonWindowBase<FadeToBlackHigh>.instance.WantsToBeSeen = true;
				SingletonWindowBase<FadeToBlackHigh>.instance.Start = WindowBase.gameTimeMS;
				SingletonWindowBase<FadeToBlackHigh>.instance.Duration = Duration;
				SingletonWindowBase<FadeToBlackHigh>.instance.Active = true;
				SingletonWindowBase<FadeToBlackHigh>.instance.From = 0f;
				SingletonWindowBase<FadeToBlackHigh>.instance.To = 1f;
				SingletonWindowBase<FadeToBlackHigh>.instance.GetComponent<CanvasGroup>().alpha = 0f;
				SingletonWindowBase<FadeToBlackHigh>.instance.Show();
			});
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000F2798 File Offset: 0x000F0998
		public static void SetTileMode()
		{
			if (SingletonWindowBase<FadeToBlackHigh>.instance.TileMode)
			{
				return;
			}
			SingletonWindowBase<FadeToBlackHigh>.instance.TileMode = true;
			WindowBase.queueUIAction(delegate
			{
				RectTransform component = SingletonWindowBase<FadeToBlackHigh>.instance.GetComponent<RectTransform>();
				component.SetParent(GameManager.Instance.TileRoot.transform);
				component.sizeDelta = new Vector2(1280f, 600f);
				component.localPosition = Vector3.zero;
			});
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000F27D6 File Offset: 0x000F09D6
		public static void SetUIMode()
		{
			if (!SingletonWindowBase<FadeToBlackHigh>.instance.TileMode)
			{
				return;
			}
			SingletonWindowBase<FadeToBlackHigh>.instance.TileMode = false;
			WindowBase.queueUIAction(delegate
			{
				RectTransform component = SingletonWindowBase<FadeToBlackHigh>.instance.GetComponent<RectTransform>();
				component.SetParent(UIManager.instance.transform);
				component.offsetMax = Vector2.zero;
				component.offsetMin = Vector2.zero;
			});
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000F2814 File Offset: 0x000F0A14
		public static void FadeIn(float duration)
		{
			FadeToBlackHigh.FadeIn(duration, ConsoleLib.Console.ColorUtility.ColorMap['k']);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000F2828 File Offset: 0x000F0A28
		public static void FadeIn(float duration, Color color)
		{
			FadeToBlackHigh.stage = FadeToBlackHigh.FadeToBlackStage.FadingIn;
			WindowBase.queueUIAction(delegate
			{
				FadeToBlackHigh.stage = FadeToBlackHigh.FadeToBlackStage.FadingIn;
				SingletonWindowBase<FadeToBlackHigh>.instance.ToColor = (SingletonWindowBase<FadeToBlackHigh>.instance.FromColor = color);
				SingletonWindowBase<FadeToBlackHigh>.instance.image.color = color;
				SingletonWindowBase<FadeToBlackHigh>.instance.WantsToBeSeen = false;
				SingletonWindowBase<FadeToBlackHigh>.instance.Start = WindowBase.gameTimeMS;
				SingletonWindowBase<FadeToBlackHigh>.instance.Duration = duration;
				SingletonWindowBase<FadeToBlackHigh>.instance.Active = true;
				SingletonWindowBase<FadeToBlackHigh>.instance.From = 1f;
				SingletonWindowBase<FadeToBlackHigh>.instance.To = 0f;
				SingletonWindowBase<FadeToBlackHigh>.instance.GetComponent<CanvasGroup>().alpha = 1f;
				SingletonWindowBase<FadeToBlackHigh>.instance.Show();
			});
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000F2853 File Offset: 0x000F0A53
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000F2856 File Offset: 0x000F0A56
		public override void Init()
		{
			base.Init();
			FadeToBlackHigh.stage = FadeToBlackHigh.FadeToBlackStage.FadedIn;
			this.Group.alpha = 0f;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000F2874 File Offset: 0x000F0A74
		public override void _HideWithoutLeave()
		{
			if (!this.WantsToBeSeen && !this.Active)
			{
				base._HideWithoutLeave();
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000F288C File Offset: 0x000F0A8C
		private CanvasGroup Group
		{
			get
			{
				CanvasGroup canvasGroup;
				if ((canvasGroup = this._Group) == null)
				{
					canvasGroup = (this._Group = base.GetComponent<CanvasGroup>());
				}
				return canvasGroup;
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000F28B4 File Offset: 0x000F0AB4
		public void Update()
		{
			if (!this.Active)
			{
				return;
			}
			CanvasGroup group = this.Group;
			float num = (float)(WindowBase.gameTimeMS - this.Start) / 1000f / this.Duration;
			group.alpha = Mathf.Lerp(this.From, this.To, num);
			this.image.color = Color.Lerp(this.FromColor, this.ToColor, num);
			if (num >= 1f)
			{
				FadeToBlackHigh.stage = ((this.From > this.To) ? FadeToBlackHigh.FadeToBlackStage.FadedIn : FadeToBlackHigh.FadeToBlackStage.FadedOut);
				this.Active = false;
				if (!this.WantsToBeSeen)
				{
					this.Hide();
				}
			}
		}

		// Token: 0x04001910 RID: 6416
		public bool WantsToBeSeen;

		// Token: 0x04001911 RID: 6417
		public bool TileMode;

		// Token: 0x04001912 RID: 6418
		public Image image;

		// Token: 0x04001913 RID: 6419
		public static FadeToBlackHigh.FadeToBlackStage stage;

		// Token: 0x04001914 RID: 6420
		private bool Active;

		// Token: 0x04001915 RID: 6421
		private long Start;

		// Token: 0x04001916 RID: 6422
		private float Duration = 3f;

		// Token: 0x04001917 RID: 6423
		private float From = 1f;

		// Token: 0x04001918 RID: 6424
		private float To;

		// Token: 0x04001919 RID: 6425
		private Color FromColor;

		// Token: 0x0400191A RID: 6426
		private Color ToColor;

		// Token: 0x0400191B RID: 6427
		private CanvasGroup _Group;

		// Token: 0x020018F7 RID: 6391
		public enum FadeToBlackStage
		{
			// Token: 0x04006440 RID: 25664
			FadingOut,
			// Token: 0x04006441 RID: 25665
			FadedOut,
			// Token: 0x04006442 RID: 25666
			FadingIn,
			// Token: 0x04006443 RID: 25667
			FadedIn
		}
	}
}
