using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000101 RID: 257
	public abstract class IGameJoinRequestedListener : GalaxyTypeAwareListenerGameJoinRequested
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x000103C8 File Offset: 0x0000E5C8
		internal IGameJoinRequestedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IGameJoinRequestedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IGameJoinRequestedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000103F0 File Offset: 0x0000E5F0
		public IGameJoinRequestedListener()
			: this(GalaxyInstancePINVOKE.new_IGameJoinRequestedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00010414 File Offset: 0x0000E614
		internal static HandleRef getCPtr(IGameJoinRequestedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00010434 File Offset: 0x0000E634
		~IGameJoinRequestedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00010464 File Offset: 0x0000E664
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IGameJoinRequestedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IGameJoinRequestedListener.listeners.ContainsKey(handle))
					{
						IGameJoinRequestedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000A7C RID: 2684
		public abstract void OnGameJoinRequested(GalaxyID userID, string connectionString);

		// Token: 0x06000A7D RID: 2685 RVA: 0x00010514 File Offset: 0x0000E714
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGameJoinRequested", IGameJoinRequestedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IGameJoinRequestedListener.SwigDelegateIGameJoinRequestedListener_0(IGameJoinRequestedListener.SwigDirectorOnGameJoinRequested);
			}
			GalaxyInstancePINVOKE.IGameJoinRequestedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00010550 File Offset: 0x0000E750
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IGameJoinRequestedListener));
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00010586 File Offset: 0x0000E786
		[MonoPInvokeCallback(typeof(IGameJoinRequestedListener.SwigDelegateIGameJoinRequestedListener_0))]
		private static void SwigDirectorOnGameJoinRequested(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString)
		{
			if (IGameJoinRequestedListener.listeners.ContainsKey(cPtr))
			{
				IGameJoinRequestedListener.listeners[cPtr].OnGameJoinRequested(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString);
			}
		}

		// Token: 0x040001BE RID: 446
		private static Dictionary<IntPtr, IGameJoinRequestedListener> listeners = new Dictionary<IntPtr, IGameJoinRequestedListener>();

		// Token: 0x040001BF RID: 447
		private HandleRef swigCPtr;

		// Token: 0x040001C0 RID: 448
		private IGameJoinRequestedListener.SwigDelegateIGameJoinRequestedListener_0 swigDelegate0;

		// Token: 0x040001C1 RID: 449
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string)
		};

		// Token: 0x02000102 RID: 258
		// (Invoke) Token: 0x06000A82 RID: 2690
		public delegate void SwigDelegateIGameJoinRequestedListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString);
	}
}
