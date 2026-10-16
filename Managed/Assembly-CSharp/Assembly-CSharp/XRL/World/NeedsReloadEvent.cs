using System;

namespace XRL.World
{
	// Token: 0x0200080E RID: 2062
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Singleton)]
	public class NeedsReloadEvent : SingletonEvent<NeedsReloadEvent>
	{
		// Token: 0x06005252 RID: 21074 RVA: 0x0018C591 File Offset: 0x0018A791
		public override int GetCascadeLevel()
		{
			return NeedsReloadEvent.CascadeLevel;
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0018C598 File Offset: 0x0018A798
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x0018C5A1 File Offset: 0x0018A7A1
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Weapon = null;
			this.Skip = null;
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0018C5C0 File Offset: 0x0018A7C0
		public static bool Check(GameObject Actor, IComponent<GameObject> Skip = null)
		{
			if (Actor.WantEvent(SingletonEvent<NeedsReloadEvent>.ID, NeedsReloadEvent.CascadeLevel))
			{
				SingletonEvent<NeedsReloadEvent>.Instance.Reset();
				SingletonEvent<NeedsReloadEvent>.Instance.Actor = Actor;
				SingletonEvent<NeedsReloadEvent>.Instance.Skip = Skip;
				if (!Actor.HandleEvent(SingletonEvent<NeedsReloadEvent>.Instance))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x0018C610 File Offset: 0x0018A810
		public static bool Check(GameObject Actor, GameObject Weapon, IComponent<GameObject> Skip = null)
		{
			if (Weapon.WantEvent(SingletonEvent<NeedsReloadEvent>.ID, NeedsReloadEvent.CascadeLevel))
			{
				SingletonEvent<NeedsReloadEvent>.Instance.Reset();
				SingletonEvent<NeedsReloadEvent>.Instance.Actor = Actor;
				SingletonEvent<NeedsReloadEvent>.Instance.Weapon = Weapon;
				SingletonEvent<NeedsReloadEvent>.Instance.Skip = Skip;
				if (!Weapon.HandleEvent(SingletonEvent<NeedsReloadEvent>.Instance))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400295D RID: 10589
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400295E RID: 10590
		public GameObject Actor;

		// Token: 0x0400295F RID: 10591
		public GameObject Weapon;

		// Token: 0x04002960 RID: 10592
		public IComponent<GameObject> Skip;
	}
}
