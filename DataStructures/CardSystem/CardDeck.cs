
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DataStructures.CardSystem {
    public class CardDeck<T> where T : CardData {
        public Card<T>[] stack;
        private Range range;
        private int total;
        private int[] percentages;
        private bool infinite = true;
        public int Lenght {
            get {
                if ( infinite ) {
                    return -1;
                }
                return total;
            }
        }
        public CardDeck ( params Card<T>[] cards ) {
            this.stack = cards;
            percentages = new int[stack.Length];
            total = 0;
            for ( int i = 0; i < percentages.Length; i++ ) {
                percentages[i] = stack[i].appearance;
                total += percentages[i];
            }
        }

        public CardDeck ( bool infinite, params Card<T>[] cards ) : this( cards ) {
            this.infinite = infinite;
        }

        public CardDeck ( bool infinite, int overrideAppearance, params Card<T>[] cards ) {
            this.stack = cards;
            percentages = new int[stack.Length];
            total = 0;
            for ( int i = 0; i < percentages.Length; i++ ) {
                percentages[i] = overrideAppearance;
                total += percentages[i];
            }
            range = new Range( percentages );
            this.infinite = infinite;
        }

        public Card<T> DrawCard () {
            var next = UnityEngine.Random.Range( 0, total );
            int i = range.GetRange( next );
            if ( !infinite ) {
                range.ModifyRange( i, -1 );
                total--;
            }
            return stack[i];
        }

        public Card<T>[] DrawCard ( int t ) {
            Card<T>[] cards = new Card<T>[t];
            for ( int i = 0; i < t; i++ ) {
                cards[i] = DrawCard();
            }
            return cards;
        }

        public void Reset () {
            range = new Range( percentages );
        }
    }

    public partial class Card<T> where T : CardData {
        internal int appearance;
    }
}
