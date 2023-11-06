using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

//必须要继承Editor
public class BuildTool : Editor
{
    [MenuItem("Tools/Bundle Windows Bundle")]
    static void BundleWindowsBuild() => Build(BuildTarget.StandaloneWindows);

    [MenuItem("Tools/Bundle Android Bundle")]
    static void BundleAndroidBuild() => Build(BuildTarget.Android);

    [MenuItem("Tools/Bundle iPhone Bundle")]
    static void BundleiPhoneBuild() => Build(BuildTarget.iOS);

    static void Build(BuildTarget target)
    {
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();

        //Directory.GetDirectories//获取所有文件夹
        //Directory.GetFiles//获取所有文件
        //参数一：搜索路径
        //参数二：匹配字符串
        //参数三：搜索范围（所有文件夹 或 只搜索最上层的文件夹）
        string[] files = Directory.GetFiles(PathUtil.BuildResourcesPath, "*", SearchOption.AllDirectories);//注意：当显示我们自己创建的文件夹的时候会用左斜杠显示，显示系统的会用右斜杠，有时系统会无法识别左斜杠

        //排除meta
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta"))
                continue;

            Debug.Log("file:" + files[i]);
            AssetBundleBuild assetBundle = new AssetBundleBuild();

            //将左斜杠替换成右斜杠，并且去除空格确保路径不会出错
            string fileName = PathUtil.GetStandardPath(files[i]);
            Debug.Log("file:" + fileName);

            //资源的绝对路径（type：string数组）
            string assetName = PathUtil.GetUnityPath(fileName);
            assetBundle.assetNames = new string[] { assetName };//资源相对目录，从Assets之后截取的部分
            //Bundle存放的绝对路径（type：string）
            string bundleName = fileName.Replace(PathUtil.BuildResourcesPath, "").ToLower();
            //注意：assetBundleName不能以/开头，不然会报错
            //并且一定要加后缀，不然prefab文件会报错
            assetBundle.assetBundleName = bundleName + ".ab";

            //放入准备打成bundle的集合中
            assetBundleBuilds.Add(assetBundle);
        }

        //判断有没有streamingAssets这个文件夹，没有创建一个
        if (Directory.Exists(PathUtil.BundleOutPath))
            //参数一：文件夹路径
            //参数二：是否包含子文件，子文件夹在内（如果删除的是非空的文件夹的话会报错）
            Directory.Delete(PathUtil.BundleOutPath, true);

        Directory.CreateDirectory(PathUtil.BundleOutPath);

        //为什么不用BuildAssetBundles中（3个参数）的方法
        //因为上面的方法需要自己去资源里面设置bundle包（去给每个资源打标签）
        //参数一：bundle存储路径
        //参数二：想要打成bundle的对象
        //参数三：压缩格式
        //参数四：bundle目标平台
        BuildPipeline.BuildAssetBundles(PathUtil.BundleOutPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, target);
    }
}
