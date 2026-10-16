using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DA RID: 2010
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncIteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x060045C3 RID: 17859 RVA: 0x000E521A File Offset: 0x000E341A
		public AsyncIteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
