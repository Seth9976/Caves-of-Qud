using System;

namespace Mono
{
	// Token: 0x020001A4 RID: 420
	internal static class Disposable
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x00017BBE File Offset: 0x00015DBE
		public static Disposable<T> Owned<T>(T value) where T : class, IDisposable
		{
			return new Disposable<T>(value, true);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00017BC7 File Offset: 0x00015DC7
		public static Disposable<T> NotOwned<T>(T value) where T : class, IDisposable
		{
			return new Disposable<T>(value, false);
		}
	}
}
