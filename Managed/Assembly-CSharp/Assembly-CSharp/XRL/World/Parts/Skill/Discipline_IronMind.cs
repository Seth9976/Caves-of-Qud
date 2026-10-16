using System;
using XRL.Messages;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200137D RID: 4989
	[Serializable]
	public class Discipline_IronMind : BaseSkill
	{
		// Token: 0x0600C1A1 RID: 49569 RVA: 0x0046C7F5 File Offset: 0x0046A9F5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyConfusion");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C1A2 RID: 49570 RVA: 0x0046C818 File Offset: 0x0046AA18
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyConfusion")
			{
				if (this.GetChance().in100())
				{
					this.ParentObject.ParticleText("*shook off confusion*", IComponent<GameObject>.ConsequentialColorChar(this.ParentObject, null), false, 1.5f, -8f);
					return false;
				}
			}
			else if (E.ID == "EndTurn" && this.ParentObject.HasEffect<Confused>() && this.GetChance().in100())
			{
				this.ParentObject.RemoveEffect<Confused>();
				if (this.ParentObject.IsPlayer())
				{
					if (The.Player.IsConfused)
					{
						MessageQueue.AddPlayerMessage("You muster your will and shake off some of your confusion.", null, true);
					}
					else
					{
						MessageQueue.AddPlayerMessage("You muster your will and shake off your confusion.", null, true);
					}
				}
				this.ParentObject.ParticleText("*shook off confusion*", IComponent<GameObject>.ConsequentialColorChar(this.ParentObject, null), false, 1.5f, -8f);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C1A3 RID: 49571 RVA: 0x0046C90C File Offset: 0x0046AB0C
		public override bool AddSkill(GameObject GO)
		{
			return base.AddSkill(GO);
		}

		// Token: 0x0600C1A4 RID: 49572 RVA: 0x0046C915 File Offset: 0x0046AB15
		public override bool RemoveSkill(GameObject GO)
		{
			return base.RemoveSkill(GO);
		}

		// Token: 0x0600C1A5 RID: 49573 RVA: 0x0046C91E File Offset: 0x0046AB1E
		public int GetChance()
		{
			return this.ParentObject.Stat("Willpower", 0) - 10;
		}
	}
}
