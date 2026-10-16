using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001123 RID: 4387
	[Serializable]
	public class TreatAsSolid : IPart
	{
		// Token: 0x0600AD92 RID: 44434 RVA: 0x0040408C File Offset: 0x0040228C
		private bool MatchInner(Cell Cell, GameObject Attacker, out GameObject ObjectHit, out bool RecheckHit, out bool RecheckPhase, GameObject Launcher = null, GameObject ApparentTarget = null, GameObject PhaseFrom = null, bool PenetrateCreatures = false, bool PenetrateWalls = false, bool Prospective = false)
		{
			ObjectHit = null;
			RecheckHit = false;
			RecheckPhase = false;
			if (Cell == null)
			{
				return false;
			}
			int num = 0;
			for (;;)
			{
				IL_0012:
				if ((this.GazeBased || this.LightBased) && Cell.IsBlackedOut())
				{
					bool flag2;
					bool flag3;
					bool flag = BeforeProjectileHitEvent.Check(this.ParentObject, Attacker, null, out flag2, out flag3, PenetrateCreatures, PenetrateWalls, Launcher, ApparentTarget, Cell, this, true, Prospective);
					if (flag3)
					{
						RecheckPhase = true;
					}
					if (flag2)
					{
						RecheckHit = true;
						if (++num < 100)
						{
							continue;
						}
					}
					if (flag)
					{
						break;
					}
				}
				if (this.RealityDistortionBased || !this.TargetPart.IsNullOrEmpty() || !this.TargetTag.IsNullOrEmpty())
				{
					int i = 0;
					int count = Cell.Objects.Count;
					while (i < count)
					{
						bool flag5;
						bool flag6;
						bool flag4 = this.Match(Cell.Objects[i], Attacker, out flag5, out flag6, Launcher, ApparentTarget, PhaseFrom, PenetrateCreatures, PenetrateWalls, Prospective, Cell);
						if (flag6)
						{
							RecheckPhase = true;
						}
						if (flag5)
						{
							RecheckHit = true;
							if (++num < 100)
							{
								goto IL_0012;
							}
						}
						if (flag4)
						{
							goto Block_11;
						}
						i++;
					}
					return false;
				}
				return false;
			}
			return true;
			Block_11:
			if (this.Hits)
			{
				int i;
				ObjectHit = Cell.Objects[i];
			}
			return true;
		}

		// Token: 0x0600AD93 RID: 44435 RVA: 0x004041B8 File Offset: 0x004023B8
		public bool Match(Cell Cell, GameObject Attacker, out GameObject ObjectHit, out bool RecheckHit, out bool RecheckPhase, GameObject Launcher = null, GameObject ApparentTarget = null, GameObject PhaseFrom = null, bool PenetrateCreatures = false, bool PenetrateWalls = false, bool Prospective = false)
		{
			bool flag = this.MatchInner(Cell, Attacker, out ObjectHit, out RecheckHit, out RecheckPhase, Launcher, ApparentTarget, PhaseFrom, PenetrateCreatures, PenetrateWalls, Prospective);
			if (flag && !Prospective && !this.Message.IsNullOrEmpty() && (Cell.IsVisible() || ((this.GazeBased || this.LightBased) && Cell.IsAnyLocalAdjacentCellVisible())))
			{
				IComponent<GameObject>.AddPlayerMessage(GameText.VariableReplace(this.Message, this.ParentObject, ObjectHit, false), null, true);
			}
			return flag;
		}

		// Token: 0x0600AD94 RID: 44436 RVA: 0x00404230 File Offset: 0x00402430
		public bool Match(Cell Cell, GameObject Attacker, GameObject Launcher = null, GameObject ApparentTarget = null, GameObject PhaseFrom = null, bool PenetrateCreatures = false, bool PenetrateWalls = false, bool Prospective = false)
		{
			GameObject gameObject;
			bool flag;
			bool flag2;
			return this.Match(Cell, Attacker, out gameObject, out flag, out flag2, Launcher, ApparentTarget, PhaseFrom, PenetrateCreatures, PenetrateWalls, Prospective);
		}

		// Token: 0x0600AD95 RID: 44437 RVA: 0x00404258 File Offset: 0x00402458
		private bool MatchInner(GameObject Object, GameObject Attacker, GameObject Launcher = null, GameObject ApparentTarget = null, GameObject PhaseFrom = null, bool PenetrateCreatures = false, bool PenetrateWalls = false, bool Prospective = false)
		{
			if (Object == null)
			{
				return false;
			}
			if (!this.TargetPart.IsNullOrEmpty() && Object.HasPart(this.TargetPart) && this.PhaseOkay(Object, PhaseFrom))
			{
				return true;
			}
			if (!this.TargetTag.IsNullOrEmpty() && Object.HasTag(this.TargetTag) && this.PhaseOkay(Object, PhaseFrom) && (this.TargetTagValue.IsNullOrEmpty() || Object.GetTag(this.TargetTag, null) == this.TargetTagValue))
			{
				return true;
			}
			if (this.RealityDistortionBased && this.PhaseOkay(Object, PhaseFrom))
			{
				foreach (RealityStabilized realityStabilized in Object.YieldEffects<RealityStabilized>())
				{
					if (Prospective ? (realityStabilized.Strength >= 70) : realityStabilized.Strength.in100())
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600AD96 RID: 44438 RVA: 0x00404354 File Offset: 0x00402554
		public bool Match(GameObject Object, GameObject Attacker, out bool RecheckHit, out bool RecheckPhase, GameObject Launcher = null, GameObject ApparentTarget = null, GameObject PhaseFrom = null, bool PenetrateCreatures = false, bool PenetrateWalls = false, bool Prospective = false, Cell FromCell = null)
		{
			RecheckHit = false;
			RecheckPhase = false;
			bool flag = this.MatchInner(Object, Attacker, Launcher, ApparentTarget, PhaseFrom, PenetrateCreatures, PenetrateWalls, Prospective);
			if (flag)
			{
				bool flag2;
				bool flag3;
				flag = BeforeProjectileHitEvent.Check(this.ParentObject, Attacker, Object, out flag2, out flag3, PenetrateCreatures, PenetrateWalls, Launcher, ApparentTarget, FromCell, this, false, Prospective);
				if (flag3)
				{
					RecheckPhase = true;
				}
				if (flag2)
				{
					RecheckHit = true;
					return flag;
				}
			}
			if (flag && FromCell == null && !Prospective && !this.Message.IsNullOrEmpty() && Object.IsVisible())
			{
				IComponent<GameObject>.AddPlayerMessage(GameText.VariableReplace(this.Message, this.ParentObject, Object, false), null, true);
			}
			return flag;
		}

		// Token: 0x0600AD97 RID: 44439 RVA: 0x004043F8 File Offset: 0x004025F8
		public bool Match(GameObject Object, GameObject Attacker, GameObject Launcher = null, GameObject ApparentTarget = null, GameObject PhaseFrom = null, bool PenetrateCreatures = false, bool PenetrateWalls = false, bool Prospective = false, Cell FromCell = null)
		{
			int num = 0;
			bool flag;
			bool flag2;
			do
			{
				flag = this.MatchInner(Object, Attacker, Launcher, ApparentTarget, PhaseFrom, PenetrateCreatures, PenetrateWalls, Prospective);
				if (!flag)
				{
					break;
				}
				bool flag3;
				flag = BeforeProjectileHitEvent.Check(this.ParentObject, Attacker, Object, out flag2, out flag3, PenetrateCreatures, PenetrateWalls, Launcher, ApparentTarget, FromCell, this, false, Prospective);
			}
			while (flag2 && ++num < 100);
			if (flag && FromCell == null && !Prospective && !this.Message.IsNullOrEmpty() && Object.IsVisible())
			{
				IComponent<GameObject>.AddPlayerMessage(GameText.VariableReplace(this.Message, this.ParentObject, Object, false), null, true);
			}
			return flag;
		}

		// Token: 0x0600AD98 RID: 44440 RVA: 0x00404494 File Offset: 0x00402694
		private bool PhaseOkay(GameObject Object, GameObject PhaseFrom)
		{
			return !this.RequiresPhaseMatch || (PhaseFrom != null && PhaseFrom.PhaseMatches(Object));
		}

		// Token: 0x0600AD99 RID: 44441 RVA: 0x004044AC File Offset: 0x004026AC
		public override bool SameAs(IPart p)
		{
			TreatAsSolid treatAsSolid = p as TreatAsSolid;
			return !(treatAsSolid.TargetPart != this.TargetPart) && !(treatAsSolid.TargetTag != this.TargetTag) && !(treatAsSolid.TargetTagValue != this.TargetTagValue) && !(treatAsSolid.Message != this.Message) && treatAsSolid.RealityDistortionBased == this.RealityDistortionBased && treatAsSolid.GazeBased == this.GazeBased && treatAsSolid.LightBased == this.LightBased && treatAsSolid.RequiresPhaseMatch == this.RequiresPhaseMatch && treatAsSolid.Hits == this.Hits && base.SameAs(p);
		}

		// Token: 0x040047D1 RID: 18385
		public string TargetPart;

		// Token: 0x040047D2 RID: 18386
		public string TargetTag;

		// Token: 0x040047D3 RID: 18387
		public string TargetTagValue;

		// Token: 0x040047D4 RID: 18388
		public string Message;

		// Token: 0x040047D5 RID: 18389
		public bool RealityDistortionBased;

		// Token: 0x040047D6 RID: 18390
		public bool GazeBased;

		// Token: 0x040047D7 RID: 18391
		public bool LightBased;

		// Token: 0x040047D8 RID: 18392
		public bool RequiresPhaseMatch = true;

		// Token: 0x040047D9 RID: 18393
		public bool Hits = true;
	}
}
