using System;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;
using XRL;

namespace Qud.UI
{
	// Token: 0x02000491 RID: 1169
	public class FadeToBlack : SingletonWindowBase<FadeToBlack>
	{
		// Token: 0x06002CDB RID: 11483 RVA: 0x000F2278 File Offset: 0x000F0478
		public static void FadeOut(float Duration, bool FadingUIManager = false)
		{
			FadeToBlack.FadeOut(Duration, The.Color.DarkBlack);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000F228C File Offset: 0x000F048C
		public static void FadeNow(float Duration, float? From = null, float? To = null, Color? FromColor = null, Color? ToColor = null)
		{
			CanvasGroup group = SingletonWindowBase<FadeToBlack>.instance.Group;
			float num = From ?? group.alpha;
			float num2 = To ?? group.alpha;
			SingletonWindowBase<FadeToBlack>.instance.FromColor = FromColor ?? SingletonWindowBase<FadeToBlack>.instance.image.color;
			SingletonWindowBase<FadeToBlack>.instance.ToColor = ToColor ?? SingletonWindowBase<FadeToBlack>.instance.FromColor;
			SingletonWindowBase<FadeToBlack>.instance.image.color = SingletonWindowBase<FadeToBlack>.instance.FromColor;
			SingletonWindowBase<FadeToBlack>.instance.WantsToBeSeen = num2 > 0f;
			SingletonWindowBase<FadeToBlack>.instance.Start = WindowBase.gameTimeMS;
			SingletonWindowBase<FadeToBlack>.instance.Duration = Duration;
			SingletonWindowBase<FadeToBlack>.instance.Active = true;
			SingletonWindowBase<FadeToBlack>.instance.From = num;
			SingletonWindowBase<FadeToBlack>.instance.To = num2;
			group.alpha = num;
			FadeToBlack.stage = ((num2 > num) ? FadeToBlack.FadeToBlackStage.FadingOut : FadeToBlack.FadeToBlackStage.FadingIn);
			SingletonWindowBase<FadeToBlack>.instance.Show();
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000F23B6 File Offset: 0x000F05B6
		public static void Fade(float Duration, float? From = null, float? To = null, Color? FromColor = null, Color? ToColor = null)
		{
			WindowBase.queueUIAction(delegate
			{
				CanvasGroup group = SingletonWindowBase<FadeToBlack>.instance.Group;
				float num = From ?? group.alpha;
				float num2 = To ?? group.alpha;
				SingletonWindowBase<FadeToBlack>.instance.FromColor = FromColor ?? SingletonWindowBase<FadeToBlack>.instance.image.color;
				SingletonWindowBase<FadeToBlack>.instance.ToColor = ToColor ?? SingletonWindowBase<FadeToBlack>.instance.FromColor;
				SingletonWindowBase<FadeToBlack>.instance.image.color = SingletonWindowBase<FadeToBlack>.instance.FromColor;
				SingletonWindowBase<FadeToBlack>.instance.WantsToBeSeen = num2 > 0f;
				SingletonWindowBase<FadeToBlack>.instance.Start = WindowBase.gameTimeMS;
				SingletonWindowBase<FadeToBlack>.instance.Duration = Duration;
				SingletonWindowBase<FadeToBlack>.instance.Active = true;
				SingletonWindowBase<FadeToBlack>.instance.From = num;
				SingletonWindowBase<FadeToBlack>.instance.To = num2;
				group.alpha = num;
				FadeToBlack.stage = ((num2 > num) ? FadeToBlack.FadeToBlackStage.FadingOut : FadeToBlack.FadeToBlackStage.FadingIn);
				SingletonWindowBase<FadeToBlack>.instance.Show();
			});
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000F23F1 File Offset: 0x000F05F1
		public static void FadeOut(float Duration, Color Color)
		{
			FadeToBlack.stage = FadeToBlack.FadeToBlackStage.FadingOut;
			WindowBase.queueUIAction(delegate
			{
				FadeToBlack.stage = FadeToBlack.FadeToBlackStage.FadingOut;
				SingletonWindowBase<FadeToBlack>.instance.ToColor = (SingletonWindowBase<FadeToBlack>.instance.FromColor = Color);
				SingletonWindowBase<FadeToBlack>.instance.image.color = Color;
				SingletonWindowBase<FadeToBlack>.instance.WantsToBeSeen = true;
				SingletonWindowBase<FadeToBlack>.instance.Start = WindowBase.gameTimeMS;
				SingletonWindowBase<FadeToBlack>.instance.Duration = Duration;
				SingletonWindowBase<FadeToBlack>.instance.Active = true;
				SingletonWindowBase<FadeToBlack>.instance.From = 0f;
				SingletonWindowBase<FadeToBlack>.instance.To = 1f;
				SingletonWindowBase<FadeToBlack>.instance.GetComponent<CanvasGroup>().alpha = 0f;
				SingletonWindowBase<FadeToBlack>.instance.Show();
			});
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000F241C File Offset: 0x000F061C
		public static void SetTileMode()
		{
			if (SingletonWindowBase<FadeToBlack>.instance.TileMode)
			{
				return;
			}
			SingletonWindowBase<FadeToBlack>.instance.TileMode = true;
			WindowBase.queueUIAction(delegate
			{
				RectTransform component = SingletonWindowBase<FadeToBlack>.instance.GetComponent<RectTransform>();
				component.SetParent(GameManager.Instance.TileRoot.transform);
				component.sizeDelta = new Vector2(1280f, 600f);
				component.localPosition = Vector3.zero;
			});
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000F245A File Offset: 0x000F065A
		public static void SetUIMode()
		{
			if (!SingletonWindowBase<FadeToBlack>.instance.TileMode)
			{
				return;
			}
			SingletonWindowBase<FadeToBlack>.instance.TileMode = false;
			WindowBase.queueUIAction(delegate
			{
				RectTransform component = SingletonWindowBase<FadeToBlack>.instance.GetComponent<RectTransform>();
				component.SetParent(UIManager.instance.transform);
				component.offsetMax = Vector2.zero;
				component.offsetMin = Vector2.zero;
			});
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000F2498 File Offset: 0x000F0698
		public static void FadeIn(float duration)
		{
			FadeToBlack.FadeIn(duration, ConsoleLib.Console.ColorUtility.ColorMap['k']);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000F24AC File Offset: 0x000F06AC
		public static void FadeIn(float duration, Color color)
		{
			FadeToBlack.stage = FadeToBlack.FadeToBlackStage.FadingIn;
			WindowBase.queueUIAction(delegate
			{
				FadeToBlack.stage = FadeToBlack.FadeToBlackStage.FadingIn;
				SingletonWindowBase<FadeToBlack>.instance.ToColor = (SingletonWindowBase<FadeToBlack>.instance.FromColor = color);
				SingletonWindowBase<FadeToBlack>.instance.image.color = color;
				SingletonWindowBase<FadeToBlack>.instance.WantsToBeSeen = false;
				SingletonWindowBase<FadeToBlack>.instance.Start = WindowBase.gameTimeMS;
				SingletonWindowBase<FadeToBlack>.instance.Duration = duration;
				SingletonWindowBase<FadeToBlack>.instance.Active = true;
				SingletonWindowBase<FadeToBlack>.instance.From = 1f;
				SingletonWindowBase<FadeToBlack>.instance.To = 0f;
				SingletonWindowBase<FadeToBlack>.instance.GetComponent<CanvasGroup>().alpha = 1f;
				SingletonWindowBase<FadeToBlack>.instance.Show();
			});
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000F24D7 File Offset: 0x000F06D7
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000F24DA File Offset: 0x000F06DA
		public override void Init()
		{
			base.Init();
			FadeToBlack.stage = FadeToBlack.FadeToBlackStage.FadedIn;
			this.Group.alpha = 0f;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000F24F8 File Offset: 0x000F06F8
		public override void _HideWithoutLeave()
		{
			if (!this.WantsToBeSeen && !this.Active)
			{
				base._HideWithoutLeave();
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000F2510 File Offset: 0x000F0710
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

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000F2538 File Offset: 0x000F0738
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
				FadeToBlack.stage = ((this.From > this.To) ? FadeToBlack.FadeToBlackStage.FadedIn : FadeToBlack.FadeToBlackStage.FadedOut);
				this.Active = false;
				if (!this.WantsToBeSeen)
				{
					this.Hide();
				}
			}
		}

		// Token: 0x04001904 RID: 6404
		public bool WantsToBeSeen;

		// Token: 0x04001905 RID: 6405
		public bool TileMode;

		// Token: 0x04001906 RID: 6406
		public Image image;

		// Token: 0x04001907 RID: 6407
		public static FadeToBlack.FadeToBlackStage stage;

		// Token: 0x04001908 RID: 6408
		private bool Active;

		// Token: 0x04001909 RID: 6409
		private long Start;

		// Token: 0x0400190A RID: 6410
		private float Duration = 3f;

		// Token: 0x0400190B RID: 6411
		private float From = 1f;

		// Token: 0x0400190C RID: 6412
		private float To;

		// Token: 0x0400190D RID: 6413
		private Color FromColor;

		// Token: 0x0400190E RID: 6414
		private Color ToColor;

		// Token: 0x0400190F RID: 6415
		private CanvasGroup _Group;

		// Token: 0x020018F2 RID: 6386
		public enum FadeToBlackStage
		{
			// Token: 0x0400642F RID: 25647
			FadingOut,
			// Token: 0x04006430 RID: 25648
			FadedOut,
			// Token: 0x04006431 RID: 25649
			FadingIn,
			// Token: 0x04006432 RID: 25650
			FadedIn
		}
	}
}
