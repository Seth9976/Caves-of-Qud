using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200013E RID: 318
	public abstract class INotificationListener : GalaxyTypeAwareListenerNotification
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x00012D60 File Offset: 0x00010F60
		internal INotificationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.INotificationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			INotificationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00012D88 File Offset: 0x00010F88
		public INotificationListener()
			: this(GalaxyInstancePINVOKE.new_INotificationListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00012DAC File Offset: 0x00010FAC
		internal static HandleRef getCPtr(INotificationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00012DCC File Offset: 0x00010FCC
		~INotificationListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00012DFC File Offset: 0x00010FFC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_INotificationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (INotificationListener.listeners.ContainsKey(handle))
					{
						INotificationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C2A RID: 3114
		public abstract void OnNotificationReceived(ulong notificationID, uint typeLength, uint contentSize);

		// Token: 0x06000C2B RID: 3115 RVA: 0x00012EAC File Offset: 0x000110AC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnNotificationReceived", INotificationListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new INotificationListener.SwigDelegateINotificationListener_0(INotificationListener.SwigDirectorOnNotificationReceived);
			}
			GalaxyInstancePINVOKE.INotificationListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00012EE8 File Offset: 0x000110E8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(INotificationListener));
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00012F1E File Offset: 0x0001111E
		[MonoPInvokeCallback(typeof(INotificationListener.SwigDelegateINotificationListener_0))]
		private static void SwigDirectorOnNotificationReceived(IntPtr cPtr, ulong notificationID, uint typeLength, uint contentSize)
		{
			if (INotificationListener.listeners.ContainsKey(cPtr))
			{
				INotificationListener.listeners[cPtr].OnNotificationReceived(notificationID, typeLength, contentSize);
			}
		}

		// Token: 0x04000245 RID: 581
		private static Dictionary<IntPtr, INotificationListener> listeners = new Dictionary<IntPtr, INotificationListener>();

		// Token: 0x04000246 RID: 582
		private HandleRef swigCPtr;

		// Token: 0x04000247 RID: 583
		private INotificationListener.SwigDelegateINotificationListener_0 swigDelegate0;

		// Token: 0x04000248 RID: 584
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x0200013F RID: 319
		// (Invoke) Token: 0x06000C30 RID: 3120
		public delegate void SwigDelegateINotificationListener_0(IntPtr cPtr, ulong notificationID, uint typeLength, uint contentSize);
	}
}
