using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> method calls a method that throws an exception.</summary>
	// Token: 0x02000710 RID: 1808
	[Serializable]
	public class PingException : InvalidOperationException
	{
		// Token: 0x060039CE RID: 14798 RVA: 0x000A716A File Offset: 0x000A536A
		internal PingException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class with serialized data. </summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the contextual information about the source or destination for this serialization.</param>
		// Token: 0x060039CF RID: 14799 RVA: 0x000A717B File Offset: 0x000A537B
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060039D0 RID: 14800 RVA: 0x000A7172 File Offset: 0x000A5372
		public PingException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" />  class using the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		/// <param name="innerException">The exception that causes the current exception.</param>
		// Token: 0x060039D1 RID: 14801 RVA: 0x000C8EF7 File Offset: 0x000C70F7
		public PingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
