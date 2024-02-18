using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Match3.Views;


public static class ItemsContainerExtensions
{
    public static TObject? GetObjectAtPoint<TItemContainer, TObject>(this ItemsControl control, Point p)
        where TItemContainer : DependencyObject
        where TObject : class
    {
        var obj = GetContainerAtPoint<TItemContainer>(control, p);
        
        if (obj == null)
            return null;

        return control.ItemContainerGenerator.ItemFromContainer(obj) as TObject;
    }


    public static ItemContainer? GetContainerAtPoint<ItemContainer>(this ItemsControl control, Point p)
        where ItemContainer : DependencyObject
    {
        var result = VisualTreeHelper.HitTest(control, p);
        var obj = result.VisualHit;

        while (VisualTreeHelper.GetParent(obj) != null && obj is not ItemContainer)
        {
            obj = VisualTreeHelper.GetParent(obj);
        }

        // Will return null if not found
        return obj as ItemContainer;
    }
}
