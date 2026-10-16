using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Anatomy;

namespace XRL.World.Capabilities
{
	// Token: 0x020009ED RID: 2541
	public static class Laterality
	{
		// Token: 0x06006C71 RID: 27761 RVA: 0x00232D44 File Offset: 0x00230F44
		public static StringBuilder BuildLateralityAdjective(int Laterality, out bool Conjoin, bool Capitalized = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int num = Laterality;
			Conjoin = false;
			if ((Laterality & 512) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Inner" : "inner");
				Laterality &= -513;
			}
			else if ((Laterality & 1024) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Outer" : "outer");
				Laterality &= -1025;
			}
			if ((Laterality & 4) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Upper" : "upper");
				Laterality &= -5;
			}
			else if ((Laterality & 8) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Lower" : "lower");
				Laterality &= -9;
			}
			if ((Laterality & 2) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Right" : "right");
				Laterality &= -3;
			}
			else if ((Laterality & 1) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Left" : "left");
				Laterality &= -2;
			}
			if ((Laterality & 128) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Inside" : "inside");
				Laterality &= -129;
			}
			else if ((Laterality & 256) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Outside" : "outside");
				Laterality &= -257;
			}
			if ((Laterality & 32) != 0)
			{
				if ((Laterality & 16) != 0)
				{
					stringBuilder.Compound(Capitalized ? "Mid-Fore" : "mid-fore");
					Laterality &= -17;
				}
				else if ((Laterality & 64) != 0)
				{
					stringBuilder.Compound(Capitalized ? "Mid-Hind" : "mid-hind");
					Laterality &= -65;
				}
				else
				{
					stringBuilder.Compound(Capitalized ? "Mid" : "mid");
				}
				Laterality &= -33;
				Conjoin = true;
			}
			else if ((Laterality & 16) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Fore" : "fore");
				Laterality &= -17;
				Conjoin = true;
			}
			else if ((Laterality & 64) != 0)
			{
				stringBuilder.Compound(Capitalized ? "Hind" : "hind");
				Laterality &= -65;
				Conjoin = true;
			}
			if (Laterality != 0)
			{
				throw new Exception("invalid concrete laterality value " + num.ToString());
			}
			return stringBuilder;
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x00232F60 File Offset: 0x00231160
		public static string WithLateralityAdjective(string Base, int Laterality, out bool Conjoin, bool Capitalized = false)
		{
			StringBuilder stringBuilder = Laterality.BuildLateralityAdjective(Laterality, out Conjoin, Capitalized);
			if (stringBuilder.Length == 0)
			{
				return Base;
			}
			if (Base == null)
			{
				return stringBuilder.ToString();
			}
			if (Conjoin && (Base.Contains(" ") || Base.Contains("-")))
			{
				Conjoin = false;
			}
			if (Conjoin)
			{
				stringBuilder.Append(Base.ToLower());
			}
			else
			{
				stringBuilder.Compound(Base);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x00232FCC File Offset: 0x002311CC
		public static string WithLateralityAdjective(string Base, int Laterality, bool Capitalized = false)
		{
			bool flag;
			return Laterality.WithLateralityAdjective(Base, Laterality, out flag, Capitalized);
		}

		// Token: 0x06006C74 RID: 27764 RVA: 0x00232FE3 File Offset: 0x002311E3
		public static string LateralityAdjective(int Laterality, out bool Conjoin, bool Capitalized = false)
		{
			return Laterality.WithLateralityAdjective(null, Laterality, out Conjoin, Capitalized);
		}

		// Token: 0x06006C75 RID: 27765 RVA: 0x00232FEE File Offset: 0x002311EE
		public static string LateralityAdjective(int Laterality, bool Capitalized = false)
		{
			return Laterality.WithLateralityAdjective(null, Laterality, Capitalized);
		}

		// Token: 0x06006C76 RID: 27766 RVA: 0x00232FF8 File Offset: 0x002311F8
		public static string StripLateralityAdjective(string Text, int Laterality, bool Capitalized = false)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag;
			StringBuilder stringBuilder = Laterality.BuildLateralityAdjective(Laterality, out flag, Capitalized);
			if (stringBuilder.Length == 0)
			{
				return Text;
			}
			string text = stringBuilder.ToString();
			if (Text.StartsWith(text) && Text.Length > text.Length)
			{
				if (flag && (Text.LastIndexOf(' ') >= text.Length || Text.LastIndexOf('-') > text.Length))
				{
					flag = false;
				}
				if (flag)
				{
					if (Capitalized)
					{
						if (Text.Length > text.Length + 1)
						{
							Text = char.ToUpper(Text[text.Length]).ToString() + Text.Substring(text.Length + 1);
						}
						else
						{
							Text = char.ToUpper(Text[text.Length]).ToString() ?? "";
						}
					}
					else
					{
						Text = Text.Substring(text.Length);
					}
				}
				else if (Text.Length > text.Length + 1 && Text[text.Length] == ' ')
				{
					Text = Text.Substring(text.Length + 1);
				}
				else
				{
					Text = Text.Substring(text.Length);
				}
			}
			return Text;
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x00233124 File Offset: 0x00231324
		public static int GetCodeFromAdjective(ref string Text)
		{
			if (!Text.Contains(" "))
			{
				return 65535;
			}
			List<string> list = new List<string>(Text.Split(' ', StringSplitOptions.None));
			int num = 0;
			while (list.Count > 1)
			{
				string text = list[0];
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num2 <= 2124115655U)
				{
					if (num2 <= 779780628U)
					{
						if (num2 <= 306900080U)
						{
							if (num2 != 176974407U)
							{
								if (num2 != 306900080U)
								{
									break;
								}
								if (!(text == "left"))
								{
									break;
								}
								goto IL_034E;
							}
							else
							{
								if (!(text == "upper"))
								{
									break;
								}
								goto IL_0354;
							}
						}
						else if (num2 != 513712005U)
						{
							if (num2 != 680803461U)
							{
								if (num2 != 779780628U)
								{
									break;
								}
								if (!(text == "Outer"))
								{
									break;
								}
								goto IL_0393;
							}
							else
							{
								if (!(text == "Mid"))
								{
									break;
								}
								goto IL_0367;
							}
						}
						else if (!(text == "Right"))
						{
							break;
						}
					}
					else if (num2 <= 1099781210U)
					{
						if (num2 != 945339055U)
						{
							if (num2 != 1029526925U)
							{
								if (num2 != 1099781210U)
								{
									break;
								}
								if (!(text == "Lower"))
								{
									break;
								}
								goto IL_035A;
							}
							else
							{
								if (!(text == "Inside"))
								{
									break;
								}
								goto IL_0375;
							}
						}
						else
						{
							if (!(text == "fore"))
							{
								break;
							}
							goto IL_0360;
						}
					}
					else if (num2 != 1539842792U)
					{
						if (num2 != 2028154341U)
						{
							if (num2 != 2124115655U)
							{
								break;
							}
							if (!(text == "Inner"))
							{
								break;
							}
							goto IL_0389;
						}
						else if (!(text == "right"))
						{
							break;
						}
					}
					else
					{
						if (!(text == "hind"))
						{
							break;
						}
						goto IL_036E;
					}
					num |= 2;
				}
				else
				{
					if (num2 <= 3038577850U)
					{
						if (num2 <= 2608827236U)
						{
							if (num2 != 2457286800U)
							{
								if (num2 != 2608827236U)
								{
									break;
								}
								if (!(text == "Outside"))
								{
									break;
								}
							}
							else
							{
								if (!(text == "Left"))
								{
									break;
								}
								goto IL_034E;
							}
						}
						else if (num2 != 2718680436U)
						{
							if (num2 != 2999076071U)
							{
								if (num2 != 3038577850U)
								{
									break;
								}
								if (!(text == "lower"))
								{
									break;
								}
								goto IL_035A;
							}
							else
							{
								if (!(text == "Upper"))
								{
									break;
								}
								goto IL_0354;
							}
						}
						else
						{
							if (!(text == "outer"))
							{
								break;
							}
							goto IL_0393;
						}
					}
					else if (num2 <= 3280944101U)
					{
						if (num2 != 3095725775U)
						{
							if (num2 != 3280923844U)
							{
								if (num2 != 3280944101U)
								{
									break;
								}
								if (!(text == "mid"))
								{
									break;
								}
								goto IL_0367;
							}
							else if (!(text == "outside"))
							{
								break;
							}
						}
						else
						{
							if (!(text == "Fore"))
							{
								break;
							}
							goto IL_0360;
						}
					}
					else if (num2 != 3638454823U)
					{
						if (num2 != 3690229512U)
						{
							if (num2 != 4148812653U)
							{
								break;
							}
							if (!(text == "inside"))
							{
								break;
							}
							goto IL_0375;
						}
						else
						{
							if (!(text == "Hind"))
							{
								break;
							}
							goto IL_036E;
						}
					}
					else
					{
						if (!(text == "inner"))
						{
							break;
						}
						goto IL_0389;
					}
					num |= 256;
				}
				IL_039B:
				list.RemoveAt(0);
				continue;
				IL_034E:
				num |= 1;
				goto IL_039B;
				IL_0354:
				num |= 4;
				goto IL_039B;
				IL_035A:
				num |= 8;
				goto IL_039B;
				IL_0360:
				num |= 16;
				goto IL_039B;
				IL_0367:
				num |= 32;
				goto IL_039B;
				IL_036E:
				num |= 64;
				goto IL_039B;
				IL_0375:
				num |= 128;
				goto IL_039B;
				IL_0389:
				num |= 512;
				goto IL_039B;
				IL_0393:
				num |= 1024;
				goto IL_039B;
			}
			if (num == 0)
			{
				num = 65535;
			}
			Text = string.Join(" ", list.ToArray());
			return num;
		}

		// Token: 0x06006C78 RID: 27768 RVA: 0x002334FB File Offset: 0x002316FB
		public static int GetCodeFromAdjective(string Text)
		{
			return Laterality.GetCodeFromAdjective(ref Text);
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x00233504 File Offset: 0x00231704
		public static int GetCode(string text)
		{
			if (text.Contains(" "))
			{
				int num = 0;
				foreach (string text2 in text.Split(' ', StringSplitOptions.None))
				{
					num |= Laterality.GetCode(text2);
				}
				return num;
			}
			uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num2 <= 1099781210U)
			{
				if (num2 <= 779780628U)
				{
					if (num2 != 513712005U)
					{
						if (num2 != 680803461U)
						{
							if (num2 == 779780628U)
							{
								if (text == "Outer")
								{
									return 1024;
								}
							}
						}
						else if (text == "Mid")
						{
							return 32;
						}
					}
					else if (text == "Right")
					{
						return 2;
					}
				}
				else if (num2 != 810547195U)
				{
					if (num2 != 1029526925U)
					{
						if (num2 == 1099781210U)
						{
							if (text == "Lower")
							{
								return 8;
							}
						}
					}
					else if (text == "Inside")
					{
						return 128;
					}
				}
				else if (text == "None")
				{
					return 0;
				}
			}
			else if (num2 <= 2457286800U)
			{
				if (num2 != 2124115655U)
				{
					if (num2 != 2394299117U)
					{
						if (num2 == 2457286800U)
						{
							if (text == "Left")
							{
								return 1;
							}
						}
					}
					else if (text == "Any")
					{
						return 65535;
					}
				}
				else if (text == "Inner")
				{
					return 512;
				}
			}
			else if (num2 <= 2999076071U)
			{
				if (num2 != 2608827236U)
				{
					if (num2 == 2999076071U)
					{
						if (text == "Upper")
						{
							return 4;
						}
					}
				}
				else if (text == "Outside")
				{
					return 256;
				}
			}
			else if (num2 != 3095725775U)
			{
				if (num2 == 3690229512U)
				{
					if (text == "Hind")
					{
						return 64;
					}
				}
			}
			else if (text == "Fore")
			{
				return 16;
			}
			throw new Exception("invalid laterality '" + text + "'");
		}

		// Token: 0x06006C7A RID: 27770 RVA: 0x0023374C File Offset: 0x0023194C
		public static int GetAxisCode(string text)
		{
			if (text == "Lateral")
			{
				return 1;
			}
			if (text == "Vertical")
			{
				return 2;
			}
			if (text == "Longitudinal")
			{
				return 4;
			}
			if (text == "Superficial")
			{
				return 8;
			}
			if (!(text == "Stratal"))
			{
				throw new Exception("invalid laterality axis '" + text + "'");
			}
			return 16;
		}

		// Token: 0x06006C7B RID: 27771 RVA: 0x002337BC File Offset: 0x002319BC
		public static string GetAxisName(int code)
		{
			switch (code)
			{
			case 1:
				return "Lateral";
			case 2:
				return "Vertical";
			case 3:
				break;
			case 4:
				return "Longitudinal";
			default:
				if (code == 8)
				{
					return "Superficial";
				}
				if (code == 16)
				{
					return "Stratal";
				}
				break;
			}
			throw new Exception("invalid laterality code " + code.ToString());
		}

		// Token: 0x06006C7C RID: 27772 RVA: 0x00233820 File Offset: 0x00231A20
		public static int GetLateralityAxes(int code)
		{
			int num = 0;
			foreach (KeyValuePair<int, int[]> keyValuePair in Laterality.Axes)
			{
				if ((code & keyValuePair.Value[0]) != 0)
				{
					num |= keyValuePair.Key;
				}
			}
			return num;
		}

		// Token: 0x06006C7D RID: 27773 RVA: 0x00233888 File Offset: 0x00231A88
		public static bool Match(int Laterality, int RequiredLaterality)
		{
			if (RequiredLaterality == 0)
			{
				if (Laterality == 0)
				{
					return true;
				}
			}
			else if ((Laterality & RequiredLaterality) == RequiredLaterality)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06006C7E RID: 27774 RVA: 0x0023389B File Offset: 0x00231A9B
		public static bool Match(BodyPart Part, int RequiredLaterality)
		{
			return Laterality.Match(Part.Laterality, RequiredLaterality);
		}

		// Token: 0x040031A4 RID: 12708
		public const int NONE = 0;

		// Token: 0x040031A5 RID: 12709
		public const int LEFT = 1;

		// Token: 0x040031A6 RID: 12710
		public const int RIGHT = 2;

		// Token: 0x040031A7 RID: 12711
		public const int UPPER = 4;

		// Token: 0x040031A8 RID: 12712
		public const int LOWER = 8;

		// Token: 0x040031A9 RID: 12713
		public const int FORE = 16;

		// Token: 0x040031AA RID: 12714
		public const int MID = 32;

		// Token: 0x040031AB RID: 12715
		public const int HIND = 64;

		// Token: 0x040031AC RID: 12716
		public const int INSIDE = 128;

		// Token: 0x040031AD RID: 12717
		public const int OUTSIDE = 256;

		// Token: 0x040031AE RID: 12718
		public const int INNER = 512;

		// Token: 0x040031AF RID: 12719
		public const int OUTER = 1024;

		// Token: 0x040031B0 RID: 12720
		public const int ANY = 65535;

		// Token: 0x040031B1 RID: 12721
		public const int AXIS_LATERAL = 1;

		// Token: 0x040031B2 RID: 12722
		public const int AXIS_VERTICAL = 2;

		// Token: 0x040031B3 RID: 12723
		public const int AXIS_LONGITUDINAL = 4;

		// Token: 0x040031B4 RID: 12724
		public const int AXIS_SUPERFICIAL = 8;

		// Token: 0x040031B5 RID: 12725
		public const int AXIS_STRATAL = 16;

		// Token: 0x040031B6 RID: 12726
		public static Dictionary<int, int[]> Axes = new Dictionary<int, int[]>
		{
			{
				1,
				new int[] { 3, 1, 2 }
			},
			{
				2,
				new int[] { 12, 4, 8 }
			},
			{
				4,
				new int[] { 112, 16, 64, 32 }
			},
			{
				8,
				new int[] { 384, 128, 256 }
			},
			{
				16,
				new int[] { 1536, 512, 1024 }
			}
		};
	}
}
