using System;

namespace XRL.World
{
	// Token: 0x02000727 RID: 1831
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetApparentSpeciesEvent : PooledEvent<GetApparentSpeciesEvent>
	{
		// Token: 0x06004BFE RID: 19454 RVA: 0x001787AA File Offset: 0x001769AA
		public override int GetCascadeLevel()
		{
			return GetApparentSpeciesEvent.CascadeLevel;
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x001787B1 File Offset: 0x001769B1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x001787BA File Offset: 0x001769BA
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Viewer = null;
			this.Species = null;
			this.ApparentSpecies = null;
			this.Priority = 0;
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x001787E8 File Offset: 0x001769E8
		public static string GetFor(GameObject Object, GameObject Viewer = null, string Species = null)
		{
			if (Species == null)
			{
				Species = ((Object != null) ? Object.GetSpecies() : null);
			}
			string text = Species;
			int num = 0;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetApparentSpecies"))
			{
				Event @event = Event.New("GetApparentSpecies", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Viewer", Viewer);
				@event.SetParameter("Species", Species);
				@event.SetParameter("ApparentSpecies", text);
				@event.SetParameter("Priority", num);
				flag = Object.FireEvent(@event);
				text = @event.GetStringParameter("ApparentSpecies", null);
				num = @event.GetIntParameter("Priority", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetApparentSpeciesEvent>.ID, GetApparentSpeciesEvent.CascadeLevel))
			{
				GetApparentSpeciesEvent getApparentSpeciesEvent = PooledEvent<GetApparentSpeciesEvent>.FromPool();
				getApparentSpeciesEvent.Object = Object;
				getApparentSpeciesEvent.Viewer = Viewer;
				getApparentSpeciesEvent.Species = Species;
				getApparentSpeciesEvent.ApparentSpecies = text;
				getApparentSpeciesEvent.Priority = num;
				flag = Object.HandleEvent(getApparentSpeciesEvent);
				text = getApparentSpeciesEvent.ApparentSpecies;
				num = getApparentSpeciesEvent.Priority;
			}
			return text;
		}

		// Token: 0x040024C3 RID: 9411
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040024C4 RID: 9412
		public GameObject Object;

		// Token: 0x040024C5 RID: 9413
		public GameObject Viewer;

		// Token: 0x040024C6 RID: 9414
		public string Species;

		// Token: 0x040024C7 RID: 9415
		public string ApparentSpecies;

		// Token: 0x040024C8 RID: 9416
		public int Priority;
	}
}
