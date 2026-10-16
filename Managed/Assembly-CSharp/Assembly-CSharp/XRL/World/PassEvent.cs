using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200093B RID: 2363
	public abstract class PassEvent<T> : PooledEvent<T> where T : PassEvent<T>, new()
	{
		// Token: 0x06005B3F RID: 23359 RVA: 0x001B94A8 File Offset: 0x001B76A8
		public void Postprocess(int MaxPasses = 25)
		{
			int i = this.Handlers.Count;
			while (i > 0)
			{
				this.Pass++;
				for (int j = 0; j < i; j++)
				{
					try
					{
						IEventHandler eventHandler = this.Handlers.Dequeue();
						this.Dispatch(eventHandler);
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("T" + string.Format("::Pass {0}", this.Pass), ex, "game_exception");
					}
				}
				i = this.Handlers.Count;
				if (this.Pass == MaxPasses)
				{
					for (int k = 0; k < i; k++)
					{
						Type type = this.Handlers.Dequeue().GetType();
						MetricsManager.LogAssemblyError(type, "T::Postprocessing ended with persistent handler of type '" + type.FullName + "'.");
					}
					return;
				}
			}
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x001B9590 File Offset: 0x001B7790
		public override void Reset()
		{
			base.Reset();
			this.Pass = 1;
			this.Handlers.Clear();
		}

		// Token: 0x04002D0C RID: 11532
		public int Pass = 1;

		// Token: 0x04002D0D RID: 11533
		public Queue<IEventHandler> Handlers = new Queue<IEventHandler>();
	}
}
