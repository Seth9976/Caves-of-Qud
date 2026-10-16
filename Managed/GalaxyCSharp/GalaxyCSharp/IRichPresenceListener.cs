using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000150 RID: 336
	public abstract class IRichPresenceListener : GalaxyTypeAwareListenerRichPresence
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x000142AC File Offset: 0x000124AC
		internal IRichPresenceListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IRichPresenceListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IRichPresenceListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000142D4 File Offset: 0x000124D4
		public IRichPresenceListener()
			: this(GalaxyInstancePINVOKE.new_IRichPresenceListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000142F8 File Offset: 0x000124F8
		internal static HandleRef getCPtr(IRichPresenceListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00014318 File Offset: 0x00012518
		~IRichPresenceListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00014348 File Offset: 0x00012548
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IRichPresenceListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IRichPresenceListener.listeners.ContainsKey(handle))
					{
						IRichPresenceListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C92 RID: 3218
		public abstract void OnRichPresenceUpdated(GalaxyID userID);

		// Token: 0x06000C93 RID: 3219 RVA: 0x000143F8 File Offset: 0x000125F8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnRichPresenceUpdated", IRichPresenceListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IRichPresenceListener.SwigDelegateIRichPresenceListener_0(IRichPresenceListener.SwigDirectorOnRichPresenceUpdated);
			}
			GalaxyInstancePINVOKE.IRichPresenceListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00014434 File Offset: 0x00012634
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IRichPresenceListener));
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0001446A File Offset: 0x0001266A
		[MonoPInvokeCallback(typeof(IRichPresenceListener.SwigDelegateIRichPresenceListener_0))]
		private static void SwigDirectorOnRichPresenceUpdated(IntPtr cPtr, IntPtr userID)
		{
			if (IRichPresenceListener.listeners.ContainsKey(cPtr))
			{
				IRichPresenceListener.listeners[cPtr].OnRichPresenceUpdated(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x04000271 RID: 625
		private static Dictionary<IntPtr, IRichPresenceListener> listeners = new Dictionary<IntPtr, IRichPresenceListener>();

		// Token: 0x04000272 RID: 626
		private HandleRef swigCPtr;

		// Token: 0x04000273 RID: 627
		private IRichPresenceListener.SwigDelegateIRichPresenceListener_0 swigDelegate0;

		// Token: 0x04000274 RID: 628
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x02000151 RID: 337
		// (Invoke) Token: 0x06000C98 RID: 3224
		public delegate void SwigDelegateIRichPresenceListener_0(IntPtr cPtr, IntPtr userID);
	}
}
