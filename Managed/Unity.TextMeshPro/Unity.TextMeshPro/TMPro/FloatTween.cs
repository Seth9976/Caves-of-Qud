using System;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x0200001F RID: 31
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002FB9 File Offset: 0x000011B9
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002FC1 File Offset: 0x000011C1
		public float startValue
		{
			get
			{
				return this.m_StartValue;
			}
			set
			{
				this.m_StartValue = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002FCA File Offset: 0x000011CA
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002FD2 File Offset: 0x000011D2
		public float targetValue
		{
			get
			{
				return this.m_TargetValue;
			}
			set
			{
				this.m_TargetValue = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002FDB File Offset: 0x000011DB
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002FE3 File Offset: 0x000011E3
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002FEC File Offset: 0x000011EC
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002FF4 File Offset: 0x000011F4
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

		// Token: 0x0600009F RID: 159 RVA: 0x00003000 File Offset: 0x00001200
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float num = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(num);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003035 File Offset: 0x00001235
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003056 File Offset: 0x00001256
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000305E File Offset: 0x0000125E
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003066 File Offset: 0x00001266
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400007B RID: 123
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x0400007C RID: 124
		private float m_StartValue;

		// Token: 0x0400007D RID: 125
		private float m_TargetValue;

		// Token: 0x0400007E RID: 126
		private float m_Duration;

		// Token: 0x0400007F RID: 127
		private bool m_IgnoreTimeScale;

		// Token: 0x0200008B RID: 139
		public class FloatTweenCallback : UnityEvent<float>
		{
		}
	}
}
