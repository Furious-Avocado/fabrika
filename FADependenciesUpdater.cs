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

            foreach (var package in request.Result)
            {
                if (package.name.StartsWith("com.furiousavocado") && package.version != package.versions.latest)
                {
                    var addRequest = Client.Add(package.name);
                    while (!addRequest.IsCompleted)
                    {
                    }

                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
