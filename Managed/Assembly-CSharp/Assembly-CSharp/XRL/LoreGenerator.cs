using System;
using System.Collections.Generic;
using System.Text;
using XRL.Rules;
using XRL.World;

namespace XRL
{
	// Token: 0x020004B7 RID: 1207
	public static class LoreGenerator
	{
		// Token: 0x060034C2 RID: 13506 RVA: 0x000FE3DC File Offset: 0x000FC5DC
		public static string RuinOfHouseIsnerLore(int X, int Y)
		{
			KeyValuePair<string, int[]> keyValuePair = (The.Game.AlternateStart ? LoreGenerator.landmarks_altStart.GetRandomElement<KeyValuePair<string, int[]>>(null) : LoreGenerator.landmarks.GetRandomElement<KeyValuePair<string, int[]>>(null));
			string key = keyValuePair.Key;
			int num = X - keyValuePair.Value[0];
			int num2 = Y - keyValuePair.Value[1];
			string text = ((num < 0) ? "west" : "east");
			string text2 = ((num2 < 0) ? "north" : "south");
			num = Math.Abs(num);
			num2 = Math.Abs(num2);
			int num3;
			string text3;
			if (Stat.Random(0, 1) == 0)
			{
				num3 = num;
				text3 = text;
			}
			else
			{
				num3 = num2;
				text3 = text2;
			}
			int num4 = Stat.Random(0, 40);
			string text4;
			if (num4 < 10)
			{
				text4 = "of the ";
			}
			else if (num4 < 20)
			{
				text4 = "to the ";
			}
			else if (num4 < 30)
			{
				text4 = "with the ";
			}
			else
			{
				text4 = "in the ";
			}
			num4 = Stat.Random(0, 70);
			string text5;
			if (num4 < 10)
			{
				text5 = "{Ruin of House Isner, ";
			}
			else if (num4 < 20)
			{
				text5 = "{masterwork pistol Ruin of House Isner, ";
			}
			else if (num4 < 30)
			{
				text5 = "{chest containing the Ruin of House Isner, ";
			}
			else if (num4 < 40)
			{
				text5 = "{chest holding the Ruin of House Isner, ";
			}
			else if (num4 < 50)
			{
				text5 = "{famous revolver Ruin of House Isner, ";
			}
			else if (num4 < 60)
			{
				text5 = "{hiding place of the Ruin of House Isner, ";
			}
			else
			{
				text5 = "{lost masterwork pistol, ";
			}
			num4 = Stat.Random(0, 60);
			string text6;
			if (num4 < 10)
			{
				text6 = ((num3 == 0) ? string.Concat(new string[] { "said to be stored somewhere under one parasang ", text3, " of ", key, ".}" }) : string.Concat(new string[]
				{
					"said to be stored somewhere ",
					num3.ToString(),
					" parasangs ",
					text3,
					" of ",
					key,
					".}"
				}));
			}
			else if (num4 < 20)
			{
				text6 = ((num3 == 0) ? string.Concat(new string[] { "rumored to be stored somewhere under one parasangs ", text3, " of ", key, ".}" }) : string.Concat(new string[]
				{
					"rumored to be stored somewhere ",
					num3.ToString(),
					" parasangs ",
					text3,
					" of ",
					key,
					".}"
				}));
			}
			else if (num4 < 30)
			{
				text6 = ((num3 == 0) ? string.Concat(new string[] { "located somewhere under one parasang ", text3, " of ", key, ", as the story goes.}" }) : string.Concat(new string[]
				{
					"located somewhere ",
					num3.ToString(),
					" parasangs ",
					text3,
					" of ",
					key,
					", as the story goes.}"
				}));
			}
			else if (num4 < 40)
			{
				text6 = ((num3 == 0) ? string.Concat(new string[] { "located somewhere near ", key, " less than one parasang ", text3, ".}" }) : string.Concat(new string[]
				{
					"located somewhere near ",
					key,
					" some ",
					num3.ToString(),
					" parasangs ",
					text3,
					".}"
				}));
			}
			else if (num4 < 50)
			{
				text6 = ((num3 == 0) ? string.Concat(new string[] { "which I hid somewhere ", text3, " of ", key, ", less than one parasang.}" }) : string.Concat(new string[]
				{
					"which I hid somewhere ",
					text3,
					" of ",
					key,
					", about ",
					num3.ToString(),
					" parasangs.}"
				}));
			}
			else
			{
				text6 = ((num3 == 0) ? string.Concat(new string[] { "where I stored it somewhere under one parasang ", text3, " of ", key, ".}" }) : string.Concat(new string[]
				{
					"where I stored it somewhere ",
					num3.ToString(),
					" parasangs ",
					text3,
					" of ",
					key,
					".}"
				}));
			}
			return text4 + text5 + text6;
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000FE817 File Offset: 0x000FCA17
		public static string GenerateLandmarkDirectionsTo(Zone Z, bool bAltStarts = false)
		{
			return LoreGenerator.GenerateLandmarkDirectionsTo(Z.ZoneID, bAltStarts);
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000FE825 File Offset: 0x000FCA25
		public static string GenerateLandmarkDirectionsTo(string ZoneID)
		{
			return LoreGenerator.GenerateLandmarkDirectionsTo(ZoneID, The.Game.AlternateStart);
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000FE838 File Offset: 0x000FCA38
		public static string GenerateLandmarkDirectionsTo(string ZoneID, bool bAltStarts)
		{
			LoreGenerator.SB.Clear();
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			ZoneID.Parse(ZoneID, out num, out num2, out num3, out num4, out num5);
			string text = "";
			string text2 = "";
			int num6 = 0;
			int num7 = 0;
			num6 = 0;
			num7 = 0;
			int num8 = int.MaxValue;
			foreach (KeyValuePair<string, int[]> keyValuePair in (bAltStarts ? LoreGenerator.landmarks_altStart : LoreGenerator.landmarks))
			{
				if (Math.Abs(num - keyValuePair.Value[0]) + Math.Abs(num2 - keyValuePair.Value[1]) < num8)
				{
					int num9 = Math.Abs(num - keyValuePair.Value[0]);
					int num10 = Math.Abs(num2 - keyValuePair.Value[1]);
					num8 = num9 + num10;
					num6 = num - keyValuePair.Value[0];
					num7 = num2 - keyValuePair.Value[1];
					text = keyValuePair.Key;
					text2 = keyValuePair.Key;
				}
			}
			if (num6 == 0 && num7 == 0)
			{
				return "near " + text;
			}
			if (text == text2)
			{
				if (num6 < 0)
				{
					if (num6 == -1)
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasang west");
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasangs west");
					}
				}
				else if (num6 > 0)
				{
					if (num6 == 1)
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasang east");
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasangs east");
					}
				}
				if (num7 < 0)
				{
					if (LoreGenerator.SB.Length > 0)
					{
						LoreGenerator.SB.Append(" and ");
					}
					if (num7 == -1)
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasang north");
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasangs north");
					}
				}
				else if (num7 > 0)
				{
					if (LoreGenerator.SB.Length > 0)
					{
						LoreGenerator.SB.Append(" and ");
					}
					if (num7 == 1)
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasang south");
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasangs south");
					}
				}
				LoreGenerator.SB.Append(" of ").Append(text);
			}
			else
			{
				if (num6 < 0)
				{
					if (num6 == -1)
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasang west of ").Append(text);
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasangs west of ").Append(text);
					}
				}
				else if (num6 > 0)
				{
					if (num6 == 1)
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasang east of ").Append(text);
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num6)).Append(" parasangs east of ").Append(text);
					}
				}
				if (num7 < 0)
				{
					if (LoreGenerator.SB.Length > 0)
					{
						LoreGenerator.SB.Append(", ");
					}
					if (num7 == -1)
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasang north of ").Append(text2);
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasangs north of ").Append(text2);
					}
				}
				else if (num7 > 0)
				{
					if (LoreGenerator.SB.Length > 0)
					{
						LoreGenerator.SB.Append(", ");
					}
					if (num7 == 1)
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasang south of ").Append(text2);
					}
					else
					{
						LoreGenerator.SB.Append(Math.Abs(num7)).Append(" parasangs south of ").Append(text2);
					}
				}
			}
			if (num5 > 10)
			{
				LoreGenerator.SB.Append(", " + (num5 - 10).ToString() + " strata deep");
			}
			return LoreGenerator.SB.ToString();
		}

		// Token: 0x04001AB7 RID: 6839
		private static StringBuilder SB = new StringBuilder();

		// Token: 0x04001AB8 RID: 6840
		private static Dictionary<string, int[]> landmarks = new Dictionary<string, int[]>
		{
			{
				"Joppa",
				new int[] { 11, 22 }
			},
			{
				"Red Rock",
				new int[] { 11, 20 }
			},
			{
				"Grit Gate",
				new int[] { 22, 14 }
			},
			{
				"the Six Day Stilt",
				new int[] { 5, 2 }
			},
			{
				"Golgotha",
				new int[] { 23, 9 }
			},
			{
				"Bethesda Susa",
				new int[] { 25, 3 }
			},
			{
				"Kyakukya",
				new int[] { 27, 20 }
			},
			{
				"Omonporch",
				new int[] { 53, 4 }
			},
			{
				"the Yd Freehold",
				new int[] { 67, 17 }
			}
		};

		// Token: 0x04001AB9 RID: 6841
		private static Dictionary<string, int[]> landmarks_altStart = new Dictionary<string, int[]>
		{
			{
				"the ruins of Joppa",
				new int[] { 11, 22 }
			},
			{
				"Red Rock",
				new int[] { 11, 20 }
			},
			{
				"Grit Gate",
				new int[] { 22, 14 }
			},
			{
				"the Six Day Stilt",
				new int[] { 5, 2 }
			},
			{
				"Golgotha",
				new int[] { 23, 9 }
			},
			{
				"Bethesda Susa",
				new int[] { 25, 3 }
			},
			{
				"Kyakukya",
				new int[] { 27, 20 }
			},
			{
				"Omonporch",
				new int[] { 53, 4 }
			},
			{
				"the Yd Freehold",
				new int[] { 67, 17 }
			}
		};
	}
}
