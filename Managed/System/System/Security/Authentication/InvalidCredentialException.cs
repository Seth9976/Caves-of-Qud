using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream and cannot be retried.</summary>
	// Token: 0x020002A2 RID: 674
	[Serializable]
	public class InvalidCredentialException : AuthenticationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with no message. </summary>
		// Token: 0x06001524 RID: 5412 RVA: 0x0005572D File Offset: 0x0005392D
		public InvalidCredentialException()
			: base(global::Locale.GetText("Invalid credentials exception."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x06001525 RID: 5413 RVA: 0x0005573F File Offset: 0x0005393F
		public InvalidCredentialException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x06001526 RID: 5414 RVA: 0x00055748 File Offset: 0x00053948
		public InvalidCredentialException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.InvalidCredentialException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance. </param>
		// Token: 0x06001527 RID: 5415 RVA: 0x00055752 File Offset: 0x00053952
		protected InvalidCredentialException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
