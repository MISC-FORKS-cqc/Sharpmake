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
namespace Sharpmake
{
    public abstract partial class BasePlatform
    {
        private const string _projectConfigurationsCompileTemplate =
            @"    <ClCompile>
      <PrecompiledHeader>[options.UsePrecompiledHeader]</PrecompiledHeader>
      <WarningLevel>[options.WarningLevel]</WarningLevel>
      <Optimization>[options.Optimization]</Optimization>
      <PreprocessorDefinitions>[options.PreprocessorDefinitions];%(PreprocessorDefinitions);$(PreprocessorDefinitions)</PreprocessorDefinitions>
      <AdditionalIncludeDirectories>[options.AdditionalIncludeDirectories]</AdditionalIncludeDirectories>
      <AdditionalUsingDirectories>[options.AdditionalUsingDirectories]</AdditionalUsingDirectories>
      <DebugInformationFormat>[options.DebugInformationFormat]</DebugInformationFormat>
      <CompileAsManaged>[clrSupport]</CompileAsManaged>
      <SuppressStartupBanner>true</SuppressStartupBanner>
      <TreatWarningAsError>[options.TreatWarningAsError]</TreatWarningAsError>
      <MultiProcessorCompilation>[options.MultiProcessorCompilation]</MultiProcessorCompilation>
      <UseUnicodeForAssemblerListing>false</UseUnicodeForAssemblerListing>
      <InlineFunctionExpansion>[options.InlineFunctionExpansion]</InlineFunctionExpansion>
      <IntrinsicFunctions>[options.EnableIntrinsicFunctions]</IntrinsicFunctions>
      <FavorSizeOrSpeed>[options.FavorSizeOrSpeed]</FavorSizeOrSpeed>
      <OmitFramePointers>[options.OmitFramePointers]</OmitFramePointers>
      <EnableFiberSafeOptimizations>[options.EnableFiberSafeOptimizations]</EnableFiberSafeOptimizations>
      <WholeProgramOptimization>[options.CompilerWholeProgramOptimization]</WholeProgramOptimization>
      <UndefineAllPreprocessorDefinitions>false</UndefineAllPreprocessorDefinitions>
      <IgnoreStandardIncludePath>[options.IgnoreStandardIncludePath]</IgnoreStandardIncludePath>
      <PreprocessToFile>[options.GeneratePreprocessedFile]</PreprocessToFile>
      <PreprocessSuppressLineNumbers>[options.KeepComments]</PreprocessSuppressLineNumbers>
      <PreprocessKeepComments>false</PreprocessKeepComments>
      <StringPooling>[options.StringPooling]</StringPooling>
      <MinimalRebuild>[options.MinimalRebuild]</MinimalRebuild>
      <ExceptionHandling>[options.ExceptionHandling]</ExceptionHandling>
      <SmallerTypeCheck>[options.SmallerTypeCheck]</SmallerTypeCheck>
      <BasicRuntimeChecks>[options.BasicRuntimeChecks]</BasicRuntimeChecks>
      <RuntimeLibrary>[options.RuntimeLibrary]</RuntimeLibrary>
      <StructMemberAlignment>[options.StructMemberAlignment]</StructMemberAlignment>
      <BufferSecurityCheck>[options.BufferSecurityCheck]</BufferSecurityCheck>
      <FunctionLevelLinking>[options.EnableFunctionLevelLinking]</FunctionLevelLinking>
      <EnableEnhancedInstructionSet>[options.EnableEnhancedInstructionSet]</EnableEnhancedInstructionSet>
      <FloatingPointModel>[options.FloatingPointModel]</FloatingPointModel>
      <FloatingPointExceptions>[options.FloatingPointExceptions]</FloatingPointExceptions>
      <CreateHotpatchableImage>false</CreateHotpatchableImage>
      <DisableLanguageExtensions>[options.DisableLanguageExtensions]</DisableLanguageExtensions>
      <TreatWChar_tAsBuiltInType>[options.TreatWChar_tAsBuiltInType]</TreatWChar_tAsBuiltInType>
      <ForceConformanceInForLoopScope>[options.ForceConformanceInForLoopScope]</ForceConformanceInForLoopScope>
      <RuntimeTypeInfo>[options.RuntimeTypeInfo]</RuntimeTypeInfo>
      <OpenMPSupport>[options.OpenMP]</OpenMPSupport>
      <ExpandAttributedSource>false</ExpandAttributedSource>
      <AssemblerOutput>NoListing</AssemblerOutput>
      <GenerateXMLDocumentationFiles>[options.GenerateXMLDocumentation]</GenerateXMLDocumentationFiles>
      <BrowseInformation>false</BrowseInformation>
      <CallingConvention>[options.CallingConvention]</CallingConvention>
      <CompileAs>Default</CompileAs>
      <DisableSpecificWarnings>[options.DisableSpecificWarnings]
      </DisableSpecificWarnings>
      <UndefinePreprocessorDefinitions>[options.UndefinePreprocessorDefinitions]
      </UndefinePreprocessorDefinitions>
      <AdditionalOptions>[options.AdditionalCompilerOptions]</AdditionalOptions>
      <PrecompiledHeaderFile>[options.PrecompiledHeaderThrough]</PrecompiledHeaderFile>
      <PrecompiledHeaderOutputFile>[options.PrecompiledHeaderFile]</PrecompiledHeaderOutputFile>
      <ProgramDatabaseFileName>[options.CompilerProgramDatabaseFile]</ProgramDatabaseFileName>
      <RuntimeLibrary>[options.RuntimeLibrary]</RuntimeLibrary>
      <ShowIncludes>[options.ShowIncludes]</ShowIncludes>
      <ForcedIncludeFiles>[options.ForcedIncludeFiles]</ForcedIncludeFiles>
    </ClCompile>
";

