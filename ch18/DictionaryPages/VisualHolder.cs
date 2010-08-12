using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DictionaryPages
{
    class VisualHolder : FrameworkElement
    {
        VisualCollection visuals;

        public VisualHolder()
        {
            visuals = new VisualCollection(this);
        }

        public Visual HeldVisual
        {
            get { return (visuals.Count > 0) ? visuals[0] : null; }
            set
            {
                visuals.Clear();
                visuals.Add(value);
            }
        }

        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
    }
}
