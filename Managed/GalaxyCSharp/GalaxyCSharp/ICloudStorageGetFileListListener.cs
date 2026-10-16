using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000C8 RID: 200
	public abstract class ICloudStorageGetFileListListener : GalaxyTypeAwareListenerCloudStorageGetFileList
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x00017A11 File Offset: 0x00015C11
		internal ICloudStorageGetFileListListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStorageGetFileListListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStorageGetFileListListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00017A39 File Offset: 0x00015C39
		public ICloudStorageGetFileListListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStorageGetFileListListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00017A5D File Offset: 0x00015C5D
		internal static HandleRef getCPtr(ICloudStorageGetFileListListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00017A7C File Offset: 0x00015C7C
		~ICloudStorageGetFileListListener()
		{
			this.Dispose();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00017AAC File Offset: 0x00015CAC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorageGetFileListListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStorageGetFileListListener.listeners.ContainsKey(handle))
					{
						ICloudStorageGetFileListListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000912 RID: 2322
		public abstract void OnGetFileListSuccess(uint fileCount, uint quota, uint quotaUsed);

		// Token: 0x06000913 RID: 2323
		public abstract void OnGetFileListFailure(ICloudStorageGetFileListListener.FailureReason failureReason);

		// Token: 0x06000914 RID: 2324 RVA: 0x00017B5C File Offset: 0x00015D5C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGetFileListSuccess", ICloudStorageGetFileListListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_0(ICloudStorageGetFileListListener.SwigDirectorOnGetFileListSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnGetFileListFailure", ICloudStorageGetFileListListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_1(ICloudStorageGetFileListListener.SwigDirectorOnGetFileListFailure);
			}
			GalaxyInstancePINVOKE.ICloudStorageGetFileListListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00017BD0 File Offset: 0x00015DD0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStorageGetFileListListener));
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00017C06 File Offset: 0x00015E06
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_0))]
		private static void SwigDirectorOnGetFileListSuccess(IntPtr cPtr, uint fileCount, uint quota, uint quotaUsed)
		{
			if (ICloudStorageGetFileListListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListListener.listeners[cPtr].OnGetFileListSuccess(fileCount, quota, quotaUsed);
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00017C2B File Offset: 0x00015E2B
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_1))]
		private static void SwigDirectorOnGetFileListFailure(IntPtr cPtr, int failureReason)
		{
			if (ICloudStorageGetFileListListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListListener.listeners[cPtr].OnGetFileListFailure((ICloudStorageGetFileListListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400012B RID: 299
		private static Dictionary<IntPtr, ICloudStorageGetFileListListener> listeners = new Dictionary<IntPtr, ICloudStorageGetFileListListener>();

		// Token: 0x0400012C RID: 300
		private HandleRef swigCPtr;

		// Token: 0x0400012D RID: 301
		private ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_0 swigDelegate0;

		// Token: 0x0400012E RID: 302
		private ICloudStorageGetFileListListener.SwigDelegateICloudStorageGetFileListListener_1 swigDelegate1;

		// Token: 0x0400012F RID: 303
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(uint),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x04000130 RID: 304
		private static Type[] swigMethodTypes1 = new Type[] { typeof(ICloudStorageGetFileListListener.FailureReason) };

		// Token: 0x020000C9 RID: 201
		// (Invoke) Token: 0x0600091A RID: 2330
		public delegate void SwigDelegateICloudStorageGetFileListListener_0(IntPtr cPtr, uint fileCount, uint quota, uint quotaUsed);

		// Token: 0x020000CA RID: 202
		// (Invoke) Token: 0x0600091E RID: 2334
		public delegate void SwigDelegateICloudStorageGetFileListListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000CB RID: 203
		public enum FailureReason
		{
			// Token: 0x04000132 RID: 306
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000133 RID: 307
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000134 RID: 308
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000135 RID: 309
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x04000136 RID: 310
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000137 RID: 311
			FAILURE_REASON_ABORTED,
			// Token: 0x04000138 RID: 312
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
