using DataStructures.CardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace UnityEngine.DataStructures.CardSystem {
    [System.Serializable]
    public class Card<T> {
        public T data;
        [DisableInPlayMode]
        public int category;
        public int appearance;

        public bool Corresponds<W> ( Card<W> other ) {
            return other.category == category;
        }

        public bool Corresponds ( int other ) {
            return other == category;
        }
    }

    #region CardData

    public class StringCardData {
        public string info;
    }

    public class StringIntCardData {
        public string text;
        public int value;
    }

    public class IntCardData {
        public int value;
    }
    #endregion
}