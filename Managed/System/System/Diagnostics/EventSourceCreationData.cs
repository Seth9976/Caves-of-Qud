using System;

namespace System.Diagnostics
{
	/// <summary>Represents the configuration settings used to create an event log source on the local computer or a remote computer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000269 RID: 617
	public class EventSourceCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSourceCreationData" /> class with a specified event source and event log name.</summary>
		/// <param name="source">The name to register with the event log as a source of entries. </param>
		/// <param name="logName">The name of the log to which entries from the source are written. </param>
		// Token: 0x06001341 RID: 4929 RVA: 0x000512DD File Offset: 0x0004F4DD
		public EventSourceCreationData(string source, string logName)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = ".";
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000512FE File Offset: 0x0004F4FE
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			if (logName == null || logName.Length == 0)
			{
				this._logName = "Application";
			}
			else
			{
				this._logName = logName;
			}
			this._machineName = machineName;
		}

		/// <summary>Gets or sets the number of categories in the category resource file.</summary>
		/// <returns>The number of categories in the category resource file. The default value is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00051333 File Offset: 0x0004F533
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x0005133B File Offset: 0x0004F53B
		public int CategoryCount
		{
			get
			{
				return this._categoryCount;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryCount = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains category strings for the source.</summary>
		/// <returns>The path of the category resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00051353 File Offset: 0x0004F553
		// (set) Token: 0x06001346 RID: 4934 RVA: 0x0005135B File Offset: 0x0004F55B
		public string CategoryResourceFile
		{
			get
			{
				return this._categoryResourceFile;
			}
			set
			{
				this._categoryResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name of the event log to which the source writes entries.</summary>
		/// <returns>The name of the event log. This can be Application, System, or a custom log name. The default value is "Application."</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00051364 File Offset: 0x0004F564
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x0005136C File Offset: 0x0004F56C
		public string LogName
		{
			get
			{
				return this._logName;
			}
			set
			{
				this._logName = value;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to register the event source.</summary>
		/// <returns>The name of the system on which to register the event source. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00051375 File Offset: 0x0004F575
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x0005137D File Offset: 0x0004F57D
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
			set
			{
				this._machineName = value;
			}
		}

		/// <summary>Gets or sets the path of the message resource file that contains message formatting strings for the source.</summary>
		/// <returns>The path of the message resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00051386 File Offset: 0x0004F586
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x0005138E File Offset: 0x0004F58E
		public string MessageResourceFile
		{
			get
			{
				return this._messageResourceFile;
			}
			set
			{
				this._messageResourceFile = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains message parameter strings for the source.</summary>
		/// <returns>The path of the parameter resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00051397 File Offset: 0x0004F597
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x0005139F File Offset: 0x0004F59F
		public string ParameterResourceFile
		{
			get
			{
				return this._parameterResourceFile;
			}
			set
			{
				this._parameterResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name to register with the event log as an event source.</summary>
		/// <returns>The name to register with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x000513A8 File Offset: 0x0004F5A8
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x000513B0 File Offset: 0x0004F5B0
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x04000B00 RID: 2816
		private string _source;

		// Token: 0x04000B01 RID: 2817
		private string _logName;

		// Token: 0x04000B02 RID: 2818
		private string _machineName;

		// Token: 0x04000B03 RID: 2819
		private string _messageResourceFile;

		// Token: 0x04000B04 RID: 2820
		private string _parameterResourceFile;

		// Token: 0x04000B05 RID: 2821
		private string _categoryResourceFile;

		// Token: 0x04000B06 RID: 2822
		private int _categoryCount;
	}
}
