using System;
using XRL.Language;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000DA5 RID: 3493
	[Serializable]
	public class Unpiloted : Effect
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06008B74 RID: 35700 RVA: 0x00319330 File Offset: 0x00317530
		public Vehicle Vehicle
		{
			get
			{
				Vehicle vehicle;
				if ((vehicle = this._Vehicle) == null)
				{
					vehicle = (this._Vehicle = base.Object.GetPart<Vehicle>());
				}
				return vehicle;
			}
		}

		// Token: 0x06008B75 RID: 35701 RVA: 0x0031935B File Offset: 0x0031755B
		public Unpiloted()
		{
			this.DisplayName = "{{C|unpiloted}}";
			this.Duration = 1;
		}

		// Token: 0x06008B76 RID: 35702 RVA: 0x0031937C File Offset: 0x0031757C
		public Unpiloted(int Percent, bool CanTakeAction = true)
			: this()
		{
			this.SpeedPenaltyPercent = Percent;
			this.CanTakeAction = CanTakeAction;
		}

		// Token: 0x06008B77 RID: 35703 RVA: 0x00319392 File Offset: 0x00317592
		public override int GetEffectType()
		{
			return 33554560;
		}

		// Token: 0x06008B78 RID: 35704 RVA: 0x00319399 File Offset: 0x00317599
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008B79 RID: 35705 RVA: 0x0031939C File Offset: 0x0031759C
		public override string GetDetails()
		{
			if (!this.CanTakeAction)
			{
				return "Can't take actions.";
			}
			string text = ((this.SpeedPenaltyPercent > 0 && this.SpeedPenaltyPercent % 100 == 0) ? (Grammar.Multiplicative(this.SpeedPenaltyPercent / 100 + 1) + " as slowly") : ("at " + this.SpeedPenaltyPercent.ToString("+0;-#") + "% action cost"));
			return "This creature performs non-movement actions " + text + ".";
		}

		// Token: 0x06008B7A RID: 35706 RVA: 0x00319417 File Offset: 0x00317617
		public override bool Apply(GameObject Object)
		{
			return !Object.HasEffect(typeof(Unpiloted)) && ApplyEffectEvent.Check(Object, "Unpiloted", this, null);
		}

		// Token: 0x06008B7B RID: 35707 RVA: 0x0031943F File Offset: 0x0031763F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == SingletonEvent<BeforeTakeActionEvent>.ID && !this.CanTakeAction) || (ID == PooledEvent<BeforeSetFeelingEvent>.ID && !this.CanTakeAction) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GetEnergyCostEvent>.ID;
		}

		// Token: 0x06008B7C RID: 35708 RVA: 0x0031947D File Offset: 0x0031767D
		public override bool HandleEvent(BeforeTakeActionEvent E)
		{
			return this.CanTakeAction && base.HandleEvent(E);
		}

		// Token: 0x06008B7D RID: 35709 RVA: 0x00319490 File Offset: 0x00317690
		public override bool HandleEvent(EndTurnEvent E)
		{
			Vehicle vehicle = this.Vehicle;
			if (!string.IsNullOrEmpty((vehicle != null) ? vehicle.PilotID : null))
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B7E RID: 35710 RVA: 0x003194C0 File Offset: 0x003176C0
		public override bool HandleEvent(GetEnergyCostEvent E)
		{
			if (E.Type.IsNullOrEmpty() || !E.Type.Contains("Move"))
			{
				E.PercentageReduction -= this.SpeedPenaltyPercent;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B7F RID: 35711 RVA: 0x003194FB File Offset: 0x003176FB
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (!this.CanTakeAction)
			{
				E.Feeling = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B80 RID: 35712 RVA: 0x00319515 File Offset: 0x00317715
		public override bool HandleEvent(BeforeSetFeelingEvent E)
		{
			if (!this.CanTakeAction)
			{
				E.Feeling = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B81 RID: 35713 RVA: 0x0031952F File Offset: 0x0031772F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (!this.CanTakeAction)
			{
				Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
			}
		}

		// Token: 0x04003ACE RID: 15054
		public int SpeedPenaltyPercent;

		// Token: 0x04003ACF RID: 15055
		public bool CanTakeAction = true;

		// Token: 0x04003AD0 RID: 15056
		[NonSerialized]
		private Vehicle _Vehicle;
	}
}
