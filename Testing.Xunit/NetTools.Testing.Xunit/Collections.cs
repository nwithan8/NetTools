using NetTools.Testing.Xunit.Exceptions;

namespace NetTools.Testing.Xunit
{
    public abstract partial class Assert
    {
        /// <summary>
        ///     Verifies that any items in the collection pass when executed against
        ///     action.
        /// </summary>
        /// <typeparam name="T">The type of the object to be verified</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <exception cref="AnyException">Thrown when the collection contains no matching element</exception>
        public static void Any<T>(IEnumerable<T> collection, Action<T> action)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            Any(collection, (item, index) => action(item));
        }

        /// <summary>
        ///     Verifies that any items in the collection pass when executed against
        ///     action. The item index is provided to the action, in addition to the item.
        /// </summary>
        /// <typeparam name="T">The type of the object to be verified</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <exception cref="AnyException">Thrown when the collection contains no matching element</exception>
        public static void Any<T>(IEnumerable<T> collection, Action<T, int> action)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            var idx = 0;

            var passed = false;

            foreach (var item in collection)
            {
                try
                {
                    action(item, idx);
                    passed = true;
                    break; // if we get here, we passed, so we can stop iterating
                }
                catch (Exception ex)
                {
                    // we don't care about the exception, we just want to keep iterating
                }

                ++idx;
            }

            if (!passed)
                throw new AnyException();
        }
        
        /// <summary>
        ///     Verifies that only one item in the collection passes when executed against
        ///     action.
        /// </summary>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <param name="countOnFail">When the collection fails, whether to continue iterating to calculate how many elements match</param>
        /// <exception cref="OnlyXException">Thrown when the collection does not contain exactly one matching element</exception>
        public static void OnlyOne<T>(IEnumerable<T> collection, Action<T> action, bool countOnFail = false)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            OnlyOne(collection, (item, index) => action(item));
        }
        
        /// <summary>
        ///     Verifies that only one item in the collection passes when executed against
        ///     action. The item index is provided to the action, in addition to the item.
        /// </summary>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <param name="countOnFail">When the collection fails, whether to continue iterating to calculate how many elements match</param>
        /// <exception cref="OnlyXException">Thrown when the collection does not contain exactly one matching element</exception>
        public static void OnlyOne<T>(IEnumerable<T> collection, Action<T, int> action, bool countOnFail = false)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            OnlyX(collection, action, 1, countOnFail);
        }
        
        /// <summary>
        ///     Verifies that only X items in the collection pass when executed against
        ///     action.
        /// </summary>
        /// <typeparam name="T">The type of the object to be verified</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <param name="count">How many elements in the collection should match</param>
        /// <param name="countOnFail">When the collection fails, whether to continue iterating to calculate how many elements match</param>
        /// <exception cref="OnlyXException">Thrown when the collection does not contain exactly X matching element</exception>
        public static void OnlyX<T>(IEnumerable<T> collection, Action<T> action, int count, bool countOnFail = false)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            OnlyX(collection, (item, index) => action(item), count);
        }
        
        /// <summary>
        ///     Verifies that only X items in the collection pass when executed against
        ///     action. The item index is provided to the action, in addition to the item.
        /// </summary>
        /// <typeparam name="T">The type of the object to be verified</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="action">The action to test each item against</param>
        /// <param name="count">How many elements in the collection should match</param>
        /// <param name="countOnFail">When the collection fails, whether to continue iterating to calculate how many elements match</param>
        /// <exception cref="OnlyXException">Thrown when the collection does not contain exactly X matching element</exception>
        public static void OnlyX<T>(IEnumerable<T> collection, Action<T, int> action, int count, bool countOnFail = false)
        {
            GuardArgumentNotNull(nameof(collection), collection);
            GuardArgumentNotNull(nameof(action), action);

            var idx = 0;
            
            var passCount = 0;
            
            var word = count == 1 ? "One" : "X";

            foreach (var item in collection)
            {
                try
                {
                    action(item, idx);
                    if (passCount > count  && !countOnFail) // if we're not counting on fail, we can stop iterating and fail immediately
                        throw new OnlyXException(word);
                    passCount++;
                }
                catch (Exception ex)
                {
                    // we don't care about the exception, we just want to keep iterating
                }

                ++idx;
            }

            // if we're counting on fail, we need to check the count after iterating
            if (passCount != count)
                throw new OnlyXException(word, countOnFail ? passCount : (int?)null);
        }
    }
}
