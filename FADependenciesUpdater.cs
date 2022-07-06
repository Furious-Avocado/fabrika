using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.PackageManager;

namespace FuriousAvocado.Editor
{
    public static class FADependenciesUpdater
    {
        public static void UpdateToLatest()
        {
            var request = Client.List();
            while (!request.IsCompleted)
            {
            }

            Dictionary<string, string> faDepsWithVersions = new Dictionary<string, string>();

            foreach (var package in request.Result)
            {
                if (package.name.StartsWith("com.furiousavocado"))
                {
                    faDepsWithVersions[package.name] = package.versions.latest;

                    if (package.version != package.versions.latest)
                    {
                        var addRequest = Client.Add(package.name);
                        while (!addRequest.IsCompleted)
                        {
                        }
                    }
                }
            }

            foreach (string fileName in Directory.EnumerateFiles("Assets/", "package.json", SearchOption.AllDirectories))
            {
                var packageJson = JObject.Parse(File.ReadAllText(fileName));
                var depsArray = packageJson["dependencies"];
                if (depsArray == null)
                {
                    continue;
                }

                foreach (KeyValuePair<string, string> dep in faDepsWithVersions)
                {
                    depsArray[dep.Key] = dep.Value;
                }

                File.WriteAllText(fileName, packageJson.ToString());
            }

            AssetDatabase.Refresh();
        }
    }
}
