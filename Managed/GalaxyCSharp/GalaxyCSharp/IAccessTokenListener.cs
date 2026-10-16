using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A5 RID: 165
	public abstract class IAccessTokenListener : GalaxyTypeAwareListenerAccessToken
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x00007D8C File Offset: 0x00005F8C
		internal IAccessTokenListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAccessTokenListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IAccessTokenListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public IAccessTokenListener()
			: this(GalaxyInstancePINVOKE.new_IAccessTokenListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00007DD8 File Offset: 0x00005FD8
		internal static HandleRef getCPtr(IAccessTokenListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00007DF8 File Offset: 0x00005FF8
		~IAccessTokenListener()
		{
			this.Dispose();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00007E28 File Offset: 0x00006028
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IAccessTokenListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IAccessTokenListener.listeners.ContainsKey(handle))
					{
						IAccessTokenListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000832 RID: 2098
		public abstract void OnAccessTokenChanged();

		// Token: 0x06000833 RID: 2099 RVA: 0x00007ED8 File Offset: 0x000060D8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnAccessTokenChanged", IAccessTokenListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IAccessTokenListener.SwigDelegateIAccessTokenListener_0(IAccessTokenListener.SwigDirectorOnAccessTokenChanged);
			}
			GalaxyInstancePINVOKE.IAccessTokenListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00007F14 File Offset: 0x00006114
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAccessTokenListener));
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00007F4A File Offset: 0x0000614A
		[MonoPInvokeCallback(typeof(IAccessTokenListener.SwigDelegateIAccessTokenListener_0))]
		private static void SwigDirectorOnAccessTokenChanged(IntPtr cPtr)
		{
			if (IAccessTokenListener.listeners.ContainsKey(cPtr))
			{
				IAccessTokenListener.listeners[cPtr].OnAccessTokenChanged();
			}
		}

		// Token: 0x040000C7 RID: 199
		private static Dictionary<IntPtr, IAccessTokenListener> listeners = new Dictionary<IntPtr, IAccessTokenListener>();

		// Token: 0x040000C8 RID: 200
		private HandleRef swigCPtr;

		// Token: 0x040000C9 RID: 201
		private IAccessTokenListener.SwigDelegateIAccessTokenListener_0 swigDelegate0;

		// Token: 0x040000CA RID: 202
		private static Type[] swigMethodTypes0 = new Type[0];

		// Token: 0x020000A6 RID: 166
		// (Invoke) Token: 0x06000838 RID: 2104
		public delegate void SwigDelegateIAccessTokenListener_0(IntPtr cPtr);
	}
}
