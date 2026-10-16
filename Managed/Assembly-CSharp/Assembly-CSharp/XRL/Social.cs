using System;

namespace XRL
{
	// Token: 0x020004EB RID: 1259
	public static class Social
	{
		// Token: 0x0600365A RID: 13914 RVA: 0x001060FA File Offset: 0x001042FA
		public static void ShareToTwitter(string textToDisplay)
		{
			Social.TweetThis = textToDisplay;
		}

		// Token: 0x04001B34 RID: 6964
		public static string TweetThis;
	}
}
