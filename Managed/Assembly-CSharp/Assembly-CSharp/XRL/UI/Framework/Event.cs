using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace XRL.UI.Framework
{
	// Token: 0x02001524 RID: 5412
	public class Event
	{
		// Token: 0x0600D2B1 RID: 53937 RVA: 0x004E3790 File Offset: 0x004E1990
		public bool IsRightClick()
		{
			object obj;
			this.data.TryGetValue("PointerEventData", out obj);
			return obj is PointerEventData && (obj as PointerEventData).button == PointerEventData.InputButton.Right;
		}

		// Token: 0x0600D2B2 RID: 53938 RVA: 0x004E37C8 File Offset: 0x004E19C8
		public void Cancel()
		{
			this.cancelled = true;
		}

		// Token: 0x0600D2B3 RID: 53939 RVA: 0x004E37D1 File Offset: 0x004E19D1
		public void Handle()
		{
			this.handled = true;
		}

		// Token: 0x04005392 RID: 21394
		public bool cancelled;

		// Token: 0x04005393 RID: 21395
		public bool handled;

		// Token: 0x04005394 RID: 21396
		public int? axisValue;

		// Token: 0x04005395 RID: 21397
		public Dictionary<string, object> data;

		// Token: 0x04005396 RID: 21398
		public Event.Type type;

		// Token: 0x02001E33 RID: 7731
		public enum Type
		{
			// Token: 0x040075E1 RID: 30177
			Enter,
			// Token: 0x040075E2 RID: 30178
			Exit,
			// Token: 0x040075E3 RID: 30179
			Input
		}

		// Token: 0x02001E34 RID: 7732
		public static class Helpers
		{
			// Token: 0x0600F67C RID: 63100 RVA: 0x0055A40A File Offset: 0x0055860A
			public static Action Handle(Action act)
			{
				return delegate
				{
					Event currentEvent = NavigationController.currentEvent;
					if (currentEvent == null || !currentEvent.handled)
					{
						act();
					}
					Event currentEvent2 = NavigationController.currentEvent;
					if (currentEvent2 == null)
					{
						return;
					}
					currentEvent2.Handle();
				};
			}

			// Token: 0x0600F67D RID: 63101 RVA: 0x0055A423 File Offset: 0x00558623
			public static Action IfAxisNeg(Action act)
			{
				return delegate
				{
					Event currentEvent = NavigationController.currentEvent;
					bool flag;
					if (currentEvent == null)
					{
						flag = false;
					}
					else
					{
						int? axisValue = currentEvent.axisValue;
						int num = 0;
						flag = (axisValue.GetValueOrDefault() < num) & (axisValue != null);
					}
					if (flag)
					{
						act();
					}
				};
			}

			// Token: 0x0600F67E RID: 63102 RVA: 0x0055A43C File Offset: 0x0055863C
			public static Action Axis(Action positive = null, Action negative = null, Action zero = null)
			{
				return delegate
				{
					Event currentEvent = NavigationController.currentEvent;
					bool flag;
					if (currentEvent == null)
					{
						flag = false;
					}
					else
					{
						int? num = currentEvent.axisValue;
						int num2 = 0;
						flag = (num.GetValueOrDefault() < num2) & (num != null);
					}
					if (flag)
					{
						if (negative != null)
						{
							negative();
							return;
						}
					}
					else
					{
						Event currentEvent2 = NavigationController.currentEvent;
						bool flag2;
						if (currentEvent2 == null)
						{
							flag2 = false;
						}
						else
						{
							int? num = currentEvent2.axisValue;
							int num2 = 0;
							flag2 = (num.GetValueOrDefault() > num2) & (num != null);
						}
						if (flag2)
						{
							if (positive != null)
							{
								positive();
								return;
							}
						}
						else if (zero != null)
						{
							zero();
						}
					}
				};
			}

			// Token: 0x0600F67F RID: 63103 RVA: 0x0055A463 File Offset: 0x00558663
			public static Action HandleIfAxisNeg(Action act)
			{
				return Event.Helpers.IfAxisNeg(Event.Helpers.Handle(act));
			}
		}
	}
}
