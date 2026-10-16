using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x020000FD RID: 253
	public class IFriends : IDisposable
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x0001831A File Offset: 0x0001651A
		internal IFriends(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00018336 File Offset: 0x00016536
		internal static HandleRef getCPtr(IFriends obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00018354 File Offset: 0x00016554
		~IFriends()
		{
			this.Dispose();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00018384 File Offset: 0x00016584
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IFriends(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00018404 File Offset: 0x00016604
		public virtual uint GetDefaultAvatarCriteria()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetDefaultAvatarCriteria(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001842E File Offset: 0x0001662E
		public virtual void SetDefaultAvatarCriteria(uint defaultAvatarCriteria)
		{
			GalaxyInstancePINVOKE.IFriends_SetDefaultAvatarCriteria(this.swigCPtr, defaultAvatarCriteria);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001844C File Offset: 0x0001664C
		public virtual void RequestUserInformation(GalaxyID userID, uint avatarCriteria, IUserInformationRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), avatarCriteria, IUserInformationRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00018476 File Offset: 0x00016676
		public virtual void RequestUserInformation(GalaxyID userID, uint avatarCriteria)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID), avatarCriteria);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001849A File Offset: 0x0001669A
		public virtual void RequestUserInformation(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_RequestUserInformation__SWIG_2(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000184C0 File Offset: 0x000166C0
		public virtual bool IsUserInformationAvailable(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsUserInformationAvailable(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000184F0 File Offset: 0x000166F0
		public virtual string GetPersonaName()
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetPersonaName(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001851C File Offset: 0x0001671C
		public virtual void GetPersonaNameCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetPersonaNameCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000A2E RID: 2606 RVA: 0x00018574 File Offset: 0x00016774
		public virtual PersonaState GetPersonaState()
		{
			PersonaState personaState = (PersonaState)GalaxyInstancePINVOKE.IFriends_GetPersonaState(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return personaState;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000185A0 File Offset: 0x000167A0
		public virtual string GetFriendPersonaName(GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetFriendPersonaName(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000185D0 File Offset: 0x000167D0
		public virtual void GetFriendPersonaNameCopy(GalaxyID userID, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetFriendPersonaNameCopy(this.swigCPtr, GalaxyID.getCPtr(userID), array, bufferLength);
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

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001862C File Offset: 0x0001682C
		public virtual PersonaState GetFriendPersonaState(GalaxyID userID)
		{
			PersonaState personaState = (PersonaState)GalaxyInstancePINVOKE.IFriends_GetFriendPersonaState(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return personaState;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001865C File Offset: 0x0001685C
		public virtual string GetFriendAvatarUrl(GalaxyID userID, AvatarType avatarType)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetFriendAvatarUrl(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00018690 File Offset: 0x00016890
		public virtual void GetFriendAvatarUrlCopy(GalaxyID userID, AvatarType avatarType, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetFriendAvatarUrlCopy(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType, array, bufferLength);
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

		// Token: 0x06000A34 RID: 2612 RVA: 0x000186F0 File Offset: 0x000168F0
		public virtual uint GetFriendAvatarImageID(GalaxyID userID, AvatarType avatarType)
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetFriendAvatarImageID(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00018721 File Offset: 0x00016921
		public virtual void GetFriendAvatarImageRGBA(GalaxyID userID, AvatarType avatarType, byte[] buffer, uint bufferLength)
		{
			GalaxyInstancePINVOKE.IFriends_GetFriendAvatarImageRGBA(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType, buffer, bufferLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00018748 File Offset: 0x00016948
		public virtual bool IsFriendAvatarImageRGBAAvailable(GalaxyID userID, AvatarType avatarType)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsFriendAvatarImageRGBAAvailable(this.swigCPtr, GalaxyID.getCPtr(userID), (int)avatarType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00018779 File Offset: 0x00016979
		public virtual void RequestFriendList(IFriendListListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendList__SWIG_0(this.swigCPtr, IFriendListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001879C File Offset: 0x0001699C
		public virtual void RequestFriendList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendList__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000187BC File Offset: 0x000169BC
		public virtual bool IsFriend(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsFriend(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000187EC File Offset: 0x000169EC
		public virtual uint GetFriendCount()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetFriendCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00018818 File Offset: 0x00016A18
		public virtual GalaxyID GetFriendByIndex(uint index)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IFriends_GetFriendByIndex(this.swigCPtr, index);
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

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001885D File Offset: 0x00016A5D
		public virtual void SendFriendInvitation(GalaxyID userID, IFriendInvitationSendListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SendFriendInvitation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IFriendInvitationSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00018886 File Offset: 0x00016A86
		public virtual void SendFriendInvitation(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_SendFriendInvitation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000188A9 File Offset: 0x00016AA9
		public virtual void RequestFriendInvitationList(IFriendInvitationListRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendInvitationList__SWIG_0(this.swigCPtr, IFriendInvitationListRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000188CC File Offset: 0x00016ACC
		public virtual void RequestFriendInvitationList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestFriendInvitationList__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000188E9 File Offset: 0x00016AE9
		public virtual void RequestSentFriendInvitationList(ISentFriendInvitationListRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestSentFriendInvitationList__SWIG_0(this.swigCPtr, ISentFriendInvitationListRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001890C File Offset: 0x00016B0C
		public virtual void RequestSentFriendInvitationList()
		{
			GalaxyInstancePINVOKE.IFriends_RequestSentFriendInvitationList__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001892C File Offset: 0x00016B2C
		public virtual uint GetFriendInvitationCount()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetFriendInvitationCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00018956 File Offset: 0x00016B56
		public virtual void GetFriendInvitationByIndex(uint index, ref GalaxyID userID, ref uint sendTime)
		{
			GalaxyInstancePINVOKE.IFriends_GetFriendInvitationByIndex(this.swigCPtr, index, GalaxyID.getCPtr(userID), ref sendTime);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001897C File Offset: 0x00016B7C
		public virtual void RespondToFriendInvitation(GalaxyID userID, bool accept, IFriendInvitationRespondToListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RespondToFriendInvitation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), accept, IFriendInvitationRespondToListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000189A6 File Offset: 0x00016BA6
		public virtual void RespondToFriendInvitation(GalaxyID userID, bool accept)
		{
			GalaxyInstancePINVOKE.IFriends_RespondToFriendInvitation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID), accept);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000189CA File Offset: 0x00016BCA
		public virtual void DeleteFriend(GalaxyID userID, IFriendDeleteListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteFriend__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IFriendDeleteListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000189F3 File Offset: 0x00016BF3
		public virtual void DeleteFriend(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteFriend__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00018A16 File Offset: 0x00016C16
		public virtual void SetRichPresence(string key, string value, IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SetRichPresence__SWIG_0(this.swigCPtr, key, value, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00018A3B File Offset: 0x00016C3B
		public virtual void SetRichPresence(string key, string value)
		{
			GalaxyInstancePINVOKE.IFriends_SetRichPresence__SWIG_1(this.swigCPtr, key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00018A5A File Offset: 0x00016C5A
		public virtual void DeleteRichPresence(string key, IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteRichPresence__SWIG_0(this.swigCPtr, key, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00018A7E File Offset: 0x00016C7E
		public virtual void DeleteRichPresence(string key)
		{
			GalaxyInstancePINVOKE.IFriends_DeleteRichPresence__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00018A9C File Offset: 0x00016C9C
		public virtual void ClearRichPresence(IRichPresenceChangeListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_ClearRichPresence__SWIG_0(this.swigCPtr, IRichPresenceChangeListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00018ABF File Offset: 0x00016CBF
		public virtual void ClearRichPresence()
		{
			GalaxyInstancePINVOKE.IFriends_ClearRichPresence__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00018ADC File Offset: 0x00016CDC
		public virtual void RequestRichPresence(GalaxyID userID, IRichPresenceRetrieveListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), IRichPresenceRetrieveListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00018B05 File Offset: 0x00016D05
		public virtual void RequestRichPresence(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00018B28 File Offset: 0x00016D28
		public virtual void RequestRichPresence()
		{
			GalaxyInstancePINVOKE.IFriends_RequestRichPresence__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00018B48 File Offset: 0x00016D48
		public virtual string GetRichPresence(string key, GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresence__SWIG_0(this.swigCPtr, key, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00018B7C File Offset: 0x00016D7C
		public virtual string GetRichPresence(string key)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresence__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00018BA8 File Offset: 0x00016DA8
		public virtual void GetRichPresenceCopy(string key, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceCopy__SWIG_0(this.swigCPtr, key, array, bufferLength, GalaxyID.getCPtr(userID));
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

		// Token: 0x06000A54 RID: 2644 RVA: 0x00018C08 File Offset: 0x00016E08
		public virtual void GetRichPresenceCopy(string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceCopy__SWIG_1(this.swigCPtr, key, array, bufferLength);
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

		// Token: 0x06000A55 RID: 2645 RVA: 0x00018C60 File Offset: 0x00016E60
		public virtual uint GetRichPresenceCount(GalaxyID userID)
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetRichPresenceCount__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00018C90 File Offset: 0x00016E90
		public virtual uint GetRichPresenceCount()
		{
			uint num = GalaxyInstancePINVOKE.IFriends_GetRichPresenceCount__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00018CBA File Offset: 0x00016EBA
		public virtual void GetRichPresenceByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength, GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IFriends_GetRichPresenceByIndex__SWIG_0(this.swigCPtr, index, key, keyLength, value, valueLength, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00018CE7 File Offset: 0x00016EE7
		public virtual void GetRichPresenceByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			GalaxyInstancePINVOKE.IFriends_GetRichPresenceByIndex__SWIG_1(this.swigCPtr, index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00018D10 File Offset: 0x00016F10
		public virtual string GetRichPresenceKeyByIndex(uint index, GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndex__SWIG_0(this.swigCPtr, index, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00018D44 File Offset: 0x00016F44
		public virtual string GetRichPresenceKeyByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndex__SWIG_1(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00018D70 File Offset: 0x00016F70
		public virtual void GetRichPresenceKeyByIndexCopy(uint index, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndexCopy__SWIG_0(this.swigCPtr, index, array, bufferLength, GalaxyID.getCPtr(userID));
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

		// Token: 0x06000A5C RID: 2652 RVA: 0x00018DD0 File Offset: 0x00016FD0
		public virtual void GetRichPresenceKeyByIndexCopy(uint index, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IFriends_GetRichPresenceKeyByIndexCopy__SWIG_1(this.swigCPtr, index, array, bufferLength);
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

		// Token: 0x06000A5D RID: 2653 RVA: 0x00018E28 File Offset: 0x00017028
		public virtual void ShowOverlayInviteDialog(string connectionString)
		{
			GalaxyInstancePINVOKE.IFriends_ShowOverlayInviteDialog(this.swigCPtr, connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00018E46 File Offset: 0x00017046
		public virtual void SendInvitation(GalaxyID userID, string connectionString, ISendInvitationListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_SendInvitation__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), connectionString, ISendInvitationListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00018E70 File Offset: 0x00017070
		public virtual void SendInvitation(GalaxyID userID, string connectionString)
		{
			GalaxyInstancePINVOKE.IFriends_SendInvitation__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID), connectionString);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00018E94 File Offset: 0x00017094
		public virtual void FindUser(string userSpecifier, IUserFindListener listener)
		{
			GalaxyInstancePINVOKE.IFriends_FindUser__SWIG_0(this.swigCPtr, userSpecifier, IUserFindListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00018EB8 File Offset: 0x000170B8
		public virtual void FindUser(string userSpecifier)
		{
			GalaxyInstancePINVOKE.IFriends_FindUser__SWIG_1(this.swigCPtr, userSpecifier);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00018ED8 File Offset: 0x000170D8
		public virtual bool IsUserInTheSameGame(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IFriends_IsUserInTheSameGame(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x040001B5 RID: 437
		private HandleRef swigCPtr;

		// Token: 0x040001B6 RID: 438
		protected bool swigCMemOwn;
	}
}
