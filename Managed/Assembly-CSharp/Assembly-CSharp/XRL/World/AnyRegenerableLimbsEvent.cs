using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200067A RID: 1658
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AnyRegenerableLimbsEvent : ILimbRegenerationEvent
	{
		// Token: 0x06004733 RID: 18227 RVA: 0x00168ABE File Offset: 0x00166CBE
		public AnyRegenerableLimbsEvent()
		{
			this.ID = AnyRegenerableLimbsEvent.ID;
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x00168AD1 File Offset: 0x00166CD1
		public static int CountPool()
		{
			if (AnyRegenerableLimbsEvent.Pool != null)
			{
				return AnyRegenerableLimbsEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x00168AE6 File Offset: 0x00166CE6
		public static void ResetPool()
		{
			while (AnyRegenerableLimbsEvent.PoolCounter > 0)
			{
				AnyRegenerableLimbsEvent.Pool[--AnyRegenerableLimbsEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x00168B0E File Offset: 0x00166D0E
		public static void ResetTo(ref AnyRegenerableLimbsEvent E)
		{
			MinEvent.ResetTo<AnyRegenerableLimbsEvent>(E, AnyRegenerableLimbsEvent.Pool, ref AnyRegenerableLimbsEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x00168B24 File Offset: 0x00166D24
		public static AnyRegenerableLimbsEvent FromPool()
		{
			return MinEvent.FromPool<AnyRegenerableLimbsEvent>(ref AnyRegenerableLimbsEvent.Pool, ref AnyRegenerableLimbsEvent.PoolCounter, 8192);
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x00168B3A File Offset: 0x00166D3A
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x00168B50 File Offset: 0x00166D50
		public static bool Send(GameObject Object, GameObject Actor = null, GameObject Source = null, bool Whole = false, bool All = false, bool IncludeMinor = true, bool Voluntary = true, int? ParentID = null, int? Category = null, int[] Categories = null, int? ExceptCategory = null, int[] ExceptCategories = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("AnyRegenerableLimbs"))
			{
				Event @event = Event.New("AnyRegenerableLimbs", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Source", Source);
				@event.SetParameter("ParentID", ParentID);
				@event.SetParameter("Category", Category);
				@event.SetParameter("Categories", Categories);
				@event.SetParameter("ExceptCategory", ExceptCategory);
				@event.SetParameter("ExceptCategories", ExceptCategories);
				@event.SetFlag("Whole", Whole);
				@event.SetFlag("All", All);
				@event.SetFlag("IncludeMinor", IncludeMinor);
				@event.SetFlag("Voluntary", Voluntary);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(AnyRegenerableLimbsEvent.ID, ILimbRegenerationEvent.CascadeLevel))
			{
				AnyRegenerableLimbsEvent anyRegenerableLimbsEvent = AnyRegenerableLimbsEvent.FromPool();
				anyRegenerableLimbsEvent.Object = Object;
				anyRegenerableLimbsEvent.Actor = Actor;
				anyRegenerableLimbsEvent.Source = Source;
				anyRegenerableLimbsEvent.ParentID = ParentID;
				anyRegenerableLimbsEvent.Category = Category;
				anyRegenerableLimbsEvent.Categories = Categories;
				anyRegenerableLimbsEvent.ExceptCategory = ExceptCategory;
				anyRegenerableLimbsEvent.ExceptCategories = ExceptCategories;
				anyRegenerableLimbsEvent.Whole = Whole;
				anyRegenerableLimbsEvent.All = All;
				anyRegenerableLimbsEvent.IncludeMinor = IncludeMinor;
				anyRegenerableLimbsEvent.Voluntary = Voluntary;
				flag = Object.HandleEvent(anyRegenerableLimbsEvent);
			}
			return !flag;
		}

		// Token: 0x040021EF RID: 8687
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AnyRegenerableLimbsEvent), null, new MinEvent.EventPoolCount(AnyRegenerableLimbsEvent.CountPool), new MinEvent.EventPoolReset(AnyRegenerableLimbsEvent.ResetPool));

		// Token: 0x040021F0 RID: 8688
		private static List<AnyRegenerableLimbsEvent> Pool;

		// Token: 0x040021F1 RID: 8689
		private static int PoolCounter;
	}
}
