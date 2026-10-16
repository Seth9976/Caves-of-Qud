using System;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x02000925 RID: 2341
	[Serializable]
	public class DynamicQuestRewardElement_GameObject : DynamicQuestRewardElement
	{
		// Token: 0x060059F5 RID: 23029 RVA: 0x001B485E File Offset: 0x001B2A5E
		public DynamicQuestRewardElement_GameObject()
		{
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x001B4866 File Offset: 0x001B2A66
		public DynamicQuestRewardElement_GameObject(GameObject go)
			: this()
		{
			this.cacheID = The.ZoneManager.CacheObject(go, false, false);
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x001B4881 File Offset: 0x001B2A81
		public override string getRewardConversationType()
		{
			return null;
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x001B4884 File Offset: 0x001B2A84
		public override string getRewardAcceptQuestText()
		{
			return null;
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x001B4888 File Offset: 0x001B2A88
		public override void award()
		{
			GameObject gameObject = The.ZoneManager.PullCachedObject(this.cacheID, true);
			gameObject.MakeUnderstood(false);
			Popup.Show("You receive " + gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			The.Player.ReceiveObject(gameObject, false, null);
		}

		// Token: 0x04002C93 RID: 11411
		public string cacheID;
	}
}
