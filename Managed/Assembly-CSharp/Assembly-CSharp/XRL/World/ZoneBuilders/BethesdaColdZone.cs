using System;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A45 RID: 2629
	public class BethesdaColdZone
	{
		// Token: 0x06006DD4 RID: 28116 RVA: 0x00252690 File Offset: 0x00250890
		public bool BuildZone(Zone Z)
		{
			Z.BaseTemperature = 25 - (Z.Z - 10) * 9;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					Cell cell = Z.Map[j][i];
					BlueTile.PaintCell(cell);
					int k = 0;
					int count = cell.Objects.Count;
					while (k < count)
					{
						GameObject gameObject = cell.Objects[k];
						if (gameObject.Physics != null)
						{
							if (gameObject.HasProperty("StartFrozen"))
							{
								gameObject.Physics._Temperature = gameObject.Physics.BrittleTemperature - 30;
							}
							else if (gameObject.Stat("ColdResistance", 0) < 100 || gameObject.IsScenery || gameObject.IsWall())
							{
								gameObject.Physics._Temperature = Z.BaseTemperature;
							}
						}
						k++;
					}
				}
			}
			return true;
		}
	}
}
