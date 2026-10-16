using System;

namespace XRL.World
{
	/// <summary>Contracts a class as capable of handling a custom event type.</summary>
	/// <example>
	/// <code>
	/// public class ExampleHandler : IPart, IModEventHandler&lt;ExampleEvent&gt;
	/// {
	///
	///     public override bool WantEvent(int ID, int Cascade)
	///     {
	///         return base.WantEvent(ID, Cascade)
	///               || ID == ExampleEvent.ID
	///             ;
	///     }
	///
	///     public bool HandleEvent(ExampleEvent E)
	///     {
	///        E.Test = true;
	///       return true;
	///     }
	///
	/// }
	/// </code>
	/// </example>
	// Token: 0x02000936 RID: 2358
	public interface IModEventHandler<T> where T : MinEvent
	{
		// Token: 0x06005B1E RID: 23326 RVA: 0x001B8DFD File Offset: 0x001B6FFD
		bool HandleEvent(T E)
		{
			return true;
		}
	}
}
