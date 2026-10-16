using System;

namespace System.Reflection
{
	// Token: 0x020008D7 RID: 2263
	public static class MemberInfoExtensions
	{
		// Token: 0x06004B79 RID: 19321 RVA: 0x000F081C File Offset: 0x000EEA1C
		public static bool HasMetadataToken(this MemberInfo member)
		{
			Requires.NotNull(member, "member");
			bool flag;
			try
			{
				flag = MemberInfoExtensions.GetMetadataTokenOrZeroOrThrow(member) != 0;
			}
			catch (InvalidOperationException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x000F0858 File Offset: 0x000EEA58
		public static int GetMetadataToken(this MemberInfo member)
		{
			Requires.NotNull(member, "member");
			int metadataTokenOrZeroOrThrow = MemberInfoExtensions.GetMetadataTokenOrZeroOrThrow(member);
			if (metadataTokenOrZeroOrThrow == 0)
			{
				throw new InvalidOperationException("There is no metadata token available for the given member.");
			}
			return metadataTokenOrZeroOrThrow;
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x000F087C File Offset: 0x000EEA7C
		private static int GetMetadataTokenOrZeroOrThrow(MemberInfo member)
		{
			int metadataToken = member.MetadataToken;
			if ((metadataToken & 16777215) == 0)
			{
				return 0;
			}
			return metadataToken;
		}
	}
}
