using System;

namespace System.Web.UI
{
	/// <summary>Defines methods that a type implements to serialize and deserialize an object graph. </summary>
	// Token: 0x0200017C RID: 380
	public interface IStateFormatter
	{
		/// <summary>Deserializes an object state graph from its serialized string form.</summary>
		/// <returns>An object that represents the state of an ASP.NET server control.</returns>
		/// <param name="serializedState">A string that the <see cref="T:System.Web.UI.IStateFormatter" /> deserializes into an initialized object.</param>
		// Token: 0x06000F51 RID: 3921
		object Deserialize(string serializedState);

		/// <summary>Serializes ASP.NET Web server control state to string form.</summary>
		/// <returns>A string that represents a Web server control's view state. </returns>
		/// <param name="state">The object that represents the view state of the Web server control to serialize to string form.</param>
		// Token: 0x06000F52 RID: 3922
		string Serialize(object state);
	}
}
