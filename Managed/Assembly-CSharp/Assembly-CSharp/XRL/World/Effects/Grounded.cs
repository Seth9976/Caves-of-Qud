using System;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D42 RID: 3394
	[Serializable]
	public class Grounded : Effect, ITierInitialized
	{
		// Token: 0x060085FE RID: 34302 RVA: 0x00304465 File Offset: 0x00302665
		public Grounded()
		{
			this.DisplayName = "{{w|grounded}}";
		}

		// Token: 0x060085FF RID: 34303 RVA: 0x00304478 File Offset: 0x00302678
		public Grounded(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008600 RID: 34304 RVA: 0x00304487 File Offset: 0x00302687
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 1000);
		}

		// Token: 0x06008601 RID: 34305 RVA: 0x0030449B File Offset: 0x0030269B
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008602 RID: 34306 RVA: 0x0030449E File Offset: 0x0030269E
		public override int GetEffectType()
		{
			return 83886336;
		}

		// Token: 0x06008603 RID: 34307 RVA: 0x003044A5 File Offset: 0x003026A5
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x003044A8 File Offset: 0x003026A8
		public override string GetDetails()
		{
			return "Cannot fly.";
		}

		// Token: 0x06008605 RID: 34309 RVA: 0x003044AF File Offset: 0x003026AF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID;
		}

		// Token: 0x06008606 RID: 34310 RVA: 0x003044C8 File Offset: 0x003026C8
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (E.Object == base.Object && E.To == "Flying")
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You can't fly right now.");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008607 RID: 34311 RVA: 0x00304517 File Offset: 0x00302717
		public override bool Apply(GameObject Object)
		{
			if (!Object.IsCreature)
			{
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyGrounded", 0, 0, 0)) || !ApplyEffectEvent.Check(Object, "Grounded", this, null))
			{
				return false;
			}
			Flight.Fall(Object);
			Acrobatics_Jump.SyncAbility(Object, true);
			return true;
		}

		// Token: 0x06008608 RID: 34312 RVA: 0x00304557 File Offset: 0x00302757
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
			Acrobatics_Jump.SyncAbility(Object, true);
		}
	}
}
