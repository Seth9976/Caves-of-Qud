using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x0200001E RID: 30
	internal struct ColorTween : ITweenValue
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002E97 File Offset: 0x00001097
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002E9F File Offset: 0x0000109F
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002EB0 File Offset: 0x000010B0
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002EB9 File Offset: 0x000010B9
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002EC1 File Offset: 0x000010C1
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002ECA File Offset: 0x000010CA
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002ED2 File Offset: 0x000010D2
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002EDB File Offset: 0x000010DB
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002EE3 File Offset: 0x000010E3
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

		// Token: 0x06000092 RID: 146 RVA: 0x00002EEC File Offset: 0x000010EC
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

		// Token: 0x06000093 RID: 147 RVA: 0x00002F7D File Offset: 0x0000117D
		public void AddOnChangedCallback(UnityAction<Color> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new ColorTween.ColorTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002F9E File Offset: 0x0000119E
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002FA6 File Offset: 0x000011A6
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002FAE File Offset: 0x000011AE
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x04000075 RID: 117
		private ColorTween.ColorTweenCallback m_Target;

		// Token: 0x04000076 RID: 118
		private Color m_StartColor;

		// Token: 0x04000077 RID: 119
		private Color m_TargetColor;

		// Token: 0x04000078 RID: 120
		private ColorTween.ColorTweenMode m_TweenMode;

		// Token: 0x04000079 RID: 121
		private float m_Duration;

		// Token: 0x0400007A RID: 122
		private bool m_IgnoreTimeScale;

		// Token: 0x02000089 RID: 137
		public enum ColorTweenMode
		{
			// Token: 0x04000601 RID: 1537
			All,
			// Token: 0x04000602 RID: 1538
			RGB,
			// Token: 0x04000603 RID: 1539
			Alpha
		}

		// Token: 0x0200008A RID: 138
		public class ColorTweenCallback : UnityEvent<Color>
		{
		}
	}
}
