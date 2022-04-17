﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AirCombatAuswertung.Services
{
    public static class UIElementExtensions
    {
        public static T DeepClone<T>(this T source) where T : UIElement
        {
            if (source == null)
            {
                return null;
            }

            T result;

            // Get the type
            Type type = source.GetType();

            // Create an instance
            // May require updates to Default.rd.xml in project properties
            // to run successfully in Release mode.
            // https://stackoverflow.com/questions/42185670/activator-createinstancetype-throws-exception
            result = Activator.CreateInstance(type) as T;

            CopyProperties<T>(source, result, type);

            DeepCopyChildren<T>(source, result);

            return result;
        }

        private static void DeepCopyChildren<T>(T source, T result) where T : UIElement
        {
            // Deep copy children.
            Panel sourcePanel = source as Panel;
            if (sourcePanel != null)
            {
                Panel resultPanel = result as Panel;
                if (resultPanel != null)
                {
                    foreach (UIElement child in sourcePanel.Children)
                    {
                        // RECURSION!
                        UIElement childClone = DeepClone(child);
                        resultPanel.Children.Add(childClone);
                    }
                }
            }
        }

        private static void CopyProperties<T>(T source, T result, Type type) where T : UIElement
        {
            // Copy all properties.

            IEnumerable<PropertyInfo> properties = type.GetRuntimeProperties();

            foreach (var property in properties)
            {
                if (property.Name != "Name" && property.Name != "ProtectedCursor") // do not copy names or we cannot add the clone to the same parent as the original.
                    //ProtectedCursor gives a error while copying
                {
                    if ((property.CanWrite) && (property.CanRead))
                    {
                        object sourceProperty = property.GetValue(source);

                        UIElement element = sourceProperty as UIElement;
                        if (element != null)
                        {
                            UIElement propertyClone = element.DeepClone();
                            property.SetValue(result, propertyClone);
                        }
                        else
                        {
                            try
                            {
                                property.SetValue(result, sourceProperty);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex);
                            }
                        }
                    }
                }
            }
        }
    }
}
