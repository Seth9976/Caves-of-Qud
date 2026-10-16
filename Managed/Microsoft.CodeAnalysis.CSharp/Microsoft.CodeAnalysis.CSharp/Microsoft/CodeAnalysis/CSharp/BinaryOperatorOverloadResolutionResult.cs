using System;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000056 RID: 86
	internal sealed class BinaryOperatorOverloadResolutionResult
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00047AFD File Offset: 0x00045CFD
		private BinaryOperatorOverloadResolutionResult()
		{
			this.Results = new ArrayBuilder<BinaryOperatorAnalysisResult>(10);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00047B14 File Offset: 0x00045D14
		public bool AnyValid()
		{
			foreach (BinaryOperatorAnalysisResult binaryOperatorAnalysisResult in this.Results)
			{
				if (binaryOperatorAnalysisResult.IsValid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00047B4C File Offset: 0x00045D4C
		public bool SingleValid()
		{
			bool flag = false;
			foreach (BinaryOperatorAnalysisResult binaryOperatorAnalysisResult in this.Results)
			{
				if (binaryOperatorAnalysisResult.IsValid)
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00047B8C File Offset: 0x00045D8C
		public BinaryOperatorAnalysisResult Best
		{
			get
			{
				BinaryOperatorAnalysisResult binaryOperatorAnalysisResult = default(BinaryOperatorAnalysisResult);
				foreach (BinaryOperatorAnalysisResult binaryOperatorAnalysisResult2 in this.Results)
				{
					if (binaryOperatorAnalysisResult2.IsValid)
					{
						if (binaryOperatorAnalysisResult.IsValid)
						{
							return default(BinaryOperatorAnalysisResult);
						}
						binaryOperatorAnalysisResult = binaryOperatorAnalysisResult2;
					}
				}
				return binaryOperatorAnalysisResult;
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00047BDF File Offset: 0x00045DDF
		public static BinaryOperatorOverloadResolutionResult GetInstance()
		{
			return BinaryOperatorOverloadResolutionResult.Pool.Allocate();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00047BEB File Offset: 0x00045DEB
		public void Free()
		{
			this.Clear();
			BinaryOperatorOverloadResolutionResult.Pool.Free(this);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00047BFE File Offset: 0x00045DFE
		public void Clear()
		{
			this.Results.Clear();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00047C0B File Offset: 0x00045E0B
		private static ObjectPool<BinaryOperatorOverloadResolutionResult> CreatePool()
		{
			return new ObjectPool<BinaryOperatorOverloadResolutionResult>(() => new BinaryOperatorOverloadResolutionResult(), 10);
		}

		// Token: 0x0400013E RID: 318
		public readonly ArrayBuilder<BinaryOperatorAnalysisResult> Results;

		// Token: 0x0400013F RID: 319
		public static readonly ObjectPool<BinaryOperatorOverloadResolutionResult> Pool = BinaryOperatorOverloadResolutionResult.CreatePool();
	}
}
