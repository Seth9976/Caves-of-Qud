using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F7 RID: 5111
	[Serializable]
	public class SensePsychic : BaseMutation
	{
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x0600C61F RID: 50719 RVA: 0x004826CC File Offset: 0x004808CC
		public int Radius
		{
			get
			{
				if (!this.Levelable)
				{
					return this.BaseRadius;
				}
				return this.BaseRadius + base.Level;
			}
		}

		// Token: 0x0600C620 RID: 50720 RVA: 0x004826EC File Offset: 0x004808EC
		public SensePsychic()
		{
			this.IsOursPredicate = new Predicate<Effect>(this.IsOurs);
			this.IsOtherPsychicPredicate = new Predicate<GameObject>(this.IsOtherPsychic);
			this.IsOtherReachablePsychicPredicate = new Predicate<GameObject>(this.IsOtherReachablePsychic);
		}

		// Token: 0x0600C621 RID: 50721 RVA: 0x0048273D File Offset: 0x0048093D
		public override bool CanLevel()
		{
			return this.Levelable;
		}

		// Token: 0x0600C622 RID: 50722 RVA: 0x00482745 File Offset: 0x00480945
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C623 RID: 50723 RVA: 0x00482748 File Offset: 0x00480948
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.SensePsychics();
		}

		// Token: 0x0600C624 RID: 50724 RVA: 0x00482750 File Offset: 0x00480950
		public override string GetDescription()
		{
			return "" + "You can sense other mental mutants through the psychic aether.\n\n" + "You detect the presence of psychic enemies within a radius of " + this.Radius.ToString() + ".\nThere's a chance you identify detected enemies.";
		}

		// Token: 0x0600C625 RID: 50725 RVA: 0x00482789 File Offset: 0x00480989
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C626 RID: 50726 RVA: 0x00482790 File Offset: 0x00480990
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C627 RID: 50727 RVA: 0x00482799 File Offset: 0x00480999
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C628 RID: 50728 RVA: 0x004827A3 File Offset: 0x004809A3
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x0600C629 RID: 50729 RVA: 0x004827AC File Offset: 0x004809AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<ExtraHostilePerceptionEvent>.ID;
		}

		// Token: 0x0600C62A RID: 50730 RVA: 0x004827C4 File Offset: 0x004809C4
		public override bool HandleEvent(ExtraHostilePerceptionEvent E)
		{
			if (E.Actor == this.ParentObject && this.ParentObject.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					GameObject[] array = The.ActionManager.ActionQueue.GetArray();
					int i = 0;
					int num = array.Length;
					while (i < num)
					{
						GameObject gameObject = array[i];
						if (gameObject.IsValid() && gameObject.CurrentZone == currentCell.ParentZone && gameObject.DistanceTo(currentCell) <= this.Radius)
						{
							SensePsychicEffect sensePsychicEffect = gameObject.GetEffect(typeof(SensePsychicEffect), this.IsOursPredicate) as SensePsychicEffect;
							if (sensePsychicEffect != null && sensePsychicEffect.Identified && this.ParentObject.IsRelevantHostile(gameObject))
							{
								E.Hostile = gameObject;
								E.PerceiveVerb = "sense";
								break;
							}
						}
						i++;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C62B RID: 50731 RVA: 0x004828B0 File Offset: 0x00480AB0
		private static bool SensePsychicListenerIsPlayer(Effect FX)
		{
			GameObject listener = (FX as SensePsychicEffect).Listener;
			return listener != null && listener.IsPlayer();
		}

		// Token: 0x0600C62C RID: 50732 RVA: 0x004828D4 File Offset: 0x00480AD4
		public static SensePsychicEffect SensePsychicFromPlayer(GameObject GO)
		{
			return GO.GetEffect(typeof(SensePsychicEffect), new Predicate<Effect>(SensePsychic.SensePsychicListenerIsPlayer)) as SensePsychicEffect;
		}

		// Token: 0x0600C62D RID: 50733 RVA: 0x004828F7 File Offset: 0x00480AF7
		private bool IsOurs(Effect FX)
		{
			return (FX as SensePsychicEffect).Listener == this.ParentObject;
		}

		// Token: 0x0600C62E RID: 50734 RVA: 0x0048290C File Offset: 0x00480B0C
		public bool IsOtherPsychic(GameObject obj)
		{
			return obj != this.ParentObject && IsSensableAsPsychicEvent.Check(obj);
		}

		// Token: 0x0600C62F RID: 50735 RVA: 0x0048291F File Offset: 0x00480B1F
		public bool IsOtherReachablePsychic(GameObject obj)
		{
			return this.IsOtherPsychic(obj) && IComponent<GameObject>.CheckRealityDistortionAccessibility(obj, null, this.ParentObject, null, this, 0);
		}

		// Token: 0x0600C630 RID: 50736 RVA: 0x0048293C File Offset: 0x00480B3C
		public void SensePsychics()
		{
			if (!this.ParentObject.IsPlayer())
			{
				return;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			if (currentCell.OnWorldMap())
			{
				return;
			}
			List<GameObject> list;
			if (this.RealityDistortionBased)
			{
				if (base.CheckMyRealityDistortionUsability(null, null, null, null, null, null, 0))
				{
					list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, this.Radius, this.IsOtherReachablePsychicPredicate, true);
				}
				else
				{
					list = null;
				}
			}
			else
			{
				list = currentCell.ParentZone.FastSquareSearch(currentCell.X, currentCell.Y, this.Radius, this.IsOtherPsychicPredicate, true);
			}
			if (list != null)
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					GameObject gameObject = list[i];
					if (!gameObject.HasEffect(typeof(SensePsychicEffect), new Predicate<Effect>(this.IsOurs)))
					{
						gameObject.ApplyEffect(new SensePsychicEffect(this.Levelable ? base.Level : 1, this.ParentObject), null);
					}
					i++;
				}
			}
		}

		// Token: 0x04004E2A RID: 20010
		public int BaseRadius = 9;

		// Token: 0x04004E2B RID: 20011
		public bool Levelable;

		// Token: 0x04004E2C RID: 20012
		public bool RealityDistortionBased;

		// Token: 0x04004E2D RID: 20013
		[NonSerialized]
		private Predicate<Effect> IsOursPredicate;

		// Token: 0x04004E2E RID: 20014
		[NonSerialized]
		private Predicate<GameObject> IsOtherPsychicPredicate;

		// Token: 0x04004E2F RID: 20015
		[NonSerialized]
		private Predicate<GameObject> IsOtherReachablePsychicPredicate;
	}
}
