using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.World;

namespace XRL.Rules
{
	// Token: 0x020005DC RID: 1500
	public static class Directions
	{
		// Token: 0x06004055 RID: 16469 RVA: 0x0013C932 File Offset: 0x0013AB32
		public static bool IsActualDirection(string dir)
		{
			return !string.IsNullOrEmpty(dir) && (Array.IndexOf<string>(Directions.DirectionList, dir) != -1 || (dir == "U" || dir == "D"));
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x0013C96B File Offset: 0x0013AB6B
		public static string GetRandomDirection()
		{
			return Directions.DirectionList.GetRandomElement<string>(null);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x0013C978 File Offset: 0x0013AB78
		public static string GetRandomCardinalDirection()
		{
			return Directions.CardinalDirectionList.GetRandomElement<string>(null);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x0013C988 File Offset: 0x0013AB88
		public static string GetUITextArrowForDirection(string D)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(D);
			if (num <= 1543670565U)
			{
				if (num <= 1012009637U)
				{
					if (num <= 722245873U)
					{
						if (num != 702965234U)
						{
							if (num == 722245873U)
							{
								if (D == ".")
								{
									return "\a";
								}
							}
						}
						else if (D == "NW")
						{
							return "↖";
						}
					}
					else if (num != 937851900U)
					{
						if (num == 1012009637U)
						{
							if (D == "se")
							{
								return "↘";
							}
						}
					}
					else if (D == "NE")
					{
						return "↗";
					}
				}
				else if (num <= 1246896303U)
				{
					if (num != 1245071922U)
					{
						if (num == 1246896303U)
						{
							if (D == "sw")
							{
								return "↙";
							}
						}
					}
					else if (D == "nw")
					{
						return "↖";
					}
				}
				else if (num != 1479958588U)
				{
					if (num == 1543670565U)
					{
						if (D == "SE")
						{
							return "↘";
						}
					}
				}
				else if (D == "ne")
				{
					return "↗";
				}
			}
			else if (num <= 3524005078U)
			{
				if (num <= 3222007936U)
				{
					if (num != 1778557231U)
					{
						if (num == 3222007936U)
						{
							if (D == "E")
							{
								return "→";
							}
						}
					}
					else if (D == "SW")
					{
						return "↙";
					}
				}
				else if (num != 3406561745U)
				{
					if (num == 3524005078U)
					{
						if (D == "W")
						{
							return "←";
						}
					}
				}
				else if (D == "N")
				{
					return "↑";
				}
			}
			else if (num <= 3758891744U)
			{
				if (num != 3591115554U)
				{
					if (num == 3758891744U)
					{
						if (D == "e")
						{
							return "→";
						}
					}
				}
				else if (D == "S")
				{
					return "↓";
				}
			}
			else if (num != 3943445553U)
			{
				if (num != 4060888886U)
				{
					if (num == 4127999362U)
					{
						if (D == "s")
						{
							return "↓";
						}
					}
				}
				else if (D == "w")
				{
					return "←";
				}
			}
			else if (D == "n")
			{
				return "↑";
			}
			return "?";
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x0013CCA8 File Offset: 0x0013AEA8
		public static string GetArrowForDirection(string D)
		{
			if (D == "N")
			{
				return "\u0018";
			}
			if (D == "S")
			{
				return "\u0019";
			}
			if (D == "E")
			{
				return "\u001a";
			}
			if (D == "W")
			{
				return "\u001b";
			}
			if (D == "n")
			{
				return "\u0018";
			}
			if (D == "s")
			{
				return "\u0019";
			}
			if (D == "e")
			{
				return "\u001a";
			}
			if (D == "w")
			{
				return "\u001b";
			}
			if (D == "U")
			{
				return "<";
			}
			if (D == "u")
			{
				return "<";
			}
			if (D == "<")
			{
				return "<";
			}
			if (D == "D")
			{
				return ">";
			}
			if (D == "d")
			{
				return ">";
			}
			if (D == ">")
			{
				return ">";
			}
			return "?";
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x0013CDC4 File Offset: 0x0013AFC4
		public static string GetExpandedDirection(string D)
		{
			if (D == "N")
			{
				return "north";
			}
			if (D == "S")
			{
				return "south";
			}
			if (D == "E")
			{
				return "east";
			}
			if (D == "W")
			{
				return "west";
			}
			if (D == "NW")
			{
				return "northwest";
			}
			if (D == "NE")
			{
				return "northeast";
			}
			if (D == "SW")
			{
				return "southwest";
			}
			if (D == "SE")
			{
				return "southeast";
			}
			if (D == "n")
			{
				return "north";
			}
			if (D == "s")
			{
				return "south";
			}
			if (D == "e")
			{
				return "east";
			}
			if (D == "w")
			{
				return "west";
			}
			if (D == "nw")
			{
				return "northwest";
			}
			if (D == "ne")
			{
				return "northeast";
			}
			if (D == "sw")
			{
				return "southwest";
			}
			if (D == "se")
			{
				return "southeast";
			}
			if (D == "U")
			{
				return "up";
			}
			if (D == "u")
			{
				return "up";
			}
			if (D == "<")
			{
				return "up";
			}
			if (D == "D")
			{
				return "down";
			}
			if (D == "d")
			{
				return "down";
			}
			if (D == ">")
			{
				return "down";
			}
			if (D == ".")
			{
				return "here";
			}
			if (D == "?")
			{
				return "somewhere";
			}
			return D;
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x0013CF9C File Offset: 0x0013B19C
		public static string GetIndicativeDirection(string D)
		{
			if (D == "N")
			{
				return "northward";
			}
			if (D == "S")
			{
				return "southward";
			}
			if (D == "E")
			{
				return "eastward";
			}
			if (D == "W")
			{
				return "westward";
			}
			if (D == "NW")
			{
				return "northwestward";
			}
			if (D == "NE")
			{
				return "northeastward";
			}
			if (D == "SW")
			{
				return "southwestward";
			}
			if (D == "SE")
			{
				return "southeastward";
			}
			if (D == "n")
			{
				return "northward";
			}
			if (D == "s")
			{
				return "southwarwd";
			}
			if (D == "e")
			{
				return "eastward";
			}
			if (D == "w")
			{
				return "westward";
			}
			if (D == "nw")
			{
				return "northwestward";
			}
			if (D == "ne")
			{
				return "northeastward";
			}
			if (D == "sw")
			{
				return "southwestward";
			}
			if (D == "se")
			{
				return "southeastward";
			}
			if (D == "U")
			{
				return "upward";
			}
			if (D == "u")
			{
				return "upward";
			}
			if (D == "<")
			{
				return "upward";
			}
			if (D == "D")
			{
				return "downward";
			}
			if (D == "d")
			{
				return "downward";
			}
			if (D == ">")
			{
				return "downward";
			}
			if (D == ".")
			{
				return "here";
			}
			if (D == "?")
			{
				return "somewhere";
			}
			return D;
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x0013D174 File Offset: 0x0013B374
		public static string GetDirectionDescription(string Direction)
		{
			if (Direction == "D" || Direction == "d" || Direction == ">")
			{
				return "below";
			}
			if (Direction == "U" || Direction == "u" || Direction == "<")
			{
				return "above";
			}
			if (Direction == ".")
			{
				return "here";
			}
			if (Direction == "?")
			{
				return "somewhere";
			}
			return "to the " + Directions.GetExpandedDirection(Direction);
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x0013D214 File Offset: 0x0013B414
		public static string GetDirectionDescription(XRL.World.GameObject Actor, string Direction)
		{
			if (Actor == null || Actor.IsPlayer())
			{
				return Directions.GetDirectionDescription(Direction);
			}
			if (Direction == "D" || Direction == "d" || Direction == ">")
			{
				return "below " + Actor.them;
			}
			if (Direction == "U" || Direction == "u" || Direction == "<")
			{
				return "above " + Actor.them;
			}
			if (Direction == ".")
			{
				return "near " + Actor.them;
			}
			return "to " + Actor.its + " " + Directions.GetExpandedDirection(Direction);
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x0013D2DC File Offset: 0x0013B4DC
		public static string GetIncomingDirectionDescription(string Direction)
		{
			if (Direction == "D" || Direction == "d" || Direction == ">")
			{
				return "from below";
			}
			if (Direction == "U" || Direction == "u" || Direction == "<")
			{
				return "from above";
			}
			if (Direction == ".")
			{
				return "from nearby";
			}
			if (Direction == "?")
			{
				return "from somewhere";
			}
			return "from the " + Directions.GetExpandedDirection(Direction);
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x0013D37C File Offset: 0x0013B57C
		public static string GetIncomingDirectionDescription(XRL.World.GameObject Actor, string Direction)
		{
			if (Actor == null || Actor.IsPlayer())
			{
				return Directions.GetIncomingDirectionDescription(Direction);
			}
			if (Direction == "D" || Direction == "d" || Direction == ">")
			{
				return "from below " + Actor.them;
			}
			if (Direction == "U" || Direction == "u" || Direction == "<")
			{
				return "from above " + Actor.them;
			}
			if (Direction == ".")
			{
				return "from near " + Actor.them;
			}
			return "from " + Actor.its + " " + Directions.GetExpandedDirection(Direction);
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x0013D444 File Offset: 0x0013B644
		public static string GetDirectionShortDescription(string Direction)
		{
			if (Direction == "D" || Direction == "d" || Direction == ">")
			{
				return "D";
			}
			if (Direction == "U" || Direction == "u" || Direction == "<")
			{
				return "U";
			}
			if (Direction == ".")
			{
				return "here";
			}
			if (Direction == "?")
			{
				return "somewhere";
			}
			return Direction.ToUpper();
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x0013D4D8 File Offset: 0x0013B6D8
		public static string GetOppositeDirection(string Direction)
		{
			if (Direction == "N")
			{
				return "S";
			}
			if (Direction == "S")
			{
				return "N";
			}
			if (Direction == "E")
			{
				return "W";
			}
			if (Direction == "W")
			{
				return "E";
			}
			if (Direction == "NW")
			{
				return "SE";
			}
			if (Direction == "NE")
			{
				return "SW";
			}
			if (Direction == "SW")
			{
				return "NE";
			}
			if (Direction == "SE")
			{
				return "NW";
			}
			if (Direction == "n")
			{
				return "s";
			}
			if (Direction == "s")
			{
				return "n";
			}
			if (Direction == "e")
			{
				return "w";
			}
			if (Direction == "w")
			{
				return "e";
			}
			if (Direction == "nw")
			{
				return "se";
			}
			if (Direction == "ne")
			{
				return "sw";
			}
			if (Direction == "sw")
			{
				return "ne";
			}
			if (Direction == "se")
			{
				return "nw";
			}
			if (Direction == "D")
			{
				return "U";
			}
			if (Direction == "d")
			{
				return "u";
			}
			if (Direction == ">")
			{
				return "<";
			}
			if (Direction == "U")
			{
				return "D";
			}
			if (Direction == "u")
			{
				return "d";
			}
			if (Direction == "<")
			{
				return ">";
			}
			if (Direction == "north")
			{
				return "south";
			}
			if (Direction == "south")
			{
				return "north";
			}
			if (Direction == "east")
			{
				return "west";
			}
			if (Direction == "west")
			{
				return "east";
			}
			if (Direction == "north-east")
			{
				return "south-west";
			}
			if (Direction == "south-east")
			{
				return "north-west";
			}
			if (Direction == "north-west")
			{
				return "south-east";
			}
			if (Direction == "south-east")
			{
				return "south-west";
			}
			if (Direction == "northeast")
			{
				return "southwest";
			}
			if (Direction == "southeast")
			{
				return "northwest";
			}
			if (Direction == "northwest")
			{
				return "southeast";
			}
			if (Direction == "southeast")
			{
				return "southwest";
			}
			if (Direction == "down")
			{
				return "up";
			}
			if (Direction == "up")
			{
				return "down";
			}
			return ".";
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x0013D798 File Offset: 0x0013B998
		public static void ApplyDirection(string dir, ref int x, ref int y, int d = 1)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(dir);
			if (num > 1778557231U)
			{
				if (num <= 3406561745U)
				{
					if (num != 3222007936U)
					{
						if (num != 3238785555U)
						{
							if (num != 3406561745U)
							{
								goto IL_0158;
							}
							if (!(dir == "N"))
							{
								goto IL_0158;
							}
							y -= d;
							return;
						}
						else if (!(dir == "D"))
						{
							goto IL_0158;
						}
					}
					else
					{
						if (!(dir == "E"))
						{
							goto IL_0158;
						}
						x += d;
						return;
					}
				}
				else if (num != 3490449840U)
				{
					if (num != 3524005078U)
					{
						if (num != 3591115554U)
						{
							goto IL_0158;
						}
						if (!(dir == "S"))
						{
							goto IL_0158;
						}
						y += d;
						return;
					}
					else
					{
						if (!(dir == "W"))
						{
							goto IL_0158;
						}
						x -= d;
						return;
					}
				}
				else if (!(dir == "U"))
				{
					goto IL_0158;
				}
				Debug.LogWarning("handling contextually unsupported direction " + dir);
				return;
			}
			if (num <= 722245873U)
			{
				if (num != 702965234U)
				{
					if (num == 722245873U)
					{
						if (dir == ".")
						{
							return;
						}
					}
				}
				else if (dir == "NW")
				{
					x -= d;
					y -= d;
					return;
				}
			}
			else if (num != 937851900U)
			{
				if (num != 1543670565U)
				{
					if (num == 1778557231U)
					{
						if (dir == "SW")
						{
							x -= d;
							y += d;
							return;
						}
					}
				}
				else if (dir == "SE")
				{
					x += d;
					y -= d;
					return;
				}
			}
			else if (dir == "NE")
			{
				x += d;
				y -= d;
				return;
			}
			IL_0158:
			if (dir == null)
			{
				Debug.LogWarning("handling null direction");
				return;
			}
			Debug.LogWarning("handling unrecognized direction " + dir);
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x0013D980 File Offset: 0x0013BB80
		public static void ApplyDirection(string dir, ref int x, ref int y, ref int z, int d = 1)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(dir);
			if (num <= 1543670565U)
			{
				if (num <= 937851900U)
				{
					if (num != 702965234U)
					{
						if (num != 722245873U)
						{
							if (num == 937851900U)
							{
								if (dir == "NE")
								{
									x += d;
									y -= d;
									return;
								}
							}
						}
						else if (dir == ".")
						{
							return;
						}
					}
					else if (dir == "NW")
					{
						x -= d;
						y -= d;
						return;
					}
				}
				else if (num != 957132539U)
				{
					if (num != 990687777U)
					{
						if (num == 1543670565U)
						{
							if (dir == "SE")
							{
								x += d;
								y += d;
								return;
							}
						}
					}
					else if (dir == ">")
					{
						goto IL_0223;
					}
				}
				else if (dir == "<")
				{
					goto IL_021B;
				}
			}
			else if (num <= 3238785555U)
			{
				if (num != 1778557231U)
				{
					if (num != 3222007936U)
					{
						if (num == 3238785555U)
						{
							if (dir == "D")
							{
								goto IL_0223;
							}
						}
					}
					else if (dir == "E")
					{
						x += d;
						return;
					}
				}
				else if (dir == "SW")
				{
					x -= d;
					y += d;
					return;
				}
			}
			else if (num <= 3490449840U)
			{
				if (num != 3406561745U)
				{
					if (num == 3490449840U)
					{
						if (dir == "U")
						{
							goto IL_021B;
						}
					}
				}
				else if (dir == "N")
				{
					y -= d;
					return;
				}
			}
			else if (num != 3524005078U)
			{
				if (num == 3591115554U)
				{
					if (dir == "S")
					{
						y += d;
						return;
					}
				}
			}
			else if (dir == "W")
			{
				x -= d;
				return;
			}
			if (dir != null)
			{
				Debug.LogWarning("handling unrecognized direction " + dir);
				return;
			}
			Debug.LogWarning("handling null direction");
			return;
			IL_021B:
			z -= d;
			return;
			IL_0223:
			z += d;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x0013DBD4 File Offset: 0x0013BDD4
		public static void ApplyDirectionGlobal(string dir, ref int x, ref int y, ref int z, ref int wx, ref int wy, int d = 1)
		{
			Directions.ApplyDirection(dir, ref x, ref y, ref z, d);
			while (x < 0)
			{
				x += 3;
				wx--;
			}
			while (x > 2)
			{
				x -= 3;
				wx++;
			}
			while (y < 0)
			{
				y += 3;
				wy--;
			}
			while (y > 2)
			{
				y -= 3;
				wy++;
			}
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x0013DC40 File Offset: 0x0013BE40
		public static string CombineDirections(string A, string B, int AD = 1, int BD = 1)
		{
			int num = 0;
			int num2 = 0;
			Directions.ApplyDirection(A, ref num, ref num2, AD);
			Directions.ApplyDirection(B, ref num, ref num2, BD);
			if (num > 0)
			{
				if (num2 > 0)
				{
					return "SE";
				}
				if (num2 < 0)
				{
					return "NE";
				}
				return "E";
			}
			else if (num < 0)
			{
				if (num2 > 0)
				{
					return "SW";
				}
				if (num2 < 0)
				{
					return "NW";
				}
				return "W";
			}
			else
			{
				if (num2 > 0)
				{
					return "S";
				}
				if (num2 < 0)
				{
					return "N";
				}
				return ".";
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x0013DCBC File Offset: 0x0013BEBC
		public static string[] GetOrthogonalDirections(string Middle)
		{
			if (!Directions.DirectionsOrthogonal.ContainsKey(Middle))
			{
				return Directions.DirectionsOrthogonal["."];
			}
			return Directions.DirectionsOrthogonal[Middle];
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x0013DCE6 File Offset: 0x0013BEE6
		public static string[] GetAdjacentDirections(string Dir)
		{
			if (!Directions.DirectionsAdjacent.ContainsKey(Dir))
			{
				return null;
			}
			return Directions.DirectionsAdjacent[Dir];
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x0013DD04 File Offset: 0x0013BF04
		public static List<string> GetAdjacentDirections(string Middle, int Range)
		{
			int num = 0;
			int i = 0;
			int num2 = Directions.DirectionList.Length;
			while (i < num2)
			{
				if (Directions.DirectionList[i].EqualsNoCase(Middle))
				{
					num = i;
					break;
				}
				i++;
			}
			List<string> list = new List<string>();
			for (int j = num - Range; j <= num + Range; j++)
			{
				int num3 = j;
				if (num3 < 0)
				{
					num3 += Directions.DirectionList.Length;
				}
				if (num3 >= Directions.DirectionList.Length)
				{
					num3 -= Directions.DirectionList.Length;
				}
				list.Add(Directions.DirectionList[num3]);
			}
			return list;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x0013DD8F File Offset: 0x0013BF8F
		public static RingSpanEnumerator<string> IterateAdjacentDirections(string Start, bool IncludeSelf = false, bool Invert = false)
		{
			return Directions.DirectionList.IterateRing(Start, IncludeSelf, Invert);
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x0013DDA0 File Offset: 0x0013BFA0
		public static List<string> DirectionsFromAngle(int degrees, int steps)
		{
			float num = (float)degrees / 58f;
			float num2 = (float)Math.Sin((double)num);
			float num3 = (float)Math.Cos((double)num);
			List<string> list = new List<string>(steps);
			float num4 = 0f;
			float num5 = 0f;
			int num6 = 0;
			int num7 = 0;
			int i = 0;
			int num8 = steps * 100;
			while (i < num8)
			{
				num4 += num2;
				num5 += num3;
				int num9 = (int)Math.Round((double)num4, MidpointRounding.AwayFromZero);
				int num10 = (int)Math.Round((double)num5, MidpointRounding.AwayFromZero);
				if (num9 != num6 || num10 != num7)
				{
					string text = "";
					if (num10 > num7)
					{
						text += "N";
					}
					else if (num10 < num7)
					{
						text += "S";
					}
					if (num9 > num6)
					{
						text += "E";
					}
					else if (num9 < num6)
					{
						text += "W";
					}
					list.Add(text);
					if (list.Count >= steps)
					{
						break;
					}
					num6 = num9;
					num7 = num10;
				}
				i++;
			}
			return list;
		}

		// Token: 0x04001F93 RID: 8083
		public static string[] DirectionList = new string[] { "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x04001F94 RID: 8084
		public static string[] CardinalDirectionList = new string[] { "N", "E", "S", "W" };

		// Token: 0x04001F95 RID: 8085
		public static Dictionary<string, string[]> DirectionsAdjacent = new Dictionary<string, string[]>(9)
		{
			{
				"NW",
				new string[] { "W", "N" }
			},
			{
				"N",
				new string[] { "NW", "NE" }
			},
			{
				"NE",
				new string[] { "N", "E" }
			},
			{
				"E",
				new string[] { "NE", "SE" }
			},
			{
				"SE",
				new string[] { "E", "S" }
			},
			{
				"S",
				new string[] { "SE", "SW" }
			},
			{
				"SW",
				new string[] { "S", "W" }
			},
			{
				"W",
				new string[] { "SW", "NW" }
			},
			{
				".",
				Directions.DirectionList
			}
		};

		// Token: 0x04001F96 RID: 8086
		public static Dictionary<string, string[]> DirectionsOrthogonal = new Dictionary<string, string[]>(9)
		{
			{
				"NW",
				new string[] { "W", "E" }
			},
			{
				"N",
				new string[] { "W", "E" }
			},
			{
				"NE",
				new string[] { "W", "S" }
			},
			{
				"E",
				new string[] { "N", "S" }
			},
			{
				"SE",
				new string[] { "N", "W" }
			},
			{
				"S",
				new string[] { "N", "W" }
			},
			{
				"SW",
				new string[] { "N", "E" }
			},
			{
				"W",
				new string[] { "N", "S" }
			},
			{
				".",
				new string[] { "W", "E" }
			}
		};
	}
}