        private const string _projectConfigurationsLinkTemplate =
            @"    <Link>
      <SubSystem>[options.SubSystem]</SubSystem>
      <GenerateDebugInformation>[options.GenerateDebugInformation]</GenerateDebugInformation>
      <FullProgramDatabaseFile>[options.FullProgramDatabaseFile]</FullProgramDatabaseFile>
      <OutputFile>[options.OutputFile]</OutputFile>
      <ShowProgress>[options.ShowProgress]</ShowProgress>
      <AdditionalLibraryDirectories>[options.AdditionalLibraryDirectories]</AdditionalLibraryDirectories>
      <ManifestFile>[options.ManifestFile]</ManifestFile>
      <ProgramDatabaseFile>[options.LinkerProgramDatabaseFile]</ProgramDatabaseFile>
      <GenerateMapFile>[options.GenerateMapFile]</GenerateMapFile>
      <MapExports>[options.MapExports]</MapExports>
      <SwapRunFromCD>false</SwapRunFromCD>
      <SwapRunFromNET>false</SwapRunFromNET>
      <Driver>NotSet</Driver>
      <OptimizeReferences>[options.OptimizeReferences]</OptimizeReferences>
      <EnableCOMDATFolding>[options.EnableCOMDATFolding]</EnableCOMDATFolding>
      <ProfileGuidedDatabase>[options.ProfileGuidedDatabase]
      </ProfileGuidedDatabase>
      <LinkTimeCodeGeneration>[options.LinkTimeCodeGeneration]</LinkTimeCodeGeneration>
      <IgnoreEmbeddedIDL>false</IgnoreEmbeddedIDL>
      <TypeLibraryResourceID>1</TypeLibraryResourceID>
      <NoEntryPoint>false</NoEntryPoint>
      <SetChecksum>false</SetChecksum>
      <RandomizedBaseAddress>[options.RandomizedBaseAddress]</RandomizedBaseAddress>
      <FixedBaseAddress>[options.FixedBaseAddress]</FixedBaseAddress>
      <TurnOffAssemblyGeneration>false</TurnOffAssemblyGeneration>
      <TargetMachine>[options.TargetMachine]</TargetMachine>
      <Profile>false</Profile>
      <CLRImageType>Default</CLRImageType>
      <LinkErrorReporting>PromptImmediately</LinkErrorReporting>
      <AdditionalOptions>[options.AdditionalLinkerOptions]</AdditionalOptions>
      <AdditionalDependencies>[options.AdditionalDependencies];%(AdditionalDependencies)</AdditionalDependencies>
      <SuppressStartupBanner>[options.SuppressStartupBanner]</SuppressStartupBanner>
      <IgnoreAllDefaultLibraries>[options.IgnoreAllDefaultLibraries]</IgnoreAllDefaultLibraries>
      <IgnoreSpecificDefaultLibraries>[options.IgnoreDefaultLibraryNames]
      </IgnoreSpecificDefaultLibraries>
      <AssemblyDebug>[options.AssemblyDebug]</AssemblyDebug>
      <HeapReserveSize>[options.HeapReserveSize]</HeapReserveSize>
      <HeapCommitSize>[options.HeapCommitSize]</HeapCommitSize>
      <StackReserveSize>[options.StackReserveSize]</StackReserveSize>
      <StackCommitSize>[options.StackCommitSize]</StackCommitSize>
      <LargeAddressAware>true</LargeAddressAware>
      <MapFileName>[options.MapFileName]</MapFileName>
      <ImportLibrary>[options.ImportLibrary]</ImportLibrary>
      <FunctionOrder>[options.FunctionOrder]</FunctionOrder>
      <ModuleDefinitionFile>[options.ModuleDefinitionFile]</ModuleDefinitionFile>
      <DelayLoadDLLs>[options.DelayLoadedDLLs]</DelayLoadDLLs>
      <BaseAddress>[options.BaseAddress]</BaseAddress>
      <UACExecutionLevel>[options.UACExecutionLevel]</UACExecutionLevel>
    </Link>
";

