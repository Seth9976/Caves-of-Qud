using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x0200004D RID: 77
	public interface IProfanityService
	{
		// Token: 0x06000100 RID: 256
		int CleanProfanity(Region region, string inputText, out string cleanedText);

		// Token: 0x06000101 RID: 257
		void CleanProfanity(Region region, string inputText, OnCleanProfanityComplete callback);
	}
}
