using System;
using System.Text;

namespace XRL.World
{
	// Token: 0x02000694 RID: 1684
	[GameEvent(Cascade = 17)]
	public class BeforeLevelGainedEvent : PassEvent<BeforeLevelGainedEvent>
	{
		// Token: 0x060047FA RID: 18426 RVA: 0x0016B0FD File Offset: 0x001692FD
		public override int GetCascadeLevel()
		{
			return BeforeLevelGainedEvent.CascadeLevel;
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x0016B104 File Offset: 0x00169304
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x0016B10D File Offset: 0x0016930D
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Kill = null;
			this.InfluencedBy = null;
			this.Message.Clear();
			this.Level = 0;
			this.Detail = false;
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0016B144 File Offset: 0x00169344
		public static BeforeLevelGainedEvent FromPool(GameObject Actor, GameObject Kill = null, GameObject InfluencedBy = null, int Level = 0, bool Detail = false)
		{
			BeforeLevelGainedEvent beforeLevelGainedEvent = PooledEvent<BeforeLevelGainedEvent>.FromPool();
			beforeLevelGainedEvent.Actor = Actor;
			beforeLevelGainedEvent.Kill = Kill;
			beforeLevelGainedEvent.InfluencedBy = InfluencedBy;
			beforeLevelGainedEvent.Level = Level;
			beforeLevelGainedEvent.Detail = Detail;
			return beforeLevelGainedEvent;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x0016B16F File Offset: 0x0016936F
		public bool Check()
		{
			if (this.Actor.HandleEvent(this) && The.Game.HandleEvent(this))
			{
				base.Postprocess(25);
				return true;
			}
			return false;
		}

		// Token: 0x04002256 RID: 8790
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002257 RID: 8791
		public GameObject Actor;

		// Token: 0x04002258 RID: 8792
		public GameObject Kill;

		// Token: 0x04002259 RID: 8793
		public GameObject InfluencedBy;

		// Token: 0x0400225A RID: 8794
		public StringBuilder Message = new StringBuilder();

		// Token: 0x0400225B RID: 8795
		public int Level;

		// Token: 0x0400225C RID: 8796
		public bool Detail;
	}
}
