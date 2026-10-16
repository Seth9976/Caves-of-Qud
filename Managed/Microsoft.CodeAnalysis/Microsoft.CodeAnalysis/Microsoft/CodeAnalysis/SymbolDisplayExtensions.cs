using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000205 RID: 517
	public static class SymbolDisplayExtensions
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x00040E14 File Offset: 0x0003F014
		[return: Nullable(1)]
		public static string ToDisplayString(this ImmutableArray<SymbolDisplayPart> parts)
		{
			if (parts.IsDefault)
			{
				throw new ArgumentException("parts");
			}
			if (parts.Length == 0)
			{
				return string.Empty;
			}
			if (parts.Length == 1)
			{
				return parts[0].ToString();
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			string text;
			try
			{
				StringBuilder builder = instance.Builder;
				foreach (SymbolDisplayPart symbolDisplayPart in parts)
				{
					builder.Append(symbolDisplayPart);
				}
				text = builder.ToString();
			}
			finally
			{
				instance.Free();
			}
			return text;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00040EC0 File Offset: 0x0003F0C0
		internal static bool IncludesOption(this SymbolDisplayCompilerInternalOptions options, SymbolDisplayCompilerInternalOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00040EC8 File Offset: 0x0003F0C8
		internal static bool IncludesOption(this SymbolDisplayGenericsOptions options, SymbolDisplayGenericsOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00040ED0 File Offset: 0x0003F0D0
		internal static bool IncludesOption(this SymbolDisplayMemberOptions options, SymbolDisplayMemberOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00040ED8 File Offset: 0x0003F0D8
		internal static bool IncludesOption(this SymbolDisplayMiscellaneousOptions options, SymbolDisplayMiscellaneousOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00040EE0 File Offset: 0x0003F0E0
		internal static bool IncludesOption(this SymbolDisplayParameterOptions options, SymbolDisplayParameterOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00040EE8 File Offset: 0x0003F0E8
		internal static bool IncludesOption(this SymbolDisplayKindOptions options, SymbolDisplayKindOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00040EF0 File Offset: 0x0003F0F0
		internal static bool IncludesOption(this SymbolDisplayLocalOptions options, SymbolDisplayLocalOptions flag)
		{
			return (options & flag) == flag;
		}
	}
}
