using System;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A86 RID: 2694
	public class Flowerfields
	{
		// Token: 0x06006E94 RID: 28308 RVA: 0x0025E340 File Offset: 0x0025C540
		public bool BuildZone(Zone Z)
		{
			NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 6, 80, 80, 4, 3, 0, 1, null);
			NoiseMap noiseMap2 = new NoiseMap(80, 25, 10, 3, 3, 6, 80, 80, 4, 3, 0, 1, null);
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (noiseMap.Noise[j, i] > 1 && (!this.Underground || Z.GetCell(j, i).IsPassable(null, true)))
					{
						if (Stat.Rnd.Next(100) < 30)
						{
							if (!Z.GetCell(j, i).IsOccluding())
							{
								Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject("Dogthorn Tree"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else if (Stat.Rnd.Next(100) < 30)
						{
							if (!Z.GetCell(j, i).IsOccluding())
							{
								Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject("Witchwood Tree"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else if (Stat.Rnd.Next(100) < 1)
						{
							if (!Z.GetCell(j, i).IsOccluding())
							{
								Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject("Lahbloom"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else if (Stat.Rnd.Next(1000) < 5)
						{
							if (Stat.Rnd.Next(100) < 50)
							{
								if (!Z.GetCell(j, i).IsOccluding())
								{
									Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject("Starapple Tree"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
							else if (!Z.GetCell(j, i).IsOccluding())
							{
								Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject("Feral Lah"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
					else if (noiseMap2.Noise[j, i] > 1 && Stat.Rnd.Next(100) < 85 && Z.GetCell(j, i).RenderedObjectsCount == 0)
					{
						Grassy.PaintCell(Z.GetCell(j, i));
					}
				}
			}
			if (!this.Underground)
			{
				Z.ClearReachableMap();
				if (Z.BuildReachableMap(0, 0) < 400)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400329B RID: 12955
		public bool Underground;
	}
}
