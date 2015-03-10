using SWG.Client.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates
{
    public class ArchiveRepository : IArchiveRepository
    {

        private static readonly LogAbstraction.ILogger _logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        protected Dictionary<string, TREFile> _archiveFilesCache;
        protected Dictionary<string, Tuple<TREFile.TreInfo, string>> _knownFiles;

        public string BasePath { get; set; }

        public ArchiveRepository()
        {
            _archiveFilesCache = new Dictionary<string, TREFile>();
            _knownFiles = new Dictionary<string, Tuple<TREFile.TreInfo, string>>();
        }

        public ArchiveRepository(string basePath) 
            : this()
        {
            this.BasePath = basePath;
        }

        public void LoadArchives(string directory = null, IEnumerable<string> filesToLoad = null)
        {
            if(string.IsNullOrEmpty(directory))
            {
                directory = BasePath;
            }

            if(filesToLoad == null)
            {
                filesToLoad = Directory.GetFiles(directory, "*.tre");
            }

            if(filesToLoad == null || !filesToLoad.Any())
            {
                _logger.Error("No tre files to load in directory {0}", directory);
                return;
            }

            filesToLoad = filesToLoad.OrderBy(cur => Path.GetFileName(cur), new TreFileNameComparer()).ToList();

            foreach (var treFilePath in filesToLoad)
            {
                var toLoad = Path.IsPathRooted(treFilePath) ? treFilePath : Path.Combine(BasePath, treFilePath);
                var treFile = new TREFileReader().Load(toLoad);
                if (treFile == null)
                {
                    _logger.Warn("Unable to load tre file at {0}", toLoad);
                    continue;
                }

                _archiveFilesCache.Add(toLoad, treFile);

                foreach (var info in treFile.InfoFiles)
                {
                    if (!_knownFiles.ContainsKey(info.Name))
                    {
                        _knownFiles.Add(info.Name, new Tuple<TREFile.TreInfo, string>(info, toLoad));
                    }
                }
            }

        }

        public IFFFile LoadIff(string path)
        {
            return LoadFromReader<IFFFile, IFFFileReader>(path);
        }

        public StringFile LoadStf(string path)
        {
            return LoadFromReader<StringFile, StringFileReader>(path);
        }

        public CSTBFile LoadCrCMap(string path)
        {
            return LoadFromReader<CSTBFile, CSTBFileReader>(path);
        }


        private T LoadFromReader<T,TReader>(string path)
            where T : class, ISWGFile
            where TReader : SWGFileReader<T>, new()
        {
            Tuple<TREFile.TreInfo, string> info;
            if (!_knownFiles.TryGetValue(path, out info))
            {
                return null;
            }

            using (var fileStream = File.OpenRead(info.Item2))
            {
                using (var treStream = info.Item1.Open(fileStream))
                {
                    return new TReader().Load(treStream);
                }

            }
        }

        protected class TreFileNameComparer : IComparer<string>
        {

            string[] _prefixes = new []
            {
                "hotfix",
                "patch",
                "data",
            };

            public int Compare(string x, string y)
            {
                foreach (var prefix in _prefixes)
                {
                    if (x.StartsWith(prefix) && y.StartsWith(prefix))
                    {
                        return x.CompareTo(y) * -1;
                    }

                    if (x.StartsWith(prefix))
                    {
                        return -1;
                    }

                    if (y.StartsWith(prefix))
                    {
                        return 1;
                    }
                }
                
                return x.CompareTo(y);
            }
        }
    }
}
