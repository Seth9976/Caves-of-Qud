using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010C5 RID: 4293
	[Serializable]
	public class BandageMedication : IPart
	{
		// Token: 0x0600AAE1 RID: 43745 RVA: 0x003E7E29 File Offset: 0x003E6029
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600AAE2 RID: 43746 RVA: 0x003E7E2C File Offset: 0x003E602C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == AIGetPassiveItemListEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600AAE3 RID: 43747 RVA: 0x003E7E52 File Offset: 0x003E6052
		public override bool HandleEvent(InventoryActionEvent E)
		{
			return (!(E.Command == "Apply") || this.PerformBandaging(E.Actor, E.ObjectTarget)) && base.HandleEvent(E);
		}

		// Token: 0x0600AAE4 RID: 43748 RVA: 0x003E7E83 File Offset: 0x003E6083
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			this.AIBandageUsage(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAE5 RID: 43749 RVA: 0x003E7E93 File Offset: 0x003E6093
		public override bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			this.AIBandageUsage(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAE6 RID: 43750 RVA: 0x003E7EA4 File Offset: 0x003E60A4
		public bool PerformBandaging(GameObject Actor, GameObject Target = null)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			if (!Actor.CanMoveExtremities(null, true, false, true))
			{
				return false;
			}
			if (Target == null)
			{
				if (Actor.IsPlayer() && BandageMedication.AnyBleedingNear(Actor))
				{
					Cell cell = Actor.Physics.PickDirection("Bandage whom?", null);
					if (cell == null)
					{
						return false;
					}
					Target = ((cell == Actor.CurrentCell) ? Actor : cell.GetCombatTarget(Actor, false, true, true, 0, null, null, null, null, null, false, false, null));
					if (Target == null)
					{
						Target = cell.GetCombatTarget(Actor, true, true, true, 0, null, null, null, null, null, false, false, null);
						if (Target != null)
						{
							Actor.Fail(string.Concat(new string[]
							{
								"You cannot reach ",
								Target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" to bandage ",
								Target.its,
								" wounds."
							}));
						}
						else
						{
							Actor.Fail("There's no one there.");
						}
						return false;
					}
				}
				else
				{
					Target = Actor;
				}
			}
			if (Target != Actor && Target.IsHostileTowards(Actor) && Target.CanMoveExtremities(null, false, false, false))
			{
				Actor.Fail(Target.Does("won't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " let you bandage " + Target.them + ".");
				return false;
			}
			bool flag;
			bool flag2;
			Bleeding bleeding = BandageMedication.FindBandageableWound(Target, out flag, out flag2);
			if (bleeding == null)
			{
				if (flag && flag2)
				{
					Actor.Fail("All of " + Target.poss("wounds", true) + " that can be staunched have been already.");
				}
				else if (flag)
				{
					Actor.Fail(Target.Poss("wounds", true) + " have been bandaged.");
				}
				else if (flag2)
				{
					Actor.Fail(Target.Poss("wounds", true) + " are too deep to bandage.");
				}
				else
				{
					Actor.Fail(Target.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not bleeding.");
				}
				return false;
			}
			int num = this.Performance.RollCached();
			this.ParentObject.SplitFromStack();
			int @for = GetBandagePerformanceEvent.GetFor(this.ParentObject, Actor, Target, num);
			bool flag3 = false;
			if (this.ParentObject.HasPropertyOrTag("MessageAsBandage"))
			{
				if (Actor != Target && !Actor.PhaseMatches(Target))
				{
					IComponent<GameObject>.XDidYToZ(Actor, "try", "to bandage", Target, "wounds, but " + this.ParentObject.does("pass", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " through " + Target.them, null, null, null, null, null, false, false, false, false, true, null, null, null, false, false, false, true, false, null);
				}
				else if (Actor != Target && Target.IsInStasis())
				{
					IComponent<GameObject>.XDidYToZ(Actor, "try", "to bandage", Target, "wounds, but cannot affect " + Target.them, null, null, null, null, null, false, false, false, false, true, null, null, null, false, false, false, true, false, null);
				}
				else
				{
					IComponent<GameObject>.XDidYToZ(Actor, "bandage", Target, "wounds", null, null, null, null, null, false, false, false, false, true, null, null, null, false, false, false, false, false, null);
					flag3 = true;
				}
			}
			else if (Actor != Target && !Actor.PhaseMatches(Target))
			{
				IComponent<GameObject>.WDidXToYWithZ(Actor, "try", "to staunch", Target, "wounds with", this.ParentObject, ", but " + this.ParentObject.does("pass", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " through " + Target.them, null, null, null, null, null, false, false, false, true, false, false, true, false, null, null, null, null, false, false, false, true, false, null);
			}
			else if (Actor != Target && Target.IsInStasis())
			{
				IComponent<GameObject>.WDidXToYWithZ(Actor, "try", "to staunch", Target, "wounds with", this.ParentObject, ", but cannot affect " + Target.them, null, null, null, null, null, false, false, false, true, false, false, true, false, null, null, null, null, false, false, false, true, false, null);
			}
			else
			{
				IComponent<GameObject>.WDidXToYWithZ(Actor, "staunch", Target, "wounds with", this.ParentObject, null, null, null, null, null, null, false, false, false, true, false, false, true, false, null, null, null, null, false, false, false, true, false, null);
				flag3 = true;
			}
			if (flag3)
			{
				Actor.PlayWorldOrUISound("Sounds/Interact/sfx_interact_bandage_apply", null);
				bleeding.Bandaged = true;
				bool flag4 = false;
				if (!bleeding.StopMessageUsePopup && (Actor.IsPlayer() || Target.IsPlayer()))
				{
					bleeding.StopMessageUsePopup = false;
					flag4 = false;
				}
				if (!bleeding.RecoveryChance(@for, true) && flag4)
				{
					bleeding.StopMessageUsePopup = false;
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			else
			{
				this.ParentObject.CheckStack();
			}
			Actor.UseEnergy(1000, "Physical Item Bandage", null, null, false);
			return true;
		}

		// Token: 0x0600AAE7 RID: 43751 RVA: 0x003E83B0 File Offset: 0x003E65B0
		public static bool AnyBleedingIn(Cell Cell)
		{
			if (Cell != null)
			{
				int i = 0;
				int count = Cell.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = Cell.Objects[i];
					if (gameObject._Effects != null)
					{
						int j = 0;
						int count2 = gameObject.Effects.Count;
						while (j < count2)
						{
							if (gameObject.Effects[j] is Bleeding)
							{
								return true;
							}
							j++;
						}
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600AAE8 RID: 43752 RVA: 0x003E8420 File Offset: 0x003E6620
		public static bool AnyBleedingNear(Cell Cell)
		{
			if (Cell != null)
			{
				using (List<Cell>.Enumerator enumerator = Cell.GetLocalAdjacentCells().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (BandageMedication.AnyBleedingIn(enumerator.Current))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600AAE9 RID: 43753 RVA: 0x003E847C File Offset: 0x003E667C
		public static bool AnyBleedingNear(GameObject Object)
		{
			return BandageMedication.AnyBleedingNear(Object.CurrentCell);
		}

		// Token: 0x0600AAEA RID: 43754 RVA: 0x003E848C File Offset: 0x003E668C
		public static Bleeding FindBandageableWound(GameObject Subject, out bool HasBandaged, out bool HasUntreatable)
		{
			HasBandaged = false;
			HasUntreatable = false;
			Bleeding bleeding = null;
			if (Subject._Effects != null)
			{
				int i = 0;
				int count = Subject.Effects.Count;
				while (i < count)
				{
					Bleeding bleeding2 = Subject.Effects[i] as Bleeding;
					if (bleeding2 != null)
					{
						if (bleeding2.Bandaged)
						{
							HasBandaged = true;
						}
						else if (bleeding2.Internal)
						{
							HasUntreatable = true;
						}
						else if (bleeding == null || bleeding2.Damage.GetCachedDieRoll().Average() > bleeding.Damage.GetCachedDieRoll().Average())
						{
							bleeding = bleeding2;
						}
					}
					i++;
				}
			}
			return bleeding;
		}

		// Token: 0x0600AAEB RID: 43755 RVA: 0x003E8518 File Offset: 0x003E6718
		public static bool HasBandageableWound(GameObject Subject)
		{
			if (Subject._Effects != null)
			{
				int i = 0;
				int count = Subject.Effects.Count;
				while (i < count)
				{
					Bleeding bleeding = Subject.Effects[i] as Bleeding;
					if (bleeding != null && !bleeding.Bandaged && !bleeding.Internal)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600AAEC RID: 43756 RVA: 0x003E8570 File Offset: 0x003E6770
		public static GameObject FindCreatureWithBandageableWound(GameObject Actor, Cell Cell)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return null;
			}
			int i = 0;
			int count = Cell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = Cell.Objects[i];
				if (gameObject != Actor && gameObject.IsCreature && BandageMedication.HasBandageableWound(gameObject) && (Actor.IsLedBy(gameObject) || gameObject.IsLedBy(Actor) || Actor.IsAlliedTowards(gameObject) || gameObject.IsAlliedTowards(Actor)) && gameObject.PhaseMatches(Actor) && !gameObject.IsInStasis() && (FungalVisionary.VisionLevel > 0 || !gameObject.HasPart<FungalVision>()))
				{
					return gameObject;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600AAED RID: 43757 RVA: 0x003E860C File Offset: 0x003E680C
		public void AIBandageUsage(IAICommandListEvent E)
		{
			bool flag = false;
			if (BandageMedication.HasBandageableWound(E.Actor))
			{
				if (!flag && !E.Actor.CanMoveExtremities(null, false, false, true))
				{
					return;
				}
				flag = true;
				E.Add("Apply", 2, this.ParentObject, true, false, null, null);
			}
			Cell currentCell = E.Actor.CurrentCell;
			if (currentCell != null)
			{
				GameObject gameObject = BandageMedication.FindCreatureWithBandageableWound(E.Actor, currentCell);
				if (gameObject != null)
				{
					if (!flag && !E.Actor.CanMoveExtremities(null, false, false, true))
					{
						return;
					}
					flag = true;
					string text = "Apply";
					GameObject gameObject2 = this.ParentObject;
					E.Add(text, E.Actor.IsInLoveWith(gameObject) ? 10 : 1, gameObject2, true, false, gameObject, null);
				}
				foreach (Cell cell in currentCell.GetLocalAdjacentCells())
				{
					GameObject gameObject3 = BandageMedication.FindCreatureWithBandageableWound(E.Actor, cell);
					if (gameObject3 != null)
					{
						if (!flag && !E.Actor.CanMoveExtremities(null, false, false, true))
						{
							break;
						}
						flag = true;
						string text2 = "Apply";
						GameObject gameObject2 = this.ParentObject;
						E.Add(text2, E.Actor.IsInLoveWith(gameObject3) ? 10 : 1, gameObject2, true, false, gameObject3, null);
					}
				}
			}
		}

		// Token: 0x04004675 RID: 18037
		public string Performance = "1d4+4";
	}
}
