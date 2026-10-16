using System;

namespace System.Web.UI
{
	/// <summary>Defines the method that ASP.NET server controls must implement to recognize when elements, either HTML or XML, are parsed.</summary>
	// Token: 0x02000176 RID: 374
	public interface IParserAccessor
	{
		/// <summary>When implemented by an ASP.NET server control, notifies the server control that an element, either XML or HTML, was parsed.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> that was parsed. </param>
		// Token: 0x06000F42 RID: 3906
		void AddParsedSubObject(object obj);
	}
}
