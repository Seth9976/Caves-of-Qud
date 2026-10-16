using System;

namespace XRL.World
{
	/// <summary>A static singleton event which is dispatched to a <see cref="T:XRL.IEventHandler" /> that implements <see cref="T:XRL.World.IModEventHandler`1" />.</summary>
	/// <example>
	/// <code>
	/// public class ExampleSingletonEvent : ModSingletonEvent&lt;ExampleSingletonEvent&gt;
	/// {
	///
	///     public static readonly int CascadeLevel = CASCADE_EQUIPMENT | CASCADE_EXCEPT_THROWN_WEAPON;
	///
	///     public bool Test;
	///
	///     public static bool Check(GameObject Object)
	///     {
	///         Object.HandleEvent(Instance);
	///         var result = Instance.Test;
	///         Instance.Reset();
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
	// Token: 0x0200093A RID: 2362
	public abstract class ModSingletonEvent<T> : SingletonEvent<T> where T : ModSingletonEvent<T>, new()
	{
		// Token: 0x06005B3D RID: 23357 RVA: 0x001B9468 File Offset: 0x001B7668
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
