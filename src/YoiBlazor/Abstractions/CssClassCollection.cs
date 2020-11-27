using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// 表示具有一组的 <see cref="CssClass"/> 类型的值的集合。
    /// </summary>
    public class CssClassCollection :IReadOnlyList<CssClass>,IReadOnlyCollection<CssClass>, IEnumerable<string>
    {
        private readonly List<CssClass> cssClassList = new List<CssClass>();

        /// <summary>
        /// 初始化 <see cref="CssClassCollection"/> 类的新实例。
        /// </summary>
        /// <param name="cssClass">一组 <see cref="CssClass"/> 类型。</param>
        public CssClassCollection(IEnumerable<object> cssClass)
        {
            foreach (var item in cssClass)
            {
                cssClassList.Add(new CssClass(item));
            }
        }

        /// <summary>
        /// 获取指定索引的 <see cref="CssClass"/> 类型。
        /// </summary>
        /// <param name="index">索引。</param>
        public CssClass this[int index]
        {
            get
            {
                return cssClassList[index];
            }
        }

        /// <summary>
        /// 获取集合的总数。
        /// </summary>
        public int Count => cssClassList.Count;


        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 转换成以空格分隔的字符串。
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var result= string.Join(" ", cssClassList.Select(m => m.ToString()));
            cssClassList.Clear();
            return result;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
             return cssClassList.Select(m => m.ToString()).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<CssClass> GetEnumerator()
        {
            return cssClassList.GetEnumerator();
        }

        /// <summary>
        /// 获取 <see cref="CssClass"/> 集合。
        /// </summary>
        public IEnumerable<CssClass> CssClasses => cssClassList;

        /// <summary>
        /// 添加指定的 <see cref="CssClass"/> 项。
        /// </summary>
        /// <param name="item">要添加的项。</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> 是 <c>null</c>。</exception>
        public void Add(CssClass item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            cssClassList.Add(item);
        }

        /// <summary>
        /// 添加满足指定条件的 <see cref="CssClass"/> 项。
        /// </summary>
        /// <param name="condition"><c>true</c> 表示条件满足。</param>
        /// <param name="item">要添加的项。</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> 是 <c>null</c>。</exception>
        public void Add(bool condition, CssClass item)
        {
            if (condition)
            {
                Add(item);
            }
        }

        /// <summary>
        /// 执行从 <see cref="System.String"/> 到 <see cref="CssClassCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="cssClass">css 类名称数组。</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="cssClass"/> 是 null。</exception>
        public static implicit operator CssClassCollection(object[] cssClass)
        {
            if (cssClass is null)
            {
                throw new ArgumentNullException(nameof(cssClass));
            }

            return new CssClassCollection(cssClass);
        }

        /// <summary>
        /// 执行从 <see cref="System.String"/> 到 <see cref="CssClassCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="cssClass">具有空格分隔的类名称。</param>
        public static implicit operator CssClassCollection(string cssClass)
        {
            return new CssClassCollection(cssClass.Split(' '));
        }

        /// <summary>
        /// 执行从 <see cref="CssClass"/> 到 <see cref="CssClassCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="cssClass"><see cref="CssClass"/> 类型数组。</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="cssClass"/> 是 null。</exception>
        public static implicit operator CssClassCollection(CssClass[] cssClass)
        {
            if (cssClass is null)
            {
                throw new ArgumentNullException(nameof(cssClass));
            }

            return new CssClassCollection(cssClass);
        }

        /// <summary>
        /// 执行从 <see cref="CssClass"/> 到 <see cref="CssClassCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="cssClass"><see cref="CssClass"/> 类型。</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="cssClass"/> 是 null。</exception>
        public static implicit operator CssClassCollection(CssClass cssClass)
        {
            if (cssClass is null)
            {
                throw new ArgumentNullException(nameof(cssClass));
            }

            return new CssClassCollection(new[] { cssClass });
        }
    }
}
