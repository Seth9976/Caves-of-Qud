using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000DA9 RID: 3497
	[Serializable]
	public class Wakeful : Effect, ITierInitialized
	{
		// Token: 0x06008BAB RID: 35755 RVA: 0x00319E05 File Offset: 0x00318005
		public Wakeful()
		{
			this.DisplayName = "{{W|wakeful}}";
		}

		// Token: 0x06008BAC RID: 35756 RVA: 0x00319E18 File Offset: 0x00318018
		public Wakeful(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008BAD RID: 35757 RVA: 0x00319E27 File Offset: 0x00318027
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(500, 2000);
		}

		// Token: 0x06008BAE RID: 35758 RVA: 0x00319E3E File Offset: 0x0031803E
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008BAF RID: 35759 RVA: 0x00319E41 File Offset: 0x00318041
		public override int GetEffectType()
		{
			return 83886082;
		}

		// Token: 0x06008BB0 RID: 35760 RVA: 0x00319E48 File Offset: 0x00318048
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008BB1 RID: 35761 RVA: 0x00319E4B File Offset: 0x0031804B
		public override string GetDescription()
		{
			if (Asleep.UsesSleepMode(base.Object))
			{
				return "{{W|safe mode}}";
			}
			return base.GetDescription();
		}

		// Token: 0x06008BB2 RID: 35762 RVA: 0x00319E66 File Offset: 0x00318066
		public override string GetDetails()
		{
			if (Asleep.UsesSleepMode(base.Object))
			{
				return "Cannot be put in sleep mode.";
			}
			return "Cannot fall asleep.";
		}

		// Token: 0x06008BB3 RID: 35763 RVA: 0x00319E80 File Offset: 0x00318080
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasPart<Brain>())
			{
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyWakeful", 0, 0, 0)) || !ApplyEffectEvent.Check(Object, "Wakeful", this, null))
			{
				return false;
			}
			Wakeful wakeful;
			if (Object.TryGetEffect<Wakeful>(out wakeful))
			{
				wakeful.Duration += this.Duration;
				return false;
			}
			return true;
		}

		// Token: 0x06008BB4 RID: 35764 RVA: 0x00319EDD File Offset: 0x003180DD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyInvoluntarySleep");
			Registrar.Register("CanApplyInvoluntarySleep");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008BB5 RID: 35765 RVA: 0x00319EFD File Offset: 0x003180FD
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanApplyInvoluntarySleep") && !(E.ID == "ApplyInvoluntarySleep") && base.FireEvent(E);
		}
	}
}
