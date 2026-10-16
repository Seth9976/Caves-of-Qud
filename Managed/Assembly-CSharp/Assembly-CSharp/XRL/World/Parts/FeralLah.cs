using System;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000E70 RID: 3696
	[Serializable]
	public class FeralLah : IPart
	{
		// Token: 0x0600954F RID: 38223 RVA: 0x0036022C File Offset: 0x0035E42C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009550 RID: 38224 RVA: 0x0036022F File Offset: 0x0035E42F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009551 RID: 38225 RVA: 0x00360232 File Offset: 0x0035E432
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIAttackRange");
			Registrar.Register("BeforeTakeAction");
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009552 RID: 38226 RVA: 0x00360260 File Offset: 0x0035E460
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (!this.ParentObject.IsFrozen() && this.PodCooldown > 0)
				{
					this.PodCooldown--;
				}
			}
			else if (E.ID == "BeforeTakeAction")
			{
				if (!this.ParentObject.IsNowhere() && !this.ParentObject.IsFrozen() && this.ParentObject.Target != null)
				{
					Cell randomElement = this.ParentObject.CurrentCell.GetEmptyAdjacentCells().GetRandomElement<Cell>(null);
					if (randomElement != null && this.PodCooldown <= 0)
					{
						this.PodCooldown = Stat.Random(5, 7);
						GameObject gameObject = GameObject.Create("Feral Lah Pod");
						gameObject.PartyLeader = this.ParentObject;
						gameObject.Brain.TakeBaseAllegiance(this.ParentObject);
						gameObject.MakeActive(false);
						randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						this.ParentObject.Splatter("&g.");
						gameObject.ParticleBlip("&go", 10, 0L, false);
						this.ParentObject.UseEnergy(1000);
						return false;
					}
					if (this.ParentObject.Target.DistanceTo(this.ParentObject) <= 1)
					{
						this.Fear();
					}
				}
			}
			else if (E.ID == "AIAttackRange")
			{
				this.ParentObject.UseEnergy(1000);
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009553 RID: 38227 RVA: 0x003603F0 File Offset: 0x0035E5F0
		public void Fear()
		{
			this.FearCooldown--;
			if (this.FearCooldown < 0 && this.FearChance.in100())
			{
				this.FearCooldown = 20 + Stat.Roll("1d6", null);
				FearAura.PulseAura(this.ParentObject);
			}
		}

		// Token: 0x04003DD4 RID: 15828
		public int FearChance = 100;

		// Token: 0x04003DD5 RID: 15829
		public int FearCooldown;

		// Token: 0x04003DD6 RID: 15830
		public int PodCooldown;
	}
}
