using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WorldBrowser
{
    public class ABSwitcher : ContentControl
    {
        public enum Elements
        {
            ElementA,
            ElementB
        }

        public static DependencyProperty ElementAProperty;
        public static DependencyProperty ElementBProperty;
        public static DependencyProperty CurrentElementProperty;

        static ABSwitcher()
        {
            ElementAProperty = DependencyProperty.Register("ElementA", typeof(object), typeof(ABSwitcher));
            ElementBProperty = DependencyProperty.Register("ElementB", typeof(object), typeof(ABSwitcher));
            CurrentElementProperty = DependencyProperty.Register("CurrentElement", typeof(Elements), typeof(ABSwitcher));
        }

        public ABSwitcher()
        {
        }

        public object ElementA
        {
            get { return GetValue(ElementAProperty); }
            set { SetValue(ElementAProperty, value); }
        }

        public object ElementB
        {
            get { return GetValue(ElementBProperty); }
            set { SetValue(ElementBProperty, value); }
        }

        public Elements CurrentElement
        {
            get { return (Elements)GetValue(CurrentElementProperty); }
            set { SetValue(CurrentElementProperty, value); }
        }

        public object SelectedElement
        {
            get { return (CurrentElement == Elements.ElementA) ? ElementA : ElementB; }
        }

        public object UnselectedElement
        {
            get { return (CurrentElement == Elements.ElementA) ? ElementB : ElementA; }
        }

        public void Switch()
        {
            if (CurrentElement == Elements.ElementA)
                CurrentElement = Elements.ElementB;
            else
                CurrentElement = Elements.ElementA;
        }
    }
}
