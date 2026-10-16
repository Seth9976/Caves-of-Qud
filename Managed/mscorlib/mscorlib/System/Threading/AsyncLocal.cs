using System;

namespace System.Threading
{
	// Token: 0x02000281 RID: 641
	public sealed class AsyncLocal<T> : IAsyncLocal
	{
		// Token: 0x06001D5A RID: 7514 RVA: 0x0000259F File Offset: 0x0000079F
		public AsyncLocal()
		{
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0006DAA1 File Offset: 0x0006BCA1
		public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.m_valueChangedHandler = valueChangedHandler;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0006DAB0 File Offset: 0x0006BCB0
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0006DAD7 File Offset: 0x0006BCD7
		public T Value
		{
			get
			{
				object localValue = ExecutionContext.GetLocalValue(this);
				if (localValue != null)
				{
					return (T)((object)localValue);
				}
				return default(T);
			}
			set
			{
				ExecutionContext.SetLocalValue(this, value, this.m_valueChangedHandler != null);
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0006DAF0 File Offset: 0x0006BCF0
		void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
		{
			T t = ((previousValueObj == null) ? default(T) : ((T)((object)previousValueObj)));
			T t2 = ((currentValueObj == null) ? default(T) : ((T)((object)currentValueObj)));
			this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(t, t2, contextChanged));
		}

		// Token: 0x04001A24 RID: 6692
		private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;
	}
}
