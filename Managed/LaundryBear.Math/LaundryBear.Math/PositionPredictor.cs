using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000009 RID: 9
	public class PositionPredictor
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002597 File Offset: 0x00000797
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000025A0 File Offset: 0x000007A0
		public float Smoothing
		{
			get
			{
				return this.m_smoothing;
			}
			set
			{
				if (value != this.m_smoothing)
				{
					this.m_smoothing = value;
					int num = Mathf.Max(10, Mathf.FloorToInt(value * 1.5f));
					this.m_velocity = new GaussianWindow1D_Vector3(this.m_smoothing, num);
					this.m_accel = new GaussianWindow1D_Vector3(this.m_smoothing, num);
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025F5 File Offset: 0x000007F5
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000025FD File Offset: 0x000007FD
		public bool IgnoreY { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002606 File Offset: 0x00000806
		public bool IsEmpty
		{
			get
			{
				return this.m_velocity.IsEmpty();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002613 File Offset: 0x00000813
		public void ApplyTransformDelta(Vector3 positionDelta)
		{
			this.m_position += positionDelta;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002627 File Offset: 0x00000827
		public void Reset()
		{
			this.m_velocity.Reset();
			this.m_accel.Reset();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002640 File Offset: 0x00000840
		public void AddPosition(Vector3 pos)
		{
			if (this.IsEmpty)
			{
				this.m_velocity.AddValue(Vector3.zero);
			}
			else if (Time.deltaTime > 1E-05f)
			{
				Vector3 vector = this.m_velocity.Value();
				Vector3 vector2 = (pos - this.m_position) / Time.deltaTime;
				if (this.IgnoreY)
				{
					vector2.y = 0f;
				}
				this.m_velocity.AddValue(vector2);
				this.m_accel.AddValue(vector2 - vector);
			}
			this.m_position = pos;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026D0 File Offset: 0x000008D0
		public Vector3 PredictPosition(float lookaheadTime)
		{
			Vector3 vector = this.m_position;
			if (Time.deltaTime > 1E-05f)
			{
				int num = Mathf.Min(Mathf.RoundToInt(lookaheadTime / Time.deltaTime), 6);
				float num2 = lookaheadTime / (float)num;
				Vector3 vector2 = (this.m_velocity.IsEmpty() ? Vector3.zero : this.m_velocity.Value());
				Vector3 vector3 = (this.m_accel.IsEmpty() ? Vector3.zero : this.m_accel.Value());
				for (int i = 0; i < num; i++)
				{
					vector += vector2 * num2;
					Vector3 vector4 = vector2 + vector3 * num2;
					vector3 = Quaternion.FromToRotation(vector2, vector4) * vector3;
					vector2 = vector4;
				}
			}
			return vector;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000278F File Offset: 0x0000098F
		public Vector3 SmoothedVelocity()
		{
			if (!this.m_velocity.IsEmpty())
			{
				return this.m_velocity.Value();
			}
			return Vector3.zero;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027AF File Offset: 0x000009AF
		public Vector3 SmoothedAcceleration()
		{
			if (!this.m_accel.IsEmpty())
			{
				return this.m_accel.Value();
			}
			return Vector3.zero;
		}

		// Token: 0x0400000E RID: 14
		private Vector3 m_position;

		// Token: 0x0400000F RID: 15
		private const float SMOOTHING_DEFAULT = 10f;

		// Token: 0x04000010 RID: 16
		private float m_smoothing = 10f;

		// Token: 0x04000012 RID: 18
		private GaussianWindow1D_Vector3 m_velocity = new GaussianWindow1D_Vector3(10f, 10);

		// Token: 0x04000013 RID: 19
		private GaussianWindow1D_Vector3 m_accel = new GaussianWindow1D_Vector3(10f, 10);
	}
}
