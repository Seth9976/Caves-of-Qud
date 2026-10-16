using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x0200065C RID: 1628
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AfterDismemberEvent : PooledEvent<AfterDismemberEvent>
	{
		// Token: 0x0600465E RID: 18014 RVA: 0x0016666B File Offset: 0x0016486B
		public override int GetCascadeLevel()
		{
			return AfterDismemberEvent.CascadeLevel;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x00166672 File Offset: 0x00164872
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x0016667B File Offset: 0x0016487B
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Limb = null;
			this.Perspective = null;
			this.Part = null;
			this.Where = null;
			this.Silent = false;
			this.Obliterate = false;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x001666BB File Offset: 0x001648BB
		public bool IsActor
		{
			get
			{
				return this.Actor != null && this.Actor == this.Perspective;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x001666D5 File Offset: 0x001648D5
		public bool IsObject
		{
			get
			{
				return this.Object != null && this.Object == this.Perspective;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x001666EF File Offset: 0x001648EF
		public bool IsLimb
		{
			get
			{
				return this.Limb != null && this.Limb == this.Perspective;
			}
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x0016670C File Offset: 0x0016490C
		public static void Send(GameObject Actor, GameObject Object, GameObject Limb, BodyPart Part, IInventory Where = null, bool Silent = false, bool Obliterate = false)
		{
			using (AfterDismemberEvent afterDismemberEvent = PooledEvent<AfterDismemberEvent>.FromPool())
			{
				afterDismemberEvent.Actor = Actor;
				afterDismemberEvent.Object = Object;
				afterDismemberEvent.Limb = Limb;
				afterDismemberEvent.Part = Part;
				afterDismemberEvent.Where = Where;
				afterDismemberEvent.Silent = Silent;
				afterDismemberEvent.Obliterate = Obliterate;
				afterDismemberEvent.Perspective = Actor;
				if (Actor != null && Actor != Object)
				{
					Actor.HandleEvent(afterDismemberEvent);
				}
				afterDismemberEvent.Perspective = Object;
				if (Object != null)
				{
					Object.HandleEvent(afterDismemberEvent);
				}
				afterDismemberEvent.Perspective = Limb;
				if (Limb != null)
				{
					Limb.HandleEvent(afterDismemberEvent);
				}
				afterDismemberEvent.Perspective = null;
				XRLGame game = The.Game;
				if (game != null)
				{
					game.HandleEvent(afterDismemberEvent);
				}
			}
		}

		// Token: 0x04002168 RID: 8552
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002169 RID: 8553
		public GameObject Actor;

		// Token: 0x0400216A RID: 8554
		public GameObject Object;

		// Token: 0x0400216B RID: 8555
		public GameObject Limb;

		// Token: 0x0400216C RID: 8556
		public GameObject Perspective;

		// Token: 0x0400216D RID: 8557
		public BodyPart Part;

		// Token: 0x0400216E RID: 8558
		public IInventory Where;

		// Token: 0x0400216F RID: 8559
		public bool Silent;

		// Token: 0x04002170 RID: 8560
		public bool Obliterate;
	}
}
