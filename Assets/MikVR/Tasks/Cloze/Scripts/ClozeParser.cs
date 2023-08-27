using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ClozeText
{
    internal class ClozeParser
    {
        private static readonly int PREFIX_MIN_LENGTH = 10;
        private static readonly string TAG_DD = "cl-dd";
        private static readonly string TAG_IN = "cl-in";
        private static readonly string TAG_END = "cl-end";
        private static readonly string KEYWORD_POOL = "pool";
        private static readonly string KEYWORD_DEPLETE = "deplete";
        private static readonly string CONTENT_PATTERN = @"<&1[^>]*>(.*?)</&1>";

        private List<Cloze> clozes = new List<Cloze>();
        private List<WordPool> wordPools = new List<WordPool>();
        private int startIndexPoolDescriptions = -1;

        internal List<Cloze> VarOut_GetClozes() { return this.clozes; }
        internal List<WordPool> VarOut_GetPools() { return this.wordPools; }
        internal string VarOut_FinalText { get; private set; } = string.Empty;

        internal void ParseClozes(string text)
        {
            clozes.Clear();
            startIndexPoolDescriptions = -1;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals('<') && (text.Length > i + PREFIX_MIN_LENGTH))
                {
                    string splitPrefix = text.Substring(i, PREFIX_MIN_LENGTH);
                    TryGettingCloze(text, i, splitPrefix, TAG_DD, Cloze.Type.Dropdown);
                    TryGettingCloze(text, i, splitPrefix, TAG_IN, Cloze.Type.InputField);
                    TryGettingPool(text, i, splitPrefix);
                }
            }

            VarOut_FinalText = new string(text);
            // remove pool descriptions
            if ((startIndexPoolDescriptions >= 0) && (startIndexPoolDescriptions < VarOut_FinalText.Length))
            {
                VarOut_FinalText = VarOut_FinalText.Substring(0, startIndexPoolDescriptions);
            }
            // create pools that were not defined at the bottom yet
            this.CreateMissingPools();
            // fillup pool with missing words
            this.FillupPoolWithMissingWords();
            // remove duplicates if necessary
            this.RemoveDuplicatesFromPools();
            // get longest word in pools
            this.GetLongestWordInPools();
            // remove raw clozes and replace them with empty text
            for (int i = 0; i < clozes.Count; i++)
            {
                VarOut_FinalText = this.RemoveRawClozesAndSetPlaceholders(i, VarOut_FinalText);
            }
            Debug.Log("CLOZES: " + this.clozes.Count + " " + this.wordPools.Count);
        }

        private void TryGettingCloze(string text, int i, string splitPrefix, string tag, Cloze.Type clozeType)
        {
            string startTag = "<" + tag;
            if (splitPrefix.StartsWith(startTag))
            {
                string regexPatternContent = CONTENT_PATTERN.Replace("&1", tag);
                Cloze cloze = new Cloze();
                cloze.ClozeType = clozeType;
                cloze.StartIndexRaw = i;
                cloze.StartIndexFinal = i;
                cloze.EndTag = "</" + tag + ">";

                for (int j = i; j < text.Length; j++)
                {
                    string substring = text.Substring(j, cloze.EndTag.Length);
                    if (substring.Equals(cloze.EndTag))
                    {
                        cloze.EndIndexRaw = j + cloze.EndTag.Length;
                        cloze.EndIndexFinal = j + cloze.EndTag.Length;
                        cloze.ClozeRaw = text.Substring(i, (j + cloze.EndTag.Length) - i);
                        break;
                    }
                }

                // get pool nr:
                cloze.PoolRaw = GetParameterInt(cloze.PoolRaw, KEYWORD_POOL, cloze.ClozeRaw);
                if (int.TryParse(cloze.PoolRaw, out int value)) { cloze.PoolIndex = value; }

                // content inside tag:
                Match match = Regex.Match(cloze.ClozeRaw, regexPatternContent);
                if (match.Success)
                {
                    cloze.RightWord = match.Groups[1].Value;
                }
                this.clozes.Add(cloze);
            }
        }

        private void TryGettingPool(string text, int i, string splitPrefix)
        {
            string tag = TAG_END;
            string startTag = "<" + tag;
            if (splitPrefix.StartsWith(startTag))
            {
                if (startIndexPoolDescriptions == -1) { startIndexPoolDescriptions = i; }
                string regexPatternContent = CONTENT_PATTERN.Replace("&1", tag);
                WordPool pool = new WordPool();
                int startIndex = i;
                string endTag = "</" + tag + ">";

                for (int j = i; j < text.Length; j++)
                {
                    if (text.Substring(j, endTag.Length).Equals(endTag))
                    {
                        pool.PoolTagRaw = text.Substring(i, (j + endTag.Length) - i);
                        break;
                    }
                }

                // get pool nr:
                pool.PoolIndexRaw = GetParameterInt(pool.PoolIndexRaw, KEYWORD_POOL, pool.PoolTagRaw);
                if (int.TryParse(pool.PoolIndexRaw, out int value)) { pool.Index = value; }

                // get deplete:
                pool.Deplete = pool.PoolTagRaw.Contains(KEYWORD_DEPLETE + ":true");

                // content inside tag:
                Match match = Regex.Match(pool.PoolTagRaw, regexPatternContent);
                if (match.Success)
                {
                    pool.Pool = match.Groups[1].Value.Split("|").ToList();
                }

                WordPool targetWordPool = this.wordPools.FirstOrDefault(wp => wp.Index == pool.Index);
                if (targetWordPool == null)
                {
                    this.wordPools.Add(pool);
                }
                else
                {
                    targetWordPool.Pool.AddRange(pool.Pool);
                }
            }
        }

        private string GetParameterInt(string parameterRaw, string parameterName, string rawString)
        {
            if (string.IsNullOrEmpty(parameterRaw) && rawString.Contains(parameterName))
            {
                for (int i = 0; i < rawString.Length; i++)
                {
                    string substring = rawString.Substring(i, parameterName.Length);
                    if (substring.Equals(parameterName))
                    {
                        for (int j = i + parameterName.Length; j < rawString.Length; j++)
                        {
                            char c = rawString[j];
                            if (rawString[j].Equals(':')) { }
                            else if (char.IsDigit(rawString[j]))
                            {
                                parameterRaw += rawString[j];
                            }
                            else { return parameterRaw; }
                        }
                    }
                }
            }
            return parameterRaw;
        }

        private void FillupPoolWithMissingWords()
        {
            for (int i = 0; i < this.clozes.Count; i++)
            {
                WordPool wordPool = this.wordPools.FirstOrDefault(wp => wp.Index == clozes[i].PoolIndex);
                if (wordPool == null)
                {
                    wordPool = new WordPool();
                    wordPool.Index = clozes[i].PoolIndex;
                }
                wordPool.Pool.Add(this.clozes[i].RightWord);
            }
        }

        private void RemoveDuplicatesFromPools()
        {
            for (int i = 0; i < this.wordPools.Count; i++)
            {
                if (!this.wordPools[i].Deplete)
                {
                    wordPools[i].Pool = wordPools[i].Pool.Distinct().ToList();
                }
            }
        }

        private void GetLongestWordInPools()
        {
            for (int i = 0; i < this.wordPools.Count; i++)
            {
                string longestWord = this.wordPools[i].Pool.OrderByDescending(s => s.Length).FirstOrDefault();
                this.wordPools[i].LongestWord = longestWord;
            }
        }

        private void CreateMissingPools()
        {
            for(int i = 0; i < this.clozes.Count; i++)
            {
                WordPool wordPool = this.wordPools.FirstOrDefault(wp => wp.Index == clozes[i].PoolIndex);
                if(wordPool == null) {
                    wordPool = new WordPool();
                    wordPool.Index = clozes[i].PoolIndex;
                    wordPool.Pool.Add(clozes[i].RightWord);
                    this.wordPools.Add(wordPool);
                }
            }
        }

        private string RemoveRawClozesAndSetPlaceholders(int clozeIndex, string finalText)
        {
            // get right pool
            WordPool wordPool = this.wordPools.FirstOrDefault(wp => wp.Index == clozes[clozeIndex].PoolIndex);

            string fillerText = string.Empty;
            for (int i = 0; i < wordPool.LongestWord.Length; i++) { fillerText += "_"; }

            // remove raw cloze tag
            int lengthRawCloze = clozes[clozeIndex].ClozeRaw.Length;
            int lengthLongestWord = wordPool.LongestWord.Length;
            int difference = lengthLongestWord - lengthRawCloze;

            finalText = finalText.Remove(clozes[clozeIndex].StartIndexFinal, clozes[clozeIndex].ClozeRaw.Length);
            finalText = finalText.Insert(clozes[clozeIndex].StartIndexFinal, fillerText);

            for (int i = clozeIndex + 1; i < this.clozes.Count; i++)
            {
                this.clozes[i].StartIndexFinal += difference;
                this.clozes[i].EndIndexFinal += difference;
            }

            return finalText;
        }
    }
    internal class WordPool
    {
        internal int Index { get; set; } = -1;
        internal bool Deplete { get; set; } = false;
        internal string PoolTagRaw { get; set; } = string.Empty;
        internal string PoolIndexRaw { get; set; } = string.Empty;
        internal List<string> Pool { get; set; } = new List<string>();
        internal string LongestWord { get; set; } = string.Empty;
    }

    internal class Cloze
    {
        internal enum Type
        {
            Dropdown = 0,
            InputField = 1,
        }

        internal Type ClozeType { get; set; } = Type.Dropdown;
        internal int StartIndexRaw { get; set; } = 0;
        internal int EndIndexRaw { get; set; } = 0;
        internal int StartIndexFinal { get; set; } = 0;
        internal int EndIndexFinal { get; set; } = 0;
        internal string ClozeRaw { get; set; } = string.Empty;
        internal string PoolRaw { get; set; } = string.Empty;
        internal int PoolIndex { get; set; } = -1;
        internal string RightWord { get; set; } = string.Empty;
        internal string EndTag { get; set; } = string.Empty;
    }
}
