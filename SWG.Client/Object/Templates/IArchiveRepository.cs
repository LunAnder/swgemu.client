using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates
{
    public interface IArchiveRepository
    {
        void LoadArchives(string directory = null, IEnumerable<string> filesToLoad = null);
        IFFFile LoadIff(string path);

        StringFile LoadStf(string path);
    }
}
