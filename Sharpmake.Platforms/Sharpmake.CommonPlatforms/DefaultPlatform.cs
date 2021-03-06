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
using System;
using System.Collections.Generic;
using Sharpmake.Generators;
using Sharpmake.Generators.FastBuild;

namespace Sharpmake
{
    [DefaultPlatformImplementation(typeof(IPlatformBff))]
    [DefaultPlatformImplementation(typeof(Project.Configuration.IConfigurationTasks))]
    public sealed class DefaultPlatform : BasePlatform, Project.Configuration.IConfigurationTasks
    {
        #region IPlatformDescriptor
        // Not used because this is not the default platform for IPlatformDescriptor. (Not listed
        // in attributes.)
        public override string SimplePlatformString => string.Empty;
        public override bool IsMicrosoftPlatform => false;
        public override bool HasSharedLibrarySupport => false;
        public override bool HasDotNetSupport => false;
        public override bool IsPcPlatform => false;
        public override bool IsUsingClang => false;

        #endregion

        #region Project.Configuration.IConfigurationTasks implementation
        public static void SetupLibraryPaths(Project.Configuration configuration, DependencySetting dependencySetting, Project.Configuration dependency)
        {
            if (dependencySetting.HasFlag(DependencySetting.LibraryPaths))
                configuration.DependenciesLibraryPaths.Add(dependency.TargetLibraryPath, dependency.TargetLibraryPathOrderNumber);

            if (dependencySetting.HasFlag(DependencySetting.LibraryFiles))
                configuration.DependenciesLibraryFiles.Add(dependency.TargetFileFullName, dependency.TargetFileOrderNumber);
        }

        void Project.Configuration.IConfigurationTasks.SetupLibraryPaths(Project.Configuration configuration, DependencySetting dependencySetting, Project.Configuration dependency)
        {
            SetupLibraryPaths(configuration, dependencySetting, dependency);
        }

        public string GetDefaultOutputExtension(Project.Configuration.OutputType outputType)
        {

            switch (outputType)
            {
                case Project.Configuration.OutputType.Exe:
                case Project.Configuration.OutputType.DotNetConsoleApp:
                case Project.Configuration.OutputType.DotNetWindowsApp:
                    return "exe";
                case Project.Configuration.OutputType.Lib:
                    return "lib";
                case Project.Configuration.OutputType.Dll:
                case Project.Configuration.OutputType.DotNetClassLibrary:
                    return "dll";
                case Project.Configuration.OutputType.None:
                    return string.Empty;
                default:
                    return outputType.ToString().ToLower();
            }
        }
        #endregion

        #region IPlatformBff implementation
        public override string BffPlatformDefine => null;
        public override string CConfigName => string.Empty;
        public override string CppConfigName => CConfigName;

        public override bool AddLibPrefix(Configuration conf)
        {
            return false;
        }

        public override void SetupExtraLinkerSettings(IFileGenerator fileGenerator, Project.Configuration.OutputType outputType, string fastBuildOutputFile)
        {
            using (fileGenerator.Resolver.NewScopedParameter("dllOption", outputType == Project.Configuration.OutputType.Dll ? " /DLL" : ""))
            {
                fileGenerator.Write(Bff.Template.ConfigurationFile.LinkerOptions);
            }
        }

        public override void AddCompilerSettings(IDictionary<string, CompilerSettings> masterCompilerSettings, string compilerName, string rootPath, DevEnv devEnv, string projectRootPath)
        {
        }

        public override CompilerSettings GetMasterCompilerSettings(IDictionary<string, CompilerSettings> masterCompilerSettings, string compilerName, string rootPath, DevEnv devEnv, string projectRootPath, bool useCCompiler)
        {
            throw new NotImplementedException();
        }

        public override void SetConfiguration(IDictionary<string, CompilerSettings.Configuration> configurations, string compilerName, string projectRootPath, DevEnv devEnv, bool useCCompiler)
        {
        }
        #endregion

        #region IPlatformVcxproj implementation
        // Those don't matter: this is only the default for IPlatformBff, not for IPlatformVcxproj.
        // Those just complete the class so it's not abstract.
        public override string ExecutableFileExtension => string.Empty;
        public override string SharedLibraryFileExtension => string.Empty;
        public override string ProgramDatabaseFileExtension => string.Empty;
        #endregion
    }
}
