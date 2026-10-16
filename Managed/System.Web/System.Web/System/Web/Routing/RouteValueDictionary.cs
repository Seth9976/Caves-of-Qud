using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Represents a case-insensitive collection of key/value pairs that you use in various places in the routing framework, such as when you define the default values for a route or when you generate a URL that is based on a route.</summary>
	// Token: 0x020004F3 RID: 1267
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteValueDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class that is empty. </summary>
		// Token: 0x060038A5 RID: 14501 RVA: 0x00096E4F File Offset: 0x0009504F
		public RouteValueDictionary()
		{
			this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class and adds values that are based on properties from the specified object. </summary>
		/// <param name="values">An object that contains properties that will be added as elements to the new collection.</param>
		// Token: 0x060038A6 RID: 14502 RVA: 0x00096E67 File Offset: 0x00095067
		public RouteValueDictionary(object values)
		{
			this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this.AddValues(values);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class and adds elements from the specified collection. </summary>
		/// <param name="dictionary">A collection whose elements are copied to the new collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060038A7 RID: 14503 RVA: 0x00096E86 File Offset: 0x00095086
		public RouteValueDictionary(IDictionary<string, object> dictionary)
		{
			this._dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Gets the number of key/value pairs that are in the collection.</summary>
		/// <returns>The number of key/value pairs that are in the collection.</returns>
		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x00096E9F File Offset: 0x0009509F
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		/// <summary>Gets a collection that contains the keys in the dictionary.</summary>
		/// <returns>A collection that contains the keys in the dictionary.</returns>
		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x00096EAC File Offset: 0x000950AC
		public Dictionary<string, object>.KeyCollection Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the dictionary.</returns>
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060038AA RID: 14506 RVA: 0x00096EB9 File Offset: 0x000950B9
		public Dictionary<string, object>.ValueCollection Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		/// <summary>Gets or sets the value that is associated with the specified key.</summary>
		/// <returns>The value that is associated with the specified key, or null if the key does not exist in the collection.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		// Token: 0x170011B3 RID: 4531
		public object this[string key]
		{
			get
			{
				object obj;
				this.TryGetValue(key, out obj);
				return obj;
			}
			set
			{
				this._dictionary[key] = value;
			}
		}

		/// <summary>Adds the specified value to the dictionary by using the specified key.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		// Token: 0x060038AD RID: 14509 RVA: 0x00096EEF File Offset: 0x000950EF
		public void Add(string key, object value)
		{
			this._dictionary.Add(key, value);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x00096F00 File Offset: 0x00095100
		private void AddValues(object values)
		{
			if (values != null)
			{
				foreach (object obj in TypeDescriptor.GetProperties(values))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					object value = propertyDescriptor.GetValue(values);
					this.Add(propertyDescriptor.Name, value);
				}
			}
		}

		/// <summary>Removes all keys and values from the dictionary.</summary>
		// Token: 0x060038AF RID: 14511 RVA: 0x00096F6C File Offset: 0x0009516C
		public void Clear()
		{
			this._dictionary.Clear();
		}

		/// <summary>Determines whether the dictionary contains the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the dictionary.</param>
		// Token: 0x060038B0 RID: 14512 RVA: 0x00096F79 File Offset: 0x00095179
		public bool ContainsKey(string key)
		{
			return this._dictionary.ContainsKey(key);
		}

		/// <summary>Determines whether the dictionary contains a specific value.</summary>
		/// <returns>true if the dictionary contains an element that has the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the dictionary.</param>
		// Token: 0x060038B1 RID: 14513 RVA: 0x00096F87 File Offset: 0x00095187
		public bool ContainsValue(object value)
		{
			return this._dictionary.ContainsValue(value);
		}

		/// <summary>Returns an enumerator that you can use to iterate through the dictionary.</summary>
		/// <returns>A structure for reading data in the dictionary.</returns>
		// Token: 0x060038B2 RID: 14514 RVA: 0x00096F95 File Offset: 0x00095195
		public Dictionary<string, object>.Enumerator GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		/// <summary>Removes the value that has the specified key from the dictionary.</summary>
		/// <returns>true if the element is found and removed; otherwise, false. This method returns false if <paramref name="key" /> is not found in the dictionary.</returns>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x060038B3 RID: 14515 RVA: 0x00096FA2 File Offset: 0x000951A2
		public bool Remove(string key)
		{
			return this._dictionary.Remove(key);
		}

		/// <summary>Gets a value that indicates whether a value is associated with the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value that is associated with the specified key, if the key is found; otherwise, contains the appropriate default value for the type of the <paramref name="value" /> parameter that you provided as an out parameter. This parameter is passed uninitialized.</param>
		// Token: 0x060038B4 RID: 14516 RVA: 0x00096FB0 File Offset: 0x000951B0
		public bool TryGetValue(string key, out object value)
		{
			return this._dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x00096EAC File Offset: 0x000950AC
		ICollection<string> IDictionary<string, object>.Keys
		{
			get
			{
				return this._dictionary.Keys;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x00096EB9 File Offset: 0x000950B9
		ICollection<object> IDictionary<string, object>.Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x00096FBF File Offset: 0x000951BF
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			((ICollection<KeyValuePair<string, object>>)this._dictionary).Add(item);
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00096FCD File Offset: 0x000951CD
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this._dictionary).Contains(item);
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x00096FDB File Offset: 0x000951DB
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, object>>)this._dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x00096FEA File Offset: 0x000951EA
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, object>>)this._dictionary).IsReadOnly;
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x00096FF7 File Offset: 0x000951F7
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return ((ICollection<KeyValuePair<string, object>>)this._dictionary).Remove(item);
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x00097005 File Offset: 0x00095205
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>A structure for reading data in the dictionary.</returns>
		// Token: 0x060038BD RID: 14525 RVA: 0x00097005 File Offset: 0x00095205
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001EFD RID: 7933
		private Dictionary<string, object> _dictionary;
	}
}
