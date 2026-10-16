using System;
using System.Threading;

namespace NUnit.Framework
{
	// Token: 0x0200002E RID: 46
	[Obsolete("Use ApartmentAttribute and pass in ApartmentState.MTA instead")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class RequiresMTAAttribute : PropertyAttribute
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00004995 File Offset: 0x00002B95
		public RequiresMTAAttribute()
		{
			base.Properties.Add("ApartmentState", ApartmentState.MTA);
		}
	}
}
