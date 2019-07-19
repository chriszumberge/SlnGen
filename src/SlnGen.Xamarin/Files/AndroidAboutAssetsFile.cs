using SlnGen.Core;
using System;

namespace SlnGen.Xamarin.Files
{
    public class AndroidAboutAssetsFile : ProjectFile
    {
        public AndroidAboutAssetsFile() : base("AboutAssets.txt", false, false)
        {
            FileContents =
                    String.Join(Environment.NewLine,
                    "Any raw assets you want to be deployed with your application can be placed in",
                    "this directory (and child directories) is given a Build Action of \"AndroidAsset\".",
                    "",
                    "These files will be deployed with your package and will be accessible using Android's",
                    "AssetManager, like this:",
                    "",
                    "public class ReadAsset : Activity",
                    "{",
                    "\tprotected override void OnCreate (Bundle bundle)",
                    "\t{",
                    "\t\tbase.OnCreate (bundle);",
                    "",
                    "\t\tInputStream input = Assets.Open (\"my_asset.txt\");",
                    "\t}",
                    "}",
                    "",
                    "Additionally, some Android functions will automatically load asset files:",
                    "",
                    "Typeface tf = Typeface.CreateFromAsset (Context.Assets, \"fonts/samplefont.tff\");"
                    );
        }
    }
}
