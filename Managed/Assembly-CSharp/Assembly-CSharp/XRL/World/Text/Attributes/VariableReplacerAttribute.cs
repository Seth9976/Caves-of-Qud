using System;

namespace XRL.World.Text.Attributes
{
	/// <summary>Marks a simple variable replacer delegate.</summary>
	// Token: 0x0200146B RID: 5227
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class VariableReplacerAttribute : Attribute
	{
		// Token: 0x0600CD9A RID: 52634 RVA: 0x004A7E90 File Offset: 0x004A6090
		public VariableReplacerAttribute()
		{
		}

		// Token: 0x0600CD9B RID: 52635 RVA: 0x004A7EA3 File Offset: 0x004A60A3
		public VariableReplacerAttribute(params string[] Keys)
		{
			this.Keys = Keys;
		}

		/// <summary>A range of keys used to invoke this delegate.</summary>
		// Token: 0x04004F71 RID: 20337
		public string[] Keys = Array.Empty<string>();

		/// <summary>A default string to return if applicable.</summary>
		// Token: 0x04004F72 RID: 20338
		public string Default;

		/// <summary>Generate an additional delegate with capitalized <see cref="F:XRL.World.Text.Attributes.VariableReplacerAttribute.Keys" /> and <see cref="F:XRL.World.Text.Attributes.VariableReplacerAttribute.Default" />.</summary>
		// Token: 0x04004F73 RID: 20339
		public bool Capitalization;

		/// <summary>Override a preceding delegate of the same key.</summary>
		/// <remarks>Modding utility, unused in base game.</remarks>
		// Token: 0x04004F74 RID: 20340
		public bool Override;

		/// <summary>Arbitrary flags passed directly from attribute to delegate context.</summary>
		/// <remarks>Modding utility, unused in base game.</remarks>
		// Token: 0x04004F75 RID: 20341
		public int Flags;
	}
}
