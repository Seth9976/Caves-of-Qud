using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000612 RID: 1554
	internal class GCHandlePool : IDisposable
	{
		// Token: 0x06002E0C RID: 11788 RVA: 0x000A2C7F File Offset: 0x000A0E7F
		public GCHandlePool(int capacity = 256, int allocBatchSize = 64)
		{
			this.m_Handles = new List<GCHandle>(capacity);
			this.m_UsedHandlesCount = 0;
			this.k_AllocBatchSize = allocBatchSize;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000A2CA4 File Offset: 0x000A0EA4
		public GCHandle Get(object target)
		{
			bool flag = target == null;
			GCHandle gchandle;
			if (flag)
			{
				gchandle = default(GCHandle);
			}
			else
			{
				bool flag2 = this.m_UsedHandlesCount < this.m_Handles.Count;
				if (flag2)
				{
					List<GCHandle> handles = this.m_Handles;
					int usedHandlesCount = this.m_UsedHandlesCount;
					this.m_UsedHandlesCount = usedHandlesCount + 1;
					GCHandle gchandle2 = handles[usedHandlesCount];
					gchandle2.Target = target;
					gchandle = gchandle2;
				}
				else
				{
					GCHandle gchandle3 = GCHandle.Alloc(target);
					this.m_Handles.Add(gchandle3);
					this.m_UsedHandlesCount++;
					int i = 0;
					int num = this.k_AllocBatchSize - 1;
					while (i < num)
					{
						this.m_Handles.Add(GCHandle.Alloc(null));
						i++;
					}
					gchandle = gchandle3;
				}
			}
			return gchandle;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000A2D70 File Offset: 0x000A0F70
		public IntPtr GetIntPtr(object target)
		{
			bool flag = target == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = GCHandle.ToIntPtr(this.Get(target));
			}
			return intPtr;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000A2DA0 File Offset: 0x000A0FA0
		public void ReturnAll()
		{
			for (int i = 0; i < this.m_UsedHandlesCount; i++)
			{
				GCHandle gchandle = this.m_Handles[i];
				gchandle.Target = null;
				this.m_Handles[i] = gchandle;
			}
			this.m_UsedHandlesCount = 0;
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000A2DF0 File Offset: 0x000A0FF0
		// (set) Token: 0x06002E11 RID: 11793 RVA: 0x000A2DF8 File Offset: 0x000A0FF8
		internal bool disposed { get; private set; }

		// Token: 0x06002E12 RID: 11794 RVA: 0x000A2E01 File Offset: 0x000A1001
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000A2E14 File Offset: 0x000A1014
		private void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					foreach (GCHandle gchandle in this.m_Handles)
					{
						bool isAllocated = gchandle.IsAllocated;
						if (isAllocated)
						{
							gchandle.Free();
						}
					}
					this.m_Handles = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x040014D1 RID: 5329
		private List<GCHandle> m_Handles;

		// Token: 0x040014D2 RID: 5330
		private int m_UsedHandlesCount;

		// Token: 0x040014D3 RID: 5331
		private readonly int k_AllocBatchSize;
	}
}
