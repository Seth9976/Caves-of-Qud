using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class MaxTimeAttribute : PropertyAttribute, IWrapSetUpTearDown, ICommandWrapper
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00003FA7 File Offset: 0x000021A7
		public MaxTimeAttribute(int milliseconds)
			: base(milliseconds)
		{
			this._milliseconds = milliseconds;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003FBC File Offset: 0x000021BC
		TestCommand ICommandWrapper.Wrap(TestCommand command)
		{
			return new MaxTimeCommand(command, this._milliseconds);
		}

		// Token: 0x04000023 RID: 35
		private int _milliseconds;
	}
}
