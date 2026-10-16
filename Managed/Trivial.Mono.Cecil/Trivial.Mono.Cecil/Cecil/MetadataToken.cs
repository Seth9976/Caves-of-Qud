using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000077 RID: 119
	public struct MetadataToken : IEquatable<MetadataToken>
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public uint RID
		{
			get
			{
				return this.token & 16777215U;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000FAA2 File Offset: 0x0000DCA2
		public TokenType TokenType
		{
			get
			{
				return (TokenType)(this.token & 4278190080U);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		public MetadataToken(uint token)
		{
			this.token = token;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000FAB9 File Offset: 0x0000DCB9
		public MetadataToken(TokenType type)
		{
			this = new MetadataToken(type, 0);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public MetadataToken(TokenType type, uint rid)
		{
			this.token = (uint)(type | (TokenType)rid);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public MetadataToken(TokenType type, int rid)
		{
			this.token = (uint)(type | (TokenType)rid);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000FAD3 File Offset: 0x0000DCD3
		public int ToInt32()
		{
			return (int)this.token;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000FAD3 File Offset: 0x0000DCD3
		public uint ToUInt32()
		{
			return this.token;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000FAD3 File Offset: 0x0000DCD3
		public override int GetHashCode()
		{
			return (int)this.token;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000FADB File Offset: 0x0000DCDB
		public bool Equals(MetadataToken other)
		{
			return other.token == this.token;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		public override bool Equals(object obj)
		{
			return obj is MetadataToken && ((MetadataToken)obj).token == this.token;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000FB0A File Offset: 0x0000DD0A
		public static bool operator ==(MetadataToken one, MetadataToken other)
		{
			return one.token == other.token;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000FB1A File Offset: 0x0000DD1A
		public static bool operator !=(MetadataToken one, MetadataToken other)
		{
			return one.token != other.token;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000FB30 File Offset: 0x0000DD30
		public override string ToString()
		{
			return string.Format("[{0}:0x{1}]", this.TokenType, this.RID.ToString("x4"));
		}

		// Token: 0x0400011D RID: 285
		private readonly uint token;

		// Token: 0x0400011E RID: 286
		public static readonly MetadataToken Zero = new MetadataToken(0U);
	}
}
