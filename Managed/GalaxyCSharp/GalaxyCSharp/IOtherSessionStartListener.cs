using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000143 RID: 323
	public abstract class IOtherSessionStartListener : GalaxyTypeAwareListenerOtherSessionStart
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x000131DC File Offset: 0x000113DC
		internal IOtherSessionStartListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOtherSessionStartListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOtherSessionStartListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00013204 File Offset: 0x00011404
		public IOtherSessionStartListener()
			: this(GalaxyInstancePINVOKE.new_IOtherSessionStartListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00013228 File Offset: 0x00011428
		internal static HandleRef getCPtr(IOtherSessionStartListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00013248 File Offset: 0x00011448
		~IOtherSessionStartListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00013278 File Offset: 0x00011478
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOtherSessionStartListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOtherSessionStartListener.listeners.ContainsKey(handle))
					{
						IOtherSessionStartListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C46 RID: 3142
		public abstract void OnOtherSessionStarted();

		// Token: 0x06000C47 RID: 3143 RVA: 0x00013328 File Offset: 0x00011528
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOtherSessionStarted", IOtherSessionStartListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOtherSessionStartListener.SwigDelegateIOtherSessionStartListener_0(IOtherSessionStartListener.SwigDirectorOnOtherSessionStarted);
			}
			GalaxyInstancePINVOKE.IOtherSessionStartListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00013364 File Offset: 0x00011564
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOtherSessionStartListener));
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0001339A File Offset: 0x0001159A
		[MonoPInvokeCallback(typeof(IOtherSessionStartListener.SwigDelegateIOtherSessionStartListener_0))]
		private static void SwigDirectorOnOtherSessionStarted(IntPtr cPtr)
		{
			if (IOtherSessionStartListener.listeners.ContainsKey(cPtr))
			{
				IOtherSessionStartListener.listeners[cPtr].OnOtherSessionStarted();
			}
		}

		// Token: 0x04000250 RID: 592
		private static Dictionary<IntPtr, IOtherSessionStartListener> listeners = new Dictionary<IntPtr, IOtherSessionStartListener>();

		// Token: 0x04000251 RID: 593
		private HandleRef swigCPtr;

		// Token: 0x04000252 RID: 594
		private IOtherSessionStartListener.SwigDelegateIOtherSessionStartListener_0 swigDelegate0;

		// Token: 0x04000253 RID: 595
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x02000144 RID: 324
		// (Invoke) Token: 0x06000C4C RID: 3148
		public delegate void SwigDelegateIOtherSessionStartListener_0(IntPtr cPtr);
	}
}
