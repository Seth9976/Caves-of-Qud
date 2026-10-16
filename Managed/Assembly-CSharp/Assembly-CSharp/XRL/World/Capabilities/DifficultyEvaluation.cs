using System;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E3 RID: 2531
	public static class DifficultyEvaluation
	{
		// Token: 0x06006C02 RID: 27650 RVA: 0x0022DD30 File Offset: 0x0022BF30
		public static int? GetDifficultyRating(GameObject Subject, GameObject Actor = null, bool IgnoreHideCon = false)
		{
			if (!GameObject.Validate(ref Subject))
			{
				return new int?(-20);
			}
			if (!GameObject.Validate(ref Actor))
			{
				Actor = The.Player;
				if (!GameObject.Validate(ref Actor))
				{
					return null;
				}
			}
			if (!Subject.IsCombatObject())
			{
				return null;
			}
			if (!IgnoreHideCon && Subject.HasPropertyOrTag("HideCon"))
			{
				return null;
			}
			int num = Subject.Stat("Level", 0);
			string text = (Subject.IsPlayer() ? "Player" : Subject.GetPropertyOrTag("Role", null));
			int num2 = num + DifficultyEvaluation.GetRoleLevelOffset(text);
			int num3 = Actor.Stat("Level", 0);
			string text2 = (Actor.IsPlayer() ? "Player" : Actor.GetPropertyOrTag("Role", null));
			int num4 = num3 + DifficultyEvaluation.GetRoleLevelOffset(text2);
			return new int?(GetDifficultyEvaluationEvent.GetFor(Subject, Actor, text, text2, num, num3, num2, num4, num2 - num4, int.MinValue, int.MaxValue));
		}

		// Token: 0x06006C03 RID: 27651 RVA: 0x0022DE2C File Offset: 0x0022C02C
		public static int GetRoleLevelOffset(string Role)
		{
			if (Role == "Minion")
			{
				return -5;
			}
			if (Role == "Skirmisher")
			{
				return -3;
			}
			if (Role == "Leader")
			{
				return 3;
			}
			if (Role == "Summoner")
			{
				return 4;
			}
			if (!(Role == "Hero"))
			{
				return 0;
			}
			return 5;
		}

		// Token: 0x06006C04 RID: 27652 RVA: 0x0022DE8C File Offset: 0x0022C08C
		public static string GetDifficultyDescription(GameObject Subject, GameObject Actor = null, int? Rating = null, bool IgnoreHideCon = false)
		{
			if (Rating == null)
			{
				Rating = DifficultyEvaluation.GetDifficultyRating(Subject, Actor, IgnoreHideCon);
				if (Rating == null)
				{
					return null;
				}
			}
			int? num = Rating;
			int num2 = 15;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				return "{{R|Impossible}}";
			}
			num = Rating;
			num2 = 10;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				return "{{r|Very Tough}}";
			}
			num = Rating;
			num2 = 5;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				return "{{W|Tough}}";
			}
			num = Rating;
			num2 = -5;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				return "{{w|Average}}";
			}
			num = Rating;
			num2 = -10;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				return "{{g|Easy}}";
			}
			return "{{G|Trivial}}";
		}

		// Token: 0x06006C05 RID: 27653 RVA: 0x0022DF68 File Offset: 0x0022C168
		public static void GetDifficultyDescription(out string Description, out string Color, GameObject Subject, GameObject Actor = null, int? Rating = null, bool IgnoreHideCon = false)
		{
			Description = null;
			Color = null;
			if (Rating == null)
			{
				Rating = DifficultyEvaluation.GetDifficultyRating(Subject, Actor, IgnoreHideCon);
				if (Rating == null)
				{
					return;
				}
			}
			int? num = Rating;
			int num2 = 15;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				Description = "Impossible";
				Color = "R";
				return;
			}
			num = Rating;
			num2 = 10;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				Description = "Very Tough";
				Color = "r";
				return;
			}
			num = Rating;
			num2 = 5;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				Description = "Tough";
				Color = "W";
				return;
			}
			num = Rating;
			num2 = -5;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				Description = "Average";
				Color = "w";
				return;
			}
			num = Rating;
			num2 = -10;
			if ((num.GetValueOrDefault() >= num2) & (num != null))
			{
				Description = "Easy";
				Color = "g";
				return;
			}
			Description = "Trivial";
			Color = "G";
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x0022E084 File Offset: 0x0022C284
		public static int GetDifficultyFromDescription(string Description)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Description);
			if (num <= 2015605295U)
			{
				if (num <= 988166649U)
				{
					if (num <= 391091564U)
					{
						if (num != 89714922U)
						{
							if (num != 391091564U)
							{
								return int.MinValue;
							}
							if (!(Description == "Tough"))
							{
								return int.MinValue;
							}
							return 5;
						}
						else
						{
							if (!(Description == "&rVery Tough"))
							{
								return int.MinValue;
							}
							return 10;
						}
					}
					else if (num != 811154168U)
					{
						if (num != 988166649U)
						{
							return int.MinValue;
						}
						if (!(Description == "{{w|Average}}"))
						{
							return int.MinValue;
						}
						return -5;
					}
					else if (!(Description == "Impossible"))
					{
						return int.MinValue;
					}
				}
				else if (num <= 1256211995U)
				{
					if (num != 1208454600U)
					{
						if (num != 1256211995U)
						{
							return int.MinValue;
						}
						if (!(Description == "{{G|Trivial}}"))
						{
							return int.MinValue;
						}
						return int.MinValue;
					}
					else
					{
						if (!(Description == "Average"))
						{
							return int.MinValue;
						}
						return -5;
					}
				}
				else if (num != 1361261182U)
				{
					if (num != 1815286075U)
					{
						if (num != 2015605295U)
						{
							return int.MinValue;
						}
						if (!(Description == "&GTrivial"))
						{
							return int.MinValue;
						}
						return int.MinValue;
					}
					else
					{
						if (!(Description == "Easy"))
						{
							return int.MinValue;
						}
						return -10;
					}
				}
				else
				{
					if (!(Description == "Trivial"))
					{
						return int.MinValue;
					}
					return int.MinValue;
				}
			}
			else if (num <= 3113865348U)
			{
				if (num <= 2914556569U)
				{
					if (num != 2072791620U)
					{
						if (num != 2914556569U)
						{
							return int.MinValue;
						}
						if (!(Description == "&WTough"))
						{
							return int.MinValue;
						}
						return 5;
					}
					else
					{
						if (!(Description == "&gEasy"))
						{
							return int.MinValue;
						}
						return -10;
					}
				}
				else if (num != 2968186206U)
				{
					if (num != 3113865348U)
					{
						return int.MinValue;
					}
					if (!(Description == "{{g|Easy}}"))
					{
						return int.MinValue;
					}
					return -10;
				}
				else
				{
					if (!(Description == "Very Tough"))
					{
						return int.MinValue;
					}
					return 10;
				}
			}
			else if (num <= 3387372965U)
			{
				if (num != 3310199694U)
				{
					if (num != 3387372965U)
					{
						return int.MinValue;
					}
					if (!(Description == "{{W|Tough}}"))
					{
						return int.MinValue;
					}
					return 5;
				}
				else if (!(Description == "{{R|Impossible}}"))
				{
					return int.MinValue;
				}
			}
			else if (num != 3401726740U)
			{
				if (num != 3670173236U)
				{
					if (num != 4173341033U)
					{
						return int.MinValue;
					}
					if (!(Description == "&wAverage"))
					{
						return int.MinValue;
					}
					return -5;
				}
				else
				{
					if (!(Description == "{{r|Very Tough}}"))
					{
						return int.MinValue;
					}
					return 10;
				}
			}
			else if (!(Description == "&RImpossible"))
			{
				return int.MinValue;
			}
			return 15;
		}

		// Token: 0x04003156 RID: 12630
		public const int IMPOSSIBLE = 15;

		// Token: 0x04003157 RID: 12631
		public const int VERY_TOUGH = 10;

		// Token: 0x04003158 RID: 12632
		public const int TOUGH = 5;

		// Token: 0x04003159 RID: 12633
		public const int AVERAGE = -5;

		// Token: 0x0400315A RID: 12634
		public const int EASY = -10;

		// Token: 0x0400315B RID: 12635
		public const int TRIVIAL = -2147483648;
	}
}
