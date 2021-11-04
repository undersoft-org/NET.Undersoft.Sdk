/*************************************************
   Copyright (c) 2021 Undersoft

   System.LinqExpressionExtension.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

using System.Series;
using System.Threading.Tasks;

namespace System.Linq
{
    using Collections.Generic;

    /// <summary>
    /// Defines the <see cref="LinqExpressionExtension" />.
    /// </summary>
    public static class SeriesLinqExtensions
    {
        #region Methods

        public static IEnumerable<TResult> ForEach<TElement, TResult>(this IEnumerable<TElement> items, Func<TElement, TResult> action)
        {
            foreach (var item in items)
            {
                yield return action(item);
            }
        }

        public static void ForEach<TElement>(this IEnumerable<TElement> items, Action<TElement> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static Task<IEnumerable<TResult>> ForEachAsync<TElement, TResult>(this IEnumerable<TElement> items, Func<TElement, TResult> action)
        {
            return Task.Run(() => items.ForEach(action));
        }

        public static Task ForEachAsync<TElement>(this IEnumerable<TElement> items, Action<TElement> action)
        {
            return Task.Run(() => items.ForEach(action));
        }

        public static Task<List<TElement>> ToListAsync<TElement>(this IEnumerable<TElement> items)
        {
            return Task.Run(() => items.ToList());
        }

        public static Task<TElement[]> ToArrayAsync<TElement>(this IEnumerable<TElement> items)
        {
            return Task.Run(() => items.ToArray());
        }

        public static Task<Catalog<TElement>> ToCatalogAsync<TElement>(this IEnumerable<TElement> items)
        {
            return Task.Run(() => items.ToCatalog());
        }

        public static Task<Deck<TElement>> ToDeckAsync<TElement>(this IEnumerable<TElement> items)
        {
            return Task.Run(() => items.ToDeck());
        }

        public static Task<Album<TElement>> ToAlbumAsync<TElement>(this IEnumerable<TElement> items)
        {
            return Task.Run(() => items.ToAlbum());
        }

        public static Catalog<TElement> ToCatalog<TElement>(this IEnumerable<TElement> items)
        {
            return new (items);
        }

        public static Deck<TElement> ToDeck<TElement>(this IEnumerable<TElement> items)
        {
            return new(items);
        }
        public static Album<TElement> ToAlbum<TElement>(this IEnumerable<TElement> items)
        {
            return new(items);
        }

        #endregion
    }
}
