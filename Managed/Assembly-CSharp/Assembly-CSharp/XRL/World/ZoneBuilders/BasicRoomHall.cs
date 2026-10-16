using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A42 RID: 2626
	public class BasicRoomHall
	{
		// Token: 0x06006DCA RID: 28106 RVA: 0x00250D70 File Offset: 0x0024EF70
		public bool BuildZone(Zone Z)
		{
			int num = 0;
			int num2;
			int num3;
			for (;;)
			{
				IL_0002:
				num++;
				Popup._ScreenBuffer.Clear();
				this.zZone = Z;
				this.Map = new int[Z.Width, Z.Height];
				for (int i = 0; i < Z.Height; i++)
				{
					for (int j = 0; j < Z.Width; j++)
					{
						this.Map[j, i] = 0;
					}
				}
				num2 = Z.Width / 2;
				num3 = Z.Height / 2;
				MetricsManager.rngCheckpoint("roomhall1");
				foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
				{
					if (zoneConnection.X > 2 && zoneConnection.X < Z.Width - 2 && zoneConnection.Y > 2 && zoneConnection.Y < Z.Height - 2)
					{
						num2 = zoneConnection.X;
						num3 = zoneConnection.Y;
						goto IL_01A5;
					}
				}
				goto IL_010F;
				IL_01A5:
				while (!this.BuildRoom(num2, num3, Directions.GetRandomDirection()))
				{
				}
				MetricsManager.rngCheckpoint("roomhall3");
				foreach (ZoneConnection zoneConnection2 in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
				{
					if (this.Map[zoneConnection2.X, zoneConnection2.Y] == 0)
					{
						this.Draw();
						bool flag = false;
						if (zoneConnection2.Y == 0)
						{
							for (int k = 0; k < Z.Height - 1; k++)
							{
								if (this.Map[zoneConnection2.X, k] != 0)
								{
									flag = true;
									break;
								}
								this.Map[zoneConnection2.X, k] = 3;
							}
						}
						else if (zoneConnection2.Y == Z.Height - 1)
						{
							for (int l = zoneConnection2.Y; l > 0; l--)
							{
								if (this.Map[zoneConnection2.X, l] != 0)
								{
									flag = true;
									break;
								}
								this.Map[zoneConnection2.X, l] = 3;
							}
						}
						else if (zoneConnection2.X == 0)
						{
							for (int m = 0; m < Z.Width - 1; m++)
							{
								if (this.Map[m, zoneConnection2.Y] != 0)
								{
									flag = true;
									break;
								}
								this.Map[m, zoneConnection2.Y] = 3;
							}
						}
						else if (zoneConnection2.X == Z.Width - 1)
						{
							for (int n = zoneConnection2.X; n > 0; n--)
							{
								if (this.Map[n, zoneConnection2.Y] != 0)
								{
									flag = true;
									break;
								}
								this.Map[n, zoneConnection2.Y] = 3;
							}
						}
						else
						{
							if (zoneConnection2.X < 50)
							{
								for (int num4 = zoneConnection2.X; num4 > 0; num4--)
								{
									if (this.Map[num4, zoneConnection2.Y] != 0)
									{
										flag = true;
										break;
									}
									this.Map[num4, zoneConnection2.Y] = 3;
								}
							}
							if (zoneConnection2.X > 20)
							{
								for (int num5 = zoneConnection2.X; num5 < Z.Width; num5++)
								{
									if (this.Map[num5, zoneConnection2.Y] != 0)
									{
										flag = true;
										break;
									}
									this.Map[num5, zoneConnection2.Y] = 3;
								}
							}
							if (zoneConnection2.Y < 20)
							{
								for (int num6 = zoneConnection2.Y; num6 > 0; num6--)
								{
									if (this.Map[zoneConnection2.X, num6] != 0)
									{
										flag = true;
										break;
									}
									this.Map[zoneConnection2.X, num6] = 3;
								}
							}
							if (zoneConnection2.Y > 10)
							{
								for (int num7 = zoneConnection2.Y; num7 < Z.Height; num7++)
								{
									if (this.Map[zoneConnection2.X, num7] != 0)
									{
										flag = true;
										break;
									}
									this.Map[zoneConnection2.X, num7] = 3;
								}
							}
						}
						if (!flag && num < BasicRoomHall.MAX_ATTEMPTS)
						{
							goto IL_0002;
						}
					}
				}
				MetricsManager.rngCheckpoint("roomhall4");
				foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
				{
					if (cachedZoneConnection.TargetDirection == "-")
					{
						bool flag2 = false;
						if (cachedZoneConnection.Y == 0)
						{
							for (int num8 = 0; num8 < Z.Height - 1; num8++)
							{
								if (this.Map[cachedZoneConnection.X, num8] != 0)
								{
									flag2 = true;
									break;
								}
								this.Map[cachedZoneConnection.X, num8] = 3;
							}
						}
						else if (cachedZoneConnection.Y == Z.Height - 1)
						{
							for (int num9 = cachedZoneConnection.Y; num9 > 0; num9--)
							{
								if (this.Map[cachedZoneConnection.X, num9] != 0)
								{
									flag2 = true;
									break;
								}
								this.Map[cachedZoneConnection.X, num9] = 3;
							}
						}
						else if (cachedZoneConnection.X == 0)
						{
							for (int num10 = 0; num10 < Z.Width - 1; num10++)
							{
								if (this.Map[num10, cachedZoneConnection.Y] != 0)
								{
									flag2 = true;
									break;
								}
								this.Map[num10, cachedZoneConnection.Y] = 3;
							}
						}
						else if (cachedZoneConnection.X == Z.Width - 1)
						{
							for (int num11 = cachedZoneConnection.X; num11 > 0; num11--)
							{
								if (this.Map[num11, cachedZoneConnection.Y] != 0)
								{
									flag2 = true;
									break;
								}
								this.Map[num11, cachedZoneConnection.Y] = 3;
							}
						}
						else
						{
							if (cachedZoneConnection.X > 20)
							{
								for (int num12 = cachedZoneConnection.X; num12 > 0; num12--)
								{
									if (this.Map[num12, cachedZoneConnection.Y] != 0)
									{
										flag2 = true;
										break;
									}
									this.Map[num12, cachedZoneConnection.Y] = 3;
								}
							}
							if (cachedZoneConnection.X < 50)
							{
								for (int num13 = cachedZoneConnection.X; num13 < Z.Width; num13++)
								{
									if (this.Map[num13, cachedZoneConnection.Y] != 0)
									{
										flag2 = true;
										break;
									}
									this.Map[num13, cachedZoneConnection.Y] = 3;
								}
							}
							if (cachedZoneConnection.Y > 10)
							{
								for (int num14 = cachedZoneConnection.Y; num14 > 0; num14--)
								{
									if (this.Map[cachedZoneConnection.X, num14] != 0)
									{
										flag2 = true;
										break;
									}
									this.Map[cachedZoneConnection.X, num14] = 3;
								}
							}
							if (cachedZoneConnection.Y < 20)
							{
								for (int num15 = cachedZoneConnection.Y; num15 < Z.Height; num15++)
								{
									if (this.Map[cachedZoneConnection.X, num15] != 0)
									{
										flag2 = true;
										break;
									}
									this.Map[cachedZoneConnection.X, num15] = 3;
								}
							}
						}
						if (!flag2 && num < BasicRoomHall.MAX_ATTEMPTS)
						{
							goto IL_0002;
						}
					}
				}
				break;
				IL_010F:
				MetricsManager.rngCheckpoint("roomhall2");
				foreach (CachedZoneConnection cachedZoneConnection2 in Z.ZoneConnectionCache)
				{
					if (cachedZoneConnection2.TargetDirection == "-" && cachedZoneConnection2.X > 2 && cachedZoneConnection2.X < Z.Width - 2 && cachedZoneConnection2.Y > 2 && cachedZoneConnection2.Y < Z.Height - 2)
					{
						num2 = cachedZoneConnection2.X;
						num3 = cachedZoneConnection2.Y;
						break;
					}
				}
				goto IL_01A5;
			}
			MetricsManager.rngCheckpoint("roomhall5");
			for (int num16 = 0; num16 < Z.Height; num16++)
			{
				for (int num17 = 0; num17 < Z.Width; num17++)
				{
					if (this.Map[num17, num16] != 0)
					{
						Z.GetCell(num17, num16).Clear(null, false, false, null);
					}
					this.Map[num17, num16];
				}
			}
			MetricsManager.rngCheckpoint("roomhall6");
			new RiverBuilder().BuildZone(Z);
			Z.ClearReachableMap();
			Z.BuildReachableMap(num2, num3);
			return true;
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x00251698 File Offset: 0x0024F898
		private bool BuildRoom(int x, int y, string Direction)
		{
			int num = Stat.Random(5, 8);
			int num2;
			int num3;
			if (Direction.Contains("N"))
			{
				num2 = y - num;
				num3 = y;
			}
			else if (Direction.Contains("S"))
			{
				num2 = y;
				num3 = y + num;
			}
			else
			{
				num2 = (int)((double)y - Math.Floor((double)(num / 2)));
				num3 = (int)((double)y + Math.Ceiling((double)(num / 2)));
			}
			int num4;
			int num5;
			if (Direction.Contains("W"))
			{
				num4 = x - num;
				num5 = x;
			}
			else if (Direction.Contains("E"))
			{
				num4 = x;
				num5 = x + num;
			}
			else
			{
				num4 = (int)((double)x - Math.Floor((double)(num / 2)));
				num5 = (int)((double)x + Math.Ceiling((double)(num / 2)));
			}
			if (num4 < 1)
			{
				return false;
			}
			if (num2 < 1)
			{
				return false;
			}
			if (num5 > this.zZone.Width - 2)
			{
				return false;
			}
			if (num3 > this.zZone.Height - 2)
			{
				return false;
			}
			for (int i = num4; i <= num5; i++)
			{
				for (int j = num2; j <= num3; j++)
				{
					if (this.Map[i, j] != 0)
					{
						return false;
					}
				}
			}
			for (int k = num4; k <= num5; k++)
			{
				for (int l = num2; l <= num3; l++)
				{
					this.Map[k, l] = 1;
				}
			}
			int m = 0;
			int num6 = Stat.Random(6, 8);
			while (m < num6)
			{
				int num7 = m % 4 + 1;
				if (num7 == 1)
				{
					int num8 = 0;
					while (num8 < 10 && !this.BuildHallway(Stat.Random(num4, num5), num2 - 1, "N"))
					{
						num8++;
					}
				}
				if (num7 == 2)
				{
					int num9 = 0;
					while (num9 < 10 && !this.BuildHallway(Stat.Random(num4, num5), num3 + 1, "N"))
					{
						num9++;
					}
				}
				if (num7 == 3)
				{
					int num10 = 0;
					while (num10 < 10 && !this.BuildHallway(num4 - 1, Stat.Random(num2, num3), "W"))
					{
						num10++;
					}
				}
				if (num7 == 4)
				{
					int num11 = 0;
					while (num11 < 10 && !this.BuildHallway(num5 + 1, Stat.Random(num2, num3), "E"))
					{
						num11++;
					}
				}
				m++;
			}
			return true;
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x002518B4 File Offset: 0x0024FAB4
		private bool BuildHallway(int x, int y, string Direction)
		{
			if (x < 1)
			{
				return false;
			}
			if (y < 1)
			{
				return false;
			}
			if (x > this.zZone.Width - 2)
			{
				return false;
			}
			if (y > this.zZone.Height - 2)
			{
				return false;
			}
			int num = Stat.Random(1, 6) + Stat.Random(1, 6);
			int num2 = 0;
			int num3 = 0;
			if (Direction == "N")
			{
				num2 = x;
				num3 = y - num;
				if (this.Map[x + 1, y] != 0)
				{
					return false;
				}
				if (this.Map[x - 1, y] != 0)
				{
					return false;
				}
			}
			if (Direction == "S")
			{
				num2 = x;
				num3 = y - num;
				if (this.Map[x + 1, y] != 0)
				{
					return false;
				}
				if (this.Map[x - 1, y] != 0)
				{
					return false;
				}
			}
			if (Direction == "E")
			{
				num2 = x + num;
				num3 = y;
				if (this.Map[x, y - 1] != 0)
				{
					return false;
				}
				if (this.Map[x, y + 1] != 0)
				{
					return false;
				}
			}
			if (Direction == "W")
			{
				num2 = x - num;
				num3 = y;
				if (this.Map[x, y - 1] != 0)
				{
					return false;
				}
				if (this.Map[x, y + 1] != 0)
				{
					return false;
				}
			}
			if (num2 < 1)
			{
				return false;
			}
			if (num3 < 1)
			{
				return false;
			}
			if (num2 > this.zZone.Width - 2)
			{
				return false;
			}
			if (num3 > this.zZone.Height - 2)
			{
				return false;
			}
			int num4 = x;
			int num5 = y;
			for (int i = 0; i < num; i++)
			{
				if (this.Map[num4, num5] != 0)
				{
					return false;
				}
				Directions.ApplyDirection(Direction, ref num4, ref num5, 1);
			}
			num4 = x;
			num5 = y;
			for (int j = 0; j < num; j++)
			{
				this.Map[num4, num5] = 2;
				Directions.ApplyDirection(Direction, ref num4, ref num5, 1);
			}
			int num6 = 0;
			bool flag;
			while (!(flag = this.BuildRoom(num4, num5, Direction)) && num6 < 5)
			{
				num6++;
			}
			if (!flag)
			{
				num4 = x;
				num5 = y;
				for (int k = 0; k < num; k++)
				{
					this.Map[num4, num5] = 0;
					Directions.ApplyDirection(Direction, ref num4, ref num5, 1);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x00251AD2 File Offset: 0x0024FCD2
		public void Draw()
		{
		}

		// Token: 0x04003243 RID: 12867
		public static readonly int MAX_ATTEMPTS = 100;

		// Token: 0x04003244 RID: 12868
		public string WallFill;

		// Token: 0x04003245 RID: 12869
		private int[,] Map;

		// Token: 0x04003246 RID: 12870
		private Zone zZone;
	}
}
