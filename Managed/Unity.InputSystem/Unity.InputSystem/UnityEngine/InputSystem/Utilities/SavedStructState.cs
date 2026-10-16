using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200014A RID: 330
	internal sealed class SavedStructState<T> : ISavedState where T : struct
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x00056354 File Offset: 0x00054554
		internal SavedStructState(ref T state, SavedStructState<T>.TypedRestore restoreAction, Action staticDisposeCurrentState = null)
		{
			this.m_State = state;
			this.m_RestoreAction = restoreAction;
			this.m_StaticDisposeCurrentState = staticDisposeCurrentState;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00056376 File Offset: 0x00054576
		public void StaticDisposeCurrentState()
		{
			if (this.m_StaticDisposeCurrentState != null)
			{
				this.m_StaticDisposeCurrentState();
				this.m_StaticDisposeCurrentState = null;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00056392 File Offset: 0x00054592
		public void RestoreSavedState()
		{
			this.m_RestoreAction(ref this.m_State);
			this.m_RestoreAction = null;
		}

		// Token: 0x04000745 RID: 1861
		private T m_State;

		// Token: 0x04000746 RID: 1862
		private SavedStructState<T>.TypedRestore m_RestoreAction;

		// Token: 0x04000747 RID: 1863
		private Action m_StaticDisposeCurrentState;

		// Token: 0x02000253 RID: 595
		// (Invoke) Token: 0x0600167E RID: 5758
		public delegate void TypedRestore(ref T state);
	}
}
