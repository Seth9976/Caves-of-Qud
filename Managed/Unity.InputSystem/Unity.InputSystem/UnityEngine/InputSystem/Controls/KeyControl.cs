using System;
using System.Globalization;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011D RID: 285
	public class KeyControl : ButtonControl
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0004F643 File Offset: 0x0004D843
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x0004F64B File Offset: 0x0004D84B
		public Key keyCode { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0004F654 File Offset: 0x0004D854
		public int scanCode
		{
			get
			{
				base.RefreshConfigurationIfNeeded();
				return this.m_ScanCode;
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0004F664 File Offset: 0x0004D864
		protected override void RefreshConfiguration()
		{
			base.displayName = null;
			this.m_ScanCode = 0;
			QueryKeyNameCommand queryKeyNameCommand = QueryKeyNameCommand.Create(this.keyCode);
			if (base.device.ExecuteCommand<QueryKeyNameCommand>(ref queryKeyNameCommand) > 0L)
			{
				this.m_ScanCode = queryKeyNameCommand.scanOrKeyCode;
				string text = queryKeyNameCommand.ReadKeyName();
				if (string.IsNullOrEmpty(text))
				{
					base.displayName = text;
					return;
				}
				string text2 = text.ToLowerInvariant();
				if (string.IsNullOrEmpty(text2))
				{
					base.displayName = text;
					return;
				}
				TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
				base.displayName = textInfo.ToTitleCase(text2);
			}
		}

		// Token: 0x040006D4 RID: 1748
		private int m_ScanCode;
	}
}
