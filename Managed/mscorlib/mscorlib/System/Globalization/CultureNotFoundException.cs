using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>The exception thrown when a method is invoked which attempts to construct a culture that is not available on the machine.</summary>
	// Token: 0x02000957 RID: 2391
	[Serializable]
	public class CultureNotFoundException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with its message string set to a system-supplied message.</summary>
		// Token: 0x0600549C RID: 21660 RVA: 0x0011ABA2 File Offset: 0x00118DA2
		public CultureNotFoundException()
			: base(CultureNotFoundException.DefaultMessage)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600549D RID: 21661 RVA: 0x0011ABAF File Offset: 0x00118DAF
		public CultureNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause of the current exception.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x0600549E RID: 21662 RVA: 0x0011ABB8 File Offset: 0x00118DB8
		public CultureNotFoundException(string paramName, string message)
			: base(message, paramName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x0600549F RID: 21663 RVA: 0x0011ABC2 File Offset: 0x00118DC2
		public CultureNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x060054A0 RID: 21664 RVA: 0x0011ABCC File Offset: 0x00118DCC
		public CultureNotFoundException(string paramName, string invalidCultureName, string message)
			: base(message, paramName)
		{
			this._invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060054A1 RID: 21665 RVA: 0x0011ABDD File Offset: 0x00118DDD
		public CultureNotFoundException(string message, string invalidCultureName, Exception innerException)
			: base(message, innerException)
		{
			this._invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060054A2 RID: 21666 RVA: 0x0011ABEE File Offset: 0x00118DEE
		public CultureNotFoundException(string message, int invalidCultureId, Exception innerException)
			: base(message, innerException)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x060054A3 RID: 21667 RVA: 0x0011AC04 File Offset: 0x00118E04
		public CultureNotFoundException(string paramName, int invalidCultureId, string message)
			: base(message, paramName)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060054A4 RID: 21668 RVA: 0x0011AC1C File Offset: 0x00118E1C
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this._invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060054A5 RID: 21669 RVA: 0x0011AC74 File Offset: 0x00118E74
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InvalidCultureId", this._invalidCultureId, typeof(int?));
			info.AddValue("InvalidCultureName", this._invalidCultureName, typeof(string));
		}

		/// <summary>Gets the Culture ID that cannot be found.</summary>
		/// <returns>The invalid Culture ID.</returns>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x0011ACC4 File Offset: 0x00118EC4
		public virtual int? InvalidCultureId
		{
			get
			{
				return this._invalidCultureId;
			}
		}

		/// <summary>Gets the Culture Name that cannot be found.</summary>
		/// <returns>The invalid Culture Name.</returns>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x0011ACCC File Offset: 0x00118ECC
		public virtual string InvalidCultureName
		{
			get
			{
				return this._invalidCultureName;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x0011ACD4 File Offset: 0x00118ED4
		private static string DefaultMessage
		{
			get
			{
				return "Culture is not supported.";
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x0011ACDC File Offset: 0x00118EDC
		private string FormatedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId == null)
				{
					return this.InvalidCultureName;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
			}
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>A text string describing the details of the exception.</returns>
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060054AA RID: 21674 RVA: 0x0011AD24 File Offset: 0x00118F24
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this._invalidCultureId == null && this._invalidCultureName == null)
				{
					return message;
				}
				string text = SR.Format("{0} is an invalid culture identifier.", this.FormatedInvalidCultureId);
				if (message == null)
				{
					return text;
				}
				return message + Environment.NewLine + text;
			}
		}

		// Token: 0x040033D2 RID: 13266
		private string _invalidCultureName;

		// Token: 0x040033D3 RID: 13267
		private int? _invalidCultureId;
	}
}
