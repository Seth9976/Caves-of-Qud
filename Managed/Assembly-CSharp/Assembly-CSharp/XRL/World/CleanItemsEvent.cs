using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006E6 RID: 1766
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class CleanItemsEvent : PooledEvent<CleanItemsEvent>
	{
		// Token: 0x06004A2B RID: 18987 RVA: 0x001739E0 File Offset: 0x00171BE0
		public override int GetCascadeLevel()
		{
			return CleanItemsEvent.CascadeLevel;
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x001739E7 File Offset: 0x00171BE7
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x001739F0 File Offset: 0x00171BF0
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.CascadeFrom = null;
			this.Using = null;
			this.Objects.Clear();
			this.Types.Clear();
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00173A23 File Offset: 0x00171C23
		public static CleanItemsEvent FromPool(GameObject Actor, GameObject CascadeFrom, GameObject Using)
		{
			CleanItemsEvent cleanItemsEvent = PooledEvent<CleanItemsEvent>.FromPool();
			cleanItemsEvent.Actor = Actor;
			cleanItemsEvent.CascadeFrom = CascadeFrom;
			cleanItemsEvent.Using = Using;
			cleanItemsEvent.Objects.Clear();
			cleanItemsEvent.Types.Clear();
			return cleanItemsEvent;
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x00173A55 File Offset: 0x00171C55
		public void RegisterObject(GameObject obj)
		{
			if (obj != null && !this.Objects.Contains(obj))
			{
				this.Objects.Add(obj);
			}
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x00173A74 File Offset: 0x00171C74
		public void RegisterType(string type)
		{
			if (!string.IsNullOrEmpty(type) && !this.Types.Contains(type))
			{
				this.Types.Add(type);
			}
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x00173A98 File Offset: 0x00171C98
		public static bool PerformFor(GameObject Actor, GameObject CascadeFrom, GameObject Using, out List<GameObject> Objects, out List<string> Types)
		{
			if (GameObject.Validate(ref CascadeFrom) && CascadeFrom.WantEvent(PooledEvent<CleanItemsEvent>.ID, CleanItemsEvent.CascadeLevel))
			{
				CleanItemsEvent cleanItemsEvent = CleanItemsEvent.FromPool(Actor, CascadeFrom, Using);
				CascadeFrom.HandleEvent(cleanItemsEvent);
				Objects = cleanItemsEvent.Objects;
				Types = cleanItemsEvent.Types;
				return true;
			}
			Objects = null;
			Types = null;
			return false;
		}

		// Token: 0x040023AC RID: 9132
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040023AD RID: 9133
		public GameObject Actor;

		// Token: 0x040023AE RID: 9134
		public GameObject CascadeFrom;

		// Token: 0x040023AF RID: 9135
		public GameObject Using;

		// Token: 0x040023B0 RID: 9136
		public List<GameObject> Objects = new List<GameObject>();

		// Token: 0x040023B1 RID: 9137
		public List<string> Types = new List<string>();
	}
}
