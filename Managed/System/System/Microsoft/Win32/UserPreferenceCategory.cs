using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers that represent categories of user preferences.</summary>
	// Token: 0x02000130 RID: 304
	public enum UserPreferenceCategory
	{
		/// <summary>Indicates user preferences associated with accessibility features of the system for users with disabilities.</summary>
		// Token: 0x04000509 RID: 1289
		Accessibility = 1,
		/// <summary>Indicates user preferences associated with system colors. This category includes such as the default color of windows or menus.</summary>
		// Token: 0x0400050A RID: 1290
		Color,
		/// <summary>Indicates user preferences associated with the system desktop. This category includes the background image or background image layout of the desktop.</summary>
		// Token: 0x0400050B RID: 1291
		Desktop,
		/// <summary>Indicates user preferences that are not associated with any other category.</summary>
		// Token: 0x0400050C RID: 1292
		General,
		/// <summary>Indicates user preferences for icon settings, including icon height and spacing.</summary>
		// Token: 0x0400050D RID: 1293
		Icon,
		/// <summary>Indicates user preferences for keyboard settings, such as the key down repeat rate and delay.</summary>
		// Token: 0x0400050E RID: 1294
		Keyboard,
		/// <summary>Indicates user preferences for menu settings, such as menu delays and text alignment.</summary>
		// Token: 0x0400050F RID: 1295
		Menu,
		/// <summary>Indicates user preferences for mouse settings, such as double-click time and mouse sensitivity.</summary>
		// Token: 0x04000510 RID: 1296
		Mouse,
		/// <summary>Indicates user preferences for policy settings, such as user rights and access levels.</summary>
		// Token: 0x04000511 RID: 1297
		Policy,
		/// <summary>Indicates the user preferences for system power settings. This category includes power feature settings, such as the idle time before the system automatically enters low power mode.</summary>
		// Token: 0x04000512 RID: 1298
		Power,
		/// <summary>Indicates user preferences associated with the screensaver.</summary>
		// Token: 0x04000513 RID: 1299
		Screensaver,
		/// <summary>Indicates user preferences associated with the dimensions and characteristics of windows on the system.</summary>
		// Token: 0x04000514 RID: 1300
		Window,
		/// <summary>Indicates changes in user preferences for regional settings, such as the character encoding and culture strings.</summary>
		// Token: 0x04000515 RID: 1301
		Locale,
		/// <summary>Indicates user preferences associated with visual styles, such as enabling or disabling visual styles and switching from one visual style to another.</summary>
		// Token: 0x04000516 RID: 1302
		VisualStyle
	}
}
