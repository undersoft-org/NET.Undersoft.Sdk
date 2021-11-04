/********************************************************************
   Copyright (c) 2021 Benefit Systems

   @based on: github.com/undersoft-org/NET.Undersoft.Vegas.Sdk.Devel
   @author: Dariusz Hanc
 ********************************************************************/

using System.Reflection;
using System.Threading.Tasks;

namespace System
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IDeputy" />.
    /// </summary>
    public interface IDeputy : IUnique
    {
        #region Properties

        string Name { get; set; }

        string QualifiedName { get; set; }

        /// <summary>
        /// Gets or sets the Info.
        /// </summary>
        MethodInfo Info { get; set; }

        /// <summary>
        /// Gets or sets the Parameters.
        /// </summary>
        ParameterInfo[] Parameters { get; set; }

        /// <summary>
        /// Gets or sets the ParameterValues.
        /// </summary>
        object[] ParameterValues { get; set; }

        DeputyRoutine Routine { get; }

        #endregion

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="object[]"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        object Execute(params object[] parameters);

        /// <summary>
        /// The ExecuteAsync.
        /// </summary>
        /// <param name="parameters">The parameters<see cref="object[]"/>.</param>
        /// <returns>The <see cref="Task{object}"/>.</returns>
        Task<object> ExecuteAsync(params object[] parameters);

        /// <summary>
        /// The ExecuteAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="parameters">The parameters<see cref="object[]"/>.</param>
        /// <returns>The <see cref="Task{T}"/>.</returns>
        Task<T> ExecuteAsync<T>(params object[] parameters);

        #endregion
    }

    #endregion
}
