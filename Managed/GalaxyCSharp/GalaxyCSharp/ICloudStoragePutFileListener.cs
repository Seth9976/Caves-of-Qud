using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000CC RID: 204
	public abstract class ICloudStoragePutFileListener : GalaxyTypeAwareListenerCloudStoragePutFile
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x00017CB1 File Offset: 0x00015EB1
		internal ICloudStoragePutFileListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStoragePutFileListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStoragePutFileListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00017CD9 File Offset: 0x00015ED9
		public ICloudStoragePutFileListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStoragePutFileListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00017CFD File Offset: 0x00015EFD
		internal static HandleRef getCPtr(ICloudStoragePutFileListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00017D1C File Offset: 0x00015F1C
		~ICloudStoragePutFileListener()
		{
			this.Dispose();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00017D4C File Offset: 0x00015F4C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStoragePutFileListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStoragePutFileListener.listeners.ContainsKey(handle))
					{
						ICloudStoragePutFileListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000926 RID: 2342
		public abstract void OnPutFileSuccess(string container, string name);

		// Token: 0x06000927 RID: 2343
		public abstract void OnPutFileFailure(string container, string name, ICloudStoragePutFileListener.FailureReason failureReason);

		// Token: 0x06000928 RID: 2344 RVA: 0x00017DFC File Offset: 0x00015FFC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnPutFileSuccess", ICloudStoragePutFileListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_0(ICloudStoragePutFileListener.SwigDirectorOnPutFileSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnPutFileFailure", ICloudStoragePutFileListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_1(ICloudStoragePutFileListener.SwigDirectorOnPutFileFailure);
			}
			GalaxyInstancePINVOKE.ICloudStoragePutFileListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00017E70 File Offset: 0x00016070
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStoragePutFileListener));
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00017EA6 File Offset: 0x000160A6
		[MonoPInvokeCallback(typeof(ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_0))]
		private static void SwigDirectorOnPutFileSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (ICloudStoragePutFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStoragePutFileListener.listeners[cPtr].OnPutFileSuccess(container, name);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00017ECA File Offset: 0x000160CA
		[MonoPInvokeCallback(typeof(ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_1))]
		private static void SwigDirectorOnPutFileFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ICloudStoragePutFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStoragePutFileListener.listeners[cPtr].OnPutFileFailure(container, name, (ICloudStoragePutFileListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000139 RID: 313
		private static Dictionary<IntPtr, ICloudStoragePutFileListener> listeners = new Dictionary<IntPtr, ICloudStoragePutFileListener>();

		// Token: 0x0400013A RID: 314
		private HandleRef swigCPtr;

		// Token: 0x0400013B RID: 315
		private ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_0 swigDelegate0;

		// Token: 0x0400013C RID: 316
		private ICloudStoragePutFileListener.SwigDelegateICloudStoragePutFileListener_1 swigDelegate1;

		// Token: 0x0400013D RID: 317
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(string)
		};

		// Token: 0x0400013E RID: 318
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(ICloudStoragePutFileListener.FailureReason)
		};

		// Token: 0x020000CD RID: 205
		// (Invoke) Token: 0x0600092E RID: 2350
		public delegate void SwigDelegateICloudStoragePutFileListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x06000932 RID: 2354
		public delegate void SwigDelegateICloudStoragePutFileListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x020000CF RID: 207
		public enum FailureReason
		{
			// Token: 0x04000140 RID: 320
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000141 RID: 321
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000142 RID: 322
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000143 RID: 323
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000144 RID: 324
			FAILURE_REASON_ABORTED,
			// Token: 0x04000145 RID: 325
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x04000146 RID: 326
			FAILURE_REASON_READ_FUNC_ERROR,
			// Token: 0x04000147 RID: 327
			FAILURE_REASON_QUOTA_EXCEEDED
		}
	}
}
