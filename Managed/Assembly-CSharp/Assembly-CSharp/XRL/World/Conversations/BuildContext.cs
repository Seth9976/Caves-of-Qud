using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XRL.World.Conversations
{
	// Token: 0x02000BD9 RID: 3033
	public class BuildContext
	{
		// Token: 0x060079CC RID: 31180 RVA: 0x002DD1E8 File Offset: 0x002DB3E8
		public void Advance()
		{
			List<ConversationXMLBlueprint> current = this.Current;
			this.Current = this.Next;
			this.Next = current;
			this.Next.Clear();
			this.Errors.Clear();
		}

		// Token: 0x060079CD RID: 31181 RVA: 0x002DD225 File Offset: 0x002DB425
		public void Clear()
		{
			this.Current.Clear();
			this.Next.Clear();
			this.Lineage.Clear();
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x002DD248 File Offset: 0x002DB448
		public void Push(ConversationXMLBlueprint Blueprint)
		{
			this.Lineage.Push(Blueprint);
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x002DD256 File Offset: 0x002DB456
		public ConversationXMLBlueprint Pop()
		{
			return this.Lineage.Pop();
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x002DD263 File Offset: 0x002DB463
		public ConversationXMLBlueprint Peek()
		{
			return this.Lineage.Peek();
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x002DD270 File Offset: 0x002DB470
		public void Record(ConversationXMLBlueprint Blueprint, XmlTextReader Reader)
		{
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x002DD274 File Offset: 0x002DB474
		public bool Missing(ConversationXMLBlueprint Blueprint, string ID)
		{
			BuildContext.DiagnosticInfo diagnosticInfo;
			if (this.Diagnostics.TryGetValue(Blueprint, out diagnosticInfo))
			{
				this.Text.Clear().Append(diagnosticInfo.URI).Append(" line ")
					.Append(diagnosticInfo.LineNumber)
					.Append(": No finalized element by ID '")
					.Append(ID)
					.Append("' could be found to inherit.");
				this.Errors.Add(this.Text.ToString());
			}
			else
			{
				if (this.Lineage.Contains(Blueprint))
				{
					this.AssemblePathID();
				}
				else
				{
					this.AssemblePathID(Blueprint);
				}
				this.Text.Append(": No finalized element by ID '").Append(ID).Append("' could be found to inherit.");
				this.Errors.Add(this.Text.ToString());
			}
			return false;
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x002DD348 File Offset: 0x002DB548
		public string AssemblePathID(string ID, int Skip = 0)
		{
			this.Text.Clear().Append(ID);
			foreach (ConversationXMLBlueprint conversationXMLBlueprint in this.Lineage)
			{
				if (Skip-- <= 0)
				{
					this.Text.Insert(0, '.');
					if (conversationXMLBlueprint.Cardinal > 1)
					{
						this.Text.Insert(0, conversationXMLBlueprint.Cardinal);
					}
					this.Text.Insert(0, conversationXMLBlueprint.ID);
				}
			}
			return this.Text.ToString();
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x002DD3F8 File Offset: 0x002DB5F8
		public string AssemblePathID(ConversationXMLBlueprint Blueprint)
		{
			this.Text.Clear().Append(Blueprint.ID);
			if (Blueprint.Cardinal > 1)
			{
				this.Text.Append(Blueprint.Cardinal);
			}
			foreach (ConversationXMLBlueprint conversationXMLBlueprint in this.Lineage)
			{
				this.Text.Insert(0, '.');
				if (conversationXMLBlueprint.Cardinal > 1)
				{
					this.Text.Insert(0, conversationXMLBlueprint.Cardinal);
				}
				this.Text.Insert(0, conversationXMLBlueprint.ID);
			}
			return this.Text.ToString();
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x002DD4C0 File Offset: 0x002DB6C0
		public string AssemblePathID()
		{
			this.Text.Clear();
			foreach (ConversationXMLBlueprint conversationXMLBlueprint in this.Lineage)
			{
				if (this.Text.Length != 0)
				{
					this.Text.Insert(0, '.');
				}
				if (conversationXMLBlueprint.Cardinal > 1)
				{
					this.Text.Insert(0, conversationXMLBlueprint.Cardinal);
				}
				this.Text.Insert(0, conversationXMLBlueprint.ID);
			}
			return this.Text.ToString();
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x002DD570 File Offset: 0x002DB770
		public string AssembleNamedID(ConversationXMLBlueprint Blueprint, string Of = null)
		{
			this.Text.Clear().Append(Blueprint.Name).Append('.')
				.Append(Of ?? Blueprint.ID);
			ConversationXMLBlueprint conversationXMLBlueprint = this.Lineage.LastOrDefault<ConversationXMLBlueprint>();
			if (conversationXMLBlueprint != null && conversationXMLBlueprint != Blueprint)
			{
				this.Text.Insert(0, '.');
				this.Text.Insert(0, conversationXMLBlueprint.ID);
			}
			return this.Text.ToString();
		}

		// Token: 0x060079D7 RID: 31191 RVA: 0x002DD5EC File Offset: 0x002DB7EC
		public ConversationXMLBlueprint Parent(ConversationXMLBlueprint Blueprint = null)
		{
			bool flag = false;
			foreach (ConversationXMLBlueprint conversationXMLBlueprint in this.Lineage)
			{
				if (flag)
				{
					return conversationXMLBlueprint;
				}
				flag = Blueprint == conversationXMLBlueprint || Blueprint == null;
			}
			return null;
		}

		// Token: 0x040037B2 RID: 14258
		public Dictionary<string, ConversationXMLBlueprint> Finalized = new Dictionary<string, ConversationXMLBlueprint>();

		// Token: 0x040037B3 RID: 14259
		public Dictionary<ConversationXMLBlueprint, BuildContext.DiagnosticInfo> Diagnostics = new Dictionary<ConversationXMLBlueprint, BuildContext.DiagnosticInfo>();

		// Token: 0x040037B4 RID: 14260
		public List<ConversationXMLBlueprint> Current = new List<ConversationXMLBlueprint>();

		// Token: 0x040037B5 RID: 14261
		public List<ConversationXMLBlueprint> Next = new List<ConversationXMLBlueprint>();

		// Token: 0x040037B6 RID: 14262
		public List<string> Errors = new List<string>();

		// Token: 0x040037B7 RID: 14263
		public Stack<ConversationXMLBlueprint> Lineage = new Stack<ConversationXMLBlueprint>();

		// Token: 0x040037B8 RID: 14264
		public StringBuilder Text = new StringBuilder();

		// Token: 0x040037B9 RID: 14265
		public DataFile File;

		// Token: 0x040037BA RID: 14266
		public ModInfo Mod;

		// Token: 0x040037BB RID: 14267
		public string Namespace;

		// Token: 0x02001C06 RID: 7174
		public class DiagnosticInfo
		{
			// Token: 0x04006F01 RID: 28417
			public string URI;

			// Token: 0x04006F02 RID: 28418
			public int LineNumber;

			// Token: 0x04006F03 RID: 28419
			public ModInfo Mod;
		}
	}
}