        private const string _projectConfigurationsStaticLinkTemplate =
            @"    <Link>
      <GenerateDebugInformation>[options.GenerateDebugInformation]</GenerateDebugInformation>
      <FullProgramDatabaseFile>[options.FullProgramDatabaseFile]</FullProgramDatabaseFile>
      <EnableCOMDATFolding>[options.EnableCOMDATFolding]</EnableCOMDATFolding>
      <OptimizeReferences>[options.OptimizeReferences]</OptimizeReferences>
    </Link>
    <Lib>
      <TargetMachine>[options.TargetMachine]</TargetMachine>
    </Lib>
    <Lib>
      <SubSystem>
      </SubSystem>
    </Lib>
    <Lib>
      <LinkTimeCodeGeneration>[options.LinkTimeCodeGeneration]</LinkTimeCodeGeneration>
      <AdditionalOptions>[options.AdditionalLinkerOptions]</AdditionalOptions>
      <OutputFile>[options.OutputFile]</OutputFile>
      <AdditionalLibraryDirectories>[options.AdditionalLibraryDirectories]</AdditionalLibraryDirectories>
      <AdditionalDependencies>[options.AdditionalDependencies];%(AdditionalDependencies)</AdditionalDependencies>
    </Lib>
";

        private const string _userFileConfigurationGeneralTemplate =
            @"    <LocalDebuggerCommand>[conf.VcxprojUserFile.LocalDebuggerCommand]</LocalDebuggerCommand>
    <LocalDebuggerCommandArguments>[conf.VcxprojUserFile.LocalDebuggerCommandArguments]</LocalDebuggerCommandArguments>
    <LocalDebuggerEnvironment>[conf.VcxprojUserFile.LocalDebuggerEnvironment]</LocalDebuggerEnvironment>
    <LocalDebuggerWorkingDirectory>[conf.VcxprojUserFile.LocalDebuggerWorkingDirectory]</LocalDebuggerWorkingDirectory>
    <DebuggerFlavor>WindowsLocalDebugger</DebuggerFlavor>
";

        private const string _projectConfigurationsGeneral =
            @"  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='[conf.Name]|[platformName]'"" Label=""Configuration"">
    <ConfigurationType>[options.ConfigurationType]</ConfigurationType>
    <UseDebugLibraries>[options.UseDebugLibraries]</UseDebugLibraries>
    <_IsNativeEnvironment>[options.NativeEnvironmentVS2012]</_IsNativeEnvironment>
    <UseNativeEnvironment>[options.NativeEnvironmentVS2013]</UseNativeEnvironment>
    <CharacterSet>[options.CharacterSet]</CharacterSet>
    <UseOfMfc>[options.UseOfMfc]</UseOfMfc>
    <CLRSupport>[clrSupport]</CLRSupport>
    <WholeProgramOptimization>[options.WholeProgramOptimization]</WholeProgramOptimization>
    <PlatformToolset>[options.PlatformToolset]</PlatformToolset>
    <TrackFileAccess>[options.TrackFileAccess]</TrackFileAccess>
    <CLRSupport>[options.CLRSupport]</CLRSupport>
    <WindowsTargetPlatformVersion>[options.WindowsTargetPlatformVersion]</WindowsTargetPlatformVersion>
  </PropertyGroup>
";

