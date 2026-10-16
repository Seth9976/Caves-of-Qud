using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000B6 RID: 182
	public abstract class IChatRoomMessagesRetrieveListener : GalaxyTypeAwareListenerChatRoomMessagesRetrieve
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
		internal IChatRoomMessagesRetrieveListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessagesRetrieveListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomMessagesRetrieveListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		public IChatRoomMessagesRetrieveListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessagesRetrieveListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		internal static HandleRef getCPtr(IChatRoomMessagesRetrieveListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000CF0C File Offset: 0x0000B10C
		~IChatRoomMessagesRetrieveListener()
		{
			this.Dispose();
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0000CF3C File Offset: 0x0000B13C
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomMessagesRetrieveListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomMessagesRetrieveListener.listeners.ContainsKey(handle))
					{
						IChatRoomMessagesRetrieveListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008A7 RID: 2215
		public abstract void OnChatRoomMessagesRetrieveSuccess(ulong chatRoomID, uint messageCount, uint longestMessageLenght);

		// Token: 0x060008A8 RID: 2216
		public abstract void OnChatRoomMessagesRetrieveFailure(ulong chatRoomID, IChatRoomMessagesRetrieveListener.FailureReason failureReason);

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessagesRetrieveSuccess", IChatRoomMessagesRetrieveListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_0(IChatRoomMessagesRetrieveListener.SwigDirectorOnChatRoomMessagesRetrieveSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessagesRetrieveFailure", IChatRoomMessagesRetrieveListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_1(IChatRoomMessagesRetrieveListener.SwigDirectorOnChatRoomMessagesRetrieveFailure);
			}
			GalaxyInstancePINVOKE.IChatRoomMessagesRetrieveListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0000D060 File Offset: 0x0000B260
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessagesRetrieveListener));
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0000D096 File Offset: 0x0000B296
		[MonoPInvokeCallback(typeof(IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_0))]
		private static void SwigDirectorOnChatRoomMessagesRetrieveSuccess(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght)
		{
			if (IChatRoomMessagesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessagesRetrieveListener.listeners[cPtr].OnChatRoomMessagesRetrieveSuccess(chatRoomID, messageCount, longestMessageLenght);
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0000D0BB File Offset: 0x0000B2BB
		[MonoPInvokeCallback(typeof(IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_1))]
		private static void SwigDirectorOnChatRoomMessagesRetrieveFailure(IntPtr cPtr, ulong chatRoomID, int failureReason)
		{
			if (IChatRoomMessagesRetrieveListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessagesRetrieveListener.listeners[cPtr].OnChatRoomMessagesRetrieveFailure(chatRoomID, (IChatRoomMessagesRetrieveListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040000F2 RID: 242
		private static Dictionary<IntPtr, IChatRoomMessagesRetrieveListener> listeners = new Dictionary<IntPtr, IChatRoomMessagesRetrieveListener>();

		// Token: 0x040000F3 RID: 243
		private HandleRef swigCPtr;

		// Token: 0x040000F4 RID: 244
		private IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_0 swigDelegate0;

		// Token: 0x040000F5 RID: 245
		private IChatRoomMessagesRetrieveListener.SwigDelegateIChatRoomMessagesRetrieveListener_1 swigDelegate1;

		// Token: 0x040000F6 RID: 246
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x040000F7 RID: 247
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(ulong),
			typeof(IChatRoomMessagesRetrieveListener.FailureReason)
		};

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x060008AF RID: 2223
		public delegate void SwigDelegateIChatRoomMessagesRetrieveListener_0(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght);

		// Token: 0x020000B8 RID: 184
		// (Invoke) Token: 0x060008B3 RID: 2227
		public delegate void SwigDelegateIChatRoomMessagesRetrieveListener_1(IntPtr cPtr, ulong chatRoomID, int failureReason);

		// Token: 0x020000B9 RID: 185
		public enum FailureReason
		{
			// Token: 0x040000F9 RID: 249
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040000FA RID: 250
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x040000FB RID: 251
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
