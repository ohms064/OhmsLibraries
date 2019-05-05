﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.CardSystem {
    public class Range {
        //Los rangos definene el límite superior (no incluyente) y todos empiezan en cero.
        private int[] ranges;
        public Range ( int[] ranges ) {
            this.ranges = ranges;
        }

        public int GetRange ( int value ) {
            int range = 0;
            int accumm = ranges[range] - 1;
            while ( range < ranges.Length - 1 ) {
                if ( accumm >= value ) {
                    return range;
                }
                accumm += ranges[range + 1];
                range++;
            }
            return range;
        }

        public void ModifyRange ( int range, int value ) {
            ranges[range] += value;
        }
    }
}
