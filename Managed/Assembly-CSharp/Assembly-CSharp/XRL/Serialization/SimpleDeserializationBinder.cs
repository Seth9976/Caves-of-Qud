using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace XRL.Serialization
{
	// Token: 0x02000573 RID: 1395
	internal sealed class SimpleDeserializationBinder : SerializationBinder
	{
		// Token: 0x06003C54 RID: 15444 RVA: 0x00126F54 File Offset: 0x00125154
		public override Type BindToType(string assemblyName, string typeName)
		{
			Debug.Log("bind@" + assemblyName + "," + typeName);
			Match match = this._assemRegex.Match(assemblyName);
			if (match.Success)
			{
				assemblyName = match.Groups["assembly"].Value;
			}
			match = this._typeRegex.Match(typeName);
			if (match.Success)
			{
				typeName = string.Format("{0},{1}{2}", match.Groups["type"].Value, match.Groups["assembly"].Value, match.Groups["end"].Value);
			}
			Type type = Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
			if (type == null)
			{
				type = ModManager.ResolveType(typeName, false, false, true);
			}
			return type;
		}

		// Token: 0x04001E1B RID: 7707
		private Regex _assemRegex = new Regex("(?<assembly>^.*?),.*");

		// Token: 0x04001E1C RID: 7708
		private Regex _typeRegex = new Regex("(?<type>.*?),(?<assembly>.*?),.*(?<end>]])");
	}
}
