using System;
using XRL.World.Parts;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BC3 RID: 3011
	public static class BodyPartCategory
	{
		// Token: 0x060078F9 RID: 30969 RVA: 0x002DACB0 File Offset: 0x002D8EB0
		public static string GetColor(int Category)
		{
			if (Category == 6)
			{
				return "C";
			}
			return null;
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x002DACC0 File Offset: 0x002D8EC0
		public static string GetName(int Category)
		{
			switch (Category)
			{
			case 1:
				return "Animal";
			case 2:
				return "Arthropod";
			case 3:
				return "Plant";
			case 4:
				return "Fungal";
			case 5:
				return "Protoplasmic";
			case 6:
				return "Cybernetic";
			case 7:
				return "Mechanical";
			case 8:
				return "Metal";
			case 9:
				return "Wooden";
			case 10:
				return "Stone";
			case 11:
				return "Glass";
			case 12:
				return "Leather";
			case 13:
				return "Bone";
			case 14:
				return "Chitin";
			case 15:
				return "Plastic";
			case 16:
				return "Cloth";
			case 17:
				return "Psionic";
			case 18:
				return "Extradimensional";
			case 19:
				return "Mollusk";
			case 20:
				return "Jelly";
			case 21:
				return "Crystal";
			case 22:
				return "Light";
			case 23:
				return "Liquid";
			default:
				throw new Exception("invalid category " + Category.ToString());
			}
		}

		// Token: 0x060078FB RID: 30971 RVA: 0x002DADD8 File Offset: 0x002D8FD8
		public static int GetCode(string Name, bool ThrowExceptions = true)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Name);
			if (num <= 1842662042U)
			{
				if (num <= 885622211U)
				{
					if (num <= 220313106U)
					{
						if (num != 112414825U)
						{
							if (num == 220313106U)
							{
								if (Name == "Mechanical")
								{
									return 7;
								}
							}
						}
						else if (Name == "Crystal")
						{
							return 21;
						}
					}
					else if (num != 687013446U)
					{
						if (num != 881552253U)
						{
							if (num == 885622211U)
							{
								if (Name == "Plastic")
								{
									return 15;
								}
							}
						}
						else if (Name == "Animal")
						{
							return 1;
						}
					}
					else if (Name == "Mollusk")
					{
						return 19;
					}
				}
				else if (num <= 1184875286U)
				{
					if (num != 1064871920U)
					{
						if (num != 1114770351U)
						{
							if (num == 1184875286U)
							{
								if (Name == "Psionic")
								{
									return 17;
								}
							}
						}
						else if (Name == "Bone")
						{
							return 13;
						}
					}
					else if (Name == "Chitin")
					{
						return 14;
					}
				}
				else if (num != 1224119515U)
				{
					if (num != 1340910203U)
					{
						if (num == 1842662042U)
						{
							if (Name == "Stone")
							{
								return 10;
							}
						}
					}
					else if (Name == "Wooden")
					{
						return 9;
					}
				}
				else if (Name == "Jelly")
				{
					return 20;
				}
			}
			else if (num <= 2840670588U)
			{
				if (num <= 2453963170U)
				{
					if (num != 2329082063U)
					{
						if (num != 2378524722U)
						{
							if (num == 2453963170U)
							{
								if (Name == "Extradimensional")
								{
									return 18;
								}
							}
						}
						else if (Name == "Plant")
						{
							return 3;
						}
					}
					else if (Name == "Light")
					{
						return 22;
					}
				}
				else if (num != 2545987019U)
				{
					if (num != 2757932989U)
					{
						if (num == 2840670588U)
						{
							if (Name == "Metal")
							{
								return 8;
							}
						}
					}
					else if (Name == "Liquid")
					{
						return 23;
					}
				}
				else if (Name == "Glass")
				{
					return 11;
				}
			}
			else if (num <= 3002236934U)
			{
				if (num != 2853127878U)
				{
					if (num != 2995012523U)
					{
						if (num == 3002236934U)
						{
							if (Name == "Fungal")
							{
								return 4;
							}
						}
					}
					else if (Name == "Cloth")
					{
						return 16;
					}
				}
				else if (Name == "Leather")
				{
					return 12;
				}
			}
			else if (num != 3106278608U)
			{
				if (num != 3740557541U)
				{
					if (num == 4093071784U)
					{
						if (Name == "Arthropod")
						{
							return 2;
						}
					}
				}
				else if (Name == "Cybernetic")
				{
					return 6;
				}
			}
			else if (Name == "Protoplasmic")
			{
				return 5;
			}
			if (ThrowExceptions)
			{
				throw new Exception("invalid category name '" + Name + "'");
			}
			return 0;
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x002DB159 File Offset: 0x002D9359
		public static int GetCodeIfExists(string Name)
		{
			return BodyPartCategory.GetCode(Name, false);
		}

		// Token: 0x060078FD RID: 30973 RVA: 0x002DB164 File Offset: 0x002D9364
		public static int BestGuessForCategoryDerivedFromGameObject(GameObject obj)
		{
			string propertyOrTag = obj.GetPropertyOrTag("DerivedBodyPartCategory", null);
			if (propertyOrTag != null)
			{
				return BodyPartCategory.GetCode(propertyOrTag, true);
			}
			if (obj.HasPart<ModExtradimensional>() || obj.HasPart<Extradimensional>())
			{
				return 18;
			}
			if (obj.HasTagOrProperty("LivePlant"))
			{
				return 3;
			}
			if (obj.HasTagOrProperty("LiveFungus"))
			{
				return 4;
			}
			if (obj.HasTagOrProperty("LiveAnimal"))
			{
				return 1;
			}
			if (obj.Render.DisplayName.Contains("skull"))
			{
				return 13;
			}
			if (obj.Render.DisplayName.Contains("shell"))
			{
				return 14;
			}
			if (obj.HasPart<EnergyCellSocket>())
			{
				return 7;
			}
			if (obj.HasPart<LiquidFueledPowerPlant>())
			{
				return 7;
			}
			if (obj.HasPart<Metal>())
			{
				return 8;
			}
			return 12;
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x002DB21D File Offset: 0x002D941D
		public static bool IsLiveCategory(int Category)
		{
			return Category - 1 <= 4 || Category - 18 <= 2;
		}

		// Token: 0x060078FF RID: 30975 RVA: 0x002DB230 File Offset: 0x002D9430
		public static int GetElectricalConductivity(int Category)
		{
			switch (Category)
			{
			case 1:
				return 80;
			case 2:
				return 70;
			case 3:
				return 90;
			case 4:
				return 60;
			case 5:
				return 90;
			case 6:
				return 70;
			case 7:
				return 90;
			case 8:
				return 100;
			case 9:
				return 20;
			case 10:
				return 10;
			case 11:
				return 0;
			case 12:
				return 0;
			case 13:
				return 0;
			case 14:
				return 10;
			case 15:
				return 0;
			case 16:
				return 0;
			case 17:
				return 30;
			case 18:
				return 80;
			case 19:
				return 80;
			case 20:
				return 90;
			case 21:
				return 10;
			case 22:
				return 0;
			case 23:
				return 100;
			default:
				throw new Exception("invalid category " + Category.ToString());
			}
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x002DB2F8 File Offset: 0x002D94F8
		public static bool ProcessList(string Spec, out int? Category, out int[] Categories, bool ThrowExceptions = true)
		{
			Category = null;
			Categories = null;
			if (!Spec.IsNullOrEmpty())
			{
				if (Spec == "Animate")
				{
					Categories = BodyPartCategory.ANIMATE;
				}
				else if (Spec == "Inanimate")
				{
					Categories = BodyPartCategory.INANIMATE;
				}
				else if (Spec == "Machine")
				{
					Categories = BodyPartCategory.MACHINE;
				}
				else if (Spec.Contains(","))
				{
					string[] array = Spec.Split(',', StringSplitOptions.None);
					int[] array2 = new int[array.Length];
					int i = 0;
					int num = array.Length;
					while (i < num)
					{
						int code = BodyPartCategory.GetCode(array[i], ThrowExceptions);
						if (code <= 0)
						{
							return false;
						}
						array2[i] = code;
						i++;
					}
					Categories = array2;
				}
				else
				{
					int code2 = BodyPartCategory.GetCode(Spec, ThrowExceptions);
					if (code2 <= 0)
					{
						return false;
					}
					Category = new int?(code2);
				}
			}
			return true;
		}

		// Token: 0x04003724 RID: 14116
		public const int ANIMAL = 1;

		// Token: 0x04003725 RID: 14117
		public const int ARTHROPOD = 2;

		// Token: 0x04003726 RID: 14118
		public const int PLANT = 3;

		// Token: 0x04003727 RID: 14119
		public const int FUNGAL = 4;

		// Token: 0x04003728 RID: 14120
		public const int PROTOPLASMIC = 5;

		// Token: 0x04003729 RID: 14121
		public const int CYBERNETIC = 6;

		// Token: 0x0400372A RID: 14122
		public const int MECHANICAL = 7;

		// Token: 0x0400372B RID: 14123
		public const int METAL = 8;

		// Token: 0x0400372C RID: 14124
		public const int WOODEN = 9;

		// Token: 0x0400372D RID: 14125
		public const int STONE = 10;

		// Token: 0x0400372E RID: 14126
		public const int GLASS = 11;

		// Token: 0x0400372F RID: 14127
		public const int LEATHER = 12;

		// Token: 0x04003730 RID: 14128
		public const int BONE = 13;

		// Token: 0x04003731 RID: 14129
		public const int CHITIN = 14;

		// Token: 0x04003732 RID: 14130
		public const int PLASTIC = 15;

		// Token: 0x04003733 RID: 14131
		public const int CLOTH = 16;

		// Token: 0x04003734 RID: 14132
		public const int PSIONIC = 17;

		// Token: 0x04003735 RID: 14133
		public const int EXTRADIMENSIONAL = 18;

		// Token: 0x04003736 RID: 14134
		public const int MOLLUSK = 19;

		// Token: 0x04003737 RID: 14135
		public const int JELLY = 20;

		// Token: 0x04003738 RID: 14136
		public const int CRYSTAL = 21;

		// Token: 0x04003739 RID: 14137
		public const int LIGHT = 22;

		// Token: 0x0400373A RID: 14138
		public const int LIQUID = 23;

		// Token: 0x0400373B RID: 14139
		public static readonly int[] ANIMATE = new int[] { 1, 2, 3, 4, 5, 19, 20 };

		// Token: 0x0400373C RID: 14140
		public static readonly int[] INANIMATE = new int[]
		{
			6, 7, 3, 4, 5, 8, 9, 10, 11, 12,
			13, 14, 15, 16, 17, 18, 21, 22, 23
		};

		// Token: 0x0400373D RID: 14141
		public static readonly int[] MACHINE = new int[] { 6, 7 };
	}
}
