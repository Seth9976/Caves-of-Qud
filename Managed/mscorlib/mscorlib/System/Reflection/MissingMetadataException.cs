using System;

namespace System.Reflection
{
	// Token: 0x020008DF RID: 2271
	public sealed class MissingMetadataException : TypeAccessException
	{
		// Token: 0x06004BA9 RID: 19369 RVA: 0x000F1124 File Offset: 0x000EF324
		public MissingMetadataException()
		{
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x000F112C File Offset: 0x000EF32C
		public MissingMetadataException(string message)
			: base(message)
		{
		}
	}
}
