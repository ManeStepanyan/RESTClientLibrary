using System.Collections.Generic;
namespace RestClientLibrary
{
    /// <summary>
    /// Interface to support crud operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IClient<T> where T : class, new()
    {
        /// <summary>
        /// Get method
        /// </summary>
        /// <returns></returns>
        List<T> Get();

        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="obj">Item to be added</param>
        void Post(T obj);

        /// <summary>
        /// Updating item
        /// </summary>
        /// <param name="obj">updated parameters</param>
        void Put(T obj);
        /// <summary>
        /// Deleting item
        /// </summary>
        void Delete();
    }
}
