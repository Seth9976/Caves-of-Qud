using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E38 RID: 3640
	[Serializable]
	public class Distraction : IPart
	{
		// Token: 0x060093C2 RID: 37826 RVA: 0x00353251 File Offset: 0x00351451
		public Distraction()
		{
		}

		// Token: 0x060093C3 RID: 37827 RVA: 0x00353287 File Offset: 0x00351487
		public Distraction(GameObject Original, GameObject Source = null, int SaveTarget = 15)
			: this()
		{
			this.Original = Original;
			this.Source = Source;
			this.SaveTarget = SaveTarget;
		}

		// Token: 0x060093C4 RID: 37828 RVA: 0x003532A4 File Offset: 0x003514A4
		public bool MakeSave(GameObject Object)
		{
			GameObject gameObject = this.Source ?? this.Original;
			if (Object.MakeSave(this.SaveStat, this.SaveTarget, null, null, this.SaveVs, false, false, false, false, gameObject))
			{
				Object.ApplyEffect(new DistractionImmunity
				{
					SourceID = gameObject.BaseID,
					DistractionID = this.ID,
					Duration = Stat.Random(3600, 8400)
				}, null);
				return true;
			}
			return false;
		}

		// Token: 0x060093C5 RID: 37829 RVA: 0x00353320 File Offset: 0x00351520
		public bool Distract(GameObject Object)
		{
			GameObject parentObject = this.ParentObject;
			if (Object.IsInvalid() || Object.IsPlayer() || Object.HasEffect<Distracted>() || Object.Brain == null || Object.Brain.Target == parentObject)
			{
				return false;
			}
			GameObject gameObject = this.Source ?? this.Original;
			if (Object.HasEffect<DistractionImmunity>() && gameObject.HasID)
			{
				foreach (Effect effect in Object.Effects)
				{
					DistractionImmunity distractionImmunity = effect as DistractionImmunity;
					if (distractionImmunity != null && distractionImmunity.SourceID == gameObject.BaseID && distractionImmunity.DistractionID == this.ID)
					{
						return false;
					}
				}
			}
			return CanApplyEffectEvent.Check<Distracted>(Object, 0) && Object.DistanceTo(parentObject) <= Object.Brain.MaxKillRadius && !this.MakeSave(Object) && Object.ApplyEffect(new Distracted(this), null);
		}

		// Token: 0x060093C6 RID: 37830 RVA: 0x00353428 File Offset: 0x00351628
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060093C7 RID: 37831 RVA: 0x0035342C File Offset: 0x0035162C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (GameObject.Validate(this.ParentObject) && GameObject.Validate(ref this.Original))
			{
				foreach (GameObject gameObject in this.ParentObject.CurrentZone.FastFloodVisibility(this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y, this.Radius, typeof(Brain), this.ParentObject, null, null))
				{
					if (gameObject.Brain != null && gameObject.Brain.Target == this.Original)
					{
						this.Distract(gameObject);
					}
				}
			}
		}

		// Token: 0x04003D20 RID: 15648
		public int ID;

		// Token: 0x04003D21 RID: 15649
		public GameObject Original;

		// Token: 0x04003D22 RID: 15650
		public string SaveStat = "Intelligence";

		// Token: 0x04003D23 RID: 15651
		public string SaveVs = "Hologram Illusion Distraction";

		// Token: 0x04003D24 RID: 15652
		public int SaveTarget = 15;

		// Token: 0x04003D25 RID: 15653
		public int SaveTurns = 10;

		// Token: 0x04003D26 RID: 15654
		public int Radius = 12;

		// Token: 0x04003D27 RID: 15655
		public GameObject Source;
	}
}
