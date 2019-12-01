using System;
using System.Collections.Generic;
using System.Linq;

namespace Naobiz.Models
{
    class SelectedValue<T>
    {
        public SelectedValue(T value) => Value = value;

        public T Value { get; }

        public bool Selected { get; set; }

        public static IEnumerable<SelectedValue<T>> Create(IEnumerable<T> items, IEnumerable<T> selectedItems) => items
            .Select(item => new SelectedValue<T>(item) { Selected = selectedItems.Contains(item) });
    }
}
