using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001276 RID: 4726
	[Serializable]
	public class OmonporchGrove : IPart
	{
		// Token: 0x0600BB25 RID: 47909 RVA: 0x00445BC4 File Offset: 0x00443DC4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB26 RID: 47910 RVA: 0x00445BC7 File Offset: 0x00443DC7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB27 RID: 47911 RVA: 0x00445BE0 File Offset: 0x00443DE0
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			try
			{
				Zone currentZone = this.ParentObject.CurrentZone;
				int num = 0;
				for (int i = 0; i < currentZone.Height; i++)
				{
					for (int j = 0; j < currentZone.Width; j++)
					{
						foreach (GameObject gameObject in currentZone.GetCell(j, i).GetObjectsWithPart("PlantProperties"))
						{
							num++;
							gameObject.RequirePart<RipePlant>(false);
						}
					}
				}
				int k = 0;
				int num2 = 200 - num;
				while (k < num2)
				{
					int num3 = 0;
					while (++num3 <= 100000)
					{
						int num4 = Stat.Random(0, 79);
						int num5 = Stat.Random(0, 24);
						Cell cell = currentZone.GetCell(num4, num5);
						if (cell != null && cell.IsEmpty() && cell.IsReachable())
						{
							GameObject gameObject2 = GameObject.Create("Red Death Dacca");
							gameObject2.RequirePart<RipePlant>(false);
							cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							break;
						}
					}
					k++;
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}
	}
}
