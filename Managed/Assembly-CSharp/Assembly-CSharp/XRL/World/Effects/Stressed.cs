using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D94 RID: 3476
	[Serializable]
	public class Stressed : Effect
	{
		// Token: 0x06008A73 RID: 35443 RVA: 0x00315BE0 File Offset: 0x00313DE0
		public Stressed()
		{
			this.DisplayName = "{{B|stressed}}";
		}

		// Token: 0x06008A74 RID: 35444 RVA: 0x00315BF3 File Offset: 0x00313DF3
		public Stressed(int Duration, int Level)
			: this()
		{
			this.Level = Level;
			this.Duration = Duration;
			this.Bonus = 17 + 7 * Level;
		}

		// Token: 0x06008A75 RID: 35445 RVA: 0x00315C15 File Offset: 0x00313E15
		public override int GetEffectType()
		{
			return 67108868;
		}

		// Token: 0x06008A76 RID: 35446 RVA: 0x00315C1C File Offset: 0x00313E1C
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008A77 RID: 35447 RVA: 0x00315C1F File Offset: 0x00313E1F
		public override string GetDetails()
		{
			return this.Bonus.Signed(false) + " Quickness";
		}

		// Token: 0x06008A78 RID: 35448 RVA: 0x00315C38 File Offset: 0x00313E38
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyStressed", "Event", this)))
			{
				return false;
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Your body flushes with adrenaline!", 'g', true);
			}
			Object.GetStat("Speed").Bonus += this.Bonus;
			return base.Apply(Object);
		}

		// Token: 0x06008A79 RID: 35449 RVA: 0x00315C98 File Offset: 0x00313E98
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Your adrenaline level returns to normal!", 'R', true);
			}
			Object.GetStat("Speed").Bonus -= this.Bonus;
		}

		// Token: 0x06008A7A RID: 35450 RVA: 0x00315CD3 File Offset: 0x00313ED3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06008A7B RID: 35451 RVA: 0x00315CF4 File Offset: 0x00313EF4
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (base.Object.OnWorldMap())
				{
					this.Duration = 0;
				}
				else if (this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A7C RID: 35452 RVA: 0x00315D44 File Offset: 0x00313F44
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration > 0)
			{
				this.Counter += 30 + 50 * this.Level;
				int num = 0;
				while (this.Counter > 100)
				{
					this.Counter -= 100;
					num++;
				}
				if (num > 0)
				{
					base.Object.TakeDamage(num, "from %t adrenal stress!", "Metabolic Unavoidable", null, null, base.Object, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A7D RID: 35453 RVA: 0x00315DCD File Offset: 0x00313FCD
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && XRLCore.CurrentFrame % 20 > 10)
			{
				E.RenderString = "\u0003";
				E.ColorString = "&r";
			}
			return true;
		}

		// Token: 0x04003A9B RID: 15003
		public int Bonus;

		// Token: 0x04003A9C RID: 15004
		public int Level;

		// Token: 0x04003A9D RID: 15005
		public int Counter;
	}
}
