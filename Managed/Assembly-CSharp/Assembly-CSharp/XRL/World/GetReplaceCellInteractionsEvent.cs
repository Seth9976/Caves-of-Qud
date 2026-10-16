using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000782 RID: 1922
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class GetReplaceCellInteractionsEvent : PooledEvent<GetReplaceCellInteractionsEvent>
	{
		// Token: 0x06004ECA RID: 20170 RVA: 0x001816C5 File Offset: 0x0017F8C5
		public override int GetCascadeLevel()
		{
			return GetReplaceCellInteractionsEvent.CascadeLevel;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x001816CC File Offset: 0x0017F8CC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x001816D5 File Offset: 0x0017F8D5
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Objects.Clear();
			this.Priorities.Clear();
			this.Interactions.Clear();
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x00181708 File Offset: 0x0017F908
		public void Add(GameObject Object, int Priority, string Interaction)
		{
			if (GameObject.Validate(ref Object))
			{
				if (!this.Objects.Contains(Object))
				{
					this.Objects.Add(Object);
				}
				this.Priorities[Object] = Priority;
				this.Interactions[Object] = Interaction;
				return;
			}
			MetricsManager.LogError("tried to add replace cell interaction with invalid object");
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00181760 File Offset: 0x0017F960
		public int Sorter(GameObject A, GameObject B)
		{
			if (A == null || B == null)
			{
				return 0;
			}
			int num = this.Priorities[A].CompareTo(this.Priorities[B]);
			if (num != 0)
			{
				return -num;
			}
			return A.GetCachedDisplayNameForSort().CompareTo(B.GetCachedDisplayNameForSort());
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x001817B0 File Offset: 0x0017F9B0
		public static GetReplaceCellInteractionsEvent GetFor(GameObject Actor)
		{
			GetReplaceCellInteractionsEvent getReplaceCellInteractionsEvent = null;
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetReplaceCellInteractionsEvent>.ID, GetReplaceCellInteractionsEvent.CascadeLevel))
			{
				getReplaceCellInteractionsEvent = PooledEvent<GetReplaceCellInteractionsEvent>.FromPool();
				getReplaceCellInteractionsEvent.Actor = Actor;
				Actor.HandleEvent(getReplaceCellInteractionsEvent);
				if (getReplaceCellInteractionsEvent.Objects.Count > 1)
				{
					getReplaceCellInteractionsEvent.Objects.Sort(new Comparison<GameObject>(getReplaceCellInteractionsEvent.Sorter));
				}
			}
			return getReplaceCellInteractionsEvent;
		}

		// Token: 0x04002683 RID: 9859
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002684 RID: 9860
		public GameObject Actor;

		// Token: 0x04002685 RID: 9861
		public List<GameObject> Objects = new List<GameObject>();

		// Token: 0x04002686 RID: 9862
		public Dictionary<GameObject, int> Priorities = new Dictionary<GameObject, int>();

		// Token: 0x04002687 RID: 9863
		public Dictionary<GameObject, string> Interactions = new Dictionary<GameObject, string>();
	}
}
