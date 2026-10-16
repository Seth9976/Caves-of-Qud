using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	/// <summary>The exception that is thrown when the key specified for accessing an element in a collection does not match any key in the collection.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000AA0 RID: 2720
	[Serializable]
	public class KeyNotFoundException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class using default property values.</summary>
		// Token: 0x0600613A RID: 24890 RVA: 0x00145230 File Offset: 0x00143430
		public KeyNotFoundException()
			: base("The given key was not present in the dictionary.")
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x0600613B RID: 24891 RVA: 0x00145248 File Offset: 0x00143448
		public KeyNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600613C RID: 24892 RVA: 0x0014525C File Offset: 0x0014345C
		public KeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />  that contains contextual information about the source or destination.</param>
		// Token: 0x0600613D RID: 24893 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
