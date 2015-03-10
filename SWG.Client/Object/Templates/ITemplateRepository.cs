using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates
{
    public interface ITemplateRepository
    {
        IFFFile LoadIff(string toLoad);
    }
}
