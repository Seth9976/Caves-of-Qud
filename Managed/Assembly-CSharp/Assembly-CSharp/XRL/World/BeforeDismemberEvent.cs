using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000691 RID: 1681
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeDismemberEvent : PooledEvent<BeforeDismemberEvent>
	{
		// Token: 0x060047E7 RID: 18407 RVA: 0x0016AE73 File Offset: 0x00169073
		public override int GetCascadeLevel()
		{
			return BeforeDismemberEvent.CascadeLevel;
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x0016AE7A File Offset: 0x0016907A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0016AE83 File Offset: 0x00169083
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Perspective = null;
			this.Part = null;
			this.Where = null;
			this.Silent = false;
			this.Obliterate = false;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x0016AEBC File Offset: 0x001690BC
		public bool IsActor
		{
			get
			{
				return this.Actor != null && this.Actor == this.Perspective;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060047EB RID: 18411 RVA: 0x0016AED6 File Offset: 0x001690D6
		public bool IsObject
		{
			get
			{
				return this.Object != null && this.Object == this.Perspective;
			}
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0016AEF0 File Offset: 0x001690F0
		public static bool Check(GameObject Actor, GameObject Object, BodyPart Part, IInventory Where = null, bool Silent = false, bool Obliterate = false)
		{
			bool flag;
			using (BeforeDismemberEvent beforeDismemberEvent = PooledEvent<BeforeDismemberEvent>.FromPool())
			{
				beforeDismemberEvent.Actor = Actor;
				beforeDismemberEvent.Object = Object;
				beforeDismemberEvent.Part = Part;
				beforeDismemberEvent.Where = Where;
				beforeDismemberEvent.Silent = Silent;
				beforeDismemberEvent.Obliterate = Obliterate;
				beforeDismemberEvent.Perspective = Actor;
				if (Actor != null && Actor != Object && !Actor.HandleEvent(beforeDismemberEvent))
				{
					flag = false;
				}
				else
				{
					beforeDismemberEvent.Perspective = Object;
					if (!Object.HandleEvent(beforeDismemberEvent))
					{
						flag = false;
					}
					else
					{
						beforeDismemberEvent.Perspective = null;
						XRLGame game = The.Game;
						flag = game == null || game.HandleEvent(beforeDismemberEvent);
					}
				}
			}
			return flag;
		}

		// Token: 0x04002245 RID: 8773
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002246 RID: 8774
		public GameObject Actor;

		// Token: 0x04002247 RID: 8775
		public GameObject Object;

		// Token: 0x04002248 RID: 8776
		public GameObject Perspective;

		// Token: 0x04002249 RID: 8777
		public BodyPart Part;

		// Token: 0x0400224A RID: 8778
		public IInventory Where;

		// Token: 0x0400224B RID: 8779
		public bool Silent;

		// Token: 0x0400224C RID: 8780
		public bool Obliterate;
	}
}
