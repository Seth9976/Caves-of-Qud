using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200015F RID: 351
	public abstract class ISharedFileDownloadListener : GalaxyTypeAwareListenerSharedFileDownload
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x00014F30 File Offset: 0x00013130
		internal ISharedFileDownloadListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISharedFileDownloadListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISharedFileDownloadListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00014F58 File Offset: 0x00013158
		public ISharedFileDownloadListener()
			: this(GalaxyInstancePINVOKE.new_ISharedFileDownloadListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00014F7C File Offset: 0x0001317C
		internal static HandleRef getCPtr(ISharedFileDownloadListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00014F9C File Offset: 0x0001319C
		~ISharedFileDownloadListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00014FCC File Offset: 0x000131CC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISharedFileDownloadListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISharedFileDownloadListener.listeners.ContainsKey(handle))
					{
						ISharedFileDownloadListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CE0 RID: 3296
		public abstract void OnSharedFileDownloadSuccess(ulong sharedFileID, string fileName);

		// Token: 0x06000CE1 RID: 3297
		public abstract void OnSharedFileDownloadFailure(ulong sharedFileID, ISharedFileDownloadListener.FailureReason failureReason);

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0001507C File Offset: 0x0001327C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnSharedFileDownloadSuccess", ISharedFileDownloadListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_0(ISharedFileDownloadListener.SwigDirectorOnSharedFileDownloadSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnSharedFileDownloadFailure", ISharedFileDownloadListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_1(ISharedFileDownloadListener.SwigDirectorOnSharedFileDownloadFailure);
			}
			GalaxyInstancePINVOKE.ISharedFileDownloadListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000150F0 File Offset: 0x000132F0
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISharedFileDownloadListener));
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00015126 File Offset: 0x00013326
		[MonoPInvokeCallback(typeof(ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_0))]
		private static void SwigDirectorOnSharedFileDownloadSuccess(IntPtr cPtr, ulong sharedFileID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName)
		{
			if (ISharedFileDownloadListener.listeners.ContainsKey(cPtr))
			{
				ISharedFileDownloadListener.listeners[cPtr].OnSharedFileDownloadSuccess(sharedFileID, fileName);
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0001514A File Offset: 0x0001334A
		[MonoPInvokeCallback(typeof(ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_1))]
		private static void SwigDirectorOnSharedFileDownloadFailure(IntPtr cPtr, ulong sharedFileID, int failureReason)
		{
			if (ISharedFileDownloadListener.listeners.ContainsKey(cPtr))
			{
				ISharedFileDownloadListener.listeners[cPtr].OnSharedFileDownloadFailure(sharedFileID, (ISharedFileDownloadListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000296 RID: 662
		private static Dictionary<IntPtr, ISharedFileDownloadListener> listeners = new Dictionary<IntPtr, ISharedFileDownloadListener>();

		// Token: 0x04000297 RID: 663
		private HandleRef swigCPtr;

		// Token: 0x04000298 RID: 664
		private ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_0 swigDelegate0;

		// Token: 0x04000299 RID: 665
		private ISharedFileDownloadListener.SwigDelegateISharedFileDownloadListener_1 swigDelegate1;

		// Token: 0x0400029A RID: 666
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(string)
		};

		// Token: 0x0400029B RID: 667
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(ulong),
			typeof(ISharedFileDownloadListener.FailureReason)
		};

		// Token: 0x02000160 RID: 352
		// (Invoke) Token: 0x06000CE8 RID: 3304
		public delegate void SwigDelegateISharedFileDownloadListener_0(IntPtr cPtr, ulong sharedFileID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName);

		// Token: 0x02000161 RID: 353
		// (Invoke) Token: 0x06000CEC RID: 3308
		public delegate void SwigDelegateISharedFileDownloadListener_1(IntPtr cPtr, ulong sharedFileID, int failureReason);

		// Token: 0x02000162 RID: 354
		public enum FailureReason
		{
			// Token: 0x0400029D RID: 669
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400029E RID: 670
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
