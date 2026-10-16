using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E5C RID: 3676
	[Serializable]
	public class Consumer : IPart
	{
		// Token: 0x060094B3 RID: 38067 RVA: 0x0035BE4F File Offset: 0x0035A04F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMoveLate");
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x060094B4 RID: 38068 RVA: 0x0035BE70 File Offset: 0x0035A070
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMoveLate")
			{
				if (this.Active && E.GetStringParameter("Type", null).IsNullOrEmpty() && !this.TryToConsumeObjectsIn(E.GetParameter("DestinationCell") as Cell))
				{
					return false;
				}
			}
			else if (E.ID == "VillageInit")
			{
				this.Active = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x060094B5 RID: 38069 RVA: 0x0035BEE4 File Offset: 0x0035A0E4
		public bool TryToConsumeObjectsIn(Cell C)
		{
			if (C == null || C.OnWorldMap())
			{
				return true;
			}
			if (!this.Chance.in100())
			{
				return true;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			List<GameObject> list = Event.NewGameObjectList();
			int consumeWeightCapacity = this.GetConsumeWeightCapacity();
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = C.Objects[i];
				if (!this.ShouldIgnore(gameObject))
				{
					if (!Consumer.CanConsume(gameObject, consumeWeightCapacity))
					{
						list.Clear();
						this.ParentObject.SetIntProperty("AIKeepMoving", 1, false);
						Combat.AttackCell(this.ParentObject, C, null, 0, 0, 0, false);
						return false;
					}
					list.Add(gameObject);
				}
				i++;
			}
			foreach (GameObject gameObject2 in list)
			{
				this.Consume(gameObject2);
			}
			return this.ParentObject.CurrentCell == currentCell;
		}

		// Token: 0x060094B6 RID: 38070 RVA: 0x0035BFEC File Offset: 0x0035A1EC
		public void Consume(GameObject Object)
		{
			string text = GameText.VariableReplace(this.Message, this.ParentObject, Object, false);
			if (!text.IsNullOrEmpty())
			{
				base.EmitMessage(text, ' ', false, false, Object.IsVisible(), null, null);
			}
			string text2 = GameText.VariableReplace(this.FloatMessage, this.ParentObject, Object, true);
			if (!text2.IsNullOrEmpty())
			{
				Object.ParticleText(text2, IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			}
			BeingConsumedEvent.Send(this.ParentObject, Object);
			bool flag = false;
			if (Object.IsVisible() && Options.UseParticleVFX)
			{
				CombatJuice.playPrefabAnimation(Object.CurrentCell.Location, "Abilities/AbilityVFXConsumed", this.ParentObject.ID, string.Format("{0};{1};{2}", Object.Render.Tile, Object.Render.GetTileForegroundColor(), Object.Render.getDetailColor()), null, false);
			}
			if (this.SuppressCorpseDrops)
			{
				Object.ModIntProperty("SuppressCorpseDrops", 1, false);
				flag = true;
			}
			try
			{
				if (Object.IsPlayer())
				{
					Achievement.SWALLOWED_WHOLE.Unlock();
				}
				if (Object.Count > 1)
				{
					Object.Obliterate("You were consumed whole by " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", false, string.Concat(new string[]
					{
						Object.It,
						Object.GetVerb("were", true, false, false, true, false),
						" @@consumed whole by ",
						this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
						"."
					}));
				}
				else
				{
					Object.Die(this.ParentObject, null, "You were consumed whole by " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", string.Concat(new string[]
					{
						Object.It,
						Object.GetVerb("were", true, false, false, true, false),
						" @@consumed whole by ",
						this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
						"."
					}), false, null, null, false, false, null, null, null);
				}
			}
			finally
			{
				if (flag && GameObject.Validate(ref Object) && !Object.IsNowhere())
				{
					Object.ModIntProperty("SuppressCorpseDrops", -1, true);
				}
			}
		}

		// Token: 0x060094B7 RID: 38071 RVA: 0x0035C2A0 File Offset: 0x0035A4A0
		public static bool CanConsume(GameObject Object, int WeightThreshold)
		{
			return Object.Weight < WeightThreshold;
		}

		// Token: 0x060094B8 RID: 38072 RVA: 0x0035C2AB File Offset: 0x0035A4AB
		public bool CanConsume(GameObject Object)
		{
			return Consumer.CanConsume(Object, this.GetConsumeWeightCapacity());
		}

		// Token: 0x060094B9 RID: 38073 RVA: 0x0035C2B9 File Offset: 0x0035A4B9
		public int GetConsumeWeightCapacity()
		{
			return this.ParentObject.Weight * this.WeightThresholdPercentage / 100;
		}

		// Token: 0x060094BA RID: 38074 RVA: 0x0035C2D0 File Offset: 0x0035A4D0
		public bool ShouldIgnore(GameObject Object)
		{
			return !Object.IsReal || Object.IsScenery || Object.HasTag("ExcavatoryTerrainFeature") || Object.GetMatterPhase() >= 3 || (Object.HasPart<FungalVision>() && FungalVisionary.VisionLevel <= 0) || !this.ParentObject.PhaseAndFlightMatches(Object);
		}

		// Token: 0x060094BB RID: 38075 RVA: 0x0035C32E File Offset: 0x0035A52E
		public bool WouldConsume(GameObject Object)
		{
			return !this.ShouldIgnore(Object) && this.CanConsume(Object);
		}

		// Token: 0x060094BC RID: 38076 RVA: 0x0035C344 File Offset: 0x0035A544
		public bool AnythingToConsume(Cell C)
		{
			foreach (GameObject gameObject in C.Objects)
			{
				if (this.WouldConsume(gameObject))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003D82 RID: 15746
		public int Chance = 100;

		// Token: 0x04003D83 RID: 15747
		public int WeightThresholdPercentage = 90;

		// Token: 0x04003D84 RID: 15748
		public bool SuppressCorpseDrops = true;

		// Token: 0x04003D85 RID: 15749
		public bool Active = true;

		// Token: 0x04003D86 RID: 15750
		public string Message = "{{R|=subject.T= =verb:consume= =object.an==object.directionIfAny=!}}";

		// Token: 0x04003D87 RID: 15751
		public string FloatMessage;
	}
}
