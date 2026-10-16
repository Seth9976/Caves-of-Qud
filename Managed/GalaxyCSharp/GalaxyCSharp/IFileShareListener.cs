using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000E0 RID: 224
	public abstract class IFileShareListener : GalaxyTypeAwareListenerFileShare
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		internal IFileShareListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IFileShareListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IFileShareListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000E210 File Offset: 0x0000C410
		public IFileShareListener()
			: this(GalaxyInstancePINVOKE.new_IFileShareListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000E234 File Offset: 0x0000C434
		internal static HandleRef getCPtr(IFileShareListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000E254 File Offset: 0x0000C454
		~IFileShareListener()
		{
			this.Dispose();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000E284 File Offset: 0x0000C484
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFileShareListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IFileShareListener.listeners.ContainsKey(handle))
					{
						IFileShareListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000993 RID: 2451
		public abstract void OnFileShareSuccess(string fileName, ulong sharedFileID);

		// Token: 0x06000994 RID: 2452
		public abstract void OnFileShareFailure(string fileName, IFileShareListener.FailureReason failureReason);

		// Token: 0x06000995 RID: 2453 RVA: 0x0000E334 File Offset: 0x0000C534
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnFileShareSuccess", IFileShareListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IFileShareListener.SwigDelegateIFileShareListener_0(IFileShareListener.SwigDirectorOnFileShareSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnFileShareFailure", IFileShareListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IFileShareListener.SwigDelegateIFileShareListener_1(IFileShareListener.SwigDirectorOnFileShareFailure);
			}
			GalaxyInstancePINVOKE.IFileShareListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IFileShareListener));
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		[MonoPInvokeCallback(typeof(IFileShareListener.SwigDelegateIFileShareListener_0))]
		private static void SwigDirectorOnFileShareSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, ulong sharedFileID)
		{
			if (IFileShareListener.listeners.ContainsKey(cPtr))
			{
				IFileShareListener.listeners[cPtr].OnFileShareSuccess(fileName, sharedFileID);
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000E402 File Offset: 0x0000C602
		[MonoPInvokeCallback(typeof(IFileShareListener.SwigDelegateIFileShareListener_1))]
		private static void SwigDirectorOnFileShareFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, int failureReason)
		{
			if (IFileShareListener.listeners.ContainsKey(cPtr))
			{
				IFileShareListener.listeners[cPtr].OnFileShareFailure(fileName, (IFileShareListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400016E RID: 366
		private static Dictionary<IntPtr, IFileShareListener> listeners = new Dictionary<IntPtr, IFileShareListener>();

		// Token: 0x0400016F RID: 367
		private HandleRef swigCPtr;

		// Token: 0x04000170 RID: 368
		private IFileShareListener.SwigDelegateIFileShareListener_0 swigDelegate0;

		// Token: 0x04000171 RID: 369
		private IFileShareListener.SwigDelegateIFileShareListener_1 swigDelegate1;

		// Token: 0x04000172 RID: 370
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(ulong)
		};

		// Token: 0x04000173 RID: 371
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(IFileShareListener.FailureReason)
		};

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x0600099B RID: 2459
		public delegate void SwigDelegateIFileShareListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, ulong sharedFileID);

		// Token: 0x020000E2 RID: 226
		// (Invoke) Token: 0x0600099F RID: 2463
		public delegate void SwigDelegateIFileShareListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string fileName, int failureReason);

		// Token: 0x020000E3 RID: 227
		public enum FailureReason
		{
			// Token: 0x04000175 RID: 373
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000176 RID: 374
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
