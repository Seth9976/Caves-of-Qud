using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000137 RID: 311
	public abstract class INatTypeDetectionListener : GalaxyTypeAwareListenerNatTypeDetection
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x0001289C File Offset: 0x00010A9C
		internal INatTypeDetectionListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INatTypeDetectionListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			INatTypeDetectionListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000128C4 File Offset: 0x00010AC4
		public INatTypeDetectionListener()
			: this(GalaxyInstancePINVOKE.new_INatTypeDetectionListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000128E8 File Offset: 0x00010AE8
		internal static HandleRef getCPtr(INatTypeDetectionListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00012908 File Offset: 0x00010B08
		~INatTypeDetectionListener()
		{
			this.Dispose();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00012938 File Offset: 0x00010B38
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INatTypeDetectionListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (INatTypeDetectionListener.listeners.ContainsKey(handle))
					{
						INatTypeDetectionListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000BE1 RID: 3041
		public abstract void OnNatTypeDetectionSuccess(NatType natType);

		// Token: 0x06000BE2 RID: 3042
		public abstract void OnNatTypeDetectionFailure();

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000129E8 File Offset: 0x00010BE8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnNatTypeDetectionSuccess", INatTypeDetectionListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_0(INatTypeDetectionListener.SwigDirectorOnNatTypeDetectionSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnNatTypeDetectionFailure", INatTypeDetectionListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_1(INatTypeDetectionListener.SwigDirectorOnNatTypeDetectionFailure);
			}
			GalaxyInstancePINVOKE.INatTypeDetectionListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00012A5C File Offset: 0x00010C5C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INatTypeDetectionListener));
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00012A92 File Offset: 0x00010C92
		[MonoPInvokeCallback(typeof(INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_0))]
		private static void SwigDirectorOnNatTypeDetectionSuccess(IntPtr cPtr, int natType)
		{
			if (INatTypeDetectionListener.listeners.ContainsKey(cPtr))
			{
				INatTypeDetectionListener.listeners[cPtr].OnNatTypeDetectionSuccess((NatType)natType);
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00012AB5 File Offset: 0x00010CB5
		[MonoPInvokeCallback(typeof(INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_1))]
		private static void SwigDirectorOnNatTypeDetectionFailure(IntPtr cPtr)
		{
			if (INatTypeDetectionListener.listeners.ContainsKey(cPtr))
			{
				INatTypeDetectionListener.listeners[cPtr].OnNatTypeDetectionFailure();
			}
		}

		// Token: 0x04000237 RID: 567
		private static Dictionary<IntPtr, INatTypeDetectionListener> listeners = new Dictionary<IntPtr, INatTypeDetectionListener>();

		// Token: 0x04000238 RID: 568
		private HandleRef swigCPtr;

		// Token: 0x04000239 RID: 569
		private INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_0 swigDelegate0;

		// Token: 0x0400023A RID: 570
		private INatTypeDetectionListener.SwigDelegateINatTypeDetectionListener_1 swigDelegate1;

		// Token: 0x0400023B RID: 571
		private static Type[] swigMethodTypes0 = new Type[] { typeof(NatType) };

		// Token: 0x0400023C RID: 572
		private static Type[] swigMethodTypes1 = new Type[0];

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x06000BE9 RID: 3049
		public delegate void SwigDelegateINatTypeDetectionListener_0(IntPtr cPtr, int natType);

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x06000BED RID: 3053
		public delegate void SwigDelegateINatTypeDetectionListener_1(IntPtr cPtr);
	}
}
