using System;

namespace XRL.UI.Framework
{
	/// <summary>
	/// Used to describe menu options in the shared overlay.
	/// </summary>
	// Token: 0x020014F9 RID: 5369
	public class MenuOption : FrameworkDataElement
	{
		// Token: 0x0600D1B9 RID: 53689 RVA: 0x004DE810 File Offset: 0x004DCA10
		public string getKeyDescription()
		{
			if (!string.IsNullOrEmpty(this.InputCommand))
			{
				this.KeyDescription = ControlManager.getCommandInputDescription(this.InputCommand, true, false, ControlManager.InputDeviceType.Unknown, false);
			}
			return this.KeyDescription;
		}

		// Token: 0x0600D1BA RID: 53690 RVA: 0x004DE83C File Offset: 0x004DCA3C
		public string getMenuText()
		{
			if (!string.IsNullOrEmpty(this.InputCommand))
			{
				this.KeyDescription = ControlManager.getCommandInputDescription(this.InputCommand, true, false, ControlManager.InputDeviceType.Unknown, false);
			}
			if (!string.IsNullOrEmpty(this.KeyDescription))
			{
				return "[{{W|" + this.KeyDescription + "}}] " + this.Description;
			}
			return this.Description;
		}

		// Token: 0x040052E6 RID: 21222
		public string KeyDescription;

		// Token: 0x040052E7 RID: 21223
		public string InputCommand;

		// Token: 0x040052E8 RID: 21224
		public bool disabled;
	}
}
