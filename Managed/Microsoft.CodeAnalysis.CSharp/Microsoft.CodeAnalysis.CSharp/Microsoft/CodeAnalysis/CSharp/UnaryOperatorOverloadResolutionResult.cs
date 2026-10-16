using System;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200005E RID: 94
	internal sealed class UnaryOperatorOverloadResolutionResult
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00048A7C File Offset: 0x00046C7C
		public UnaryOperatorOverloadResolutionResult()
		{
			this.Results = new ArrayBuilder<UnaryOperatorAnalysisResult>(10);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00048A94 File Offset: 0x00046C94
		public bool AnyValid()
		{
			foreach (UnaryOperatorAnalysisResult unaryOperatorAnalysisResult in this.Results)
			{
				if (unaryOperatorAnalysisResult.IsValid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00048ACC File Offset: 0x00046CCC
		public bool SingleValid()
		{
			bool flag = false;
			foreach (UnaryOperatorAnalysisResult unaryOperatorAnalysisResult in this.Results)
			{
				if (unaryOperatorAnalysisResult.IsValid)
				{
					if (flag)
					{
						return false;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00048B0C File Offset: 0x00046D0C
		public UnaryOperatorAnalysisResult Best
		{
			get
			{
				UnaryOperatorAnalysisResult unaryOperatorAnalysisResult = default(UnaryOperatorAnalysisResult);
				foreach (UnaryOperatorAnalysisResult unaryOperatorAnalysisResult2 in this.Results)
				{
					if (unaryOperatorAnalysisResult2.IsValid)
					{
						if (unaryOperatorAnalysisResult.IsValid)
						{
							return default(UnaryOperatorAnalysisResult);
						}
						unaryOperatorAnalysisResult = unaryOperatorAnalysisResult2;
					}
				}
				return unaryOperatorAnalysisResult;
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00048B5F File Offset: 0x00046D5F
		public static UnaryOperatorOverloadResolutionResult GetInstance()
		{
			return UnaryOperatorOverloadResolutionResult.Pool.Allocate();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00048B6B File Offset: 0x00046D6B
		public void Free()
		{
			this.Results.Clear();
			UnaryOperatorOverloadResolutionResult.Pool.Free(this);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00048B83 File Offset: 0x00046D83
		private static ObjectPool<UnaryOperatorOverloadResolutionResult> CreatePool()
		{
			return new ObjectPool<UnaryOperatorOverloadResolutionResult>(() => new UnaryOperatorOverloadResolutionResult(), 10);
		}

		// Token: 0x040003FF RID: 1023
		public readonly ArrayBuilder<UnaryOperatorAnalysisResult> Results;

		// Token: 0x04000400 RID: 1024
		public static readonly ObjectPool<UnaryOperatorOverloadResolutionResult> Pool = UnaryOperatorOverloadResolutionResult.CreatePool();
	}
}
