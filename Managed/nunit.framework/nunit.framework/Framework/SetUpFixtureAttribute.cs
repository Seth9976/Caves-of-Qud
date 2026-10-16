using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000038 RID: 56
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class SetUpFixtureAttribute : NUnitAttribute, IFixtureBuilder
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00004A98 File Offset: 0x00002C98
		public IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo)
		{
			SetUpFixture setUpFixture = new SetUpFixture(typeInfo);
			if (setUpFixture.RunState != RunState.NotRunnable)
			{
				string text = null;
				if (!this.IsValidFixtureType(typeInfo, ref text))
				{
					setUpFixture.RunState = RunState.NotRunnable;
					setUpFixture.Properties.Set("_SKIPREASON", text);
				}
			}
			return new TestSuite[] { setUpFixture };
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004AE4 File Offset: 0x00002CE4
		private bool IsValidFixtureType(ITypeInfo typeInfo, ref string reason)
		{
			if (typeInfo.IsAbstract)
			{
				reason = string.Format("{0} is an abstract class", typeInfo.FullName);
				return false;
			}
			if (!typeInfo.HasConstructor(new Type[0]))
			{
				reason = string.Format("{0} does not have a default constructor", typeInfo.FullName);
				return false;
			}
			foreach (Type type in new Type[]
			{
				typeof(SetUpAttribute),
				typeof(TearDownAttribute),
				typeof(TestFixtureSetUpAttribute),
				typeof(TestFixtureTearDownAttribute)
			})
			{
				if (typeInfo.HasMethodWithAttribute(type))
				{
					reason = type.Name + " attribute not allowed in a SetUpFixture";
					return false;
				}
			}
			return true;
		}
	}
}
