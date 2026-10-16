using System;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000C4 RID: 196
	public interface ICommandWrapper
	{
		// Token: 0x0600069D RID: 1693
		TestCommand Wrap(TestCommand command);
	}
}
