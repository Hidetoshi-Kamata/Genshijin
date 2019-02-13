using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using NMeCab;

namespace Genhsijin
{
    /// <summary>
    /// 変換タイプ
    /// </summary>
    public enum GenshiType
    {
        Genshijin,      // 原始人
        Gien,           // 魏延（三國無双）
    }

    /// <summary>
    /// 原始人化
    /// </summary>
    public class Genshijin
    {
        private MeCabTagger tagger;
        private GenshiType type;

        /// <summary>
        /// 解析結果から抜き出す文字列
        /// </summary>
        private Dictionary<GenshiType, int> typeSurfaces = new Dictionary<GenshiType, int>
        {
            { GenshiType.Genshijin, 7},
            { GenshiType.Gien, 6},
        };

        /// <summary>
        /// 解析結果のセパレータ
        /// </summary>
        private Dictionary<GenshiType, String> typeSeparator = new Dictionary<GenshiType, String>
        {
            { GenshiType.Genshijin, " "},
            { GenshiType.Gien, "..."},
        };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type">変換するタイプを指定</param>
        public Genshijin(GenshiType type)
        {
            this.tagger = MeCabTagger.Create();
            this.type = type;
        }

        /// <summary>
        /// 文字列を原始人化
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public String Convert(String source)
        {
            var parts = new List<String>();
            var node = tagger.ParseToNode(source);
            node = node.Next;   // 最初のノードは抜く

            while (true)
            {
                if (node.Next == null) break;
                var surfaces = node.Feature.Split(',');
                var surface = node.Surface;

                node = node.Next;

                if (surfaces[0] == "助詞") continue;  // 助詞を抜く
                if (surfaces.Count() <= 7)
                {
                    parts.Add(surface);
                }
                else
                {
                    parts.Add(surfaces[typeSurfaces[type]]);
                }
            }
            if (type == GenshiType.Gien)
            {
                parts = parts.Select(m => Strings.StrConv(m, VbStrConv.Katakana)).ToList();
            }

            return String.Join(typeSeparator[type], parts.ToArray());
        }
    }
}
