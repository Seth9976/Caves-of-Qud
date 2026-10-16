using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception that is thrown when an invalid type is used with a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020001D5 RID: 469
	[Serializable]
	public class SettingsPropertyWrongTypeException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class.</summary>
		// Token: 0x06000C4B RID: 3147 RVA: 0x0000DC12 File Offset: 0x0000BE12
		public SettingsPropertyWrongTypeException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000C4C RID: 3148 RVA: 0x0000DC2F File Offset: 0x0000BE2F
		public SettingsPropertyWrongTypeException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000C4D RID: 3149 RVA: 0x0002C411 File Offset: 0x0002A611
		protected SettingsPropertyWrongTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000C4E RID: 3150 RVA: 0x000320CC File Offset: 0x000302CC
		public SettingsPropertyWrongTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
