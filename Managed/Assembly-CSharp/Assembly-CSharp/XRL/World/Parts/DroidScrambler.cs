using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001021 RID: 4129
	[HasGameBasedStaticCache]
	[Serializable]
	public class DroidScrambler : IPart
	{
		// Token: 0x0600A46F RID: 42095 RVA: 0x003C1E06 File Offset: 0x003C0006
		[GameBasedCacheInit]
		public static void ResetCache()
		{
			DroidScrambler.Scramblers.Clear();
			DroidScrambler.Scrambled.Clear();
			DroidScrambler.ScrambledCheck = -1L;
		}

		// Token: 0x0600A470 RID: 42096 RVA: 0x003C1E23 File Offset: 0x003C0023
		public override void Attach()
		{
			DroidScrambler.Scramblers.Add(this);
		}

		// Token: 0x0600A471 RID: 42097 RVA: 0x003C1E34 File Offset: 0x003C0034
		public static void CheckScramblingFactions(long TurnNumber)
		{
			if (DroidScrambler.ScrambledCheck >= TurnNumber - 5L)
			{
				return;
			}
			DroidScrambler.ScrambledCheck = TurnNumber;
			DroidScrambler.Scrambled.Clear();
			if (DroidScrambler.Scramblers.IsNullOrEmpty<DroidScrambler>())
			{
				return;
			}
			List<DroidScrambler> list = null;
			foreach (DroidScrambler droidScrambler in DroidScrambler.Scramblers)
			{
				GameObject parentObject = droidScrambler.ParentObject;
				if (!GameObject.Validate(ref parentObject))
				{
					if (list == null)
					{
						list = new List<DroidScrambler>();
					}
					list.Add(droidScrambler);
				}
				else
				{
					GameObject holder = parentObject.Holder;
					if (GameObject.Validate(ref holder))
					{
						Zone currentZone = holder.CurrentZone;
						string text = ((currentZone != null) ? currentZone.ZoneID : null);
						if (!text.IsNullOrEmpty() && !parentObject.HasEffect(typeof(Broken)) && !parentObject.HasEffect(typeof(Rusted)) && !parentObject.HasEffect(typeof(ElectromagneticPulsed)))
						{
							string scrambledFaction = DroidScrambler.GetScrambledFaction(holder);
							if (!scrambledFaction.IsNullOrEmpty())
							{
								List<string> list2;
								if (!DroidScrambler.Scrambled.TryGetValue(text, out list2))
								{
									DroidScrambler.Scrambled[text] = new List<string> { scrambledFaction };
								}
								else if (!list2.Contains(scrambledFaction))
								{
									list2.Add(scrambledFaction);
								}
							}
						}
					}
				}
			}
			if (list != null)
			{
				DroidScrambler.Scramblers.ExceptWith(list);
			}
		}

		// Token: 0x0600A472 RID: 42098 RVA: 0x003C1FA0 File Offset: 0x003C01A0
		public static string GetScrambledFaction(GameObject Object)
		{
			if (Object == null)
			{
				return null;
			}
			Brain brain = Object.Brain;
			Object = ((brain != null) ? brain.GetFinalLeader() : null) ?? Object;
			if (!Object.IsPlayer())
			{
				return Object.GetPrimaryFaction(false);
			}
			return "Player";
		}

		// Token: 0x0600A473 RID: 42099 RVA: 0x003C1FD5 File Offset: 0x003C01D5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == TakenEvent.ID || ID == DroppedEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600A474 RID: 42100 RVA: 0x003C1FFB File Offset: 0x003C01FB
		public override bool HandleEvent(TakenEvent E)
		{
			DroidScrambler.ScrambledCheck = -1L;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A475 RID: 42101 RVA: 0x003C200B File Offset: 0x003C020B
		public override bool HandleEvent(DroppedEvent E)
		{
			DroidScrambler.ScrambledCheck = -1L;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A476 RID: 42102 RVA: 0x003C201B File Offset: 0x003C021B
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			DroidScrambler.ScrambledCheck = -1L;
			return base.HandleEvent(E);
		}

		// Token: 0x04004427 RID: 17447
		public static HashSet<DroidScrambler> Scramblers = new HashSet<DroidScrambler>();

		// Token: 0x04004428 RID: 17448
		public static Dictionary<string, List<string>> Scrambled = new Dictionary<string, List<string>>();

		// Token: 0x04004429 RID: 17449
		public static long ScrambledCheck = -1L;
	}
}
