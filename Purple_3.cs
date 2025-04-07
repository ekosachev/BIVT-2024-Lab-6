namespace Lab_6
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _scoresRecorded;
            private bool _scoresFilled;
            private bool _placesFilled;

            public string Name => _name;
            public string Surname => _surname;
            public double[]? Marks
            {
                get
                {
                    if (this._marks == null)
                        return null;
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int[]? Places
            {
                get
                {
                    if (this._marks == null)
                        return null;
                    int[] copy = new int[_places.Length];
                    Array.Copy(_places, copy, _places.Length);
                    return copy;
                }
            }

            public int Score => this.Places != null ? this.Places.Sum() : 0;
            private double TotalMarks => this.Marks != null ? this.Marks.Sum() : 0;
            private int BestPlace => this.Places != null ? this.Places.Min() : int.MaxValue;

            public void Print()
            {
                System.Console.WriteLine(
                    $"{this.Name}\t{this.Surname}\t\t{this.Score}\t{String.Join(", ", this.Places)}\t{this.TotalMarks}"
                );
            }

            public Participant(string name, string surname)
            {
                this._name = name;
                this._surname = surname;
                this._marks = new double[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
                this._places = new int[] { 0, 0, 0, 0, 0, 0, 0 };
                this._scoresRecorded = 0;
                this._scoresFilled = false;
                this._placesFilled = false;
            }

            public void Evaluate(double result)
            {
                if (this._scoresRecorded >= 7)
                    return;
                if (result < 0.0 || result > 6.0)
                    return;
                if (this._marks == null)
                    this._marks = new double[7];
                this._marks[this._scoresRecorded++] = result;
                if (this._scoresRecorded >= 7)
                    this._scoresFilled = true;
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null)
                    return;
                if (participants.Length == 0)
                    return;
                if (participants.Any(p => p._marks == null || p._places == null))
                    return;
                if (participants.Any(p => !p._scoresFilled))
                    return;

                double[,] marksMatrix = new double[participants.Length, 7];

                for (int participant = 0; participant < participants.Length; participant++)
                {
                    for (int judge = 0; judge < 7; judge++)
                    {
                        marksMatrix[participant, judge] = participants[participant]._marks[judge];
                    }
                }

                for (int participant = 0; participant < participants.Length; participant++)
                {
                    for (int judge = 0; judge < 7; judge++)
                    {
                        int place = 1;
                        for (
                            int otherParticipant = 0;
                            otherParticipant < participants.Length;
                            otherParticipant++
                        )
                        {
                            if (
                                marksMatrix[otherParticipant, judge]
                                > marksMatrix[participant, judge]
                            )
                                place++;
                        }
                        participants[participant]._places[judge] = place;
                    }
                    participants[participant]._placesFilled = true;
                }

                Participant[] sortedArray = participants.OrderBy(p => p._places.Last()).ToArray();
                Array.Copy(sortedArray, participants, sortedArray.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null)
                    return;
                if (array.Length < 2)
                    return;
                if (array.Any(p => !p._placesFilled))
                    return;

                if (array.Any(p => p.Places == null) || array.Any(p => p.Marks == null))
                    return;

                Participant[] sortedArray = array
                    .OrderBy(p => p.Score)
                    .ThenBy(p => p.Places.Min())
                    .ThenByDescending(p => p.TotalMarks)
                    .ToArray();
                Array.Copy(sortedArray, array, sortedArray.Length);
            }
        }
    }
}
