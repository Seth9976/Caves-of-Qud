using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000BA RID: 186
	public abstract class IChatRoomWithUserRetrieveListener : GalaxyTypeAwareListenerChatRoomWithUserRetrieve
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x0000D27C File Offset: 0x0000B47C
		internal IChatRoomWithUserRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomWithUserRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomWithUserRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public IChatRoomWithUserRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomWithUserRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		internal static HandleRef getCPtr(IChatRoomWithUserRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		~IChatRoomWithUserRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000D318 File Offset: 0x0000B518
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomWithUserRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomWithUserRetrieveListener.listeners.ContainsKey(handle))
					{
						IChatRoomWithUserRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008BB RID: 2235
		public abstract void OnChatRoomWithUserRetrieveSuccess(GalaxyID userID, ulong chatRoomID);

		// Token: 0x060008BC RID: 2236
		public abstract void OnChatRoomWithUserRetrieveFailure(GalaxyID userID, IChatRoomWithUserRetrieveListener.FailureReason failureReason);

		// Token: 0x060008BD RID: 2237 RVA: 0x0000D3C8 File Offset: 0x0000B5C8
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomWithUserRetrieveSuccess", IChatRoomWithUserRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_0(IChatRoomWithUserRetrieveListener.SwigDirectorOnChatRoomWithUserRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnChatRoomWithUserRetrieveFailure", IChatRoomWithUserRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_1(IChatRoomWithUserRetrieveListener.SwigDirectorOnChatRoomWithUserRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IChatRoomWithUserRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0000D43C File Offset: 0x0000B63C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomWithUserRetrieveListener));
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0000D472 File Offset: 0x0000B672
		[MonoPInvokeCallback(typeof(IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_0))]
		private static void SwigDirectorOnChatRoomWithUserRetrieveSuccess(IntPtr cPtr, IntPtr userID, ulong chatRoomID)
		{
			if (IChatRoomWithUserRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomWithUserRetrieveListener.listeners[cPtr].OnChatRoomWithUserRetrieveSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()), chatRoomID);
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		[MonoPInvokeCallback(typeof(IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_1))]
		private static void SwigDirectorOnChatRoomWithUserRetrieveFailure(IntPtr cPtr, IntPtr userID, int failureReason)
		{
			if (IChatRoomWithUserRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomWithUserRetrieveListener.listeners[cPtr].OnChatRoomWithUserRetrieveFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), (IChatRoomWithUserRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040000FC RID: 252
		private static Dictionary<IntPtr, IChatRoomWithUserRetrieveListener> listeners = new Dictionary<IntPtr, IChatRoomWithUserRetrieveListener>();

		// Token: 0x040000FD RID: 253
		private HandleRef swigCPtr;

		// Token: 0x040000FE RID: 254
		private IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_0 swigDelegate0;

		// Token: 0x040000FF RID: 255
		private IChatRoomWithUserRetrieveListener.SwigDelegateIChatRoomWithUserRetrieveListener_1 swigDelegate1;

		// Token: 0x04000100 RID: 256
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(ulong)
		};

		// Token: 0x04000101 RID: 257
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(IChatRoomWithUserRetrieveListener.FailureReason)
		};

		// Token: 0x020000BB RID: 187
		// (Invoke) Token: 0x060008C3 RID: 2243
		public delegate void SwigDelegateIChatRoomWithUserRetrieveListener_0(IntPtr cPtr, IntPtr userID, ulong chatRoomID);

		// Token: 0x020000BC RID: 188
		// (Invoke) Token: 0x060008C7 RID: 2247
		public delegate void SwigDelegateIChatRoomWithUserRetrieveListener_1(IntPtr cPtr, IntPtr userID, int failureReason);

		// Token: 0x020000BD RID: 189
		public enum FailureReason
		{
			// Token: 0x04000103 RID: 259
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000104 RID: 260
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000105 RID: 261
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
