using System.Collections.Generic;

namespace WinFormsApp1.Controls
{
    public enum Moeum
    {
        A = 0,
        AE = 1,
        YA = 2,
        YAE = 3,
        EO = 4,
        E = 5,
        YEO = 6,
        YE = 7,
        O = 8,
        WA = 9,
        WAE = 10,
        OE = 11,
        YO = 12,
        U = 13,
        WEO = 14,
        WE = 15,
        WI = 16,
        YU = 17,
        EU = 18,
        UI = 19,
        I = 20
    }

    public static class Korean
    {
        private static readonly Dictionary<(Moeum, Moeum), Moeum> mergingRules = new Dictionary<(Moeum, Moeum), Moeum>
        {
            {(Moeum.O,  Moeum.A ),  Moeum.WA },
            {(Moeum.O,  Moeum.AE), Moeum.WAE},
            {(Moeum.O,  Moeum.I ),  Moeum.OE },
            {(Moeum.U,  Moeum.EO), Moeum.WEO},
            {(Moeum.U,  Moeum.E ), Moeum.WE },
            {(Moeum.U,  Moeum.I ), Moeum.WI },
            {(Moeum.EU, Moeum.I ), Moeum.UI}
        };

        private static readonly Dictionary<string, Moeum> vowelMap = new Dictionary<string, Moeum>
        {
            {"ㅏ", Moeum.A }, {"ㅐ", Moeum.AE}, {"ㅑ", Moeum.YA }, {"ㅒ", Moeum.YAE}, {"ㅓ", Moeum.EO},
            {"ㅔ", Moeum.E }, {"ㅕ", Moeum.YEO}, {"ㅖ", Moeum.YE }, {"ㅗ", Moeum.O }, {"ㅘ", Moeum.WA },
            {"ㅙ", Moeum.WAE}, {"ㅚ", Moeum.OE }, {"ㅛ", Moeum.YO }, {"ㅜ", Moeum.U }, {"ㅝ", Moeum.WEO},
            {"ㅞ", Moeum.WE }, {"ㅟ", Moeum.WI }, {"ㅠ", Moeum.YU }, {"ㅡ", Moeum.EU }, {"ㅢ", Moeum.UI },
            {"ㅣ", Moeum.I }
        };

        public static Moeum MergeMoeum(char m1, char m2)
        {
            string s1 = m1.ToString();
            string s2 = m2.ToString();
            if (!vowelMap.ContainsKey(s1) || !vowelMap.ContainsKey(s2))
                return (Moeum)(-1);

            var key = (vowelMap[s1], vowelMap[s2]);
            return mergingRules.TryGetValue(key, out Moeum merged) ? merged : (Moeum)(-1);
        }
    }
}
