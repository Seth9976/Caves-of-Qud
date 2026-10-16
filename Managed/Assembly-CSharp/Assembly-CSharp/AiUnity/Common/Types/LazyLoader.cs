using System;

namespace AiUnity.Common.Types
{
	// Token: 0x020002BC RID: 700
	public class LazyLoader<T> where T : class
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0007EFE4 File Offset: 0x0007D1E4
		public T Value
		{
			get
			{
				object obj = this.padlock;
				lock (obj)
				{
					if (!this.hasRun)
					{
						this.instance = this.function();
						this.hasRun = true;
					}
				}
				return this.instance;
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0007F044 File Offset: 0x0007D244
		public LazyLoader(Func<T> function)
		{
			this.hasRun = false;
			this.padlock = new object();
			this.function = function;
		}

		// Token: 0x04000E7F RID: 3711
		private readonly Func<T> function;

		// Token: 0x04000E80 RID: 3712
		private readonly object padlock;

		// Token: 0x04000E81 RID: 3713
		private bool hasRun;

		// Token: 0x04000E82 RID: 3714
		private T instance;
	}
}
