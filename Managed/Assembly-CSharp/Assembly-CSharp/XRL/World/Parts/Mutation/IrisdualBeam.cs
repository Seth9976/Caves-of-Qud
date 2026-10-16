using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Genkit;
using Qud.UI;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200141E RID: 5150
	[Serializable]
	public class IrisdualBeam : BaseMutation
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600C858 RID: 51288 RVA: 0x0048EA76 File Offset: 0x0048CC76
		public GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref IrisdualBeam._Projectile))
				{
					IrisdualBeam._Projectile = GameObject.CreateUnmodified("ProjectileIrisdualBeam");
				}
				return IrisdualBeam._Projectile;
			}
		}

		// Token: 0x0600C85A RID: 51290 RVA: 0x0048EACC File Offset: 0x0048CCCC
		public int GetDelay(int Level)
		{
			return 1;
		}

		// Token: 0x0600C85B RID: 51291 RVA: 0x0048EACF File Offset: 0x0048CCCF
		public int GetBeams(int Level)
		{
			return Mathf.RoundToInt((float)Level * 0.6f);
		}

		// Token: 0x0600C85C RID: 51292 RVA: 0x0048EADE File Offset: 0x0048CCDE
		public int GetDuration(int Level)
		{
			return this.GetDelay(Level) * 3;
		}

		// Token: 0x0600C85D RID: 51293 RVA: 0x0048EAE9 File Offset: 0x0048CCE9
		public int GetCooldown(int Level)
		{
			return Mathf.RoundToInt(36f - (float)Level * 1.1f);
		}

		// Token: 0x0600C85E RID: 51294 RVA: 0x0048EAFE File Offset: 0x0048CCFE
		public int GetMinimumRange(int Level)
		{
			return 1;
		}

		// Token: 0x0600C85F RID: 51295 RVA: 0x0048EB01 File Offset: 0x0048CD01
		public override string GetDescription()
		{
			return "You molt powerful beams across the spectrum of light and matter.";
		}

		// Token: 0x0600C860 RID: 51296 RVA: 0x0048EB08 File Offset: 0x0048CD08
		public override string GetLevelText(int Level)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			string text = ((this.GetBeams(Level) == 1) ? "beam" : "beams");
			stringBuilder.Append("Fires {{rules|").Append(this.GetBeams(Level)).Append("}} " + text + " at random enemies every round for {{rules|")
				.Append(this.GetDuration(Level))
				.Append("}} rounds\n");
			stringBuilder.Append("Cooldown: {{rules|").Append(this.GetCooldown(Level)).Append("}} rounds");
			return stringBuilder.ToString();
		}

		// Token: 0x0600C861 RID: 51297 RVA: 0x0048EB9C File Offset: 0x0048CD9C
		public override void CollectStats(Templates.StatCollector Collector, int Level)
		{
			Collector.Set("Beams", this.GetBeams(Level), false, 0);
			Collector.Set("Duration", this.GetDuration(Level), false, 0);
			Collector.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C862 RID: 51298 RVA: 0x0048EBEC File Offset: 0x0048CDEC
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			Writer.WriteOptimized(this.Targets.Count);
			foreach (Location2D location2D in this.Targets)
			{
				Writer.WriteOptimized(location2D.X);
				Writer.WriteOptimized(location2D.Y);
			}
		}

		// Token: 0x0600C863 RID: 51299 RVA: 0x0048EC6C File Offset: 0x0048CE6C
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			int num = Reader.ReadOptimizedInt32();
			this.Targets.EnsureCapacity<Location2D>(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = Reader.ReadOptimizedInt32();
				int num3 = Reader.ReadOptimizedInt32();
				this.Targets.Add(Location2D.Get(num2, num3));
			}
		}

		// Token: 0x0600C864 RID: 51300 RVA: 0x0048ECC0 File Offset: 0x0048CEC0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EnteredCellEvent.ID || ID == LeftCellEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<AfterGameLoadedEvent>.ID;
		}

		// Token: 0x0600C865 RID: 51301 RVA: 0x0048ECFE File Offset: 0x0048CEFE
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.Duration <= 0 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandIrisdualBeam", 10, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C866 RID: 51302 RVA: 0x0048ED34 File Offset: 0x0048CF34
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (!IrisdualBeam.Preloaded)
			{
				this.PreloadClips();
			}
			if (this.Duration > 0)
			{
				this.LockRefractors();
				int num = this.Delay - 1;
				this.Delay = num;
				if (num > 0)
				{
					if (this.ParentObject.IsPlayer())
					{
						The.Core.RenderDelay(500, true, true, false);
					}
					this.ParentObject.ForfeitTurn(false);
					this.AlertTargetLine();
				}
				else
				{
					this.Perform();
					if (this.Duration > 1)
					{
						this.PickTarget();
						this.AlertTargetLine();
					}
				}
				num = this.Duration - 1;
				this.Duration = num;
				if (num == 0)
				{
					this.ParentObject.RemoveEffect(typeof(IrisdualMolting));
					this.ParentObject.ApplyEffect(new IrisdualCallow(5), null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C867 RID: 51303 RVA: 0x0048EE07 File Offset: 0x0048D007
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Duration > 0 && !this.Targets.IsNullOrEmpty<Location2D>())
			{
				this.RecalculateLines(true, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C868 RID: 51304 RVA: 0x0048EE2E File Offset: 0x0048D02E
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			if (this.Duration > 0 && !this.Targets.IsNullOrEmpty<Location2D>())
			{
				this.LockRefractors();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C869 RID: 51305 RVA: 0x0048EE53 File Offset: 0x0048D053
		public bool IsValidTarget(GameObject Object)
		{
			return !Object.InSameCellAs(this.ParentObject) && (this.ParentObject.IsHostileTowards(Object) || Object.IsHostileTowards(this.ParentObject) || Object.HasPart<RefractCosmic>());
		}

		// Token: 0x0600C86A RID: 51306 RVA: 0x0048EE8C File Offset: 0x0048D08C
		public int CompareTarget(GameObject First, GameObject Second)
		{
			bool flag = this.Threatened.Contains(this.ParentObject.GetDirectionToward(First, false));
			bool flag2 = this.Threatened.Contains(this.ParentObject.GetDirectionToward(Second, false));
			int num = flag.CompareTo(flag2);
			if (num != 0)
			{
				return num;
			}
			GameObject target = this.ParentObject.Target;
			if (target != null)
			{
				flag = First == target;
				num = (Second == target).CompareTo(flag);
				if (num != 0)
				{
					return num;
				}
			}
			flag = First.HasPart<RefractCosmic>();
			flag2 = Second.HasPart<RefractCosmic>();
			return flag.CompareTo(flag2);
		}

		// Token: 0x0600C86B RID: 51307 RVA: 0x0048EF18 File Offset: 0x0048D118
		public bool PickTarget()
		{
			if (!this.Targets.IsNullOrEmpty<Location2D>())
			{
				return false;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell.OnWorldMap())
			{
				return this.ParentObject.Fail("You cannot do that on the world map.");
			}
			List<GameObject> list = currentCell.ParentZone.FastCombatSquareVisibility(currentCell.X, currentCell.Y, 9999999, this.ParentObject, new Predicate<GameObject>(this.IsValidTarget), false, true, true, true, null);
			this.Threatened.Clear();
			foreach (Cell cell in currentCell.YieldAdjacentCells(1, true, true))
			{
				if (cell.HasCombatObject(false))
				{
					this.Threatened.Add(currentCell.GetDirectionFromCell(cell, false));
				}
			}
			list.ShuffleInPlace<GameObject>(null);
			Algorithms.StableSortInPlace<GameObject>(list, new Comparison<GameObject>(this.CompareTarget));
			this.ClearTargets();
			int level = base.Level;
			int beams = this.GetBeams(level);
			int i = 0;
			int num = Math.Min(beams, list.Count);
			while (i < beams)
			{
				Cell cell2 = null;
				if (i >= num)
				{
					for (int j = 0; j < 25; j++)
					{
						cell2 = currentCell.ParentZone.GetRandomCell(0);
						if (!this.IsDirectionTargeted(cell2))
						{
							break;
						}
					}
				}
				else
				{
					cell2 = list[i].CurrentCell;
				}
				if (cell2 != null)
				{
					this.Targets.Add(cell2.Location);
				}
				i++;
			}
			this.Delay = this.GetDelay(level);
			return true;
		}

		// Token: 0x0600C86C RID: 51308 RVA: 0x0048F0AC File Offset: 0x0048D2AC
		public bool IsDirectionTargeted(Cell Cell)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			string directionFrom = currentCell.GetDirectionFrom(Cell.Location);
			foreach (Location2D location2D in this.Targets)
			{
				if (currentCell.GetDirectionFrom(location2D) == directionFrom)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C86D RID: 51309 RVA: 0x0048F12C File Offset: 0x0048D32C
		public void Perform()
		{
			bool flag = this.ParentObject.IsVisible();
			IrisdualMolting effect = this.ParentObject.GetEffect<IrisdualMolting>();
			try
			{
				this.PausePaint = true;
				bool flag2 = false;
				this.RecalculateLines(false, true);
				if (flag)
				{
					FadeToBlack.SetTileMode();
					FadeToBlack.FadeOut(1f, new Color(0f, 0f, 0f, 0.25f));
					if (effect != null)
					{
						effect.SetActive(false);
					}
					base.PlayWorldSound("Sounds/Creatures/Ability/sfx_creature_girshNephilim_irisdualBeam_windup", 0.5f, 0f, false, null, 0f, 1f, 0.2f, 20);
					CombatJuice.playPrefabAnimation(this.ParentObject, "Particles/BeamWarmUp", null, null, null, true);
					flag2 = flag2 || !The.Core.RenderDelay(1000, true, true, false);
					MissileWeaponVFXConfiguration missileWeaponVFXConfiguration = null;
					for (int i = 0; i < this.Lines.Count; i++)
					{
						this.CollectVFX(i, ref missileWeaponVFXConfiguration);
					}
					if (missileWeaponVFXConfiguration != null)
					{
						CombatJuice.missileWeaponVFX(missileWeaponVFXConfiguration, true);
						CombatJuice.cameraShake(1.5f, true);
					}
				}
				this.PlayBeamSound();
				for (int j = 0; j < this.Lines.Count; j++)
				{
					this.FireLine(j);
				}
				if (flag)
				{
					flag2 = flag2 || !The.Core.RenderDelay(1500, true, true, false);
					FadeToBlack.FadeIn(0.5f, new Color(0f, 0f, 0f, 0.25f));
					flag2 = flag2 || !The.Core.RenderDelay(500, true, true, false);
					if (flag2)
					{
						CombatJuice.StopPrefabAnimation("Particles/BeamWarmUp");
					}
				}
			}
			finally
			{
				this.PausePaint = false;
				this.Delay = 0;
				this.ClearTargets();
				base.UseEnergy(1000);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
				if (effect != null)
				{
					effect.SetActive(true);
				}
				if (flag)
				{
					FadeToBlack.FadeIn(0f, new Color(0f, 0f, 0f, 0f));
					FadeToBlack.SetUIMode();
				}
			}
		}

		// Token: 0x0600C86E RID: 51310 RVA: 0x0048F354 File Offset: 0x0048D554
		public void CollectVFX(int Index, ref MissileWeaponVFXConfiguration VFXConfig)
		{
			List<Cell> list = this.Lines[Index];
			if (this.Projectile.HasTagOrProperty("ProjectileVFX") && list.Count > 1)
			{
				string propertyOrTag = this.Projectile.GetPropertyOrTag("ProjectileVFXConfiguration", null);
				if (VFXConfig == null)
				{
					VFXConfig = MissileWeaponVFXConfiguration.next();
				}
				VFXConfig.addStep(Index, list[0].Location);
				MissileWeaponVFXConfiguration missileWeaponVFXConfiguration = VFXConfig;
				List<Cell> list2 = list;
				missileWeaponVFXConfiguration.addStep(Index, list2[list2.Count - 1].Location);
				VFXConfig.setPathProjectileVFX(Index, this.Projectile.GetPropertyOrTag("ProjectileVFX", null), propertyOrTag);
			}
		}

		// Token: 0x0600C86F RID: 51311 RVA: 0x0048F3F0 File Offset: 0x0048D5F0
		public void FireLine(int Index)
		{
			List<GameObject> objects = this.Objects;
			List<Cell> list = this.Lines[Index];
			Projectile part = this.Projectile.GetPart<Projectile>();
			objects.Clear();
			int i = 1;
			int count = list.Count;
			while (i < count)
			{
				foreach (GameObject gameObject in list[i].Objects)
				{
					if (gameObject != this.ParentObject && gameObject.IsReal && !gameObject.HasPart<RefractCosmic>())
					{
						objects.Add(gameObject);
					}
				}
				for (int j = objects.Count - 1; j >= 0; j--)
				{
					this.InflictDamage(objects[j], part);
					objects.RemoveAt(j);
				}
				i++;
			}
		}

		// Token: 0x0600C870 RID: 51312 RVA: 0x0048F4D8 File Offset: 0x0048D6D8
		public void PlayBeamSound()
		{
			Cell cell = this.ParentObject.CurrentCell;
			Cell playerCell = The.PlayerCell;
			if (playerCell != null && cell.ParentZone == playerCell.ParentZone)
			{
				int num = int.MaxValue;
				int num2 = int.MaxValue;
				int i = 0;
				int count = this.Lines.Count;
				while (i < count)
				{
					List<Cell> list = this.Lines[i];
					int j = 1;
					int count2 = list.Count;
					while (j < count2)
					{
						Cell cell2 = list[j];
						int costAtPoint = Zone.SoundMap.GetCostAtPoint(cell2.Location);
						int num3 = cell2.PathDistanceTo(playerCell);
						if (costAtPoint < num)
						{
							cell = cell2;
							num = costAtPoint;
							num2 = cell2.PathDistanceTo(playerCell);
						}
						else if (costAtPoint == num && num3 < num2)
						{
							cell = cell2;
							num2 = cell2.PathDistanceTo(playerCell);
						}
						j++;
					}
					i++;
				}
			}
			cell.PlayWorldSound("sfx_creature_girshNephilim_irisdualBeam_attack", 0.5f, 0f, false, 0f, 1f, 0.5f, 20);
		}

		// Token: 0x0600C871 RID: 51313 RVA: 0x0048F5E4 File Offset: 0x0048D7E4
		public void InflictDamage(GameObject Object, Projectile ProjectilePart)
		{
			int num = Stat.RollDamagePenetrations(Stats.GetCombatAV(Object), ProjectilePart.BasePenetration, ProjectilePart.BasePenetration);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				num2 += ProjectilePart.BaseDamage.RollCached();
			}
			if (num2 > 0)
			{
				Object.TakeDamage(ref num2, null, null, null, null, this.ParentObject, null, null, null, "", false, false, false, false, false, false, false, false, false, 5, null);
			}
			int j = 0;
			int num3 = IrisdualBeam.DamageTypes.Length;
			while (j < num3)
			{
				int num4 = Stat.Random(30, 40);
				GameObject gameObject = this.ParentObject;
				Object.TakeDamage(ref num4, IrisdualBeam.DamageTypes[j], null, null, null, gameObject, null, null, null, "", false, false, false, false, false, false, false, false, false, 5, null);
				num2 += num4;
				j++;
			}
			if (num2 > 0)
			{
				IComponent<GameObject> physics = Object.Physics;
				string text = "take";
				string text2 = num2.ToString() + " damage from";
				GameObject projectile = this.Projectile;
				string text3 = null;
				string text4 = null;
				string text5 = null;
				string text6 = null;
				GameObject gameObject2 = null;
				GameObject gameObject = this.ParentObject;
				physics.DidXToY(text, text2, projectile, text3, text4, text5, text6, gameObject2, Object, false, false, false, false, false, null, gameObject, null, false, false, false, false, false, null);
			}
		}

		// Token: 0x0600C872 RID: 51314 RVA: 0x0048F6ED File Offset: 0x0048D8ED
		public void ClearTargets()
		{
			this.Targets.Clear();
			this.ClearLines();
		}

		// Token: 0x0600C873 RID: 51315 RVA: 0x0048F700 File Offset: 0x0048D900
		public void ClearLines()
		{
			this.RecalculateSegment = 0L;
			foreach (List<Cell> list in this.Lines)
			{
				list.Clear();
			}
		}

		// Token: 0x0600C874 RID: 51316 RVA: 0x0048F758 File Offset: 0x0048D958
		public List<Cell> TakeLine()
		{
			foreach (List<Cell> list in this.Lines)
			{
				if (list.IsNullOrEmpty<Cell>())
				{
					return list;
				}
			}
			List<Cell> list2 = new List<Cell>();
			this.Lines.Add(list2);
			return list2;
		}

		// Token: 0x0600C875 RID: 51317 RVA: 0x0048F7C8 File Offset: 0x0048D9C8
		public void RecalculateLines(bool Prospective = false, bool Force = false)
		{
			long segments = The.Game.Segments;
			if (!Force && segments == this.RecalculateSegment)
			{
				return;
			}
			this.ClearLines();
			Cell currentCell = this.ParentObject.CurrentCell;
			foreach (Location2D location2D in this.Targets)
			{
				IDelayedLineMutation.FillLine(this.TakeLine(), currentCell, currentCell.ParentZone.GetCell(location2D), 80, null, false);
			}
			this.Objects.Clear();
			for (int i = 0; i < this.Lines.Count; i++)
			{
				this.Refract(i, this.Objects, Prospective);
			}
			this.RecalculateSegment = segments;
		}

		// Token: 0x0600C876 RID: 51318 RVA: 0x0048F898 File Offset: 0x0048DA98
		public unsafe void Refract(int Index, List<GameObject> Refractors, bool Prospective = false)
		{
			List<Cell> list = this.Lines[Index];
			for (int i = 1; i < list.Count; i++)
			{
				Cell cell = list[i];
				RefractCosmic refractCosmic;
				if (cell.TryGetFirstObjectPart<RefractCosmic>(out refractCosmic))
				{
					GameObject parentObject = refractCosmic.ParentObject;
					if (GameObject.Validate(parentObject) && !Refractors.Contains(parentObject))
					{
						ReadOnlySpan<float> directions = refractCosmic.GetDirections(list[0].Location, 3);
						if (!directions.IsEmpty)
						{
							Refractors.Add(parentObject);
							list.RemoveRange(i + 1, list.Count - i - 1);
							if (!Prospective)
							{
								cell.PlayWorldSound("sfx_light_refract", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
								IComponent<GameObject>.XDidYToZ(parentObject, "refract", this.Projectile, null, "!", null, null, parentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							}
							int j = 0;
							int length = directions.Length;
							while (j < length)
							{
								List<Cell> list2 = this.TakeLine();
								list2.Add(cell);
								float num = *directions[j];
								float num2 = (float)cell.X;
								float num3 = (float)cell.Y;
								float num4 = Mathf.Sin(num * 0.017453292f);
								float num5 = Mathf.Cos(num * 0.017453292f);
								Cell cell2 = cell;
								do
								{
									num2 += num4;
									num3 += num5;
									Cell cell3 = cell.ParentZone.GetCell((int)num2, (int)num3);
									if (cell3 == null)
									{
										break;
									}
									if (cell3 != cell2)
									{
										list2.Add(cell2 = cell3);
									}
								}
								while (num2 > 0f && num2 < 79f && num3 > 0f && num3 < 24f && list.Count < 400);
								j++;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600C877 RID: 51319 RVA: 0x0048FA65 File Offset: 0x0048DC65
		public void PreloadClips()
		{
			SoundManager.PreloadClipSet("Sounds/Creatures/Ability/sfx_creature_girshNephilim_irisdualBeam_windup");
			SoundManager.PreloadClipSet("Sounds/Creatures/Ability/sfx_creature_girshNephilim_irisdualBeam_attack");
			SoundManager.PreloadClipSet("Sounds/Creatures/Ability/sfx_creature_girshNephilim_irisdualBeam_molting_lp");
			IrisdualBeam.Preloaded = true;
		}

		// Token: 0x0600C878 RID: 51320 RVA: 0x0048FA8B File Offset: 0x0048DC8B
		public void Stop()
		{
			this.Duration = 0;
			this.Delay = 0;
			this.PausePaint = false;
			this.ClearTargets();
			this.ParentObject.RemoveEffect(typeof(IrisdualMolting));
		}

		// Token: 0x0600C879 RID: 51321 RVA: 0x0048FAC0 File Offset: 0x0048DCC0
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandIrisdualBeam" && this.PickTarget())
			{
				this.Duration = this.GetDuration(base.Level);
				this.ParentObject.ApplyEffect(new IrisdualMolting(this.Duration + 1), null);
				CombatJuice.cameraShake(1f, false);
				this.ParentObject.ForfeitTurn(false);
				this.AlertTargetLine();
				this.LockRefractors();
				base.DidX("start", "to shine over the full spectrum", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, true, false, false, null);
				if (this.ParentObject.IsInActiveZone())
				{
					AutoAct.Interrupt(null, null, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C87A RID: 51322 RVA: 0x0048FB81 File Offset: 0x0048DD81
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C87B RID: 51323 RVA: 0x0048FB84 File Offset: 0x0048DD84
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Irisdual Beam", "CommandIrisdualBeam", "Mutation", null, "è", null, false, false, false, true, false, false, false, false, true, false, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C87C RID: 51324 RVA: 0x0048FBCD File Offset: 0x0048DDCD
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C87D RID: 51325 RVA: 0x0048FBE4 File Offset: 0x0048DDE4
		public override void OnPaint(ScreenBuffer SB)
		{
			if (this.PausePaint || !this.AllowPaint || this.Targets.IsNullOrEmpty<Location2D>())
			{
				return;
			}
			this.RecalculateLines(true, false);
			int i = 0;
			int count = this.Lines.Count;
			while (i < count)
			{
				List<Cell> list = this.Lines[i];
				if (list.IsNullOrEmpty<Cell>())
				{
					return;
				}
				int num = (int)(IComponent<GameObject>.frameTimerMS % 500L / (long)(500 / list.Count));
				for (int j = 0; j < list.Count; j++)
				{
					Cell cell = list[j];
					if (cell.IsVisible() && cell != this.ParentObject.CurrentCell && cell.ParentZone == this.ParentObject.CurrentZone)
					{
						ConsoleChar consoleChar = SB[cell];
						if (j == num)
						{
							consoleChar.TileBackground = (consoleChar.Background = The.Color.DarkRed);
							consoleChar.TileForeground = (consoleChar.Detail = The.Color.Red);
						}
						else
						{
							consoleChar.TileBackground = (consoleChar.Background = The.Color.Red);
							consoleChar.TileForeground = (consoleChar.Detail = The.Color.DarkRed);
						}
						consoleChar.SetForeground('r');
					}
				}
				i++;
			}
		}

		// Token: 0x0600C87E RID: 51326 RVA: 0x0048FD4F File Offset: 0x0048DF4F
		public override bool FinalRender(RenderEvent E)
		{
			this.AllowPaint = !this.PausePaint && !this.Targets.IsNullOrEmpty<Location2D>();
			E.WantsToPaint = E.WantsToPaint || this.AllowPaint;
			return true;
		}

		// Token: 0x0600C87F RID: 51327 RVA: 0x0048FD88 File Offset: 0x0048DF88
		public void LockRefractors()
		{
			foreach (GameObject gameObject in this.ParentObject.CurrentZone.IterateObjects())
			{
				if (gameObject.IsCombatObject() && gameObject.HasPart<RefractCosmic>())
				{
					Wait wait = gameObject.Brain.Goals.Peek() as Wait;
					if (wait != null)
					{
						wait.TicksLeft = Math.Max(wait.TicksLeft, this.Duration + 1);
					}
					else
					{
						gameObject.Brain.RemoveGoalsDescendedFrom<IMovementGoal>();
						gameObject.Brain.PushGoal(new Wait(this.Duration + 1, "there's an incoming beam to refract"));
					}
				}
			}
		}

		// Token: 0x0600C880 RID: 51328 RVA: 0x0048FE30 File Offset: 0x0048E030
		public void AlertTargetLine()
		{
			if (this.Targets.IsNullOrEmpty<Location2D>())
			{
				return;
			}
			this.RecalculateLines(true, false);
			int i = 0;
			int count = this.Lines.Count;
			while (i < count)
			{
				int j = 1;
				int count2 = this.Lines[i].Count;
				while (j < count2)
				{
					Cell cell = this.Lines[i][j];
					int k = 0;
					int count3 = cell.Objects.Count;
					while (k < count3)
					{
						GameObject gameObject = cell.Objects[k];
						if (gameObject.IsCombatObject())
						{
							if (gameObject.IsPlayer())
							{
								AutoAct.Interrupt("you are in the path of " + this.ParentObject.poss("irisdual beam", true), cell, null, true);
							}
							else if (gameObject.IsPotentiallyMobile() && !gameObject.HasPart<RefractCosmic>() && !(gameObject.Brain.Goals.Peek() is FleeLocation))
							{
								gameObject.Brain.RemoveGoalsDescendedFrom<IMovementGoal>();
								gameObject.Brain.PushGoal(new FleeLocation(cell, 2, false, false));
							}
						}
						k++;
					}
					j++;
				}
				i++;
			}
		}

		// Token: 0x04004E78 RID: 20088
		public const string ABL_COMMAND = "CommandIrisdualBeam";

		// Token: 0x04004E79 RID: 20089
		public const int SND_MAX_COST = 20;

		// Token: 0x04004E7A RID: 20090
		[NonSerialized]
		public static string[] DamageTypes = new string[] { "Acid", "Electric", "Heat", "Cold", "Poison", "Umbral", "Cosmic" };

		// Token: 0x04004E7B RID: 20091
		public int Duration;

		// Token: 0x04004E7C RID: 20092
		public int Delay;

		// Token: 0x04004E7D RID: 20093
		[NonSerialized]
		private bool PausePaint;

		// Token: 0x04004E7E RID: 20094
		[NonSerialized]
		private bool AllowPaint;

		// Token: 0x04004E7F RID: 20095
		[NonSerialized]
		private List<GameObject> Objects = new List<GameObject>();

		// Token: 0x04004E80 RID: 20096
		[NonSerialized]
		private List<Location2D> Targets = new List<Location2D>();

		// Token: 0x04004E81 RID: 20097
		[NonSerialized]
		private List<List<Cell>> Lines = new List<List<Cell>>();

		// Token: 0x04004E82 RID: 20098
		[NonSerialized]
		private List<string> Threatened = new List<string>();

		// Token: 0x04004E83 RID: 20099
		[NonSerialized]
		private long RecalculateSegment;

		// Token: 0x04004E84 RID: 20100
		[NonSerialized]
		private static bool Preloaded;

		// Token: 0x04004E85 RID: 20101
		[NonSerialized]
		private static GameObject _Projectile;
	}
}
