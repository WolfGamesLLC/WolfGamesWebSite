using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using WGSystem.Collections.Generic;
using WolfGamesWebSite.Test.Framework.Identifiers;

namespace WolfGamesWebSite.Test.Framework.Mocks
{
    /// <summary>
    /// A mock service collection
    /// </summary>
    public class MockServiceCollection : IServiceCollection
    {
        private static IList<ServiceDescriptor> _services { get; set; }

        /// <summary>
        /// Returns the number of service descriptors in the collection
        /// </summary>
        public int Count => _services.Count;

        /// <summary>
        /// Returns the read only state of the collection
        /// </summary>
        public bool IsReadOnly => _services.IsReadOnly;

        public ServiceDescriptor this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Create a MockServiceCollection using the provided list of
        /// strings to hold the names of the services that will be added
        /// </summary>
        /// <param name="services">A list of strings, normally this would be empty</param>
        public MockServiceCollection(IList<ServiceDescriptor> services)
        {
            _services = services;
        }

        /// <summary>
        /// Overload the AddMvc operation
        /// </summary>
        /// <returns></returns>
        public static IMvcBuilder AddMvc()
        {
            _services.Add(new ServiceDescriptor(typeof(MockMvcBuilder), new MockMvcBuilder()));
            return null;
        }

        public int IndexOf(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add an item to the service collection
        /// </summary>
        /// <param name="item">The service descriptor item to be added</param>
        public void Add(ServiceDescriptor item)
        {
            _services.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the lists enumerator
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
