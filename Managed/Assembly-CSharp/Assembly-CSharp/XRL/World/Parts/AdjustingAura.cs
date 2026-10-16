using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FF2 RID: 4082
	[Serializable]
	public class AdjustingAura : IActivePart
	{
		// Token: 0x0600A2BA RID: 41658 RVA: 0x003B735A File Offset: 0x003B555A
		public AdjustingAura()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A2BB RID: 41659 RVA: 0x003B7390 File Offset: 0x003B5590
		public AdjustingAura(string Spec)
			: this()
		{
			this.WorksOnSelf = true;
			this.AdjustSpec = Spec;
		}

		// Token: 0x0600A2BC RID: 41660 RVA: 0x003B73A6 File Offset: 0x003B55A6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A2BD RID: 41661 RVA: 0x003B73C4 File Offset: 0x003B55C4
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (!this.AdjustSpec.IsNullOrEmpty() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				int refreshDelay = this.RefreshDelay;
				this.RefreshDelay = refreshDelay - 1;
				if (refreshDelay <= 0)
				{
					this.RefreshTargets(activePartFirstSubject);
					this.RefreshDelay = this.TargetingInterval;
				}
				this.RefreshEffects(activePartFirstSubject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2BE RID: 41662 RVA: 0x003B7444 File Offset: 0x003B5644
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.WorksOnSelf)
			{
				if (this.AdjustDetails == null)
				{
					Adjusted adjusted = new Adjusted(this.AdjustSpec, this.Duration, this.ParentObject);
					adjusted.ApplySpec();
					this.AdjustDetails = Grammar.MakeAndList(adjusted.GetDetails().Split('\n', StringSplitOptions.None), true);
				}
				E.Postfix.Compound("{{rules|", '\n').Append(this.AdjustDetails).Append(" to ");
				if (this.AdjustSubject)
				{
					E.Postfix.Append("you and ");
				}
				E.Postfix.Append("all your followers");
				if (this.Radius < 80)
				{
					E.Postfix.Append(" within a radius of ").Append(this.Radius);
				}
				E.Postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2BF RID: 41663 RVA: 0x003B752C File Offset: 0x003B572C
		public void RefreshTargets(GameObject Subject)
		{
			Cell currentCell = Subject.CurrentCell;
			if (currentCell == null || !currentCell.ParentZone.IsActive())
			{
				return;
			}
			if (this.AdjustSubject)
			{
				this.RefreshTarget(Subject, Subject, true);
			}
			if (this.Radius >= currentCell.ParentZone.Width)
			{
				foreach (GameObject gameObject in currentCell.ParentZone.IterateObjects())
				{
					this.RefreshTarget(Subject, gameObject, false);
				}
				return;
			}
			foreach (Cell cell in currentCell.IterateAdjacent(this.Radius, false, false, true))
			{
				foreach (GameObject gameObject2 in cell.Objects)
				{
					this.RefreshTarget(Subject, gameObject2, false);
				}
			}
		}

		// Token: 0x0600A2C0 RID: 41664 RVA: 0x003B761C File Offset: 0x003B581C
		public void RefreshTarget(GameObject Subject, GameObject Target, bool RefreshSubject = false)
		{
			if (!Target.IsCombatObject())
			{
				return;
			}
			if (Subject == Target)
			{
				if (!RefreshSubject)
				{
					return;
				}
			}
			else if (this.RequiresParty)
			{
				GameObject partyLeader = Target.PartyLeader;
				if (partyLeader == null)
				{
					if (!this.AdjustLeader || !Subject.IsLedBy(Target))
					{
						return;
					}
				}
				else if (partyLeader != Subject && partyLeader != Subject.PartyLeader)
				{
					return;
				}
			}
			else if (!Target.IsAlliedTowards(Subject))
			{
				return;
			}
			Effect effect = Target.GetEffect(new Predicate<Effect>(this.IsOurEffect));
			if (effect == null)
			{
				this.Effects.Add(effect = new Adjusted(this.AdjustSpec, this.Duration, this.ParentObject));
				Target.ApplyEffect(effect, null);
				int num = Mathf.CeilToInt((float)Target.Speed / 100f - 1f);
				if (num > 0)
				{
					effect.Duration += num;
					return;
				}
			}
			else if (!this.Effects.Contains(effect))
			{
				this.Effects.Add(effect);
			}
		}

		// Token: 0x0600A2C1 RID: 41665 RVA: 0x003B7700 File Offset: 0x003B5900
		public bool IsOurEffect(Effect FX)
		{
			Adjusted adjusted = FX as Adjusted;
			return adjusted != null && adjusted.SourceID == this.ParentObject.ID;
		}

		// Token: 0x0600A2C2 RID: 41666 RVA: 0x003B7730 File Offset: 0x003B5930
		public void RefreshEffects(GameObject Subject)
		{
			for (int i = this.Effects.Count - 1; i >= 0; i--)
			{
				Effect effect = this.Effects[i];
				if (effect.Object == null || !effect.Object.IsValid())
				{
					this.Effects.RemoveAt(i);
				}
				else if (effect.Object.DistanceTo(Subject) <= this.Radius)
				{
					effect.Duration = this.Duration;
					int num = Mathf.CeilToInt((float)effect.Object.Speed / 100f - 1f);
					if (num > 0)
					{
						effect.Duration += num;
					}
				}
			}
		}

		// Token: 0x04004369 RID: 17257
		public string AdjustSpec;

		// Token: 0x0400436A RID: 17258
		public int Duration = 3;

		// Token: 0x0400436B RID: 17259
		public int Radius = 4;

		// Token: 0x0400436C RID: 17260
		public int TargetingInterval = 5;

		// Token: 0x0400436D RID: 17261
		public bool RequiresParty = true;

		// Token: 0x0400436E RID: 17262
		public bool AdjustSubject;

		// Token: 0x0400436F RID: 17263
		public bool AdjustLeader;

		// Token: 0x04004370 RID: 17264
		[NonSerialized]
		private string AdjustDetails;

		// Token: 0x04004371 RID: 17265
		[NonSerialized]
		private List<Effect> Effects = new List<Effect>();

		// Token: 0x04004372 RID: 17266
		[NonSerialized]
		private int RefreshDelay;
	}
}
