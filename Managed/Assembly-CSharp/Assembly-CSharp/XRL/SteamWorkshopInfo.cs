using System;
using Newtonsoft.Json;
using Steamworks;
using UnityEngine;
using XRL.Serialization;

namespace XRL
{
	// Token: 0x02000527 RID: 1319
	public class SteamWorkshopInfo
	{
		/// <summary>
		/// Open the mod's workshop page in the steam overlay, client, or browser.
		/// </summary>
		// Token: 0x06003888 RID: 14472 RVA: 0x00115BA0 File Offset: 0x00113DA0
		public void OpenWorkshopPage()
		{
			if (this.WorkshopId == 0UL)
			{
				return;
			}
			if (!PlatformManager.IsInitialized())
			{
				Application.OpenURL("https://steamcommunity.com/sharedfiles/filedetails/?id=" + this.WorkshopId.ToString());
				return;
			}
			string text = "steam://url/CommunityFilePage/" + this.WorkshopId.ToString();
			if (SteamUtils.IsOverlayEnabled())
			{
				SteamFriends.ActivateGameOverlayToWebPage(text, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
				return;
			}
			Application.OpenURL(text);
		}

		// Token: 0x04001C7E RID: 7294
		public ulong WorkshopId;

		// Token: 0x04001C7F RID: 7295
		public string Title;

		// Token: 0x04001C80 RID: 7296
		public string Description;

		// Token: 0x04001C81 RID: 7297
		[JsonConverter(typeof(CommaDelimitedArrayConverter))]
		public string[] Tags;

		// Token: 0x04001C82 RID: 7298
		public string Visibility;

		// Token: 0x04001C83 RID: 7299
		public string ImagePath;
	}
}
