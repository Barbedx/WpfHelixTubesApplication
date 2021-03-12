using System;
using System.Collections.Generic;
using System.Reflection;

namespace WpfAppDatagridGroupingHeader.Extensions
{
public static    class FieldInfoExtensions
    {

        public static IEnumerable<Type> GetTypeAndBaseTypes(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return GetTypeAndBaseTypesImpl(type);
        }
        private static IEnumerable<Type> GetTypeAndBaseTypesImpl(Type type)
        {
            yield return type;

            var current = type;
            while (current.BaseType != null)
            {
                yield return current.BaseType;
                current = current.BaseType;
            }
        }

        public static IEnumerable<FieldInfo> GetPublicStaticFields(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return GetPublicStaticFieldsImpl(type);
        }
        private static IEnumerable<FieldInfo> GetPublicStaticFieldsImpl(Type type)
        {
            foreach (var t in GetTypeAndBaseTypes(type))
            {
                foreach (var fi in t.GetFields(BindingFlags.Public | BindingFlags.Static))
                    yield return fi;
            }
        }
    }
}
