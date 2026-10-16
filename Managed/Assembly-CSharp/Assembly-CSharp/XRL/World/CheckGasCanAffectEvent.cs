using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006DB RID: 1755
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Pool)]
	public class CheckGasCanAffectEvent : PooledEvent<CheckGasCanAffectEvent>
	{
		// Token: 0x060049F9 RID: 18937 RVA: 0x00172F5E File Offset: 0x0017115E
		public override int GetCascadeLevel()
		{
			return CheckGasCanAffectEvent.CascadeLevel;
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x00172F65 File Offset: 0x00171165
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x00172F6E File Offset: 0x0017116E
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.GasObject = null;
			this.Gas = null;
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x00172F8C File Offset: 0x0017118C
		public static bool Check(GameObject Object, GameObject GasObject, Gas Gas = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && GameObject.Validate(ref GasObject))
			{
				CheckGasCanAffectEvent checkGasCanAffectEvent = PooledEvent<CheckGasCanAffectEvent>.FromPool();
				checkGasCanAffectEvent.Object = Object;
				checkGasCanAffectEvent.GasObject = GasObject;
				checkGasCanAffectEvent.Gas = Gas ?? GasObject.GetPart<Gas>();
				flag = Object.HandleEvent(checkGasCanAffectEvent);
			}
			return flag;
		}

		// Token: 0x04002388 RID: 9096
		public new static readonly int CascadeLevel;

		// Token: 0x04002389 RID: 9097
		public GameObject Object;

		// Token: 0x0400238A RID: 9098
		public GameObject GasObject;

		// Token: 0x0400238B RID: 9099
		public Gas Gas;
	}
}
