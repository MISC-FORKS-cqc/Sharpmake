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
using System.IO;

namespace Sharpmake.Generators.VisualStudio
{
    public abstract class UserFileBase
    {
        protected const string RemoveLineTag = "REMOVE_LINE_TAG";
        protected const string UserFileExtension = ".user";

        private readonly string _userFilePath;

        /// <summary>
        /// Base class for generating VS user files.
        /// </summary>
        /// <param name="projectFilePath">
        /// Path to the project file. The suffix ".user" will be appended to the file name.
        /// </param>
        protected UserFileBase(string projectFilePath)
        {
            _userFilePath = projectFilePath + UserFileExtension;
        }

        protected abstract void GenerateConfigurationContent(IFileGenerator fileGenerator, Project.Configuration conf);

        protected abstract bool HasContentForConfiguration(Project.Configuration conf, out bool overwrite);

        // Generate the user file. The base class is reponsible for generating the file header and footer.
        // Actual user content is generated by the specialized user file class.
        public void GenerateUserFile(Builder builder, Project project, IEnumerable<Project.Configuration> configurations, IList<string> generatedFiles, IList<string> skipFiles)
        {
            var fileGenerator = new FileGenerator();
            bool needToWriteFile = false;
            bool overwriteFile = true;

            fileGenerator.WriteLine(Template.UserFileHeader);
            foreach (Project.Configuration conf in configurations)
            {
                bool overwriteFileConfig;
                if (HasContentForConfiguration(conf, out overwriteFileConfig))
                {
                    needToWriteFile = true;
                    overwriteFile &= overwriteFileConfig;

                    using (fileGenerator.Declare("platformName", Util.GetPlatformString(conf.Platform, conf.Project)))
                    using (fileGenerator.Declare("conf", conf))
                    using (fileGenerator.Declare("project", project))
                    {
                        fileGenerator.WriteLine(Template.PropertyGroupHeader);
                        GenerateConfigurationContent(fileGenerator, conf);
                        fileGenerator.WriteLine(Template.PropertyGroupFooter);
                    }
                }
            }
            fileGenerator.WriteLine(Template.UserFileFooter);

            if (needToWriteFile)
            {
                // remove all line that contain RemoveLineTag
                fileGenerator.RemoveTaggedLines();
                using (MemoryStream cleanMemoryStream = fileGenerator.ToMemoryStream())
                {
                    FileInfo userFileInfo = new FileInfo(_userFilePath);
                    //Skip overwritting user file if it exists already so he can keep his setup
                    // unless the UserProjSettings specifies to overwrite
                    bool shouldWrite = !userFileInfo.Exists || overwriteFile;
                    if (shouldWrite && builder.Context.WriteGeneratedFile(project.GetType(), userFileInfo, cleanMemoryStream))
                        generatedFiles.Add(userFileInfo.FullName);
                    else
                        skipFiles.Add(userFileInfo.FullName);
                }
            }
        }

        public static class Template
        {
            public static readonly string UserFileHeader =
                @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""4.0"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">";

            public static readonly string UserFileFooter = @"</Project>";

            public static readonly string PropertyGroupHeader =
                @"  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='[conf.Name]|[platformName]'"">";

            public static readonly string PropertyGroupFooter = @"  </PropertyGroup>";
        }
    }
}
