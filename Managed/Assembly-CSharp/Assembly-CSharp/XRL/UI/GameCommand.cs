using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

namespace XRL.UI
{
	// Token: 0x02001471 RID: 5233
	public class GameCommand
	{
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x0600CE04 RID: 52740 RVA: 0x004AC9A1 File Offset: 0x004AABA1
		public bool IgnoreConflicts
		{
			get
			{
				return this.Auto == "DownPass";
			}
		}

		// Token: 0x0600CE05 RID: 52741 RVA: 0x004AC9B3 File Offset: 0x004AABB3
		public bool CanRemoveBinding()
		{
			return !this.Required || CommandBindingManager.GetCommandBindings(this.ID).Count<string>() > 1;
		}

		// Token: 0x0600CE06 RID: 52742 RVA: 0x004AC9D2 File Offset: 0x004AABD2
		public bool CanRemoveBinding(ControlManager.InputDeviceType deviceType)
		{
			return !this.Required || CommandBindingManager.GetCommandBindings(this.ID, deviceType, true).Count<string>() > 1;
		}

		// Token: 0x0600CE07 RID: 52743 RVA: 0x004AC9F4 File Offset: 0x004AABF4
		public bool SharesBindsWith(GameCommand with)
		{
			List<string> canShareBindsWith = this.CanShareBindsWith;
			if (canShareBindsWith == null || !canShareBindsWith.Contains((with != null) ? with.ID : null))
			{
				bool? flag;
				if (with == null)
				{
					flag = null;
				}
				else
				{
					List<string> canShareBindsWith2 = with.CanShareBindsWith;
					flag = ((canShareBindsWith2 != null) ? new bool?(canShareBindsWith2.Contains(this.ID)) : null);
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			}
			return true;
		}

		// Token: 0x04004FA8 RID: 20392
		public string ID;

		// Token: 0x04004FA9 RID: 20393
		public string DisplayText;

		// Token: 0x04004FAA RID: 20394
		public string ConsoleDisplayText;

		// Token: 0x04004FAB RID: 20395
		public string Category;

		// Token: 0x04004FAC RID: 20396
		public string Layer;

		// Token: 0x04004FAD RID: 20397
		public string Display;

		// Token: 0x04004FAE RID: 20398
		public bool Bindable = true;

		// Token: 0x04004FAF RID: 20399
		public string InputModuleBind;

		// Token: 0x04004FB0 RID: 20400
		public bool SkipUpgrade;

		// Token: 0x04004FB1 RID: 20401
		public string UpgradeFrom;

		// Token: 0x04004FB2 RID: 20402
		public string Auto;

		// Token: 0x04004FB3 RID: 20403
		public string Instrument;

		// Token: 0x04004FB4 RID: 20404
		public bool Required;

		// Token: 0x04004FB5 RID: 20405
		public InputActionType Type = InputActionType.Button;

		// Token: 0x04004FB6 RID: 20406
		public CapabilityManager.CapabilityType? RequiresCapability;

		// Token: 0x04004FB7 RID: 20407
		public List<string> CanShareBindsWith;

		// Token: 0x04004FB8 RID: 20408
		public List<GameCommand.KeyboardBinding> keyboardBindings = new List<GameCommand.KeyboardBinding>();

		// Token: 0x04004FB9 RID: 20409
		public List<GameCommand.MouseBinding> mouseBindings = new List<GameCommand.MouseBinding>();

		// Token: 0x04004FBA RID: 20410
		public List<GameCommand.GamepadBinding> gamepadBindings = new List<GameCommand.GamepadBinding>();

		// Token: 0x02001D84 RID: 7556
		public class KeyboardBinding
		{
			// Token: 0x0400731C RID: 29468
			public string Key;

			// Token: 0x0400731D RID: 29469
			public string Modifier;

			// Token: 0x0400731E RID: 29470
			public string Set;
		}

		// Token: 0x02001D85 RID: 7557
		public class MouseBinding
		{
			// Token: 0x0400731F RID: 29471
			public string Button;

			// Token: 0x04007320 RID: 29472
			public string Modifier;

			// Token: 0x04007321 RID: 29473
			public string Set;
		}

		// Token: 0x02001D86 RID: 7558
		public class GamepadBinding
		{
			// Token: 0x04007322 RID: 29474
			public string Button;

			// Token: 0x04007323 RID: 29475
			public bool Alt;
		}
	}
}
