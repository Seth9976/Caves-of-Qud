using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerMovieTexture : DownloadHandler
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002205 File Offset: 0x00000405
		public DownloadHandlerMovieTexture()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002218 File Offset: 0x00000418
		protected override byte[] GetData()
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002231 File Offset: 0x00000431
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for movies");
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002240 File Offset: 0x00000440
		public MovieTexture movieTexture
		{
			get
			{
				DownloadHandlerMovieTexture.FeatureRemoved();
				return null;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000225C File Offset: 0x0000045C
		public static MovieTexture GetContent(UnityWebRequest uwr)
		{
			DownloadHandlerMovieTexture.FeatureRemoved();
			return null;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002275 File Offset: 0x00000475
		private static void FeatureRemoved()
		{
			throw new Exception("Movie texture has been removed, use VideoPlayer instead");
		}
	}
}
