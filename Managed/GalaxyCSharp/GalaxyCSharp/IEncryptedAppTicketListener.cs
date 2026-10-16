using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000DA RID: 218
	public abstract class IEncryptedAppTicketListener : GalaxyTypeAwareListenerEncryptedAppTicket
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x000084A0 File Offset: 0x000066A0
		internal IEncryptedAppTicketListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IEncryptedAppTicketListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IEncryptedAppTicketListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000084C8 File Offset: 0x000066C8
		public IEncryptedAppTicketListener()
			: this(GalaxyInstancePINVOKE.new_IEncryptedAppTicketListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000084EC File Offset: 0x000066EC
		internal static HandleRef getCPtr(IEncryptedAppTicketListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000850C File Offset: 0x0000670C
		~IEncryptedAppTicketListener()
		{
			this.Dispose();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000853C File Offset: 0x0000673C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IEncryptedAppTicketListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IEncryptedAppTicketListener.listeners.ContainsKey(handle))
					{
						IEncryptedAppTicketListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000977 RID: 2423
		public abstract void OnEncryptedAppTicketRetrieveSuccess();

		// Token: 0x06000978 RID: 2424 RVA: 0x000085EC File Offset: 0x000067EC
		public virtual void OnEncryptedAppTicketRetrieveFailure(IEncryptedAppTicketListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.IEncryptedAppTicketListener_OnEncryptedAppTicketRetrieveFailure(this.swigCPtr, (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0000860C File Offset: 0x0000680C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnEncryptedAppTicketRetrieveSuccess", IEncryptedAppTicketListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_0(IEncryptedAppTicketListener.SwigDirectorOnEncryptedAppTicketRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnEncryptedAppTicketRetrieveFailure", IEncryptedAppTicketListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_1(IEncryptedAppTicketListener.SwigDirectorOnEncryptedAppTicketRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IEncryptedAppTicketListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00008680 File Offset: 0x00006880
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IEncryptedAppTicketListener));
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000086B6 File Offset: 0x000068B6
		[MonoPInvokeCallback(typeof(IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_0))]
		private static void SwigDirectorOnEncryptedAppTicketRetrieveSuccess(IntPtr cPtr)
		{
			if (IEncryptedAppTicketListener.listeners.ContainsKey(cPtr))
			{
				IEncryptedAppTicketListener.listeners[cPtr].OnEncryptedAppTicketRetrieveSuccess();
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000086D8 File Offset: 0x000068D8
		[MonoPInvokeCallback(typeof(IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_1))]
		private static void SwigDirectorOnEncryptedAppTicketRetrieveFailure(IntPtr cPtr, int failureReason)
		{
			if (IEncryptedAppTicketListener.listeners.ContainsKey(cPtr))
			{
				IEncryptedAppTicketListener.listeners[cPtr].OnEncryptedAppTicketRetrieveFailure((IEncryptedAppTicketListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400015E RID: 350
		private static Dictionary<IntPtr, IEncryptedAppTicketListener> listeners = new Dictionary<IntPtr, IEncryptedAppTicketListener>();

		// Token: 0x0400015F RID: 351
		private HandleRef swigCPtr;

		// Token: 0x04000160 RID: 352
		private IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_0 swigDelegate0;

		// Token: 0x04000161 RID: 353
		private IEncryptedAppTicketListener.SwigDelegateIEncryptedAppTicketListener_1 swigDelegate1;

		// Token: 0x04000162 RID: 354
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x04000163 RID: 355
		private static Type[] swigMethodTypes1 = new Type[] { typeof(IEncryptedAppTicketListener.FailureReason) };

		// Token: 0x020000DB RID: 219
		// (Invoke) Token: 0x0600097F RID: 2431
		public delegate void SwigDelegateIEncryptedAppTicketListener_0(IntPtr cPtr);

		// Token: 0x020000DC RID: 220
		// (Invoke) Token: 0x06000983 RID: 2435
		public delegate void SwigDelegateIEncryptedAppTicketListener_1(IntPtr cPtr, int failureReason);

		// Token: 0x020000DD RID: 221
		public enum FailureReason
		{
			// Token: 0x04000165 RID: 357
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000166 RID: 358
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
