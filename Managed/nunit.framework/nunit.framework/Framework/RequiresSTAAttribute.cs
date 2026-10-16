using System;
using System.Threading;

namespace NUnit.Framework
{
	// Token: 0x0200002F RID: 47
	[Obsolete("Use ApartmentAttribute and pass in ApartmentState.STA instead")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class RequiresSTAAttribute : PropertyAttribute
	{
		// Token: 0x06000180 RID: 384 RVA: 0x000049B3 File Offset: 0x00002BB3
		public RequiresSTAAttribute()
		{
			base.Properties.Add("ApartmentState", ApartmentState.STA);
		}
	}
}
