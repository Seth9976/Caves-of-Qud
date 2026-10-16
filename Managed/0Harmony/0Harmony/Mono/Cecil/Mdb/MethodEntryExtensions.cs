using System;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.Cecil.Mdb
{
	// Token: 0x0200031D RID: 797
	internal static class MethodEntryExtensions
	{
		// Token: 0x0600140F RID: 5135 RVA: 0x0004153F File Offset: 0x0003F73F
		public static bool HasColumnInfo(this MethodEntry entry)
		{
			return (entry.MethodFlags & MethodEntry.Flags.ColumnsInfoIncluded) > (MethodEntry.Flags)0;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0004154C File Offset: 0x0003F74C
		public static bool HasEndInfo(this MethodEntry entry)
		{
			return (entry.MethodFlags & MethodEntry.Flags.EndInfoIncluded) > (MethodEntry.Flags)0;
		}
	}
}
