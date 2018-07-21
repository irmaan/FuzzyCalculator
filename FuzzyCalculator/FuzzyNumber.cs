using System;

namespace Fuzzy
{
    class FuzzyNumber
    {
        private float _c, _a, _b;
        public float[] vect;

        public float C
        {
            get { return _c; }
        }

        public float A
        {
            get { return _a; }
        }

        public float B
        {
            get { return _b; }
        }

        public FuzzyNumber(float c, float a, float b, int d)
        {
            //if (a < 0 || b < 0)
            //    throw new Exception();
            _c = c;
            _a = a;
            _b = b;
            vect = new float[d];
        }

    }
}
