using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000005 RID: 5
	public static class UnityWebRequestMultimedia
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002284 File Offset: 0x00000484
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAudioClip(uri, audioType), null);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022AC File Offset: 0x000004AC
		public static UnityWebRequest GetAudioClip(Uri uri, AudioType audioType)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAudioClip(uri, audioType), null);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022D4 File Offset: 0x000004D4
		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
		public static UnityWebRequest GetMovieTexture(string uri)
		{
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022E8 File Offset: 0x000004E8
		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", true)]
		public static UnityWebRequest GetMovieTexture(Uri uri)
		{
			return null;
		}
	}
}
