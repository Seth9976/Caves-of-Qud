using System;

namespace System.Web.Management
{
	/// <summary>Formats ASP.NET health monitoring event information.</summary>
	// Token: 0x0200052D RID: 1325
	public class WebEventFormatter
	{
		// Token: 0x06003A11 RID: 14865 RVA: 0x00002050 File Offset: 0x00000250
		internal WebEventFormatter()
		{
		}

		/// <summary>Gets or sets the indentation level.</summary>
		/// <returns>The number of tabs used for the indentation level. </returns>
		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x0009AD3F File Offset: 0x00098F3F
		// (set) Token: 0x06003A13 RID: 14867 RVA: 0x0009AD47 File Offset: 0x00098F47
		public int IndentationLevel
		{
			get
			{
				return this.indentation_level;
			}
			set
			{
				this.indentation_level = value;
			}
		}

		/// <summary>Gets or sets the tab size.</summary>
		/// <returns>The number of spaces in a tab.</returns>
		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x0009AD50 File Offset: 0x00098F50
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x0009AD58 File Offset: 0x00098F58
		public int TabSize
		{
			get
			{
				return this.tab_size;
			}
			set
			{
				this.tab_size = value;
			}
		}

		/// <summary>Appends the specified string and a carriage return to the event information.</summary>
		/// <param name="s">The string to add to the event information.</param>
		// Token: 0x06003A16 RID: 14870 RVA: 0x00003A2B File Offset: 0x00001C2B
		public void AppendLine(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the event information in string format.</summary>
		/// <returns>The event information.</returns>
		// Token: 0x06003A17 RID: 14871 RVA: 0x00003A2B File Offset: 0x00001C2B
		public new string ToString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001FB3 RID: 8115
		private int indentation_level;

		// Token: 0x04001FB4 RID: 8116
		private int tab_size;
	}
}
