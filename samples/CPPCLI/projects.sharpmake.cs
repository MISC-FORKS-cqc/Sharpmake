using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpmake;

namespace CLR_SharpmakeTest
{
    public class CommonProject : Project
    {
        public CommonProject()
        {
            RootPath = @"[project.SharpmakeCsPath]\codebase\[project.Name]";
            SourceRootPath = @"[project.RootPath]";
            AddTargets(Common.CommonTarget);
        }

        [Configure()]
        public virtual void ConfigureAll(Configuration conf, Target target)
        {
            conf.ProjectPath = @"[project.SharpmakeCsPath]\projects\";
            conf.ProjectFileName = @"[project.Name].[target.DevEnv].[target.Platform].[target.Framework]";
            conf.IntermediatePath = @"[conf.ProjectPath]\temp\[target.DevEnv]\[target.Platform]\[target]";
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            if (target.Optimization == Optimization.Debug)
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDebugDLL);
            else
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDLL);
        }
    }

    public class CommonCSharpProject : CSharpProject
    {
        public CommonCSharpProject()
        {
            RootPath = @"[project.SharpmakeCsPath]\codebase\[project.Name]";
            SourceRootPath = @"[project.RootPath]";

            AddTargets(Common.CommonTarget);
        }

        [Configure()]
        public virtual void ConfigureAll(Configuration conf, Target target)
        {
            conf.ProjectPath = @"[project.SharpmakeCsPath]\projects";
            conf.ProjectFileName = @"[project.Name].[target.DevEnv].[target.Framework]";
            conf.IntermediatePath = @"[conf.ProjectPath]\temp\[target.DevEnv]\[target.Framework]\[target]";
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            if (target.Optimization == Optimization.Debug)
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDebugDLL);
            else
                conf.Options.Add(Options.Vc.Compiler.RuntimeLibrary.MultiThreadedDLL);

            conf.Options.Add(Sharpmake.Options.CSharp.TreatWarningsAsErrors.Enabled);
        }
    }

    [Sharpmake.Generate]
    public class TestCSharpConsole : CommonCSharpProject
    {
        public TestCSharpConsole() { }

        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.DotNetConsoleApp;
            conf.AddPrivateDependency<CLR_CPP_Proj>(target);
        }
    }

    [Sharpmake.Generate]
    public class OtherCSharpProj : CommonCSharpProject
    {
        public OtherCSharpProj() { }
    }

    [Sharpmake.Generate]
    public class CLR_CPP_Proj : CommonProject
    {
        public CLR_CPP_Proj()
        {
            Name = "CLRCPPProj";
        }

        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.ReferencesByName.Add("System", "System.Data", "System.Xml");
            conf.ReferencesByPath.Add(@"..\..\..\..\..\external\MySql\v2.0\MySql.Data.Entity.dll");
            conf.AddPrivateDependency<OtherCSharpProj>(target, DependencySetting.OnlyBuildOrder);
            conf.AddPrivateDependency<TheEmptyCPPProject>(target);
        }
    }
    [Sharpmake.Generate]
    public class TheEmptyCPPProject : CommonProject
    {
        public TheEmptyCPPProject()
        {
            Name = "theEmptyCPPProject";
        }

        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.Lib;
            conf.Options.Add(Options.Vc.Compiler.Exceptions.EnableWithSEH);
        }
    }
}
