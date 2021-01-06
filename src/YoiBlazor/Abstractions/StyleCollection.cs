using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YoiBlazor
{
    /// <summary>
    /// Represents a group of style.
    /// </summary>
    public class StyleCollection : IReadOnlyDictionary<string,string>,IEnumerable<string>,IEnumerable<KeyValuePair<string,string>>
    {
        private readonly Dictionary<string,string> stylesCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleCollection"/> class.
        /// </summary>
        public StyleCollection()
        {
            stylesCollection = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleCollection"/> class.
        /// </summary>
        /// <param name="styles">The styles.</param>
        public StyleCollection((string name, string value)[] styles):this(styles.Select(m => new KeyValuePair<string, string>(m.name, m.value)))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleCollection"/> class.
        /// </summary>
        /// <param name="styles">The styles.</param>
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
        /// Gets the styles.
        /// </summary>
        /// <value>
        /// The styles.
        /// </value>
        public IEnumerable<KeyValuePair<string, string>> Styles => stylesCollection;

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        public int Count => stylesCollection.Count;

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
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
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="StyleCollection"/>.
        /// </summary>
        /// <param name="styles">The styles string, the format like 'key:value; key:value...'</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="InvalidOperationException"></exception>
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
        /// Performs an implicit conversion from <see cref="System.ValueTuple{System.String, System.String}[]"/> to <see cref="StyleCollection"/>.
        /// </summary>
        /// <param name="styles">The styles.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentNullException">styles</exception>
        public static implicit operator StyleCollection((string name, string value)[] styles)
        {
            if (styles is null)
            {
                throw new ArgumentNullException(nameof(styles));
            }

            return new StyleCollection(styles);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="KeyValuePair{System.String, System.String}[]"/> to <see cref="StyleCollection"/>.
        /// </summary>
        /// <param name="styles">The styles.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentNullException">styles</exception>
        public static implicit operator StyleCollection(KeyValuePair<string,string>[] styles)
        {
            if (styles is null)
            {
                throw new ArgumentNullException(nameof(styles));
            }

            return new StyleCollection(styles);
        }

        /// <summary>
        /// Converts to string seperated by semicolmn for each item.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
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
