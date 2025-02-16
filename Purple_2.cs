namespace Purple_2
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[] _marks;
            private int _distance;
            private bool _hasJumped;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    int[] copy = new int[5];
                    Array.Copy(_marks, copy, 5);
                    return copy;
                }
            }

            public int Result
            {
                get
                {
                    int stylePoints = 0;
                    int bestMark = int.MinValue;
                    int worstMark = int.MaxValue;

                    for (int judge = 0; judge < 5; judge++)
                    {
                        int currentMark = Marks[judge];
                        if (currentMark > bestMark)
                            bestMark = currentMark;
                        if (currentMark < worstMark)
                            worstMark = currentMark;

                        stylePoints += currentMark;
                    }

                    stylePoints -= worstMark;
                    stylePoints -= bestMark;

                    int distancePoints = int.Max(0, 60 - 2 * (Distance - 120));

                    return stylePoints + distancePoints;
                }
            }

            public Participant(string name, string surname)
            {
                this._name = name;
                this._surname = surname;
                this._marks = new int[5] { 0, 0, 0, 0, 0 };
                this._distance = 0;
            }

            public void Jump(int distance, int[] marks)
            {
                if (this._hasJumped)
                    return;
                if (marks == null)
                    return;
                if (marks.Length != 5)
                    return;

                if (distance < 0)
                    return;

                this._distance = distance;
                Array.Copy(marks, this._marks, 5);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null)
                    return;

                Participant[] sorted = array.OrderByDescending(p => p.Result).ToArray();
                array = sorted;
            }
        }
    }
}
