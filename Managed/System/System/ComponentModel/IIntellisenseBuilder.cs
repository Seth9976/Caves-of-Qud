using System;

namespace System.ComponentModel
{
	/// <summary>Provides an interface to facilitate the retrieval of the builder's name and to display the builder.</summary>
	// Token: 0x020003B9 RID: 953
	public interface IIntellisenseBuilder
	{
		/// <summary>Gets a localized name.</summary>
		/// <returns>A localized name.</returns>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001F1B RID: 7963
		string Name { get; }

		/// <summary>Shows the builder.</summary>
		/// <returns>true if the value should be replaced with <paramref name="newValue" />; otherwise, false (if the user cancels, for example).</returns>
		/// <param name="language">The language service that is calling the builder.</param>
		/// <param name="value">The expression being edited.</param>
		/// <param name="newValue">The new value.</param>
		// Token: 0x06001F1C RID: 7964
		bool Show(string language, string value, ref string newValue);
	}
}
