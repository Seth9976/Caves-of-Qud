using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001458 RID: 5208
	[Serializable]
	public class TwoHeaded : BaseMutation
	{
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600CBD9 RID: 52185 RVA: 0x004A1C84 File Offset: 0x0049FE84
		public string AdditionsManagerID
		{
			get
			{
				return this.ParentObject.ID + "::TwoHeaded::Add";
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600CBDA RID: 52186 RVA: 0x004A1C9B File Offset: 0x0049FE9B
		public string ChangesManagerID
		{
			get
			{
				return this.ParentObject.ID + "::TwoHeaded::Change";
			}
		}

		// Token: 0x0600CBDC RID: 52188 RVA: 0x004A1CBA File Offset: 0x0049FEBA
		public override bool AffectsBodyParts()
		{
			return true;
		}

		// Token: 0x0600CBDD RID: 52189 RVA: 0x004A1CBD File Offset: 0x0049FEBD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GetEnergyCostEvent>.ID;
		}

		// Token: 0x0600CBDE RID: 52190 RVA: 0x004A1CE3 File Offset: 0x0049FEE3
		public override bool HandleEvent(GetEnergyCostEvent E)
		{
			if (E.Type != null && E.Type.Contains("Mental"))
			{
				E.PercentageReduction += this.GetReducedMentalActionCost(base.Level);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBDF RID: 52191 RVA: 0x004A1D20 File Offset: 0x0049FF20
		public void PrintMessage(BodyPart Head, Effect Effect, bool Definite = true)
		{
			if (!this.ParentObject.IsPlayerControlled() || !this.ParentObject.IsVisible())
			{
				return;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Compound(this.ParentObject.Poss(Head.GetOrdinalName(), true)).Compound(Head.Plural ? "help " : "helps ").Append(this.ParentObject.them)
				.Append(" shake off ");
			string stateDescription = Effect.GetStateDescription();
			if (stateDescription.IsNullOrEmpty())
			{
				stringBuilder.Append(Definite ? "the effect!" : "a mental state!");
			}
			else
			{
				stringBuilder.Append("being ").Append(stateDescription).Append('!');
			}
			IComponent<GameObject>.AddPlayerMessage(stringBuilder.ToString(), 'g', true);
		}

		// Token: 0x0600CBE0 RID: 52192 RVA: 0x004A1DEC File Offset: 0x0049FFEC
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			if (this.AffectEffect(E.Effect) && this.GetShakeOff(base.Level).in100())
			{
				BodyPart bodyPart = this.FindExtraHead();
				if (bodyPart != null)
				{
					this.PrintMessage(bodyPart, E.Effect, true);
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBE1 RID: 52193 RVA: 0x004A1E3C File Offset: 0x004A003C
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.ParentObject.Effects != null && this.GetShakeOff(base.Level).in100())
			{
				TwoHeaded.targetEffects.Clear();
				int i = 0;
				int count = this.ParentObject.Effects.Count;
				while (i < count)
				{
					if (this.AffectEffect(this.ParentObject.Effects[i]))
					{
						TwoHeaded.targetEffects.Add(this.ParentObject.Effects[i]);
					}
					i++;
				}
				if (TwoHeaded.targetEffects.Count > 0)
				{
					BodyPart bodyPart = this.FindExtraHead();
					if (bodyPart != null)
					{
						Effect randomElement = TwoHeaded.targetEffects.GetRandomElement<Effect>(null);
						if (randomElement != null)
						{
							this.PrintMessage(bodyPart, randomElement, false);
							this.ParentObject.RemoveEffect(randomElement, true);
						}
					}
				}
				TwoHeaded.targetEffects.Clear();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBE2 RID: 52194 RVA: 0x004A1F18 File Offset: 0x004A0118
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CBE3 RID: 52195 RVA: 0x004A1F1B File Offset: 0x004A011B
		public override string GetDescription()
		{
			return "You have two heads.";
		}

		// Token: 0x0600CBE4 RID: 52196 RVA: 0x004A1F22 File Offset: 0x004A0122
		public int GetReducedMentalActionCost(int Level)
		{
			return 15 + 5 * Level;
		}

		// Token: 0x0600CBE5 RID: 52197 RVA: 0x004A1F2A File Offset: 0x004A012A
		public int GetShakeOff(int Level)
		{
			return 50;
		}

		// Token: 0x0600CBE6 RID: 52198 RVA: 0x004A1F30 File Offset: 0x004A0130
		public override string GetLevelText(int Level)
		{
			return "Mental actions have {{rules|" + this.GetReducedMentalActionCost(Level).ToString() + "%}} lower action costs\n" + "{{rules|" + this.GetShakeOff(Level).ToString() + "%}} chance initially and each round to shake off a negative mental status effect";
		}

		// Token: 0x0600CBE7 RID: 52199 RVA: 0x004A1F7C File Offset: 0x004A017C
		public BodyPart FindExtraHead()
		{
			BodyPart bodyPartByManager = this.ParentObject.GetBodyPartByManager(this.AdditionsManagerID, "Head", false);
			if (bodyPartByManager == null)
			{
				return null;
			}
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return null;
			}
			if (body.GetPartCount("Head") < 2)
			{
				return null;
			}
			return bodyPartByManager;
		}

		// Token: 0x0600CBE8 RID: 52200 RVA: 0x004A1FC8 File Offset: 0x004A01C8
		private bool AffectEffect(Effect FX)
		{
			return FX.IsOfTypes(100663298) && !FX.IsOfType(134217728);
		}

		// Token: 0x0600CBE9 RID: 52201 RVA: 0x004A1FE7 File Offset: 0x004A01E7
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CBEA RID: 52202 RVA: 0x004A1FF0 File Offset: 0x004A01F0
		public override bool Mutate(GameObject GO, int Level)
		{
			Body body = GO.Body;
			if (body != null)
			{
				BodyPart body2 = body.GetBody();
				BodyPart firstAttachedPart = body2.GetFirstAttachedPart("Head", 0, body, true);
				BodyPart bodyPart = ((firstAttachedPart == null) ? null : firstAttachedPart.GetFirstAttachedPart("Face", 0, body, true));
				if (firstAttachedPart != null && firstAttachedPart.Manager == null && bodyPart != null && bodyPart.Manager == null && firstAttachedPart.IsLateralitySafeToChange(0, body, bodyPart))
				{
					BodyPart bodyPart2 = body2;
					BodyPart bodyPart3 = firstAttachedPart;
					string text = "Head";
					int num = 1;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string text5 = null;
					int? num2 = new int?(body2.Category);
					BodyPart bodyPart4 = bodyPart2.AddPartAt(bodyPart3, text, num, text2, text3, text4, text5, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					BodyPart bodyPart5 = bodyPart4;
					string text6 = "Face";
					int num3 = 1;
					string text7 = null;
					string text8 = null;
					string text9 = null;
					string text10 = null;
					num2 = new int?(body2.Category);
					bodyPart5.AddPart(text6, num3, text7, text8, text9, text10, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					firstAttachedPart.ChangeLaterality(2, false);
					bodyPart.ChangeLaterality(2, false);
					firstAttachedPart.Manager = this.ChangesManagerID;
					bodyPart.Manager = this.ChangesManagerID;
				}
				else
				{
					int? num2;
					BodyPart bodyPart4;
					if (firstAttachedPart != null)
					{
						BodyPart bodyPart6 = body2;
						BodyPart bodyPart7 = firstAttachedPart;
						string text11 = "Head";
						int num4 = 0;
						string text12 = null;
						string text13 = null;
						string text14 = null;
						string text15 = null;
						num2 = new int?(body2.Category);
						bodyPart4 = bodyPart6.AddPartAt(bodyPart7, text11, num4, text12, text13, text14, text15, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
					}
					else
					{
						BodyPart bodyPart8 = body2;
						string text16 = "Head";
						int num5 = 0;
						string text17 = null;
						string text18 = null;
						string text19 = null;
						string text20 = null;
						num2 = new int?(body2.Category);
						string additionsManagerID = this.AdditionsManagerID;
						int? num6 = num2;
						string[] array = new string[] { "Back", "Arm", "Leg", "Foot", "Hands", "Feet", "Roots", "Thrown Weapon" };
						bodyPart4 = bodyPart8.AddPartAt(text16, num5, text17, text18, text19, text20, additionsManagerID, num6, null, null, null, null, null, null, null, null, null, null, null, null, "Head", array, true);
					}
					BodyPart bodyPart9 = bodyPart4;
					string text21 = "Face";
					int num7 = 0;
					string text22 = null;
					string text23 = null;
					string text24 = null;
					string text25 = null;
					num2 = new int?(body2.Category);
					bodyPart9.AddPart(text21, num7, text22, text23, text24, text25, this.AdditionsManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, true);
				}
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CBEB RID: 52203 RVA: 0x004A2418 File Offset: 0x004A0618
		public override bool Unmutate(GameObject GO)
		{
			GO.RemoveBodyPartsByManager(this.AdditionsManagerID, true);
			foreach (BodyPart bodyPart in GO.GetBodyPartsByManager(this.ChangesManagerID, false))
			{
				if (bodyPart.Laterality == 2 && bodyPart.IsLateralityConsistent(null))
				{
					bodyPart.ChangeLaterality(0, false);
				}
			}
			return base.Unmutate(GO);
		}

		// Token: 0x04004F2D RID: 20269
		private static List<Effect> targetEffects = new List<Effect>();
	}
}
