using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown in <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> when the filter criteria is not valid for the type of filter you are using.</summary>
	// Token: 0x020008A7 RID: 2215
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the default properties.</summary>
		// Token: 0x06004907 RID: 18695 RVA: 0x000EE950 File Offset: 0x000ECB50
		public InvalidFilterCriteriaException()
			: this("Specified filter criteria was invalid.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the given HRESULT and message string.</summary>
		/// <param name="message">The message text for the exception. </param>
		// Token: 0x06004908 RID: 18696 RVA: 0x000EE95D File Offset: 0x000ECB5D
		public InvalidFilterCriteriaException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06004909 RID: 18697 RVA: 0x000EE967 File Offset: 0x000ECB67
		public InvalidFilterCriteriaException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232831;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">A SerializationInfo object that contains the information required to serialize this instance. </param>
		/// <param name="context">A StreamingContext object that contains the source and destination of the serialized stream associated with this instance. </param>
		// Token: 0x0600490A RID: 18698 RVA: 0x0006E7C5 File Offset: 0x0006C9C5
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
