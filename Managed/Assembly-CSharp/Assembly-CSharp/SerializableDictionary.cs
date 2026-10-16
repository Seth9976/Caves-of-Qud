using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

// Token: 0x0200016A RID: 362
[XmlRoot("dictionary")]
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
	// Token: 0x06000E45 RID: 3653 RVA: 0x0005BFE8 File Offset: 0x0005A1E8
	public XmlSchema GetSchema()
	{
		return null;
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0005BFEC File Offset: 0x0005A1EC
	public void ReadXml(XmlReader reader)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(TKey));
		XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(TValue));
		bool isEmptyElement = reader.IsEmptyElement;
		reader.Read();
		if (isEmptyElement)
		{
			return;
		}
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			reader.ReadStartElement("item");
			reader.ReadStartElement("key");
			TKey tkey = (TKey)((object)xmlSerializer.Deserialize(reader));
			reader.ReadEndElement();
			reader.ReadStartElement("value");
			TValue tvalue = (TValue)((object)xmlSerializer2.Deserialize(reader));
			reader.ReadEndElement();
			base.Add(tkey, tvalue);
			reader.ReadEndElement();
			reader.MoveToContent();
		}
		reader.ReadEndElement();
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0005C098 File Offset: 0x0005A298
	public void WriteXml(XmlWriter writer)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(TKey));
		XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(TValue));
		foreach (TKey tkey in base.Keys)
		{
			writer.WriteStartElement("item");
			writer.WriteStartElement("key");
			xmlSerializer.Serialize(writer, tkey);
			writer.WriteEndElement();
			writer.WriteStartElement("value");
			TValue tvalue = base[tkey];
			xmlSerializer2.Serialize(writer, tvalue);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}
	}
}
