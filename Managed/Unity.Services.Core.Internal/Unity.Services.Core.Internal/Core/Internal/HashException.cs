using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000044 RID: 68
	internal class HashException : Exception
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000377E File Offset: 0x0000197E
		public int Hash { get; }

		// Token: 0x06000130 RID: 304 RVA: 0x00003786 File Offset: 0x00001986
		public HashException(int hash)
		{
			this.Hash = hash;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003795 File Offset: 0x00001995
		public HashException(int hash, string message)
		{
			this.Hash = hash;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000037A4 File Offset: 0x000019A4
		public HashException(int hash, string message, Exception inner)
			: base(message, inner)
		{
			this.Hash = hash;
		}
	}
}
