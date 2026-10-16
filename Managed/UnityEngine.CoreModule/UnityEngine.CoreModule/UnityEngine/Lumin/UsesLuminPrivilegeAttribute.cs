using System;

namespace UnityEngine.Lumin
{
	// Token: 0x0200043A RID: 1082
	[Obsolete("Lumin is no longer supported in Unity 2022.2")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class UsesLuminPrivilegeAttribute : Attribute
	{
		// Token: 0x060029ED RID: 10733 RVA: 0x00050EBC File Offset: 0x0004F0BC
		public UsesLuminPrivilegeAttribute(string privilege)
		{
			this.m_Privilege = privilege;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x00050ED0 File Offset: 0x0004F0D0
		public string privilege
		{
			get
			{
				return this.m_Privilege;
			}
		}

		// Token: 0x04000BF4 RID: 3060
		private readonly string m_Privilege;
	}
}
