using System;

namespace XRL.World
{
	// Token: 0x02000722 RID: 1826
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GenericRatingEvent : PooledEvent<GenericRatingEvent>
	{
		// Token: 0x06004BE2 RID: 19426 RVA: 0x001780B9 File Offset: 0x001762B9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x001780C2 File Offset: 0x001762C2
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

		// Token: 0x06004BE4 RID: 19428 RVA: 0x001780FC File Offset: 0x001762FC
		public static int GetFor(GameObject Object, string Type, GameObject Subject = null, GameObject Source = null, int Level = 0, int BaseRating = 0)
		{
			bool flag = true;
			int num = BaseRating;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GenericRating"))
			{
				Event @event = Event.New("GenericRating", 0, 0, 0);
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
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GenericRatingEvent>.ID, MinEvent.CascadeLevel))
			{
				GenericRatingEvent genericRatingEvent = PooledEvent<GenericRatingEvent>.FromPool();
				genericRatingEvent.Object = Object;
				genericRatingEvent.Subject = Subject;
				genericRatingEvent.Source = Source;
				genericRatingEvent.Type = Type;
				genericRatingEvent.Level = Level;
				genericRatingEvent.BaseRating = BaseRating;
				genericRatingEvent.Rating = num;
				flag = Object.HandleEvent(genericRatingEvent);
				num = genericRatingEvent.Rating;
			}
			return num;
		}

		// Token: 0x040024AF RID: 9391
		public GameObject Object;

		// Token: 0x040024B0 RID: 9392
		public GameObject Subject;

		// Token: 0x040024B1 RID: 9393
		public GameObject Source;

		// Token: 0x040024B2 RID: 9394
		public string Type;

		// Token: 0x040024B3 RID: 9395
		public int Level;

		// Token: 0x040024B4 RID: 9396
		public int BaseRating;

		// Token: 0x040024B5 RID: 9397
		public int Rating;
	}
}
