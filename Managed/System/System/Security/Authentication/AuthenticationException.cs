using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream.</summary>
	// Token: 0x0200029E RID: 670
	[Serializable]
	public class AuthenticationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with no message.</summary>
		// Token: 0x06001520 RID: 5408 RVA: 0x00055711 File Offset: 0x00053911
		public AuthenticationException()
			: base(global::Locale.GetText("Authentication exception."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x06001521 RID: 5409 RVA: 0x0002F140 File Offset: 0x0002D340
		public AuthenticationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x06001522 RID: 5410 RVA: 0x0002F175 File Offset: 0x0002D375
		public AuthenticationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.AuthenticationException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance. </param>
		// Token: 0x06001523 RID: 5411 RVA: 0x00055723 File Offset: 0x00053923
		protected AuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
