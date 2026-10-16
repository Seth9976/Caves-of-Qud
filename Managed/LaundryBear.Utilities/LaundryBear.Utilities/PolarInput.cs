using System;
using LaundryBear.Math;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class PolarInput
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
	public float AngularVelocitySmoothed
	{
		get
		{
			return this.m_velocityQueue.GetAverage();
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000005 RID: 5 RVA: 0x00002105 File Offset: 0x00000305
	public float AngularVelocity
	{
		get
		{
			return this.m_velocityQueue.GetMostRecent();
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000006 RID: 6 RVA: 0x00002112 File Offset: 0x00000312
	public float Angle
	{
		get
		{
			return Mathf.Atan2(this.m_previousRawInput.y, this.m_previousRawInput.x);
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000007 RID: 7 RVA: 0x0000212F File Offset: 0x0000032F
	public float SquareMagnitude
	{
		get
		{
			return this.m_previousRawInput.sqrMagnitude;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000008 RID: 8 RVA: 0x0000213C File Offset: 0x0000033C
	public float Magnitude
	{
		get
		{
			return this.m_previousRawInput.magnitude;
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002149 File Offset: 0x00000349
	public PolarInput(PolarInput.PollJoystickInput axesDelegate)
		: this(axesDelegate, 0.25f, 8)
	{
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002158 File Offset: 0x00000358
	public PolarInput(PolarInput.PollJoystickInput axesDelegate, float noiseFilter)
		: this(axesDelegate, noiseFilter, 8)
	{
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002163 File Offset: 0x00000363
	public PolarInput(PolarInput.PollJoystickInput axesDelegate, float noiseFilter, int smoothingQuality)
	{
		this.m_velocityQueue = new StatisticalFloatQueue(smoothingQuality);
		this.m_noiseFilter = noiseFilter;
		this.m_axesDelegate = axesDelegate;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002188 File Offset: 0x00000388
	public void Update(float deltaTime)
	{
		Vector2 vector = this.m_axesDelegate();
		if (vector.sqrMagnitude < this.m_noiseFilter)
		{
			vector = Vector2.zero;
		}
		float num = Mathf.Atan2(vector.y, vector.x);
		float num2 = Mathf.Atan2(this.m_previousRawInput.y, this.m_previousRawInput.x);
		float num3 = num;
		if (num < 0f)
		{
			num3 = num + 6.2831855f;
		}
		float num4 = num2;
		if (num2 < 0f)
		{
			num4 = num2 + 6.2831855f;
		}
		float num5 = num3 - num4;
		if (Mathf.Abs(num5) > 3.1415927f)
		{
			num5 -= Mathf.Sign(num5) * 3.1415927f * 2f;
		}
		this.m_velocityQueue.AddValue(num5 / deltaTime);
		this.m_previousRawInput = vector;
	}

	// Token: 0x04000002 RID: 2
	private const int DEFAULT_QUEUE_CAPACITY = 8;

	// Token: 0x04000003 RID: 3
	private const float DEFAULT_NOISE_FILTER = 0.25f;

	// Token: 0x04000004 RID: 4
	private PolarInput.PollJoystickInput m_axesDelegate;

	// Token: 0x04000005 RID: 5
	private Vector2 m_previousRawInput;

	// Token: 0x04000006 RID: 6
	private StatisticalFloatQueue m_velocityQueue;

	// Token: 0x04000007 RID: 7
	private float m_noiseFilter;

	// Token: 0x02000019 RID: 25
	// (Invoke) Token: 0x06000089 RID: 137
	public delegate Vector2 PollJoystickInput();
}
