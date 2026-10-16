using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001059 RID: 4185
	[Serializable]
	public class KindrishProperties : LowStatBooster
	{
		// Token: 0x0600A69A RID: 42650 RVA: 0x003CF064 File Offset: 0x003CD264
		public static bool ReturnAward()
		{
			The.Game.PlayerReputation.Modify("Hindren", 400, "Quest", null, null, false, false);
			GameObject gameObject = GameObject.Create("Force Bracelet", 0, 1, null, null, null, null, null);
			gameObject.MakeUnderstood(false);
			Popup.Show("You receive " + gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			IComponent<GameObject>.ThePlayer.ReceiveObject(gameObject, false, null);
			return true;
		}

		// Token: 0x0600A69B RID: 42651 RVA: 0x003CF100 File Offset: 0x003CD300
		public KindrishProperties()
		{
			base.AffectedStats = "Strength,Agility,Toughness,Intelligence,Willpower,Ego";
			base.Amount = 3;
			this.DescribeStatusForProperty = null;
		}
	}
}
