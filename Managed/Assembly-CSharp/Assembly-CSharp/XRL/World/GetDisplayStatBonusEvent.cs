using System;

namespace XRL.World
{
	// Token: 0x02000745 RID: 1861
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetDisplayStatBonusEvent : PooledEvent<GetDisplayStatBonusEvent>
	{
		// Token: 0x06004D15 RID: 19733 RVA: 0x0017BF6D File Offset: 0x0017A16D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x0017BF76 File Offset: 0x0017A176
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Component = null;
			this.Stat = null;
			this.BaseAmount = 0;
			this.Amount = 0;
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x0017BFA4 File Offset: 0x0017A1A4
		public static int GetFor(GameObject Object, IComponent<GameObject> Component, string Stat, int BaseAmount)
		{
			int num = BaseAmount;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetDisplayStatBonus"))
			{
				Event @event = Event.New("GetDisplayStatBonus", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Component", Component);
				@event.SetParameter("Stat", Stat);
				@event.SetParameter("BaseAmount", BaseAmount);
				@event.SetParameter("Amount", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Amount", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetDisplayStatBonusEvent>.ID, MinEvent.CascadeLevel))
			{
				GetDisplayStatBonusEvent getDisplayStatBonusEvent = PooledEvent<GetDisplayStatBonusEvent>.FromPool();
				getDisplayStatBonusEvent.Object = Object;
				getDisplayStatBonusEvent.Component = Component;
				getDisplayStatBonusEvent.Stat = Stat;
				getDisplayStatBonusEvent.BaseAmount = BaseAmount;
				getDisplayStatBonusEvent.Amount = num;
				flag = Object.HandleEvent(getDisplayStatBonusEvent);
				num = getDisplayStatBonusEvent.Amount;
			}
			return num;
		}

		// Token: 0x04002553 RID: 9555
		public GameObject Object;

		// Token: 0x04002554 RID: 9556
		public IComponent<GameObject> Component;

		// Token: 0x04002555 RID: 9557
		public string Stat;

		// Token: 0x04002556 RID: 9558
		public int BaseAmount;

		// Token: 0x04002557 RID: 9559
		public int Amount;
	}
}
