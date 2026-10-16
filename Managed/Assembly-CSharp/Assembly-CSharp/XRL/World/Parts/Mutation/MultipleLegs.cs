using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200144F RID: 5199
	[Serializable]
	public class MultipleLegs : BaseMutation, IRankedMutation
	{
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600CB28 RID: 52008 RVA: 0x0049E19F File Offset: 0x0049C39F
		public string AdditionsManagerID
		{
			get
			{
				return this.ParentObject.ID + "::MultipleLegs::Add";
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600CB29 RID: 52009 RVA: 0x0049E1B6 File Offset: 0x0049C3B6
		public string ChangesManagerID
		{
			get
			{
				return this.ParentObject.ID + "::MultipleLegs::Change";
			}
		}

		// Token: 0x0600CB2B RID: 52011 RVA: 0x0049E1DC File Offset: 0x0049C3DC
		public override bool AffectsBodyParts()
		{
			return true;
		}

		// Token: 0x0600CB2C RID: 52012 RVA: 0x0049E1DF File Offset: 0x0049C3DF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CB2D RID: 52013 RVA: 0x0049E1E2 File Offset: 0x0049C3E2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetMaxCarriedWeightEvent.ID;
		}

		// Token: 0x0600CB2E RID: 52014 RVA: 0x0049E200 File Offset: 0x0049C400
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB2F RID: 52015 RVA: 0x0049E228 File Offset: 0x0049C428
		public override bool HandleEvent(GetMaxCarriedWeightEvent E)
		{
			E.AdjustWeight((1.0 + (double)this.GetCarryCapacityBonus(base.Level) / 100.0) * (double)this.Rank);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB30 RID: 52016 RVA: 0x0049E260 File Offset: 0x0049C460
		public override string GetDescription()
		{
			return "You have an extra set of legs.";
		}

		// Token: 0x0600CB31 RID: 52017 RVA: 0x0049E268 File Offset: 0x0049C468
		public override string GetLevelText(int Level)
		{
			return "+{{rules|" + this.GetMoveSpeedBonus(Level).ToString() + "}} move speed\n" + "+{{rules|" + this.GetCarryCapacityBonus(Level).ToString() + "%}} carry capacity";
		}

		// Token: 0x0600CB32 RID: 52018 RVA: 0x0049E2B1 File Offset: 0x0049C4B1
		public int GetMoveSpeedBonus(int Level)
		{
			return Level * 20;
		}

		// Token: 0x0600CB33 RID: 52019 RVA: 0x0049E2B7 File Offset: 0x0049C4B7
		public int GetCarryCapacityBonus(int Level)
		{
			return Level + 5;
		}

		// Token: 0x0600CB34 RID: 52020 RVA: 0x0049E2BC File Offset: 0x0049C4BC
		public int GetRank()
		{
			return this.Rank;
		}

		// Token: 0x0600CB35 RID: 52021 RVA: 0x0049E2C4 File Offset: 0x0049C4C4
		public int AdjustRank(int amount)
		{
			this.Rank += amount;
			return this.Rank;
		}

		// Token: 0x0600CB36 RID: 52022 RVA: 0x0049E2DA File Offset: 0x0049C4DA
		public override bool ChangeLevel(int NewLevel)
		{
			base.StatShifter.SetStatShift(this.ParentObject, "MoveSpeed", -this.GetMoveSpeedBonus(NewLevel), true);
			CarryingCapacityChangedEvent.Send(this.ParentObject);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CB37 RID: 52023 RVA: 0x0049E310 File Offset: 0x0049C510
		public void AddMoreLegs(GameObject Subject)
		{
			Body body = Subject.Body;
			if (body == null)
			{
				return;
			}
			BodyPart body2 = body.GetBody();
			BodyPart firstAttachedPart = body2.GetFirstAttachedPart("Feet", 0, body, true);
			if (firstAttachedPart != null)
			{
				if (firstAttachedPart.Manager == null && firstAttachedPart.IsLateralitySafeToChange(0, body, null))
				{
					BodyPart bodyPart = body2;
					BodyPart bodyPart2 = firstAttachedPart;
					string text = "Feet";
					int num = 64;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string text5 = null;
					int? num2 = new int?(body2.Category);
					bodyPart.AddPartAt(bodyPart2, text, num, text2, text3, text4, text5, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					firstAttachedPart.ChangeLaterality(16, false);
					firstAttachedPart.Manager = this.ChangesManagerID;
				}
				else
				{
					BodyPart bodyPart3 = body2;
					BodyPart bodyPart4 = firstAttachedPart;
					string text6 = "Feet";
					int num3 = 0;
					string text7 = null;
					string text8 = null;
					string text9 = null;
					string text10 = null;
					int? num2 = new int?(body2.Category);
					bodyPart3.AddPartAt(bodyPart4, text6, num3, text7, text8, text9, text10, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
				}
			}
			else
			{
				BodyPart bodyPart5 = body2;
				string text11 = "Feet";
				int num4 = 0;
				string text12 = null;
				string text13 = null;
				string text14 = null;
				string text15 = null;
				int? num2 = new int?(body2.Category);
				string additionsManagerID = this.AdditionsManagerID;
				int? num5 = num2;
				string[] array = new string[] { "Roots", "Tail", "Thrown Weapon" };
				bodyPart5.AddPartAt(text11, num4, text12, text13, text14, text15, additionsManagerID, num5, null, null, null, null, null, null, null, null, null, null, null, null, "Feet", array, true);
			}
			Subject.WantToReequip();
		}

		// Token: 0x0600CB38 RID: 52024 RVA: 0x0049E587 File Offset: 0x0049C787
		public override bool Mutate(GameObject GO, int Level)
		{
			this.AddMoreLegs(GO);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB39 RID: 52025 RVA: 0x0049E598 File Offset: 0x0049C798
		public override bool Unmutate(GameObject GO)
		{
			GO.RemoveBodyPartsByManager(this.AdditionsManagerID, true);
			foreach (BodyPart bodyPart in GO.GetBodyPartsByManager(this.ChangesManagerID, true))
			{
				if (bodyPart.Laterality == 16 && bodyPart.IsLateralityConsistent(null))
				{
					bodyPart.ChangeLaterality(bodyPart.Laterality & -17, false);
				}
			}
			base.StatShifter.RemoveStatShifts();
			CarryingCapacityChangedEvent.Send(this.ParentObject);
			GO.WantToReequip();
			return base.Unmutate(GO);
		}

		// Token: 0x04004F0A RID: 20234
		public int Rank = 1;

		// Token: 0x04004F0B RID: 20235
		public int Bonus;
	}
}
