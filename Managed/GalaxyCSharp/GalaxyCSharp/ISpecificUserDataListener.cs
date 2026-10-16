using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000163 RID: 355
	public abstract class ISpecificUserDataListener : GalaxyTypeAwareListenerSpecificUserData
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x0000B68C File Offset: 0x0000988C
		internal ISpecificUserDataListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISpecificUserDataListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISpecificUserDataListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0000B6B4 File Offset: 0x000098B4
		public ISpecificUserDataListener()
			: this(GalaxyInstancePINVOKE.new_ISpecificUserDataListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0000B6D8 File Offset: 0x000098D8
		internal static HandleRef getCPtr(ISpecificUserDataListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0000B6F8 File Offset: 0x000098F8
		~ISpecificUserDataListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0000B728 File Offset: 0x00009928
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISpecificUserDataListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISpecificUserDataListener.listeners.ContainsKey(handle))
					{
						ISpecificUserDataListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CF4 RID: 3316
		public abstract void OnSpecificUserDataUpdated(GalaxyID userID);

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0000B7D8 File Offset: 0x000099D8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnSpecificUserDataUpdated", ISpecificUserDataListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISpecificUserDataListener.SwigDelegateISpecificUserDataListener_0(ISpecificUserDataListener.SwigDirectorOnSpecificUserDataUpdated);
			}
			GalaxyInstancePINVOKE.ISpecificUserDataListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0000B814 File Offset: 0x00009A14
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISpecificUserDataListener));
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0000B84A File Offset: 0x00009A4A
		[MonoPInvokeCallback(typeof(ISpecificUserDataListener.SwigDelegateISpecificUserDataListener_0))]
		private static void SwigDirectorOnSpecificUserDataUpdated(IntPtr cPtr, IntPtr userID)
		{
			if (ISpecificUserDataListener.listeners.ContainsKey(cPtr))
			{
				ISpecificUserDataListener.listeners[cPtr].OnSpecificUserDataUpdated(new GalaxyID(new GalaxyID(userID, false).ToUint64()));
			}
		}

		// Token: 0x0400029F RID: 671
		private static Dictionary<IntPtr, ISpecificUserDataListener> listeners = new Dictionary<IntPtr, ISpecificUserDataListener>();

		// Token: 0x040002A0 RID: 672
		private HandleRef swigCPtr;

		// Token: 0x040002A1 RID: 673
		private ISpecificUserDataListener.SwigDelegateISpecificUserDataListener_0 swigDelegate0;

		// Token: 0x040002A2 RID: 674
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x02000164 RID: 356
		// (Invoke) Token: 0x06000CFA RID: 3322
		public delegate void SwigDelegateISpecificUserDataListener_0(IntPtr cPtr, IntPtr userID);
	}
}
