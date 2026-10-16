using System;

namespace Steamworks
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	public struct CGameID : IEquatable<CGameID>, IComparable<CGameID>
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x0000ED13 File Offset: 0x0000CF13
		public CGameID(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public CGameID(AppId_t nAppID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000ED2D File Offset: 0x0000CF2D
		public CGameID(AppId_t nAppID, uint nModID)
		{
			this.m_GameID = 0UL;
			this.SetAppID(nAppID);
			this.SetType(CGameID.EGameIDType.k_EGameIDTypeGameMod);
			this.SetModID(nModID);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0000ED4C File Offset: 0x0000CF4C
		public bool IsSteamApp()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeApp;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0000ED57 File Offset: 0x0000CF57
		public bool IsMod()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000ED62 File Offset: 0x0000CF62
		public bool IsShortcut()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000ED6D File Offset: 0x0000CF6D
		public bool IsP2PFile()
		{
			return this.Type() == CGameID.EGameIDType.k_EGameIDTypeP2P;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public AppId_t AppID()
		{
			return new AppId_t((uint)(this.m_GameID & 16777215UL));
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0000ED8D File Offset: 0x0000CF8D
		public CGameID.EGameIDType Type()
		{
			return (CGameID.EGameIDType)((this.m_GameID >> 24) & 255UL);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public uint ModID()
		{
			return (uint)((this.m_GameID >> 32) & (ulong)(-1));
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public bool IsValid()
		{
			switch (this.Type())
			{
			case CGameID.EGameIDType.k_EGameIDTypeApp:
				return this.AppID() != AppId_t.Invalid;
			case CGameID.EGameIDType.k_EGameIDTypeGameMod:
				return this.AppID() != AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeShortcut:
				return (this.ModID() & 2147483648U) > 0U;
			case CGameID.EGameIDType.k_EGameIDTypeP2P:
				return this.AppID() == AppId_t.Invalid && (this.ModID() & 2147483648U) > 0U;
			default:
				return false;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000EE46 File Offset: 0x0000D046
		public void Reset()
		{
			this.m_GameID = 0UL;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000EE50 File Offset: 0x0000D050
		public void Set(ulong GameID)
		{
			this.m_GameID = GameID;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0000EE59 File Offset: 0x0000D059
		private void SetAppID(AppId_t other)
		{
			this.m_GameID = (this.m_GameID & 18446744073692774400UL) | ((ulong)(uint)other & 16777215UL);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0000EE7D File Offset: 0x0000D07D
		private void SetType(CGameID.EGameIDType other)
		{
			this.m_GameID = (this.m_GameID & 18446744069431361535UL) | (ulong)((ulong)((long)other & 255L) << 24);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000EEA2 File Offset: 0x0000D0A2
		private void SetModID(uint other)
		{
			this.m_GameID = (this.m_GameID & (ulong)(-1)) | (((ulong)other & (ulong)(-1)) << 32);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public override string ToString()
		{
			return this.m_GameID.ToString();
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0000EEC9 File Offset: 0x0000D0C9
		public override bool Equals(object other)
		{
			return other is CGameID && this == (CGameID)other;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000EEE6 File Offset: 0x0000D0E6
		public override int GetHashCode()
		{
			return this.m_GameID.GetHashCode();
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000EEF3 File Offset: 0x0000D0F3
		public static bool operator ==(CGameID x, CGameID y)
		{
			return x.m_GameID == y.m_GameID;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000EF03 File Offset: 0x0000D103
		public static bool operator !=(CGameID x, CGameID y)
		{
			return !(x == y);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000EF0F File Offset: 0x0000D10F
		public static explicit operator CGameID(ulong value)
		{
			return new CGameID(value);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000EF17 File Offset: 0x0000D117
		public static explicit operator ulong(CGameID that)
		{
			return that.m_GameID;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000EF1F File Offset: 0x0000D11F
		public bool Equals(CGameID other)
		{
			return this.m_GameID == other.m_GameID;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000EF2F File Offset: 0x0000D12F
		public int CompareTo(CGameID other)
		{
			return this.m_GameID.CompareTo(other.m_GameID);
		}

		// Token: 0x04000AB2 RID: 2738
		public ulong m_GameID;

		// Token: 0x020001F2 RID: 498
		public enum EGameIDType
		{
			// Token: 0x04000B61 RID: 2913
			k_EGameIDTypeApp,
			// Token: 0x04000B62 RID: 2914
			k_EGameIDTypeGameMod,
			// Token: 0x04000B63 RID: 2915
			k_EGameIDTypeShortcut,
			// Token: 0x04000B64 RID: 2916
			k_EGameIDTypeP2P
		}
	}
}
