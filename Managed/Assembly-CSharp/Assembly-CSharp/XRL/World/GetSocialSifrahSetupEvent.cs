using System;

namespace XRL.World
{
	// Token: 0x0200078D RID: 1933
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class GetSocialSifrahSetupEvent : PooledEvent<GetSocialSifrahSetupEvent>
	{
		// Token: 0x06004F19 RID: 20249 RVA: 0x0018278E File Offset: 0x0018098E
		public override int GetCascadeLevel()
		{
			return GetSocialSifrahSetupEvent.CascadeLevel;
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x00182795 File Offset: 0x00180995
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0018279E File Offset: 0x0018099E
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Interlocutor = null;
			this.Type = null;
			this.Difficulty = 0;
			this.Rating = 0;
			this.Turns = 0;
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x001827D0 File Offset: 0x001809D0
		public static GetSocialSifrahSetupEvent FromPool(GameObject Actor, GameObject Interlocutor, string Type, int Difficulty, int Rating, int Turns)
		{
			GetSocialSifrahSetupEvent getSocialSifrahSetupEvent = PooledEvent<GetSocialSifrahSetupEvent>.FromPool();
			getSocialSifrahSetupEvent.Actor = Actor;
			getSocialSifrahSetupEvent.Interlocutor = Interlocutor;
			getSocialSifrahSetupEvent.Type = Type;
			getSocialSifrahSetupEvent.Difficulty = Difficulty;
			getSocialSifrahSetupEvent.Rating = Rating;
			getSocialSifrahSetupEvent.Turns = Turns;
			return getSocialSifrahSetupEvent;
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x00182804 File Offset: 0x00180A04
		public static bool GetFor(GameObject Actor, GameObject Interlocutor, string Type, ref int Difficulty, ref int Rating, ref int Turns)
		{
			bool flag = true;
			if (Actor.WantEvent(PooledEvent<GetSocialSifrahSetupEvent>.ID, GetSocialSifrahSetupEvent.CascadeLevel))
			{
				GetSocialSifrahSetupEvent getSocialSifrahSetupEvent = GetSocialSifrahSetupEvent.FromPool(Actor, Interlocutor, Type, Difficulty, Rating, Turns);
				flag = Actor.HandleEvent(getSocialSifrahSetupEvent);
				Difficulty = getSocialSifrahSetupEvent.Difficulty;
				Rating = getSocialSifrahSetupEvent.Rating;
				Turns = getSocialSifrahSetupEvent.Turns;
			}
			return flag;
		}

		// Token: 0x040026CD RID: 9933
		public new static readonly int CascadeLevel = 3;

		// Token: 0x040026CE RID: 9934
		public GameObject Actor;

		// Token: 0x040026CF RID: 9935
		public GameObject Interlocutor;

		// Token: 0x040026D0 RID: 9936
		public string Type;

		// Token: 0x040026D1 RID: 9937
		public int Difficulty;

		// Token: 0x040026D2 RID: 9938
		public int Rating;

		// Token: 0x040026D3 RID: 9939
		public int Turns;
	}
}
