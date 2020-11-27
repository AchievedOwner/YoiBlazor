using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// 表示组件元素的内联样式集合。
    /// </summary>
    public class StyleCollection : IReadOnlyDictionary<string,string>,IEnumerable<string>,IEnumerable<KeyValuePair<string,string>>
    {
        private readonly Dictionary<string,string> stylesCollection;

        /// <summary>
        /// 初始化 <see cref="StyleCollection"/> 类的新实例。
        /// </summary>
        public StyleCollection()
        {
            stylesCollection = new Dictionary<string, string>();
        }

        /// <summary>
        /// 初始化 <see cref="StyleCollection"/> 类的新实例。
        /// </summary>
        /// <param name="styles">键值对数组。</param>
        public StyleCollection((string name, string value)[] styles):this(styles.Select(m => new KeyValuePair<string, string>(m.name, m.value)))
        {

        }

        /// <summary>
        /// 初始化 <see cref="StyleCollection"/> 类的新实例。
        /// </summary>
        /// <param name="styles">可迭代的键值对。</param>
        public StyleCollection(IEnumerable<KeyValuePair<string, string>> styles)
        {
            stylesCollection = new Dictionary<string, string>(styles.Count());
            foreach (var item in styles)
            {
                stylesCollection.Add(item.Key,item.Value);
            }
        }

        /// <summary>
        /// Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<string> Keys => stylesCollection.Keys;
        /// <summary>
        /// Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<string> Values => stylesCollection.Values;

        /// <summary>
        /// 获取样式的键值对。
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Styles => stylesCollection;

        /// <summary>
        /// 获取样式表的数量。
        /// </summary>
        public int Count => stylesCollection.Count;

        /// <summary>
        /// 获取或设置指定样式名称的值。
        /// </summary>
        /// <param name="name">样式的名称。</param>
        /// <returns>
        /// 指定样式名称的值或 <c>null</c>。
        /// </returns>
        public string this[string name]
        {
            get
            {
                if (TryGetValue(name, out string value))
                {
                    return value;
                }
                return null;
            }
            set
            {
                if (ContainsKey(name))
                {
                    stylesCollection[name] = value;
                }
                else
                {
                    stylesCollection.Add(name, value);
                }
            }
        }

        /// <summary>
        /// 执行样式字符串 <see cref="System.String"/> 到 <see cref="StyleCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="styles">内联样式字符串。</param>
        /// <returns>
        /// 转换的结果。
        /// </returns>
        /// <exception cref="InvalidOperationException">不符合样式表的语法格式。</exception>
        public static implicit operator StyleCollection(string styles)
        {
            try
            {
                var keyValueGroup = styles.Split(';').Select(m => new KeyValuePair<string, string>(m.Split(':')[0], m.Split(':')[1]));
                return new StyleCollection(keyValueGroup);
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException)
            {
                throw new InvalidOperationException($"{nameof(styles)} 不符合 style 样式表“{{name}}:{{value}};{{name}}:{{value}};...”的格式");
            }
        }

        /// <summary>
        /// 执行键值对数组到 <see cref="StyleCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="styles">键值对数组。</param>
        /// <returns>
        /// 转换的结果。
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="styles"/> 是 null。</exception>
        public static implicit operator StyleCollection((string name, string value)[] styles)
        {
            if (styles is null)
            {
                throw new ArgumentNullException(nameof(styles));
            }

            return new StyleCollection(styles);
        }

        /// <summary>
        /// 执行键值对数组到 <see cref="StyleCollection"/> 的隐式转换。
        /// </summary>
        /// <param name="styles">键值对数组。</param>
        /// <returns>
        /// 转换的结果。
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="styles"/> 是 null。</exception>
        public static implicit operator StyleCollection(KeyValuePair<string,string>[] styles)
        {
            if (styles is null)
            {
                throw new ArgumentNullException(nameof(styles));
            }

            return new StyleCollection(styles);
        }

        /// <summary>
        /// 转换成字符串。
        /// </summary>
        /// <returns>
        /// 一个格式为：{Key}:{Value};{Key}:{Value}... 的字符串。
        /// </returns>
        public override string ToString()
        {
            var result = string.Join(";", stylesCollection.Select(m => $"{m.Key}:{m.Value}"));
            stylesCollection.Clear();
            return result;
        }

        /// <summary>
        /// Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        /// true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public bool ContainsKey(string key)
        {
            return stylesCollection.ContainsKey(key);
        }

        /// <summary>
        /// Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements the <see cref="T:System.Collections.Generic.IReadOnlyDictionary`2"></see> interface contains an element that has the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(string key, out string value)
        {
            return stylesCollection.TryGetValue(key, out value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return stylesCollection.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return stylesCollection.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return stylesCollection.Select(m => $"{m.Key}:{m.Value}").GetEnumerator();
        }
    }
}
