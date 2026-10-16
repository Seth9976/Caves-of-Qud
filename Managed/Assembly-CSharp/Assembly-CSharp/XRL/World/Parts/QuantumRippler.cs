using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FAC RID: 4012
	[Serializable]
	public class QuantumRippler : IPoweredPart
	{
		// Token: 0x06009F98 RID: 40856 RVA: 0x0039FCCC File Offset: 0x0039DECC
		public QuantumRippler()
		{
			this.WorksOnSelf = true;
			this.IsRealityDistortionBased = true;
		}

		// Token: 0x06009F99 RID: 40857 RVA: 0x0039FD2B File Offset: 0x0039DF2B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009F9A RID: 40858 RVA: 0x0039FD2E File Offset: 0x0039DF2E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || (ID == PooledEvent<RealityStabilizeEvent>.ID && this.ExplodeOnRealityStabilize) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x06009F9B RID: 40859 RVA: 0x0039FD5C File Offset: 0x0039DF5C
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckDirections(false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009F9C RID: 40860 RVA: 0x0039FD6C File Offset: 0x0039DF6C
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.CheckDirections(true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009F9D RID: 40861 RVA: 0x0039FD7C File Offset: 0x0039DF7C
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (this.ExplodeOnRealityStabilize && E.Check(true))
			{
				this.ParentObject.ParticleBlip("&K!", 10, 0L, false);
				if (base.Visible())
				{
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("collapse", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " under the pressure of normality and" + this.ParentObject.GetVerb("implode", true, false, false, true, false) + ".", null, true);
				}
				this.ParentObject.Explode(20000, E.Effect.Owner, "12d10+300", 1f, true, false, false, 0, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009F9E RID: 40862 RVA: 0x0039FE50 File Offset: 0x0039E050
		public void CheckDirections(bool Initial = false)
		{
			if (this.Directions.IsNullOrEmpty())
			{
				return;
			}
			foreach (string text in this.Directions.CachedCommaExpansion())
			{
				this.CheckDirection(text, Initial);
			}
		}

		// Token: 0x06009F9F RID: 40863 RVA: 0x0039FEB8 File Offset: 0x0039E0B8
		public void CheckDirection(string Direction, bool Initial = false)
		{
			if (this.Blueprint.IsNullOrEmpty())
			{
				return;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			Cell cellFromDirection = currentCell.GetCellFromDirection(Direction, true);
			if (cellFromDirection == null)
			{
				return;
			}
			Cell cellFromDirection2 = cellFromDirection.GetCellFromDirection(Direction, true);
			if (cellFromDirection2 == null)
			{
				return;
			}
			GameObject firstObjectWithPart = cellFromDirection2.GetFirstObjectWithPart(base.Name);
			if (firstObjectWithPart == null)
			{
				return;
			}
			if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return;
			}
			QuantumRippler part = firstObjectWithPart.GetPart<QuantumRippler>();
			if (part == null || !part.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return;
			}
			GameObject firstObject;
			for (;;)
			{
				firstObject = cellFromDirection.GetFirstObject(this.Blueprint);
				if (firstObject != null)
				{
					break;
				}
				if (this.TimeToGenerate.IsNullOrEmpty())
				{
					return;
				}
				if (Initial)
				{
					this.TimeLeftToGenerate = 0;
				}
				else
				{
					this.TimeLeftToGenerate--;
				}
				if (this.TimeLeftToGenerate < 0)
				{
					goto Block_18;
				}
				if (this.TimeLeftToGenerate != 0)
				{
					goto IL_01D7;
				}
				cellFromDirection.AddObject(this.Blueprint, null, null, null, null);
				if (!Initial)
				{
					goto IL_01D7;
				}
				Initial = false;
			}
			QuantumStabilized effect = firstObject.GetEffect<QuantumStabilized>();
			if (effect == null)
			{
				if (this.TimeToStabilize.IsNullOrEmpty())
				{
					return;
				}
				if (Initial)
				{
					this.TimeLeftToStabilize = 0;
				}
				else
				{
					this.TimeLeftToStabilize--;
				}
				if (this.TimeLeftToStabilize < 0)
				{
					this.TimeLeftToStabilize = this.TimeToStabilize.RollCached();
					goto IL_01D7;
				}
				if (this.TimeLeftToStabilize == 0)
				{
					firstObject.ForceApplyEffect(new QuantumStabilized(this.StabilizeDuration.RollCached()), null);
					goto IL_01D7;
				}
				goto IL_01D7;
			}
			else
			{
				if (this.StabilizeDuration.IsNullOrEmpty())
				{
					return;
				}
				int num = this.StabilizeDuration.RollCached();
				if (effect.Duration < num)
				{
					effect.Duration = num;
					goto IL_01D7;
				}
				goto IL_01D7;
			}
			return;
			Block_18:
			this.TimeLeftToGenerate = this.TimeToGenerate.RollCached();
			IL_01D7:
			base.ConsumeCharge(null, null);
		}

		// Token: 0x04004236 RID: 16950
		public string Blueprint = "Space-Time Rift";

		// Token: 0x04004237 RID: 16951
		public string TimeToGenerate = "3d10";

		// Token: 0x04004238 RID: 16952
		public string TimeToStabilize = "3d10";

		// Token: 0x04004239 RID: 16953
		public string StabilizeDuration = "1d6";

		// Token: 0x0400423A RID: 16954
		public string Directions = "N,S,E,W";

		// Token: 0x0400423B RID: 16955
		public int TimeLeftToGenerate;

		// Token: 0x0400423C RID: 16956
		public int TimeLeftToStabilize;

		// Token: 0x0400423D RID: 16957
		public bool ExplodeOnRealityStabilize = true;
	}
}
