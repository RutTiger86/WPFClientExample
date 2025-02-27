using System.Collections.ObjectModel;

namespace WPFClientExample.Commons.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) return;

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
