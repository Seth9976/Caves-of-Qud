using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000120 RID: 288
	public abstract class ILobbyDataUpdateListener : GalaxyTypeAwareListenerLobbyDataUpdate
	{
		// Token: 0x06000B21 RID: 2849 RVA: 0x00009628 File Offset: 0x00007828
		internal ILobbyDataUpdateListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataUpdateListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyDataUpdateListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00009650 File Offset: 0x00007850
		public ILobbyDataUpdateListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataUpdateListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00009674 File Offset: 0x00007874
		internal static HandleRef getCPtr(ILobbyDataUpdateListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00009694 File Offset: 0x00007894
		~ILobbyDataUpdateListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000096C4 File Offset: 0x000078C4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyDataUpdateListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyDataUpdateListener.listeners.ContainsKey(handle))
					{
						ILobbyDataUpdateListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B26 RID: 2854
		public abstract void OnLobbyDataUpdateSuccess(GalaxyID lobbyID);

		// Token: 0x06000B27 RID: 2855
		public abstract void OnLobbyDataUpdateFailure(GalaxyID lobbyID, ILobbyDataUpdateListener.FailureReason failureReason);

		// Token: 0x06000B28 RID: 2856 RVA: 0x00009774 File Offset: 0x00007974
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyDataUpdateSuccess", ILobbyDataUpdateListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_0(ILobbyDataUpdateListener.SwigDirectorOnLobbyDataUpdateSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLobbyDataUpdateFailure", ILobbyDataUpdateListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_1(ILobbyDataUpdateListener.SwigDirectorOnLobbyDataUpdateFailure);
			}
			GalaxyInstancePINVOKE.ILobbyDataUpdateListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000097E8 File Offset: 0x000079E8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataUpdateListener));
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0000981E File Offset: 0x00007A1E
		[MonoPInvokeCallback(typeof(ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_0))]
		private static void SwigDirectorOnLobbyDataUpdateSuccess(IntPtr cPtr, IntPtr lobbyID)
		{
			if (ILobbyDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataUpdateListener.listeners[cPtr].OnLobbyDataUpdateSuccess(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()));
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00009851 File Offset: 0x00007A51
		[MonoPInvokeCallback(typeof(ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_1))]
		private static void SwigDirectorOnLobbyDataUpdateFailure(IntPtr cPtr, IntPtr lobbyID, int failureReason)
		{
			if (ILobbyDataUpdateListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataUpdateListener.listeners[cPtr].OnLobbyDataUpdateFailure(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (ILobbyDataUpdateListener.FailureReason)failureReason);
			}
		}

		// Token: 0x04000202 RID: 514
		private static Dictionary<IntPtr, ILobbyDataUpdateListener> listeners = new Dictionary<IntPtr, ILobbyDataUpdateListener>();

		// Token: 0x04000203 RID: 515
		private HandleRef swigCPtr;

		// Token: 0x04000204 RID: 516
		private ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_0 swigDelegate0;

		// Token: 0x04000205 RID: 517
		private ILobbyDataUpdateListener.SwigDelegateILobbyDataUpdateListener_1 swigDelegate1;

		// Token: 0x04000206 RID: 518
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x04000207 RID: 519
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ILobbyDataUpdateListener.FailureReason)
		};

		// Token: 0x02000121 RID: 289
		// (Invoke) Token: 0x06000B2E RID: 2862
		public delegate void SwigDelegateILobbyDataUpdateListener_0(IntPtr cPtr, IntPtr lobbyID);

		// Token: 0x02000122 RID: 290
		// (Invoke) Token: 0x06000B32 RID: 2866
		public delegate void SwigDelegateILobbyDataUpdateListener_1(IntPtr cPtr, IntPtr lobbyID, int failureReason);

		// Token: 0x02000123 RID: 291
		public enum FailureReason
		{
			// Token: 0x04000209 RID: 521
			FAILURE_REASON_UNDEFINED,
			// Token: 0x0400020A RID: 522
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			// Token: 0x0400020B RID: 523
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
