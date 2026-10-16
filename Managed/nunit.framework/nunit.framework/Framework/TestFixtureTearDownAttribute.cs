using System;

namespace NUnit.Framework
{
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	[Obsolete("Use OneTimeTearDownAttribute")]
	public class TestFixtureTearDownAttribute : OneTimeTearDownAttribute
	{
	}
}
