using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C5E RID: 3166
	[Serializable]
	public class NoThirst_ProceduralCookingTriggeredAction_Effect : BasicTriggeredCookingEffect
	{
		// Token: 0x060080D0 RID: 32976 RVA: 0x002F536C File Offset: 0x002F356C
		public override string GetDetails()
		{
			return "@they don't thirst.";
		}

		// Token: 0x060080D1 RID: 32977 RVA: 0x002F5373 File Offset: 0x002F3573
		public NoThirst_ProceduralCookingTriggeredAction_Effect()
		{
			this.Duration = 1;
			this.DisplayName = null;
		}

		// Token: 0x060080D2 RID: 32978 RVA: 0x002F5389 File Offset: 0x002F3589
		public override void ApplyEffect(GameObject Object)
		{
			this.Duration = 1200;
			Object.RegisterEffectEvent(this, "CalculatingThirst");
			base.ApplyEffect(Object);
		}

		// Token: 0x060080D3 RID: 32979 RVA: 0x002F53A9 File Offset: 0x002F35A9
		public override void RemoveEffect(GameObject Object)
		{
			Object.UnregisterEffectEvent(this, "CalculatingThirst");
			base.RemoveEffect(Object);
		}

		// Token: 0x060080D4 RID: 32980 RVA: 0x002F53BE File Offset: 0x002F35BE
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CalculatingThirst")
			{
				E.SetParameter("Amount", 0);
				return false;
			}
			return base.FireEvent(E);
		}
	}
}
