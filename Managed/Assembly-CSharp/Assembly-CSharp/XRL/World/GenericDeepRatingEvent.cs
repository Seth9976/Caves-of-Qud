using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200071F RID: 1823
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 15)]
	public class GenericDeepRatingEvent : MinEvent
	{
		// Token: 0x06004BD0 RID: 19408 RVA: 0x00177D7A File Offset: 0x00175F7A
		public GenericDeepRatingEvent()
		{
			this.ID = GenericDeepRatingEvent.ID;
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x00177D8D File Offset: 0x00175F8D
		public override int GetCascadeLevel()
		{
			return GenericDeepRatingEvent.CascadeLevel;
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x00177D94 File Offset: 0x00175F94
		public static int CountPool()
		{
			if (GenericDeepRatingEvent.Pool != null)
			{
				return GenericDeepRatingEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x00177DA9 File Offset: 0x00175FA9
		public static void ResetPool()
		{
			while (GenericDeepRatingEvent.PoolCounter > 0)
			{
				GenericDeepRatingEvent.Pool[--GenericDeepRatingEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x00177DD1 File Offset: 0x00175FD1
		public static void ResetTo(ref GenericDeepRatingEvent E)
		{
			MinEvent.ResetTo<GenericDeepRatingEvent>(E, GenericDeepRatingEvent.Pool, ref GenericDeepRatingEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00177DE7 File Offset: 0x00175FE7
		public static GenericDeepRatingEvent FromPool()
		{
			return MinEvent.FromPool<GenericDeepRatingEvent>(ref GenericDeepRatingEvent.Pool, ref GenericDeepRatingEvent.PoolCounter, 8192);
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00177DFD File Offset: 0x00175FFD
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00177E06 File Offset: 0x00176006
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Subject = null;
			this.Source = null;
			this.Type = null;
			this.Level = 0;
			this.BaseRating = 0;
			this.Rating = 0;
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x00177E40 File Offset: 0x00176040
		public static int GetFor(GameObject Object, string Type, GameObject Subject = null, GameObject Source = null, int Level = 0, int BaseRating = 0)
		{
			bool flag = true;
			int num = BaseRating;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GenericDeepRating"))
			{
				Event @event = Event.New("GenericDeepRating", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("Source", Source);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Level", Level);
				@event.SetParameter("BaseRating", BaseRating);
				@event.SetParameter("Rating", num);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Rating", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(GenericDeepRatingEvent.ID, GenericDeepRatingEvent.CascadeLevel))
			{
				GenericDeepRatingEvent genericDeepRatingEvent = GenericDeepRatingEvent.FromPool();
				genericDeepRatingEvent.Object = Object;
				genericDeepRatingEvent.Subject = Subject;
				genericDeepRatingEvent.Source = Source;
				genericDeepRatingEvent.Type = Type;
				genericDeepRatingEvent.Level = Level;
				genericDeepRatingEvent.BaseRating = BaseRating;
				genericDeepRatingEvent.Rating = num;
				flag = Object.HandleEvent(genericDeepRatingEvent);
				num = genericDeepRatingEvent.Rating;
			}
			return num;
		}

		// Token: 0x04002499 RID: 9369
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GenericDeepRatingEvent), null, new MinEvent.EventPoolCount(GenericDeepRatingEvent.CountPool), new MinEvent.EventPoolReset(GenericDeepRatingEvent.ResetPool));

		// Token: 0x0400249A RID: 9370
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400249B RID: 9371
		private static List<GenericDeepRatingEvent> Pool;

		// Token: 0x0400249C RID: 9372
		private static int PoolCounter;

		// Token: 0x0400249D RID: 9373
		public GameObject Object;

		// Token: 0x0400249E RID: 9374
		public GameObject Subject;

		// Token: 0x0400249F RID: 9375
		public GameObject Source;

		// Token: 0x040024A0 RID: 9376
		public string Type;

		// Token: 0x040024A1 RID: 9377
		public int Level;

		// Token: 0x040024A2 RID: 9378
		public int BaseRating;

		// Token: 0x040024A3 RID: 9379
		public int Rating;
	}
}
