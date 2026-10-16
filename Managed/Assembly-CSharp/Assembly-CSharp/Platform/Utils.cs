using System;
using System.Runtime.CompilerServices;

namespace Platform
{
	// Token: 0x02000331 RID: 817
	public static class Utils
	{
		// Token: 0x06001E82 RID: 7810 RVA: 0x00094CC1 File Offset: 0x00092EC1
		public static bool IsStrictIO()
		{
			return true;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00094CC4 File Offset: 0x00092EC4
		public static bool OnlyAOT()
		{
			return false;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00094CC7 File Offset: 0x00092EC7
		[NullableContext(1)]
		public static bool ShouldOnlyBeCalledOnStandalone(string msg)
		{
			return false;
		}

		// Token: 0x0200171D RID: 5917
		public struct OnScopeExit : IDisposable
		{
			// Token: 0x0600DBEE RID: 56302 RVA: 0x00507261 File Offset: 0x00505461
			[NullableContext(1)]
			public OnScopeExit(Action cleanup)
			{
				this.Cleanup = cleanup;
			}

			// Token: 0x0600DBEF RID: 56303 RVA: 0x0050726A File Offset: 0x0050546A
			public void Dispose()
			{
				Action cleanup = this.Cleanup;
				if (cleanup == null)
				{
					return;
				}
				cleanup();
			}

			// Token: 0x04005CF6 RID: 23798
			[Nullable(1)]
			public Action Cleanup;
		}
	}
}
