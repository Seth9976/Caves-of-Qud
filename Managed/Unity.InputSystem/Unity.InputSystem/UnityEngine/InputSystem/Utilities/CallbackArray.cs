using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012E RID: 302
	internal struct CallbackArray<TDelegate> where TDelegate : Delegate
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00051CC5 File Offset: 0x0004FEC5
		public int length
		{
			get
			{
				return this.m_Callbacks.length;
			}
		}

		// Token: 0x170004DD RID: 1245
		public TDelegate this[int index]
		{
			get
			{
				return this.m_Callbacks[index];
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00051CE0 File Offset: 0x0004FEE0
		public void Clear()
		{
			this.m_Callbacks.Clear();
			this.m_CallbacksToAdd.Clear();
			this.m_CallbacksToRemove.Clear();
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00051D04 File Offset: 0x0004FF04
		public void AddCallback(TDelegate dlg)
		{
			if (!this.m_CannotMutateCallbacksArray)
			{
				if (!this.m_Callbacks.Contains(dlg))
				{
					this.m_Callbacks.AppendWithCapacity(dlg, 4);
				}
				return;
			}
			if (this.m_CallbacksToAdd.Contains(dlg))
			{
				return;
			}
			int num = this.m_CallbacksToRemove.IndexOf(dlg);
			if (num != -1)
			{
				this.m_CallbacksToRemove.RemoveAtByMovingTailWithCapacity(num);
			}
			this.m_CallbacksToAdd.AppendWithCapacity(dlg, 10);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00051D74 File Offset: 0x0004FF74
		public void RemoveCallback(TDelegate dlg)
		{
			if (!this.m_CannotMutateCallbacksArray)
			{
				int num = this.m_Callbacks.IndexOf(dlg);
				if (num >= 0)
				{
					this.m_Callbacks.RemoveAtWithCapacity(num);
				}
				return;
			}
			if (this.m_CallbacksToRemove.Contains(dlg))
			{
				return;
			}
			int num2 = this.m_CallbacksToAdd.IndexOf(dlg);
			if (num2 != -1)
			{
				this.m_CallbacksToAdd.RemoveAtByMovingTailWithCapacity(num2);
			}
			this.m_CallbacksToRemove.AppendWithCapacity(dlg, 10);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00051DE2 File Offset: 0x0004FFE2
		public void LockForChanges()
		{
			this.m_CannotMutateCallbacksArray = true;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00051DEC File Offset: 0x0004FFEC
		public void UnlockForChanges()
		{
			this.m_CannotMutateCallbacksArray = false;
			for (int i = 0; i < this.m_CallbacksToRemove.length; i++)
			{
				this.RemoveCallback(this.m_CallbacksToRemove[i]);
			}
			for (int j = 0; j < this.m_CallbacksToAdd.length; j++)
			{
				this.AddCallback(this.m_CallbacksToAdd[j]);
			}
			this.m_CallbacksToAdd.Clear();
			this.m_CallbacksToRemove.Clear();
		}

		// Token: 0x04000711 RID: 1809
		private bool m_CannotMutateCallbacksArray;

		// Token: 0x04000712 RID: 1810
		private InlinedArray<TDelegate> m_Callbacks;

		// Token: 0x04000713 RID: 1811
		private InlinedArray<TDelegate> m_CallbacksToAdd;

		// Token: 0x04000714 RID: 1812
		private InlinedArray<TDelegate> m_CallbacksToRemove;
	}
}
