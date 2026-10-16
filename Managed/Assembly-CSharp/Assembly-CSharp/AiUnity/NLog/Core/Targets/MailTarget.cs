using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using AiUnity.Common.Attributes;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200021D RID: 541
	[Target("Email")]
	[Preserve]
	public class MailTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x060013E8 RID: 5096 RVA: 0x000721A0 File Offset: 0x000703A0
		public MailTarget()
		{
			this.Body = "${message}${newline}";
			this.Subject = "Message from NLog on ${machinename}";
			this.Encoding = Encoding.UTF8;
			this.SmtpPort = 3325;
			this.SmtpAuthentication = SmtpAuthenticationMode.Basic;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000721F0 File Offset: 0x000703F0
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x000721F7 File Offset: 0x000703F7
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x000721FF File Offset: 0x000703FF
		[RequiredParameter]
		[Display("From", "From email address.", false, 0)]
		public Layout From { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00072208 File Offset: 0x00070408
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00072210 File Offset: 0x00070410
		[RequiredParameter]
		[Display("To", "To email address.", false, 0)]
		public Layout To { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00072219 File Offset: 0x00070419
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00072221 File Offset: 0x00070421
		public Layout CC { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0007222A File Offset: 0x0007042A
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00072232 File Offset: 0x00070432
		public Layout Bcc { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0007223B File Offset: 0x0007043B
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00072243 File Offset: 0x00070443
		public bool AddNewLines { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0007224C File Offset: 0x0007044C
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00072254 File Offset: 0x00070454
		[DefaultValue("Message from NLog on ${machinename}")]
		[RequiredParameter]
		[Display("Subject", "Subject of email.", false, 0)]
		public Layout Subject { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0007225D File Offset: 0x0007045D
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x00072265 File Offset: 0x00070465
		[DefaultValue("${message}")]
		public Layout Body
		{
			get
			{
				return this.Layout;
			}
			set
			{
				this.Layout = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0007226E File Offset: 0x0007046E
		// (set) Token: 0x060013F9 RID: 5113 RVA: 0x00072276 File Offset: 0x00070476
		[DefaultValue("UTF8")]
		public Encoding Encoding { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0007227F File Offset: 0x0007047F
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x00072287 File Offset: 0x00070487
		[DefaultValue(false)]
		public bool Html { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00072290 File Offset: 0x00070490
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x00072298 File Offset: 0x00070498
		[Display("Server", "Smtp Server address", false, 0)]
		public Layout SmtpServer { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000722A1 File Offset: 0x000704A1
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x000722A9 File Offset: 0x000704A9
		[Display("Auth Mode", "Smtp authentication mode.", false, 0)]
		[DefaultValue("Basic")]
		public SmtpAuthenticationMode SmtpAuthentication { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x000722B2 File Offset: 0x000704B2
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x000722BA File Offset: 0x000704BA
		[Display("Smtp username", "Smtp username for authentication", false, 0)]
		public Layout SmtpUserName { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x000722C3 File Offset: 0x000704C3
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x000722CB File Offset: 0x000704CB
		[Display("Smtp password", "Smtp password for authentication", false, 0)]
		public Layout SmtpPassword { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x000722D4 File Offset: 0x000704D4
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x000722DC File Offset: 0x000704DC
		[DefaultValue(false)]
		public bool EnableSsl { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000722E5 File Offset: 0x000704E5
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x000722ED File Offset: 0x000704ED
		[DefaultValue(25)]
		public int SmtpPort { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000722F6 File Offset: 0x000704F6
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x000722FE File Offset: 0x000704FE
		[DefaultValue(false)]
		public bool UseSystemNetMailSettings { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00072307 File Offset: 0x00070507
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x0007230F File Offset: 0x0007050F
		public Layout Priority { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00072318 File Offset: 0x00070518
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x00072320 File Offset: 0x00070520
		[DefaultValue(false)]
		public bool ReplaceNewlineWithBrTagInHtml { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00072329 File Offset: 0x00070529
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x00072331 File Offset: 0x00070531
		[DefaultValue(10000)]
		public int Timeout { get; set; }

		// Token: 0x06001410 RID: 5136 RVA: 0x0007233A File Offset: 0x0007053A
		internal virtual ISmtpClient CreateSmtpClient()
		{
			return new MySmtpClient();
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00072341 File Offset: 0x00070541
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			this.Write(new AsyncLogEventInfo[] { logEvent });
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00072358 File Offset: 0x00070558
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			foreach (KeyValuePair<string, List<AsyncLogEventInfo>> keyValuePair in logEvents.BucketSort<AsyncLogEventInfo, string>((AsyncLogEventInfo c) => this.GetSmtpSettingsKey(c.LogEvent)))
			{
				List<AsyncLogEventInfo> value = keyValuePair.Value;
				this.ProcessSingleMailMessage(value);
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x000723C0 File Offset: 0x000705C0
		private void ProcessSingleMailMessage(List<AsyncLogEventInfo> events)
		{
			try
			{
				LogEventInfo logEvent = events[0].LogEvent;
				LogEventInfo logEvent2 = events[events.Count - 1].LogEvent;
				StringBuilder stringBuilder = new StringBuilder();
				if (base.Header != null)
				{
					stringBuilder.Append(base.Header.Render(logEvent));
					if (this.AddNewLines)
					{
						stringBuilder.Append(Environment.NewLine);
					}
				}
				foreach (AsyncLogEventInfo asyncLogEventInfo in events)
				{
					stringBuilder.Append(this.Layout.Render(asyncLogEventInfo.LogEvent));
					if (this.AddNewLines)
					{
						stringBuilder.Append(Environment.NewLine);
					}
				}
				if (base.Footer != null)
				{
					stringBuilder.Append(base.Footer.Render(logEvent2));
					if (this.AddNewLines)
					{
						stringBuilder.Append(Environment.NewLine);
					}
				}
				using (MailMessage mailMessage = new MailMessage())
				{
					this.SetupMailMessage(mailMessage, logEvent2);
					mailMessage.Body = stringBuilder.ToString();
					if (mailMessage.IsBodyHtml && this.ReplaceNewlineWithBrTagInHtml)
					{
						mailMessage.Body = mailMessage.Body.Replace(Environment.NewLine, "<br/>");
					}
					using (ISmtpClient smtpClient = this.CreateSmtpClient())
					{
						if (!this.UseSystemNetMailSettings)
						{
							this.ConfigureMailClient(logEvent2, smtpClient);
						}
						MailTarget.Logger.Debug("Sending mail to {0} using {1}:{2} (ssl={3})", new object[] { mailMessage.To, smtpClient.Host, smtpClient.Port, smtpClient.EnableSsl });
						MailTarget.Logger.Trace("  Subject: '{0}'", new object[] { mailMessage.Subject });
						MailTarget.Logger.Trace("  From: '{0}'", new object[] { mailMessage.From.ToString() });
						smtpClient.Send(mailMessage);
						foreach (AsyncLogEventInfo asyncLogEventInfo2 in events)
						{
							asyncLogEventInfo2.Continuation(null);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				foreach (AsyncLogEventInfo asyncLogEventInfo3 in events)
				{
					asyncLogEventInfo3.Continuation(ex);
				}
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000726DC File Offset: 0x000708DC
		private void ConfigureMailClient(LogEventInfo lastEvent, ISmtpClient client)
		{
			client.Host = this.SmtpServer.Render(lastEvent);
			client.Port = this.SmtpPort;
			client.EnableSsl = this.EnableSsl;
			client.Timeout = this.Timeout;
			if (this.SmtpAuthentication == SmtpAuthenticationMode.Ntlm)
			{
				MailTarget.Logger.Trace("  Using NTLM authentication.", Array.Empty<object>());
				client.Credentials = CredentialCache.DefaultNetworkCredentials;
				return;
			}
			if (this.SmtpAuthentication == SmtpAuthenticationMode.Basic)
			{
				string text = this.SmtpUserName.Render(lastEvent);
				string text2 = this.SmtpPassword.Render(lastEvent);
				MailTarget.Logger.Trace("  Using basic authentication: Username='{0}' Password='{1}'", new object[]
				{
					text,
					new string('*', text2.Length)
				});
				client.Credentials = new NetworkCredential(text, text2);
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000727A4 File Offset: 0x000709A4
		private string GetSmtpSettingsKey(LogEventInfo logEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.From.Render(logEvent));
			stringBuilder.Append("|");
			stringBuilder.Append(this.To.Render(logEvent));
			stringBuilder.Append("|");
			if (this.CC != null)
			{
				stringBuilder.Append(this.CC.Render(logEvent));
			}
			stringBuilder.Append("|");
			if (this.Bcc != null)
			{
				stringBuilder.Append(this.Bcc.Render(logEvent));
			}
			stringBuilder.Append("|");
			if (this.SmtpServer != null)
			{
				stringBuilder.Append(this.SmtpServer.Render(logEvent));
			}
			if (this.SmtpPassword != null)
			{
				stringBuilder.Append(this.SmtpPassword.Render(logEvent));
			}
			stringBuilder.Append("|");
			if (this.SmtpUserName != null)
			{
				stringBuilder.Append(this.SmtpUserName.Render(logEvent));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000728A8 File Offset: 0x00070AA8
		private void SetupMailMessage(MailMessage msg, LogEventInfo logEvent)
		{
			msg.From = new MailAddress(this.From.Render(logEvent));
			foreach (string text in this.To.Render(logEvent).Split(';', StringSplitOptions.None))
			{
				msg.To.Add(text);
			}
			if (this.Bcc != null)
			{
				foreach (string text2 in this.Bcc.Render(logEvent).Split(';', StringSplitOptions.None))
				{
					msg.Bcc.Add(text2);
				}
			}
			if (this.CC != null)
			{
				foreach (string text3 in this.CC.Render(logEvent).Split(';', StringSplitOptions.None))
				{
					msg.CC.Add(text3);
				}
			}
			msg.Subject = this.Subject.Render(logEvent).Trim();
			msg.BodyEncoding = this.Encoding;
			msg.IsBodyHtml = this.Html;
			if (this.Priority != null)
			{
				string text4 = this.Priority.Render(logEvent);
				try
				{
					msg.Priority = (MailPriority)Enum.Parse(typeof(MailPriority), text4, true);
				}
				catch
				{
					MailTarget.Logger.Warn("Could not convert '{0}' to MailPriority, valid values are Low, Normal and High. Using normal priority as fallback.", Array.Empty<object>());
					msg.Priority = MailPriority.Normal;
				}
			}
		}
	}
}
