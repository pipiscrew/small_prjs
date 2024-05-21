using System;
using System.Collections.Generic;
using System.ComponentModel;

//////////////////////////////////
/* ENABLE SORT COLUMNS ON GRID */
////////////////////////////////
public class SortableBindingList<T> : BindingList<T>
{
    private bool isSorted;
    private ListSortDirection sortDirection;
    private PropertyDescriptor sortProperty;

    public SortableBindingList() : base() { }

    public SortableBindingList(IList<T> list) : base(list) { }

    protected override bool SupportsSortingCore
    {
        get { return true; }
    }

    protected override bool IsSortedCore
    {
        get { return isSorted; }
    }

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        var items = this.Items as List<T>;

        if (items != null)
        {
            var comparer = new PropertyComparer<T>(prop, direction);
            items.Sort(comparer);

            sortProperty = prop;
            sortDirection = direction;
            isSorted = true;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        else
        {
            isSorted = false;
        }
    }

    protected override void RemoveSortCore()
    {
        isSorted = false;
    }

    protected override PropertyDescriptor SortPropertyCore
    {
        get { return sortProperty; }
    }

    protected override ListSortDirection SortDirectionCore
    {
        get { return sortDirection; }
    }
}

public class PropertyComparer<T> : IComparer<T>
{
    private PropertyDescriptor property;
    private ListSortDirection direction;

    public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
    {
        this.property = property;
        this.direction = direction;
    }

    public int Compare(T x, T y)
    {
        var value1 = property.GetValue(x);
        var value2 = property.GetValue(y);

        int result;

        if (value1 == null)
        {
            result = value2 == null ? 0 : -1;
        }
        else
        {
            result = value1.Equals(value2) ? 0 : ((IComparable)value1).CompareTo(value2);
        }

        if (direction == ListSortDirection.Descending)
        {
            result = -result;
        }

        return result;
    }
}
