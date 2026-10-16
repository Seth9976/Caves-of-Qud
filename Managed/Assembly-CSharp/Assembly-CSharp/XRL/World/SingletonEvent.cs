using System;

namespace XRL.World
{
	// Token: 0x02000940 RID: 2368
	public abstract class SingletonEvent<T> : CachedEvent where T : MinEvent, new()
	{
		// Token: 0x06005B6B RID: 23403 RVA: 0x001B9EEE File Offset: 0x001B80EE
		public SingletonEvent()
		{
			this.ID = SingletonEvent<T>.ID;
		}

		// Token: 0x04002D35 RID: 11573
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(T), null, null, null);

		// Token: 0x04002D36 RID: 11574
		public static readonly T Instance = new T();
	}
}
