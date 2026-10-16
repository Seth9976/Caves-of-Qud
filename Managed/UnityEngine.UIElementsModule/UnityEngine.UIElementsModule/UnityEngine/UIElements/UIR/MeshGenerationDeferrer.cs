using System;
using System.Collections.Generic;
using Unity.Jobs;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200064D RID: 1613
	internal class MeshGenerationDeferrer : IDisposable
	{
		// Token: 0x06002F34 RID: 12084 RVA: 0x000AC4FA File Offset: 0x000AA6FA
		public void AddMeshGenerationJob(JobHandle jobHandle)
		{
			this.m_Dependencies.Enqueue(jobHandle);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000AC50C File Offset: 0x000AA70C
		public void AddMeshGenerationCallback(MeshGenerationCallback callback, object userData, MeshGenerationCallbackType callbackType, bool isJobDependent)
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentNullException("callback");
			}
			MeshGenerationDeferrer.CallbackInfo callbackInfo = new MeshGenerationDeferrer.CallbackInfo
			{
				callback = callback,
				userData = userData
			};
			bool flag2 = !isJobDependent;
			if (flag2)
			{
				switch (callbackType)
				{
				case MeshGenerationCallbackType.Fork:
					this.m_Fork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.WorkThenFork:
					this.m_WorkThenFork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.Work:
					this.m_Work.Enqueue(callbackInfo);
					break;
				default:
					throw new NotImplementedException();
				}
			}
			else
			{
				switch (callbackType)
				{
				case MeshGenerationCallbackType.Fork:
					this.m_JobDependentFork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.WorkThenFork:
					this.m_JobDependentWorkThenFork.Enqueue(callbackInfo);
					break;
				case MeshGenerationCallbackType.Work:
					this.m_JobDependentWork.Enqueue(callbackInfo);
					break;
				default:
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000AC5F4 File Offset: 0x000AA7F4
		public void ProcessDeferredWork(MeshGenerationContext meshGenerationContext)
		{
			for (;;)
			{
				int count = this.m_Fork.Count;
				int count2 = this.m_WorkThenFork.Count;
				int count3 = this.m_Work.Count;
				int count4 = this.m_JobDependentFork.Count;
				int count5 = this.m_JobDependentWorkThenFork.Count;
				int count6 = this.m_JobDependentWork.Count;
				int count7 = this.m_Dependencies.Count;
				bool flag = count + count2 + count3 + count7 == 0;
				if (flag)
				{
					break;
				}
				for (int i = 0; i < count; i++)
				{
					MeshGenerationDeferrer.CallbackInfo callbackInfo = this.m_Fork.Dequeue();
					MeshGenerationDeferrer.Invoke(callbackInfo, meshGenerationContext);
				}
				for (int j = 0; j < count2; j++)
				{
					MeshGenerationDeferrer.CallbackInfo callbackInfo2 = this.m_WorkThenFork.Dequeue();
					MeshGenerationDeferrer.Invoke(callbackInfo2, meshGenerationContext);
				}
				for (int k = 0; k < count3; k++)
				{
					MeshGenerationDeferrer.CallbackInfo callbackInfo3 = this.m_Work.Dequeue();
					MeshGenerationDeferrer.Invoke(callbackInfo3, meshGenerationContext);
				}
				for (int l = 0; l < count7; l++)
				{
					this.m_DependencyMerger.Add(this.m_Dependencies.Dequeue());
				}
				this.m_DependencyMerger.MergeAndReset().Complete();
				for (int m = 0; m < count4; m++)
				{
					MeshGenerationDeferrer.CallbackInfo callbackInfo4 = this.m_JobDependentFork.Dequeue();
					MeshGenerationDeferrer.Invoke(callbackInfo4, meshGenerationContext);
				}
				for (int n = 0; n < count5; n++)
				{
					MeshGenerationDeferrer.CallbackInfo callbackInfo5 = this.m_JobDependentWorkThenFork.Dequeue();
					MeshGenerationDeferrer.Invoke(callbackInfo5, meshGenerationContext);
				}
				for (int num = 0; num < count6; num++)
				{
					MeshGenerationDeferrer.CallbackInfo callbackInfo6 = this.m_JobDependentWork.Dequeue();
					MeshGenerationDeferrer.Invoke(callbackInfo6, meshGenerationContext);
				}
			}
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000AC7DC File Offset: 0x000AA9DC
		private static void Invoke(MeshGenerationDeferrer.CallbackInfo ci, MeshGenerationContext mgc)
		{
			try
			{
				ci.callback(mgc, ci.userData);
				bool flag = mgc.visualElement != null;
				if (flag)
				{
					Debug.LogWarning(string.Format("MeshGenerationContext is assigned to a VisualElement after calling '{0}'. Did you forget to call '{1}'?", ci.callback, "End"));
					mgc.End();
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000AC850 File Offset: 0x000AAA50
		// (set) Token: 0x06002F39 RID: 12089 RVA: 0x000AC858 File Offset: 0x000AAA58
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002F3A RID: 12090 RVA: 0x000AC861 File Offset: 0x000AAA61
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000AC874 File Offset: 0x000AAA74
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_DependencyMerger.Dispose();
					this.m_DependencyMerger = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001661 RID: 5729
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_Fork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x04001662 RID: 5730
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_WorkThenFork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x04001663 RID: 5731
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_Work = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x04001664 RID: 5732
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_JobDependentFork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x04001665 RID: 5733
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_JobDependentWorkThenFork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x04001666 RID: 5734
		private Queue<MeshGenerationDeferrer.CallbackInfo> m_JobDependentWork = new Queue<MeshGenerationDeferrer.CallbackInfo>(32);

		// Token: 0x04001667 RID: 5735
		private Queue<JobHandle> m_Dependencies = new Queue<JobHandle>(32);

		// Token: 0x04001668 RID: 5736
		private JobMerger m_DependencyMerger = new JobMerger(64);

		// Token: 0x0200064E RID: 1614
		private struct CallbackInfo
		{
			// Token: 0x0400166A RID: 5738
			public MeshGenerationCallback callback;

			// Token: 0x0400166B RID: 5739
			public object userData;
		}
	}
}
