using System;
using System.Collections;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x0200000B RID: 11
	public class BufferedAction
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000021 RID: 33 RVA: 0x00002520 File Offset: 0x00000720
		// (remove) Token: 0x06000022 RID: 34 RVA: 0x00002558 File Offset: 0x00000758
		public event Action ActionBufferedEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000023 RID: 35 RVA: 0x00002590 File Offset: 0x00000790
		// (remove) Token: 0x06000024 RID: 36 RVA: 0x000025C8 File Offset: 0x000007C8
		public event Action ActionUnbufferedEvent;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025FD File Offset: 0x000007FD
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002605 File Offset: 0x00000805
		public bool Buffered { get; private set; }

		// Token: 0x06000027 RID: 39 RVA: 0x0000260E File Offset: 0x0000080E
		public Coroutine Buffer(MonoBehaviour behaviour, float time)
		{
			if (this.m_coroutine == null)
			{
				this.m_bufferTime = time;
				this.m_coroutine = behaviour.StartCoroutine(this.JumpBufferCoroutine());
			}
			else
			{
				this.m_bufferTime = time;
				this.m_bufferTimer = 0f;
			}
			return this.m_coroutine;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000264B File Offset: 0x0000084B
		public void Unbuffer(MonoBehaviour behaviour)
		{
			if (this.m_coroutine != null)
			{
				behaviour.StopCoroutine(this.m_coroutine);
				this.m_coroutine = null;
				if (this.ActionUnbufferedEvent != null)
				{
					this.ActionUnbufferedEvent();
				}
			}
			this.Buffered = false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002682 File Offset: 0x00000882
		private IEnumerator JumpBufferCoroutine()
		{
			this.Buffered = true;
			if (this.ActionBufferedEvent != null)
			{
				this.ActionBufferedEvent();
			}
			while (this.m_bufferTimer < this.m_bufferTime)
			{
				this.m_bufferTimer = Mathf.Clamp(this.m_bufferTimer + Time.deltaTime, 0f, this.m_bufferTime);
				yield return null;
			}
			this.m_bufferTimer = 0f;
			this.Buffered = false;
			if (this.ActionUnbufferedEvent != null)
			{
				this.ActionUnbufferedEvent();
			}
			this.m_coroutine = null;
			yield break;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002691 File Offset: 0x00000891
		public static implicit operator bool(BufferedAction action)
		{
			return action.Buffered;
		}

		// Token: 0x04000010 RID: 16
		private float m_bufferTime;

		// Token: 0x04000011 RID: 17
		private float m_bufferTimer;

		// Token: 0x04000012 RID: 18
		private Coroutine m_coroutine;
	}
}
