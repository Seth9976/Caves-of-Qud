using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI
{
	/// <summary>Provides support for rapid application development (RAD) designers to parse data-binding expressions that use XPath expressions. This class cannot be inherited.</summary>
	// Token: 0x0200024C RID: 588
	public sealed class XPathBinder
	{
		// Token: 0x060017ED RID: 6125 RVA: 0x00002050 File Offset: 0x00000250
		private XPathBinder()
		{
		}

		/// <summary>Evaluates XPath data-binding expressions at run time.</summary>
		/// <returns>An <see cref="T:System.Object" /> that results from the evaluation of the data-binding expression.</returns>
		/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language. </param>
		/// <param name="xPath">The XPath query from <paramref name="container" /> to the property value that is placed in the bound control property. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="container" /> or <paramref name="xpath" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="container" /> is not an <see cref="T:System.Xml.XPath.IXPathNavigable" /> object.</exception>
		// Token: 0x060017EE RID: 6126 RVA: 0x0003E472 File Offset: 0x0003C672
		public static object Eval(object container, string xPath)
		{
			return XPathBinder.Eval(container, xPath, null);
		}

		/// <summary>Evaluates XPath data-binding expressions at run time and formats the result as text to be displayed in the requesting browser, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression.</summary>
		/// <returns>A <see cref="T:System.Object" /> that results from the evaluation of the data-binding expression.</returns>
		/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language.</param>
		/// <param name="xPath">The XPath query from the <paramref name="container" /> to the property value to be placed in the bound control property.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
		// Token: 0x060017EF RID: 6127 RVA: 0x0003E47C File Offset: 0x0003C67C
		public static object Eval(object container, string xPath, IXmlNamespaceResolver resolver)
		{
			if (xPath == null || xPath.Length == 0)
			{
				throw new ArgumentNullException("xPath");
			}
			IXPathNavigable ixpathNavigable = container as IXPathNavigable;
			if (ixpathNavigable == null)
			{
				throw new ArgumentException("container");
			}
			object obj = ixpathNavigable.CreateNavigator().Evaluate(xPath, resolver);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator == null)
			{
				return obj;
			}
			if (xpathNodeIterator.MoveNext())
			{
				return xpathNodeIterator.Current.Value;
			}
			return null;
		}

		/// <summary>Evaluates XPath data-binding expressions at run time and formats the result as text to be displayed in the requesting browser.</summary>
		/// <returns>A <see cref="T:System.String" /> that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language. </param>
		/// <param name="xPath">The XPath query from the <paramref name="container" /> to the property value to be placed in the bound control property. </param>
		/// <param name="format">A .NET Framework format string, similar to those used by <see cref="M:System.String.Format(System.String,System.Object)" />, that converts the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object (which results from the evaluation of the data-binding expression) to a <see cref="T:System.String" /> that can be displayed by the requesting browser. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="container" /> or <paramref name="xpath" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="container" /> is not an <see cref="T:System.Xml.XPath.IXPathNavigable" />.</exception>
		// Token: 0x060017F0 RID: 6128 RVA: 0x0003E4E1 File Offset: 0x0003C6E1
		public static string Eval(object container, string xPath, string format)
		{
			return XPathBinder.Eval(container, xPath, format, null);
		}

		/// <summary>Evaluates XPath data-binding expressions at run time and formats the result as text to be displayed in the requesting browser, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression..</summary>
		/// <returns>A <see cref="T:System.String" /> that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language.</param>
		/// <param name="xPath">The XPath query from the <paramref name="container" /> to the property value to be placed in the bound control property.</param>
		/// <param name="format">A .NET Framework format string, similar to those used by <see cref="M:System.String.Format(System.String,System.Object)" />, that converts the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object (which results from the evaluation of the data-binding expression) to a <see cref="T:System.String" /> that can be displayed by the requesting browser.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
		// Token: 0x060017F1 RID: 6129 RVA: 0x0003E4EC File Offset: 0x0003C6EC
		public static string Eval(object container, string xPath, string format, IXmlNamespaceResolver resolver)
		{
			object obj = XPathBinder.Eval(container, xPath, resolver);
			if (obj == null)
			{
				return string.Empty;
			}
			if (format == null || format.Length == 0)
			{
				return obj.ToString();
			}
			return string.Format(format, obj);
		}

		/// <summary>Uses an XPath data-binding expression at run time to return a list of nodes.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes.</returns>
		/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language. </param>
		/// <param name="xPath">The XPath query that retrieves a list of nodes. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="container" /> or <paramref name="xpath" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="container" /> is not an <see cref="T:System.Xml.XPath.IXPathNavigable" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current node of the <see cref="T:System.Xml.XPath.XPathNodeIterator" /> does not have an associated XML node.</exception>
		// Token: 0x060017F2 RID: 6130 RVA: 0x0003E524 File Offset: 0x0003C724
		public static IEnumerable Select(object container, string xPath)
		{
			return XPathBinder.Select(container, xPath, null);
		}

		/// <summary>Uses an XPath data-binding expression at run time to return a list of nodes, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes.</returns>
		/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language.</param>
		/// <param name="xPath">The XPath query that retrieves a list of nodes.</param>
		/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
		// Token: 0x060017F3 RID: 6131 RVA: 0x0003E530 File Offset: 0x0003C730
		public static IEnumerable Select(object container, string xPath, IXmlNamespaceResolver resolver)
		{
			if (xPath == null || xPath.Length == 0)
			{
				throw new ArgumentNullException("xPath");
			}
			IXPathNavigable ixpathNavigable = container as IXPathNavigable;
			if (ixpathNavigable == null)
			{
				throw new ArgumentException("container");
			}
			XPathNodeIterator xpathNodeIterator = ixpathNavigable.CreateNavigator().Select(xPath, resolver);
			ArrayList arrayList = new ArrayList();
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				IHasXmlNode hasXmlNode = xpathNavigator as IHasXmlNode;
				if (hasXmlNode == null)
				{
					throw new InvalidOperationException();
				}
				arrayList.Add(hasXmlNode.GetNode());
			}
			return arrayList;
		}
	}
}
