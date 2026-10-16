using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000048 RID: 72
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001731B File Offset: 0x0001551B
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00017323 File Offset: 0x00015523
		public Color startColor
		{
			get
			{
				return this.m_StartColor;
			}
			set
			{
				this.m_StartColor = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001732C File Offset: 0x0001552C
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00017334 File Offset: 0x00015534
		public Color targetColor
		{
			get
			{
				return this.m_TargetColor;
			}
			set
			{
				this.m_TargetColor = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001733D File Offset: 0x0001553D
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00017345 File Offset: 0x00015545
		public ColorTween.ColorTweenMode tweenMode
		{
			get
			{
				return this.m_TweenMode;
			}
			set
			{
				this.m_TweenMode = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001734E File Offset: 0x0001554E
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00017356 File Offset: 0x00015556
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001735F File Offset: 0x0001555F
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00017367 File Offset: 0x00015567
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00017370 File Offset: 0x00015570
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			Color color = Color.Lerp(this.m_StartColor, this.m_TargetColor, floatPercentage);
			if (this.m_TweenMode == ColorTween.ColorTweenMode.Alpha)
			{
				color.r = this.m_StartColor.r;
				color.g = this.m_StartColor.g;
				color.b = this.m_StartColor.b;
			}
			else if (this.m_TweenMode == ColorTween.ColorTweenMode.RGB)
			{
				color.a = this.m_StartColor.a;
			}
			this.m_Target.Invoke(color);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00017401 File Offset: 0x00015601
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00017422 File Offset: 0x00015622
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001742A File Offset: 0x0001562A
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00017432 File Offset: 0x00015632
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000198 RID: 408
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x04000199 RID: 409
		private Color m_StartColor;

		// Token: 0x0400019A RID: 410
		private Color m_TargetColor;

		// Token: 0x0400019B RID: 411
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x0400019C RID: 412
		private float m_Duration;

		// Token: 0x0400019D RID: 413
		private bool m_IgnoreTimeScale;

		// Token: 0x020000B9 RID: 185
		public enum ColorTweenMode
		{
			// Token: 0x04000327 RID: 807
			All,
			// Token: 0x04000328 RID: 808
			RGB,
			// Token: 0x04000329 RID: 809
			Alpha
		}

		// Token: 0x020000BA RID: 186
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
