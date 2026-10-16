using System;
using System.Collections;
using System.Collections.Concurrent;
using Kobold;
using UnityEngine;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000443 RID: 1091
	public class Notification : MonoBehaviour
	{
		// Token: 0x0600297F RID: 10623 RVA: 0x000D92C8 File Offset: 0x000D74C8
		public static void Enqueue(string Title, string Text = null, string Icon = null, string Sound = null, int? Value = null, int? MaxValue = null, Color? TitleColor = null, Color? TextColor = null, Color? FrameColor = null)
		{
			Notification.Queue.Enqueue(new Notification.Data
			{
				Title = Title,
				Text = Text,
				Sound = Sound,
				Icon = Icon,
				Value = Value,
				MaxValue = MaxValue,
				TitleColor = TitleColor,
				TextColor = TextColor,
				FrameColor = FrameColor
			});
			if (Notification.Running)
			{
				return;
			}
			Notification.Running = true;
			if (GameManager.IsOnUIContext())
			{
				Notification.StartDelegate();
				return;
			}
			GameManager.Instance.uiQueue.queueTask(Notification.StartDelegate, 0);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000D936C File Offset: 0x000D756C
		public void Resize()
		{
			Vector2 preferredValues = this.Title.GetPreferredValues(this.TextSize.x, this.TextSize.y);
			Vector2 preferredValues2 = this.Text.GetPreferredValues(this.TextSize.x, this.TextSize.y);
			this.Transform.sizeDelta = new Vector2(Mathf.Clamp(this.BaseSize.x + (Mathf.Max(preferredValues.x, preferredValues2.x) - this.TextSize.x), 240f, 400f), this.BaseSize.y);
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000D9410 File Offset: 0x000D7610
		private IEnumerator Routine()
		{
			this.Object.SetActive(true);
			this.Transform.anchoredPosition = this.Origin;
			Notification.Data data;
			while (Notification.Queue.TryDequeue(out data))
			{
				if (data.Sound != null)
				{
					SoundManager.PlayUISound(data.Sound, 1f, false, false, SoundRequest.SoundEffectType.None);
				}
				this.Transform.anchoredPosition = this.Origin;
				this.Title.SetText(data.Title ?? "");
				this.Title.color = data.TitleColor ?? The.Color.Yellow;
				this.Text.SetText(data.Text ?? "");
				this.Text.color = data.TextColor ?? The.Color.Gray;
				if (data.Value != null && data.MaxValue != null)
				{
					this.Progress.Set(data.Value.Value, data.MaxValue.Value);
				}
				else
				{
					this.Progress.Hide();
				}
				if (!data.Icon.IsNullOrEmpty())
				{
					this.Icon.gameObject.SetActive(true);
					this.Icon.sprite = SpriteManager.GetUnitySprite(data.Icon);
					this.Icon.borderColor = data.FrameColor ?? The.Color.Yellow;
				}
				else
				{
					this.Icon.gameObject.SetActive(false);
				}
				this.Resize();
				float d = 0f;
				while (d < this.TimeIn)
				{
					yield return null;
					d += Time.deltaTime;
					float num = d / this.TimeIn;
					this.Transform.anchoredPosition = Vector2.Lerp(this.Origin, this.Target, Easing.SineEaseOut(num));
				}
				this.Transform.anchoredPosition = this.Target;
				yield return new WaitForSeconds(this.TimeHold);
				d = 0f;
				while (d < this.TimeOut)
				{
					yield return null;
					d += Time.deltaTime;
					float num = d / this.TimeOut;
					this.Transform.anchoredPosition = Vector2.Lerp(this.Target, this.Origin, Easing.SineEaseIn(num));
				}
				this.Transform.anchoredPosition = this.Origin;
			}
			this.Object.SetActive(false);
			Notification.Running = false;
			yield break;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000D9420 File Offset: 0x000D7620
		private void Start()
		{
			this.Object = base.gameObject;
			this.Transform = (RectTransform)this.Object.transform;
			Notification.Instance = this;
			this.BaseSize = this.Transform.sizeDelta;
			this.TextSize = this.Title.GetPreferredValues("HEAIAAAAAAASCREAMINGAAAAAAAAAAAUGH", 400f, 40f);
			Vector2 anchoredPosition = this.Transform.anchoredPosition;
			this.Origin = new Vector2(anchoredPosition.x, Math.Min(anchoredPosition.y, -this.BaseSize.y));
			this.Target = new Vector2(this.Origin.x, 0f);
			this.Object.SetActive(false);
		}

		// Token: 0x04001559 RID: 5465
		private static Notification Instance;

		// Token: 0x0400155A RID: 5466
		private static readonly ConcurrentQueue<Notification.Data> Queue = new ConcurrentQueue<Notification.Data>();

		// Token: 0x0400155B RID: 5467
		private static readonly Action StartDelegate = delegate
		{
			GameManager.Instance.StartCoroutine(Notification.Instance.Routine());
		};

		// Token: 0x0400155C RID: 5468
		private static bool Running;

		// Token: 0x0400155D RID: 5469
		public ImageTinyFrame Icon;

		// Token: 0x0400155E RID: 5470
		public UITextSkin Title;

		// Token: 0x0400155F RID: 5471
		public UITextSkin Text;

		// Token: 0x04001560 RID: 5472
		public ProgressBar Progress;

		// Token: 0x04001561 RID: 5473
		public float TimeIn = 0.5f;

		// Token: 0x04001562 RID: 5474
		public float TimeOut = 0.5f;

		// Token: 0x04001563 RID: 5475
		public float TimeHold = 4f;

		// Token: 0x04001564 RID: 5476
		private GameObject Object;

		// Token: 0x04001565 RID: 5477
		private RectTransform Transform;

		// Token: 0x04001566 RID: 5478
		private Vector2 BaseSize;

		// Token: 0x04001567 RID: 5479
		private Vector2 TextSize;

		// Token: 0x04001568 RID: 5480
		private Vector2 Origin;

		// Token: 0x04001569 RID: 5481
		private Vector2 Target;

		// Token: 0x0200184B RID: 6219
		public struct Data
		{
			// Token: 0x040061EC RID: 25068
			public string Title;

			// Token: 0x040061ED RID: 25069
			public string Text;

			// Token: 0x040061EE RID: 25070
			public string Icon;

			// Token: 0x040061EF RID: 25071
			public string Sound;

			// Token: 0x040061F0 RID: 25072
			public int? Value;

			// Token: 0x040061F1 RID: 25073
			public int? MaxValue;

			// Token: 0x040061F2 RID: 25074
			public Color? TitleColor;

			// Token: 0x040061F3 RID: 25075
			public Color? TextColor;

			// Token: 0x040061F4 RID: 25076
			public Color? FrameColor;
		}
	}
}
