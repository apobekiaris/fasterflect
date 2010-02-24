#region License
// Copyright 2010 Buu Nguyen, Morten Mertner
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fasterflect.codeplex.com/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fasterflect.Caching;
using Fasterflect.Emitter;
using Fasterflect.ObjectConstruction;

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for inspecting assemblies.
    /// </summary>
    public static class AssemblyExtensions
    {
        #region Types
		/// <summary>
		/// Find all types in the given <paramref name="assembly"/>.
		/// </summary>
		/// <param name="assembly">The assembly in which to look for types.</param>
		/// <returns>A list of all matching types. This method never returns null.</returns>
        public static IList<Type> Types( this Assembly assembly )
        {
            return assembly.Types( Flags.None, null );
        }

		/// <summary>
		/// Find all types in the given <paramref name="assembly"/> matching the specified
		/// <paramref name="bindingFlags"/> and <paramref name="names"/>.
		/// </summary>
		/// <param name="assembly">The assembly in which to look for types.</param>
		/// <param name="bindingFlags">The <see cref="BindingFlags"/> used to filter the result. Only
		/// the Flags.PartialNameMatch option will filter the result set. </param>
		/// <param name="names">An optional list of names against which to filter the result.</param>
		/// <returns>A list of all matching types. This method never returns null.</returns>
		public static IList<Type> Types( this Assembly assembly, Flags bindingFlags, params string[] names )
        {
			Type[] types = assembly.GetTypes();

			bool hasNames = names != null && names.Length > 0;
			bool partialNameMatch = bindingFlags.IsSet( Flags.PartialNameMatch );

			return hasNames ? types.Where( t => names.Any( n => partialNameMatch ? t.Name.Contains( n ) : t.Name == n ) ).ToArray() : types;
        }
        #endregion
    }
}