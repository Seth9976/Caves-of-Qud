using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x020000AF RID: 175
	public class IChat : IDisposable
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x00016C48 File Offset: 0x00014E48
		internal IChat(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00016C64 File Offset: 0x00014E64
		internal static HandleRef getCPtr(IChat obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00016C84 File Offset: 0x00014E84
		~IChat()
		{
			this.Dispose();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00016CB4 File Offset: 0x00014EB4
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IChat(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00016D34 File Offset: 0x00014F34
		public virtual void RequestChatRoomWithUser(GalaxyID userID, IChatRoomWithUserRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomWithUser__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IChatRoomWithUserRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00016D5D File Offset: 0x00014F5D
		public virtual void RequestChatRoomWithUser(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomWithUser__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00016D80 File Offset: 0x00014F80
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit, ulong referenceMessageID, IChatRoomMessagesRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_0(this.swigCPtr, chatRoomID, limit, referenceMessageID, IChatRoomMessagesRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00016DA7 File Offset: 0x00014FA7
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit, ulong referenceMessageID)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_1(this.swigCPtr, chatRoomID, limit, referenceMessageID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00016DC7 File Offset: 0x00014FC7
		public virtual void RequestChatRoomMessages(ulong chatRoomID, uint limit)
		{
			GalaxyInstancePINVOKE.IChat_RequestChatRoomMessages__SWIG_2(this.swigCPtr, chatRoomID, limit);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00016DE8 File Offset: 0x00014FE8
		public virtual uint SendChatRoomMessage(ulong chatRoomID, string msg, IChatRoomMessageSendListener listener)
		{
			uint num = GalaxyInstancePINVOKE.IChat_SendChatRoomMessage__SWIG_0(this.swigCPtr, chatRoomID, msg, IChatRoomMessageSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00016E1C File Offset: 0x0001501C
		public virtual uint SendChatRoomMessage(ulong chatRoomID, string msg)
		{
			uint num = GalaxyInstancePINVOKE.IChat_SendChatRoomMessage__SWIG_1(this.swigCPtr, chatRoomID, msg);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00016E48 File Offset: 0x00015048
		public virtual uint GetChatRoomMessageByIndex(uint index, ref ulong messageID, ref ChatMessageType messageType, ref GalaxyID senderID, ref uint sendTime, out string buffer, uint bufferLength)
		{
			int num = 0;
			byte[] array = new byte[bufferLength];
			uint num3;
			try
			{
				uint num2 = GalaxyInstancePINVOKE.IChat_GetChatRoomMessageByIndex(this.swigCPtr, index, ref messageID, ref num, GalaxyID.getCPtr(senderID), ref sendTime, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				num3 = num2;
			}
			finally
			{
				messageType = (ChatMessageType)num;
				buffer = Encoding.UTF8.GetString(array);
			}
			return num3;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00016EB8 File Offset: 0x000150B8
		public virtual uint GetChatRoomMemberCount(ulong chatRoomID)
		{
			uint num = GalaxyInstancePINVOKE.IChat_GetChatRoomMemberCount(this.swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00016EE4 File Offset: 0x000150E4
		public virtual GalaxyID GetChatRoomMemberUserIDByIndex(ulong chatRoomID, uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IChat_GetChatRoomMemberUserIDByIndex(this.swigCPtr, chatRoomID, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID galaxyID = null;
			if (intPtr != IntPtr.Zero)
			{
				galaxyID = new GalaxyID(intPtr, true);
			}
			return galaxyID;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00016F2C File Offset: 0x0001512C
		public virtual uint GetChatRoomUnreadMessageCount(ulong chatRoomID)
		{
			uint num = GalaxyInstancePINVOKE.IChat_GetChatRoomUnreadMessageCount(this.swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00016F57 File Offset: 0x00015157
		public virtual void MarkChatRoomAsRead(ulong chatRoomID)
		{
			GalaxyInstancePINVOKE.IChat_MarkChatRoomAsRead(this.swigCPtr, chatRoomID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x040000E2 RID: 226
		private HandleRef swigCPtr;

		// Token: 0x040000E3 RID: 227
		protected bool swigCMemOwn;
	}
}
