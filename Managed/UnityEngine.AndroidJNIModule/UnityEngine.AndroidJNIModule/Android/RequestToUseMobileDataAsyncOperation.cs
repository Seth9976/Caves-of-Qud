using System;

namespace UnityEngine.Android
{
	// Token: 0x02000021 RID: 33
	public class RequestToUseMobileDataAsyncOperation : CustomYieldInstruction
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A914 File Offset: 0x00008B14
		public override bool keepWaiting
		{
			get
			{
				object operationLock = this.m_OperationLock;
				bool flag2;
				lock (operationLock)
				{
					flag2 = this.m_RequestResult == null;
				}
				return flag2;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000A438 File Offset: 0x00008638
		public bool isDone
		{
			get
			{
				return !this.keepWaiting;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A960 File Offset: 0x00008B60
		public AndroidAssetPackUseMobileDataRequestResult result
		{
			get
			{
				object operationLock = this.m_OperationLock;
				AndroidAssetPackUseMobileDataRequestResult requestResult;
				lock (operationLock)
				{
					requestResult = this.m_RequestResult;
				}
				return requestResult;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		internal RequestToUseMobileDataAsyncOperation()
		{
			this.m_OperationLock = new object();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		internal void OnResult(AndroidAssetPackUseMobileDataRequestResult result)
		{
			object operationLock = this.m_OperationLock;
			lock (operationLock)
			{
				this.m_RequestResult = result;
			}
		}

		// Token: 0x04000073 RID: 115
		private AndroidAssetPackUseMobileDataRequestResult m_RequestResult;

		// Token: 0x04000074 RID: 116
		private readonly object m_OperationLock;
	}
}
