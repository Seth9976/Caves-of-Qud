using System;
using System.Collections.Generic;

namespace XRL.World.Conversations.Parts
{
	/// <summary>Base consumer of predicates to satisfy condition for a derived action.</summary>
	/// <seealso cref="T:XRL.World.Conversations.Parts.ChangeTarget" />
	// Token: 0x02000C00 RID: 3072
	public abstract class IPredicatePart : IConversationPart
	{
		// Token: 0x06007B6B RID: 31595 RVA: 0x002E5CD8 File Offset: 0x002E3ED8
		public override void LoadAttributes(Dictionary<string, string> Attributes)
		{
			foreach (KeyValuePair<string, string> keyValuePair in Attributes)
			{
				if (ConversationDelegates.Predicates.ContainsKey(keyValuePair.Key))
				{
					if (this.Predicates == null)
					{
						this.Predicates = new Dictionary<string, string>();
					}
					this.Predicates[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			base.LoadAttributes(Attributes);
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x002E5D68 File Offset: 0x002E3F68
		public virtual bool Check(bool AnyPredicate = false, bool? Default = null)
		{
			if (Default != null)
			{
				if (!AnyPredicate)
				{
					return this.All(Default.Value);
				}
				return this.Any(Default.Value);
			}
			else
			{
				if (!AnyPredicate)
				{
					return this.All(true);
				}
				return this.Any(false);
			}
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x002E5DA4 File Offset: 0x002E3FA4
		public virtual bool All(bool Default = true)
		{
			if (this.Predicates.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				return Default;
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.Predicates)
			{
				if (!this.ParentElement.CheckPredicate(keyValuePair.Key, keyValuePair.Value, true))
				{
					return false;
				}
			}
			return Default;
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x002E5E24 File Offset: 0x002E4024
		public virtual bool Any(bool Default = false)
		{
			if (this.Predicates.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				return Default;
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.Predicates)
			{
				if (this.ParentElement.CheckPredicate(keyValuePair.Key, keyValuePair.Value, false))
				{
					return true;
				}
			}
			return Default;
		}

		// Token: 0x04003827 RID: 14375
		public Dictionary<string, string> Predicates;
	}
}
