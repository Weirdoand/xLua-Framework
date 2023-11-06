using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil
{
    //根目录
    private static readonly string AssetsPath = Application.dataPath;
    private static readonly string OutPath = Application.streamingAssetsPath;
    
    //需要打Bundle的目录
    public static readonly string BuildResourcesPath = AssetsPath + "/BuildResources/";
    
    //Bundle输出目录
    public static readonly string BundleOutPath = OutPath;

    /// <summary>
    /// 获取Unity的相对路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetUnityPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;

        //字符串截取（从字符串中第一次出现Assets的位置截取后面字符串）
        return path.Substring(path.IndexOf("Assets"));
    }

    /// <summary>
    /// 获取标准路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetStandardPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;

        //处理路径前后空格
        return path.Trim().Replace("\\","/");//将左斜杠替换成右斜杠
    }
}
