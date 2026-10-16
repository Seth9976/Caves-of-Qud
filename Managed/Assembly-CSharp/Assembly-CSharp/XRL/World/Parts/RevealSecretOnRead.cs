using System;
using System.Collections.Generic;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x0200122C RID: 4652
	[Serializable]
	public class RevealSecretOnRead : IPart
	{
		// Token: 0x17000B32 RID: 2866
		// (set) Token: 0x0600B8C7 RID: 47303 RVA: 0x004363A4 File Offset: 0x004345A4
		public string Secrets
		{
			set
			{
				if (this.SecretID == null)
				{
					this.SecretID = new List<string>();
				}
				if (!value.Contains(','))
				{
					this.SecretID.Add(value);
					return;
				}
				foreach (ReadOnlySpan<char> readOnlySpan in value.DelimitedBy(','))
				{
					this.SecretID.Add(new string(readOnlySpan));
				}
			}
		}

		// Token: 0x0600B8C8 RID: 47304 RVA: 0x00436410 File Offset: 0x00434610
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<AfterReadBookEvent>.ID;
		}

		// Token: 0x0600B8C9 RID: 47305 RVA: 0x0043641C File Offset: 0x0043461C
		public override bool HandleEvent(AfterReadBookEvent E)
		{
			if (!this.SecretID.IsNullOrEmpty<string>())
			{
				foreach (string text in this.SecretID)
				{
					IBaseJournalEntry baseJournalEntry;
					if (JournalAPI.NotesByID.TryGetValue(text, out baseJournalEntry) && !baseJournalEntry.Revealed)
					{
						baseJournalEntry.Reveal(null, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8CA RID: 47306 RVA: 0x0043649C File Offset: 0x0043469C
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			List<string> secretID = this.SecretID;
			int num = ((secretID != null) ? secretID.Count : 0);
			Writer.WriteOptimized(num);
			for (int i = 0; i < num; i++)
			{
				Writer.WriteOptimized(this.SecretID[i]);
			}
		}

		// Token: 0x0600B8CB RID: 47307 RVA: 0x004364E4 File Offset: 0x004346E4
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.SecretID = new List<string>(num);
				for (int i = 0; i < num; i++)
				{
					this.SecretID.Add(Reader.ReadOptimizedString());
				}
			}
		}

		// Token: 0x04004A64 RID: 19044
		public List<string> SecretID;
	}
}
