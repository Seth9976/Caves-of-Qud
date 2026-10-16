using System;

namespace UnityEngine.Android
{
	// Token: 0x02000020 RID: 32
	public class GetAssetPackStateAsyncOperation : CustomYieldInstruction
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public override bool keepWaiting
		{
			get
			{
				object operationLock = this.m_OperationLock;
				bool flag2;
				lock (operationLock)
				{
					flag2 = this.m_States == null;
				}
				return flag2;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000A438 File Offset: 0x00008638
		public bool isDone
		{
			get
			{
				return !this.keepWaiting;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A81C File Offset: 0x00008A1C
		public ulong size
		{
			get
			{
				object operationLock = this.m_OperationLock;
				ulong size;
				lock (operationLock)
				{
					size = this.m_Size;
				}
				return size;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000A864 File Offset: 0x00008A64
		public AndroidAssetPackState[] states
		{
			get
			{
				object operationLock = this.m_OperationLock;
				AndroidAssetPackState[] states;
				lock (operationLock)
				{
					states = this.m_States;
				}
				return states;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A8AC File Offset: 0x00008AAC
		internal GetAssetPackStateAsyncOperation()
		{
			this.m_OperationLock = new object();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		internal void OnResult(ulong size, AndroidAssetPackState[] states)
		{
			object operationLock = this.m_OperationLock;
			lock (operationLock)
			{
				this.m_Size = size;
				this.m_States = states;
			}
		}

		// Token: 0x04000070 RID: 112
		private ulong m_Size;

		// Token: 0x04000071 RID: 113
		private AndroidAssetPackState[] m_States;

		// Token: 0x04000072 RID: 114
		private readonly object m_OperationLock;
	}
}
