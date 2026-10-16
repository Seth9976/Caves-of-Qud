using System;
using System.Collections.Generic;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F6 RID: 2550
	public static class SecurityClearance
	{
		// Token: 0x06006CCD RID: 27853 RVA: 0x00236724 File Offset: 0x00234924
		public static string GetKeySpecificationBySecurityClearance(int Clearance)
		{
			switch (Clearance)
			{
			case -5:
				return "Purple Security Card";
			case -4:
				return "Purple Security Card,Blue Security Card";
			case -3:
				return "Purple Security Card,Blue Security Card,Green Security Card";
			case -2:
				return "Purple Security Card,Blue Security Card,Green Security Card,Yellow Security Card";
			case -1:
				return "Purple Security Card,Blue Security Card,Green Security Card,Yellow Security Card,Red Security Card";
			case 1:
				return "*Psychometry,Purple Security Card,Blue Security Card,Green Security Card,Yellow Security Card,Red Security Card";
			case 2:
				return "*Psychometry,Purple Security Card,Blue Security Card,Green Security Card,Yellow Security Card";
			case 3:
				return "*Psychometry,Purple Security Card,Blue Security Card,Green Security Card";
			case 4:
				return "*Psychometry,Purple Security Card,Blue Security Card";
			case 5:
				return "*Psychometry,Purple Security Card";
			}
			return null;
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x002367A8 File Offset: 0x002349A8
		public static void HandleSecurityClearanceSpecification(int Spec, ref string KeyObject)
		{
			string keySpecificationBySecurityClearance = SecurityClearance.GetKeySpecificationBySecurityClearance(Spec);
			if (keySpecificationBySecurityClearance != null)
			{
				KeyObject = keySpecificationBySecurityClearance;
			}
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x002367C4 File Offset: 0x002349C4
		public static int GetSecurityClearanceByKeySpecification(string KeySpec)
		{
			List<string> list = KeySpec.CachedCommaExpansion();
			if (list.Contains("Red Security Card"))
			{
				return 1;
			}
			if (list.Contains("Yellow Security Card"))
			{
				return 2;
			}
			if (list.Contains("Green Security Card"))
			{
				return 3;
			}
			if (list.Contains("Blue Security Card"))
			{
				return 4;
			}
			if (list.Contains("Purple Security Card"))
			{
				return 5;
			}
			return 0;
		}
	}
}
