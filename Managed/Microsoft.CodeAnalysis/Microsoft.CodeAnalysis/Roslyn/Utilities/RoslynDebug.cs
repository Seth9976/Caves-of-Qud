using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000038 RID: 56
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RoslynDebug
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00004BFE File Offset: 0x00002DFE
		[Conditional("DEBUG")]
		public static void Assert([DoesNotReturnIf(false)] bool b)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004C00 File Offset: 0x00002E00
		[Conditional("DEBUG")]
		public static void Assert([DoesNotReturnIf(false)] bool b, string message)
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004C02 File Offset: 0x00002E02
		[Conditional("DEBUG")]
		public static void AssertNotNull<[Nullable(2)] T>([NotNull] T value)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004C04 File Offset: 0x00002E04
		[NullableContext(2)]
		[Conditional("DEBUG")]
		internal static void AssertOrFailFast([DoesNotReturnIf(false)] bool condition, string message = null)
		{
			if (!condition && !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("HELIX_DUMP_FOLDER")))
			{
				if (message == null)
				{
					message = "AssertOrFailFast failed";
				}
				object obj = new StackTrace();
				Console.WriteLine(message);
				Console.WriteLine(obj);
				Environment.FailFast(message);
			}
		}
	}
}
