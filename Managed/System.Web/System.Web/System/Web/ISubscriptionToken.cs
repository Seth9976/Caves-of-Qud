using System;

namespace System.Web
{
	/// <summary>Represents an interface that is implemented by an object and that can be used to unsubscribe listeners.</summary>
	// Token: 0x0200004D RID: 77
	public interface ISubscriptionToken
	{
		/// <summary>Returns a value that indicates whether the subscription is currently active.</summary>
		/// <returns>true if the subscription is currently active; otherwise, false.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060003D1 RID: 977
		bool IsActive { get; }

		/// <summary>Unsubscribes a listener from the event.</summary>
		// Token: 0x060003D2 RID: 978
		void Unsubscribe();
	}
}
