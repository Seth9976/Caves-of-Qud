using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x0200129B RID: 4763
	[Serializable]
	public class TerrainTravelFungal : IPart
	{
		// Token: 0x0600BC50 RID: 48208 RVA: 0x0044D70B File Offset: 0x0044B90B
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600BC51 RID: 48209 RVA: 0x0044D70E File Offset: 0x0044B90E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CheckLostChance");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC52 RID: 48210 RVA: 0x0044D724 File Offset: 0x0044B924
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CheckLostChance")
			{
				if (The.Core.IDKFA)
				{
					return false;
				}
				Cell currentCell = The.Player.CurrentCell;
				string zoneWorld = currentCell.ParentZone.GetZoneWorld();
				int x = currentCell.X;
				int y = currentCell.Y;
				int num = Stat.Random(0, 2);
				int num2 = Stat.Random(0, 2);
				int num3 = 10;
				if (x == FungalJungle.UpperLeft.x)
				{
					num = 0;
				}
				if (y == FungalJungle.UpperLeft.y)
				{
					num2 = 0;
				}
				if (x == FungalJungle.LowerRight.x)
				{
					num = 2;
				}
				if (y == FungalJungle.LowerRight.y)
				{
					num2 = 2;
				}
				string text = ZoneID.Assemble(zoneWorld, x, y, num, num2, num3);
				Lost lost = new Lost(9999, text, zoneWorld, 10, false);
				if (The.Player.ApplyEffect(lost, null))
				{
					Zone zone = The.ZoneManager.GetZone(text);
					Zone zone2 = The.ZoneManager.SetActiveZone(zone);
					zone2.CheckWeather();
					The.Player.SystemMoveTo(zone2.GetPullDownLocation(The.Player), null, false, true, false, false);
					Popup.ShowBlock("You lose your way beneath a dense canopy of spores.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					The.ZoneManager.ProcessGoToPartyLeader();
					The.Player.FireEvent(Event.New("AfterLost", "FromCell", currentCell));
					return false;
				}
			}
			return base.FireEvent(E);
		}
	}
}
