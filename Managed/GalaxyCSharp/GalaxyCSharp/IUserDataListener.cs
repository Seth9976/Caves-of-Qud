using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000172 RID: 370
	public abstract class IUserDataListener : GalaxyTypeAwareListenerUserData
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x000158F0 File Offset: 0x00013AF0
		internal IUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IUserDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00015918 File Offset: 0x00013B18
		public IUserDataListener()
			: this(GalaxyInstancePINVOKE.new_IUserDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0001593C File Offset: 0x00013B3C
		internal static HandleRef getCPtr(IUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0001595C File Offset: 0x00013B5C
		~IUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0001598C File Offset: 0x00013B8C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUserDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IUserDataListener.listeners.ContainsKey(handle))
					{
						IUserDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000DD9 RID: 3545
		public abstract void OnUserDataUpdated();

		// Token: 0x06000DDA RID: 3546 RVA: 0x00015A3C File Offset: 0x00013C3C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnUserDataUpdated", IUserDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IUserDataListener.SwigDelegateIUserDataListener_0(IUserDataListener.SwigDirectorOnUserDataUpdated);
			}
			GalaxyInstancePINVOKE.IUserDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00015A78 File Offset: 0x00013C78
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IUserDataListener));
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00015AAE File Offset: 0x00013CAE
		[MonoPInvokeCallback(typeof(IUserDataListener.SwigDelegateIUserDataListener_0))]
		private static void SwigDirectorOnUserDataUpdated(IntPtr cPtr)
		{
			if (IUserDataListener.listeners.ContainsKey(cPtr))
			{
				IUserDataListener.listeners[cPtr].OnUserDataUpdated();
			}
		}

		// Token: 0x040002C7 RID: 711
		private static Dictionary<IntPtr, IUserDataListener> listeners = new Dictionary<IntPtr, IUserDataListener>();

		// Token: 0x040002C8 RID: 712
		private HandleRef swigCPtr;

		// Token: 0x040002C9 RID: 713
		private IUserDataListener.SwigDelegateIUserDataListener_0 swigDelegate0;

		// Token: 0x040002CA RID: 714
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x02000173 RID: 371
		// (Invoke) Token: 0x06000DDF RID: 3551
		public delegate void SwigDelegateIUserDataListener_0(IntPtr cPtr);
	}
}
