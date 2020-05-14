/*
 * 長嶋
 * 
 * 下記サイトを参考にして作成しました
 * http://baba-s.hatenablog.com/entry/2014/02/24/000000_1
 * 
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Common
{

    /// <summary>
    /// @brief シーン名を定数で管理するクラスを作成するクラス
    /// </summary>
    public static class SceneNameListCreator
    {

        #region 独自で変更する部分

        //namespace部分
        private const string NameSpace = "namespace Sailing";
        //プルダウンメニューにつける名前
        private const string MenuName = "Scenes/Create Scripts/Scene Name List";
        //保存するファイルパス
        private const string FilePass = "Assets/Scripts/Define/SceneNameList.cs";
        
        #endregion

        //無効な文字を管理する配列
        private static readonly string[] Invalud_Chars =
        {
            " " , "!", "\"", "#", "$",
            "%", "&", "\'", "(", ")",
            "-", "=", "^" , "~", "\\",
            "|", "[", "{" , "@", "`",
            "]", "}", ":" , "*", ";",
            "+", "/", "?" , ".", ">",
            ",", "<"
        };

        //作成するクラス名
        private const string ClassName_Enum = "SceneNameEnum";
        private const string ClassName_String = "SceneNameString";

        //ファイル名（上が拡張子あり）
        private static readonly string FileName = Path.GetFileName(FilePass);
        //private static readonly string FileName_Without_Extension = Path.GetFileNameWithoutExtension(FilePass);

        /// <summary>
        /// @brief シーン名を定数で管理するクラスを作成する
        /// </summary>
        [MenuItem(MenuName, priority = 0)]
        public static void Create()
        {

            if (!CanCreate())
            {
                return;
            }

            CreateScript();

            EditorUtility.DisplayDialog(FileName, "作成が完了しました", "OK");

        }

        /// <summary>
        /// @brief シーン名を定数で管理するクラスを作成できるかどうかを返す
        /// </summary>
        /// <returns>作成できる場合true、できなければfalseを返す</returns>
        [MenuItem(MenuName, true)]
        public static bool CanCreate()
        {

            return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
        }

        /// <summary>
        /// @brief ファイル内の記述部分を作成する
        /// </summary>
        private static void CreateScript()
        {

            var builder = new StringBuilder();

            //namespaceの挿入
            builder.AppendLine(NameSpace);
            builder.AppendLine("{\n");

            #region Enum型の管理クラスを作成

            //クラス部分の挿入
            builder.AppendLine("\t/// <summary>");
            builder.AppendLine("\t/// @brief シーン名をEnum型で管理するクラス");
            builder.AppendLine("\t/// <summary>");
            builder.AppendFormat("\tpublic enum {0}", ClassName_Enum).AppendLine();
            builder.AppendLine("\t{\n");

            //定数部分の挿入
            foreach (var n in EditorBuildSettings.scenes
                .Select(c => Path.GetFileNameWithoutExtension(c.path))
                .Distinct()
                .Select(c => new { var = RemoveInvalidChars(c), val = c }))
            {
                builder.Append("\t\t").AppendFormat(@"{0},", n.var).AppendLine();
            }

            builder.AppendLine("\n\t}\n");

            #endregion

            #region String型の管理クラスを作成

            //クラス部分の挿入
            builder.AppendLine("\t/// <summary>");
            builder.AppendLine("\t/// @brief シーン名をstring型で管理するクラス");
            builder.AppendLine("\t/// <summary>");
            builder.AppendFormat("\tpublic static class {0}", ClassName_String).AppendLine();
            builder.AppendLine("\t{\n");

            //定数部分の挿入
            foreach (var n in EditorBuildSettings.scenes
                .Select(c => Path.GetFileNameWithoutExtension(c.path))
                .Distinct()
                .Select(c => new { var = RemoveInvalidChars(c), val = c }))
            {
                builder.Append("\t\t").AppendFormat(@"public const string {0} = ""{1}"";", n.var, n.val).AppendLine();
            }

            builder.AppendLine("\n\t}\n");

            #endregion

            //namespaceの終了
            builder.AppendLine("}");

            var directoryName = Path.GetDirectoryName(FilePass);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            File.WriteAllText(FilePass, builder.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

        }

        /// <summary>
        /// @brief 使用できない文字を削除する
        /// </summary>
        /// <param name="str">調べる文字列</param>
        /// <returns>削除後の文字列</returns>
        private static string RemoveInvalidChars(string str)
        {

            Array.ForEach(Invalud_Chars, c => str = str.Replace(c, string.Empty));

            return str;
        }

    }

}