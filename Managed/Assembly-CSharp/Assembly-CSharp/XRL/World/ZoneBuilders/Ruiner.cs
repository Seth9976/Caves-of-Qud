using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ACF RID: 2767
	public class Ruiner
	{
		// Token: 0x06006FCF RID: 28623 RVA: 0x00276590 File Offset: 0x00274790
		public void BlowUp(int x, int y, int Force, bool bUnderground)
		{
			Physics.LegacyApplyExplosion(this.Z.GetCell(x, y), new List<Cell>(), new List<GameObject>(), Force, true, false, null, null, 1, 0.05f);
		}

		// Token: 0x06006FD0 RID: 28624 RVA: 0x002765C4 File Offset: 0x002747C4
		public bool BuildZone(Zone _Z)
		{
			return this.RuinZone(_Z, this.RuinAmount, true, 100000, 500000);
		}

		// Token: 0x06006FD1 RID: 28625 RVA: 0x002765E0 File Offset: 0x002747E0
		public bool RuinZone(Zone _Z, int RuinLevel, bool bUnderground, int SurfaceExplosionForce = 100000, int UndergroundExplosionForce = 500000)
		{
			this.Z = _Z;
			int num = Stat.Random(0, 3);
			num += RuinLevel / 5;
			int num2 = (bUnderground ? UndergroundExplosionForce : SurfaceExplosionForce);
			for (int i = 0; i < num; i++)
			{
				this.BlowUp(Stat.Random(0, this.Z.Width - 1), Stat.Random(0, this.Z.Height - 1), num2, bUnderground);
			}
			if (!bUnderground)
			{
				_Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			foreach (CachedZoneConnection cachedZoneConnection in this.Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection != "-")
				{
					this.Z.GetCell(cachedZoneConnection.X, cachedZoneConnection.Y).Clear(null, false, false, null);
				}
			}
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(this.Z.ZoneID))
			{
				this.Z.GetCell(zoneConnection.X, zoneConnection.Y).Clear(null, false, false, null);
			}
			Ruiner.CleanNarrowConnections(this.Z);
			this.CleanOrphanDoors(this.Z);
			this.Z.ClearReachableMap();
			foreach (CachedZoneConnection cachedZoneConnection2 in this.Z.ZoneConnectionCache)
			{
				this.Z.GetCell(cachedZoneConnection2.X, cachedZoneConnection2.Y).Clear(null, false, false, null);
				if (this.Z.BuildReachableMap(cachedZoneConnection2.X, cachedZoneConnection2.Y) > 400)
				{
					return true;
				}
				this.Z.ClearReachableMap();
			}
			for (int j = 0; j < 10; j++)
			{
				if (this.Z.BuildReachableMap(Stat.Random(0, this.Z.Width - 1), Stat.Random(0, this.Z.Height - 1)) > 400)
				{
					return true;
				}
				this.Z.ClearReachableMap();
			}
			return false;
		}

		// Token: 0x06006FD2 RID: 28626 RVA: 0x0027687C File Offset: 0x00274A7C
		public static void CleanNarrowConnections(Zone Z)
		{
			bool flag = true;
			while (flag)
			{
				flag = false;
				int i = 1;
				int num = Z.Height - 1;
				while (i < num)
				{
					int j = 1;
					int num2 = Z.Width - 1;
					while (j < num2)
					{
						if (Z.GetCell(j, i).IsEmpty())
						{
							if (Z.GetCell(j + 1, i).HasWall() && Z.GetCell(j, i + 1).HasWall() && !Z.GetCell(j + 1, i + 1).HasWall())
							{
								if (Stat.Random(0, 1) == 0)
								{
									Z.GetCell(j + 1, i).Clear(null, false, false, null);
								}
								else
								{
									Z.GetCell(j, i + 1).Clear(null, false, false, null);
								}
								flag = true;
							}
							if (Z.GetCell(j + 1, i).HasWall() && Z.GetCell(j, i - 1).HasWall() && !Z.GetCell(j + 1, i - 1).HasWall())
							{
								flag = true;
								if (Stat.Random(0, 1) == 0)
								{
									Z.GetCell(j + 1, i).Clear(null, false, false, null);
								}
								else
								{
									Z.GetCell(j, i - 1).Clear(null, false, false, null);
								}
							}
						}
						j++;
					}
					i++;
				}
			}
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x002769B8 File Offset: 0x00274BB8
		private void CleanOrphanDoors(Zone Z)
		{
			int i = 1;
			int num = Z.Height - 1;
			while (i < num)
			{
				int j = 1;
				int num2 = Z.Width - 1;
				while (j < num2)
				{
					if (Z.GetCell(j, i).HasObjectWithPart("Door"))
					{
						int num3 = 0;
						int num4 = 0;
						if (Z.GetCell(j - 1, i).HasWall())
						{
							num4++;
						}
						if (Z.GetCell(j + 1, i).HasWall())
						{
							num4++;
						}
						if (Z.GetCell(j, i - 1).HasWall())
						{
							num3++;
						}
						if (Z.GetCell(j, i + 1).HasWall())
						{
							num3++;
						}
						if (num3 + num4 != 2 || num3 == 1 || num4 == 1)
						{
							Z.GetCell(j, i).Clear(null, false, false, null);
						}
					}
					j++;
				}
				i++;
			}
		}

		// Token: 0x0400336D RID: 13165
		private Zone Z;

		// Token: 0x0400336E RID: 13166
		public int RuinAmount = 50;
	}
}
