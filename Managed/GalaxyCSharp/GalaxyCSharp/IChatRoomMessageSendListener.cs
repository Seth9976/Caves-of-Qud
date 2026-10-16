using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000B0 RID: 176
	public abstract class IChatRoomMessageSendListener : GalaxyTypeAwareListenerChatRoomMessageSend
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x0000C758 File Offset: 0x0000A958
		internal IChatRoomMessageSendListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessageSendListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomMessageSendListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0000C780 File Offset: 0x0000A980
		public IChatRoomMessageSendListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessageSendListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		internal static HandleRef getCPtr(IChatRoomMessageSendListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		~IChatRoomMessageSendListener()
		{
			this.Dispose();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomMessageSendListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomMessageSendListener.listeners.ContainsKey(handle))
					{
						IChatRoomMessageSendListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000885 RID: 2181
		public abstract void OnChatRoomMessageSendSuccess(ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime);

		// Token: 0x06000886 RID: 2182
		public abstract void OnChatRoomMessageSendFailure(ulong chatRoomID, uint sentMessageIndex, IChatRoomMessageSendListener.FailureReason failureReason);

		// Token: 0x06000887 RID: 2183 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessageSendSuccess", IChatRoomMessageSendListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_0(IChatRoomMessageSendListener.SwigDirectorOnChatRoomMessageSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessageSendFailure", IChatRoomMessageSendListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_1(IChatRoomMessageSendListener.SwigDirectorOnChatRoomMessageSendFailure);
			}
			GalaxyInstancePINVOKE.IChatRoomMessageSendListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0000C918 File Offset: 0x0000AB18
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessageSendListener));
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0000C94E File Offset: 0x0000AB4E
		[MonoPInvokeCallback(typeof(IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_0))]
		private static void SwigDirectorOnChatRoomMessageSendSuccess(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime)
		{
			if (IChatRoomMessageSendListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessageSendListener.listeners[cPtr].OnChatRoomMessageSendSuccess(chatRoomID, sentMessageIndex, messageID, sendTime);
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0000C975 File Offset: 0x0000AB75
		[MonoPInvokeCallback(typeof(IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_1))]
		private static void SwigDirectorOnChatRoomMessageSendFailure(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, int failureReason)
		{
			if (IChatRoomMessageSendListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessageSendListener.listeners[cPtr].OnChatRoomMessageSendFailure(chatRoomID, sentMessageIndex, (IChatRoomMessageSendListener.FailureReason)failureReason);
			}
		}

		// Token: 0x040000E4 RID: 228
		private static Dictionary<IntPtr, IChatRoomMessageSendListener> listeners = new Dictionary<IntPtr, IChatRoomMessageSendListener>();

		// Token: 0x040000E5 RID: 229
		private HandleRef swigCPtr;

		// Token: 0x040000E6 RID: 230
		private IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_0 swigDelegate0;

		// Token: 0x040000E7 RID: 231
		private IChatRoomMessageSendListener.SwigDelegateIChatRoomMessageSendListener_1 swigDelegate1;

		// Token: 0x040000E8 RID: 232
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(ulong),
			typeof(uint)
		};

		// Token: 0x040000E9 RID: 233
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(IChatRoomMessageSendListener.FailureReason)
		};

		// Token: 0x020000B1 RID: 177
		// (Invoke) Token: 0x0600088D RID: 2189
		public delegate void SwigDelegateIChatRoomMessageSendListener_0(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, ulong messageID, uint sendTime);

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x06000891 RID: 2193
		public delegate void SwigDelegateIChatRoomMessageSendListener_1(IntPtr cPtr, ulong chatRoomID, uint sentMessageIndex, int failureReason);

		// Token: 0x020000B3 RID: 179
		public enum FailureReason
		{
			// Token: 0x040000EB RID: 235
			FAILURE_REASON_UNDEFINED,
			// Token: 0x040000EC RID: 236
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x040000ED RID: 237
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
