using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides the abstract base class for the listeners who monitor trace and debug output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000234 RID: 564
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public abstract class TraceListener : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		// Token: 0x0600109D RID: 4253 RVA: 0x00048A5C File Offset: 0x00046C5C
		protected TraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class using the specified name as the listener.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" />. </param>
		// Token: 0x0600109E RID: 4254 RVA: 0x00048A72 File Offset: 0x00046C72
		protected TraceListener(string name)
		{
			this.listenerName = name;
		}

		/// <summary>Gets the custom trace listener attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace listener.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00048A8F File Offset: 0x00046C8F
		public StringDictionary Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets a name for this <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <returns>A name for this <see cref="T:System.Diagnostics.TraceListener" />. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00048AAA File Offset: 0x00046CAA
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x00048AC0 File Offset: 0x00046CC0
		public virtual string Name
		{
			get
			{
				if (this.listenerName != null)
				{
					return this.listenerName;
				}
				return "";
			}
			set
			{
				this.listenerName = value;
			}
		}

		/// <summary>Gets a value indicating whether the trace listener is thread safe. </summary>
		/// <returns>true if the trace listener is thread safe; otherwise, false. The default is false.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool IsThreadSafe
		{
			get
			{
				return false;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A3 RID: 4259 RVA: 0x00048AC9 File Offset: 0x00046CC9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.TraceListener" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060010A4 RID: 4260 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, closes the output stream so it no longer receives tracing or debugging output.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A5 RID: 4261 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, flushes the output buffer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A6 RID: 4262 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Flush()
		{
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00048AD8 File Offset: 0x00046CD8
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00048AE0 File Offset: 0x00046CE0
		public int IndentLevel
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = ((value < 0) ? 0 : value);
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four spaces.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is less than zero.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00048AF0 File Offset: 0x00046CF0
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00048AF8 File Offset: 0x00046CF8
		public int IndentSize
		{
			get
			{
				return this.indentSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("IndentSize", value, SR.GetString("The IndentSize property must be non-negative."));
				}
				this.indentSize = value;
			}
		}

		/// <summary>Gets and sets the trace filter for the trace listener.</summary>
		/// <returns>An object derived from the <see cref="T:System.Diagnostics.TraceFilter" /> base class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00048B20 File Offset: 0x00046D20
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x00048B28 File Offset: 0x00046D28
		[ComVisible(false)]
		public TraceFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent the output.</summary>
		/// <returns>true if the output should be indented; otherwise, false.</returns>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00048B31 File Offset: 0x00046D31
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00048B39 File Offset: 0x00046D39
		protected bool NeedIndent
		{
			get
			{
				return this.needIndent;
			}
			set
			{
				this.needIndent = value;
			}
		}

		/// <summary>Gets or sets the trace output options.</summary>
		/// <returns>A bitwise combination of the enumeration values. The default is <see cref="F:System.Diagnostics.TraceOptions.None" />. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00048B42 File Offset: 0x00046D42
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00048B4A File Offset: 0x00046D4A
		[ComVisible(false)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return this.traceOptions;
			}
			set
			{
				if (value >> 6 != TraceOptions.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.traceOptions = value;
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00048B63 File Offset: 0x00046D63
		internal void SetAttributes(Hashtable attribs)
		{
			TraceUtils.VerifyAttributes(attribs, this.GetSupportedAttributes(), this);
			this.attributes = new StringDictionary();
			this.attributes.ReplaceHashtable(attribs);
		}

		/// <summary>Emits an error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B2 RID: 4274 RVA: 0x00048B89 File Offset: 0x00046D89
		public virtual void Fail(string message)
		{
			this.Fail(message, null);
		}

		/// <summary>Emits an error message and a detailed error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit. </param>
		/// <param name="detailMessage">A detailed message to emit. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B3 RID: 4275 RVA: 0x00048B94 File Offset: 0x00046D94
		public virtual void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(SR.GetString("Fail:"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.WriteLine(stringBuilder.ToString());
		}

		/// <summary>Gets the custom attributes supported by the trace listener.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace listener, or null if there are no custom attributes.</returns>
		// Token: 0x060010B4 RID: 4276 RVA: 0x00002F6A File Offset: 0x0000116A
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, writes the specified message to the listener you create in the derived class.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B5 RID: 4277
		public abstract void Write(string message);

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B6 RID: 4278 RVA: 0x00048BEF File Offset: 0x00046DEF
		public virtual void Write(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			if (o == null)
			{
				return;
			}
			this.Write(o.ToString());
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B7 RID: 4279 RVA: 0x00048C24 File Offset: 0x00046E24
		public virtual void Write(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.Write(message);
				return;
			}
			this.Write(category + ": " + ((message == null) ? string.Empty : message));
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B8 RID: 4280 RVA: 0x00048C78 File Offset: 0x00046E78
		public virtual void Write(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			if (category == null)
			{
				this.Write(o);
				return;
			}
			this.Write((o == null) ? "" : o.ToString(), category);
		}

		/// <summary>Writes the indent to the listener you create when you implement this class, and resets the <see cref="P:System.Diagnostics.TraceListener.NeedIndent" /> property to false.</summary>
		// Token: 0x060010B9 RID: 4281 RVA: 0x00048CCC File Offset: 0x00046ECC
		protected virtual void WriteIndent()
		{
			this.NeedIndent = false;
			for (int i = 0; i < this.indentLevel; i++)
			{
				if (this.indentSize == 4)
				{
					this.Write("    ");
				}
				else
				{
					for (int j = 0; j < this.indentSize; j++)
					{
						this.Write(" ");
					}
				}
			}
		}

		/// <summary>When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010BA RID: 4282
		public abstract void WriteLine(string message);

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010BB RID: 4283 RVA: 0x00048D23 File Offset: 0x00046F23
		public virtual void WriteLine(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString());
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="message">A message to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010BC RID: 4284 RVA: 0x00048D60 File Offset: 0x00046F60
		public virtual void WriteLine(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.WriteLine(message);
				return;
			}
			this.WriteLine(category + ": " + ((message == null) ? string.Empty : message));
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write. </param>
		/// <param name="category">A category name used to organize the output. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010BD RID: 4285 RVA: 0x00048DB4 File Offset: 0x00046FB4
		public virtual void WriteLine(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString(), category);
		}

		/// <summary>Writes trace information, a data object and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data to emit.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060010BE RID: 4286 RVA: 0x00048DF0 File Offset: 0x00046FF0
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			string text = string.Empty;
			if (data != null)
			{
				text = data.ToString();
			}
			this.WriteLine(text);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, an array of data objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of objects to emit as data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060010BF RID: 4287 RVA: 0x00048E48 File Offset: 0x00047048
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			StringBuilder stringBuilder = new StringBuilder();
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					if (data[i] != null)
					{
						stringBuilder.Append(data[i].ToString());
					}
				}
			}
			this.WriteLine(stringBuilder.ToString());
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060010C0 RID: 4288 RVA: 0x00048ED0 File Offset: 0x000470D0
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.TraceEvent(eventCache, source, eventType, id, string.Empty);
		}

		/// <summary>Writes trace information, a message, and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060010C1 RID: 4289 RVA: 0x00048EE2 File Offset: 0x000470E2
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.WriteLine(message);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a formatted array of objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060010C2 RID: 4290 RVA: 0x00048F1C File Offset: 0x0004711C
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			if (args != null)
			{
				this.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
			}
			else
			{
				this.WriteLine(format);
			}
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a message, a related activity identity and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="relatedActivityId"> A <see cref="T:System.Guid" />  object identifying a related activity.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010C3 RID: 4291 RVA: 0x00048F7B File Offset: 0x0004717B
		[ComVisible(false)]
		public virtual void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			this.TraceEvent(eventCache, source, TraceEventType.Transfer, id, message + ", relatedActivityId=" + relatedActivityId.ToString());
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00048FA4 File Offset: 0x000471A4
		private void WriteHeader(string source, TraceEventType eventType, int id)
		{
			this.Write(string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", source, eventType.ToString(), id.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00048FD8 File Offset: 0x000471D8
		private void WriteFooter(TraceEventCache eventCache)
		{
			if (eventCache == null)
			{
				return;
			}
			this.indentLevel++;
			if (this.IsEnabled(TraceOptions.ProcessId))
			{
				this.WriteLine("ProcessId=" + eventCache.ProcessId.ToString());
			}
			if (this.IsEnabled(TraceOptions.LogicalOperationStack))
			{
				this.Write("LogicalOperationStack=");
				Stack logicalOperationStack = eventCache.LogicalOperationStack;
				bool flag = true;
				foreach (object obj in logicalOperationStack)
				{
					if (!flag)
					{
						this.Write(", ");
					}
					else
					{
						flag = false;
					}
					this.Write(obj.ToString());
				}
				this.WriteLine(string.Empty);
			}
			if (this.IsEnabled(TraceOptions.ThreadId))
			{
				this.WriteLine("ThreadId=" + eventCache.ThreadId);
			}
			if (this.IsEnabled(TraceOptions.DateTime))
			{
				this.WriteLine("DateTime=" + eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			if (this.IsEnabled(TraceOptions.Timestamp))
			{
				this.WriteLine("Timestamp=" + eventCache.Timestamp.ToString());
			}
			if (this.IsEnabled(TraceOptions.Callstack))
			{
				this.WriteLine("Callstack=" + eventCache.Callstack);
			}
			this.indentLevel--;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00049148 File Offset: 0x00047348
		internal bool IsEnabled(TraceOptions opts)
		{
			return (opts & this.TraceOutputOptions) > TraceOptions.None;
		}

		// Token: 0x04000A15 RID: 2581
		private int indentLevel;

		// Token: 0x04000A16 RID: 2582
		private int indentSize = 4;

		// Token: 0x04000A17 RID: 2583
		private TraceOptions traceOptions;

		// Token: 0x04000A18 RID: 2584
		private bool needIndent = true;

		// Token: 0x04000A19 RID: 2585
		private string listenerName;

		// Token: 0x04000A1A RID: 2586
		private TraceFilter filter;

		// Token: 0x04000A1B RID: 2587
		private StringDictionary attributes;

		// Token: 0x04000A1C RID: 2588
		internal string initializeData;
	}
}
