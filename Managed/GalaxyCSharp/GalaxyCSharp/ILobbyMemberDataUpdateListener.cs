using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200012B RID: 299
	public abstract class ILobbyMemberDataUpdateListener : GalaxyTypeAwareListenerLobbyMemberDataUpdate
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x0000A0A8 File Offset: 0x000082A8
		internal ILobbyMemberDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyMemberDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyMemberDataUpdateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public ILobbyMemberDataUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyMemberDataUpdateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0000A0F4 File Offset: 0x000082F4
		internal static HandleRef getCPtr(ILobbyMemberDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0000A114 File Offset: 0x00008314
		~ILobbyMemberDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0000A144 File Offset: 0x00008344
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyMemberDataUpdateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyMemberDataUpdateListener.listeners.ContainsKey(handle))
					{
						ILobbyMemberDataUpdateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B64 RID: 2916
		public abstract void OnLobbyMemberDataUpdateSuccess(GalaxyID lobbyID, GalaxyID memberID);

		// Token: 0x06000B65 RID: 2917
		public abstract void OnLobbyMemberDataUpdateFailure(GalaxyID lobbyID, GalaxyID memberID, ILobbyMemberDataUpdateListener.FailureReason failureReason);

		// Token: 0x06000B66 RID: 2918 RVA: 0x0000A1F4 File Offset: 0x000083F4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyMemberDataUpdateSuccess", ILobbyMemberDataUpdateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_0(ILobbyMemberDataUpdateListener.SwigDirectorOnLobbyMemberDataUpdateSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLobbyMemberDataUpdateFailure", ILobbyMemberDataUpdateListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_1(ILobbyMemberDataUpdateListener.SwigDirectorOnLobbyMemberDataUpdateFailure);
			}
			GalaxyInstancePINVOKE.ILobbyMemberDataUpdateListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0000A268 File Offset: 0x00008468
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyMemberDataUpdateListener));
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0000A2A0 File Offset: 0x000084A0
		[MonoPInvokeCallback(typeof(ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_0))]
		private static void SwigDirectorOnLobbyMemberDataUpdateSuccess(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID)
		{
			if (ILobbyMemberDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMemberDataUpdateListener.listeners[cPtr].OnLobbyMemberDataUpdateSuccess(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()));
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0000A2F0 File Offset: 0x000084F0
		[MonoPInvokeCallback(typeof(ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_1))]
		private static void SwigDirectorOnLobbyMemberDataUpdateFailure(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int failureReason)
		{
			if (ILobbyMemberDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyMemberDataUpdateListener.listeners[cPtr].OnLobbyMemberDataUpdateFailure(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), new GalaxyID(new GalaxyID(memberID, false).ToUint64()), (ILobbyMemberDataUpdateListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400021D RID: 541
		private static Dictionary<IntPtr, ILobbyMemberDataUpdateListener> listeners = new Dictionary<IntPtr, ILobbyMemberDataUpdateListener>();

		// Token: 0x0400021E RID: 542
		private HandleRef swigCPtr;

		// Token: 0x0400021F RID: 543
		private ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_0 swigDelegate0;

		// Token: 0x04000220 RID: 544
		private ILobbyMemberDataUpdateListener.SwigDelegateILobbyMemberDataUpdateListener_1 swigDelegate1;

		// Token: 0x04000221 RID: 545
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID)
		};

		// Token: 0x04000222 RID: 546
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(GalaxyID),
			typeof(ILobbyMemberDataUpdateListener.FailureReason)
		};

		// Token: 0x0200012C RID: 300
		// (Invoke) Token: 0x06000B6C RID: 2924
		public delegate void SwigDelegateILobbyMemberDataUpdateListener_0(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID);

		// Token: 0x0200012D RID: 301
		// (Invoke) Token: 0x06000B70 RID: 2928
		public delegate void SwigDelegateILobbyMemberDataUpdateListener_1(IntPtr cPtr, IntPtr lobbyID, IntPtr memberID, int failureReason);

		// Token: 0x0200012E RID: 302
		public enum FailureReason
		{
			// Token: 0x04000224 RID: 548
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000225 RID: 549
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			// Token: 0x04000226 RID: 550
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
