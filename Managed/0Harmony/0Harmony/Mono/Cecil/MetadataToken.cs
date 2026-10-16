using System;

namespace Mono.Cecil
{
	// Token: 0x02000286 RID: 646
	public struct MetadataToken : IEquatable<MetadataToken>
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003320C File Offset: 0x0003140C
		public uint RID
		{
			get
			{
				return this.token & 16777215U;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0003321A File Offset: 0x0003141A
		public TokenType TokenType
		{
			get
			{
				return (TokenType)(this.token & 4278190080U);
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00033228 File Offset: 0x00031428
		public MetadataToken(uint token)
		{
			this.token = token;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00033231 File Offset: 0x00031431
		public MetadataToken(TokenType type)
		{
			this = new MetadataToken(type, 0);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0003323B File Offset: 0x0003143B
		public MetadataToken(TokenType type, uint rid)
		{
			this.token = (uint)(type | (TokenType)rid);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003323B File Offset: 0x0003143B
		public MetadataToken(TokenType type, int rid)
		{
			this.token = (uint)(type | (TokenType)rid);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00033246 File Offset: 0x00031446
		public int ToInt32()
		{
			return (int)this.token;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00033246 File Offset: 0x00031446
		public uint ToUInt32()
		{
			return this.token;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00033246 File Offset: 0x00031446
		public override int GetHashCode()
		{
			return (int)this.token;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003324E File Offset: 0x0003144E
		public bool Equals(MetadataToken other)
		{
			return other.token == this.token;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003325E File Offset: 0x0003145E
		public override bool Equals(object obj)
		{
			return obj is MetadataToken && ((MetadataToken)obj).token == this.token;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003327D File Offset: 0x0003147D
		public static bool operator ==(MetadataToken one, MetadataToken other)
		{
			return one.token == other.token;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003328D File Offset: 0x0003148D
		public static bool operator !=(MetadataToken one, MetadataToken other)
		{
			return one.token != other.token;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000332A0 File Offset: 0x000314A0
		public override string ToString()
		{
			return string.Format("[{0}:0x{1}]", this.TokenType, this.RID.ToString("x4"));
		}

		// Token: 0x040005CC RID: 1484
		private readonly uint token;

		// Token: 0x040005CD RID: 1485
		public static readonly MetadataToken Zero = new MetadataToken(0U);
	}
}
