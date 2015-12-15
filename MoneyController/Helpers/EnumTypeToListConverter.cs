﻿namespace MoneyController
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Windows.UI.Xaml.Data;

    public class EnumTypeToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            var enumType = value as Type;
            if (enumType == null || !enumType.GetTypeInfo().IsEnum)
                return null;

            var values = Enum.GetValues((Type)value).Cast<Enum>();
            return values.Select(@enum => @enum.ToString()).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}