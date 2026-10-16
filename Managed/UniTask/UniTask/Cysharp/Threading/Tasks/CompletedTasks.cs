using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003C RID: 60
	internal static class CompletedTasks
	{
		// Token: 0x04000084 RID: 132
		public static readonly UniTask<AsyncUnit> AsyncUnit = UniTask.FromResult<AsyncUnit>(Cysharp.Threading.Tasks.AsyncUnit.Default);

		// Token: 0x04000085 RID: 133
		public static readonly UniTask<bool> True = UniTask.FromResult<bool>(true);

		// Token: 0x04000086 RID: 134
		public static readonly UniTask<bool> False = UniTask.FromResult<bool>(false);

		// Token: 0x04000087 RID: 135
		public static readonly UniTask<int> Zero = UniTask.FromResult<int>(0);

		// Token: 0x04000088 RID: 136
		public static readonly UniTask<int> MinusOne = UniTask.FromResult<int>(-1);

		// Token: 0x04000089 RID: 137
		public static readonly UniTask<int> One = UniTask.FromResult<int>(1);
	}
}
