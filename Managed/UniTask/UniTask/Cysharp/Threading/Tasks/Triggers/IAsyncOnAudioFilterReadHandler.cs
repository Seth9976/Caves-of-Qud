using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000081 RID: 129
	public interface IAsyncOnAudioFilterReadHandler
	{
		// Token: 0x0600041F RID: 1055
		[return: TupleElementNames(new string[] { "data", "channels" })]
		UniTask<ValueTuple<float[], int>> OnAudioFilterReadAsync();
	}
}
