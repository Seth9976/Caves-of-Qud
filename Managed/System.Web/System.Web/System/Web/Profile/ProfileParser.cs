using System;
using System.Web.Compilation;

namespace System.Web.Profile
{
	// Token: 0x02000509 RID: 1289
	internal sealed class ProfileParser
	{
		// Token: 0x0600394D RID: 14669 RVA: 0x00002050 File Offset: 0x00000250
		internal ProfileParser(HttpContext context)
		{
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x00098430 File Offset: 0x00096630
		public static Type GetProfileCommonType(HttpContext context)
		{
			string text;
			if (AppCodeCompiler.DefaultAppCodeAssemblyName != null)
			{
				text = "ProfileCommon, " + AppCodeCompiler.DefaultAppCodeAssemblyName;
			}
			else
			{
				text = "ProfileCommon";
			}
			Type type = Type.GetType(text);
			type == null;
			return type;
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x0009846C File Offset: 0x0009666C
		public static Type GetProfileGroupType(HttpContext context, string groupName)
		{
			string text;
			if (AppCodeCompiler.DefaultAppCodeAssemblyName != null)
			{
				text = "ProfileGroup" + groupName + ", " + AppCodeCompiler.DefaultAppCodeAssemblyName;
			}
			else
			{
				text = "ProfileGroup" + groupName;
			}
			Type type = Type.GetType(text);
			type == null;
			return type;
		}
	}
}
