using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200137E RID: 4990
	[Serializable]
	public class Discipline_Lionheart : BaseSkill
	{
		// Token: 0x0600C1A7 RID: 49575 RVA: 0x0046C93C File Offset: 0x0046AB3C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C1A8 RID: 49576 RVA: 0x0046C93F File Offset: 0x0046AB3F
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckRemoval(Amount);
		}

		// Token: 0x0600C1A9 RID: 49577 RVA: 0x0046C948 File Offset: 0x0046AB48
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyFear");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C1AA RID: 49578 RVA: 0x0046C960 File Offset: 0x0046AB60
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyFear" && this.GetChance().in100())
			{
				this.ParentObject.ParticleText("*shook off fear*", IComponent<GameObject>.ConsequentialColorChar(this.ParentObject, null), false, 1.5f, -8f);
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C1AB RID: 49579 RVA: 0x0046C9BC File Offset: 0x0046ABBC
		public void CheckRemoval(int Chances)
		{
			if (this.ParentObject.HasEffect<Terrified>())
			{
				int chance = this.GetChance();
				for (int i = 0; i < Chances; i++)
				{
					if (chance.in100())
					{
						this.ParentObject.RemoveEffect<Terrified>();
						return;
					}
				}
			}
		}

		// Token: 0x0600C1AC RID: 49580 RVA: 0x0046C9FE File Offset: 0x0046ABFE
		public int GetChance()
		{
			return this.ParentObject.Stat("Willpower", 0) - 10;
		}
	}
}
