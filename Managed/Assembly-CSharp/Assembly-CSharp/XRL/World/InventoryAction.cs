using System;
using System.Collections.Generic;
using XRL.World.Parts.Mutation;

namespace XRL.World
{
	// Token: 0x0200095A RID: 2394
	public class InventoryAction
	{
		// Token: 0x060062ED RID: 25325 RVA: 0x001E606D File Offset: 0x001E426D
		public bool IsUsable(GameObject Twiddler, GameObject Twiddlee, bool Distant = false)
		{
			return !Distant || this.WorksAtDistance || (this.WorksTelepathically && Twiddler.CanMakeTelepathicContactWith(Twiddlee)) || (this.WorksTelekinetically && Twiddler.CanManipulateTelekinetically(Twiddlee));
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x001E60A8 File Offset: 0x001E42A8
		public bool IsUsable(GameObject Twiddler, GameObject Twiddlee, bool Distant, out bool Telekinetic)
		{
			Telekinetic = false;
			if (!Distant)
			{
				return true;
			}
			if (this.WorksAtDistance)
			{
				return true;
			}
			if (this.WorksTelepathically && Twiddler.CanMakeTelepathicContactWith(Twiddlee))
			{
				return true;
			}
			if (this.WorksTelekinetically && Twiddler.HasPart<Telekinesis>())
			{
				Telekinetic = true;
				if (Twiddler.CanManipulateTelekinetically(Twiddlee))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x001E60FB File Offset: 0x001E42FB
		public bool IsVisible(GameObject Twiddler, GameObject Twiddlee, bool Distant = false)
		{
			return !Distant || this.WorksAtDistance || (this.WorksTelepathically && Twiddler.CanMakeTelepathicContactWith(Twiddlee)) || (this.WorksTelekinetically && Twiddler.HasPart<Telekinesis>());
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x001E6134 File Offset: 0x001E4334
		public IEvent Process(GameObject GO, GameObject Owner, bool Telekinetic = false)
		{
			if (Telekinetic)
			{
				GO.TelekinesisBlip();
			}
			Owner.FireEvent(Event.New("InvCommandActivating", "Object", GO));
			GameObject gameObject = (this.FireOnActor ? Owner : (this.FireOn ?? GO));
			IEvent @event;
			if (this.AsMinEvent)
			{
				InventoryActionEvent.Check(out @event, gameObject, Owner, GO, this.Command, false, false, false, false, false, 0, 0, 0, null, null, null, null);
			}
			else
			{
				Event event2 = Event.New(this.Command, "Object", GO, "Owner", Owner);
				@event = event2;
				gameObject.FireEvent(event2);
			}
			return @event;
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x001E61C4 File Offset: 0x001E43C4
		public static int SortCompare(InventoryAction a, InventoryAction b)
		{
			bool flag = a.Key == ' ';
			bool flag2 = b.Key == ' ';
			if (!flag && !flag2)
			{
				int num = char.ToUpper(a.Key).CompareTo(char.ToUpper(b.Key));
				if (num != 0)
				{
					return num;
				}
				if (a.Key != b.Key)
				{
					return -a.Key.CompareTo(b.Key);
				}
				int num2 = a.Default.CompareTo(b.Default);
				if (num2 != 0)
				{
					return -num2;
				}
			}
			else if (flag || flag2)
			{
				return flag.CompareTo(flag2);
			}
			return InventoryAction.PriorityCompare(a, b);
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x001E6264 File Offset: 0x001E4464
		public static int PriorityCompare(InventoryAction a, InventoryAction b)
		{
			int num = a.Priority.CompareTo(b.Priority);
			if (num != 0)
			{
				return -num;
			}
			return -a.Display.CompareTo(b.Display);
		}

		// Token: 0x04002E6A RID: 11882
		public string Name;

		// Token: 0x04002E6B RID: 11883
		public char Key;

		// Token: 0x04002E6C RID: 11884
		public string Display;

		// Token: 0x04002E6D RID: 11885
		public string Command;

		// Token: 0x04002E6E RID: 11886
		public string PreferToHighlight;

		// Token: 0x04002E6F RID: 11887
		public int Default;

		// Token: 0x04002E70 RID: 11888
		public int Priority;

		// Token: 0x04002E71 RID: 11889
		public bool FireOnActor;

		// Token: 0x04002E72 RID: 11890
		public bool WorksAtDistance;

		// Token: 0x04002E73 RID: 11891
		public bool WorksTelekinetically;

		// Token: 0x04002E74 RID: 11892
		public bool WorksTelepathically;

		// Token: 0x04002E75 RID: 11893
		public bool AsMinEvent;

		// Token: 0x04002E76 RID: 11894
		public GameObject FireOn;

		// Token: 0x04002E77 RID: 11895
		public bool ReturnToModernUI;

		// Token: 0x02001A6A RID: 6762
		public class Comparer : IComparer<InventoryAction>
		{
			// Token: 0x0600E776 RID: 59254 RVA: 0x00535A38 File Offset: 0x00533C38
			public int Compare(InventoryAction a, InventoryAction b)
			{
				if (!this.priorityFirst)
				{
					return InventoryAction.SortCompare(a, b);
				}
				return InventoryAction.PriorityCompare(a, b);
			}

			// Token: 0x04006941 RID: 26945
			public bool priorityFirst;
		}
	}
}
