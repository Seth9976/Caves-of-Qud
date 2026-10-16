using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000A8 RID: 168
	public class OneTimeSetUpCommand : TestCommand
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0000EEC1 File Offset: 0x0000D0C1
		public OneTimeSetUpCommand(TestSuite suite, List<SetUpTearDownItem> setUpTearDown, List<TestActionItem> actions)
			: base(suite)
		{
			this._suite = suite;
			this._typeInfo = suite.TypeInfo;
			this._arguments = suite.Arguments;
			this._setUpTearDown = setUpTearDown;
			this._actions = actions;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public override TestResult Execute(ITestExecutionContext context)
		{
			if (this._typeInfo != null)
			{
				if (!this._typeInfo.IsStaticClass)
				{
					context.TestObject = this._suite.Fixture ?? this._typeInfo.Construct(this._arguments);
					if (this._suite.Fixture == null)
					{
						this._suite.Fixture = context.TestObject;
					}
					base.Test.Fixture = this._suite.Fixture;
				}
				int i = this._setUpTearDown.Count;
				while (i > 0)
				{
					this._setUpTearDown[--i].RunSetUp(context);
				}
			}
			for (int j = 0; j < this._actions.Count; j++)
			{
				this._actions[j].BeforeTest(base.Test);
			}
			return context.CurrentResult;
		}

		// Token: 0x04000148 RID: 328
		private readonly TestSuite _suite;

		// Token: 0x04000149 RID: 329
		private readonly ITypeInfo _typeInfo;

		// Token: 0x0400014A RID: 330
		private readonly object[] _arguments;

		// Token: 0x0400014B RID: 331
		private readonly List<SetUpTearDownItem> _setUpTearDown;

		// Token: 0x0400014C RID: 332
		private readonly List<TestActionItem> _actions;
	}
}
