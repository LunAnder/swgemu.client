using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SWG.Client.Utils
{
    public class AssemblyTypesEnumerator: IEnumerable<Type>
    {

        private readonly Assembly _rootAssembly;

        public AssemblyTypesEnumerator(Assembly rootAssembly = null)
        {
            _rootAssembly = rootAssembly ?? Assembly.GetExecutingAssembly();
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return GetAllAssemblies()
                .SelectMany(
                    referencedAsyms => referencedAsyms.GetTypes()).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<Assembly> GetAllAssemblies()
        {
            var assemblies = new List<Assembly>
            {
                _rootAssembly
            };

            AddChildAssemblies(_rootAssembly, assemblies);

            return assemblies;
        }

        private static void AddChildAssemblies(Assembly parent, ICollection<Assembly> assembliesCol)
        {
            try
            {
                foreach (var asym in parent.GetReferencedAssemblies().Select(Assembly.Load).Where(asym => !assembliesCol.Contains(asym)))
                {
                    assembliesCol.Add(asym);
                    AddChildAssemblies(asym, assembliesCol);
                }
            }
            catch (FileNotFoundException) { }

        }

        public static IEnumerable<Type> CreateEnumerable(Assembly rootAssembly = null)
        {
            return new AssemblyTypesEnumerator(rootAssembly);
        } 
    }
}
