using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020D RID: 525
	internal static class DropdownUtility
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x00046D34 File Offset: 0x00044F34
		internal static IGenericMenu CreateDropdown()
		{
			IGenericMenu genericMenu2;
			if (DropdownUtility.MakeDropdownFunc == null)
			{
				IGenericMenu genericMenu = new GenericDropdownMenu();
				genericMenu2 = genericMenu;
			}
			else
			{
				genericMenu2 = DropdownUtility.MakeDropdownFunc();
			}
			return genericMenu2;
		}

		// Token: 0x040009B3 RID: 2483
		internal static Func<IGenericMenu> MakeDropdownFunc;
	}
}
