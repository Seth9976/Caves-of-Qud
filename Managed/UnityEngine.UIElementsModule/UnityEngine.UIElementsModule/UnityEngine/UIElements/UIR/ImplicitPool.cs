using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000642 RID: 1602
	internal class ImplicitPool<T> where T : class
	{
		// Token: 0x06002F0F RID: 12047 RVA: 0x000ABD44 File Offset: 0x000A9F44
		public ImplicitPool(Func<T> createAction, Action<T> resetAction, int startCapacity, int maxCapacity)
		{
			Debug.Assert(createAction != null);
			Debug.Assert(startCapacity > 0);
			Debug.Assert(startCapacity <= maxCapacity);
			Debug.Assert(maxCapacity > 0);
			this.m_List = new List<T>(0);
			this.m_StartCapacity = startCapacity;
			this.m_MaxCapacity = maxCapacity;
			this.m_CreateAction = createAction;
			this.m_ResetAction = resetAction;
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000ABDB0 File Offset: 0x000A9FB0
		public T Get()
		{
			bool flag = this.m_UsedCount < this.m_List.Count;
			T t;
			if (flag)
			{
				List<T> list = this.m_List;
				int usedCount = this.m_UsedCount;
				this.m_UsedCount = usedCount + 1;
				t = list[usedCount];
			}
			else
			{
				bool flag2 = this.m_UsedCount < this.m_MaxCapacity;
				if (flag2)
				{
					int num = Mathf.Max(this.m_StartCapacity, this.m_UsedCount);
					int num2 = this.m_MaxCapacity - this.m_UsedCount;
					int num3 = Mathf.Min(num2, num);
					this.m_List.Capacity = this.m_UsedCount + num3;
					T t2 = this.m_CreateAction();
					this.m_List.Add(t2);
					this.m_UsedCount++;
					for (int i = 1; i < num3; i++)
					{
						this.m_List.Add(this.m_CreateAction());
					}
					t = t2;
				}
				else
				{
					t = this.m_CreateAction();
				}
			}
			return t;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000ABEBC File Offset: 0x000AA0BC
		public void ReturnAll()
		{
			Debug.Assert(this.m_List.Count <= this.m_MaxCapacity);
			bool flag = this.m_ResetAction != null;
			if (flag)
			{
				for (int i = 0; i < this.m_UsedCount; i++)
				{
					this.m_ResetAction(this.m_List[i]);
				}
			}
			this.m_UsedCount = 0;
		}

		// Token: 0x04001627 RID: 5671
		private readonly int m_StartCapacity;

		// Token: 0x04001628 RID: 5672
		private readonly int m_MaxCapacity;

		// Token: 0x04001629 RID: 5673
		private Func<T> m_CreateAction;

		// Token: 0x0400162A RID: 5674
		private Action<T> m_ResetAction;

		// Token: 0x0400162B RID: 5675
		private List<T> m_List;

		// Token: 0x0400162C RID: 5676
		private int m_UsedCount;
	}
}
