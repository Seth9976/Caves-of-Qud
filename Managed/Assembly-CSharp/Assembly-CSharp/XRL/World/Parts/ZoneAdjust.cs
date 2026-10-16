using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FBC RID: 4028
	[Serializable]
	public class ZoneAdjust : IPoweredPart
	{
		// Token: 0x0600A017 RID: 40983 RVA: 0x003A43C2 File Offset: 0x003A25C2
		public ZoneAdjust()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A018 RID: 40984 RVA: 0x003A43D8 File Offset: 0x003A25D8
		public bool Affects(GameObject obj)
		{
			return (this.RequiresPart == null || obj.HasPart(this.RequiresPart)) && (this.RequiresEffect == null || obj.HasEffect(this.RequiresEffect)) && (this.RequiresTag == null || obj.HasTagOrProperty(this.RequiresTag)) && (this.RequiresBlueprint == null || this.RequiresBlueprint.CachedCommaExpansion().Contains(obj.Blueprint));
		}

		// Token: 0x0600A019 RID: 40985 RVA: 0x003A4450 File Offset: 0x003A2650
		public override bool SameAs(IPart p)
		{
			ZoneAdjust zoneAdjust = p as ZoneAdjust;
			return !(zoneAdjust.AdjustSpec != this.AdjustSpec) && zoneAdjust.Duration == this.Duration && !(zoneAdjust.VariableDuration != this.VariableDuration) && !(zoneAdjust.RequiresBlueprint != this.RequiresBlueprint) && !(zoneAdjust.RequiresEffect != this.RequiresEffect) && !(zoneAdjust.RequiresPart != this.RequiresPart) && !(zoneAdjust.RequiresTag != this.RequiresTag) && base.SameAs(p);
		}

		// Token: 0x0600A01A RID: 40986 RVA: 0x003A44F9 File Offset: 0x003A26F9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600A01B RID: 40987 RVA: 0x003A4510 File Offset: 0x003A2710
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (!this.AdjustSpec.IsNullOrEmpty() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null && currentCell.ParentZone != null)
				{
					int num = (this.VariableDuration.IsNullOrEmpty() ? this.Duration : Stat.Roll(this.VariableDuration, null));
					Adjusted adjusted = new Adjusted(this.AdjustSpec, num, this.ParentObject);
					foreach (GameObject gameObject in currentCell.ParentZone.GetObjects(new Predicate<GameObject>(this.Affects)))
					{
						bool flag = false;
						foreach (Effect effect in gameObject.Effects)
						{
							Adjusted adjusted2 = effect as Adjusted;
							if (adjusted2 != null && this.ParentObject.IDMatch(adjusted2.SourceID))
							{
								adjusted2.Duration = 0;
								flag = true;
							}
						}
						if (flag)
						{
							gameObject.CleanEffects();
						}
						gameObject.ApplyEffect(new Adjusted(adjusted), null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A01C RID: 40988 RVA: 0x003A4690 File Offset: 0x003A2890
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.AdjustSpec.IsNullOrEmpty();
		}

		// Token: 0x0600A01D RID: 40989 RVA: 0x003A469D File Offset: 0x003A289D
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			if (this.AdjustSpec.IsNullOrEmpty() && !this.NoAdjustSpecFailureDescription.IsNullOrEmpty())
			{
				return this.NoAdjustSpecFailureDescription;
			}
			return null;
		}

		// Token: 0x0400428A RID: 17034
		public string AdjustSpec;

		// Token: 0x0400428B RID: 17035
		public int Duration = 2;

		// Token: 0x0400428C RID: 17036
		public string VariableDuration;

		// Token: 0x0400428D RID: 17037
		public string RequiresBlueprint;

		// Token: 0x0400428E RID: 17038
		public string RequiresEffect;

		// Token: 0x0400428F RID: 17039
		public string RequiresPart;

		// Token: 0x04004290 RID: 17040
		public string RequiresTag;

		// Token: 0x04004291 RID: 17041
		public string NoAdjustSpecFailureDescription;
	}
}
