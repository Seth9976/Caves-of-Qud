using System;

namespace Trivial.Mono
{
	// Token: 0x02000004 RID: 4
	internal static class Disposable
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
		public static Disposable<T> Owned<T>(T value) where T : class, IDisposable
		{
			return new Disposable<T>(value, true);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public static Disposable<T> NotOwned<T>(T value) where T : class, IDisposable
		{
			return new Disposable<T>(value, false);
		}
	}
}
