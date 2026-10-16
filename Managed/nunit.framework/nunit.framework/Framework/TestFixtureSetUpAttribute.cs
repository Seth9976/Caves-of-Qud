using System;

namespace NUnit.Framework
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	[Obsolete("Use OneTimeSetUpAttribute")]
	public class TestFixtureSetUpAttribute : OneTimeSetUpAttribute
	{
	}
}
