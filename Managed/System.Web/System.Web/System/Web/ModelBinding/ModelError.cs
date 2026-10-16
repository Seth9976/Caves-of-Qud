using System;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an error that occurs during model binding.</summary>
	// Token: 0x0200051D RID: 1309
	[Serializable]
	public class ModelError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified exception.</summary>
		/// <param name="exception">The exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> parameter is null.</exception>
		// Token: 0x060039B5 RID: 14773 RVA: 0x0009A674 File Offset: 0x00098874
		public ModelError(Exception exception)
			: this(exception, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified exception and error message.</summary>
		/// <param name="exception">The exception.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="exception" /> parameter is null.</exception>
		// Token: 0x060039B6 RID: 14774 RVA: 0x0009A67E File Offset: 0x0009887E
		public ModelError(Exception exception, string errorMessage)
			: this(errorMessage)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelError" /> class using the specified error message.</summary>
		/// <param name="errorMessage">The error message.</param>
		// Token: 0x060039B7 RID: 14775 RVA: 0x0009A69C File Offset: 0x0009889C
		public ModelError(string errorMessage)
		{
			this.ErrorMessage = errorMessage ?? string.Empty;
		}

		/// <summary>Gets the exception object.</summary>
		/// <returns>The exception object.</returns>
		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x0009A6B4 File Offset: 0x000988B4
		// (set) Token: 0x060039B9 RID: 14777 RVA: 0x0009A6BC File Offset: 0x000988BC
		public Exception Exception { get; private set; }

		/// <summary>Gets the error message.</summary>
		/// <returns>The error message.</returns>
		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x0009A6C5 File Offset: 0x000988C5
		// (set) Token: 0x060039BB RID: 14779 RVA: 0x0009A6CD File Offset: 0x000988CD
		public string ErrorMessage { get; private set; }
	}
}
