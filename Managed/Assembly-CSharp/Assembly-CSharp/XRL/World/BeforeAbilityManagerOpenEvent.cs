using System;

namespace XRL.World
{
	// Token: 0x02000686 RID: 1670
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class BeforeAbilityManagerOpenEvent : SingletonEvent<BeforeAbilityManagerOpenEvent>
	{
		// Token: 0x06004790 RID: 18320 RVA: 0x00169D24 File Offset: 0x00167F24
		public override int GetCascadeLevel()
		{
			return BeforeAbilityManagerOpenEvent.CascadeLevel;
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x00169D2B File Offset: 0x00167F2B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00169D34 File Offset: 0x00167F34
		public static bool Check(GameObject Object)
		{
			return Object.HandleEvent(SingletonEvent<BeforeAbilityManagerOpenEvent>.Instance);
		}

		// Token: 0x04002221 RID: 8737
		public new static readonly int CascadeLevel = 15;
	}
}
