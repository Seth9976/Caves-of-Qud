using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI;

namespace System.Web
{
	/// <summary>Captures and presents execution details about a Web request. This class cannot be inherited.</summary>
	// Token: 0x020000DC RID: 220
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TraceContext
	{
		/// <summary>Raised by the <see cref="T:System.Web.TraceContext" /> object to expose trace messages after all request information is gathered.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000BD2 RID: 3026 RVA: 0x0001F23C File Offset: 0x0001D43C
		// (remove) Token: 0x06000BD3 RID: 3027 RVA: 0x0001F23C File Offset: 0x0001D43C
		public event TraceContextEventHandler TraceFinished
		{
			add
			{
				this.events.AddHandler(TraceContext.traceFinishedEvent, value);
			}
			remove
			{
				this.events.AddHandler(TraceContext.traceFinishedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.TraceContext" /> class.</summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that contains information about the current Web request. </param>
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001F24F File Offset: 0x0001D44F
		public TraceContext(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0001F270 File Offset: 0x0001D470
		internal bool HaveTrace
		{
			get
			{
				return this._haveTrace;
			}
		}

		/// <summary>Gets or sets a value indicating whether tracing is enabled for the current Web request.</summary>
		/// <returns>true if tracing is enabled; otherwise, false. </returns>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0001F278 File Offset: 0x0001D478
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0001F294 File Offset: 0x0001D494
		public bool IsEnabled
		{
			get
			{
				if (!this._haveTrace)
				{
					return this.TraceManager.Enabled;
				}
				return this._Enabled;
			}
			set
			{
				if (value && this.data == null)
				{
					this.data = new TraceData();
				}
				this._haveTrace = true;
				this._Enabled = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0001F2BA File Offset: 0x0001D4BA
		private TraceManager TraceManager
		{
			get
			{
				if (this._traceManager == null)
				{
					this._traceManager = HttpRuntime.TraceManager;
				}
				return this._traceManager;
			}
		}

		/// <summary>Gets or sets the sorted order in which trace messages should be output to a requesting browser.</summary>
		/// <returns>One of the <see cref="T:System.Web.TraceMode" /> enumeration values. The default is the setting specified by the traceMode attribute in the trace section of a configuration file, whose default is SortByTime.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.TraceMode" /> enumeration values.</exception>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0001F2D5 File Offset: 0x0001D4D5
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0001F2F2 File Offset: 0x0001D4F2
		public TraceMode TraceMode
		{
			get
			{
				if (this._Mode != TraceMode.Default)
				{
					return this._Mode;
				}
				return this.TraceManager.TraceMode;
			}
			set
			{
				this._Mode = value;
			}
		}

		/// <summary>Writes a trace message to the trace log. All warnings appear in the log as red text.</summary>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000BDB RID: 3035 RVA: 0x0001F2FB File Offset: 0x0001D4FB
		public void Warn(string message)
		{
			this.Write(string.Empty, message, null, true);
		}

		/// <summary>Writes trace information to the trace log, including any user-defined categories and trace messages. All warnings appear in the log as red text.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000BDC RID: 3036 RVA: 0x0001F30B File Offset: 0x0001D50B
		public void Warn(string category, string message)
		{
			this.Write(category, message, null, true);
		}

		/// <summary>Writes trace information to the trace log, including any user-defined categories, trace messages, and error information. All warnings appear in the log as red text.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		/// <param name="errorInfo">An <see cref="T:System.Exception" /> that contains information about the error. </param>
		// Token: 0x06000BDD RID: 3037 RVA: 0x0001F317 File Offset: 0x0001D517
		public void Warn(string category, string message, Exception errorInfo)
		{
			this.Write(category, message, errorInfo, true);
		}

		/// <summary>Writes a trace message to the trace log.</summary>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000BDE RID: 3038 RVA: 0x0001F323 File Offset: 0x0001D523
		public void Write(string message)
		{
			this.Write(string.Empty, message, null, false);
		}

		/// <summary>Writes trace information to the trace log, including a message and any user-defined categories.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000BDF RID: 3039 RVA: 0x0001F333 File Offset: 0x0001D533
		public void Write(string category, string message)
		{
			this.Write(category, message, null, false);
		}

		/// <summary>Writes trace information to the trace log, including any user-defined categories, trace messages, and error information.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		/// <param name="errorInfo">An <see cref="T:System.Exception" /> that contains information about the error. </param>
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001F33F File Offset: 0x0001D53F
		public void Write(string category, string message, Exception errorInfo)
		{
			this.Write(category, message, errorInfo, false);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001F34B File Offset: 0x0001D54B
		private void Write(string category, string msg, Exception error, bool Warning)
		{
			if (!this.IsEnabled)
			{
				return;
			}
			if (this.data == null)
			{
				this.data = new TraceData();
			}
			this.data.Write(category, msg, error, Warning);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001F37C File Offset: 0x0001D57C
		internal void SaveData()
		{
			if (this.data == null)
			{
				this.data = new TraceData();
			}
			this.data.TraceMode = this._Context.Trace.TraceMode;
			this.SetRequestDetails();
			if (this._Context.Handler is Page)
			{
				this.data.AddControlTree((Page)this._Context.Handler, this.view_states, this.control_states, this.sizes);
			}
			this.AddCookies();
			this.AddHeaders();
			this.AddServerVars();
			this.TraceManager.AddTraceData(this.data);
			this.data_saved = true;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001F426 File Offset: 0x0001D626
		internal void SaveViewState(Control ctrl, object vs)
		{
			if (this.view_states == null)
			{
				this.view_states = new Hashtable();
			}
			this.view_states[ctrl] = vs;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0001F448 File Offset: 0x0001D648
		internal void SaveControlState(Control ctrl, object vs)
		{
			if (this.control_states == null)
			{
				this.control_states = new Hashtable();
			}
			this.control_states[ctrl] = vs;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001F46A File Offset: 0x0001D66A
		internal void SaveSize(Control ctrl, int size)
		{
			if (this.sizes == null)
			{
				this.sizes = new Hashtable();
			}
			this.sizes[ctrl] = size;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001F491 File Offset: 0x0001D691
		internal void Render(HtmlTextWriter output)
		{
			if (!this.data_saved)
			{
				this.SaveData();
			}
			this.data.Render(output);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
		private void SetRequestDetails()
		{
			this.data.RequestPath = this._Context.Request.FilePath;
			this.data.SessionID = ((this._Context.Session != null) ? this._Context.Session.SessionID : string.Empty);
			this.data.RequestType = this._Context.Request.RequestType;
			this.data.RequestTime = this._Context.Timestamp;
			this.data.StatusCode = this._Context.Response.StatusCode;
			this.data.RequestEncoding = this._Context.Request.ContentEncoding;
			this.data.ResponseEncoding = this._Context.Response.ContentEncoding;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001F58C File Offset: 0x0001D78C
		private void AddCookies()
		{
			foreach (object obj in this._Context.Request.Cookies.Keys)
			{
				string text = (string)obj;
				this.data.AddCookie(text, this._Context.Request.Cookies[text].Value);
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001F614 File Offset: 0x0001D814
		private void AddHeaders()
		{
			foreach (object obj in this._Context.Request.Headers.Keys)
			{
				string text = (string)obj;
				this.data.AddHeader(text, this._Context.Request.Headers[text]);
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001F698 File Offset: 0x0001D898
		private void AddServerVars()
		{
			foreach (object obj in this._Context.Request.ServerVariables)
			{
				string text = (string)obj;
				this.data.AddServerVar(text, this._Context.Request.ServerVariables[text]);
			}
		}

		// Token: 0x040010BC RID: 4284
		private static readonly object traceFinishedEvent = new object();

		// Token: 0x040010BD RID: 4285
		private HttpContext _Context;

		// Token: 0x040010BE RID: 4286
		private TraceManager _traceManager;

		// Token: 0x040010BF RID: 4287
		private bool _Enabled;

		// Token: 0x040010C0 RID: 4288
		private TraceMode _Mode = TraceMode.Default;

		// Token: 0x040010C1 RID: 4289
		private TraceData data;

		// Token: 0x040010C2 RID: 4290
		private bool data_saved;

		// Token: 0x040010C3 RID: 4291
		private bool _haveTrace;

		// Token: 0x040010C4 RID: 4292
		private Hashtable view_states;

		// Token: 0x040010C5 RID: 4293
		private Hashtable control_states;

		// Token: 0x040010C6 RID: 4294
		private Hashtable sizes;

		// Token: 0x040010C7 RID: 4295
		private EventHandlerList events = new EventHandlerList();
	}
}
