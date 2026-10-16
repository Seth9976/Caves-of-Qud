using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000A5 RID: 165
	public interface IAsyncOnJointBreak2DHandler
	{
		// Token: 0x0600049D RID: 1181
		UniTask<Joint2D> OnJointBreak2DAsync();
	}
}
