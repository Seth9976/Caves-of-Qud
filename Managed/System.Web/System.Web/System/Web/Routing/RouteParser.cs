using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web.Routing
{
	// Token: 0x020004F0 RID: 1264
	internal static class RouteParser
	{
		// Token: 0x06003894 RID: 14484 RVA: 0x00096800 File Offset: 0x00094A00
		private static string GetLiteral(string segmentLiteral)
		{
			string text = segmentLiteral.Replace("{{", "").Replace("}}", "");
			if (text.Contains("{") || text.Contains("}"))
			{
				return null;
			}
			return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x00096868 File Offset: 0x00094A68
		private static int IndexOfFirstOpenParameter(string segment, int startIndex)
		{
			for (;;)
			{
				startIndex = segment.IndexOf('{', startIndex);
				if (startIndex == -1)
				{
					break;
				}
				if (startIndex + 1 == segment.Length || (startIndex + 1 < segment.Length && segment[startIndex + 1] != '{'))
				{
					return startIndex;
				}
				startIndex += 2;
			}
			return -1;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000968A6 File Offset: 0x00094AA6
		internal static bool IsSeparator(string s)
		{
			return string.Equals(s, "/", StringComparison.Ordinal);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000968B4 File Offset: 0x00094AB4
		private static bool IsValidParameterName(string parameterName)
		{
			if (parameterName.Length == 0)
			{
				return false;
			}
			foreach (char c in parameterName)
			{
				if (c == '/' || c == '{' || c == '}')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000968F6 File Offset: 0x00094AF6
		internal static bool IsInvalidRouteUrl(string routeUrl)
		{
			return routeUrl.StartsWith("~", StringComparison.Ordinal) || routeUrl.StartsWith("/", StringComparison.Ordinal) || routeUrl.IndexOf('?') != -1;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x00096924 File Offset: 0x00094B24
		public static ParsedRoute Parse(string routeUrl)
		{
			if (routeUrl == null)
			{
				routeUrl = string.Empty;
			}
			if (RouteParser.IsInvalidRouteUrl(routeUrl))
			{
				throw new ArgumentException(global::SR.GetString("The route URL cannot start with a '/' or '~' character and it cannot contain a '?' character."), "routeUrl");
			}
			IList<string> list = RouteParser.SplitUrlToPathSegmentStrings(routeUrl);
			Exception ex = RouteParser.ValidateUrlParts(list);
			if (ex != null)
			{
				throw ex;
			}
			return new ParsedRoute(RouteParser.SplitUrlToPathSegments(list));
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x00096974 File Offset: 0x00094B74
		private static IList<PathSubsegment> ParseUrlSegment(string segment, out Exception exception)
		{
			int i = 0;
			List<PathSubsegment> list = new List<PathSubsegment>();
			while (i < segment.Length)
			{
				int num = RouteParser.IndexOfFirstOpenParameter(segment, i);
				if (num == -1)
				{
					string literal = RouteParser.GetLiteral(segment.Substring(i));
					if (literal == null)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
						return null;
					}
					if (literal.Length > 0)
					{
						list.Add(new LiteralSubsegment(literal));
						break;
					}
					break;
				}
				else
				{
					int num2 = segment.IndexOf('}', num + 1);
					if (num2 == -1)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
						return null;
					}
					string literal2 = RouteParser.GetLiteral(segment.Substring(i, num - i));
					if (literal2 == null)
					{
						exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
						return null;
					}
					if (literal2.Length > 0)
					{
						list.Add(new LiteralSubsegment(literal2));
					}
					string text = segment.Substring(num + 1, num2 - num - 1);
					list.Add(new ParameterSubsegment(text));
					i = num2 + 1;
				}
			}
			exception = null;
			return list;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x00096AA4 File Offset: 0x00094CA4
		private static IList<PathSegment> SplitUrlToPathSegments(IList<string> urlParts)
		{
			List<PathSegment> list = new List<PathSegment>();
			foreach (string text in urlParts)
			{
				if (RouteParser.IsSeparator(text))
				{
					list.Add(new SeparatorPathSegment());
				}
				else
				{
					Exception ex;
					IList<PathSubsegment> list2 = RouteParser.ParseUrlSegment(text, out ex);
					list.Add(new ContentPathSegment(list2));
				}
			}
			return list;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x00096B18 File Offset: 0x00094D18
		internal static IList<string> SplitUrlToPathSegmentStrings(string url)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(url))
			{
				return list;
			}
			int i = 0;
			while (i < url.Length)
			{
				int num = url.IndexOf('/', i);
				if (num == -1)
				{
					string text = url.Substring(i);
					if (text.Length > 0)
					{
						list.Add(text);
						break;
					}
					break;
				}
				else
				{
					string text2 = url.Substring(i, num - i);
					if (text2.Length > 0)
					{
						list.Add(text2);
					}
					list.Add("/");
					i = num + 1;
				}
			}
			return list;
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x00096B98 File Offset: 0x00094D98
		private static Exception ValidateUrlParts(IList<string> pathSegments)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			bool? flag = null;
			bool flag2 = false;
			foreach (string text in pathSegments)
			{
				if (flag2)
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A catch-all parameter can only appear as the last segment of the route URL."), Array.Empty<object>()), "routeUrl");
				}
				bool flag3;
				if (flag == null)
				{
					flag = new bool?(RouteParser.IsSeparator(text));
					flag3 = flag.Value;
				}
				else
				{
					flag3 = RouteParser.IsSeparator(text);
					if (flag3 && flag.Value)
					{
						return new ArgumentException(global::SR.GetString("The route URL separator character '/' cannot appear consecutively. It must be separated by either a parameter or a literal value."), "routeUrl");
					}
					flag = new bool?(flag3);
				}
				if (!flag3)
				{
					Exception ex;
					IList<PathSubsegment> list = RouteParser.ParseUrlSegment(text, out ex);
					if (ex != null)
					{
						return ex;
					}
					ex = RouteParser.ValidateUrlSegment(list, hashSet, text);
					if (ex != null)
					{
						return ex;
					}
					flag2 = list.Any<PathSubsegment>((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);
				}
			}
			return null;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x00096CD4 File Offset: 0x00094ED4
		private static Exception ValidateUrlSegment(IList<PathSubsegment> pathSubsegments, HashSet<string> usedParameterNames, string pathSegment)
		{
			bool flag = false;
			Type type = null;
			foreach (PathSubsegment pathSubsegment in pathSubsegments)
			{
				if (type != null && type == pathSubsegment.GetType())
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A path segment cannot contain two consecutive parameters. They must be separated by a '/' or by a literal string."), Array.Empty<object>()), "routeUrl");
				}
				type = pathSubsegment.GetType();
				if (!(pathSubsegment is LiteralSubsegment))
				{
					ParameterSubsegment parameterSubsegment = pathSubsegment as ParameterSubsegment;
					if (parameterSubsegment != null)
					{
						string parameterName = parameterSubsegment.ParameterName;
						if (parameterSubsegment.IsCatchAll)
						{
							flag = true;
						}
						if (!RouteParser.IsValidParameterName(parameterName))
						{
							return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route parameter name '{0}' is invalid. Route parameter names must be non-empty and cannot contain these characters: \"{{\", \"}}\", \"/\", \"?\""), parameterName), "routeUrl");
						}
						if (usedParameterNames.Contains(parameterName))
						{
							return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route parameter name '{0}' appears more than one time in the URL."), parameterName), "routeUrl");
						}
						usedParameterNames.Add(parameterName);
					}
				}
			}
			if (flag && pathSubsegments.Count != 1)
			{
				return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A path segment that contains more than one section, such as a literal section or a parameter, cannot contain a catch-all parameter."), Array.Empty<object>()), "routeUrl");
			}
			return null;
		}
	}
}
