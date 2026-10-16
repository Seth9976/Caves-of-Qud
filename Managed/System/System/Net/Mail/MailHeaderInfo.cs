using System;
using System.Collections.Generic;

namespace System.Net.Mail
{
	// Token: 0x020007F8 RID: 2040
	internal static class MailHeaderInfo
	{
		// Token: 0x0600412D RID: 16685 RVA: 0x000DF4F0 File Offset: 0x000DD6F0
		static MailHeaderInfo()
		{
			for (int i = 0; i < MailHeaderInfo.s_headerInfo.Length; i++)
			{
				MailHeaderInfo.s_headerDictionary.Add(MailHeaderInfo.s_headerInfo[i].NormalizedName, i);
			}
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000DF82C File Offset: 0x000DDA2C
		internal static string GetString(MailHeaderID id)
		{
			if (id == MailHeaderID.Unknown || id == (MailHeaderID)33)
			{
				return null;
			}
			return MailHeaderInfo.s_headerInfo[(int)id].NormalizedName;
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x000DF84C File Offset: 0x000DDA4C
		internal static MailHeaderID GetID(string name)
		{
			int num;
			if (!MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num))
			{
				return MailHeaderID.Unknown;
			}
			return (MailHeaderID)num;
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000DF86C File Offset: 0x000DDA6C
		internal static bool IsUserSettable(string name)
		{
			int num;
			return !MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) || MailHeaderInfo.s_headerInfo[num].IsUserSettable;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000DF89C File Offset: 0x000DDA9C
		internal static bool IsSingleton(string name)
		{
			int num;
			return MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) && MailHeaderInfo.s_headerInfo[num].IsSingleton;
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000DF8CC File Offset: 0x000DDACC
		internal static string NormalizeCase(string name)
		{
			int num;
			if (!MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num))
			{
				return name;
			}
			return MailHeaderInfo.s_headerInfo[num].NormalizedName;
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000DF8FC File Offset: 0x000DDAFC
		internal static bool AllowsUnicode(string name)
		{
			int num;
			return !MailHeaderInfo.s_headerDictionary.TryGetValue(name, out num) || MailHeaderInfo.s_headerInfo[num].AllowsUnicode;
		}

		// Token: 0x04002737 RID: 10039
		private static readonly MailHeaderInfo.HeaderInfo[] s_headerInfo = new MailHeaderInfo.HeaderInfo[]
		{
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Bcc, "Bcc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Cc, "Cc", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Comments, "Comments", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDescription, "Content-Description", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDisposition, "Content-Disposition", true, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentID, "Content-ID", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentLocation, "Content-Location", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentTransferEncoding, "Content-Transfer-Encoding", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentType, "Content-Type", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Date, "Date", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.From, "From", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Importance, "Importance", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.InReplyTo, "In-Reply-To", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Keywords, "Keywords", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Max, "Max", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MessageID, "Message-ID", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MimeVersion, "MIME-Version", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Priority, "Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.References, "References", true, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ReplyTo, "Reply-To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentBcc, "Resent-Bcc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentCc, "Resent-Cc", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentDate, "Resent-Date", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentFrom, "Resent-From", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentMessageID, "Resent-Message-ID", false, true, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentSender, "Resent-Sender", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentTo, "Resent-To", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Sender, "Sender", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Subject, "Subject", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.To, "To", true, false, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XPriority, "X-Priority", true, false, false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XReceiver, "X-Receiver", false, true, true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XSender, "X-Sender", true, true, true)
		};

		// Token: 0x04002738 RID: 10040
		private static readonly Dictionary<string, int> s_headerDictionary = new Dictionary<string, int>(33, StringComparer.OrdinalIgnoreCase);

		// Token: 0x020007F9 RID: 2041
		private readonly struct HeaderInfo
		{
			// Token: 0x06004134 RID: 16692 RVA: 0x000DF92A File Offset: 0x000DDB2A
			public HeaderInfo(MailHeaderID id, string name, bool isSingleton, bool isUserSettable, bool allowsUnicode)
			{
				this.ID = id;
				this.NormalizedName = name;
				this.IsSingleton = isSingleton;
				this.IsUserSettable = isUserSettable;
				this.AllowsUnicode = allowsUnicode;
			}

			// Token: 0x04002739 RID: 10041
			public readonly string NormalizedName;

			// Token: 0x0400273A RID: 10042
			public readonly bool IsSingleton;

			// Token: 0x0400273B RID: 10043
			public readonly MailHeaderID ID;

			// Token: 0x0400273C RID: 10044
			public readonly bool IsUserSettable;

			// Token: 0x0400273D RID: 10045
			public readonly bool AllowsUnicode;
		}
	}
}
