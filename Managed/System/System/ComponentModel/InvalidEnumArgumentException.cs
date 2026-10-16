using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.ComponentModel
{
	/// <summary>The exception thrown when using invalid arguments that are enumerators.</summary>
	// Token: 0x02000377 RID: 887
	[Serializable]
	public class InvalidEnumArgumentException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class without a message.</summary>
		// Token: 0x06001D30 RID: 7472 RVA: 0x0006853A File Offset: 0x0006673A
		public InvalidEnumArgumentException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with the specified message.</summary>
		/// <param name="message">The message to display with this exception. </param>
		// Token: 0x06001D31 RID: 7473 RVA: 0x00068543 File Offset: 0x00066743
		public InvalidEnumArgumentException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x06001D32 RID: 7474 RVA: 0x0006854C File Offset: 0x0006674C
		public InvalidEnumArgumentException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with a message generated from the argument, the invalid value, and an enumeration class.</summary>
		/// <param name="argumentName">The name of the argument that caused the exception. </param>
		/// <param name="invalidValue">The value of the argument that failed. </param>
		/// <param name="enumClass">A <see cref="T:System.Type" /> that represents the enumeration class with the valid values. </param>
		// Token: 0x06001D33 RID: 7475 RVA: 0x00068556 File Offset: 0x00066756
		public InvalidEnumArgumentException(string argumentName, int invalidValue, Type enumClass)
			: base(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", argumentName, invalidValue.ToString(CultureInfo.CurrentCulture), enumClass.Name), argumentName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x06001D34 RID: 7476 RVA: 0x0006857C File Offset: 0x0006677C
		protected InvalidEnumArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
