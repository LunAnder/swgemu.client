using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared
{
    public class SharedIntangibleObjectTemplate : SharedObjectTemplate
    {
        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public override void ReadFile(IFFFile File, ITemplateRepository repo)
        {
            if (File.Root.Type != "SITN")
            {
                _Logger.Warn("Got {0} when expecting SHOT", File.Root.Type);
                return;
            }

            ReadNode(File.Root, repo, false);
        }

    }
}
