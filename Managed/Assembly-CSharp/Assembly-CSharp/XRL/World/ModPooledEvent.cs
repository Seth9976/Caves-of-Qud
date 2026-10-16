using System;

namespace XRL.World
{
	/// <summary>A pooled event which is dispatched to a <see cref="T:XRL.IEventHandler" /> that implements <see cref="T:XRL.World.IModEventHandler`1" />.</summary>
	/// <example>
	/// <code>
	/// public class ExamplePooledEvent : ModPooledEvent&lt;ExamplePooledEvent&gt;
	/// {
	///
	///     public static readonly int CascadeLevel = CASCADE_EQUIPMENT | CASCADE_EXCEPT_THROWN_WEAPON;
	///
	///     public bool Test;
	///
	///     public static bool Check(GameObject Object)
	///     {
	///         var E = FromPool();
	///         Object.HandleEvent(E);
	///         var result = E.Test;
	///         ResetTo(ref E);
	///
	///         return result;
	///     }
	///
	///     public override void Reset()
	///     {
	///         base.Reset();
	///         Test = false;
	///     }
	///
	///     public override int GetCascadeLevel()
	///     {
	///         return CascadeLevel;
	///     }
	///
	/// }
	/// </code>
	/// </example>
	// Token: 0x02000939 RID: 2361
	public abstract class ModPooledEvent<T> : PooledEvent<T> where T : ModPooledEvent<T>, new()
	{
		// Token: 0x06005B3B RID: 23355 RVA: 0x001B9428 File Offset: 0x001B7628
		public override bool Dispatch(IEventHandler Handler)
		{
			IModEventHandler<T> modEventHandler = Handler as IModEventHandler<T>;
			if (modEventHandler != null)
			{
				return modEventHandler.HandleEvent((T)((object)this));
			}
			return !(Handler is IEventSource) || Handler.HandleEvent(this);
		}
	}
}
