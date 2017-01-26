using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using Microsoft.Practices.Unity;

namespace GildedRose.WebServices
{
	/// <summary>
	/// Resolves MVC controller dependencies (controllers with parameterized constructors) by using a Unity Container.
	/// </summary>
	public class UnityDependencyResolver : IDependencyResolver
	{
		private readonly IUnityContainer container;

		/// <summary>
		/// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <exception cref="System.ArgumentNullException">container</exception>
		public UnityDependencyResolver(IUnityContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException(nameof(container));
			}

			this.container = container;
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="UnityDependencyResolver"/> class.
		/// </summary>
		~UnityDependencyResolver()
		{
			this.Dispose();
		}

		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns>The requested Service.</returns>
		public object GetService(Type serviceType)
		{
			try
			{
				return this.container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the services.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns>The list of available services.</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return this.container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		/// <summary>
		/// Starts a resolution scope.
		/// </summary>
		/// <returns>
		/// The dependency scope.
		/// </returns>
		public IDependencyScope BeginScope()
		{
			var childContainer = this.container.CreateChildContainer();
			return new UnityDependencyResolver(childContainer);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.container.Dispose();
			}
		}
	}
}