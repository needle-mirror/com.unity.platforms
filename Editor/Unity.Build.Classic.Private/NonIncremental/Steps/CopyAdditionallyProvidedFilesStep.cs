using NiceIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Bee.Core;

namespace Unity.Build.Classic
{
    public sealed class CopyAdditionallyProvidedFilesStep : BuildStepBase
    {
        public override BuildResult Run(BuildContext context)
        {
            var classicSharedData = context.GetValue<ClassicSharedData>();

            foreach (var customizer in classicSharedData.Customizers)
            {
                customizer.RegisterAdditionalFilesToDeploy((from, to) =>
                {
                    new NPath(from).MakeAbsolute().Copy(new NPath(to).MakeAbsolute().EnsureParentDirectoryExists());
                });
            }
            return context.Success();
        }
    }
}
