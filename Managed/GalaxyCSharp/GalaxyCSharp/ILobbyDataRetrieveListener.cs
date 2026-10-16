using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x0200011C RID: 284
	public abstract class ILobbyDataRetrieveListener : GalaxyTypeAwareListenerLobbyDataRetrieve
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x00009248 File Offset: 0x00007448
		internal ILobbyDataRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ILobbyDataRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ILobbyDataRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00009270 File Offset: 0x00007470
		public ILobbyDataRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_ILobbyDataRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00009294 File Offset: 0x00007494
		internal static HandleRef getCPtr(ILobbyDataRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000092B4 File Offset: 0x000074B4
		~ILobbyDataRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000092E4 File Offset: 0x000074E4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ILobbyDataRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ILobbyDataRetrieveListener.listeners.ContainsKey(handle))
					{
						ILobbyDataRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000B12 RID: 2834
		public abstract void OnLobbyDataRetrieveSuccess(GalaxyID lobbyID);

		// Token: 0x06000B13 RID: 2835
		public abstract void OnLobbyDataRetrieveFailure(GalaxyID lobbyID, ILobbyDataRetrieveListener.FailureReason failureReason);

		// Token: 0x06000B14 RID: 2836 RVA: 0x00009394 File Offset: 0x00007594
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnLobbyDataRetrieveSuccess", ILobbyDataRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_0(ILobbyDataRetrieveListener.SwigDirectorOnLobbyDataRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnLobbyDataRetrieveFailure", ILobbyDataRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_1(ILobbyDataRetrieveListener.SwigDirectorOnLobbyDataRetrieveFailure);
			}
			GalaxyInstancePINVOKE.ILobbyDataRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00009408 File Offset: 0x00007608
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ILobbyDataRetrieveListener));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0000943E File Offset: 0x0000763E
		[MonoPInvokeCallback(typeof(ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_0))]
		private static void SwigDirectorOnLobbyDataRetrieveSuccess(IntPtr cPtr, IntPtr lobbyID)
		{
			if (ILobbyDataRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataRetrieveListener.listeners[cPtr].OnLobbyDataRetrieveSuccess(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()));
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00009471 File Offset: 0x00007671
		[MonoPInvokeCallback(typeof(ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_1))]
		private static void SwigDirectorOnLobbyDataRetrieveFailure(IntPtr cPtr, IntPtr lobbyID, int failureReason)
		{
			if (ILobbyDataRetrieveListener.listeners.ContainsKey(cPtr))
			{
				ILobbyDataRetrieveListener.listeners[cPtr].OnLobbyDataRetrieveFailure(new GalaxyID(new GalaxyID(lobbyID, false).ToUint64()), (ILobbyDataRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040001F8 RID: 504
		private static Dictionary<IntPtr, ILobbyDataRetrieveListener> listeners = new Dictionary<IntPtr, ILobbyDataRetrieveListener>();

		// Token: 0x040001F9 RID: 505
		private HandleRef swigCPtr;

		// Token: 0x040001FA RID: 506
		private ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_0 swigDelegate0;

		// Token: 0x040001FB RID: 507
		private ILobbyDataRetrieveListener.SwigDelegateILobbyDataRetrieveListener_1 swigDelegate1;

		// Token: 0x040001FC RID: 508
		private static Type[] swigMethodTypes0 = new Type[] { typeof(GalaxyID) };

		// Token: 0x040001FD RID: 509
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ILobbyDataRetrieveListener.FailureReason)
		};

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x06000B1A RID: 2842
		public delegate void SwigDelegateILobbyDataRetrieveListener_0(IntPtr cPtr, IntPtr lobbyID);

		// Token: 0x0200011E RID: 286
		// (Invoke) Token: 0x06000B1E RID: 2846
		public delegate void SwigDelegateILobbyDataRetrieveListener_1(IntPtr cPtr, IntPtr lobbyID, int failureReason);

		// Token: 0x0200011F RID: 287
		public enum FailureReason
		{
			// Token: 0x040001FF RID: 511
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000200 RID: 512
			FAILURE_REASON_LOBBY_DOES_NOT_EXIST,
			// Token: 0x04000201 RID: 513
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
