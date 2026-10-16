using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000841 RID: 2113
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class RegenerateLimbEvent : ILimbRegenerationEvent
	{
		// Token: 0x060053EC RID: 21484 RVA: 0x00190ABB File Offset: 0x0018ECBB
		public RegenerateLimbEvent()
		{
			this.ID = RegenerateLimbEvent.ID;
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x00190ACE File Offset: 0x0018ECCE
		public static int CountPool()
		{
			if (RegenerateLimbEvent.Pool != null)
			{
				return RegenerateLimbEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x00190AE3 File Offset: 0x0018ECE3
		public static void ResetPool()
		{
			while (RegenerateLimbEvent.PoolCounter > 0)
			{
				RegenerateLimbEvent.Pool[--RegenerateLimbEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x00190B0B File Offset: 0x0018ED0B
		public static void ResetTo(ref RegenerateLimbEvent E)
		{
			MinEvent.ResetTo<RegenerateLimbEvent>(E, RegenerateLimbEvent.Pool, ref RegenerateLimbEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x00190B21 File Offset: 0x0018ED21
		public static RegenerateLimbEvent FromPool()
		{
			return MinEvent.FromPool<RegenerateLimbEvent>(ref RegenerateLimbEvent.Pool, ref RegenerateLimbEvent.PoolCounter, 8192);
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x00190B37 File Offset: 0x0018ED37
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x00190B4C File Offset: 0x0018ED4C
		public static bool Send(GameObject Object, GameObject Actor = null, GameObject Source = null, bool Whole = false, bool All = false, bool IncludeMinor = true, bool Voluntary = true, int? ParentID = null, int? Category = null, int[] Categories = null, int? ExceptCategory = null, int[] ExceptCategories = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("RegenerateLimb"))
			{
				Event @event = Event.New("RegenerateLimb", 0, 0, 0);
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
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(RegenerateLimbEvent.ID, ILimbRegenerationEvent.CascadeLevel))
			{
				RegenerateLimbEvent regenerateLimbEvent = RegenerateLimbEvent.FromPool();
				regenerateLimbEvent.Object = Object;
				regenerateLimbEvent.Actor = Actor;
				regenerateLimbEvent.Source = Source;
				regenerateLimbEvent.ParentID = ParentID;
				regenerateLimbEvent.Category = Category;
				regenerateLimbEvent.Categories = Categories;
				regenerateLimbEvent.ExceptCategory = ExceptCategory;
				regenerateLimbEvent.ExceptCategories = ExceptCategories;
				regenerateLimbEvent.Whole = Whole;
				regenerateLimbEvent.All = All;
				regenerateLimbEvent.IncludeMinor = IncludeMinor;
				regenerateLimbEvent.Voluntary = Voluntary;
				flag = Object.HandleEvent(regenerateLimbEvent);
			}
			return !flag;
		}

		// Token: 0x04002A2D RID: 10797
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(RegenerateLimbEvent), null, new MinEvent.EventPoolCount(RegenerateLimbEvent.CountPool), new MinEvent.EventPoolReset(RegenerateLimbEvent.ResetPool));

		// Token: 0x04002A2E RID: 10798
		private static List<RegenerateLimbEvent> Pool;

		// Token: 0x04002A2F RID: 10799
		private static int PoolCounter;
	}
}
