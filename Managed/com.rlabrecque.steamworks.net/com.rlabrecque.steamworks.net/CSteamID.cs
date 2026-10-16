using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct CSteamID : IEquatable<CSteamID>, IComparable<CSteamID>
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0000EF42 File Offset: 0x0000D142
		public CSteamID(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.Set(unAccountID, eUniverse, eAccountType);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0000EF55 File Offset: 0x0000D155
		public CSteamID(AccountID_t unAccountID, uint unAccountInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.m_SteamID = 0UL;
			this.InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0000EF6A File Offset: 0x0000D16A
		public CSteamID(ulong ulSteamID)
		{
			this.m_SteamID = ulSteamID;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000EF73 File Offset: 0x0000D173
		public void Set(AccountID_t unAccountID, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			if (eAccountType == EAccountType.k_EAccountTypeClan || eAccountType == EAccountType.k_EAccountTypeGameServer)
			{
				this.SetAccountInstance(0U);
				return;
			}
			this.SetAccountInstance(1U);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public void InstancedSet(AccountID_t unAccountID, uint unInstance, EUniverse eUniverse, EAccountType eAccountType)
		{
			this.SetAccountID(unAccountID);
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(eAccountType);
			this.SetAccountInstance(unInstance);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public void Clear()
		{
			this.m_SteamID = 0UL;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000EFCA File Offset: 0x0000D1CA
		public void CreateBlankAnonLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonGameServer);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0000EFED File Offset: 0x0000D1ED
		public void CreateBlankAnonUserLogon(EUniverse eUniverse)
		{
			this.SetAccountID(new AccountID_t(0U));
			this.SetEUniverse(eUniverse);
			this.SetEAccountType(EAccountType.k_EAccountTypeAnonUser);
			this.SetAccountInstance(0U);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0000F011 File Offset: 0x0000D211
		public bool BBlankAnonAccount()
		{
			return this.GetAccountID() == new AccountID_t(0U) && this.BAnonAccount() && this.GetUnAccountInstance() == 0U;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0000F039 File Offset: 0x0000D239
		public bool BGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0000F04F File Offset: 0x0000D24F
		public bool BPersistentGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeGameServer;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000F05A File Offset: 0x0000D25A
		public bool BAnonGameServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0000F065 File Offset: 0x0000D265
		public bool BContentServerAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeContentServer;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0000F070 File Offset: 0x0000D270
		public bool BClanAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeClan;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0000F07B File Offset: 0x0000D27B
		public bool BChatAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000F086 File Offset: 0x0000D286
		public bool IsLobby()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeChat && (this.GetUnAccountInstance() & 262144U) > 0U;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000F0A2 File Offset: 0x0000D2A2
		public bool BIndividualAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeIndividual || this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000F0B9 File Offset: 0x0000D2B9
		public bool BAnonAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser || this.GetEAccountType() == EAccountType.k_EAccountTypeAnonGameServer;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public bool BAnonUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeAnonUser;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public bool BConsoleUserAccount()
		{
			return this.GetEAccountType() == EAccountType.k_EAccountTypeConsoleUser;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		public void SetAccountID(AccountID_t other)
		{
			this.m_SteamID = (this.m_SteamID & 18446744069414584320UL) | ((ulong)(uint)other & (ulong)(-1));
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0000F10B File Offset: 0x0000D30B
		public void SetAccountInstance(uint other)
		{
			this.m_SteamID = (this.m_SteamID & 18442240478377148415UL) | (((ulong)other & 1048575UL) << 32);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0000F130 File Offset: 0x0000D330
		public void SetEAccountType(EAccountType other)
		{
			this.m_SteamID = (this.m_SteamID & 18379190079298994175UL) | (ulong)((ulong)((long)other & 15L) << 52);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0000F152 File Offset: 0x0000D352
		public void SetEUniverse(EUniverse other)
		{
			this.m_SteamID = (this.m_SteamID & 72057594037927935UL) | (ulong)((ulong)((long)other & 255L) << 56);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0000F177 File Offset: 0x0000D377
		public AccountID_t GetAccountID()
		{
			return new AccountID_t((uint)(this.m_SteamID & (ulong)(-1)));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0000F188 File Offset: 0x0000D388
		public uint GetUnAccountInstance()
		{
			return (uint)((this.m_SteamID >> 32) & 1048575UL);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0000F19B File Offset: 0x0000D39B
		public EAccountType GetEAccountType()
		{
			return (EAccountType)((this.m_SteamID >> 52) & 15UL);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0000F1AB File Offset: 0x0000D3AB
		public EUniverse GetEUniverse()
		{
			return (EUniverse)((this.m_SteamID >> 56) & 255UL);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public bool IsValid()
		{
			return this.GetEAccountType() > EAccountType.k_EAccountTypeInvalid && this.GetEAccountType() < EAccountType.k_EAccountTypeMax && this.GetEUniverse() > EUniverse.k_EUniverseInvalid && this.GetEUniverse() < EUniverse.k_EUniverseMax && (this.GetEAccountType() != EAccountType.k_EAccountTypeIndividual || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() <= 1U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeClan || (!(this.GetAccountID() == new AccountID_t(0U)) && this.GetUnAccountInstance() == 0U)) && (this.GetEAccountType() != EAccountType.k_EAccountTypeGameServer || !(this.GetAccountID() == new AccountID_t(0U)));
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0000F262 File Offset: 0x0000D462
		public override string ToString()
		{
			return this.m_SteamID.ToString();
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0000F26F File Offset: 0x0000D46F
		public override bool Equals(object other)
		{
			return other is CSteamID && this == (CSteamID)other;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000F28C File Offset: 0x0000D48C
		public override int GetHashCode()
		{
			return this.m_SteamID.GetHashCode();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0000F299 File Offset: 0x0000D499
		public static bool operator ==(CSteamID x, CSteamID y)
		{
			return x.m_SteamID == y.m_SteamID;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000F2A9 File Offset: 0x0000D4A9
		public static bool operator !=(CSteamID x, CSteamID y)
		{
			return !(x == y);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000F2B5 File Offset: 0x0000D4B5
		public static explicit operator CSteamID(ulong value)
		{
			return new CSteamID(value);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000F2BD File Offset: 0x0000D4BD
		public static explicit operator ulong(CSteamID that)
		{
			return that.m_SteamID;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0000F2C5 File Offset: 0x0000D4C5
		public bool Equals(CSteamID other)
		{
			return this.m_SteamID == other.m_SteamID;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000F2D5 File Offset: 0x0000D4D5
		public int CompareTo(CSteamID other)
		{
			return this.m_SteamID.CompareTo(other.m_SteamID);
		}

		// Token: 0x04000AB3 RID: 2739
		public static readonly CSteamID Nil = default(CSteamID);

		// Token: 0x04000AB4 RID: 2740
		public static readonly CSteamID OutofDateGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB5 RID: 2741
		public static readonly CSteamID LanModeGS = new CSteamID(new AccountID_t(0U), 0U, EUniverse.k_EUniversePublic, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB6 RID: 2742
		public static readonly CSteamID NotInitYetGS = new CSteamID(new AccountID_t(1U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB7 RID: 2743
		public static readonly CSteamID NonSteamGS = new CSteamID(new AccountID_t(2U), 0U, EUniverse.k_EUniverseInvalid, EAccountType.k_EAccountTypeInvalid);

		// Token: 0x04000AB8 RID: 2744
		public ulong m_SteamID;
	}
}
