using System;
using System.Collections.Generic;
using XRL.Names;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A0C RID: 2572
	public class EyelessKingCrabHero1 : IObjectBuilder
	{
		// Token: 0x06006D11 RID: 27921 RVA: 0x00248160 File Offset: 0x00246360
		public override void Apply(GameObject Object, string Context)
		{
			string text = NameMaker.MakeTitle(Object, null, null, null, null, null, null, null, null, null, "Hero", null, true, null, null);
			Object.GiveProperName(null, false, "Hero", true, null, null, null, null);
			Object.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
			if (!text.IsNullOrEmpty())
			{
				Object.RequirePart<Titles>(false).AddTitle(text, -40);
				if (text.Contains("many-legged"))
				{
					Object.RequirePart<Mutations>(false).AddMutation(new MultipleLegs(), 5, true);
				}
				if (text.Contains("one-clawed"))
				{
					Body body = Object.Body;
					if (body != null)
					{
						List<BodyPart> part = body.GetPart("Hand");
						if (part != null)
						{
							BodyPart randomElement = part.GetRandomElement<BodyPart>(null);
							if (randomElement != null)
							{
								GameObject gameObject = randomElement.Dismember(false);
								if (gameObject != null)
								{
									gameObject.Destroy(null, false, false, null);
								}
							}
						}
					}
				}
				if (text.Contains("massive"))
				{
					Object.AddBaseStat("MoveSpeed", Stat.Random(10, 20));
					Object.AddBaseStat("Hitpoints", Stat.Random(10, 20));
				}
				if (text.Contains("echoing"))
				{
					Object.BoostStat("Ego", Stat.Random(1, 3));
				}
				if (text.Contains("frenetic"))
				{
					Object.AddBaseStat("Speed", Stat.Random(15, 30));
				}
				if (text.Contains("shell-cracked"))
				{
					Object.AddBaseStat("AV", -1);
					Object.AddBaseStat("Hitpoints", Stat.Random(20, 30));
				}
				if (text.Contains("Skuttler"))
				{
					Object.AddBaseStat("MoveSpeed", -Stat.Random(10, 20));
				}
				if (text.Contains("Ancient"))
				{
					Object.AddBaseStat("AV", 2);
				}
				if (text.Contains("Deepcrawler"))
				{
					Object.RequirePart<Mutations>(false).AddMutation(new BurrowingClaws(), 5, true);
				}
				if (text.Contains("Goliath"))
				{
					Object.AddBaseStat("AV", 1);
					Object.AddBaseStat("MoveSpeed", Stat.Random(10, 20));
					Object.AddBaseStat("Hitpoints", Stat.Random(30, 50));
				}
				if (text.Contains("Lord") || text.Contains("Patriarch"))
				{
					Object.BoostStat("Strength", Stat.Random(1, 3));
					Object.BoostStat("Ego", Stat.Random(1, 3));
					Object.BoostStat("Willpower", Stat.Random(1, 3));
					Object.AddBaseStat("Hitpoints", Stat.Random(10, 20));
				}
			}
			Object.ReceiveObjectFromPopulation("Melee Weapons 2", null, false, 50, 0, null, null, null, Context, null);
			Object.ReceiveObjectFromPopulation("Armor 2", null, false, 50, 0, null, null, null, Context, null);
			Object.ReceiveObjectFromPopulation("Junk 4R", null, false, 0, 0, null, null, null, Context, null);
			Object.ReceiveObjectFromPopulation("Junk 5", null, false, 0, 0, null, null, null, Context, null);
			Object.MultiplyStat("Hitpoints", 2);
		}
	}
}
