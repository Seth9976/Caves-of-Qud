using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000006 RID: 6
	public static class UnityWebRequestTexture
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002220 File Offset: 0x00000420
		public static UnityWebRequest GetTexture(string uri)
		{
			return UnityWebRequestTexture.GetTexture(uri, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000223C File Offset: 0x0000043C
		public static UnityWebRequest GetTexture(Uri uri)
		{
			return UnityWebRequestTexture.GetTexture(uri, false);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002258 File Offset: 0x00000458
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerTexture(!nonReadable), null);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002280 File Offset: 0x00000480
		public static UnityWebRequest GetTexture(Uri uri, bool nonReadable)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerTexture(!nonReadable), null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022A8 File Offset: 0x000004A8
		public static UnityWebRequest GetTexture(string uri, DownloadedTextureParams parameters)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerTexture(parameters), null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022CC File Offset: 0x000004CC
		public static UnityWebRequest GetTexture(Uri uri, DownloadedTextureParams parameters)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerTexture(parameters), null);
		}
	}
}
