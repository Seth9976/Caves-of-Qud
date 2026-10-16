using System;
using XRL.World;

namespace XRL
{
	/// <summary>Registers and unregisters event handlers to a source.</summary>
	/// <remarks>This allows implementing a single method for both registration and unregistration.</remarks>
	// Token: 0x020004E0 RID: 1248
	public interface IEventRegistrar
	{
		/// <summary>Whether this instance will unregister provided events.</summary>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060035EB RID: 13803
		bool IsUnregister { get; }

		/// <summary>Register a handler to receive a <see cref="T:XRL.World.MinEvent" /> from the specified source.</summary>
		// Token: 0x060035EC RID: 13804
		void Register(IEventSource Source, IEventHandler Handler, int EventID, int Order = 0, bool Serialize = false);

		/// <summary>Register this handler to receive a <see cref="T:XRL.World.MinEvent" /> from the specified source.</summary>
		// Token: 0x060035ED RID: 13805
		void Register(IEventSource Source, int EventID, int Order = 0, bool Serialize = false);

		/// <summary>Register this handler to receive a <see cref="T:XRL.World.MinEvent" /> from the default source.</summary>
		// Token: 0x060035EE RID: 13806
		void Register(int EventID, int Order = 0, bool Serialize = false);

		/// <summary>Register this handler to receive a <see cref="T:XRL.World.Event" /> from the default source.</summary>
		/// <remarks>It's recommended to avoid using string events where possible.</remarks>
		// Token: 0x060035EF RID: 13807
		void Register(string EventID);

		// Token: 0x060035F0 RID: 13808 RVA: 0x001044F5 File Offset: 0x001026F5
		[Obsolete("Use Register(string EventId)")]
		void RegisterPartEvent(IPart Ef, string Event)
		{
			this.Register(Event);
		}
	}
}
