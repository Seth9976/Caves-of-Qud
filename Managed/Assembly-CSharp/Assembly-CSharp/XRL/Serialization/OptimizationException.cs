using System;

namespace XRL.Serialization
{
	/// <summary>
	/// Exception thrown when a value being optimized does not meet the required criteria for optimization.
	/// </summary>
	// Token: 0x02000571 RID: 1393
	public class OptimizationException : Exception
	{
		// Token: 0x06003C53 RID: 15443 RVA: 0x00126F4B File Offset: 0x0012514B
		public OptimizationException(string message)
			: base(message)
		{
		}
	}
}
