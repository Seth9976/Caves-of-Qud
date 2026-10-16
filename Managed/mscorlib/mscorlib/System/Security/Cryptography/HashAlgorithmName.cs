using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000472 RID: 1138
	public readonly struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000A5DDA File Offset: 0x000A3FDA
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000A5DE6 File Offset: 0x000A3FE6
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000A5DF2 File Offset: 0x000A3FF2
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000A5DFE File Offset: 0x000A3FFE
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000A5E0A File Offset: 0x000A400A
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000A5E16 File Offset: 0x000A4016
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000A5E1F File Offset: 0x000A401F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000A5E27 File Offset: 0x000A4027
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000A5E38 File Offset: 0x000A4038
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000A5E50 File Offset: 0x000A4050
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000A5E63 File Offset: 0x000A4063
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000A5E7A File Offset: 0x000A407A
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000A5E84 File Offset: 0x000A4084
		public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
		{
			return !(left == right);
		}

		// Token: 0x04002118 RID: 8472
		private readonly string _name;
	}
}
