using System;
using System.Threading;

namespace NUnit.Framework
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class ApartmentAttribute : PropertyAttribute
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000039AC File Offset: 0x00001BAC
		public ApartmentAttribute(ApartmentState apartmentState)
		{
			Guard.ArgumentValid(apartmentState != ApartmentState.Unknown, "must be STA or MTA", "apartmentState");
			base.Properties.Add("ApartmentState", apartmentState);
		}
	}
}
