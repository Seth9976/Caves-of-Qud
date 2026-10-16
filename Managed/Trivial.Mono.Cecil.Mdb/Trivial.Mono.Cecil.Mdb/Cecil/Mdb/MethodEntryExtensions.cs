using System;
using Trivial.Mono.CompilerServices.SymbolWriter;

namespace Trivial.Mono.Cecil.Mdb
{
	// Token: 0x02000020 RID: 32
	internal static class MethodEntryExtensions
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000061E3 File Offset: 0x000043E3
		public static bool HasColumnInfo(this MethodEntry entry)
		{
			return (entry.MethodFlags & MethodEntry.Flags.ColumnsInfoIncluded) > (MethodEntry.Flags)0;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000061F0 File Offset: 0x000043F0
		public static bool HasEndInfo(this MethodEntry entry)
		{
			return (entry.MethodFlags & MethodEntry.Flags.EndInfoIncluded) > (MethodEntry.Flags)0;
		}
	}
}
