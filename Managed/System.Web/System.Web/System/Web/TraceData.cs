using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web
{
	// Token: 0x020000E1 RID: 225
	internal sealed class TraceData
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001F7EC File Offset: 0x0001D9EC
		public TraceData()
		{
			this.info = new Queue<InfoTraceData>();
			this.control_data = new Queue<ControlTraceData>();
			this.cookie_data = new Queue<NameValueTraceData>();
			this.header_data = new Queue<NameValueTraceData>();
			this.servervar_data = new Queue<NameValueTraceData>();
			this.is_first_time = true;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001F84D File Offset: 0x0001DA4D
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0001F855 File Offset: 0x0001DA55
		public TraceMode TraceMode
		{
			get
			{
				return this._traceMode;
			}
			set
			{
				this._traceMode = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001F85E File Offset: 0x0001DA5E
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0001F866 File Offset: 0x0001DA66
		public string RequestPath
		{
			get
			{
				return this.request_path;
			}
			set
			{
				this.request_path = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0001F86F File Offset: 0x0001DA6F
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0001F877 File Offset: 0x0001DA77
		public string SessionID
		{
			get
			{
				return this.session_id;
			}
			set
			{
				this.session_id = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0001F880 File Offset: 0x0001DA80
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0001F888 File Offset: 0x0001DA88
		public DateTime RequestTime
		{
			get
			{
				return this.request_time;
			}
			set
			{
				this.request_time = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0001F891 File Offset: 0x0001DA91
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0001F899 File Offset: 0x0001DA99
		public Encoding RequestEncoding
		{
			get
			{
				return this.request_encoding;
			}
			set
			{
				this.request_encoding = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0001F8A2 File Offset: 0x0001DAA2
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0001F8AA File Offset: 0x0001DAAA
		public Encoding ResponseEncoding
		{
			get
			{
				return this.response_encoding;
			}
			set
			{
				this.response_encoding = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0001F8B3 File Offset: 0x0001DAB3
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0001F8BB File Offset: 0x0001DABB
		public string RequestType
		{
			get
			{
				return this.request_type;
			}
			set
			{
				this.request_type = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0001F8CC File Offset: 0x0001DACC
		public int StatusCode
		{
			get
			{
				return this.status_code;
			}
			set
			{
				this.status_code = value;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		public void Write(string category, string msg, Exception error, bool Warning)
		{
			double num;
			double num2;
			if (this.is_first_time)
			{
				num = 0.0;
				num2 = 0.0;
				this.prev_time = 0.0;
				this.is_first_time = false;
				this.first_time = DateTime.Now;
			}
			else
			{
				num = (DateTime.Now - this.first_time).TotalSeconds;
				num2 = num - this.prev_time;
				this.prev_time = num;
			}
			this.info.Enqueue(new InfoTraceData(category, TraceData.HtmlEncode(msg), (error != null) ? error.ToString() : null, num, num2, Warning));
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001F974 File Offset: 0x0001DB74
		private static string HtmlEncode(string s)
		{
			if (s == null)
			{
				return "";
			}
			return HttpUtility.HtmlEncode(s).Replace("\n", "<br>").Replace(" ", "&nbsp;");
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001F9A3 File Offset: 0x0001DBA3
		public void AddControlTree(Page page, Hashtable ctrl_vs, Hashtable ctrl_cs, Hashtable sizes)
		{
			this.page = page;
			this.ctrl_vs = ctrl_vs;
			this.sizes = sizes;
			this.ctrl_cs = ctrl_cs;
			this.AddControl(page, 0);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		private void AddControl(Control c, int control_pos)
		{
			this.control_data.Enqueue(new ControlTraceData(c.UniqueID, c.GetType(), this.GetRenderSize(c), TraceData.GetViewStateSize(c, (this.ctrl_vs != null) ? this.ctrl_vs[c] : null), TraceData.GetViewStateSize(c, (this.ctrl_cs != null) ? this.ctrl_cs[c] : null), control_pos));
			if (c.HasControls())
			{
				foreach (object obj in c.Controls)
				{
					Control control = (Control)obj;
					this.AddControl(control, control_pos + 1);
				}
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001FA90 File Offset: 0x0001DC90
		private int GetRenderSize(Control ctrl)
		{
			if (this.sizes == null)
			{
				return 0;
			}
			object obj = this.sizes[ctrl];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001FAC0 File Offset: 0x0001DCC0
		private static int GetViewStateSize(Control ctrl, object vs)
		{
			if (vs == null)
			{
				return 0;
			}
			StringWriter stringWriter = new StringWriter();
			new LosFormatter().Serialize(stringWriter, vs);
			return stringWriter.GetStringBuilder().Length;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0001FAEF File Offset: 0x0001DCEF
		public void AddCookie(string name, string value)
		{
			this.cookie_data.Enqueue(new NameValueTraceData(name, value));
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001FB03 File Offset: 0x0001DD03
		public void AddHeader(string name, string value)
		{
			this.header_data.Enqueue(new NameValueTraceData(name, value));
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001FB17 File Offset: 0x0001DD17
		public void AddServerVar(string name, string value)
		{
			this.servervar_data.Enqueue(new NameValueTraceData(name, value));
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		public void Render(HtmlTextWriter output)
		{
			output.AddAttribute("id", "__asptrace");
			output.RenderBeginTag(HtmlTextWriterTag.Div);
			TraceData.RenderStyleSheet(output);
			output.AddAttribute("class", "tracecontent");
			output.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderRequestDetails(output);
			this.RenderTraceInfo(output);
			this.RenderControlTree(output);
			this.RenderCookies(output);
			this.RenderHeaders(output);
			this.RenderServerVars(output);
			output.RenderEndTag();
			output.RenderEndTag();
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		private void RenderRequestDetails(HtmlTextWriter output)
		{
			Table table = TraceData.CreateTable();
			table.Rows.Add(TraceData.AltRow("Request Details:"));
			table.Rows.Add(this.InfoRow2("Session Id:", this.session_id, "Request Type", this.request_type));
			table.Rows.Add(this.InfoRow2("Time of Request:", this.request_time.ToString(), "State Code:", this.status_code.ToString()));
			table.Rows.Add(this.InfoRow2("Request Encoding:", this.request_encoding.EncodingName, "Response Encoding:", this.response_encoding.EncodingName));
			table.RenderControl(output);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0001FC64 File Offset: 0x0001DE64
		private void RenderTraceInfo(HtmlTextWriter output)
		{
			Table table = TraceData.CreateTable();
			table.Rows.Add(TraceData.AltRow("Trace Information"));
			table.Rows.Add(TraceData.SubHeadRow(new string[] { "Category", "Message", "From First(s)", "From Lasts(s)" }));
			int num = 0;
			IEnumerable<InfoTraceData> enumerable = this.info;
			if (this.TraceMode == TraceMode.SortByCategory)
			{
				List<InfoTraceData> list = new List<InfoTraceData>(this.info);
				list.Sort((InfoTraceData x, InfoTraceData y) => string.Compare(x.Category, y.Category, StringComparison.Ordinal));
				enumerable = list;
			}
			foreach (InfoTraceData infoTraceData in enumerable)
			{
				this.RenderTraceInfoRow(table, infoTraceData, num++);
			}
			table.RenderControl(output);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0001FD54 File Offset: 0x0001DF54
		private void RenderControlTree(HtmlTextWriter output)
		{
			Table table = TraceData.CreateTable();
			int num = ((this.page == null) ? 0 : TraceData.GetViewStateSize(this.page, this.page.GetSavedViewState()));
			table.Rows.Add(TraceData.AltRow("Control Tree"));
			table.Rows.Add(TraceData.SubHeadRow(new string[]
			{
				"Control Id",
				"Type",
				"Render Size Bytes (including children)",
				string.Format("ViewState Size (total: {0} bytes)(excluding children)", num),
				"ControlState Size (excluding children)"
			}));
			int num2 = 0;
			foreach (ControlTraceData controlTraceData in this.control_data)
			{
				this.RenderControlTraceDataRow(table, controlTraceData, num2++);
			}
			table.RenderControl(output);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0001FE40 File Offset: 0x0001E040
		private void RenderControlTraceDataRow(Table table, ControlTraceData r, int pos)
		{
			if (r == null)
			{
				return;
			}
			int depth = r.Depth;
			string text = string.Empty;
			for (int i = 0; i < depth; i++)
			{
				text += "&nbsp;&nbsp;&nbsp;&nbsp;";
			}
			TraceData.RenderAltRow(table, pos, new string[]
			{
				text + r.ControlId,
				r.Type.ToString(),
				r.RenderSize.ToString(),
				r.ViewstateSize.ToString(),
				r.ControlstateSize.ToString()
			});
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0001FECC File Offset: 0x0001E0CC
		private void RenderCookies(HtmlTextWriter output)
		{
			Table table = TraceData.CreateTable();
			table.Rows.Add(TraceData.AltRow("Cookies Collection"));
			table.Rows.Add(TraceData.SubHeadRow(new string[] { "Name", "Value", "Size" }));
			int num = 0;
			foreach (NameValueTraceData nameValueTraceData in this.cookie_data)
			{
				this.RenderCookieDataRow(table, nameValueTraceData, num++);
			}
			table.RenderControl(output);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0001FF7C File Offset: 0x0001E17C
		private void RenderCookieDataRow(Table table, NameValueTraceData r, int pos)
		{
			if (r == null)
			{
				return;
			}
			int num = r.Name.Length + ((r.Value == null) ? 0 : r.Value.Length);
			TraceData.RenderAltRow(table, pos++, new string[]
			{
				r.Name,
				r.Value,
				num.ToString()
			});
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0001FFE0 File Offset: 0x0001E1E0
		private void RenderHeaders(HtmlTextWriter output)
		{
			Table table = TraceData.CreateTable();
			table.Rows.Add(TraceData.AltRow("Headers Collection"));
			table.Rows.Add(TraceData.SubHeadRow(new string[] { "Name", "Value" }));
			int num = 0;
			foreach (NameValueTraceData nameValueTraceData in this.header_data)
			{
				TraceData.RenderAltRow(table, num++, new string[] { nameValueTraceData.Name, nameValueTraceData.Value });
			}
			table.RenderControl(output);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002009C File Offset: 0x0001E29C
		private void RenderServerVars(HtmlTextWriter output)
		{
			Table table = TraceData.CreateTable();
			table.Rows.Add(TraceData.AltRow("Server Variables"));
			table.Rows.Add(TraceData.SubHeadRow(new string[] { "Name", "Value" }));
			int num = 0;
			foreach (NameValueTraceData nameValueTraceData in this.servervar_data)
			{
				TraceData.RenderAltRow(table, num++, new string[] { nameValueTraceData.Name, nameValueTraceData.Value });
			}
			table.RenderControl(output);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00020158 File Offset: 0x0001E358
		internal static TableRow AltRow(string title)
		{
			TableRow tableRow = new TableRow();
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.CssClass = "alt";
			tableHeaderCell.HorizontalAlign = HorizontalAlign.Left;
			tableHeaderCell.Attributes[" colspan"] = "10";
			tableHeaderCell.Text = "<h3><b>" + title + "</b></h3>";
			tableRow.Cells.Add(tableHeaderCell);
			return tableRow;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000201BC File Offset: 0x0001E3BC
		private void RenderTraceInfoRow(Table table, InfoTraceData i, int pos)
		{
			if (i == null)
			{
				return;
			}
			string text2;
			string text = (text2 = string.Empty);
			if (i.IsWarning)
			{
				text2 = "<span style=\"color:red\">";
				text = "</span>";
			}
			string text4;
			string text3;
			if (i.TimeSinceFirst == 0.0)
			{
				text3 = (text4 = string.Empty);
			}
			else
			{
				text4 = i.TimeSinceFirst.ToString("0.000000");
				if (i.TimeSinceLast >= 0.1)
				{
					text3 = "<span style=\"color:red;font-weight:bold\">" + i.TimeSinceLast.ToString("0.000000") + "</span>";
				}
				else
				{
					text3 = i.TimeSinceLast.ToString("0.000000");
				}
			}
			TraceData.RenderAltRow(table, pos, new string[]
			{
				text2 + i.Category + text,
				text2 + i.Message + text,
				text4,
				text3
			});
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00020290 File Offset: 0x0001E490
		internal static TableRow SubHeadRow(params string[] cells)
		{
			TableRow tableRow = new TableRow();
			foreach (string text in cells)
			{
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				tableHeaderCell.Text = text;
				tableRow.Cells.Add(tableHeaderCell);
			}
			tableRow.CssClass = "subhead";
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			return tableRow;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000202E8 File Offset: 0x0001E4E8
		internal static TableRow RenderAltRow(Table table, int pos, params string[] cells)
		{
			TableRow tableRow = new TableRow();
			foreach (string text in cells)
			{
				TableCell tableCell = new TableCell();
				tableCell.Text = text;
				tableRow.Cells.Add(tableCell);
			}
			if (pos % 2 != 0)
			{
				tableRow.CssClass = "alt";
			}
			table.Rows.Add(tableRow);
			return tableRow;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002034C File Offset: 0x0001E54C
		private TableRow InfoRow2(string title1, string info1, string title2, string info2)
		{
			TableRow tableRow = new TableRow();
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			TableHeaderCell tableHeaderCell2 = new TableHeaderCell();
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			tableHeaderCell.Text = title1;
			tableHeaderCell2.Text = title2;
			tableCell.Text = info1;
			tableCell2.Text = info2;
			tableRow.Cells.Add(tableHeaderCell);
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(tableHeaderCell2);
			tableRow.Cells.Add(tableCell2);
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			return tableRow;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000203CE File Offset: 0x0001E5CE
		internal static Table CreateTable()
		{
			return new Table
			{
				Width = Unit.Percentage(100.0),
				CellSpacing = 0,
				CellPadding = 0
			};
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000203F8 File Offset: 0x0001E5F8
		internal static void RenderStyleSheet(HtmlTextWriter o)
		{
			o.WriteLine("<style type=\"text/css\">");
			o.Write("span.tracecontent { background-color:white; ");
			o.WriteLine("color:black;font: 10pt verdana, arial; }");
			o.Write("span.tracecontent table { font: 10pt verdana, ");
			o.WriteLine("arial; cellspacing:0; cellpadding:0; margin-bottom:25}");
			o.WriteLine("span.tracecontent tr.subhead { background-color:cccccc;}");
			o.WriteLine("span.tracecontent th { padding:0,3,0,3 }");
			o.WriteLine("span.tracecontent th.alt { background-color:black; color:white; padding:3,3,2,3; }");
			o.WriteLine("span.tracecontent td { padding:0,3,0,3 }");
			o.WriteLine("span.tracecontent tr.alt { background-color:eeeeee }");
			o.WriteLine("span.tracecontent h1 { font: 24pt verdana, arial; margin:0,0,0,0}");
			o.WriteLine("span.tracecontent h2 { font: 18pt verdana, arial; margin:0,0,0,0}");
			o.WriteLine("span.tracecontent h3 { font: 12pt verdana, arial; margin:0,0,0,0}");
			o.WriteLine("span.tracecontent th a { color:darkblue; font: 8pt verdana, arial; }");
			o.WriteLine("span.tracecontent a { color:darkblue;text-decoration:none }");
			o.WriteLine("span.tracecontent a:hover { color:darkblue;text-decoration:underline; }");
			o.WriteLine("span.tracecontent div.outer { width:90%; margin:15,15,15,15}");
			o.Write("span.tracecontent table.viewmenu td { background-color:006699; ");
			o.WriteLine("color:white; padding:0,5,0,5; }");
			o.WriteLine("span.tracecontent table.viewmenu td.end { padding:0,0,0,0; }");
			o.WriteLine("span.tracecontent table.viewmenu a {color:white; font: 8pt verdana, arial; }");
			o.WriteLine("span.tracecontent table.viewmenu a:hover {color:white; font: 8pt verdana, arial; }");
			o.WriteLine("span.tracecontent a.tinylink {color:darkblue; font: 8pt verdana, ");
			o.WriteLine("arial;text-decoration:underline;}");
			o.WriteLine("span.tracecontent a.link {color:darkblue; text-decoration:underline;}");
			o.WriteLine("span.tracecontent div.buffer {padding-top:7; padding-bottom:17;}");
			o.WriteLine("span.tracecontent .small { font: 8pt verdana, arial }");
			o.WriteLine("span.tracecontent table td { padding-right:20 }");
			o.WriteLine("span.tracecontent table td.nopad { padding-right:5 }");
			o.WriteLine("</style>");
		}

		// Token: 0x040010DA RID: 4314
		private bool is_first_time;

		// Token: 0x040010DB RID: 4315
		private DateTime first_time;

		// Token: 0x040010DC RID: 4316
		private double prev_time;

		// Token: 0x040010DD RID: 4317
		private Queue<InfoTraceData> info;

		// Token: 0x040010DE RID: 4318
		private Queue<ControlTraceData> control_data;

		// Token: 0x040010DF RID: 4319
		private Queue<NameValueTraceData> cookie_data;

		// Token: 0x040010E0 RID: 4320
		private Queue<NameValueTraceData> header_data;

		// Token: 0x040010E1 RID: 4321
		private Queue<NameValueTraceData> servervar_data;

		// Token: 0x040010E2 RID: 4322
		private Hashtable ctrl_cs;

		// Token: 0x040010E3 RID: 4323
		private string request_path;

		// Token: 0x040010E4 RID: 4324
		private string session_id;

		// Token: 0x040010E5 RID: 4325
		private DateTime request_time;

		// Token: 0x040010E6 RID: 4326
		private Encoding request_encoding;

		// Token: 0x040010E7 RID: 4327
		private Encoding response_encoding;

		// Token: 0x040010E8 RID: 4328
		private string request_type;

		// Token: 0x040010E9 RID: 4329
		private int status_code;

		// Token: 0x040010EA RID: 4330
		private Page page;

		// Token: 0x040010EB RID: 4331
		private TraceMode _traceMode = HttpRuntime.TraceManager.TraceMode;

		// Token: 0x040010EC RID: 4332
		private Hashtable sizes;

		// Token: 0x040010ED RID: 4333
		private Hashtable ctrl_vs;
	}
}
