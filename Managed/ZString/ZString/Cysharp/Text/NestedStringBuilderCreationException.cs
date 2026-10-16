using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	internal class NestedStringBuilderCreationException : InvalidOperationException
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00009EF9 File Offset: 0x000080F9
		protected internal NestedStringBuilderCreationException(string typeName, string extraMessage = "")
			: base("A nested call with `notNested: true`, or Either You forgot to call " + typeName + ".Dispose() of  in the past." + extraMessage)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00009F12 File Offset: 0x00008112
		protected internal NestedStringBuilderCreationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
