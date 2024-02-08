using System;
using System.ComponentModel;

namespace OfflineCalc2
{
    public class BigNum
    {
        private static readonly Dictionary<string, int> values =
            new (){
                  { "", 0 },{ "K", 3},{ "M", 6},{ "B", 9},{ "T", 12},{ "AA", 15},{ "BB", 18},{ "CC", 21},{ "DD", 24},{ "EE", 27},{ "FF", 30},{
    "GG", 33},{ "HH", 36},{ "II", 39},{ "JJ", 42},{ "KK", 45},{ "LL", 48},{ "MM", 51},{ "NN", 54},{ "OO", 57},{
    "PP", 60},{ "QQ", 63},{ "RR", 66},{ "SS", 69},{ "TT", 72},{ "UU", 75},{ "VV", 78},{ "WW", 81},{ "XX", 84},{
    "YY", 87},{ "ZZ", 90},{
    "AAA", 93} };
        public double firstPart { get; set; } = 0.0;
        public int secondPart { get; set; } = 0;

        public BigNum(double firstPart, int secondPart) {
            this.firstPart = firstPart;
            this.secondPart = secondPart;
        }
        public BigNum(string num)
        {
            firstPart = 0.0;
            secondPart = 0;
            if (num is null || num == "")
            {
                return;
            }
            num = num.Replace(',', '.');
            String[] tmp = num.Split();

            if (tmp.Length > 1)
            {
                secondPart = values[tmp[1].ToUpper()];        
            }
            firstPart = float.Parse(tmp[0]);
            this.NormalizeNum();
        }

        public void NormalizeNum()
        {
            if (firstPart == 0) return;

            while (firstPart >= 10)
            {
                secondPart++;
                firstPart /= 10;
            }
            while (firstPart < 1)
            {
                secondPart--;
                firstPart *= 10;
            }
            return;
        }

        public static BigNum AddNums(BigNum numA, BigNum numB)
        {
            
            if (numB.secondPart > numA.secondPart)
            {
                (numA, numB) = (numB, numA);
            }

            int diff = numA.secondPart - numB.secondPart;
            if (diff > 2) return numA;

            BigNum res = new BigNum(numA.firstPart* Convert.ToSingle(Math.Pow(10,diff)) +
                numB.firstPart, numB.secondPart);
            res.NormalizeNum();
            return res;
        }
        public static BigNum DivideNums(BigNum numA, BigNum numB)
        {
            BigNum res = new BigNum(numA.firstPart / numB.firstPart,
                numA.secondPart - numB.secondPart);
            res.NormalizeNum();
            return res;
        }

        public static BigNum MinNum(BigNum numA, BigNum numB)
        {
            numA.NormalizeNum();
            numB.NormalizeNum();

            if (numA.secondPart == numB.secondPart)
                return numA.firstPart < numB.firstPart ? numA : numB;
            return numA.secondPart < numB.secondPart ? numA : numB;
        }

        public static BigNum MaxNum(BigNum numA, BigNum numB)
        {
            numA.NormalizeNum();
            numB.NormalizeNum();

            if (numA.secondPart == numB.secondPart)
                return numA.firstPart > numB.firstPart ? numA : numB;
            return numA.secondPart > numB.secondPart ? numA : numB;
        }

        public string ReverseTranslate()
        {
            string res = (this.firstPart *
                Convert.ToSingle(Math.Pow(10, this.secondPart % 3))).ToString("n2");
            var myKey = values.FirstOrDefault(x => x.Value == this.secondPart/3*3).Key;
            res += " " + myKey;

            return res;
        }
    }

    
    
}
