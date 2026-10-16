using System;
using System.Collections.Generic;
using XRL.Collections;

namespace XRL.World
{
	// Token: 0x0200079F RID: 1951
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetTransitiveLocationEvent : PooledEvent<GetTransitiveLocationEvent>
	{
		// Token: 0x06004F8A RID: 20362 RVA: 0x001847DE File Offset: 0x001829DE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x001847E8 File Offset: 0x001829E8
		public override void Reset()
		{
			base.Reset();
			this.Type = "";
			this.Zone = null;
			this.Origin = null;
			this.Actor = null;
			this.Source = null;
			this.Locations.Clear();
			this.IngressType = null;
			this.EgressType = null;
			this.InteriorType = null;
			this.PullDownType = null;
			this.WorldMapType = null;
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x00184869 File Offset: 0x00182A69
		public Cell Destination
		{
			get
			{
				if (this.Locations.Count <= 0)
				{
					return null;
				}
				return this.Locations[0].Cell;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06004F8D RID: 20365 RVA: 0x0018488C File Offset: 0x00182A8C
		public GameObject Portal
		{
			get
			{
				if (this.Locations.Count <= 0)
				{
					return null;
				}
				return this.Locations[0].Portal;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x001848B0 File Offset: 0x00182AB0
		public bool IsIngress
		{
			get
			{
				bool flag = this.IngressType.GetValueOrDefault();
				if (this.IngressType == null)
				{
					flag = this.Type.HasDelimitedSubstring(',', "Ingress", StringComparison.Ordinal);
					this.IngressType = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x001848FC File Offset: 0x00182AFC
		public bool IsEgress
		{
			get
			{
				bool flag = this.EgressType.GetValueOrDefault();
				if (this.EgressType == null)
				{
					flag = this.Type.HasDelimitedSubstring(',', "Egress", StringComparison.Ordinal);
					this.EgressType = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x00184948 File Offset: 0x00182B48
		public bool IsInterior
		{
			get
			{
				bool flag = this.InteriorType.GetValueOrDefault();
				if (this.InteriorType == null)
				{
					flag = this.Type.HasDelimitedSubstring(',', "Interior", StringComparison.Ordinal);
					this.InteriorType = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06004F91 RID: 20369 RVA: 0x00184994 File Offset: 0x00182B94
		public bool IsPullDown
		{
			get
			{
				bool flag = this.PullDownType.GetValueOrDefault();
				if (this.PullDownType == null)
				{
					flag = this.Type.HasDelimitedSubstring(',', "PullDown", StringComparison.Ordinal);
					this.PullDownType = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x001849E0 File Offset: 0x00182BE0
		public bool IsWorldMap
		{
			get
			{
				bool flag = this.WorldMapType.GetValueOrDefault();
				if (this.WorldMapType == null)
				{
					flag = this.Type.HasDelimitedSubstring(',', "WorldMap", StringComparison.Ordinal);
					this.WorldMapType = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x00184A2C File Offset: 0x00182C2C
		public void AddLocation(Cell Cell, GameObject Portal = null, int Priority = 1000)
		{
			int num = this.Locations.Count;
			while (num > 0 && this.Locations[num - 1].Priority < Priority)
			{
				num--;
			}
			this.Locations.Insert(num, new GetTransitiveLocationEvent.Location(Cell, Portal, Priority));
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x00184A7C File Offset: 0x00182C7C
		public static void GetFor(Zone Zone, string Type, GameObject Actor, GameObject Source, Cell Origin, out Cell Destination, out GameObject Portal)
		{
			if (Zone == null)
			{
				Destination = null;
				Portal = null;
				return;
			}
			using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
			{
				Zone.GetObjectsThatWantEvent(PooledEvent<GetTransitiveLocationEvent>.ID, MinEvent.CascadeLevel, fromPool);
				if (GameObject.Validate(ref Source) && Source.WantEvent(PooledEvent<GetTransitiveLocationEvent>.ID, MinEvent.CascadeLevel) && !fromPool.Contains(Source))
				{
					fromPool.Add(Source);
				}
				if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetTransitiveLocationEvent>.ID, MinEvent.CascadeLevel) && !fromPool.Contains(Actor))
				{
					fromPool.Add(Actor);
				}
				if (fromPool.IsNullOrEmpty<GameObject>())
				{
					Destination = null;
					Portal = null;
				}
				else
				{
					GetTransitiveLocationEvent getTransitiveLocationEvent = PooledEvent<GetTransitiveLocationEvent>.FromPool();
					getTransitiveLocationEvent.Type = Type ?? "";
					getTransitiveLocationEvent.Zone = Zone;
					getTransitiveLocationEvent.Origin = Origin;
					getTransitiveLocationEvent.Actor = Actor;
					getTransitiveLocationEvent.Source = Source;
					using (PooledContainer<GameObject>.Enumerator enumerator = fromPool.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!enumerator.Current.HandleEvent(getTransitiveLocationEvent))
							{
								break;
							}
						}
					}
					Destination = getTransitiveLocationEvent.Destination;
					Portal = getTransitiveLocationEvent.Portal;
				}
			}
		}

		// Token: 0x04002747 RID: 10055
		public string Type = "";

		// Token: 0x04002748 RID: 10056
		public Zone Zone;

		// Token: 0x04002749 RID: 10057
		public Cell Origin;

		// Token: 0x0400274A RID: 10058
		public GameObject Actor;

		// Token: 0x0400274B RID: 10059
		public GameObject Source;

		// Token: 0x0400274C RID: 10060
		private List<GetTransitiveLocationEvent.Location> Locations = new List<GetTransitiveLocationEvent.Location>();

		// Token: 0x0400274D RID: 10061
		private bool? IngressType;

		// Token: 0x0400274E RID: 10062
		private bool? EgressType;

		// Token: 0x0400274F RID: 10063
		private bool? InteriorType;

		// Token: 0x04002750 RID: 10064
		private bool? PullDownType;

		// Token: 0x04002751 RID: 10065
		private bool? WorldMapType;

		// Token: 0x02001A20 RID: 6688
		private struct Location
		{
			// Token: 0x0600E639 RID: 58937 RVA: 0x00531C8D File Offset: 0x0052FE8D
			public Location(Cell Cell, GameObject Portal, int Priority)
			{
				this.Cell = Cell;
				this.Portal = Portal;
				this.Priority = Priority;
			}

			// Token: 0x0400682B RID: 26667
			public Cell Cell;

			// Token: 0x0400682C RID: 26668
			public GameObject Portal;

			// Token: 0x0400682D RID: 26669
			public int Priority;
		}
	}
}
