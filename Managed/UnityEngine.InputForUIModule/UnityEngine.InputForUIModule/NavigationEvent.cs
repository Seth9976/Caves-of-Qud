using System;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000017 RID: 23
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct NavigationEvent : IEventProperties
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002ABF File Offset: 0x00000CBF
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002AC7 File Offset: 0x00000CC7
		public DiscreteTime timestamp { readonly get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002AD0 File Offset: 0x00000CD0
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public EventSource eventSource { readonly get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002AE1 File Offset: 0x00000CE1
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002AE9 File Offset: 0x00000CE9
		public uint playerId { readonly get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002AF2 File Offset: 0x00000CF2
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002AFA File Offset: 0x00000CFA
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00002B04 File Offset: 0x00000D04
		public override string ToString()
		{
			return string.Format("Navigation {0}", this.type) + ((this.type == NavigationEvent.Type.Move) ? string.Format(" {0}", this.direction) : "") + ((this.eventSource != EventSource.Keyboard) ? string.Format(" {0}", this.eventSource) : "");
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002B7C File Offset: 0x00000D7C
		internal static NavigationEvent.Direction DetermineMoveDirection(Vector2 vec, float deadZone = 0.6f)
		{
			bool flag = vec.sqrMagnitude < deadZone * deadZone;
			NavigationEvent.Direction direction;
			if (flag)
			{
				direction = NavigationEvent.Direction.None;
			}
			else
			{
				bool flag2 = Mathf.Abs(vec.x) > Mathf.Abs(vec.y);
				if (flag2)
				{
					direction = ((vec.x > 0f) ? NavigationEvent.Direction.Right : NavigationEvent.Direction.Left);
				}
				else
				{
					direction = ((vec.y > 0f) ? NavigationEvent.Direction.Up : NavigationEvent.Direction.Down);
				}
			}
			return direction;
		}

		// Token: 0x04000069 RID: 105
		public NavigationEvent.Type type;

		// Token: 0x0400006A RID: 106
		public NavigationEvent.Direction direction;

		// Token: 0x0400006B RID: 107
		public bool shouldBeUsed;

		// Token: 0x02000018 RID: 24
		public enum Type
		{
			// Token: 0x04000071 RID: 113
			Move = 1,
			// Token: 0x04000072 RID: 114
			Submit,
			// Token: 0x04000073 RID: 115
			Cancel
		}

		// Token: 0x02000019 RID: 25
		public enum Direction
		{
			// Token: 0x04000075 RID: 117
			None,
			// Token: 0x04000076 RID: 118
			Left,
			// Token: 0x04000077 RID: 119
			Up,
			// Token: 0x04000078 RID: 120
			Right,
			// Token: 0x04000079 RID: 121
			Down,
			// Token: 0x0400007A RID: 122
			Next,
			// Token: 0x0400007B RID: 123
			Previous
		}
	}
}
