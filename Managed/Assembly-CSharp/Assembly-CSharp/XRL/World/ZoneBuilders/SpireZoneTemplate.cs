using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AEB RID: 2795
	[Serializable]
	public class SpireZoneTemplate : IComposite
	{
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06007016 RID: 28694 RVA: 0x0027B19D File Offset: 0x0027939D
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007017 RID: 28695 RVA: 0x0027B1A0 File Offset: 0x002793A0
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteObject(this.Template);
			Writer.WriteOptimized(this.Width);
			Writer.WriteOptimized(this.Height);
			Writer.WriteOptimized(this.ZonesWide);
			Writer.WriteOptimized(this.ZonesHigh);
		}

		// Token: 0x06007018 RID: 28696 RVA: 0x0027B1E0 File Offset: 0x002793E0
		public void Read(SerializationReader Reader)
		{
			this.Template = Reader.ReadObject() as SpireTemplate;
			this.Width = Reader.ReadOptimizedInt32();
			this.Height = Reader.ReadOptimizedInt32();
			this.ZonesWide = Reader.ReadOptimizedInt32();
			this.ZonesHigh = Reader.ReadOptimizedInt32();
		}

		// Token: 0x06007019 RID: 28697 RVA: 0x0027B230 File Offset: 0x00279430
		public SpireZoneTemplate Copy()
		{
			return new SpireZoneTemplate
			{
				Width = this.Width,
				Height = this.Height,
				ZonesHigh = this.ZonesHigh,
				ZonesWide = this.ZonesWide,
				Template = new SpireTemplate(this.Template)
			};
		}

		// Token: 0x0600701A RID: 28698 RVA: 0x0027B284 File Offset: 0x00279484
		public bool EnsureConnections(Zone Z)
		{
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (this.Template.Map[zoneConnection.X, zoneConnection.Y] != SpireTemplateTile.Inside)
				{
					bool flag = false;
					if (zoneConnection.Y == 0)
					{
						for (int i = 0; i < Z.Height - 1; i++)
						{
							if (this.Template.Map[zoneConnection.X, i] == SpireTemplateTile.Inside)
							{
								flag = true;
								break;
							}
							this.Template.Map[zoneConnection.X, i] = SpireTemplateTile.Inside;
						}
					}
					else if (zoneConnection.Y == Z.Height - 1)
					{
						for (int j = zoneConnection.Y; j > 0; j--)
						{
							if (this.Template.Map[zoneConnection.X, j] == SpireTemplateTile.Inside)
							{
								flag = true;
								break;
							}
							this.Template.Map[zoneConnection.X, j] = SpireTemplateTile.Inside;
						}
					}
					else if (zoneConnection.X == 0)
					{
						for (int k = 0; k < Z.Width - 1; k++)
						{
							if (this.Template.Map[k, zoneConnection.Y] == SpireTemplateTile.Inside)
							{
								flag = true;
								break;
							}
							this.Template.Map[k, zoneConnection.Y] = SpireTemplateTile.Inside;
						}
					}
					else if (zoneConnection.X == Z.Width - 1)
					{
						for (int l = zoneConnection.X; l > 0; l--)
						{
							if (this.Template.Map[l, zoneConnection.Y] == SpireTemplateTile.Inside)
							{
								flag = true;
								break;
							}
							this.Template.Map[l, zoneConnection.Y] = SpireTemplateTile.Inside;
						}
					}
					else
					{
						if (zoneConnection.X < 50)
						{
							for (int m = zoneConnection.X; m < Z.Width; m++)
							{
								if (this.Template.Map[m, zoneConnection.Y] == SpireTemplateTile.Inside)
								{
									flag = true;
									break;
								}
								this.Template.Map[m, zoneConnection.Y] = SpireTemplateTile.Inside;
							}
						}
						if (zoneConnection.X > 20)
						{
							for (int n = zoneConnection.X; n > 0; n--)
							{
								if (this.Template.Map[n, zoneConnection.Y] == SpireTemplateTile.Inside)
								{
									flag = true;
									break;
								}
								this.Template.Map[n, zoneConnection.Y] = SpireTemplateTile.Inside;
							}
						}
						if (zoneConnection.Y < 20)
						{
							for (int num = zoneConnection.Y; num < Z.Height; num++)
							{
								if (this.Template.Map[zoneConnection.X, num] == SpireTemplateTile.Inside)
								{
									flag = true;
									break;
								}
								this.Template.Map[zoneConnection.X, num] = SpireTemplateTile.Inside;
							}
						}
						if (zoneConnection.Y > 10)
						{
							for (int num2 = zoneConnection.Y; num2 > 0; num2--)
							{
								if (this.Template.Map[zoneConnection.X, num2] == SpireTemplateTile.Inside)
								{
									flag = true;
									break;
								}
								this.Template.Map[zoneConnection.X, num2] = SpireTemplateTile.Inside;
							}
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-")
				{
					bool flag2 = false;
					if (cachedZoneConnection.Y == 0)
					{
						for (int num3 = 0; num3 < Z.Height - 1; num3++)
						{
							if (this.Template.Map[cachedZoneConnection.X, num3] == SpireTemplateTile.Inside)
							{
								flag2 = true;
								break;
							}
							this.Template.Map[cachedZoneConnection.X, num3] = SpireTemplateTile.Inside;
						}
					}
					else if (cachedZoneConnection.Y == Z.Height - 1)
					{
						for (int num4 = cachedZoneConnection.Y; num4 > 0; num4--)
						{
							if (this.Template.Map[cachedZoneConnection.X, num4] == SpireTemplateTile.Inside)
							{
								flag2 = true;
								break;
							}
							this.Template.Map[cachedZoneConnection.X, num4] = SpireTemplateTile.Inside;
						}
					}
					else if (cachedZoneConnection.X == 0)
					{
						for (int num5 = 0; num5 < Z.Width - 1; num5++)
						{
							if (this.Template.Map[num5, cachedZoneConnection.Y] == SpireTemplateTile.Inside)
							{
								flag2 = true;
								break;
							}
							this.Template.Map[num5, cachedZoneConnection.Y] = SpireTemplateTile.Inside;
						}
					}
					else if (cachedZoneConnection.X == Z.Width - 1)
					{
						for (int num6 = cachedZoneConnection.X; num6 > 0; num6--)
						{
							if (this.Template.Map[num6, cachedZoneConnection.Y] == SpireTemplateTile.Inside)
							{
								flag2 = true;
								break;
							}
							this.Template.Map[num6, cachedZoneConnection.Y] = SpireTemplateTile.Inside;
						}
					}
					else
					{
						if (cachedZoneConnection.X > 20)
						{
							for (int num7 = cachedZoneConnection.X; num7 > 0; num7--)
							{
								if (this.Template.Map[num7, cachedZoneConnection.Y] == SpireTemplateTile.Inside)
								{
									flag2 = true;
									break;
								}
								this.Template.Map[num7, cachedZoneConnection.Y] = SpireTemplateTile.Inside;
							}
						}
						if (cachedZoneConnection.X < 50)
						{
							for (int num8 = cachedZoneConnection.X; num8 < Z.Width; num8++)
							{
								if (this.Template.Map[num8, cachedZoneConnection.Y] == SpireTemplateTile.Inside)
								{
									flag2 = true;
									break;
								}
								this.Template.Map[num8, cachedZoneConnection.Y] = SpireTemplateTile.Inside;
							}
						}
						if (cachedZoneConnection.Y > 10)
						{
							for (int num9 = cachedZoneConnection.Y; num9 < Z.Height; num9++)
							{
								if (this.Template.Map[cachedZoneConnection.X, num9] == SpireTemplateTile.Inside)
								{
									flag2 = true;
									break;
								}
								this.Template.Map[cachedZoneConnection.X, num9] = SpireTemplateTile.Inside;
							}
						}
						if (cachedZoneConnection.Y < 20)
						{
							for (int num10 = cachedZoneConnection.Y; num10 > 0; num10--)
							{
								if (this.Template.Map[cachedZoneConnection.X, num10] == SpireTemplateTile.Inside)
								{
									flag2 = true;
									break;
								}
								this.Template.Map[cachedZoneConnection.X, num10] = SpireTemplateTile.Inside;
							}
						}
					}
					if (!flag2)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600701B RID: 28699 RVA: 0x0027B998 File Offset: 0x00279B98
		public void GenerateRooms(int Depth)
		{
			this.Template.GenerateRooms(Depth);
		}

		// Token: 0x0600701C RID: 28700 RVA: 0x0027B9A8 File Offset: 0x00279BA8
		public void New(int nWidth, int nHeight, int ZonesWide, int ZonesHigh)
		{
			this.Template = new SpireTemplate(nWidth, nHeight);
			this.Width = nWidth;
			this.Height = nHeight;
			for (int i = 0; i < ZonesWide; i++)
			{
				for (int j = 0; j < ZonesHigh; j++)
				{
					int num = 80 / ZonesWide;
					int num2 = 25 / ZonesHigh;
					int num3 = num * i;
					int num4 = num2 * j;
					SpireTemplate spireTemplate = new SpireTemplate(num, num2, 20);
					this.Template.AddMap(num3, num4, spireTemplate);
				}
			}
		}

		// Token: 0x0600701D RID: 28701 RVA: 0x0027BA1C File Offset: 0x00279C1C
		public void BuildZone(Zone Z, bool bUnderground)
		{
			SpireZoneTemplate.foo++;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			List<Cell> list = new List<Cell>();
			int num = Z.GetZoneZ() - 10;
			bool flag = false;
			for (int i = 0; i < this.Template.Height; i++)
			{
				for (int j = 0; j < this.Template.Width; j++)
				{
					SpireTemplateTile spireTemplateTile = this.Template.Map[j, i];
					Cell cell = Z.GetCell(j, i);
					if (spireTemplateTile == SpireTemplateTile.OutsideWall || spireTemplateTile == SpireTemplateTile.Wall)
					{
						cell.SetReachable(false);
						cell.AddObject("Fulcrete", null, null, null, null);
					}
					else if (spireTemplateTile == SpireTemplateTile.SecurityDoor)
					{
						int num2 = "2d10".RollCached() + num;
						string text;
						if (num2 < 10)
						{
							text = "Red Security Door";
						}
						else if (num2 < 15)
						{
							text = "Yellow Security Door";
						}
						else if (num2 < 20)
						{
							text = "Green Security Door";
						}
						else if (num2 < 25)
						{
							text = "Blue Security Door";
						}
						else
						{
							text = "Purple Security Door";
						}
						cell.SetReachable(false);
						cell.AddObject(text, null, null, null, null);
					}
					else if (spireTemplateTile == SpireTemplateTile.InsideSealed)
					{
						if (!dictionary.ContainsKey(this.Template.Rooms[j, i]))
						{
							dictionary.Add(this.Template.Rooms[j, i], Stat.Random(1, 100));
						}
						int num3 = dictionary[this.Template.Rooms[j, i]];
						if (num3 < 5)
						{
							cell.SetReachable(false);
							if (!flag && 2.in100())
							{
								cell.AddObject("SparkingBaetyl", null, null, null, null);
								flag = true;
							}
							else if (25.in100())
							{
								int num4 = ("2d30".RollCached() + num * 5) / 20;
								if (num4 < 1)
								{
									num4 = 1;
								}
								if (num4 > 8)
								{
									num4 = 8;
								}
								cell.AddObject(GameObjectFactory.create("Chest" + num4.ToString()), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
							else if (45.in100())
							{
								cell.AddObject(GameObjectFactory.create(PopulationManager.RollOneFrom("Vault Robots", null, null).Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else if (num3 < 10)
						{
							cell.AddObject(PopulationManager.CreateOneFrom("Scrap 3", null, 0, 0, null, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						else if (num3 < 15)
						{
							cell.SetReachable(false);
							if (15.in100())
							{
								cell.AddObject("Locker", null, null, null, null);
							}
							else
							{
								cell.AddObject("Garbage", null, null, null, null);
							}
						}
						else if (num3 < 20)
						{
							cell.SetReachable(false);
							if (15.in100())
							{
								cell.AddObject("Garbage", null, null, null, null);
							}
							else
							{
								cell.AddObject(PopulationManager.CreateOneFrom("Scrap 1", null, 0, 0, null, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
					else if (spireTemplateTile == SpireTemplateTile.Door)
					{
						cell.SetReachable(true);
						cell.AddObject("Door", null, null, null, null);
					}
					else if (spireTemplateTile == SpireTemplateTile.StairsDown)
					{
						cell.SetReachable(true);
						if (Z.GetZoneZ() != 10)
						{
							if (Z.GetZoneZ() % 2 == 0)
							{
								cell.AddObject("StairsUp", null, null, null, null);
							}
							else
							{
								cell.AddObject("StairsDown", null, null, null, null);
							}
						}
					}
					else if (spireTemplateTile == SpireTemplateTile.StairsUp)
					{
						cell.SetReachable(true);
						if (Z.GetZoneZ() % 2 == 0)
						{
							cell.AddObject("StairsDown", null, null, null, null);
						}
						else
						{
							cell.AddObject("StairsUp", null, null, null, null);
						}
					}
					else if (spireTemplateTile == SpireTemplateTile.Outside)
					{
						if (bUnderground)
						{
							cell.AddObject("Sandstone", null, null, null, null);
							cell.SetReachable(false);
						}
						else
						{
							cell.SetReachable(true);
						}
					}
					else
					{
						list.Add(cell);
						cell.SetReachable(true);
					}
				}
			}
		}

		// Token: 0x040033A9 RID: 13225
		private SpireTemplate Template;

		// Token: 0x040033AA RID: 13226
		public int Width;

		// Token: 0x040033AB RID: 13227
		public int Height;

		// Token: 0x040033AC RID: 13228
		public int ZonesWide;

		// Token: 0x040033AD RID: 13229
		public int ZonesHigh;

		// Token: 0x040033AE RID: 13230
		public static int foo;
	}
}
