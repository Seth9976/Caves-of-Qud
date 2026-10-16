using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001420 RID: 5152
	[Serializable]
	public class MagneticPulse : BaseMutation
	{
		// Token: 0x0600C88B RID: 51339 RVA: 0x00490238 File Offset: 0x0048E438
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C88C RID: 51340 RVA: 0x0049023B File Offset: 0x0048E43B
		public override void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Radius", base.Level, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.Cooldown, 0);
		}

		// Token: 0x0600C88D RID: 51341 RVA: 0x0049026A File Offset: 0x0048E46A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C88E RID: 51342 RVA: 0x00490298 File Offset: 0x0048E498
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance > 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add(MagneticPulse.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C88F RID: 51343 RVA: 0x004902CA File Offset: 0x0048E4CA
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (this.PulseCharging == 0 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				CommandEvent.Send(E.Actor, MagneticPulse.COMMAND_NAME, null, null, 0, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C890 RID: 51344 RVA: 0x00490304 File Offset: 0x0048E504
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.PulseCharging > 0)
			{
				this.PulseCharging++;
				this.ParentObject.UseEnergy(1000, "Physical Mutation", null, null, false);
				if (this.PulseCharging >= 2)
				{
					MagneticPulse.EmitMagneticPulse(this.ParentObject, this.GetRadius(base.Level));
					this.PulseCharging = 0;
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C891 RID: 51345 RVA: 0x0049037C File Offset: 0x0048E57C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == MagneticPulse.COMMAND_NAME)
			{
				this.PulseCharging = 1;
				this.ParentObject.UseEnergy(1000, "Physical Mutation", null, null, false);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.Cooldown, null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C892 RID: 51346 RVA: 0x004903E0 File Offset: 0x0048E5E0
		public override string GetDescription()
		{
			return "You emit powerful magnetic pulses.";
		}

		// Token: 0x0600C893 RID: 51347 RVA: 0x004903E7 File Offset: 0x0048E5E7
		public override string GetLevelText(int Level)
		{
			return "After a one-round warmup, you emit a pulse with radius " + Level.ToString() + " that attempts to pull metal objects toward you, including metal gear equipped on creatures.\n" + "Cooldown: " + this.Cooldown.Things("round", null) + "\n";
		}

		// Token: 0x0600C894 RID: 51348 RVA: 0x00490420 File Offset: 0x0048E620
		public override bool Render(RenderEvent E)
		{
			if (this.PulseCharging == 1)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 35 && num < 45)
				{
					E.Tile = null;
					E.RenderString = ">";
					E.ColorString = "&C";
				}
			}
			return base.Render(E);
		}

		// Token: 0x0600C895 RID: 51349 RVA: 0x0049046D File Offset: 0x0048E66D
		public static bool CanManipulate(GameObject Object)
		{
			return !Object.IsNatural() && CanBeMagneticallyManipulatedEvent.Check(Object);
		}

		// Token: 0x0600C896 RID: 51350 RVA: 0x0049047F File Offset: 0x0048E67F
		private static int PullResistance(GameObject obj)
		{
			if (obj.HasStat("Strength"))
			{
				return obj.Stat("Strength", 0);
			}
			return (int)Math.Round((double)obj.GetKineticResistance() * 0.1, MidpointRounding.AwayFromZero);
		}

		// Token: 0x0600C897 RID: 51351 RVA: 0x004904B3 File Offset: 0x0048E6B3
		private static int PullStrengthInstance(int Radius)
		{
			return Radius * 10 - Stat.Random(1, 20);
		}

		// Token: 0x0600C898 RID: 51352 RVA: 0x004904C2 File Offset: 0x0048E6C2
		private static int PullSquares(int PullStrength, int PullResistance)
		{
			return (PullStrength - PullResistance) / 5;
		}

		// Token: 0x0600C899 RID: 51353 RVA: 0x004904CC File Offset: 0x0048E6CC
		public static void EmitMagneticPulse(GameObject Actor, int Radius)
		{
			if (!GameObject.Validate(Actor))
			{
				return;
			}
			Cell currentCell = Actor.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			GameObject actor = Actor;
			if (actor != null)
			{
				actor.PlayWorldSound("Sounds/Abilities/sfx_ability_magnetic_pulse", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			IComponent<GameObject>.XDidY(Actor, "emit", "a powerful magnetic pulse", "!", null, null, Actor, null, false, false, null, null, false, false, false, false, false, null);
			if (IComponent<GameObject>.Visible(Actor))
			{
				for (int i = Radius; i > 0; i -= 2)
				{
					float num = 1f;
					for (int j = 0; j < 360; j++)
					{
						float num2 = (float)Math.Sin((double)((float)j) * 0.017) / num;
						float num3 = (float)Math.Cos((double)((float)j) * 0.017) / num;
						float num4 = (float)i / num;
						The.ParticleManager.Add("@", (float)currentCell.X + num2 * num4, (float)currentCell.Y + num3 * num4, -num2, -num3, (int)num4);
					}
				}
			}
			List<Cell> realAdjacentCells = currentCell.GetRealAdjacentCells(Radius, true);
			List<Tuple<GameObject, int>> list = new List<Tuple<GameObject, int>>(Radius + 2);
			int num5 = 0;
			foreach (Cell cell in realAdjacentCells)
			{
				foreach (GameObject gameObject in cell.Objects)
				{
					if (MagneticPulse.CanManipulate(gameObject) || gameObject.AnyInstalledCybernetics(new Predicate<GameObject>(MagneticPulse.CanManipulate)))
					{
						list.Add(new Tuple<GameObject, int>(gameObject, -1));
					}
				}
				num5 += cell.Objects.Count;
			}
			List<GameObject> list2 = new List<GameObject>(num5);
			foreach (Cell cell2 in realAdjacentCells)
			{
				list2.AddRange(cell2.Objects);
			}
			list2.Sort((GameObject a, GameObject b) => a.DistanceTo(Actor).CompareTo(b.DistanceTo(Actor)));
			using (List<GameObject>.Enumerator enumerator3 = list2.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					GameObject affectedObject = enumerator3.Current;
					List<Tuple<Cell, char>> lineTo = affectedObject.GetLineTo(Actor, true, false);
					GameObject randomElement = (from o in affectedObject.GetInventoryAndEquipment()
						where MagneticPulse.CanManipulate(o) && o.Implantee == null
						select o).GetRandomElement<GameObject>(null);
					GameObject gameObject2 = null;
					if (randomElement != null)
					{
						int num6 = MagneticPulse.PullStrengthInstance(Radius);
						int num7 = MagneticPulse.PullResistance(randomElement);
						int num8 = MagneticPulse.PullSquares(num6, num7);
						if (num8 > 0)
						{
							if (randomElement.Equipped != null)
							{
								BodyPart bodyPart = randomElement.EquippedOn();
								if (((bodyPart != null) ? bodyPart.Type : null) == "Body" || !randomElement.UnequipAndRemove())
								{
									if (!list.Any<Tuple<GameObject, int>>((Tuple<GameObject, int> e) => e.Item1 == affectedObject))
									{
										list.Add(new Tuple<GameObject, int>(affectedObject, num8));
										goto IL_03F4;
									}
									goto IL_03F4;
								}
							}
							if (affectedObject.IsPlayer())
							{
								gameObject2 = randomElement;
							}
							randomElement.RemoveFromContext(null);
							affectedObject.CurrentCell.AddObject(randomElement, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							Cell cell3 = null;
							int num9 = 1;
							while (num9 <= num8 && num9 < lineTo.Count - 1 && !lineTo[num9].Item1.IsSolid())
							{
								if (!randomElement.IsCombat || lineTo[num9].Item1.IsEmpty())
								{
									cell3 = lineTo[num9].Item1;
								}
								num9++;
							}
							if (cell3 != null)
							{
								randomElement.DirectMoveTo(cell3, 0, false, false, false, null);
							}
						}
					}
					IL_03F4:
					if (gameObject2 != null)
					{
						if (affectedObject.IsPlayer())
						{
							if (AutoAct.IsInterruptable())
							{
								AutoAct.Interrupt(gameObject2.does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " been ripped from your body", null, IComponent<GameObject>.Visible(Actor) ? Actor : affectedObject, true);
							}
							else
							{
								Popup.Show(gameObject2.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " ripped from your body!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
						else if (affectedObject.IsPlayerLed() && !affectedObject.IsTrifling && IComponent<GameObject>.Visible(affectedObject))
						{
							if (AutoAct.IsInterruptable())
							{
								AutoAct.Interrupt(string.Concat(new string[]
								{
									"your companion, ",
									affectedObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, true, false, null, false, false, false, false, null, false, null, false, true),
									",",
									affectedObject.GetVerb("have", true, false, false, true, false),
									" had ",
									gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
									" ripped from ",
									affectedObject.its,
									" body"
								}), null, IComponent<GameObject>.Visible(Actor) ? Actor : affectedObject, true);
							}
							else
							{
								Popup.Show(string.Concat(new string[]
								{
									"Your companion, ",
									affectedObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, true, false, null, false, false, false, false, null, false, null, false, true),
									",",
									affectedObject.GetVerb("have", true, false, false, true, false),
									" had ",
									gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
									" ripped from ",
									affectedObject.its,
									" body!"
								}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
					}
				}
			}
			list.Sort((Tuple<GameObject, int> a, Tuple<GameObject, int> b) => a.Item1.DistanceTo(Actor).CompareTo(b.Item1.DistanceTo(Actor)));
			foreach (Tuple<GameObject, int> tuple in list)
			{
				int num12;
				if (tuple.Item2 < 0)
				{
					int num10 = MagneticPulse.PullStrengthInstance(Radius);
					int num11 = MagneticPulse.PullResistance(tuple.Item1);
					num12 = MagneticPulse.PullSquares(num10, num11);
				}
				else
				{
					num12 = tuple.Item2;
				}
				if (num12 > 0)
				{
					List<Tuple<Cell, char>> lineTo2 = tuple.Item1.GetLineTo(Actor, true, false);
					Cell cell4 = null;
					int num13 = 1;
					while (num13 <= num12 && num13 < lineTo2.Count - 1 && lineTo2[num13].Item1.IsEmptyOfSolidFor(tuple.Item1, tuple.Item1.IsCombatObject()))
					{
						cell4 = lineTo2[num13].Item1;
						num13++;
					}
					if (cell4 != null && tuple.Item1.DirectMoveTo(cell4, 0, false, false, false, null) && IComponent<GameObject>.Visible(tuple.Item1))
					{
						if (IComponent<GameObject>.Visible(Actor))
						{
							IComponent<GameObject>.AddPlayerMessage(tuple.Item1.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " pulled toward " + Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
						}
						else
						{
							IComponent<GameObject>.AddPlayerMessage(tuple.Item1.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " pulled toward something.", null, true);
						}
					}
				}
			}
		}

		// Token: 0x0600C89A RID: 51354 RVA: 0x00490E04 File Offset: 0x0048F004
		public int GetRadius(int Level)
		{
			return 1 + Level;
		}

		// Token: 0x0600C89B RID: 51355 RVA: 0x00490E09 File Offset: 0x0048F009
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C89C RID: 51356 RVA: 0x00490E14 File Offset: 0x0048F014
		private void AddAbility(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Magnetic Pulse", MagneticPulse.COMMAND_NAME, "Physical Mutations", null, "æ", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x0600C89D RID: 51357 RVA: 0x00490E55 File Offset: 0x0048F055
		public override bool Mutate(GameObject GO, int Level)
		{
			this.AddAbility(GO);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C89E RID: 51358 RVA: 0x00490E66 File Offset: 0x0048F066
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E8F RID: 20111
		public static readonly string COMMAND_NAME = "CommandMagneticPulse";

		// Token: 0x04004E90 RID: 20112
		public int Cooldown = 100;

		// Token: 0x04004E91 RID: 20113
		public int PulseCharging;
	}
}
