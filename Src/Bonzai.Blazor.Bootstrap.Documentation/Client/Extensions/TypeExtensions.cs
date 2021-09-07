using System;
using System.Text;

namespace Bonzai.Blazor.Bootstrap.Documentation.Client.Extensions
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            if (nullableType != null)
                return nullableType.Name + "?";

            if (!(type.IsGenericType && type.Name.Contains('`')))
                switch (type.Name)
                {
                    case "String":
                        return "string";

                    case "Int32":
                        return "int";

                    case "Decimal":
                        return "decimal";

                    case "Object":
                        return "object";

                    case "Void":
                        return "void";

                    case "Boolean":
                        return "bool";

                    default:
                        {
                            return string.IsNullOrWhiteSpace(type.FullName) ? type.Name : type.FullName;
                        }
                }

            var stringBuilder = new StringBuilder(type.Name.Substring(
                0,
                type.Name.IndexOf('`')
            ));
            stringBuilder.Append('<');
            var first = true;
            foreach (var genericType in type.GetGenericArguments())
            {
                if (!first)
                    stringBuilder.Append(',');
                stringBuilder.Append(genericType.GetFriendlyName());
                first = false;
            }
            stringBuilder.Append('>');
            return stringBuilder.ToString();
        }
    }
}