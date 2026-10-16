using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x02000136 RID: 310
	public class IMatchmaking : IDisposable
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x000193C4 File Offset: 0x000175C4
		internal IMatchmaking(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000193E0 File Offset: 0x000175E0
		internal static HandleRef getCPtr(IMatchmaking obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00019400 File Offset: 0x00017600
		~IMatchmaking()
		{
			this.Dispose();
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00019430 File Offset: 0x00017630
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IMatchmaking(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000194B0 File Offset: 0x000176B0
		public bool SendLobbyMessage(GalaxyID lobbyID, string msg)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(msg);
			return this.SendLobbyMessage(lobbyID, bytes, (uint)bytes.Length);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000194D4 File Offset: 0x000176D4
		public uint GetLobbyMessage(GalaxyID lobbyID, uint messageID, ref GalaxyID _senderID, out string msg, uint internalBufferLen = 1024U)
		{
			byte[] array = new byte[internalBufferLen];
			GalaxyID galaxyID = new GalaxyID();
			uint lobbyMessage = this.GetLobbyMessage(lobbyID, messageID, ref galaxyID, ref array, (uint)array.Length);
			msg = Encoding.UTF8.GetString(array, 0, (int)lobbyMessage);
			return lobbyMessage;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00019511 File Offset: 0x00017711
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType, ILobbyCreatedListener lobbyCreatedListener, ILobbyEnteredListener lobbyEnteredListener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_0(this.swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType, ILobbyCreatedListener.getCPtr(lobbyCreatedListener), ILobbyEnteredListener.getCPtr(lobbyEnteredListener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00019541 File Offset: 0x00017741
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType, ILobbyCreatedListener lobbyCreatedListener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_1(this.swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType, ILobbyCreatedListener.getCPtr(lobbyCreatedListener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001956A File Offset: 0x0001776A
		public virtual void CreateLobby(LobbyType lobbyType, uint maxMembers, bool joinable, LobbyTopologyType lobbyTopologyType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_CreateLobby__SWIG_2(this.swigCPtr, (int)lobbyType, maxMembers, joinable, (int)lobbyTopologyType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001958C File Offset: 0x0001778C
		public virtual void RequestLobbyList(bool allowFullLobbies, ILobbyListListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_0(this.swigCPtr, allowFullLobbies, ILobbyListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000195B0 File Offset: 0x000177B0
		public virtual void RequestLobbyList(bool allowFullLobbies)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_1(this.swigCPtr, allowFullLobbies);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000195CE File Offset: 0x000177CE
		public virtual void RequestLobbyList()
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyList__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000195EB File Offset: 0x000177EB
		public virtual void AddRequestLobbyListResultCountFilter(uint limit)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListResultCountFilter(this.swigCPtr, limit);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00019609 File Offset: 0x00017809
		public virtual void AddRequestLobbyListStringFilter(string keyToMatch, string valueToMatch, LobbyComparisonType comparisonType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListStringFilter(this.swigCPtr, keyToMatch, valueToMatch, (int)comparisonType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00019629 File Offset: 0x00017829
		public virtual void AddRequestLobbyListNumericalFilter(string keyToMatch, int valueToMatch, LobbyComparisonType comparisonType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListNumericalFilter(this.swigCPtr, keyToMatch, valueToMatch, (int)comparisonType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00019649 File Offset: 0x00017849
		public virtual void AddRequestLobbyListNearValueFilter(string keyToMatch, int valueToBeCloseTo)
		{
			GalaxyInstancePINVOKE.IMatchmaking_AddRequestLobbyListNearValueFilter(this.swigCPtr, keyToMatch, valueToBeCloseTo);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00019668 File Offset: 0x00017868
		public virtual GalaxyID GetLobbyByIndex(uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyByIndex(this.swigCPtr, index);
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

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000196AD File Offset: 0x000178AD
		public virtual void JoinLobby(GalaxyID lobbyID, ILobbyEnteredListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_JoinLobby__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyEnteredListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000196D6 File Offset: 0x000178D6
		public virtual void JoinLobby(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_JoinLobby__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000196F9 File Offset: 0x000178F9
		public virtual void LeaveLobby(GalaxyID lobbyID, ILobbyLeftListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_LeaveLobby__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyLeftListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00019722 File Offset: 0x00017922
		public virtual void LeaveLobby(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_LeaveLobby__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00019745 File Offset: 0x00017945
		public virtual void SetMaxNumLobbyMembers(GalaxyID lobbyID, uint maxNumLobbyMembers, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetMaxNumLobbyMembers__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), maxNumLobbyMembers, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001976F File Offset: 0x0001796F
		public virtual void SetMaxNumLobbyMembers(GalaxyID lobbyID, uint maxNumLobbyMembers)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetMaxNumLobbyMembers__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), maxNumLobbyMembers);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00019794 File Offset: 0x00017994
		public virtual uint GetMaxNumLobbyMembers(GalaxyID lobbyID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetMaxNumLobbyMembers(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000197C4 File Offset: 0x000179C4
		public virtual uint GetNumLobbyMembers(GalaxyID lobbyID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetNumLobbyMembers(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000197F4 File Offset: 0x000179F4
		public virtual GalaxyID GetLobbyMemberByIndex(GalaxyID lobbyID, uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberByIndex(this.swigCPtr, GalaxyID.getCPtr(lobbyID), index);
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

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001983F File Offset: 0x00017A3F
		public virtual void SetLobbyType(GalaxyID lobbyID, LobbyType lobbyType, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyType__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), (int)lobbyType, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00019869 File Offset: 0x00017A69
		public virtual void SetLobbyType(GalaxyID lobbyID, LobbyType lobbyType)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyType__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), (int)lobbyType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00019890 File Offset: 0x00017A90
		public virtual LobbyType GetLobbyType(GalaxyID lobbyID)
		{
			LobbyType lobbyType = (LobbyType)GalaxyInstancePINVOKE.IMatchmaking_GetLobbyType(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return lobbyType;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000198C0 File Offset: 0x00017AC0
		public virtual void SetLobbyJoinable(GalaxyID lobbyID, bool joinable, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyJoinable__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), joinable, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000198EA File Offset: 0x00017AEA
		public virtual void SetLobbyJoinable(GalaxyID lobbyID, bool joinable)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyJoinable__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), joinable);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00019910 File Offset: 0x00017B10
		public virtual bool IsLobbyJoinable(GalaxyID lobbyID)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_IsLobbyJoinable(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00019940 File Offset: 0x00017B40
		public virtual void RequestLobbyData(GalaxyID lobbyID, ILobbyDataRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), ILobbyDataRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00019969 File Offset: 0x00017B69
		public virtual void RequestLobbyData(GalaxyID lobbyID)
		{
			GalaxyInstancePINVOKE.IMatchmaking_RequestLobbyData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001998C File Offset: 0x00017B8C
		public virtual string GetLobbyData(GalaxyID lobbyID, string key)
		{
			string text = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyData(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000199C0 File Offset: 0x00017BC0
		public virtual void GetLobbyDataCopy(GalaxyID lobbyID, string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataCopy(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00019A20 File Offset: 0x00017C20
		public virtual void SetLobbyData(GalaxyID lobbyID, string key, string value, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00019A4C File Offset: 0x00017C4C
		public virtual void SetLobbyData(GalaxyID lobbyID, string key, string value)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00019A74 File Offset: 0x00017C74
		public virtual uint GetLobbyDataCount(GalaxyID lobbyID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataCount(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public virtual bool GetLobbyDataByIndex(GalaxyID lobbyID, uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyDataByIndex(this.swigCPtr, GalaxyID.getCPtr(lobbyID), index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00019ADE File Offset: 0x00017CDE
		public virtual void DeleteLobbyData(GalaxyID lobbyID, string key, ILobbyDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, ILobbyDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00019B08 File Offset: 0x00017D08
		public virtual void DeleteLobbyData(GalaxyID lobbyID, string key)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00019B2C File Offset: 0x00017D2C
		public virtual string GetLobbyMemberData(GalaxyID lobbyID, GalaxyID memberID, string key)
		{
			string text = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberData(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00019B64 File Offset: 0x00017D64
		public virtual void GetLobbyMemberDataCopy(GalaxyID lobbyID, GalaxyID memberID, string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataCopy(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), key, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00019BCC File Offset: 0x00017DCC
		public virtual void SetLobbyMemberData(GalaxyID lobbyID, string key, string value, ILobbyMemberDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyMemberData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value, ILobbyMemberDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00019BF8 File Offset: 0x00017DF8
		public virtual void SetLobbyMemberData(GalaxyID lobbyID, string key, string value)
		{
			GalaxyInstancePINVOKE.IMatchmaking_SetLobbyMemberData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00019C20 File Offset: 0x00017E20
		public virtual uint GetLobbyMemberDataCount(GalaxyID lobbyID, GalaxyID memberID)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataCount(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00019C58 File Offset: 0x00017E58
		public virtual bool GetLobbyMemberDataByIndex(GalaxyID lobbyID, GalaxyID memberID, uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMemberDataByIndex(this.swigCPtr, GalaxyID.getCPtr(lobbyID), GalaxyID.getCPtr(memberID), index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00019C99 File Offset: 0x00017E99
		public virtual void DeleteLobbyMemberData(GalaxyID lobbyID, string key, ILobbyMemberDataUpdateListener listener)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyMemberData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key, ILobbyMemberDataUpdateListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00019CC3 File Offset: 0x00017EC3
		public virtual void DeleteLobbyMemberData(GalaxyID lobbyID, string key)
		{
			GalaxyInstancePINVOKE.IMatchmaking_DeleteLobbyMemberData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(lobbyID), key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public virtual GalaxyID GetLobbyOwner(GalaxyID lobbyID)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyOwner(this.swigCPtr, GalaxyID.getCPtr(lobbyID));
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

		// Token: 0x06000BDA RID: 3034 RVA: 0x00019D34 File Offset: 0x00017F34
		public virtual bool SendLobbyMessage(GalaxyID lobbyID, byte[] data, uint dataSize)
		{
			bool flag = GalaxyInstancePINVOKE.IMatchmaking_SendLobbyMessage(this.swigCPtr, GalaxyID.getCPtr(lobbyID), data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00019D68 File Offset: 0x00017F68
		public virtual uint GetLobbyMessage(GalaxyID lobbyID, uint messageID, ref GalaxyID senderID, ref byte[] msg, uint msgLength)
		{
			uint num = GalaxyInstancePINVOKE.IMatchmaking_GetLobbyMessage(this.swigCPtr, GalaxyID.getCPtr(lobbyID), messageID, GalaxyID.getCPtr(senderID), msg, msgLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x04000235 RID: 565
		private HandleRef swigCPtr;

		// Token: 0x04000236 RID: 566
		protected bool swigCMemOwn;
	}
}
