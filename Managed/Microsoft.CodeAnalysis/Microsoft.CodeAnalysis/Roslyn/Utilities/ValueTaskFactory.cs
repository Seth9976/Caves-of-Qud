using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Roslyn.Utilities
{
	// Token: 0x0200006B RID: 107
	internal static class ValueTaskFactory
	{
		// Token: 0x06000286 RID: 646 RVA: 0x000091A4 File Offset: 0x000073A4
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ValueTask<T> FromResult<[Nullable(2)] T>(T result)
		{
			return new ValueTask<T>(result);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000091AC File Offset: 0x000073AC
		public static ValueTask CompletedTask
		{
			get
			{
				return default(ValueTask);
			}
		}
	}
}
