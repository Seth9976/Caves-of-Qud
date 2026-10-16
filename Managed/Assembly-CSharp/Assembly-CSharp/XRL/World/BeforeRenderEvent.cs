using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200069E RID: 1694
	[GameEvent(Cache = MinEvent.Cache.Singleton, Cascade = 273)]
	public class BeforeRenderEvent : MinEvent
	{
		// Token: 0x0600484C RID: 18508 RVA: 0x0016BE5B File Offset: 0x0016A05B
		public BeforeRenderEvent()
		{
			this.ID = BeforeRenderEvent.ID;
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x0016BE80 File Offset: 0x0016A080
		public override int GetCascadeLevel()
		{
			return BeforeRenderEvent.CascadeLevel;
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0016BE87 File Offset: 0x0016A087
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x0016BE90 File Offset: 0x0016A090
		public override void Reset()
		{
			base.Reset();
			this.Pass = 1;
			this.AfterHandlers.Clear();
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x0016BEAC File Offset: 0x0016A0AC
		public static bool Send(Zone Z)
		{
			BeforeRenderEvent instance = BeforeRenderEvent.Instance;
			try
			{
				Z.HandleEvent(instance);
				if (instance.AfterHandlers.Count > 0)
				{
					instance.Pass = 2;
					foreach (IEventHandler eventHandler in instance.AfterHandlers)
					{
						eventHandler.HandleEvent(instance);
					}
				}
				TutorialManager.BeforeRenderEvent();
			}
			finally
			{
				instance.Reset();
			}
			return true;
		}

		// Token: 0x0400228A RID: 8842
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeRenderEvent), null, null, null);

		// Token: 0x0400228B RID: 8843
		public new static readonly int CascadeLevel = 273;

		// Token: 0x0400228C RID: 8844
		public static readonly BeforeRenderEvent Instance = new BeforeRenderEvent();

		// Token: 0x0400228D RID: 8845
		public int Pass = 1;

		// Token: 0x0400228E RID: 8846
		public List<IEventHandler> AfterHandlers = new List<IEventHandler>();
	}
}
