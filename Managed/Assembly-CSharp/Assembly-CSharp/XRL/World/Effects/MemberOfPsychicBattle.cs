using System;
using System.Text;

namespace XRL.World.Effects
{
	// Token: 0x02000DB1 RID: 3505
	[Serializable]
	public class MemberOfPsychicBattle : Effect
	{
		// Token: 0x06008BF9 RID: 35833 RVA: 0x0031B160 File Offset: 0x00319360
		public MemberOfPsychicBattle()
		{
			this.Duration = 9999;
		}

		// Token: 0x06008BFA RID: 35834 RVA: 0x0031B17B File Offset: 0x0031937B
		public MemberOfPsychicBattle(GameObject other, bool isAttacker)
			: this()
		{
			this.other = other.Reference();
			this.isAttacker = isAttacker;
		}

		// Token: 0x06008BFB RID: 35835 RVA: 0x0031B198 File Offset: 0x00319398
		public override int GetEffectType()
		{
			int num = 32770;
			if (!this.isAttacker)
			{
				num |= 33554432;
			}
			return num;
		}

		// Token: 0x06008BFC RID: 35836 RVA: 0x0031B1BC File Offset: 0x003193BC
		public override string GetDescription()
		{
			return "locked in psychic battle";
		}

		// Token: 0x06008BFD RID: 35837 RVA: 0x0031B1C4 File Offset: 0x003193C4
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Locked in psychic battle with a foe. (").Append(this.turnsRemaining.Things("turn", null)).Append(" remaining)");
			if (this.turnsRemaining <= 5 && base.Object.CanHaveNosebleed())
			{
				stringBuilder.Append("\nHas a bloody nose.");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008BFE RID: 35838 RVA: 0x0031B22C File Offset: 0x0031942C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || ID == SingletonEvent<BeforeTakeActionEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06008BFF RID: 35839 RVA: 0x0031B252 File Offset: 0x00319452
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.IsInvalid())
			{
				GameObject @object = base.Object;
				if (@object != null)
				{
					@object.RemoveEffect(this, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008C00 RID: 35840 RVA: 0x0031B278 File Offset: 0x00319478
		public override bool HandleEvent(BeforeTakeActionEvent E)
		{
			if (this.IsInvalid())
			{
				base.Object.RemoveEffect(this, true);
			}
			if (base.Object != null && !base.Object.IsPlayer() && this.isAttacker)
			{
				base.Object.PassTurn();
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008C01 RID: 35841 RVA: 0x0031B2CC File Offset: 0x003194CC
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.other.Object == null)
			{
				base.Object.RemoveEffect(this, true);
				return true;
			}
			Cell currentCell = base.Object.CurrentCell;
			if (currentCell != null && (base.Object.IsPlayer() || this.other.Object.IsPlayer()))
			{
				Zone parentZone = currentCell.ParentZone;
				if (parentZone != null)
				{
					parentZone.AddExplored(currentCell.X, currentCell.Y, 0);
				}
				Zone parentZone2 = currentCell.ParentZone;
				if (parentZone2 != null)
				{
					parentZone2.AddLight(currentCell.X, currentCell.Y, 0, LightLevel.Omniscient, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008C02 RID: 35842 RVA: 0x0031B370 File Offset: 0x00319570
		public bool IsInvalid()
		{
			GameObjectReference gameObjectReference = this.other;
			GameObject gameObject = ((gameObjectReference != null) ? gameObjectReference.Object : null);
			return gameObject == null || gameObject.IsInvalid() || !gameObject.InSameZone(base.Object) || !gameObject.HasEffect<MemberOfPsychicBattle>() || (this.isAttacker && !base.Object.IsPlayer() && base.Object.IsAlliedTowards(gameObject));
		}

		// Token: 0x04003AEA RID: 15082
		public GameObjectReference other;

		// Token: 0x04003AEB RID: 15083
		public int turnsRemaining = 10;

		// Token: 0x04003AEC RID: 15084
		public bool isAttacker;
	}
}
