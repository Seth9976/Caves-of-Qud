using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Text.Delegates
{
	/// <remarks>Used to decouple parameters from delegate signature for mod compatibility.</remarks>
	// Token: 0x02001466 RID: 5222
	public class DelegateContext
	{
		// Token: 0x0600CD2D RID: 52525 RVA: 0x004A5DB8 File Offset: 0x004A3FB8
		internal static DelegateContext Set(StringBuilder Value, List<string> Parameters, GameObject Target, IPronounProvider Pronouns, TargetType Type, string Explicit, string Default, bool Capitalize, int Flags)
		{
			DelegateContext.Instance.Value = Value;
			DelegateContext.Instance.Parameters = Parameters;
			DelegateContext.Instance.Target = Target;
			DelegateContext.Instance.Type = Type;
			DelegateContext.Instance.Pronouns = Pronouns ?? ((Target != null) ? Target.GetPronounProvider(false) : null) ?? Gender.DefaultNeuter;
			DelegateContext.Instance.Explicit = Explicit;
			DelegateContext.Instance.Default = Default;
			DelegateContext.Instance.Capitalize = Capitalize;
			DelegateContext.Instance.Flags = Flags;
			return DelegateContext.Instance;
		}

		// Token: 0x04004F5A RID: 20314
		public StringBuilder Value;

		/// <summary>The target game object as defined by the attribute, if applicable.</summary>
		// Token: 0x04004F5B RID: 20315
		public GameObject Target;

		/// <summary>Pronouns of the target, defaults to the neuter pronoun provider.</summary>
		// Token: 0x04004F5C RID: 20316
		public IPronounProvider Pronouns = Gender.DefaultNeuter;

		/// <summary>The target type.</summary>
		// Token: 0x04004F5D RID: 20317
		public TargetType Type;

		/// <summary>The explicit string target, used for non-game object transformations.</summary>
		// Token: 0x04004F5E RID: 20318
		public string Explicit;

		/// <summary>An optional default string.</summary>
		/// <remarks>Defined in attribute.</remarks>
		// Token: 0x04004F5F RID: 20319
		public string Default;

		/// <summary>Whether the output should be capitalized.</summary>
		// Token: 0x04004F60 RID: 20320
		public bool Capitalize;

		/// <summary>Attribute flags, mod field.</summary>
		// Token: 0x04004F61 RID: 20321
		public int Flags;

		/// <summary>A list of XML provided parameters.</summary>
		// Token: 0x04004F62 RID: 20322
		public List<string> Parameters;

		// Token: 0x04004F63 RID: 20323
		internal static DelegateContext Instance = new DelegateContext();
	}
}
