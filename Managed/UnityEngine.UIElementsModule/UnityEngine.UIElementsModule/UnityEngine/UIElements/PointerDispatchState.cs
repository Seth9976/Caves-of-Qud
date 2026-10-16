using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000316 RID: 790
	internal class PointerDispatchState
	{
		// Token: 0x060016B3 RID: 5811 RVA: 0x00057CCC File Offset: 0x00055ECC
		public PointerDispatchState()
		{
			this.Reset();
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00057D18 File Offset: 0x00055F18
		internal void Reset()
		{
			for (int i = 0; i < this.m_PointerCapture.Length; i++)
			{
				this.m_PendingPointerCapture[i] = null;
				this.m_PointerCapture[i] = null;
				this.m_ShouldSendCompatibilityMouseEvents[i] = true;
			}
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00057D5C File Offset: 0x00055F5C
		public IEventHandler GetCapturingElement(int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId];
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00057D78 File Offset: 0x00055F78
		public bool HasPointerCapture(IEventHandler handler, int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId] == handler;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00057D98 File Offset: 0x00055F98
		public void CapturePointer(IEventHandler handler, int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId && this.m_PendingPointerCapture[pointerId] != handler && GUIUtility.hotControl != 0;
			if (flag)
			{
				GUIUtility.hotControl = 0;
			}
			this.m_PendingPointerCapture[pointerId] = handler;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00057DDA File Offset: 0x00055FDA
		public void ReleasePointer(int pointerId)
		{
			this.m_PendingPointerCapture[pointerId] = null;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00057DE8 File Offset: 0x00055FE8
		public void ReleasePointer(IEventHandler handler, int pointerId)
		{
			bool flag = handler == this.m_PendingPointerCapture[pointerId];
			if (flag)
			{
				this.m_PendingPointerCapture[pointerId] = null;
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00057E10 File Offset: 0x00056010
		public void ProcessPointerCapture(int pointerId)
		{
			IEventHandler eventHandler = this.m_PointerCapture[pointerId];
			bool flag = eventHandler == this.m_PendingPointerCapture[pointerId];
			if (!flag)
			{
				bool flag2 = eventHandler != null;
				if (flag2)
				{
					using (PointerCaptureOutEvent pooled = PointerCaptureEventBase<PointerCaptureOutEvent>.GetPooled(eventHandler, this.m_PendingPointerCapture[pointerId], pointerId))
					{
						eventHandler.SendEvent(pooled);
					}
					bool flag3 = pointerId == PointerId.mousePointerId && this.m_PointerCapture[pointerId] == eventHandler;
					if (flag3)
					{
						using (MouseCaptureOutEvent pooled2 = PointerCaptureEventBase<MouseCaptureOutEvent>.GetPooled(eventHandler, this.m_PendingPointerCapture[pointerId], pointerId))
						{
							eventHandler.SendEvent(pooled2);
						}
					}
				}
				IEventHandler eventHandler2 = this.m_PendingPointerCapture[pointerId];
				bool flag4 = eventHandler2 != null;
				if (flag4)
				{
					using (PointerCaptureEvent pooled3 = PointerCaptureEventBase<PointerCaptureEvent>.GetPooled(eventHandler2, this.m_PointerCapture[pointerId], pointerId))
					{
						eventHandler2.SendEvent(pooled3);
					}
					bool flag5 = pointerId == PointerId.mousePointerId && this.m_PendingPointerCapture[pointerId] == eventHandler2;
					if (flag5)
					{
						using (MouseCaptureEvent pooled4 = PointerCaptureEventBase<MouseCaptureEvent>.GetPooled(eventHandler2, this.m_PointerCapture[pointerId], pointerId))
						{
							eventHandler2.SendEvent(pooled4);
						}
					}
				}
				this.m_PointerCapture[pointerId] = this.m_PendingPointerCapture[pointerId];
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00057F88 File Offset: 0x00056188
		public void ActivateCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = true;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00057F94 File Offset: 0x00056194
		public void PreventCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = false;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00057FA0 File Offset: 0x000561A0
		public bool ShouldSendCompatibilityMouseEvents(IPointerEvent evt)
		{
			return evt.isPrimary && this.m_ShouldSendCompatibilityMouseEvents[evt.pointerId];
		}

		// Token: 0x04000C69 RID: 3177
		private IEventHandler[] m_PendingPointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x04000C6A RID: 3178
		private IEventHandler[] m_PointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x04000C6B RID: 3179
		private bool[] m_ShouldSendCompatibilityMouseEvents = new bool[PointerId.maxPointers];
	}
}
