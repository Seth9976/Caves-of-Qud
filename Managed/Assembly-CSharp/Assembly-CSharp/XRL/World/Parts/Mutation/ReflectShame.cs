using System;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200145C RID: 5212
	[Serializable]
	public class ReflectShame : BaseMutation
	{
		// Token: 0x0600CC37 RID: 52279 RVA: 0x004A33B5 File Offset: 0x004A15B5
		public ReflectShame()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600CC38 RID: 52280 RVA: 0x004A33DA File Offset: 0x004A15DA
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600CC39 RID: 52281 RVA: 0x004A33DD File Offset: 0x004A15DD
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600CC3A RID: 52282 RVA: 0x004A33E4 File Offset: 0x004A15E4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CC3B RID: 52283 RVA: 0x004A33FA File Offset: 0x004A15FA
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("glass", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC3C RID: 52284 RVA: 0x004A341D File Offset: 0x004A161D
		public override string GetDescription()
		{
			return "You reflect the shameful countenance of nearby creatures.";
		}

		// Token: 0x0600CC3D RID: 52285 RVA: 0x004A3424 File Offset: 0x004A1624
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600CC3E RID: 52286 RVA: 0x004A3427 File Offset: 0x004A1627
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ShameNearby();
		}

		// Token: 0x0600CC3F RID: 52287 RVA: 0x004A3430 File Offset: 0x004A1630
		public void ShameNearby()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			foreach (GameObject gameObject in currentCell.ParentZone.FastCombatSquareVisibility(currentCell.X, currentCell.Y, this.Radius, this.ParentObject, new Predicate<GameObject>(this.IsValidTarget), false, false, true, true, null))
			{
				base.PerformMentalAttack(new Mental.Attack(this.Shame), this.ParentObject, gameObject, null, "Shame ReflectShame", "1d8", 4, Stat.RollCached(this.Duration), int.MinValue, this.ParentObject.StatMod("Ego", 0), 0);
			}
		}

		// Token: 0x0600CC40 RID: 52288 RVA: 0x004A3504 File Offset: 0x004A1704
		public bool Shame(MentalAttackEvent E)
		{
			if (E.Penetrations >= 1 && CanApplyEffectEvent.Check<Shamed>(E.Defender, E.Magnitude) && E.Defender.ApplyEffect(new Shamed(E.Magnitude), null))
			{
				Messaging.XDidYToZ(E.Defender, "are", "shamed by", E.Defender, "reflection", "!", null, null, null, E.Defender, false, false, false, false, true, null, null, null, false, false, false, false, false, null);
				return true;
			}
			return false;
		}

		// Token: 0x0600CC41 RID: 52289 RVA: 0x004A3585 File Offset: 0x004A1785
		private bool IsValidTarget(GameObject Object)
		{
			return (this.Friendly || Object.IsHostileTowards(this.ParentObject)) && !Object.HasEffect<Shamed>() && Object.HasLOSTo(this.ParentObject, true, false, false, true, null);
		}

		/// <summary>Shamed effect duration.</summary>
		// Token: 0x04004F37 RID: 20279
		public string Duration = "20-30";

		// Token: 0x04004F38 RID: 20280
		public bool Friendly;

		/// <summary>Affected target radius.</summary>
		// Token: 0x04004F39 RID: 20281
		public int Radius = 5;
	}
}
