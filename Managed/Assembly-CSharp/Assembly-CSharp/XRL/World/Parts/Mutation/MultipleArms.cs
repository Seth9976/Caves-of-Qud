using System;
using XRL.Rules;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200144E RID: 5198
	[Serializable]
	public class MultipleArms : BaseMutation, IRankedMutation
	{
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600CB17 RID: 51991 RVA: 0x0049D520 File Offset: 0x0049B720
		public string AdditionsManagerID
		{
			get
			{
				return this.ParentObject.ID + "::MultipleArms::Add";
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600CB18 RID: 51992 RVA: 0x0049D537 File Offset: 0x0049B737
		public string ChangesManagerID
		{
			get
			{
				return this.ParentObject.ID + "::MultipleArms::Change";
			}
		}

		// Token: 0x0600CB1A RID: 51994 RVA: 0x0049D564 File Offset: 0x0049B764
		public override bool AffectsBodyParts()
		{
			return true;
		}

		// Token: 0x0600CB1B RID: 51995 RVA: 0x0049D567 File Offset: 0x0049B767
		public override string GetDescription()
		{
			return "You have an extra set of arms.";
		}

		// Token: 0x0600CB1C RID: 51996 RVA: 0x0049D570 File Offset: 0x0049B770
		public override string GetLevelText(int Level)
		{
			return "{{rules|" + this.GetAttackChance(Level).ToString() + "%}} chance for each extra arm to deliver an additional melee attack whenever you make a melee attack";
		}

		// Token: 0x0600CB1D RID: 51997 RVA: 0x0049D59B File Offset: 0x0049B79B
		public int GetAttackChance(int Level)
		{
			return 7 + Level * 3;
		}

		// Token: 0x0600CB1E RID: 51998 RVA: 0x0049D5A2 File Offset: 0x0049B7A2
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CB1F RID: 51999 RVA: 0x0049D5AB File Offset: 0x0049B7AB
		public int GetRank()
		{
			return this.Rank;
		}

		// Token: 0x0600CB20 RID: 52000 RVA: 0x0049D5B4 File Offset: 0x0049B7B4
		public int AdjustRank(int Amount)
		{
			this.Rank += Amount;
			for (int i = 0; i < Amount; i++)
			{
				this.AddMoreArms(this.ParentObject);
			}
			return this.Rank;
		}

		// Token: 0x0600CB21 RID: 52001 RVA: 0x0049D5F0 File Offset: 0x0049B7F0
		public void AddMoreArms(GameObject Subject)
		{
			Body body = ((Subject != null) ? Subject.Body : null);
			if (body == null)
			{
				return;
			}
			BodyPart body2 = body.GetBody();
			BodyPart firstAttachedPart = body2.GetFirstAttachedPart("Arm", 2, body, true);
			BodyPart bodyPart = ((firstAttachedPart == null) ? null : firstAttachedPart.GetFirstAttachedPart("Hand", 2, body, true));
			BodyPart firstAttachedPart2 = body2.GetFirstAttachedPart("Arm", 1, body, true);
			BodyPart bodyPart2 = ((firstAttachedPart2 == null) ? null : firstAttachedPart2.GetFirstAttachedPart("Hand", 1, body, true));
			BodyPart firstAttachedPart3 = body2.GetFirstAttachedPart("Hands", 0, body, true);
			BodyPart bodyPart3 = firstAttachedPart2;
			BodyPart bodyPart4 = firstAttachedPart3;
			bool flag = false;
			for (int i = 0; i < this.Pairs; i++)
			{
				string text = ((i == 0) ? "Multiple Arms Hands" : ("Multiple Arms Hands " + (i + 1).ToString()));
				if (i == 0 && firstAttachedPart != null && firstAttachedPart.Manager == null && bodyPart != null && bodyPart.Manager == null && firstAttachedPart2 != null && firstAttachedPart2.Manager == null && bodyPart2 != null && bodyPart2.Manager == null && firstAttachedPart3 != null && firstAttachedPart3.Manager == null && firstAttachedPart.IsLateralitySafeToChange(2, body, bodyPart) && firstAttachedPart2.IsLateralitySafeToChange(1, body, bodyPart2) && firstAttachedPart3.IsLateralitySafeToChange(0, body, null) && firstAttachedPart3.DependsOn == bodyPart.SupportsDependent && firstAttachedPart3.DependsOn == bodyPart2.SupportsDependent)
				{
					BodyPart bodyPart5 = body2;
					BodyPart bodyPart6 = bodyPart3;
					string text2 = "Arm";
					int num = 10;
					string text3 = null;
					string text4 = null;
					string text5 = null;
					string text6 = null;
					int? num2 = new int?(body2.Category);
					BodyPart bodyPart7 = (bodyPart3 = bodyPart5.AddPartAt(bodyPart6, text2, num, text3, text4, text5, text6, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true));
					string text7 = "Hand";
					int num3 = 10;
					string text8 = null;
					string text9 = text;
					string text10 = null;
					string text11 = null;
					num2 = new int?(body2.Category);
					bodyPart7.AddPart(text7, num3, text8, text9, text10, text11, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					BodyPart bodyPart8 = body2;
					BodyPart bodyPart9 = bodyPart3;
					string text12 = "Arm";
					int num4 = 9;
					string text13 = null;
					string text14 = null;
					string text15 = null;
					string text16 = null;
					num2 = new int?(body2.Category);
					BodyPart bodyPart10 = (bodyPart3 = bodyPart8.AddPartAt(bodyPart9, text12, num4, text13, text14, text15, text16, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true));
					string text17 = "Hand";
					int num5 = 9;
					string text18 = null;
					string text19 = text;
					string text20 = null;
					string text21 = null;
					num2 = new int?(body2.Category);
					bodyPart10.AddPart(text17, num5, text18, text19, text20, text21, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					BodyPart bodyPart11 = body2;
					BodyPart bodyPart12 = bodyPart4;
					string text22 = "Hands";
					int num6 = 8;
					string text23 = null;
					string text24 = null;
					string text25 = text;
					string text26 = null;
					num2 = new int?(body2.Category);
					bodyPart4 = bodyPart11.AddPartAt(bodyPart12, text22, num6, text23, text24, text25, text26, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					this.ProcessChangedLimb(firstAttachedPart.ChangeLaterality(firstAttachedPart.Laterality | 4, false));
					this.ProcessChangedLimb(bodyPart.ChangeLaterality(bodyPart.Laterality | 4, false));
					this.ProcessChangedLimb(firstAttachedPart2.ChangeLaterality(firstAttachedPart2.Laterality | 4, false));
					this.ProcessChangedLimb(bodyPart2.ChangeLaterality(bodyPart2.Laterality | 4, false));
					this.ProcessChangedLimb(firstAttachedPart3.ChangeLaterality(firstAttachedPart3.Laterality | 4, false));
					flag = true;
				}
				else
				{
					int num7 = 2;
					int num8 = 1;
					int num9 = 0;
					if (i == 1 && flag)
					{
						num7 |= 32;
						num8 |= 32;
						num9 |= 32;
					}
					int? num2;
					BodyPart bodyPart15;
					if (bodyPart3 != null)
					{
						BodyPart bodyPart13 = body2;
						BodyPart bodyPart14 = bodyPart3;
						string text27 = "Arm";
						int num10 = num7;
						string text28 = null;
						string text29 = null;
						string text30 = null;
						string text31 = null;
						num2 = new int?(body2.Category);
						bodyPart3 = (bodyPart15 = bodyPart13.AddPartAt(bodyPart14, text27, num10, text28, text29, text30, text31, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true));
					}
					else
					{
						BodyPart bodyPart16 = body2;
						string text32 = "Arm";
						int num11 = num7;
						string text33 = null;
						string text34 = null;
						string text35 = null;
						string text36 = null;
						num2 = new int?(body2.Category);
						string additionsManagerID = this.AdditionsManagerID;
						int? num12 = num2;
						string[] array = new string[] { "Hands", "Feet", "Roots", "Thrown Weapon" };
						bodyPart3 = (bodyPart15 = bodyPart16.AddPartAt(text32, num11, text33, text34, text35, text36, additionsManagerID, num12, null, null, null, null, null, null, null, null, null, null, null, null, "Arm", array, true));
					}
					BodyPart bodyPart17 = bodyPart15;
					string text37 = "Hand";
					int num13 = num7;
					string text38 = null;
					string text39 = text;
					string text40 = null;
					string text41 = null;
					num2 = new int?(body2.Category);
					bodyPart17.AddPart(text37, num13, text38, text39, text40, text41, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					BodyPart bodyPart18 = body2;
					BodyPart bodyPart19 = bodyPart3;
					string text42 = "Arm";
					int num14 = num7;
					string text43 = null;
					string text44 = null;
					string text45 = null;
					string text46 = null;
					num2 = new int?(body2.Category);
					BodyPart bodyPart20 = bodyPart18.AddPartAt(bodyPart19, text42, num14, text43, text44, text45, text46, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					string text47 = "Hand";
					int num15 = num7;
					string text48 = null;
					string text49 = text;
					string text50 = null;
					string text51 = null;
					num2 = new int?(body2.Category);
					bodyPart20.AddPart(text47, num15, text48, text49, text50, text51, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					if (bodyPart4 != null)
					{
						BodyPart bodyPart21 = body2;
						BodyPart bodyPart22 = bodyPart4;
						string text52 = "Hands";
						int num16 = num9;
						string text53 = null;
						string text54 = null;
						string text55 = text;
						string text56 = null;
						num2 = new int?(body2.Category);
						bodyPart4 = bodyPart21.AddPartAt(bodyPart22, text52, num16, text53, text54, text55, text56, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					}
					else
					{
						BodyPart bodyPart23 = body2;
						string text57 = "Hands";
						int num17 = num9;
						string text58 = null;
						string text59 = null;
						string text60 = text;
						string text61 = null;
						num2 = new int?(body2.Category);
						string additionsManagerID2 = this.AdditionsManagerID;
						int? num18 = num2;
						string[] array = new string[] { "Feet", "Roots", "Thrown Weapon" };
						bodyPart4 = bodyPart23.AddPartAt(text57, num17, text58, text59, text60, text61, additionsManagerID2, num18, null, null, null, null, null, null, null, null, null, null, null, null, "Hands", array, true);
					}
				}
			}
			Subject.WantToReequip();
		}

		// Token: 0x0600CB22 RID: 52002 RVA: 0x0049E02F File Offset: 0x0049C22F
		public override bool Mutate(GameObject GO, int Level)
		{
			this.AddMoreArms(GO);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB23 RID: 52003 RVA: 0x0049E040 File Offset: 0x0049C240
		private BodyPart ProcessChangedLimb(BodyPart Part)
		{
			if (Part != null)
			{
				Part.Manager = this.ChangesManagerID;
			}
			return Part;
		}

		// Token: 0x0600CB24 RID: 52004 RVA: 0x0049E054 File Offset: 0x0049C254
		public override bool Unmutate(GameObject GO)
		{
			GO.RemoveBodyPartsByManager(this.AdditionsManagerID, true);
			foreach (BodyPart bodyPart in GO.GetBodyPartsByManager(this.ChangesManagerID, true))
			{
				if (bodyPart.HasLaterality(4) && bodyPart.IsLateralityConsistent(null))
				{
					bodyPart.ChangeLaterality(bodyPart.Laterality & -5, false);
				}
			}
			GO.WantToReequip();
			return base.Unmutate(GO);
		}

		// Token: 0x0600CB25 RID: 52005 RVA: 0x0049E0E8 File Offset: 0x0049C2E8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CB26 RID: 52006 RVA: 0x0049E106 File Offset: 0x0049C306
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("chance", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB27 RID: 52007 RVA: 0x0049E130 File Offset: 0x0049C330
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Intrinsic && !E.Primary)
			{
				BodyPart bodyPart = E.BodyPart;
				if (((bodyPart != null) ? bodyPart.Manager : null) != null && E.BodyPart.Manager == this.AdditionsManagerID)
				{
					E.Chance += this.GetAttackChance(base.Level) - RuleSettings.BASE_SECONDARY_ATTACK_CHANCE;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004F08 RID: 20232
		public int Rank = 1;

		// Token: 0x04004F09 RID: 20233
		public int Pairs = 1;
	}
}
