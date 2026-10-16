using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000207 RID: 519
	public class DropdownMenuAction : DropdownMenuItem
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00046863 File Offset: 0x00044A63
		public string name { get; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0004686B File Offset: 0x00044A6B
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00046873 File Offset: 0x00044A73
		public DropdownMenuAction.Status status { get; private set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0004687C File Offset: 0x00044A7C
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x00046884 File Offset: 0x00044A84
		public DropdownMenuEventInfo eventInfo { get; private set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0004688D File Offset: 0x00044A8D
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x00046895 File Offset: 0x00044A95
		public object userData { get; private set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0004689E File Offset: 0x00044A9E
		internal VisualElement content { get; }

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000468A8 File Offset: 0x00044AA8
		public static DropdownMenuAction.Status AlwaysEnabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Normal;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000468BC File Offset: 0x00044ABC
		public static DropdownMenuAction.Status AlwaysDisabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000468CF File Offset: 0x00044ACF
		public DropdownMenuAction(string actionName, Action<DropdownMenuAction> actionCallback, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			this.name = actionName;
			this.actionCallback = actionCallback;
			this.actionStatusCallback = actionStatusCallback;
			this.userData = userData;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000468F7 File Offset: 0x00044AF7
		public void UpdateActionStatus(DropdownMenuEventInfo eventInfo)
		{
			this.eventInfo = eventInfo;
			Func<DropdownMenuAction, DropdownMenuAction.Status> func = this.actionStatusCallback;
			this.status = ((func != null) ? func(this) : DropdownMenuAction.Status.Hidden);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004691C File Offset: 0x00044B1C
		public void Execute()
		{
			Action<DropdownMenuAction> action = this.actionCallback;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x040009A5 RID: 2469
		private readonly Action<DropdownMenuAction> actionCallback;

		// Token: 0x040009A6 RID: 2470
		private readonly Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback;

		// Token: 0x02000208 RID: 520
		[Flags]
		public enum Status
		{
			// Token: 0x040009A8 RID: 2472
			None = 0,
			// Token: 0x040009A9 RID: 2473
			Normal = 1,
			// Token: 0x040009AA RID: 2474
			Disabled = 2,
			// Token: 0x040009AB RID: 2475
			Checked = 4,
			// Token: 0x040009AC RID: 2476
			Hidden = 8
		}
	}
}
