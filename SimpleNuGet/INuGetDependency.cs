// Copyright (c) 2017 Ubisoft Entertainment
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Collections.Generic;
using SimpleNuGet;

namespace SimpleNuGet
{
    /// <summary>
    /// Dependency between packages
    /// </summary>
    public interface INuGetDependency
    {
        /// <summary>
        /// The package ID of the dependency.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The range of versions acceptable as a dependency. See Dependency versions for exact syntax.
        /// </summary>
        VersionRange VersionRange { get; }
    }
}