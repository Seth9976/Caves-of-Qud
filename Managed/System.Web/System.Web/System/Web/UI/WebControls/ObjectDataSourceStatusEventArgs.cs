using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Selected" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Inserted" />, <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Updated" />, and <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleted" /> events of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	// Token: 0x020002F2 RID: 754
	public class ObjectDataSourceStatusEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> class using the specified output parameters and return value.</summary>
		/// <param name="returnValue">An object that represents a return value for the completed database operation. </param>
		/// <param name="outputParameters">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs of parameter objects. </param>
		// Token: 0x06001B90 RID: 7056 RVA: 0x0004377C File Offset: 0x0004197C
		public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters)
			: this(returnValue, outputParameters, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> class using the specified output parameters, return value, and exception.</summary>
		/// <param name="returnValue">An object that represents a return value for the completed database operation. </param>
		/// <param name="outputParameters">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs of parameter objects. </param>
		/// <param name="exception">An <see cref="T:System.Exception" /> that wraps any internal exceptions thrown during the method call. </param>
		// Token: 0x06001B91 RID: 7057 RVA: 0x00043787 File Offset: 0x00041987
		public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters, Exception exception)
		{
			this._returnValue = returnValue;
			this._outputParameters = outputParameters;
			this._exception = exception;
		}

		/// <summary>Gets a collection that contains business object method parameters and their values.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent the business object method parameters and their corresponding values.</returns>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000437AB File Offset: 0x000419AB
		public IDictionary OutputParameters
		{
			get
			{
				return this._outputParameters;
			}
		}

		/// <summary>Gets a wrapper for any exceptions that are thrown by the method that is called by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control during a data operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that wraps any exceptions thrown by the business object in its <see cref="P:System.Exception.InnerException" />.</returns>
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000437B3 File Offset: 0x000419B3
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets or sets a value indicating whether an exception that was thrown by the business object has been handled.</summary>
		/// <returns>true if an exception thrown by the business object has been handled and should not be thrown by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />; otherwise, false.</returns>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000437BB File Offset: 0x000419BB
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x000437C3 File Offset: 0x000419C3
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		/// <summary>Gets the return value that is returned by the business object method, if any, as an object.</summary>
		/// <returns>An object that represents the return value returned by the business object method; otherwise, null, if the business object method returns no value.</returns>
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000437CC File Offset: 0x000419CC
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		/// <summary>Gets or sets the number of rows that are affected by the data operation.</summary>
		/// <returns>The number of rows affected by the data operation.</returns>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000437D4 File Offset: 0x000419D4
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x000437DC File Offset: 0x000419DC
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
			set
			{
				this._affectedRows = value;
			}
		}

		// Token: 0x04001728 RID: 5928
		private object _returnValue;

		// Token: 0x04001729 RID: 5929
		private IDictionary _outputParameters;

		// Token: 0x0400172A RID: 5930
		private Exception _exception;

		// Token: 0x0400172B RID: 5931
		private bool _exceptionHandled;

		// Token: 0x0400172C RID: 5932
		private int _affectedRows = -1;
	}
}
