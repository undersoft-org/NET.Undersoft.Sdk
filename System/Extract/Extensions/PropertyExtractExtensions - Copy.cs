/*************************************************
   Copyright (c) 2021 Undersoft

   System.Extractor.TypeExtractExtensions.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

using System.Reflection;

namespace System.Extract
{
    /// <summary>
    /// Defines the <see cref="TypeExtractExtenstion" />.
    /// </summary>
    public static class PropertyInfoExtractExtenstion
    {
        #region Methods

        public static string GetBackingFieldName(string propertyName)
        {
            return $"<{propertyName}>k__BackingField";
        }

        public static bool HaveBackingField(string propertyName)
        {
            return (GetBackingFieldName(propertyName) != null);
        }

        public static bool HaveBackingField(this PropertyInfo property)
        {
            return (GetBackingFieldName(property.Name) != null);
        }

        private static FieldInfo GetBackingField(Type type, string propertyName)
        {
            return type.GetField(GetBackingFieldName(propertyName), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static FieldInfo GetBackingField(object obj, string propertyName)
        {
            return obj.GetType().GetField(GetBackingFieldName(propertyName), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        #endregion
    }
}
