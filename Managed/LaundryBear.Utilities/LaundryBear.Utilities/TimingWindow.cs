using System;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class TimingWindow
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002F73 File Offset: 0x00001173
		public bool Closed
		{
			get
			{
				return this.m_timer >= this.m_length + this.m_allowanceLate;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002F8D File Offset: 0x0000118D
		public bool TargetPassed
		{
			get
			{
				return this.m_timer > this.m_length;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002F9D File Offset: 0x0000119D
		public float TargetTime
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002FA5 File Offset: 0x000011A5
		public float CurrentTime
		{
			get
			{
				return this.m_timer;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002FAD File Offset: 0x000011AD
		public float CurrentTimeNormalized
		{
			get
			{
				return this.m_timer / this.m_length;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FBC File Offset: 0x000011BC
		public TimingWindow(float length, float allowanceEarly, float allowanceLate)
		{
			this.m_length = length;
			this.m_allowanceEarly = allowanceEarly;
			this.m_allowanceLate = allowanceLate;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002FD9 File Offset: 0x000011D9
		public void Update(float deltaTime)
		{
			this.m_timer = Mathf.Min(this.m_timer + deltaTime, this.m_length + this.m_allowanceLate);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002FFC File Offset: 0x000011FC
		public bool PollWindow(out TimingWindow.TimingResult result)
		{
			if (this.m_length - this.m_allowanceEarly >= this.m_timer)
			{
				result = new TimingWindow.TimingResult(TimingWindow.TimingStatus.OutsideEarly, this.m_timer - this.m_length, (this.m_timer - this.m_length) / this.m_length);
				return false;
			}
			if (this.m_timer < this.m_length)
			{
				result = new TimingWindow.TimingResult(TimingWindow.TimingStatus.InsideEarly, this.m_timer - this.m_length, (this.m_timer - this.m_length) / this.m_length);
				return true;
			}
			if (this.m_timer < this.m_length + this.m_allowanceLate)
			{
				result = new TimingWindow.TimingResult(TimingWindow.TimingStatus.InsideLate, this.m_timer - this.m_length, (this.m_timer - this.m_length) / this.m_length);
				return true;
			}
			result = new TimingWindow.TimingResult(TimingWindow.TimingStatus.OutsideLate, this.m_timer - this.m_length, (this.m_timer - this.m_length) / this.m_length);
			return false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000030FF File Offset: 0x000012FF
		public void Reset()
		{
			this.m_timer = 0f;
		}

		// Token: 0x04000029 RID: 41
		[SerializeField]
		private float m_length;

		// Token: 0x0400002A RID: 42
		[SerializeField]
		private float m_allowanceEarly;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		private float m_allowanceLate;

		// Token: 0x0400002C RID: 44
		private float m_timer;

		// Token: 0x02000027 RID: 39
		public enum TimingStatus
		{
			// Token: 0x04000064 RID: 100
			OutsideEarly,
			// Token: 0x04000065 RID: 101
			InsideEarly,
			// Token: 0x04000066 RID: 102
			InsideLate,
			// Token: 0x04000067 RID: 103
			OutsideLate
		}

		// Token: 0x02000028 RID: 40
		public struct TimingResult
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003811 File Offset: 0x00001A11
			public TimingWindow.TimingStatus Status
			{
				get
				{
					return this.m_status;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003819 File Offset: 0x00001A19
			public float Delta
			{
				get
				{
					return this.m_delta;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003821 File Offset: 0x00001A21
			public float NormalizedDelta
			{
				get
				{
					return this.m_normalizedDelta;
				}
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00003829 File Offset: 0x00001A29
			public TimingResult(TimingWindow.TimingStatus status, float delta, float normalizedDelta)
			{
				this.m_status = status;
				this.m_delta = delta;
				this.m_normalizedDelta = normalizedDelta;
			}

			// Token: 0x04000068 RID: 104
			private TimingWindow.TimingStatus m_status;

			// Token: 0x04000069 RID: 105
			private float m_delta;

			// Token: 0x0400006A RID: 106
			private float m_normalizedDelta;
		}
	}
}
