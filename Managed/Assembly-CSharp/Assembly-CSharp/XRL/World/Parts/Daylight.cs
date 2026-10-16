using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F55 RID: 3925
	[Serializable]
	public class Daylight : IPart
	{
		// Token: 0x06009C95 RID: 40085 RVA: 0x0038C556 File Offset: 0x0038A756
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009C96 RID: 40086 RVA: 0x0038C559 File Offset: 0x0038A759
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x0038C570 File Offset: 0x0038A770
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			Cell playerCell = The.PlayerCell;
			if (playerCell == null || playerCell.ParentZone.IsInside())
			{
				return base.HandleEvent(E);
			}
			int num = Calendar.CurrentDaySegment / 500;
			int num2 = (int)((float)(Calendar.CurrentDaySegment - 500 * num) / 8.33333f);
			int num3 = 5;
			if (num < 5)
			{
				num3 = 0;
			}
			else if (num >= 5 && (num < 18 || (num == 18 && num2 < 15)))
			{
				num3 = (Calendar.CurrentDaySegment - 2500) / 10;
			}
			else
			{
				num3 = 80 - (Calendar.CurrentDaySegment - 9124) / 10;
				if (num3 < 0)
				{
					num3 = 0;
				}
			}
			LightLevel lightLevel = LightLevel.Light;
			GetAmbientLightEvent.Send(this, "Daylight", ref lightLevel, ref num3);
			if (num3 > playerCell.ParentZone.Width)
			{
				playerCell.ParentZone.AddLight(lightLevel, false);
			}
			else if (num3 > 0)
			{
				playerCell.ParentZone.AddLight(playerCell.X, playerCell.Y, num3, lightLevel, false);
			}
			if (num3 < 3)
			{
				playerCell.ParentZone.AddExplored(playerCell.X, playerCell.Y, 3);
			}
			return base.HandleEvent(E);
		}
	}
}
