using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000EB2 RID: 3762
	[Serializable]
	public class ExtradimensionalHunterSummoner : IPart
	{
		// Token: 0x060096EC RID: 38636 RVA: 0x0036C0C4 File Offset: 0x0036A2C4
		public override void Initialize()
		{
			this.Counter = this.Cooldown.RollCached();
		}

		// Token: 0x060096ED RID: 38637 RVA: 0x0036C0D7 File Offset: 0x0036A2D7
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060096EE RID: 38638 RVA: 0x0036C0DA File Offset: 0x0036A2DA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (!this.Active && ID == TookDamageEvent.ID);
		}

		// Token: 0x060096EF RID: 38639 RVA: 0x0036C0FA File Offset: 0x0036A2FA
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060096F0 RID: 38640 RVA: 0x0036C0FD File Offset: 0x0036A2FD
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.Tick(Amount);
		}

		// Token: 0x060096F1 RID: 38641 RVA: 0x0036C108 File Offset: 0x0036A308
		public void Tick(int Turns)
		{
			if (!this.Active)
			{
				if (!this.Registered)
				{
					this.RegisterClamDeath();
				}
				return;
			}
			if (!this.ParentObject.IsHostileTowards(The.Player))
			{
				return;
			}
			this.Counter -= Turns;
			if (this.Counter <= 0)
			{
				this.Counter = this.Cooldown.RollCached();
				this.Summon(Stat.Roll(this.NumberHuntersOngoing, null));
			}
		}

		// Token: 0x060096F2 RID: 38642 RVA: 0x0036C179 File Offset: 0x0036A379
		public List<GameObject> GetClams()
		{
			return this.ParentObject.CurrentZone.GetObjects("Giant Clam");
		}

		// Token: 0x060096F3 RID: 38643 RVA: 0x0036C190 File Offset: 0x0036A390
		public void Summon(int Number = 1)
		{
			List<GameObject> list = new List<GameObject>();
			List<GameObject> clams = this.GetClams();
			for (int i = 0; i < Number; i++)
			{
				if (this.CultChance.in100())
				{
					PsychicHunterSystem.CreateExtradimensionalCultHunters(this.ParentObject.CurrentZone, 1, list, false, false, false, true);
				}
				else
				{
					PsychicHunterSystem.CreateExtradimensionalSoloHunters(this.ParentObject.CurrentZone, 1, list, false, false, false, true);
				}
				GameObject gameObject = list.Last<GameObject>();
				GameObject randomElement = clams.GetRandomElement<GameObject>(null);
				if (randomElement != null && GiantClamProperties.Teleport(gameObject, randomElement.CurrentCell, 'O'))
				{
					IComponent<GameObject>.XDidYToZ(gameObject, "emerge", "from", randomElement, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, true, false, false, false, null);
				}
				else
				{
					gameObject.SetLongProperty("ClamTeleportTurn", The.Game.Turns);
					PsychicHunterSystem.PlaceHunter(this.ParentObject.CurrentZone, gameObject, null, true, "&O");
				}
			}
			if (list.Count > 0)
			{
				base.DidX("open", "wide, and the nacre inside " + this.ParentObject.its + " shell glimmers with unseen light", null, null, null, null, null, false, false, null, null, false, false, false, false, true, null);
				if (this.ParentObject.CurrentZone.IsActive())
				{
					PsychicHunterSystem.PsychicPresenceMessage(list.Count, true);
				}
			}
		}

		// Token: 0x060096F4 RID: 38644 RVA: 0x0036C2CD File Offset: 0x0036A4CD
		public void Activate()
		{
			this.Summon(Stat.Roll(this.NumberHuntersStart, null));
			this.Active = true;
		}

		// Token: 0x060096F5 RID: 38645 RVA: 0x0036C2E8 File Offset: 0x0036A4E8
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (!this.Active && E.Object == this.ParentObject && E.Actor != null && E.Actor.IsPlayerControlled())
			{
				this.Activate();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096F6 RID: 38646 RVA: 0x0036C322 File Offset: 0x0036A522
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060096F7 RID: 38647 RVA: 0x0036C328 File Offset: 0x0036A528
		public void RegisterClamDeath()
		{
			foreach (GameObject gameObject in this.GetClams())
			{
				gameObject.RegisterPartEvent(this, "BeforeDeathRemoval");
			}
			this.Registered = true;
		}

		// Token: 0x060096F8 RID: 38648 RVA: 0x0036C388 File Offset: 0x0036A588
		public override bool FireEvent(Event E)
		{
			if (!this.Active && E.ID == "BeforeDeathRemoval" && this.ActiveOnDeathChance.in100())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Killer");
				if (gameObjectParameter != null)
				{
					this.ParentObject.AddOpinion<OpinionKilledAlly>(gameObjectParameter, this.ParentObject, 1f);
				}
				this.Activate();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003EA9 RID: 16041
		public int CultChance = 50;

		// Token: 0x04003EAA RID: 16042
		public int ActiveOnDeathChance = 20;

		// Token: 0x04003EAB RID: 16043
		public string NumberHuntersStart = "3";

		// Token: 0x04003EAC RID: 16044
		public string NumberHuntersOngoing = "1-3";

		// Token: 0x04003EAD RID: 16045
		public string Cooldown = "150-200";

		// Token: 0x04003EAE RID: 16046
		public int Counter;

		// Token: 0x04003EAF RID: 16047
		public bool Active;

		// Token: 0x04003EB0 RID: 16048
		[NonSerialized]
		private bool Registered;
	}
}
