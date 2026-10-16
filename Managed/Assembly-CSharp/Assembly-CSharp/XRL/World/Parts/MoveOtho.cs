using System;

namespace XRL.World.Parts
{
	// Token: 0x0200121E RID: 4638
	[Serializable]
	public class MoveOtho : IPart
	{
		// Token: 0x0600B837 RID: 47159 RVA: 0x0042FE49 File Offset: 0x0042E049
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B838 RID: 47160 RVA: 0x0042FE4C File Offset: 0x0042E04C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == TakenEvent.ID;
		}

		// Token: 0x0600B839 RID: 47161 RVA: 0x0042FE64 File Offset: 0x0042E064
		public override bool HandleEvent(TakenEvent E)
		{
			if (E.Actor != null && E.Actor.IsPlayer())
			{
				Zone zone = The.ZoneManager.GetZone("JoppaWorld.22.14.1.0.13");
				for (int i = 0; i < 80; i++)
				{
					for (int j = 0; j < 25; j++)
					{
						Cell cell = zone.GetCell(i, j);
						foreach (GameObject gameObject in cell.GetObjectsWithPart("Combat"))
						{
							if (gameObject.Blueprint == "Otho")
							{
								cell.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
								zone.GetCell(39, 13).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}
	}
}
