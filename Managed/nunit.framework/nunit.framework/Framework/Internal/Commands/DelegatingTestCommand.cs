using System;

namespace NUnit.Framework.Internal.Commands
{
	// Token: 0x020000AC RID: 172
	public abstract class DelegatingTestCommand : TestCommand
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
		public TestCommand GetInnerCommand()
		{
			return this.innerCommand;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		protected DelegatingTestCommand(TestCommand innerCommand)
			: base(innerCommand.Test)
		{
			this.innerCommand = innerCommand;
		}

		// Token: 0x04000155 RID: 341
		protected TestCommand innerCommand;
	}
}
