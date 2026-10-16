using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D76 RID: 3446
	[Serializable]
	public class RealityStabilized : Effect, ITierInitialized
	{
		// Token: 0x06008894 RID: 34964 RVA: 0x0030E723 File Offset: 0x0030C923
		public RealityStabilized()
		{
			this.Duration = 1;
			this.DisplayName = "{{Y|astrally tethered}}";
		}

		// Token: 0x06008895 RID: 34965 RVA: 0x0030E74B File Offset: 0x0030C94B
		public RealityStabilized(int Strength = 0, GameObject Owner = null, int Duration = 1, int IndependentStrength = 0, int IndependentStrengthDecline = 0, int IndependentVisibility = 2, bool IndependentlyProjective = false)
			: this()
		{
			this.Strength = Strength;
			this.Owner = Owner;
			this.Duration = Duration;
			this.IndependentStrength = IndependentStrength;
			this.IndependentStrengthDecline = IndependentStrengthDecline;
			this.IndependentVisibility = IndependentVisibility;
			this.IndependentlyProjective = IndependentlyProjective;
		}

		// Token: 0x06008896 RID: 34966 RVA: 0x0030E788 File Offset: 0x0030C988
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 300);
			this.Strength = Stat.Random(1, 200);
		}

		// Token: 0x06008897 RID: 34967 RVA: 0x0030E7AD File Offset: 0x0030C9AD
		public override int GetEffectType()
		{
			return 64;
		}

		// Token: 0x06008898 RID: 34968 RVA: 0x0030E7B4 File Offset: 0x0030C9B4
		public override bool SameAs(Effect e)
		{
			RealityStabilized realityStabilized = e as RealityStabilized;
			return realityStabilized.Strength == this.Strength && realityStabilized.Visibility == this.Visibility && realityStabilized.Projective == this.Projective && realityStabilized.Owner == this.Owner && realityStabilized.ScanAdjacentCells == this.ScanAdjacentCells && realityStabilized.IndependentStrength == this.IndependentStrength && realityStabilized.IndependentStrengthDecline == this.IndependentStrengthDecline && realityStabilized.IndependentVisibility == this.IndependentVisibility && realityStabilized.IndependentlyProjective == this.IndependentlyProjective && base.SameAs(e);
		}

		// Token: 0x06008899 RID: 34969 RVA: 0x0030E85F File Offset: 0x0030CA5F
		public override string GetDescription()
		{
			if (this.Visibility < 1)
			{
				return null;
			}
			if (this.Strength < 100)
			{
				return "{{y|astrally burdened}}";
			}
			return "{{Y|astrally tethered}}";
		}

		// Token: 0x0600889A RID: 34970 RVA: 0x0030E881 File Offset: 0x0030CA81
		public override string GetDetails()
		{
			if (this.Strength < 100)
			{
				return "Has trouble distorting spacetime in the local region.";
			}
			return "Cannot distort spacetime in the local region.";
		}

		// Token: 0x0600889B RID: 34971 RVA: 0x0030E898 File Offset: 0x0030CA98
		public override bool CanApplyToStack()
		{
			return true;
		}

		// Token: 0x0600889C RID: 34972 RVA: 0x0030E89C File Offset: 0x0030CA9C
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<RealityStabilized>())
			{
				return false;
			}
			if (!Object.FireEvent("ApplyRealityStabilized"))
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_spacetimeAnchored", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			return true;
		}

		// Token: 0x0600889D RID: 34973 RVA: 0x0030E8ED File Offset: 0x0030CAED
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID || ID == PooledEvent<CheckRealityDistortionAccessibilityEvent>.ID || ID == PooledEvent<CheckRealityDistortionAdvisabilityEvent>.ID || ID == PooledEvent<CheckRealityDistortionUsabilityEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == MissileTraversingCellEvent.ID;
		}

		// Token: 0x0600889E RID: 34974 RVA: 0x0030E92B File Offset: 0x0030CB2B
		public override bool HandleEvent(MissileTraversingCellEvent E)
		{
			return ((E.Cell != null && base.Object != this.GetStrongestRealityStabilizationSource(E.Cell)) || !this.RandomlyTakeEffect() || !this.Stabilize(E.Object, false)) && base.HandleEvent(E);
		}

		// Token: 0x0600889F RID: 34975 RVA: 0x0030E96C File Offset: 0x0030CB6C
		public override bool HandleEvent(CheckRealityDistortionAccessibilityEvent E)
		{
			Cell currentCell = base.Object.CurrentCell;
			if (currentCell == null || base.Object == this.GetStrongestRealityStabilizationSource(currentCell))
			{
				if (this.RandomlyTakeEffect(-E.Penetration))
				{
					E.Allow = false;
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088A0 RID: 34976 RVA: 0x0030E9B8 File Offset: 0x0030CBB8
		public override bool HandleEvent(CheckRealityDistortionUsabilityEvent E)
		{
			if (E.Threshold != null)
			{
				if (Math.Min(E.Threshold.Value, 100) <= this.Strength - E.Penetration)
				{
					return false;
				}
			}
			else if (this.RandomlyTakeEffect(-E.Penetration))
			{
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088A1 RID: 34977 RVA: 0x0030EA0D File Offset: 0x0030CC0D
		public override bool HandleEvent(CheckRealityDistortionAdvisabilityEvent E)
		{
			return this.Strength - E.Penetration <= E.Threshold && base.HandleEvent(E);
		}

		// Token: 0x060088A2 RID: 34978 RVA: 0x0030EA30 File Offset: 0x0030CC30
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			GameObject @object = base.Object;
			if (((@object != null) ? @object.Brain : null) != null)
			{
				if (this.Maintain())
				{
					this.Stabilize(false);
				}
				if (this.Duration > 1 && this.Duration != 9999)
				{
					this.Duration--;
					if (this.Duration <= 1 && this.Owner != null)
					{
						this.Owner = null;
					}
				}
				if (this.IndependentStrengthDecline != 0)
				{
					this.IndependentStrength = Math.Max(this.IndependentStrength - this.IndependentStrengthDecline, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088A3 RID: 34979 RVA: 0x0030EAC8 File Offset: 0x0030CCC8
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject @object = base.Object;
			if (((@object != null) ? @object.Brain : null) == null)
			{
				if (this.Maintain())
				{
					this.Stabilize(false);
				}
				if (this.Duration > 1 && this.Duration != 9999)
				{
					this.Duration--;
					if (this.Duration <= 1 && this.Owner != null)
					{
						this.Owner = null;
					}
				}
				if (this.IndependentStrengthDecline != 0)
				{
					this.IndependentStrength = Math.Max(this.IndependentStrength - this.IndependentStrengthDecline, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088A4 RID: 34980 RVA: 0x0030EB60 File Offset: 0x0030CD60
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EquipperUnequipped");
			Registrar.Register("InitiateRealityDistortionLocal");
			Registrar.Register("InitiateRealityDistortionRemote");
			Registrar.Register("InitiateRealityDistortionTransit");
			Registrar.Register("MaintainRealityStabilization");
			Registrar.Register("ObjectEnteringCellBlockedBySolid");
			Registrar.Register("SpaceTimeVortexContact");
			base.Register(Object, Registrar);
		}

		// Token: 0x060088A5 RID: 34981 RVA: 0x0030EBC4 File Offset: 0x0030CDC4
		public override bool FinalRender(RenderEvent E)
		{
			if (!base.Object.IsVisible())
			{
				return true;
			}
			for (int i = 0; i < E.Imposters.Count; i++)
			{
				if (E.Imposters[i].prefab == "Prefabs/Particles/NormalityField")
				{
					return true;
				}
			}
			E.Imposters.Add(new ImposterExtra.ImposterInfo("Prefabs/Particles/NormalityField", 0, null));
			return base.FinalRender(E);
		}

		// Token: 0x060088A6 RID: 34982 RVA: 0x0030EC34 File Offset: 0x0030CE34
		public override bool Render(RenderEvent E)
		{
			if (!Options.DisableImposters)
			{
				return true;
			}
			if (this.Visibility > 1)
			{
				if (this.FrameOffset == 0)
				{
					this.FrameOffset = Stat.Random(1, 1000);
				}
				if ((XRLCore.CurrentFrameLong10 + this.FrameOffset) % 2000 >= 1000)
				{
					E.ColorString = ((this.Visibility > 2) ? "&y^Y" : "&y");
					E.DetailColor = "K";
				}
			}
			return true;
		}

		// Token: 0x060088A7 RID: 34983 RVA: 0x0030ECAC File Offset: 0x0030CEAC
		protected void ProcessGameObject(GameObject obj)
		{
			RealityStabilization part = obj.GetPart<RealityStabilization>();
			if (part == null || !part.IsObjectActivePartSubject(base.Object))
			{
				return;
			}
			if (part.WorksOnAdjacentCellContents && !this.ScanAdjacentCells && !obj.InSameCellAs(base.Object))
			{
				this.ScanAdjacentCells = true;
			}
			int effectiveStrength = part.EffectiveStrength;
			if (effectiveStrength > 0)
			{
				if (effectiveStrength > this.Strength)
				{
					this.Strength = effectiveStrength;
				}
				int visibilityFor = part.GetVisibilityFor(base.Object);
				if (visibilityFor > this.Visibility)
				{
					this.Visibility = visibilityFor;
				}
				if (part.Projective && obj.CurrentCell == null)
				{
					this.Projective = true;
				}
			}
		}

		// Token: 0x060088A8 RID: 34984 RVA: 0x0030ED48 File Offset: 0x0030CF48
		public virtual bool Maintain()
		{
			if (this.Duration > 1)
			{
				this.Strength = this.IndependentStrength;
				this.Visibility = this.IndependentVisibility;
				this.Projective = this.IndependentlyProjective;
			}
			else
			{
				this.Strength = 0;
				this.Visibility = 0;
				this.Projective = false;
			}
			if (base.Object == null)
			{
				return false;
			}
			this.ProcessGameObject(base.Object);
			AmbientRealityStabilized effect = base.Object.GetEffect<AmbientRealityStabilized>();
			if (effect != null && effect.Duration > 0)
			{
				if (this.Strength < effect.Strength)
				{
					this.Strength = effect.Strength;
				}
				if (this.Visibility < effect.Visibility)
				{
					this.Visibility = effect.Visibility;
				}
				if (effect.Projective && !this.Projective)
				{
					this.Projective = true;
				}
			}
			Cell currentCell = base.Object.CurrentCell;
			if (currentCell != null)
			{
				int i = 0;
				int count = currentCell.Objects.Count;
				while (i < count)
				{
					if (currentCell.Objects[i] != base.Object)
					{
						this.ProcessGameObject(currentCell.Objects[i]);
					}
					i++;
				}
				if (this.ScanAdjacentCells)
				{
					this.ScanAdjacentCells = false;
					List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells();
					int j = 0;
					int count2 = localAdjacentCells.Count;
					while (j < count2)
					{
						Cell cell = localAdjacentCells[j];
						int k = 0;
						int count3 = cell.Objects.Count;
						while (k < count3)
						{
							this.ProcessGameObject(cell.Objects[k]);
							k++;
						}
						j++;
					}
				}
			}
			List<GameObject> equippedObjectsAndInstalledCyberneticsReadonly = base.Object.GetEquippedObjectsAndInstalledCyberneticsReadonly();
			if (equippedObjectsAndInstalledCyberneticsReadonly != null)
			{
				int l = 0;
				int count4 = equippedObjectsAndInstalledCyberneticsReadonly.Count;
				while (l < count4)
				{
					this.ProcessGameObject(equippedObjectsAndInstalledCyberneticsReadonly[l]);
					l++;
				}
			}
			if (this.Strength <= 0 && this.Duration <= 1)
			{
				this.Duration = 0;
				base.Object.RemoveEffect(this, true);
				return false;
			}
			return true;
		}

		// Token: 0x060088A9 RID: 34985 RVA: 0x0030EF34 File Offset: 0x0030D134
		public bool SynchronizeEffect()
		{
			if (base.Object == null)
			{
				return false;
			}
			int strength = this.Strength;
			int visibility = this.Visibility;
			bool projective = this.Projective;
			if (!this.Maintain())
			{
				return false;
			}
			if (this.Strength <= 0)
			{
				return false;
			}
			if (strength == this.Strength && visibility == this.Visibility && projective == this.Projective)
			{
				return false;
			}
			this.Stabilize(false);
			return true;
		}

		// Token: 0x060088AA RID: 34986 RVA: 0x0030EF9B File Offset: 0x0030D19B
		public bool RandomlyTakeEffect()
		{
			return this.Strength.in100();
		}

		// Token: 0x060088AB RID: 34987 RVA: 0x0030EFA8 File Offset: 0x0030D1A8
		public bool RandomlyTakeEffect(int Adjust)
		{
			return (this.Strength + Adjust).in100();
		}

		// Token: 0x060088AC RID: 34988 RVA: 0x0030EFB7 File Offset: 0x0030D1B7
		public bool TakeEffectBasedOnPerformance(int Performance)
		{
			return this.Strength > Performance;
		}

		// Token: 0x060088AD RID: 34989 RVA: 0x0030EFC4 File Offset: 0x0030D1C4
		private void FailedToContest(GameObject GO)
		{
			Body body = GO.Body;
			BodyPart bodyPart = ((body != null) ? body.GetFirstPart("Face") : null);
			if (GO.IsPlayer())
			{
				if (bodyPart != null && bodyPart.VariantType == "Face")
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("You try to push through the normality lattice, but it snaps back into place.");
					if (GO.GetIntProperty("Analgesia", 0) <= 0)
					{
						stringBuilder.Append(" You wince in pain.");
					}
					Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("You try to push through the normality lattice, but it snaps back into place.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			else
			{
				SensePsychicEffect sensePsychicEffect = SensePsychic.SensePsychicFromPlayer(GO);
				if (sensePsychicEffect != null)
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"You feel a psychic thud as ",
						sensePsychicEffect.Identified ? GO.does("push", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) : "someone pushes",
						" against the structure of spacetime and ",
						sensePsychicEffect.Identified ? GO.GetVerb("fail", true, false, false, true, false) : "fails",
						" to break through."
					}), null, true);
				}
				if (bodyPart != null && bodyPart.VariantType == "Face" && GO.GetIntProperty("Analgesia", 0) <= 0 && IComponent<GameObject>.Visible(GO))
				{
					IComponent<GameObject>.AddPlayerMessage(GO.Does("wince", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ".", null, true);
				}
			}
			if (GO.TakeDamage(Stat.Random(1, 6), "from %t failed assault on the structure of spacetime.", "Cosmic RealityDistortionFailure Unavoidable", "You dashed " + GO.itself + " against the crags of spacetime.", GO.It + " @@dashed " + GO.itself + " against the crags of spacetime.", null, GO, null, null, null, true, false, false, false, false, false, false, false, false, 5, null) && !GO.MakeSave("Willpower", 20, null, null, "RealityStabilization Daze", false, false, false, false, null))
			{
				GO.ApplyEffect(new Dazed(Stat.Random(1, 6)), null);
			}
			GO.UseEnergy(1000, "Effect", null, null, false);
		}

		// Token: 0x060088AE RID: 34990 RVA: 0x0030F218 File Offset: 0x0030D418
		private RealityStabilized.ContestResult TryContest(GameObject GO, int Adjust = 0, int Performance = -2147483648)
		{
			bool flag;
			if (Performance != -2147483648)
			{
				flag = this.TakeEffectBasedOnPerformance(Performance);
			}
			else
			{
				flag = this.RandomlyTakeEffect(Adjust);
			}
			if (flag)
			{
				this.FailedToContest(GO);
				return RealityStabilized.ContestResult.Failure;
			}
			if (!GO.IsPlayer())
			{
				SensePsychicEffect sensePsychicEffect = SensePsychic.SensePsychicFromPlayer(GO);
				if (sensePsychicEffect != null)
				{
					IComponent<GameObject>.AddPlayerMessage("You feel a psychic whiff as " + (sensePsychicEffect.Identified ? GO.does("push", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) : "something pushes") + " past resistance in the structure of spacetime.", null, true);
				}
			}
			return RealityStabilized.ContestResult.Success;
		}

		// Token: 0x060088AF RID: 34991 RVA: 0x0030F2B0 File Offset: 0x0030D4B0
		private int ProcessEvent(Event E, out bool Dual, bool MarkHandled = false)
		{
			Dual = false;
			int num = -E.GetIntParameter("RealityStabilizationPenetration", 0);
			if (E.HasParameter("Mutation"))
			{
				IPart part = E.GetParameter("Mutation") as IPart;
				if (part != null)
				{
					GameObject parentObject = part.ParentObject;
					if (parentObject != null && parentObject != base.Object)
					{
						RealityStabilized effect = parentObject.GetEffect<RealityStabilized>();
						if (effect != null)
						{
							Dual = true;
							int strength = effect.Strength;
							if (strength > this.Strength + num)
							{
								num += strength - (this.Strength + num);
							}
						}
						if (MarkHandled)
						{
							E.SetParameter("InterdictionHandled", 1);
							MarkHandled = false;
						}
					}
				}
			}
			if (E.HasParameter("Cell"))
			{
				Cell cell = E.GetParameter("Cell") as Cell;
				GameObject strongestRealityStabilizationSource = this.GetStrongestRealityStabilizationSource(cell);
				if (strongestRealityStabilizationSource != null && strongestRealityStabilizationSource != base.Object)
				{
					RealityStabilized effect2 = strongestRealityStabilizationSource.GetEffect<RealityStabilized>();
					if (effect2 != null)
					{
						Dual = true;
						int strength2 = effect2.Strength;
						if (strength2 > this.Strength + num)
						{
							num += strength2 - (this.Strength + num);
						}
					}
					if (MarkHandled)
					{
						E.SetParameter("InterdictionHandled", 1);
						MarkHandled = false;
					}
				}
			}
			return num;
		}

		// Token: 0x060088B0 RID: 34992 RVA: 0x0030F3C4 File Offset: 0x0030D5C4
		private int ProcessEvent(Event E, bool MarkHandled = false)
		{
			bool flag;
			return this.ProcessEvent(E, out flag, MarkHandled);
		}

		// Token: 0x060088B1 RID: 34993 RVA: 0x0030F3DC File Offset: 0x0030D5DC
		public RealityStabilized.ContestResult OptionToContest(GameObject GO, int Adjust = 0, bool Dual = false)
		{
			int num = 100 - (this.Strength + Adjust);
			int num2 = GO.Stat("Intelligence", 0);
			Mutations part = GO.GetPart<Mutations>();
			if (part != null)
			{
				if (part.HasMutation("Intuition"))
				{
					num2 += 10;
				}
				if (part.HasMutation("Precognition"))
				{
					num2 += 5;
				}
				if (part.HasMutation("SpacetimeVortex"))
				{
					num2 += 5;
				}
				if (part.HasMutation("Skittish"))
				{
					num -= 10;
				}
			}
			if (GO.HasSkill("Discipline_IronMind"))
			{
				num2 += 2;
			}
			if (GO.HasSkill("Discipline_Lionheart"))
			{
				num += 3;
			}
			if (num2 <= 10)
			{
				num += 20;
			}
			else if (num2 <= 15)
			{
				num += 10;
			}
			else if (num2 <= 20)
			{
				num -= 10;
			}
			else if (num2 <= 25)
			{
				num -= 5;
			}
			int num3;
			if (num2 <= 10)
			{
				num3 = 20;
			}
			else if (num2 <= 20)
			{
				num3 = 10;
			}
			else if (num2 <= 30)
			{
				num3 = 5;
			}
			else
			{
				num3 = 1;
			}
			if (num3 > 1)
			{
				num -= num % num3;
			}
			if (num > 100 - num3)
			{
				num = 100 - num3;
			}
			bool flag = GO.IsPlayer() && Options.SifrahRealityDistortion;
			if (num <= 0 && !flag)
			{
				return RealityStabilized.ContestResult.Abjection;
			}
			if (GO.IsPlayer())
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				string chanceColor = Stat.GetChanceColor(num);
				if (Dual)
				{
					stringBuilder.Append("A normality lattice prevents you from altering spacetime in both your local region and the local region you're trying to interact with.");
				}
				else
				{
					stringBuilder.Append("A normality lattice prevents you from altering spacetime in ").Append((GO.CurrentCell == base.Object.CurrentCell) ? "the" : "that").Append(" local region.");
				}
				stringBuilder.Append(" You can try to push through at some risk.");
				if (flag)
				{
					int num4 = GO.Stat("Ego", 0);
					int num5 = 20 - num / 5 - num4 / 5;
					string text;
					if (num5 >= 12)
					{
						text = "almost impossible";
					}
					else if (num5 >= 9)
					{
						text = "challenging";
					}
					else if (num5 >= 6)
					{
						text = "moderately difficult";
					}
					else if (num5 >= 3)
					{
						text = "easy";
					}
					else
					{
						text = "very easy";
					}
					stringBuilder.Append(" Your feeling is that success would be ").Append(text).Append(". Do you want to try?");
					if (Popup.ShowYesNo(stringBuilder.ToString(), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return RealityStabilized.ContestResult.Surrender;
					}
					GameObject gameObject;
					if ((gameObject = this.Owner) == null)
					{
						gameObject = base.Object ?? GO;
					}
					GameObject gameObject2 = gameObject;
					RealityDistortionSifrah realityDistortionSifrah = new RealityDistortionSifrah(gameObject2, "RealityStabilizationPenetration", "overcoming a normality lattice", num4, Math.Max((this.Strength + Adjust) / 5, 1));
					realityDistortionSifrah.Play(gameObject2);
					if (realityDistortionSifrah.Abort || realityDistortionSifrah.InterfaceExitRequested)
					{
						return RealityStabilized.ContestResult.Surrender;
					}
					return this.TryContest(GO, 0, realityDistortionSifrah.Performance);
				}
				else
				{
					stringBuilder.Append(" You estimate");
					if (num < num3)
					{
						stringBuilder.Append(" less than a {{").Append(chanceColor).Append('|')
							.Append(num3.ToString())
							.Append("%}}");
					}
					else
					{
						stringBuilder.Append(" about a {{").Append(chanceColor).Append('|')
							.Append(num.ToString())
							.Append("%}}");
					}
					stringBuilder.Append(chanceColor).Append(" chance of success. Do you want to try?");
					if (Popup.ShowYesNo(stringBuilder.ToString(), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						return this.TryContest(GO, Adjust, int.MinValue);
					}
					return RealityStabilized.ContestResult.Surrender;
				}
			}
			else
			{
				if (num.in100() && num.in100())
				{
					return this.TryContest(GO, Adjust, int.MinValue);
				}
				return RealityStabilized.ContestResult.Surrender;
			}
		}

		// Token: 0x060088B2 RID: 34994 RVA: 0x0030F738 File Offset: 0x0030D938
		private bool AttemptInterdictedAction(Event E)
		{
			if (E.HasFlag("Forced"))
			{
				return true;
			}
			if (E.HasParameter("Mutation"))
			{
				IPart part = E.GetParameter("Mutation") as IPart;
				GameObject gameObject = E.GetGameObjectParameter("Actor") ?? part.ParentObject;
				bool flag;
				int num = this.ProcessEvent(E, out flag, true);
				RealityStabilized.ContestResult contestResult = this.OptionToContest(gameObject, num, flag && num > 0);
				if (contestResult == RealityStabilized.ContestResult.Abjection)
				{
					if (flag && num > 0)
					{
						RealityStabilized.ShowDualInterdictMessage(gameObject, E);
					}
					else if (gameObject.CurrentCell == base.Object.CurrentCell)
					{
						RealityStabilized.ShowGenericInterdictMessage(gameObject, E);
					}
					else
					{
						RealityStabilized.ShowDistantInterdictMessage(gameObject, E);
					}
				}
				if (contestResult == RealityStabilized.ContestResult.Success)
				{
					return true;
				}
				if (contestResult == RealityStabilized.ContestResult.Failure)
				{
					E.RequestInterfaceExit();
				}
			}
			else if (E.HasParameter("Device"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Device");
				GameObject gameObject2 = E.GetGameObjectParameter("Actor") ?? E.GetGameObjectParameter("Operator");
				int num2 = -GetRealityStabilizationPenetrationEvent.GetFor(gameObjectParameter) + this.ProcessEvent(E, true);
				if (!this.RandomlyTakeEffect(num2))
				{
					return true;
				}
				RealityStabilized.ShortCircuitDevice(gameObjectParameter, gameObject2, E);
			}
			else if (E.HasParameter("Food"))
			{
				int num3 = -GetRealityStabilizationPenetrationEvent.GetFor(E.GetGameObjectParameter("Food")) + this.ProcessEvent(E, true);
				if (!this.RandomlyTakeEffect(num3))
				{
					return true;
				}
			}
			else if (E.HasParameter("Source"))
			{
				int num4 = -GetRealityStabilizationPenetrationEvent.GetFor(E.GetGameObjectParameter("Source")) + this.ProcessEvent(E, true);
				if (!this.RandomlyTakeEffect(num4))
				{
					return true;
				}
			}
			else if (!this.RandomlyTakeEffect())
			{
				return true;
			}
			return false;
		}

		// Token: 0x060088B3 RID: 34995 RVA: 0x0030F8D4 File Offset: 0x0030DAD4
		public static void ShowGenericInterdictMessage(GameObject CheckObject = null, Event E = null)
		{
			if (CheckObject == null || CheckObject.IsPlayer())
			{
				string text = ((E != null) ? E.GetStringParameter("Purpose", null) : null);
				Popup.Show("You cannot alter spacetime through the normality lattice in the local region" + ((!string.IsNullOrEmpty(text)) ? (", in order to " + text) : "") + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x060088B4 RID: 34996 RVA: 0x0030F938 File Offset: 0x0030DB38
		public static void ShowDistantInterdictMessage(GameObject CheckObject = null, Event E = null)
		{
			if (CheckObject == null || CheckObject.IsPlayer())
			{
				string text = ((E != null) ? E.GetStringParameter("Purpose", null) : null);
				Popup.Show("You cannot alter spacetime through the normality lattice in the local region" + ((!string.IsNullOrEmpty(text)) ? (", in order to " + text) : "") + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x060088B5 RID: 34997 RVA: 0x0030F99C File Offset: 0x0030DB9C
		public static void ShowDualInterdictMessage(GameObject CheckObject = null, Event E = null)
		{
			if (CheckObject == null || CheckObject.IsPlayer())
			{
				string text = ((E != null) ? E.GetStringParameter("Purpose", null) : null);
				Popup.Show("You cannot alter spacetime through either the normality lattice in your local region or the local region you're trying to interact with" + ((!string.IsNullOrEmpty(text)) ? (", in order to " + text) : "") + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x060088B6 RID: 34998 RVA: 0x0030FA00 File Offset: 0x0030DC00
		private static void ShortCircuitDevice(GameObject Device, GameObject User, Event FromEvent = null)
		{
			if (User == null)
			{
				GameObject gameObject;
				if ((gameObject = Device.Equipped) == null)
				{
					gameObject = Device.Implantee ?? Device.InInventory;
				}
				User = gameObject;
			}
			if (User == null)
			{
				IComponent<GameObject>.AddPlayerMessage(Device.Does("shower", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, new bool?(true), true, null, false) + " sparks everywhere.", null, true);
			}
			else if (User.IsPlayer())
			{
				Popup.Show(Device.Does("emit", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, new bool?(true), true, User, false) + " a shower of sparks!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else if (IComponent<GameObject>.Visible(User))
			{
				IComponent<GameObject>.AddPlayerMessage(Device.Does("shower", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, new bool?(true), true, User, false) + " sparks everywhere.", null, true);
			}
			if (User != null)
			{
				if (User.TakeDamage(Stat.Random(1, 6), "from %t electrical discharge!", "Electric", null, null, User, Device, null, null, Device, true, false, false, false, false, false, false, false, User.IsPlayer(), 5, null))
				{
					for (int i = 0; i < 1; i++)
					{
						User.ParticleText("&Wú", false);
					}
					for (int j = 0; j < 1; j++)
					{
						User.ParticleText("&Yú", false);
					}
				}
				Device.UseCharge("10d100".RollCached(), false, 0L, true, true, null);
				User.UseEnergy(1000, "Item Use Failure", null, null, false);
			}
			if (FromEvent != null)
			{
				FromEvent.RequestInterfaceExit();
			}
		}

		// Token: 0x060088B7 RID: 34999 RVA: 0x0030FBA9 File Offset: 0x0030DDA9
		private static void ShortCircuitDevice(Event E)
		{
			RealityStabilized.ShortCircuitDevice(E.GetGameObjectParameter("Device"), E.GetGameObjectParameter("Operator"), E);
		}

		// Token: 0x060088B8 RID: 35000 RVA: 0x0030FBC8 File Offset: 0x0030DDC8
		public bool Stabilize(bool Projecting = false)
		{
			bool flag;
			bool flag2;
			return this.Stabilize(base.Object, Projecting, out flag, out flag2);
		}

		// Token: 0x060088B9 RID: 35001 RVA: 0x0030FBE8 File Offset: 0x0030DDE8
		public bool Stabilize(GameObject GO, bool Projecting = false)
		{
			bool flag;
			bool flag2;
			return this.Stabilize(GO, Projecting, out flag, out flag2);
		}

		// Token: 0x060088BA RID: 35002 RVA: 0x0030FC04 File Offset: 0x0030DE04
		public bool Stabilize(GameObject GO, out bool Relevant)
		{
			bool flag;
			return this.Stabilize(GO, false, out Relevant, out flag);
		}

		// Token: 0x060088BB RID: 35003 RVA: 0x0030FC1C File Offset: 0x0030DE1C
		public bool Stabilize(out bool Relevant, out bool CanDestroy)
		{
			return this.Stabilize(base.Object, false, out Relevant, out CanDestroy);
		}

		// Token: 0x060088BC RID: 35004 RVA: 0x0030FC2D File Offset: 0x0030DE2D
		public bool Stabilize(bool Projecting, out bool Relevant, out bool CanDestroy)
		{
			return this.Stabilize(base.Object, Projecting, out Relevant, out CanDestroy);
		}

		// Token: 0x060088BD RID: 35005 RVA: 0x0030FC40 File Offset: 0x0030DE40
		public bool Stabilize(GameObject GO, bool Projecting, out bool Relevant)
		{
			bool flag;
			return this.Stabilize(GO, Projecting, out Relevant, out flag);
		}

		// Token: 0x060088BE RID: 35006 RVA: 0x0030FC58 File Offset: 0x0030DE58
		public bool Stabilize(GameObject GO, bool Projecting, out bool Relevant, out bool CanDestroy)
		{
			RealityStabilizeEvent.Send(this, GO, Projecting, out Relevant, out CanDestroy);
			return CanDestroy && (GO == null || GO.IsInvalid());
		}

		// Token: 0x060088BF RID: 35007 RVA: 0x0030FC78 File Offset: 0x0030DE78
		private GameObject GetStrongestRealityStabilizationSource(Cell C)
		{
			GameObject gameObject = null;
			int num = 0;
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				RealityStabilized effect = C.Objects[i].GetEffect<RealityStabilized>();
				if (effect != null && effect.Strength > num)
				{
					gameObject = C.Objects[i];
					num = effect.Strength;
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060088C0 RID: 35008 RVA: 0x0030FCD8 File Offset: 0x0030DED8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectEnteringCellBlockedBySolid")
			{
				if (this.Projective)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
					bool flag;
					bool flag2;
					if (this.Stabilize(gameObjectParameter, true, out flag, out flag2))
					{
						return false;
					}
					if (flag2)
					{
						if (base.Object.IsPlayer())
						{
							StringBuilder stringBuilder = Event.NewStringBuilder(null);
							stringBuilder.Append(gameObjectParameter.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
							if (gameObjectParameter.HasPart<Metal>())
							{
								stringBuilder.Append(" {{electrical|").Append(gameObjectParameter.GetVerb("spark", false, false, false, true, false)).Append(" and")
									.Append(gameObjectParameter.GetVerb("buzz", true, false, false, true, false))
									.Append("}}");
							}
							else
							{
								stringBuilder.Append(" {{C|").Append(gameObjectParameter.GetVerb("shift", false, false, false, true, false)).Append(" and")
									.Append(gameObjectParameter.GetVerb("waver", true, false, false, true, false))
									.Append("}}");
							}
							stringBuilder.Append(" for a moment, but nothing else happens.");
							Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						base.Object.UseEnergy(1000, "Movement Failure", null, null, false);
					}
				}
			}
			else if (E.ID == "InitiateRealityDistortionLocal")
			{
				if (!this.AttemptInterdictedAction(E))
				{
					return false;
				}
			}
			else if (E.ID == "InitiateRealityDistortionRemote" || E.ID == "InitiateRealityDistortionTransit")
			{
				if (E.GetIntParameter("InterdictionHandled", 0) < 1 && (base.Object == E.GetGameObjectParameter("Object") || base.Object == E.GetGameObjectParameter("Subject") || base.Object.CurrentCell == null || base.Object == this.GetStrongestRealityStabilizationSource(base.Object.CurrentCell)))
				{
					return this.AttemptInterdictedAction(E);
				}
			}
			else if (E.ID == "SpaceTimeVortexContact")
			{
				if (this.Stabilize(E.GetGameObjectParameter("Object"), false))
				{
					return false;
				}
			}
			else if (E.ID == "MaintainRealityStabilization" || E.ID == "EquipperUnequipped")
			{
				this.Maintain();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003A55 RID: 14933
		public const int DEFAULT_VISIBILITY = 2;

		// Token: 0x04003A56 RID: 14934
		public int Strength;

		// Token: 0x04003A57 RID: 14935
		public int Visibility = 2;

		// Token: 0x04003A58 RID: 14936
		public bool Projective;

		// Token: 0x04003A59 RID: 14937
		public GameObject Owner;

		// Token: 0x04003A5A RID: 14938
		public bool ScanAdjacentCells;

		// Token: 0x04003A5B RID: 14939
		public int IndependentStrength;

		// Token: 0x04003A5C RID: 14940
		public int IndependentStrengthDecline;

		// Token: 0x04003A5D RID: 14941
		public int IndependentVisibility = 2;

		// Token: 0x04003A5E RID: 14942
		public bool IndependentlyProjective;

		// Token: 0x04003A5F RID: 14943
		private int FrameOffset;

		// Token: 0x02001C37 RID: 7223
		public enum ContestResult
		{
			// Token: 0x04006F81 RID: 28545
			Abjection,
			// Token: 0x04006F82 RID: 28546
			Surrender,
			// Token: 0x04006F83 RID: 28547
			Failure,
			// Token: 0x04006F84 RID: 28548
			Success
		}
	}
}
