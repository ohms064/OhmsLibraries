using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.CardSystem {
    public class StringCardData : CardData {
        public string info;
    }

    public class StringIntCardData : CardData {
        public string text;
        public int value;
    }

    public class IntCardData : CardData {
        public int value;
    }
}
