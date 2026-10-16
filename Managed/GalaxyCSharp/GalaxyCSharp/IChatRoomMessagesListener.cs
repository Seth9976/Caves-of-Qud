using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000B4 RID: 180
	public abstract class IChatRoomMessagesListener : GalaxyTypeAwareListenerChatRoomMessages
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x0000CB50 File Offset: 0x0000AD50
		internal IChatRoomMessagesListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IChatRoomMessagesListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IChatRoomMessagesListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0000CB78 File Offset: 0x0000AD78
		public IChatRoomMessagesListener()
			: this(GalaxyInstancePINVOKE.new_IChatRoomMessagesListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		internal static HandleRef getCPtr(IChatRoomMessagesListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		~IChatRoomMessagesListener()
		{
			this.Dispose();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChatRoomMessagesListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IChatRoomMessagesListener.listeners.ContainsKey(handle))
					{
						IChatRoomMessagesListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000899 RID: 2201
		public abstract void OnChatRoomMessagesReceived(ulong chatRoomID, uint messageCount, uint longestMessageLenght);

		// Token: 0x0600089A RID: 2202 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnChatRoomMessagesReceived", IChatRoomMessagesListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IChatRoomMessagesListener.SwigDelegateIChatRoomMessagesListener_0(IChatRoomMessagesListener.SwigDirectorOnChatRoomMessagesReceived);
			}
			GalaxyInstancePINVOKE.IChatRoomMessagesListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IChatRoomMessagesListener));
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		[MonoPInvokeCallback(typeof(IChatRoomMessagesListener.SwigDelegateIChatRoomMessagesListener_0))]
		private static void SwigDirectorOnChatRoomMessagesReceived(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght)
		{
			if (IChatRoomMessagesListener.listeners.ContainsKey(cPtr))
			{
				IChatRoomMessagesListener.listeners[cPtr].OnChatRoomMessagesReceived(chatRoomID, messageCount, longestMessageLenght);
			}
		}

		// Token: 0x040000EE RID: 238
		private static Dictionary<IntPtr, IChatRoomMessagesListener> listeners = new Dictionary<IntPtr, IChatRoomMessagesListener>();

		// Token: 0x040000EF RID: 239
		private HandleRef swigCPtr;

		// Token: 0x040000F0 RID: 240
		private IChatRoomMessagesListener.SwigDelegateIChatRoomMessagesListener_0 swigDelegate0;

		// Token: 0x040000F1 RID: 241
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(ulong),
			typeof(uint),
			typeof(uint)
		};

		// Token: 0x020000B5 RID: 181
		// (Invoke) Token: 0x0600089F RID: 2207
		public delegate void SwigDelegateIChatRoomMessagesListener_0(IntPtr cPtr, ulong chatRoomID, uint messageCount, uint longestMessageLenght);
	}
}
