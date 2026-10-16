using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000140 RID: 320
	public abstract class IOperationalStateChangeListener : GalaxyTypeAwareListenerOperationalStateChange
	{
		// Token: 0x06000C33 RID: 3123 RVA: 0x0000AF30 File Offset: 0x00009130
		internal IOperationalStateChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IOperationalStateChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IOperationalStateChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0000AF58 File Offset: 0x00009158
		public IOperationalStateChangeListener()
			: this(GalaxyInstancePINVOKE.new_IOperationalStateChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0000AF7C File Offset: 0x0000917C
		internal static HandleRef getCPtr(IOperationalStateChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0000AF9C File Offset: 0x0000919C
		~IOperationalStateChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0000AFCC File Offset: 0x000091CC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IOperationalStateChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IOperationalStateChangeListener.listeners.ContainsKey(handle))
					{
						IOperationalStateChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C38 RID: 3128
		public abstract void OnOperationalStateChanged(uint operationalState);

		// Token: 0x06000C39 RID: 3129 RVA: 0x0000B07C File Offset: 0x0000927C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnOperationalStateChanged", IOperationalStateChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IOperationalStateChangeListener.SwigDelegateIOperationalStateChangeListener_0(IOperationalStateChangeListener.SwigDirectorOnOperationalStateChanged);
			}
			GalaxyInstancePINVOKE.IOperationalStateChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0000B0B8 File Offset: 0x000092B8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IOperationalStateChangeListener));
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0000B0EE File Offset: 0x000092EE
		[MonoPInvokeCallback(typeof(IOperationalStateChangeListener.SwigDelegateIOperationalStateChangeListener_0))]
		private static void SwigDirectorOnOperationalStateChanged(IntPtr cPtr, uint operationalState)
		{
			if (IOperationalStateChangeListener.listeners.ContainsKey(cPtr))
			{
				IOperationalStateChangeListener.listeners[cPtr].OnOperationalStateChanged(operationalState);
			}
		}

		// Token: 0x04000249 RID: 585
		private static Dictionary<IntPtr, IOperationalStateChangeListener> listeners = new Dictionary<IntPtr, IOperationalStateChangeListener>();

		// Token: 0x0400024A RID: 586
		private HandleRef swigCPtr;

		// Token: 0x0400024B RID: 587
		private IOperationalStateChangeListener.SwigDelegateIOperationalStateChangeListener_0 swigDelegate0;

		// Token: 0x0400024C RID: 588
		private static Type[] swigMethodTypes0 = new Type[] { typeof(uint) };

		// Token: 0x02000141 RID: 321
		// (Invoke) Token: 0x06000C3E RID: 3134
		public delegate void SwigDelegateIOperationalStateChangeListener_0(IntPtr cPtr, uint operationalState);

		// Token: 0x02000142 RID: 322
		public enum OperationalState
		{
			// Token: 0x0400024E RID: 590
			OPERATIONAL_STATE_SIGNED_IN = 1,
			// Token: 0x0400024F RID: 591
			OPERATIONAL_STATE_LOGGED_ON
		}
	}
}