        private const string _projectConfigurationsGeneral2 =
            @"  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='[conf.Name]|[platformName]'"">
    <TargetName>[options.OutputFileName]</TargetName>
    <OutDir>[options.OutputDirectory]\</OutDir>
    <IntDir>[options.IntermediateDirectory]\</IntDir>
    <TargetExt>[options.OutputFileExtension]</TargetExt>
    <GenerateManifest>[options.GenerateManifest]</GenerateManifest>
    <PostBuildEventUseInBuild>[options.PostBuildEventEnable]</PostBuildEventUseInBuild>
    <PreBuildEventUseInBuild>[options.PreBuildEventEnable]</PreBuildEventUseInBuild>
    <PreLinkEventUseInBuild>[options.PreLinkEventEnable]</PreLinkEventUseInBuild>
    <LinkIncremental>[options.LinkIncremental]</LinkIncremental>
    <OutputFile>[options.OutputFile]</OutputFile>
    <EmbedManifest>[options.EmbedManifest]</EmbedManifest>
    <IgnoreImportLibrary>[options.IgnoreImportLibrary]</IgnoreImportLibrary>
    <RunCodeAnalysis>[options.RunCodeAnalysis]</RunCodeAnalysis>
    <CustomBuildBeforeTargets>[options.CustomBuildStepBeforeTargets]</CustomBuildBeforeTargets>
    <CustomBuildAfterTargets>[options.CustomBuildStepAfterTargets]</CustomBuildAfterTargets>
    <ExecutablePath>[options.ExecutablePath]</ExecutablePath>
    <IncludePath>[options.IncludePath]</IncludePath>
    <LibraryPath>[options.LibraryPath]</LibraryPath>
    <ExcludePath>[options.ExcludePath]</ExcludePath>
    <DisableFastUpToDateCheck>[options.DisableFastUpToDateCheck]</DisableFastUpToDateCheck>
  </PropertyGroup>
";

        // Notes: 
        // Clean:
        // Visual Studio automatically cleans most files from the intermediate directory as soon as the clean command is active. However, it doesn't
        // clean them all, so we manually delete everything!
        // Also, it doesn't erase the target file and its related file extensions. We now clean them manually. Feel free to add more extensions if some were forgotten.
        // Removing unity blobs is useful if the number of blobs changes this will erase the ones that are no longer used.
        // Implementation note:
        // del *.* doesn't work and Visual studio seems to have a protection against this so we must delete explicit files or extensions...
        private const string _projectConfigurationsFastBuildMakefile =
            @"  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='[conf.Name]|[platformName]'"">
    <OutDir>[options.OutputDirectory]\</OutDir>
    <IntDir>[options.IntermediateDirectory]\</IntDir>
    <NMakeBuildCommandLine>cd [relativeMasterBffPath]
[conf.FastBuildCustomActionsBeforeBuildCommand]
[fastBuildMakeCommandBuild] </NMakeBuildCommandLine>
    <NMakeReBuildCommandLine>cd [relativeMasterBffPath]
[conf.FastBuildCustomActionsBeforeBuildCommand]
[fastBuildMakeCommandRebuild] </NMakeReBuildCommandLine>
    <NMakeCleanCommandLine>del ""[options.IntermediateDirectory]\*unity*.cpp"" >NUL 2>NUL
del ""[options.IntermediateDirectory]\*.obj"" >NUL 2>NUL
del ""[options.IntermediateDirectory]\*.a"" >NUL 2>NUL
del ""[options.IntermediateDirectory]\*.lib"" >NUL 2>NUL
del ""[options.OutputDirectory]\[conf.TargetFileFullName].exe"" >NUL 2>NUL
del ""[options.OutputDirectory]\[conf.TargetFileFullName].elf"" >NUL 2>NUL
del ""[options.OutputDirectory]\[conf.TargetFileFullName].exp"" >NUL 2>NUL
del ""[options.OutputDirectory]\[conf.TargetFileFullName].ilk"" >NUL 2>NUL
del ""[options.OutputDirectory]\[conf.TargetFileFullName].lib"" >NUL 2>NUL
del ""[options.OutputDirectory]\[conf.TargetFileFullName].pdb"" >NUL 2>NUL</NMakeCleanCommandLine>
    <NMakeOutput>[options.OutputFile]</NMakeOutput>
    <NMakePreprocessorDefinitions>[options.PreprocessorDefinitions]</NMakePreprocessorDefinitions>
    <NMakeIncludeSearchPath>[options.AdditionalIncludeDirectories]</NMakeIncludeSearchPath>
    <TargetFileName>[options.OutputFileName].exe</TargetFileName>
  </PropertyGroup>
";
    }
}
