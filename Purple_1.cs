namespace Purple_1
{
    class Purple_1
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpsRecorded;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Coefs
            {
                get
                {
                    double[] copy = new double[4];
                    this._coefs.CopyTo(copy, 0);
                    return copy;
                }
            }

            public int[,] Marks
            {
                get
                {
                    int[,] copy = new int[4, 7];
                    Array.Copy(this._marks, copy, this._marks.Length);
                    return copy;
                }
            }

            public double TotalScore
            {
                get
                {
                    double score = 0.0;
                    for (int jump = 0; jump < 4; jump++)
                    {
                        int worstMark = int.MaxValue;
                        int bestMark = int.MinValue;
                        int jumpMark = 0;
                        for (int judge = 0; judge < 7; judge++)
                        {
                            int currentMark = this.Marks[jump, judge];
                            if (currentMark > bestMark)
                                bestMark = currentMark;
                            if (currentMark < worstMark)
                                worstMark = currentMark;
                            jumpMark += currentMark;
                        }
                        jumpMark -= worstMark;
                        jumpMark -= bestMark;
                        score += jumpMark * this.Coefs[jump];
                    }
                    return score;
                }
            }

            public Participant(string name, string surname)
            {
                this._name = name;
                this._surname = surname;
                this._coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                this._marks = new int[,]
                {
                    { 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 0, 0, 0, 0, 0 },
                };

                this._jumpsRecorded = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null)
                    return;
                if (coefs.Length != 4)
                    return;

                foreach (double coeff in coefs)
                {
                    if (coeff > 3.5 || coeff < 2.5)
                        return;
                }

                Array.Copy(coefs, this._coefs, 4);
            }

            public void Jump(int[] marks)
            {
                if (marks == null)
                    return;
                if (marks.Length != 7)
                    return;
                if (this._jumpsRecorded >= 4)
                    return;

                foreach (int mark in marks)
                {
                    if (mark > 6 || mark < 1)
                        return;
                }

                for (int judge = 0; judge < 7; judge++)
                {
                    this._marks[this._jumpsRecorded, judge] = marks[judge];
                }

                this._jumpsRecorded++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null)
                    return;

                Participant[] sorted = array.OrderByDescending(p => p.TotalScore).ToArray();
                array = sorted;
            }
        }
    }
}
