using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x02000171 RID: 369
	public class IUser : IDisposable
	{
		// Token: 0x06000D94 RID: 3476 RVA: 0x0001B8A0 File Offset: 0x00019AA0
		internal IUser(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0001B8BC File Offset: 0x00019ABC
		internal static HandleRef getCPtr(IUser obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0001B8DC File Offset: 0x00019ADC
		~IUser()
		{
			this.Dispose();
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0001B90C File Offset: 0x00019B0C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IUser(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0001B98C File Offset: 0x00019B8C
		public virtual bool SignedIn()
		{
			bool flag = GalaxyInstancePINVOKE.IUser_SignedIn(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		public virtual GalaxyID GetGalaxyID()
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IUser_GetGalaxyID(this.swigCPtr);
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

		// Token: 0x06000D9A RID: 3482 RVA: 0x0001B9FC File Offset: 0x00019BFC
		public virtual void SignInCredentials(string login, string password, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInCredentials__SWIG_0(this.swigCPtr, login, password, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0001BA21 File Offset: 0x00019C21
		public virtual void SignInCredentials(string login, string password)
		{
			GalaxyInstancePINVOKE.IUser_SignInCredentials__SWIG_1(this.swigCPtr, login, password);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0001BA40 File Offset: 0x00019C40
		public virtual void SignInToken(string refreshToken, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInToken__SWIG_0(this.swigCPtr, refreshToken, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0001BA64 File Offset: 0x00019C64
		public virtual void SignInToken(string refreshToken)
		{
			GalaxyInstancePINVOKE.IUser_SignInToken__SWIG_1(this.swigCPtr, refreshToken);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0001BA82 File Offset: 0x00019C82
		public virtual void SignInLauncher(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInLauncher__SWIG_0(this.swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0001BAA5 File Offset: 0x00019CA5
		public virtual void SignInLauncher()
		{
			GalaxyInstancePINVOKE.IUser_SignInLauncher__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0001BAC2 File Offset: 0x00019CC2
		public virtual void SignInSteam(byte[] steamAppTicket, uint steamAppTicketSize, string personaName, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInSteam__SWIG_0(this.swigCPtr, steamAppTicket, steamAppTicketSize, personaName, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0001BAE9 File Offset: 0x00019CE9
		public virtual void SignInSteam(byte[] steamAppTicket, uint steamAppTicketSize, string personaName)
		{
			GalaxyInstancePINVOKE.IUser_SignInSteam__SWIG_1(this.swigCPtr, steamAppTicket, steamAppTicketSize, personaName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0001BB09 File Offset: 0x00019D09
		public virtual void SignInGalaxy(bool requireOnline, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_0(this.swigCPtr, requireOnline, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0001BB2D File Offset: 0x00019D2D
		public virtual void SignInGalaxy(bool requireOnline)
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_1(this.swigCPtr, requireOnline);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0001BB4B File Offset: 0x00019D4B
		public virtual void SignInGalaxy()
		{
			GalaxyInstancePINVOKE.IUser_SignInGalaxy__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0001BB68 File Offset: 0x00019D68
		public virtual void SignInPS4(string ps4ClientID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInPS4__SWIG_0(this.swigCPtr, ps4ClientID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0001BB8C File Offset: 0x00019D8C
		public virtual void SignInPS4(string ps4ClientID)
		{
			GalaxyInstancePINVOKE.IUser_SignInPS4__SWIG_1(this.swigCPtr, ps4ClientID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0001BBAA File Offset: 0x00019DAA
		public virtual void SignInXB1(string xboxOneUserID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXB1__SWIG_0(this.swigCPtr, xboxOneUserID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0001BBCE File Offset: 0x00019DCE
		public virtual void SignInXB1(string xboxOneUserID)
		{
			GalaxyInstancePINVOKE.IUser_SignInXB1__SWIG_1(this.swigCPtr, xboxOneUserID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0001BBEC File Offset: 0x00019DEC
		public virtual void SignInXbox(ulong xboxID, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXbox__SWIG_0(this.swigCPtr, xboxID, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0001BC10 File Offset: 0x00019E10
		public virtual void SignInXbox(ulong xboxID)
		{
			GalaxyInstancePINVOKE.IUser_SignInXbox__SWIG_1(this.swigCPtr, xboxID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0001BC2E File Offset: 0x00019E2E
		public virtual void SignInXBLive(string token, string signature, string marketplaceID, string locale, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInXBLive__SWIG_0(this.swigCPtr, token, signature, marketplaceID, locale, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001BC57 File Offset: 0x00019E57
		public virtual void SignInXBLive(string token, string signature, string marketplaceID, string locale)
		{
			GalaxyInstancePINVOKE.IUser_SignInXBLive__SWIG_1(this.swigCPtr, token, signature, marketplaceID, locale);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0001BC79 File Offset: 0x00019E79
		public virtual void SignInAnonymous(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymous__SWIG_0(this.swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0001BC9C File Offset: 0x00019E9C
		public virtual void SignInAnonymous()
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymous__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0001BCB9 File Offset: 0x00019EB9
		public virtual void SignInAnonymousTelemetry(IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymousTelemetry__SWIG_0(this.swigCPtr, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0001BCDC File Offset: 0x00019EDC
		public virtual void SignInAnonymousTelemetry()
		{
			GalaxyInstancePINVOKE.IUser_SignInAnonymousTelemetry__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001BCF9 File Offset: 0x00019EF9
		public virtual void SignInServerKey(string serverKey, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInServerKey__SWIG_0(this.swigCPtr, serverKey, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001BD1D File Offset: 0x00019F1D
		public virtual void SignInServerKey(string serverKey)
		{
			GalaxyInstancePINVOKE.IUser_SignInServerKey__SWIG_1(this.swigCPtr, serverKey);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001BD3B File Offset: 0x00019F3B
		public virtual void SignInAuthorizationCode(string authorizationCode, string redirectURI, IAuthListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SignInAuthorizationCode__SWIG_0(this.swigCPtr, authorizationCode, redirectURI, IAuthListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001BD60 File Offset: 0x00019F60
		public virtual void SignInAuthorizationCode(string authorizationCode, string redirectURI)
		{
			GalaxyInstancePINVOKE.IUser_SignInAuthorizationCode__SWIG_1(this.swigCPtr, authorizationCode, redirectURI);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0001BD7F File Offset: 0x00019F7F
		public virtual void SignOut()
		{
			GalaxyInstancePINVOKE.IUser_SignOut(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0001BD9C File Offset: 0x00019F9C
		public virtual void RequestUserData(GalaxyID userID, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID), ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001BDC5 File Offset: 0x00019FC5
		public virtual void RequestUserData(GalaxyID userID)
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_1(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		public virtual void RequestUserData()
		{
			GalaxyInstancePINVOKE.IUser_RequestUserData__SWIG_2(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001BE08 File Offset: 0x0001A008
		public virtual bool IsUserDataAvailable(GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_IsUserDataAvailable__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001BE38 File Offset: 0x0001A038
		public virtual bool IsUserDataAvailable()
		{
			bool flag = GalaxyInstancePINVOKE.IUser_IsUserDataAvailable__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001BE64 File Offset: 0x0001A064
		public virtual string GetUserData(string key, GalaxyID userID)
		{
			string text = GalaxyInstancePINVOKE.IUser_GetUserData__SWIG_0(this.swigCPtr, key, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001BE98 File Offset: 0x0001A098
		public virtual string GetUserData(string key)
		{
			string text = GalaxyInstancePINVOKE.IUser_GetUserData__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		public virtual void GetUserDataCopy(string key, out string buffer, uint bufferLength, GalaxyID userID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetUserDataCopy__SWIG_0(this.swigCPtr, key, array, bufferLength, GalaxyID.getCPtr(userID));
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

		// Token: 0x06000DBE RID: 3518 RVA: 0x0001BF24 File Offset: 0x0001A124
		public virtual void GetUserDataCopy(string key, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetUserDataCopy__SWIG_1(this.swigCPtr, key, array, bufferLength);
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

		// Token: 0x06000DBF RID: 3519 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public virtual void SetUserData(string key, string value, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_SetUserData__SWIG_0(this.swigCPtr, key, value, ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001BFA1 File Offset: 0x0001A1A1
		public virtual void SetUserData(string key, string value)
		{
			GalaxyInstancePINVOKE.IUser_SetUserData__SWIG_1(this.swigCPtr, key, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		public virtual uint GetUserDataCount(GalaxyID userID)
		{
			uint num = GalaxyInstancePINVOKE.IUser_GetUserDataCount__SWIG_0(this.swigCPtr, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		public virtual uint GetUserDataCount()
		{
			uint num = GalaxyInstancePINVOKE.IUser_GetUserDataCount__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0001C01C File Offset: 0x0001A21C
		public virtual bool GetUserDataByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength, GalaxyID userID)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_GetUserDataByIndex__SWIG_0(this.swigCPtr, index, key, keyLength, value, valueLength, GalaxyID.getCPtr(userID));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0001C058 File Offset: 0x0001A258
		public virtual bool GetUserDataByIndex(uint index, ref byte[] key, uint keyLength, ref byte[] value, uint valueLength)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_GetUserDataByIndex__SWIG_1(this.swigCPtr, index, key, keyLength, value, valueLength);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0001C08B File Offset: 0x0001A28B
		public virtual void DeleteUserData(string key, ISpecificUserDataListener listener)
		{
			GalaxyInstancePINVOKE.IUser_DeleteUserData__SWIG_0(this.swigCPtr, key, ISpecificUserDataListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0001C0AF File Offset: 0x0001A2AF
		public virtual void DeleteUserData(string key)
		{
			GalaxyInstancePINVOKE.IUser_DeleteUserData__SWIG_1(this.swigCPtr, key);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		public virtual bool IsLoggedOn()
		{
			bool flag = GalaxyInstancePINVOKE.IUser_IsLoggedOn(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001C0FA File Offset: 0x0001A2FA
		public virtual void RequestEncryptedAppTicket(byte[] data, uint dataSize, IEncryptedAppTicketListener listener)
		{
			GalaxyInstancePINVOKE.IUser_RequestEncryptedAppTicket__SWIG_0(this.swigCPtr, data, dataSize, IEncryptedAppTicketListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0001C11F File Offset: 0x0001A31F
		public virtual void RequestEncryptedAppTicket(byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.IUser_RequestEncryptedAppTicket__SWIG_1(this.swigCPtr, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0001C13E File Offset: 0x0001A33E
		public virtual void GetEncryptedAppTicket(byte[] encryptedAppTicket, uint maxEncryptedAppTicketSize, ref uint currentEncryptedAppTicketSize)
		{
			GalaxyInstancePINVOKE.IUser_GetEncryptedAppTicket(this.swigCPtr, encryptedAppTicket, maxEncryptedAppTicketSize, ref currentEncryptedAppTicketSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0001C160 File Offset: 0x0001A360
		public virtual ulong GetSessionID()
		{
			ulong num = GalaxyInstancePINVOKE.IUser_GetSessionID(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0001C18C File Offset: 0x0001A38C
		public virtual string GetAccessToken()
		{
			string text = GalaxyInstancePINVOKE.IUser_GetAccessToken(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		public virtual void GetAccessTokenCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetAccessTokenCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000DCE RID: 3534 RVA: 0x0001C210 File Offset: 0x0001A410
		public virtual string GetRefreshToken()
		{
			string text = GalaxyInstancePINVOKE.IUser_GetRefreshToken(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0001C23C File Offset: 0x0001A43C
		public virtual void GetRefreshTokenCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetRefreshTokenCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0001C294 File Offset: 0x0001A494
		public virtual string GetIDToken()
		{
			string text = GalaxyInstancePINVOKE.IUser_GetIDToken(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public virtual void GetIDTokenCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IUser_GetIDTokenCopy(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0001C318 File Offset: 0x0001A518
		public virtual bool ReportInvalidAccessToken(string accessToken, string info)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_ReportInvalidAccessToken__SWIG_0(this.swigCPtr, accessToken, info);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0001C344 File Offset: 0x0001A544
		public virtual bool ReportInvalidAccessToken(string accessToken)
		{
			bool flag = GalaxyInstancePINVOKE.IUser_ReportInvalidAccessToken__SWIG_1(this.swigCPtr, accessToken);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x040002C5 RID: 709
		private HandleRef swigCPtr;

		// Token: 0x040002C6 RID: 710
		protected bool swigCMemOwn;
	}
}
