using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x020000A0 RID: 160
	public struct TouchHistory : IReadOnlyList<Touch>, IEnumerable<Touch>, IEnumerable, IReadOnlyCollection<Touch>
	{
		// Token: 0x06000C80 RID: 3200 RVA: 0x00042CAC File Offset: 0x00040EAC
		internal TouchHistory(Finger finger, InputStateHistory<TouchState> history, int startIndex = -1, int count = -1)
		{
			this.m_Finger = finger;
			this.m_History = history;
			this.m_Version = history.version;
			this.m_Count = ((count >= 0) ? count : this.m_History.Count);
			this.m_StartIndex = ((startIndex >= 0) ? startIndex : (this.m_History.Count - 1));
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00042D07 File Offset: 0x00040F07
		public IEnumerator<Touch> GetEnumerator()
		{
			return new TouchHistory.Enumerator(this);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00042D14 File Offset: 0x00040F14
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00042D1C File Offset: 0x00040F1C
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x1700035B RID: 859
		public Touch this[int index]
		{
			get
			{
				this.CheckValid();
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.Count), "index");
				}
				return new Touch(this.m_Finger, this.m_History[this.m_StartIndex - index]);
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00042D88 File Offset: 0x00040F88
		internal void CheckValid()
		{
			if (this.m_Finger == null || this.m_History == null)
			{
				throw new InvalidOperationException("Touch history not initialized");
			}
			if (this.m_History.version != this.m_Version)
			{
				throw new InvalidOperationException("Touch history is no longer valid; the recorded history has been changed");
			}
		}

		// Token: 0x0400048B RID: 1163
		private readonly InputStateHistory<TouchState> m_History;

		// Token: 0x0400048C RID: 1164
		private readonly Finger m_Finger;

		// Token: 0x0400048D RID: 1165
		private readonly int m_Count;

		// Token: 0x0400048E RID: 1166
		private readonly int m_StartIndex;

		// Token: 0x0400048F RID: 1167
		private readonly uint m_Version;

		// Token: 0x020001FC RID: 508
		private class Enumerator : IEnumerator<Touch>, IEnumerator, IDisposable
		{
			// Token: 0x06001502 RID: 5378 RVA: 0x00062733 File Offset: 0x00060933
			internal Enumerator(TouchHistory owner)
			{
				this.m_Owner = owner;
				this.m_Index = -1;
			}

			// Token: 0x06001503 RID: 5379 RVA: 0x0006274C File Offset: 0x0006094C
			public bool MoveNext()
			{
				if (this.m_Index >= this.m_Owner.Count - 1)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x06001504 RID: 5380 RVA: 0x00062782 File Offset: 0x00060982
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001505 RID: 5381 RVA: 0x0006278C File Offset: 0x0006098C
			public Touch Current
			{
				get
				{
					return this.m_Owner[this.m_Index];
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x06001506 RID: 5382 RVA: 0x000627AD File Offset: 0x000609AD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001507 RID: 5383 RVA: 0x000627BA File Offset: 0x000609BA
			public void Dispose()
			{
			}

			// Token: 0x04000B53 RID: 2899
			private readonly TouchHistory m_Owner;

			// Token: 0x04000B54 RID: 2900
			private int m_Index;
		}
	}
}
